using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Enumeraciones
{
    public class EnumAdministracionUsuarios
    {
        public enum RolesSistema
        {
            RepresentanteLegal = 1,
            ResponsableSST = 2,
            LiderSST = 3,
            AsesorSST = 4,
            AnalistaSST = 5,
            Trabajador = 6,
            Administrador = 7
        }
        public enum EstadosUsuariosSistema
        {
            Activo = 1,
            Inactivo = 2,
            Bloqueado = 3
        }
        public enum ValidaEstadoEmpAfi
        {
            NoExisteEmp = 0,
            ExisteInactivoEmp = 1,
            ExisteActivoEmp = 2,
            NoExisteAfi = 3,
            ExisteInactivoAfi = 4,
            ExisteActivoAfi = 5
        }
        public enum ValidaEstadoObjeto
        {
            NoExiste = 0,
            ExisteInactivo = 1,
            ExisteActivo = 2,
            ExistePorAprobar = 3
        }
        public enum ValidaCantidadUsuariosPorRol
        {
            NoSePuedeCrearTodosAprobados = 0,
            NoSePuedeCrearAlgunosSinAprobar = 1,
            SePuedeCrear = 2,
        }
        public enum ParametrosSistema
        {
            RutaHttpSitio = 1,
            ServidorStmp = 2,
            PuertoServidorStmp = 3,
            RemitenteNotificaion = 4,
            CorreoRemitente = 5,
            CaracteresPasswordTemporal = 6,
            LongitudPasswordTemporal = 7,
            UsuarioServidorStmp = 8,
            PasswordServidorStmp = 9,
        }
        public enum PlantillasCorreo
        {
            NotificacionAprobacionUsuario = 1,
            NotificacionRechazoUsuario = 2,
            NotificacionRecuperarClave = 3
        }

        public static class NombresEstadosUsuariosSistema
        {
            public static string Activo { get { return "Activo"; } }
            public static string Inactivo { get { return "Inactivo"; } }
            public static string Bloqueado { get { return "Bloqueado"; } }
        }

        public static class PlantillasCorreoPorNombre
        {
            public static string NotificacionAprobacionUsuario { get { return "NotificacionAprobacionUsuario"; } }
            public static string NotificacionRechazoUsuario { get { return "NotificacionRechazoUsuario"; } }
            public static string NotificacionRecuperarClave { get { return "NotificacionRecuperarClave"; } }
        }

        public static class ParametrosSistemaPorNombre
        {
            public static string ServidorStmp { get { return "ServidorStmp"; } }
            public static string RutaHttpSitio { get { return "RutaHttpSitio"; } }
            public static string PuertoServidorStmp { get { return "PuertoServidorStmp"; } }

            public static string RemitenteNotificaion { get { return "RemitenteNotificaion"; } }
            public static string CorreoRemitente { get { return "CorreoRemitente"; } }
            public static string CaracteresPasswordTemporal { get { return "CaracteresPasswordTemporal"; } }
            public static string LongitudPasswordTemporal { get { return "LongitudPasswordTemporal"; } }
            public static string UsuarioServidorStmp { get { return "UsuarioServidorStmp"; } }
            public static string ClaceServidorStmp { get { return "ClaceServidorStmp"; } }
            public static string CantRegistrosPagPaginador { get { return "CantRegistrosPagPaginador"; } }
            public static string RolesBaseEmpresa { get { return "RolesBaseEmpresa"; } }
        }
    }
}
