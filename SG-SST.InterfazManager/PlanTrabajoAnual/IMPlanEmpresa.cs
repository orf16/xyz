using SG_SST.Interfaces.PlanTrabajoAnual;
using SG_SST.Repositorio.PlanTrabajoAnual;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.InterfazManager.PlanTrabajoAnual
{
    public class IMPlanEmpresa
    {
        public static IPlanEmpresa PlanEmpresa()
        {
            return new PlanEmpresaManager();
        }
    }
}
