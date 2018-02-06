using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SG_SST.Models.Empresas;

namespace SG_SST.Models.Participacion
{
    [Table("Tbl_ActaConvivenciaQuejas")]
    public class ActaConvivenciaQuejas
    {
        [Key]
        public int PK_Id_Queja { get; set; }
        [Required]
        public int Consecutivo_Queja { get; set; }
        [Required]
        public int Consecutivo_Caso { get; set; }
        public DateTime Fecha { get; set; }
        [StringLength(50)]
        public string NombreRefiereSituacion { get; set; }
        [StringLength(1000)]
        public string AspectosNoResueltos { get; set; }
        [StringLength(1000)]
        public string Compromisos { get; set; }
        public int Fk_Id_Sede { get; set; }
        [ForeignKey("ActasConvivencia")]
        public int Fk_Id_Acta { get; set; }
        [ForeignKey("PK_Id_Acta")]
        public virtual ActasConvivencia ActasConvivencia { get; set; }
        public List<ResponsablesQuejas> ResponsablesQujas { get; set; }
        public List<AccionesActaQuejas> AccionesActaQuejas { get; set; }
     }
} 
