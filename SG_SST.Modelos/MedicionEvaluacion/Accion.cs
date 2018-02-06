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
    [Table("Tbl_Acciones")]
    public class Accion
    {
        [Key]
        public int Pk_Id_Accion { get; set; }

        [Required()]
        public int Id_Accion { get; set; }

        [MaxLength(100)]
        [DisplayName("Tipo de acción")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public string Tipo { get; set; }

        [DisplayName("Fecha diligenciamiento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public DateTime Fecha_dil { get; set; }

        [DisplayName("Fecha ocurrencia")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public DateTime Fecha_ocurrencia { get; set; }

        [MaxLength(100)]
        [DisplayName("Clasificación")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public string Clase { get; set; }

        [DisplayName("Fecha hallazgo")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public Nullable<System.DateTime> Fecha_hall { get; set; }

        [MaxLength(50)]
        [DisplayName("Origen")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public string Origen { get; set; }

        [MaxLength(250)]
        [DisplayName("Otro Origen")]
        public string Otro_Origen { get; set; }

        [MaxLength(100)]
        [DisplayName("Numero documento")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public string Halla_Num_Doc { get; set; }

        [MaxLength(300)]
        [DisplayName("Nombre")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0} apartir de la información de la cédula de un Empleado")]
        public string Halla_Nombre { get; set; }

        [MaxLength(300)]
        [DisplayName("Tipo documento")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0} apartir de la información de la cédula de un Empleado")]
        public string Halla_TipoDoc { get; set; }

        [MaxLength(300)]
        [DisplayName("Cargo")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public string Halla_Cargo { get; set; }

        [MaxLength(2000)]
        [DisplayName("Sede hallazgo")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public string Halla_Sede { get; set; }

        [MaxLength(2000)]
        [DisplayName("Corrección Propuesta")]
        [DataType(DataType.MultilineText)]
        public string Correccion { get; set; }

        [MaxLength(2000)]
        [DisplayName("Causa Raiz")]
        [DataType(DataType.MultilineText)]
        public string Causa_Raiz { get; set; }

        [MaxLength(20)]
        [DisplayName("Cambio documental")]
        public string Cambio_Doc { get; set; }

        [MaxLength(2000)]
        [DisplayName("Tiene Cambio documental")]
        [DataType(DataType.MultilineText)]
        public string Des_Cambio_Doc { get; set; }

        [MaxLength(1000)]
        [DisplayName("Verificacion")]
        [DataType(DataType.MultilineText)]
        public string Verificacion { get; set; }

        [MaxLength(50)]
        [DisplayName("Eficacia")]
        public string Eficacia { get; set; }

        [MaxLength(50)]
        [DisplayName("Estado")]
        public string Estado { get; set; }

        //Campos de Auditor
        [StringLength(300)]
        public string NombreArchivoAuditor { get; set; }
        [StringLength(1000)]
        public string RutaArchivoAuditor { get; set; }
        [MaxLength(250)]
        [DisplayName("Nombre del Auditor")]
        public string Nombre_Auditor { get; set; }
        [MaxLength(250)]
        [DisplayName("Cargo Auditor")]
        public string Cargo_Auditor { get; set; }

        //Campos de responsable
        [StringLength(300)]
        public string NombreArchivoResp { get; set; }
        [StringLength(1000)]
        public string RutaArchivoResp { get; set; }
        [MaxLength(250)]
        [DisplayName("Nombre del Responsable")]
        public string Nombre_Responsable { get; set; }
        [MaxLength(250)]
        [DisplayName("Cargo del Responsable")]
        public string Cargo_Responsable { get; set; }

        //FK EMPRESA
        [ForeignKey("Empresa")]
        public int Fk_Id_Empresa { get; set; }
        [ForeignKey("Pk_Id_Empresa")]
        public virtual Empresa Empresa { get; set; }


        //FK FROM
        public IList<Seguimiento> Seguimientos { get; set; }
        public IList<Analisis> Analisis { get; set; }
        public IList<Hallazgo> Hallazgos { get; set; }
        public IList<ActividadAccion> ActividadAcciones { get; set; }
        public IList<ArchivosAccion> Archivos { get; set; }
    }
}
