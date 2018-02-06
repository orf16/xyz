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
    [Table("Tbl_Ingresos")]
   public class Ingresos
    {
        [Key]
        public int PK_Ingresos { get; set; }
        public string Descripcion_Ingresos { get; set; }

    }
}
