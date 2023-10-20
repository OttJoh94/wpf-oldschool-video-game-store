using System.Windows;
using WpfOldschoolVideoGameStore.Managers;

namespace WpfOldschoolVideoGameStore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Kod för att checka användare
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            foreach (var user in StoreManager.Users)
            {
                if (user.Username == username && user.Password == password)
                {
                    StoreManager.SignedInUser = user;
                    StoreWindow storeWindow = new();
                    storeWindow.Show();
                    Close();
                    return;

                }
            }

            MessageBox.Show("Invalid username or password");

        }
    }
}
