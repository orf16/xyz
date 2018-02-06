
namespace SG_SST.Models.Empresas
{
    
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    [Table("Tbl_Roles_Por_ResponsabilidadesBase")]
    public class RolesPorResponsabilidadesBase
    {
        [Key]
        public int Id_Pk_RolesPorResponsabilidadesBase { get; set; }

        [ForeignKey("RolesBase")]
        public int Fk_Id_RolesBase { get; set; }
        [ForeignKey("Pk_Id_RolesBase")]
        public virtual RolesBase RolesBase { get; set; }

        [ForeignKey("ResponsabilidadesBase")]
        public int Fk_Id_ResponsabilidadesBase { get; set; }

        [ForeignKey("Pk_Id_ResponsabilidadesBase")]
        public virtual ResponsabilidadesBase ResponsabilidadesBase { get; set; }
    }
}
