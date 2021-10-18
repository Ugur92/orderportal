using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models
{
    public partial class İptalSiparisLog
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [Column("SIPARISID")]
        public long SiparisId { get; set; }
        [Column("CHID")]
        public long Chid { get; set; }
        [Column("IPTALTARIHI")]
        public DateTime IptalTarihi { get; set; }
    }
}
