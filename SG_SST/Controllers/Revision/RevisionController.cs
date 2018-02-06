using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SG_SST.Controllers.Base;
using SG_SST.Models.Revision;
using SG_SST.ServiceRequest;
using SG_SST.EntidadesDominio.Revision;
using System.Configuration;
using SG_SST.EntidadesDominio.Empresas;
using SG_SST.Models.AdminUsuarios;
using RestSharp;
using System.Net;
using System.IO;
using SG_SST.Services.Politica.Services;
using SG_SST.Services.Politica.IServices;
using System.Drawing;
using SG_SST.Audotoria;
using SG_SST.Models;

namespace SG_SST.Controllers.Revision
{
    public class RevisionController : BaseController
    {
        public static int intvalorvalidacion = 0;
        IpoliticaServicios gs;
        string UrlServicioEmpresas = ConfigurationManager.AppSettings["UrlServicioEmpresas"];
        string UrlServicioRevision = ConfigurationManager.AppSettings["UrlServicioRevision"];

        string ObtenerInformacionSede = ConfigurationManager.AppSettings["ObtenerInformacionSedeRevision"];
        string ObtenerActasRevisionEmpresa = ConfigurationManager.AppSettings["ObtenerActasRevisionEmpresa"];
        string ObtenerActasRevisionId = ConfigurationManager.AppSettings["ObtenerActasRevisionId"];
        string ObtenerParticipantesRevisionActa = ConfigurationManager.AppSettings["ObtenerParticipantesRevisionActa"];
        string GuardarParticipanteRevision = ConfigurationManager.AppSettings["GuardarParticipanteRevision"];
        string GuardarActaRevision = ConfigurationManager.AppSettings["GuardarActaRevision"];
        string EliminarParticipanteRevision = ConfigurationManager.AppSettings["EliminarParticipanteRevision"];
        string ObtenerItemsRevision = ConfigurationManager.AppSettings["ObtenerItemsRevision"];
        string GuardarTemaAgendaRevision = ConfigurationManager.AppSettings["GuardarTemaAgendaRevision"];
        string ObtenerAgendaActaRevision = ConfigurationManager.AppSettings["ObtenerAgendaActaRevision"];
        string EliminarTemaAgendaRevision = ConfigurationManager.AppSettings["EliminarTemaAgendaRevision"];
        string ObtenerTemas = ConfigurationManager.AppSettings["ObtenerTemas"];
        string EliminarActaRevision = ConfigurationManager.AppSettings["EliminarActaRevision"];
        string ValidarExisteFirma = ConfigurationManager.AppSettings["ValidarExisteFirma"];
        string GuardarDesarrolloTemaAgendaRevision = ConfigurationManager.AppSettings["GuardarDesarrolloTemaAgendaRevision"];
        string ObtenerTemaAgendaActaRevision = ConfigurationManager.AppSettings["ObtenerTemaAgendaActaRevision"];
        string GuardarAdjuntoTemaAgendaRevision = ConfigurationManager.AppSettings["GuardarAdjuntoTemaAgendaRevision"];        
        string EliminarAdjuntoTemaAgendaRevision = ConfigurationManager.AppSettings["EliminarAdjuntoTemaAgendaRevision"];
        string ValidarExistePlanAccionCerradoRevision = ConfigurationManager.AppSettings["ValidarExistePlanAccionCerradoRevision"];

        string ObtenerTemasPlan = ConfigurationManager.AppSettings["ObtenerTemasPlan"];
        string GuardaPlanRevision = ConfigurationManager.AppSettings["GuardaPlanRevision"];
        string ObtenerplanesporActa = ConfigurationManager.AppSettings["ObtenerplanesporActa"];
        string DocumentosRevision = ConfigurationManager.AppSettings["DocumentosRevision"];
        string GuardaFirmaGerente = ConfigurationManager.AppSettings["GuardaFirmaGerente"];
        string validarfirmareplegal = ConfigurationManager.AppSettings["validarfirmareplegal"];
        string validarfirmaresponsable = ConfigurationManager.AppSettings["validarfirmaresponsable"];
        string ValidarFirmasRlRs = ConfigurationManager.AppSettings["ValidarFirmasRlRs"];
        string EdicionEstadoFirmaRes = ConfigurationManager.AppSettings["EdicionEstadoFirmaRes"];
        string EdicionEstadoFirmaRep = ConfigurationManager.AppSettings["EdicionEstadoFirmaRep"];
        string EliminarPlanRevision = ConfigurationManager.AppSettings["EliminarPlanRevision"];

        public ActionResult Index(int? IdActa)
        {
            var acta = new RevisionVM();

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            acta.NombreActa = "Acta de Revisión Gerencial del SGSST";
            acta.FechaCreacionActa = DateTime.Now;
            acta.NombreEmpresa = usuarioActual.RazonSocialEmpresa;
            acta.NitEmpresa = usuarioActual.NitEmpresa;
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("NIT", usuarioActual.NitEmpresa);
            var resultSede = ServiceClient.ObtenerArrayJsonRestFul<EDSede>(urlServicioEmpresas, CapacidadObtenerSedesPorNit, RestSharp.Method.GET);
            if (resultSede != null && resultSede.Count() > 0)
            {
                List<SelectListItem> sedes = new List<SelectListItem>();
                SelectListItem sli;
                foreach (var s in resultSede)
                {
                    if (s != null)
                    {
                        sli = new SelectListItem();
                        sli.Value = s.IdSede.ToString();
                        sli.Text = s.NombreSede;
                        sedes.Add(sli);
                    }
                }
                acta.Sedes = sedes;
            }
            acta.PKActaRevision = IdActa == null ? 0 : IdActa.Value;
            acta.FKActa = acta.PKActaRevision;
            return View(acta);
        }

        public ActionResult TemasActaRevision(int IdActa)
        {
            var acta = new RevisionVM();
            List<AgendaRevisionVM> agenda = new List<AgendaRevisionVM>();
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            acta.Temas = new List<ItemRevisionVM>();
            ServiceClient.EliminarParametros();
            var resultItems = ServiceClient.ObtenerArrayJsonRestFul<EDItemRevision>(UrlServicioRevision, ObtenerItemsRevision, RestSharp.Method.GET);
            if (resultItems != null && resultItems.Count() > 0)
            {
                List<SelectListItem> items = new List<SelectListItem>();
                SelectListItem sli;
                foreach (var s in resultItems)
                {
                    if (s != null)
                    {
                        sli = new SelectListItem();
                        sli.Value = s.Tema;
                        sli.Text = s.Tema;
                        items.Add(sli);
                    }
                }
                sli = new SelectListItem();
                sli.Value = "Otro";
                sli.Text = "Otro";
                items.Add(sli);
                acta.Items = items;


                ItemRevisionVM it;
                foreach (var s in resultItems)
                {
                    if (s != null)
                    {
                        it = new ItemRevisionVM();
                        it.PKItemRevision = s.PK_Id_ItemRevision;
                        it.TemaItem = s.Tema;
                        acta.Temas.Add(it);
                    }
                }
            }
            acta.PKActaRevision = IdActa;
            return View(acta);
        }

