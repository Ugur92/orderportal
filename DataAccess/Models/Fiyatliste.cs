using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Fiyatliste
    {
        public long Id { get; set; }
        public string Kod { get; set; }
        public string Isim { get; set; }
        public string Tip { get; set; }
        public bool? Aktif { get; set; }
        public DateTime? Tarih { get; set; }
        public string Islemtip { get; set; }
        public int? Carisecimtip { get; set; }
        public bool? Kdvdahil { get; set; }
        public bool? Onay1 { get; set; }
        public bool? Onay2 { get; set; }
        public long? Onay1userid { get; set; }
        public long? Onay2userid { get; set; }
        public long? ReferansListeid { get; set; }
        public decimal? ReferansCarpan { get; set; }
        public string Ozelkod1 { get; set; }
        public string Ozelkod2 { get; set; }
        public string Ozelkod3 { get; set; }
        public long? Subeid { get; set; }
        public long? Led5refid { get; set; }
    }
}
