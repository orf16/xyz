using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SG_SST.Models.Empleado;

namespace SG_SST.Models.Participacion
{
    [Table("Tbl_CompromisosPendientes")]
    public class CompromisosPendientes
    {
        [Key]
        public int Pk_Id_Compromiso { get; set; }
         [Required]
        [StringLength(1000)]
        public string CompromisoPendiente { get; set; }

        [ForeignKey("SeguimientoActaConvivencia")]
         public int FK_Id_Seguimiento { get; set; }
        [ForeignKey("PK_Id_Seguimiento")]
        public virtual SeguimientoActaConvivencia SeguimientoActaConvivencia { get; set; }
    }
}
