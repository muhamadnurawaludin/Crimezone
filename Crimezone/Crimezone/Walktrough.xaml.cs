using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media;

namespace Crimezone
{
    public partial class Walktrough1 : PhoneApplicationPage
    {
        public Walktrough1()
        {
            InitializeComponent();
        }

        private void buttonImage_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {

        }

        private void MasukAplikasi_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

        private void FacebookBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/ConnectPage.xaml", UriKind.Relative));
        }

        

       
    }
}