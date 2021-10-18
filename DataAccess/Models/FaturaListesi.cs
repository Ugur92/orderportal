using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models
{
    public class FaturaListesi
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [Column("UserId")]
        public long UserId { get; set; }
        [Column("TIPSTR")]
        public string TipStr { get; set; }
        [Column("TARIH")]
        public DateTime Tarih { get; set; }
        [Column("SAAT")]
        public DateTime Saat { get; set; }
        [Column("BELGENO")]
        public string BelgeNo { get; set; }
        [Column("EVRAKNO")]
        public string EvrakNo { get; set; }
        [Column("IRSALIYENO")]
        public string IrsaliyeNo { get; set; }
        [Column("CHKOD")]
        public string ChKod { get; set; }
        [Column("CHUNVAN")]
        public string ChUnvan { get; set; }
        [Column("SEVKKOD")]
        public string SevkKod { get; set; }
        [Column("SEVKISIM")]
        public string SevkIsim { get; set; }
        [Column("MIKTAR")]
        public decimal Miktar { get; set; }
        [Column("AMBALAJMIKTAR")]
        public decimal AmbalajMiktar { get; set; }
        [Column("MATRAH")]
        public decimal Matrah { get; set; }
        [Column("KDV")]
        public decimal Kdv { get; set; }
        [Column("TUTAR")]
        public decimal Tutar { get; set; }
    }
}