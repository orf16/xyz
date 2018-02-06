using SG_SST.EntidadesDominio.Ausentismo;
using SG_SST.Interfaces.Ausentismo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Interfaces.Ausentismo
{
    public interface IAlertas
    {
        List<EDAlertas> ConsultarAlertas(int anio);

    }
}
