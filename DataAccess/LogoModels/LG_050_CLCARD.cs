
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.LogoModels
{
    public class LG_050_CLCARD
    {
        [Key]
        [Column("LOGICALREF")]
        public int LogicalRef { get; set; }

        [Column("CODE")]
        public string Code { get; set; }
    }
}