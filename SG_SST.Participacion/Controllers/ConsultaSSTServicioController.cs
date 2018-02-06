using SG_SST.EntidadesDominio.Participacion;
using SG_SST.Logica.Participacion;
using SG_SST.Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SG_SST.Participacion.Controllers
{
    public class ConsultaSSTServicioController : ApiController
    {
        [HttpPost]
        [ActionName("Grabar-Consulta")]
        public HttpResponseMessage GrabarConsulta(EDConsultaSST consulta)
        {
            try
            {
                LNConsultaSST logica = new LNConsultaSST();
                var resultado = logica.GrabarConsultaSST(consulta);
                if (resultado != null)
                {
                    var response = Request.CreateResponse<EDConsultaSST>(HttpStatusCode.Created, resultado);
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
        [ActionName("Obtener-ConsultasSST")]
        public HttpResponseMessage ObtenConsultasSST(int idEmpresa)
        {
            try
            {
                var logica = new LNConsultaSST();
                var result = logica.ObtenerConsultasSST(idEmpresa);
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
        [ActionName("Obtener-Una-ConsultaSST")]
        public HttpResponseMessage ObtenerUnConsultaSST(int idConsulta)
        {
            try
            {
                var logica = new LNConsultaSST();
                var result = logica.ObtenerUnaConsultaSST(idConsulta);
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

        //Editar-Gestion-Consulta
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Empresa"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("Editar-Gestion-Consulta")]
        public HttpResponseMessage EditGestionConsulta(EDConsultaTrazabilidad NuevoAdmonCTZB)
        {
            try
            {
                var logica = new LNConsultaSST();
                bool result = logica.EditarGestionConsulta(NuevoAdmonCTZB);
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
                var response = Request.CreateResponse(HttpStatusCode.InternalServerError);
                return response;
            }
        }
        
        [HttpDelete]
        [ActionName("Eliminar-Evidencia-Consulta")]
        public HttpResponseMessage EliminarEvidenciaConsulta(int id, string ruta, int dato)
        {
            try
            {
                var logica = new LNConsultaSST();
                bool result = logica.EliminarEvidenciaConsulta(id, ruta, dato);
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
                
                var response = Request.CreateResponse(HttpStatusCode.InternalServerError);
                return response;
            }
        }

        [HttpPost]
        [ActionName("Consultar-ConsultasSST")]
        public HttpResponseMessage ConsultarConsultasSST(object consultar)
        {
            try
            {
                EDConsultarConsultasSST data = Newtonsoft.Json.JsonConvert.DeserializeObject<EDConsultarConsultasSST>(consultar.ToString());

                var logica = new LNConsultaSST();
                var result = logica.ConsultarConsultasSST(data);
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
        [ActionName("Descargar-ConsultaSST")]
        public HttpResponseMessage ObtenerReporteExcel(object consultar)
        {
            HttpResponseMessage response = null;
            try
            {
                EDConsultarConsultasSST data = Newtonsoft.Json.JsonConvert.DeserializeObject<EDConsultarConsultasSST>(consultar.ToString());
                var logica = new LNConsultaSST();
                var archivo = logica.DescargarConsultaSST(data);
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
        [ActionName("Descargar-ConsultaSST-SinFiltro")]
        public HttpResponseMessage ObtenerReporteExcel(int idEmpresa)
        {
            HttpResponseMessage response = null;
            try
            {
                var logica = new LNConsultaSST();
                var archivo = logica.DescargarConsultaSSTSinFiltro(idEmpresa);
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
    }


}
