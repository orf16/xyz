using SG_SST.EntidadesDominio.PlanTrabajoAnual;
using SG_SST.Interfaces.PlanTrabajoAnual;
using SG_SST.InterfazManager.PlanTrabajoAnual;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Logica.PlanTrabajoAnual
{
    public class LNPlanEmpresa
    {
        private static IPlanEmpresa em = IMPlanEmpresa.PlanEmpresa();

        public EDPlanEmpresa GuardarPlanEmpresa(EDPlanEmpresa planempresa)
        {
            EDPlanEmpresa mp = em.GuardarPlanEmpresa(planempresa);

            if (mp.pk_id_plan_empresa > 0)
            {
                return mp;
            }
            else
            {
                return null;
            }
        }
    }
}
