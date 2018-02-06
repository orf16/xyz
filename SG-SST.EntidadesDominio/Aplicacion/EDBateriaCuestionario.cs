using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Aplicacion
{
    public class EDBateriaCuestionario
    {
        public int Pk_Id_BateriaCuestionario { get; set; }
        public string Pregunta { get; set; }
        public int Fk_Id_BateriaDimension { get; set; }
        public int Orden { get; set; }
        public int Pagina { get; set; }
        public int Dominio { get; set; }
        public int Valor { get; set; }
    }
}
