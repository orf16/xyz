
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
    [Table("Tbl_Cargo_Por_Rol")]
    public class CargoPorRol
    {
        [Key]
        public int Id_Pk_CargoPorRol { get; set; }

        [ForeignKey("Cargo")]
        public int Fk_Id_Cargo { get; set; }
        [ForeignKey("Pk_Id_Cargo")]
        public virtual Cargo Cargo { get; set; }

        [ForeignKey("Rol")]
        public int Fk_Id_Rol { get; set; }

        [ForeignKey("Pk_Id_Rol")]
        public virtual Rol Rol { get; set; }
    }
}
