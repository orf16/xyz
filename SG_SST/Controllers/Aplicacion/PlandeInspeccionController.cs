using RestSharp;
using SG_SST.Controllers.Base;
using SG_SST.EntidadesDominio.Aplicacion;
using SG_SST.EntidadesDominio.Empleado;
using SG_SST.EntidadesDominio.Empresas;
using SG_SST.EntidadesDominio.MedicionEvaluacion;
using SG_SST.EntidadesDominio.Planificacion;
using SG_SST.Logica.Aplicacion;
using SG_SST.Logica.Empresas;
using SG_SST.Logica.MedicionEvaluacion;
using SG_SST.Models;
using SG_SST.Models.Aplicacion;
using SG_SST.Models.Ausentismo;
using SG_SST.Models.Empleado;
using SG_SST.Models.MedicionEvaluacion;
using SG_SST.Models.Planificacion;
using SG_SST.ServiceRequest;
using SG_SST.Services.Planificacion.IServices;
using SG_SST.Services.Planificacion.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.IO;
using SG_SST.Audotoria;


namespace SG_SST.Controllers.Aplicacion
{
    public class PlandeInspeccionController : BaseController
    {
        private SG_SSTContext db = new SG_SSTContext();
        SG_SSTContext context = new SG_SSTContext();
        //Poner Aca todas las logicas de Negocio.
        LNInspeccion insp = new LNInspeccion();
        LNAcciones LNAcciones = new LNAcciones();
        LNProcesos LNProcesos = new LNProcesos();
        LNEmpresa LNEmpresa = new LNEmpresa();
        LNAdmoEMH LNAdmoEMH = new LNAdmoEMH();
        IClasificacionDePeligrosServicios clasificacionDePeligrosServicios = new ClasificacionDePeligrosServicios();
        private static string RutaFirmas = "~/Content/ArchivosAccionesCP/FirmasAcciones/";
        private static string RutaArchivosBD = "~/Content/ArchivosAccionesCP/ArchivosAcciones/";
        private static string RutaArchivosTemporales = "~/Content/ArchivosAccionesCP/ArchivosTemporales/";
        private static string SrcWhite = "data:image/png;base64,R0lGODlhAQABAAAAACH5BAEKAAEALAAAAAABAAEAAAICTAEAOw==";

        //Parametros WebConfig
       // string urlPlanificacion = ConfigurationManager.AppSettings["UrlServicioPlanificacion"];
        
        string UrlServicioAplicacion = ConfigurationManager.AppSettings["UrlServicioAplicacion"];
        string urlServicioEmpresas = ConfigurationManager.AppSettings["UrlServicioEmpresas"];
        string CapacidadObtenerTipoInspeccion = ConfigurationManager.AppSettings["CapacidadObtenerTipoInspeccion"];
        string CapacidadObtenerTiposPeligros = ConfigurationManager.AppSettings["CapacidadObtenerTiposPeligros"];
        string CapacidadGuardarPlanInspeccion = ConfigurationManager.AppSettings["CapacidadGuardarPlanInspeccion"];
        string CapacidadObtenerSedesPorNit = ConfigurationManager.AppSettings["CapacidadObtenerSedesPorNit"];
        string CapacidadObtenerprocesosEmpresa = ConfigurationManager.AppSettings["CapacidadObtenerprocesosEmpresa"];
        string CapacidadObtenerPlanInspeccion = ConfigurationManager.AppSettings["CapacidadObtenerPlanInspeccion"];
        string CapacidadObtenerConfiguracionPrioridades = ConfigurationManager.AppSettings["CapacidadObtenerConfiguracionPrioridades"];
        string CapacidadGuardarInspeccion = ConfigurationManager.AppSettings["CapacidadGuardarInspeccion"];
        string CapacidadObtenerConfiguracionInspeccion = ConfigurationManager.AppSettings["CapacidadObtenerConfiguracionInspeccion"];
        string ObtenerInspeccionRangoFechas = ConfigurationManager.AppSettings["ObtenerInspeccionRangoFechas"];
        string capacidadTiposDePeligros = ConfigurationManager.AppSettings["CapacidadTipoDePeligros"];
        string GuardarCondicionInsegura = ConfigurationManager.AppSettings["GuardarCondicionInsegura"];
        string ObtenerCondicionesporInspeccion = ConfigurationManager.AppSettings["ObtenerCondicionesporInspeccion"];
        string GuardarPlanAccion = ConfigurationManager.AppSettings["GuardarPlanAccion"];
        string ObtenerPlanAccionInspeccion = ConfigurationManager.AppSettings["ObtenerPlanAccionInspeccion"];
        string GuardarPlanCorrectivo = ConfigurationManager.AppSettings["GuardarPlanCorrectivo"];
        string ObtenerParaCorrectiva = ConfigurationManager.AppSettings["ObtenerParaCorrectiva"];
        string ObtenerInspeccionTipo = ConfigurationManager.AppSettings["ObtenerInspeccionTipo"];
        string EditarCondicion = ConfigurationManager.AppSettings["EditarCondicion"];
        string ObtenerTodasCorrectivas = ConfigurationManager.AppSettings["ObtenerTodasCorrectivas"];
        string ObtenerInspeccionesEmpresa = ConfigurationManager.AppSettings["ObtenerInspeccionesEmpresa"];
        string ObtenerPlanporempresa = ConfigurationManager.AppSettings["ObtenerPlanporempresa"];
        string ObtenerPlanporempresase = ConfigurationManager.AppSettings["ObtenerPlanporempresase"];
        string EjecutarPlan = ConfigurationManager.AppSettings["EjecutarPlan"];
        string ContinuarEjecucion = ConfigurationManager.AppSettings["ContinuarEjecucion"];
        string ObtenerprocesosEmpresaprnivel = ConfigurationManager.AppSettings["ObtenerprocesosEmpresaprnivel"];
        string ObtenerInspeccionNoEjecutada = ConfigurationManager.AppSettings["ObtenerInspeccionNoEjecutada"];
        string EliminarInspecciones = ConfigurationManager.AppSettings["EliminarInspecciones"];
        string CapacidadObtenerConfiguracionInspeccionID = ConfigurationManager.AppSettings["CapacidadObtenerConfiguracionInspeccionID"];
        string EliminarPlaneacion = ConfigurationManager.AppSettings["EliminarPlaneacion"];
        string ObtenerCondicionInsegura = ConfigurationManager.AppSettings["ObtenerCondicionInsegura"];
        string ObtenerInfoInspeccion = ConfigurationManager.AppSettings["ObtenerInfoInspeccion"];
        string DocumentosInspecciones = ConfigurationManager.AppSettings["DocumentosInspecciones"];
        string EliminarCondicionI = ConfigurationManager.AppSettings["EliminarCondicionI"];
     
        





        /// <summary>
        /// Metodo para retornar a la vista los tipos de Inspeccion
        /// </summary>
        /// <returns>listp</returns>
        public ActionResult Index()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("NIT", usuarioActual.NitEmpresa);
            var resulta = ServiceClient.ObtenerArrayJsonRestFul<EDTipoInspeccion>(UrlServicioAplicacion, CapacidadObtenerTipoInspeccion, RestSharp.Method.GET);
            var listp = new CrearInspeccionModel();
            if (resulta != null)
            {
                listp.tiposinspeccion = resulta.Select(ti => new InspeccionModel()
                {
                    IdTipoInspeccion = ti.IdTipoInspeccion,
                    DescripcionInspeccion = ti.DescripcionTipoInspeccion
                }).ToList();
            }
            else
            {
            }
            return View(listp);
        }

