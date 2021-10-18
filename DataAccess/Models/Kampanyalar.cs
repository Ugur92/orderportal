using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models
{
    public class Kampanyalar
    {

        [Key]
        [Column("ID")]
        public int Id { get; set; }     
        [Column("BASLIK")]
        public string Baslik { get; set; }
        [Column("BASLANGICTARIH")]
        public DateTime BaslangicTarihi { get; set; }
        [Column("BITISTARIH")]
        public DateTime BitisTarihi { get; set; }
        [Column("METIN")]
        public string Metin { get; set; }     
        [Column("DURUM")]
        public bool Durum { get; set; }
        [Column("KAYITTARIHI")]
        public DateTime KayitTarihi { get; set; }

    }
}
