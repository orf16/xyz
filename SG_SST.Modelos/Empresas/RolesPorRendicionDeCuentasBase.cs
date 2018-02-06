
namespace SG_SST.Models.Empresas
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    [Table("Tbl_Roles_Por_RendicionDeCuentasBase")]
    public class RolesPorRendicionDeCuentasBase
    {
        [Key]
        public int Id_Pk_RolesPorRendicionDeCuentasBase { get; set; }

        [ForeignKey("RolesBase")]
        public int Fk_Id_RolesBase { get; set; }
        [ForeignKey("Pk_Id_RolesBase")]
        public virtual RolesBase RolesBase { get; set; }

        [ForeignKey("RendicionDeCuentasBase")]
        public int Fk_Id_RendicionDeCuentasBase { get; set; }

        [ForeignKey("Pk_Id_RendicionDeCuentasBase")]
        public virtual RendicionDeCuentasBase RendicionDeCuentasBase { get; set; }
    }
}
