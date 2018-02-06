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
    [Table("Tbl_SegVialParametro")]
    public class SegVialParametro
    {
        [Key]
        public int Pk_Id_SegVialParametro { get; set; }

        [DisplayName("Numero - Orden")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public int Numero { get; set; }

        [MaxLength(30)]
        [DisplayName("Numeral")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public string Numeral { get; set; }

        [MaxLength(500)]
        [DisplayName("Definición de Parametro")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public string ParametroDef { get; set; }

        
        [DisplayName("Valor Parametro")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public decimal Valor_Parametro { get; set; }

        [Required(ErrorMessage = "Debe ingresar el valor del Pilar")]
        [ForeignKey("SegVialPilar")]
        public int Fk_Id_SegVialPilar { get; set; }

        [ForeignKey("Pk_Id_SegVialPilar")]
        public virtual SegVialPilar SegVialPilar { get; set; }

        public int Fk_Id_Empresa { get; set; }
        public bool disabled { get; set; }

        public ICollection<SegVialDetalle> SegVialDetalles { get; set; }
    }
}
