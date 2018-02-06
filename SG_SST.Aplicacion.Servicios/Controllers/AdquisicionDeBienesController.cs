
namespace SG_SST.Aplicacion.Servicios.Controllers
{
    using SG_SST.EntidadesDominio.Aplicacion;
    using SG_SST.Logica.Aplicacion;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    public class AdquisicionDeBienesController : ApiController
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Empresa"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("Manual_Adquisicion_Bienes")]
        public HttpResponseMessage GuardarManual(EDManualAdquisicion documento)
        {
            try
            {
                var logica = new LNAdquisicionBienes();
                bool result = logica.GuardarManualAdquisiones(documento);
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

        [HttpGet]
        [ActionName("Manuales_Adquisiciones")]
        public HttpResponseMessage ObtenerManual(int idEmpresa)
        {
            try
            {
                var logica = new LNAdquisicionBienes();
                var result = logica.ObtenerManualAdquisiones(idEmpresa);
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
        [ActionName("Manual_Adquisicion")]
        public HttpResponseMessage ObtenerManulAdq(int idManualAdq)
        {
            try
            {
                var logica = new LNAdquisicionBienes();
                var result = logica.ObtenerManualAdquisionBienes(idManualAdq);
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
        [ActionName("Eliminar_Manul_Adq")]
        public HttpResponseMessage EliminarManulAdq(int idManualAdq)
        {
            try
            {
                var logica = new LNAdquisicionBienes();
                bool result = logica.EliminarManualAdqBienes(idManualAdq);
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
       


    }
}