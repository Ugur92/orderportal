using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Stoktanim
    {
        public long Id { get; set; }
        public string Stokkod { get; set; }
        public string Stokisim { get; set; }
        public string Birim { get; set; }
        public string Ambalajbirim { get; set; }
        public decimal ListeFiyat { get; set; }
        public string Ozelkod1 { get; set; }
        public string Ozelkod2 { get; set; }
        public string Ozelkod3 { get; set; }
        public string Ozelkod4 { get; set; }
        public string Ozelkod5 { get; set; }
        public string Barkod1 { get; set; }
        public string Barkod2 { get; set; }
        public string Entegrasyonkod { get; set; }
        public string Aktif { get; set; }
        public bool? Siparislistevar { get; set; }
    }
}
