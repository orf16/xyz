using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SG_SST.Models.Planificacion;
using SG_SST.Models.Empresas;

namespace SG_SST.Models.Aplicacion
{
    [Table("Tbl_AplicacionPlanTrabajoDetalle")]
    public class AplicacionPlanTrabajoDetalle
    {
        [Key]
        public int Pk_Id_PlanTrabajoDetalle { get; set; }

        [MaxLength(2000)]
        [DisplayName("Objetivo")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public string Objetivo { get; set; }

        [MaxLength(2000)]
        [DisplayName("Metas")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public string Metas { get; set; }

        


        [MaxLength(2000)]
        [DisplayName("Recurso Humano")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public string RecursoHumano { get; set; }

        [MaxLength(2000)]
        [DisplayName("Recurso Tecnológico")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public string RecursoTec { get; set; }

        [MaxLength(2000)]
        [DisplayName("Recurso Financiero")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public string RecursoFinanciero { get; set; }

        [Required(ErrorMessage = "Debe seleccionar el plan de trabajo")]
        [ForeignKey("PlanTrabajo")]
        public int Fk_Id_PlanTrabajo { get; set; }
        [ForeignKey("Pk_Id_PlanTrabajo")]
        public virtual AplicacionPlanTrabajo PlanTrabajo { get; set; }

        public ICollection<AplicacionPlanTrabajoActividad> PlanTrabajoActividades { get; set; }
    }
}
