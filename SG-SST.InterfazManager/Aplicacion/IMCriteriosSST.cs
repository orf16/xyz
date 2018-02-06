using System;

namespace SG_SST.InterfazManager.Aplicacion
{
    using SG_SST.Interfaces.Aplicacion;
    using SG_SST.Repositorio.Aplicacion;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public class IMCriteriosSST
    {
        public static ICriterioSST Criterios()
        {
            return new CriteriosSSTManager();
        }
    }
}
