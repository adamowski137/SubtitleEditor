using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace plugins
{
    public class SubtitleRecord
    {
        public TimeSpan ShowTime { get; set; }
        public TimeSpan HideTime { get; set; }
        public string Text { get; set; }
    }
}
