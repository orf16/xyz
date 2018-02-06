using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Participacion
{
    public class EDCompromisosPendientes
    {
        public int Pk_Id_Compromiso { get; set; }
        public string CompromisoPendiente { get; set; }
        public int FK_Id_Seguimiento { get; set; }
        public int PK_Id_Acta { get; set; }
        public int UsuarioSistema { get; set; }
        public string NombreUsuario { get; set; }
    }
}
