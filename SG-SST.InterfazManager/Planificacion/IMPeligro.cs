using SG_SST.Interfaces.Planificacion;
using SG_SST.Repositorio.Planificacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.InterfazManager.Planificacion
{
    public class IMPeligro
    {
        public static IPeligro Peligro() 
        {
            return new PeligroManager();
        }
    }
}
