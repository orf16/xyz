using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Participacion
{
   public class EDBusqueda
    {
       public DateTime fechaInicio { get; set; }

       public DateTime fechaFin { get; set; }

       public List<int> sedes { get; set; }

       public int tipo { get; set; }

       public int cedula { get; set; }

    }
}
