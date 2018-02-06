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
    [Table("Tbl_AuditoriaInforme")]
    public class AuditoriaInforme
    {
        [Key, ForeignKey("Auditoria")]
        public int Pk_Id_Auditoria { get; set; }

        [MaxLength(100)]
        [DisplayName("Fecha de Realizacion")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public string FechaRealizacion { get; set; }

        [MaxLength(1000)]
        [DisplayName("Conclusiones")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public string Conclusiones { get; set; }

        //Campos de Responsable proceso
        [StringLength(200)]
        public string NombreArchivoRes { get; set; }
        [StringLength(1000)]
        public string RutaArchivoRes { get; set; }
        [MaxLength(8000)]
        public string FirmaScrImageRes { get; set; }
        [StringLength(1000)]
        public string Nombre_Responsable { get; set; }
        [StringLength(1000)]
        public string Numero_Id_Responsable { get; set; }

        //Campos del Auditor
        [StringLength(200)]
        public string NombreArchivoAuditor { get; set; }
        [StringLength(1000)]
        public string RutaArchivoAuditor { get; set; }
        [MaxLength(8000)]
        public string FirmaScrImageAuditor { get; set; }
        [StringLength(1000)]
        public string Nombre_Auditor { get; set; }
        [StringLength(1000)]
        public string Numero_Id_Auditor { get; set; }

        public virtual Auditorias Auditoria { get; set; }
    }
}
