using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SG_SST.Models.Aplicacion
{
    public class CondicionesVModel
    {
        public int pkCondicionVM { get; set; }
        public string DescribeCondicionVM { get; set; }
        public string UbicacionespecificaVM { get; set; }
        public string RiesgopeligroVM { get; set; }
        public string ClasificacionRiesgoVM { get; set; }
        public string EvidenciacondicionVM { get; set; }
        public int ConfiguracioncondicionVM { get; set; }
        public string EstadoConfiguracionVM { get; set; }

        public string OtroRiesgoVM{ get; set; }
        public int idinspeccion { get; set; }
    }
}