using SG_SST.EntidadesDominio.Planificacion;
using SG_SST.ServiceRequest;
using SG_SST.Services.Empresas.IServices;
using SG_SST.Services.Empresas.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using SG_SST.Models;
using SG_SST.Models.DxCondicionesSalud;
using SG_SST.Controllers.Base;
using SG_SST.Models.Planificacion;
using SG_SST.Audotoria;
using Rotativa.Options;
using SG_SST.Services.General.IServices;
using SG_SST.Services.General.Services;
using SG_SST.Models.Empresas;
using SG_SST.EntidadesDominio.Empresas;
using SG_SST.Logica.Planificacion;

namespace SG_SST.Controllers.Planificacion
{
    public class DxGralCondicionSaludController : BaseController
    {
        
        private SG_SSTContext db = new SG_SSTContext();
        private ISedeServicios sedeServicio = new SedeServicios();
        private IProcesoServicios procesoServicios = new ProcesoServicios();
        private IRecursosServicios recursosServicios = new RecursosServicios();
        private int anioIncial = Int32.Parse(ConfigurationManager.AppSettings["anioInicial"]);
        private int anioFinal = Int32.Parse(ConfigurationManager.AppSettings["anioFinal"]);
        string urlPlanificacion = ConfigurationManager.AppSettings["UrlServicioPlanificacion"];
        string CapacidadCargarDxGralDeSalud = ConfigurationManager.AppSettings["CapacidadConsultarDocumentosDx"];
        string CapacidadGuardarDxGralDeSalud = ConfigurationManager.AppSettings["CapacidadGuardarDocumentosDx"];
        string CapacidadConsultarDxGralDeSalud = ConfigurationManager.AppSettings["CapacidadConsultarDocumentoDx"];
        string CapacidadEliminarDxGralDeSalud = ConfigurationManager.AppSettings["CapacidadEliminarDocumentoDx"];
        string CapacidadEliminarDxGralDeSaludGeneral = ConfigurationManager.AppSettings["CapacidadEliminarDx"];
        string CapacidadObtenerLugares = ConfigurationManager.AppSettings["CapacidadLugares"];
        string CapacidadCrearDiagnostico = ConfigurationManager.AppSettings["CapacidadGuardarDiagnostico"];
        string CapacidadVisualiazarHistorico = ConfigurationManager.AppSettings["CapacidadHistorico"];
        string CapacidadBuscarHistorico = ConfigurationManager.AppSettings["CapacidadBuscarHistorico"];        
        string CapacidadVisualiazarHistoricoAnio = ConfigurationManager.AppSettings["CapacidadHistoricoAnio"];
        string CapacidadDescargarHistoricoAnio = ConfigurationManager.AppSettings["CapacidadDescargarHistoricoAnio"];
        string CapacidadDescargarReporte = ConfigurationManager.AppSettings["CapacidadDescargarReporte"];
        string CapacidadCargarDx = ConfigurationManager.AppSettings["CapacidadCargarDx"];
       
        string rutaDocumentosDx = ConfigurationManager.AppSettings["rutaDocumentosDx"];

        LNPerfilSocioDemografico lnPerfil = new LNPerfilSocioDemografico();

        // GET: DxGralCondicionSalud
        #region metodos para cargar el documento de diagnostico
        public ActionResult Index()
        {
            return View();
        }


        //public ActionResult CargarDxGralDeSalud()
        //{
        //    var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
        //    if (usuarioActual == null)
        //    {
        //        ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
        //        return RedirectToAction("Login", "Home");
        //    }
        //    ViewBag.idSede = new SelectList(sedeServicio.SedesPorEmpresa(usuarioActual.IdEmpresa), "Pk_Id_Sede", "Nombre_Sede");
        //    ServiceClient.EliminarParametros();
        //    ServiceClient.AdicionarParametro("idEmpresa", usuarioActual.IdEmpresa);
        //    var result = ServiceClient.ObtenerArrayJsonRestFul<EDDocDxSalud>(urlPlanificacion, CapacidadCargarDxGralDeSalud, RestSharp.Method.GET);
        //    List<DocDxSaludModel> docs = result.Select(doc => new DocDxSaludModel(){
        //        idEDDocDxSalud = doc.idEDDocDxSalud,
        //        Nombre_Diagnostico = doc.Nombre_Diagnostico
        //    }).ToList();
        //    return View(docs);
        //}

