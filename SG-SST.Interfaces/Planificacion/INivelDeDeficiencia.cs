using SG_SST.EntidadesDominio.Planificacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Interfaces.Planificacion
{
    public interface INivelDeDeficiencia
    {
        /// <summary>
        /// Definicion del metodos que me retornas los niveles de deficiencia filtrados por el tipo de deficiencia 
        /// Cuantitativa y Quimico
        /// </summary>
        /// <param name="FLAG_Cuantitativa">verdadero para tipo cuantitiva</param>    
        /// <returns></returns>
        List<EDNivelDeDeficiencia> ObtenerNivelesDeDeficiencia(bool FLAG_Cuantitativa);
    }
}
