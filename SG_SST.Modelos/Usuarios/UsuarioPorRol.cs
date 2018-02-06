using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SG_SST.Models.Usuarios
{
    [Table("Tbl_UsuariosPorRol")]
    public class UsuarioPorRol
    {
        [Key]
        public int Pk_Id_UsuarioPorRol { get; set; }

        [ForeignKey("UsuarioSistema")]
        public int Fk_Id_UsuarioSistema { get; set; }

        [ForeignKey("RolSistema")]
        public int Fk_Id_RolSistema { get; set; }

        [ForeignKey("Pk_Id_UsuarioSistema")]
        public virtual UsuarioSistema UsuarioSistema { get; set; }

        [ForeignKey("Pk_Id_RolSistema")]
        public virtual RolSistema RolSistema { get; set; }
    }
}
