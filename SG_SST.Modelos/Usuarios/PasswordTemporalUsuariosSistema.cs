using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Models.Usuarios
{
    [Table("Tbl_PasswordsTemporalesUsuariosSistema")]
    public class PasswordTemporalUsuariosSistema
    {
        [Key]
        public int Pk_Id_PasswordTemporal { get; set; }

        [ForeignKey("UsuarioSistema")]
        public int Fk_Id_UsuarioSistema { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public string PasswordHash { get; set; }

        [ForeignKey("Pk_Id_UsuarioSistema")]
        public virtual UsuarioSistema UsuarioSistema { get; set; }
    }
}
