using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SG_SST.Interfaces.MedicionEvaluacion;
using SG_SST.Repositorio.MedicionEvaluacion;

namespace SG_SST.InterfazManager.MedicionEvaluacion
{
    public class IMPlanDeAccion
    {
        public static IPlanDeAccion PlanDeAccion()
        {
            return new PlanDeAccionManager();
        }
    }
}