        /// <summary>
        /// Metodo para obtener las Planeaciones de Inspeccion Sin Ejecutar.
        /// es ejecutado cuando se realiza todo el flujo.
        /// </summary>
        /// <returns></returns>
        public ActionResult ListaNoEjecutados()
        {
            CrearInspeccionModel noejecutados = new CrearInspeccionModel();
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("id", usuarioActual.IdEmpresa);
            var resultsinejecutar = ServiceClient.ObtenerArrayJsonRestFul<EDInspeccion>(UrlServicioAplicacion, ObtenerPlanporempresase, RestSharp.Method.GET);
            if (resultsinejecutar != null)
            {


                return Json(new { Data = resultsinejecutar, Mensaje = "OK" }, JsonRequestBehavior.AllowGet);
            }
            else
            {

                return Json(new { Data = string.Empty, Mensaje = "ERROR" }, JsonRequestBehavior.AllowGet);
            }

        }
        /// <summary>
        /// Metodo para crear la planeacion de la Inspeccion
        /// </summary>
        /// <returns>planinspeccion</returns>
        public ActionResult CrearPlaneacionInspeccion(PlanInspeccionModel planInspeccion)
        {
            var consecutivoplan = 0;
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                ServiceClient.EliminarParametros();
                ServiceClient.AdicionarParametro("id", usuarioActual.IdEmpresa);
                var resultplaneacionporEM = ServiceClient.ObtenerArrayJsonRestFul<EDPlanInspeccion>(UrlServicioAplicacion, ObtenerPlanporempresa, RestSharp.Method.GET);
                if (resultplaneacionporEM.Count() == 0)
                {
                    consecutivoplan = 1;
                }
                else
                {
                    consecutivoplan = resultplaneacionporEM.Count() + 1;
                }
                planInspeccion.idEmpresaVM = usuarioActual.IdEmpresa;
                var planinspecciones = new EDPlanInspeccion()
                {
                    Idplaninspeccion = planInspeccion.Idplaninspeccion,
                    DescripcionTipoInspeccion = planInspeccion.DescripcionTipoInspeccion,
                    responsable = planInspeccion.responsable.ToUpper(),
                    Fecha = planInspeccion.Fecha,
                    idEmpresaED = planInspeccion.idEmpresaVM,
                    ConsecutivoPlanED = consecutivoplan,
                };
                ServiceClient.EliminarParametros();
                var result = ServiceClient.RealizarPeticionesPostJsonRestFul<EDPlanInspeccion>(UrlServicioAplicacion, CapacidadGuardarPlanInspeccion, planinspecciones);
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

        /// <summary>
        /// Metodo para Retornar a la Vista la Informacion de la 
        /// Inspeccion No Ejecutada.
        /// </summary>
        /// <param name="Idplaninspeccion"></param>
        /// <returns></returns>

        public ActionResult ObtenerInspeccionNoejecutada(int Idplaninspeccion, int Idinspeccion)
        {
            CrearInspeccionModel Noejecutada = new CrearInspeccionModel();
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarce para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("idp", Idplaninspeccion);
            ServiceClient.AdicionarParametro("idi", Idinspeccion);
            ServiceClient.AdicionarParametro("id", usuarioActual.IdEmpresa);
            var resultInspeccionNoEjecutada = ServiceClient.ObtenerObjetoJsonRestFul<EDInspeccion>(UrlServicioAplicacion, ObtenerInspeccionNoEjecutada, RestSharp.Method.GET);
            if (resultInspeccionNoEjecutada != null)
            {

                return Json(new { Data = resultInspeccionNoEjecutada, Mensaje = "OK" }, JsonRequestBehavior.AllowGet);
            }

            //return View(Noejecutada);
            return Json(new { Data = string.Empty, Mensaje = "ERROR" }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Metodo para continuar con el proceso de ejecucion de una Planeacion de Inspeccion.
        /// </summary>
        /// <param name="continuar"></param>
        /// <returns></returns>
        public ActionResult ContinuarEjecucionPlan(ContinuarPlaneacionVM continuar)
        {
            var creanoejecutada = new CrearInspeccionModel();
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            //Cargar en el DropDown la lista de tipos de opciones
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "-- Seleccionar --", Value = "", Selected = true });
            items.Add(new SelectListItem { Text = "Todos", Value = "Todos" });
            items.Add(new SelectListItem { Text = "Máquina", Value = "Máquina" });
            items.Add(new SelectListItem { Text = "Equipo", Value = "Equipo" });
            items.Add(new SelectListItem { Text = "Herramienta", Value = "Herramienta" });
            ViewBag.TipoElemento = items;



            List<EDAdmoEMH> ListaEHM = LNAdmoEMH.ConsultaAdmoEMH("", "", usuarioActual.IdEmpresa);
            List<string> ListaEHM1 = new List<string>();
            foreach (var item in ListaEHM)
            {
                string valor = item.Pk_Id_AdmoEMH + "@" + item.NombreElemento + "@" + item.TipoElemento;
                ListaEHM1.Add(valor);
            }
            ViewBag.TodosEHM = ListaEHM1;
            creanoejecutada.IDEmpresa = usuarioActual.IdEmpresa;
            creanoejecutada.idempresa = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current) == null ? string.Empty : ObtenerUsuarioEnSesion(System.Web.HttpContext.Current).NitEmpresa;
            creanoejecutada.RazonSocial = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current) == null ? string.Empty : ObtenerUsuarioEnSesion(System.Web.HttpContext.Current).RazonSocialEmpresa;
            var Planeacion = (from plin in db.Tbl_Planeacion_Inspeccion
                              join mti in db.Tbl_Maestro_Planeación_Inspeccion
                              on plin.Fk_Id_Maestro_Tipo_Inspeccion equals mti.Pk_Id_Maestro_Tipo_Inspeccion
                              where plin.Pk_Id_PlaneacionInspeccion == continuar.IdPlaneacion
                              select plin);
            var consecutivo = 0;
            var tipoInspeccion = "";
            DateTime fechaplan = Planeacion.FirstOrDefault().Fecha;
            var responsables = "";
            foreach (var i in Planeacion)
            {
                consecutivo = i.ConsecutivoPlan;
                responsables = i.Responsable_Tipo_Inspeccion;
                tipoInspeccion = i.MaestrotipoInspeccion.Descripcion_Tipo_Inspeccion;
                DateTime fecha = Convert.ToDateTime(i.Fecha);
                fechaplan = fecha;
            }
            creanoejecutada.Consecutivo = consecutivo;
            creanoejecutada.responsableplaninspeccion = responsables;
            creanoejecutada.descripcion = tipoInspeccion;
            creanoejecutada.fechaplaninspeccion = fechaplan;
            creanoejecutada.idplaninspeccion = continuar.IdPlaneacion;
            if (creanoejecutada.descripcion == "Inspección Maquinaria, Equipo y otras herramientas")
            {
                creanoejecutada.IdTipoInspeccion = 4;
            }
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("NIT", usuarioActual.NitEmpresa);
            var resultSede = ServiceClient.ObtenerArrayJsonRestFul<EDSede>(urlServicioEmpresas, CapacidadObtenerSedesPorNit, RestSharp.Method.GET);
            if (resultSede != null && resultSede.Count() > 0)
            {
                creanoejecutada.Sedes = resultSede.Select(c => new SelectListItem()
                {
                    Value = c.IdSede.ToString(),
                    Text = c.NombreSede
                }).ToList();
            }
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("NIT", usuarioActual.NitEmpresa);
            var resultconfiguracion = ServiceClient.ObtenerArrayJsonRestFul<EDConfiguracion>(UrlServicioAplicacion, CapacidadObtenerConfiguracionPrioridades, RestSharp.Method.GET);
            if (resultconfiguracion != null && resultconfiguracion.Count() > 0)
            {
                creanoejecutada.Configuraciones = resultconfiguracion.Select(cf => new ConfiguracionesModel()
                {
                    idconfiguracion = cf.idconfiguracion,
                    Descripcionconfiguracion = cf.Descripcion,
                    diasdesde = cf.diasdesde,
                    diashasta = cf.diashasta
                }).ToList();
            }
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("NIT", usuarioActual.NitEmpresa);
            var resultProceso = ServiceClient.ObtenerArrayJsonRestFul<EDProceso>(urlServicioEmpresas, CapacidadObtenerprocesosEmpresa, RestSharp.Method.GET);
            if (resultProceso != null && resultProceso.Count() > 0)
            {
                creanoejecutada.Procesos = resultProceso.Select(p => new SelectListItem()
                {
                    Value = p.Id_Proceso.ToString(),
                    Text = p.Descripcion
                }).ToList();
            }


