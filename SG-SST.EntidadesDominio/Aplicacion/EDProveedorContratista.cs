using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Aplicacion
{
    public class EDProveedorContratista
    {
        public int PK_ProveedorContratista { get; set; }
        public string Nombre_ProveedorContratista { get; set; }
        public string Nit_ProveedorContratista { get; set; }
        public int? CalificacionHistorico { get; set; }
        public DateTime fechapi { get; set; }
        public string frecuenciaEvaluacion { get; set; }
        public int idEmpresa { get; set; }
    }
}
