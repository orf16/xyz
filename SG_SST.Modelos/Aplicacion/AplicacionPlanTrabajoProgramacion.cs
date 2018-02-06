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
    [Table("Tbl_AplicacionPlanTrabajoProgramacion")]
    public class AplicacionPlanTrabajoProgramacion
    {
        [Key]
        public int Pk_Id_AplicacionPlanTrabajoProgramacion { get; set; }

        [DisplayName("Fecha de Programación Planeada")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public DateTime FechaProgramacionIncial { get; set; }

        [DisplayName("Fecha de Programación Actual")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public DateTime FechaEstado { get; set; }

        //0 Sin Estado, 1 Programada, 2 Reprogramada, 3 Ejecución
        [Required()]
        public short Estado { get; set; }

        [MaxLength(2000)]
        [DisplayName("Observaciones")]
        public string Observaciones { get; set; }

        [Required(ErrorMessage = "Debe seleccionar la actividad")]
        [ForeignKey("PlanTrabajoActividad")]
        public int Fk_Id_PlanTrabajoActividad { get; set; }
        [ForeignKey("Pk_Id_PlanTrabajoActividad")]
        public virtual AplicacionPlanTrabajoActividad PlanTrabajoActividad { get; set; }



    }
}
