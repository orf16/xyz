using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.ComunicadosAPP
{
    public class EDComunicadosAPP
    {
        public int IDComunicadosAPP { get; set; }
        public string Titulo { get; set; }
        public string Asunto { get; set; }
        public string Destinatarios { get; set; }
        public string FechaCreacion { get; set; }
        public string FechaEnvio { get; set; }
    }
}
