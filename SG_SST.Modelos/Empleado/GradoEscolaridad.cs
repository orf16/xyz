using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.SqlServer;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SG_SST.Models.Planificacion
{
    [Table("Tbl_GradoEscolaridad")]
    public class GradoEscolaridad
    {
        [Key]
        public int PK_GradoEscolaridad { get; set; }
        public string Descripcion_GradoEscolaridad { get; set; }

    }
}
