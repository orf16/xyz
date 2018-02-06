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
    public class LNMunicipio
    {
        private static IMunicipio municipioMG = IMAusentismo.Municipio();

        public IEnumerable<EDMunicipio> ObtenerListadoMunicipio(int idDepto)
        {
            var resultado = municipioMG.ObtenerMunicipio(idDepto);
            return resultado;
        }       
    }
}
