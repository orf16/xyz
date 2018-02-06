using SG_SST.Models.Organizacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Services.Organizacion.Iservices
{
    interface IFaseServices
    {
        /// <summary>
        /// Definicion del Metodo que me graba una fase 
        /// </summary>
        /// <param name="fase">Grabar Fase </param>
        /// <returns>retorna si fue exitosa o no el guardadola fase</returns>
        bool GuardarFase(Fase fase);




        List<Fase> ObtenerFase();
    }




    
}
