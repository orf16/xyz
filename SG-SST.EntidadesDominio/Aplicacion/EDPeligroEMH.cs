using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Aplicacion
{
    public class EDPeligroEMH
    {
        public int Pk_Id_PeligroEMH { get; set; }
        public int Fk_Id_Peligro { get; set; }
        public int Fk_Id_AdmoEMH { get; set; }
    }
}
