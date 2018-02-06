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
    [Table("Tbl_AuditoriaListaVer")]
    public class AuditoriaListaVer
    {
        [Key]
        public int Pk_Id_Lista_Verificacion { get; set; }

        [MaxLength(3000)]
        [DisplayName("Pregunta")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public string Pregunta { get; set; }

        [MaxLength(3000)]
        [DisplayName("Requisito")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public string Requisito { get; set; }

        [MaxLength(3000)]
        [DisplayName("Hallazgo")]
        public string Hallazgo { get; set; }

        [MaxLength(300)]
        [DisplayName("Tipo_Hallazgo")]
        public string Tipo_Hallazgo { get; set; }

        [MaxLength(3000)]
        [DisplayName("Requisito")]
        public string Compromiso { get; set; }

        [MaxLength(1000)]
        [DisplayName("Responsable")]
        public string Responsable { get; set; }

        [DisplayName("Fecha de Cierre")]
        public Nullable<System.DateTime> FechaCierre { get; set; }

        [Required()]
        public int Fk_Id_Auditoria { get; set; }
        public virtual Auditorias Auditoria { get; set; }
    }
}
