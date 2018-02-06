
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

    [Table("Tbl_Tematica_Por_Empresa")]
    public class TematicaPorEmpresa
    {
        [Key]
        public int Id_Pk_TematicaPorEmpresa { get; set; }

        [ForeignKey("Tematica")]
        public int Fk_Id_Tematica { get; set; }
        [ForeignKey("Pk_Id_Tematica")]
        public virtual Tematica Tematica { get; set; }

        [ForeignKey("Empresa")]
        public int Fk_Id_Empresa { get; set; }

        [ForeignKey("Pk_Id_Empresa")]
        public virtual Empresa Empresa { get; set; }
    }
}
