using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Models.Planificacion
{
    [Table("Tbl_Actividades_Evaluacion")]
    public partial class ActividadEvaluacion
    {      
        public ActividadEvaluacion ()
        {
            this.ActividadesCriterio = new List<Actividad_Criterio>();
        }
        [Key]
        public int Pk_Id_Actividad { get; set; }
        [StringLength(500)]
        public string Descripcion { get; set; }
        [StringLength(50)]
        public string Responsable { get; set; }
        public DateTime FechaFin { get; set; }

        public virtual ICollection<Actividad_Criterio> ActividadesCriterio { get; set; }
    }
}
