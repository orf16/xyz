using SG_SST.EntidadesDominio.PlanTrabajoAnual;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Interfaces.PlanTrabajoAnual
{
    public interface IPlanEmpresa
    {
        EDPlanEmpresa GuardarPlanEmpresa(EDPlanEmpresa planempresa); 
    }
}
