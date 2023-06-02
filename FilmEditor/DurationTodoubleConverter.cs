using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;

namespace FilmEditor
{
    public class DurationToDoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Duration duration && duration.HasTimeSpan)
            {
                TimeSpan timeSpan = duration.TimeSpan;
                return timeSpan.TotalMilliseconds;
            }
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double milliseconds = (double)value;
            Duration duration = new Duration(TimeSpan.FromMilliseconds(milliseconds));
            return duration;
        }
    }
}
