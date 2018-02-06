using SG_SST.EntidadesDominio.EstudioPuestoTrabajo;
using SG_SST.Interfaces.EstudioPuestoTrabajo;
using SG_SST.InterfazManager.EstudioPuestoTrabajo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Logica.EstudioPuestoTrabajo
{
    public class LNTipoAnalisisPT
    {
        private static ITipoAnalisisPT tipoanalisispt = IMTipoAnalisisPT.TipoAnalisisPT();

        public List<EDTipoAnalisisPuestoTrabajo> ObtenerTiposAnalisisPT()
        {
            return tipoanalisispt.ObtenerTiposAnalisisPT();
        }
    }
}
