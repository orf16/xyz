// <copyright file="RAM.cs" company="Ada SA">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Cristian Mauricio Arenas Gomez.</author>
// <date>06/01/2017</date>
// <summary>Modelo que contiene la informacion del peligro de la metodologia RAM.</summary>

namespace SG_SST.Models.Planificacion
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Web;

    [Table("Tbl_RAM")]
    public class RAM 
    {
        /// <summary>
        /// Obtiene y establece la clave primaria del peligro de la metodologia RAM.
        /// </summary>
        [Key]
        public int PK_RAM { get; set; }

        /// <summary>
        /// Obtiene y establece las consecuencias reales del peligro.
        /// </summary>
        public string Consecuencias_Reales { get; set; }

        /// <summary>
        /// Obtiene y establece las consecuencias potenciales del peligro.
        /// </summary>
        public string Consecuencias_Potenciales { get; set; }

        /// <summary>
        /// Obtiene y establece el nivel de riesgo.
        /// </summary>
        public string Nivel_De_Riesgo { get; set; }

        /// <summary>
        /// Obtiene y establece la clave foranea a la tabla  personas expuesta.
        /// </summary>
        [ForeignKey("PersonaExpuesta")]
        public int FK_Persona_Expuesta { get; set; }

        /// <summary>
        /// Obtiene y establece un objeto de tipo de PersonaExpuesta.
        /// </summary>
        [ForeignKey("PK_Persona_Expuesta")]
        public virtual PersonaExpuesta PersonaExpuesta { get; set; }

        /// <summary>
        /// obtiene y estable una coleccion de estimacion de riesgos por metodologia Ram
        /// </summary>
        public ICollection<EstimacionDeRiesgoPorRAM> EstimacionDeRiesgosPorRAM { get; set; }
   
    }
}