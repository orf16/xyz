// <copyright file="PersonaExpuesta.cs" company="Ada SA">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Cristian Mauricio Arenas Gomez.</author>
// <date>16/01/2017</date>
// <summary>Modelo que contiene la informacion de las personas expuestas al peligro de las metodologia
//de tipo INSHT y RAM </summary>

namespace SG_SST.Models.Planificacion
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Web;

    [Table("Tbl_Persona_Expuesta_INSHT_RAM")]
    public class PersonaExpuesta
    {
        /// <summary>
        /// Obtiene y establece la clave primaria la persona expuesta.
        /// </summary>
        [Key]
        public int PK_Persona_Expuesta { get; set; }

        /// <summary>
        /// Obtiene y establece la cantidad de personas de planta de la empresa expuestas al peligro.
        /// </summary>
        public int Planta_Directo { get; set; }

        /// <summary>
        /// Obtiene y establece la cantidad de personas contratista de la empresa expuestas al peligro.
        /// </summary>
        public int Contratista { get; set; }

        /// <summary>
        /// Obtiene y establece  la cantidad de personas temporales de la empresa expuestas al peligro.
        /// </summary>
        public int Temporal { get; set; }

        /// <summary>
        /// Obtiene y establece  la cantidad de personas estudiantes o pasantes de la empresa expuestas al peligro.
        /// </summary>
        public int Estudiante_Pasante { get; set; }

        /// <summary>
        /// Obtiene y establece la cantidad de personas visitantes a la empresa expuestas al peligro.
        /// </summary>
        public int Visitante { get; set; }

        /// <summary>
        /// Obtiene y establece la sumatoria de los campos: Planta o directo, Contratista, Temporales, Estudiantes/Pasantes, Visitantes
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// Obtiene y establece las horas de exposición al peligro del personal directo.
        /// </summary>
        public int Horas_De_Exposicion_Planta { get; set; }

        /// <summary>
        /// Obtiene y establece las horas de exposición al peligro del personal contratista.
        /// </summary>
        public int Horas_De_Exposicion_Contratista{ get; set; }

        /// <summary>
        /// Obtiene y establece las horas de exposición al peligro del personal Temporal.
        /// </summary>
        public int Horas_De_Exposicion_Temporal { get; set; }

        /// <summary>
        /// Obtiene y establece las horas de exposición al peligro del personal Estudiante.
        /// </summary>
        public int Horas_De_Exposicion_Estudiante { get; set; }

        /// <summary>
        /// Obtiene y establece las horas de exposición al peligro del personal Visitante.
        /// </summary>
        public int Horas_De_Exposicion_Visitante { get; set; }
       

        /// <summary>
        /// Obtiene y establece la clave foranea a la tabla  peligro.
        /// </summary>
        [ForeignKey("Peligro")]
        public int FK_Peligro { get; set; }

        /// <summary>
        /// Obtiene y establece un objeto de tipo de Peligro.
        /// </summary>
        [ForeignKey("PK_Peligro")]
        public virtual Peligro Peligro { get; set; }

        
        /// <summary>
        /// Obtiene y establece una coleccion de INSHT.
        /// </summary>
        public ICollection<INSHT> INSHT { get; set; }

        /// <summary>
        /// Obtiene y establece una coleccion de RAM.
        /// </summary>
        public ICollection<RAM> RAM { get; set; }

         /// <summary>
        /// Obtiene y establece una coleccion de ProbabilidadPorPersonaExpuesta.
        /// </summary>
        public ICollection<ProbabilidadPorPersonaExpuesta> ProbabilidadesPorPersonasExpuestas { get; set; }
        
    }


}