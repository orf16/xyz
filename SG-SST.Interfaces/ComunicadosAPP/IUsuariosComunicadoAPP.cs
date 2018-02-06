using SG_SST.EntidadesDominio.ComunicadosAPP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Interfaces.ComunicadosAPP
{
    public interface IUsuariosComunicadoAPP
    {
        List<EDUsuarioComunicadosAPP> ListarComunicadosPorUsuario(string IdentificacionUsuario);
        List<EDUsuarioComunicadosAPP> ListarComunicadosUsuarioPorEstado(string IdentificacionUsuario, int Estado);
        bool UpdateUsuarioComunicadoAPP(int PK_Id_Mensaje, int Estado);

        bool UpdateUsuarioPlayerIDComunicadoAPP(string IdentificacionUsuario, string PlayerID);
    }
}
