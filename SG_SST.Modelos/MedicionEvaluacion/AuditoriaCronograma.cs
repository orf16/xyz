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
    [Table("Tbl_AuditoriaCronograma")]
    public class AuditoriaCronograma
    {
        [Key]
        public int Pk_Id_Cronograma_Auditoria { get; set; }

        [MaxLength(1000)]
        [DisplayName("Tema")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public string Tema { get; set; }

        [MaxLength(1000)]
        [DisplayName("Responsable")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public string Responsable { get; set; }

        [DisplayName("Fecha de Realizacion")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public DateTime Fecha_Hora { get; set; }

        [MaxLength(1000)]
        [DisplayName("Tiempo Estimado")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public string TiempoEstimado { get; set; }

        [MaxLength(1000)]
        [DisplayName("Lugar")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public string Lugar { get; set; }

        [Required()]
        public int Fk_Id_Auditoria { get; set; }
        public virtual Auditorias Auditoria { get; set; }
    }
}
