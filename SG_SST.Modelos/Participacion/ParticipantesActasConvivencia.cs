using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SG_SST.Models.Empleado;

namespace SG_SST.Models.Participacion
{
    [Table("Tbl_ParticipantesActasConvivencia")]
    public class ParticipantesActasConvivencia
    {
        [Key]
        public int Pk_Id_Participante { get; set; }
        [Required]
        public int Numero_Documento { get; set; }
        [Required]
        [StringLength(60)]
        public string Nombre { get; set; }

        [ForeignKey("ActasConvivencia")]
        public int Fk_Id_Acta { get; set; }
        [ForeignKey("PK_Id_Acta")]
        public virtual ActasConvivencia ActasConvivencia { get; set; }
    }
}
