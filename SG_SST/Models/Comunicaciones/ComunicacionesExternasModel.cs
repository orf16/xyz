using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SG_SST.Models.Comunicaciones
{
    public class ComunicacionesExternasModel
    {
        public int PK_Id_Comunicado { get; set; }
        public string Titulo { get; set; }
        public string Asunto { get; set; }
        public string CuerpoMensaje { get; set; }
        public string Destinatarios { get; set; }
        public string EstadoComunicado { get; set; }
        public string FechaCreacion { get; set; }
        public string FechaEnvio { get; set; }
        public int PK_Id_grupo { get; set; }
        public int pk_id_grupo_usuario_comunicaciones { get; set; }
    }
}