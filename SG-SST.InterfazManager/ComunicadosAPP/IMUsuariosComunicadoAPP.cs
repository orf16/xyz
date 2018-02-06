using SG_SST.Interfaces.ComunicadosAPP;
using SG_SST.Repositorio.ComunicadosAPP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.InterfazManager.ComunicadosAPP
{
    public class IMUsuariosComunicadoAPP
    {
        public static IUsuariosComunicadoAPP UsuarioComunicadoAPP()
        {
            return new ComunicadosAppManager();
        }
    }
}
