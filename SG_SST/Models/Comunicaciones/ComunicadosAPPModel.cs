using SG_SST.EntidadesDominio.ComunicadosAPP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SG_SST.Models.Comunicaciones
{
    public class ComunicadosAPPModel
    {
        public int IDComunicadosAPP { get; set; }
        public int FK_Id_ComunicadosAPP { get; set; }
        public string Titulo { get; set; }
        
        [AllowHtml]
        public string Asunto { get; set; }
        
        public string Destinatarios { get; set; }
        public string FechaCreacion { get; set; }
        public string FechaEnvio { get; set; }
    
    }
}