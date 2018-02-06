using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SG_SST.Models.Empleado;

namespace SG_SST.Models.Participacion
{
    [Table("Tbl_MiembrosActaConvivencia")]
    public class MiembrosActaConvivencia
    {
        [Key]
        public int Pk_Id_Miembro { get; set; }
        [Required]
        public int Numero_Documento { get; set; }
        [Required]
        [StringLength(60)]
        public string Nombre { get; set; }
        [ForeignKey("TipoPrioridadMiembro")]
        public int Fk_Id_TipoPrioridadMiembro { get; set; }
        [ForeignKey("PK_Id_TipoPrioridadMiembro")]
        public virtual TipoPrioridadMiembro TipoPrioridadMiembro { get; set; }
        [ForeignKey("TipoPrincipalActa")]
        public int? Fk_Id_TipoPrincipal { get; set; }
        [ForeignKey("PK_Id_TipoPrincipal")]
        public virtual TipoPrincipalActa TipoPrincipalActa { get; set; }
        [StringLength(15)]
        public string TipoRepresentante { get; set; }
        [ForeignKey("ActasConvivencia")]
        public int Fk_Id_Acta { get; set; }
        [ForeignKey("PK_Id_Acta")]
        public virtual ActasConvivencia ActasConvivencia { get; set; }
    }
}
