
namespace SG_SST.Models.Organizacion
{
    using SG_SST.Models.Empresas;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    [Table("Tbl_Rol_Por_Tematica")]
    public class RolPorTematica
    {
        [Key]
        public int Id_Pk_RolPorTematica { get; set; }

        [ForeignKey("Tematica")]
        public int Fk_Id_Tematica { get; set; }
        [ForeignKey("Pk_Id_Tematica")]
        public virtual Tematica Tematica { get; set; }

        [ForeignKey("Rol")]
        public int Fk_Id_Rol { get; set; }

        [ForeignKey("Pk_Id_Rol")]
        public virtual Rol Rol { get; set; }
    }
}
