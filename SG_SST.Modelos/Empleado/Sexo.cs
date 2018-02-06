using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.SqlServer;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SG_SST.Models.Planificacion
{
        [Table("Tbl_Sexo")]
    public class Sexo
    {
        [Key]
            public int PK_Sexo { get; set; }
        public string Descripcion_TurnoTrabajo { get; set; }

    }
}
