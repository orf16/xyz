using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Models.Planificacion
{

     [Table("Tbl_Clasificacion_Peligro_Dx")]
    public class ClasificacionPeligroDx
    {

        /// <summary>
        /// obtiene y establece  la pk o id de la DiagnosticoCie10Dx
        /// </summary>
        [Key]
        public int idClasificacionPeligroDx { get; set; }

        /// <summary>
        /// Obtiene y establece la clave foranea a la tabla  dx condiciones  de salud.
        /// </summary>
        [ForeignKey("DxCondiciones")]
        public int FK_DxCondicionesDeSalud { get; set; }

        /// <summary>
        /// Obtiene y establece un objeto de dx condiciones  de salud.
        /// </summary>
        [ForeignKey("Pk_DxCondicionesDeSalud")]
        public virtual DxCondicionesDeSalud DxCondiciones { get; set; }

        /// <summary>
        /// Obtiene y establece la clave foranea a la tabla  clasificacion de peligro.
        /// </summary>
        [ForeignKey("ClasificacionDePeligro")]
        public int FK_Clasificacion_De_Peligro { get; set; }

        /// <summary>
        /// Obtiene y establece un objeto de clasificacion de peligro.
        /// </summary>
        [ForeignKey("PK_Clasificacion_De_Peligro")]
        public virtual ClasificacionDePeligro ClasificacionDePeligro { get; set; }
    }
}
