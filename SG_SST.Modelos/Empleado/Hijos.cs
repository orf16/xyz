using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.SqlServer;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SG_SST.Models.Empleado
{

     [Table("Tbl_Hijos")]
    public class Hijos
    {
        [Key]
         public int PK_Hijos { get; set; }
        public string Descripcion_Hijos { get; set; }



    }
}
