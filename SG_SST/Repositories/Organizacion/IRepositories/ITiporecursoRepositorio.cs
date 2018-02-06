using SG_SST.Models.Organizacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Repositories.Organizacion.IRepositories
{
    interface ITiporecursoRepositorio
    {

        /// <summary>
        /// Definicion del Metodo que me graba un tipo de Recurso 
        /// </summary>
        /// <param name="fase">Grabar Recurso </param>
        /// <returns>retorna si fue exitosa o no el guardado el Tipo Recurso</returns>
        bool GuardarTipoRecurso(TipoRecurso tiporecurso);



        /// <summary>
        /// Definicion del Metodo que trae lista de Tipo Recursos.
        /// 
        /// </summary>
        /// <param name="TipoRecurso">Listar Tipos de Recursos </param>
        /// <returns></returns>

        List<TipoRecurso> ObtenerTipoRecurso();
    }
}
