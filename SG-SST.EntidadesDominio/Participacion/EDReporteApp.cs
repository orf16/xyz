using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SG_SST.EntidadesDominio.Participacion
{
    public class EDReporteApp
    {
        public int sede { get; set; }
        public int tipo { get; set; }

        public int? proceso { get; set; }

        public string area { get; set; }

        public DateTime fechaEvento { get; set; }

        public string fechaOcurrencia { get; set; }

        public string descripcion { get; set; }

        public string causa { get; set; }

        public string sugerencia { get; set; }

        public string nombreImagen { get; set; }

        public string nitEmpresa { get; set; }

        public string nombreEmpresa { get; set; }

        public int cedulaQuienReporta { get; set; }
        public string imagen { get; set; }

        public bool medioAcceso { get; set; }
    }
}
