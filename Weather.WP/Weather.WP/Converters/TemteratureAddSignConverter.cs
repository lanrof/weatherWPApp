using System;
using System.Globalization;
using System.Windows.Data;

namespace Weather.WP.Converters
{
    public class TemteratureAddSignConverter : IValueConverter
    {
        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            String temperature = "";
            if(value != null)
            {
                temperature = System.Convert.ToString((double)value) + "°";
            }

            return temperature;
        }

        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new NotImplementedException();
        }
    }    
}
