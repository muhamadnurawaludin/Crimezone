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
using Microsoft.Phone.Maps.Services;
using System.Windows.Shapes;
using Windows.Devices.Geolocation;
using Microsoft.Phone.Maps.Controls;
using System.Device.Location;
using System.Windows.Input;
using Crimezone.Resources;
using MapPushpinSample;



namespace Crimezone
{
    public partial class MainPage : PhoneApplicationPage
    {

        private bool _isDirectionsShown = false;
        private GeoCoordinate MyCoordinate = null;
        private ProgressIndicator ProgressIndicator = null;
        private double _accuracy = 0.0;
        private List<GeoCoordinate> MyCoordinates = new List<GeoCoordinate>();
        private Route MyRoute = null;
        private ReverseGeocodeQuery MyReverseGeocodeQuery = null;

        public MainPage()
        {
            InitializeComponent();
           // LoadURLkegiatan();
            DataContext = new LaporanKejahatanViewModel();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            GetCurrentCoordinate();
            DrawMapMarkers();
        }

        private void ShowProgressIndicator(String msg)
        {
            if (ProgressIndicator == null)
            {
                ProgressIndicator = new ProgressIndicator();
                ProgressIndicator.IsIndeterminate = true;
            }
            ProgressIndicator.Text = msg;
            ProgressIndicator.IsVisible = true;
            SystemTray.SetProgressIndicator(this, ProgressIndicator);
        }

        private async void GetCurrentCoordinate()
        {
            ShowProgressIndicator("getting location...");
            Geolocator geolocator = new Geolocator();
            geolocator.DesiredAccuracy = PositionAccuracy.High;

            try
            {
                //Disini binding koordinat dari database
               

                //Ini koordinat kita
                Geoposition currentPosition = await geolocator.GetGeopositionAsync(TimeSpan.FromMinutes(1), TimeSpan.FromSeconds(10));
                _accuracy = currentPosition.Coordinate.Accuracy;

                Dispatcher.BeginInvoke(() =>
                {
                    MyCoordinate = new GeoCoordinate(currentPosition.Coordinate.Latitude, currentPosition.Coordinate.Longitude);
                    DrawMapMarkers();
                    MyMap.SetView(MyCoordinate, 10, MapAnimationKind.Parabolic);
                });
            }
            catch (Exception)
            {
                // Couldn't get current location - location might be disabled in settings
                MessageBox.Show("Current location cannot be obtained. Check that location service is turned on in phone settings.", "MAP EXPLORER", MessageBoxButton.OK);
            }
            HideProgressIndicator();
        }
        private void HideProgressIndicator()
        {
            ProgressIndicator.IsVisible = false;
            SystemTray.SetProgressIndicator(this, ProgressIndicator);
        }

        private void DrawMapMarker(GeoCoordinate coordinate, Color color, MapLayer mapLayer)
        {
            //Map marker kejahatan
            //Image img = new Image();

            // Create a map marker
          //  Image img = new Image();
            
            Polygon polygon = new Polygon();
            polygon.Points.Add(new Point(0, 0));
            polygon.Points.Add(new Point(0, 75));
            polygon.Points.Add(new Point(25, 0));
            polygon.Fill = new SolidColorBrush(color);

            // Enable marker to be tapped for location information
            polygon.Tag = new GeoCoordinate(coordinate.Latitude, coordinate.Longitude);
            polygon.MouseLeftButtonUp += new MouseButtonEventHandler(Marker_Click);

            //img.MouseLeftButtonUp += new MouseButtonEventHandler(Marker_Click);

            // Create a MapOverlay and add marker.
            MapOverlay overlay = new MapOverlay();
            overlay.Content = polygon;
            overlay.GeoCoordinate = new GeoCoordinate(coordinate.Latitude, coordinate.Longitude);
            overlay.PositionOrigin = new Point(0.0, 1.0);
            mapLayer.Add(overlay);
        }

        private void Marker_Click(object sender, MouseButtonEventArgs e)
        {
            Polygon p = (Polygon)sender;
            GeoCoordinate geoCoordinate = (GeoCoordinate)p.Tag;
            if (MyReverseGeocodeQuery == null || !MyReverseGeocodeQuery.IsBusy)
            {
                MyReverseGeocodeQuery = new ReverseGeocodeQuery();
                MyReverseGeocodeQuery.GeoCoordinate = new GeoCoordinate(geoCoordinate.Latitude, geoCoordinate.Longitude);
                MyReverseGeocodeQuery.QueryCompleted += ReverseGeocodeQuery_QueryCompleted;
                MyReverseGeocodeQuery.QueryAsync();
            }
        }

