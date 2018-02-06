using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.EstudioPuestoTrabajo
{
    public class EDSeguimientoEstudioPuestoTrabajo
    {
        public int IdEstudioPuestoTrabajo { get; set; }
        public string Actividad { get; set; }
        public DateTime Fecha { get; set; }
        public string FechaStr { get; set; }
        public string Responsable { get; set; }
        public string Result { get; set; }
    }
}
