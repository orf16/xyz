using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SG_SST.Models.Empresas
{
    [Table("Tbl_Privilegios_Por_Rol")]
    public class PrivilegiosPorRol
    {
    [Key]
        public int  Id_Pk_PrivilegioRol { get; set; }

        [ForeignKey("Rol")]
        public int Fk_Id_Rol { get; set; }
        [ForeignKey("Pk_Id_Rol")]
        public virtual Rol Rol { get; set; }


        [ForeignKey("Privilegios")]
        public int Fk_Id_Privilegios { get; set; }
        [ForeignKey("Pk_Id_Privilegios")]
        public virtual Privilegios Privilegios { get; set; }
    
    }
}