using SG_SST.EntidadesDominio.PlanTrabajoAnual;
using SG_SST.Logica.PlanTrabajoAnual;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace SG_SST.PlanTrabajoAnual.Servicios.Controllers
{
    public class PlanAnualEmpresaController : ApiController
    {

        [HttpPost]
        [ActionName("guardar-plan-empresa")]
        public HttpResponseMessage GuardarPlanEmpresa(EDPlanEmpresa planempresa)
        {

            try
            {
                LNPlanEmpresa logicas = new LNPlanEmpresa();
                var resultado = logicas.GuardarPlanEmpresa(planempresa);
                if (resultado != null)
                {
                    var response = Request.CreateResponse<EDPlanEmpresa>(HttpStatusCode.Created, resultado);

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