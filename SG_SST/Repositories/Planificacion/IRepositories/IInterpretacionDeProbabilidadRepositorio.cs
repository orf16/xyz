// <copyright file="IInterpretacionDeProbabilidadRepositorio.cs" company="Ada SA">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Cristian Mauricio Arenas Gomez.</author>
// <date>13/01/2017</date>
// <summary>Interfaz  que contiene la definicion de los metodos a implementar para la realizar las operaciones con la base datos
// de la clases InterpretacionDeProbabilidad.</summary>

namespace SG_SST.Repositories.Planificacion.IRepositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    interface IInterpretacionDeProbabilidadRepositorio
    {

        /// <summary>
        /// Definicion del metodo que me retorna la interpretacion(descripcion) de probabilidad.
        /// </summary>
        /// <param name="valor_De_Probalidad">valor o cantidad de probablidad</param>
        /// <returns>descripcion o interpretacion de la probabilidad</returns>
        string ConsultarInterpretacion(int valor_De_Probalidad);

    }
}
