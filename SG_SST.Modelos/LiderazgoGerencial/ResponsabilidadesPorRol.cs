
namespace SG_SST.Models.LiderazgoGerencial
{
    using SG_SST.Models.Empresas;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    [Table("Tbl_Responsabilidades_Por_Rol")]
    public class ResponsabilidadesPorRol
    {
        [Key]
        public int Id_Pk_ResponsabilidadesPorRol { get; set; }

        [ForeignKey("Rol")]
        public int Fk_Id_Rol { get; set; }
        [ForeignKey("Pk_Id_Rol")]
        public virtual Rol Rol { get; set; }

        [ForeignKey("Responsabilidades")]
        public int Fk_Id_Responsabilidades { get; set; }

        [ForeignKey("Pk_Id_Responsabilidades")]
        public virtual Responsabilidades Responsabilidades { get; set; }
    }
}
