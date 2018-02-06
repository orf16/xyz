using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Participacion
{
    public class EDParticipantesActaConvivencia
    {
        public int? PK_Id_Acta { get; set; }
        public int Consecutivo_Acta { get; set; }
        public int IdSede { get; set; }
        public int Pk_Id_Participante { get; set; }
        public int Numero_Documento { get; set; }
        public string Nombre { get; set; }
        public int UsuarioSistema { get; set; }
        public string NombreUsuario { get; set; }
    }
}
