using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LedSiparisModulu.ViewModel
{
    public class SepetVM
    {
        public long? StokId { get; set; }
        public string StokIsim { get; set; }
        public string StokKod { get; set; }
        public string Birim { get; set; }
        public double? Miktar { get; set; }
    }
}