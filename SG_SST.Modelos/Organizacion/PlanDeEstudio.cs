using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Models.Organizacion
{
    [Table("Tbl_Plan_De_Estudio")]
    public class PlanDeEstudio
    {
        [Key]
        public int PK_Plan_De_Estudio { get; set; }

       // public ICollection<Actividad> PersonaExpuestas { get; set; }
    }
}
