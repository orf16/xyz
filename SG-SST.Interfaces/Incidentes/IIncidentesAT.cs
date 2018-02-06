using SG_SST.EntidadesDominio.Incidentes;
using SG_SST.Models.Empresas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Interfaces.Incidentes
{
    public interface IIncidentesAT
    {
        /// <summary>
        /// Metodo para guardar los incidentes de trabajo
        /// </summary>
        EDIncidentesAT GuardarIncidentesAT(EDIncidentesAT incidentesat);
    }
}
