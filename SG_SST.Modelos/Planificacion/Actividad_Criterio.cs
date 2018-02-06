using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Models.Planificacion
{
    [Table("Tbl_Actividades_Criterio")]
    public partial class Actividad_Criterio
    {
        [Key]
        public int Pk_Id_Actividad_Criterio { get; set; }
        [ForeignKey("Actividad")]
        public int Fk_Id_Actividad { get; set; }
        [ForeignKey("Evaluacion_Estandar_Minimo")]
        public int Fk_Id_Eval_Estandar_Minimo { get; set; }
        

        [ForeignKey("PK_Id_Actividad")]
        public virtual ActividadEvaluacion Actividad { get; set; }
        [ForeignKey("Pk_Id_Eval_Estandar_Minimo")]
        public virtual Evaluacion_Estandar_Minimo Evaluacion_Estandar_Minimo { get; set; }
    }
}
