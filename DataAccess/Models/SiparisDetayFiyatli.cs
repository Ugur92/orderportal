using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models
{
    public class SiparisDetayFiyatli
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [Column("KOD")]
        public string Kod { get; set; }
        [Column("TARIH")]
        public DateTime Tarih { get; set; }
        [Column("STOKKOD")]
        public string StokKod { get; set; }
        [Column("STOKISIM")]
        public string StokIsım { get; set; }
        [Column("ORTALAMAKG")]
        public float OrtalamaKg { get; set; }
        [Column("AMBALAJMIKTAR")]
        public int AmbalajMiktar { get; set; }
        [Column("FIYAT")]
        public float Fiyat { get; set; }
        [Column("MALTOPLAM")]
        public float MalToplam { get; set; }
        [Column("ISKONTOTOPLAM")]
        public float IskontoToplam { get; set; }
        [Column("MATRAHTOPLAM")]
        public float MatrahToplam { get; set; }
        [Column("KDVTOPLAM")]
        public float KdvToplam { get; set; }
        [Column("TUTAR")]
        public float Tutar { get; set; }
    }
}
