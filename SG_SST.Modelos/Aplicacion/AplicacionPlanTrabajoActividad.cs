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
    [Table("Tbl_AplicacionPlanTrabajoActividad")]
    public class AplicacionPlanTrabajoActividad
    {
        [Key]
        public int Pk_Id_PlanTrabajoActividad { get; set; }

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
        [DisplayName("Descripcion")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public string Descripcion { get; set; }

        [MaxLength(2000)]
        [DisplayName("Observaciones")]
        public string Observaciones { get; set; }

        [MaxLength(1000)]
        [DisplayName("Nombre del Responsable")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public string ResponsableNombre { get; set; }

        [MaxLength(250)]
        [DisplayName("Documento del Responsable")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public string ResponsableDocumento { get; set; }

        [MaxLength(250)]
        [DisplayName("Tipo de documento del Responsable")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public string ResponsableTipoDocumento { get; set; }

        [Required(ErrorMessage = "Debe seleccionar el Objetivo del Plan de Trabajo")]
        [ForeignKey("PlanTrabajoDetalle")]
        public int Fk_Id_PlanTrabajoDetalle { get; set; }
        [ForeignKey("Pk_Id_PlanTrabajoDetalle")]
        public virtual AplicacionPlanTrabajoDetalle PlanTrabajoDetalle { get; set; }


        public ICollection<AplicacionPlanTrabajoProgramacion> Programaciones { get; set; }
    }
}
