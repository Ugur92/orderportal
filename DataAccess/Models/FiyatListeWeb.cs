namespace DataAccess.Models
{
    public class FiyatListeWeb
    {
        public long Id { get; set; }
        public string Kod { get; set; }
        public string Isim { get; set; }
        public string Birim { get; set; }
        public decimal Fiyat { get; set; }
        public long ChId { get; set; }
    }
}