using Caliburn.Micro;
using System;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Threading;
using Weather.Model.SearchResponse;
using Weather.WP.Utilities;

namespace Weather.WP.ViewModels
{
    public class AddNewLocalizationViewModel : PropertyChangedBase
    {
        #region fields
        private readonly INavigationService _navigationService;
        private DispatcherTimer _timer;
        private ObservableCollection<City> _cities;
        private string _cityName;
        private string _searchingCity;
        private bool _isLoading;
        #endregion

        #region properties
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
       
        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                if(Equals(value, _isLoading))
                {
                    return;
                }
                _isLoading = value;
                NotifyOfPropertyChange(() => IsLoading);
            }
        }

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

        public AddNewLocalizationViewModel(INavigationService navigationService)
        {            
            _navigationService = navigationService;
            Cities = new BindableCollection<City>();
            InitTimer();
        }

        #region methods
        public void OnTap(City city)
        {
            StorageHelper.AddCityToStorage(city);
            _navigationService.For<WeatherViewModel>()
                .WithParam(x => x.CityId, city.Id)
                .Navigate();
        }

        public void OnTextBoxTextChanged(object sender)
        {
            TextBox textBox = sender as TextBox;
            _searchingCity = textBox.Text;

            if (!Regex.IsMatch(textBox.Text, @"^[a-zA-Z ]+$") && _searchingCity.Length > 0)
            {
                _searchingCity = _searchingCity.Remove(_searchingCity.Length - 1);
                textBox.Text = textBox.Text.Remove(textBox.Text.Length - 1);
                //Move the caret to the end of textBox
                textBox.SelectionStart = textBox.Text.Length;
                textBox.SelectionLength = 0;
            }

            if (_searchingCity.Trim().Length >= 3)
            {                
                _timer.Start();
            }
            else
            {
                Cities = new BindableCollection<City>();
            }

            BindingExpression bindingExpr = textBox.GetBindingExpression(TextBox.TextProperty);
            bindingExpr.UpdateSource();
        }

        private async void ReloadCitiesDelayTick(object sender, EventArgs e)
        {
            IsLoading = true;
            _timer.Stop();
            var service = new WeatherService();
            var list = await service.GetCitiesByName(_searchingCity);
            Cities = new ObservableCollection<City>(list);
            IsLoading = false;
        }

        private void InitTimer()
        {
            _timer = new DispatcherTimer();
            _timer.Tick += new EventHandler(ReloadCitiesDelayTick);
            _timer.Interval = new TimeSpan(0, 0, 2);
        }
        #endregion
    }
}
