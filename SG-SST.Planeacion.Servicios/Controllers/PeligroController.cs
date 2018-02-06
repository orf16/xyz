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
    public class PeligroController : ApiController
    {
        /// <summary>
        /// guarda la informacion de un peligro
        /// </summary>
        /// <param name="Empresa"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("peligros")] 
        public HttpResponseMessage GuardarPeligros(EDPeligro edpeligro)
        {
            try
            {
                var logica = new LNPeligro();
                var result = logica.GuardarPeligro(edpeligro);
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
        /// obtiene los lugares que se identificaron en la matriz de peligros
        /// </summary>
        /// <param name="idEmpresa"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("Lugares")]
        public HttpResponseMessage ObtenerLugares(int idEmpresa)
        {
            try
            {
                var logica = new LNPeligro();
                var result = logica.ObtenerLugaresPeligros(idEmpresa);
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
