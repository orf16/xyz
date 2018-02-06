using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Models.Planificacion
{
      [Table("Tbl_Sintomatologia_Dx")]
    public class SintomatologiaDx
    {
        /// <summary>
        /// obtiene y establece  la pk o id de la sintomatologia
        /// </summary>
        [Key]
        public int idSintomatologia { get; set; }

        /// <summary>
        /// Obtiene y establece la sintomatologia.
        /// </summary>
        public string Sintomatologia { get; set; }

        /// <summary>
        /// Obtiene y establece el numero de trababajdores con sintomatologia. 
        /// </summary>
        public int Trabajadores_Sintomatologia { get; set; }


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
    }
}
