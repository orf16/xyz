using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Planificacion
{
    public class EDCriterio
    {
        public int Id_Criterio { get; set; }
        public string Descripcion { get; set; }
        public string Descripcion_Corta { get; set; }
        public string Marco_Legal { get; set; }
        public string Modo_Verificacion { get; set; }
        public string Numeral { get; set; }
        public string ValPorPregunta { get; set; }
        public EDEvaluacionEstandarMinimo Evaluacion { get; set; }
    }
}
