using SG_SST.Logica.ComunicadosAPP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SG_SST.PlanTrabajoAnual.Servicios.Controllers
{
    public class ComunicadosAPPController : ApiController
    {
        [HttpGet]
        [ActionName("Listar-Comunicados-Por-Usuario")]
        public HttpResponseMessage ListarComunicadosPorUsuario(string IdentificacionUsuario)
        {
            try
            {
                var logica = new LNUsuariosComunicadoAPP();
                var result = logica.ListarComunicadosPorUsuario(IdentificacionUsuario);
                if (result != null)
                {
                    var response = Request.CreateResponse(HttpStatusCode.OK, result);
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
                var response = Request.CreateResponse(HttpStatusCode.InternalServerError,ex.ToString());
                return response;
            }
        }

        [HttpGet]
        [ActionName("Listar-Comunicados-Usuario-Por-Estado")]
        public HttpResponseMessage ListarComunicadosUsuarioPorEstado(string IdentificacionUsuario, int Estado)
        {
            try
            {
                var logica = new LNUsuariosComunicadoAPP();
                var result = logica.ListarComunicadosUsuarioPorEstado(IdentificacionUsuario, Estado);
                if (result != null)
                {
                    var response = Request.CreateResponse(HttpStatusCode.OK, result);
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
                var response = Request.CreateResponse(HttpStatusCode.InternalServerError,ex.ToString());
                return response;
            }
        }

        [HttpGet]
        [ActionName("Actualizar-Usuario-Comunicado-APP")]
        public HttpResponseMessage UpdateUsuarioComunicadoAPP(int PK_Id_Mensaje, int Estado)
        {
            try
            {
                var logica = new LNUsuariosComunicadoAPP();
                bool result = logica.UpdateUsuarioComunicadoAPP(PK_Id_Mensaje, Estado);
                if (result)
                {
                    var response = Request.CreateResponse(HttpStatusCode.OK, result);
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
                var response = Request.CreateResponse(HttpStatusCode.InternalServerError,ex.ToString());
                return response;
            }
        }

        [HttpGet]
        [ActionName("Actualizar-PlayerID-Comunicado-APP")]
        public HttpResponseMessage UpdateUsuarioComunicadoAPP(string IdentificacionUsuario, string PlayerID)
        {
            try
            {
                var logica = new LNUsuariosComunicadoAPP();
                bool result = logica.UpdateUsuarioPlayerIDComunicadoAPP(IdentificacionUsuario, PlayerID);
                if (result)
                {
                    var response = Request.CreateResponse(HttpStatusCode.OK, result);
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
                var response = Request.CreateResponse(HttpStatusCode.InternalServerError,ex.ToString());
                return response;
            }
        }
    }
}
