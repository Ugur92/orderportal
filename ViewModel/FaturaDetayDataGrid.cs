using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace LedSiparisModulu.ViewModel
{
    public class FaturaDetayDataGrid
    {
        public long ID { get; set; }
        public DateTime TARIH { get; set; }
        public string EVRAKNO { get; set; }
        public string IRSALIYENO { get; set; }      
        public string KOD { get; set; }
        public string ISIM { get; set; }
        public decimal MIKTAR { get; set; }
        public string BIRIM { get; set; }
        public decimal AMBALAJMIKTAR { get; set; }
        public string AMBALAJBIRIM { get; set; }
        public decimal MALTOPLAM { get; set; }
        public decimal ISKONTOTOPLAM { get; set; }
        public decimal MATRAHTOPLAM { get; set; }
        public decimal KDVTOPLAM { get; set; }
        public decimal TUTAR { get; set; }

    }
}
