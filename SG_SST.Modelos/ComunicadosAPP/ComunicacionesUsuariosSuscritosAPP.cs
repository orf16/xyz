using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Models.ComunicadosAPP
{
    [Table("Tbl_ComunicacionesUsuariosSuscritosAPP")]
    public class ComunicacionesUsuariosSuscritosAPP
    {
        [Key]
        public int PK_Id_Suscrito { get; set; }
        public string IdentificacionUsuario { get; set; }
        public string PlayerID { get; set; }
    }

}
