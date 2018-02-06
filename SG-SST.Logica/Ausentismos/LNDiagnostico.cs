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
    public class LNDiagnostico
    {
        private static IDiagnostico diagnosticoMG = IMAusentismo.Diagnostico();

        public IEnumerable<EDDiagnostico> ObtenerListadoDisagnostico()
        {
            var resultado = diagnosticoMG.ObtenerDiagnostico();
            return resultado;
        }
        public List<EDDiagnostico> AutoCompletarDiagnostico(string prefijo)
        {
            var respuesta = diagnosticoMG.BuscarDiagnostico(prefijo);
            return respuesta;
        }

        public IEnumerable<EDDiagnostico>  ObtenerListadoDisagnostico(string idEmpresa)
        {
            var resultado = diagnosticoMG.ObtenerDiagnostico(idEmpresa);
            return resultado;
        }
    }
}
