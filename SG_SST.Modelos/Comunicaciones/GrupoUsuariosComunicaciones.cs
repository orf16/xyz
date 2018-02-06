using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Models.Comunicaciones
{
    [Table("Tbl_GrupoUsuariosComunicaciones")]
    public class GrupoUsuariosComunicaciones
    {
        [Key]
        public int pk_id_grupo_usuario_comunicaciones { get; set; }
        public int fk_id_grupo_comunicaciones { get; set; }
        public string nombre_contacto { get; set; }
        public string email { get; set; }
        public bool Status { get; set; }

    }
}
