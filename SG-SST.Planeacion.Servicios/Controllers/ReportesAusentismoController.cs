using SG_SST.EntidadesDominio.Ausentismo;
using SG_SST.Logica.Ausentismo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SG_SST.Planeacion.Servicios.Controllers
{
    public class ReportesAusentismoController : ApiController
    {
        [HttpGet]
        [ActionName("anoinicio-empresa")]
        public HttpResponseMessage ObtenerAnoInicioEmpresa(string Nit)
        {
            HttpResponseMessage response = null;
            try
            {
                LNReportes logica = new LNReportes();
                var anio = logica.ObtenerAnoInicioEmpresa(Nit);
                if (anio > 0)
                {
                    response = Request.CreateResponse<int>(HttpStatusCode.OK, anio);
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
        [ActionName("descargar-excel-contigencias")]
        public HttpResponseMessage ObtenerReporteContigenciasExcel(object dataReportes)
        {
            HttpResponseMessage response = null;
            try
            {
                EDReportes data = Newtonsoft.Json.JsonConvert.DeserializeObject<EDReportes>(dataReportes.ToString());
                LNReportes logica = new LNReportes();
                var archivo = logica.ConsultarPorContingenciaExcel(data);
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

        [HttpPost]
        [ActionName("descargar-excel-evento")]
        public HttpResponseMessage ObtenerReporteEventoExcel(object dataReportes)
        {
            HttpResponseMessage response = null;
            try
            {
                EDReportes data = Newtonsoft.Json.JsonConvert.DeserializeObject<EDReportes>(dataReportes.ToString());
                LNReportes logica = new LNReportes();
                var archivo = logica.ConsultarPorEventosExcel(data);
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

        [HttpPost]
        [ActionName("descargar-excel-departamento")]
        public HttpResponseMessage ObtenerReporteDepartamentoExcel(object dataReportes)
        {
            HttpResponseMessage response = null;
            try
            {
                EDReportes data = Newtonsoft.Json.JsonConvert.DeserializeObject<EDReportes>(dataReportes.ToString());
                LNReportes logica = new LNReportes();
                var archivo = logica.ConsultarPorDepartamentoExcel(data);
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

        [HttpPost]
        [ActionName("descargar-excel-enfermedades")]
        public HttpResponseMessage ObtenerReporteEnfermedadesExcel(object dataReportes)
        {
            HttpResponseMessage response = null;
            try
            {
                EDReportes data = Newtonsoft.Json.JsonConvert.DeserializeObject<EDReportes>(dataReportes.ToString());
                LNReportes logica = new LNReportes();
                var archivo = logica.ConsultarPorEnfermedadesExcel(data);
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

        [HttpPost]
        [ActionName("descargar-excel-enfermedadeseven")]
        public HttpResponseMessage ObtenerReporteEnfermedadesEvenExcel(object dataReportes)
        {
            HttpResponseMessage response = null;
            try
            {
                EDReportes data = Newtonsoft.Json.JsonConvert.DeserializeObject<EDReportes>(dataReportes.ToString());
                LNReportes logica = new LNReportes();
                var archivo = logica.ReporteCantiEventPorEnfermedadesExcel(data);
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

        [HttpPost]
        [ActionName("descargar-excel-proceso")]
        public HttpResponseMessage ObtenerReporteProcesosExcel(object dataReportes)
        {
            HttpResponseMessage response = null;
            try
            {
                EDReportes data = Newtonsoft.Json.JsonConvert.DeserializeObject<EDReportes>(dataReportes.ToString());
                LNReportes logica = new LNReportes();
                var archivo = logica.ReporteDiasAusentismoPorProcesoExcel(data);
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

        [HttpPost]
        [ActionName("descargar-excel-sede")]
        public HttpResponseMessage ObtenerReporteSedesExcel(object dataReportes)
        {
            HttpResponseMessage response = null;
            try
            {
                EDReportes data = Newtonsoft.Json.JsonConvert.DeserializeObject<EDReportes>(dataReportes.ToString());
                LNReportes logica = new LNReportes();
                var archivo = logica.ConsultarPorSedeExcel(data);
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

        [HttpPost]
        [ActionName("descargar-excel-promedio-contingencias")]
        public HttpResponseMessage ObtenerReportePromedioContierngenciaExcel(object dataReportes)
        {
            HttpResponseMessage response = null;
            try
            {
                EDReportes data = Newtonsoft.Json.JsonConvert.DeserializeObject<EDReportes>(dataReportes.ToString());
                LNReportes logica = new LNReportes();
                var archivo = logica.ConsultarPorCostoExcel(data);
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

        [HttpPost]
        [ActionName("descargar-excel-eps")]
        public HttpResponseMessage ObtenerReporteEPSExcel(object dataReportes)
        {
            HttpResponseMessage response = null;
            try
            {
                EDReportes data = Newtonsoft.Json.JsonConvert.DeserializeObject<EDReportes>(dataReportes.ToString());
                LNReportes logica = new LNReportes();
                var archivo = logica.ConsultarPorEpsExcel(data);
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

        [HttpPost]
        [ActionName("descargar-excel-sexo")]
        public HttpResponseMessage ObtenerReporteSexoExcel(object dataReportes)
        {
            HttpResponseMessage response = null;
            try
            {
                EDReportes data = Newtonsoft.Json.JsonConvert.DeserializeObject<EDReportes>(dataReportes.ToString());
                LNReportes logica = new LNReportes();
                var archivo = logica.ConsultarPorSexoExcel(data);
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

        [HttpPost]
        [ActionName("descargar-excel-vinculacion")]
        public HttpResponseMessage ObtenerReporteVinculacionExcel(object dataReportes)
        {
            HttpResponseMessage response = null;
            try
            {
                EDReportes data = Newtonsoft.Json.JsonConvert.DeserializeObject<EDReportes>(dataReportes.ToString());
                LNReportes logica = new LNReportes();
                var archivo = logica.ConsultarAusenciasPorVinculacionExcel(data);
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

        [HttpPost]
        [ActionName("descargar-excel-ocupacion")]
        public HttpResponseMessage ObtenerReporteOcupacionExcel(object dataReportes)
        {
            HttpResponseMessage response = null;
            try
            {
                EDReportes data = Newtonsoft.Json.JsonConvert.DeserializeObject<EDReportes>(dataReportes.ToString());
                LNReportes logica = new LNReportes();
                var archivo = logica.ReporteCantidadAusenciasPorOcupacionExcel(data);
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

        [HttpPost]
        [ActionName("descargar-excel-grupo-etarios")]
        public HttpResponseMessage ObtenerReporteGriposEtariosExcel(object dataReportes)
        {
            HttpResponseMessage response = null;
            try
            {
                EDReportes data = Newtonsoft.Json.JsonConvert.DeserializeObject<EDReportes>(dataReportes.ToString());
                LNReportes logica = new LNReportes();
                var archivo = logica.ReporteCantidadAusenGrupEtariosExcel(data);
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
