using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using plugins;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace FilmEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<DataTemplate> data { get; set; }
        string SelectedFilm { get; set; }
        bool isPaused { get; set; }
        DispatcherTimer timer { get; set; }
        bool userIsDragging { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            isPaused = false;
            userIsDragging= false;
            SelectedFilm = string.Empty;
            timer = new DispatcherTimer();
            data = new ObservableCollection<DataTemplate>();
            //MessageBox.Show(Assembly.GetEntryAssembly().GetName().ToString());

            data.Add(new DataTemplate()
            {
                ShowTime = new TimeSpan(0),
                HideTime = TimeSpan.FromSeconds(10),
                Text = "Aaaaaaaaaa"
            });
            data.Add(new DataTemplate()
            {
                ShowTime = new TimeSpan(0),
                HideTime = TimeSpan.FromSeconds(2),
                Text = "bbbbbbbbbbbbb"
            });
            SubtitleDataTable.DataContext = data;
            SubtitleDataTable.ItemsSource= data;

            try
            {
                foreach(var file in Directory.GetFiles("C:\\Users\\adam\\Desktop\\projekty\\SubtitleEditor\\FilmEditor\\plugins"))
                {
                    Assembly _Assembly = Assembly.LoadFile(file);
                    var types = _Assembly.GetTypes()?.ToList();
                    var type = types?.Find(a => typeof(ISubtitlesPlugin).IsAssignableFrom(a));
                    var win = (ISubtitlesPlugin)Activator.CreateInstance(type);
                    MenuItem t = (MenuItem) win;
                    t.Click += OnOpen;
                    Open.Items.Add(t);
                    win = (ISubtitlesPlugin)Activator.CreateInstance(type);
                    t = (MenuItem)win;
                    t.Click += OnSave;
                    Save.Items.Add(t);
                    win = (ISubtitlesPlugin)Activator.CreateInstance(type);
                    t = (MenuItem)win;
                    t.Click += OnSaveTranslation;
                    Save_Translation.Items.Add(t);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Internal Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Subtitles maker");
        }

        private void Translation_Click(object sender, RoutedEventArgs e)
        {
            Save_Translation.IsEnabled = Translation.IsChecked;
            
            if (!Translation.IsChecked)
            {
                SubtitleDataTable.Columns[3].Visibility = Visibility.Collapsed;
                TranslationGroupBox.Visibility = Visibility.Collapsed;
                SecondColumn.Width = new GridLength(0);
                FirstColumn.Width = new GridLength(1, GridUnitType.Star);
            }
            else
            {
                SubtitleDataTable.Columns[3].Visibility = Visibility.Visible;
                TranslationGroupBox.Visibility = Visibility.Visible;
                SecondColumn.Width = new GridLength(1, GridUnitType.Star);
                FirstColumn.Width = new GridLength(1, GridUnitType.Star);

            }
        }

        private void SubtitleDataTable_AddingNewItem(object sender, AddingNewItemEventArgs e)
        {
            SortDataGrid(SubtitleDataTable);
        }
        public static void SortDataGrid(DataGrid dataGrid, int columnIndex = 0, ListSortDirection sortDirection = ListSortDirection.Ascending)
        {
            var column = dataGrid.Columns[columnIndex];

            dataGrid.Items.SortDescriptions.Clear();

            dataGrid.Items.SortDescriptions.Add(new SortDescription(column.SortMemberPath, sortDirection));

            foreach (var col in dataGrid.Columns)
            {
                col.SortDirection = null;
            }
            column.SortDirection = sortDirection;

            dataGrid.Items.Refresh();
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog= new OpenFileDialog();
            if(openFileDialog.ShowDialog() == true)
            {
                SelectedFilm = openFileDialog.FileName;
                MediaPlayer.Source = new Uri(SelectedFilm);
            }
        }

        private void MediaPlayer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                if (MediaPlayer.Volume < 1.0)
                {
                    MediaPlayer.Volume += 0.1;
                }
            }
            else
            {
                if (MediaPlayer.Volume > 0.0)
                {
                    MediaPlayer.Volume -= 0.1;
                }
            }
            VolumeBar.Value = MediaPlayer.Volume;

        }

        private void MediaPlayer_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if(isPaused)
            {
                MediaPlayer.Play();
                isPaused = false;
            }
            else
            {
                isPaused = true;
                MediaPlayer.Pause();
            }
        }

        private void MediaPlayer_Loaded(object sender, RoutedEventArgs e)
        {
            isPaused = false;
            MediaPlayer.Play();
            if(MediaPlayer.NaturalDuration.HasTimeSpan)
            {
                Slider.Maximum = MediaPlayer.NaturalDuration.TimeSpan.TotalSeconds;
            }

        }

        private void SubtitleDataTable_Selected(object sender, RoutedEventArgs e)
        {
            try
            {
                var t = (DataTemplate)(SubtitleDataTable.SelectedItem);
                if(t != null)
                    MediaPlayer.Position = t.ShowTime;
            }
            catch { }
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            MediaPlayer.Play();
            isPaused = false;
        }

        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            MediaPlayer.Pause();
            isPaused= false;
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            MediaPlayer.Stop();
            isPaused= true;
        }

        private void MediaPlayer_MediaOpened(object sender, RoutedEventArgs e)
        {
            Slider.Maximum = MediaPlayer.NaturalDuration.TimeSpan.TotalMilliseconds;
            VolumeBar.Value = MediaPlayer.Volume;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(10);
            timer.Tick += Timer_Tick;
            timer.Start();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            string text = "";
            foreach(var item in data.Select(x => x).Where(x => x.ShowTime <= MediaPlayer.Position && x.HideTime >= MediaPlayer.Position).OrderBy(x => x.ShowTime))
            {
                if(Translation.IsChecked)
                {
                    text += $"{item.Translation}\n";
                }
                else
                {
                    text += $"{item.Text}\n";
                }
            }
            SubtitleTextBlock.Text = text;
            if (userIsDragging) return;
            Slider.Value = MediaPlayer.Position.TotalMilliseconds;
        }

        private void Slider_DragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {
            userIsDragging= true;
        }

        private void Slider_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            userIsDragging= false;
            MediaPlayer.Position = TimeSpan.FromMilliseconds(Slider.Value);
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            CurrentTime.Text = TimeSpan.FromMilliseconds(Slider.Value).ToString(@"hh\:mm\:ss\.ff");
        }

        private void ContextMenuAdd_Click(object sender, RoutedEventArgs e)
        {
            DataTemplate tmp = new DataTemplate()
            {
                Text = "",
                Translation = "",
                ShowTime = data.Select(x => x.HideTime).Max(),
                HideTime = data.Select(x => x.HideTime).Max()
            };
            data.Add(tmp);
            SortDataGrid(SubtitleDataTable);
        }

        private void ContextMenuAddAfter_Click(object sender, RoutedEventArgs e)
        {
            TimeSpan max = new TimeSpan(0);
            foreach(var item in SubtitleDataTable.SelectedItems)
            {
                DataTemplate t = (DataTemplate) item;
                if(max < t.HideTime)
                {
                    max = t.HideTime;
                }
            }
            
            DataTemplate tmp = new DataTemplate()
            {
                Text = "",
                Translation = "",
                ShowTime = max,
                HideTime = max
            };
            data.Add(tmp);
            SortDataGrid(SubtitleDataTable);
        }

        private void ContextMenuDelete_Click(object sender, RoutedEventArgs e)
        {
            List<DataTemplate> tmp = new List<DataTemplate>();
            foreach (var item in SubtitleDataTable.SelectedItems)
            {
                DataTemplate t = (DataTemplate)item;
                tmp.Add(t);
            }
            foreach(var item in tmp)
            {
                data.Remove(item);
            }
        }

        private void OnSave(object sender, RoutedEventArgs e)
        {
            ISubtitlesPlugin plugin = (ISubtitlesPlugin)sender;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = $"Subtitile file (*.{plugin.Extension})|*.{plugin.Extension}";
            if(saveFileDialog.ShowDialog() == true)
            {
                var tmp = data.Select(x => (SubtitleRecord)x).ToList();
                plugin.Save(saveFileDialog.FileName, tmp);
            }
        }
        private void OnSaveTranslation(object sender, RoutedEventArgs e)
        {
            ISubtitlesPlugin plugin = (ISubtitlesPlugin)sender;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = $"Subtitile file (*.{plugin.Extension})|*.{plugin.Extension}";
            if (saveFileDialog.ShowDialog() == true)
            {
                var tmp = data.Select(x => new SubtitleRecord { Text = x.Translation, HideTime = x.HideTime, ShowTime = x.ShowTime}).ToList();
                plugin.Save(saveFileDialog.FileName, tmp);
            }
        }
        private void OnOpen(object sender, RoutedEventArgs e)
        {
            ISubtitlesPlugin plugin = (ISubtitlesPlugin)sender;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = $"Subtitile file (*.{plugin.Extension})|*.{plugin.Extension}";
            if (openFileDialog.ShowDialog() == true)
            {
                data.Clear();
                foreach(var item in plugin.Load(openFileDialog.FileName))
                {
                    data.Add(item);
                }
            }
        }


    }
}
