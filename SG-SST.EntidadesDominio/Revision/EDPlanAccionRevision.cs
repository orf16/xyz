using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Revision
{
    public class EDPlanAccionRevision
    {
        public int PK_Id_PlanAccion { get; set; }

        public int FK_Acta { get; set; }
        
        public string Actividad { get; set; }

        public string Responsable { get; set; }

        public DateTime Fecha { get; set; }

        public int Num_Acta { get; set; }

    }
}
