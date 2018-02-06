
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
    [Table("Tbl_Proveedor_Por_Anexo")]
    public class ProveedorPorAnexo
    {
        [Key]
        public int Id_Pk_ProveedorPorAnexo { get; set; }

        [ForeignKey("CalificacionProveedor")]
        public int Fk_Id_CalificacionProveedor { get; set; }
        [ForeignKey("PK_CalificacionProveedor")]
        public virtual CalificacionProveedor CalificacionProveedor { get; set; }

        [ForeignKey("ArchivosAnexos")]
        public int Fk_Id_Archivos_Anexos { get; set; }
        [ForeignKey("PK_Archivos_Anexos")]
        public virtual ArchivosAnexos ArchivosAnexos { get; set; }
    }
}
