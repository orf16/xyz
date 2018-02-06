using SG_SST.Models.Empleado;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Models.Usuarios
{
    [Table("Tbl_UsuariosParaAprobar")]
    public class UsuarioParaAprobar
    {
        [Key]
        public int Pk_id_UsuarioParaAprobar { get; set; }
        public int TipoDocumentoEmpresa { get; set; }
        public string NumeroDocumentoEmprsa { get; set; }
        public string RazonSocial { get; set; }
        public string MunicipioSedePpal { get; set; }
        public int TipoDocumentoUsuario { get; set; }
        public string NumeroDocumentoUsuario { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }

        [ForeignKey("RolSistema")]
        public int Fk_Id_RolSistema { get; set; }
        public string EmailUsuario { get; set; }

        [ForeignKey("Pk_Id_RolSistema")]
        public virtual RolSistema RolSistema { get; set; }
    }
}
