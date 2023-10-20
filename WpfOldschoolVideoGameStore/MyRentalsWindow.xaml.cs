using System.Windows;
using System.Windows.Controls;
using WpfOldschoolVideoGameStore.Managers;
using WpfOldschoolVideoGameStore.Models;

namespace WpfOldschoolVideoGameStore
{
    /// <summary>
    /// Interaction logic for MyRentalsWindow.xaml
    /// </summary>
    public partial class MyRentalsWindow : Window
    {
        private Customer signedInCustomer;
        public MyRentalsWindow()
        {
            InitializeComponent();
            signedInCustomer = (Customer)StoreManager.SignedInUser;
            UpdateUI();
        }

        private void UpdateUI()
        {
            lstMedia.Items.Clear();

            foreach (var media in signedInCustomer.RentedMedia)
            {
                ListViewItem item = new();
                item.Content = new { Type = media.TypeOfMedia, Name = media.Name, Rating = media.Rating.ToString(), Genre = media.Genre.ToString(), Available = media.IsAvailable(), RRated = media.IsRRatedString() };
                item.Tag = media;
                lstMedia.Items.Add(item);
            }
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            if (lstMedia.SelectedItem == null)
            {
                return;
            }

            ListViewItem item = (ListViewItem)lstMedia.SelectedItem;
            IMedia media = (IMedia)item.Tag;
            media.IsRentedOut = false;
            signedInCustomer.RentedMedia.Remove(media);

            UpdateUI();

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            StoreWindow window = new();
            window.Show();
            Close();
        }
    }
}
