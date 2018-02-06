using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SG_SST.Interfaces.Aplicacion;
using SG_SST.Repositorio.Aplicacion;

namespace SG_SST.InterfazManager.Aplicacion
{
    public class IMEPP
    {
        public static IEPP IEPP()
        {
            return new EPPManager();
        }
    }
}
