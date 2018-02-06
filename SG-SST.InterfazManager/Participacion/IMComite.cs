using SG_SST.Interfaces.Participacion;
using SG_SST.Repositorio.Participacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.InterfazManager.Participacion
{
    public class IMComite
    {
        public static IComite Comite()
        {
            return new ComiteManager();
        }

    }
}
