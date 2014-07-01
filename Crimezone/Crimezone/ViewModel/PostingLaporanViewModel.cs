using Crimezone.Common;
using Crimezone.Model;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Crimezone.ViewModel
{
    class PostingLaporanViewModel : ViewModelBase
    {
        string url = Helper.BASE;
        //private int listSelectedIndex = -1;
        private JenisKejahatan jenisKejahatanSelecte = new JenisKejahatan();

        public PostingLaporanViewModel()
        {
            JenisKejahatan jenisKejahatan = new JenisKejahatan();
            jenisKejahatan.id_jenis_kejahatan = 1;
            jenisKejahatan.nama_jenis_kejahatan = "Perampokan";
            postingLaporanCollection.Add(jenisKejahatan);

            jenisKejahatan = new JenisKejahatan();
            jenisKejahatan.id_jenis_kejahatan = 2;
            jenisKejahatan.nama_jenis_kejahatan = "Penganiayaan";
            postingLaporanCollection.Add(jenisKejahatan);

            jenisKejahatan = new JenisKejahatan();
            jenisKejahatan.id_jenis_kejahatan = 3;
            jenisKejahatan.nama_jenis_kejahatan = "Penculikan";
            postingLaporanCollection.Add(jenisKejahatan);

            jenisKejahatan = new JenisKejahatan();
            jenisKejahatan.id_jenis_kejahatan = 4;
            jenisKejahatan.nama_jenis_kejahatan = "Pemerkosaan";
            postingLaporanCollection.Add(jenisKejahatan);

            jenisKejahatan = new JenisKejahatan();
            jenisKejahatan.id_jenis_kejahatan = 5;
            jenisKejahatan.nama_jenis_kejahatan = "Pembunuhan";
            postingLaporanCollection.Add(jenisKejahatan);

            jenisKejahatan = new JenisKejahatan();
            jenisKejahatan.id_jenis_kejahatan = 6;
            jenisKejahatan.nama_jenis_kejahatan = "Pembakaran";
            postingLaporanCollection.Add(jenisKejahatan);

            LokasiKejahatan lokasiKejahatan = new LokasiKejahatan();
            lokasiKejahatan.id_lokasi_kejahatan = 1;
            lokasiKejahatan.nama_lokasi_kejahatan = "Jalan Umum";

            lokasiKejahatan = new LokasiKejahatan();
            lokasiKejahatan.id_lokasi_kejahatan = 2;
            lokasiKejahatan.nama_lokasi_kejahatan = "Kampus / Sekolah";

            lokasiKejahatan = new LokasiKejahatan();
            lokasiKejahatan.id_lokasi_kejahatan = 3;
            lokasiKejahatan.nama_lokasi_kejahatan = "Pemukiman";

            lokasiKejahatan = new LokasiKejahatan();
            lokasiKejahatan.id_lokasi_kejahatan = 4;
            lokasiKejahatan.nama_lokasi_kejahatan = "Perkantoran";

            lokasiKejahatan = new LokasiKejahatan();
            lokasiKejahatan.id_lokasi_kejahatan = 5;
            lokasiKejahatan.nama_lokasi_kejahatan = "Pertokoan / Pasar / Mall";

            lokasiKejahatan = new LokasiKejahatan();
            lokasiKejahatan.id_lokasi_kejahatan = 6;
            lokasiKejahatan.nama_lokasi_kejahatan = "Tempat Parkir";

            lokasiKejahatan = new LokasiKejahatan();
            lokasiKejahatan.id_lokasi_kejahatan = 7;
            lokasiKejahatan.nama_lokasi_kejahatan = "Tempat Ramai";
        }


        private ObservableCollection<JenisKejahatan> postingLaporanCollection = new ObservableCollection<JenisKejahatan>();

        public ObservableCollection<JenisKejahatan> PostingLaporanCollection
        {
            get { return postingLaporanCollection; }
            set { postingLaporanCollection = value;
            RaisePropertyChanged("");
            }
        }


        //public int JenisKejahatanSelected
        //{
        //    get { return listSelectedIndex; }
        //    set { listSelectedIndex = value;
        //    RaisePropertyChanged("");
        //    int index = 0;
        //        foreach(JenisKejahatan entity in PostingLaporanCollection)
        //        {
        //            if (index == listSelectedIndex)
        //            {
        //                jeni
        //            }
        //        }
        //    }
        //}

        private string fotoKejahatan;
        private string judulKejahatan;
        private string deskripsiKejahatan;
        private string jenisKejahatan;
        private string lokasiKejahatan;
       

        public string FotoKejahatan
        {
            get { return fotoKejahatan; }
            set { fotoKejahatan = value; }
        }


        public string JudulKejahatan
        {
            get { return judulKejahatan; }
            set { judulKejahatan = value; }
        }


        public string DeskripsiKejahatan
        {
            get { return deskripsiKejahatan; }
            set { deskripsiKejahatan = value; }
        }


        public string JenisKejahatan
        {
            get { return jenisKejahatan; }
            set { jenisKejahatan = value; }
        }

        public string JenisKejahatanSelected
        {
            get { return jenisKejahatan; }
            set { jenisKejahatan = value; }
        }

        public string LokasiKejahatan
        {
            get { return lokasiKejahatan; }
            set { lokasiKejahatan = value; }
        }

        private void LoadUrlPosting()
        {
            RestRequest request = new RestRequest(url + "post_laporan_kejahatan.php", Method.POST);

            request.AddHeader("content-type", "multipart/form-data");
            request.AddParameter("foto", 15);
            request.AddParameter("id_daftar_pelapor", 15);
            request.AddParameter("judul_kejahatan", JudulKejahatan);
            request.AddParameter("deskripsi_kejahatan", DeskripsiKejahatan);
            request.AddParameter("id_jenis_kejahatan", JenisKejahatan);
            request.AddParameter("id_lokasi_kejahatan", LokasiKejahatan);
            request.AddParameter("tanggal_kejadian", " ");
            request.AddParameter("waktu_kejadian", " ");
            request.AddParameter("latitude", " ");
            request.AddParameter("longitude", " ");
            request.AddParameter("alamat_kejahatan", " ");
            request.AddParameter("alamat_kejahatan", 1);
        }



            
    }
}