        //[HttpPost]
        //public ActionResult CargarDxGralDeSalud(EDDocDxSalud documento, HttpPostedFileBase File)
        //{
        //    var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
        //    if (usuarioActual == null)
        //    {
        //        ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
        //        return RedirectToAction("Login", "Home");
        //    }
        //    bool resultMetodologias = false;
        //    if (File != null && Path.GetExtension(File.FileName).ToLower() == ".pdf" || Path.GetExtension(File.FileName).ToLower() == ".xlsx"
        //        || Path.GetExtension(File.FileName).ToLower() == ".xls")
        //    {
        //        documento.Nombre_Documento = File.FileName;
        //        ServiceClient.EliminarParametros();
        //        resultMetodologias = ServiceClient.PeticionesPostJsonRestFulRespuetaBool<EDDocDxSalud>(urlPlanificacion,CapacidadGuardarDxGralDeSalud, documento);
        //        if (resultMetodologias)
        //        {
        //            var img = Path.Combine(Server.MapPath(rutaDocumentosDx), File.FileName);
        //            File.SaveAs(img);
        //        }
        //    }

        //    ViewBag.idSede = new SelectList(sedeServicio.SedesPorEmpresa(usuarioActual.IdEmpresa), "Pk_Id_Sede", "Nombre_Sede");
        //    ServiceClient.EliminarParametros();
        //    ServiceClient.AdicionarParametro("idEmpresa", usuarioActual.IdEmpresa);
        //    var result = ServiceClient.ObtenerArrayJsonRestFul<EDDocDxSalud>(urlPlanificacion, CapacidadCargarDxGralDeSalud, RestSharp.Method.GET);
        //    List<DocDxSaludModel> docs = result.Select(doc => new DocDxSaludModel()
        //    {
        //        idEDDocDxSalud = doc.idEDDocDxSalud,
        //        Nombre_Diagnostico = doc.Nombre_Diagnostico
        //    }).ToList();
        //    ViewBag.guardadoConExito = resultMetodologias;
        //    return View("CargarDxGralDeSalud", docs);
        //}

