using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Aplicacion
{
    public class EDBateriaResultado
    {
        public int Pk_Id_BateriaResultado { get; set; }
        public int Fk_Id_BateriaUsuario { get; set; }
        public int Fk_Id_BateriaCuestionario { get; set; }
        public int Orden { get; set; }
        public int Valor { get; set; }
        public string ValorS { get; set; }
        public string key { get; set; }

        public string Dimension { get; set; }
        public int DimensionInt { get; set; }
        public string Dominio { get; set; }
        public int DominioInt { get; set; }
        public int ValorResultado { get; set; }
    }
}
