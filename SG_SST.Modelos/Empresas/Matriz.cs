// <copyright file="Matriz.cs" company="Ada SA">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Cristian Mauricio Arenas Gomez.</author>
// <date>27/01/2017</date>
// <summary>Modelo que contiene la informacion de la matriz.</summary>

namespace SG_SST.Models.Empresas
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Tbl_Matriz")]
    public class Matriz
    {
        /// <summary>
        /// Obtiene y establece la clave primaria de la matriz.
        /// </summary>
        [Key]
        public int PK_Matriz { get; set; }

        /// <summary>
        /// Obtiene y establece la descripcion de la matriz
        /// </summary>
        [Display(Name = "Descripcion")]
        public string  Descripcion { get; set; }

        /// <summary>
        /// Obtiene y establece una clave foranea a empresa
        /// </summary>
        [ForeignKey("Empresa")]
        public int FK_Empresa { get; set; }

        /// <summary>
        /// Obtiene y establece un objeto de tipo empresa
        /// </summary>
        [ForeignKey("Pk_Id_Empresa")]
        public virtual Empresa Empresa { get; set; }

        /// <summary>
        /// Obtiene y establece una coleccion de  elementos de matriz.
        /// </summary>
        public ICollection<ElementoMatriz> ElementosMatriz { get; set; }

    }
}