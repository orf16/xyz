using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Planificacion
{
    public class EDAspecto
    {
        public int IdAspecto { get; set; }
        public string Aspecto { get; set; }
        public string Observacion { get; set; }
        public int IdValorizacion { get; set; }
    }
}
