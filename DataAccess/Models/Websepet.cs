using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public partial class Websepet
    {
        [Key]
        public long Id { get; set; }
        public long? Chid { get; set; }
        public long? Stokid { get; set; }
        public string Birim { get; set; }
        public double? Miktar { get; set; }
    }
}
