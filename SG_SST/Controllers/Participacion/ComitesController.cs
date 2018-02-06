using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SG_SST.Controllers.Base;
using SG_SST.EntidadesDominio.Empresas;
using SG_SST.EntidadesDominio.Participacion;
using SG_SST.ServiceRequest;
using SG_SST.Models.Aplicacion;
using System.Configuration;
using SG_SST.Models.Participacion;
using SG_SST.Models.AdminUsuarios;
using RestSharp;
using System.Net;
using System.IO;
using SG_SST.Models;
using SG_SST.Dtos.Participacion;
using SG_SST.Models.ReporteIncidente;


namespace SG_SST.Controllers.Participacion
{
    public class ComitesController : BaseController
    {
        private SG_SSTContext db = new SG_SSTContext();
        string UrlServicioEmpresas = ConfigurationManager.AppSettings["UrlServicioEmpresas"];
        string UrlServicioParticipacion = ConfigurationManager.AppSettings["UrlServicioParticipacion"];
        string ObtenerInformacionSede = ConfigurationManager.AppSettings["ObtenerInformacionSede"];
        string ObtenerActasCopasstPorNit = ConfigurationManager.AppSettings["ObtenerInformacionActaCopasst"];
        string CapacidadObtenerSedesPorNit = ConfigurationManager.AppSettings["CapacidadObtenerSedesPorNit"];
        string ObtenerTipoDocumento = ConfigurationManager.AppSettings["ObtenerTipoDocumento"];
        string ObtenerTipoPrincipal = ConfigurationManager.AppSettings["ObtenerTipoPrincipal"];
        string ObtenerTipoPrioridad = ConfigurationManager.AppSettings["ObtenerTipoPrioridad"];
        string ObtenerActasCopasstPorEmpresa = ConfigurationManager.AppSettings["ObtenerActasCopasstPorEmpresa"];
        string ObtenerActasCopasstPorId = ConfigurationManager.AppSettings["ObtenerActasCopasstPorId"];
        string ImportarActaCopasst = ConfigurationManager.AppSettings["ImportarActaCopasst"];
        string CapacidadActualizarActaCopasst = ConfigurationManager.AppSettings["ActualizarActaCopasst"];
        string rutaArchivosActas = ConfigurationManager.AppSettings["rutaArchivosActas"];

        string CapacidadGuardarMiembrosActaCopasst = ConfigurationManager.AppSettings["CapacidadGuardarMiembrosActaCopasst"];
        string ObtenerMiembrosCopasstPorActa = ConfigurationManager.AppSettings["ObtenerMiembrosCopasstPorActa"];
        string EliminarMiembroActaCopasst = ConfigurationManager.AppSettings["EliminarMiembroActaCopasst"];

        string GuardarParticipantesActaCopasst = ConfigurationManager.AppSettings["GuardarParticipantesActaCopasst"];
        string ObtenerParticipantesCopasstPorActa = ConfigurationManager.AppSettings["ObtenerParticipantesCopasstPorActa"];
        string EliminarParticipanteActaCopasst = ConfigurationManager.AppSettings["EliminarParticipanteActaCopasst"];

        string GuardarTemasActaCopasst = ConfigurationManager.AppSettings["GuardarTemasActaCopasst"];
        string ActualizarTemasActaCopasst = ConfigurationManager.AppSettings["ActualizarTemasActaCopasst"];
        string ObtenerTemasCopasstPorActa = ConfigurationManager.AppSettings["ObtenerTemasCopasstPorActa"];
        string EliminarTemaActaCopasst = ConfigurationManager.AppSettings["EliminarTemaActaCopasst"];

        string GuardarAccionesActaCopasst = ConfigurationManager.AppSettings["GuardarAccionesActaCopasst"];
        string ObtenerAccionesCopasstPorActa = ConfigurationManager.AppSettings["ObtenerAccionesCopasstPorActa"];
        string EliminarAccionActaCopasst = ConfigurationManager.AppSettings["EliminarAccionActaCopasst"];

        /// <summary>
       /// Metodo Inicio que retorna a la Vista la Lista de Sedes
       /// </summary>
       /// <returns></returns>
        public ActionResult Index()
        {
            var copasst = new CopasstVM();


            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe iniciar Sesión para Continuar.";
                return RedirectToAction("Login", "Home");
            }
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("NIT", usuarioActual.NitEmpresa);
            var resultSede = ServiceClient.ObtenerArrayJsonRestFul<EDSede>(urlServicioEmpresas, CapacidadObtenerSedesPorNit, RestSharp.Method.GET);
            if (resultSede != null && resultSede.Count() > 0)
            {
                copasst.Sedes = resultSede.Select(c => new SelectListItem()
                {
                    Value = c.IdSede.ToString(),
                    Text = c.NombreSede
                }).ToList();
            }
            return View(copasst);
        }
        public ActionResult HistoricoActaCopasst(int? IdSede)
        {
           var actaCopasst = new CopasstVM();
           var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);

           if (usuarioActual == null)
           {
               ViewBag.mensaje1 = "Debe iniciar Sesión para Continuar.";
               return RedirectToAction("Login", "Home");
           }

           if (IdSede == null)
           {
               var copasst = new CopasstVM();
               ServiceClient.EliminarParametros();
               ServiceClient.AdicionarParametro("NIT", usuarioActual.NitEmpresa);
               var resultSede = ServiceClient.ObtenerArrayJsonRestFul<EDSede>(urlServicioEmpresas, CapacidadObtenerSedesPorNit, RestSharp.Method.GET);
               if (resultSede != null && resultSede.Count() > 0)
               {
                   copasst.Sedes = resultSede.Select(c => new SelectListItem()
                   {
                       Value = c.IdSede.ToString(),
                       Text = c.NombreSede
                   }).ToList();
               }
               return View("Index",copasst);
           }
           Int32 Insede = 0;
           ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("NIT", usuarioActual.NitEmpresa);
            ServiceClient.AdicionarParametro("idsede", IdSede);
            var resultacta = ServiceClient.ObtenerArrayJsonRestFul<EDActasCopasst>(UrlServicioParticipacion, ObtenerActasCopasstPorNit, RestSharp.Method.GET);
            if (resultacta != null && resultacta.Count() > 0)
            {
                actaCopasst.ActasCopasst = resultacta.Select(a => new CopasstVM
                    {
                        PK_Id_Acta = a.PK_Id_Acta,
                        Consecutivo_Acta = a.Consecutivo_Acta,
                        Fecha = a.Fecha,
                        TemaReunion = a.TemaReunion,
                        NombreUsuario = a.NombreUsuario,
                        Fk_Id_Sede = a.Fk_Id_Sede,
                        NombreArchivo = a.NombreArchivo,
                    }).ToList();

                Insede = Convert.ToInt32(IdSede);
                actaCopasst.Fk_Id_Sede = Insede;
                return View(actaCopasst);
            }

