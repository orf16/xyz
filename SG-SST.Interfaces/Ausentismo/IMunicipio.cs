using SG_SST.EntidadesDominio.Ausentismo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Interfaces.Ausentismo
{
    public interface IMunicipio
    {
        IEnumerable<EDMunicipio> ObtenerMunicipio(int idDepto);        
        List<EDMunicipio> ObtenerMunicipiosConDetps();
        int ValidarMunicipio(int idMunicipio, int idDepartamento);
    }
}
