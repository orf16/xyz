using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SG_SST.EntidadesDominio.Empresas;

namespace SG_SST.Interfaces.Empresas
{
   public interface IncidenteAPP
    {
       EDIncidenteAPP GuardarIncidenteAPP(EDIncidenteAPP incidente);
    }
}
