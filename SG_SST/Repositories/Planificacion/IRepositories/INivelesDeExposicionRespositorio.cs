// <copyright file="INivelesDeExposicionRespositorio.cs" company="Ada SA">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Cristian Mauricio Arenas Gomez.</author>
// <date>13/01/2017</date>
// <summary>Interfaz  que contiene la definicion de los metodos a implementar para la realizar las operaciones con la base datos
// de la clases de NivelesDeExposicion.</summary>

namespace SG_SST.Repositories.Planificacion.IRepositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    interface INivelesDeExposicionRespositorio
    {
        /// <summary>
        /// Defincion del metodo que me retornar el valor de la exposicion del peligro
        /// </summary>
        /// <param name="PK_Deficiencia">clave o id de la deficiencia</param>
        /// <returns>valor de la Exposicion</returns>
        int ObtenerValorExposicion(int PK_Exposicion);
        

    }
}
