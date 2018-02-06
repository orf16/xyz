using SG_SST.EntidadesDominio.EstudioPuestoTrabajo;
using SG_SST.Interfaces.EstudioPuestoTrabajo;
using SG_SST.InterfazManager.EstudioPuestoTrabajo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Logica.EstudioPuestoTrabajo
{
    public class LNObjetivoAnalisis
    {
        private static IObjetivoAnalisis objanalisis = IMObjetivoAnalisis.ObjetivoAnalisis();

        public List<EDObjetivoAnalisis> ObtenerObjetivosAnalisis()
        {
            return objanalisis.ObtenerObjetivosAnalisis();
        }
    }
}
