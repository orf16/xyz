using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SG_SST.Models.Empleado
{
     [Table("Tbl_TipoInconsistenciaLaboral")]
    public class TipoInconsistencia
    {

        [Key]
        public int PKTipoInconsistencia { get; set; }
        public string DescripcionTipInc { get; set; }



    }
}