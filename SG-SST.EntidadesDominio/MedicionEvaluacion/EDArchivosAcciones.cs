using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SG_SST.EntidadesDominio.MedicionEvaluacion
{
    public class EDArchivosAcciones
    {     
        public int Pk_Id_Archivo { get; set; }
        public string NombreArchivo { get; set; }
        public string Ruta { get; set; }
        public string Extension { get; set; }
        public string Tamaño { get; set; }
        public HttpPostedFileBase file { set; get; }
        public int Estado { set; get; }
        public int IdFile { set; get; }

        public int Fk_Id_Accion { get; set; }
    }
}
