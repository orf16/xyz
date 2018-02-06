using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace SG_SST.Models.Empresas
{

    [Table("Tbl_TipoTercero")]
    public class TipoTercero
    {
        [Key]
        public int Pk_Id_TipoTercero { get; set; }

        [Display(Name = "Descripcion Tipo Tercero")]
        [Required(ErrorMessage = "Tipo Tercero no puede estar vacio.")]
        public string Descripcion_Tipo_Tercero { get; set; }
    }
}
