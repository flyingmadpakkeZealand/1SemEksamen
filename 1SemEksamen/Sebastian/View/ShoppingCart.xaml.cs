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
using _1SemEksamen.MainView;
using _1SemEksamen.Sebastian.ViewModel;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace _1SemEksamen.Sebastian.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ShoppingCart : Page
    {
        public ShoppingCart()
        {
            this.InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {

            Frame.Navigate(typeof(MainPage));
        }

        private void ButtonBase_OnClick2(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(FoodList));
        }

        private void ButtonBase_OnClick3(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(DrinksList));
        }

        private void IsPaneOpen(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = true;
            
        }
    }
}
