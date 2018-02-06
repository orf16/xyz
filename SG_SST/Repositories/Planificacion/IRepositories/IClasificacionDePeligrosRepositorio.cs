// <copyright file="IClasificacionDePeligrosRepositorio.cs" company="Ada SA">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Cristian Mauricio Arenas Gomez.</author>
// <date>06/01/2017</date>
// <summary>Interfaz  que contiene la definicion de los metodos a implementar para la realizar las operaciones con la base datos
// de la clases de peligros.</summary>

namespace SG_SST.Repositories.Planificacion.IRepositories
{
    using SG_SST.Models.Planificacion;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    interface IClasificacionDePeligrosRepositorio
    {
        /// <summary>
        /// Definicion del metodo que me retona una lista de clases de peligros 
        /// consultadolas por su tipo de peligro
        /// </summary>
        /// <param name="Pk_Tipo_Peligro">id o clave primaria del tipo de de peligro</param>
        /// <returns>Lista de clases de peligros</returns>
        List<ClasificacionDePeligro> ConsultarClasesDePeligros(int Pk_Tipo_Peligro);
    }
}
