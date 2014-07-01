using Crimezone.Common;
using Crimezone.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Crimezone.ViewModel
{
    class LaporanKejahatanViewModel : ViewModelBase
    {
        private ObservableCollection<LaporanKejahatan> bindLaporanCollection = new ObservableCollection<LaporanKejahatan>();

        public ObservableCollection<LaporanKejahatan> BindLaporanCollection
        {
            get { return bindLaporanCollection; }
            set
            {
                bindLaporanCollection = value;
                RaisePropertyChanged("");
            }
        }



        public LaporanKejahatanViewModel()
        {
            LoadURL();
            LoadURLKegiatan();
        }

        public void LoadURL()
        {
            WebClient wcLaporanKejahatan = new WebClient();
            wcLaporanKejahatan.DownloadStringCompleted += new DownloadStringCompletedEventHandler(DownloadLaporanKejahatan);
            wcLaporanKejahatan.DownloadStringAsync(new Uri(Helper.BASE + "bind_laporan_kejahatan.php"));
        }

        private void DownloadLaporanKejahatan(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                JObject jRoot = JObject.Parse(e.Result);
                JArray jItem = JArray.Parse(jRoot.SelectToken("item").ToString());
                foreach (JObject item in jItem)
                {
                    LaporanKejahatan laporanKejahatan = new LaporanKejahatan();
                    laporanKejahatan.id_laporan_kejahatan = item.SelectToken("id_laporan_kejahatan").ToString();
                    laporanKejahatan.id_daftar_pelapor = item.SelectToken("id_daftar_pelapor").ToString();
                    laporanKejahatan.foto_kejahatan = Helper.IMG_BASE + item.SelectToken("foto_kejahatan").ToString();
                    laporanKejahatan.tanggal_kejadian = item.SelectToken("tanggal_kejadian").ToString();
                    laporanKejahatan.alamat_kejahatan = item.SelectToken("alamat_kejahatan").ToString();
                    laporanKejahatan.judul_laporan_kegiatan = item.SelectToken("judul_laporan_kegiatan").ToString();
                    laporanKejahatan.nama_jenis_kejahatan = "#" + item.SelectToken("nama_jenis_kejahatan").ToString();
                    laporanKejahatan.latitude = item.SelectToken("latitude").ToString();
                    laporanKejahatan.longtitude = item.SelectToken("longtitude").ToString();
                    laporanKejahatan.nama_status_laporan = "Status Laporan : " +item.SelectToken("nama_status_laporan").ToString();
                    laporanKejahatan.deskripsi_laporan_kegiatan = item.SelectToken("deskripsi_laporan_kegiatan").ToString();
                    laporanKejahatan.nama_petugas = item.SelectToken("nama_petugas").ToString();
                    BindLaporanCollection.Add(laporanKejahatan);
                }
            }
            catch(Exception m)
            {
                MessageBox.Show(m.Message+"Gagal menampilkan, Koneksi internet tidak stabil");
            }

        }

        private ObservableCollection<KegiatanPolrestabes> bindKegiatanCollection = new ObservableCollection<KegiatanPolrestabes>();

        public ObservableCollection<KegiatanPolrestabes> BindKegiatanCollection
        {
            get { return bindKegiatanCollection; }
            set { bindKegiatanCollection = value;
                    RaisePropertyChanged("");
            }
        }

        
        public void LoadURLKegiatan()
        {
            WebClient wcKegiatanPolrestabes = new WebClient();
            wcKegiatanPolrestabes.DownloadStringCompleted += new DownloadStringCompletedEventHandler(DownloadKegiatanPolrestabes);
            wcKegiatanPolrestabes.DownloadStringAsync(new Uri(Helper.BASE + "bind_kegiatan_polrestabes.php"));
        }

        private void DownloadKegiatanPolrestabes(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                JObject jRoot = JObject.Parse(e.Result);
                JArray jItem = JArray.Parse(jRoot.SelectToken("item").ToString());
                foreach (JObject item in jItem)
                {
                    KegiatanPolrestabes kegiatanPolrestabes = new KegiatanPolrestabes();
                    kegiatanPolrestabes.judul_kegiatan = item.SelectToken("judul_kegiatan").ToString();
                    kegiatanPolrestabes.deskripsi_kegiatan = item.SelectToken("deskripsi_kegiatan").ToString();
                    kegiatanPolrestabes.tanggal_kegiatan = item.SelectToken("tanggal_kegiatan").ToString();
                    kegiatanPolrestabes.waktu_kegiatan = item.SelectToken("waktu_kegiatan").ToString();
                    kegiatanPolrestabes.alamat_kegiatan = item.SelectToken("alamat_kegiatan").ToString();
                    BindKegiatanCollection.Add(kegiatanPolrestabes);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Gagal menampilkan, Koneksi internet tidak stabil");
            }

        }
    }
}