        public ActionResult ConsultaActasRevision()
        {
            var acta = new RevisionVM();
            List<ActaVM> actas = new List<ActaVM>();
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            acta.Actas = new List<ActaVM>();
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("id", usuarioActual.IdEmpresa);
            var resultItems = ServiceClient.ObtenerArrayJsonRestFul<EDActaRevision>(UrlServicioRevision, ObtenerActasRevisionEmpresa, RestSharp.Method.GET);
            if (resultItems != null && resultItems.Count() > 0)
            {

                ActaVM it;
                foreach (var s in resultItems)
                {
                    if (s != null)
                    {
                        it = new ActaVM();
                        it.PKActa = s.PK_Id_ActaRevision;
                        it.NumActa = s.Num_Acta;
                        it.NombreActa = s.Nombre;
                        it.FechaCreacionActa = s.Fecha_Creacion_Acta;
                        it.IdEmpresa = s.FK_Empresa;
                        it.FKSede = s.FK_Sede;
                        acta.Actas.Add(it);
                    }
                }
            }
            return View(acta);
        }

        public ActionResult BuscarInformacionSede(int IdSede)
        {
            var acta = new RevisionVM();
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe iniciar Sesión para Continuar.";
                return RedirectToAction("Login", "Home");
            }
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("NIT", usuarioActual.NitEmpresa);
            ServiceClient.AdicionarParametro("idsede", IdSede);
            var resultinfoSede = ServiceClient.ObtenerObjetoJsonRestFul<EDSede>(UrlServicioRevision, ObtenerInformacionSede, RestSharp.Method.GET);
            if (resultinfoSede != null)
            {
                return Json(new { Data = resultinfoSede, Mensaje = "OK" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Data = string.Empty, Mensaje = "ERROR" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult BorrarParticipanteRevision(int FK_Id_Acta, int Documento)
        {

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }

            if (ModelState.IsValid)
            {
                ServiceClient.EliminarParametros();
                ServiceClient.AdicionarParametro("Documento", Documento);
                ServiceClient.AdicionarParametro("PK_Id_Acta", FK_Id_Acta);
                var result = ServiceClient.ObtenerArrayJsonRestFul<EDParticipanteRevision>(UrlServicioRevision, EliminarParticipanteRevision, RestSharp.Method.GET);
                if (result != null || result[0] != null)
                {
                    return Json(new { Data = result, Mensaje = "OK" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Data = string.Empty, Mensaje = "ERROR" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { Data = string.Empty, Mensaje = "ERROR" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GuardarActa(RevisionVM acta)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                var actaR = new EDActaRevision()
                {
                    PK_Id_ActaRevision = acta.PKActaRevision,
                    FK_ActaRevision = acta.FKActa,
                    Nombre = acta.NombreActa,
                    Num_Acta = acta.NumActa,
                    Fecha_Creacion_Acta = acta.FechaCreacionActa,
                    Fecha_Inicial_Revision = acta.FechaInicialRevision,
                    Fecha_Final_Revision = acta.FechaFinalRevision,
                    FK_Empresa = usuarioActual.IdEmpresa,
                    NitEmpresa = usuarioActual.NitEmpresa,
                    NombreEmpresa = usuarioActual.RazonSocialEmpresa,
                    FK_Sede = acta.FKSede,
                };
                ServiceClient.EliminarParametros();
                var result = ServiceClient.RealizarPeticionesPostJsonRestFul<EDActaRevision>(UrlServicioRevision, GuardarActaRevision, actaR);
                if (result != null)
                {
                    return Json(new { Data = result }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    return Json(new { Data = string.Empty, Mensaje = "ERROR" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { Data = string.Empty, Mensaje = "ERROR" }, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult GuardarParticipante(RevisionVM acta)
        {
            var consecutivoActa = 0;
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                if (acta.NumActa == 0)
                {
                    ServiceClient.EliminarParametros();
                    ServiceClient.AdicionarParametro("id", usuarioActual.IdEmpresa);
                    var resultActaRevisionEM = ServiceClient.ObtenerArrayJsonRestFul<EDActaRevision>(UrlServicioRevision, ObtenerActasRevisionEmpresa, RestSharp.Method.GET);
                    if (resultActaRevisionEM.Count() == 0 || resultActaRevisionEM[0] == null)
                    {
                        consecutivoActa = 1;
                    }
                    else
                    {
                        consecutivoActa = resultActaRevisionEM.First().Num_Acta + 1;
                    }
                }
                else
                {
                    consecutivoActa = Convert.ToInt32(acta.NumActa);
                }
                var actaR = new EDActaRevision()
                {
                    Nombre = acta.NombreActa,
                    Num_Acta = consecutivoActa,
                    Fecha_Creacion_Acta = acta.FechaCreacionActa,
                    Fecha_Inicial_Revision = acta.FechaInicialRevision,
                    Fecha_Final_Revision = acta.FechaFinalRevision,
                    Elaborada = acta.ElaboradaPor,
                    Firma_Gerente_General = string.Empty,
                    Firma_Representante_SGSST = false,
                    Firma_Responsable_SGSST = false,
                    DocumentoParticipante = acta.DocumentoParticipante,
                    NombreParticipante = acta.NombreParticipante,
                    CargoParticipante = acta.CargoParticipante,
                    FK_Empresa = usuarioActual.IdEmpresa,
                    NitEmpresa = usuarioActual.NitEmpresa,
                    NombreEmpresa = usuarioActual.RazonSocialEmpresa,
                    FK_Sede = acta.FKSede,
                };
                ServiceClient.EliminarParametros();
                var result = ServiceClient.RealizarPeticionesPostJsonRestFul<EDActaRevision>(UrlServicioRevision, GuardarParticipanteRevision, actaR);
                if (result != null)
                {
                    return Json(new { Data = result }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    return Json(new { Data = string.Empty, Mensaje = "ERROR" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { Data = string.Empty, Mensaje = "ERROR" }, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult ObtenerParticipantesActa(string id_Acta)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }

            if (ModelState.IsValid)
            {
                ServiceClient.EliminarParametros();
                ServiceClient.AdicionarParametro("id_Acta", id_Acta);
                var participantesActa = ServiceClient.ObtenerArrayJsonRestFul<EDParticipanteRevision>(UrlServicioRevision, ObtenerParticipantesRevisionActa, RestSharp.Method.GET);
                if (participantesActa != null || participantesActa[0] != null)
                {
                    return Json(new { Data = participantesActa, Mensaje = "OK" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Data = string.Empty, Mensaje = "ERROR" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { Data = string.Empty, Mensaje = "ERROR" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult ConsultarDatosTrabajador(string numeroDocumento)
        {
            try
            {
                EmpresaAfiliadoModel datos = null;
                if (!string.IsNullOrEmpty(numeroDocumento))
                {
                    var cliente = new RestSharp.RestClient(ConfigurationManager.AppSettings["Url"]);
                    var request = new RestRequest(ConfigurationManager.AppSettings["consultaAfiliado"], RestSharp.Method.GET);
                    request.RequestFormat = DataFormat.Xml;
                    request.Parameters.Clear();
                    request.AddParameter("tpDoc", "cc");
                    request.AddParameter("doc", numeroDocumento);
                    request.AddHeader("Content-Type", "application/json");
                    request.AddHeader("Accept", "application/json");

                    //se omite la validación de certificado de SSL
                    ServicePointManager.ServerCertificateValidationCallback = delegate
                    { return true; };
                    IRestResponse<List<EmpresaAfiliadoModel>> response = cliente.Execute<List<EmpresaAfiliadoModel>>(request);
                    var result = response.Content;
                    if (!string.IsNullOrWhiteSpace(result))
                    {
                        var respuesta = Newtonsoft.Json.JsonConvert.DeserializeObject<List<EmpresaAfiliadoModel>>(result);
                        if (respuesta.Count == 0)
                            return Json(new { Data = "No se encontró ningun Trabajador asociado al documento ingresado.", Mensaje = "NOTFOUND" });
                        var afiliado = respuesta.Where(a => a.Estado == "Activo").FirstOrDefault();
                        if (afiliado == null)
                            return Json(new { Data = "No se encontró ningun Trabajador asociado al documento ingresado.", Mensaje = "NOTFOUND" });
                        else
                        {
                            datos = afiliado;
                        }
                    }
                }
                if (datos != null)
                {
                    return Json(new { Data = datos, Mensaje = "OK" });
                }
                else
                    return Json(new { Data = "No se encontró ningun trabajador asociado al documento ingresado", Mensaje = "NOTFOUND" });
            }
            catch (Exception ex)
            {
                return Json(new { Data = "No se logró consultar la información del Trabajador. Intente más tarde.", Mensaje = "ERROR" });
            }
        }

        public ActionResult ObtenerDatosActaRevision(int? PK_Id_Acta)
        {
            var actaRevisionVM = new RevisionVM();

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }

            if (ModelState.IsValid)
            {
                ServiceClient.EliminarParametros();
                ServiceClient.AdicionarParametro("Id_Acta", PK_Id_Acta);
                var resultActaRevisionEM = ServiceClient.ObtenerObjetoJsonRestFul<EDActaRevision>(UrlServicioRevision, ObtenerActasRevisionId, RestSharp.Method.GET);
                if (resultActaRevisionEM != null)
                {
                    actaRevisionVM.PKActaRevision = resultActaRevisionEM.PK_Id_ActaRevision;
                    actaRevisionVM.NumActa = resultActaRevisionEM.Num_Acta;
                    actaRevisionVM.NombreActa = resultActaRevisionEM.Nombre;
                    actaRevisionVM.FechaCreacionActa = resultActaRevisionEM.Fecha_Creacion_Acta;
                    actaRevisionVM.FechaInicialRevision = resultActaRevisionEM.Fecha_Inicial_Revision;
                    actaRevisionVM.FechaFinalRevision = resultActaRevisionEM.Fecha_Final_Revision;
                    actaRevisionVM.FKSede = resultActaRevisionEM.FK_Sede;
                    actaRevisionVM.ElaboradaPor = resultActaRevisionEM.Elaborada;
                    actaRevisionVM.FirmaGerenteGeneral = resultActaRevisionEM.Firma_Gerente_General;
                    actaRevisionVM.FirmaRepresentanteSGSST = resultActaRevisionEM.Firma_Representante_SGSST;
                    actaRevisionVM.FirmaResponsableSGSST = resultActaRevisionEM.Firma_Responsable_SGSST;
                    actaRevisionVM.IdEmpresa = resultActaRevisionEM.FK_Empresa;
                    actaRevisionVM.NombreEmpresa = resultActaRevisionEM.NombreEmpresa;
                    actaRevisionVM.NitEmpresa = resultActaRevisionEM.NitEmpresa;
                }

                ServiceClient.EliminarParametros();
                ServiceClient.AdicionarParametro("id_acta", PK_Id_Acta);
                var participantesActa = ServiceClient.ObtenerArrayJsonRestFul<EDParticipanteRevision>(UrlServicioRevision, ObtenerParticipantesRevisionActa, RestSharp.Method.GET);
                if (participantesActa != null)
                {
                    actaRevisionVM.ParticipantesActa = participantesActa.Select(p => new ParticipanteRevisionVM()
                    {
                        DocumentoParticipante = p.Documento,
                        NombreParticipante = p.Nombre,
                        CargoParticipante = p.Cargo,
                        FKActaRevision = Convert.ToInt32(p.FK_ActaRevision),
                    }).ToList();
                }

                if (actaRevisionVM != null)
                {
                    return Json(new { Data = actaRevisionVM, Mensaje = "OK" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Data = string.Empty, Mensaje = "ERROR" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { Data = string.Empty, Mensaje = "ERROR" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GuardarItem(RevisionVM acta)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                var actaR = new EDActaRevision()
                {
                    PK_Id_ActaRevision = acta.PKActaRevision,
                    Tema = acta.Item,
                    Desarrollo = ""

                };
                ServiceClient.EliminarParametros();
                var result = ServiceClient.RealizarPeticionesPostJsonRestFul<EDActaRevision>(UrlServicioRevision, GuardarTemaAgendaRevision, actaR);
                if (result != null)
                {
                    return Json(new { Data = result }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    return Json(new { Data = string.Empty, Mensaje = "ERROR" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { Data = string.Empty, Mensaje = "ERROR" }, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult ObtenerAgendaPorActa(string id)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }

            if (ModelState.IsValid)
            {
                ServiceClient.EliminarParametros();
                ServiceClient.AdicionarParametro("id", id);
                var agendaActa = ServiceClient.ObtenerArrayJsonRestFul<EDAgendaRevision>(UrlServicioRevision, ObtenerAgendaActaRevision, RestSharp.Method.GET);
                if (agendaActa != null || agendaActa[0] != null)
                {
                    return Json(new { Data = agendaActa, Mensaje = "OK" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Data = string.Empty, Mensaje = "ERROR" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { Data = string.Empty, Mensaje = "ERROR" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult BorrarTemaAgendaRevision(int IdTema, int FK_Id_Acta)
        {

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }

            if (ModelState.IsValid)
            {
                ServiceClient.EliminarParametros();
                ServiceClient.AdicionarParametro("IdTema", IdTema);
                ServiceClient.AdicionarParametro("PK_Id_Acta", FK_Id_Acta);
                var result = ServiceClient.ObtenerArrayJsonRestFul<EDParticipanteRevision>(UrlServicioRevision, EliminarTemaAgendaRevision, RestSharp.Method.GET);
                if (result != null || result[0] != null)
                {
                    return Json(new { Data = result, Mensaje = "OK" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Data = string.Empty, Mensaje = "ERROR" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { Data = string.Empty, Mensaje = "ERROR" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ValidarTemas(string id)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }

            if (ModelState.IsValid)
            {
                List<EDAgendaRevision> agActa = new List<EDAgendaRevision>();
                ServiceClient.EliminarParametros();
                ServiceClient.AdicionarParametro("id", id);
                var agendaActa = ServiceClient.ObtenerArrayJsonRestFul<EDAgendaRevision>(UrlServicioRevision, ObtenerAgendaActaRevision, RestSharp.Method.GET);
                if (agendaActa != null && agendaActa.Length != 0 && agendaActa[0] != default(EDAgendaRevision) && agendaActa[0] != null)
                {
                    agActa = (from act in agendaActa
                              where act.Desarrollo == ""
                              select new EDAgendaRevision
                              {
                                  PK_Id_Agenda = act.PK_Id_Agenda,
                                  Titulo = act.Titulo,
                                  Desarrollo = act.Desarrollo,
                                  FK_ActaRevision = act.FK_ActaRevision
                              }).ToList();
                    if (agActa == null || agActa.Count() == 0)
                    {
                        return Json(new { Data = agendaActa, Mensaje = "OK" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { Data = string.Empty, Mensaje = "ERROR" }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { Data = string.Empty, Mensaje = "ERROR" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { Data = string.Empty, Mensaje = "ERROR" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult CargarActas()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }

            if (ModelState.IsValid)
            {
                var acta = new RevisionVM();
                acta.Actas = new List<ActaVM>();
                ServiceClient.EliminarParametros();
                ServiceClient.AdicionarParametro("id", usuarioActual.IdEmpresa);
                var resultItems = ServiceClient.ObtenerArrayJsonRestFul<EDActaRevision>(UrlServicioRevision, ObtenerActasRevisionEmpresa, RestSharp.Method.GET);
                if (resultItems != null && resultItems.Count() > 0)
                {
                    ActaVM it;
                    foreach (var s in resultItems)
                    {
                        if (s != null)
                        {
                            it = new ActaVM();
                            it.PKActa = s.PK_Id_ActaRevision;
                            it.NumActa = s.Num_Acta;
                            it.NombreActa = s.Nombre;
                            it.FechaCreacionActa = s.Fecha_Creacion_Acta;
                            it.IdEmpresa = s.FK_Empresa;
                            it.FKSede = s.FK_Sede;
                            acta.Actas.Add(it);
                        }
                    }

                    if (acta.Actas != null && acta.Actas.Count() > 0)
                    {
                        return Json(new { Data = acta, Mensaje = "OK" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { Data = string.Empty, Mensaje = "ERROR" }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { Data = string.Empty, Mensaje = "ERROR" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { Data = string.Empty, Mensaje = "ERROR" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult BorrarActaRevision(int FK_Id_Acta)
        {

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }

            if (ModelState.IsValid)
            {
                ServiceClient.EliminarParametros();
                ServiceClient.AdicionarParametro("idActa", FK_Id_Acta);
                var planCerrado = ServiceClient.ObtenerObjetoJsonRestFul<bool>(UrlServicioRevision, ValidarExistePlanAccionCerradoRevision, RestSharp.Method.GET);
                if (!planCerrado)
                {
                    ServiceClient.EliminarParametros();
                    ServiceClient.AdicionarParametro("PK_Id_Acta", FK_Id_Acta);
                    var result = ServiceClient.ObtenerArrayJsonRestFul<EDParticipanteRevision>(UrlServicioRevision, EliminarActaRevision, RestSharp.Method.GET);
                    if (result != null || result[0] != null)
                    {
                        return Json(new { Data = result, Mensaje = "OK" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { Data = string.Empty, Mensaje = "ERROR" }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { Data = string.Empty, Mensaje = "ERRORPLAN" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { Data = string.Empty, Mensaje = "ERROR" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ActaRevision_PDF(int PKActa)
        {
            var revisionVM = new RevisionVM();

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }

            string nombreLogo = "alissta-logo.png";
            var pathLogo = Server.MapPath("~/Images");
            var fileLogo = nombreLogo;
            var fullPathLogo = Path.Combine(pathLogo, fileLogo);
            revisionVM.LogoFullPath = fullPathLogo;
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("Id_Acta", PKActa);
            var resultActaEM = ServiceClient.ObtenerObjetoJsonRestFul<EDActaRevision>(UrlServicioRevision, ObtenerActasRevisionId, RestSharp.Method.GET);
            if (resultActaEM != null)
            {
                revisionVM.PKActaRevision = resultActaEM.PK_Id_ActaRevision;
                revisionVM.NombreActa = resultActaEM.Nombre;
                revisionVM.NumActa = resultActaEM.Num_Acta;
                revisionVM.FechaCreacionActa = resultActaEM.Fecha_Creacion_Acta;
                revisionVM.FechaInicialRevision = resultActaEM.Fecha_Inicial_Revision;
                revisionVM.FechaFinalRevision = resultActaEM.Fecha_Final_Revision;
                revisionVM.IdEmpresa = resultActaEM.FK_Empresa;
                revisionVM.NitEmpresa = resultActaEM.NitEmpresa;
                revisionVM.NombreEmpresa = resultActaEM.NombreEmpresa;
                revisionVM.FKSede = resultActaEM.FK_Sede;
                revisionVM.ElaboradaPor = resultActaEM.Elaborada;
                revisionVM.FirmaGerenteGeneral = resultActaEM.Firma_Gerente_General;
                revisionVM.ExisteFirmaGerenteGeneral = !string.IsNullOrEmpty(revisionVM.FirmaGerenteGeneral);
                if (!string.IsNullOrEmpty(revisionVM.FirmaGerenteGeneral))
                {
                    string nombreFirma = revisionVM.FirmaGerenteGeneral;
                    var path = Server.MapPath(DocumentosRevision);
                    var file = nombreFirma;
                    var fullPath = Path.Combine(path, file);
                    revisionVM.FirmaGerenteGeneral = fullPath;
                }
                revisionVM.FirmaRepresentanteSGSST = resultActaEM.Firma_Representante_SGSST;
                revisionVM.FirmaResponsableSGSST = resultActaEM.Firma_Responsable_SGSST;

            }

            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("NIT", usuarioActual.NitEmpresa);
            ServiceClient.AdicionarParametro("idsede", revisionVM.FKSede);
            var resultinfoSede = ServiceClient.ObtenerObjetoJsonRestFul<EDSede>(UrlServicioRevision, ObtenerInformacionSede, RestSharp.Method.GET);
            if (resultinfoSede != null)
            {
                revisionVM.NombreSede = resultinfoSede.NombreSede;
            }

            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("idEmpresa", revisionVM.IdEmpresa);
            ServiceClient.AdicionarParametro("descripcion", "REPRESENTANTE LEGAL");
            var imgRep = ServiceClient.ObtenerObjetoJsonRestFul<string>(UrlServicioRevision, ValidarExisteFirma, RestSharp.Method.GET);
            if (imgRep != null)
            {
                string nombreFirma = imgRep;
                var path = Server.MapPath("~/Content/Images");
                var file = nombreFirma;
                var fullPath = Path.Combine(path, file);
                revisionVM.FirmaRepresentanteSGSSTFullPath = fullPath;
            }

            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("idEmpresa", revisionVM.IdEmpresa);
            ServiceClient.AdicionarParametro("descripcion", "RESPONSABLE DE SGSST");
            var imgRes = ServiceClient.ObtenerObjetoJsonRestFul<string>(UrlServicioRevision, ValidarExisteFirma, RestSharp.Method.GET);
            if (imgRes != null)
            {
                string nombreFirma = imgRes;
                var path = Server.MapPath("~/Content/Images");
                var file = nombreFirma;
                var fullPath = Path.Combine(path, file);
                revisionVM.FirmaResponsableSGSSTFullPath = fullPath;
            }

            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("id_Acta", PKActa);
            var participantesActa = ServiceClient.ObtenerArrayJsonRestFul<EDParticipanteRevision>(UrlServicioRevision, ObtenerParticipantesRevisionActa, RestSharp.Method.GET);
            if (participantesActa != null)
            {
                revisionVM.ParticipantesActa = participantesActa.Select(p => new ParticipanteRevisionVM()
                {
                    PKParticipanteRevision = p.PK_Id_ParticipanteRevision,
                    DocumentoParticipante = p.Documento,
                    NombreParticipante = p.Nombre,
                    CargoParticipante = p.Cargo,
                    FKActaRevision = p.FK_ActaRevision
                }).ToList();

            }

            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("id", PKActa);
            var agendaActa = ServiceClient.ObtenerArrayJsonRestFul<EDAgendaRevision>(UrlServicioRevision, ObtenerAgendaActaRevision, RestSharp.Method.GET);
            if (agendaActa != null)
            {
                revisionVM.AgendaActa = agendaActa.Select(p => new AgendaRevisionVM()
                {
                    PKIdAgenda = p.PK_Id_Agenda,
                    TituloAgenda = p.Titulo,
                    DesarrolloAgenda = p.Desarrollo,
                    FKActaRevision = p.FK_ActaRevision
                }).ToList();

            }


            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("NIT", usuarioActual.NitEmpresa);
            ServiceClient.AdicionarParametro("idActa", PKActa);
            var accionesActa = ServiceClient.ObtenerArrayJsonRestFul<EDPlanAccionRevision>(UrlServicioRevision, ObtenerplanesporActa, RestSharp.Method.GET);
            if (accionesActa != null)
            {
                revisionVM.PlanesAccionActa = accionesActa.Select(p => new PlanAccionRevisionVM()
                {
                    PKPlanAccion = p.PK_Id_PlanAccion,
                    ActividadPlan = p.Actividad,
                    FechaPlan = p.Fecha,
                    ResponsablePlan = p.Responsable,
                    FKActa = p.FK_Acta
                }).ToList();
            }


            //string SwitchNombreEmpresa = usuarioActual.RazonSocialEmpresa;
            //string SwitchNitEmpresa = usuarioActual.NitEmpresa;
            //string SwitchNombreDocumento = "Acta Revisión por la Dirección";

            //var fullFooter = Url.Action("Footer", "Revision", null, Request.Url.Scheme);
            //var fullHeader = Url.Action("Header", "Revision", new { NombreEmpresa = SwitchNombreEmpresa, NitEmpresa = SwitchNitEmpresa, NombreInforme = SwitchNombreDocumento }, Request.Url.Scheme);

            //var uriFooter = new Uri(Url.Action("Footer", "Revision", null, Request.Url.Scheme));
            //var clean1 = uriFooter.GetComponents(UriComponents.AbsoluteUri & ~UriComponents.Port, UriFormat.UriEscaped);
            //var uriHeader = new Uri(Url.Action("Header", "Revision", new { NombreEmpresa = SwitchNombreEmpresa, NitEmpresa = SwitchNitEmpresa, NombreInforme = SwitchNombreDocumento }, Request.Url.Scheme));
            //var clean2 = uriHeader.GetComponents(UriComponents.AbsoluteUri & ~UriComponents.Port, UriFormat.UriEscaped);
            ////string cusomtSwitches = string.Format("--footer-line --print-media-type --allow {0} --footer-html {0} --header-line --print-media-type --allow {1} --header-html {1} --header-spacing 5", clean1, clean2);

            //string cusomtSwitches = string.Format("--footer-line --print-media-type --allow {0} --footer-html {0} --header-line --print-media-type --allow {1} --header-html {1} --header-spacing 5", fullFooter, fullHeader);

            //return new Rotativa.ViewAsPdf("GetReporte", revisionVM) { FileName = "Acta_Revisión_Dirección_No." + revisionVM.NumActa + ".pdf", CustomSwitches = cusomtSwitches };


            return new Rotativa.ViewAsPdf("GetReporte", revisionVM);                       

        }

        /// <summary>
        /// action result que maneja la vista - q muestra la descripcion de la politica y la firma del rep legal
        /// </summary>
        /// <param name="pdf"></param>
        /// <returns></returns>
        public ActionResult GetReporte()
        {
            int PKActa=0;
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
                return View();
            }

            var revisionVM = new RevisionVM();

            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("Id_Acta", PKActa);
            var resultActaEM = ServiceClient.ObtenerObjetoJsonRestFul<EDActaRevision>(UrlServicioRevision, ObtenerActasRevisionId, RestSharp.Method.GET);
            if (resultActaEM != null)
            {
                revisionVM.PKActaRevision = resultActaEM.PK_Id_ActaRevision;
                revisionVM.NombreActa = resultActaEM.Nombre;
                revisionVM.NumActa = resultActaEM.Num_Acta;
                revisionVM.FechaCreacionActa = resultActaEM.Fecha_Creacion_Acta;
                revisionVM.FechaInicialRevision = resultActaEM.Fecha_Inicial_Revision;
                revisionVM.FechaFinalRevision = resultActaEM.Fecha_Final_Revision;
                revisionVM.IdEmpresa = resultActaEM.FK_Empresa;
                revisionVM.NitEmpresa = resultActaEM.NitEmpresa;
                revisionVM.NombreEmpresa = resultActaEM.NombreEmpresa;
                revisionVM.FKSede = resultActaEM.FK_Sede;
                revisionVM.ElaboradaPor = resultActaEM.Elaborada;
                revisionVM.FirmaGerenteGeneral = resultActaEM.Firma_Gerente_General;
                revisionVM.FirmaRepresentanteSGSST = resultActaEM.Firma_Representante_SGSST;
                revisionVM.FirmaResponsableSGSST = resultActaEM.Firma_Responsable_SGSST;

            }

            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("id_Acta", PKActa);
            var participantesActa = ServiceClient.ObtenerArrayJsonRestFul<EDParticipanteRevision>(UrlServicioRevision, ObtenerParticipantesRevisionActa, RestSharp.Method.GET);
            if (participantesActa != null)
            {
                revisionVM.ParticipantesActa = participantesActa.Select(p => new ParticipanteRevisionVM()
                {
                    PKParticipanteRevision = p.PK_Id_ParticipanteRevision,
                    DocumentoParticipante = p.Documento,
                    NombreParticipante = p.Nombre,
                    CargoParticipante = p.Cargo,
                    FKActaRevision = p.FK_ActaRevision
                }).ToList();

            }

            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("id", PKActa);
            var agendaActa = ServiceClient.ObtenerArrayJsonRestFul<EDAgendaRevision>(UrlServicioRevision, ObtenerAgendaActaRevision, RestSharp.Method.GET);
            if (agendaActa != null)
            {
                revisionVM.AgendaActa = agendaActa.Select(p => new AgendaRevisionVM()
                {
                    PKIdAgenda = p.PK_Id_Agenda,
                    TituloAgenda = p.Titulo,
                    DesarrolloAgenda = p.Desarrollo,
                    FKActaRevision = p.FK_ActaRevision
                }).ToList();

            }
            return View(revisionVM);
        }

        [AllowAnonymous]
        public ActionResult Header(string NombreEmpresa, string NitEmpresa)
        {
            ViewBag.NombreEmpresa = NombreEmpresa;
            ViewBag.NitEmpresa = NitEmpresa;
            return View();
        }
        [AllowAnonymous]
        public ActionResult Footer()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GuardarDesarrolloTema(RevisionVM modelo)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                var temaAg = new EDAgendaRevision()
                {
                    PK_Id_Agenda = modelo.FKItem,
                    FK_ActaRevision = modelo.FKActa,
                    Titulo = modelo.Item,
                    Desarrollo = modelo.DesarrolloItem
                };
                ServiceClient.EliminarParametros();
                var result = ServiceClient.RealizarPeticionesPostJsonRestFul<EDAgendaRevision>(UrlServicioRevision, GuardarDesarrolloTemaAgendaRevision, temaAg);
                if (result != null)
                {
                    return Json(new { Data = result, Mensaje = "OK" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Data = string.Empty, Mensaje = "ERROR" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { Data = string.Empty, Mensaje = "ERROR" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DesarrolloTemaActaRevision(int IdActa, int IdAgenda)
        {
            var tema = new AgendaRevisionVM();
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            tema.FKActaRevision = IdActa;
            tema.PKIdAgenda = IdAgenda;
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("id", IdAgenda);
            var agendaActa = ServiceClient.ObtenerObjetoJsonRestFul<EDAgendaRevision>(UrlServicioRevision, ObtenerTemaAgendaActaRevision, RestSharp.Method.GET);
            if (agendaActa != null)
            {
                tema.TituloAgenda = agendaActa.Titulo;
                tema.DesarrolloAgenda = agendaActa.Desarrollo;
                tema.AdjuntosAgenda = new List<AdjuntoAgendaRevisionVM>();
                foreach (var a in agendaActa.AdjuntosAgendaRevision)
                {
                    var adj = new AdjuntoAgendaRevisionVM();
                    adj.PKAdjuntoAgendaRevision = a.PK_Id_AdjuntoAgendaRevision;
                    adj.FKAgendaRevision = a.FK_AgendaRevision;
                    adj.NombreArchivo = a.Nombre_Archivo;
                    if (!string.IsNullOrEmpty(adj.NombreArchivo))
                    {
                        string nombreAdj = adj.NombreArchivo;
                        var path = Server.MapPath(DocumentosRevision);
                        var file = nombreAdj;
                        var fullPath = Path.Combine(path, file);
                        adj.NombreArchivoFullPath = fullPath;
                    }
                    tema.AdjuntosAgenda.Add(adj);

                }
                 
            }
            return View(tema);
        }

        public ActionResult CargarTemaAgenda(int id)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }

            if (ModelState.IsValid)
            {
                ServiceClient.EliminarParametros();
                ServiceClient.AdicionarParametro("id", id);
                var resultItems = ServiceClient.ObtenerObjetoJsonRestFul<EDAgendaRevision>(UrlServicioRevision, ObtenerTemaAgendaActaRevision, RestSharp.Method.GET);
                if (resultItems != null)
                {
                    return Json(new { Data = resultItems, Mensaje = "OK" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Data = string.Empty, Mensaje = "ERROR" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { Data = string.Empty, Mensaje = "ERROR" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult BorrarAdjuntoTemaActaRevision(int idAdjunto, int idAgenda)
        {
            SG_SSTContext context = new SG_SSTContext();
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }

            if (ModelState.IsValid)
            {
                var DeletedocEvidencia = (from ci in context.Tbl_AdjuntoAgendaRevision
                                          where ci.PK_Id_AdjuntoAgendaRevision == idAdjunto
                                          select ci).FirstOrDefault();
                ServiceClient.EliminarParametros();
                ServiceClient.AdicionarParametro("idAdjunto", idAdjunto);
                ServiceClient.AdicionarParametro("idAgenda", idAgenda);
                var result = ServiceClient.ObtenerArrayJsonRestFul<EDParticipanteRevision>(UrlServicioRevision, EliminarAdjuntoTemaAgendaRevision, RestSharp.Method.GET);
                if (result != null || result[0] != null)
                {
                    var path = Server.MapPath(DocumentosRevision);
                    var file = DeletedocEvidencia.Nombre_Archivo;
                    var fullPath = Path.Combine(path, file);
                    if (System.IO.File.Exists(fullPath))
                    {
                        RegistraLog registraLog = new RegistraLog();
                        try
                        {
                            System.IO.File.Delete(fullPath);
                        }
                        catch (System.IO.IOException e)
                        {
                            registraLog.RegistrarError(typeof(RevisionController), string.Format("Error al eliminar el documento del servidor  {0}: {1}", DateTime.Now, e.StackTrace), e);
                        }
                    }
                    return Json(new { Data = result, Mensaje = "OK" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Data = string.Empty, Mensaje = "ERROR" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { Data = string.Empty, Mensaje = "ERROR" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult AgregarAdjunto(HttpPostedFileBase archivo, int idAgenda)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                if (archivo != null)
                {
                    var adjunto = new EDAdjuntoAgendaRevision()
                    {
                        Nombre_Archivo = idAgenda + archivo.FileName,
                        FK_AgendaRevision = idAgenda
                    };
                    if (Path.GetExtension(archivo.FileName).ToLower() == ".jpg" || Path.GetExtension(archivo.FileName).ToLower() == ".png" || Path.GetExtension(archivo.FileName).ToLower() == ".pdf" || Path.GetExtension(archivo.FileName).ToLower() == ".xls" || Path.GetExtension(archivo.FileName).ToLower() == ".xlsx")
                    {
                        int contadorAdjuntos = 0;
                        ServiceClient.EliminarParametros();
                        ServiceClient.AdicionarParametro("id", idAgenda);
                        var resultItems = ServiceClient.ObtenerObjetoJsonRestFul<EDAgendaRevision>(UrlServicioRevision, ObtenerTemaAgendaActaRevision, RestSharp.Method.GET);
                        if (resultItems != null)
                        {
                            contadorAdjuntos = resultItems.AdjuntosAgendaRevision.Count();
                        }

                        if (contadorAdjuntos < 3)
                        {
                            ServiceClient.EliminarParametros();
                            var resultfirma = ServiceClient.RealizarPeticionesPostJsonRestFul<EDAdjuntoAgendaRevision>(UrlServicioRevision, GuardarAdjuntoTemaAgendaRevision, adjunto);
                            if (resultfirma != null)
                            {
                                var arc = Path.Combine(Server.MapPath(DocumentosRevision), idAgenda + archivo.FileName);
                                archivo.SaveAs(arc);

                                return Json(new { Data = resultfirma, Mensaje = "OK" }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                return Json(new { Data = string.Empty, Mensaje = "ERROR" }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            return Json(new { Data = string.Empty, Mensaje = "ERRORCONTADOR" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        return Json(new { Data = string.Empty, Mensaje = "ERRORTIPO" }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { Data = string.Empty, Mensaje = "ERRORVACIO" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { Data = string.Empty, Mensaje = "ERROR" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ObtenerTemaAgendaPorActa(string id)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }

            if (ModelState.IsValid)
            {
                ServiceClient.EliminarParametros();
                ServiceClient.AdicionarParametro("id", id);
                var agendaActa = ServiceClient.ObtenerObjetoJsonRestFul<EDAgendaRevision>(UrlServicioRevision, ObtenerTemaAgendaActaRevision, RestSharp.Method.GET);
                if (agendaActa != null)
                {
                    return Json(new { Data = agendaActa, Mensaje = "OK" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Data = string.Empty, Mensaje = "ERROR" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { Data = string.Empty, Mensaje = "ERROR" }, JsonRequestBehavior.AllowGet);
            }
        }

        private static Image RedimensionarImagen(Stream stream)
        {
            // Se crea un objeto Image, que contiene las propiedades de la imagen
            Image img = Image.FromStream(stream);
            int newH = 100;
            int newW = 250;
            // Si las dimensiones cambiaron, se modifica la imagen
            Bitmap newImg = new Bitmap(img, newW, newH);
            Graphics g = Graphics.FromImage(newImg);
            g.InterpolationMode =
                System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
            g.DrawImage(img, 0, 0, newImg.Width, newImg.Height);
            return newImg;
        }

        public ActionResult ReportesEstadisticas()
        {

            return RedirectToAction("MetodoControlador", "Controlador");
        }

        public ActionResult PlanAccionActaRevision(int IdActa)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            var temas = new RevisionVM();
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("idacta", IdActa);
            var resulttemas = ServiceClient.ObtenerArrayJsonRestFul<EDAgendaRevision>(UrlServicioRevision, ObtenerTemasPlan, RestSharp.Method.GET);
            if (resulttemas != null && resulttemas.Count() > 0)
            {
                temas.AgendaActa = resulttemas.Select(ci => new AgendaRevisionVM()
                {
                    PKIdAgenda = ci.PK_Id_Agenda,
                    TituloAgenda = ci.Titulo,
                    DesarrolloAgenda = ci.Desarrollo,
                    FKActaRevision = ci.FK_ActaRevision,
                    ConsecutivoActaRevVM = ci.ConsecutivoActaEmpresaED,

                }).ToList();
                foreach (var t in temas.AgendaActa)
                {
                    t.AdjuntosAgenda = new List<AdjuntoAgendaRevisionVM>();
                    var tema = (from tm in resulttemas
                               where tm.PK_Id_Agenda == t.PKIdAgenda
                               select tm).FirstOrDefault();
                    foreach(var adj in tema.AdjuntosAgendaRevision)
                    {
                        var adjunto = new AdjuntoAgendaRevisionVM();
                        adjunto.FKAgendaRevision = adj.FK_AgendaRevision;
                        adjunto.PKAdjuntoAgendaRevision = adj.PK_Id_AdjuntoAgendaRevision;
                        adjunto.NombreArchivo = adj.Nombre_Archivo;
                        if (!string.IsNullOrEmpty(adjunto.NombreArchivo))
                        {
                            string nombreAdj = adjunto.NombreArchivo;
                            var path = Server.MapPath(DocumentosRevision);
                            var file = nombreAdj;
                            var fullPath = Path.Combine(path, file);
                            adjunto.NombreArchivoFullPath = fullPath;
                        }

                        t.AdjuntosAgenda.Add(adjunto);
                    }
                }

                var ms = TempData["shortMessage"];
                if (ms != null)
                {
                    ViewBag.mensaje = ms;
                }
                return View(temas);
            }
            return View(temas);
        }

        /// <summary>
        /// Metodo para Grabar Plan de Accion Revision.
        /// </summary>
        /// <param name="planaccionrevision"></param>
        /// <returns></returns>
        public ActionResult GrabarPlanAccionRevision(PlanAccionRevisionVM planaccionrevision)
        {

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                var plaaccionrevision = new EDPlanAccionRevision()
                {
                    PK_Id_PlanAccion = planaccionrevision.PKPlanAccion,
                    FK_Acta = planaccionrevision.FKActa,
                    Actividad = planaccionrevision.ActividadPlan,
                    Responsable = planaccionrevision.ResponsablePlan,
                    Fecha = planaccionrevision.FechaPlan,
                    Num_Acta = planaccionrevision.NumActa,
                };
                ServiceClient.EliminarParametros();
                var result = ServiceClient.RealizarPeticionesPostJsonRestFul<EDPlanAccionRevision>(UrlServicioRevision, GuardaPlanRevision, plaaccionrevision);
                if (result != null)
                {
                    ServiceClient.EliminarParametros();
                    ServiceClient.AdicionarParametro("NIT", usuarioActual.NitEmpresa);
                    ServiceClient.AdicionarParametro("idActa", result.FK_Acta);
                    var resultplanes = ServiceClient.ObtenerArrayJsonRestFul<EDPlanAccionRevision>(UrlServicioRevision, ObtenerplanesporActa, RestSharp.Method.GET);
                    if (resultplanes != null & resultplanes.Count() > 0)
                    {
                        return Json(new { Data = resultplanes, Mensaje = "OK" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { Data = string.Empty, Mensaje = "ERROR" }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { Data = string.Empty, Mensaje = "ERROR" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { Data = string.Empty, Mensaje = "ERROR" }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Metodo para cargar automaticamente los Planes de accion 
        /// almacenados.
        /// </summary>
        /// <param name="idacta"></param>
        /// <returns></returns>
        public ActionResult CargarPlanes(int idacta)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("NIT", usuarioActual.NitEmpresa);
            ServiceClient.AdicionarParametro("idActa", idacta);
            var resultplanes = ServiceClient.ObtenerArrayJsonRestFul<EDPlanAccionRevision>(UrlServicioRevision, ObtenerplanesporActa, RestSharp.Method.GET);
            if (resultplanes != null & resultplanes.Count() > 0)
            {
                return Json(new { Data = resultplanes, Mensaje = "OK" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Data = string.Empty, Mensaje = "ERROR" }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Metodo para Cargar la Imagen del Gerente Gral.
        /// </summary>
        /// <param name="ImagenGerente"></param>
        /// <returns></returns>
        /// 
        [HttpPost]
        public ActionResult CargarFirmaGerente(HttpPostedFileBase ImagenGerente, RevisionVM revision, int IdActa)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("NIT", usuarioActual.NitEmpresa);
            ServiceClient.AdicionarParametro("idActa", IdActa);
            var resultplanes = ServiceClient.ObtenerArrayJsonRestFul<EDPlanAccionRevision>(UrlServicioRevision, ObtenerplanesporActa, RestSharp.Method.GET);
            if (resultplanes.Count() == 0)
            {
                TempData["shortMessage"] = "Debe Registrar como minimo un plan de accion para generar el acta.";
                return RedirectToAction("PlanAccionActaRevision", new { IdActa = IdActa });

            }
            else
            {
                if (revision.ElaboradaPor == null)
                {
                    TempData["shortMessage"] = "Debe Diligenciar el Nombre de la Persona que genera el Acta.";
                    return RedirectToAction("PlanAccionActaRevision", new { IdActa = IdActa });

                }
                else
                {
                    if (ModelState.IsValid)
                    {

                        if (ImagenGerente != null)
                        {
                            var revisiones = new EDActaRevision()
                            {
                                Firma_Gerente_General = usuarioActual.IdEmpresa + "Firma" + ImagenGerente.FileName,
                                FK_ActaRevision = IdActa,
                                Elaborada = revision.ElaboradaPor,
                            };
                            if (ImagenGerente != null && Path.GetExtension(ImagenGerente.FileName).ToLower() == ".jpg" || Path.GetExtension(ImagenGerente.FileName).ToLower() == ".png")
                            {
                                ServiceClient.EliminarParametros();
                                var resultfirma = ServiceClient.RealizarPeticionesPostJsonRestFul<EDActaRevision>(UrlServicioRevision, GuardaFirmaGerente, revisiones);
                                if (resultfirma != null)
                                {
                                    var img = Path.Combine(Server.MapPath(DocumentosRevision), usuarioActual.IdEmpresa + "Firma" + ImagenGerente.FileName);
                                    Image imgRe = RedimensionarImagen(ImagenGerente.InputStream);
                                    imgRe.Save(img);
                                    //ImagenGerente.SaveAs(img);

                                    TempData["shortMessage"] = "Informacion Almacenada Satisfactoriamente.";
                                    return RedirectToAction("PlanAccionActaRevision", new { IdActa = IdActa });
                                }
                                else
                                {
                                    TempData["shortMessage"] = "Se presento un error, intente mas tarde.";
                                    return RedirectToAction("PlanAccionActaRevision", new { IdActa = IdActa });

                                }
                            }
                            else
                            {
                                TempData["shortMessage"] = "Solo es permitido cargar archivos jpg y png para la firma del gerente general.";
                                return RedirectToAction("PlanAccionActaRevision", new { IdActa = IdActa });

                            }

                        }
                        else
                        {
                            var revisiones = new EDActaRevision()
                            {
                                FK_ActaRevision = IdActa,
                                Elaborada = revision.ElaboradaPor,
                            };
                            ServiceClient.EliminarParametros();
                            var resultfirma = ServiceClient.RealizarPeticionesPostJsonRestFul<EDActaRevision>(UrlServicioRevision, GuardaFirmaGerente, revisiones);
                            if (resultfirma != null)
                            {
                                //var img = Path.Combine(Server.MapPath(DocumentosRevision), ImagenGerente.FileName);
                                //ImagenGerente.SaveAs(img);
                                TempData["shortMessage"] = "Informacion Almacenada Satisfactoriamente.";
                                return RedirectToAction("PlanAccionActaRevision", new { IdActa = IdActa });
                            }
                            else
                            {
                                TempData["shortMessage"] = "Se presento un error, intente mas tarde.";
                                return RedirectToAction("PlanAccionActaRevision", new { IdActa = IdActa });
                            }

                        }

                    }

                }

            }


            return RedirectToAction("PlanAccionActaRevision", new { IdActa = IdActa });



        }

        public ActionResult ValidarFirmas(int idacta)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("NIT", usuarioActual.NitEmpresa);
            ServiceClient.AdicionarParametro("idActa", idacta);
            var resultfirmasRSRL = ServiceClient.ObtenerObjetoJsonRestFul<EDActaRevision>(UrlServicioRevision, ValidarFirmasRlRs, RestSharp.Method.GET);
            if (resultfirmasRSRL != null)
            {
                return Json(new { Data = resultfirmasRSRL, Mensaje = "OK" }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { Data = string.Empty, Mensaje = "ERROR" }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Metodo para validar si existe la firma en Bd del Rep Legal
        /// </summary>
        /// <param name="IdActa"></param>
        /// <returns></returns>
        public ActionResult Validar_ExisteFirmaRepLegal(int IdActa)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            gs = new PoliticaServicios();
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("idempresa", usuarioActual.IdEmpresa);
            var resultfirma = ServiceClient.ObtenerObjetoJsonRestFul<EDUsuario>(UrlServicioRevision, validarfirmareplegal, RestSharp.Method.GET);
            intvalorvalidacion = 0;
            if (resultfirma != null && resultfirma.ImagenFirmausuarioED != null)
            {
                ActaRevision acta = new ActaRevision();
                acta.Fk_Id_Empresa = usuarioActual.IdEmpresa;
                acta.PK_Id_ActaRevision = IdActa;
                acta.Firma_Representante_SGSST = true;
                gs.ObtenerGuardar_Estadofirmas(acta);
                return Json(new { Data = true, mensaje = "¡Firma anexada al documento con éxito!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Data = string.Empty, Mensaje = "No Existe Registrado aun el Representante Dirijase al Modulo Empresa y haga el proceso" }, JsonRequestBehavior.AllowGet);
            }


        }

        /// <summary>
        /// Metodo para validar si existe en BD la firma del Responsable SGSST.
        /// </summary>
        /// <param name="IdActa"></param>
        /// <returns></returns>
        public ActionResult Validar_ExisteFirmaResponsable(int IdActa)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            gs = new PoliticaServicios();
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("idempresa", usuarioActual.IdEmpresa);
            var resultfirmares = ServiceClient.ObtenerObjetoJsonRestFul<EDUsuario>(UrlServicioRevision, validarfirmaresponsable, RestSharp.Method.GET);
            intvalorvalidacion = 0;
            if (resultfirmares != null && resultfirmares.ImagenFirmausuarioED != null)
            {
                ActaRevision acta = new ActaRevision();
                acta.Fk_Id_Empresa = usuarioActual.IdEmpresa;
                acta.PK_Id_ActaRevision = IdActa;
                acta.Firma_Responsable_SGSST = true;
                gs.ObtenerGuardar_EstadofirmasR(acta);
                return Json(new { Data = true, mensaje = "¡Firma anexada al documento con éxito!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Data = string.Empty, Mensaje = "No Existe Registrado aun el Responsable Dirijase al Modulo Empresa y haga el proceso" }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Metodo para cambiar el estado de la Firma del responsable.
        /// </summary>
        /// <param name="idacta"></param>
        /// <returns></returns>
        public ActionResult CambiarEstadoFirmaResponsable(int idacta)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }

            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("idacta", idacta);
            var resultestadofirmaRes = ServiceClient.ObtenerObjetoJsonRestFul<bool>(UrlServicioRevision, EdicionEstadoFirmaRes, Method.GET);
            if (resultestadofirmaRes != null)
            {
                return Json(new { Data = resultestadofirmaRes, Mensaje = "OK" }, JsonRequestBehavior.AllowGet);
            }
            else
            {

                return Json(new { Data = string.Empty, Mensaje = "Error" }, JsonRequestBehavior.AllowGet);
            }

        }

        /// <summary>
        /// Metodo para Cambiar el estado de la Firma del Representante Legal.
        /// </summary>
        /// <param name="idacta"></param>
        /// <returns></returns>
        public ActionResult CambiarEstadoFirmaRepresentante(int idacta)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }

            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("idacta", idacta);
            var resultestadofirmaRep = ServiceClient.ObtenerObjetoJsonRestFul<bool>(UrlServicioRevision, EdicionEstadoFirmaRep, Method.GET);
            if (resultestadofirmaRep != null)
            {
                return Json(new { Data = resultestadofirmaRep, Mensaje = "OK" }, JsonRequestBehavior.AllowGet);
            }
            else
            {

                return Json(new { Data = string.Empty, Mensaje = "Error" }, JsonRequestBehavior.AllowGet);
            }

        }

        /// <summary>
        /// metodo para Descargar los Archivos Adjuntos en el Tema.
        /// </summary>
        /// <param name="NombreArchivo"></param>
        /// <returns></returns>
        public FileResult Download(string NombreArchivo)
        {
            return File(DocumentosRevision + NombreArchivo, System.Net.Mime.MediaTypeNames.Application.Octet, NombreArchivo);
        }

        public ActionResult EliminarPlanAccionRevision(int pkplan, int idacta)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }

            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("pkplan", pkplan);
            var resultestadoEliminar = ServiceClient.PeticionesPostJsonRestFulRespuetaBool(UrlServicioRevision, EliminarPlanRevision, Method.DELETE);
            if (resultestadoEliminar != null)
            {
                ServiceClient.EliminarParametros();
                ServiceClient.AdicionarParametro("idActa", idacta);
                var resultplanes = ServiceClient.ObtenerArrayJsonRestFul<EDPlanAccionRevision>(UrlServicioRevision, ObtenerplanesporActa, RestSharp.Method.GET);
                if (resultplanes.Count() > 0)
                {

                    return Json(new { Data = resultplanes, Mensaje = "OK" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Data = string.Empty, Mensaje = "ERROR" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {

                return Json(new { Data = string.Empty, Mensaje = "Error" }, JsonRequestBehavior.AllowGet);
            }


        }
        ///Fin metodos Robinson.
    }
}