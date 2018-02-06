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
    [Table("Tbl_Bateria")]
    public class Bateria
    {
        [Key]
        public int Pk_Id_Bateria { get; set; }

        [MaxLength(100)]
        [DisplayName("Tipo de Elemento")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public string Nombre { get; set; }

        [MaxLength(1000)]
        [DisplayName("Tipo de Elemento")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public string Descripción { get; set; }

        [MaxLength(1000)]
        [DisplayName("Tipo de Elemento")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public string Fecha_publicacion { get; set; }

        [MaxLength(1000)]
        [DisplayName("Tipo de Elemento")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public string TiposAplicacion { get; set; }

        [MaxLength(1000)]
        [DisplayName("Tipo de Elemento")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public string ModalidadesAplicacion { get; set; }

        [MaxLength(1000)]
        [DisplayName("Tipo de Elemento")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public string Poblacion { get; set; }

        [MaxLength(1000)]
        [DisplayName("Tipo de Elemento")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public string Objetivo { get; set; }

        [MaxLength(1000)]
        [DisplayName("Tipo de Elemento")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public string Baremacion { get; set; }

        [MaxLength(1000)]
        [DisplayName("Tipo de Elemento")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public string TipoInstrumento { get; set; }

        [MaxLength(1000)]
        [DisplayName("Tipo de Elemento")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public string NumeroItems { get; set; }

        [MaxLength(1000)]
        [DisplayName("Tipo de Elemento")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public string DuracionAplicacion { get; set; }

        [MaxLength(1000)]
        [DisplayName("Tipo de Elemento")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public string Materiales { get; set; }

        public ICollection<BateriaGestion> BateriaGestiones { get; set; }
        public ICollection<BateriaDimension> BateriaDimensiones { get; set; }
    }
}
