using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Participacion
{
    public class EDSeguimientoActaConvivencia
    {
        public int PK_Id_Seguimiento { get; set; }
        public int Consecutivo_Evento { get; set; }
        public DateTime Fecha { get; set; }
        public string NombreParteInvolucrada { get; set; }
        public string CompromisosAdquiridos { get; set; }
        public string Observaciones { get; set; }
        public int Fk_Id_Acta { get; set; }
        public int Fk_Id_Sede { get; set; }
        public int UsuarioSistema { get; set; }
        public string NombreUsuario { get; set; }
        public List<EDCompromisosPendientes> CompromisosPendientes { get; set; }
    }
}
