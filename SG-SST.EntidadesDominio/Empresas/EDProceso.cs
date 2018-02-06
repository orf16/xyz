using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Empresas
{
    public class EDProceso
    {
        public int Id_Proceso { get; set; }
        public string Descripcion { get; set; }
        public int? Id_Proceso_Padre { get; set; }
    }
}
