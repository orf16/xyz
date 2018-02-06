// <copyright file="InterpretacionNivelDeRiesgo.cs" company="Ada SA">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Cristian Mauricio Arenas Gomez.</author>
// <date>07/01/2017</date>
// <summary>Modelo que contiene la informacion de interpretacion del nivel de riesgo.</summary>

namespace SG_SST.Models.Planificacion
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Web;

    [Table("Tbl_Interpretacion_Nivel_Riesgo")]
    public class InterpretacionNivelDeRiesgo
    {
        /// <summary>
        /// Obtiene y establece la clave primaria de una interpretacion de riesgo.
        /// </summary>
        [Key]
        public int PK_Interpretacion_Nivel_Riesgo { get; set; }

        /// <summary>
        /// Obtiene y establece el nivel inferior del nivel de riesgo.
        /// </summary>
        public int Nivel_Inferior { get; set; }

        /// <summary>
        /// Obtiene y establece el nivel superior del nivel de riesgo.
        /// </summary>
        public int Nivel_Superior { get; set; }

        /// <summary>
        /// Obtiene y establece el resultado en numero romanos del nivel de riesgo .
        /// </summary>
        public string Resultado { get; set; }

        /// <summary>
        /// Obtiene y establece la descripcion o interpretacion del nivel de riesgo.
        /// </summary>
        public string Interpretacion { get; set; }


    }
}