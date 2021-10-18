using System.ComponentModel.DataAnnotations.Schema;

namespace LedSiparisModulu.ViewModel
{
    public class SiparisKarsilamaVM
    {
        public string STOKKOD { get; set; }
        public string STOKISIM { get; set; }
        public string OZELKOD2 { get; set; }
        public string OZELKOD3 { get; set; }
        public string OZELKOD4 { get; set; }
        public string OZELKOD5 { get; set; }
        public string PARCAGRUBU { get; set; }
        public string PARCAGRUBU2 { get; set; }
        public double ORTALAMAKOLIKG { get; set; }
        public double HAMSIPARISKG { get; set; }
        public double HAMSIPARISKOLI { get; set; }
        public double OPTIMIZESIPARISKG { get; set; }
        public double OPTIMIZESIPARISKOLI { get; set; }
        public double OPTIMIZEKARSILAMAORAN { get; set; }
        public double SEVKKG { get; set; }
        public double SEVKKOLI { get; set; }
        public double SEVKKARSILAMAORAN { get; set; }
        public double HAMDANSEVKKARSILAMAORAN { get; set; }
    }
}
