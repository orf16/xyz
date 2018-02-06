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
    [Table("Tbl_SegVialPilar")]
    public class SegVialPilar
    {
        [Key]
        public int Pk_Id_SegVialPilar { get; set; }

        [MaxLength(250)]
        [DisplayName("Descripción")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public string Descripcion { get; set; }

        [DisplayName("Versión")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public int Version { get; set; }


        [DisplayName("Valor Ponderado")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public decimal Valor_Ponderado { get; set; }

        public ICollection<SegVialParametro> SegVialParametros { get; set; }
    }
}
