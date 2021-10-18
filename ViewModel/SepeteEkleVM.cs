using System.ComponentModel.DataAnnotations;

namespace LedSiparisModulu.ViewModel
{
    public class SepeteEkleVM
    {
        [Required]
        public int StokId { get; set; }

        [Required]
        public string Birim { get; set; }

        [Required]
        public float Miktar { get; set; }
    }
}