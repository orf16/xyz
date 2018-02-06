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
    [Table("Tbl_Actividad_Plan_Accion")]
    public class ActividadPlanDeAccion
    {
        [Key]
        public int Pk_Id_ActividadPlanAccion { get; set; }


        [ForeignKey("ModulosPlanAccion")]
        public int Fk_Id_ModuloPlanAccion { get; set; }

        [ForeignKey("Pk_Id_ModuloPlanAccion")]
        public virtual ModulosPlanAccion ModulosPlanAccion { get; set; }

        public int Fk_Plan_Inspección{ get; set; }

        public int Fk_Id_Actividad { get; set; }

        public DateTime FechaCierre { get; set; }

        [DisplayName("Observaciones")]
        [MaxLength(50)]
        public string Observaciones { get; set; }

    }
}
