using SG_SST.EntidadesDominio.ComunicadosAPP;
using SG_SST.Interfaces.ComunicadosAPP;
using SG_SST.InterfazManager.ComunicadosAPP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Logica.ComunicadosAPP
{
    public class LNUsuariosComunicadoAPP
    {
        private static IUsuariosComunicadoAPP uca = IMUsuariosComunicadoAPP.UsuarioComunicadoAPP();

        public List<EDUsuarioComunicadosAPP> ListarComunicadosPorUsuario(string IdentificacionUsuario)
        {
            return uca.ListarComunicadosPorUsuario(IdentificacionUsuario);
        }

        public List<EDUsuarioComunicadosAPP> ListarComunicadosUsuarioPorEstado(string IdentificacionUsuario, int Estado)
        {
            return uca.ListarComunicadosUsuarioPorEstado(IdentificacionUsuario, Estado);
        }

        public bool UpdateUsuarioComunicadoAPP(int PK_Id_Mensaje, int Estado)
        {
            return uca.UpdateUsuarioComunicadoAPP(PK_Id_Mensaje, Estado);
        }

        public bool UpdateUsuarioPlayerIDComunicadoAPP(string IdentificacionUsuario, string PlayerID)
        {
            return uca.UpdateUsuarioPlayerIDComunicadoAPP(IdentificacionUsuario, PlayerID);
        }
    }
}
