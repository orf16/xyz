using SG_SST.EntidadesDominio.Empresas;
using SG_SST.Logica.Empresas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SG_SST.IncidenteInvestigacion.Servicios.Controllers
{
    public class IncidenteController : ApiController
    {
        /// <returns></returns>
        [HttpPost]
        [ActionName("guardar-incidente")]
        public HttpResponseMessage GuardarIncidente(EDIncidente Inidente)
        {
            HttpResponseMessage response = null;
            try
            {
                LNIncidente logica = new LNIncidente();
                EDIncidente inicente = logica.GuardarIncidente(Inidente);
                if (inicente != null)
                {
                    response = Request.CreateResponse<EDIncidente>(HttpStatusCode.Created, inicente);
                    return response;
                }
                else
                {
                    response = Request.CreateResponse(HttpStatusCode.ExpectationFailed);
                    return response;
                }
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.InternalServerError);
                return response;
            }
        }

        [HttpPost]
        [ActionName("consultar-incidentes")]
        public HttpResponseMessage ConsultarIncidentes(object Parametros)
        {
            try
            {
                EDIncidente_Modelo_Consulta data = Newtonsoft.Json.JsonConvert.DeserializeObject<EDIncidente_Modelo_Consulta>(Parametros.ToString());
                LNIncidente logica = new LNIncidente();
                var result = logica.ConsultarIncidentes(data);
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
        [ActionName("consultar-incidente-id")]
        public HttpResponseMessage ConsultarIncidentes(int idincidente)
        {
            try
            {
                EDIncidente_Modelo_Consulta data = new EDIncidente_Modelo_Consulta();
                data.IncidenteID = idincidente;
                LNIncidente logica = new LNIncidente();
                var result = logica.ConsultarIncidente(data);
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

        [HttpPost]
        [ActionName("descargar-excel-incidente")]
        public HttpResponseMessage ObtenerReporteEventoExcel(object Parametros)
        {
            HttpResponseMessage response = null;
            try
            {
                EDIncidente_Modelo_Consulta data = Newtonsoft.Json.JsonConvert.DeserializeObject<EDIncidente_Modelo_Consulta>(Parametros.ToString());
                LNIncidente logica = new LNIncidente();
                var archivo = logica.ObtenerReporteIncidentesExcel(data);
                if (archivo != null)
                {
                    response = Request.CreateResponse<byte[]>(HttpStatusCode.OK, archivo);
                    return response;
                }
                else
                {
                    response = Request.CreateResponse(HttpStatusCode.ExpectationFailed);
                    return response;
                }
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.InternalServerError);
                return response;
            }
        }

        [HttpGet]
        [ActionName("Obtener-listas")]
        public HttpResponseMessage ObtenerListasBasicas(string Nit)
        {
            try
            {
                LNIncidente inc = new LNIncidente();
                var resultados = inc.ObtenerListasBasicas(Nit);
                if (resultados != null)
                {
                    var response = Request.CreateResponse(HttpStatusCode.OK, resultados);
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
