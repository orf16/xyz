using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Aplicacion
{
    public class EDPlanTrabajoMeses
    {
        public int IdMesPlan { get; set; }
        public string mes { get; set; }
        public string mes_table { get; set; }
        public DateTime fecha_inicio { get; set; }
        public DateTime fecha_despues { get; set; }



    }
}
