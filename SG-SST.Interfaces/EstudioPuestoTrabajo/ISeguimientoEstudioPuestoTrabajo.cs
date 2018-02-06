using SG_SST.EntidadesDominio.EstudioPuestoTrabajo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Interfaces.EstudioPuestoTrabajo
{
    public interface ISeguimientoEstudioPuestoTrabajo
    {
        bool GuardarSeguimiento(EDSeguimientoEstudioPuestoTrabajo estudioPT);
    }
}
