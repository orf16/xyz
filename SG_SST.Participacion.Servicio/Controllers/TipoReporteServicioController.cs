using SG_SST.EntidadesDominio.Participacion;
using SG_SST.Logica.Participacion;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SG_SST.Participacion.Servicio.Controllers
{
    public class TipoReporteServicioController : ApiController
    {
        [HttpGet]
        [ActionName("visualizar-historico-tipo-reporte")]

        public HttpResponseMessage ObtenerTiposDeReporte()
        {

            try
            {
                var logica = new LNTipoReporte();
                var result = logica.ObtenerTiposDeReporte();
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
    }
}
