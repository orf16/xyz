using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SG_SST.Models.Organizacion;

namespace SG_SST.Repositories.Organizacion.IRepositories
{
    interface IFaseRepositorio
    {
        /// <summary>
        /// Definicion del Metodo que me graba una fase 
        /// </summary>
        /// <param name="fase">Grabar Fase </param>
        /// <returns>retorna si fue exitosa o no el guardadola fase</returns>
        bool GuardarFase(Fase fase);


        /// <summary>
        /// Definicion del Metodo que me modifica una fase
        /// 
        /// </summary>
        /// <param name="fase">Modificar fase </param>
        /// <returns></returns>
        void ModificarFase(Fase fase);

         /// <summary>
        /// Definicion del Metodo que trae lista de Fases
        /// 
        /// </summary>
        /// <param name="fase">Listar fases </param>
        /// <returns></returns>
        
        List<Fase> ObtenerFase();

        
    }
}
