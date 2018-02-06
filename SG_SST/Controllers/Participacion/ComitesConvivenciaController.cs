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


namespace SG_SST.Controllers.Participacion
{
    public class ComitesConvivenciaController : BaseController
    {
        private SG_SSTContext db = new SG_SSTContext();
        string UrlServicioEmpresas = ConfigurationManager.AppSettings["UrlServicioEmpresas"];
        string UrlServicioParticipacion = ConfigurationManager.AppSettings["UrlServicioParticipacion"];
        string ObtenerInformacionSede = ConfigurationManager.AppSettings["ObtenerInformacionSede"];
        string ObtenerActasConvivenciaPorNit = ConfigurationManager.AppSettings["ObtenerInformacionActaConvivencia"];
        string CapacidadObtenerSedesPorNit = ConfigurationManager.AppSettings["CapacidadObtenerSedesPorNit"];
        string ObtenerTipoDocumento = ConfigurationManager.AppSettings["ObtenerTipoDocumento"];
        string ObtenerTipoPrincipal = ConfigurationManager.AppSettings["ObtenerTipoPrincipal"];
        string ObtenerTipoPrioridad = ConfigurationManager.AppSettings["ObtenerTipoPrioridad"];
        string ObtenerActasConvivenciaPorEmpresa = ConfigurationManager.AppSettings["ObtenerActasConvivenciaPorEmpresa"];
        string ObtenerActasConvivenciaPorId = ConfigurationManager.AppSettings["ObtenerActasConvivenciaPorId"];
        string ImportarActaConvivencia = ConfigurationManager.AppSettings["ImportarActaConvivencia"];
        string CapacidadActualizarActaConvivencia = ConfigurationManager.AppSettings["ActualizarActaConvivencia"];
        string rutaArchivosActas = ConfigurationManager.AppSettings["rutaArchivosActas"];

        string CapacidadGuardarMiembrosActaConvivencia = ConfigurationManager.AppSettings["CapacidadGuardarMiembrosActaConvivencia"];
        string ObtenerMiembrosConvivenciaPorActa = ConfigurationManager.AppSettings["ObtenerMiembrosConvivenciaPorActa"];
        string EliminarMiembroActaConvivencia = ConfigurationManager.AppSettings["EliminarMiembroActaConvivencia"];

        string GuardarParticipantesActaConvivencia = ConfigurationManager.AppSettings["GuardarParticipantesActaConvivencia"];
        string ObtenerParticipantesConvivenciaPorActa = ConfigurationManager.AppSettings["ObtenerParticipantesConvivenciaPorActa"];
        string EliminarParticipanteActaConvivencia = ConfigurationManager.AppSettings["EliminarParticipanteActaConvivencia"];

        string GuardarTemasActaConvivencia = ConfigurationManager.AppSettings["GuardarTemasActaConvivencia"];
        string ActualizarTemasActaConvivencia = ConfigurationManager.AppSettings["ActualizarTemasActaConvivencia"];
        string ObtenerTemasConvivenciaPorActa = ConfigurationManager.AppSettings["ObtenerTemasConvivenciaPorActa"];
        string EliminarTemaActaConvivencia = ConfigurationManager.AppSettings["EliminarTemaActaConvivencia"];

        string GuardarAccionesActaConvivencia = ConfigurationManager.AppSettings["GuardarAccionesActaConvivencia"];
        string ObtenerAccionesConvivenciaPorActa = ConfigurationManager.AppSettings["ObtenerAccionesConvivenciaPorActa"];
        string EliminarAccionActaConvivencia = ConfigurationManager.AppSettings["EliminarAccionActaConvivencia"];

        string ObtenerActasConvivenciaQueja = ConfigurationManager.AppSettings["ObtenerActasConvivenciaQueja"];
        string ObtenerAccionesActasQueja = ConfigurationManager.AppSettings["ObtenerAccionesActasQueja"];
        string ObtenerResponsablesQueja = ConfigurationManager.AppSettings["ObtenerResponsablesQueja"];
        string GuardarActasQueja = ConfigurationManager.AppSettings["GuardarActasQueja"];
        string GuardarAccionesActaQueja = ConfigurationManager.AppSettings["GuardarAccionesActaQueja"];
        string GuardarResponsablesQueja = ConfigurationManager.AppSettings["GuardarResponsablesQueja"];
        string CapacidadActualizarActaConvivenciaQueja = ConfigurationManager.AppSettings["CapacidadActualizarActaConvivenciaQueja"];
        string EliminarAccionActaQueja = ConfigurationManager.AppSettings["EliminarAccionActaQueja"];
        string EliminarResponsableQueja = ConfigurationManager.AppSettings["EliminarResponsableQueja"];
        
