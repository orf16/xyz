using SG_SST.EntidadesDominio.EnfermedadLaboral;
using SG_SST.Logica.EnfermedadLaboral;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SG_SST.EnfermedadLaboral.Servicios.Controllers
{
    public class EnfermedadLaboralController : ApiController
    {
        [HttpPost]
        [ActionName("registrar-enfermedad-laboral")]
        public HttpResponseMessage AutenticarUsuario(EDEnfermedadLaboral objEnfermedadLaboral)
        {
            try
            {
                var lnEnfermedadLaboral = new LNEnfermedadLaboral();
                var result = lnEnfermedadLaboral.RegistrarEnfermedadLaboralDiagnosticada(objEnfermedadLaboral);
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
    }
}
