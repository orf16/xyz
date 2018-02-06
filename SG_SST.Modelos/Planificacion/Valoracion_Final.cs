using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Models.Planificacion
{
    [Table("Tbl_Valoracion_Final")]
    public partial class Valoracion_Final
    {
        [Key]
        public int Pk_Id_Valoracion_Final { get; set; }
        public decimal Rango_Inicial { get; set; }
        public decimal Rango_Final { get; set; }
        [StringLength(100)]
        public string CriterioEvaluacion { get; set; }
        [StringLength(100)]
        public string Valoracion { get; set; }
        [StringLength(4000)]
        public string  Accion { get; set; }       
    }
}
