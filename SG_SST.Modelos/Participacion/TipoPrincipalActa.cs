using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SG_SST.Models.Participacion
{
    [Table ("Tbl_TipoPrincipalActa")]
    public class TipoPrincipalActa
    {
        [Key]
        public int PK_Id_TipoPrincipal { get; set; }
        [Required(ErrorMessage = "El Tipo de Principal es Requerido...")]
        [StringLength(20)]
        public string DescripcionTipoPrincipal { get; set; }
        
    }
}
