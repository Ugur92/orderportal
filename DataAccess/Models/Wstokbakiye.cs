using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Wstokbakiye
    {
        public DateTime? Tarih { get; set; }
        public DateTime? Sevktarih { get; set; }
        public long? Depoid { get; set; }
        public long? Projeid { get; set; }
        public long? Stokid { get; set; }
        public long? Varyasyonid { get; set; }
        public decimal? Miktar { get; set; }
        public decimal? Ambalajmiktar { get; set; }
    }
}
