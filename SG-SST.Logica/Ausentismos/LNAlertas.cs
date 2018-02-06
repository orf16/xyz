using SG_SST.EntidadesDominio.Ausentismo;
using SG_SST.Interfaces.Ausentismo;
using SG_SST.InterfazManager.Ausentismo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Logica.Ausentismo
{
    public class LNAlertas
    {
        private static IAlertas alertasMG = IMAusentismo.Alertas();

        public List<EDAlertas> ConsultarAusencias(int anio)
        {
            var respuesta = alertasMG.ConsultarAlertas(anio);
            return respuesta;
        }
    }
}
