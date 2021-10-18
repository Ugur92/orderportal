
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.LogoModels
{
    public class LG_050_01_CLFLINE
    {
        [Key]
        [Column("LOGICALREF")]
        public int LogicalRef { get; set; }

        [Column("CLIENTREF")]
        public int? ClientRef {get;set;}
    }
}