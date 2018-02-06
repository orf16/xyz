
namespace SG_SST.Planeacion.Servicios.Controllers
{
    using SG_SST.EntidadesDominio.Planificacion;
    using SG_SST.Logica.Planificacion;
    //using SG_SST.Utilidades.Traza;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    public class DxDeCondicionSaludController : ApiController
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Empresa"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("Doc_Dx_Condiciones_Salud")]
        public HttpResponseMessage GuardarDxDocSalud(EDDocDxSalud documento)
        {
            try
            {
                var logica = new LNDxGralCondicionesDeSalud();
                bool result = logica.GuardarDocDXSalud(documento);
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
               // RegistroInformacion.EnviarError<DxDeCondicionSaludController>(ex.Message);
                var response = Request.CreateResponse(HttpStatusCode.InternalServerError);
                return response;
            }
        }



        [HttpGet]
        [ActionName("Documentos_Dx")]
        public HttpResponseMessage ObtenerDocsDx(int idEmpresa)
        {
            try
            {
               var logica = new LNDxGralCondicionesDeSalud();
               var result = logica.ObtenerDocsDXSalud(idEmpresa);
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
                //RegistroInformacion.EnviarError<DxDeCondicionSaludController>(ex.Message);
                var response = Request.CreateResponse(HttpStatusCode.InternalServerError);
                return response;
            }
        }

        [HttpDelete]
        [ActionName("Eliminar_Doc_Dx")]
        public HttpResponseMessage EliminarDocDxSalud(int idDocDx)
        {
            try
            {
                var logica = new LNDxGralCondicionesDeSalud();
                bool result = logica.EliminarDocDxSalud(idDocDx);
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
                //RegistroInformacion.EnviarError<DxDeCondicionSaludController>(ex.Message);
                var response = Request.CreateResponse(HttpStatusCode.InternalServerError);
                return response;
            }
        }

        [HttpGet]
        [ActionName("Documento_Dx")]
        public HttpResponseMessage ObtenerDocDx(int idDocDx)
        {
            try
            {
                var logica = new LNDxGralCondicionesDeSalud();
                var result = logica.ObtenerDocDXSalud(idDocDx);
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
                //RegistroInformacion.EnviarError<DxDeCondicionSaludController>(ex.Message);
                var response = Request.CreateResponse(HttpStatusCode.InternalServerError);
                return response;
            }
        }

        [HttpPost]
        [ActionName("Crear-Diagnostico")]

        public HttpResponseMessage GrabarDiagnostico(EDDxSalud Diagnostico)
        {

            try
            {
                var logica = new LNDxGralCondicionesDeSalud();
                var resultado = logica.GuardarDxSalud(Diagnostico);
                if (resultado != null)
                {
                    var response = Request.CreateResponse<EDDxSalud>(HttpStatusCode.Created, resultado);

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
        [ActionName("visualizar-historico")]

        public HttpResponseMessage ObtenerDiagnosticosPorsedeAnio(int idEmpresa) 
        {

            try
            {
                var logica = new LNDxGralCondicionesDeSalud();
                var result = logica.ObtenerDiagnosticosPorsedeAnio(idEmpresa);
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
        [ActionName("visualizar-historicos-anio")]

        public HttpResponseMessage ObtenerHistoricoDxDeSedePorAnio(int idDxSalud)
        {

            try
            {
                var logica = new LNDxGralCondicionesDeSalud();
                var result = logica.ObtenerHistoricoDxDeSedePorAnio(idDxSalud);
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
        [ActionName("descargar-hisotorico_sede_anio")]
        public HttpResponseMessage ObtenerReporteExcel(int idDxSalud)
        {
            HttpResponseMessage response = null;
            try
            {
                var logica = new LNDxGralCondicionesDeSalud();
                var archivo = logica.DescargarHistoricoSedeAnio(idDxSalud);
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
        [ActionName("descargar-reporte")]
        public HttpResponseMessage ObtenerReporteExcelSedes(int idEmpresa)
        {
            HttpResponseMessage response = null;
            try
            {
                var logica = new LNDxGralCondicionesDeSalud();
                var archivo = logica.ObtenerReporte(idEmpresa);
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

        [HttpDelete]
        [ActionName("Eliminar_Dx")]
        public HttpResponseMessage EliminarDxSalud(int idDx)
        {
            try
            {
                var logica = new LNDxGralCondicionesDeSalud();
                bool result = logica.EliminarDxSalud(idDx);
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
                //RegistroInformacion.EnviarError<DxDeCondicionSaludController>(ex.Message);
                var response = Request.CreateResponse(HttpStatusCode.InternalServerError);
                return response;
            }
        }

        [HttpGet]
        [ActionName("Buscar-historico")]

        public HttpResponseMessage BuscarDiagnosticosPorsedeAnio(int idEmpresa, int strZonaLugar)
        {

            try
            {
                var logica = new LNDxGralCondicionesDeSalud();
                var result = logica.BuscarDiagnosticosPorsedeAnio(idEmpresa, strZonaLugar);
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
        [ActionName("cargar-plantilla-dx")]
        public HttpResponseMessage ObtenerReporteExcel(EDCarguePerfil cargue)
        {
            HttpResponseMessage response = null;
            try
            {
                LNCargueDx  logica = new LNCargueDx();
                var archivo = logica.CargarPlantillaDx(cargue);
                if (archivo != null)
                {
                    response = Request.CreateResponse<EDCarguePerfil>(HttpStatusCode.Created, archivo);
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
