using SG_SST.ClienteServicios;
using SG_SST.EntidadesDominio.Empresas;
using SG_SST.EntidadesDominio.Usuario;
using SG_SST.Interfaces.Usuarios;
using SG_SST.InterfazManager.Usuarios;
using SG_SST.Logica.Empresas;
using SG_SST.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Logica.Usuarios
{
    public class LNUsuario
    {
        private static IUsuario us = IMUsuario.UsuarioSesion();

        public IEnumerable<EDTipoDocumento> ObtenerTiposDocumento()
        {
            return us.ObtenerTiposDocumento();
        }
        public IEnumerable<EDRolSistema> ObtenerRolesSistema()
        {
            return us.ObtenerRolesSistema();
        }
        public IEnumerable<EDCausalRechazoUsuarioSistema> ObtenerCausalesRechazoUsuariosSistema()
        {
            return us.ObtenerCausalesRechazoUsuariosSistema();
        }
        public IEnumerable<EDPreguntaSeguridad> ObtenerPreguntasSeguridad()
        {
            return us.ObtenerPreguntasSeguridad();
        }
        public IEnumerable<EDUsuarioPorAprobar> ObtenerUsuariosParaAprobar(string numDocEmp, string numDocUsu, string rolSeleccionado, int paginaActual)
        {
            return us.ObtenerUsuariosParaAprobar(numDocEmp, numDocUsu, rolSeleccionado, paginaActual);
        }

        public int ObtenerTotalUsuariosParaAprobar(string numDocEmp, string numDocUsu, string rolSeleccionado)
        {
            return us.ObtenerTotalUsuariosParaAprobar(numDocEmp, numDocUsu, rolSeleccionado);
        }

        public EDUsuarioSistema ConsultarDatosUsuarioPorRelacionLaboral(string tipoDocEmp, string numDocEmp, string tipoDocUsuario, string numDocUsuario)
        {
            return us.ConsultarDatosUsuarioPorRelacionLaboral(tipoDocEmp, numDocEmp, tipoDocUsuario, numDocUsuario);
        }
        /// <summary>
        /// Se regsitra la información del empleado que ha solicitado
        /// la creación de su usuario.
        /// </summary>
        /// <param name="usuarioRegistrar"></param>
        /// <returns></returns>
        public string RegistrarUsusariosParaAprobar(EDUsuarioPorAprobar usuarioRegistrar)
        {
            var resultado = string.Empty;
            try
            {
                var error = 0;
                var valid = us.ValidarExistenciaUsuario(usuarioRegistrar, out error);
                if (valid == false && error == 0)
                {
                    var result = us.RegistrarUsuarioParaAprobar(usuarioRegistrar);
                    if (result)
                    {
                        var enviarCorreo = EnvioCorreos.EnviarCorreo(string.Empty, string.Empty, string.Empty, false, string.Empty, 25, string.Empty, "Usuario para Aprobar", usuarioRegistrar.Correo);
                    }
                    resultado = Recursos.AdministracionUsuarios.MensajesAdministracionUsuarios.UsuarioParaAprobarRegistrado;
                }
                else if (error > 0)
                    resultado = Recursos.AdministracionUsuarios.MensajesAdministracionUsuarios.ErrorUsuarioParaAprobar;
                else
                    resultado = Recursos.AdministracionUsuarios.MensajesAdministracionUsuarios.UsuarioParaAprobarExiste;
            }
            catch (Exception ex)
            {
                resultado = Recursos.AdministracionUsuarios.MensajesAdministracionUsuarios.ErrorUsuarioParaAprobar;
            }
            return resultado;
        }

        /// <summary>
        /// Se registran los usuarios que han sido aprobados en la tabla UsuariosSistema
        /// y los que han sido rechazados en la tabla UsuariosRechazados
        /// </summary>
        /// <param name="usuarioRegistrar"></param>
        /// <returns></returns>
        public bool RegistrarUsusariosPorEmpresa(List<EDUsuarioPorAprobar> usuariosAprobar)
        {
            try
            {
                //Se obtienen los usuarios que han sido aprobados
                var usuariosAprobados = usuariosAprobar.Where(ua => ua.Aprobado == true).ToList();
                //Se obtienen los usuarios que no fueron aprobados
                var usuariosNoAprobados = usuariosAprobar.Where(ua => ua.Aprobado == false).ToList();
                //Se obtienen las empresas que no se encuentran registradas en Alista
                var usuariosSinEmpresas = us.ObtenerEmpresasSinRegistrar(usuariosAprobados);
                var infoEmpresasSiarp = new List<EDEmpresas>();
                //se obtiene la informacion de las empresas ante Siarp
                foreach (var empresa in usuariosSinEmpresas)
                {
                    infoEmpresasSiarp.Add(ClienteEmpresa.ObtenerEmpresasSiarp(empresa.Value, empresa.Key));
                }
                if (infoEmpresasSiarp != null && infoEmpresasSiarp.Count > 0)
                {
                    var lnEmpresa = new LNEmpresa();
                    foreach (var infoEmp in infoEmpresasSiarp)
                    {
                        if (infoEmp != null)
                        {
                            //se guardan los roles para asociarlos a la nueva empresa
                            var emp = lnEmpresa.GuardarEmpresaYSusRelaciones(infoEmp);
                        }
                    }
                }
                var result = false;
                //Se obtiene la información de los usuarios que han sido aprobados.
                var usuariosSistema = us.ObtenerDatosUsuariosSistema(usuariosAprobados);
                if (usuariosSistema != null && usuariosSistema.Count > 0)
                {
                    var caracteresPasswordTemporal = us.ObtenerParametrosSistema(new List<int>() { (int)Enumeraciones.EnumAdministracionUsuarios.ParametrosSistema.CaracteresPasswordTemporal }).FirstOrDefault().Valor;
                    var longitudPasswordTemporal = us.ObtenerParametrosSistema(new List<int>() { (int)Enumeraciones.EnumAdministracionUsuarios.ParametrosSistema.LongitudPasswordTemporal }).FirstOrDefault().Valor;
                    var passwordTemporal = LNGeneraPassword.GenerarPasswordTemporalAleatorio(caracteresPasswordTemporal, longitudPasswordTemporal);
                    var saltPassword = LNGeneraPassword.GenerarSalt();
                    var hashPassword = LNGeneraPassword.CalcularHash(passwordTemporal, saltPassword);
                    //se le asigna clave temporal a los usuarios que han sido aprobados
                    usuariosSistema.ForEach(u => {
                        u.ClaveSalt = Convert.ToBase64String(saltPassword);
                        u.ClaveHash = Convert.ToBase64String(hashPassword);
                        u.Activo = true;
                        u.Clave = passwordTemporal;
                    });
                    //se insertan en base de datos los usuarios aprobados
                    result = us.InsertarUsuariosAprobadosSistema(usuariosSistema);
                    if (result)
                    {
                        foreach (var u in usuariosSistema)
                        {
                            //envia correo a los usuarios aprobados y activados
                            EnviarCorreoParaNotificacion((int)Enumeraciones.EnumAdministracionUsuarios.PlantillasCorreo.NotificacionAprobacionUsuario, u);
                        }
                    }
                }
                if (usuariosNoAprobados != null && usuariosNoAprobados.Count > 0)
                {
                    result = us.InsertarUsuariosNoAprobadosSistema(usuariosNoAprobados);
                    if (result)
                    {
                        foreach (var u in usuariosNoAprobados)
                        {
                            EnviarCorreoParaNotificacion((int)Enumeraciones.EnumAdministracionUsuarios.PlantillasCorreo.NotificacionRechazoUsuario, u);
                        }
                    }
                }
                return result;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Valida la autenticidad del usuario y devuelve los 
        /// datos del usuario si la autenticación fue correcta
        /// </summary>
        /// <param name="usuarioRegistrar"></param>
        /// <returns></returns>
        public EDUsuarioSistema AutenticarUsuario(EDUsuarioSistema usuarioRegistrar)
        {
            try
            {
                var usuarioPorAutenticar = us.AutenticarUsuario(usuarioRegistrar);
                if (usuarioPorAutenticar != null)
                {
                    var saltPassword = Convert.FromBase64String(usuarioPorAutenticar.ClaveSalt);
                    var hashPassword = Convert.FromBase64String(usuarioPorAutenticar.ClaveHash);
                    var validaAutenticacion = LNGeneraPassword.VerificarPassword(usuarioRegistrar.Clave, saltPassword, hashPassword);
                    if (validaAutenticacion)
                        return usuarioPorAutenticar;
                    else
                        return null;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public bool CambiarClaveUsuario(EDUsuarioSistema usuario)
        {
            var saltPassword = LNGeneraPassword.GenerarSalt();
            var hashPassword = LNGeneraPassword.CalcularHash(usuario.Clave, saltPassword);
            //se le asigna clave temporal a los usuarios que han sido aprobados
            usuario.ClaveSalt = Convert.ToBase64String(saltPassword);
            usuario.ClaveHash = Convert.ToBase64String(hashPassword);
            usuario.Activo = true;
            usuario.Clave = string.Empty;
            usuario.PrimerAcceso = false;
            var result = us.CambiarClaveUsuario(usuario);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tipoDocEmp"></param>
        /// <param name="numDocEmp"></param>
        /// <param name="tipoDocUsuario"></param>
        /// <param name="numDocUsuario"></param>
        /// <returns></returns>
        public bool RecuperarClaveUsuario(string tipoDocEmp, string numDocEmp, string tipoDocUsuario, string numDocUsuario, string respuestaUno, string respuestaDos, string respuestaTres, out string resultadoRecuperarClave)
        {
            var result = false;
            var resultRespuestas = true;
            resultadoRecuperarClave = string.Empty;
            var usuario = us.ConsultarDatosUsuarioPorRelacionLaboral(tipoDocEmp, numDocEmp, tipoDocUsuario, numDocUsuario);
            if (usuario != null)
            {
                var resultRtaUno = usuario.PreguntasSeguridad.Where(rps => rps.RespuestaSeguridad.Equals(respuestaUno)).FirstOrDefault();
                if (resultRtaUno == null)
                    resultRespuestas = false;
                var resultRtaDos = usuario.PreguntasSeguridad.Where(rps => rps.RespuestaSeguridad.Equals(respuestaDos)).FirstOrDefault();
                if (resultRtaDos == null)
                    resultRespuestas = false;
                var resultRtaTres = usuario.PreguntasSeguridad.Where(rps => rps.RespuestaSeguridad.Equals(respuestaTres)).FirstOrDefault();
                if (resultRtaTres == null)
                    resultRespuestas = false;
                if (resultRespuestas)
                {
                    var caracteresPasswordTemporal = us.ObtenerParametrosSistema(new List<int>() { (int)Enumeraciones.EnumAdministracionUsuarios.ParametrosSistema.CaracteresPasswordTemporal }).FirstOrDefault().Valor;
                    var longitudPasswordTemporal = us.ObtenerParametrosSistema(new List<int>() { (int)Enumeraciones.EnumAdministracionUsuarios.ParametrosSistema.LongitudPasswordTemporal }).FirstOrDefault().Valor;
                    var passwordTemporal = LNGeneraPassword.GenerarPasswordTemporalAleatorio(caracteresPasswordTemporal, longitudPasswordTemporal);
                    var saltPassword = LNGeneraPassword.GenerarSalt();
                    var hashPassword = LNGeneraPassword.CalcularHash(passwordTemporal, saltPassword);
                    //se le asigna clave temporal al usuario que desea recuperar su clave y 
                    //se obliga a que realice el proceso de cambiar su clave (primerAcceso = true)
                    usuario.Clave = passwordTemporal;
                    usuario.ClaveHash = Convert.ToBase64String(hashPassword);
                    usuario.ClaveSalt = Convert.ToBase64String(saltPassword);
                    usuario.PrimerAcceso = true;
                    //actualiza el usuario del sistema con una clave temporal
                    var resultado = us.ActualizarClaveUsuarioParaRecuperarClave(usuario);
                    if (resultado)
                    {
                        //envia correo a los usuarios aprobados y activados
                        EnviarCorreoParaNotificacion(0, usuario);
                        result = true;
                    }
                }else
                    resultadoRecuperarClave = "Las respuestas diligenciadas no corresponden con las registradas en el sistema.";
            }
            else
                resultadoRecuperarClave = "No existe un usuario asociado a los datos ingresados.";
            return result;
        }

        public int ValidarExistenciaUsusarioReprLegal(string tipoDocumentoEmpresa, string numeroDocEmpresa, int RolEvaluar)
        {
            return us.ValidarExistenciaUsuarioRolRepresLegal(tipoDocumentoEmpresa, numeroDocEmpresa, RolEvaluar);
        }

        public int validarCantidadUsuariosPorRol(string tipoDocumentoEmpresa, string documentoEmpresa, int idRolSeleccionado)
        {
            return us.validarCantidadUsuariosPorRol(tipoDocumentoEmpresa, documentoEmpresa, idRolSeleccionado);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="codPlantilla"></param>
        /// <param name="usuario"></param>
        private void EnviarCorreoParaNotificacion(int codPlantilla, EDUsuarioSistema usuario)
        {
            var idParametros = new List<int>() {
                    (int)Enumeraciones.EnumAdministracionUsuarios.ParametrosSistema.RutaHttpSitio,
                    (int)Enumeraciones.EnumAdministracionUsuarios.ParametrosSistema.ServidorStmp,
                    (int)Enumeraciones.EnumAdministracionUsuarios.ParametrosSistema.RemitenteNotificaion,
                    (int)Enumeraciones.EnumAdministracionUsuarios.ParametrosSistema.CorreoRemitente,
                    (int)Enumeraciones.EnumAdministracionUsuarios.ParametrosSistema.PuertoServidorStmp,
                    (int)Enumeraciones.EnumAdministracionUsuarios.ParametrosSistema.UsuarioServidorStmp,
                    (int)Enumeraciones.EnumAdministracionUsuarios.ParametrosSistema.PasswordServidorStmp
                };
            var parametros = us.ObtenerParametrosSistema(idParametros);
            var plantilla = us.ObtenerParametrosSistema(codPlantilla);
            if(plantilla == null)
                plantilla = us.ObtenerParametrosSistema(Enumeraciones.EnumAdministracionUsuarios.PlantillasCorreoPorNombre.NotificacionRecuperarClave);
            var rutaHttpSitio = parametros.Where(p => p.IdParametro == (int)Enumeraciones.EnumAdministracionUsuarios.ParametrosSistema.RutaHttpSitio).Select(p => p).FirstOrDefault().Valor;
            var servidorSTMP = parametros.Where(p => p.IdParametro == (int)Enumeraciones.EnumAdministracionUsuarios.ParametrosSistema.ServidorStmp).Select(p => p).FirstOrDefault().Valor;
            var remitente = parametros.Where(p => p.IdParametro == (int)Enumeraciones.EnumAdministracionUsuarios.ParametrosSistema.RemitenteNotificaion).Select(p => p).FirstOrDefault().Valor;
            var correoRemitente = parametros.Where(p => p.IdParametro == (int)Enumeraciones.EnumAdministracionUsuarios.ParametrosSistema.CorreoRemitente).Select(p => p).FirstOrDefault().Valor;
            var puertoServidorStmp = parametros.Where(p => p.IdParametro == (int)Enumeraciones.EnumAdministracionUsuarios.ParametrosSistema.PuertoServidorStmp).Select(p => p).FirstOrDefault().Valor;
            var usuarioServidorStmp = parametros.Where(p => p.IdParametro == (int)Enumeraciones.EnumAdministracionUsuarios.ParametrosSistema.UsuarioServidorStmp).Select(p => p).FirstOrDefault().Valor;
            var passwordServidorStmp = parametros.Where(p => p.IdParametro == (int)Enumeraciones.EnumAdministracionUsuarios.ParametrosSistema.PasswordServidorStmp).Select(p => p).FirstOrDefault().Valor;

            var plantillaHtml = plantilla.Valor.Replace("[[RutaHttpSitio]]", rutaHttpSitio);
            plantillaHtml=  plantillaHtml.Replace("[[NombreUsuario]]", string.Format("{0} {1}", usuario.Nombres, usuario.Apellidos));
            plantillaHtml = plantillaHtml.Replace("[[EmailUsuario]]", usuario.Correo);
            plantillaHtml = plantillaHtml.Replace("[[LoguinUsuario]]", usuario.Documento);
            plantillaHtml = plantillaHtml.Replace("[[PasswordTemporal]]", usuario.Clave);
            EnvioCorreos.EnviarCorreo(plantillaHtml, correoRemitente, remitente, true, passwordServidorStmp, Convert.ToInt32(puertoServidorStmp), servidorSTMP, "Aprobación de usuario", usuario.Correo);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="codPlantilla"></param>
        /// <param name="usuario"></param>
        private void EnviarCorreoParaNotificacion(int codPlantilla, EDUsuarioPorAprobar usuario)
        {
            var idParametros = new List<int>() {
                    (int)Enumeraciones.EnumAdministracionUsuarios.ParametrosSistema.RutaHttpSitio,
                    (int)Enumeraciones.EnumAdministracionUsuarios.ParametrosSistema.ServidorStmp,
                    (int)Enumeraciones.EnumAdministracionUsuarios.ParametrosSistema.RemitenteNotificaion,
                    (int)Enumeraciones.EnumAdministracionUsuarios.ParametrosSistema.CorreoRemitente,
                    (int)Enumeraciones.EnumAdministracionUsuarios.ParametrosSistema.PuertoServidorStmp,
                    (int)Enumeraciones.EnumAdministracionUsuarios.ParametrosSistema.UsuarioServidorStmp,
                    (int)Enumeraciones.EnumAdministracionUsuarios.ParametrosSistema.PasswordServidorStmp
                };
            var parametros = us.ObtenerParametrosSistema(idParametros);
            var plantilla = us.ObtenerParametrosSistema(codPlantilla);
            var rutaHttpSitio = parametros.Where(p => p.IdParametro == (int)Enumeraciones.EnumAdministracionUsuarios.ParametrosSistema.RutaHttpSitio).Select(p => p).FirstOrDefault().Valor;
            var servidorSTMP = parametros.Where(p => p.IdParametro == (int)Enumeraciones.EnumAdministracionUsuarios.ParametrosSistema.ServidorStmp).Select(p => p).FirstOrDefault().Valor;
            var remitente = parametros.Where(p => p.IdParametro == (int)Enumeraciones.EnumAdministracionUsuarios.ParametrosSistema.RemitenteNotificaion).Select(p => p).FirstOrDefault().Valor;
            var correoRemitente = parametros.Where(p => p.IdParametro == (int)Enumeraciones.EnumAdministracionUsuarios.ParametrosSistema.CorreoRemitente).Select(p => p).FirstOrDefault().Valor;
            var puertoServidorStmp = parametros.Where(p => p.IdParametro == (int)Enumeraciones.EnumAdministracionUsuarios.ParametrosSistema.PuertoServidorStmp).Select(p => p).FirstOrDefault().Valor;
            var usuarioServidorStmp = parametros.Where(p => p.IdParametro == (int)Enumeraciones.EnumAdministracionUsuarios.ParametrosSistema.UsuarioServidorStmp).Select(p => p).FirstOrDefault().Valor;
            var passwordServidorStmp = parametros.Where(p => p.IdParametro == (int)Enumeraciones.EnumAdministracionUsuarios.ParametrosSistema.PasswordServidorStmp).Select(p => p).FirstOrDefault().Valor;

            var plantillaHtml = plantilla.Valor.Replace("[[RutaHttpSitio]]", rutaHttpSitio);
            plantillaHtml = plantillaHtml.Replace("[[NombreUsuario]]", string.Format("{0} {1}", usuario.Nombres, usuario.Apellidos));
            plantillaHtml = plantillaHtml.Replace("[[EmailUsuario]]", usuario.Correo);
            plantillaHtml = plantillaHtml.Replace("[[CausalRechazo]]", usuario.NombreCausalRechazo);
            EnvioCorreos.EnviarCorreo(plantillaHtml, correoRemitente, remitente, true, passwordServidorStmp, Convert.ToInt32(puertoServidorStmp), servidorSTMP, "Rechazo solicitud usuario Alissta", usuario.Correo);
        }

        public EDNotificarInconsistencia EnviaNotificacionInconsistenciaLaborales(string NombrePlantilla, EDNotificarInconsistencia notIncon)
        {
            var idParametros = new List<int>() {
                    (int)Enumeraciones.EnumAdministracionUsuarios.ParametrosSistema.RutaHttpSitio,
                    (int)Enumeraciones.EnumAdministracionUsuarios.ParametrosSistema.ServidorStmp,
                    (int)Enumeraciones.EnumAdministracionUsuarios.ParametrosSistema.RemitenteNotificaion,
                    (int)Enumeraciones.EnumAdministracionUsuarios.ParametrosSistema.CorreoRemitente,
                    (int)Enumeraciones.EnumAdministracionUsuarios.ParametrosSistema.PuertoServidorStmp,
                    (int)Enumeraciones.EnumAdministracionUsuarios.ParametrosSistema.UsuarioServidorStmp,
                    (int)Enumeraciones.EnumAdministracionUsuarios.ParametrosSistema.PasswordServidorStmp
                };
            var parametros = us.ObtenerParametrosSistema(idParametros);
            var plantilla = us.ObtenerParametrosSistema(NombrePlantilla);
            var rutaHttpSitio = parametros.Where(p => p.IdParametro == (int)Enumeraciones.EnumAdministracionUsuarios.ParametrosSistema.RutaHttpSitio).Select(p => p).FirstOrDefault().Valor;
            var servidorSTMP = parametros.Where(p => p.IdParametro == (int)Enumeraciones.EnumAdministracionUsuarios.ParametrosSistema.ServidorStmp).Select(p => p).FirstOrDefault().Valor;
            var remitente = parametros.Where(p => p.IdParametro == (int)Enumeraciones.EnumAdministracionUsuarios.ParametrosSistema.RemitenteNotificaion).Select(p => p).FirstOrDefault().Valor;
            var correoRemitente = parametros.Where(p => p.IdParametro == (int)Enumeraciones.EnumAdministracionUsuarios.ParametrosSistema.CorreoRemitente).Select(p => p).FirstOrDefault().Valor;
            var puertoServidorStmp = parametros.Where(p => p.IdParametro == (int)Enumeraciones.EnumAdministracionUsuarios.ParametrosSistema.PuertoServidorStmp).Select(p => p).FirstOrDefault().Valor;
            var usuarioServidorStmp = parametros.Where(p => p.IdParametro == (int)Enumeraciones.EnumAdministracionUsuarios.ParametrosSistema.UsuarioServidorStmp).Select(p => p).FirstOrDefault().Valor;
            var passwordServidorStmp = parametros.Where(p => p.IdParametro == (int)Enumeraciones.EnumAdministracionUsuarios.ParametrosSistema.PasswordServidorStmp).Select(p => p).FirstOrDefault().Valor;

            var plantillaHtml = plantilla.Valor.Replace("[[RutaHttpSitio]]", rutaHttpSitio);
            plantillaHtml = plantillaHtml.Replace("[[NombreUsuario]]", string.Format("{0}", notIncon.Nombre_Gerente));
            plantillaHtml = plantillaHtml.Replace("[[USUARIO_SISTEMA]]", notIncon.usuario_sistema);
            plantillaHtml = plantillaHtml.Replace("[[EMPRESA_SISTEMA]]", notIncon.empresa_sistema);
            plantillaHtml = plantillaHtml.Replace("[[OBSERVACION]]", notIncon.Observacion);

            notIncon.Rta = EnvioCorreos.EnviarCorreo(plantillaHtml, correoRemitente, remitente, true, passwordServidorStmp, Convert.ToInt32(puertoServidorStmp), servidorSTMP, "Inconsistencia No. " + notIncon.Id.ToString() +  " de Relaciones Laborales", notIncon.Email_Gerente);

            return notIncon;
        }
    }
}
