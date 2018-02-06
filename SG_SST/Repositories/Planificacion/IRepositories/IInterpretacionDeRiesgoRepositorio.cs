// <copyright file="IInterpretacionDeRiesgoRepositorio.cs" company="Ada SA">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Cristian Mauricio Arenas Gomez.</author>
// <date>13/01/2017</date>
// <summary>Interfaz  que contiene la definicion de los metodos a implementar para la realizar las operaciones con la base datos
// de la clases InterpretacionNivelDeRiesgo.</summary>

namespace SG_SST.Repositories.Planificacion.IRepositories
{
    using SG_SST.Models.Planificacion;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Helpers;

    interface IInterpretacionDeRiesgoRepositorio
    {
        /// <summary>
        /// Definicion del metodo que un objeto de tipo interpretacion de riesgo buscandolo por el valor de riesgo.
        /// </summary>
        /// <param name="valor_De_Probalidad">valor o cantidad del riesgo</param>
        /// <returns>un objeto de tipo Intepretacion de riesgo</returns>
        InterpretacionNivelDeRiesgo ObtenerInterpretacionDeRiesgo(int valor_Del_Riesgo);
    }
}
