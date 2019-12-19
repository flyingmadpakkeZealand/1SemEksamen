using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using _1SemEksamen.Magnus.View;
using _1SemEksamen.Sebastian.View;
using _1SemEksamen.Tristan.View;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace _1SemEksamen.MainView
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UserPage : Page
    {
        public static UserPage TheUserPage { get; set; }
        public UserPage()
        {
            this.InitializeComponent();
            TheUserPage = this;
        }

        public void ResetView()
        {
            Frame.Navigate(typeof(UserPage));
        }

        public void QuitToMainPage()
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void MenuButton1_Click(object sender, RoutedEventArgs e)
        {
            Mainframe.Navigate(typeof(PianoPage));
        }

        private void MenuButton3_Click(object sender, RoutedEventArgs e)
        {
            Mainframe.Navigate(typeof(Store));
        }

        private void MenuButton2_Click(object sender, RoutedEventArgs e)
        {
            Mainframe.Navigate(typeof(Tickets));
        }

        private void MenuButton4_Click(object sender, RoutedEventArgs e)
        {
            Mainframe.Navigate(typeof(Rundvisning));
        }

        private void MenuButton5_Click(object sender, RoutedEventArgs e)
        {
            Mainframe.Navigate(typeof(FoodList));
        }

        private void MenuButton6_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(UserPage));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }
    }
}
