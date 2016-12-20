using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Weather.Model.NextDaysWeather;
using Weather.Model.SearchResponse;
using Weather.Model.WeatherResponse;

namespace Weather.WP.Utilities
{
    public class WeatherService
    {
        private static string API_URL = "http://api.openweathermap.org/data/2.5/";
        private static string APP_ID = "10e242e0f49644f3c116c9b80ce1ce04";
        private static int NEXT_DAYS_NUMBER = 16;
               
        public WeatherService() { }

        public async Task<List<City>> GetCitiesByName(String name)
        {
            var cities = new TaskCompletionSource<List<City>>();

            var client = new RestClient(API_URL);
            var request = new RestRequest("find", Method.GET);
            request.AddParameter("q", name);
            request.AddParameter("type", "like");
            request.AddParameter("appid", APP_ID);
            request.AddHeader("Accept", "application/json");

            client.ExecuteAsync(request, restResponse =>
            {
                List<City> cityList = new List<City>();

                if (restResponse.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    MessageBox.Show("Wystąpił błąd podczas pobierania danych.", "", MessageBoxButton.OK);
                    return;
                }
                try
                {
                    var json = JObject.Parse(restResponse.Content).SelectToken("list").ToString();
                    cityList = JsonConvert.DeserializeObject<List<City>>(json).Where(x => x.Id > 0).ToList();
                }
                catch { }
                finally
                {
                    cities.SetResult(cityList);
                }              
                
            });

            return await cities.Task;
        }
        
        public async Task<CityWeather> GetWeatherForCity(int cityId)
        {
            var weather = new TaskCompletionSource<CityWeather>();
            var client = new RestClient(API_URL);
            var request = new RestRequest("weather", Method.GET);
            request.AddParameter("id", cityId);
            request.AddParameter("lang", "pl");
            request.AddParameter("appid", APP_ID);
            request.AddParameter("units", "metric");
            request.AddHeader("Accept", "application/json");

            client.ExecuteAsync(request, restResponse =>
            {
                if (restResponse.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    MessageBox.Show("Wystąpił błąd podczas pobierania danych.", "", MessageBoxButton.OK);
                    return;
                }
                var json = restResponse.Content;
                if (String.IsNullOrEmpty(json)) return;
                weather.SetResult(JsonConvert.DeserializeObject<CityWeather>(json));                
            });

            return await weather.Task;
        }

        public async Task<List<NextDaysWeather>> GetWeatherForNextDays(int cityId)
        {
            var daysWeather = new TaskCompletionSource<List<NextDaysWeather>>();
            var client = new RestClient(API_URL);
            var request = new RestRequest("forecast/daily", Method.GET);
            request.AddParameter("id", cityId);
            request.AddParameter("lang", "pl");
            request.AddParameter("appid", APP_ID);
            request.AddParameter("units", "metric");
            request.AddParameter("cnt", NEXT_DAYS_NUMBER);
            request.AddHeader("Accept", "application/json");

            client.ExecuteAsync(request, restResponse =>
            {
                if (restResponse.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    MessageBox.Show("Wystąpił błąd podczas pobierania danych.", "", MessageBoxButton.OK);
                    return;
                }
                var json = JObject.Parse(restResponse.Content).SelectToken("list").ToString();
                if (String.IsNullOrEmpty(json)) return;
                var days = JsonConvert.DeserializeObject<List<NextDaysWeather>>(json);
                //we need to skip first day (we want next days)
                days.RemoveAt(0);
                daysWeather.SetResult(days);
            });

            return await daysWeather.Task;
        }
    }
}
