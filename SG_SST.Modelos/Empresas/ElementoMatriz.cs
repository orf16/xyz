// <copyright file="ElementoMatriz.cs" company="Ada SA">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Cristian Mauricio Arenas Gomez.</author>
// <date>27/01/2017</date>
// <summary>Modelo que contiene la informacion de los ElementoMatriz.</summary>

namespace SG_SST.Models.Empresas
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Tbl_Elemento_Matriz")]
    public class ElementoMatriz
    {
        /// <summary>
        /// Obtiene y establece la clave del elemento de la matriz.
        /// </summary>
        [Key]
        public int PK_Elemento_Matriz { get; set; }

        /// <summary>
        /// Obtiene y establece el detalle del analisis dependiento del tipo.
        /// </summary>
        
        [Display(Name = "Descripción del elemento")]
        public string Descripcion_Elemento { get; set; }

        /// <summary>
        /// Obtiene y establece una clave foranea a matriz.
        /// </summary>
        [ForeignKey("Matriz")]
        public int FK_Matriz { get; set; }


        /// <summary>
        /// Obtiene y establece un objeto de tipo matriz.
        /// </summary>
        [ForeignKey("PK_Matriz ")]
        public virtual Matriz Matriz { get; set; }


        /// <summary>
        /// Obtiene y establece una clave foranea a tipo de elemento analisis.
        /// </summary>
        [ForeignKey("TipoElementoAnalisis")]
        public int FK_TipoElementoAnalisis { get; set; }


        /// <summary>
        /// Obtiene y establece un objeto de tipo elemento analiss.
        /// </summary>
        [ForeignKey("PK_Tipo_Elemneto_Analisi ")]
        public virtual TipoElementoAnalisis TipoElementoAnalisis { get; set; }

    }
}