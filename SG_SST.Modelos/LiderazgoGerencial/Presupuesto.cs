// <copyright file="Presupuesto.cs" company="Ada SA">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Cristian Mauricio Arenas Gomez.</author>
// <date>17/03/2017</date>
// <summary>Modelo que contiene la informacion del presupeusto.</summary>

namespace SG_SST.Models
{
    using SG_SST.Models.LiderazgoGerencial;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    [Table("Tbl_Presupuesto")]
    public class Presupuesto
    {
        /// <summary>
        /// Obtiene y  establece la clave primaria del presupuesto.
        /// </summary>
        [Key]
        public int PK_Prepuesto { get; set; }

        /// <summary>
        /// Obtiene y estable el rubro total del presupuesto total asignado para el año
        /// </summary>
        public double RubroTotal { get; set; }

        public ICollection<PresupuestoPorAnio> presupuestosPorAnio { get; set; }

    }
}
