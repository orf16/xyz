// <copyright file="IEstimacionDeRiesgosRepositorio.cs" company="Ada SA">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Cristian Mauricio Arenas Gomez.</author>
// <date>11/01/2017</date>
// <summary>Interfaz  que contiene la definicion de los metodos a implementar para la realizar las operaciones con la base datos
// de la estimacion de riesgos.</summary>

namespace SG_SST.Repositories.Planificacion.IRepositories
{
    using SG_SST.Models.Planificacion;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    interface IEstimacionDeRiesgosRepositorio
    {
        /// <summary>
        /// Definicion del metodo que me retorna  el detalle de la estimacion que es una combinacion
        /// de la probabilidad y la consecuencia
        /// </summary>
        /// <param name="Pk_Probabilidad">clave primaria de probabilidad</param>
        /// <param name="PK_Consecuencia">clave primaria de consecuencias</param>
        /// <returns>la estimacion de riesgo</returns>
        EstimacionDeRiesgo ObtenerDetalleEstimacion(int Pk_Probabilidad, int PK_Consecuencia);
    }
}
