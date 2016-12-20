using System;
using System.Globalization;
using System.Windows.Data;
using Weather.WP.Utilities;

namespace Weather.WP.Converters
{
    public class TimestampToTimeStringConverter : IValueConverter
    {
        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            String time = "";
            if (value != null)
            {
                var timestamp = (int)value;
                var dateTime = TimestampToDateTimeHelper.UnixTimeStampToDateTime(timestamp);
                time = dateTime.ToString("hh: mm tt");
            }

            return time;
        }

        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new NotImplementedException();
        }
    }
}
