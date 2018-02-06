using SG_SST.Models.Empresas;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SG_SST.Models.Usuarios
{
    [Table("Tbl_UsuarioSistemaEmpresa")]
    public class UsuarioSistemaEmpresa
    {
        [Key]    
        public int Pk_Id_UsuarioSistemaEmpresa { get; set; }
        [ForeignKey("Empresa")]
        public int Fk_Id_Empresa { get; set; }
        [ForeignKey("UsuarioSistema")]
        public int Fk_Id_UsuarioSistema { get; set; }

        [ForeignKey("Pk_Id_Empresa")]
        public virtual Empresa Empresa { get; set; }
        [ForeignKey("Pk_Id_UsuarioSistema")]
        public virtual UsuarioSistema UsuarioSistema { get; set; }
    }
}
