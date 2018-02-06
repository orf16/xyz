using SG_SST.EntidadesDominio.Ausentismo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Interfaces.Ausentismo
{
    public interface IDiagnostico
    {
        IEnumerable<EDDiagnostico> ObtenerDiagnostico();
        IEnumerable<EDDiagnostico> ObtenerDiagnostico(string idEmpresa);
        EDDiagnostico ObtenerGiagnosticoPorCodigo(string codigo);

        List<EDDiagnostico> BuscarDiagnostico(string prefijo);
    }
}
