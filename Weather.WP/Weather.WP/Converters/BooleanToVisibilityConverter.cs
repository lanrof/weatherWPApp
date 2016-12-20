using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Weather.WP.Converters
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public bool InvertValue { get; set; }
        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var boolValue = false;

            if (value != null)
            {
                boolValue = (bool)value;
            }
            if(InvertValue)
            {
                boolValue = !boolValue;
            }

            return boolValue ? Visibility.Visible : Visibility.Collapsed;
        }

        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new NotImplementedException();
        }
    }
}
