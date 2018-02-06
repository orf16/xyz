using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Planificacion
{
    public class EDConsecuenciasPorPeligros
    {
       
        public int PK_Consecuencia_Por_Peligro { get; set; }
    
        public int FK_Peligro { get; set; }
      
        public int FK_Consecuencia { get; set; }

      
    }
}
