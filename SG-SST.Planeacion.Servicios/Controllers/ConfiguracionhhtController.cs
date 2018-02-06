using SG_SST.Logica.Ausentismo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SG_SST.Planeacion.Servicios.Controllers
{
    public class ConfiguracionhhtController : ApiController
    {
        [HttpGet]
        [ActionName("obtener-por-empresa")]
        public HttpResponseMessage ObtenerConfiguraciones(string NitEmpresa, int Ano)
        {
            try
            {
                var logica = new LNConfiguracion();
                var result = logica.ObtenerConfiguraciones(NitEmpresa, Ano);
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
                var response = Request.CreateResponse(HttpStatusCode.InternalServerError);
                return response;
            }
        }

        [HttpGet]
        [ActionName("eliminar-configuracion")]
        public HttpResponseMessage EliminarConfiguracion(string NitEmpresa, int idConfiguracion)
        {
            try
            {
                var logica = new LNConfiguracion();
                var result = logica.EliminarConfiguracion(NitEmpresa, idConfiguracion);

                var response = Request.CreateResponse(HttpStatusCode.OK, result);
                return response;
            }
            catch (Exception ex)
            {
                var response = Request.CreateResponse(HttpStatusCode.InternalServerError);
                return response;
            }
        }
    }
}
