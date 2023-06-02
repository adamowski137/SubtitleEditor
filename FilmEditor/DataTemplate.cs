using plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmEditor
{
    public class DataTemplate
    {
        public DataTemplate() { }
        public TimeSpan ShowTime { get; set; }
        public TimeSpan HideTime { get; set; }
        public string Text { get; set; }
        public string Translation { get; set; }

        public static implicit operator SubtitleRecord(DataTemplate dataTemplate)
        {
            return new SubtitleRecord() 
            { 
                Text = dataTemplate.Text,
                ShowTime= dataTemplate.ShowTime,
                HideTime= dataTemplate.HideTime,
            };

        }

        public static implicit operator DataTemplate(SubtitleRecord subtitleRecord)
        {
            return new DataTemplate()
            {
                Text = subtitleRecord.Text,
                ShowTime = subtitleRecord.ShowTime,
                HideTime = subtitleRecord.HideTime,
            };

        }
    }
}