            //return Json(true, JsonRequestBehavior.AllowGet);
            return View(creanoejecutada);

        }




        /// <summary>
        /// Metodo para retornar a la vista los datos para grabar una Inspeccion.
        /// </summary>
        /// <returns>crearInspeccion</returns>
        public ActionResult CrearI()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            //Cargar en el DropDown la lista de tipos de opciones
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "-Seleccionar-", Value = "-Seleccionar-", Selected = true });
            items.Add(new SelectListItem { Text = "Todos", Value = "Todos" });
            items.Add(new SelectListItem { Text = "Máquina", Value = "Máquina" });
            items.Add(new SelectListItem { Text = "Equipo", Value = "Equipo" });
            items.Add(new SelectListItem { Text = "Herramienta", Value = "Herramienta" });
            ViewBag.TipoElemento = items;



            List<EDAdmoEMH> ListaEHM = LNAdmoEMH.ConsultaAdmoEMH("", "", usuarioActual.IdEmpresa);
            List<string> ListaEHM1 = new List<string>();
            foreach (var item in ListaEHM)
            {
                string valor = item.Pk_Id_AdmoEMH + "@" + item.NombreElemento + "@" + item.TipoElemento;
                ListaEHM1.Add(valor);
            }
            ViewBag.TodosEHM = ListaEHM1;

            var crearinspeccion = new CrearInspeccionModel();
            crearinspeccion.idempresa = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current) == null ? string.Empty : ObtenerUsuarioEnSesion(System.Web.HttpContext.Current).NitEmpresa;
            crearinspeccion.RazonSocial = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current) == null ? string.Empty : ObtenerUsuarioEnSesion(System.Web.HttpContext.Current).RazonSocialEmpresa;
            crearinspeccion.IDEmpresa = usuarioActual.IdEmpresa;
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("NIT", usuarioActual.NitEmpresa);
            var resultinspeccion = ServiceClient.ObtenerObjetoJsonRestFul<EDPlanInspeccion>(UrlServicioAplicacion, CapacidadObtenerPlanInspeccion, RestSharp.Method.GET);
            if (resultinspeccion != null)
            {

                crearinspeccion.Consecutivo = resultinspeccion.ConsecutivoPlanED;
                crearinspeccion.idplaninspeccion = resultinspeccion.Idplaninspeccion;
                crearinspeccion.fechaplaninspeccion = resultinspeccion.Fecha;
                crearinspeccion.descripcion = resultinspeccion.descripcion;
                crearinspeccion.responsableplaninspeccion = resultinspeccion.responsable;
                crearinspeccion.IdTipoInspeccion = resultinspeccion.Idtipoinspeccion;
            }
            else
            {
                crearinspeccion.idplaninspeccion = resultinspeccion.Idplaninspeccion + 1;
                crearinspeccion.fechaplaninspeccion = resultinspeccion.Fecha;
                crearinspeccion.descripcion = resultinspeccion.descripcion;
                crearinspeccion.responsableplaninspeccion = resultinspeccion.responsable;
            }

            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("NIT", usuarioActual.NitEmpresa);
            var resultSede = ServiceClient.ObtenerArrayJsonRestFul<EDSede>(urlServicioEmpresas, CapacidadObtenerSedesPorNit, RestSharp.Method.GET);
            if (resultSede != null && resultSede.Count() > 0)
            {
                crearinspeccion.Sedes = resultSede.Select(c => new SelectListItem()
                {
                    Value = c.IdSede.ToString(),
                    Text = c.NombreSede
                }).ToList();
            }
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("NIT", usuarioActual.NitEmpresa);
            var resultconfiguracion = ServiceClient.ObtenerArrayJsonRestFul<EDConfiguracion>(UrlServicioAplicacion, CapacidadObtenerConfiguracionPrioridades, RestSharp.Method.GET);

            if (resultconfiguracion != null && resultconfiguracion.Count() > 0)
            {
                crearinspeccion.Configuraciones = resultconfiguracion.Select(cf => new ConfiguracionesModel()
                {
                    idconfiguracion = cf.idconfiguracion,
                    Descripcionconfiguracion = cf.Descripcion,
                    diasdesde = cf.diasdesde,
                    diashasta = cf.diashasta
                }).ToList();
            }
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("NIT", usuarioActual.NitEmpresa);
            var resultProceso = ServiceClient.ObtenerArrayJsonRestFul<EDProceso>(urlServicioEmpresas, ObtenerprocesosEmpresaprnivel, RestSharp.Method.GET);
            if (resultProceso != null && resultProceso.Count() > 0)
            {
                crearinspeccion.Procesos = resultProceso.Select(p => new SelectListItem()
                {
                    Value = p.Id_Proceso.ToString(),
                    Text = p.Descripcion
                }).ToList();
            }
            else
            {
                crearinspeccion.Procesos = resultProceso.Select(p => new SelectListItem()
                {
                    Value = p.Id_Proceso.ToString(),
                    Text = p.Descripcion
                }).ToList();

                ViewBag.mensaje = "Se deben Registrar los procesos para continuar";

            }
            return View(crearinspeccion);
        }


        /// <summary>
        /// Metodo para Retornar a una Vista la Informacion completa
        /// de una Inspeccion.
        /// </summary>
        /// <param name="claveInspeccion"></param>
        /// <param name="claveCondicion"></param>
        /// <returns></returns>
        public ActionResult InformacionInspeccion(int claveInspeccion, int claveCondicion)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarce para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("NIT", usuarioActual.NitEmpresa);
            ServiceClient.AdicionarParametro("idInspeccion", claveInspeccion);
            ServiceClient.AdicionarParametro("idCondicion", claveCondicion);
            var resultInspeccion = ServiceClient.ObtenerObjetoJsonRestFul<EDInspeccion>(UrlServicioAplicacion, ObtenerInfoInspeccion, RestSharp.Method.GET);
            if (resultInspeccion != null)
            {
                return Json(new { Data = resultInspeccion, Mesaje = "Ok" }, JsonRequestBehavior.AllowGet);

            }
            return Json(new { Data = string.Empty, Mensaje = "ERROR" }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Inspecciones_Pdf()
        {
            List<EDCondicionInsegura> CIS = new List<EDCondicionInsegura>();
            var resumen = new CrearInspeccionModel();
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                return View();
            }
            var resultInspeccion = ServiceClient.ObtenerObjetoJsonRestFul<EDInspeccion>(UrlServicioAplicacion, ObtenerInfoInspeccion, RestSharp.Method.GET);
            if (resultInspeccion != null)
            {
                resumen.area = resultInspeccion.EDarealugar;
                resumen.Consecutivo = Convert.ToInt32(resultInspeccion.EDConsecutivo);
                resumen.EstadoVm = Convert.ToInt32(resultInspeccion.EstadoIdED);
                resumen.fecharealizacioninspeccion = resultInspeccion.EDFechaRealizacion;
                resumen.fechaplaninspeccion = resultInspeccion.EDFechaPlaneacionIns;
                resumen.responsableplaninspeccion = resultInspeccion.EDResponsablePlaneacion;
                resumen.responsable = resultInspeccion.EDresponsableLugar;
                resumen.hora = resultInspeccion.EDhora;
                resumen.DescripcionTipoInspeccion = resultInspeccion.EDDescripcionTipoI;
                resumen.DescribeInspeccion = resumen.DescribeInspeccion;
                resumen.Sede = resultInspeccion.EDdescribesede;
                resumen.Proceso = resultInspeccion.EDdescribeProceso;
                resumen.Asistentes = resultInspeccion.Asistentes.Select(a => new AsistentesviewModel()
                {
                    nombreasistente = a.NombreAsistente,
                }).ToList();
                resumen.Condiciones = resultInspeccion.CondicionesIns.Select(ci => new CondicionesInsegurasVM()
               {
                   pkcondicionvm = ci.EDpkCondicion,
                   DescribeCondicionvm = ci.EDDescribeCondicion,
                   Prioridad = ci.EDescribePrioridad,
                   Diadesde = ci.EDDiasDesde,
                   Diahasta = ci.EDDiasHasta,
                   Ubicacionespecificavm = ci.EDUbicacionespecifica,

               }).ToList();
                resumen.Elementos = resultInspeccion.EDElementos.Select(e => new ElementosEMHVM()
                    {
                        NombreElemento = e.NombreElemento,
                        TipoElemento = e.TipoElemento,
                        Marca = e.Marca,
                    }).ToList();
            }


            string SwitchNombreEmpresa = usuarioActual.RazonSocialEmpresa;
            string SwitchNitEmpresa = usuarioActual.NitEmpresa;
            string SwitchNombreDocumento = "Resumen General Inspeccion";
          //  var fullFooter = Url.Action("Footer", "PlandeInspeccion", null, Request.Url.Scheme);
          //  var fullHeader = Url.Action("Header", "PlandeInspeccion", new { NombreEmpresa = SwitchNombreEmpresa, NitEmpresa = SwitchNitEmpresa, NombreInforme = SwitchNombreDocumento }, Request.Url.Scheme);
            var uriFooter = new Uri(Url.Action("Footer", "PlandeInspeccion", null, Request.Url.Scheme));
            var clean1 = uriFooter.GetComponents(UriComponents.AbsoluteUri & ~UriComponents.Port, UriFormat.UriEscaped);
            var uriHeader = new Uri(Url.Action("Header", "PlandeInspeccion", new { NombreEmpresa = SwitchNombreEmpresa, NitEmpresa = SwitchNitEmpresa, NombreInforme = SwitchNombreDocumento }, Request.Url.Scheme));
            var clean2 = uriHeader.GetComponents(UriComponents.AbsoluteUri & ~UriComponents.Port, UriFormat.UriEscaped);
            string cusomtSwitches = string.Format("--footer-line --print-media-type --allow {0} --footer-html {0} --header-line --print-media-type --allow {1} --header-html {1} --header-spacing 5", clean1, clean2);
            //string cusomtSwitches = string.Format("--footer-line --print-media-type --allow {0} --footer-html {0} --header-line --print-media-type --allow {1} --header-html {1} --header-spacing 5", fullFooter, fullHeader);
            return new Rotativa.PartialViewAsPdf("ResumenGeneralInspecciones", resumen) { FileName = "Resumen_Inspección.pdf", CustomSwitches = cusomtSwitches };
        }

        [AllowAnonymous]
        public ActionResult Header(string NombreEmpresa, string NitEmpresa, string NombreInforme)
        {
            ViewBag.NombreEmpresa = NombreEmpresa;
            ViewBag.NitEmpresa = NitEmpresa;
            ViewBag.NombreInforme = NombreInforme;
            return View("Header");
        }
        [AllowAnonymous]
        public ActionResult Footer()
        {
            return View("Footer");
        }
        /// <summary>
        /// Metodo para Eliminar una inspeccion 
        ///y sus relaciones.
        /// </summary>
        /// <param name="IdInspeccion"></param>
        /// <param name="IdPlaneacion"></param>
        /// <returns></returns>
        public ActionResult EliminarInspeccion(int IdInspeccion, int IdPlaneacion)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarce para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("NIT", usuarioActual.NitEmpresa);
            ServiceClient.AdicionarParametro("IdInspeccion", IdInspeccion);
            ServiceClient.AdicionarParametro("IdPlaneacion", IdPlaneacion);
            var resultEliminar = ServiceClient.PeticionesPostJsonRestFulRespuetaBool(UrlServicioAplicacion, EliminarInspecciones, RestSharp.Method.DELETE);
            if (resultEliminar != false)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Data = string.Empty, Mensaje = "ERROR" }, JsonRequestBehavior.AllowGet);
            }

        }



        /// <summary>
        /// Metodo que Elimina una Planeacion
        /// sin Registro de Inspeccion
        /// </summary>
        /// <param name="IdPlaneacion"></param>
        /// <returns></returns>
        public ActionResult EliminarPlaneaciones(int IdPlaneacion)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarce para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("NIT", usuarioActual.NitEmpresa);
            ServiceClient.AdicionarParametro("IdPlaneacion", IdPlaneacion);
            var resultEliminarPlan = ServiceClient.PeticionesPostJsonRestFulRespuetaBool(UrlServicioAplicacion, EliminarPlaneacion, RestSharp.Method.DELETE);
            if (resultEliminarPlan != false)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Data = string.Empty, Mensaje = "ERROR" }, JsonRequestBehavior.AllowGet);
            }


        }


        //CrearInspeccionModel


        /// <summary>
        /// Metodo para registrar una inspeccion y generar formulario de registro condicion Insegura
        /// </summary>
        /// <returns>crearmodel</returns>
        public ActionResult ObtenerFormularioCondicion(ObtenerCondicionVM crearmodel)
        {
            
            List<Inspecciones> insp = new List<Inspecciones>();
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }

            insp = (from i in context.Tbl_Inspecciones select i).ToList();
            foreach (var busca in insp)
            {
                if (busca.Id_Inspeccion == crearmodel.Consecutivo & busca.Fk_Id_PlaneacionInspeccion==crearmodel.idplaninspeccion & busca.Fk_IdEmpresa==usuarioActual.IdEmpresa)
                {
                    return Json(new { crearmodel = 0 }, JsonRequestBehavior.AllowGet);
                }
            }
            if (ModelState.IsValid)
            {
                if (crearmodel.elementos == null)
                {
                    var InfoInspeccion = new EDInspeccion()
                    {
                        EDFechaRealizacion = crearmodel.fecharealizacioninspeccion,
                        EDfkEmpresa = usuarioActual.IdEmpresa,
                        EDarealugar = crearmodel.area,
                        EDfkIdPlaneacionInspeccion = crearmodel.idplaninspeccion,
                        EDDescribeinspeccion = crearmodel.DescribeInspeccion,
                        EDhora = crearmodel.hora,
                        EDpkinspeccion = crearmodel.idinspeccion,
                        EDproceso = crearmodel.idProceso,
                        EDresponsableLugar = crearmodel.responsable,
                        EDsede = crearmodel.idSede,
                        EDConsecutivo = crearmodel.Consecutivo,
                        Asistentes = crearmodel.Asistentes.Select(asis => new EDAsistente()
                        {
                            Idasistente = asis.idasistente,
                            NombreAsistente = asis.nombreasistente
                        }).ToList(),

                        Configuraciones = crearmodel.Configuraciones.Select(config => new EDConfiguracion()
                        {
                            idconfiguracion = config.idconfiguracion,
                            Descripcion = config.Descripcionconfiguracion,
                            diasdesde = config.diasdesde,
                            diashasta = config.diashasta
                        }).ToList(),
                    };
                    ServiceClient.EliminarParametros();
                    var result = ServiceClient.RealizarPeticionesPostJsonRestFul<EDInspeccion>(UrlServicioAplicacion, CapacidadGuardarInspeccion, InfoInspeccion);
                    if (result != null)
                    {
                        crearmodel.idinspeccion = Convert.ToInt32(result.EDpkinspeccion);
                        crearmodel.Configuraciones = result.Configuraciones.Select(CF => new ConfiguracionesModel

                            {
                                idconfiguracion = CF.idconfiguracion,
                                Descripcionconfiguracion = CF.Descripcion,
                                diasdesde = CF.diasdesde,
                                diashasta = CF.diashasta,


                            }).ToList();
                    }
                }
                else
                {
                    var InfoInspeccion = new EDInspeccion()
                    {
                        EDFechaRealizacion = crearmodel.fecharealizacioninspeccion,
                        EDfkEmpresa = usuarioActual.IdEmpresa,
                        EDarealugar = crearmodel.area,
                        EDfkIdPlaneacionInspeccion = crearmodel.idplaninspeccion,
                        EDDescribeinspeccion = crearmodel.DescribeInspeccion,
                        EDhora = crearmodel.hora,
                        EDpkinspeccion = crearmodel.idinspeccion,
                        EDproceso = crearmodel.idProceso,
                        EDresponsableLugar = crearmodel.responsable,
                        EDsede = crearmodel.idSede,
                        EDConsecutivo = crearmodel.Consecutivo,
                        EDElementos = crearmodel.elementos.Select(asis => new EDAdmoEMH()
                        {
                            Pk_Id_AdmoEMH = asis
                        }).ToList(),
                        Asistentes = crearmodel.Asistentes.Select(asis => new EDAsistente()
                        {
                            Idasistente = asis.idasistente,
                            NombreAsistente = asis.nombreasistente
                        }).ToList(),
                        Configuraciones = crearmodel.Configuraciones.Select(config => new EDConfiguracion()
                        {
                            idconfiguracion = config.idconfiguracion,
                            Descripcion = config.Descripcionconfiguracion,
                            diasdesde = config.diasdesde,
                            diashasta = config.diashasta
                        }).ToList(),
                    };
                    ServiceClient.EliminarParametros();
                    var resultconelementos = ServiceClient.RealizarPeticionesPostJsonRestFul<EDInspeccion>(UrlServicioAplicacion, CapacidadGuardarInspeccion, InfoInspeccion);
                    if (resultconelementos != null)
                    {
                        crearmodel.idinspeccion = Convert.ToInt32(resultconelementos.EDpkinspeccion);
                        crearmodel.Configuraciones = resultconelementos.Configuraciones.Select(CF => new ConfiguracionesModel

                        {
                            idconfiguracion = CF.idconfiguracion,
                            Descripcionconfiguracion = CF.Descripcion,
                            diasdesde = CF.diasdesde,
                            diashasta = CF.diashasta,


                        }).ToList();
                    }

                }
                ServiceClient.EliminarParametros();
                ServiceClient.AdicionarParametro("NIT", usuarioActual.NitEmpresa);
                var resultpeligros = ServiceClient.ObtenerArrayJsonRestFul<EDTipoDePeligro>(UrlServicioAplicacion, CapacidadObtenerTiposPeligros, RestSharp.Method.GET);
                if (resultpeligros != null && resultpeligros.Count() > 0)
                {
                    crearmodel.peligrosos = resultpeligros.Select(cf => new PeligrosModel()
                    {

                        idpeligro = cf.PK_Tipo_De_Peligro,
                        Describepeligro = cf.Descripcion_Del_Peligro
                    }).ToList();
                }
                return Json(crearmodel, JsonRequestBehavior.AllowGet);      
            }
            else
            {
                return Json(new { Data = string.Empty, Mensaje = "ERROR" }, JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// Metodo para Eliminar una Condicion 
        /// Insegura seleccionada por el Usuario
        /// </summary>
        /// <param name="clavecondicion"></param>
        /// <returns></returns>

        public ActionResult EliminarCondicion(int clavecondicion, int claveinspeccion)
        {
            List<EDCondicionInsegura> condicionesIns = new List<EDCondicionInsegura>();
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarce para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            var DeletedocEvidencia = (from ci in db.CondicionInsegura
                       where ci.Pk_Id_CondicionInsegura == clavecondicion
                       select ci).FirstOrDefault();
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("NIT", usuarioActual.NitEmpresa);
            ServiceClient.AdicionarParametro("IdCondicion", clavecondicion);
            var resultEliminar = ServiceClient.PeticionesPostJsonRestFulRespuetaBool(UrlServicioAplicacion, EliminarCondicionI, RestSharp.Method.DELETE);
            if (resultEliminar != false)
            { 
                var path = Server.MapPath(DocumentosInspecciones);
                var file = DeletedocEvidencia.Evidencia;
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
                        registraLog.RegistrarError(typeof(PlandeInspeccionController), string.Format("Error al eliminar el documento del servidor   {0}: {1}", DateTime.Now, e.StackTrace), e);
                    }
                }
                ServiceClient.EliminarParametros();
                ServiceClient.AdicionarParametro("NIT", usuarioActual.NitEmpresa);
                ServiceClient.AdicionarParametro("idinspeccion", claveinspeccion);
                var resultcondicionespi = ServiceClient.ObtenerArrayJsonRestFul<EDCondicionInsegura>(UrlServicioAplicacion, ObtenerCondicionesporInspeccion, RestSharp.Method.GET);
                if (resultcondicionespi != null)
                {
                    //condicionesIns = resultcondicionespi.ToList();

                    return Json(new { Data = resultcondicionespi, Mensaje = "OK" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Data = condicionesIns, Mensaje = "OK" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { Data = string.Empty, Mensaje = "ERROR" }, JsonRequestBehavior.AllowGet);
            }

        }

        /// <summary>
        /// Metodo para Retornar al Formulario Condicion Insegura los Tipos de Peligro.
        /// </summary>
        /// <param name="Pk_Tipo_Peligro"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ConsultarClasesPeligro(int Pk_Tipo_Peligro)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                return View();
            }

            List<ClasificacionDePeligro> clasesDePeligrosList = clasificacionDePeligrosServicios.ConsultarClasesDePeligros(Pk_Tipo_Peligro);
            if (clasesDePeligrosList.Count != 0)
            {
                return Json(
                   clasesDePeligrosList.Select(ClasesPeligros => new
                   {
                       PK_ClasesPeligros = ClasesPeligros.PK_Clasificacion_De_Peligro,
                       ClasesDescription = ClasesPeligros.Descripcion_Clase_De_Peligro
                   })
                , JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Funcion para Obtener las configuraciones por Inspección
        /// en el flujo de Ejecutar la Planeacion de Inspeccion, 
        /// para el Modal Agregar Condicion Insegura.
        /// </summary>
        /// <param name="IdInspeccion"></param>
        /// <returns></returns>
        public ActionResult ObtenerConfiguracionesPorInspeccion(int idinspeccion)
        {
            var inspeccion = 0;
            List<ConfiguracionInspModel> configurados = new List<ConfiguracionInspModel>();
            List<PeligrosModel> peligrosos = new List<PeligrosModel>();
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                return View();
            }
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("NIT", usuarioActual.NitEmpresa);
            ServiceClient.AdicionarParametro("idinspeccion", idinspeccion);
            var resultconfigI = ServiceClient.ObtenerArrayJsonRestFul<EDConfiguracion>(UrlServicioAplicacion, CapacidadObtenerConfiguracionInspeccionID, RestSharp.Method.GET);
            {
                configurados = resultconfigI.Select(cf => new ConfiguracionInspModel()
                {

                    idconfiguracionViewModel = cf.idconfiguracion,
                    DescripcionViewModel = cf.Descripcion
                }).ToList();
            }
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("NIT", usuarioActual.NitEmpresa);
            var resultpeligros = ServiceClient.ObtenerArrayJsonRestFul<EDTipoDePeligro>(UrlServicioAplicacion, CapacidadObtenerTiposPeligros, RestSharp.Method.GET);
            //var resultpeligros = ServiceClient.ObtenerArrayJsonRestFul<EDTipoDePeligro>(urlPlanificacion, capacidadTiposDePeligros, RestSharp.Method.GET);
            if (resultpeligros != null)
            {

                peligrosos = resultpeligros.Select(cf => new PeligrosModel()
               {
                   idpeligro = cf.PK_Tipo_De_Peligro,
                   Describepeligro = cf.Descripcion_Del_Peligro
               }).ToList();
            }

            inspeccion = idinspeccion;
            return Json(new { configurados, peligrosos, inspeccion }, JsonRequestBehavior.AllowGet);

        }


        [HttpGet]
        /// <summary>
        /// Metodo para grabar las condiciones Inseguras de la Inspeccion
        /// </summary>
        /// <param name="condiciones"></param>
        /// <returns></returns>
        /// 
        public ActionResult GuardarCondicionesInseguras(CondicionesVModel condiciones)
        {
            List<EDCondicionInsegura> condicionesIns = new List<EDCondicionInsegura>();
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                var InfoConfiguracion = new EDCondicionInsegura()
                {
                    EDPkInspeccion = condiciones.idinspeccion,
                    EDpkCondicion = condiciones.pkCondicionVM,
                    EDDescribeCondicion = condiciones.DescribeCondicionVM,
                    EDUbicacionespecifica = condiciones.UbicacionespecificaVM,
                    EDRiesgopeligro = condiciones.RiesgopeligroVM,
                    EDClasificacionRiesgo = condiciones.ClasificacionRiesgoVM,
                    EDConfiguracioncondicion = condiciones.ConfiguracioncondicionVM,
                    EDEvidenciacondicion = condiciones.EvidenciacondicionVM,
                    EDOtraClasePeligro=condiciones.OtroRiesgoVM,
                    //EDEstadoCondicion=EstadoConfiguracionVM,
                };
                ServiceClient.EliminarParametros();
                var resultCondicion = ServiceClient.RealizarPeticionesPostJsonRestFul<EDCondicionInsegura>(UrlServicioAplicacion, GuardarCondicionInsegura, InfoConfiguracion);
                if (resultCondicion != null)
                {
                    //return Json(new { condicionesIns = resultCondicion, Mensaje = "OK" }, JsonRequestBehavior.AllowGet);

                    ServiceClient.EliminarParametros();
                    ServiceClient.AdicionarParametro("NIT", usuarioActual.NitEmpresa);
                    ServiceClient.AdicionarParametro("idinspeccion", resultCondicion.EDPkInspeccion);
                    var resultcondicionespi = ServiceClient.ObtenerArrayJsonRestFul<EDCondicionInsegura>(UrlServicioAplicacion, ObtenerCondicionesporInspeccion, RestSharp.Method.GET);
                    if (resultcondicionespi != null)
                    {
                        
                       
                        //condicionesIns = resultcondicionespi.ToList();

                        return Json(new { Data = resultcondicionespi, Mensaje = "OK" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { Data = condicionesIns, Mensaje = "OK" }, JsonRequestBehavior.AllowGet);
                    }

                }
                else
                {
                    return Json(new { Data = condicionesIns, Mensaje = "ERROR" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { Data = condicionesIns, Mensaje = "ERROR" }, JsonRequestBehavior.AllowGet);
            }
           
        }

        /// <summary>
        /// Metodo para cargar la Evidencia de una condicion Insegura.
        /// </summary>
        /// <returns></returns>

        [HttpPost]
        public virtual ActionResult SubirArchivo()
        {
            HttpPostedFileBase myFile = Request.Files["Evidencia"];
            bool isUploaded = false;
             string message = "File upload failed";
            string fullpath = string.Empty;
            if (myFile != null && myFile.ContentLength != 0)
            {
                if (Path.GetExtension(myFile.FileName).ToLower() == ".jpg"
                    || Path.GetExtension(myFile.FileName).ToLower() == ".png"
                    //|| Path.GetExtension(myFile.FileName).ToLower() == ".xls"
                    //|| Path.GetExtension(myFile.FileName).ToLower() == ".xlsx"
                     || Path.GetExtension(myFile.FileName).ToLower() == ".pdf"
                && myFile.ContentLength <= (6 * (1048576)))
                {
                    string pathForSaving = Server.MapPath(DocumentosInspecciones);
                    if (this.CreateFolderIfNeeded(pathForSaving))
                    {
                        try
                        {

                            var mes = DateTime.Now.Month.ToString();
                            var dia = DateTime.Now.Day.ToString();
                            var anio = DateTime.Now.Year.ToString();
                            var hora = DateTime.Now.Hour.ToString();
                            var min = DateTime.Now.Minute.ToString();
                            var second = DateTime.Now.Second.ToString();
                            var currentmes = hora + "_" + min + "_" + second + "_" + dia + "_" + mes + "_" + anio + "_";
                            string filen = currentmes + myFile.FileName;
                            myFile.SaveAs(Path.Combine(pathForSaving, filen));
                            //myFile.SaveAs(Path.Combine(pathForSaving, myFile.FileName));
                            isUploaded = true;
                            fullpath = filen;
                        }
                        catch (Exception ex)
                        {
                            message = string.Format("File upload failed: {0}", ex.Message);
                        }
                    }

                }
                else 
                {
                    var Resultado = 1;
                    return Json(new { Data = Resultado, Mensaje = "ERROR" }, JsonRequestBehavior.AllowGet);
                }
                
               
            }
            return Json(new { isUploaded = isUploaded, message = fullpath }, "text/html");


        }

        /// <summary>
        /// Funcion para visualizar
        /// las Evidencia de la Condicion Insegura.
        /// </summary>
        /// <param name="pk_id_plan_empresa"></param>
        /// <param name="tipo"></param>
        /// <returns></returns>
         [HttpGet]
        public ActionResult ObtenerImagen(int clavecondicion)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
                return View("Login","Home");
            }
            string Archivo = "";
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    Archivo = db.CondicionInsegura.Find(clavecondicion).Evidencia;  
                }
                catch (Exception)
                {
                    transaction.Rollback();
                   
                }
            }
            return Json(Archivo, JsonRequestBehavior.AllowGet);
           
        }

        
        /// <summary>
        /// Creates the folder if needed.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        private bool CreateFolderIfNeeded(string path)
        {
            bool result = true;
            if (!Directory.Exists(path))
            {
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch (Exception)
                {
                    /*TODO: You must process this exception.*/
                    result = false;
                }
            }
            return result;
        }

        /// <summary>
        /// Metodo para Retornar a la Vista las Condiciones Inseguras por Inspeccion
        /// </summary>
        /// <returns></returns>
        public ActionResult ListarCondicionesPorInspeccion()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            var crearinspecciones = new CrearInspeccionModel();
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("NIT", usuarioActual.NitEmpresa);
            var resultcondicionespi = ServiceClient.ObtenerArrayJsonRestFul<EDCondicionInsegura>(UrlServicioAplicacion, ObtenerCondicionesporInspeccion, RestSharp.Method.GET);
            if (resultcondicionespi != null && resultcondicionespi.Count() > 0)
            {
                crearinspecciones.Condiciones = resultcondicionespi.Select(ci => new CondicionesInsegurasVM()
                {
                    pkcondicionvm = ci.EDpkCondicion,
                    Describelaconfiguracion = ci.EDDescribeCondicion,
                    Prioridad = ci.EDescribePrioridad,
                    Diadesde = ci.EDDiasDesde,
                    Diahasta = ci.EDDiasHasta,
                    ClasificacionRiesgovm = ci.EDClasificacionRiesgo,
                    Configuracioncondicionvm = ci.EDConfiguracioncondicion,
                    DescribeCondicionvm = ci.EDDescribeCondicion,
                    Ubicacionespecificavm = ci.EDUbicacionespecifica,
                }).ToList();
                return View(crearinspecciones);
            }
            else
            {
                ViewBag.mensaje = "La Inspeccion no tiene condiciones inseguras registradas";
            }
            return View("CrearI", crearinspecciones);
        }

        /// <summary>
        /// Metodo que obtiene los planes de accion Generados a las Condiciones Inseguras de la Inspeccion
        /// </summary>
        /// <returns></returns>
        public ActionResult ListarPlanesAccion()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            var planaccion = new PlanAccionVM();
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("NIT", usuarioActual.NitEmpresa);
            var resultplanaccioni = ServiceClient.ObtenerArrayJsonRestFul<EDPlanAccionInspeccion>(UrlServicioAplicacion, ObtenerPlanAccionInspeccion, RestSharp.Method.GET);
            if (resultplanaccioni != null)
            {
                return Json(new { Data = resultplanaccioni, Mensaje = "OK" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Data = string.Empty, Mensaje = "ERROR" }, JsonRequestBehavior.AllowGet);
            }
        }



        /// <summary>
        /// Metodo para Generar la Fecha de Finalizacion de las condiciones inseguras en el plan de accion.
        /// </summary>
        /// <param name="creai"></param>
        /// <returns></returns>
        public ActionResult PlanAccion(ObtenerCondicionVM creai)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {

                int[] FechaMax = new int[creai.Condiciones.Count()];
                int valormax = 0;
                int posicion = -1;
                for (int i = 0; i < creai.Condiciones.Count(); i++)
                {
                    string valor = creai.Condiciones[i].Diashasta;
                    if (int.TryParse(valor, out FechaMax[i]))
                    {

                    }
                    else
                    {
                        FechaMax[i] = -1;
                    }
                }
                for (int i = 0; i < creai.Condiciones.Count(); i++)
                {
                    if (FechaMax[i] > valormax)
                    {
                        posicion = i;
                        valormax = FechaMax[i];
                    }
                }
                int diasmax = 0;
                if (posicion == -1)
                {
                    diasmax = 0;
                }
                else
                {
                    diasmax = FechaMax[posicion];
                }
                ServiceClient.EliminarParametros();
                ServiceClient.AdicionarParametro("NIT", usuarioActual.NitEmpresa);
                var resultinspeccion = ServiceClient.ObtenerObjetoJsonRestFul<EDPlanInspeccion>(UrlServicioAplicacion, CapacidadObtenerPlanInspeccion, RestSharp.Method.GET);
                if (resultinspeccion != null)
                {
                    var fechafin = resultinspeccion.Fecha;
                    fechafin = fechafin.AddDays(diasmax);
                    return Json(new { Data = fechafin }, JsonRequestBehavior.AllowGet);
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
        /// Metodo para Guardar el Plan de Accion de las condiciones Inseguras.
        /// </summary>
        /// <param name="plan"></param>
        /// <returns></returns>
        public ActionResult GuardarPlanesAccion(PlanAccionVM plan)
        {
            var sw = false;
            string condicioneshalladas = "";
            SG_SSTContext context = new SG_SSTContext();
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }

            List<PlanaccionPorCondicion> AC = context.Tbl_PlanAccionporCondicion.Where(ct => ct.Pk_Id_PlanaccionporCondicion > 0).ToList();
            if (AC.Count > 0)
            {
                foreach (var sct in AC)
                {
                    for (int i = 0; i < plan.Condiciones.Count; i++)
                    {
                        if (sct.Fk_Id_CondicionInsegura == plan.Condiciones[i].pkcondicionvm)
                        {
                            condicioneshalladas += (plan.Condiciones[i].DescribeCondicionvm.ToString() + " , ");
                            sw = true;
                        }
                    }
                }
                if (sw == true)
                {
                    return Json(new { Data = 0 }, JsonRequestBehavior.AllowGet);

                }
            }
            if (ModelState.IsValid)
            {
                var Planaccion = new EDPlanAccionInspeccion()

                {
                    PkPlanAccionInspeccionED = plan.pkplanaccionvm,
                    ActividadPlanAccionInspeccionED = plan.actividadvm,
                    ResponsablePlanAccionED = plan.responsablevm,
                    FechaFinPlanAccionED = plan.fechafinvm,
                    EstadoPlanAccionED = plan.estadovm.ToString(),
                    Condiciones = plan.Condiciones.Select(asis => new EDCondicionInsegura()
                    {
                        EDDiasDesde = asis.Diadesde,
                        EDDiasHasta = asis.Diahasta,
                        EDpkCondicion = asis.pkcondicionvm,
                        EDDescribeCondicion = asis.DescribeCondicionvm,
                    }).ToList(),

                };
                ServiceClient.EliminarParametros();
                var resultplan = ServiceClient.RealizarPeticionesPostJsonRestFul<EDPlanAccionInspeccion>(UrlServicioAplicacion, GuardarPlanAccion, Planaccion);
                if (resultplan != null)
                {
                    return Json(new { Data = resultplan }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    return Json(new { Data = string.Empty, Mensaje = "ERROR" }, JsonRequestBehavior.AllowGet);

                }

            }



            return Json(JsonRequestBehavior.AllowGet);
        }



        /// <summary>
        /// Metodo que Obtiene la consulta de las inspecciones Generadas en el sistema.
        /// </summary>
        /// <returns></returns>
        public ActionResult BuscarInspeccion()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            var busqueda = new BusquedaInspeccionVModel();
            busqueda.idempresa = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current) == null ? string.Empty : ObtenerUsuarioEnSesion(System.Web.HttpContext.Current).NitEmpresa;
            busqueda.RazonSocial = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current) == null ? string.Empty : ObtenerUsuarioEnSesion(System.Web.HttpContext.Current).RazonSocialEmpresa;
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("NIT", usuarioActual.NitEmpresa);
            var resultSede = ServiceClient.ObtenerArrayJsonRestFul<EDSede>(urlServicioEmpresas, CapacidadObtenerSedesPorNit, RestSharp.Method.GET);
            if (resultSede != null && resultSede.Count() > 0)
            {
                busqueda.Sedes = resultSede.Select(c => new SelectListItem()
                {
                    Value = c.IdSede.ToString(),
                    Text = c.NombreSede
                }).ToList();
            }
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("NIT", usuarioActual.NitEmpresa);
            var resulta = ServiceClient.ObtenerArrayJsonRestFul<EDTipoInspeccion>(UrlServicioAplicacion, CapacidadObtenerTipoInspeccion, RestSharp.Method.GET);
            if (resulta != null)
            {
                busqueda.Inspecciones = resulta.Select(ti => new SelectListItem()
                {
                    Text = ti.IdTipoInspeccion.ToString(),
                    Value = ti.DescripcionTipoInspeccion,
                }).ToList();
            }
            return View(busqueda);
        }
        /// <summary>
        /// Metodo que Obtiene informacion por tipos de Inspeccion 
        /// </summary>
        /// <param name="IdSede"></param>
        /// <param name="DescripcionTipoInspeccion"></param>
        /// <param name="FechaInicialB"></param>
        /// <param name="FechaFinal"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult BuscaTipoInspeccion(int IdSede, string DescripcionTipoInspeccion, DateTime? FechaInicialB, DateTime? FechaFinal)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("idsede", IdSede);
            ServiceClient.AdicionarParametro("tipoInspeccion", DescripcionTipoInspeccion);
            ServiceClient.AdicionarParametro("fechai", FechaInicialB);
            ServiceClient.AdicionarParametro("fechaf", FechaFinal);
            ServiceClient.AdicionarParametro("NIT", usuarioActual.NitEmpresa);
            var resultinspeccion = ServiceClient.ObtenerArrayJsonRestFul<EDInspeccion>(UrlServicioAplicacion, ObtenerInspeccionTipo, RestSharp.Method.GET);
            if (resultinspeccion != null)
            {
                return Json(new { Data = resultinspeccion }, JsonRequestBehavior.AllowGet);
            }
            return Json(JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Metodo que Obtiene las Inspecciones Generadas en el 
        /// sistema por un  rango de Fecha y una sede Especifica
        /// </summary>
        /// <param name="IdSede"></param>
        /// <param name="FechaIniVer"></param>
        /// <param name="FechaFinVer"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult BuscaInspeccionRangoFechas(int IdSede, string FechaIniVer, string FechaFinVer)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("idsede", IdSede);
            ServiceClient.AdicionarParametro("fechai", FechaIniVer);
            ServiceClient.AdicionarParametro("fechaf", FechaFinVer);
            ServiceClient.AdicionarParametro("NIT", usuarioActual.NitEmpresa);
            var resultfechas = ServiceClient.ObtenerArrayJsonRestFul<EDPlanAccionInspeccion>(UrlServicioAplicacion, ObtenerInspeccionRangoFechas, RestSharp.Method.GET);
            if (resultfechas != null)
            {
                
                return Json(new { Data = resultfechas }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Data = string.Empty, Mensaje = "ERROR" }, JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// Metodo para retornar a la vista el Servicio de Sedes para 
        /// poder realizar una busqueda de Planes de Accion por Sede y Fecha.
        /// </summary>
        /// <returns></returns>
        public ActionResult VerificarInspeccion()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Módulo.";
                return RedirectToAction("Login", "Home");
            }
            var verificar = new VerificarInspeccionVModel();
            verificar.idempresa = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current) == null ? string.Empty : ObtenerUsuarioEnSesion(System.Web.HttpContext.Current).NitEmpresa;
            verificar.RazonSocial = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current) == null ? string.Empty : ObtenerUsuarioEnSesion(System.Web.HttpContext.Current).RazonSocialEmpresa;
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("NIT", usuarioActual.NitEmpresa);
            var resultSede = ServiceClient.ObtenerArrayJsonRestFul<EDSede>(urlServicioEmpresas, CapacidadObtenerSedesPorNit, RestSharp.Method.GET);
            if (resultSede != null && resultSede.Count() > 0)
            {
                verificar.sedes = resultSede.Select(c => new SelectListItem()
                {
                    Value = c.IdSede.ToString(),
                    Text = c.NombreSede
                }).ToList();
            }
            return View(verificar);
        }

        //Metodo que envia al Modulo de Acciones Correctivas los planes de Accion enviados para tal fin.
        public ActionResult GuardarPlanAccionesCorrectivas(PlanCorrectivaVM planparac)
        {
            var sw = false;
            string centrosencontrados = "";
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Módulo.";
                return RedirectToAction("Login", "Home");
            }
            SG_SSTContext context = new SG_SSTContext();
            List<PlanAccionCorrectiva> AC = context.Tbl_PlanAccionCorrectiva.Where(ct => ct.Pk_Plan_Accion_Correctiva > 0).ToList();
            if (AC.Count > 0)
            {
                foreach (var sct in AC)
                {
                    for (int i = 0; i < planparac.correctivas.Count; i++)
                    {
                        if (sct.Fk_Id_PlanAcccionInspeccion == planparac.correctivas[i].pkplan)
                        {
                            centrosencontrados += (planparac.correctivas[i].pkplan.ToString() + " , ");
                            sw = true;
                        }
                    }
                }
            }
            if (sw == true)
            {
                return Json(new { Data = 0 }, JsonRequestBehavior.AllowGet);
                //ViewBag.mensaje = "Centro de Trabajo con ID: " + centrosencontrados + "  ya asignado a otra sede.";   
            }
            else
            {
                List<EDPlanAccionCorrectiva> pls = new List<EDPlanAccionCorrectiva>();
                if (ModelState.IsValid)
                {
                    foreach (var y in planparac.correctivas)
                    {
                        var PlanaccCorrectiva = new EDPlanAccionCorrectiva()
                        {
                            AdjuntoSeguimientoED = planparac.seguimiento,
                            NombreVerificadorED = planparac.verificador,
                            RespuestaED = planparac.accioncorrectiva,
                            FkPlaAccionED = y.pkplan,
                        };
                        pls.Add(PlanaccCorrectiva);
                    }
                }
                ServiceClient.EliminarParametros();
                var resultaccioncorrec = ServiceClient.RealizarPeticionesPostJsonRestFul<List<EDPlanAccionCorrectiva>>(UrlServicioAplicacion, GuardarPlanCorrectivo, pls);
                if (resultaccioncorrec != null)
                {
                    return Json(new { Data = resultaccioncorrec }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Data = string.Empty, Mensaje = "ERROR" }, JsonRequestBehavior.AllowGet);
                }
            }
        }


        /// <summary>
        /// Metodo para Listar los planes de Accion que generarles accion correctiva.
        /// </summary>
        /// <returns></returns>
        public ActionResult ListaPlanAccionesParaCorrectivas()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Módulo.";
                return RedirectToAction("Login", "Home");
            }
            var accioncorrectivavm = new AccionCorrectivaVM();
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("NIT", usuarioActual.NitEmpresa);
            ServiceClient.AdicionarParametro("IdEmpresa", usuarioActual.IdEmpresa);
            var resultcorrecctivas = ServiceClient.ObtenerArrayJsonRestFul<EDPlanAccionCorrectiva>(UrlServicioAplicacion, ObtenerParaCorrectiva, RestSharp.Method.GET);
            if (resultcorrecctivas != null)
            {
                accioncorrectivavm.acciones = resultcorrecctivas.Select(ac => new AccionCorrectivaVM()
                {
                    pkplanaccionvm = ac.PkplanAccionCorrectivaED,
                    respuestavm = ac.RespuestaED,
                    resumenvm = ac.InformacionActividadED,
                    procesoVM = ac.PkprocesoED,
                    DescribeProcesoVM = ac.NombreProcesoED,
                    sedeVM = usuarioActual.IdEmpresa,
                    DescribeSedeVM = ac.nombresedeED,

                }).ToList();


                return View(accioncorrectivavm);
            }
            else
            {

                return View();
            }


        }

        /// <summary>
        /// Metodo para Obtener la Lista de 
        /// todas las condiciones Inseguras.
        /// </summary>
        /// <returns></returns>
        public ActionResult ListaTodasLasCorrectivas()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Módulo.";
                return RedirectToAction("Login", "Home");
            }
            var todasaccioncorrectivavm = new AccionCorrectivaVM();
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("NIT", usuarioActual.NitEmpresa);
            ServiceClient.AdicionarParametro("IdEmpresa", usuarioActual.IdEmpresa);
            var resulttodascorrecctivas = ServiceClient.ObtenerArrayJsonRestFul<EDPlanAccionCorrectiva>(UrlServicioAplicacion, ObtenerTodasCorrectivas, RestSharp.Method.GET);
            if (resulttodascorrecctivas != null)
            {
                return Json(new { Data = resulttodascorrecctivas }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Data = string.Empty, Mensaje = "ERROR" }, JsonRequestBehavior.AllowGet);
            }

        }

        /// <summary>
        /// metodo que Obtiene la Informacion 
        /// de una condicion Insegura por su ID.
        /// </summary>
        /// <param name="EDpkCondicion"></param>
        /// <returns></returns>
        public ActionResult ObtenerInformacionCondicionInspeccion(int EDpkCondicion, int EDpkinspeccion)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarce para Ingresar a este Módulo.";
                return RedirectToAction("Login", "Home");
            }

            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("NIT", usuarioActual.NitEmpresa);
            ServiceClient.AdicionarParametro("IdCondicion", EDpkCondicion);
            ServiceClient.AdicionarParametro("IdInspeccion", EDpkinspeccion);
            var resultCondicion = ServiceClient.ObtenerObjetoJsonRestFul<EDCondicionInsegura>(UrlServicioAplicacion, ObtenerCondicionInsegura, RestSharp.Method.GET);
            if (resultCondicion != null)
            {
                
               
                return Json(new { Data = resultCondicion }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Data = string.Empty, Mensaje = "ERROR" }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Metodo que modifica en la Busqueda de inspeccion la condicion Insegura Seleccionada.
        /// </summary>
        /// <param name="PkCondicionVM"></param>
        /// <param name="DescribeCondicionVM"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ModificarCondicion(CondicionesVModel condiciones)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Módulo.";
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                var InfoConfiguracion = new EDCondicionInsegura()
                {
                    EDPkInspeccion = condiciones.idinspeccion,
                    EDpkCondicion = condiciones.pkCondicionVM,
                    EDDescribeCondicion = condiciones.DescribeCondicionVM,
                    EDUbicacionespecifica = condiciones.UbicacionespecificaVM,
                    EDRiesgopeligro = condiciones.RiesgopeligroVM,
                    EDClasificacionRiesgo = condiciones.ClasificacionRiesgoVM,
                    EDConfiguracioncondicion = condiciones.ConfiguracioncondicionVM,
                    EDEvidenciacondicion = condiciones.EvidenciacondicionVM,
                    EDOtraClasePeligro=condiciones.OtroRiesgoVM,
                    //EDEstadoCondicion=EstadoConfiguracionVM,
                };



                ServiceClient.EliminarParametros();
                var resultmodificacion = ServiceClient.RealizarPeticionesPostJsonRestFul<EDCondicionInsegura>(UrlServicioAplicacion, EditarCondicion, InfoConfiguracion);
                if (resultmodificacion != null)
                {
                    return Json(new { Data = resultmodificacion }, JsonRequestBehavior.AllowGet);

                }
                else
                {

                    return Json(new { Data = string.Empty, Mensaje = "ERROR" }, JsonRequestBehavior.AllowGet);
                }
               
            }
            return Json(new { Data = string.Empty, Mensaje = "ERROR" }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Metodo que Guardar en el Modulo de Acciones Correctivas los planes de Accion de Inspeccion 
        /// que son seleccionados por el usuario para generarle las acciones Correctivas.
        /// </summary>
        /// <param name="incomingAccion"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult PostGuardar1(EDAccion incomingAccion)
        {
            string status = "";
            bool probar = false;
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                status = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { status, probar });
            }

            incomingAccion.Fk_Id_Empresa = usuarioActual.IdEmpresa;

            var datosede = (from ds in db.Tbl_Sede
                            join emp in db.Tbl_Empresa on ds.Fk_Id_Empresa equals usuarioActual.IdEmpresa
                            select ds.Pk_Id_Sede).FirstOrDefault();
            incomingAccion.Halla_Sede = datosede.ToString();
            incomingAccion.Tipo = "Acción correctiva";
            incomingAccion.Fecha_dil = DateTime.Now;
            incomingAccion.Fecha_hall = DateTime.Now;
            incomingAccion.Fecha_ocurrencia = DateTime.Now;
            incomingAccion.Clase = "No Conformidad";
            incomingAccion.Halla_Num_Doc = usuarioActual.Documento;
            incomingAccion.Origen = "Inspección";
            var ConsultaWS = BuscarPersonaDocumentoCargoInspeccion(usuarioActual.Documento, usuarioActual.NitEmpresa);
            if (ConsultaWS[0] != null)
            {
                incomingAccion.Halla_Nombre = ConsultaWS[0];
            }
            if (ConsultaWS[1] != null)
            {
                incomingAccion.Halla_Cargo = ConsultaWS[1];
            }
            if (ConsultaWS[2] != null)
            {
                if (ConsultaWS[2] != null)
                {
                    if (ConsultaWS[2] != "")
                    {
                        incomingAccion.Halla_TipoDoc = ConsultaWS[2];
                    }
                    else
                    {
                        incomingAccion.Halla_TipoDoc = "Cédula de Ciudadanía";
                    }
                }
                else
                {
                    incomingAccion.Halla_TipoDoc = "Cédula de Ciudadanía";
                }

            }
            //Buscar ID Cargo
            List<EDCargo> ListaEDCargo = new List<EDCargo>();
            ListaEDCargo = LNAcciones.ListaCargos();
            EDCargo EDCargo = ListaEDCargo.Where(s => s.NombreCargo == incomingAccion.Halla_Cargo).FirstOrDefault();
            if (EDCargo != null)
            {
                incomingAccion.Halla_Cargo = EDCargo.IDCargo.ToString();
            }
            else
            {
                incomingAccion.Halla_Cargo = "0";
            }
            //Buscar Sede
            int IdSede = 0;
            bool isNumeric = int.TryParse(incomingAccion.Halla_Sede, out IdSede);
            if (isNumeric == true)
            {
                List<EDSede> ListaSede = new List<EDSede>();
                ListaSede = LNEmpresa.ObtenerSedesPorNit(usuarioActual.NitEmpresa);
                EDSede EDSede = ListaSede.Find(s => s.IdSede == IdSede);
                if (EDSede != null)
                {
                }
                else
                {
                    incomingAccion.Halla_Sede = "0";
                }
            }
            incomingAccion.Estado = "Abierta";
            incomingAccion.Id_Accion = LNAcciones.NuevoNumeroACP(usuarioActual.IdEmpresa);
            int Idaccionmostrar = 0;
            try
            {
                Idaccionmostrar = LNAcciones.NuevoNumeroACP(usuarioActual.IdEmpresa);
            }
            catch
            {

            }
            int UltimoId = 0;
            bool ProbarGuardar = false;
            try
            {
                ProbarGuardar = LNAcciones.GuardarAccionbool(incomingAccion);
                if (ProbarGuardar)
                {
                    UltimoId = LNAcciones.GuardarAccion(usuarioActual.IdEmpresa);
                    string numeroid = UltimoId.ToString();
                    List<EDHallazgo> HallazgoLista = incomingAccion.HallazgoLista;
                    EDHallazgo Hallazgo = HallazgoLista.FirstOrDefault();
                    Hallazgo.Fk_Id_Accion = UltimoId;
                    LNAcciones.GuardarHallazgo(Hallazgo);
                    status = "Acción correctiva guardada exitosamente";
                    probar = true;
                    return Json(new { status, probar, Idaccionmostrar });
                }
                else
                {
                    status = "No se pudo guardar la 'Acción' en la aplicación, por favor intente de nuevo";
                    probar = true;
                    return Json(new { status, probar });
                }
            }
            catch (Exception)
            {
                status = "No se pudo guardar la 'Acción' en la aplicación, por favor intente de nuevo";
                probar = false;
                return Json(new { status, probar });
            }
        }


        /// <summary>
        /// Metodo que busca el cargo y datos de quien registra la inspeccion
        /// </summary>
        /// <param name="documento"></param>
        /// <param name="nit"></param>
        /// <returns></returns>
        private string[] BuscarPersonaDocumentoCargoInspeccion(string documento, string nit)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {

            }
            List<EDTipoDocumento> EDTipoDoc = LNAcciones.BuscarTipoDocumento();
            List<TipoDocumento> ListaDocumentos = new List<TipoDocumento>();
            List<string> ListaDocumentosStr = new List<string>();
            foreach (var item in EDTipoDoc)
            {
                TipoDocumento td = new TipoDocumento();
                td.PK_IDTipo_Documento = item.Id_Tipo_Documento;
                td.Sigla = item.Sigla;
                td.Descripcion = item.Descripcion;
                ListaDocumentos.Add(td);
                ListaDocumentosStr.Add(td.Descripcion);
                ListaDocumentosStr.Add(td.Sigla);
            }
            string[] resultado = new string[3] { string.Empty, string.Empty, string.Empty };
            bool probar = false;
            try
            {
                foreach (var item in ListaDocumentosStr)
                {
                    if (!string.IsNullOrEmpty(documento) && !string.IsNullOrEmpty(item))
                    {
                        var cliente = new RestSharp.RestClient(ConfigurationManager.AppSettings["Url"]);
                        var request = new RestRequest(ConfigurationManager.AppSettings["consultaAfiliado"], RestSharp.Method.GET);
                        request.RequestFormat = DataFormat.Xml;
                        request.Parameters.Clear();
                        request.AddParameter("tpDoc", "CC");
                        request.AddParameter("doc", documento);
                        request.AddHeader("Content-Type", "application/json");
                        request.AddHeader("Accept", "application/json");

                        //se omite la validación de certificado de SSL
                        ServicePointManager.ServerCertificateValidationCallback = delegate
                        { return true; };
                        IRestResponse<List<AfiliadoModel>> response = cliente.Execute<List<AfiliadoModel>>(request);
                        var result = response.Content;
                        if (!string.IsNullOrWhiteSpace(result))
                        {
                            var respuesta = Newtonsoft.Json.JsonConvert.DeserializeObject<List<AfiliadoModel>>(result);
                            var afiliado = respuesta.Where(a => a.Estado == "Activo").FirstOrDefault();
                            if (afiliado == null)
                            { }
                            else
                            {
                                if (nit == afiliado.IdEmpresa)
                                {
                                    resultado[0] = afiliado.Nombre1 + " " + afiliado.Nombre2 + " " + afiliado.Apellido1 + " " + afiliado.Apellido2;
                                    resultado[1] = afiliado.Ocupacion;
                                    resultado[2] = "CC";
                                    probar = true;
                                    return resultado;
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }
                }



                if (resultado[0] == string.Empty)
                {
                    if (!string.IsNullOrEmpty(documento))
                    {
                        var cliente = new RestSharp.RestClient(ConfigurationManager.AppSettings["Url"]);
                        var request = new RestRequest(ConfigurationManager.AppSettings["consultaAfiliado"], RestSharp.Method.GET);
                        request.RequestFormat = DataFormat.Xml;
                        request.Parameters.Clear();
                        request.AddParameter("tpDoc", "CC");
                        request.AddParameter("doc", documento);
                        request.AddHeader("Content-Type", "application/json");
                        request.AddHeader("Accept", "application/json");

                        //se omite la validación de certificado de SSL
                        ServicePointManager.ServerCertificateValidationCallback = delegate
                        { return true; };
                        IRestResponse<List<AfiliadoModel>> response = cliente.Execute<List<AfiliadoModel>>(request);
                        var result = response.Content;
                        if (!string.IsNullOrWhiteSpace(result))
                        {
                            var respuesta = Newtonsoft.Json.JsonConvert.DeserializeObject<List<AfiliadoModel>>(result);
                            if (respuesta.Count != 0)
                            {
                                var afiliado = respuesta.Where(a => a.Estado == "Activo").FirstOrDefault();
                                if (afiliado == null)
                                {

                                }
                                else
                                {
                                    if (nit == afiliado.IdEmpresa)
                                    {
                                        resultado[0] = afiliado.Nombre1 + " " + afiliado.Nombre2 + " " + afiliado.Apellido1 + " " + afiliado.Apellido2;
                                        resultado[1] = afiliado.Ocupacion;
                                        resultado[2] = afiliado.TipoDocumento;
                                        probar = true;
                                        return resultado;
                                    }
                                }
                            }

                        }
                    }
                    return resultado;
                }
                else
                    return resultado;
            }
            catch (Exception ex)
            {
                return resultado;
            }
        }




    }

}


