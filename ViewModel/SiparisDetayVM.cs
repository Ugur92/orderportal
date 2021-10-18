using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LedSiparisModulu.ViewModel
{
    public class SiparisDetayVM
    {
        public long? SiparisId { get; set; }
        public long? StokId { get; set; }
        public string Stokkod { get; set; }
        public int Miktar { get; set; }
        public string Birim { get; set; }
        public DateTime SiparisTarihi { get; set; }
        public DateTime TeminTarih { get; set; }
        public DateTime TeslimTarih { get; set; }
        public string SiparisNotu { get; set; }
    }
}