using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Models.Planificacion
{
    [Table("Tbl_Valoracion_Aspectos")]
    public partial class Valoracion_Aspecto
    {
        [Key]
        public int Pk_Id_Valoracion_Aspecto { get; set; }
        [StringLength(100)]
        public string Valoracion { get; set; }
    }
}