        private void ReverseGeocodeQuery_QueryCompleted(object sender, QueryCompletedEventArgs<IList<MapLocation>> e)
        {
            if (e.Error == null)
            {
                if (e.Result.Count > 0)
                {
                    MapAddress address = e.Result[0].Information.Address;
                    String msgBoxText = "";
                    if (address.Street.Length > 0)
                    {
                        msgBoxText += "\n" + address.Street;
                        if (address.HouseNumber.Length > 0) msgBoxText += " " + address.HouseNumber;
                    }
                    if (address.PostalCode.Length > 0) msgBoxText += "\n" + address.PostalCode;
                    if (address.City.Length > 0) msgBoxText += "\n" + address.City;
                    if (address.Country.Length > 0) msgBoxText += "\n" + address.Country;
                    MessageBox.Show(msgBoxText, AppResources.ApplicationTitle, MessageBoxButton.OK);
                }
                else
                {
                    MessageBox.Show("Location information not found.", AppResources.ApplicationTitle, MessageBoxButton.OK);
                }
                MyReverseGeocodeQuery.Dispose();
            }
        }

        private void DrawMapMarkers()
        {
            MyMap.Layers.Clear();
            MapLayer mapLayer = new MapLayer();

            // Draw marker for current position
            if (MyCoordinate != null)
            {
                DrawAccuracyRadius(mapLayer);
                DrawMapMarker(MyCoordinate, Colors.Red, mapLayer);
                //DrawMapMarker(koordinatkejahatan);
            }

            // Draw markers for location(s) / destination(s)
            for (int i = 0; i < MyCoordinates.Count; i++)
            {
                DrawMapMarker(MyCoordinates[i], Colors.Blue, mapLayer);
            }

            // Draw markers for possible waypoints when directions are shown.
            // Start and end points are already drawn with different colors.
            if (_isDirectionsShown && MyRoute.LengthInMeters > 0)
            {
                for (int i = 1; i < MyRoute.Legs[0].Maneuvers.Count - 1; i++)
                {
                    DrawMapMarker(MyRoute.Legs[0].Maneuvers[i].StartGeoCoordinate, Colors.Purple, mapLayer);
                }
            }

            MyMap.Layers.Add(mapLayer);
        }

        private void DrawAccuracyRadius(MapLayer mapLayer)
        {
            // The ground resolution (in meters per pixel) varies depending on the level of detail 
            // and the latitude at which it’s measured. It can be calculated as follows:
            double metersPerPixels = (Math.Cos(MyCoordinate.Latitude * Math.PI / 180) * 2 * Math.PI * 6378137) / (256 * Math.Pow(2, MyMap.ZoomLevel));
            double radius = _accuracy / metersPerPixels;

            Ellipse ellipse = new Ellipse();
            ellipse.Width = radius * 2;
            ellipse.Height = radius * 2;
            ellipse.Fill = new SolidColorBrush(Color.FromArgb(75, 200, 0, 0));

            MapOverlay overlay = new MapOverlay();
            overlay.Content = ellipse;
            overlay.GeoCoordinate = new GeoCoordinate(MyCoordinate.Latitude, MyCoordinate.Longitude);
            overlay.PositionOrigin = new Point(0.5, 0.5);
            mapLayer.Add(overlay);
        }

        private void ZoomLevelChanged(object sender, MapZoomLevelChangedEventArgs e)
        {
            DrawMapMarkers();
        }

        private void MyMap_Loaded(object sender, RoutedEventArgs e)
        {
            Microsoft.Phone.Maps.MapsSettings.ApplicationContext.ApplicationId = "__ApplicationID__";
            Microsoft.Phone.Maps.MapsSettings.ApplicationContext.AuthenticationToken = "__AuthenticationToken__";
        }

       
        private void CrimezoneNavigation(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Grid tapping = (Grid)sender;

            if (tapping.Name.Equals("Timeline_Tap"))
            {
                Pivot_Control.SelectedIndex = 0;
            }
            else if (tapping.Name.Equals("Map_Tap"))
            {
                Pivot_Control.SelectedIndex = 1;
            }
            else if (tapping.Name.Equals("Polrestabes_Tap"))
            {
                Pivot_Control.SelectedIndex = 2;
            }
        }

        private void PostingLaporan_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/PostingLaporan.xaml", UriKind.Relative));
           
        }

        private void DetailKegiatanPolrestabes_Click(object sender, RoutedEventArgs e)
        {

            NavigationService.Navigate(new Uri("/DetailKegiatanPolrestabes.xaml", UriKind.Relative));
        }

        public void Method()
        {
            throw new System.NotImplementedException();
        }

        private void DetailPosting_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/DetailPosting.xaml", UriKind.Relative));
        }


       
       



    }
}
