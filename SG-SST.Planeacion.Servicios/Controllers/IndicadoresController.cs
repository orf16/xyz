using SG_SST.Logica.Ausentismos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SG_SST.Planeacion.Servicios.Controllers
{
    public class IndicadoresController : ApiController
    {
        [HttpGet]
        [ActionName("descargar-excel-indicadores")]
        public HttpResponseMessage ObtenerIndicadoresPorPeriodoExcel(int anio, int valorK, string Nit, int idEmpresaUsuaria, int IdContingencia)
        {
            HttpResponseMessage response = null;
            try
            {
                LNIndicadores logica = new LNIndicadores();
                var archivo = logica.IndicadoresPorPeriodoExcel(anio, valorK, Nit, idEmpresaUsuaria, IdContingencia);
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
        [ActionName("descargar-excel-acumulado")]
        public HttpResponseMessage ObtenerAcumuladoContingExcel(int anio, int valorK, string Nit, int idEmpresaUsuaria, int tipoContingenciaComparar)
        {
            HttpResponseMessage response = null;
            try
            {
                LNIndicadores logica = new LNIndicadores();
                var archivo = logica.ObtenerExcelAcumuladoTotalContingencias(anio, valorK, Nit, idEmpresaUsuaria, tipoContingenciaComparar);
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
        [ActionName("descargar-excel-comparativo")]
        public HttpResponseMessage ObtenerAcumuladoContingExcel(int anio1, int anio2, int valorK, string Nit, int idEmpresaUsuaria, int tipoContingenciaComparar)
        {
            HttpResponseMessage response = null;
            try
            {
                LNIndicadores logica = new LNIndicadores();
                var archivo = logica.ObtenerExcelComparativo(anio1, anio2, valorK, Nit, idEmpresaUsuaria, tipoContingenciaComparar);
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
