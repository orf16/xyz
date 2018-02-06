using SG_SST.EntidadesDominio.Aplicacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Interfaces.Aplicacion
{
   public interface IInspeccion
    {
        List<EDTipoInspeccion> ObtenerTiposInspeccion();

        

    }
}