            Insede = Convert.ToInt32(IdSede);
            actaCopasst.Fk_Id_Sede = Insede;

            return View(actaCopasst);
        }
        public ActionResult CrearActaCopasst(int? IdSede)
         {
             var actaCopasst = new CopasstVM();
             var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);

             if (usuarioActual == null)
             {
                 ViewBag.mensaje = "Debe iniciar Sesión para Continuar.";
                 return RedirectToAction("Login", "Home");
             }

             if (IdSede == null)
             {
                 var copasst = new CopasstVM();
                 ServiceClient.EliminarParametros();
                 ServiceClient.AdicionarParametro("NIT", usuarioActual.NitEmpresa);
                 var resultSede = ServiceClient.ObtenerArrayJsonRestFul<EDSede>(urlServicioEmpresas, CapacidadObtenerSedesPorNit, RestSharp.Method.GET);
                 if (resultSede != null && resultSede.Count() > 0)
                 {
                     copasst.Sedes = resultSede.Select(c => new SelectListItem()
                     {
                         Value = c.IdSede.ToString(),
                         Text = c.NombreSede
                     }).ToList();
                 }
                 return View("Index", copasst);
             }

             var crearActaCopasst = new CrearActaCopasstVM();

            ServiceClient.EliminarParametros();
            var resultTipoPrincipal = ServiceClient.ObtenerArrayJsonRestFul<EDTipoPrincipalActa>(UrlServicioParticipacion, ObtenerTipoPrincipal, RestSharp.Method.GET);
            if (resultTipoPrincipal != null && resultTipoPrincipal.Count() > 0)
            {
                crearActaCopasst.TiposPrincipales = resultTipoPrincipal.Select(cf => new SelectListItem()
                {
                    Value = cf.Id_TipoPrincipal.ToString(),
                    Text = cf.DescripcionTipoPrincipal,
                }).ToList();
            }
            else
            {
                crearActaCopasst.TiposPrincipales = resultTipoPrincipal.Select(cf => new SelectListItem()
                {
                    Value = cf.Id_TipoPrincipal.ToString(),
                    Text = cf.DescripcionTipoPrincipal,
                }).ToList();

                ViewBag.mensaje = "Se deben Registrar los Tipos de Principales Actas para continuar";

            }
            ServiceClient.EliminarParametros();
            var resultTipoPrioridad = ServiceClient.ObtenerArrayJsonRestFul<EDTipoPrioridadActa>(UrlServicioParticipacion, ObtenerTipoPrioridad, RestSharp.Method.GET);

            if (resultTipoPrioridad != null && resultTipoPrioridad.Count() > 0)
            {
                crearActaCopasst.TiposPrioridadMiembros = resultTipoPrioridad.Select(cf => new SelectListItem()
                {
                    Value = cf.Id_TipoPrioridadMiembro.ToString(),
                    Text = cf.DescripcionTipoMiembro,
                }).ToList();
            }
            else
            {
                crearActaCopasst.TiposPrioridadMiembros = resultTipoPrioridad.Select(p => new SelectListItem()
                {
                    Value = p.Id_TipoPrioridadMiembro.ToString(),
                    Text = p.DescripcionTipoMiembro,
                }).ToList();

                ViewBag.mensaje = "Se deben Registrar los Tipos de Prioridad Miembros Actas para continuar";
                
            }

            crearActaCopasst.Fk_Id_Sede = Convert.ToInt32(IdSede);

             return View(crearActaCopasst);
         }
        public ActionResult BuscarInformacionSede(int IdSede)
        {
            var copasst = new CopasstVM();
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe iniciar Sesión para Continuar.";
                return RedirectToAction("Login", "Home");
            }
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("NIT", usuarioActual.NitEmpresa);
            ServiceClient.AdicionarParametro("idsede", IdSede);
            var resultinfoSede = ServiceClient.ObtenerObjetoJsonRestFul<EDSede>(UrlServicioParticipacion, ObtenerInformacionSede, RestSharp.Method.GET);
            if(resultinfoSede!=null)
            {
                return Json(new { Data = resultinfoSede, Mensaje = "OK" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Data = string.Empty, Mensaje = "ERROR" }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult ComiteConvivencia()
        {
            var convivencia = new CopasstVM();
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe iniciar Sesión para Continuar.";
                return RedirectToAction("Login", "Home");
            }

            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("NIT", usuarioActual.NitEmpresa);
            
            var resultSede = ServiceClient.ObtenerArrayJsonRestFul<EDSede>(urlServicioEmpresas, CapacidadObtenerSedesPorNit, RestSharp.Method.GET);
            if (resultSede != null && resultSede.Count() > 0)
            {
                convivencia.Sedes = resultSede.Select(c => new SelectListItem()
                {
                    Value = c.IdSede.ToString(),
                    Text = c.NombreSede
                }).ToList();
            }
            return View(convivencia);
        }

        [HttpPost]
        public JsonResult ConsultarDatosTrabajador(string numeroDocumento)
        {
            EmpresaAfiliadoModel datos = null;
 
            try
            {
                if (!string.IsNullOrEmpty(numeroDocumento))
                {
                    var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
                    var cliente = new RestSharp.RestClient(ConfigurationManager.AppSettings["Url"]);
                    var request = new RestRequest(consultaAfiliadoEmpresaActivo, RestSharp.Method.GET);
                    request.RequestFormat = DataFormat.Xml;
                    request.Parameters.Clear();
                    request.AddParameter("tpEm", "NI");
                    request.AddParameter("docEm", usuarioActual.NitEmpresa);
                    request.AddParameter("tpAfiliado", "CC");
                    request.AddParameter("docAfiliado", numeroDocumento);
                    request.AddHeader("Content-Type", "application/json");
                    request.AddHeader("Accept", "application/json");

                    //var request = new RestRequest(ConfigurationManager.AppSettings["consultaAfiliadoEmpresaActivo"], RestSharp.Method.GET);
                    //request.RequestFormat = DataFormat.Xml;
                    //request.Parameters.Clear();
                    //request.AddParameter("tpDoc", "cc");
                    //request.AddParameter("doc", numeroDocumento);
                    //request.AddHeader("Content-Type", "application/json");
                    //request.AddHeader("Accept", "application/json");

                    //se omite la validación de certificado de SSL
                    ServicePointManager.ServerCertificateValidationCallback = delegate
                    { return true; };
                    IRestResponse<List<EmpresaAfiliadoModel>> response = cliente.Execute<List<EmpresaAfiliadoModel>>(request);
                    var result = response.Content;
                    if (!string.IsNullOrWhiteSpace(result))
                    {
                        var respuesta = Newtonsoft.Json.JsonConvert.DeserializeObject<List<EmpresaAfiliadoModel>>(result);

                        if (respuesta.Count == 0)
                        {
                            return Json(new { Data = "El documento ingresado, no pertenece a la empresa", Mensaje = "ERROR" });
                        }
                        else
                        {
                            var nitEmpresaU = "";
                            nitEmpresaU = respuesta[0].documentoEmp;
                            if (nitEmpresaU.Equals(usuarioActual.NitEmpresa))
                            {
                                datos = respuesta.First();
                            }
                            else
                            {
                                return Json(new { Data = "El documento ingresado, no pertenece a la empresa", Mensaje = "ERROR" });

                            }
                        }


                     }
                    if (datos != null)
                    {
                        return Json(new { Data = datos, Mensaje = "OK" });
                    }
                    else
                        return Json(new { Data = "No se encontró información, asociada al documento ingresado.", Mensaje = "NOTFOUND" });
                }

                if (numeroDocumento.Equals(""))
                {

                    return Json(new { Data = "Por favor ingrese un documento", Mensaje = "VACIO" }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(new { Data = "No se encontró información, asociada al documento ingresado", Mensaje = "ERROR" });
            }

            return Json(new { Data = datos, Mensaje = "ERROR" });
        }
        
            public ActionResult GuardarMiembro(CrearActaCopasstVM Miembro)
        {
            var consecutivoActaCopasst = 0;
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                if (Miembro.Consecutivo_Acta == 0 || Miembro.Consecutivo_Acta == null)
                {
                    ServiceClient.EliminarParametros();
                    ServiceClient.AdicionarParametro("id", usuarioActual.IdEmpresa);
                    var resultActaCopasstEM = ServiceClient.ObtenerArrayJsonRestFul<EDActasCopasst>(UrlServicioParticipacion, ObtenerActasCopasstPorEmpresa, RestSharp.Method.GET);
                    if (resultActaCopasstEM.Count() == 0)
                    {
                        consecutivoActaCopasst = 1;
                    }
                    else
                    {
                        consecutivoActaCopasst = resultActaCopasstEM.Last().Consecutivo_Acta + 1;
                    }
                }
                else
                {
                    consecutivoActaCopasst = Convert.ToInt32(Miembro.Consecutivo_Acta);
                }
                var actasCopasst = new EDMiembrosCopasst()
                {
                    Numero_Documento = Miembro.Numero_Documento,
                    Nombre = Miembro.Nombre,
                    Fk_Id_TipoPrioridadMiembro = Miembro.Fk_Id_TipoPrioridadMiembro,
                    Fk_Id_TipoPrincipal = Miembro.Fk_Id_TipoPrincipal,
                    TipoRepresentante = Miembro.TipoRepresentante,
                    Consecutivo_Acta = consecutivoActaCopasst,
                    IdEmpresa = usuarioActual.IdEmpresa,
                    UsuarioSistema = usuarioActual.IdUsuario,
                    NombreUsuario = usuarioActual.NombreUsuario,
                    NombreEmpresa = usuarioActual.RazonSocialEmpresa,
                    IdSede = Miembro.Fk_Id_Sede,
               };
                ServiceClient.EliminarParametros();
                var result = ServiceClient.RealizarPeticionesPostJsonRestFul<EDMiembrosCopasst>(UrlServicioParticipacion, CapacidadGuardarMiembrosActaCopasst, actasCopasst);
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

        public ActionResult GuardarParticipante(CrearActaCopasstVM Participante)
        {
                 var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                var participantesCopasst = new EDParticipantes()
                {
                    Numero_Documento = Convert.ToInt32(Participante.Numero_Documento),
                    Nombre = Participante.Nombre,
                    PK_Id_Acta = Participante.PK_Id_Acta,
                    UsuarioSistema = usuarioActual.IdUsuario,
                    NombreUsuario = usuarioActual.NombreUsuario,
                    Consecutivo_Acta = Convert.ToInt32(Participante.Consecutivo_Acta),
                    IdSede = Convert.ToInt32(Participante.Fk_Id_Sede),
                };
                ServiceClient.EliminarParametros();
                var result = ServiceClient.RealizarPeticionesArrayPostJsonRest<EDParticipantes>(UrlServicioParticipacion, GuardarParticipantesActaCopasst, participantesCopasst);
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

        public ActionResult GuardarTema(CrearActaCopasstVM Tema)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                var temasCopasst = new EDTemasActasCopasst()
                {
                    Tema = Tema.TemaOrdenDia,
                    PK_Id_Acta = Tema.PK_Id_Acta,
                    UsuarioSistema = usuarioActual.IdUsuario,
                    NombreUsuario = usuarioActual.NombreUsuario,
                    Consecutivo_Acta = Convert.ToInt32(Tema.Consecutivo_Acta),
                    IdSede = Convert.ToInt32(Tema.Fk_Id_Sede),
                };
                ServiceClient.EliminarParametros();
                var result = ServiceClient.RealizarPeticionesArrayPostJsonRest<EDTemasActasCopasst>(UrlServicioParticipacion, GuardarTemasActaCopasst, temasCopasst);
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

        public ActionResult GuardarAccion(CrearActaCopasstVM Accion)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                var accionCopasst = new EDAccionesActaCopasst()
                {
                    AccionARealizar = Accion.AccionARealizar,
                    Responsable = Accion.Responsable,
                    FechaProbable = Accion.FechaProbable,
                    PK_Id_Acta = Accion.PK_Id_Acta,
                    UsuarioSistema = usuarioActual.IdUsuario,
                    NombreUsuario = usuarioActual.NombreUsuario,
                    Consecutivo_Acta = Convert.ToInt32(Accion.Consecutivo_Acta),
                    IdSede = Convert.ToInt32(Accion.Fk_Id_Sede),
                };
                ServiceClient.EliminarParametros();
                var result = ServiceClient.RealizarPeticionesArrayPostJsonRest<EDAccionesActaCopasst>(UrlServicioParticipacion, GuardarAccionesActaCopasst, accionCopasst);
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

        public ActionResult MiembroActaCopasst(CrearActaCopasstVM Miembro)
        {
            var miembrosActasCopasst = new CrearActaCopasstVM();

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }

            if (ModelState.IsValid)
            {
                ServiceClient.EliminarParametros();
                ServiceClient.AdicionarParametro("id_acta", Miembro.PK_Id_Acta);
                var miembrosActa = ServiceClient.ObtenerArrayJsonRestFul<EDMiembrosCopasst>(UrlServicioParticipacion, ObtenerMiembrosCopasstPorActa, RestSharp.Method.GET);
                if (miembrosActa != null)
                {
                    return Json(new { Data = miembrosActa, Mensaje = "OK" }, JsonRequestBehavior.AllowGet);
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


        public ActionResult DatosActaCopasst (int? PK_Id_Acta, int? IdSede)
        {
            var crearActaCopasstVM = new CrearActaCopasstVM();

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }

            if (PK_Id_Acta == null || PK_Id_Acta == 0)
            {
                var crearActaCopasst = new CrearActaCopasstVM();
                ViewBag.mensaje = "Debe ingresar los miembros para continuar.";
                ServiceClient.EliminarParametros();
                var resultTipoPrincipal = ServiceClient.ObtenerArrayJsonRestFul<EDTipoPrincipalActa>(UrlServicioParticipacion, ObtenerTipoPrincipal, RestSharp.Method.GET);
                if (resultTipoPrincipal != null && resultTipoPrincipal.Count() > 0)
                {
                    crearActaCopasst.TiposPrincipales = resultTipoPrincipal.Select(cf => new SelectListItem()
                    {
                        Value = cf.Id_TipoPrincipal.ToString(),
                        Text = cf.DescripcionTipoPrincipal,
                    }).ToList();
                }
                else
                {
                    crearActaCopasst.TiposPrincipales = resultTipoPrincipal.Select(cf => new SelectListItem()
                    {
                        Value = cf.Id_TipoPrincipal.ToString(),
                        Text = cf.DescripcionTipoPrincipal,
                    }).ToList();

                    ViewBag.mensaje = "Se deben Registrar los Tipos de Principales Actas para continuar";

                }
                ServiceClient.EliminarParametros();
                var resultTipoPrioridad = ServiceClient.ObtenerArrayJsonRestFul<EDTipoPrioridadActa>(UrlServicioParticipacion, ObtenerTipoPrioridad, RestSharp.Method.GET);

                if (resultTipoPrioridad != null && resultTipoPrioridad.Count() > 0)
                {
                    crearActaCopasst.TiposPrioridadMiembros = resultTipoPrioridad.Select(cf => new SelectListItem()
                    {
                        Value = cf.Id_TipoPrioridadMiembro.ToString(),
                        Text = cf.DescripcionTipoMiembro,
                    }).ToList();
                }
                else
                {
                    crearActaCopasst.TiposPrioridadMiembros = resultTipoPrioridad.Select(p => new SelectListItem()
                    {
                        Value = p.Id_TipoPrioridadMiembro.ToString(),
                        Text = p.DescripcionTipoMiembro,
                    }).ToList();

                    ViewBag.mensaje = "Se deben Registrar los Tipos de Prioridad Miembros Actas para continuar";

                }

                crearActaCopasst.Fk_Id_Sede = Convert.ToInt32(IdSede);

                 return View("CrearActaCopasst", crearActaCopasst);
            }

            if (ModelState.IsValid)
            {
                ServiceClient.EliminarParametros();
                ServiceClient.AdicionarParametro("Id_Acta", PK_Id_Acta);
                var resultActaCopasstEM = ServiceClient.ObtenerObjetoJsonRestFul<EDActasCopasst>(UrlServicioParticipacion, ObtenerActasCopasstPorId, RestSharp.Method.GET);
                if (resultActaCopasstEM != null)
                {
                    crearActaCopasstVM.Consecutivo_Acta = resultActaCopasstEM.Consecutivo_Acta;
                    crearActaCopasstVM.Fecha = resultActaCopasstEM.Fecha;
                    crearActaCopasstVM.PK_Id_Acta = resultActaCopasstEM.PK_Id_Acta;
                    crearActaCopasstVM.NombreEmpresa = resultActaCopasstEM.NombreEmpresa;
                    crearActaCopasstVM.Fk_Id_Sede = resultActaCopasstEM.Fk_Id_Sede;
                    crearActaCopasstVM.TemaReunion = resultActaCopasstEM.TemaReunion;
                    crearActaCopasstVM.Conclusiones = resultActaCopasstEM.Conclusiones;

                }
                ServiceClient.EliminarParametros();
                ServiceClient.AdicionarParametro("id_acta", PK_Id_Acta);
                var miembrosActa = ServiceClient.ObtenerArrayJsonRestFul<EDMiembrosCopasst>(UrlServicioParticipacion, ObtenerMiembrosCopasstPorActa, RestSharp.Method.GET);
                if (miembrosActa != null)
                {
                    crearActaCopasstVM.MiembrosActaCopasst = miembrosActa.Select(p => new MiembrosActaCopasstVM()
                    {
                        Numero_Documento=p.Numero_Documento,
                        Nombre=p.Nombre,
                        Fk_Id_TipoPrioridadMiembro=p.Fk_Id_TipoPrioridadMiembro,
                        Des_TipoPrioridadMiembro=p.Des_TipoPrioridadMiembro,
                        Fk_Id_TipoPrincipal=p.Fk_Id_TipoPrincipal,
                        Des_TipoPrincipal=p.Des_TipoPrincipal,
                        TipoRepresentante=p.TipoRepresentante,
                        Fk_Id_Acta = Convert.ToInt32(p.PK_Id_Acta),
                    }).ToList();

                }

                ServiceClient.EliminarParametros();
                ServiceClient.AdicionarParametro("id_acta", PK_Id_Acta);
                var participantesActa = ServiceClient.ObtenerArrayJsonRestFul<EDParticipantes>(UrlServicioParticipacion, ObtenerParticipantesCopasstPorActa, RestSharp.Method.GET);
                if (participantesActa != null)
                {
                    crearActaCopasstVM.ParticipantesActaCopasst = participantesActa.Select(p => new ParticipantesActaCopasstVM()
                    {
                        Numero_Documento = p.Numero_Documento,
                        Nombre = p.Nombre,
                        Fk_Id_Acta = Convert.ToInt32(p.PK_Id_Acta),
                    }).ToList();

                }

                ServiceClient.EliminarParametros();
                ServiceClient.AdicionarParametro("id_acta", PK_Id_Acta);
                var temasActa = ServiceClient.ObtenerArrayJsonRestFul<EDTemasActasCopasst>(UrlServicioParticipacion, ObtenerTemasCopasstPorActa, RestSharp.Method.GET);
                if (temasActa != null)
                {
                    crearActaCopasstVM.TemasActaCopasst = temasActa.Select(p => new TemasActaCopasstVM()
                    {
                        PK_Id_TemaActa = p.PK_Id_TemaActa,
                        Tema = p.Tema,
                        Observaciones = p.Observaciones,
                        Fk_Id_Acta = Convert.ToInt32(p.PK_Id_Acta),
                    }).ToList();

                }

                ServiceClient.EliminarParametros();
                ServiceClient.AdicionarParametro("id_acta", PK_Id_Acta);
                var accionesActa = ServiceClient.ObtenerArrayJsonRestFul<EDAccionesActaCopasst>(UrlServicioParticipacion, ObtenerAccionesCopasstPorActa, RestSharp.Method.GET);
                if (accionesActa != null)
                {
                    crearActaCopasstVM.AccionesActaCopasst = accionesActa.Select(p => new AccionesActaCopasstVM()
                    {
                        Pk_Id_AccionActaCopasst = p.Pk_Id_AccionActaCopasst,
                        AccionARealizar = p.AccionARealizar,
                        FechaProbable = p.FechaProbable,
                        Responsable = p.Responsable,
                        Fk_Id_Acta = Convert.ToInt32(p.PK_Id_Acta),
                    }).ToList();

                }
            
            }
            return View("DatosActaCopasst", crearActaCopasstVM);
        }

        public ActionResult eliminarMiembroCopasst(int FK_Id_Acta, int Documento)
        {
            var miembrosActasCopasst = new CrearActaCopasstVM();

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
                ServiceClient.AdicionarParametro("Usuario", usuarioActual.IdUsuario);
                ServiceClient.AdicionarParametro("NombreUsuario", usuarioActual.NombreUsuario);
                ServiceClient.AdicionarParametro("PK_Id_Acta", FK_Id_Acta);
                var result = ServiceClient.ObtenerArrayJsonRestFul<EDMiembrosCopasst>(UrlServicioParticipacion, EliminarMiembroActaCopasst, RestSharp.Method.GET);
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
        public ActionResult eliminarParticipanteCopasst(int FK_Id_Acta, int Documento)
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
                ServiceClient.AdicionarParametro("Usuario", usuarioActual.IdUsuario);
                ServiceClient.AdicionarParametro("NombreUsuario", usuarioActual.NombreUsuario);
                ServiceClient.AdicionarParametro("PK_Id_Acta", FK_Id_Acta);
                var result = ServiceClient.ObtenerArrayJsonRestFul<EDParticipantesActaConvivencia>(UrlServicioParticipacion, EliminarParticipanteActaCopasst, RestSharp.Method.GET);
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
        
        public ActionResult eliminarTemaCopasst(int FK_Id_Acta, int PK_Id_TemaActa)
        {
            var miembrosActasCopasst = new CrearActaCopasstVM();

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }

            if (ModelState.IsValid)
            {
                ServiceClient.EliminarParametros();
                ServiceClient.AdicionarParametro("PK_Id_TemaActa", PK_Id_TemaActa);
                ServiceClient.AdicionarParametro("Usuario", usuarioActual.IdUsuario);
                ServiceClient.AdicionarParametro("NombreUsuario", usuarioActual.NombreUsuario);
                ServiceClient.AdicionarParametro("PK_Id_Acta", FK_Id_Acta);
                var result = ServiceClient.ObtenerArrayJsonRestFul<EDTemasActasCopasst>(UrlServicioParticipacion, EliminarTemaActaCopasst, RestSharp.Method.GET);
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
        public ActionResult eliminarAccionCopasst(int FK_Id_Acta, int Pk_Id_AccionActaCopasst)
        {
            var miembrosActasCopasst = new CrearActaCopasstVM();

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }

            if (ModelState.IsValid)
            {
                ServiceClient.EliminarParametros();
                ServiceClient.AdicionarParametro("Pk_Id_AccionActaCopasst", Pk_Id_AccionActaCopasst);
                ServiceClient.AdicionarParametro("Usuario", usuarioActual.IdUsuario);
                ServiceClient.AdicionarParametro("NombreUsuario", usuarioActual.NombreUsuario);
                ServiceClient.AdicionarParametro("PK_Id_Acta", FK_Id_Acta);
                var result = ServiceClient.ObtenerArrayJsonRestFul<EDAccionesActaCopasst>(UrlServicioParticipacion, EliminarAccionActaCopasst, RestSharp.Method.GET);
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
        public ActionResult MiembroActaCopasstGuardados(int? PK_Id_Acta, int? IdSede, int consecutivo_acta)
        {
            var miembrosActasCopasst = new CrearActaCopasstVM();

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }

            if (ModelState.IsValid)
            {
                ServiceClient.EliminarParametros();
                ServiceClient.AdicionarParametro("id_acta", PK_Id_Acta);
                var miembrosActa = ServiceClient.ObtenerArrayJsonRestFul<EDMiembrosCopasst>(UrlServicioParticipacion, ObtenerMiembrosCopasstPorActa, RestSharp.Method.GET);
                if (miembrosActa != null)
                {
                    ServiceClient.EliminarParametros();
                    var resultTipoPrincipal = ServiceClient.ObtenerArrayJsonRestFul<EDTipoPrincipalActa>(UrlServicioParticipacion, ObtenerTipoPrincipal, RestSharp.Method.GET);
                    if (resultTipoPrincipal != null && resultTipoPrincipal.Count() > 0)
                    {
                        miembrosActasCopasst.TiposPrincipales = resultTipoPrincipal.Select(cf => new SelectListItem()
                        {
                            Value = cf.Id_TipoPrincipal.ToString(),
                            Text = cf.DescripcionTipoPrincipal,
                        }).ToList();
                    }
                    else
                    {
                        miembrosActasCopasst.TiposPrincipales = resultTipoPrincipal.Select(cf => new SelectListItem()
                        {
                            Value = cf.Id_TipoPrincipal.ToString(),
                            Text = cf.DescripcionTipoPrincipal,
                        }).ToList();

                        ViewBag.mensaje = "Se deben Registrar los Tipos de Principales Actas para continuar";

                    }
                    ServiceClient.EliminarParametros();
                    var resultTipoPrioridad = ServiceClient.ObtenerArrayJsonRestFul<EDTipoPrioridadActa>(UrlServicioParticipacion, ObtenerTipoPrioridad, RestSharp.Method.GET);

                    if (resultTipoPrioridad != null && resultTipoPrioridad.Count() > 0)
                    {
                        miembrosActasCopasst.TiposPrioridadMiembros = resultTipoPrioridad.Select(cf => new SelectListItem()
                        {
                            Value = cf.Id_TipoPrioridadMiembro.ToString(),
                            Text = cf.DescripcionTipoMiembro,
                        }).ToList();
                    }
                    else
                    {
                        miembrosActasCopasst.TiposPrioridadMiembros = resultTipoPrioridad.Select(p => new SelectListItem()
                        {
                            Value = p.Id_TipoPrioridadMiembro.ToString(),
                            Text = p.DescripcionTipoMiembro,
                        }).ToList();

                        ViewBag.mensaje = "Se deben Registrar los Tipos de Prioridad Miembros Actas para continuar";

                    }
                    miembrosActasCopasst.MiembrosActaCopasst = miembrosActa.Select(cf => new MiembrosActaCopasstVM()
                    {
                        Numero_Documento = cf.Numero_Documento,
                        Nombre = cf.Nombre,
                        Fk_Id_TipoPrioridadMiembro = cf.Fk_Id_TipoPrioridadMiembro,
                        Des_TipoPrioridadMiembro = cf.Des_TipoPrioridadMiembro,
                        Fk_Id_TipoPrincipal = cf.Fk_Id_TipoPrincipal,
                        Des_TipoPrincipal = cf.Des_TipoPrincipal,
                        TipoRepresentante = cf.TipoRepresentante,
                        Fk_Id_Acta = Convert.ToInt32(PK_Id_Acta),
                    }).ToList();
                    
                    miembrosActasCopasst.Fk_Id_Sede = Convert.ToInt32(IdSede);
                    miembrosActasCopasst.Consecutivo_Acta = consecutivo_acta;
                    miembrosActasCopasst.Fk_Id_Acta = Convert.ToInt32(PK_Id_Acta);
                    miembrosActasCopasst.PK_Id_Acta = Convert.ToInt32(PK_Id_Acta);

                    return View(miembrosActasCopasst);

                }
                else
                {
                    return View(miembrosActasCopasst);
                }
            }
            else
            {
                return View(miembrosActasCopasst);
            }
        }


        //Carga Archivos PDF
        public ActionResult CargarActaCopasst(HttpPostedFileBase NombreArchivo, int? PK_Id_Acta, int? Consecutivo_Acta, int? Fk_Id_Sede)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }

            var crearActaCopasstVM = new CrearActaCopasstVM();

            //crearActaCopasstVM.Fk_Id_Sede = Convert.ToInt32(IdSede);

            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("Id_Acta", PK_Id_Acta);
            var resultActaCopasstEM = ServiceClient.ObtenerObjetoJsonRestFul<EDActasCopasst>(UrlServicioParticipacion, ObtenerActasCopasstPorId, RestSharp.Method.GET);
            if (resultActaCopasstEM != null)
            {
                crearActaCopasstVM.Consecutivo_Acta = resultActaCopasstEM.Consecutivo_Acta;
                crearActaCopasstVM.Fecha = resultActaCopasstEM.Fecha;
                crearActaCopasstVM.PK_Id_Acta = resultActaCopasstEM.PK_Id_Acta;
                crearActaCopasstVM.NombreEmpresa = resultActaCopasstEM.NombreEmpresa;
                crearActaCopasstVM.Fk_Id_Sede = resultActaCopasstEM.Fk_Id_Sede;
                crearActaCopasstVM.TemaReunion = resultActaCopasstEM.TemaReunion;
                crearActaCopasstVM.Conclusiones = resultActaCopasstEM.Conclusiones;

            }

            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("id_acta", PK_Id_Acta);
            var miembrosActa = ServiceClient.ObtenerArrayJsonRestFul<EDMiembrosCopasst>(UrlServicioParticipacion, ObtenerMiembrosCopasstPorActa, RestSharp.Method.GET);
            if (miembrosActa != null)
            {
                crearActaCopasstVM.MiembrosActaCopasst = miembrosActa.Select(p => new MiembrosActaCopasstVM()
                {
                    Numero_Documento = p.Numero_Documento,
                    Nombre = p.Nombre,
                    Fk_Id_TipoPrioridadMiembro = p.Fk_Id_TipoPrioridadMiembro,
                    Des_TipoPrioridadMiembro = p.Des_TipoPrioridadMiembro,
                    Fk_Id_TipoPrincipal = p.Fk_Id_TipoPrincipal,
                    Des_TipoPrincipal = p.Des_TipoPrincipal,
                    TipoRepresentante = p.TipoRepresentante,
                    Fk_Id_Acta = Convert.ToInt32(p.PK_Id_Acta),
                }).ToList();

            }

            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("id_acta", PK_Id_Acta);
            var participantesActa = ServiceClient.ObtenerArrayJsonRestFul<EDParticipantes>(UrlServicioParticipacion, ObtenerParticipantesCopasstPorActa, RestSharp.Method.GET);
            if (participantesActa != null)
            {
                crearActaCopasstVM.ParticipantesActaCopasst = participantesActa.Select(p => new ParticipantesActaCopasstVM()
                {
                    Numero_Documento = p.Numero_Documento,
                    Nombre = p.Nombre,
                    Fk_Id_Acta = Convert.ToInt32(p.PK_Id_Acta),
                }).ToList();

            }

            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("id_acta", PK_Id_Acta);
            var temasActa = ServiceClient.ObtenerArrayJsonRestFul<EDTemasActasCopasst>(UrlServicioParticipacion, ObtenerTemasCopasstPorActa, RestSharp.Method.GET);
            if (temasActa != null)
            {
                crearActaCopasstVM.TemasActaCopasst = temasActa.Select(p => new TemasActaCopasstVM()
                {
                    PK_Id_TemaActa = p.PK_Id_TemaActa,
                    Tema = p.Tema,
                    Observaciones = p.Observaciones,
                    Fk_Id_Acta = Convert.ToInt32(p.PK_Id_Acta),
                }).ToList();

            }

            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("id_acta", PK_Id_Acta);
            var accionesActa = ServiceClient.ObtenerArrayJsonRestFul<EDAccionesActaCopasst>(UrlServicioParticipacion, ObtenerAccionesCopasstPorActa, RestSharp.Method.GET);
            if (accionesActa != null)
            {
                crearActaCopasstVM.AccionesActaCopasst = accionesActa.Select(p => new AccionesActaCopasstVM()
                {
                    Pk_Id_AccionActaCopasst = p.Pk_Id_AccionActaCopasst,
                    AccionARealizar = p.AccionARealizar,
                    FechaProbable = p.FechaProbable,
                    Responsable = p.Responsable,
                    Fk_Id_Acta = Convert.ToInt32(p.PK_Id_Acta),
                }).ToList();

            }

            if (ModelState.IsValid)
            {


                EDActasCopasst ImpActaCopasst = new EDActasCopasst();
                var path = "";

                if (NombreArchivo != null)
                {

                    if (NombreArchivo.ContentLength > 0)
                    {
                        if (Path.GetExtension(NombreArchivo.FileName).ToLower() == ".pdf")
                        {
                            path = Path.Combine(Server.MapPath(rutaArchivosActas), NombreArchivo.FileName);
                            NombreArchivo.SaveAs(path);
                            ViewBag.UploadSuccess = true;
                        }
                        else
                        {
                            ViewBag.mensaje = "Debe cargar documentos tipo PDF" ;
                            return View("DatosActaCopasst", crearActaCopasstVM);
                        }

                    }

                     ImpActaCopasst.Fk_Id_UsuarioSistema = usuarioActual.IdUsuario;
                    ImpActaCopasst.NombreUsuario = usuarioActual.NombreUsuario;
                    ImpActaCopasst.Consecutivo_Acta = Convert.ToInt32(Consecutivo_Acta);
                    ImpActaCopasst.PK_Id_Acta = Convert.ToInt32(PK_Id_Acta);
                    ImpActaCopasst.Fk_Id_Sede = Convert.ToInt32(Fk_Id_Sede);
                    ImpActaCopasst.NombreArchivo = NombreArchivo.FileName;

                    ServiceClient.EliminarParametros();
                    var respuesta = ServiceClient.RealizarPeticionesPostJsonRestFul<EDActasCopasst>(UrlServicioParticipacion, ImportarActaCopasst, ImpActaCopasst);
                    if (respuesta != null)
                    {
                        ViewBag.mensaje1 = "El archivo ha sido Importado.";
                        return View("DatosActaCopasst", crearActaCopasstVM);
                    }
                    else
                    {
                        ViewBag.mensaje = "El archivo no ha sido Importado.";
                        return View("DatosActaCopasst", crearActaCopasstVM);
                    }
                }
                else
                {
                    ViewBag.mensaje = "Seleccione un archivo para Importar por favor";
                    return View("DatosActaCopasst", crearActaCopasstVM);
                }

            }

            ViewBag.mensaje =  "ERROR";
            return View("DatosActaCopasst", crearActaCopasstVM);
         }

        //Visualiza Archivo PDF
        public FileStreamResult VisualizarActaCopasstPDF(int Pk_Id_Acta)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
            }
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("Id_Acta", Pk_Id_Acta);
            var resultActaEM = ServiceClient.ObtenerObjetoJsonRestFul<EDActasCopasst>(UrlServicioParticipacion, ObtenerActasCopasstPorId, RestSharp.Method.GET);
            if (resultActaEM != null)
            {
                var path = Server.MapPath(rutaArchivosActas);
                var file = resultActaEM.NombreArchivo;
                if (file != null)
                {
                    var fullPath = Path.Combine(path, file);
                    FileStream fs = new FileStream(fullPath, FileMode.Open, FileAccess.Read);
                    return File(fs, "application/pdf");
                }
                return null;
            }
            return null;
        }

        //Actualizar los Datos del Acta Coppast
        public ActionResult ActualizarActaCopasst(EDActasCopasst actaCopasst)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }

            if (ModelState.IsValid)
            {

            EDActasCopasst InformacionActa = new EDActasCopasst();
  
                InformacionActa.Fk_Id_UsuarioSistema = usuarioActual.IdUsuario;
                InformacionActa.NombreUsuario = usuarioActual.NombreUsuario;
                InformacionActa.Fk_Id_Sede = actaCopasst.Fk_Id_Sede;
                InformacionActa.Consecutivo_Acta = actaCopasst.Consecutivo_Acta;
                InformacionActa.Fecha = actaCopasst.Fecha;
                InformacionActa.TemaReunion = actaCopasst.TemaReunion;
                InformacionActa.Conclusiones = actaCopasst.Conclusiones;
                InformacionActa.PK_Id_Acta = actaCopasst.PK_Id_Acta;

                ServiceClient.EliminarParametros();
                var respuesta = ServiceClient.RealizarPeticionesPostJsonRestFul<EDActasCopasst>(UrlServicioParticipacion, CapacidadActualizarActaCopasst, InformacionActa);
                if (respuesta != null)
                {
                    return Json(new { Data = respuesta, Mensaje = "OK" }, JsonRequestBehavior.AllowGet);
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
        
        //Actualizar Observacion Tema Acta Coppast
        public ActionResult actualizarTemaCopasst(TemasActaCopasstVM temaActaCopasst)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }

            if (ModelState.IsValid)
            {

                EDTemasActasCopasst observacionTema = new EDTemasActasCopasst();
  
                observacionTema.UsuarioSistema = usuarioActual.IdUsuario;
                observacionTema.NombreUsuario = usuarioActual.NombreUsuario;
                observacionTema.PK_Id_TemaActa = temaActaCopasst.PK_Id_TemaActa;
                observacionTema.PK_Id_Acta = temaActaCopasst.Fk_Id_Acta;
                observacionTema.Observaciones = temaActaCopasst.Observaciones;

                ServiceClient.EliminarParametros();
                var respuesta = ServiceClient.RealizarPeticionesArrayPostJsonRest<EDTemasActasCopasst>(UrlServicioParticipacion, ActualizarTemasActaCopasst, observacionTema);
                if (respuesta != null)
                {
                    return Json(new { Data = respuesta, Mensaje = "OK" }, JsonRequestBehavior.AllowGet);
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



        public ActionResult ActaCopasst_PDF(int PK_Id_Acta)
        {


           var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
           if (usuarioActual == null)
           {
               ViewBag.mensaje = "Debe Registrarse para Ingresar a este Modulo.";
               return RedirectToAction("Login", "Home");
           }

           var crearActaCopasstVM = new CrearActaCopasstVM();
           
            string EncodedRazonSocial = System.Net.WebUtility.UrlEncode(usuarioActual.RazonSocialEmpresa);
           string EncodedNit = System.Net.WebUtility.UrlEncode(usuarioActual.NitEmpresa);
           var footurl = "https://alissta.gov.co/Acciones/Footer";
           var headerurl = "https://alissta.gov.co/Acciones/Header?NombreEmpresa=" + EncodedRazonSocial + "&NitEmpresa=" + EncodedNit;

            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("Id_Acta", PK_Id_Acta);
            var resultActaCopasstEM = ServiceClient.ObtenerObjetoJsonRestFul<EDActasCopasst>(UrlServicioParticipacion, ObtenerActasCopasstPorId, RestSharp.Method.GET);
            if (resultActaCopasstEM != null)
            {
                crearActaCopasstVM.Consecutivo_Acta = resultActaCopasstEM.Consecutivo_Acta;
                crearActaCopasstVM.Fecha = resultActaCopasstEM.Fecha;
                crearActaCopasstVM.PK_Id_Acta = resultActaCopasstEM.PK_Id_Acta;
                crearActaCopasstVM.NombreEmpresa = resultActaCopasstEM.NombreEmpresa;
                crearActaCopasstVM.Fk_Id_Sede = resultActaCopasstEM.Fk_Id_Sede;
                crearActaCopasstVM.TemaReunion = resultActaCopasstEM.TemaReunion;
                crearActaCopasstVM.Conclusiones = resultActaCopasstEM.Conclusiones;

            }

            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("id_acta", PK_Id_Acta);
            var miembrosActa = ServiceClient.ObtenerArrayJsonRestFul<EDMiembrosCopasst>(UrlServicioParticipacion, ObtenerMiembrosCopasstPorActa, RestSharp.Method.GET);
            if (miembrosActa != null)
            {
                crearActaCopasstVM.MiembrosActaCopasst = miembrosActa.Select(p => new MiembrosActaCopasstVM()
                {
                    Numero_Documento=p.Numero_Documento,
                    Nombre=p.Nombre,
                    Fk_Id_TipoPrioridadMiembro=p.Fk_Id_TipoPrioridadMiembro,
                    Des_TipoPrioridadMiembro=p.Des_TipoPrioridadMiembro,
                    Fk_Id_TipoPrincipal=p.Fk_Id_TipoPrincipal,
                    Des_TipoPrincipal=p.Des_TipoPrincipal,
                    TipoRepresentante=p.TipoRepresentante,
                    Fk_Id_Acta = Convert.ToInt32(p.PK_Id_Acta),
                }).ToList();

            }

            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("id_acta", PK_Id_Acta);
            var participantesActa = ServiceClient.ObtenerArrayJsonRestFul<EDParticipantes>(UrlServicioParticipacion, ObtenerParticipantesCopasstPorActa, RestSharp.Method.GET);
            if (participantesActa != null)
            {
                crearActaCopasstVM.ParticipantesActaCopasst = participantesActa.Select(p => new ParticipantesActaCopasstVM()
                {
                    Numero_Documento = p.Numero_Documento,
                    Nombre = p.Nombre,
                    Fk_Id_Acta = Convert.ToInt32(p.PK_Id_Acta),
                }).ToList();

            }

            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("id_acta", PK_Id_Acta);
            var temasActa = ServiceClient.ObtenerArrayJsonRestFul<EDTemasActasCopasst>(UrlServicioParticipacion, ObtenerTemasCopasstPorActa, RestSharp.Method.GET);
            if (temasActa != null)
            {
                crearActaCopasstVM.TemasActaCopasst = temasActa.Select(p => new TemasActaCopasstVM()
                {
                    PK_Id_TemaActa = p.PK_Id_TemaActa,
                    Tema = p.Tema,
                    Observaciones = p.Observaciones,
                    Fk_Id_Acta = Convert.ToInt32(p.PK_Id_Acta),
                }).ToList();

            }

            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("id_acta", PK_Id_Acta);
            var accionesActa = ServiceClient.ObtenerArrayJsonRestFul<EDAccionesActaCopasst>(UrlServicioParticipacion, ObtenerAccionesCopasstPorActa, RestSharp.Method.GET);
            if (accionesActa != null)
            {
                crearActaCopasstVM.AccionesActaCopasst = accionesActa.Select(p => new AccionesActaCopasstVM()
                {
                    Pk_Id_AccionActaCopasst = p.Pk_Id_AccionActaCopasst,
                    AccionARealizar = p.AccionARealizar,
                    FechaProbable = p.FechaProbable,
                    Responsable = p.Responsable,
                    Fk_Id_Acta = Convert.ToInt32(p.PK_Id_Acta),
                }).ToList();

            }

            string cusomtSwitches = string.Format("--footer-line --print-media-type --allow {0} --footer-html {0} --header-line --print-media-type --allow {1} --header-html {1} --header-spacing 5", footurl, headerurl);
            return new Rotativa.PartialViewAsPdf("ActaCopasstPDF", crearActaCopasstVM) { FileName = "ActaCopasst_No." + crearActaCopasstVM.Consecutivo_Acta + ".pdf", CustomSwitches = cusomtSwitches};
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

    }
}