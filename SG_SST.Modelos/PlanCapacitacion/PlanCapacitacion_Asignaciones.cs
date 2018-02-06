using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Models.PlanCapacitacion
{
    [Table("Tbl_PlanCapacitacion_Asignaciones")]
    public class PlanCapacitacion_Asignaciones
    {
        [Key]
        public int pk_id_asignaciones { get; set; }
        public int fk_id_plan_capacitacion { get; set; }
        public string numero_documento { get; set; }
        public string nombre { get; set; }
        public bool Enviado { get; set; }
        public bool asistencia { get; set; }
        public string NitEmpresa { get; set; }

    }
}
