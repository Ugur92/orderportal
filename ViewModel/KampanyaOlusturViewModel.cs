using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LedSiparisModulu.ViewModel
{
    public class KampanyaOlusturViewModel
    {
        public int Id { get; set; }
        public string Baslik { get; set; }
        public DateTime BaslangicTarihi { get; set; }
        public DateTime BitisTarihi { get; set; }
        public string Metin { get; set; }
    }
}
