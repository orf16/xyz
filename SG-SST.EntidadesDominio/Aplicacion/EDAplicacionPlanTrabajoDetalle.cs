using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Aplicacion
{
    public class EDAplicacionPlanTrabajoDetalle
    {
        public int Pk_Id_PlanTrabajoDetalle { get; set; }
        public string Objetivo { get; set; }
        public string Metas { get; set; }
        public string RecursoHumano { get; set; }
        public string RecursoTec { get; set; }
        public string RecursoFinanciero { get; set; }
        public int Fk_Id_PlanTrabajo { get; set; }
        public decimal Ejecutado { get; set; }
        public List<EDAplicacionPlanTrabajoActividad> ListaActividades { get; set; }
    }
}
