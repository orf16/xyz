using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SG_SST.EntidadesDominio.MedicionEvaluacion
{
    public class EDAccion
    {
        
        public int Pk_Id_Accion { get; set; }        
        public int Id_Accion { get; set; }      
        public string Tipo { get; set; }       
        public DateTime Fecha_dil { get; set; }       
        public DateTime Fecha_ocurrencia { get; set; }       
        public string Clase { get; set; }      
        public Nullable<System.DateTime> Fecha_hall { get; set; }
        public string Origen { get; set; }
        public string Otro_Origen { get; set; }
        public string Halla_Num_Doc { get; set; }     
        public string Halla_Nombre { get; set; }      
        public string Halla_TipoDoc { get; set; }        
        public string Halla_Cargo { get; set; }  
        public string Halla_Sede { get; set; }   
        public string Correccion { get; set; }
        public string Causa_Raiz { get; set; }  
        public string Cambio_Doc { get; set; }
        public string Des_Cambio_Doc { get; set; }  
        public string Verificacion { get; set; }    
        public string Eficacia { get; set; }
        public string Estado { get; set; }
        //Campos de Auditor
        public string NombreArchivoAuditor { get; set; }
        public string RutaArchivoAuditor { get; set; }
        public HttpPostedFileBase File_Auditor { set; get; }
        public string Nombre_Auditor { get; set; }
        public string Cargo_Auditor { get; set; }
        public string FirmaScrImageAud { set; get; }

        //Campos de responsable

        public string NombreArchivoResp { get; set; }
        public string RutaArchivoResp { get; set; }
        public HttpPostedFileBase File_Responable { set; get; }
        public string Nombre_Responsable { get; set; }
        public string Cargo_Responsable { get; set; }
        public string FirmaScrImageRes { set; get; }


        public int Fk_Id_Empresa { get; set; }


        public List<EDHallazgo> HallazgoLista { get; set; }
        public List<EDAnalisis> AnalisisLista { get; set; }
        public List<EDActividad> ActividadLista { get; set; }
        public List<EDSeguimiento> SeguimientoLista { get; set; }
        public List<EDArchivosAcciones> ArchivosLista { get; set; }

    }
}
