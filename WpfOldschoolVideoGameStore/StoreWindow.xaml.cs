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
            UpdateUI();
            if (StoreManager.SignedInUser.GetType() == typeof(Admin))
            {
                btnAddMedia.Visibility = Visibility.Visible;
                btnShowAdminInfo.Visibility = Visibility.Visible;
                btnRent.IsEnabled = false;
            }
        }

        private void LoadComboBox()
        {
            cbMediaType.Items.Add("Game");
            cbMediaType.Items.Add("Movie");
        }

        private void UpdateUI()
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
            CheckMyRentals();
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
            UpdateUI();
        }

        private void btnSignOut_Click(object sender, RoutedEventArgs e)
        {
            StoreManager.SignedInUser = null;
            MainWindow mainWindow = new();
            mainWindow.Show();
            Close();
        }

        private void btnRent_Click(object sender, RoutedEventArgs e)
        {
            if (lstMedia.SelectedItem == null)
            {
                return;
            }

            Customer signedInCustomer = (Customer)StoreManager.SignedInUser!;

            ListViewItem selectedItem = (ListViewItem)lstMedia.SelectedItem;
            IMedia selectedMedia = (IMedia)selectedItem.Tag;

            if (selectedMedia.IsRentedOut)
            {
                MessageBox.Show($"{selectedMedia.TypeOfMedia} is already rented out.", "Warning");
                return;
            }

            selectedMedia.IsRentedOut = true;
            signedInCustomer.RentedMedia.Add(selectedMedia);

            MessageBox.Show($"{selectedMedia.Name} added to your rentals");

            UpdateUI();
        }

        private void btnMyRentals_Click(object sender, RoutedEventArgs e)
        {
            MyRentalsWindow window = new();
            window.Show();
            Close();
        }

        private void CheckMyRentals()
        {
            if (StoreManager.SignedInUser.GetType() != typeof(Customer))
            {
                return;
            }

            Customer signedInCustomer = (Customer)StoreManager.SignedInUser;
            if (signedInCustomer.RentedMedia.Count == 0)
            {
                btnMyRentals.IsEnabled = false;
            }
            else
            {
                btnMyRentals.IsEnabled = true;
            }
        }
    }

}
