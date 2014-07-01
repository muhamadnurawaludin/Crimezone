using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Crimezone.ViewModel;

namespace Crimezone
{
    public partial class DetailPosting : PhoneApplicationPage
    {
        public DetailPosting()
        {
            InitializeComponent();
            DataContext = new LaporanKejahatanViewModel();
        }

        private void CrimezoneNavigation(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Grid tapping = (Grid)sender;

            if (tapping.Name.Equals("Timeline_Tap"))
            {
                NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
            }
            else if (tapping.Name.Equals("Map_Tap"))
            {
                NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
            }
            else if (tapping.Name.Equals("Polrestabes_Tap"))
            {
                NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
            }
        }

        private void Lapor_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}