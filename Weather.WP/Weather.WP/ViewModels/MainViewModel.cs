using Caliburn.Micro;
using System.Collections.ObjectModel;
using Weather.Model.SearchResponse;
using Weather.WP.Utilities;

namespace Weather.WP.ViewModels
{
    public class MainViewModel : PropertyChangedBase
    {
        #region fields 
        private readonly INavigationService _navigationService;
        private ObservableCollection<City> _cities;
        #endregion

        #region properties
        public ObservableCollection<City> Cities
        {
            get { return _cities; }
            set
            {
                if (Equals(value, _cities))
                {
                    return;
                }
                _cities = value;
                NotifyOfPropertyChange(() => Cities);
            }
        }
        #endregion

        public MainViewModel(INavigationService navigationService)
        {                       
            _navigationService = navigationService;
            Cities = StorageHelper.GetCities();
        }

        #region methods
        public void AddNewLocalization()
        {
            _navigationService.For<AddNewLocalizationViewModel>()                
                .Navigate();
        }

        public void OnTap(City city)
        {
            _navigationService.For<WeatherViewModel>()
                .WithParam(x => x.CityId, city.Id)
                .Navigate();
        }

        public void RefreshView()
        {
            Cities = StorageHelper.GetCities();
        }

        public void RemoveCity(City city)
        {
            Cities.Remove(city);
            StorageHelper.RemoveCityFromStorage(city);
            NotifyOfPropertyChange(() => Cities);
        }
        #endregion
    }
}
