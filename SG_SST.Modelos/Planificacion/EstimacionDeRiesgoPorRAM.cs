
namespace SG_SST.Models.Planificacion
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Web;

    [Table("Tbl_Estimacion_Riesgo_Por_RAM")]
    public class EstimacionDeRiesgoPorRAM
    {
        /// <summary>
        /// Obtiene y establece la clave primaria de las estimaciones o valores de riesgos por peligro RAM.
        /// </summary>
        [Key]
        public int PK_Estimacion_Riesgo_Por_RAM { get; set; }

        /// <summary>
        /// Obtiene y establece la clave foranea a la tabla  RAM.
        /// </summary>
        [ForeignKey("RAM")]
        public int FK_RAM { get; set; }

        /// <summary>
        /// Obtiene y establece un objeto de tipo RAM.
        /// </summary>
        [ForeignKey("PK_RAM")]
        public virtual RAM RAM { get; set; }

        /// <summary>
        /// Obtiene y establece la clave foranea a la tabla  EstimacionDeRiesgo.
        /// </summary>
        [ForeignKey("EstimacionDeRiesgo")]
        public int FK_Estimacion_De_Riesgo { get; set; }

        /// <summary>
        /// Obtiene y establece un objeto de tipo EstimacionDeRiesgo.
        /// </summary>
        [ForeignKey("PK_Estimacion_De_Riesgo")]
        public virtual EstimacionDeRiesgo EstimacionDeRiesgo { get; set; }

    }
}