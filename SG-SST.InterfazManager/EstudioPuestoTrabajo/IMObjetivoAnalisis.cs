using SG_SST.Interfaces.EstudioPuestoTrabajo;
using SG_SST.Repositorio.EstudioPuestoTrabajo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.InterfazManager.EstudioPuestoTrabajo
{
    public class IMObjetivoAnalisis
    {
        public static IObjetivoAnalisis ObjetivoAnalisis()
        {
            return new ObjetivoAnalisisManager();
        }
    }
}
