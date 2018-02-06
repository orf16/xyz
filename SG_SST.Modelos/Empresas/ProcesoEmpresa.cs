using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace SG_SST.Models.Empresas
{
    [Table("Tbl_EmpresaProceso")]
    public class ProcesoEmpresa
    {
        [Key]
        public int Pk_Id_ProcesoEmpresa { get; set; }

        [ForeignKey("Empresa")]
        public int Fk_Id_Empresa { get; set; }
        [ForeignKey("Pk_Id_Empresa")]
        public virtual Empresa Empresa { get; set; }

        [ForeignKey("Proceso")]
        public int Fk_Id_Proceso { get; set; }

        [ForeignKey("Pk_Id_Proceso")]
        public virtual Proceso Proceso { get; set; }


    }
}
