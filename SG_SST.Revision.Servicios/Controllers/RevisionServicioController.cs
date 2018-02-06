using SG_SST.EntidadesDominio.Revision;
using SG_SST.Logica.Revision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SG_SST.Revision.Servicios.Controllers
{
    public class RevisionServicioController : ApiController
    {
        [HttpGet]
        [ActionName("Obtener-Informacion-Sede")]
        public HttpResponseMessage ObtenerInformacionSede(int idsede)
        {
            try
            {
                var logica = new LNRevision();
                var result = logica.ObtenerInformacionSede(idsede);
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
        [ActionName("Obtener-Actas-Revision-Empresa")]
        public HttpResponseMessage ObtenerActasRevisionPorEmpresa(int id)
        {
            try
            {
                var logica = new LNRevision();
                var result = logica.ObtenerActasRevisionPorEmpresa(id);
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
        [ActionName("Obtener-Actas-Revision-Id")]
        public HttpResponseMessage ObtenerActaRevisionPorId(int id_Acta)
        {
            try
            {
                var logica = new LNRevision();
                var result = logica.ObtenerActaRevisionPorId(id_Acta);
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
        [ActionName("Obtener-Participantes-Revision-Acta")]
        public HttpResponseMessage ObtenerParticipantesRevisionPorActa(int id_Acta)
        {
            try
            {
                var logica = new LNRevision();
                var result = logica.ObtenerParticipantesRevisionPorActa(id_Acta);
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
        [ActionName("Guardar-Participante-Revision")]
        public HttpResponseMessage GuardarParticipanteRevision(EDActaRevision acta)
        {
            try
            {
                var logica = new LNRevision();
                var result = logica.GuardarParticipanteRevision(acta);
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
        [ActionName("Guardar-Acta-Revision")]
        public HttpResponseMessage GuardarActaRevision(EDActaRevision acta)
        {
            try
            {
                var logica = new LNRevision();
                var result = logica.GuardarActaRevision(acta);
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
        [ActionName("Eliminar-Participante-Revision")]
        public HttpResponseMessage EliminarParticipanteRevision(string documento, int id_Acta)
        {
            try
            {
                var logica = new LNRevision();
                var result = logica.EliminarParticipanteRevision(documento, id_Acta);
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
