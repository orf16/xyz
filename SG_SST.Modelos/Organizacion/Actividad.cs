using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Models.Organizacion
{
    [Table("Tbl_Actividad")]
    public class Actividad
    {
        [Key]
        public int PK_Actividad { get; set; }


        [ForeignKey("PlanDeEstudio")]
        public int FK_Plan_De_Estudio { get; set; }

        /// <summary>
        /// Obtiene y establece un objeto de clasificacion de peligro.
        /// </summary>
        [ForeignKey("PK_Plan_De_Estudio")]
        public virtual PlanDeEstudio PlanDeEstudio { get; set; }
    }
}
