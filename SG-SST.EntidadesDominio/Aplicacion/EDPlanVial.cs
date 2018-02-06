using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Aplicacion
{
    public class EDPlanVial
    {

        public int Pk_Id_SegVial { get; set; }
        public int Id_Consecutivo { get; set; }
        public DateTime Fecha_Registro { get; set; }
        public bool Estado { get; set; }

        public int Version { get; set; }
        public int Fk_Id_Sede { get; set; }
        public string NombreSede { get; set; }

        public List<EDSegVialResultado> ListaResultados { get; set; }

    }
}
