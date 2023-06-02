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
            string[] formats = 
                {"%s", "%ss\\.f", "%ss\\.ff", "%s\\.fff", "%ss\\.f" , "%ss\\.ff", "%ss\\.fff", "%m\\:ss", "%mm\\:ss",
                "%m\\:ss\\.f", "%m\\:ss\\.ff", "%m\\:ss\\.fff", "%mm\\:ss\\.f", "%mm\\:ss\\.ff", "%mm\\:ss\\.ff", "%mm\\:ss\\.fff",
                "%h\\:mm\\:ss", "%h\\:mm\\:ss\\.f", "%h\\:mm\\:ss\\.ff", "%h\\:mm\\:ss\\.fff", "%hh\\:mm:\\:ss\\", "%hh\\:mm\\:ss\\.f",
                "%hh\\:mm\\:ss\\.ff", "%hh\\:mm\\:ss\\.fff"};
            if (TimeSpan.TryParseExact(t2, formats, CultureInfo.InvariantCulture, out timeSpan))
            {
                return timeSpan;
            }
            return null;
            
        }
    }
}
