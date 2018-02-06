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
    [Table("Tbl_Auditorias")]
    public class Auditorias
    {
        public Auditorias() { }

        public int Pk_Id_Auditoria { get; set; }

        [MaxLength(100)]
        [DisplayName("Periodo")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public string Periodo { get; set; }

        [MaxLength(3000)]
        [DisplayName("Objetivo")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public string Objetivo { get; set; }

        [MaxLength(3000)]
        [DisplayName("Alcance")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public string Alcance { get; set; }

        [MaxLength(3000)]
        [DisplayName("Criterios")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public string Criterios { get; set; }

        [DisplayName("Fecha de Realizacion")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public DateTime FechaRealizacion { get; set; }

        [MaxLength(1000)]
        [DisplayName("Auditado")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public string Auditado { get; set; }

        [MaxLength(1000)]
        [DisplayName("Auditador")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public string Auditador { get; set; }

        [MaxLength(1000)]
        [DisplayName("Requisito")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public string Requisito { get; set; }

        [MaxLength(1000)]
        [DisplayName("Duracion")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public string Duracion { get; set; }


        public int Fk_Id_Programa { get; set; }
        public virtual AuditoriaPrograma AuditoriaPrograma { get; set; }

        //FK Proceso
        [Required()]
        [ForeignKey("Proceso")]
        public int Fk_Id_Proceso { get; set; }
        [ForeignKey("Proceso")]
        public virtual Proceso Proceso { get; set; }


        public IList<AuditoriaListaVer> AuditoriaListaVers { get; set; }
        public IList<AuditoriaCronograma> AuditoriaCronogramas { get; set; }
        public IList<ActividadAuditoria> AuditoriaActividades { get; set; }
    }
}
