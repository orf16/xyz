using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Participacion
{
    public class EDActividadesActosInseguros
    {
        public int ID_ActividadActosInseguros { get; set; }

        public string nombreActividad { get; set; }

        public string RespActividad { get; set; }

        public DateTime FecEjecucion { get; set; }

        public int FKReportes { get; set; }

   
 
    }
}
