using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public partial class Websiparis
    {
        [Key]
        public long Id { get; set; }
        public long? Chid { get; set; }
        public string Siparisno { get; set; }
        public string Aciklama { get; set; }
        public DateTime? Tarih { get; set; }
        public DateTime? Termintarih { get; set; }
        public DateTime? Teslimtarih { get; set; }
        public long? SevkAdresId { get; set; }
    }
}
