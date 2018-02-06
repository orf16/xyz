using SG_SST.EntidadesDominio.Empresas;
using SG_SST.EntidadesDominio.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Interfaces.Usuarios
{
    public interface IUsuario
    {
        IEnumerable<EDTipoDocumento> ObtenerTiposDocumento();
        IEnumerable<EDRolSistema> ObtenerRolesSistema();
        IEnumerable<EDCausalRechazoUsuarioSistema> ObtenerCausalesRechazoUsuariosSistema();
        IEnumerable<EDPreguntaSeguridad> ObtenerPreguntasSeguridad();
        IEnumerable<EDParametroSistema> ObtenerParametrosSistema(List<int> idParametros);
        EDParametroSistema ObtenerParametrosSistema(string NombrePlantilla);
        Dictionary<string, string> ObtenerEmpresasSinRegistrar(List<EDUsuarioPorAprobar> usuariosAprobar);
        List<EDUsuarioSistema> ObtenerDatosUsuariosSistema(List<EDUsuarioPorAprobar> usuariosAprobar);
        IEnumerable<EDUsuarioPorAprobar> ObtenerUsuariosParaAprobar(string numDocEmp, string numDocUsu, string rolSeleccionado, int paginaActual);
        int ObtenerTotalUsuariosParaAprobar(string numDocEmp, string numDocUsu, string rolSeleccionado);
        EDParametroSistema ObtenerParametrosSistema(int codPlantilla);
        bool ValidarExistenciaUsuario(EDUsuarioPorAprobar usuarioRegistrar, out int error);
        bool RegistrarUsuarioParaAprobar(EDUsuarioPorAprobar usuarioSistema);
        bool InsertarUsuariosAprobadosSistema(List<EDUsuarioSistema> usuariosSistema);
        bool InsertarUsuariosNoAprobadosSistema(List<EDUsuarioPorAprobar> usuariosSistema);
        EDUsuarioSistema AutenticarUsuario(EDUsuarioSistema usuarioActual);
        EDUsuarioSistema ConsultarUsuarioPorRelacionLaboral(string tipoDocEmp, string numDocEmp, string tipoDocUsuario, string numDocUsuario);
        EDUsuarioSistema ConsultarDatosUsuarioPorRelacionLaboral(string tipoDocEmp, string numDocEmp, string tipoDocUsuario, string numDocUsuario);
        bool CambiarClaveUsuario(EDUsuarioSistema usuario);
        bool ActualizarClaveUsuarioParaRecuperarClave(EDUsuarioSistema usuario);
        int ValidarExistenciaUsuarioRolRepresLegal(string tipoDocumentoEmpresa, string numeroDocEmpresa, int RolEvaluar);
        int validarCantidadUsuariosPorRol(string tipoDocumentoEmpresa, string documentoEmpresa, int idRolSeleccionado);
        void ActualizarRolesNuevaEmpresa(int id);

    }
}
