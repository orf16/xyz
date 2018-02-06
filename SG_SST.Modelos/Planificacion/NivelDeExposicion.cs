// <copyright file="NivelDeExposicion.cs" company="Ada SA">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Cristian Mauricio Arenas Gomez.</author>
// <date>07/01/2017</date>
// <summary>Modelo que contiene la informacion de interpretacion del nivel de Exposicion.</summary>

namespace SG_SST.Models.Planificacion
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Web;

    [Table("Tbl_Nivel_De_Exposicion")]
    public class NivelDeExposicion
    {
        /// <summary>
        /// Obtiene y establece la clave primaria del nivel de Exposicion.
        /// </summary>
        [Key]
        public int PK_Nivel_De_Exposicion { get; set; }

        /// <summary>
        /// Obtiene y establece el valor de la exposicion
        /// </summary>
        public int Valor_Exposicion { get; set; }

        /// <summary>
        /// Obtiene y establece el valor de la exposicion
        /// </summary>
        public string Descripcion_Exposicion { get; set; }
    
        /// <summary>
        /// Obtiene y establece de riesgos de la metodologia gtc45.
        /// </summary>
        public ICollection<GTC45> GTC45 { get; set; }
    }
}