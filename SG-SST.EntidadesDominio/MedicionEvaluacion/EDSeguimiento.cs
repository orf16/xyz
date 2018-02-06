using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SG_SST.EntidadesDominio.MedicionEvaluacion
{
    public class EDSeguimiento
    {
        public int Pk_Id_Seguimiento { get; set; }
        public DateTime Fecha_Seg { get; set; }
        public string Observaciones { get; set; }

        public string NombreArchivoSeg { get; set; }   
        public string RutaArchivoSeg { get; set; }
        public HttpPostedFileBase File_Seguimiento { set; get; } 
        public string FirmaScrImage { set; get; }
        public int Clave { set; get; }
        public int Estado { set; get; }

        public int Fk_Id_Accion { get; set; }
    }
}
