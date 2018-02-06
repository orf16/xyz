using SG_SST.EntidadesDominio.Planificacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Interfaces.Planificacion
{
    public interface INivelesDeExposicion
    {
        /// <summary>
        /// Definicion del repositorio que me retornar los niveles de exposicion 
        /// </summary>
        /// <returns>lista de nivles de exposicion</returns>
        List<EDNivelDeExposicion> ObtenerNivelesDeExposicion();
    }
}
