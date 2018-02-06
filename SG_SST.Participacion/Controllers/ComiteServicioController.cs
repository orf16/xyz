using SG_SST.EntidadesDominio.Participacion;
using SG_SST.Logica.Participacion;
using SG_SST.Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SG_SST.Participacion.Controllers
{
    public class ComiteServicioController : ApiController
    {

        [HttpGet]
        [ActionName("Obtener-Informacion-Sede")]
        public HttpResponseMessage ObtenerInformacionSede(int idsede)
        {
            try
            {
                var logica = new LNComite();
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
        [ActionName("Obtener-Informacion-Acta-Copasst")]
        public HttpResponseMessage ObtenerInformacionActaCopasst(int idsede)
        {
            try
            {
                var logica = new LNComite();
                var result = logica.ObtenerInformacionActaCopasst(idsede);
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
        [ActionName("Obtener-Tipo-Principal")]
        public HttpResponseMessage ObtenerTipoPrincipal()
        {
            try
            {
                var logica = new LNComite();
                var result = logica.ObtenerTipoPrincipal();
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
        [ActionName("Obtener-Tipo-Prioridad")]
        public HttpResponseMessage ObtenerTipoPrioridad()
        {
            try
            {
                var logica = new LNComite();
                var result = logica.ObtenerTipoPrioridad();
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
        [ActionName("Guardar-Miembros-Acta-Copasst")]
        public HttpResponseMessage GuardarMiembrosActaCopasst(EDMiembrosCopasst MiembrosCopasst)
        {
            try
            {
                var logica = new LNComite();
                var result = logica.GuardarMiembrosActaCopasst(MiembrosCopasst);
                if (result != null)
                {
                    var response = Request.CreateResponse<EDMiembrosCopasst>(HttpStatusCode.Created, result);
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
        [ActionName("Guardar-Actas-Queja")]
        public HttpResponseMessage GuardarActasQueja(EDActaConvivenciaQuejas acta)
        {
            try
            {
                var logica = new LNComite();
                var result = logica.GuardarActasQueja(acta);
                if (result != null)
                {
                    var response = Request.CreateResponse<EDActaConvivenciaQuejas>(HttpStatusCode.OK, result);
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
        [ActionName("Guardar-Actas-Seguimiento")]
        public HttpResponseMessage GuardarActasSeguimiento(EDSeguimientoActaConvivencia acta)
        {
            try
            {
                var logica = new LNComite();
                var result = logica.GuardarActasSeguimiento(acta);
                if (result != null)
                {
                    var response = Request.CreateResponse<EDSeguimientoActaConvivencia>(HttpStatusCode.OK, result);
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
        [ActionName("Guardar-Participantes-Acta-Copasst")]
        public HttpResponseMessage GuardarParticipantesActaCopasst(EDParticipantes ParticipantesCopasst)
        {
            try
            {
                var logica = new LNComite();
                var result = logica.GuardarParticipantesActaCopasst(ParticipantesCopasst);
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
        [ActionName("Guardar-Temas-Acta-Copasst")]
        public HttpResponseMessage GuardarTemasActaCopasst(EDTemasActasCopasst TemasCopasst)
        {
            try
            {
                var logica = new LNComite();
                var result = logica.GuardarTemasActaCopasst(TemasCopasst);
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
        [ActionName("Actualizar-Temas-Acta-Copasst")]
        public HttpResponseMessage ActualizarTemasActaCopasst(EDTemasActasCopasst TemasCopasst)
        {
            try
            {
                var logica = new LNComite();
                var result = logica.ActualizarTemasActaCopasst(TemasCopasst);
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
        [ActionName("Guardar-Acciones-Acta-Copasst")]
        public HttpResponseMessage GuardarAccionesCopasst(EDAccionesActaCopasst AccionesCopasst)
        {
            try
            {
                var logica = new LNComite();
                var result = logica.GuardarAccionesCopasst(AccionesCopasst);
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
        [ActionName("Obtener-Actas-Copasst-Empresa")]
        public HttpResponseMessage ObtenerActasCopasstPorEmpresa(int id)
        {
            try
            {
                var logica = new LNComite();
                var result = logica.ObtenerActasCopasstPorEmpresa(id);
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
        [ActionName("Obtener-Actas-Copasst-Id")]
        public HttpResponseMessage ObtenerActasCopasstPorId(int Id_Acta)
        {
            try
            {
                var logica = new LNComite();
                var result = logica.ObtenerActasCopasstPorId(Id_Acta);
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
        [ActionName("Obtener-Miembros-Copasst-Acta")]
        public HttpResponseMessage ObtenerMiembrosCopasstPorActa(int id_Acta)
        {
            try
            {
                var logica = new LNComite();
                var result = logica.ObtenerMiembrosCopasstPorActa(id_Acta);
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
        [ActionName("Obtener-Participantes-Copasst-Acta")]
        public HttpResponseMessage ObtenerParticipantesCopasstPorActa(int id_Acta)
        {
            try
            {
                var logica = new LNComite();
                var result = logica.ObtenerParticipantesCopasstPorActa(id_Acta);
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
        [ActionName("Obtener-Temas-Copasst-Acta")]
        public HttpResponseMessage ObtenerTemasCopasstPorActa(int id_Acta)
        {
            try
            {
                var logica = new LNComite();
                var result = logica.ObtenerTemasCopasstPorActa(id_Acta);
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
        [ActionName("Obtener-Acciones-Copasst-Acta")]
        public HttpResponseMessage ObtenerAccionesCopasstPorActa(int id_Acta)
        {
            try
            {
                var logica = new LNComite();
                var result = logica.ObtenerAccionesCopasstPorActa(id_Acta);
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
        [ActionName("Eliminar-Miembro-Acta-Copasst")]
        public HttpResponseMessage EliminarMiembroActaCopasst(int Documento, int Usuario, string NombreUsuario, int PK_Id_Acta)
        {
            try
            {
                var logica = new LNComite();
                var result = logica.EliminarMiembroActaCopasst(Documento, Usuario,NombreUsuario,PK_Id_Acta);
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
        [ActionName("Eliminar-Participante-Acta-Copasst")]
        public HttpResponseMessage EliminarParticipanteCopasst(int Documento, int Usuario, string NombreUsuario, int PK_Id_Acta)
        {
            try
            {
                var logica = new LNComite();
                var result = logica.EliminarParticipanteCopasst(Documento, Usuario, NombreUsuario, PK_Id_Acta);
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
        [ActionName("Eliminar-Tema-Acta-Copasst")]
        public HttpResponseMessage EliminarTemaActaCopasst(int PK_Id_TemaActa, int Usuario, string NombreUsuario, int PK_Id_Acta)
        {
            try
            {
                var logica = new LNComite();
                var result = logica.EliminarTemaActaCopasst(PK_Id_TemaActa, Usuario, NombreUsuario, PK_Id_Acta);
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
        [ActionName("Eliminar-Accion-Acta-Copasst")]
        public HttpResponseMessage EliminarAccionActaCopasst(int Pk_Id_AccionActaCopasst, int Usuario, string NombreUsuario, int PK_Id_Acta)
        {
            try
            {
                var logica = new LNComite();
                var result = logica.EliminarAccionActaCopasst(Pk_Id_AccionActaCopasst, Usuario, NombreUsuario, PK_Id_Acta);
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
        [ActionName("Importar-Acta-Copasst")]
        public HttpResponseMessage ImportarActaCopasst(EDActasCopasst ImportarActaCopasst)
        {
            try
            {
                var logica = new LNComite();
                var result = logica.ImportarActaCopasst(ImportarActaCopasst);
                if (result != null)
                {
                    var response = Request.CreateResponse<EDActasCopasst>(HttpStatusCode.Created, result);
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
        [ActionName("Actualizar-Acta-Copasst")]
        public HttpResponseMessage ActualizarActaCopasst(EDActasCopasst InformacionActaCopasst)
        {
            try
            {
                var logica = new LNComite();
                var result = logica.ActualizarActaCopasst(InformacionActaCopasst);
                if (result != null)
                {
                    var response = Request.CreateResponse<EDActasCopasst>(HttpStatusCode.Created, result);
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

        ///////////////////////////////////////////////////////////////////////////////////////

        [HttpGet]
        [ActionName("Obtener-Informacion-Acta-Convivencia")]
        public HttpResponseMessage ObtenerInformacionActaConvivencia(int idsede)
        {
            try
            {
                var logica = new LNComite();
                var result = logica.ObtenerInformacionActaConvivencia(idsede);
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
        [ActionName("Guardar-Miembros-Acta-Convivencia")]
        public HttpResponseMessage GuardarMiembrosActaConvivencia(EDMiembrosConvivencia MiembrosConvivencia)
        {
            try
            {
                var logica = new LNComite();
                var result = logica.GuardarMiembrosActaConvivencia(MiembrosConvivencia);
                if (result != null)
                {
                    var response = Request.CreateResponse<EDMiembrosConvivencia>(HttpStatusCode.Created, result);
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
        [ActionName("Guardar-Participantes-Acta-Convivencia")]
        public HttpResponseMessage GuardarParticipantesActaConvivencia(EDParticipantesActaConvivencia ParticipantesConvivencia)
        {
            try
            {
                var logica = new LNComite();
                var result = logica.GuardarParticipantesActaConvivencia(ParticipantesConvivencia);
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
        [ActionName("Guardar-Responsables-Queja")]
        public HttpResponseMessage GuardarResponsablesQueja(EDResponsablesQuejas Responsable)
        {
            try
            {
                var logica = new LNComite();
                var result = logica.GuardarResponsablesQueja(Responsable);
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
        [ActionName("Guardar-Temas-Acta-Convivencia")]
        public HttpResponseMessage GuardarTemasActaConvivencia(EDTemasActasConvivencia TemasConvivencia)
        {
            try
            {
                var logica = new LNComite();
                var result = logica.GuardarTemasActaConvivencia(TemasConvivencia);
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
        [ActionName("Actualizar-Temas-Acta-Convivencia")]
        public HttpResponseMessage ActualizarTemasActaConvivencia(EDTemasActasConvivencia TemasConvivencia)
        {
            try
            {
                var logica = new LNComite();
                var result = logica.ActualizarTemasActaConvivencia(TemasConvivencia);
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
        [ActionName("Guardar-Acciones-Acta-Convivencia")]
        public HttpResponseMessage GuardarAccionesConvivencia(EDAccionesActaConvivencia AccionesConvivencia)
        {
            try
            {
                var logica = new LNComite();
                var result = logica.GuardarAccionesConvivencia(AccionesConvivencia);
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
        [ActionName("Guardar-Acciones-Acta-Queja")]
        public HttpResponseMessage GuardarAccionesActaQueja(EDAccionesActaQuejas Acciones)
        {
            try
            {
                var logica = new LNComite();
                var result = logica.GuardarAccionesActaQueja(Acciones);
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
        [ActionName("Guardar-Compromisos-Seguimiento")]
        public HttpResponseMessage GuardarCompromisosSeguimiento(EDCompromisosPendientes Compromiso)
        {
            try
            {
                var logica = new LNComite();
                var result = logica.GuardarCompromisosSeguimiento(Compromiso);
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
        [ActionName("Obtener-Actas-Convivencia-Empresa")]
        public HttpResponseMessage ObtenerActasConvivenciaPorEmpresa(int id)
        {
            try
            {
                var logica = new LNComite();
                var result = logica.ObtenerActasConvivenciaPorEmpresa(id);
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
        [ActionName("Obtener-Actas-Convivencia-Id")]
        public HttpResponseMessage ObtenerActasConvivenciaPorId(int Id_Acta)
        {
            try
            {
                var logica = new LNComite();
                var result = logica.ObtenerActasConvivenciaPorId(Id_Acta);
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
        [ActionName("Obtener-Miembros-Convivencia-Acta")]
        public HttpResponseMessage ObtenerMiembrosConvivenciaPorActa(int id_Acta)
        {
            try
            {
                var logica = new LNComite();
                var result = logica.ObtenerMiembrosConvivenciaPorActa(id_Acta);
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
        [ActionName("Obtener-Participantes-Convivencia-Acta")]
        public HttpResponseMessage ObtenerParticipantesConvivenciaPorActa(int id_Acta)
        {
            try
            {
                var logica = new LNComite();
                var result = logica.ObtenerParticipantesConvivenciaPorActa(id_Acta);
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
        [ActionName("Obtener-Temas-Convivencia-Acta")]
        public HttpResponseMessage ObtenerTemasConvivenciaPorActa(int id_Acta)
        {
            try
            {
                var logica = new LNComite();
                var result = logica.ObtenerTemasConvivenciaPorActa(id_Acta);
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
        [ActionName("Obtener-Acciones-Convivencia-Acta")]
        public HttpResponseMessage ObtenerAccionesConvivenciaPorActa(int id_Acta)
        {
            try
            {
                var logica = new LNComite();
                var result = logica.ObtenerAccionesConvivenciaPorActa(id_Acta);
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
        [ActionName("Obtener-Actas-Convivencia-Queja")]
        public HttpResponseMessage ObtenerActasConvivenciaQueja(int IdSede)
        {
            try
            {
                var logica = new LNComite();
                var result = logica.ObtenerActasConvivenciaQueja(IdSede); 
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
        [ActionName("Obtener-Actas-Convivencia-Seguimiento")]
        public HttpResponseMessage ObtenerActasConvivenciaSeguimiento(int IdSede)
        {
            try
            {
                var logica = new LNComite();
                var result = logica.ObtenerActasConvivenciaSeguimiento(IdSede); 
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
       [ActionName("Obtener-Acciones-Actas-Queja")]
        public HttpResponseMessage ObtenerAccionesActasQueja(int PK_Id_Queja)
        {
            try
            {
                var logica = new LNComite();
                var result = logica.ObtenerAccionesActasQueja(PK_Id_Queja); 
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
       [ActionName("Obtener-Compromisos-Acta-Convivencia")]
       public HttpResponseMessage ObtenerCompromisosActaConvivencia(int PK_Id_Seguimiento)
        {
            try
            {
                var logica = new LNComite();
                var result = logica.ObtenerCompromisosActaConvivencia(PK_Id_Seguimiento); 
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
       [ActionName("Obtener-Responsables-Queja")]
        public HttpResponseMessage ObtenerResponsablesQueja(int PK_Id_Queja)
        {
            try
            {
                var logica = new LNComite();
                var result = logica.ObtenerResponsablesQueja(PK_Id_Queja); 
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
        [ActionName("Eliminar-Miembro-Acta-Convivencia")]
        public HttpResponseMessage EliminarMiembroActaConvivencia(int Documento, int Usuario, string NombreUsuario, int PK_Id_Acta)
        {
            try
            {
                var logica = new LNComite();
                var result = logica.EliminarMiembroActaConvivencia(Documento, Usuario, NombreUsuario, PK_Id_Acta);
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
        [ActionName("Eliminar-Participante-Acta-Convivencia")]
        public HttpResponseMessage EliminarParticipanteConvivencia(int Documento, int Usuario, string NombreUsuario, int PK_Id_Acta)
        {
            try
            {
                var logica = new LNComite();
                var result = logica.EliminarParticipanteConvivencia(Documento, Usuario, NombreUsuario, PK_Id_Acta);
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
       [ActionName("Eliminar-Responsable-Queja")]
        public HttpResponseMessage EliminarResponsableQueja(int Documento, int Usuario, string NombreUsuario, int Fk_Id_Queja, int PK_Id_Acta)
        {
            try
            {
                var logica = new LNComite();
                var result = logica.EliminarResponsableQueja( Documento,  Usuario,  NombreUsuario,  Fk_Id_Queja,  PK_Id_Acta);
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
        [ActionName("Eliminar-Tema-Acta-Convivencia")]
        public HttpResponseMessage EliminarTemaActaConvivencia(int PK_Id_TemaActa, int Usuario, string NombreUsuario, int PK_Id_Acta)
        {
            try
            {
                var logica = new LNComite();
                var result = logica.EliminarTemaActaConvivencia(PK_Id_TemaActa, Usuario, NombreUsuario, PK_Id_Acta);
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
        [ActionName("Eliminar-Accion-Acta-Convivencia")]
        public HttpResponseMessage EliminarAccionActaConvivencia(int Pk_Id_AccionActaConvivencia, int Usuario, string NombreUsuario, int PK_Id_Acta)
        {
            try
            {
                var logica = new LNComite();
                var result = logica.EliminarAccionActaConvivencia(Pk_Id_AccionActaConvivencia, Usuario, NombreUsuario, PK_Id_Acta);
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
        [ActionName("Eliminar-Accion-Acta-Queja")]
        public HttpResponseMessage EliminarAccionActaQueja(int Pk_Id_AccionQueja, int Usuario, string NombreUsuario, int Fk_Id_Queja, int PK_Id_Acta)
        {
            try
            {
                var logica = new LNComite();
                var result = logica.EliminarAccionActaQueja( Pk_Id_AccionQueja,  Usuario,  NombreUsuario,  Fk_Id_Queja,  PK_Id_Acta);
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
        [ActionName("Eliminar-Compromiso-Seguimiento")]
        public HttpResponseMessage EliminarCompromisoSeguimiento(int Pk_Id_Compromiso, int Usuario, string NombreUsuario, int FK_Id_Seguimiento, int PK_Id_Acta)
        {
            try
            {
                var logica = new LNComite();
                var result = logica.EliminarCompromisoSeguimiento( Pk_Id_Compromiso,  Usuario,  NombreUsuario,  FK_Id_Seguimiento,  PK_Id_Acta);
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
        [ActionName("Importar-Acta-Convivencia")]
        public HttpResponseMessage ImportarActaConvivencia(EDActasConvivencia ImportarActaConvivencia)
        {
            try
            {
                var logica = new LNComite();
                var result = logica.ImportarActaConvivencia(ImportarActaConvivencia);
                if (result != null)
                {
                    var response = Request.CreateResponse<EDActasConvivencia>(HttpStatusCode.Created, result);
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
        [ActionName("Actualizar-Acta-Convivencia")]
        public HttpResponseMessage ActualizarActaConvivencia(EDActasConvivencia InformacionActaConvivencia)
        {
            try
            {
                var logica = new LNComite();
                var result = logica.ActualizarActaConvivencia(InformacionActaConvivencia);
                if (result != null)
                {
                    var response = Request.CreateResponse<EDActasConvivencia>(HttpStatusCode.Created, result);
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
       [ActionName("Actualizar-Acta-Convivencia-Queja")]
        public HttpResponseMessage ActualizarActaConvivenciaQueja(EDActaConvivenciaQuejas InformacionActaConvivenciaQueja)
        {
            try
            {
                var logica = new LNComite();
                var result = logica.ActualizarActaConvivenciaQueja(InformacionActaConvivenciaQueja);
                if (result != null)
                {
                    var response = Request.CreateResponse<EDActaConvivenciaQuejas>(HttpStatusCode.Created, result);
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
       [ActionName("Actualizar-Acta-Convivencia-Seguimiento")]
       public HttpResponseMessage ActualizarActaConvivenciaSeguimiento(EDSeguimientoActaConvivencia InformacionActaConvivenciaSeg)
        {
            try
            {
                var logica = new LNComite();
                var result = logica.ActualizarActaConvivenciaSeguimiento(InformacionActaConvivenciaSeg);
                if (result != null)
                {
                    var response = Request.CreateResponse<EDSeguimientoActaConvivencia>(HttpStatusCode.Created, result);
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
