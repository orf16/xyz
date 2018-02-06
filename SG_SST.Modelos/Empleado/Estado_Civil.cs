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
    [Table("Tbl_Estado_Civil")]
    public class Estado_Civil
    {
        [Key]
        public int PK_Estado_Civil { get; set; }
        public string Descripcion_EstadoCivil { get; set; }
        
    }
}
