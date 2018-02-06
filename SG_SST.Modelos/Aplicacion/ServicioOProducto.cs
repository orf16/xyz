
namespace SG_SST.Models.Aplicacion
{
    using SG_SST.Models.Empresas;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Data;
    using System.ComponentModel.DataAnnotations;
    using System.Collections;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("Tbl_ServicioOProducto")]
    public class ServicioOProducto
    {
        /// <summary>
        /// Obtiene y establece la clave primaria del Servicio o Producto.
        /// </summary>
        [Key]
        public int PK_ServicioOProducto { get; set; }

        /// <summary>
        /// Obtiene y establece el nombre del Servicio o Producto.
        /// </summary>
        public string Nombre_ServicioOProducto { get; set; }

        /// <summary>
        /// Obtiene y establece la clave foranea a la tabla  empresa.
        /// </summary>
        [ForeignKey("Empresa")]
        public int FK_Empresa { get; set; }

        /// <summary>
        /// Obtiene y establece un objeto de tipo empresa
        /// </summary>
        [ForeignKey("Pk_Id_Empresa")]
        public virtual Empresa Empresa { get; set; }

        public ICollection<ProductoPorCriterio> ProductoPorCriterio { get; set; }

        public ICollection<ProveedorPorProducto> ProveedorPorProducto { get; set; }

        [NotMapped]
        public int[] SelectedCriterioCode { get; set; }
    }
}
