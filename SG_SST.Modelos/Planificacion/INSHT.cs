// <copyright file="INSHT.cs" company="Ada SA">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Cristian Mauricio Arenas Gomez.</author>
// <date>06/01/2017</date>
// <summary>Modelo que contiene la informacion del peligro de la metodologia INSHT.</summary>

namespace SG_SST.Models.Planificacion
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Tbl_INSHT")]
    public class INSHT
    {
        /// <summary>
        /// Obtiene y establece la clave primaria del peligro de la metodologia INSHT.
        /// </summary>
        [Key]
        public int PK_INSHT { get; set; }

        /// <summary>
        /// Obtiene y establece la clave foranea a la tabla  personas expuesta.
        /// </summary>
        [ForeignKey("PersonaExpuesta")]
        public int FK_Persona_Expuesta { get; set; }

        /// <summary>
        /// Obtiene y establece un objeto de tipo de PersonaExpuesta.
        /// </summary>
        [ForeignKey("PK_Persona_Expuesta")]
        public virtual PersonaExpuesta PersonaExpuesta { get; set; }

        /// <summary>
        /// Obtiene y establece la estimacion del riesgo que es el 
        /// resultado obtenido de la selección de los campos probabilidad estimada y consecuencia esperada. .
        /// </summary>
        public string Estimacion_Riesgo { get; set; }

        /// <summary>
        /// Obtiene y establece la información de las medidas de control para combatir el peligro
        /// </summary>
        public string Medidas_De_Control { get; set; }

        /// <summary>
        /// Obtiene y establece los procedimientos de trabajo para combatir el peligro.
        /// </summary>
        public string Procedimientos_De_Trabajo { get; set; }

        /// <summary>
        /// Obtiene y establece la información para combatir el peligro..
        /// </summary>
        public string Informacion { get; set; }

        /// <summary>
        /// Obtiene y establece la formación.
        /// </summary>
        public string Formacion { get; set; }

        /// <summary>
        /// Obtiene y establece si el peligro está controlado o no, las opciones de la lista son si(verdadero) y no (falso).
        /// </summary>
        public bool Riesgo_Controlado { get; set; }

        /// <summary>
        /// Obtiene y establece la acción requerida para controlar el peligro.
        /// </summary>
        public string Accion_Requerida { get; set; }

        /// <summary>
        /// Obtiene y establece el responsable del plan de acción para controlar el peligro.
        /// </summary>
        public string Responsable { get; set; }

        /// <summary>
        /// Obtiene y establece  la fecha de finalización del plan de acción para controlar el peligro.
        /// </summary>
        public DateTime Fecha_Finalizacion { get; set; }

        /// <summary>
        /// Obtiene y establece  la fecha de comprobación de la eficacia de la acción del plan de acción para controlar el peligro.
        /// </summary>
        public DateTime Fecha_De_Comprobacion  { get; set; }


        /// <summary>
        /// Obtiene y establece el nombre de la firma responsable del plan de acción para controlar el peligro.
        /// </summary>
        public string FirmaResponsable { get; set; }

    }
}