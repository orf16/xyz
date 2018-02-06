using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.ComunicadosAPP
{
    public class EDUsuarioComunicadosAPP
    {
        public int PK_Id_Mensaje { get; set; }
        public int FK_Id_ComunicadosAPP { get; set; }
        public string PlayerID { get; set; }
        public string IdentificacionUsuario { get; set; }
        public int IDEstadoComunicado { get; set; }
        public string EstadoComunicado { get; set; }
        public string Titulo { get; set; }
        public string Asunto { get; set; }
        public string Destinatario { get; set; }
        public string FechaCreacion { get; set; }
        public string FechaEnvio { get; set; }

    }
}
