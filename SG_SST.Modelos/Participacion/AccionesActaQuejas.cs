using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SG_SST.Models.Participacion
{
    [Table("Tbl_AccionesActaQuejas")]
    public class AccionesActaQuejas
    {
        [Key]
        public int Pk_Id_AccionQueja { get; set; }
        [Required]
        [StringLength(1000)]
        public string AccionARealizar { get; set; }
         [ForeignKey("ActaConvivenciaQuejas")]
        public int Fk_Id_Queja { get; set; }
        [ForeignKey("Pk_Id_Queja")]
         public virtual ActaConvivenciaQuejas ActaConvivenciaQuejas { get; set; }
    }
}
