// <copyright file="ProbabilidadPorPersonaExpuesta.cs" company="Ada SA">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author> Cristian Mauricio Arenas Gomez.</author>
// <date>01/02/2017</date>
// <summary>Modelo que contiene la informacion de las probabilidades por persona expuesta.</summary>
namespace SG_SST.Models.Planificacion
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Web;

    [Table("Tbl_Probabilidad_Por_PersonaExpuesta")]
    public class ProbabilidadPorPersonaExpuesta
    {
        /// <summary>
        /// Obtiene y establece la clave primaria.
        /// </summary>
        [Key]
        public int PK_Probabilidad_Por_PersonaExpuesta { get; set; }

        /// <summary>
        /// Obtiene y establece la clave foranea a la tabla  probalidad.
        /// </summary>
        [ForeignKey("Probabilidad")]
        public int FK_Probabilidad { get; set; }

        /// <summary>
        /// Obtiene y establece un objeto de tipo de probalidad.
        /// </summary>
        [ForeignKey("PK_Probabilidad")]
        public virtual Probabilidad Probabilidad { get; set; }

        /// <summary>
        /// Obtiene y establece la clave foranea a la tabla  personas expuesta.
        /// </summary>
        [ForeignKey("PersonaExpuesta")]
        public int Fk_Persona_Expuesta { get; set; }

        /// <summary>
        /// Obtiene y establece un objeto de tipo de PersonaExpuesta.
        /// </summary>
        [ForeignKey("PK_Persona_Expuesta")]
        public virtual PersonaExpuesta PersonaExpuesta { get; set; }

    }
}