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
    [Table("Tbl_BateriaDimension")]
    public class BateriaDimension
    {
        [Key]
        public int Pk_Id_BateriaDimension { get; set; }

        [MaxLength(1000)]
        [DisplayName("Nombre")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public string Nombre { get; set; }

        [MaxLength(1000)]
        [DisplayName("Descripcion")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public string Descripcion { get; set; }

        [ForeignKey("Bateria")]
        public int Fk_Id_Bateria { get; set; }
        [ForeignKey("Pk_Id_Bateria")]
        public virtual Bateria Bateria { get; set; }

        public double FactorTransformacion { get; set; }

        public ICollection<BateriaCuestionario> BateriaUsuarios { get; set; }
    }
}
