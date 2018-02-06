using SG_SST.EntidadesDominio.Participacion;
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
using SG_SST.Models.Participacion;
using SG_SST.Controllers.Base;
using SG_SST.Audotoria;
using SG_SST.Models.Metodologia;
using SG_SST.Models.Empresas;
using SG_SST.Dtos.Participacion;
using System.Data;
using System.Data.Entity;
using SG_SST.Logica.Planificacion;
using System.Drawing;
using RestSharp;
using System.Net;
using SG_SST.Dtos.Planificacion;
using SG_SST.Models.Empleado;
using SG_SST.Models.Planificacion;
using SG_SST.EntidadesDominio.Planificacion;
using SG_SST.Services.Planificacion.Services;
using SG_SST.Services.Planificacion.IServices;
using System.Threading;
using SG_SST.Controllers.Planificacion;
using Rotativa.Options;
using SG_SST.EntidadesDominio.Ausentismo;
using SG_SST.EntidadesDominio.Empresas;
using Newtonsoft.Json;
using SG_SST.Logica.Participacion;
namespace SG_SST.Controllers.Participacion
{
    public class ReporteController : BaseController
    {
        private SG_SSTContext db = new SG_SSTContext();
        private ISedeServicios sedeServicio = new SedeServicios();
        string UrlServicioParticipacion = ConfigurationManager.AppSettings["UrlServicioParticipacion"];
        string CapacidadObtenerLugares = ConfigurationManager.AppSettings["CapacidadLugares"];
        string CapacidadGuardarReporte = ConfigurationManager.AppSettings["CapacidadGuardarReporte"];
        string CapacidadObtenerReporteEmpresa = ConfigurationManager.AppSettings["CapacidadObtenerReporteEmpresa"];
        string CapacidadDescargarPDFPorReporte = ConfigurationManager.AppSettings["CapacidadDescargarPDFPorReporte"];
        string CapacidadObtenerActividadesEmpresa = ConfigurationManager.AppSettings["CapacidadObtenerActividadesEmpresa"];
        string CapacidadObtenerImagenesEmpresa = ConfigurationManager.AppSettings["CapacidadObtenerImagenesEmpresa"];
        string CapacidadDescargarExcelPorReporte = ConfigurationManager.AppSettings["CapacidadDescargarExcelPorReporte"];
        string CapacidadDescargarExcelReportesCondicionesInseguras = ConfigurationManager.AppSettings["CapacidadDescargarExcelReportesCondicionesInseguras"];
        string CapacidadVerReportesFiltrados = ConfigurationManager.AppSettings["CapacidadVerReportesFiltrados"];
        string CapacidadVerActividadesFiltrados = ConfigurationManager.AppSettings["CapacidadVerActividadesFiltrados"];
        // Eliminar Imagen
        string CapacidadEliminarImagen = ConfigurationManager.AppSettings["CapacidadEliminarImagen"];
        string CapacidadConsultarImagen = ConfigurationManager.AppSettings["CapacidadConsultarImagen"];
        //
        string rutaImagenesReportesCI = ConfigurationManager.AppSettings["rutaImagenesReportesCI"];
        string rutaImagenesReportesCIA = ConfigurationManager.AppSettings["rutaImagenesReportesCIA"];

        string VisualiazarHistorico = ConfigurationManager.AppSettings["historico"];
        string UrlServicioPlanificacion = ConfigurationManager.AppSettings["UrlServicioPlanificacion"];
        string CapacidadGuardarReporteEditado = ConfigurationManager.AppSettings["CapacidadGuardarReporteEditado"];
        //APP
        string CapacidadObtenerTipoReporte = ConfigurationManager.AppSettings["CapacidadObtenerTipoReporte"];
        string CapacidaGuardarReporteCI = ConfigurationManager.AppSettings["CapacidaGuardarReporteCI"];
        


        List<int> id;
        LNReporte lnReporte = new LNReporte();

        EDReporte rep;
        static EDReporte resReporte;
        private IProcesoServicios procesoServicios = new ProcesoServicios();
        string cargo = "";
        string nombre = "";
       static string nitEmpresa = "";
        int documento;

        public ActionResult Index()
        {
    
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
                return RedirectToAction("Login", "Home");
            }

          
            nitEmpresa = usuarioActual.NitEmpresa;
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("nit", usuarioActual.NitEmpresa);
            var reporte = ServiceClient.ObtenerArrayJsonRestFul<EDReporte>(UrlServicioParticipacion, CapacidadObtenerReporteEmpresa, RestSharp.Method.GET);


           
            return View(reporte.ToList());
        }

