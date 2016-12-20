using System;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Weather.Model.SearchResponse;

namespace Weather.WP.Controls
{
    public partial class CityControl : UserControl, INotifyPropertyChanged
    {
        #region fields
        private string _cityName;
        private string _country;
        #endregion

        #region properties
        public City City
        {
            get { return (City)GetValue(CityProperty); }
            set { SetValue(CityProperty, value); }
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
                NotifyPropertyChanged();
            }
        }

        public string Country
        {
            get { return _country; }
            set
            {
                if (Equals(value, _country))
                {
                    return;
                }
                _country = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region DependencyProperty
        public static readonly DependencyProperty CityProperty = DependencyProperty.Register("City", typeof(City),
        typeof(CityControl), new PropertyMetadata(default(City), OnCityChanged));
        private static void OnCityChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var cityrControl = sender as CityControl;
            if (cityrControl != null)
            {
                cityrControl.CityChanged();
            }
        }
        #endregion

        public CityControl()
        {
            InitializeComponent();
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        #region methods
        private void CityChanged()
        {
            CityName = City.Name;
            Country = City.AdditionalInfo.Country;
        }
        #endregion
    }
}
