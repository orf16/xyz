using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SG_SST.Models.Participacion
{
    [Table("Tbl_TemasActaConvivencia")]
    public class TemasActaConvivencia
    {
        [Key]
        public int PK_Id_TemaActa { get; set; }
        [Required]
        [StringLength(300)]
        public string Tema { get; set; }
        [StringLength(1000)]
        public string Observaciones { get; set; }

        [ForeignKey("ActasConvivencia")]
        public int Fk_Id_Acta { get; set; }
        [ForeignKey("PK_Id_Acta")]
        public virtual ActasConvivencia ActasConvivencia { get; set; }
    }
}
