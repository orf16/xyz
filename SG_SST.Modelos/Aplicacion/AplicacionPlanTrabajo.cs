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
    [Table("Tbl_AplicacionPlanTrabajo")]
    public class AplicacionPlanTrabajo
    {
        [Key]
        public int Pk_Id_PlanTrabajo { get; set; }

        [DisplayName("Fecha de Inicio")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public DateTime FechaInicio { get; set; }

        [DisplayName("Fecha de Fin")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public DateTime FechaFinal { get; set; }

        [DisplayName("Año de Vigencia")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public int Vigencia { get; set; }

        [MaxLength(50)]
        [DisplayName("Descripcion")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public string Tipo { get; set; }

        [DisplayName("Fecha de Aplicación de la bateria")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> FechaAplicacion { get; set; }

        //Firmas
        [StringLength(2000)]
        public string RepLegalImagen { get; set; }
        [StringLength(2000)]
        public string RepSGSSTImagen { get; set; }
        [MaxLength(8000)]
        public string RepLegalRuta { get; set; }
        [MaxLength(8000)]
        public string RepSGSSTRuta { get; set; }

        [StringLength(1000)]
        public string RepLegalNombre { get; set; }
        [StringLength(1000)]
        public string RepSGSSTNombre { get; set; }
        [MaxLength(100)]
        public string RepLegalTipoDocumento { get; set; }
        [MaxLength(100)]
        public string RepSGSSTTipoDocumento { get; set; }
        [MaxLength(250)]
        public string RepLegalDocumento { get; set; }
        [MaxLength(250)]
        public string RepSGSSTDocumento { get; set; }

        [Required(ErrorMessage = "Debe ingresar el valor de la sede")]
        [ForeignKey("Sede")]
        public int Fk_Id_Sede { get; set; }

        [ForeignKey("Pk_Id_Sede")]
        public virtual Sede Sede { get; set; }

        public ICollection<AplicacionPlanTrabajoDetalle> PlanTrabajoDetalles { get; set; }

    }
}
