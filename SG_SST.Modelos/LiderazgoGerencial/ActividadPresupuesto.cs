// <copyright file="ActividadPresupuesto.cs" company="Ada SA">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Cristian Mauricio Arenas Gomez.</author>
// <date>17/03/2017</date>
// <summary>Modelo que contiene la informacion de la actividad del presupeusto.</summary>

namespace SG_SST.Models.LiderazgoGerencial
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    [Table("Tbl_Actividad_Presupuesto")]
    public class ActividadPresupuesto
    {
        /// <summary>
        /// Obtiene y establece la clave primaria del la actividad del presupuesto
        /// </summary>
        [Key]
        public int PK_Actividad_Presupuesto { get; set; }

        public string DescripcionActividad { get; set; }

        /// <summary>
        /// Obtiene y establece  una clave foranea a la actividad del prepuesto
        /// </summary>
        [ForeignKey("actividadPres")]
        public int? FK_Actividad_Presupuesto { get; set; }

        /// <summary>
        /// Obtiene y establece  un objeto de tipo actividadpresupuesto
        /// </summary>
        [ForeignKey("PK_Actividad_Presupuesto")]
        public virtual ActividadPresupuesto actividadPres { get; set; }

        public ICollection<ActividadPresupuesto> actividadesPresupuesto { get; set; }
        public ICollection<PresupuestoPorMes> presupuestosPorMes { get; set; }
    }
}
