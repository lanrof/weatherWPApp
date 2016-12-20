using Caliburn.Micro;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Weather.Model.NextDaysWeather;
using Weather.WP.Utilities;

namespace Weather.WP.ViewModels
{
    public class WeatherViewModel : PropertyChangedBase
    {
        private static string ICON_URL = "http://openweathermap.org/img/w/";

        #region fields 
        private readonly INavigationService _navigationService;
        private int _cityId;
        private string _cityName;
        private string _iconUrl;
        private double _temperature;
        private string _description;
        private int _sunrise;
        private int _sunset;
        private string _humadity;
        private string _pressure;
        private string _wind;
        private string _cloudiness;
        private string _visibility;
        private ObservableCollection<NextDaysWeather> _nextDaysWeather;
        private bool _isLoading;
        #endregion

        #region properties
        public int CityId
        {
            get { return _cityId; }
            set
            {
                if (Equals(value, _cityId))
                {
                    return;
                }
                _cityId = value;
                NotifyOfPropertyChange(() => CityId);
                LoadWeatherDetails();
            }
        }
        public string CityName
        {
            get { return _cityName; }
            set
            {
                if (Equals(value, _cityName))
                {
                    return;
                }
                _cityName = value;
                NotifyOfPropertyChange(() => CityName);
            }
        }
        public double Temperature
        {
            get { return _temperature; }
            set
            {
                if (Equals(value, _temperature))
                {
                    return;
                }
                _temperature = value;
                NotifyOfPropertyChange(() => Temperature);
                NotifyOfPropertyChange(() => TemperatureWithSymbol);
            }
        }

        public string TemperatureWithSymbol
        {
            get { return String.Format("{0} {1}", Temperature, "°C"); }
        }

        public string IconUrl
        {
            get { return _iconUrl; }
            set
            {
                if (Equals(value, _iconUrl))
                {
                    return;
                }
                _iconUrl = value;
                NotifyOfPropertyChange(() => IconUrl);
            }
        }
        public string Description
        {
            get { return _description; }
            set
            {
                if (Equals(value, _description))
                {
                    return;
                }
                _description = value;
                NotifyOfPropertyChange(() => Description);
            }
        }
        public int Sunrise
        {
            get { return _sunrise; }
            set
            {
                if (Equals(value, _sunrise))
                {
                    return;
                }
                _sunrise = value;
                NotifyOfPropertyChange(() => Sunrise);
            }
        }
        public int Sunset
        {
            get { return _sunset; }
            set
            {
                if (Equals(value, _sunset))
                {
                    return;
                }
                _sunset = value;
                NotifyOfPropertyChange(() => Sunset);
            }
        }
        public string Pressure
        {
            get { return _pressure; }
            set
            {
                if (Equals(value, _pressure))
                {
                    return;
                }
                _pressure = value;
                NotifyOfPropertyChange(() => Pressure);
            }
        }
        public string Humidity
        {
            get { return _humadity; }
            set
            {
                if (Equals(value, _humadity))
                {
                    return;
                }
                _humadity = value;
                NotifyOfPropertyChange(() => Humidity);
            }
        }
        public string Cloudiness
        {
            get { return _cloudiness; }
            set
            {
                if (Equals(value, _cloudiness))
                {
                    return;
                }
                _cloudiness = value;
                NotifyOfPropertyChange(() => Cloudiness);
            }
        }
        public string Wind
        {
            get { return _wind; }
            set
            {
                if (Equals(value, _wind))
                {
                    return;
                }
                _wind = value;
                NotifyOfPropertyChange(() => Wind);
            }
        }
        public string Visibility
        {
            get { return _visibility; }
            set
            {
                if (Equals(value, _visibility))
                {
                    return;
                }
                _visibility = value;
                NotifyOfPropertyChange(() => Visibility);
            }
        }

        public ObservableCollection<NextDaysWeather> NextDaysWeather
        {
            get { return _nextDaysWeather; }
            set
            {
                if (Equals(value, _nextDaysWeather))
                {
                    return;
                }
                _nextDaysWeather = value;
                NotifyOfPropertyChange(() => NextDaysWeather);
            }
        }

        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                if (Equals(value, _isLoading))
                {
                    return;
                }
                _isLoading = value;
                NotifyOfPropertyChange(() => IsLoading);
            }
        }
        #endregion

        public WeatherViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            NextDaysWeather = new BindableCollection<NextDaysWeather>();
        }

        #region methods
        private async void LoadWeatherDetails()
        {
            IsLoading = true;
            var restService = new WeatherService();
            var weather = await restService.GetWeatherForCity(CityId);
            CityName = weather.Name;
            Temperature = weather.WeatherMainParameters.Temp;
            IconUrl = ICON_URL + weather.Weather[0].Icon + ".png";
            Description = weather.Weather[0].Description;
            Humidity = weather.WeatherMainParameters.Humidity + "%";
            Pressure = weather.WeatherMainParameters.Pressure + " hPa";
            Wind = weather.Wind.Speed + " m/s";
            Cloudiness = weather.Clouds.CloudinessInPercents + "%";
            Visibility = weather.Visibility + " m";            
            Sunrise = weather.AdditionalInfo.Sunrise;
            Sunset = weather.AdditionalInfo.Sunset;

            await LoadNextDaysWeather();
            IsLoading = false;
        }

        private async Task LoadNextDaysWeather()
        {
            var restService = new WeatherService();
            var days = await restService.GetWeatherForNextDays(CityId);
            NextDaysWeather = new ObservableCollection<NextDaysWeather>(days);
        }
        #endregion
    }
}