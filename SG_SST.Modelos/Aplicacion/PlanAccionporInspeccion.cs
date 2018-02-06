using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SG_SST.Models.MedicionEvaluacion;

namespace SG_SST.Models.Aplicacion
{
    [Table("Tbl_PlanAccionPorInspeccion")]
    public class PlanAccionporInspeccion
    {
        [Key]
        public int Pk_Id_PlanAccionPorInspeccion { get; set; }

        //[ForeignKey("ActividadAccion")]
        public int Fk_Id_Actividad { get; set; }
        //[ForeignKey("Pk_Id_Actividad")]
        public virtual ActividadAccion ActividadAccion { get; set; }

        [ForeignKey("Inspecciones")]
        public int Fk_Id_Inspecciones { get; set; }
        [ForeignKey("Pk_Id_Inspecciones ")]

        public virtual Inspecciones Inspecciones { get; set; }



    }
}
