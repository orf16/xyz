// <copyright file="INivelDeDeficienciaServicios.cs" company="Ada SA">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Cristian Mauricio Arenas Gomez.</author>
// <date>11/01/2017</date>
// <summary>Clase que contiene toda la implementacion de la Interfaz INivelDeDeficienciaServicios y servicios para las
// la gestion de los niveles de deficiencia</summary>

namespace SG_SST.Services.Planificacion.IServices
{
    using SG_SST.Models.Planificacion;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    interface INivelDeDeficienciaServicios
    {
        /// <summary>
        /// Definicion del metodos que me retornas los niveles de deficiencia filtrados por el tipo de deficiencia 
        /// Cuantitativa y Quimico
        /// </summary>
        /// <param name="FLAG_Cuantitativa">verdadero para tipo cuantitiva</param>      
        /// <returns></returns>
        List<NivelDeDeficiencia> ConsultarNivelesDeDeficiencia(bool FLAG_Cuantitativa);
    }
}
