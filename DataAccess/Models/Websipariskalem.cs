using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public partial class Websipariskalem
    {
        [Key]
        public long Id { get; set; }
        public string Stokkod { get; set; }
        public decimal? Miktarambalaj { get; set; }
        public string Birimambalaj { get; set; }
        public int? Stokid { get; set; }
        public int? Siparisid { get; set; }
        public int? Islemtipi { get; set; }
        public DateTime? Tarih { get; set; }
        public DateTime? Termintarih { get; set; }
        public DateTime? Teslimtarih { get; set; }
        public string Islemtipia { get; set; }
        public int? Chid { get; set; }
        public long? SevkAdresId { get; set; }
    }
}
