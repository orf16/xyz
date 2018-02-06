using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using SG_SST.Models.Aplicacion;

namespace SG_SST.Models.MedicionEvaluacion
{
    [Table("Tbl_ActividadAccion")]
    public class ActividadAccion
    {
        [Key]
        public int Pk_Id_Actividad { get; set; }

        [DataType(DataType.MultilineText)]
        [DisplayName("Actividad")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        [MaxLength(1000)]
        public string Actividad { get; set; }

        [DisplayName("Responsable")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        [MaxLength(500)]
        public string Responsable { get; set; }

        [DisplayName("Fecha de finalización")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaFinalizacion { get; set; }

        [StringLength(1100)]
        public string NombreArchivoAct { get; set; }

        [StringLength(1100)]
        public string RutaArchivoAct { get; set; }

        [Required()]
        public byte Estado { get; set; }

        //FK
        public int Fk_Id_Accion { get; set; }
        public virtual Accion Accion { get; set; }

        public ICollection<PlanAccionporInspeccion> PlanAccionporInspeccion { get; set; }
        

    }
}
