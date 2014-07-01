using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO.IsolatedStorage;
using System.Windows;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization.Json;
using Crimezone.Model;
using RestSharp;
using System.Windows.Navigation;
using Crimezone;
using System.Windows.Media.Imaging;
using System.IO;
using Microsoft.Phone;

namespace FacebookUtils
{
    public class FacebookClient
    {
        private static FacebookClient instance;
        private string accessToken;
        string url = Helper.BASE;

        private static readonly IsolatedStorageSettings appSettings = IsolatedStorageSettings.ApplicationSettings;

        private String appId = "866993516650718";
        private String clientSecret = "22d56788b8a920a0f6e30d398a15e141";
        private String scope = "email,user_location,friends_location,user_hometown,friends_hometown,publish_stream,offline_access,read_stream,user_status,user_photos,friends_photos,friends_status,user_checkins,friends_checkins,user_events,publish_checkins,publish_stream";

        public FacebookClient()
        {
            try
            {
                accessToken = (string)appSettings["accessToken"];
            }
            catch (KeyNotFoundException)
            {
                accessToken = "";
            }
        }

        public static FacebookClient Instance
        {
            get
            {
                if (instance == null)
                    instance = new FacebookClient();
                return instance;
            }
            set
            {
                instance = value;
            }
        }

        public string AccessToken
        {
            get
            {
                return accessToken;
            }
            set
            {
                accessToken = value;
                if (accessToken.Equals(""))
                    appSettings.Remove("accessToken");
                else
                    appSettings.Add("accessToken", accessToken);
            }
        }

        public virtual String GetLoginUrl()
        {
            return "https://m.facebook.com/dialog/oauth?client_id=" + appId + "&redirect_uri=https://www.facebook.com/connect/login_success.html&scope=" + scope + "&display=touch";
        }

        public virtual String GetAccessTokenRequestUrl(string code)
        {
            return "https://graph.facebook.com/oauth/access_token?client_id=" + appId + "&redirect_uri=https://www.facebook.com/connect/login_success.html&client_secret=" + clientSecret + "&code=" + code;
        }

        public virtual String GetAccessTokenExchangeUrl(string accessToken)
        {
            return "https://graph.facebook.com/oauth/access_token?client_id=" + appId + "&client_secret=" + clientSecret + "&grant_type=fb_exchange_token&fb_exchange_token=" + accessToken;
        }

        public void PostMessageOnWall(string message, UploadStringCompletedEventHandler handler)
        {
            WebClient client = new WebClient();
            client.UploadStringCompleted += handler;
            client.UploadStringAsync(new Uri("https://graph.facebook.com/me/feed"), "POST", "message=" + HttpUtility.UrlEncode(message) + "&access_token=" + FacebookClient.Instance.AccessToken);
        }

        public void ExchangeAccessToken(UploadStringCompletedEventHandler handler)
        {
            WebClient client = new WebClient();
            client.UploadStringCompleted += handler;
            client.UploadStringAsync(new Uri(GetAccessTokenExchangeUrl(FacebookClient.Instance.AccessToken)), "POST", "");
        }

        public void LoadUserProfile()
        {
            var urlProfile = "https://graph.facebook.com/me?fileds=ids,name,pictures&access_token=" + FacebookClient.Instance.AccessToken;
            //call access token
            WebClient client = new WebClient();
            client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(DownloadUserProfileComplete);
            client.DownloadStringAsync(new Uri(urlProfile));
        }

        public void LoadProfilePicture()
        {
            var urlProfile = "https://graph.facebook.com/me/picture?redirect=false&access_token=" + FacebookClient.Instance.AccessToken;
            //call access token
            WebClient client = new WebClient();
            client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(DownloadProfilePictureComplete);
            client.DownloadStringAsync(new Uri(urlProfile));
        }




        private void DownloadUserProfileComplete(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                string result = e.Result.ToString();
                MessageBox.Show(result);
                JObject jb = JObject.Parse(e.Result);
                User.Instance.Namapengguna = jb.SelectToken("name").ToString();
                User.Instance.Password = jb.SelectToken("id").ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("");
            }

        }

        private void DownloadProfilePictureComplete(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                MessageBox.Show(e.Result);
                string result = e.Result.ToString();
                JObject jb = JObject.Parse(e.Result);
                string mu = jb.SelectToken("data").ToString();

                jb = JObject.Parse(mu);
                User.Instance.Fotopengguna = jb.SelectToken("url").ToString();

                MessageBox.Show(User.Instance.Fotopengguna);
            }
            catch (Exception)
            {
                MessageBox.Show("");
            }

            try
            {

                RestRequest request = new RestRequest(url + "post_user_data.php", Method.POST);
                Random rand = new Random();
                request.AddHeader("content-type", "multipart/form-data");
                request.AddParameter("nama_pengguna", User.Instance.Namapengguna);
                request.AddParameter("password", User.Instance.Password);
                request.AddParameter("foto_pengguna", User.Instance.Fotopengguna);
                //calling server with restClient
                RestClient restClient = new RestClient();
                restClient.ExecuteAsync(request, (response) =>
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        MessageBox.Show("Anda Berhasil Terdaftar Ke Dalam Aplikasi Crimezone");
                        //NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
                    }
                    else
                    {
                        //error ocured during upload
                        MessageBox.Show("Your data is failed to uploaded. Please check your connection.");
                        //progressBar1.Visibility = System.Windows.Visibility.Visible;
                    }
                });
            }
            catch (Exception r)
            {
                MessageBox.Show("salahnya di akhir" + r.Message);
            }
            //catch (TimeoutException)
            //{
            //    MessageBox.Show("Login failed. Please check your internet connection");

            //}
            //catch (NullReferenceException)
            //{
            //    MessageBox.Show("Login failed. Please check your username or password");

            //}
            //catch (WebException)
            //{
            //    MessageBox.Show("Login failed. Please check your internet connection");

            //}
        }



    }
}
