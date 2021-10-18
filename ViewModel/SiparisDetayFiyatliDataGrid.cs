using System.ComponentModel.DataAnnotations.Schema;

namespace LedSiparisModulu.ViewModel
{
    public class SiparisDetayFiyatliDataGrid
    {
        public long ID { get; set; }
        public string STOKKOD { get; set; }
        public string STOKISIM { get; set; }
        public double ORTALAMAKG { get; set; }
        public double AMBALAJMIKTAR { get; set; }
        public double FIYAT { get; set; }
        public double MALTOPLAM { get; set; }
        public double ISKONTOTOPLAM { get; set; }
        public double MATRAHTOPLAM { get; set; }
        public double KDVTOPLAM { get; set; }
        public double TUTAR { get; set; }

    }
}
