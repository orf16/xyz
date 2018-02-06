// <copyright file="GTC45.cs" company="Ada SA">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Cristian Mauricio Arenas Gomez.</author>
// <date>06/01/2017</date>
// <summary>Modelo que contiene la informacion del peligro de la metodologia GTC45.</summary>

namespace SG_SST.Models.Planificacion
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Tbl_GTC45")]
    public class GTC45
    {
        /// <summary>
        /// Obtiene y establece la clave primaria del peligro de la metodologia gtc45.
        /// </summary>
        [Key]
        public int PK_GTC45 { get; set; }

        /// <summary>
        /// Obtiene y establece si el peligro es higienico.
        /// </summary>
        public bool FLG_Higienico { get; set; }

        /// <summary>
        /// Obtiene y establece del tipo de calificacion.cuantitativo como verdadero(true) y cualitativo como falso(false)
        /// </summary>
        public bool FLG_Tipo_De_Calificacion { get; set; }

        /// <summary>
        /// Obtiene y establece los efectos del peligro.
        /// </summary>
        public string Efectos_Posibles { get; set; }

        /// <summary>
        /// Obtiene y establece el nivel de probabiliad que es 
        /// la multiplicación del campo Nivel de deficiencia por Nivel de exposición.
        /// </summary>
        public int Nivel_De_Probablidad { get; set; }

        /// <summary>
        /// Obtiene y establece el nivel de riesgo que es 
        /// la multiplicación del campo Nivel de probabilidad por Nivel de consecuencia.
        /// </summary>
        public int Nivel_De_Riesgo { get; set; }

        /// <summary>
        /// Obtiene y establece la cantidad de trabajadores expuesto al peligro   
        /// </summary>
        public int Numero_De_Expuestos { get; set; }

        /// <summary>
        /// Obtiene y establece la peor consecuencia al estar expuesto al peligro..
        /// </summary>
        public string Peor_Consecuencia { get; set; }

        /// <summary>
        /// Obtiene y establece si existe requisito legal para el peligro.
        /// </summary>
        public bool FLG_Requisito_Legal { get; set; }


        /// <summary>
        /// Obtiene y establece la clave foranea a la tabla  peligro.
        /// </summary>
        [ForeignKey("Peligro")]
        public int FK_Peligro { get; set; }

        /// <summary>
        /// Obtiene y establece un objeto de tipo de Peligro.
        /// </summary>
        [ForeignKey("PK_Peligro")]
        public virtual Peligro Peligro { get; set; }

        /// <summary>
        /// Obtiene y establece la clave foranea a la tabla  nivel de deficiencia.
        /// </summary>
        [ForeignKey("NivelDeDeficiencia")]
        public int FK_Nivel_De_Deficiencia { get; set; }

        /// <summary>
        /// Obtiene y establece un objeto de tipo de nivel de deficiencia.
        /// </summary>
        [ForeignKey("PK_Nivel_De_Deficiencia")]
        public virtual NivelDeDeficiencia NivelDeDeficiencia { get; set; }

        /// <summary>
        /// Obtiene y establece la clave foranea a la tabla  nivel de deficiencia.
        /// </summary>
        [ForeignKey("NivelDeExposicion")]
        public int FK_Nivel_De_Exposicion { get; set; }

        /// <summary>
        /// Obtiene y establece un objeto de tipo de nivel de deficiencia.
        /// </summary>
        [ForeignKey("PK_Nivel_De_Exposicion")]
        public virtual NivelDeExposicion NivelDeExposicion { get; set; }

    }
}