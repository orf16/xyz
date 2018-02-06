using SG_SST.Interfaces.EnfermedadLaboral;
using SG_SST.Repositorio.EnfermedadesLaborales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.InterfazManager.EnfermedadLaboral
{
    public class IMEnfermedadLaboral
    {
        public static IEnfermedadLaboral EnfermedadLaboral()
        {
            return new EnfermedadLaboralManager();
        }
    }
}
