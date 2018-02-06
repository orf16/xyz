using SG_SST.EntidadesDominio.EstudioPuestoTrabajo;
using SG_SST.Logica.EstudioPuestoTrabajo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace SG_SST.Planeacion.Servicios.Controllers
{
    public class EstudioPuestosTrabajoController : ApiController
    {
        [HttpGet]
        [ActionName("Obtener-Objetivo-Analisis")]
        public HttpResponseMessage ConsultarObjetivoAnalisis()
        {
            try
            {
                var logica = new LNObjetivoAnalisis();
                var result = logica.ObtenerObjetivosAnalisis();
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
        [ActionName("Obtener-Tipo-AnalisisPT")]
        public HttpResponseMessage ConsultarTipoAnalisisPT()
        {
            try
            {
                var logica = new LNTipoAnalisisPT();
                var result = logica.ObtenerTiposAnalisisPT();
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

        /// <summary>
        /// Guarda un estudio de puesto de Trabajo
        /// </summary>
        /// <param name="objetivo">Estudio a guardar</param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("guardar-estudiopt")]
        public HttpResponseMessage GuardarEstudio(EDEstudioPuestoTrabajo estudio)
        {
            try
            {
                var logica = new LNEstudioPuestoTrabajo();
                var result = logica.GuardarEstudio(estudio);
                if (result != null)
                {
                    var response = Request.CreateResponse(HttpStatusCode.Created, result);
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

        /// <summary>
        /// Guarda un seguimiento de un estudio de puesto de Trabajo
        /// </summary>
        /// <param name="objetivo">Estudio a guardar</param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("guardar-seguimientoestudiopt")]
        public HttpResponseMessage GuardarSeguimiento(EDSeguimientoEstudioPuestoTrabajo seguimiento)
        {
            try
            {
                var logica = new LNSeguimientoEstudioPuestoTrabajo();
                var result = logica.GuardarSeguimiento(seguimiento);
                if (result != null)
                {
                    var response = Request.CreateResponse(HttpStatusCode.Created, result);
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

        /// <summary>
        /// Obtiene los seguimientos de un estudio de puesto de trabajo
        /// </summary>
        /// <param name="objetivo">Estudio</param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("mostrar-gridseguimiento")]
        public HttpResponseMessage MostrarGridSeguimiento(EDEstudioPuestoTrabajo estudio)
        {
            try
            {
                var logica = new LNEstudioPuestoTrabajo();
                var result = logica.ConsultarSeguimientoEstudio(estudio.IdEstudioPuestoTrabajo);
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

        /// <summary>
        /// Guarda un archivo de un estudio de puesto de Trabajo
        /// </summary>
        /// <param name="objetivo">Estudio a guardar</param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("guardar-archivoestudiopt")]
        public HttpResponseMessage GuardarArchivo(EDArchivoEstudioPuestoTrabajo archivo)
        {
            try
            {
                var logica = new LNArchivoEstudioPuestoTrabajo();
                var result = logica.GuardarArchivo(archivo);
                if (result != null)
                {
                    var response = Request.CreateResponse(HttpStatusCode.Created, result);
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

        /// <summary>
        /// Obtiene los estudios de puesto de trabajo por numero de identificacion
        /// </summary>
        /// <param name="objetivo">Numero de Identificacion</param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("get-estudioptxnumiden")]
        public HttpResponseMessage ConsultarEstudioXNumIden(EDEstudioPuestoTrabajo estudio)
        {
            try
            {
                var logica = new LNEstudioPuestoTrabajo();
                var result = logica.ConsultarEstudioPTXNumIden(estudio.NumeroIdentificacion);
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
        [ActionName("get-cargospt")]
        public HttpResponseMessage ConsultarCargos(EDEstudioPuestoTrabajo estudio)
        {
            try
            {
                var logica = new LNEstudioPuestoTrabajo();
                var result = logica.BuscarCargo(estudio.Cargo);
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

        /// <summary>
        /// Obtiene los estudios de puesto de trabajo por numero de identificacion
        /// </summary>
        /// <param name="objetivo">Numero de Identificacion</param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("get-estudioptxcargo")]
        public HttpResponseMessage ConsultarEstudioXCargo(EDEstudioPuestoTrabajo estudio)
        {
            try
            {
                var logica = new LNEstudioPuestoTrabajo();
                var result = logica.ConsultarEstudioPTXCargo(estudio.Cargo);
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

        /// <summary>
        /// Obtiene los archivos de un estudio de puesto de trabajo
        /// </summary>
        /// <param name="objetivo">Estudio</param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("mostrar-archivospt")]
        public HttpResponseMessage MostrarArchivosPT(EDEstudioPuestoTrabajo estudio)
        {
            try
            {
                var logica = new LNEstudioPuestoTrabajo();
                var result = logica.ConsultarArchivosEstudio(estudio.IdEstudioPuestoTrabajo);
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