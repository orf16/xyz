using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SG_SST.Models.MedicionEvaluacion
{
     [Table("Tbl_Modulos_Plan_Accion")]
    public class ModulosPlanAccion
    {

        [Key]
        public int Pk_Id_ModuloPlanAccion { get; set; }

        [DataType(DataType.MultilineText)]
        [DisplayName("Modulo")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        [MaxLength(50)]
        public string Modulo { get; set; }

        [DataType(DataType.MultilineText)]
        [DisplayName("PlanInspeccion")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        [MaxLength(100)]
        public string PlanInspeccion { get; set; }

        [DisplayName("Actividad")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        [MaxLength(100)]
        public string Actividad { get; set; }


    }
}
