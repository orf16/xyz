using SG_SST.Models.Aplicacion;
using SG_SST.Models.Empresas;
using SG_SST.Models.Organizacion;
using SG_SST.Models.Planificacion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Models.Aplicacion
{
    [Table("Tbl_EPPCargo")]
    public class EPPCargo
    {
        [Key]
        public int Pk_Id_EPPCargo { get; set; }

        [Display(Name = "Cantidad")]
        [Required(ErrorMessage = "el campo {0} debe ser completado")]
        public int Cantidad { get; set; }

        [ForeignKey("Cargo")]
        public int Fk_Id_Cargo { get; set; }

        [ForeignKey("Pk_Cargo")]
        public virtual Cargo Cargo { get; set; }

        [ForeignKey("AdmoEPP")]
        public int Fk_Id_EPP { get; set; }

        [ForeignKey("Pk_EPP")]
        public virtual EPP AdmoEPP { get; set; }
    }
}
