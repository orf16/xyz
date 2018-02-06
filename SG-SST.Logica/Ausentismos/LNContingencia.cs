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
    public class LNContingencia
    {
        private static IContingencia contingenciaMG = IMAusentismo.Contingencia();

        public IEnumerable<EDContingencia> ObtenerListadoContingencia()
        {
            var resultado = contingenciaMG.ObtenerContingencia();
            return resultado;
        }
        public List<EDContingencia> AutoCompletarContingencia(string prefijo)
        {
            var respuesta = contingenciaMG.BuscarContingencia(prefijo);
            return respuesta;
        }
    }
}
