using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Aplicacion
{
    public class EDSegVialDetalle
    {
        public int Pk_Id_SegVialParametroDetalle { get; set; }
        public string Numeral { get; set; }
        public string VariableDesc { get; set; }
        public string CriterioAval { get; set; }
        public int Fk_Id_SegVialPilar { get; set; }
        public List<EDSegVialResultado> ListaResultados  { get; set; }
    }
}
