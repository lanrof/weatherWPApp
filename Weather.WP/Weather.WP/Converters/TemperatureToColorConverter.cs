using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Weather.WP.Converters
{
    public class TemperatureToColorConverter : IValueConverter
    {
        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            SolidColorBrush brush = App.Current.Resources["WhiteBrush"] as SolidColorBrush;

            if (value != null)
            {
                var temperature = (double)value;
                if(temperature < 10)
                {
                    brush = App.Current.Resources["BlueBrush"] as SolidColorBrush; 
                }
                else if(temperature > 20)
                {
                    brush = App.Current.Resources["RedBrush"] as SolidColorBrush;
                }
            }

            return brush;
        }

        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new NotImplementedException();
        }
    }
}
