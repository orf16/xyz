
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

    [Table("Tbl_RendicionDeCuentas")]
    public class RendicionDeCuentas
    {
        [Key]
        public int Pk_Id_RendicionDeCuentas { get; set; }

        public string Descripcion { get; set; }

        public ICollection<RendicionDeCuentasPorRol> RendicionDeCuentasPorRoles { get; set; }
    }
}
