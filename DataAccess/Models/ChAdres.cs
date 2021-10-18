using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models
{
    public partial class ChAdres
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [Column("KOD")]
        public string Kod { get; set; }
        [Column("ISIM")]
        public string Isim { get; set; }
        [Column("ADRES")]
        public string Adres { get; set; }
        [Column("CHID")]
        public long ChId { get; set; }
        [Column("AKTIF")]
        public bool Aktif { get; set; }

    }
}
