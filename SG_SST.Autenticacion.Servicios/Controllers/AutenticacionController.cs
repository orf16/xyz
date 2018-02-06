using SG_SST.Audotoria;
using SG_SST.EntidadesDominio.Usuario;
using SG_SST.Logica.Usuarios;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SG_SST.Autenticacion.Servicios.Controllers
{

    public class AutenticacionController : ApiController
    {
        RegistraLog registroLog = new RegistraLog();

        [HttpPost]
        [ActionName("autenticar-usuario")]
        public HttpResponseMessage AutenticarUsuario(EDUsuarioSistema objUsuario)
        {
            try
            {
                var lnUsuario = new LNUsuario();
                var result = lnUsuario.AutenticarUsuario(objUsuario);
                if (result != null)
                {
                    var response = Request.CreateResponse(HttpStatusCode.Created, result);
                    return response;
                }
                else
                {
                    var response = Request.CreateResponse(HttpStatusCode.ExpectationFailed);
                    return response;
                }
            }
            catch (Exception ex)
            {
                var response = Request.CreateResponse(HttpStatusCode.InternalServerError);
                registroLog.RegistrarError(typeof(AutenticacionController), string.Format("Error en la accion para Autenticar Usuario. Hora: {0}, Error: {1}", DateTime.Now, ex.StackTrace), ex);

                return response;
            }
        }

        [HttpGet]
        [ActionName("autenticar-prueba")]
        public HttpResponseMessage AutenticarPrueba()
        {
            try
            {
                var lnUsuario = new LNUsuario();
                var result = lnUsuario.ObtenerRolesSistema();
                if (result != null)
                {
                    var response = Request.CreateResponse(HttpStatusCode.Created, result);
                    return response;
                }
                else
                {
                    var response = Request.CreateResponse(HttpStatusCode.ExpectationFailed);
                    return response;
                }
            }
            catch (Exception ex)
            {
                var response = Request.CreateResponse(HttpStatusCode.InternalServerError);
                return response;
            }
        }
    }
}