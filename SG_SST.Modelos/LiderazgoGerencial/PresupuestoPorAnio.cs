// <copyright file="PresupuestoPorAño.cs" company="Ada SA">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Cristian Mauricio Arenas Gomez.</author>
// <date>17/03/2017</date>
// <summary>Modelo que contiene la informacion del presupuesto por año.</summary>

namespace SG_SST.Models.LiderazgoGerencial
{
    using SG_SST.Models.Empresas;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    [Table("Tbl_Presupuesto_Por_Año")]
    public class PresupuestoPorAnio
    {
        /// <summary>
        /// Obtiene y  establece la clave primaria del presupuesto por año
        /// </summary>
        [Key]
        public int PK_Presupuesto_Por_Año { get; set; }

        /// <summary>
        /// el año del presupuesto a crear a partir del 2016 hasta el 2050
        /// </summary>
        public int Periodo { get; set; }

        /// <summary>
        /// Obtiene y establece  una clave foranea al prepuesto
        /// </summary>
        [ForeignKey("Presupuesto")]
        public int FK_Presupuesto { get; set; }

        /// <summary>
        /// Obtiene y establece  un objeto de tipo Presupuesto
        /// </summary>
        [ForeignKey("PK_Prepuesto")]
        public virtual Presupuesto Presupuesto { get; set; }

        /// <summary>
        /// Obtiene y establece la clave foranea a la tabla  sede.
        /// </summary>
        [ForeignKey("Sede")]
        public int FK_Sede { get; set; }

        /// <summary>
        /// Obtiene y establece un objeto de sede.
        /// </summary>
        [ForeignKey("Pk_Id_Sede")]
        public virtual Sede Sede { get; set; }

    }

}
