// <copyright file="INivelDeDeficienciaRepositorio.cs" company="Ada SA">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Cristian Mauricio Arenas Gomez.</author>
// <date>06/01/2017</date>
// <summary>Interfaz  que contiene la definicion de los metodos a implementar para la realizar las operaciones con la base datos
// de la clases de NivelDeDeficiencia.</summary>

namespace SG_SST.Repositories.Planificacion.IRepositories
{
    using SG_SST.Models.Planificacion;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    interface INivelDeDeficienciaRepositorio
    {
        /// <summary>
        /// Definicion del metodos que me retornas los niveles de deficiencia filtrados por el tipo de deficiencia 
        /// Cuantitativa y Quimico
        /// </summary>
        /// <param name="FLAG_Cuantitativa">verdadero para tipo cuantitiva</param>    
        /// <returns></returns>
        List<NivelDeDeficiencia> ConsultarNivelesDeDeficiencia(bool FLAG_Cuantitativa);

        /// <summary>
        /// Defincion del metodo que me retornar el valor de la deficiencia del peligro
        /// </summary>
        /// <param name="PK_Deficiencia">clave o id de la deficiencia</param>
        /// <returns>valor de la deficiencia</returns>
        int ObtenerValorDeficiencia(int PK_Deficiencia);
        
        

        

    }
}
