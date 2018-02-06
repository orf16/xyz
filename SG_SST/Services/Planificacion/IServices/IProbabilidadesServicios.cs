// <copyright file="IProbabilidadesServicios.cs" company="Ada SA">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Cristian Mauricio Arenas Gomez.</author>
// <date>19/01/2017</date>
// <summary>Interfaz  que contiene la definicion de los metodos(Servicios) a implementar para las probabilidades de los peligros.</summary>


namespace SG_SST.Services.Planificacion.IServices
{
    using SG_SST.Models.Planificacion;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    interface IProbabilidadesServicios
    {
        /// <summary>
        /// Definicion del servicio que me retorna todas las Probabilidades por  tipo de metodologias 
        /// </summary>
        /// <param name="PK_TipoMedologia">id/pk del tipo de metodologia</param>
        /// <returns></returns>
        List<Probabilidad> ObtenerProbabilidades(int PK_TipoMedologia);
    }
}
