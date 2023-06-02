using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace FilmEditor
{
    class DurationConverter : IMultiValueConverter
    {
        private TimeSpan ShowTime;
        public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value[0] is TimeSpan start && value[1] is TimeSpan end) 
            {
                ShowTime = start;
                if ((end - start) < new TimeSpan(0, 0, 1))
                {
                    return (end - start).ToString(@"s\.fff").TrimEnd('0').TrimEnd('.');
                }
                return (end - start).ToString(@"hh\:mm\:ss\.fff").TrimStart('0', ':').TrimEnd('0').TrimEnd('.');
            }
            return "";
        }

        public object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture)
        {
            if(value is string t2)
            {
               TimeSpan timeSpan;
               string[] formats = { "s\\.f", "ss\\.fff", "mm\\:ss\\.fff", "mm\\:ss\\.ff", "mm\\:ss\\.f", "hh\\:mm\\:ss\\.fff", "hh\\:mm\\:ss\\.ff", "hh\\:mm\\:ss\\.f", "hh\\:mm\\:ss", "mm\\:ss" };
               if (TimeSpan.TryParseExact(t2, formats, CultureInfo.InvariantCulture, out timeSpan))
               {
                    return new object[] { ShowTime, ShowTime + timeSpan};
               }
               return new object[] { ShowTime, null };
            }
            return new object[] { ShowTime, null };
        }
    }
}
