using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Planificacion
{
    public class EDNivelDeExposicion
    {
        public int PK_Nivel_De_Exposicion { get; set; }
        public string Descripcion_Exposicion { get; set; }

        public int Valor_De_Exposicion { get; set; }
    }
}
