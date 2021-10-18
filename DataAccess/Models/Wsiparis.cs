using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Wsiparis
    {
        public long Id { get; set; }
        public long? Depoid { get; set; }
        public long? Chid { get; set; }
        public string Evrakseri { get; set; }
        public string Evraksira { get; set; }
        public string Evrakno { get; set; }
        public DateTime? Tarih { get; set; }
        public DateTime? Vadetarih { get; set; }
        public DateTime? Saat { get; set; }
        public decimal? Iskonto1 { get; set; }
        public decimal? Iskonto2 { get; set; }
        public int? Onay { get; set; }
        public long? Temsilciid { get; set; }
        public string Onaystr { get; set; }
        public decimal Tutar { get; set; }
        public string Tip { get; set; }
        public string Aciklama { get; set; }
        public bool? B2b { get; set; }
        public int Subeid { get; set; }
    }
}
