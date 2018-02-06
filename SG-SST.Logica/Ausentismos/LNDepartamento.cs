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
    public class LNDepartamento
    {
        private static IDepartamento departamentoMG = IMAusentismo.Departamento();

        public IEnumerable<EDDepartamento> ObtenerListadoDepartamento()
        {
            var resultado = departamentoMG.ObtenerDepartamento();
            return resultado;
        }
    }
}
