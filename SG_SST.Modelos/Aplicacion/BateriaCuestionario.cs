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
    [Table("Tbl_BateriaCuestionario")]
    public class BateriaCuestionario
    {
        [Key]
        public int Pk_Id_BateriaCuestionario { get; set; }

        [MaxLength(1000)]
        [DisplayName("Pregunta")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public string Pregunta { get; set; }


        [ForeignKey("BateriaDimension")]
        public int Fk_Id_BateriaDimension { get; set; }
        [ForeignKey("Pk_Id_BateriaDimension")]
        public virtual BateriaDimension BateriaDimension { get; set; }

        [DisplayName("Orden")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public int Orden { get; set; }

        [DisplayName("Página")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public int Pagina { get; set; }

        [DisplayName("Orden")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public int Dominio { get; set; }

        public ICollection<BateriaResultado> BateriaResultados { get; set; }
    }
}
