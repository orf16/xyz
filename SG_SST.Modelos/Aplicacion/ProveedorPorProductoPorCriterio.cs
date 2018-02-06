
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
    [Table("Tbl_Proveedor_ProductoPorCriterio")]
    public class ProveedorPorProductoPorCriterio
    {
        [Key]
        public int Id_Pk_ProveedorPorProductoPorCriterio { get; set; }

        [ForeignKey("ProveedorPorNumeroCalificacion")]
        public int Fk_Id_ProveedorPorNumeroCalificacion { get; set; }
        [ForeignKey("PK_ProveedorPorNumeroCalificacion")]
        public virtual ProveedorPorNumeroCalificacion ProveedorPorNumeroCalificacion { get; set; }


        [ForeignKey("ProductoPorCriterio")]
        public int Fk_Id_ProductoPorCriterio { get; set; }
        [ForeignKey("Id_Pk_ProductoPorCriterio")]
        public virtual ProductoPorCriterio ProductoPorCriterio { get; set; }

        public bool Calificacion { get; set; }
        public double CalificacionProducto { get; set; }
    }
}
