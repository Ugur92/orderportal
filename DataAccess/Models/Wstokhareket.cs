using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Wstokhareket
    {
        public DateTime? Tarih { get; set; }
        public string Saat { get; set; }
        public DateTime? Sevktarih { get; set; }
        public string Aciklama2 { get; set; }
        public string Aciklama { get; set; }
        public string Tip { get; set; }
        public long? Faturaid { get; set; }
        public long? Faturadetayid { get; set; }
        public long? Depoharid { get; set; }
        public long? Depohardetayid { get; set; }
        public long? Uretimid { get; set; }
        public long? Stokvirmanid { get; set; }
        public string Evrakseri { get; set; }
        public string Evraksira { get; set; }
        public string Evrakno { get; set; }
        public long? Stokid { get; set; }
        public long? Chid { get; set; }
        public long? Depoid { get; set; }
        public string Gc { get; set; }
        public decimal? Girenmiktar { get; set; }
        public decimal? Cikanmiktar { get; set; }
        public decimal? Miktar { get; set; }
        public long? Birimid { get; set; }
        public string Birim { get; set; }
        public decimal? Fiyat { get; set; }
        public decimal? Netfiyat { get; set; }
        public decimal? Kdvoran { get; set; }
        public decimal? Matrah { get; set; }
        public decimal? Tutar { get; set; }
        public decimal? Girenambalajmiktar { get; set; }
        public decimal? Cikanambalajmiktar { get; set; }
        public string Ambalajbirim { get; set; }
        public DateTime? Stokvadetarih { get; set; }
        public long? Subeid { get; set; }
        public long? Temsilciid { get; set; }
        public long Projeid { get; set; }
        public string Harozelkod { get; set; }
        public int? Sevkadresid { get; set; }
    }
}
