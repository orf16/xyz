using SG_SST.EntidadesDominio.Empresas;
using SG_SST.Interfaces.Empresas;
using SG_SST.InterfazManager.Empresas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Logica.Empresas
{
    public class LNProcesos
    {
        private static IEmpresa em = IMEmpresa.Empresa();

        public List<EDProceso> ObtenerProcesosPorEmpresa(string Nit)
        {
            return em.ObtenerProcesosPorEmpres(Nit);
        }
    }
}
