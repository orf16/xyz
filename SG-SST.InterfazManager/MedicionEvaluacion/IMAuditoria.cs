using SG_SST.Interfaces.MedicionEvaluacion;
using SG_SST.Repositorio.MedicionEvaluacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.InterfazManager.MedicionEvaluacion
{
   public class IMAuditoria
    {
        public static IAuditoria Auditoria()
        {
            return new AuditoriaManager();
        }
    }
}
