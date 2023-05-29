using Microsoft.Windows.Themes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace FilmEditor
{
    public class TimeSpanConverter : IValueConverter
    {
        public object Convert(object input, Type t, object param, CultureInfo cultureInfo)
        {
            TimeSpan timeSpan = (TimeSpan)input;
            if(timeSpan < new TimeSpan(0, 0, 1))
            {
                return timeSpan.ToString(@"s\.fff").TrimEnd('0').TrimEnd('.');
            }
            return timeSpan.ToString(@"hh\:mm\:ss\.fff").TrimStart('0',':').TrimEnd('0').TrimEnd('.');

        }
        public object ConvertBack(object input, Type t, object param, CultureInfo cultureInfo)
        {
            string t2 = (string)input;
            TimeSpan timeSpan;
            string[] formats = { "s\\.f", "ss\\.fff", "mm\\:ss\\.fff", "mm\\:ss\\.ff", "mm\\:ss\\.f", "hh\\:mm\\:ss\\.fff", "hh\\:mm\\:ss\\.ff", "hh\\:mm\\:ss\\.f", "hh\\:mm\\:ss", "mm\\:ss" };
            if (TimeSpan.TryParseExact(t2, formats, CultureInfo.InvariantCulture, out timeSpan))
            {
                return timeSpan;
            }
            return null;
            
        }
    }
}
