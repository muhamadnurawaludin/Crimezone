using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using Newtonsoft.Json.Linq;
using Crimezone.ViewModel;
using RestSharp;
using System.IO;


namespace Crimezone
{
    public partial class PostingLaporan : PhoneApplicationPage
    {
        Stream bitmapUserProfile;
        Random rand = new Random();

        public PostingLaporan()
        {
            InitializeComponent();
          //  DataContext = new PostingLaporanViewModel();
        }

        private void BtnPublish(object sender, RoutedEventArgs e)
        {
            LoadPostingLaporan();
           // NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            
        }

        private void BtnUpload_Click(object sender, RoutedEventArgs e)
        {
            CameraCaptureTask camera = new CameraCaptureTask();
            camera.Show();
            camera.Completed += new EventHandler<PhotoResult>(camera_Completed);
        }

        void camera_Completed(object sender, PhotoResult e)
        {

            BitmapImage image = new BitmapImage();
            bitmapUserProfile = e.ChosenPhoto;
            image.SetSource(e.ChosenPhoto);
            foto_kejahatan.Source=(image);
            NavigationService.Navigate(new Uri("/PostingLaporan.xaml?image=" + image.UriSource.ToString(), UriKind.Relative));
        }

        //private void judul_kejahatan_LostFocus(object sender, RoutedEventArgs e)
        //{
        //    if (string.IsNullOrEmpty(judul_kejahatan.Text))
        //    {
        //        judul_kejahatan.Text = "Masukkan Judul Kejahatan . . .";
        //        judul_kejahatan.Foreground = new SolidColorBrush(Colors.Gray);
        //    }
        //    else
        //    {
        //        judul_kejahatan.Foreground = new SolidColorBrush(Colors.Black);
        //    }
        //}

        //private void judul_kejahatan_GotFocus(object sender, RoutedEventArgs e)
        //{
        //    if (judul_kejahatan.Text.Equals("Masukkan Judul Kejahatan . . ."))
        //    {
        //        judul_kejahatan.Text = string.Empty;
        //        judul_kejahatan.Foreground = new SolidColorBrush(Colors.Black);
        //    }
        //}

        private void Header_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

        //private void deskripsi_kejahatan_LostFocus(object sender, RoutedEventArgs e)
        //{
        //    if (string.IsNullOrEmpty(judul_kejahatan.Text))
        //    {
        //        deskripsi_kejahatan.Text = "Masukkan Deskripsi Kejahatan . . .";
        //        deskripsi_kejahatan.Foreground = new SolidColorBrush(Colors.Gray);
        //    }
        //    else
        //    {
        //        deskripsi_kejahatan.Foreground = new SolidColorBrush(Colors.Black);
        //    }
        //}

        private void deskripsi_kejahatan_GotFocus(object sender, RoutedEventArgs e)
        {
            if (deskripsi_kejahatan.Text.Equals("Masukkan Deskripsi Kejahatan . . ."))
            {
                deskripsi_kejahatan.Text = string.Empty;
                deskripsi_kejahatan.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        public byte[] ReadToEnd(System.IO.Stream stream)
        {
            long originalPosition = stream.Position;
            stream.Position = 0;

            try
            {
                byte[] readBuffer = new byte[4096];

                int totalBytesRead = 0;
                int bytesRead;

                while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
                {
                    totalBytesRead += bytesRead;

                    if (totalBytesRead == readBuffer.Length)
                    {
                        int nextByte = stream.ReadByte();
                        if (nextByte != -1)
                        {
                            byte[] temp = new byte[readBuffer.Length * 2];
                            Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
                            Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
                            readBuffer = temp;
                            totalBytesRead++;
                        }
                    }
                }

                byte[] buffer = readBuffer;
                if (readBuffer.Length != totalBytesRead)
                {
                    buffer = new byte[totalBytesRead];
                    Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
                }
                return buffer;
            }
            finally
            {
                stream.Position = originalPosition;
            }
        }


        private void LoadPostingLaporan()
        {
            try
            {
                RestRequest request = new RestRequest(Helper.BASE + "post_laporan_kejahatan.php", Method.POST);

                request.AddHeader("content-type", "multipart/form-data");
                request.AddParameter("id_daftar_pelapor", 15);
                request.AddFile("foto_kejahatan", ReadToEnd(bitmapUserProfile), "photo" + rand.Next(0, 99999999).ToString() + ".jpg");
                request.AddParameter("judul_kejahatan", judulKejahatan);
                request.AddParameter("deskripsi_kejahatan", deskripsi_kejahatan);
                request.AddParameter("id_jenis_kejahatan", 1);
                request.AddParameter("id_lokasi_kejahatan", 1);
                request.AddParameter("tanggal_kejadian", "27-06-2014");
                request.AddParameter("waktu_kejadian", "11:00");
                request.AddParameter("latitude", "0.123123");
                request.AddParameter("longitude", "0.123123");
                request.AddParameter("alamat_kejahatan", "jalan cikapayung");


                //calling server with restClient
                RestClient restClient = new RestClient();
                restClient.ExecuteAsync(request, (response) =>
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
                    }
                    else
                    {
                        //error ocured during upload
                        MessageBox.Show("Your data is failed to uploaded. Please check your connection.");
                        //progressBar1.Visibility = System.Windows.Visibility.Visible;
                    }
                });
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }


      

       
    }
}