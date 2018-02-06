// <copyright file="Consecuencia.cs" company="Ada SA">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author> Cristian Mauricio Arenas Gomez.</author>
// <date>07/01/2017</date>
// <summary>Modelo que contiene la informacion de las consecuencias de los peligros.</summary>

namespace SG_SST.Models.Planificacion
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Web;

    [Table("Tbl_Consecuencia")]
    public class Consecuencia
    {

        /// <summary>
        /// Obtiene y establece la clave primaria de la consecuencia.
        /// </summary>
        [Key]
        public int PK_Consecuencia { get; set; }

        /// <summary>
        /// Obtiene y establece el valor de la consecuencia.
        /// </summary>
        public int Valor_Consecuencia { get; set; }

        /// <summary>
        /// Obtiene y establece la descripcion o el nombre de la consecuencia.
        /// </summary>
        public string Descripcion_Consecuencia { get; set; }

        /// <summary>
        /// Obtiene y establece la clave foranea a la tabla  Metodologia.
        /// </summary>
        [ForeignKey("Grupo")]
        public int FK_Grupo { get; set; }

        /// <summary>
        /// Obtiene y establece un objeto de tipo metodologia.
        /// </summary>
        [ForeignKey("PK_Grupo")]
        public virtual Grupo Grupo { get; set; }

        ///// <summary>
        ///// Obtiene y establece una coleccion de peligros.
        ///// </summary>
        //public ICollection<Peligro> Peligros { get; set; }

        /// <summary>
        /// Obtiene y establece una coleccion de EstimacionDeRiesgo.
        /// </summary>
        public ICollection<EstimacionDeRiesgo> EstimacionesDeRiesgos { get; set; }

    }
}