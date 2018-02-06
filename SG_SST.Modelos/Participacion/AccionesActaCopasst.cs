using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SG_SST.Models.Participacion
{
    [Table("Tbl_AccionesActaCopasst")]
    public class AccionesActaCopasst
    {
        [Key]
        public int Pk_Id_AccionActaCopasst { get; set; }
        [Required]
        public DateTime FechaProbable { get; set; }
        [Required]
        [StringLength(1000)]
        public string AccionARealizar { get; set; }
        [Required]
        [StringLength(60)]
        public string Responsable { get; set; }
        [ForeignKey("ActasCopasst")]
        public int Fk_Id_Acta { get; set; }
        [ForeignKey("PK_Id_Acta")]
        public virtual ActasCopasst ActasCopasst { get; set; }
    }
}
