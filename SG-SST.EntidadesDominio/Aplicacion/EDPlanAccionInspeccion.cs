using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Aplicacion
{
   public class EDPlanAccionInspeccion
    {
        public int PkPlanAccionInspeccionED { get; set; }
        public string ActividadPlanAccionInspeccionED { get; set; }
        public string ResponsablePlanAccionED { get; set; }
        public DateTime FechaFinPlanAccionED { get; set; }
        public DateTime? FechaCierrePlanAccionED { get; set; }
        public string EstadoPlanAccionED { get; set; }

     
        public string CondicionesInsegurasED { get; set; }
        public int EstadoIDED { get; set; }

        public int PKplaneacionED { get; set; }

        public int ConsecutivoPlanInspeccionED { get; set; }

        public int ConsecutivoPlanAccionInspeccionED { get; set; }

        public List<EDCondicionInsegura> Condiciones { get; set; }
    }
}
