
namespace SG_SST.Models.Aplicacion
{
    using SG_SST.Models.Empresas;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    [Table("Tbl_CalificacionProveedor")]
    public class CalificacionProveedor
    {
        /// <summary>
        /// Obtiene y establece la clave primaria de la Calificacion del Proveedor.
        /// </summary>
        [Key]
        public int PK_CalificacionProveedor { get; set; }
        /// <summary>
        /// Obtiene y establece la fecha que se califica a el Proveedor o Contratista.
        /// </summary>
        public DateTime Fecha_Calificacion { get; set; }

        /// <summary>
        /// Obtiene y establece el resultado de la calificación del Proveedor o Contratista.
        /// </summary>
        public double ResultadoCalificacion { get; set; }

        /// <summary>
        /// Obtiene y establece las observaciones de la calificación del Proveedor o Contratista.
        /// </summary>
        public string Observaciones { get; set; }

        /// <summary>
        /// Obtiene y establece el numero de la calificación del Proveedor o Contratista.
        /// </summary>
        public int NumeroCalificion { get; set; }

        public ICollection<ProveedorPorNumeroCalificacion> ProveedorPorNumeroCalificacion { get; set; }
        public ICollection<ProveedorPorAnexo> ProveedorPorAnexo { get; set; }
    }
}
