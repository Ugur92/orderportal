using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models
{
    public class SiparisDetay
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [Column("CHKOD")]
        public string Kod { get; set; }
        [Column("CHUNVAN")]
        public string Unvan { get; set; }
        [Column("SEVKADRES")]
        public string SevkAdres { get; set; }
        [Column("TARIH")]
        public DateTime Tarih { get; set; }
        [Column("TERMINTARIHI")]
        public DateTime TerminTarihi { get; set; }
        [Column("TESLIMTARIH")]
        public DateTime TeslimTarihi { get; set; }
        [Column("EVRAKNO")]
        public string EvrakNo { get; set; }
        [Column("TIPSTR")]
        public string Tipstr { get; set; }
        [Column("ONAY")]
        public int? Onay { get; set; }
        [Column("ONAYSTR")]
        public string Onaystr { get; set; }
        [Column("ACIKLAMA")]
        public string Aciklama { get; set; }
    }
}
