using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Planificacion
{
    public class EDEvaluacionStandMinimo
    {
        public int IdEvalEstandarMinimo { get; set; }
        public int IdEmpresaEvaluar { get; set; }
        public int IdCriterio { get; set; }
        public int IdConfigValoracionSubEstand { get; set; }
        public string Justificacion { get; set; }
        public List<EDActividad> Actividades { get; set; }
    }
}
