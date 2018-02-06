// <copyright file="IConsecuenciasServicios.cs" company="Ada SA">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Cristian Mauricio Arenas Gomez.</author>
// <date>12/01/2017</date>
// <summary>Interfaz  que contiene la definicion de los metodos(Servicios) a implementar para las consecuencias de los peligros.</summary>

namespace SG_SST.Services.Planificacion.IServices
{
    using SG_SST.Models.Planificacion;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    interface IConsecuenciasServicios
    {
        /// <summary>
        /// Definicion del servicio que me retorna todas las consecuencias por  tipo de metodologias 
        /// </summary>
        /// <param name="PK_TipoMedologia">id/pk del tipo de metodologia</param>
        /// <returns></returns>
        List<Consecuencia> ObtenerConsecuencias(int PK_TipoMedologia);

        /// <summary>
        /// Defincion del metodo que me retornar el valor de la consecuencia del peligro
        /// </summary>
        /// <param name="PK_Consecuencia">clave o id de la consecuencia</param>
        /// <returns>valor de la consecuencia</returns>
        int ObtenerValorConsecuencia(int PK_Consecuencia);

        /// <summary>
        /// Definicion del servicio que me retorna todas las consecuencias por grupo
        /// </summary>
        /// <param name="PK_TipoMedologia">id/pk del grupo</param>
        /// <returns></returns>
        List<Consecuencia> ObtenerConsecuenciasPorGrupo(int PK_Grupo);
    }
}
