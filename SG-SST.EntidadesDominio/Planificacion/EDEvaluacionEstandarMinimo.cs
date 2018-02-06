using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Planificacion
{
    public class EDEvaluacionEstandarMinimo
    {
        public int IdEvalEstandarMinimo { get; set; }
        public int IdEmpresaEvaluar { get; set; }
        public int IdCriterio { get; set; }
        public int IdCiclo { get; set; }
        public int IdValoracionCriterio { get; set; }
        public string Justificacion { get; set; }
        public string Valor { get; set; }
        public string Nit { get; set; }
        public List<EDActividad> Actividades { get; set; }
    }
}
