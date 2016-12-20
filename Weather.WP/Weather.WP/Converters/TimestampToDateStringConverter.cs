using System;
using System.Globalization;
using System.Windows.Data;
using Weather.WP.Utilities;

namespace Weather.WP.Converters
{
    public class TimestampToDateStringConverter : IValueConverter
    {
        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            String date = "";
            if(value != null)
            {
                var timestamp = (int)value;
                var dateTime = TimestampToDateTimeHelper.UnixTimeStampToDateTime(timestamp);
                date = dateTime.ToString("dd/MM");
            }

            return date;
        }

        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new NotImplementedException();
        }
    }
}
