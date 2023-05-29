using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FilmEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<DataTemplate> data { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            data = new ObservableCollection<DataTemplate>();
            data.Add(new DataTemplate()
            {
                HideTime = new TimeSpan(0),
                ShowTime = new TimeSpan(0),
                Text = "Aaaaaaaaaa"
            });
            SubtitleDataTable.DataContext = data;
            SubtitleDataTable.ItemsSource= data;
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
    }
}
