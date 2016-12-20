using System;
using System.Globalization;
using System.Windows.Data;

namespace Weather.WP.Converters
{
    public class IconNameToUrlConverter : IValueConverter
    {
        private static string ICON_URL = "http://openweathermap.org/img/w/";

        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                value = ICON_URL + value + ".png";
            }

            return value;
        }

        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new NotImplementedException();
        }
    }
}
