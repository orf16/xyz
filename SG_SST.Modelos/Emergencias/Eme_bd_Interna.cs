using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SG_SST.Models.Emergencias
{
    [Table("Tbl_Eme_bd_Interna")]
    public class Eme_bd_Interna
    {
        [Key]
        public int pk_id_bd_interna { get; set; }
        public int fk_id_sede { get; set; }
        public string nombre { get; set; }
        public string numdocumento { get; set; }
        public string genero { get; set; }
        public string eps { get; set; }
        public string rh { get; set; }
        public string contacto_nombre { get; set; }
        public string contacto_telefono { get; set; }
        public string contacto_parentesco { get; set; }
        public string requiere_manejo { get; set; }
        public string cual_manejo { get; set; }
    }
}
