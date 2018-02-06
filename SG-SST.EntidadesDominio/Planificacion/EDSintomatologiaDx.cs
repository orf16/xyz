using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Planificacion
{
    public class EDSintomatologiaDx
    {
     
        public int idSintomatologia { get; set; }
      
        public string Sintomatologia { get; set; }
       
        public int Trabajadores_Sintomatologia { get; set; }

        public double porcentajeSintomatologia { get; set; }
    }
}
