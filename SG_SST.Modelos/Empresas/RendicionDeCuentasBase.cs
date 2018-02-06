
namespace SG_SST.Models.Empresas
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Data;
    using System.ComponentModel.DataAnnotations;
    using System.Collections;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Tbl_RendicionDeCuentasBase")]
    public class RendicionDeCuentasBase
    {
        [Key]
        public int Pk_Id_RendicionDeCuentasBase { get; set; }

        public string Descripcion { get; set; }

        public ICollection<RolesPorRendicionDeCuentasBase> RolesPorRendicionDeCuentasBase { get; set; }
    }
}
