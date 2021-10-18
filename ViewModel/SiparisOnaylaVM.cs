using System;

namespace LedSiparisModulu.ViewModel
{
    public class SiparisOnaylaVM
    {
        public DateTime SiparisTarihi { get; set; }
        public DateTime TeminTarih { get; set; }
        public DateTime TeslimTarih { get; set; }
        public string SiparisNotu { get; set; }
        public long SevkAdresId { get; set; }
    }
}