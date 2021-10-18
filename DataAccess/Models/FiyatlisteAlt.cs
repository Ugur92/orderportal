using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class FiyatlisteAlt
    {
        public long Id { get; set; }
        public long? Fiyatlisteid { get; set; }
        public string Aciklama { get; set; }
        public bool? Ilktarihvar { get; set; }
        public DateTime? Ilktarih { get; set; }
        public bool? Sontarihvar { get; set; }
        public DateTime? Sontarih { get; set; }
        public bool? Kdvdahil { get; set; }
        public bool? Onay1 { get; set; }
        public bool? Onay2 { get; set; }
        public long? Onay1userid { get; set; }
        public long? Onay2userid { get; set; }
        public int? Sevkadressecimtip { get; set; }
        public bool? Aktif { get; set; }
        public long? Led5refid { get; set; }
    }
}
