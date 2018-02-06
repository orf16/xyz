using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Models.PlanCapacitacion
{
    [Table("Tbl_PlanCapacitacion_Soporte")]
    public class PlanCapacitacion_Soporte
    {
        [Key]
        public int pk_id_soporte {get;set;}
        public int fk_id_plan_capacitacion {get;set;}
        public string adjunto { get; set; }
        public string NitEmpresa { get; set; }
     }

}
