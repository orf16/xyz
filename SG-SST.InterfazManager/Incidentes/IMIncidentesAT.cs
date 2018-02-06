using SG_SST.Interfaces.Incidentes;
using SG_SST.Repositorio.Incidentes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.InterfazManager.Incidentes
{
    public class IMIncidentesAT
    {
        public static IIncidentesAT Empresa()
        {
            return new IncidentesATManager();
        }

    }
}
