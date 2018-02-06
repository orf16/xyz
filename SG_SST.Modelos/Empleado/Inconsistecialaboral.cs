using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SG_SST.Models.Empleado
{
     [Table("Tbl_InconsistenciasLaborales")]
    public class Inconsistecialaboral
    {
        [Key]
        public int PKInconsistencia { get; set; }


        [ForeignKey("TipoInconsistencia")]
        public int FKTipoInconsistencia { get; set; }
        [ForeignKey("PKTipoInconsistencia")]
        public virtual TipoInconsistencia TipoInconsistencia { get; set; }

        /*  
        [ForeignKey("Empleado_Consulta")]
        public int FKIDEmpleado { get; set; }
        [ForeignKey("ID_Empleado")]
        public virtual Empleado_Consulta Empleado_Consulta { get; set; }
        */

        public string DescripcionInconsistencia { get; set; }



    }
}