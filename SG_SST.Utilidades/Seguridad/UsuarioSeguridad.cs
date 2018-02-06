using SG_SST.Models.Empresas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Utilidades.Seguridad
{
    public class UsuarioSeguridad
    {
        public static Usuario Obtener()
        {
            Usuario objUsuario = new Usuario();
            objUsuario.Fk_Tipo_Documento = 1;
            objUsuario.Numero_Documento = 34243432;
            //objUsuario.UsuarioRoles.FirstOrDefault().Fk_Id_Rol = 1;
            objUsuario.Nombre_Usuario = "JUAN RESTREPO";
            objUsuario.nit_Empresa = "123";
            objUsuario.Fk_Id_Empresa = 1;
            return objUsuario;
        }
    }
}
