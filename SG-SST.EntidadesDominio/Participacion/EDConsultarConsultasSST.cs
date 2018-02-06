using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Participacion
{
    public class EDConsultarConsultasSST
    {
        public string tipoConsult { get; set; }
        public DateTime? Fecha_ini {get; set; }
        public DateTime? Fecha_Fin { get; set; }
        public int idEmpresa { get; set; } 
    }
}
