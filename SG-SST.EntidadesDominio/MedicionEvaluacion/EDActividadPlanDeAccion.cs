using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.MedicionEvaluacion
{
   public class EDActividadPlanDeAccion
    {
        public int Fk_Id_ModuloPlanAccion { get; set; }

        public int Pk_Id_ActividadPlanAccion { get; set; }

        public int Fk_Plan_Inspección { get; set; }

        public int Num_Actividad { get; set; }

        public int Fk_Id_Actividad { get; set; }

        public string Actividad { get; set; }

        public string Responsable { get; set; }

        public DateTime FechaFinalizacion { get; set; }

        public DateTime FechaCierre { get; set; }

        public string Observaciones { get; set; }

        public DateTime fechaEvaluacion { get; set; }

        public int consecutivo { get; set; }

        public string FechaFinalizacionString { get; set; }

        public string actividadReporte { get; set; }


    }
}
