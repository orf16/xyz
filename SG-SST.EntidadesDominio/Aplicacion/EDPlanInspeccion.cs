using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Aplicacion
{
    public class EDPlanInspeccion
    {

        public int Idplaninspeccion { get; set; }
        public string responsable { get; set; }

        public int Idtipoinspeccion { get; set; }
        public int DescripcionTipoInspeccion { get; set; }
        public string Describetipoinspeccion { get; set; }
        public string descripcion { get; set; }
        public DateTime Fecha { get; set; }
        public int idEmpresaED { get; set; }
        public string EstadoPlaneacionED { get; set; }
        public int ConsecutivoPlanED { get; set; }

        

    }

}