        string ObtenerActasConvivenciaSeguimiento = ConfigurationManager.AppSettings["ObtenerActasConvivenciaSeguimiento"];
        string GuardarActasSeguimiento = ConfigurationManager.AppSettings["GuardarActasSeguimiento"];
        string ObtenerCompromisosActaConvivencia = ConfigurationManager.AppSettings["ObtenerCompromisosActaConvivencia"];
        string CapacidadActualizarActaConvivenciaSeguimiento = ConfigurationManager.AppSettings["CapacidadActualizarActaConvivenciaSeguimiento"];
        string GuardarCompromisosSeguimiento = ConfigurationManager.AppSettings["GuardarCompromisosSeguimiento"];
        string EliminarCompromisoSeguimiento = ConfigurationManager.AppSettings["EliminarCompromisoSeguimiento"];

        
        /// <summary>
       /// Metodo Inicio que retorna a la Vista la Lista de Sedes
       /// </summary>
       /// <returns></returns>
        public ActionResult Index()
        {
            var convivencia = new ConvivenciaVM();


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
        public ActionResult HistoricoActaConvivencia(int? IdSede)
        {
           var actaConvivencia = new ConvivenciaVM();
           var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);

           if (usuarioActual == null)
           {
               ViewBag.mensaje1 = "Debe iniciar Sesión para Continuar.";
               return RedirectToAction("Login", "Home");
           }

           if (IdSede == null)
           {
               var copasst = new ConvivenciaVM();
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
            var resultacta = ServiceClient.ObtenerArrayJsonRestFul<EDActasConvivencia>(UrlServicioParticipacion, ObtenerActasConvivenciaPorNit, RestSharp.Method.GET);
            if (resultacta != null && resultacta.Count() > 0)
            {
                actaConvivencia.ActasConvivencia = resultacta.Select(a => new ConvivenciaVM
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
                actaConvivencia.Fk_Id_Sede = Insede;
                return View(actaConvivencia);
            }

            Insede = Convert.ToInt32(IdSede);
            actaConvivencia.Fk_Id_Sede = Insede;

            return View(actaConvivencia);
        }
        public ActionResult CrearActaConvivencia(int? IdSede)
         {
             var actaConvivencia = new ConvivenciaVM();
             var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);

             if (usuarioActual == null)
             {
                 ViewBag.mensaje = "Debe iniciar Sesión para Continuar.";
                 return RedirectToAction("Login", "Home");
             }

             if (IdSede == null)
             {
                 var convivencia = new ConvivenciaVM();
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
                 return View("Index", convivencia);
             }

             var crearActaConvivencia = new CrearActaConvivenciaVM();

            ServiceClient.EliminarParametros();
            var resultTipoPrincipal = ServiceClient.ObtenerArrayJsonRestFul<EDTipoPrincipalActa>(UrlServicioParticipacion, ObtenerTipoPrincipal, RestSharp.Method.GET);
            if (resultTipoPrincipal != null && resultTipoPrincipal.Count() > 0)
            {
                crearActaConvivencia.TiposPrincipales = resultTipoPrincipal.Select(cf => new SelectListItem()
                {
                    Value = cf.Id_TipoPrincipal.ToString(),
                    Text = cf.DescripcionTipoPrincipal,
                }).ToList();
            }
            else
            {
                crearActaConvivencia.TiposPrincipales = resultTipoPrincipal.Select(cf => new SelectListItem()
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
                crearActaConvivencia.TiposPrioridadMiembros = resultTipoPrioridad.Select(cf => new SelectListItem()
                {
                    Value = cf.Id_TipoPrioridadMiembro.ToString(),
                    Text = cf.DescripcionTipoMiembro,
                }).ToList();
            }
            else
            {
                crearActaConvivencia.TiposPrioridadMiembros = resultTipoPrioridad.Select(p => new SelectListItem()
                {
                    Value = p.Id_TipoPrioridadMiembro.ToString(),
                    Text = p.DescripcionTipoMiembro,
                }).ToList();

                ViewBag.mensaje = "Se deben Registrar los Tipos de Prioridad Miembros Actas para continuar";
                
            }

            crearActaConvivencia.Fk_Id_Sede = Convert.ToInt32(IdSede);

             return View(crearActaConvivencia);
         }
        public ActionResult BuscarInformacionSede(int IdSede)
        {
            var copasst = new ConvivenciaVM();
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
        public ActionResult GuardarMiembro(CrearActaConvivenciaVM Miembro)
        {
            var consecutivoActaConvivencia = 0;
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
                    var resultActaConvivenciaEM = ServiceClient.ObtenerArrayJsonRestFul<EDActasConvivencia>(UrlServicioParticipacion, ObtenerActasConvivenciaPorEmpresa, RestSharp.Method.GET);
                    if (resultActaConvivenciaEM.Count() == 0)
                    {
                        consecutivoActaConvivencia = 1;
                    }
                    else
                    {
                        consecutivoActaConvivencia = resultActaConvivenciaEM.Last().Consecutivo_Acta + 1;
                    }
                }
                else
                {
                    consecutivoActaConvivencia = Convert.ToInt32(Miembro.Consecutivo_Acta);
                }
                var actasConvivencia = new EDMiembrosConvivencia()
                {
                    Numero_Documento = Miembro.Numero_Documento,
                    Nombre = Miembro.Nombre,
                    Fk_Id_TipoPrioridadMiembro = Miembro.Fk_Id_TipoPrioridadMiembro,
                    Fk_Id_TipoPrincipal = Miembro.Fk_Id_TipoPrincipal,
                    TipoRepresentante = Miembro.TipoRepresentante,
                    Consecutivo_Acta = consecutivoActaConvivencia,
                    IdEmpresa = usuarioActual.IdEmpresa,
                    UsuarioSistema = usuarioActual.IdUsuario,
                    NombreUsuario = usuarioActual.NombreUsuario,
                    NombreEmpresa = usuarioActual.RazonSocialEmpresa,
                    IdSede = Miembro.Fk_Id_Sede,
               };
                ServiceClient.EliminarParametros();
                var result = ServiceClient.RealizarPeticionesPostJsonRestFul<EDMiembrosConvivencia>(UrlServicioParticipacion, CapacidadGuardarMiembrosActaConvivencia, actasConvivencia);
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

        public ActionResult ActaConvivenciaQueja(int PK_Id_Acta, int IdSede)
        {
            ActaConvivenciaQuejasVM actaConvivenciaQuejasVM = new ActaConvivenciaQuejasVM();
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                ServiceClient.EliminarParametros();
                ServiceClient.AdicionarParametro("IdSede", IdSede);
                var resultActaConvivenciaQueja = ServiceClient.ObtenerArrayJsonRestFul<EDActaConvivenciaQuejas>(UrlServicioParticipacion, ObtenerActasConvivenciaQueja, RestSharp.Method.GET);
                if (resultActaConvivenciaQueja.Count() == 0)
                {
                        EDActaConvivenciaQuejas acta = new EDActaConvivenciaQuejas();

                        acta.Consecutivo_Queja =  1;
                        acta.Consecutivo_Caso =  1;
                        acta.Fk_Id_Acta = PK_Id_Acta;
                        acta.Fk_Id_Sede = IdSede;
                        acta.UsuarioSistema = usuarioActual.IdUsuario;
                        acta.NombreUsuario = usuarioActual.NombreUsuario;

                        ServiceClient.EliminarParametros();
                        var result = ServiceClient.ObtenerObjetoJsonRestFul<EDActaConvivenciaQuejas>(UrlServicioParticipacion, GuardarActasQueja, acta);
                        if (result != null)
                        {
                            actaConvivenciaQuejasVM.PK_Id_Queja = result.PK_Id_Queja;
                            actaConvivenciaQuejasVM.Consecutivo_Caso = result.Consecutivo_Caso;
                            actaConvivenciaQuejasVM.Consecutivo_Queja = result.Consecutivo_Queja; ;
                            actaConvivenciaQuejasVM.Fecha = result.Fecha;
                            actaConvivenciaQuejasVM.PK_Id_Acta = result.Fk_Id_Acta;
                            actaConvivenciaQuejasVM.IdSede = result.Fk_Id_Sede;
                            actaConvivenciaQuejasVM.NombreRefiereSituacion = result.NombreRefiereSituacion;
                            actaConvivenciaQuejasVM.AspectosNoResueltos = result.AspectosNoResueltos;
                            actaConvivenciaQuejasVM.Compromisos = result.Compromisos;

                            return View("DatosQuejas", actaConvivenciaQuejasVM);
                        }
                }
                else
                {
                    var actaQuejaGuardada = (from aq in resultActaConvivenciaQueja
                                             where aq.Fk_Id_Acta == PK_Id_Acta
                                             select aq);

                    if (actaQuejaGuardada.Count() == 0)
                    {
                        EDActaConvivenciaQuejas acta = new EDActaConvivenciaQuejas();

                        acta.Consecutivo_Queja = resultActaConvivenciaQueja.Last().Consecutivo_Queja + 1;
                        acta.Consecutivo_Caso = resultActaConvivenciaQueja.Last().Consecutivo_Caso + 1;
                        acta.Fk_Id_Acta = PK_Id_Acta;
                        acta.Fk_Id_Sede = IdSede;
                        acta.UsuarioSistema = usuarioActual.IdUsuario;
                        acta.NombreUsuario = usuarioActual.NombreUsuario;

                        ServiceClient.EliminarParametros();
                        var result = ServiceClient.ObtenerObjetoJsonRestFul<EDActaConvivenciaQuejas>(UrlServicioParticipacion, GuardarActasQueja, acta);
                        if (result != null )
                        {
                            actaConvivenciaQuejasVM.PK_Id_Queja = result.PK_Id_Queja;
                            actaConvivenciaQuejasVM.Consecutivo_Caso = result.Consecutivo_Caso;
                            actaConvivenciaQuejasVM.Consecutivo_Queja = result.Consecutivo_Queja; ;
                            actaConvivenciaQuejasVM.Fecha = result.Fecha;
                            actaConvivenciaQuejasVM.PK_Id_Acta = result.Fk_Id_Acta;
                            actaConvivenciaQuejasVM.IdSede = result.Fk_Id_Sede;
                            actaConvivenciaQuejasVM.NombreRefiereSituacion = result.NombreRefiereSituacion;
                            actaConvivenciaQuejasVM.AspectosNoResueltos = result.AspectosNoResueltos;
                            actaConvivenciaQuejasVM.Compromisos = result.Compromisos;

                            return View("DatosQuejas", actaConvivenciaQuejasVM);
                        }
                    }
                    else
                    {
                        actaConvivenciaQuejasVM.PK_Id_Queja = actaQuejaGuardada.First().PK_Id_Queja;
                        actaConvivenciaQuejasVM.Consecutivo_Caso = actaQuejaGuardada.First().Consecutivo_Caso;
                        actaConvivenciaQuejasVM.Consecutivo_Queja = actaQuejaGuardada.First().Consecutivo_Queja; ;
                        actaConvivenciaQuejasVM.Fecha = actaQuejaGuardada.First().Fecha;
                        actaConvivenciaQuejasVM.PK_Id_Acta = actaQuejaGuardada.First().Fk_Id_Acta;
                        actaConvivenciaQuejasVM.IdSede = actaQuejaGuardada.First().Fk_Id_Sede;
                        actaConvivenciaQuejasVM.NombreRefiereSituacion = actaQuejaGuardada.First().NombreRefiereSituacion;
                        actaConvivenciaQuejasVM.AspectosNoResueltos = actaQuejaGuardada.First().AspectosNoResueltos;
                        actaConvivenciaQuejasVM.Compromisos = actaQuejaGuardada.First().Compromisos;

                        ServiceClient.EliminarParametros();
                        ServiceClient.AdicionarParametro("PK_Id_Queja", actaQuejaGuardada.First().PK_Id_Queja);
                        var resultAccionesQueja = ServiceClient.ObtenerArrayJsonRestFul<EDAccionesActaQuejas>(UrlServicioParticipacion, ObtenerAccionesActasQueja, RestSharp.Method.GET);
                        if (resultAccionesQueja.Count() > 0)
                        {
                            actaConvivenciaQuejasVM.AccionesActaQuejas = resultAccionesQueja.Select(r => new AccionesActaQuejasVM
                                                                                            {
                                                                                                Pk_Id_AccionQueja = r.Pk_Id_AccionQueja,
                                                                                                AccionARealizar = r.AccionARealizar,
                                                                                                Fk_Id_Queja = r.Fk_Id_Queja,
                                                                                            }).ToList();
                        }

                        ServiceClient.EliminarParametros();
                        ServiceClient.AdicionarParametro("PK_Id_Queja", actaQuejaGuardada.First().PK_Id_Queja);
                        var resultResponsablesQueja = ServiceClient.ObtenerArrayJsonRestFul<EDResponsablesQuejas>(UrlServicioParticipacion, ObtenerResponsablesQueja, RestSharp.Method.GET);
                        if (resultResponsablesQueja.Count() > 0)
                        {
                            actaConvivenciaQuejasVM.ResponsablesQuejas = resultResponsablesQueja.Select(r => new ResponsablesQuejasVM
                                                                                                    {
                                                                                                    Pk_Id_Responsable = r.Pk_Id_Responsable,
                                                                                                    Numero_Documento = r.Numero_Documento,
                                                                                                    Nombre = r.Nombre,
                                                                                                    Fk_Id_Queja = r.Fk_Id_Queja
                                                                                                    }).ToList();
                        }

                         return View("DatosQuejas", actaConvivenciaQuejasVM);
                    }
                }

            }

            return null;
        }

        public ActionResult SeguimientoActas(int PK_Id_Acta, int IdSede)
        {
            SeguimientoActaConvivenciaVM seguimientoActaConvivenciaVM = new SeguimientoActaConvivenciaVM();

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                ServiceClient.EliminarParametros();
                ServiceClient.AdicionarParametro("IdSede", IdSede);
                var resultActaConvivenciaSeguimiento = ServiceClient.ObtenerArrayJsonRestFul<EDSeguimientoActaConvivencia>(UrlServicioParticipacion, ObtenerActasConvivenciaSeguimiento, RestSharp.Method.GET);
                if (resultActaConvivenciaSeguimiento.Count() == 0)
                {
                    EDSeguimientoActaConvivencia seguimiento = new EDSeguimientoActaConvivencia();

                    seguimiento.Consecutivo_Evento = 1;
                     seguimiento.Fk_Id_Acta = PK_Id_Acta;
                    seguimiento.Fk_Id_Sede = IdSede;
                    seguimiento.UsuarioSistema = usuarioActual.IdUsuario;
                    seguimiento.NombreUsuario = usuarioActual.NombreUsuario;

                    ServiceClient.EliminarParametros();
                    var result = ServiceClient.ObtenerObjetoJsonRestFul<EDSeguimientoActaConvivencia>(UrlServicioParticipacion, GuardarActasSeguimiento, seguimiento);
                    if (result != null)
                    {
                        seguimientoActaConvivenciaVM.PK_Id_Seguimiento = result.PK_Id_Seguimiento;
                        seguimientoActaConvivenciaVM.Consecutivo_Evento = result.Consecutivo_Evento;
                        seguimientoActaConvivenciaVM.Fecha = result.Fecha;
                        seguimientoActaConvivenciaVM.PK_Id_Acta = result.Fk_Id_Acta;
                        seguimientoActaConvivenciaVM.IdSede = result.Fk_Id_Sede;
                        seguimientoActaConvivenciaVM.NombreParteInvolucrada = result.NombreParteInvolucrada;
                        seguimientoActaConvivenciaVM.CompromisosAdquiridos = result.CompromisosAdquiridos;
                        seguimientoActaConvivenciaVM.Observaciones = result.Observaciones;
                        seguimientoActaConvivenciaVM.PK_Id_Seguimiento = result.PK_Id_Seguimiento;

                        return View("DatosSeguimiento", seguimientoActaConvivenciaVM);
                    }
                }
                else
                {
                    var actaSeguimientoGuardada = (from aq in resultActaConvivenciaSeguimiento
                                             where aq.Fk_Id_Acta == PK_Id_Acta
                                             select aq);

                    if (actaSeguimientoGuardada.Count() == 0)
                    {
                        EDSeguimientoActaConvivencia seguimiento = new EDSeguimientoActaConvivencia();

                        seguimiento.Consecutivo_Evento = resultActaConvivenciaSeguimiento.Last().Consecutivo_Evento + 1;
                        seguimiento.Fk_Id_Acta = PK_Id_Acta;
                        seguimiento.Fk_Id_Sede = IdSede;
                        seguimiento.UsuarioSistema = usuarioActual.IdUsuario;
                        seguimiento.NombreUsuario = usuarioActual.NombreUsuario;

                        ServiceClient.EliminarParametros();
                        var result = ServiceClient.ObtenerObjetoJsonRestFul<EDSeguimientoActaConvivencia>(UrlServicioParticipacion, GuardarActasSeguimiento, seguimiento);
                        if (result != null)
                        {
                            seguimientoActaConvivenciaVM.PK_Id_Seguimiento = result.PK_Id_Seguimiento;
                            seguimientoActaConvivenciaVM.Consecutivo_Evento = result.Consecutivo_Evento;
                            seguimientoActaConvivenciaVM.Fecha = result.Fecha;
                            seguimientoActaConvivenciaVM.PK_Id_Acta = result.Fk_Id_Acta;
                            seguimientoActaConvivenciaVM.IdSede = result.Fk_Id_Sede;
                            seguimientoActaConvivenciaVM.NombreParteInvolucrada = result.NombreParteInvolucrada;
                            seguimientoActaConvivenciaVM.CompromisosAdquiridos = result.CompromisosAdquiridos;
                            seguimientoActaConvivenciaVM.Observaciones = result.Observaciones;
                            seguimientoActaConvivenciaVM.PK_Id_Seguimiento = result.PK_Id_Seguimiento;

                            return View("DatosSeguimiento", seguimientoActaConvivenciaVM);
                        }
                    }
                    else
                    {
                        seguimientoActaConvivenciaVM.PK_Id_Seguimiento = actaSeguimientoGuardada.First().PK_Id_Seguimiento;
                        seguimientoActaConvivenciaVM.Consecutivo_Evento = actaSeguimientoGuardada.First().Consecutivo_Evento; ;
                        seguimientoActaConvivenciaVM.Fecha = actaSeguimientoGuardada.First().Fecha;
                        seguimientoActaConvivenciaVM.PK_Id_Acta = actaSeguimientoGuardada.First().Fk_Id_Acta;
                        seguimientoActaConvivenciaVM.IdSede = actaSeguimientoGuardada.First().Fk_Id_Sede;
                        seguimientoActaConvivenciaVM.NombreParteInvolucrada = actaSeguimientoGuardada.First().NombreParteInvolucrada;
                        seguimientoActaConvivenciaVM.CompromisosAdquiridos = actaSeguimientoGuardada.First().CompromisosAdquiridos;
                        seguimientoActaConvivenciaVM.Observaciones = actaSeguimientoGuardada.First().Observaciones;
                        seguimientoActaConvivenciaVM.PK_Id_Seguimiento = actaSeguimientoGuardada.First().PK_Id_Seguimiento;

                        ServiceClient.EliminarParametros();
                        ServiceClient.AdicionarParametro("PK_Id_Seguimiento", actaSeguimientoGuardada.First().PK_Id_Seguimiento);
                        var resultCompromisos = ServiceClient.ObtenerArrayJsonRestFul<EDCompromisosPendientes>(UrlServicioParticipacion, ObtenerCompromisosActaConvivencia, RestSharp.Method.GET);
                        if (resultCompromisos.Count() > 0)
                        {
                            seguimientoActaConvivenciaVM.CompromisosPendientes = resultCompromisos.Select(r => new CompromisosPendientesVM
                            {
                                Pk_Id_Compromiso = r.Pk_Id_Compromiso,
                                CompromisoPendiente = r.CompromisoPendiente,
                                FK_Id_Seguimiento = r.FK_Id_Seguimiento,
                            }).ToList();
                        }

                        return View("DatosSeguimiento", seguimientoActaConvivenciaVM);
                    }
                }

            }

            return null;
        }

        public ActionResult GuardarParticipante(CrearActaConvivenciaVM Participante)
        {
                 var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                var participantesConvivencia = new EDParticipantes()
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
                var result = ServiceClient.RealizarPeticionesArrayPostJsonRest<EDParticipantes>(UrlServicioParticipacion, GuardarParticipantesActaConvivencia, participantesConvivencia);
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
                return Json(new { Data = string.Empty, Mensaje = "El Número ingresado no es valido" }, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult GuardarTema(CrearActaConvivenciaVM Tema)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                var temasConvivencia = new EDTemasActasConvivencia()
                {
                    Tema = Tema.TemaOrdenDia,
                    PK_Id_Acta = Tema.PK_Id_Acta,
                    Fk_Id_UsuarioSistema = usuarioActual.IdUsuario,
                    NombreUsuario = usuarioActual.NombreUsuario,
                    Consecutivo_Acta = Convert.ToInt32(Tema.Consecutivo_Acta),
                    IdSede = Convert.ToInt32(Tema.Fk_Id_Sede),
                };
                ServiceClient.EliminarParametros();
                var result = ServiceClient.RealizarPeticionesArrayPostJsonRest<EDTemasActasConvivencia>(UrlServicioParticipacion, GuardarTemasActaConvivencia, temasConvivencia);
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

        public ActionResult GuardarAccion(CrearActaConvivenciaVM Accion)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                var accionConvivencia = new EDAccionesActaConvivencia()
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
                var result = ServiceClient.RealizarPeticionesArrayPostJsonRest<EDAccionesActaConvivencia>(UrlServicioParticipacion, GuardarAccionesActaConvivencia, accionConvivencia);
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

        public ActionResult guardarAccionQueja(AccionesActaQuejasVM Accion)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                var accionQueja = new EDAccionesActaQuejas()
                {
                    AccionARealizar = Accion.AccionARealizar,
                    Fk_Id_Queja = Accion.Fk_Id_Queja,
                    PK_Id_Acta = Convert.ToInt32(Accion.PK_Id_Acta),
                    UsuarioSistema = usuarioActual.IdUsuario,
                    NombreUsuario = usuarioActual.NombreUsuario,
                };
                ServiceClient.EliminarParametros();
                var result = ServiceClient.RealizarPeticionesArrayPostJsonRest<EDAccionesActaQuejas>(UrlServicioParticipacion, GuardarAccionesActaQueja, accionQueja);
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

        public ActionResult guardarCompromisoSeguimiento(CompromisosPendientesVM Compromiso)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                var compromisoSeguimiento = new EDCompromisosPendientes()
                {
                    CompromisoPendiente = Compromiso.CompromisoPendiente,
                    FK_Id_Seguimiento = Compromiso.FK_Id_Seguimiento,
                    PK_Id_Acta = Convert.ToInt32(Compromiso.PK_Id_Acta),
                    UsuarioSistema = usuarioActual.IdUsuario,
                    NombreUsuario = usuarioActual.NombreUsuario,
                };
                ServiceClient.EliminarParametros();
                var result = ServiceClient.RealizarPeticionesArrayPostJsonRest<EDCompromisosPendientes>(UrlServicioParticipacion, GuardarCompromisosSeguimiento, compromisoSeguimiento);
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

      public ActionResult guardarResponsableQueja(ResponsablesQuejasVM Responsable)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                var responsableQueja = new EDResponsablesQuejas()
                {
                    Numero_Documento = Responsable.Numero_Documento,
                    Nombre = Responsable.Nombre,
                    Fk_Id_Queja = Responsable.Fk_Id_Queja,
                    PK_Id_Acta = Convert.ToInt32(Responsable.PK_Id_Acta),
                    UsuarioSistema = usuarioActual.IdUsuario,
                    NombreUsuario = usuarioActual.NombreUsuario,
                };
                ServiceClient.EliminarParametros();
                var result = ServiceClient.RealizarPeticionesArrayPostJsonRest<EDResponsablesQuejas>(UrlServicioParticipacion, GuardarResponsablesQueja, responsableQueja);
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

        public ActionResult MiembroActaConvivencia(CrearActaConvivenciaVM Miembro)
        {
            var miembrosActasConvivencia = new CrearActaConvivenciaVM();

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
                var miembrosActa = ServiceClient.ObtenerArrayJsonRestFul<EDMiembrosConvivencia>(UrlServicioParticipacion, ObtenerMiembrosConvivenciaPorActa, RestSharp.Method.GET);
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


        public ActionResult DatosActaConvivencia (int? PK_Id_Acta, int? IdSede)
        {
            var crearActaConvivenciaVM = new CrearActaConvivenciaVM();

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }

            if (PK_Id_Acta == null || PK_Id_Acta == 0)
            {
                var crearActaConvivencia = new CrearActaConvivenciaVM();
                ViewBag.mensaje = "Debe ingresar los miembros para continuar.";
                ServiceClient.EliminarParametros();
                var resultTipoPrincipal = ServiceClient.ObtenerArrayJsonRestFul<EDTipoPrincipalActa>(UrlServicioParticipacion, ObtenerTipoPrincipal, RestSharp.Method.GET);
                if (resultTipoPrincipal != null && resultTipoPrincipal.Count() > 0)
                {
                    crearActaConvivencia.TiposPrincipales = resultTipoPrincipal.Select(cf => new SelectListItem()
                    {
                        Value = cf.Id_TipoPrincipal.ToString(),
                        Text = cf.DescripcionTipoPrincipal,
                    }).ToList();
                }
                else
                {
                    crearActaConvivencia.TiposPrincipales = resultTipoPrincipal.Select(cf => new SelectListItem()
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
                    crearActaConvivencia.TiposPrioridadMiembros = resultTipoPrioridad.Select(cf => new SelectListItem()
                    {
                        Value = cf.Id_TipoPrioridadMiembro.ToString(),
                        Text = cf.DescripcionTipoMiembro,
                    }).ToList();
                }
                else
                {
                    crearActaConvivencia.TiposPrioridadMiembros = resultTipoPrioridad.Select(p => new SelectListItem()
                    {
                        Value = p.Id_TipoPrioridadMiembro.ToString(),
                        Text = p.DescripcionTipoMiembro,
                    }).ToList();

                    ViewBag.mensaje = "Se deben Registrar los Tipos de Prioridad Miembros Actas para continuar";

                }

                crearActaConvivencia.Fk_Id_Sede = Convert.ToInt32(IdSede);

                 return View("CrearActaConvivencia", crearActaConvivencia);
            }

            if (ModelState.IsValid)
            {
                ServiceClient.EliminarParametros();
                ServiceClient.AdicionarParametro("Id_Acta", PK_Id_Acta);
                var resultActaConvivenciaEM = ServiceClient.ObtenerObjetoJsonRestFul<EDActasConvivencia>(UrlServicioParticipacion, ObtenerActasConvivenciaPorId, RestSharp.Method.GET);
                if (resultActaConvivenciaEM != null)
                {
                    crearActaConvivenciaVM.Consecutivo_Acta = resultActaConvivenciaEM.Consecutivo_Acta;
                    crearActaConvivenciaVM.Fecha = resultActaConvivenciaEM.Fecha;
                    crearActaConvivenciaVM.PK_Id_Acta = resultActaConvivenciaEM.PK_Id_Acta;
                    crearActaConvivenciaVM.NombreEmpresa = resultActaConvivenciaEM.NombreEmpresa;
                    crearActaConvivenciaVM.Fk_Id_Sede = resultActaConvivenciaEM.Fk_Id_Sede;
                    crearActaConvivenciaVM.TemaReunion = resultActaConvivenciaEM.TemaReunion;
                    crearActaConvivenciaVM.Conclusiones = resultActaConvivenciaEM.Conclusiones;

                }
                ServiceClient.EliminarParametros();
                ServiceClient.AdicionarParametro("id_acta", PK_Id_Acta);
                var miembrosActa = ServiceClient.ObtenerArrayJsonRestFul<EDMiembrosConvivencia>(UrlServicioParticipacion, ObtenerMiembrosConvivenciaPorActa, RestSharp.Method.GET);
                if (miembrosActa != null)
                {
                    crearActaConvivenciaVM.MiembrosActaConvivencia = miembrosActa.Select(p => new MiembrosActaConvivenciaVM()
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
                var participantesActa = ServiceClient.ObtenerArrayJsonRestFul<EDParticipantes>(UrlServicioParticipacion, ObtenerParticipantesConvivenciaPorActa, RestSharp.Method.GET);
                if (participantesActa != null)
                {
                    crearActaConvivenciaVM.ParticipantesActaConvivencia = participantesActa.Select(p => new ParticipantesActaConvivenciaVM()
                    {
                        Numero_Documento = p.Numero_Documento,
                        Nombre = p.Nombre,
                        Fk_Id_Acta = Convert.ToInt32(p.PK_Id_Acta),
                    }).ToList();

                }

                ServiceClient.EliminarParametros();
                ServiceClient.AdicionarParametro("id_acta", PK_Id_Acta);
                var temasActa = ServiceClient.ObtenerArrayJsonRestFul<EDTemasActasConvivencia>(UrlServicioParticipacion, ObtenerTemasConvivenciaPorActa, RestSharp.Method.GET);
                if (temasActa != null)
                {
                    crearActaConvivenciaVM.TemasActaConvivencia = temasActa.Select(p => new TemasActaConvivenciaVM()
                    {
                        PK_Id_TemaActa = p.PK_Id_TemaActa,
                        Tema = p.Tema,
                        Observaciones = p.Observaciones,
                        Fk_Id_Acta = Convert.ToInt32(p.PK_Id_Acta),
                    }).ToList();

                }

                ServiceClient.EliminarParametros();
                ServiceClient.AdicionarParametro("id_acta", PK_Id_Acta);
                var accionesActa = ServiceClient.ObtenerArrayJsonRestFul<EDAccionesActaConvivencia>(UrlServicioParticipacion, ObtenerAccionesConvivenciaPorActa, RestSharp.Method.GET);
                if (accionesActa != null)
                {
                    crearActaConvivenciaVM.AccionesActaConvivencia = accionesActa.Select(p => new AccionesActaConvivenciaVM()
                    {
                        Pk_Id_AccionActaConvivencia = p.Pk_Id_AccionActaConvivencia,
                        AccionARealizar = p.AccionARealizar,
                        FechaProbable = p.FechaProbable,
                        Responsable = p.Responsable,
                        Fk_Id_Acta = Convert.ToInt32(p.PK_Id_Acta),
                    }).ToList();

                }
            
            }
            return View("DatosActaConvivencia", crearActaConvivenciaVM);
        }

        public ActionResult eliminarMiembroConvivencia(int FK_Id_Acta, int Documento)
        {
            var miembrosActasConvivencia = new CrearActaConvivenciaVM();

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
                var result = ServiceClient.ObtenerArrayJsonRestFul<EDMiembrosConvivencia>(UrlServicioParticipacion, EliminarMiembroActaConvivencia, RestSharp.Method.GET);
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

        public ActionResult eliminarParticipanteConvivencia(int FK_Id_Acta, int Documento)
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
                var result = ServiceClient.ObtenerArrayJsonRestFul<EDParticipantesActaConvivencia>(UrlServicioParticipacion, EliminarParticipanteActaConvivencia, RestSharp.Method.GET);
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

        public ActionResult eliminarResponsableQueja(int Fk_Id_Queja, int Documento, int PK_Id_Acta)
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
                ServiceClient.AdicionarParametro("Fk_Id_Queja", Fk_Id_Queja);
                ServiceClient.AdicionarParametro("PK_Id_Acta", PK_Id_Acta);
                var result = ServiceClient.ObtenerArrayJsonRestFul<EDResponsablesQuejas>(UrlServicioParticipacion, EliminarResponsableQueja, RestSharp.Method.GET);
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

        public ActionResult eliminarTemaConvivencia(int FK_Id_Acta, int PK_Id_TemaActa)
        {
            var miembrosActasConvivencia = new CrearActaConvivenciaVM();

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
                var result = ServiceClient.ObtenerArrayJsonRestFul<EDTemasActasConvivencia>(UrlServicioParticipacion, EliminarTemaActaConvivencia, RestSharp.Method.GET);
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
        public ActionResult eliminarAccionConvivencia(int FK_Id_Acta, int Pk_Id_AccionActaConvivencia)
        {
            var miembrosActasConvivencia = new CrearActaConvivenciaVM();

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }

            if (ModelState.IsValid)
            {
                ServiceClient.EliminarParametros();
                ServiceClient.AdicionarParametro("Pk_Id_AccionActaConvivencia", Pk_Id_AccionActaConvivencia);
                ServiceClient.AdicionarParametro("Usuario", usuarioActual.IdUsuario);
                ServiceClient.AdicionarParametro("NombreUsuario", usuarioActual.NombreUsuario);
                ServiceClient.AdicionarParametro("PK_Id_Acta", FK_Id_Acta);
                var result = ServiceClient.ObtenerArrayJsonRestFul<EDAccionesActaConvivencia>(UrlServicioParticipacion, EliminarAccionActaConvivencia, RestSharp.Method.GET);
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
        public ActionResult eliminarAccionQueja(int Fk_Id_Queja, int Pk_Id_AccionQueja, int PK_Id_Acta)
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
                ServiceClient.AdicionarParametro("Pk_Id_AccionQueja", Pk_Id_AccionQueja);
                ServiceClient.AdicionarParametro("Usuario", usuarioActual.IdUsuario);
                ServiceClient.AdicionarParametro("NombreUsuario", usuarioActual.NombreUsuario);
                ServiceClient.AdicionarParametro("Fk_Id_Queja", Fk_Id_Queja);
                ServiceClient.AdicionarParametro("PK_Id_Acta", PK_Id_Acta);
                var result = ServiceClient.ObtenerArrayJsonRestFul<EDAccionesActaQuejas>(UrlServicioParticipacion, EliminarAccionActaQueja, RestSharp.Method.GET);
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
        public ActionResult eliminarCompromisoSeguimiento(int FK_Id_Seguimiento, int Pk_Id_Compromiso, int PK_Id_Acta)
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
                ServiceClient.AdicionarParametro("Pk_Id_Compromiso", Pk_Id_Compromiso);
                ServiceClient.AdicionarParametro("Usuario", usuarioActual.IdUsuario);
                ServiceClient.AdicionarParametro("NombreUsuario", usuarioActual.NombreUsuario);
                ServiceClient.AdicionarParametro("FK_Id_Seguimiento", FK_Id_Seguimiento);
                ServiceClient.AdicionarParametro("PK_Id_Acta", PK_Id_Acta);
                var result = ServiceClient.ObtenerArrayJsonRestFul<EDCompromisosPendientes>(UrlServicioParticipacion, EliminarCompromisoSeguimiento, RestSharp.Method.GET);
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
        public ActionResult MiembroActaConvivenciaGuardados(int? PK_Id_Acta, int? IdSede, int consecutivo_acta)
        {
            var miembrosActasConvivencia = new CrearActaConvivenciaVM();

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
                var miembrosActa = ServiceClient.ObtenerArrayJsonRestFul<EDMiembrosConvivencia>(UrlServicioParticipacion, ObtenerMiembrosConvivenciaPorActa, RestSharp.Method.GET);
                if (miembrosActa != null)
                {
                    ServiceClient.EliminarParametros();
                    var resultTipoPrincipal = ServiceClient.ObtenerArrayJsonRestFul<EDTipoPrincipalActa>(UrlServicioParticipacion, ObtenerTipoPrincipal, RestSharp.Method.GET);
                    if (resultTipoPrincipal != null && resultTipoPrincipal.Count() > 0)
                    {
                        miembrosActasConvivencia.TiposPrincipales = resultTipoPrincipal.Select(cf => new SelectListItem()
                        {
                            Value = cf.Id_TipoPrincipal.ToString(),
                            Text = cf.DescripcionTipoPrincipal,
                        }).ToList();
                    }
                    else
                    {
                        miembrosActasConvivencia.TiposPrincipales = resultTipoPrincipal.Select(cf => new SelectListItem()
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
                        miembrosActasConvivencia.TiposPrioridadMiembros = resultTipoPrioridad.Select(cf => new SelectListItem()
                        {
                            Value = cf.Id_TipoPrioridadMiembro.ToString(),
                            Text = cf.DescripcionTipoMiembro,
                        }).ToList();
                    }
                    else
                    {
                        miembrosActasConvivencia.TiposPrioridadMiembros = resultTipoPrioridad.Select(p => new SelectListItem()
                        {
                            Value = p.Id_TipoPrioridadMiembro.ToString(),
                            Text = p.DescripcionTipoMiembro,
                        }).ToList();

                        ViewBag.mensaje = "Se deben Registrar los Tipos de Prioridad Miembros Actas para continuar";

                    }
                    miembrosActasConvivencia.MiembrosActaConvivencia = miembrosActa.Select(cf => new MiembrosActaConvivenciaVM()
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
                    
                    miembrosActasConvivencia.Fk_Id_Sede = Convert.ToInt32(IdSede);
                    miembrosActasConvivencia.Consecutivo_Acta = consecutivo_acta;
                    miembrosActasConvivencia.Fk_Id_Acta = Convert.ToInt32(PK_Id_Acta);
                    miembrosActasConvivencia.PK_Id_Acta = Convert.ToInt32(PK_Id_Acta);

                    return View(miembrosActasConvivencia);

                }
                else
                {
                    return View(miembrosActasConvivencia);
                }
            }
            else
            {
                return View(miembrosActasConvivencia);
            }
        }

        //Carga Archivos PDF
        public ActionResult CargarActaConvivencia(HttpPostedFileBase NombreArchivo, int? PK_Id_Acta, int? Consecutivo_Acta, int? Fk_Id_Sede)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }

            var crearActaConvivenciaVM = new CrearActaConvivenciaVM();

            //crearActaConvivenciaVM.Fk_Id_Sede = Convert.ToInt32(IdSede);

            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("Id_Acta", PK_Id_Acta);
            var resultActaConvivenciaEM = ServiceClient.ObtenerObjetoJsonRestFul<EDActasConvivencia>(UrlServicioParticipacion, ObtenerActasConvivenciaPorId, RestSharp.Method.GET);
            if (resultActaConvivenciaEM != null)
            {
                crearActaConvivenciaVM.Consecutivo_Acta = resultActaConvivenciaEM.Consecutivo_Acta;
                crearActaConvivenciaVM.Fecha = resultActaConvivenciaEM.Fecha;
                crearActaConvivenciaVM.PK_Id_Acta = resultActaConvivenciaEM.PK_Id_Acta;
                crearActaConvivenciaVM.NombreEmpresa = resultActaConvivenciaEM.NombreEmpresa;
                crearActaConvivenciaVM.Fk_Id_Sede = resultActaConvivenciaEM.Fk_Id_Sede;
                crearActaConvivenciaVM.TemaReunion = resultActaConvivenciaEM.TemaReunion;
                crearActaConvivenciaVM.Conclusiones = resultActaConvivenciaEM.Conclusiones;

            }

            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("id_acta", PK_Id_Acta);
            var miembrosActa = ServiceClient.ObtenerArrayJsonRestFul<EDMiembrosConvivencia>(UrlServicioParticipacion, ObtenerMiembrosConvivenciaPorActa, RestSharp.Method.GET);
            if (miembrosActa != null)
            {
                crearActaConvivenciaVM.MiembrosActaConvivencia = miembrosActa.Select(p => new MiembrosActaConvivenciaVM()
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
            var participantesActa = ServiceClient.ObtenerArrayJsonRestFul<EDParticipantes>(UrlServicioParticipacion, ObtenerParticipantesConvivenciaPorActa, RestSharp.Method.GET);
            if (participantesActa != null)
            {
                crearActaConvivenciaVM.ParticipantesActaConvivencia = participantesActa.Select(p => new ParticipantesActaConvivenciaVM()
                {
                    Numero_Documento = p.Numero_Documento,
                    Nombre = p.Nombre,
                    Fk_Id_Acta = Convert.ToInt32(p.PK_Id_Acta),
                }).ToList();

            }

            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("id_acta", PK_Id_Acta);
            var temasActa = ServiceClient.ObtenerArrayJsonRestFul<EDTemasActasConvivencia>(UrlServicioParticipacion, ObtenerTemasConvivenciaPorActa, RestSharp.Method.GET);
            if (temasActa != null)
            {
                crearActaConvivenciaVM.TemasActaConvivencia = temasActa.Select(p => new TemasActaConvivenciaVM()
                {
                    PK_Id_TemaActa = p.PK_Id_TemaActa,
                    Tema = p.Tema,
                    Observaciones = p.Observaciones,
                    Fk_Id_Acta = Convert.ToInt32(p.PK_Id_Acta),
                }).ToList();

            }

            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("id_acta", PK_Id_Acta);
            var accionesActa = ServiceClient.ObtenerArrayJsonRestFul<EDAccionesActaConvivencia>(UrlServicioParticipacion, ObtenerAccionesConvivenciaPorActa, RestSharp.Method.GET);
            if (accionesActa != null)
            {
                crearActaConvivenciaVM.AccionesActaConvivencia = accionesActa.Select(p => new AccionesActaConvivenciaVM()
                {
                    Pk_Id_AccionActaConvivencia = p.Pk_Id_AccionActaConvivencia,
                    AccionARealizar = p.AccionARealizar,
                    FechaProbable = p.FechaProbable,
                    Responsable = p.Responsable,
                    Fk_Id_Acta = Convert.ToInt32(p.PK_Id_Acta),
                }).ToList();

            }

            if (ModelState.IsValid)
            {


                EDActasConvivencia ImpActaConvivencia = new EDActasConvivencia();
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
                            return View("DatosActaConvivencia", crearActaConvivenciaVM);
                        }

                    }

                     ImpActaConvivencia.Fk_Id_UsuarioSistema = usuarioActual.IdUsuario;
                    ImpActaConvivencia.NombreUsuario = usuarioActual.NombreUsuario;
                    ImpActaConvivencia.Consecutivo_Acta = Convert.ToInt32(Consecutivo_Acta);
                    ImpActaConvivencia.PK_Id_Acta = Convert.ToInt32(PK_Id_Acta);
                    ImpActaConvivencia.Fk_Id_Sede = Convert.ToInt32(Fk_Id_Sede);
                    ImpActaConvivencia.NombreArchivo = NombreArchivo.FileName;

                    ServiceClient.EliminarParametros();
                    var respuesta = ServiceClient.RealizarPeticionesPostJsonRestFul<EDActasConvivencia>(UrlServicioParticipacion, ImportarActaConvivencia, ImpActaConvivencia);
                    if (respuesta != null)
                    {
                        ViewBag.mensaje1 = "El archivo ha sido Importado.";
                        return View("DatosActaConvivencia", crearActaConvivenciaVM);
                    }
                    else
                    {
                        ViewBag.mensaje = "El archivo no ha sido Importado.";
                        return View("DatosActaConvivencia", crearActaConvivenciaVM);
                    }
                }
                else
                {
                    ViewBag.mensaje =  "Seleccione un archivo para Importar por favor" ;
                    return View("DatosActaConvivencia", crearActaConvivenciaVM);
                }

            }

            ViewBag.mensaje =  "ERROR";
            return View("DatosActaConvivencia", crearActaConvivenciaVM);
         }

        //Visualiza Archivo PDF
        public FileStreamResult VisualizarActaConvivenciaPDF(int Pk_Id_Acta)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
            }
            
               ServiceClient.EliminarParametros();
               ServiceClient.AdicionarParametro("Id_Acta", Pk_Id_Acta);
                var resultActaConvivenciaEM = ServiceClient.ObtenerObjetoJsonRestFul<EDActasConvivencia>(UrlServicioParticipacion, ObtenerActasConvivenciaPorId, RestSharp.Method.GET);
                if (resultActaConvivenciaEM != null)
                {
                    var path = Server.MapPath(rutaArchivosActas);
                    var file = resultActaConvivenciaEM.NombreArchivo;
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
        public ActionResult ActualizarActaConvivencia(EDActasConvivencia actaConvivencia)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }

            if (ModelState.IsValid)
            {

            EDActasConvivencia InformacionActa = new EDActasConvivencia();
  
                InformacionActa.Fk_Id_UsuarioSistema = usuarioActual.IdUsuario;
                InformacionActa.NombreUsuario = usuarioActual.NombreUsuario;
                InformacionActa.Fk_Id_Sede = actaConvivencia.Fk_Id_Sede;
                InformacionActa.Consecutivo_Acta = actaConvivencia.Consecutivo_Acta;
                InformacionActa.Fecha = actaConvivencia.Fecha;
                InformacionActa.TemaReunion = actaConvivencia.TemaReunion;
                InformacionActa.Conclusiones = actaConvivencia.Conclusiones;
                InformacionActa.PK_Id_Acta = actaConvivencia.PK_Id_Acta;

                ServiceClient.EliminarParametros();
                var respuesta = ServiceClient.RealizarPeticionesPostJsonRestFul<EDActasConvivencia>(UrlServicioParticipacion, CapacidadActualizarActaConvivencia, InformacionActa);
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
        
       //Actualizar los Datos del Seguimiento a Quejas
        public ActionResult ActualizarQueja(EDActaConvivenciaQuejas acta)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }

            if (ModelState.IsValid)
            {

                EDActaConvivenciaQuejas InformacionActa = new EDActaConvivenciaQuejas();
  
                InformacionActa.UsuarioSistema = usuarioActual.IdUsuario;
                InformacionActa.NombreUsuario = usuarioActual.NombreUsuario;
                InformacionActa.Fecha = acta.Fecha;
                InformacionActa.NombreRefiereSituacion = acta.NombreRefiereSituacion;
                InformacionActa.AspectosNoResueltos = acta.AspectosNoResueltos;
                InformacionActa.Compromisos = acta.Compromisos;
                InformacionActa.Fk_Id_Acta = acta.Fk_Id_Acta;
                InformacionActa.PK_Id_Queja = acta.PK_Id_Queja;

                ServiceClient.EliminarParametros();
                var respuesta = ServiceClient.RealizarPeticionesPostJsonRestFul<EDActaConvivenciaQuejas>(UrlServicioParticipacion, CapacidadActualizarActaConvivenciaQueja, InformacionActa);
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
         
       //Actualizar los Datos del Seguimiento a Actas Convivencia
        public ActionResult ActualizarSeguimiento(EDSeguimientoActaConvivencia seguimiento)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }

            if (ModelState.IsValid)
            {

                EDSeguimientoActaConvivencia InformacionActa = new EDSeguimientoActaConvivencia();
  
                InformacionActa.UsuarioSistema = usuarioActual.IdUsuario;
                InformacionActa.NombreUsuario = usuarioActual.NombreUsuario;
                InformacionActa.Fecha = seguimiento.Fecha;
                InformacionActa.NombreParteInvolucrada = seguimiento.NombreParteInvolucrada;
                InformacionActa.CompromisosAdquiridos = seguimiento.CompromisosAdquiridos;
                InformacionActa.Observaciones = seguimiento.Observaciones;
                InformacionActa.Fk_Id_Acta = seguimiento.Fk_Id_Acta;
                InformacionActa.PK_Id_Seguimiento = seguimiento.PK_Id_Seguimiento;

                ServiceClient.EliminarParametros();
                var respuesta = ServiceClient.RealizarPeticionesPostJsonRestFul<EDSeguimientoActaConvivencia>(UrlServicioParticipacion, CapacidadActualizarActaConvivenciaSeguimiento, InformacionActa);
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
        public ActionResult actualizarTemaConvivencia(TemasActaConvivenciaVM temaActaConvivencia)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }

            if (ModelState.IsValid)
            {

                EDTemasActasConvivencia observacionTema = new EDTemasActasConvivencia();
  
                observacionTema.Fk_Id_UsuarioSistema = usuarioActual.IdUsuario;
                observacionTema.NombreUsuario = usuarioActual.NombreUsuario;
                observacionTema.PK_Id_TemaActa = temaActaConvivencia.PK_Id_TemaActa;
                observacionTema.PK_Id_Acta = temaActaConvivencia.Fk_Id_Acta;
                observacionTema.Observaciones = temaActaConvivencia.Observaciones;

                ServiceClient.EliminarParametros();
                var respuesta = ServiceClient.RealizarPeticionesArrayPostJsonRest<EDTemasActasConvivencia>(UrlServicioParticipacion, ActualizarTemasActaConvivencia, observacionTema);
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

        public ActionResult ActaQueja_PDF(int PK_Id_Acta, int IdSede)
        {
            ActaConvivenciaQuejasVM actaConvivenciaQuejasVM = new ActaConvivenciaQuejasVM();

           var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
           if (usuarioActual == null)
           {
               ViewBag.mensaje = "Debe Registrarse para Ingresar a este Modulo.";
               return RedirectToAction("Login", "Home");
           }

           ServiceClient.EliminarParametros();
           ServiceClient.AdicionarParametro("IdSede", IdSede);
           var resultActaConvivenciaQueja = ServiceClient.ObtenerArrayJsonRestFul<EDActaConvivenciaQuejas>(UrlServicioParticipacion, ObtenerActasConvivenciaQueja, RestSharp.Method.GET);
           if (resultActaConvivenciaQueja.Count() > 0)
           {
                
                var actaQuejaGuardada = (from aq in resultActaConvivenciaQueja
                                             where aq.Fk_Id_Acta == PK_Id_Acta
                                             select aq);

                if (actaQuejaGuardada.Count() > 0)
                {
                    actaConvivenciaQuejasVM.PK_Id_Queja = actaQuejaGuardada.First().PK_Id_Queja;
                    actaConvivenciaQuejasVM.Consecutivo_Caso = actaQuejaGuardada.First().Consecutivo_Caso;
                    actaConvivenciaQuejasVM.Consecutivo_Queja = actaQuejaGuardada.First().Consecutivo_Queja; ;
                    actaConvivenciaQuejasVM.Fecha = actaQuejaGuardada.First().Fecha;
                    actaConvivenciaQuejasVM.PK_Id_Acta = actaQuejaGuardada.First().Fk_Id_Acta;
                    actaConvivenciaQuejasVM.IdSede = actaQuejaGuardada.First().Fk_Id_Sede;
                    actaConvivenciaQuejasVM.NombreRefiereSituacion = actaQuejaGuardada.First().NombreRefiereSituacion;
                    actaConvivenciaQuejasVM.AspectosNoResueltos = actaQuejaGuardada.First().AspectosNoResueltos;
                    actaConvivenciaQuejasVM.Compromisos = actaQuejaGuardada.First().Compromisos;

                    ServiceClient.EliminarParametros();
                    ServiceClient.AdicionarParametro("PK_Id_Queja", actaQuejaGuardada.First().PK_Id_Queja);
                    var resultAccionesQueja = ServiceClient.ObtenerArrayJsonRestFul<EDAccionesActaQuejas>(UrlServicioParticipacion, ObtenerAccionesActasQueja, RestSharp.Method.GET);
                    if (resultAccionesQueja.Count() > 0)
                    {
                        actaConvivenciaQuejasVM.AccionesActaQuejas = resultAccionesQueja.Select(r => new AccionesActaQuejasVM
                        {
                            Pk_Id_AccionQueja = r.Pk_Id_AccionQueja,
                            AccionARealizar = r.AccionARealizar,
                            Fk_Id_Queja = r.Fk_Id_Queja,
                        }).ToList();
                    }

                    ServiceClient.EliminarParametros();
                    ServiceClient.AdicionarParametro("PK_Id_Queja", actaQuejaGuardada.First().PK_Id_Queja);
                    var resultResponsablesQueja = ServiceClient.ObtenerArrayJsonRestFul<EDResponsablesQuejas>(UrlServicioParticipacion, ObtenerResponsablesQueja, RestSharp.Method.GET);
                    if (resultResponsablesQueja.Count() > 0)
                    {
                        actaConvivenciaQuejasVM.ResponsablesQuejas = resultResponsablesQueja.Select(r => new ResponsablesQuejasVM
                        {
                            Pk_Id_Responsable = r.Pk_Id_Responsable,
                            Numero_Documento = r.Numero_Documento,
                            Nombre = r.Nombre,
                            Fk_Id_Queja = r.Fk_Id_Queja
                        }).ToList();
                    }                 
                }

            }

           string EncodedRazonSocial = System.Net.WebUtility.UrlEncode(usuarioActual.RazonSocialEmpresa);
           string EncodedNit = System.Net.WebUtility.UrlEncode(usuarioActual.NitEmpresa);
           var footurl = "https://alissta.gov.co/Acciones/Footer";
           var headerurl = "https://alissta.gov.co/Acciones/Header?NombreEmpresa=" + EncodedRazonSocial + "&NitEmpresa=" + EncodedNit;

           string cusomtSwitches = string.Format("--footer-line --print-media-type --allow {0} --footer-html {0} --header-line --print-media-type --allow {1} --header-html {1} --header-spacing 5", footurl, headerurl);
           return new Rotativa.PartialViewAsPdf("DatosQuejasPDF", actaConvivenciaQuejasVM) { FileName = "SeguimientoQueja_No." + actaConvivenciaQuejasVM.PK_Id_Queja + ".pdf", CustomSwitches = cusomtSwitches };

        }

        public ActionResult Seguimiento_PDF(int PK_Id_Acta, int IdSede)
        {
            SeguimientoActaConvivenciaVM seguimientoActaConvivenciaVM = new SeguimientoActaConvivenciaVM();

           var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
           if (usuarioActual == null)
           {
               ViewBag.mensaje = "Debe Registrarse para Ingresar a este Modulo.";
               return RedirectToAction("Login", "Home");
           }

           ServiceClient.EliminarParametros();
           ServiceClient.AdicionarParametro("IdSede", IdSede);
           var resultSeguimiento = ServiceClient.ObtenerArrayJsonRestFul<EDSeguimientoActaConvivencia>(UrlServicioParticipacion, ObtenerActasConvivenciaSeguimiento, RestSharp.Method.GET);
           if (resultSeguimiento.Count() > 0)
           {
                
                var seguimientoGuardado = (from aq in resultSeguimiento
                                             where aq.Fk_Id_Acta == PK_Id_Acta
                                             select aq);

                if (seguimientoGuardado.Count() > 0)
                {
                    seguimientoActaConvivenciaVM.PK_Id_Seguimiento = seguimientoGuardado.First().PK_Id_Seguimiento;
                    seguimientoActaConvivenciaVM.Consecutivo_Evento = seguimientoGuardado.First().Consecutivo_Evento;
                    seguimientoActaConvivenciaVM.Fecha = seguimientoGuardado.First().Fecha;
                    seguimientoActaConvivenciaVM.PK_Id_Acta = seguimientoGuardado.First().Fk_Id_Acta;
                    seguimientoActaConvivenciaVM.IdSede = seguimientoGuardado.First().Fk_Id_Sede;
                    seguimientoActaConvivenciaVM.NombreParteInvolucrada = seguimientoGuardado.First().NombreParteInvolucrada;
                    seguimientoActaConvivenciaVM.CompromisosAdquiridos = seguimientoGuardado.First().CompromisosAdquiridos;
                    seguimientoActaConvivenciaVM.Observaciones = seguimientoGuardado.First().Observaciones;

                    ServiceClient.EliminarParametros();
                    ServiceClient.AdicionarParametro("PK_Id_Seguimiento", seguimientoGuardado.First().PK_Id_Seguimiento);
                    var resultResponsablesQueja = ServiceClient.ObtenerArrayJsonRestFul<EDCompromisosPendientes>(UrlServicioParticipacion, ObtenerCompromisosActaConvivencia, RestSharp.Method.GET);
                    if (resultResponsablesQueja.Count() > 0)
                    {
                        seguimientoActaConvivenciaVM.CompromisosPendientes = resultResponsablesQueja.Select(r => new CompromisosPendientesVM
                        {
                            Pk_Id_Compromiso = r.Pk_Id_Compromiso,
                            CompromisoPendiente = r.CompromisoPendiente,
                            FK_Id_Seguimiento = r.FK_Id_Seguimiento,
                            PK_Id_Acta = r.PK_Id_Acta
                        }).ToList();
                    }                 
                }

            }

           string EncodedRazonSocial = System.Net.WebUtility.UrlEncode(usuarioActual.RazonSocialEmpresa);
           string EncodedNit = System.Net.WebUtility.UrlEncode(usuarioActual.NitEmpresa);
           var footurl = "https://alissta.gov.co/Acciones/Footer";
           var headerurl = "https://alissta.gov.co/Acciones/Header?NombreEmpresa=" + EncodedRazonSocial + "&NitEmpresa=" + EncodedNit;

            string cusomtSwitches = string.Format("--footer-line --print-media-type --allow {0} --footer-html {0} --header-line --print-media-type --allow {1} --header-html {1} --header-spacing 5", footurl, headerurl);
            return new Rotativa.PartialViewAsPdf("DatosSeguimientoPDF", seguimientoActaConvivenciaVM) { FileName = "SeguimientoActa_No." + seguimientoActaConvivenciaVM.Consecutivo_Evento + ".pdf", CustomSwitches = cusomtSwitches };
        }


        public ActionResult ActaConvivencia_PDF(int PK_Id_Acta)
        {
           var crearActaConvivenciaVM = new CrearActaConvivenciaVM();

           var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
           if (usuarioActual == null)
           {
               ViewBag.mensaje = "Debe Registrarse para Ingresar a este Modulo.";
               return RedirectToAction("Login", "Home");
           }
           
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("Id_Acta", PK_Id_Acta);
            var resultActaConvivenciaEM = ServiceClient.ObtenerObjetoJsonRestFul<EDActasConvivencia>(UrlServicioParticipacion, ObtenerActasConvivenciaPorId, RestSharp.Method.GET);
            if (resultActaConvivenciaEM != null)
            {
                crearActaConvivenciaVM.Consecutivo_Acta = resultActaConvivenciaEM.Consecutivo_Acta;
                crearActaConvivenciaVM.Fecha = resultActaConvivenciaEM.Fecha;
                crearActaConvivenciaVM.PK_Id_Acta = resultActaConvivenciaEM.PK_Id_Acta;
                crearActaConvivenciaVM.NombreEmpresa = resultActaConvivenciaEM.NombreEmpresa;
                crearActaConvivenciaVM.Fk_Id_Sede = resultActaConvivenciaEM.Fk_Id_Sede;
                crearActaConvivenciaVM.TemaReunion = resultActaConvivenciaEM.TemaReunion;
                crearActaConvivenciaVM.Conclusiones = resultActaConvivenciaEM.Conclusiones;

            }

            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("id_acta", PK_Id_Acta);
            var miembrosActa = ServiceClient.ObtenerArrayJsonRestFul<EDMiembrosConvivencia>(UrlServicioParticipacion, ObtenerMiembrosConvivenciaPorActa, RestSharp.Method.GET);
            if (miembrosActa != null)
            {
                crearActaConvivenciaVM.MiembrosActaConvivencia = miembrosActa.Select(p => new MiembrosActaConvivenciaVM()
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
            var participantesActa = ServiceClient.ObtenerArrayJsonRestFul<EDParticipantes>(UrlServicioParticipacion, ObtenerParticipantesConvivenciaPorActa, RestSharp.Method.GET);
            if (participantesActa != null)
            {
                crearActaConvivenciaVM.ParticipantesActaConvivencia = participantesActa.Select(p => new ParticipantesActaConvivenciaVM()
                {
                    Numero_Documento = p.Numero_Documento,
                    Nombre = p.Nombre,
                    Fk_Id_Acta = Convert.ToInt32(p.PK_Id_Acta),
                }).ToList();

            }

            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("id_acta", PK_Id_Acta);
            var temasActa = ServiceClient.ObtenerArrayJsonRestFul<EDTemasActasConvivencia>(UrlServicioParticipacion, ObtenerTemasConvivenciaPorActa, RestSharp.Method.GET);
            if (temasActa != null)
            {
                crearActaConvivenciaVM.TemasActaConvivencia = temasActa.Select(p => new TemasActaConvivenciaVM()
                {
                    PK_Id_TemaActa = p.PK_Id_TemaActa,
                    Tema = p.Tema,
                    Observaciones = p.Observaciones,
                    Fk_Id_Acta = Convert.ToInt32(p.PK_Id_Acta),
                }).ToList();

            }

            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("id_acta", PK_Id_Acta);
            var accionesActa = ServiceClient.ObtenerArrayJsonRestFul<EDAccionesActaConvivencia>(UrlServicioParticipacion, ObtenerAccionesConvivenciaPorActa, RestSharp.Method.GET);
            if (accionesActa != null)
            {
                crearActaConvivenciaVM.AccionesActaConvivencia = accionesActa.Select(p => new AccionesActaConvivenciaVM()
                {
                    Pk_Id_AccionActaConvivencia = p.Pk_Id_AccionActaConvivencia,
                    AccionARealizar = p.AccionARealizar,
                    FechaProbable = p.FechaProbable,
                    Responsable = p.Responsable,
                    Fk_Id_Acta = Convert.ToInt32(p.PK_Id_Acta),
                }).ToList();

            }

            string EncodedRazonSocial = System.Net.WebUtility.UrlEncode(usuarioActual.RazonSocialEmpresa);
            string EncodedNit = System.Net.WebUtility.UrlEncode(usuarioActual.NitEmpresa);
            var footurl = "https://alissta.gov.co/Acciones/Footer";
            var headerurl = "https://alissta.gov.co/Acciones/Header?NombreEmpresa=" + EncodedRazonSocial + "&NitEmpresa=" + EncodedNit;

            string cusomtSwitches = string.Format("--footer-line --print-media-type --allow {0} --footer-html {0} --header-line --print-media-type --allow {1} --header-html {1} --header-spacing 5", footurl, headerurl);
            return new Rotativa.PartialViewAsPdf("ActaConvivenciaPDF", crearActaConvivenciaVM) { FileName = "ActaConvivencia_No." + crearActaConvivenciaVM.Consecutivo_Acta + ".pdf", CustomSwitches = cusomtSwitches };
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