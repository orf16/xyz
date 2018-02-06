// <copyright file="ConsecuenciaPorPeligro.cs" company="Ada SA">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author> Cristian Mauricio Arenas Gomez.</author>
// <date>01/02/2017</date>
// <summary>Modelo que contiene la informacion de las consecuencias por cada  peligros.</summary>
namespace SG_SST.Models.Planificacion
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Web;


    [Table("Tbl_Consecuencia_Por_Peligro")]
    public class ConsecuenciaPorPeligro
    {
        /// <summary>
        /// Obtiene y establece la clave primaria de las consecuencias por Peligro.
        /// </summary>
        [Key]
        public int PK_Consecuencia_Por_Peligro { get; set; }

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
        /// Obtiene y establece la clave foranea a la tabla  consecuencia.
        /// </summary>
        [ForeignKey("Consecuencia")]
        public int FK_Consecuencia { get; set; }

        /// <summary>
        /// Obtiene y establece un objeto de tipo consecuencia.
        /// </summary>
        [ForeignKey("PK_Consecuencia")]
        public virtual Consecuencia Consecuencia { get; set; }

    }
}