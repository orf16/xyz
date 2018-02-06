// <copyright file="TipoAnalisis.cs" company="Ada SA">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Cristian Mauricio Arenas Gomez.</author>
// <date>27/01/2017</date>
// <summary>Modelo que contiene la informacion del tipo de analisis a realizar.</summary>

namespace SG_SST.Models.Empresas
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Tbl_Tipo_Analisis")]
    public class TipoAnalisis
    {
        /// <summary>
        /// Obtiene y establece un tipo de matriz(DOFA,PEST) para realizar los analisis 
        /// </summary>
        [Key]
        public int PK_Tipo_Analisis { get; set; }

        /// <summary>
        /// Obtiene y establece la descripcion del tipo de analisis
        /// </summary>
        public string Descripcion { get; set; }

        /// <summary>
        /// Obtiene y establece una coleccion de tipos de elementos de analisis
        /// </summary>
         public ICollection<TipoElementoAnalisis> TiposElementosAnalisis { get; set; }

    }
}