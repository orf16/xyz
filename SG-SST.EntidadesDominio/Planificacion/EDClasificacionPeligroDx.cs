using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Planificacion
{
    public class EDClasificacionPeligroDx
    {
        public int idClasificacionPeligroDx { get; set; }
        public int idClasifiacionPeligro { get; set; }
        public int FK_DxCondicionesDeSalud { get; set; }
        public string nombreTipoPeligro { get; set; }
        public string nombreDescripcion { get; set; }

        public double porcentajeClasificacionPeligros { get; set; }

    }
}
