
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

    [Table("Tbl_ResponsabilidadesBase")]
    public class ResponsabilidadesBase
    {
        [Key]
        public int Pk_Id_ResponsabilidadesBase { get; set; }

        public string Descripcion { get; set; }

        public ICollection<RolesPorResponsabilidadesBase> RolesPorResponsabilidadesBase { get; set; }
    }
}
