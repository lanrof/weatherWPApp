using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Weather.Model.SearchResponse;

namespace Weather.Model.WeatherResponse
{
    public class CityWeather : City
    {
        public List<Weather> Weather { get; set; }
        [JsonProperty(PropertyName = "main")]
        public WeatherMainParameters WeatherMainParameters { get; set; }
        public double Visibility { get; set; }
        public Wind Wind { get; set; }
        public Clouds Clouds { get; set; }
    }
}
