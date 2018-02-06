using SG_SST.EntidadesDominio.Aplicacion;
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
using SG_SST.Controllers.Base;
using SG_SST.Models.Aplicacion;
using SG_SST.Audotoria;
using Rotativa.Options;

namespace SG_SST.Controllers.Aplicacion
{
    public class AdquisicionBienesController : BaseController
    {
        private SG_SSTContext db = new SG_SSTContext();
        string urlAplicacion = ConfigurationManager.AppSettings["UrlServicioAplicacion"];
        string CapacidadGuardarManulAdquisiciones = ConfigurationManager.AppSettings["CapacidadGuardarManulAdquisiciones"];
        string CapacidadConsultarAdquisiciones = ConfigurationManager.AppSettings["CapacidadConsultarManualAdquisicion"];
        string CapacidadEliminarManualAdqBienes = ConfigurationManager.AppSettings["CapacidadEliminarManualBienes"];
        string CapacidadConsultarManulAdq = ConfigurationManager.AppSettings["CapacidadConsultarManualAdq"];
        string rutaManualAdquisicion = ConfigurationManager.AppSettings["rutaManulaAdq"];

        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CrearManualesAdquisicion()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            bool resultCargarManuales = false;
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("idEmpresa", usuarioActual.IdEmpresa);

            var result = ServiceClient.ObtenerArrayJsonRestFul<EDManualAdquisicion>(urlAplicacion, CapacidadConsultarAdquisiciones, RestSharp.Method.GET);
            List<ManualGuiaAdBienes> docs = result.Select(doc => new ManualGuiaAdBienes()
            {
                PK_ManualGuiaAdBienes = doc.Id_Manuales_Bienes,
                Nombre_Manual = doc.Nombre_Manual,
                FK_Empresa = doc.Fk_Empresa,
            }).ToList();
            ViewBag.guardadoConExito = resultCargarManuales;
            return View("CrearManualesAdquisicion", docs);
        }

        [HttpPost]
        public ActionResult CrearManualesAdquisicion(EDManualAdquisicion documento, HttpPostedFileBase File)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            bool resultCargarManuales = false;
            if (File != null && Path.GetExtension(File.FileName).ToLower() == ".pdf" || Path.GetExtension(File.FileName).ToLower() == ".xlsx"
                || Path.GetExtension(File.FileName).ToLower() == ".doc" || Path.GetExtension(File.FileName).ToLower() == ".docx"
                || Path.GetExtension(File.FileName).ToLower() == ".ppt" || Path.GetExtension(File.FileName).ToLower() == ".pptx"
                || Path.GetExtension(File.FileName).ToLower() == ".xls" || Path.GetExtension(File.FileName).ToLower() == ".png"
                || Path.GetExtension(File.FileName).ToLower() == ".jpg")
            {
                documento.Nombre_Manual = File.FileName;
                documento.Fk_Empresa = usuarioActual.IdEmpresa;
                ServiceClient.EliminarParametros();
                resultCargarManuales = ServiceClient.PeticionesPostJsonRestFulRespuetaBool<EDManualAdquisicion>(urlAplicacion, CapacidadGuardarManulAdquisiciones, documento);
                if (resultCargarManuales)
                {
                    var img = Path.Combine(Server.MapPath(rutaManualAdquisicion), File.FileName);
                    File.SaveAs(img);
                }
            }

            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("idEmpresa", usuarioActual.IdEmpresa);

            var result = ServiceClient.ObtenerArrayJsonRestFul<EDManualAdquisicion>(urlAplicacion, CapacidadConsultarAdquisiciones, RestSharp.Method.GET);
            List<ManualGuiaAdBienes> docs = result.Select(doc => new ManualGuiaAdBienes()
            {
                PK_ManualGuiaAdBienes = doc.Id_Manuales_Bienes,
                Nombre_Manual = doc.Nombre_Manual,
                FK_Empresa = doc.Fk_Empresa,
            }).ToList();
            ViewBag.guardadoConExito = resultCargarManuales;
            return View("CrearManualesAdquisicion", docs);
        }

        // GET: AdquisionBienesContra
        public ActionResult CargarManualesAdquisionBienes()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("idEmpresa", usuarioActual.IdEmpresa);

            var result = ServiceClient.ObtenerArrayJsonRestFul<EDManualAdquisicion>(urlAplicacion, CapacidadConsultarAdquisiciones, RestSharp.Method.GET);
            List<ManualGuiaAdBienes> docs = result.Select(doc => new ManualGuiaAdBienes()
            {
                PK_ManualGuiaAdBienes = doc.Id_Manuales_Bienes,
                Nombre_Manual = doc.Nombre_Manual,
                FK_Empresa = doc.Fk_Empresa,
            }).ToList();
            return View(docs);
        }

        public ActionResult EliminarManualAdqBienes(int idManualAdq)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("idManualAdq", idManualAdq);
            string doc = ServiceClient.ObtenerObjetoJsonRestFul<string>(urlAplicacion, CapacidadConsultarManulAdq, RestSharp.Method.GET);
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("idManualAdq", idManualAdq);
            bool resultMetodologias = ServiceClient.PeticionesPostJsonRestFulRespuetaBool(urlAplicacion, CapacidadEliminarManualAdqBienes, RestSharp.Method.DELETE);
            if (resultMetodologias)
            {
                var path = Server.MapPath(rutaManualAdquisicion);
                var file = doc;
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
                        registraLog.RegistrarError(typeof(AdquisicionBienesController), string.Format("Error al eliminar el Manual del servidor   {0}: {1}", DateTime.Now, e.StackTrace), e);
                    }
                }
            }
            return Json(new
            {
                success = resultMetodologias
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DescargarManualAdq(int idManualAdq)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("idManualAdq", idManualAdq);
            string doc = ServiceClient.ObtenerObjetoJsonRestFul<string>(urlAplicacion, CapacidadConsultarManulAdq, RestSharp.Method.GET);
            var path = Server.MapPath(rutaManualAdquisicion);
            var file = doc;
            var fullPath = Path.Combine(path, file);
            byte[] fileBytes = System.IO.File.ReadAllBytes(fullPath);
            MemoryStream ms = new MemoryStream(fileBytes, 0, 0, true, true);
            Response.AddHeader("content-disposition", "attachment;filename=" + doc + "");
            Response.Buffer = true;
            Response.Clear();
            Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length);
            Response.OutputStream.Flush();
            Response.End();
            if (Path.GetExtension(doc).ToLower() == ".pdf")
            {
                return new FileStreamResult(Response.OutputStream, "application/pdf");
            }
            else if (Path.GetExtension(doc).ToLower() == ".png")
            {
                return new FileStreamResult(Response.OutputStream, "application/png");
            }
            else if (Path.GetExtension(doc).ToLower() == ".jpg")
            {
                return new FileStreamResult(Response.OutputStream, "application/jpg");
            }
            else if (Path.GetExtension(doc).ToLower() == ".doc")
            {
                return new FileStreamResult(Response.OutputStream, "application/doc");
            }
            else if (Path.GetExtension(doc).ToLower() == ".docx")
            {
                return new FileStreamResult(Response.OutputStream, "application/docx");
            }
            else if (Path.GetExtension(doc).ToLower() == ".ppt")
            {
                return new FileStreamResult(Response.OutputStream, "application/doc");
            }
            else if (Path.GetExtension(doc).ToLower() == ".pptx")
            {
                return new FileStreamResult(Response.OutputStream, "application/docx");
            }
            else
            {
                return new FileStreamResult(Response.OutputStream, "application/vnd.ms-excel");
            }
        }
    }
}