        public ActionResult Edit(int id)
        {
          
            EDReporte reporte = generarEDReporte(id);
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
                return RedirectToAction("Login", "Home");
            }
            //List<Proceso> procesos = procesoServicios.ObtenerProcesosPrincipales(usuarioActual.IdEmpresa);

           
            //List<Proceso> subProcesos = procesoServicios.ObtenerSubProcesos(reportes.FK_Proceso);
            //ViewBag.Procesos = new SelectList(procesos, "Pk_Id_Proceso", "Descripcion_Proceso", reportes.Procesos.Fk_Id_Proceso);
      
            var resultTipoReporte = ServiceClient.ObtenerArrayJsonRestFul<EDTipoReporte>(UrlServicioParticipacion, CapacidadObtenerTipoReporte, RestSharp.Method.GET);

       
    

            //Reporte reportes = db.Tbl_Reportes.Find(id);
      
            
            //Reporte reportes = db.Tbl_Reportes.Find(id);


            if (reporte.FK_Proceso != null)
            {
                var fkProceso = (reporte.Procesos == null) ? 0 : reporte.FK_Proceso;
                List<Proceso> procesos = procesoServicios.ObtenerProcesosPrincipales(usuarioActual.IdEmpresa);
                Proceso proceso = procesoServicios.ObtenerProceso((int)fkProceso);
                ViewBag.Procesos = new SelectList(procesos, "Pk_Id_Proceso", "Descripcion_Proceso", reporte.FK_Proceso);

            }
            else
            {


                List<Proceso> procesos = procesoServicios.ObtenerProcesosPrincipales(usuarioActual.IdEmpresa);
                ViewBag.Procesos = new SelectList(procesos, "Pk_Id_Proceso", "Descripcion_Proceso");


            }
      
            //if(reporte.FK_Proceso!=null)
            //{
            //    var fkProceso = (reporte.FK_Proceso == null) ? 0:reporte.FK_Proceso;
            //List<Proceso> procesos = procesoServicios.ObtenerProcesosPrincipales(usuarioActual.IdEmpresa);
            //Proceso proceso = procesoServicios.ObtenerProceso((int)fkProceso);
            //List<Proceso> subProcesos = procesoServicios.ObtenerSubProcesos(proceso.Procesos.Pk_Id_Proceso);
            //ViewBag.Procesos = new SelectList(procesos, "Pk_Id_Proceso", "Descripcion_Proceso", proceso.Fk_Id_Proceso);
            //ViewBag.FK_Proceso = new SelectList(subProcesos, "Pk_Id_Proceso", "Descripcion_Proceso", reporte.FK_Proceso);
            //}
            //else
            //{
            //    //ServiceClient.EliminarParametros();
            //    //ServiceClient.AdicionarParametro("NIT", usuarioActual.NitEmpresa);
            //    //var resultProceso = ServiceClient.ObtenerArrayJsonRestFul<EDProceso>(urlServicioEmpresas, CapacidadObtenerprocesosEmpresa, RestSharp.Method.GET);

            //    //ViewBag.Procesos = resultProceso.Select(p => new SelectListItem()
            //    //{
            //    //    Value = p.Id_Proceso.ToString(),
            //    //    Text = p.Descripcion
            //    //}).ToList();


            //    List<Proceso> procesos = procesoServicios.ObtenerProcesosPrincipales(usuarioActual.IdEmpresa);
            //    ViewBag.Procesos = new SelectList(procesos, "Pk_Id_Proceso", "Descripcion_Proceso");


            //}

            ViewBag.idReporte = reporte.IdReportes;
            ViewBag.FKSede = new SelectList(sedeServicio.SedesPorEmpresa(usuarioActual.IdEmpresa), "Pk_Id_Sede", "Nombre_Sede", reporte.FKSede);
         
