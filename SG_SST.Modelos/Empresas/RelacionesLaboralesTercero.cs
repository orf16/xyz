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
    [Table("Tbl_RelacionesLaboralesTercero")]
    public class RelacionesLaboralesTercero
    {
        [Key]
        public int Pk_Id_RelacionesLaboralesTercero { get; set; }

        [Display(Name = "Descripcion Tipo Relacion")]
        [Required(ErrorMessage = "Tipo Relacion no puede estar vacio.")]
        public string Descripcion_Relacion { get; set; }

    }
}
