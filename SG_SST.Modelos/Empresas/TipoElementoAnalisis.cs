// <copyright file="TipoElementoAnalisis.cs" company="Ada SA">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Cristian Mauricio Arenas Gomez.</author>
// <date>27/01/2017</date>
// <summary>Modelo que contiene la informacion del tipo de elementos del analisis.</summary>

namespace SG_SST.Models.Empresas
{
    using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

    [Table("Tbl_Tipo_Elemento_Analisis")]
    public class TipoElementoAnalisis
    {

        /// <summary>
        /// Obtiene y establece un tipo de elementos  para realizar los analisis.
        /// </summary>
        [Key]
        public int PK_Tipo_Elemneto_Analisis { get; set; }

        /// <summary>
        /// Obtiene y establece la descripcion del tipo de elemento del analisis.
        /// </summary>

        [Display(Name = "Descripción del elemento")]
        public string Descripcion_Elemento { get; set; }


        /// <summary>
        /// Obtiene y establece la clave foranea a tipo de analisis.
        /// </summary>
        [Display(Name = "Tipo Elemento")]
        [ForeignKey("TipoAnalisis")]
        public int FK_Tipo_Analisis { get; set; }

        /// <summary>
        /// Obtiene y establece un objeto de tipo de analisis.
        /// </summary>
        [ForeignKey("PK_Tipo_Analisis")]
        public virtual TipoAnalisis TipoAnalisis { get; set; }

        /// <summary>
        /// Obtiene y establece una coleccion de  elementos de matriz.
        /// </summary>
        public ICollection<ElementoMatriz> ElementosMatriz { get; set; }


    }
}