            if(reporte.medioAcceso)
            {
                ViewBag.MedioAcceso = '1';
            }
            else
            {
                ViewBag.MedioAcceso = '0';

            }
            
   
            ViewBag.FKTipoReporte = new SelectList(resultTipoReporte.ToList(), "IdTipoReporte", "DescripcionTipoReporte", reporte.FKTipoReporte);
            ViewBag.Cedula = reporte.CedulaQuienReporta;
            ViewBag.Consecutivo = reporte.ConsecutivoReporte;
            ViewBag.fechaSistena = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            ViewBag.FechaOcurrencia = reporte.FechaOcurrencia.ToString("dd/MM/yyyy").Replace('-', '/');
            ViewBag.Descripcion = reporte.DescripcionReporte;
            ViewBag.Causa = reporte.CausaReporte;
            ViewBag.Sugerencia = reporte.SugerenciasReporte;
            ObtenerSiarp(Convert.ToString(reporte.CedulaQuienReporta));
            ViewBag.Cargo = cargo.ToLower();
            ViewBag.Nombre = nombre.ToLower();
            ViewBag.ruta = rutaImagenesReportesCI + usuarioActual.NitEmpresa + "/";
            return View(reporte);
        }
        public ActionResult CrearReporte()
        {
            int consecutivo=0;
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
                return RedirectToAction("Login", "Home");
            }

            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("nit", usuarioActual.NitEmpresa);
            var reporte = ServiceClient.ObtenerArrayJsonRestFul<EDReporte>(UrlServicioParticipacion, CapacidadObtenerReporteEmpresa, RestSharp.Method.GET);
            if (reporte.Count() == 0)
            {
                consecutivo = 1;
            }
            else
            {
                consecutivo = reporte.Count() + 1;
            }

            ViewBag.Consecutivo = consecutivo;
          
            ViewBag.RazonSocialEmpresa = usuarioActual.RazonSocialEmpresa;
            ViewBag.nitEmpresa = usuarioActual.NitEmpresa;


            List<Proceso> procesos = procesoServicios.ObtenerProcesosPrincipales(usuarioActual.IdEmpresa);
            ViewBag.Procesos = new SelectList(procesos, "Pk_Id_Proceso", "Descripcion_Proceso");


            //ServiceClient.EliminarParametros();
            //ServiceClient.AdicionarParametro("NIT", usuarioActual.NitEmpresa);
            //var resultProceso = ServiceClient.ObtenerArrayJsonRestFul<EDProceso>(urlServicioEmpresas, CapacidadObtenerprocesosEmpresa, RestSharp.Method.GET);
            //if (resultProceso != null && resultProceso.Count() > 0)
            //{
            //    ViewBag.Procesos = resultProceso.Select(p => new SelectListItem()
            //    {
            //        Value = p.Id_Proceso.ToString(),
            //        Text = p.Descripcion
            //    }).ToList();
            //}
            ViewBag.fechaSistena = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            ViewBag.FKSede = new SelectList(sedeServicio.SedesPorEmpresa(usuarioActual.IdEmpresa), "Pk_Id_Sede", "Nombre_Sede");
            var resultTipoReporte = ServiceClient.ObtenerArrayJsonRestFul<EDTipoReporte>(UrlServicioParticipacion, CapacidadObtenerTipoReporte, RestSharp.Method.GET);

            ViewBag.FKTipoReporte = resultTipoReporte.Select(c => new SelectListItem()
            {
                Value = c.IdTipoReporte.ToString(),
                Text = c.DescripcionTipoReporte
            }).ToList();

            return View();
        }

        public ActionResult DescargarPDFPorReporte(int id)
        {

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
                return RedirectToAction("Login", "Home");
            }
           
            EDReporte reporte = generarEDReporte(id);
            //string SwitchNombreEmpresa = usuarioActual.RazonSocialEmpresa;
            //string SwitchNitEmpresa = usuarioActual.NitEmpresa;
            //string SwitchNombreDocumento = "Reporte de Condiciones y Actos Inseguros";
            //var uriFooter = new Uri(Url.Action("Footer", "Reporte", null, Request.Url.Scheme));
            //var clean1 = uriFooter.GetComponents(UriComponents.AbsoluteUri & ~UriComponents.Port, UriFormat.UriEscaped);
            //var uriHeader = new Uri(Url.Action("Header", "Reporte", new { NombreEmpresa = SwitchNombreEmpresa, NitEmpresa = SwitchNitEmpresa, NombreInforme = SwitchNombreDocumento }, Request.Url.Scheme));
            //var clean2 = uriHeader.GetComponents(UriComponents.AbsoluteUri & ~UriComponents.Port, UriFormat.UriEscaped);

            ///Prueba Pdf en Blanco
            string EncodedRazonSocial = System.Net.WebUtility.UrlEncode(usuarioActual.RazonSocialEmpresa);
            string EncodedNit = System.Net.WebUtility.UrlEncode(usuarioActual.NitEmpresa);
            string EncodedNombreInforme = System.Net.WebUtility.UrlEncode("Reporte de Condiciones y Actos Inseguros");

            var footurl = "https://alissta.gov.co/Acciones/Footer";
            var headerurl = "https://alissta.gov.co/Acciones/Header?NombreEmpresa=" + EncodedRazonSocial + "&NitEmpresa=" + EncodedNit + "&NombreInforme=" + EncodedNombreInforme;

            string cusomtSwitches = string.Format("--footer-line --print-media-type --allow {0} --footer-html {0} --header-line --print-media-type --allow {1} --header-html {1} --header-spacing 5", footurl, headerurl);
            ///FIN Prueba Pdf en Blanco
            // string cusomtSwitches = string.Format("--footer-line --print-media-type --allow {0} --footer-html {0} --header-line --print-media-type --allow {1} --header-html {1} --header-spacing 5", uriFooter, uriHeader);
            documento = rep.CedulaQuienReporta;
            ObtenerSiarp(Convert.ToString(documento));
            ViewBag.Cargo = cargo.ToLower();
            ViewBag.Nombre = nombre.ToLower();
            ViewBag.fechaSistena = reporte.fechaSistena;
            ViewBag.rutaAbsoluta = rutaImagenesReportesCIA + usuarioActual.NitEmpresa+"/";

           
            
            //List<Proceso> procesos = procesoServicios.ObtenerProcesosPrincipales(usuarioActual.IdEmpresa);

           
            //List<Proceso> subProcesos = procesoServicios.ObtenerSubProcesos(reportes.FK_Proceso);
            //ViewBag.Procesos = new SelectList(procesos, "Pk_Id_Proceso", "Descripcion_Proceso", reportes.Procesos.Fk_Id_Proceso);
     
            //ViewBag.FK_Proceso = new SelectList(db.Tbl_Procesos, "Pk_Id_Proceso", "Descripcion_Proceso", reportes.FK_Proceso);


            Reporte reportes = db.Tbl_Reportes.Find(id);
            if (reporte.FK_Proceso != null)
            {

                ViewBag.FK_Proceso = reporte.nombreProceso;

            }
            else
            {
                ViewBag.FK_Proceso = "NA";
            }


            ViewBag.FechaOcurrencia = reporte.FechaOcurrencia.ToString("dd/MM/yyyy").Replace('-', '/');
            if (reporte.medioAcceso == false)
            {
                ViewBag.Origen = "Alissta WEB";
            }
            else
            {
                ViewBag.Origen = "Alissta APP";
            }
            ViewBag.ruta = rutaImagenesReportesCI + usuarioActual.NitEmpresa + "/";
            return new Rotativa.PartialViewAsPdf("ReporteCondicionesInsegurasPDF", reporte)
            {

                FileName = "ReporteCondicionesActosInseguros" + DateTime.Now.ToString().Replace("/", "").Replace(":", "").Replace(" ", "") + ".pdf",
                PageOrientation = Orientation.Landscape,
                CustomSwitches = cusomtSwitches
            };
        }


      


        public FileResult DescargarReporteExcelCondicionesInsegurasPorReporte(int id)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
            }
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("id", id);
            var result = ServiceClient.ObtenerObjetoJsonRestFul<byte[]>(UrlServicioParticipacion, CapacidadDescargarExcelPorReporte, RestSharp.Method.GET);
            return File(result, "application/vnd.ms-excel", "ReporteDeCondicionInsegura"  + DateTime.Now.ToString().Replace("/", "").Replace(":", "").Replace(" ", "") +".xlsx");
        }



        public FileResult DescargarReporteExcelCondiciones(EDReporte reporte)
        {

            reporte = resReporte;

            List<EDReporte> listaReportes = new List<EDReporte>();
            List<EDActividadesActosInseguros> listaActividades = new List<EDActividadesActosInseguros>();
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
            }

            ServiceClient.EliminarParametros();
            var resultReporte = ServiceClient.RealizarPeticionesArrayPostJsonRest<EDReporte>(UrlServicioParticipacion, CapacidadVerReportesFiltrados, reporte);
            listaReportes = resultReporte.ToList();
            ServiceClient.EliminarParametros();
            var resultActividades = ServiceClient.RealizarPeticionesArrayPostJsonRest<EDActividadesActosInseguros>(UrlServicioParticipacion, CapacidadVerActividadesFiltrados, reporte);
            listaActividades = resultActividades.ToList();
            var result = lnReporte.ObtenerReporteExcel(listaReportes, listaActividades);
            //ServiceClient.AdicionarParametro("rep", resultReporte.ToList());
            //var result = ServiceClient.ObtenerObjetoJsonRestFul<byte[]>(UrlServicioParticipacion, CapacidadDescargarExcelReportesCondicionesInseguras, RestSharp.Method.POST);

            return File(result, "application/vnd.ms-excel", "ReporteDeCondicionesInseguras" + DateTime.Now.ToString().Replace("/", "").Replace(":", "").Replace(" ", "") + ".xlsx");
        }

        [HttpPost]
        public ActionResult GuardarReporteEditado(EDReporte varReporte, List<HttpPostedFileBase> files, List<EDActividadesActosInseguros> actividades)
        {
           
            List<string> listaImagenes = new List<string>();
            var agregarActividades = false;
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
                return RedirectToAction("Login", "Home");
            }
            nitEmpresa = usuarioActual.NitEmpresa;
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("nit", usuarioActual.NitEmpresa);

            var reporte = ServiceClient.ObtenerArrayJsonRestFul<EDReporte>(UrlServicioParticipacion, CapacidadObtenerReporteEmpresa, RestSharp.Method.GET);

            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("id", varReporte.IdReportes);
            // var result = ServiceClient.ObtenerObjetoJsonRestFul<EDReporte>(UrlServicioParticipacion, CapacidadDescargarPDFPorReporte, RestSharp.Method.GET);
            var activi = ServiceClient.ObtenerArrayJsonRestFul<EDActividadesActosInseguros>(UrlServicioParticipacion, CapacidadObtenerActividadesEmpresa, RestSharp.Method.GET);
            List<EDActividadesActosInseguros> actividadesPlanAccion = activi.ToList();



            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("id", varReporte.IdReportes);
            // var result = ServiceClient.ObtenerObjetoJsonRestFul<EDReporte>(UrlServicioParticipacion, CapacidadDescargarPDFPorReporte, RestSharp.Method.GET);
            var imagenes = ServiceClient.ObtenerArrayJsonRestFul<EDImagenesReportes>(UrlServicioParticipacion, CapacidadObtenerImagenesEmpresa, RestSharp.Method.GET);
            List<EDImagenesReportes> imagenesPlanAccion = imagenes.ToList();

            varReporte.imagenesReporte = imagenesPlanAccion;
            varReporte.actividades = actividadesPlanAccion;
            if (varReporte.actividades.Count() == 0)
            {

                varReporte.actividades = actividades;
            }
            var path = "";
            var ruta = rutaImagenesReportesCI + usuarioActual.NitEmpresa;
            var rutaImagen = "";
            var rutaGuardar = "";
            List<string> imagenesGuardar = new List<string>();
            for (int i = 0; i < Request.Files.Count; i++)
            {

                HttpPostedFileBase ima = Request.Files[i] as HttpPostedFileBase;

                if (ima.ContentLength == 0)
                {
                }
                if (ima.ContentLength > 0)
                {
                    if (Path.GetExtension(ima.FileName).ToLower() == ".jpg" || Path.GetExtension(ima.FileName).ToLower() == ".png"
                       || Path.GetExtension(ima.FileName).ToLower() == ".bmp" || Path.GetExtension(ima.FileName).ToLower() == ".gif"
                        || Path.GetExtension(ima.FileName).ToLower() == ".tiff" || Path.GetExtension(ima.FileName).ToLower() == ".jpeg"
                        )
                    {
                        if (!Directory.Exists(ruta))
                        {
                            System.IO.Directory.CreateDirectory(Server.MapPath(ruta));
                        }
                        rutaImagen = Guid.NewGuid()+ima.FileName;
                        path = Path.Combine(Server.MapPath(ruta), rutaImagen);
                        rutaGuardar = rutaImagen;
                   
                        listaImagenes.Add(rutaGuardar);
                        imagenesGuardar.Add(path);
                        // ima.SaveAs(path);
                    }
                    else
                    {
                        ViewBag.MensajeError = "Error al almacenar, Formato de carga incorrecto";

                        return View("Index", reporte.ToList());
                    }
                }

            }
            varReporte.imagenes = listaImagenes;

            ServiceClient.EliminarParametros();
            var result = ServiceClient.RealizarPeticionesPostJsonRestFul<EDReporte>(UrlServicioParticipacion, CapacidadGuardarReporteEditado, varReporte);

            if (result != null)
            {
                if (imagenesGuardar.Count() > 0)
                {
                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        //HttpPostedFileBase ima = Request.Files[i] as HttpPostedFileBase;
                        Image imagenRedimensionada = RedimensionarImagen(Request.Files[i].InputStream);


                        imagenRedimensionada.Save(imagenesGuardar[i]);
                    }
                }
                ViewBag.MensajeExitoso = "El reporte de incidentes fué actualizado satisfactoramiente";

                //RedirectToAction("CrearReporte", "Reporte");
            }
            else
            {

                ViewBag.MensajeError = "No se pudo  actualizar  el reporte de incidentes";

                //RedirectToAction("CrearReporte", "Reporte");
            }
         
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("nit", usuarioActual.NitEmpresa);

            var reporte2 = ServiceClient.ObtenerArrayJsonRestFul<EDReporte>(UrlServicioParticipacion, CapacidadObtenerReporteEmpresa, RestSharp.Method.GET);
         
            return View("Index", reporte2.ToList());
        }


        [HttpPost]
        public ActionResult GuardarReporteCondicionesInseguras(EDReporte varReporte, List<HttpPostedFileBase> files, List<EDActividadesActosInseguros> actividades)
        {
           
            List<string> imagenes = new List<string>();
                
            varReporte.actividades = actividades;
            EDImagenesReportes img = new EDImagenesReportes();


            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
                return RedirectToAction("Login", "Home");
            }

            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("nit", usuarioActual.NitEmpresa);
            var reporte = ServiceClient.ObtenerArrayJsonRestFul<EDReporte>(UrlServicioParticipacion, CapacidadObtenerReporteEmpresa, RestSharp.Method.GET);


            var path = "";
            var ruta = rutaImagenesReportesCI + usuarioActual.NitEmpresa;
            var rutaImagen = "";
            var rutaGuardar = "";
            List<string> imagenesGuardar = new List<string>();
            for (int i = 0; i < Request.Files.Count; i++)
            {

                HttpPostedFileBase ima = Request.Files[i] as HttpPostedFileBase;

             
                if (ima.ContentLength == 0)
                {
                }

                if (Path.GetExtension(ima.FileName).ToLower() == ".jpg" || Path.GetExtension(ima.FileName).ToLower() == ".png"
               || Path.GetExtension(ima.FileName).ToLower() == ".bmp" || Path.GetExtension(ima.FileName).ToLower() == ".gif"
                || Path.GetExtension(ima.FileName).ToLower() == ".tiff" || Path.GetExtension(ima.FileName).ToLower() == ".jpeg"
                )
                {
                    if (!Directory.Exists(ruta))
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath(ruta));
                    }


                    rutaImagen =  Guid.NewGuid()+ ima.FileName;
                    path = Path.Combine(Server.MapPath(ruta), rutaImagen);
                    rutaGuardar = rutaImagen;
                    imagenes.Add(rutaGuardar);
                    imagenesGuardar.Add(path);
                    // ima.SaveAs(path);
                }
                else
                {
                     ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("nit", usuarioActual.NitEmpresa);
           
                    ViewBag.MensajeError = "Error al almacenar, Formato de carga incorrecto";
                  
                    return View("Index",reporte.ToList());
                }
            }



            varReporte.imagenes = imagenes;

            ServiceClient.EliminarParametros();
            var result = ServiceClient.RealizarPeticionesPostJsonRestFul<EDReporte>(UrlServicioParticipacion, CapacidadGuardarReporte, varReporte);

            if (result != null)
            {

                for (int i = 0; i < Request.Files.Count; i++)
                {
                    //HttpPostedFileBase ima = Request.Files[i] as HttpPostedFileBase;


                    Image imagenRedimensionada = RedimensionarImagen(Request.Files[i].InputStream);


                    imagenRedimensionada.Save(imagenesGuardar[i]);
                }

                List<Proceso> procesos = procesoServicios.ObtenerProcesosPrincipales(usuarioActual.IdEmpresa);
                ViewBag.RazonSocialEmpresa = usuarioActual.RazonSocialEmpresa;
                ViewBag.nitEmpresa = usuarioActual.NitEmpresa;
                ViewBag.Procesos = new SelectList(procesos, "Pk_Id_Proceso");
                ViewBag.fechaSistena = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
                ViewBag.FKSede = new SelectList(sedeServicio.SedesPorEmpresa(usuarioActual.IdEmpresa), "Pk_Id_Sede");
                ViewBag.FKTipoReporte = new SelectList(db.Tbl_Tipo_Reporte, "Pk_Id_Tipo_Reporte");

                ViewBag.MensajeGuardar = "El reporte de incidentes fu\u00e9 almacenado satisfactoramiente";

                //RedirectToAction("CrearReporte", "Reporte");
            }
            else
            {

                ViewBag.MensajeError = "No se pudo  almacenar el reporte de incidenetes";

                return View("Index", reporte.ToList());
            }


            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("nit", usuarioActual.NitEmpresa);
            var reporte2 = ServiceClient.ObtenerArrayJsonRestFul<EDReporte>(UrlServicioParticipacion, CapacidadObtenerReporteEmpresa, RestSharp.Method.GET);

         
            return View("Index", reporte2.ToList());
        }



        public JsonResult ObtenerSiarp(string Documento)
        {
            string nitEmpresaU = "";
            cargo = "";
            nombre = "";
            var datos = string.Empty;

            try
            {
                var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);

                if (!string.IsNullOrEmpty(Documento))
                {

                    var sigla = usuarioActual.SiglaTipoDocumentoEmpresa;
                    var cliente = new RestSharp.RestClient(ConfigurationManager.AppSettings["Url"]);
                    var request = new RestRequest(consultaAfiliadoEmpresaActivo, RestSharp.Method.GET);
                    request.RequestFormat = DataFormat.Xml;
                    request.Parameters.Clear();
                    request.AddParameter("tpEm", usuarioActual.SiglaTipoDocumentoEmpresa);
                    request.AddParameter("docEm", usuarioActual.NitEmpresa);
                    request.AddParameter("tpAfiliado", "cc");
                    request.AddParameter("docAfiliado", Documento);
                    request.AddHeader("Content-Type", "application/json");
                    request.AddHeader("Accept", "application/json");

                    ServicePointManager.ServerCertificateValidationCallback = delegate
                    { return true; };
                    IRestResponse<List<PerfilSocioDemograficoDTO>> response = cliente.Execute<List<PerfilSocioDemograficoDTO>>(request);
                    var result = response.Content;

                    var respuesta = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PerfilSocioDemograficoDTO>>(result);

                    nitEmpresa = usuarioActual.NitEmpresa;
                    nitEmpresaU = "";
                    nitEmpresaU = respuesta[0].documentoEmp;
                    if (nitEmpresaU.Equals(nitEmpresa))
                    {

                        cargo = respuesta[0].ocupacion;
                        nombre = respuesta[0].nombre1 + " " + respuesta[0].nombre2 + " " + respuesta[0].apellido1 + " " + respuesta[0].apellido2;


                      
                        return Json(new { Data = respuesta, Mensaje = "OK" }, JsonRequestBehavior.AllowGet);
                    }


                    else
                    {
                        return Json(new { Data = "El Usuario no pertenece a la empresa", Mensaje = "ERROR" });

                    }

                }

                if (Documento.Equals(""))
                {

                    return Json(new { Data = "Por favor ingrese un documento", Mensaje = "VACIO" }, JsonRequestBehavior.AllowGet);
                }
            }

            catch (Exception e)
            {
                return Json(new { Data = "El usuario no existe en el sistema SIARP", Mensaje = "CONEXION" });
            }


            // return Json(new { Data = respuesta, Mensaje = "OK" }, JsonRequestBehavior.AllowGet);

            return Json(new { Data = datos, Mensaje = "ERROR" });

        }




    

        public EDReporte generarEDReporte(int id)
        {
            rep = new EDReporte();
            EDActividadesActosInseguros actividades = new EDActividadesActosInseguros();


            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("id", id);
            // var result = ServiceClient.ObtenerObjetoJsonRestFul<EDReporte>(UrlServicioParticipacion, CapacidadDescargarPDFPorReporte, RestSharp.Method.GET);
            var reporte = ServiceClient.ObtenerArrayJsonRestFul<EDReporte>(UrlServicioParticipacion, CapacidadDescargarPDFPorReporte, RestSharp.Method.GET);

            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("id", id);
            // var result = ServiceClient.ObtenerObjetoJsonRestFul<EDReporte>(UrlServicioParticipacion, CapacidadDescargarPDFPorReporte, RestSharp.Method.GET);
            var activi = ServiceClient.ObtenerArrayJsonRestFul<EDActividadesActosInseguros>(UrlServicioParticipacion, CapacidadObtenerActividadesEmpresa, RestSharp.Method.GET);
            List<EDActividadesActosInseguros> actividadesPlanAccion = activi.ToList();

            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("id", id);
            // var result = ServiceClient.ObtenerObjetoJsonRestFul<EDReporte>(UrlServicioParticipacion, CapacidadDescargarPDFPorReporte, RestSharp.Method.GET);
            var imagenes = ServiceClient.ObtenerArrayJsonRestFul<EDImagenesReportes>(UrlServicioParticipacion, CapacidadObtenerImagenesEmpresa, RestSharp.Method.GET);


            List<EDImagenesReportes> imagenesPlanAccion = imagenes.ToList();

            foreach (var repor in reporte)
            {

                rep.nombreProceso = repor.nombreProceso;
                rep.FKSede = repor.FKSede;
                rep.FKTipoReporte = repor.FKTipoReporte;
                rep.FK_Proceso = repor.FK_Proceso;
                rep.IdReportes = repor.IdReportes;
                rep.nitEmpresa = repor.nitEmpresa;
                rep.tipo = repor.tipo;
                rep.FechaOcurrencia = repor.FechaOcurrencia;
                rep.AreaLugar = repor.AreaLugar;
                rep.CausaReporte = repor.CausaReporte;
                rep.SugerenciasReporte = repor.SugerenciasReporte;
                rep.CedulaQuienReporta = repor.CedulaQuienReporta;
                rep.NombreQuienReporta = repor.NombreQuienReporta;
                rep.RazonSocialEmpresa = repor.RazonSocialEmpresa;
                rep.fechaSistena = repor.fechaSistena;
                rep.sede = repor.sede;
                rep.nombreProceso = repor.nombreProceso;
                rep.DescripcionReporte = repor.DescripcionReporte;
                rep.actividades = actividadesPlanAccion;
                rep.imagenesReporte = imagenesPlanAccion;
                rep.ConsecutivoReporte = repor.ConsecutivoReporte;
                rep.medioAcceso = repor.medioAcceso;
            }
            return rep;
        }


        public ActionResult VisualizarReporte()
        {
            EDReporte reporte = new EDReporte();
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
                return RedirectToAction("Login", "Home");
            }

            ViewBag.sedes = new SelectList(sedeServicio.SedesPorEmpresa(usuarioActual.IdEmpresa), "Pk_Id_Sede", "Nombre_Sede");
            ViewBag.FKTipoReporte = new SelectList(db.Tbl_Tipo_Reporte, "Pk_Id_Tipo_Reporte", "Descripcion_Tipo_Reporte");

            reporte.CedulaQuienReporta = 1;


            var result = ServiceClient.RealizarPeticionesArrayPostJsonRest<EDReporte>(UrlServicioParticipacion, CapacidadVerReportesFiltrados, reporte);

            return View(result.ToList());
        }



        public ActionResult VerReporte(EDReporte reporte)
        {

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
                return RedirectToAction("Login", "Home");
            }
            //var fechaInicio = reporte.fechaInicio;
            reporte.nitEmpresa = usuarioActual.NitEmpresa;
            ViewBag.sedes = new SelectList(sedeServicio.SedesPorEmpresa(usuarioActual.IdEmpresa), "Pk_Id_Sede", "Nombre_Sede");
            ViewBag.FKTipoReporte = new SelectList(db.Tbl_Tipo_Reporte, "Pk_Id_Tipo_Reporte", "Descripcion_Tipo_Reporte");


            ServiceClient.EliminarParametros();
            var result = ServiceClient.RealizarPeticionesArrayPostJsonRest<EDReporte>(UrlServicioParticipacion, CapacidadVerReportesFiltrados, reporte);

            if (!reporte.CedulaQuienReporta.Equals("") || !reporte.FKTipoReporte.Equals("") || reporte.sedes != null || !(reporte.fechaFin.Year == 1))
            {
                resReporte = reporte;

            }
            if(result.Count()==0)
            {

                ViewBag.MensajeError = "No se encontraron registros, que cumplan con la búsqueda";

            }

            return View("VisualizarReporte", result.ToList());


        }


        public ActionResult EliminarImagenes(int idImagen)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
                return View();
            }
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("idImagen", idImagen);

            EDImagenesReportes imagen = ServiceClient.ObtenerObjetoJsonRestFul<EDImagenesReportes>(UrlServicioParticipacion, CapacidadConsultarImagen, RestSharp.Method.GET);
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("idImagen", idImagen);
            bool resultImagenes = ServiceClient.PeticionesPostJsonRestFulRespuetaBool(UrlServicioParticipacion, CapacidadEliminarImagen, RestSharp.Method.DELETE);
            string ruta = rutaImagenesReportesCI + usuarioActual.NitEmpresa;

            if (resultImagenes)
            {
                var path = Server.MapPath(ruta);
                var file = imagen.rutaArchivo;
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
                        registraLog.RegistrarError(typeof(ActividadesActosInseguros), string.Format("Error al eliminar la imagen del servidor   {0}: {1}", DateTime.Now, e.StackTrace), e);
                    }
                }
            }
            return Json(new
            {
                success = resultImagenes
            }, JsonRequestBehavior.AllowGet);
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


        public FileStreamResult MostrarImagen(string nombre)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
      
            var ruta="";
            if(usuarioActual!=null)
            {

                ruta= rutaImagenesReportesCI + usuarioActual.NitEmpresa + "/";
            }
            else
            {
                ruta = rutaImagenesReportesCI + nitEmpresa + "/";
            }

            var fullPath = Path.Combine(Server.MapPath(ruta), nombre);
            //var fullPath = Path.Combine(ruta, nombre);
            if (System.IO.File.Exists(fullPath))
            {
                FileStream fs = new FileStream(fullPath, FileMode.Open, FileAccess.Read);
                return File(fs, "image/png");
            }
            else
            {
        
                return null;
            }


        }

        private static Image RedimensionarImagen(Stream stream)
        {
            // Se crea un objeto Image, que contiene las propiedades de la imagen
            Image img = Image.FromStream(stream);
            int newH = 300;
            int newW = 300;
            // Si las dimensiones cambiaron, se modifica la imagen
            Bitmap newImg = new Bitmap(img, newW, newH);
            Graphics g = Graphics.FromImage(newImg);
            g.InterpolationMode =
                System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
            g.DrawImage(img, 0, 0, newImg.Width, newImg.Height);
            return newImg;
          }



        }     


}

