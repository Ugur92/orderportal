using System.ComponentModel.DataAnnotations;

namespace LedSiparisModulu.ViewModel
{
    public class LoginLogVM
    {
        [Required]
        public int ChId { get; set; }

        [Required]
        public string Tarih { get; set; }
    }
}