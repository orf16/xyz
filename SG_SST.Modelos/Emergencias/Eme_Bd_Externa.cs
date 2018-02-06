using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Models.Emergencias
{
    [Table("Tbl_Eme_Bd_Externa")]
    public class Eme_Bd_Externa
    {
        [Key]
        public int pk_id_bd_externa { get; set; }
        public int fk_id_sede { get; set; }
        public string entidad { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public string nombre_contacto { get; set; }
        public string NitEmpresa { get; set; }

    }
}
