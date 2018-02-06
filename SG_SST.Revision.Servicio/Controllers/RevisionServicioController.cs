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
                    var response = Request.CreateResponse<EDActaRevision>(HttpStatusCode.Created, result);
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
                    var response = Request.CreateResponse<EDActaRevision>(HttpStatusCode.Created, result);
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
        public HttpResponseMessage EliminarParticipanteRevision(string Documento, int PK_Id_Acta)
        {
            try
            {
                var logica = new LNRevision();
                var result = logica.EliminarParticipanteRevision(Documento, PK_Id_Acta);
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
        [ActionName("Obtener-Items-Revision")]
        public HttpResponseMessage ObtenerItemsRevision()
        {
            try
            {
                var logica = new LNRevision();
                var result = logica.ObtenerItemsRevision();
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
        [ActionName("Obtener-Temas")]
        public HttpResponseMessage ObtenerTemas(int idacta)
        {
            try
            {
                var logica = new LNRevision();
                var result = logica.ObtenerTemas(idacta);
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
        [ActionName("Guardar-Tema-Agenda-Revision")]
        public HttpResponseMessage GuardarTemaAgendaRevision(EDActaRevision acta)
        {
            try
            {
                var logica = new LNRevision();
                var result = logica.GuardarTemaAgendaRevision(acta);
                if (result != null)
                {
                    var response = Request.CreateResponse<EDActaRevision>(HttpStatusCode.Created, result);
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
        [ActionName("Obtener-Agenda-Acta-Revision")]
        public HttpResponseMessage ObtenerAgendaPorActaRevision(int id)
        {
            try
            {
                var logica = new LNRevision();
                var result = logica.ObtenerAgendaPorActaRevision(id);
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
        [ActionName("Eliminar-Tema-Agenda-Revision")]
        public HttpResponseMessage EliminarTemaAgendaRevision(int IdTema, int PK_Id_Acta)
        {
            try
            {
                var logica = new LNRevision();
                var result = logica.EliminarTemaAgendaRevision(IdTema, PK_Id_Acta);
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
        [ActionName("Eliminar-Acta-Revision")]
        public HttpResponseMessage EliminarActaRevision(int PK_Id_Acta)
        {
            try
            {
                var logica = new LNRevision();
                var result = logica.EliminarActaRevision(PK_Id_Acta);
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
        [ActionName("Validar-Existe-Firma")]
        public HttpResponseMessage ValidarExisteFirma(int idEmpresa, string descripcion)
        {
            try
            {
                var logica = new LNRevision();
                var result = logica.ValidarExisteFirma(idEmpresa, descripcion);
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
        [ActionName("Guardar-Desarrollo-Tema-Agenda-Revision")]
        public HttpResponseMessage GuardarDesarrolloTemaAgendaRevision(EDAgendaRevision tema)
        {
            try
            {
                var logica = new LNRevision();
                var result = logica.GuardarDesarrolloTemaAgendaRevision(tema);
                if (result != null)
                {
                    var response = Request.CreateResponse<EDAgendaRevision>(HttpStatusCode.Created, result);
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
        [ActionName("Obtener-Tema-Agenda-Acta-Revision")]
        public HttpResponseMessage ObtenerTemaAgendaPorActaRevision(int id)
        {
            try
            {
                var logica = new LNRevision();
                var result = logica.ObtenerTemaAgendaPorActaRevision(id);
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
        [ActionName("Guardar-Adjunto-Tema-Agenda-Revision")]

        public HttpResponseMessage GuardarAdjuntoTemaAgendaRevision(EDAdjuntoAgendaRevision adjunto)
        {
            try
            {
                var logica = new LNRevision();
                var result = logica.GuardarAdjuntoTemaAgendaRevision(adjunto);
                if (result != null)
                {
                    var response = Request.CreateResponse<EDAdjuntoAgendaRevision>(HttpStatusCode.Created, result);
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
        [ActionName("Eliminar-Adjunto-Tema-Agenda-Revision")]
        public HttpResponseMessage EliminarAdjuntoTemaAgendaRevision(int idAdjunto, int idAgenda)
        {
            try
            {
                var logica = new LNRevision();
                var result = logica.EliminarAdjuntoTemaAgendaRevision(idAdjunto, idAgenda);
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
        [ActionName("Validar-Existe-Plan-Accion-Cerrado-Revision")]
        public HttpResponseMessage ValidarExistePlanAccionCerradoRevision(int idActa)
        {
            try
            {
                var logica = new LNRevision();
                var result = logica.ValidarExistePlanAccionCerradoRevision(idActa);
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
        /// metodos Servicio por Robinson.
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        [ActionName("Obtener-Temas-Plan")]
        public HttpResponseMessage ObtenerTemasPlan(int idacta)
        {
            try
            {
                var logica = new LNRevision();
                var result = logica.ObtenerTemasPlan(idacta);
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
        [ActionName("Validar-firma-Responsable")]
        public HttpResponseMessage validarfirmaresponsable(int idempresa)
        {
            try
            {
                var logica = new LNRevision();
                var result = logica.validarfirmaresponsable(idempresa);
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
        [ActionName("Validar-firma-RepLegal")]
        public HttpResponseMessage validarfirmareplegal(int idempresa)
        {
            try
            {
                var logica = new LNRevision();
                var result = logica.validarfirmareplegal(idempresa);
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
        [ActionName("Guarda-Planaccion-Revision")]
        public HttpResponseMessage GuardaPlanaccionRevision(EDPlanAccionRevision planaccionrev)
        {
            try
            {
                var logica = new LNRevision();
                var result = logica.GrabarPlanAccionRevision(planaccionrev);
                if (result != null)
                {
                    var response = Request.CreateResponse<EDPlanAccionRevision>(HttpStatusCode.Created, result);
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
        [ActionName("Obtener-PlanesporActa")]
        public HttpResponseMessage ObtenerplanesporActa(int idActa)
        {
            try
            {
                var logica = new LNRevision();
                var result = logica.ObtenerplanesporActa(idActa);
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
        [ActionName("Guarda-Firma-Gerente")]

        public HttpResponseMessage GuardaFirmaGerente(EDActaRevision actarevision)
        {
            try
            {
                var logica = new LNRevision();
                var result = logica.GuardaFirmaGerente(actarevision);
                if (result != null)
                {
                    var response = Request.CreateResponse<EDActaRevision>(HttpStatusCode.Created, result);
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
        [ActionName("Validar-Firmas-Rl-Rs")]
        public HttpResponseMessage ValidarFirmasRSRL(int idActa)
        {
            try
            {
                var logica = new LNRevision();
                var result = logica.ValidarFirmasRSRL(idActa);
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
        [ActionName("Edicion-EstadoFirma-Res")]
        public HttpResponseMessage CambioEstadoRes(int idacta)
        {
            try
            {
                var logica = new LNRevision();
                var result = logica.CambioEstadoRes(idacta);
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
        [ActionName("Edicion-EstadoFirma-Rep")]
        public HttpResponseMessage CambioEstadoRep(int idacta)
        {
            try
            {
                var logica = new LNRevision();
                var result = logica.CambioEstadoRep(idacta);
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

        [HttpDelete]
        [ActionName("Eliminar-Plan-Revision")]
        public HttpResponseMessage EliminarCondicion(int pkplan)
        {
            try
            {
                var logica = new LNRevision();
                var result = logica.EliminarPlanRevision(pkplan);
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
        ///  fin metodos Servicio por Robinson.
        /// </summary>
        /// <returns></returns>
    }
}
