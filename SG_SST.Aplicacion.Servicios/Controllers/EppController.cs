using SG_SST.EntidadesDominio.Aplicacion;
using SG_SST.Logica;
using SG_SST.Aplicacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SG_SST.Logica.Aplicacion;
using SG_SST.Audotoria;

namespace SG_SST.Aplicacion.Servicios.Controllers
{
    public class EppController : ApiController
    {
        [HttpGet]
        [ActionName("EPP_Persona")]
        public HttpResponseMessage ObtenerEPPporPersona(string IdPersona, int idEmpresa)
        {
            try
            {
                var LNEPP = new LNEPP();
                var result = LNEPP.ConsultaMatrizEppPersona(IdPersona, idEmpresa);
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
