using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Models.Usuarios
{
    [Table("Tbl_UsuariosRechazadosSitema")]
    public class UsuarioRechazadoSistema
    {
        [Key]
        public int Pk_Id_UsuarioRechazadoSistema { get; set; }

        [ForeignKey("UsuarioParaAprobar")]
        public int Fk_Id_UsuarioParaActivar { get; set; }

        [ForeignKey("CausalRechazoUsuariosSistema")]
        public int Fk_Id_CausalRechazoUsuario { get; set; }

        [ForeignKey("Pk_Id_USuarioParaAprobar")]
        public virtual UsuarioParaAprobar UsuarioParaAprobar { get; set; }

        [ForeignKey("Pk_Id_CausalRechazoUsuarioSistema")]
        public virtual CausalRechazoUsuariosSistema CausalRechazoUsuariosSistema { get; set; }
    }
}
