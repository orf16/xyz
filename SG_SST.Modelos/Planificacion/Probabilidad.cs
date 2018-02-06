// <copyright file="Probabilidad.cs" company="Ada SA">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Cristian Mauricio Arenas Gomez.</author>
// <date>07/01/2017</date>
// <summary>Modelo que contiene la informacion los niveles de probabilidad</summary>

namespace SG_SST.Models.Planificacion
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Web;

     [Table("Tbl_Probabilidad")]
    public class Probabilidad
    {
        /// <summary>
        /// Obtiene y establece la clave primaria de la probalidad.
        /// </summary>
        [Key]
         public int PK_Probabilidad { get; set; }

        /// <summary>
        /// Obtiene y establece el detalle o la descripcion de la probabilidad.
        /// </summary>
        public string Descripcion_Probabilidad { get; set; }

        /// <summary>
        /// Obtiene y establece la clave foranea a la tabla  Metodologia.
        /// </summary>
        [ForeignKey("Metodologia")]
        public int FK_Metodologia { get; set; }

        /// <summary>
        /// Obtiene y establece un objeto de tipo metodologia.
        /// </summary>
        [ForeignKey("PK_Metodologia")]
        public virtual TipoMetodologia Metodologia { get; set; }

        /// <summary>
        /// Obtiene y establece una coleccion de EstimacionDeRiesgo.
        /// </summary>
        public ICollection<EstimacionDeRiesgo> EstimacionesDeRiesgos { get; set; }

        /// <summary>
        /// Obtiene y establece una coleccion de ProbabilidadPorPersonaExpuesta.
        /// </summary>
        public ICollection<ProbabilidadPorPersonaExpuesta> ProbabilidadesPorPersonasExpuestas { get; set; }
        
    }
}