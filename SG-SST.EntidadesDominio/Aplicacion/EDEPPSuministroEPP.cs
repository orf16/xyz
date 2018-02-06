using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Aplicacion
{
    public class EDEPPSuministroEPP
    {
        public int Pk_Id_EPPSuministroEPP { get; set; }
        public int Cantidad { get; set; }
        public DateTime Fecha { get; set; }
        public int Fk_Id_EPP { get; set; }
        public int Fk_Id_EPPSuministro { get; set; }
    }
}
