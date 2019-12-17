using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using _1SemEksamen.MainModel;
using _1SemEksamen.MainViewModel;
using _1SemEksamen.Sebastian.View;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace _1SemEksamen.MainView
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(FoodList));
        }

        private void Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Frame.Navigate(typeof(UserPage));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(UserPage));
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            LoginButton.IsEnabled = false;
            LoginProgress.IsActive = true;
            User receivedUser = await MainPageVM.MainPageVmInstance.Login();
            if (receivedUser != null)
            {
                if (receivedUser.IsAdmin)
                {
                    Frame.Navigate(typeof(AdminPage));
                }
                else
                {
                    Frame.Navigate(typeof(UserPage));
                }
            }
            LoginButton.IsEnabled = true;
            LoginProgress.IsActive = false;
        }
    }
}
