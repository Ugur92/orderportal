using System.ComponentModel.DataAnnotations.Schema;

namespace LedSiparisModulu.ViewModel
{
    public class SiparisDetayDataGrid
    {
        public long ID { get; set; }
        public string STOKKOD { get; set; }
        public string STOKISIM { get; set; }
        public double SIPARISMIKTAR { get; set; }
        public string AMBALAJBIRIM { get; set; }
        public string ACIKLAMA { get; set; }
    }
}
