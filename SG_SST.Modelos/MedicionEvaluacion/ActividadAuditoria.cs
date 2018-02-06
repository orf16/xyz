using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Data.Entity;
using SG_SST.Models.Empresas;
using System.ComponentModel.DataAnnotations.Schema;

namespace SG_SST.Models.MedicionEvaluacion
{
    [Table("Tbl_ActividadAuditoria")]
    public class ActividadAuditoria
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

        [DisplayName("Fecha de Finalización")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaFinalizacion { get; set; }

        [Required()]
        public int Fk_Id_Auditoria { get; set; }
        public virtual Auditorias Auditoria { get; set; }
    }
}

