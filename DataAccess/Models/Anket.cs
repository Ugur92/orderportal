using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models
{
    public partial class Anket
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("CHID")]
        public long ChId { get; set; }
        [Column("UNVAN")]
        public string Unvan { get; set; }
        [Column("TARIH")]
        public DateTime Tarih { get; set; }
        [Column("SORU1")]
        public int Soru1 { get; set; }
        [Column("SORU2")]
        public int Soru2 { get; set; }
        [Column("SORU3")]
        public int Soru3 { get; set; }
        [Column("SORU4")]
        public int Soru4 { get; set; }
        [Column("SORU5")]
        public int Soru5 { get; set; }
        [Column("SORU6")]
        public int Soru6 { get; set; }
        [Column("METIN")]
        public string Metin { get; set; }

    }
}
