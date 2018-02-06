using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Models.Planificacion
{
    [Table("Tbl_Pruebas_P_Clinica_Dx")]
    public class PruebasPClinicasDx
    {
        /// <summary>
        /// obtiene y establece  la pk o id de las prueba p clincica
        /// </summary>
        [Key]
        public int idPruebasPClinicas { get; set; }

        /// <summary>
        /// Obtiene y establece la prueba clinica.
        /// </summary>
        public string Prueba_P_Clinica { get; set; }

        /// <summary>
        /// Obtiene y establece el numero de trababajdores con Prueba clinica. 
        /// </summary>
        public int Trabajadores_Con_Prueba_P { get; set; }

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
