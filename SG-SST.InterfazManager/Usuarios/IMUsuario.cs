using SG_SST.Interfaces.Usuarios;
using SG_SST.Repositorio.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.InterfazManager.Usuarios
{
    public class IMUsuario
    {
        public static IUsuario UsuarioSesion()
        {
            return new UsuarioManager();
        }
    }
}
