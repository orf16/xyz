using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Aplicacion
{
    public class EDCondicionporInspeccion
    {

        public int pkcondicionporinspeccion { get; set; }
        public int fkcondicioninspeccion { get; set; }
        public int fkInspeccion { get; set; }
    }
}
