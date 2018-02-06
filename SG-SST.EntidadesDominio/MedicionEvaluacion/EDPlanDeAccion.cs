using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.MedicionEvaluacion
{
    public class EDPlanDeAccion
    {
        public int Pk_Id_PlanDeAccion { get; set; }
        public string Origen { get; set; }
        public int Estado{ get; set; }

        public int cantidadActividades { get; set; }
        public ICollection<EDActividadPlanDeAccion> EDActividadPlanDeAccion { get; set; }
    }
}
