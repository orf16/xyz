using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.SqlServer;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SG_SST.Models.Planificacion
{
    [Table("Tbl_Ocupacion")]
    public class Ocupacion
    {
        [Key]
        public int PK_Ocupacion { get; set; }
        public string Descripcion_Ocupacion { get; set; }

    }
}
