using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models
{
    public class KampanyaListe
    {

        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [Column("URUNID")]
        public long UrunId { get; set; }
        [Column("STOKKOD")]
        public string StokKod { get; set; }
        [Column("STOKISIM")]
        public string StokIsim { get; set; }
        [Column("LISTEFIYATI")]
        public decimal? ListeFiyati { get; set; }
        [Column("BASLIK")]
        public string Baslik { get; set; }
        [Column("BASLANGICTARIH")]
        public DateTime BaslangicTarihi { get; set; }
        [Column("BITISTARIH")]
        public DateTime BitisTarihi { get; set; }
        [Column("METIN")]
        public string Metin { get; set; }
        [Column("RESIM")]
        public byte[] Resim { get; set; }
        [Column("DURUM")]
        public bool Durum { get; set; }
        [Column("KAYITTARIHSAAT")]
        public DateTime KayitTarihi { get; set; }

    }
}
