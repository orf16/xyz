using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SG_SST.Models.Comunicaciones
{
    public class ComunicacionesInternasModel
    {
        public int PK_Id_Encuesta { get; set;}
        public string Titulo { get; set;}
        [AllowHtml]
        public string CuerpoHTML { get; set;}
        public string CuerpoHtmlTemp { get; set; }
        public string EstadoEncuesta { get; set;}
        public string FechaCreacion { get; set;}
        public string FechaEnvio { get; set; }
        public string URL { get; set; }

    }

    public class ComunicacionesPublicas
    {
        public int fk_pk_id_encuesta { get; set; }
        public string titulo { get; set; }

        [AllowHtml]
        public string contenido { get; set; }
    }
}