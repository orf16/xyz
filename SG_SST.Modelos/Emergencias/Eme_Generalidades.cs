using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Models.Emergencias
{
    [Table("Tbl_Eme_Generalidades")]
    public class Eme_Generalidades
    {
        [Key]
        public int pk_id_generalidades { get; set; }
        public int fk_id_sede { get; set; }
        public string objetivos { get; set; }
        public string alcance { get; set; }
        public string NitEmpresa { get; set; }

    }
}
