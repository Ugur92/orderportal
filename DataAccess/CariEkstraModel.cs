using System;

namespace DataAccess
{
    public class CariEkstraModel
    {
        //public int LOGICALREF { get; set; }
        //public int CLIENTREF { get; set; }
        //public int SOURCEFREF { get; set; }
        public DateTime IslemTarihi { get; set; }
        public string BelgeNo { get; set; }
        public string IslemTuru { get; set; }
        public string Aciklama { get; set; }
        public float IslemTutari { get; set; }
        public float Borc { get; set; }
        public float Alacak { get; set; }
        public float Bakiye { get; set; }
    }
}