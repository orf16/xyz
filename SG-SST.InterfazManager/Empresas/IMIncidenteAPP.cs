using SG_SST.Interfaces.Empresas;
using SG_SST.Repositorio.Empresas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.InterfazManager.Empresas
{
    public class IMIncidenteAPP
    {

        public static IncidenteAPP IncidenteAPP()
        {
            return new IncidenteAppManager();
        }
    }
}