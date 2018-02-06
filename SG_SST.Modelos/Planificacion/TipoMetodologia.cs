// <copyright file="Metodologia.cs" company="Ada SA">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Cristian Mauricio Arenas Gomez.</author>
// <date>06/01/2017</date>
// <summary>Modelo que contiene la informacion de las metodologias.</summary>

namespace SG_SST.Models.Planificacion
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Tbl_Tipo_Metodologia")]
    public class TipoMetodologia
    {
        /// <summary>
        /// Obtiene y establece la clave primaria a la tabla  peligro.
        /// </summary>
        [Key]
        public int PK_Metodologia { get; set; }

        /// <summary>
        /// Obtiene y establece la descripcion o nombre de la metodologia.
        /// </summary>
        public string Descripcion_Metodologia { get; set; }

        /// <summary>
        /// Obtiene y establece una coleccion de grupos.
        /// </summary>
        public ICollection<Grupo> Grupos { get; set; }

        /// <summary>
        /// Obtiene y establece una coleccion de grupos.
        /// </summary>
        public ICollection<Probabilidad> Probabilidades { get; set; }
    }
}