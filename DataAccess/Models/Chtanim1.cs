using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Chtanim1
    {

        public long Id { get; set; }
        public string Kod { get; set; }
        public string Isim { get; set; }
        public string Tip { get; set; }
        public string Dovizcinsi { get; set; }
        public bool? Aktif { get; set; }
        public int? Subeid { get; set; }
        public long? Led5refid { get; set; }
        public decimal? Listesirano { get; set; }
        public string Kisaisim { get; set; }

    }
}
