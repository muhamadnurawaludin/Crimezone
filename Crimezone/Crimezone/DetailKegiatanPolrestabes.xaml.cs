using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Devices;
using System.IO;
using System.IO.IsolatedStorage;
using Microsoft.Xna.Framework.Media;
using System.Windows.Media;
using Microsoft.Phone.Tasks;
using System.Windows.Media.Imaging;
using Newtonsoft.Json.Linq;
using Crimezone.ViewModel;

namespace Crimezone
{
    public partial class DetailKegiatanPolrestabes : PhoneApplicationPage
    {
        public DetailKegiatanPolrestabes()
        {
            InitializeComponent();
            DataContext = new LaporanKejahatanViewModel();
        }

    }
}