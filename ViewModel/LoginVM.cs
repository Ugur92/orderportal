using System.ComponentModel.DataAnnotations;

namespace LedSiparisModulu.ViewModel
{
    public class LoginVM
    {
        [Required(ErrorMessage ="Kullanıcı adı boş geçilemez !")]
        public string KullaniciAdi { get; set; }
        [Required(ErrorMessage = "Şifre boş geçilemez !")]
        public string Sifre { get; set; }
    }
}