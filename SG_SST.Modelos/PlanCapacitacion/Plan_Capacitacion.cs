using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Models.PlanCapacitacion
{
    [Table("Tbl_PlanCapacitacion")]
    public class Plan_Capacitacion
    {
        [Key]
        public int pk_id_plan_capacitacion { get; set; }
        public int fk_id_tipo_actividad { get; set; }
        public string tema { get; set; }
        public int fk_id_rol { get; set; }
        public int fk_id_competencia { get; set; }
        public string fecha_programada { get; set; }
        public string hora_inicio { get; set; }
        public string hora_fin { get; set; }
        public string NitEmpresa { get; set; }

    }
}
