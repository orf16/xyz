using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Planificacion
{
    public class EDGTC45
    {
      
        public int PK_GTC45 { get; set; }
       
        public bool FLG_Higienico { get; set; }
       
        public bool FLG_Tipo_De_Calificacion { get; set; }
      
        public string Efectos_Posibles { get; set; }
       
        public int Nivel_De_Probablidad { get; set; }
       
        public int Nivel_De_Riesgo { get; set; }
       
        public int Numero_De_Expuestos { get; set; }
        
        public string Peor_Consecuencia { get; set; }
       
        public bool FLG_Requisito_Legal { get; set; }

        public int FK_Peligro { get; set; }

        public int FK_Nivel_De_Deficiencia { get; set; }

        public int FK_Nivel_De_Exposicion { get; set; }

      
    }
}
