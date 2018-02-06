
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

    [Table("Tbl_Proveedor_Por_NumeroCalificacion")]
    public class ProveedorPorNumeroCalificacion
    {
        /// <summary>
        /// Obtiene y establece la clave primaria del ProveedorPorNumeroCalificacion.
        /// </summary>
        [Key]
        public int PK_ProveedorPorNumeroCalificacion { get; set; }

        [ForeignKey("ProveedorContratista")]
        public int Fk_Id_ProveedorContratista { get; set; }
        [ForeignKey("PK_ProveedorContratista")]
        public virtual ProveedorContratista ProveedorContratista { get; set; }

        [ForeignKey("CalificacionProveedor")]
        public int Fk_Id_CalificacionProveedor { get; set; }
        [ForeignKey("PK_CalificacionProveedor")]
        public virtual CalificacionProveedor CalificacionProveedor { get; set; }
        public ICollection<ProveedorPorProductoPorCriterio> ProveedorPorProductoPorCriterio { get; set; }


    }
}
