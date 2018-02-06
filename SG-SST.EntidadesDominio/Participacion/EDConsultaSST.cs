using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Participacion
{
   public class EDConsultaSST
    {
        public int PkConsultaED { get; set; }
        public int FkEmpresaED { get; set; }
        public string TipoConsultaED { get; set; }
        public string DescripcionConsultaED { get; set; }
        public int IdUsuarioED { get; set; }
        public DateTime FechaConsultaED { get; set; }
        public DateTime FechaRevisionED { get; set; }

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

        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Documento { get; set; }
    }
}
