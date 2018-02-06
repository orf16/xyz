using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Aplicacion
{
    public class EDAplicacionPlanTrabajoProgramacion
    {
        public int Pk_Id_AplicacionPlanTrabajoProgramacion { get; set; }
        public DateTime FechaProgramacionIncial { get; set; }
        public DateTime FechaEstado { get; set; }
        public short Estado { get; set; }
        public string Observaciones { get; set; }
        public int Fk_Id_PlanTrabajoActividad { get; set; }

        public string Horas { get; set; }
    }
}
