using SG_SST.EntidadesDominio.Planificacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Interfaces.Planificacion
{
    public interface IConsecuencias
    {
        /// <summary>
        /// Definicion del repositorio que me retorna todas las consecuencias por  tipo de metodologias 
        /// </summary>
        /// <param name="PK_TipoMedologia">id/pk del tipo de metodologia</param>
        /// <returns></returns>
        List<EDConsecuencia> ObtenerConsecuencias(int PK_TipoMedologia);

        /// <summary>
        /// Definicion del repositorio que me retorna todas las consecuencias por  grupo
        /// </summary>
        /// <param name="PK_TipoMedologia">id/pk del grupo</param>
        /// <returns></returns>
        List<EDConsecuencia> ObtenerConsecuenciasPorGrupo(int PK_Grupo);
    }
}
