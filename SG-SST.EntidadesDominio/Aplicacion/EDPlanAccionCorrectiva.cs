using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Aplicacion
{
    public class EDPlanAccionCorrectiva
    {
        public int PkplanAccionCorrectivaED { get; set; }
        public string AdjuntoSeguimientoED { get; set; }
        public string NombreVerificadorED { get; set; }
        public string RespuestaED { get; set; }
        public int FkPlaAccionED { get; set; }
        public string InformacionActividadED { get; set; }

        public int PkprocesoED { get; set; }

        public string  NombreProcesoED{ get; set; }
        public int PksedeED { get; set; }
        public string nombresedeED { get; set; }

        public string DescribeCondicionED { get; set; }

        public List<EDPlanAccionInspeccion> Planes { get; set; }
    }
}
