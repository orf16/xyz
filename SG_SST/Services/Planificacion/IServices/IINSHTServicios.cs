// <copyright file="IINSHTServicios.cs" company="Ada SA">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Cristian Mauricio Arenas Gomez.</author>
// <date>13/01/2017</date>
// <summary>Interfaz  que contiene la definicion de los metodos(Servicios) a implementar para la gestion del peligro del tipo de metodologia INSHT.</summary>

namespace SG_SST.Services.Planificacion.IServices
{
    using SG_SST.Models.Planificacion;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    interface IINSHTServicios
    {
        /// <summary>
        /// Definicion del metodo que me retorna  el detalle de la estimacion que es una combinacion
        /// de la probabilidad y la consecuencia
        /// </summary>
        /// <param name="Pk_Probabilidad">clave primaria de probabilidad</param>
        /// <param name="PK_Consecuencia">clave primaria de consecuencias</param>
        /// <returns>la estiamacion de riesgo</returns>
        EstimacionDeRiesgo ObtenerEstimacionDelRiesgo(int Pk_Probabilidad, int PK_Consecuencia);
    }
}
