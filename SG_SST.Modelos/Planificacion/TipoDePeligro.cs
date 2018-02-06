// <copyright file="TipoDePeligro.cs" company="Ada SA">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Cristian Mauricio Arenas Gomez.</author>
// <date>06/01/2017</date>
// <summary>Modelo que contiene la informacion de la clasificacion de los peligros.</summary>

namespace SG_SST.Models.Planificacion
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Web;

    [Table("Tbl_Tipo_De_Peligro")]
    public class TipoDePeligro
    {
        /// <summary>
        /// Obtiene y establece la clave primaria del tipo de peligros.
        /// </summary>
        [Key]
        public int PK_Tipo_De_Peligro { get; set; }

        /// <summary>
        /// Obtiene y establece la descripcion del tipo de peligro.
        /// </summary>
        public string Descripcion_Del_Peligro { get; set; }

        /// <summary>
        /// Obtiene y establece una coleccion de Clasificacion de peligros.
        /// </summary>
        public ICollection<ClasificacionDePeligro> ClasificacionDePeligros { get; set; }
    }
}