using SG_SST.Logica.Planificacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SG_SST.Planeacion.Servicios.Controllers
{
    public class ReportesController : ApiController
    {
        [HttpGet]
        [ActionName("estandares-minimos-excel")]
        public HttpResponseMessage ObtenerReporteExcel(string Nit)
        {
            HttpResponseMessage response = null;
            try
            {
                LNEvaluacionStandMinimos logica = new LNEvaluacionStandMinimos();
                var archivo = logica.GenerarArchivoExcel(Nit);
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
        [ActionName("estandares-plan-accion-excel")]
        public HttpResponseMessage ObtenerReporteActividadesExcel(string Nit)
        {
            HttpResponseMessage response = null;
            try
            {
                LNEvaluacionStandMinimos logica = new LNEvaluacionStandMinimos();
                var archivo = logica.ObtenerActividadesExcel(Nit);
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
        [ActionName("estandares-minimos-respuestas")]
        public HttpResponseMessage ObtenerPorcentajeDeRespuestas(string Nit)
        {
            HttpResponseMessage response = null;
            try
            {
                LNReportesEstandaresMinimos logica = new LNReportesEstandaresMinimos();
                var Ciclos = logica.ObtenerPorcentajeDeRespuestas(Nit);
                if (Ciclos != null)
                {
                    response = Request.CreateResponse(HttpStatusCode.OK, Ciclos);
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
        [ActionName("estandares-minimos-respuestas-excel")]
        public HttpResponseMessage ObtenerExcelPorcentajeDeRespuestas(string Nit)
        {
            HttpResponseMessage response = null;
            try
            {
                LNReportesEstandaresMinimos logica = new LNReportesEstandaresMinimos();
                var Ciclos = logica.ObtenerExcelPorcentajeDeRespuestas(Nit);
                if (Ciclos != null)
                {
                    response = Request.CreateResponse(HttpStatusCode.OK, Ciclos);
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
        [ActionName("estandares-minimos-puntaje")]
        public HttpResponseMessage ObtenerPorcentajeObtenido(string Nit)
        {
            HttpResponseMessage response = null;
            try
            {
                LNReportesEstandaresMinimos logica = new LNReportesEstandaresMinimos();
                var Ciclos = logica.ObtenerPorcentajeObtenido(Nit);
                if (Ciclos != null)
                {
                    response = Request.CreateResponse(HttpStatusCode.OK, Ciclos);
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
        [ActionName("estandares-minimos-puntaje-excel")]
        public HttpResponseMessage ObtenerExcelPorcentajeObtenido(string Nit)
        {
            HttpResponseMessage response = null;
            try
            {
                LNReportesEstandaresMinimos logica = new LNReportesEstandaresMinimos();
                var Ciclos = logica.ObtenerExcelPorcentajeObtenido(Nit);
                if (Ciclos != null)
                {
                    response = Request.CreateResponse(HttpStatusCode.OK, Ciclos);
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
        [ActionName("estandares-minimos-calificacion-final")]
        public HttpResponseMessage ObtenerPorcentajeObtenidoCiclo(string Nit, int IdCiclo)
        {
            HttpResponseMessage response = null;
            try
            {
                LNReportesEstandaresMinimos logica = new LNReportesEstandaresMinimos();
                var Ciclos = logica.ObtenerCalificacionEstandresPorCliclo(Nit, IdCiclo);
                if (Ciclos != null)
                {
                    response = Request.CreateResponse(HttpStatusCode.OK, Ciclos);
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
        [ActionName("estandares-minimos-calificacion-ciclo")]
        public HttpResponseMessage ObtenerExcelPorcentajeObtenidoCiclo(string Nit, int IdCiclo)
        {
            HttpResponseMessage response = null;
            try
            {
                LNReportesEstandaresMinimos logica = new LNReportesEstandaresMinimos();
                var Ciclos = logica.ObtenerExcelCalificacionEstandresPorCliclo(Nit, IdCiclo);
                if (Ciclos != null)
                {
                    response = Request.CreateResponse(HttpStatusCode.OK, Ciclos);
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
