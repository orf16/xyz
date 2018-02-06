
namespace SG_SST.Services.Organizacion.Iservices
{

using SG_SST.Models.Organizacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
    interface ITiporecursoServices
    {
        /// <summary>
        /// Definicion del Metodo que me graba una fase 
        /// </summary>
        /// <param name="fase">Grabar Fase </param>
        /// <returns>retorna si fue exitosa o no el guardadola fase</returns>
        bool GuardarTipoRecurso(TipoRecurso tiporecurso);

        List<TipoRecurso> ObtenerTipoRecurso();
    }

}
