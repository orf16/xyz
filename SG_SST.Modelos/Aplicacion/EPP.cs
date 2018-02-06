using SG_SST.Models.Empresas;
using SG_SST.Models.Planificacion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Models.Aplicacion
{
    [Table("Tbl_EPP")]
    public class EPP
    {
        [Key]
        public int Pk_Id_EPP { get; set; }

        [Display(Name = "Nombre del EPP")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(250)]
        public string NombreEPP { get; set; }

        [Display(Name = "Parte del cuerpo a proteger")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(250)]
        public string ParteCuerpo { get; set; }

        [Display(Name = "Especificación Técnica")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(1000)]
        public string EspecificacionTecnica { get; set; }

        [Display(Name = "Uso")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(1000)]
        public string Uso { get; set; }

        [Display(Name = "Mantenimiento")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(1000)]
        public string Mantenimiento { get; set; }

        [Display(Name = "Vida Útil")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(200)]
        public string VidaUtil { get; set; }

        [Display(Name = "Reposicion")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(200)]
        public string Reposicion { get; set; }

        [Display(Name = "Disposicion Final")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(1000)]
        public string DisposicionFinal { get; set; }

        //Imagen y Archivos
        [StringLength(2000)]
        public string ArchivoImagen { get; set; }
        [StringLength(2000)]
        public string ArchivoImagen_download { get; set; }
        [StringLength(3000)]
        public string RutaImage { get; set; }
        [StringLength(2000)]
        public string NombreArchivo { get; set; }
        [StringLength(2000)]
        public string NombreArchivo_download { get; set; }
        [StringLength(3000)]
        public string RutaArchivo { get; set; }

        //Relaciones
        [Required(ErrorMessage = "El campo riesgo asociado es obligatorio")]
        [ForeignKey("ClasificacionDePeligro")]
        public int Fk_Id_Clasificacion_De_Peligro { get; set; }
        [ForeignKey("PK_ClasificacionDePeligro")]
        public virtual ClasificacionDePeligro ClasificacionDePeligro { get; set; }

        [ForeignKey("Empresa")]
        public int Fk_Id_Empresa { get; set; }
        [ForeignKey("PK_Empresa")]
        public virtual Empresa Empresa { get; set; }

        public ICollection<EPPCargo> Cargos { get; set; }

        public ICollection<EPPSuministroEPP> EPPSuministroEPPs { get; set; }
    }
}
