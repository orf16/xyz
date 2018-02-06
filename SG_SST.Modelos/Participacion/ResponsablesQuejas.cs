using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SG_SST.Models.Empleado;

namespace SG_SST.Models.Participacion
{
    [Table("Tbl_ResponsablesQuejas")]
    public class ResponsablesQuejas
    {
        [Key]
        public int Pk_Id_Responsable { get; set; }
        [Required]
        public int Numero_Documento { get; set; }
        [Required]
        [StringLength(60)]
        public string Nombre { get; set; }

        [ForeignKey("ActaConvivenciaQuejas")]
        public int Fk_Id_Queja { get; set; }
        [ForeignKey("PK_Id_Queja")]
        public virtual ActaConvivenciaQuejas ActaConvivenciaQuejas { get; set; }
    }
}
