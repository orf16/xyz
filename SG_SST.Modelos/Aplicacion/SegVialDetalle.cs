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
    [Table("Tbl_SegVialDetalle")]
    public class SegVialDetalle
    {
        [Key]
        public int Pk_Id_SegVialParametroDetalle { get; set; }

        [MaxLength(30)]
        [DisplayName("Numeral")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public string Numeral { get; set; }

        [MaxLength(500)]
        [DisplayName("Descripción de Variable")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public string VariableDesc { get; set; }

        [MaxLength(1000)]
        [DisplayName("Criterio de Aval")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public string CriterioAval { get; set; }

        [Required(ErrorMessage = "Debe ingresar el valor del Parametro")]
        [ForeignKey("SegVialParametro")]
        public int Fk_Id_SegVialPilar { get; set; }
        [ForeignKey("Pk_Id_SegVialParametro")]
        public virtual SegVialParametro SegVialParametro { get; set; }

        public ICollection<SegVialResultado> SegVialResultados { get; set; }
    }
}
