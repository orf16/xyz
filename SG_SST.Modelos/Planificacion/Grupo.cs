// <copyright file="TipoDePeligro.cs" company="Ada SA">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Cristian Mauricio Arenas Gomez.</author>
// <date>06/01/2017</date>
// <summary>Modelo que contiene la informacion de los grupos a evaluar.</summary>

namespace SG_SST.Models.Planificacion
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Tbl_Grupo")]
    public class Grupo
    {
        /// <summary>
        /// Obtiene y establece la clave primaria de los grupos.
        /// </summary>
        [Key]
        public int PK_Grupo { get; set; }

        /// <summary>
        /// Obtiene y establece la descripcion o nombre del grupo
        /// </summary>
        public string Descripcion_Grupo { get; set; }

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

        // <summary>
        /// Obtiene y establece una coleccion de Consecuencias.
        /// </summary>
        public ICollection<Consecuencia> Consecuencias { get; set; }
    }
}