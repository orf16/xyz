using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;



namespace SG_SST.Models.Empresas
{
  [Table("Tbl_Maestro_Roles_SGSST")]
   public class MaestroRolesSGSST
    {

        [Key]
        public int Pk_Id_MaestroRol_SGSST { get; set; }
        public string DescripcionRol { get; set; }
        
        

    }
}
