using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using _1SemEksamen.MainViewModel;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace _1SemEksamen.MainView
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SignUpPage : Page
    {
        public SignUpPage()
        {
            this.InitializeComponent();
        }

        private async void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            SignUpProgress.IsActive = true;
            SignUpButton.IsEnabled = false;
            bool signUpSuccessful = await MainPageVM.MainPageVmInstance.SignUp();
            SignUpProgress.IsActive = false;
            SignUpButton.IsEnabled = true;
            if (signUpSuccessful)
            {
                Frame.Navigate(typeof(UserPage));
            }
        }
    }
}
