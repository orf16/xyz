// <copyright file="NivelDeDeficiencia.cs" company="Ada SA">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Cristian Mauricio Arenas Gomez.</author>
// <date>07/01/2017</date>
// <summary>Modelo que contiene la informacion de interpretacion del nivel de dificiencia</summary>

namespace SG_SST.Models.Planificacion
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Web;

    [Table("Tbl_Nivel_De_Deficiencia")]
    public class NivelDeDeficiencia
    {

        /// <summary>
        /// Obtiene y establece la clave primaria del nivel de dificiencia.
        /// </summary>
        [Key]
        public int PK_Nivel_De_Deficiencia { get; set; }

        /// <summary>
        /// Obtiene y establece que tipo de deficiencia cuantitativa o cualitativa verdadero para cuantitativa
        /// y falso para cualitativa
        /// </summary>
        public bool FLAG_Cuantitativa { get; set; }

        ///// <summary>
        ///// Obtiene y establece si el tipo de de deificiencia es quimico verdadero para quimico
        ///// y falso para cualitativa
        ///// </summary>
        //public bool FLAG_Quimico { get; set; }

        /// <summary>
        /// Obtiene y establece el valor de  tipo de deficiencia cuantitativa o cualitativa
        /// </summary>
        public int Valor_Deficiencia { get; set; }

        /// <summary>
        /// Obtiene y establece la descripcion del  tipo de deficiencia cuantitativa o cualitativa
        /// </summary>
        public string Descripcion_Deficiciencia { get; set; }

        /// <summary>
        /// Obtiene y establece de riesgos de la metodologia gtc45.
        /// </summary>
        public ICollection<GTC45> GTC45 { get; set; }
    }
}