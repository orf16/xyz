// <copyright file="PrepuestoPorMes.cs" company="Ada SA">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Cristian Mauricio Arenas Gomez.</author>
// <date>17/03/2017</date>
// <summary>Modelo que contiene la informacion del presupuesto por mes.</summary>

namespace SG_SST.Models.LiderazgoGerencial
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

     [Table("Tbl_Prepuesto_Por_Mes")]
    public class PresupuestoPorMes
    {
        /// <summary>
        /// Obtiene y establece la clave primaria del prespuesto por mes 
        /// </summary>
        [Key]
        public int PK_Prepuesto_Por_Mes { get; set; }

         /// <summary>
         /// Obtiene y estable el valor del prespuesto del mes
         /// </summary>
        public double PresupuestoMes { get; set; }

        /// <summary>
        /// Obtiene y estable el valor del prespuesto del mes ejecutado
        /// </summary>
        public double PresupuestoEjecutadoPorMes { get; set; }

         /// <summary>
         /// obtiene y establece el mes del año
         /// </summary>
        public int Mes { get; set; }

        /// <summary>
        /// Obtiene y estable el valor del prespuesto del mes ejecutado
        /// </summary>
        public string ComentarioPresupuestoMesEjecutado { get; set; }


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
        /// Obtiene y establece  una clave foranea a la actividad del prepuesto
        /// </summary>
        [ForeignKey("ActividadPresupuesto")]
        public int FK_Actividad_Presupuesto { get; set; }

        /// <summary>
        /// Obtiene y establece  un objeto de tipo actividadpresupuesto
        /// </summary>
        [ForeignKey("PK_Actividad_Presupuesto")]
        public virtual ActividadPresupuesto ActividadPresupuesto { get; set; }
    }
}
