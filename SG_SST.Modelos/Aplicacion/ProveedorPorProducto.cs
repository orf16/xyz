
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
    [Table("Tbl_Proveedor_Por_Producto")]
    public class ProveedorPorProducto
    {
        [Key]
        public int Id_Pk_ProveedorPorProducto { get; set; }

        [ForeignKey("ProveedorContratista")]
        public int Fk_Id_ProveedorContratista { get; set; }
        [ForeignKey("PK_ProveedorContratista")]
        public virtual ProveedorContratista ProveedorContratista { get; set; }

        [ForeignKey("ServicioOProducto")]
        public int Fk_Id_ServicioOProducto { get; set; }
        [ForeignKey("PK_ServicioOProducto")]
        public virtual ServicioOProducto ServicioOProducto { get; set; }
    }
}
