using SG_SST.EntidadesDominio.Ausentismo;
using SG_SST.Logica.Ausentismo;
using SG_SST.Logica.Ausentismos;
using SG_SST.Logica.Planificacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SG_SST.Planeacion.Servicios.Controllers
{
    public class AusentismoController : ApiController
    {
        [HttpGet]
        [ActionName("descargar-plantilla")]
        public HttpResponseMessage ObtenerReporteExcel(string Nit)
        {
            HttpResponseMessage response = null;
            try
            {
                LNCargue logica = new LNCargue();
                var archivo = logica.DescargarPlantillaCargue(Nit);
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

        [HttpPost]
        [ActionName("cargar-plantilla")]
        public HttpResponseMessage ObtenerReporteExcel(EDCargueMasivo cargue)
        {
            HttpResponseMessage response = null;
            try
            {
                LNCargue logica = new LNCargue();
                var archivo = logica.CargarPlantillaCargueAusentismo(cargue);
                if (archivo != null)
                {
                    response = Request.CreateResponse<EDCargueMasivo>(HttpStatusCode.Created, archivo);
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
