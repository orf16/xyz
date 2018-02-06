using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.SqlServer;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SG_SST.Models.Planificacion
{
    [Table("Tbl_Raza")]
    public class Raza
    {
        [Key]
        public int PK_Raza { get; set; }
        public string Descripcion_Raza { get; set; }



    }
}
