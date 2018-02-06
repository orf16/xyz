using SG_SST.EntidadesDominio.Planificacion;
using SG_SST.Logica.Planificacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SG_SST.Planeacion.Servicios.Controllers
{
    public class ObjetivoSSTController : ApiController
    {
        /// <summary>
        /// Obtiene el listado de objetivo creados para la empresa
        /// </summary>
        /// <param name="IdEmpresa">Id de la empresa</param>
        /// <returns>Lista de objetivos de la empresa</returns>
        [HttpGet]
        [ActionName("obtener-objetivossst")]
        public HttpResponseMessage ObtenerObjetivos(int IdEmpresa)
        {
            try
            {
                var logica = new LNObjetivoSST();
                var result = logica.ObtenerObjetivos(IdEmpresa);
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
        /// Guarda un objetivo para la empresa
        /// </summary>
        /// <param name="objetivo">Objetivo a guardar</param>
        /// <returns>Lista de objetivos de la empresa</returns>
        [HttpPost]
        [ActionName("guardar-objetivosst")]
        public HttpResponseMessage GuardarObjetivo(EDObjetivoSST objetivo)
        {
            try
            {
                var logica = new LNObjetivoSST();
                var result = logica.GuardarObjetivo(objetivo);
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
        /// Eliminar objetivos
        /// </summary>
        /// <param name="objetivo">Listas de objetivos a eliminar</param>
        /// <returns>Lista de objetivos restantes</returns>
        [HttpPost]
        [ActionName("eliminar-objetivosst")]
        public HttpResponseMessage EliminarObjetivo(List<EDObjetivoSST> objetivos)
        {
            try
            {
                var logica = new LNObjetivoSST();
                var result = logica.EliminarObjetivos(objetivos);
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
