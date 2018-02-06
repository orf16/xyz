using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Models.Usuarios
{
    [Table("Tbl_PermisosDenegadosPorRol")]
    public class PermisoDenegadoPorRol
    {
        [Key]
        public int Pk_Id_PermisoDenegadoPorRol { get; set; }

        [ForeignKey("PermisoSistema")]
        public int Fk_Id_PermisoSistema { get; set; }

        [ForeignKey("RolSistema")]
        public int Fk_Id_RolSistema { get; set; }

        [ForeignKey("Pk_Id_PermisoSistema")]
        public virtual PermisoSistema PermisoSistema { get; set; }

        [ForeignKey("Pk_Id_RolSistema")]
        public virtual RolSistema RolSistema { get; set; }
    }
}
