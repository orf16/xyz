// <copyright file="InterpretacionDeProbabilidad.cs" company="Ada SA">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Cristian Mauricio Arenas Gomez.</author>
// <date>07/01/2017</date>
// <summary>Modelo que contiene la informacion de interpretacion de probabilidad.</summary>

namespace SG_SST.Models.Planificacion
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Tbl_Interpretacion_De_Probabilidad")]
    public class InterpretacionDeProbabilidad
    {
        /// <summary>
        /// Obtiene y establece la clave primaria de una interpretacion de probabilidad.
        /// </summary>
        [Key]
        public int PK_Interpretacion_De_Probabilidad { get; set; }

        /// <summary>
        /// Obtiene y establece el nivel inferior de la probabilidad.
        /// </summary>
        public int Nivel_Inferior { get; set; }

        /// <summary>
        /// Obtiene y establece el nivel superior de la probabilidad.
        /// </summary>
        public int Nivel_Superior { get; set; }

        /// <summary>
        /// Obtiene y establece la descripcion o interpretacion de la probabilidad.
        /// </summary>
        public string Interpretacion { get; set; }
    }
}