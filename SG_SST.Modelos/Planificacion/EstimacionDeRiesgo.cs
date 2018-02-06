// <copyright file="EstimacionDeRiesgo.cs" company="Ada SA">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Cristian Mauricio Arenas Gomez.</author>
// <date>07/01/2017</date>
// <summary>Modelo que contiene la informacion de estimacion de riesgos.</summary>

namespace SG_SST.Models.Planificacion
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Tbl_Estimacion_De_Riesgo")]
    public class EstimacionDeRiesgo
    {
        /// <summary>
        /// Obtiene y establece la clave primaria de una estimacion de riesgo.
        /// </summary>
        [Key]
        public int PK_Estimacion_De_Riesgo { get; set; }

        /// <summary>
        /// Obtiene y establece la clave foranea a la tabla  probalidad.
        /// </summary>
        [ForeignKey("Probabilidad")]
        public int FK_Probabilidad { get; set; }

        /// <summary>
        /// Obtiene y establece un objeto de tipo de probalidad.
        /// </summary>
        [ForeignKey("PK_Probabilidad")]
        public virtual Probabilidad Probabilidad { get; set; }

        /// <summary>
        /// Obtiene y establece la clave foranea a la tabla  consecuencia.
        /// </summary>
        [ForeignKey("Consecuencia")]
        public int FK_Consecuencia { get; set; }

        /// <summary>
        /// Obtiene y establece un objeto de tipo consecuencia.
        /// </summary>
        [ForeignKey("PK_Consecuencia")]
        public virtual Consecuencia Consecuencia { get; set; }

        /// <summary>
        /// Obtiene y establece el detalle de la estimacion que es la 
        /// combinacion de la selección de los campos probabilidad estimada y consecuencia esperada.
        /// </summary>
        public string Detalle_Estimacion { get; set; }


        /// <summary>
        /// Obtiene y establece si el riesgo  no es aceptable, no aceptable: verdadero, acpetable :falso
        /// </summary>
        public bool RiesgoNoAceptable { get; set; }

        /// <summary>
        ///  Obtiene y establece  el valor del riesgo donde se definen los valores como:
        ///  Ninguo:1
        ///  Bajo:2
        ///  Medio:3
        ///  Alto:4
        ///  Muy Alto:5
        /// </summary>
        public int ? ValorDelRiesgo { get; set; }
        
        /// <summary>
        /// obtiene y estable una coleccion de estimacion de riesgos por metodologia Ram
        /// </summary>
        public ICollection<EstimacionDeRiesgoPorRAM> EstimacionDeRiesgosPorRAM { get; set; }
    }
}