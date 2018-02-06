using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SG_SST.EntidadesDominio.MedicionEvaluacion
{
    public  class EDActividad
    {
        public int Pk_Id_Actividad { get; set; }
        public string Actividad { get; set; }
        public string Responsable { get; set; }
        public DateTime FechaFinalizacion { get; set; }   


    
        public string NombreArchivoAct { get; set; }
        public string RutaArchivoAct { get; set; }   
        public HttpPostedFileBase File_Actividad { set; get; }     
        public string FirmaScrImage { set; get; }
        public int Clave { set; get; }
        public int Estado { set; get; }
        public byte Estado_1 { set; get; }
        public int Fk_Id_Accion { get; set; }
    }
}
