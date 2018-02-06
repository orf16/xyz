

namespace SG_SST.Models.Planificacion
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using SG_SST.Models.Planificacion;
    using System.Data.Entity.SqlServer;
    using System;

    [Table("Tbl_Estado_RequisitoslegalesOtros")]
    public class Estado_RequisitoslegalesOtros
    {
        [Key]
        public int PK_Estado_RequisitoslegalesOtros { get; set; }
        public string Descripcion_Estado_RequisitoslegalesOtros { get; set; }
    }
}
