using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Planificacion
{
    public class EDEmpresaEvaluar
    {
        public int IdEmpresaEvaluar { get; set; }
        public string RazonSocial { get; set; }
        public string Nit { get; set; }
        public int CodActividadEconomica { get; set; }
        public string ActividadEconomica { get; set; }
        public string ResponsableSGSST { get; set; }
        public string ElaboradoPor { get; set; }
        public string NumLicenciaSOSL { get; set; }
        public int CodSede { get; set; }
        public decimal CalificacionFinal { get; set; }
        public DateTime FechaDiligencia { get; set; }
        public List<EDAspecto> Aspectos { get; set; }
    }
}
