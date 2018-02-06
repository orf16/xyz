using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Aplicacion
{
    public class EDSegVialFormulario
    {
        public List<EDSegVialResultado> ListaResultados { get; set; }
        public List<EDSegVialPilar> ListaPilares { get; set; }
        public int PilarActual { get; set; }
        public int IdEvaluacion { get; set; }
    }
}
