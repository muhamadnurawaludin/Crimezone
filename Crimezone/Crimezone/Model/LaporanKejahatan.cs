using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crimezone.Model
{
    class LaporanKejahatan
    {
        public string id_laporan_kejahatan { get; set; }
        public string id_daftar_pelapor { get; set; }
        public string foto_kejahatan { get; set; }
        public string judul_laporan_kegiatan { get; set; }
        public string deskripsi_laporan_kegiatan { get; set; }
        public string id_jenis_kejahatan { get; set; }
        public string id_lokasi_kejahatan { get; set; }
        public string tanggal_kejadian { get; set; }
        public string waktu_kejadian { get; set; }
        public string latitude { get; set; }
        public string longtitude { get; set; }
        public string alamat_kejahatan { get; set; }
        public string id_status_laporan { get; set; }
        public string nama_jenis_kejahatan { get; set; }
        public string nama_status_laporan { get; set; }
        public string nama_petugas { get; set; }
    }
}
