using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Aplicacion
{
    public class EDEPPCargo
    {
        public int Pk_Id_EPPCargo { get; set; }
        public int Cantidad { get; set; }
        public int Fk_Id_Cargo { get; set; }
        public int Fk_Id_EPP { get; set; }
        public string Nombre { get; set; }
    }
}
