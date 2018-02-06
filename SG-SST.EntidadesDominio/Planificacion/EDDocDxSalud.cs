using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Planificacion
{
    public class EDDocDxSalud
    {
        public int idEDDocDxSalud { get; set; }

        public int idSede { get; set; }

        public string Nombre_Diagnostico { get; set; }
     
        public string Nombre_Documento { get; set; }
    }
}
