using System;
using System.Windows;
using System.Windows.Controls;
using WpfOldschoolVideoGameStore.Managers;
using WpfOldschoolVideoGameStore.Models;

namespace WpfOldschoolVideoGameStore
{
    /// <summary>
    /// Interaction logic for StoreWindow.xaml
    /// </summary>
    public partial class StoreWindow : Window
    {
        public StoreWindow()
        {
            InitializeComponent();
            LoadComboBox();
            UpdateUi();
        }

        private void LoadComboBox()
        {
            cbMediaType.Items.Add("Games");
            cbMediaType.Items.Add("Movies");
        }

        private void UpdateUi()
        {
            lstMedia.Items.Clear();
            cbMediaType.SelectedIndex = -1;
            cbFilter.SelectedIndex = -1;

            foreach (var media in StoreManager.Medias)
            {

                ListViewItem item = new();
                item.Content = new { Type = media.TypeOfMedia, Name = media.Name, Rating = media.Rating.ToString(), Genre = media.Genre.ToString(), Available = media.IsAvailable(), RRated = media.IsRRatedString() };
                item.Tag = media;
                lstMedia.Items.Add(item);

            }
        }

        private void cbMediaType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cbFilter.IsEnabled = true;

            if (cbMediaType.SelectedIndex == 0)
            {
                cbFilter.Items.Clear();
                foreach (GameGenres genres in Enum.GetValues(typeof(GameGenres)))
                {
                    cbFilter.Items.Add(genres.ToString());
                }
            }
            else if (cbMediaType.SelectedIndex == 1)
            {
                cbFilter.Items.Clear();
                foreach (MovieGenres genres in Enum.GetValues(typeof(MovieGenres)))
                {
                    cbFilter.Items.Add(genres.ToString());
                }
            }
        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            if (cbFilter.SelectedItem == null)
            {
                return;
            }
            FilterUi(cbFilter.SelectedItem.ToString());


        }

        private void FilterUi(string filter)
        {
            lstMedia.Items.Clear();

            foreach (var media in StoreManager.Medias)
            {
                if (media.Genre == filter)
                {
                    ListViewItem item = new();
                    item.Content = new { Type = media.TypeOfMedia, Name = media.Name, Rating = media.Rating.ToString(), Genre = media.Genre.ToString(), Available = media.IsAvailable(), RRated = media.IsRRatedString() };
                    item.Tag = media;
                    lstMedia.Items.Add(item);
                }
            }

            if (lstMedia.Items.Count == 0)
            {
                lstMedia.Items.Add(new { Type = "No matches" });
            }

        }

        private void btnClearFilter_Click(object sender, RoutedEventArgs e)
        {
            UpdateUi();
        }
    }

}
