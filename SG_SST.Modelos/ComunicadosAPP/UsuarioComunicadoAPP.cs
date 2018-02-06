using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Models.ComunicadosAPP
{
    [Table("Tbl_UsuarioComunicadoAPP")]
    public class UsuarioComunicadoAPP
    {

        [Key]
        public int PK_Id_Mensaje { get; set; }
        public int FK_Id_ComunicadosAPP { get; set; }
        public string PlayerID { get; set; }
        public string IdentificacionUsuario { get; set; }
        public int IDEstadoComunicado { get; set; }

    }
}
