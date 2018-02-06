using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.SqlServer;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SG_SST.Models.Planificacion
{
    /// <summary>
    /// modulo planificacion - perfil sociodemografico
    /// </summary>
      [Table("Tbl_SedePeligro")]
    public class SedePeligro
    {
        [Key]
        public int PK_SedePeligro { get; set; }
        public string Descripcion_TurnoTrabajo { get; set; }


    }
}
