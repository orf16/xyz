// <copyright file="ClasificacionDePeligro.cs" company="Ada SA">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Cristian Mauricio Arenas Gomez.</author>
// <date>06/01/2017</date>
// <summary>Modelo que contiene la informacion de la clasificacion de los peligros.</summary>

namespace SG_SST.Models.Planificacion
{
    using Aplicacion;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Tbl_Clasificacion_De_Peligro")]
    public class ClasificacionDePeligro
    {
        /// <summary>
        /// Obtiene y establece la clave primaria de la clasificaicon del peligro.
        /// </summary>
        [Key]
        public int PK_Clasificacion_De_Peligro { get; set; }

        /// <summary>
        /// Obtiene y establece la descripcion de la clase  de peligro.
        /// </summary>
        public string Descripcion_Clase_De_Peligro { get; set; }

        /// <summary>
        /// Obtiene y establece el detalle de la clase  de peligro.
        /// </summary>
        public string Detalle_Clase_De_Peligro { get; set; }

        /// <summary>
        /// Obtiene y establece la clave foranea a la tabla de tipo de peligro.
        /// </summary>
        [ForeignKey("TipoDePeligro")]
        public int FK_Tipo_De_Peligro { get; set; }

        /// <summary>
        /// Obtiene y establece un objeto de TipoDePeligro.
        /// </summary>
        [ForeignKey("PK_Tipo_De_Peligro")]
        public virtual TipoDePeligro TipoDePeligro { get; set; }

        /// <summary>
        /// Obtiene y establece una coleccion de peligros.
        /// </summary>
        public ICollection<Peligro> Peligros { get; set; }

        /// <summary>
        /// Obtiene y establece una coleccion de elementos de protección personal.
        /// </summary>
        public ICollection<EPP> EPPs { get; set; }
    }
}