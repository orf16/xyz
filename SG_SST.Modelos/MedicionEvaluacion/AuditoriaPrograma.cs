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
    [Table("Tbl_AuditoriaPrograma")]
    public class AuditoriaPrograma
    {
        [Key]
        public int Pk_Id_Programa { get; set; }

        [MaxLength(500)]
        [DisplayName("Titulo")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public string Titulo { get; set; }

        [MaxLength(3000)]
        [DisplayName("Objetivo")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public string Objetivo { get; set; }

        [MaxLength(3000)]
        [DisplayName("Alcance")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public string Alcance { get; set; }

        [MaxLength(3000)]
        [DisplayName("Metodología")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public string Metodologia { get; set; }

        [MaxLength(3000)]
        [DisplayName("Competencia")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public string Competencia { get; set; }

        [MaxLength(3000)]
        [DisplayName("Recursos")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public string Recursos { get; set; }

        [DisplayName("Fecha de Programación")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public DateTime Fecha_Programacion { get; set; }

        [DisplayName("Año")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public int Año { get; set; }

        [MaxLength(100)]
        [DisplayName("Periodicidad")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public string Periodicidad { get; set; }

        //Campos de Responsable SGSST
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

        //Campos del Presidente Copasst
        [StringLength(200)]
        public string NombreArchivoCopasst { get; set; }
        [StringLength(1000)]
        public string RutaArchivoPres { get; set; }
        [MaxLength(8000)]
        public string FirmaScrImagePres { get; set; }
        [StringLength(1000)]
        public string Nombre_Copasst { get; set; }
        [StringLength(1000)]
        public string Numero_Id_Copasst { get; set; }

        [MaxLength(2000)]
        [DisplayName("Sede")]
        public string SedeAuditoria { get; set; }
        //FK Empresa
        [ForeignKey("Empresa")]
        public int Fk_Id_Empresa { get; set; }
        [ForeignKey("Empresa")]
        public virtual Empresa Empresa { get; set; }
        //FK Sede
        [ForeignKey("Sede")]
        public int Fk_Id_Sede { get; set; }
        [ForeignKey("Sede")]
        public virtual Sede Sede { get; set; }


        public IList<Auditorias> Auditorias { get; set; }
    }
}
