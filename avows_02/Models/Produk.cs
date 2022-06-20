using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace avows_02.Models
{
    public class Produk
    {
        [Key]
        public   int Kode_Barang { get; set; }

        [Required]
        [DisplayName ("Nama Barang")]
        public string Nama_Barang { get; set; }
        [Required]
        public int Harga { get; set; }
        [Required]
        public string Keterangan { get; set; }
    }
}