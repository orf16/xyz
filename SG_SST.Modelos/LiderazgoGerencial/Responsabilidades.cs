
namespace SG_SST.Models.LiderazgoGerencial
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

    [Table("Tbl_Responsabilidades")]
    public class Responsabilidades
    {
        [Key]
        public int Pk_Id_Responsabilidades { get; set; }

        public string Descripcion { get; set; }

        public ICollection<ResponsabilidadesPorRol> ResponsabilidadesPorRoles { get; set; }

    }
}
