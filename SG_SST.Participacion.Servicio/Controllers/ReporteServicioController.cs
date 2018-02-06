using SG_SST.EntidadesDominio.Participacion;
using SG_SST.Logica.Participacion;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;

namespace SG_SST.Participacion.Servicio.Controllers
{
    public class ReporteServicioController : ApiController
    {
        [HttpPost]
        [ActionName("Crear-Reporte")]

        public HttpResponseMessage GrabarReporte(EDReporte Reporte)
        {

            try
            {
                var logica = new LNReporte();
                var resultado = logica.GuardarReporte(Reporte);
                if (resultado != null)
                {
                    var response = Request.CreateResponse<EDReporte>(HttpStatusCode.Created, resultado);

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
        [ActionName("visualizar-historico-reporte")]

        public HttpResponseMessage ObtenerReportesPorsede(int idEmpresa)
        {

            try
            {
                var logica = new LNReporte();
                var result = logica.ObtenerReportesPorsede(idEmpresa);
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
