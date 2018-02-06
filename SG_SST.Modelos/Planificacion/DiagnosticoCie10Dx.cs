using SG_SST.Models.Ausentismo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Models.Planificacion
{
    [Table("Tbl_Diagnostico_Cie10_Dx")]
    public class DiagnosticoCie10Dx
    {

        /// <summary>
        /// obtiene y establece  la pk o id de la DiagnosticoCie10Dx
        /// </summary>
        [Key]
        public int idDiagnosticoCie10Dx { get; set; }

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
        /// Obtiene y establece el numero de trababajdores con diagnostico. 
        /// </summary>
        public int Trabajadores_Con_Diagnostico { get; set; }

        /// <summary>
        /// Obtiene y establece la clave foranea a la tabla  Diagnostico.
        /// </summary>
        [ForeignKey("Diagnostico")]
        public int FK_Diagnostico { get; set; }

        /// <summary>
        /// Obtiene y establece un objeto de Diagnostico.
        /// </summary>
        [ForeignKey("id_Diagnostico")]
        public virtual Diagnostico Diagnostico { get; set; }
    }
}
