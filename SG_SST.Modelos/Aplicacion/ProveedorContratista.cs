
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

    [Table("Tbl_ProveedorContratista")]
    public class ProveedorContratista
    {
        /// <summary>
        /// Obtiene y establece la clave primaria del Servicio o Producto.
        /// </summary>
        [Key]
        public int PK_ProveedorContratista { get; set; }

        /// <summary>
        /// Obtiene y establece el nombre del Proveedor o Contratista.
        /// </summary>
        public string Nombre_ProveedorContratista { get; set; }

        /// <summary>
        /// Obtiene y establece el Nit del Proveedor o Contratista.
        /// </summary>
        public string Nit_ProveedorContratista { get; set; }       
        
        public int IdEmpresa { get; set; }

        public string FrecuenciaEval { get; set; }
        public int? CalificacionHist { get; set; }

        public DateTime VigenciaContrato { get; set; }

        public ICollection<ProveedorPorProducto> ProveedorPorProducto { get; set; }
        //public ICollection<ProveedorPorProductoPorCriterio> ProveedorPorProductoPorCriterio { get; set; }
        public ICollection<ProveedorPorNumeroCalificacion> ProveedorPorNumeroCalificacion { get; set; }
    }
}
