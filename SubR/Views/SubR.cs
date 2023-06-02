using plugins;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SubR.Views
{
    public class SubR : MenuItem, ISubtitlesPlugin
    {
        public SubR() 
        {
            Header = Name;
        }
        public string Name { get; } = "SubRep";
        public string Extension { get; } = "srt";
        public ICollection<SubtitleRecord> Load(string path)
        {
            var fileContent = File.ReadAllLines(path);
            if (fileContent.Length <= 0)
                return new List<SubtitleRecord>();

            var content = new List<SubtitleRecord>();
            var segment = 1;
            for (int item = 0; item < fileContent.Length; item++)
            {
                if (segment.ToString() == fileContent[item])
                {
                    content.Add(new SubtitleRecord
                    {
                        ShowTime = TimeSpan.Parse(fileContent[item + 1].Substring(0, fileContent[item + 1].LastIndexOf("-->")).Trim()),
                        HideTime = TimeSpan.Parse(fileContent[item + 1].Substring(fileContent[item + 1].LastIndexOf("-->") + 3).Trim()),
                        Text = fileContent[item + 2]

                    });
                    // The block numbers of SRT like 1, 2, 3, ... and so on
                    segment++;
                    // Iterate one block at a time
                    item += 3;
                }
            }
            return content;
        }

        public void Save(string path, ICollection<SubtitleRecord> subtitles)
        {
            Stream s = File.OpenWrite(path);
            FileStreamOptions options = new FileStreamOptions();
            StreamWriter streamWriter = new StreamWriter(s, Encoding.UTF8);
            int i = 1;
            foreach (var subtitle in subtitles)
            {
                streamWriter.WriteLine(i++);
                streamWriter.WriteLine($"{subtitle.ShowTime} --> {subtitle.HideTime}");
                streamWriter.WriteLine(subtitle.Text);
                streamWriter.WriteLine();
            }
            streamWriter.Close();   
            s.Close();
        }
    }
}
