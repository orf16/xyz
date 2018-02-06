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
    [Table("Tbl_Rendicion_Cuenta_Por_Rol")]
    public class RendicionDeCuentasPorRol
    {
        [Key]
        public int Id_Pk_RendicionDeCuentasPorRol { get; set; }

        [ForeignKey("Rol")]
        public int Fk_Id_Rol { get; set; }
        [ForeignKey("Pk_Id_Rol")]
        public virtual Rol Rol { get; set; }

        [ForeignKey("RendicionDeCuentas")]
        public int Fk_Id_RendicionDeCuentas { get; set; }

        [ForeignKey("Pk_Id_RendicionDeCuentas")]
        public virtual RendicionDeCuentas RendicionDeCuentas { get; set; }
    }
}

