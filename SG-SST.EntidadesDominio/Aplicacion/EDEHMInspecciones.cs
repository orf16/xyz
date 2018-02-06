using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Aplicacion
{
    public class EDEHMInspecciones
    {
        public int PK_Id_inspeccion { get; set; }
        public int IdConsecutivo { get; set; }
        public string EDDescribeinspeccion { get; set; }
        public string EDNombreInspeccion { get; set; }
        public DateTime Fecha { get; set; }

    }
}
