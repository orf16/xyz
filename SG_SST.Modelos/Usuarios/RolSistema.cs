using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Models.Usuarios
{
    [Table("Tbl_RolesSistema")]
    public class RolSistema
    {
        [Key]
        public int Pk_Id_RolSistema { get; set; }
        public string Nombre { get; set; }
        public string Sigla { get; set; }
        public string Descripcion { get; set; }
        public int CantidadUsuariosPorRol { get; set; }
        public bool Activo { get; set; }
        public bool Bloqueado { get; set; }
    }
}
