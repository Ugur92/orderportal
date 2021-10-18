using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Wfiyat
    {
        public long Fiyatlisteid { get; set; }
        public DateTime? Tarih { get; set; }
        public string Kod { get; set; }
        public string Isim { get; set; }
        public string Tip { get; set; }
        public bool? Aktif { get; set; }
        public string Islemtip { get; set; }
        public int? Carisecimtip { get; set; }
        public int Sevkadressecimtip { get; set; }
        public long Fiyataltlisteid { get; set; }
        public DateTime? Ilktarih { get; set; }
        public DateTime? Sontarih { get; set; }
        public string Aciklama { get; set; }
        public long Fiyatlistedetayid { get; set; }
        public long? Stokid { get; set; }
        public decimal? Fiyat { get; set; }
        public decimal? Iskonto1 { get; set; }
        public decimal? Iskonto2 { get; set; }
        public decimal? Iskonto3 { get; set; }
        public bool Kdvdahil { get; set; }
        public long? Birimid { get; set; }
        public decimal Birimcarpan { get; set; }
        public DateTime? Duzenlemetarih { get; set; }
    }
}
