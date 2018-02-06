using SG_SST.EntidadesDominio.Planificacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Interfaces.Planificacion
{
    public interface ITipoDePeligro
    {
        /// <summary>
        /// Definicion del repositorio que me retorna todos los tipos de peligros
        /// </summary>
        /// <returns>iista de peligros</returns>
        List<EDTipoDePeligro> ObtenerTiposDePeligro();
    }
}
