using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Participacion
{
    public class EDConsultaTrazabilidad
    {
        public int PkConsultaED { get; set; }

        public DateTime Fecha_Fab { get; set; }

        public string ObservacionesED { get; set; }

        
        public string NombreArchivo1 { get; set; }
        
        public string NombreArchivo1_download { get; set; }
        
        public string Ruta1 { get; set; }
        
        public string NombreArchivo2 { get; set; }
        public string NombreArchivo2_download { get; set; }
        
        public string Ruta2 { get; set; }
       
        public string NombreArchivo3 { get; set; }
        public string NombreArchivo3_download { get; set; }
        
        public string Ruta3 { get; set; }

    }
}
