using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Ausentismo
{
    public class EDContingencia
    {
        public int IdContingencia { get; set; }
        public int idTipoContingencia { get; set; }
        public string Detalle { get; set; }
        public DateTime Fecha_Ingreso { get; set; }
        public DateTime Fecha_Modificacion { get; set; }
    }
}