        public ActionResult EliminarDocDxSalud(int idDocDx) 
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
                return RedirectToAction("Login", "Home");
            }
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("idDocDx", idDocDx);
            EDDocDxSalud doc = ServiceClient.ObtenerObjetoJsonRestFul<EDDocDxSalud>(urlPlanificacion, CapacidadConsultarDxGralDeSalud, RestSharp.Method.GET);
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("idDocDx", idDocDx);
            bool resultMetodologias = ServiceClient.PeticionesPostJsonRestFulRespuetaBool(urlPlanificacion,CapacidadEliminarDxGralDeSalud, RestSharp.Method.DELETE);
            if (resultMetodologias)
            {
                var path = Server.MapPath(rutaDocumentosDx);
                var file = doc.Nombre_Documento;
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
                        registraLog.RegistrarError(typeof(DxGralCondicionSaludController), string.Format("Error al eliminar el documento del servidor   {0}: {1}", DateTime.Now, e.StackTrace), e);                                              
                    }
                }
            }
            return Json(new
            {
                success = resultMetodologias               
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CargarDocDx(int idDocDx)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
                return RedirectToAction("Login", "Home");
            }
            ViewBag.idDocDx = idDocDx;
            return View("VisualizadorDocDX");
        }

        public ActionResult VisualizadorDocDX(int idDocDx)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
                return RedirectToAction("Login", "Home");
            }
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("idDocDx", idDocDx);
            EDDocDxSalud doc = ServiceClient.ObtenerObjetoJsonRestFul<EDDocDxSalud>(urlPlanificacion,CapacidadConsultarDxGralDeSalud, RestSharp.Method.GET);
            var path = Server.MapPath(rutaDocumentosDx);
            var file = doc.Nombre_Documento;
            var fullPath = Path.Combine(path, file);           
            byte[] fileBytes = System.IO.File.ReadAllBytes(fullPath);
            MemoryStream ms = new MemoryStream(fileBytes, 0, 0, true, true);
            Response.AddHeader("content-disposition", "attachment;filename="+doc.Nombre_Documento+"");
            Response.Buffer = true;
            Response.Clear();
            Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length);
            Response.OutputStream.Flush();
            Response.End();            
            if (Path.GetExtension(doc.Nombre_Documento).ToLower() == ".pdf")
            {               
                return new FileStreamResult(Response.OutputStream, "application/pdf");
            }
            else 
            {              
                return new FileStreamResult(Response.OutputStream, "application/vnd.ms-excel");
            }
        }
        #endregion

        #region crear diagnostico
        [HttpGet]
        public ActionResult CrearDxGralDeSalud() 
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
                return RedirectToAction("Login", "Home");
            }
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("idEmpresa", usuarioActual.IdEmpresa);
            
            List<Proceso> procesos = procesoServicios.ObtenerProcesosPrincipales(usuarioActual.IdEmpresa);
            ViewBag.Procesos = new SelectList(procesos, "Pk_Id_Proceso", "Descripcion_Proceso");
            ViewBag.Pk_Id_Sede = new SelectList(sedeServicio.SedesPorEmpresa(usuarioActual.IdEmpresa), "Pk_Id_Sede", "Nombre_Sede");
            ViewBag.FK_Tipo_De_Peligro = new SelectList(db.Tbl_Tipo_De_Peligro, "PK_Tipo_De_Peligro", "Descripcion_Del_Peligro");

            ViewBag.vigencia = new SelectList(recursosServicios.ObtenerPeriodosAnios(anioIncial, DateTime.Now.Year), "Text", "Value", DateTime.Now.Year);
            return View();
        }

        [HttpPost]
        public ActionResult CrearDxGralDeSalud(EDDxSalud dxSalud) 
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
                return RedirectToAction("Login", "Home");
            }
            ServiceClient.EliminarParametros();
           
            var result = ServiceClient.RealizarPeticionesPostJsonRestFul<EDDxSalud>(urlPlanificacion, CapacidadCrearDiagnostico, dxSalud);
            if (result.IdDxCondicionesDeSalud > 0) 
            {
                return RedirectToAction("HistoricoDxSedesPorAnio", new { idDxSalud = result.IdDxCondicionesDeSalud });
            }
            List<Proceso> procesos = procesoServicios.ObtenerProcesosPrincipales(usuarioActual.IdEmpresa);
            ViewBag.Procesos = new SelectList(procesos, "Pk_Id_Proceso", "Descripcion_Proceso");
            ViewBag.Pk_Id_Sede = new SelectList(sedeServicio.SedesPorEmpresa(usuarioActual.IdEmpresa), "Pk_Id_Sede", "Nombre_Sede");
            ViewBag.FK_Tipo_De_Peligro = new SelectList(db.Tbl_Tipo_De_Peligro, "PK_Tipo_De_Peligro", "Descripcion_Del_Peligro");
            ViewBag.vigencia = new SelectList(recursosServicios.ObtenerPeriodosAnios(anioIncial, anioFinal), "Text", "Value", DateTime.Now.Year);
            return View();
           
        }

        public ActionResult HistoricoDxPorSedes() 
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
                return RedirectToAction("Login", "Home");
            }
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("idEmpresa", usuarioActual.IdEmpresa);
            var result = ServiceClient.ObtenerArrayJsonRestFul<EDDxSalud>(urlPlanificacion, CapacidadVisualiazarHistorico, RestSharp.Method.GET);
            ViewBag.Pk_Id_Sede = new SelectList(sedeServicio.SedesPorEmpresa(usuarioActual.IdEmpresa), "Pk_Id_Sede", "Nombre_Sede");
            return View(result);
        }

        public ActionResult BuscarHistoricoDxPorSedes(int strZonaLugar=-1)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
                return View();
            }
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("idEmpresa", usuarioActual.IdEmpresa);
            ServiceClient.AdicionarParametro("strZonaLugar", strZonaLugar);
            var result = ServiceClient.ObtenerArrayJsonRestFul<EDDxSalud>(urlPlanificacion, CapacidadBuscarHistorico, RestSharp.Method.GET);
            return PartialView("HistoricoDxPorSedesVP", result.ToList());            
        }
        public ActionResult HistoricoDxSedesPorAnio(int idDxSalud)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
                return RedirectToAction("Login", "Home");
            }
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("idDxSalud", idDxSalud);           
            var result = ServiceClient.ObtenerArrayJsonRestFul<EDDxSalud>(urlPlanificacion, CapacidadVisualiazarHistoricoAnio, RestSharp.Method.GET);

            return View("DetalleDxCondicionesSalud", result.FirstOrDefault());
        }

        public ActionResult EliminarDxSalud(int idDx)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
                return RedirectToAction("Login", "Home");
            }            
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("idDx", idDx);
            bool resultMetodologias = ServiceClient.PeticionesPostJsonRestFulRespuetaBool(urlPlanificacion, CapacidadEliminarDxGralDeSaludGeneral, RestSharp.Method.DELETE);
            
            return Json(new
            {
                success = resultMetodologias
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion
        #region descargar de archivos
        public FileResult DescargarExcelDxSedesPorAnio(int idDxSalud)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
            }
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("idDxSalud", idDxSalud);            
            var result = ServiceClient.ObtenerObjetoJsonRestFul<byte[]>(urlServicioPlanificacion, CapacidadDescargarHistoricoAnio, RestSharp.Method.GET);
            return File(result, "application/vnd.ms-excel", "Historico.xlsx");
        }

        public ActionResult DescargarPDFDxSedesPorAnio(int idDxSalud)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
                return View();
            }
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("idDxSalud", idDxSalud);            
            var result = ServiceClient.ObtenerArrayJsonRestFul<EDDxSalud>(urlPlanificacion, CapacidadVisualiazarHistoricoAnio, RestSharp.Method.GET);

            string SwitchNombreEmpresa = System.Net.WebUtility.UrlEncode(usuarioActual.RazonSocialEmpresa);
            string SwitchNitEmpresa = System.Net.WebUtility.UrlEncode(usuarioActual.NitEmpresa);
         
            var uriFooter = "https://alissta.gov.co/Acciones/Footer";
            var headerurl = "https://alissta.gov.co/Acciones/Header?NombreEmpresa=" + SwitchNombreEmpresa + "&NitEmpresa=" + SwitchNitEmpresa ;
            string cusomtSwitches = string.Format("--footer-line --print-media-type --allow {0} --footer-html {0} --header-line --print-media-type --allow {1} --header-html {1} --header-spacing 5", uriFooter, headerurl);

            return new Rotativa.PartialViewAsPdf("DetalleDxCondicionesSaludPDF", result.FirstOrDefault()) 
            {
                FileName = "Diagnóstico de condiciones de salud.pdf",              
              CustomSwitches = cusomtSwitches 
            };
        }

        [AllowAnonymous]
        public ActionResult Footer()
        {
            return View("Footer");
        }


        [AllowAnonymous]
        public ActionResult Header(string NombreEmpresa, string NitEmpresa, string NombreInforme)
        {
            ViewBag.NombreEmpresa = NombreEmpresa;
            ViewBag.NitEmpresa = NitEmpresa;
            ViewBag.NombreInforme = NombreInforme;
            return View("Header");
        }


        public FileResult DescargarReporteExcel()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
            }
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("idEmpresa", usuarioActual.IdEmpresa);
            var result = ServiceClient.ObtenerObjetoJsonRestFul<byte[]>(urlServicioPlanificacion, CapacidadDescargarReporte, RestSharp.Method.GET);
            return File(result, "application/vnd.ms-excel", "Reporte.xlsx");
        }

        public FileResult DescargarPlantillaDX()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
            }
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("idEmpresa", 0);
            var result = ServiceClient.ObtenerObjetoJsonRestFul<byte[]>(urlServicioPlanificacion, CapacidadDescargarReporte, RestSharp.Method.GET);
            return File(result, "application/vnd.ms-excel", "PlantillaDx.xlsx");
        }
        #endregion

        #region cargue masivo DX General Condiciones de salud



        public FileResult DescargarReporteExcelSedesYProcesos()
        {

            EDProcesoSede infoProcesoSede = new EDProcesoSede();
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
            }

            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("NIT", usuarioActual.NitEmpresa);
            var resultSede = ServiceClient.ObtenerArrayJsonRestFul<EDSede>(urlServicioEmpresas, CapacidadObtenerSedesPorNit, RestSharp.Method.GET);

            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("NIT", usuarioActual.NitEmpresa);
            var resultProceso = ServiceClient.ObtenerArrayJsonRestFul<EDProceso>(urlServicioEmpresas, CapacidadObtenerprocesosEmpresa, RestSharp.Method.GET);

            infoProcesoSede.sedes = resultSede.ToList();
            infoProcesoSede.procesos = resultProceso.ToList();

            var result = lnPerfil.ObtenerReporteExcelProcesoYSede(infoProcesoSede);
          
            return File(result, "application/vnd.ms-excel", "Códigos Plantilla DX General de condiciones.xlsx");
        }
        public ActionResult CargarDxGralDeSalud()
        {
            EDProcesoSede infoProcesoSede = new EDProcesoSede();
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
                return RedirectToAction("Login", "Home");
            }
            else
            {
                ServiceClient.EliminarParametros();
                ServiceClient.AdicionarParametro("NIT", usuarioActual.NitEmpresa);
                var resultSede = ServiceClient.ObtenerArrayJsonRestFul<EDSede>(urlServicioEmpresas, CapacidadObtenerSedesPorNit, RestSharp.Method.GET);

                ServiceClient.EliminarParametros();
                ServiceClient.AdicionarParametro("NIT", usuarioActual.NitEmpresa);
                var resultProceso = ServiceClient.ObtenerArrayJsonRestFul<EDProceso>(urlServicioEmpresas, CapacidadObtenerprocesosEmpresa, RestSharp.Method.GET);

                infoProcesoSede.sedes = resultSede.ToList();
                infoProcesoSede.procesos = resultProceso.ToList();
                return View(infoProcesoSede);
            }

        }

        public ActionResult DescargarPlantilla()
        {
            string fileName = "Plantilla de Cargue DxSalud.xlsx";
            string path = System.AppDomain.CurrentDomain.BaseDirectory + "Plantillas/";
            byte[] fileBytes = System.IO.File.ReadAllBytes(path + "Plantilla de Cargue DxSalud.xlsx");
            return File(fileBytes, "application/vnd.ms-excel", fileName);


        }



        [HttpPost]
        public ActionResult CargueMasivo(object form_data)
        {
            try
            {
                var objEvaluacion = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);



                if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
                {
                    var pic = System.Web.HttpContext.Current.Request.Files["cargarArchivo"];

                    HttpPostedFileBase file = new HttpPostedFileWrapper(pic);
                    if (file.FileName.EndsWith("xls") || file.FileName.EndsWith("xlsx"))
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        string path = string.Empty;
                        //if (int.Parse(idempresausuaria) > 0)
                        //    path = Path.Combine(rutaPlantillaAusentismo, objEvaluacion.NitEmpresa, idempresausuaria);
                        //else
                        path = Path.Combine(rutaPlantillaAusentismo, objEvaluacion.NitEmpresa);


                        if (!Directory.Exists(path))
                            Directory.CreateDirectory(path);


                        path = Path.Combine(path, fileName);
                        if (System.IO.File.Exists(path))
                            System.IO.File.Delete(path);
                        file.SaveAs(path);

                        EDCarguePerfil cargue = new EDCarguePerfil();
                        //cargue.Id_Empresa_Usuaria = int.Parse(idempresausuaria);

                        cargue.path = path;
                        cargue.NitEmpresa = objEvaluacion.NitEmpresa;
                 
                        ServiceClient.EliminarParametros();
                        var result = ServiceClient.RealizarPeticionesPostJsonRestFul<EDCarguePerfil>(urlServicioPlanificacion, CapacidadCargarDx, cargue);


                        if (result != null)
                        {
                            if (result.Message.Equals("OK"))
                                return Json(new { Data = "Plantilla cargada correctamente!.", Mensaje = "Success" });
                            else
                                return Json(new { Data = result.Message, Mensaje = "ERROR" });
                        }
                        else
                            return Json(new { Data = "Se presentó un error de comunicación con el servidor; por favor intente nuevamente o comuníquese con el administrador del sistema.", Mensaje = "ERROR" });

                    }
                    else
                    {
                        return Json(new { Data = "Debe seleccionar un archivo en formato Excel con extensión .xls o .xlsx", Mensaje = "ERROR" });
                    }
                }
                else
                    return Json(new { Data = "Se presentó un error en la carga del archivo; por favor intente ingresando nuevamente o comuníquese con el administrador del sistema.", Mensaje = "ERROR" });

            }
            catch (Exception e)
            {
                return Json(new { Data = "Se presentó un error con la conexión.", Mensaje = "CONEXION" });

            }
        }
        #endregion
    }
}