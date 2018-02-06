using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SG_SST.Models.Emergencias
{
     [Table("Tbl_Eme_RecursosTecnicos")]
    public class Eme_RecursosTecnicos
    {
         [Key]
        public int pk_id_recursostecnicos { get; set; }
        public int fk_id_sede { get; set; }
        public string tipo { get; set; }
        public string cantidad { get; set; }
        public string ubicacion { get; set; }
        public string NitEmpresa { get; set; }

    }
}
