using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class WebappSepet
    {
        public long Id { get; set; }
        public long? Chid { get; set; }
        public long? Stokid { get; set; }
        public string Birim { get; set; }
        public double? Miktar { get; set; }
    }
}
