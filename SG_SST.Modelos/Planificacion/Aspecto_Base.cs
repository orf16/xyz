using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Models.Planificacion
{
    [Table("Tbl_Aspecto_Base")]
    public class Aspecto_Base
    {
        [Key]
        public int PK_Id_Aspecto_Base { get; set; }
        public string AspectoBase { get; set; }
    }
}
