using SG_SST.EntidadesDominio.Planificacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Interfaces.Planificacion
{
    public interface IProbabilidad
    {
        /// <summary>
        /// Definicion del repositorio que me retorna todas las probabilidades por  tipo de metodologias 
        /// </summary>
        /// <param name="PK_TipoMedologia">id/pk del tipo de metodologia</param>
        /// <returns></returns>
        List<EDProbabilidad> ObtenerProbabilidades(int PK_TipoMedologia);
    }
}
