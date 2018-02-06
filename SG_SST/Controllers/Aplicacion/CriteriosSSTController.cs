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
using SG_SST.Logica.Aplicacion;
using SG_SST.Models;
using SG_SST.Controllers.Base;
using SG_SST.Models.Aplicacion;
using SG_SST.Audotoria;
using Rotativa.Options;

namespace SG_SST.Controllers.Aplicacion
{
    public class CriteriosSSTController : BaseController
    {
        LNCriteriosSST LNCriteriosSST = new LNCriteriosSST();
        private SG_SSTContext db = new SG_SSTContext();
        string urlAplicacion = ConfigurationManager.AppSettings["UrlServicioAplicacion"];

        string CapacidadObtenerCriteriosSST = ConfigurationManager.AppSettings["CapacidadObtenerCriteriosSST"];

        string CapacidadObtenerProductosPorCriterios = ConfigurationManager.AppSettings["CapacidadObtenerProductosPorCriterios"];
        string CapacidadObtenerProveedorContratista = ConfigurationManager.AppSettings["CapacidadObtenerProveedorContratista"];
        string CapacidadObtenerProveedorContratistaEditar = ConfigurationManager.AppSettings["CapacidadObtenerProveedorContratistaEditar"];
        string CapacidadObtenerProducto = ConfigurationManager.AppSettings["CapacidadObtenerProducto"];
        string CapacidadObtenerListaProveedores = ConfigurationManager.AppSettings["CapacidadObtenerListaProveedores"];
        string CapacidadGuardarProductoCriterio = ConfigurationManager.AppSettings["CapacidadGuardarProductoCriterio"];
        string CapacidadObtenerProveedorContratistaGraficar = ConfigurationManager.AppSettings["CapacidadObtenerProveedorContratistaGraficar"];
        string CapacidadEditarProductoCriterio = ConfigurationManager.AppSettings["CapacidadEditarProductoCriterio"];
        string CapacidadObtenerProveedor = ConfigurationManager.AppSettings["CapacidadObtenerProveedor"];
        string CapacidadEliminarProductoCriterio = ConfigurationManager.AppSettings["CapacidadEliminarProductoCriterio"];
        string CapacidadEliminarCalificacionProveedor = ConfigurationManager.AppSettings["CapacidadEliminarCalificacionProveedor"];
        string CapacidadEditarProveedorContratista = ConfigurationManager.AppSettings["CapacidadEditarProveedorContratista"];
        string CapacidadGuardarSeleccionYEvaluacion = ConfigurationManager.AppSettings["CapacidadGuardarSeleccionYEvaluacion"];
        string CapacidadEditarSeleccionYEvaluacion = ConfigurationManager.AppSettings["CapacidadEditarSeleccionYEvaluacion"];
        string CapacidadConsultarAnexosProveedor = ConfigurationManager.AppSettings["CapacidadConsultarAnexosProveedor"];


        //string CapacidadConsultarAdquisiciones = ConfigurationManager.AppSettings["CapacidadConsultarManualAdquisicion"];




        string rutaManualAdquisicion = ConfigurationManager.AppSettings["rutaManulaAdq"];
        string rutaAnexosProv = ConfigurationManager.AppSettings["rutaAnexosProv"];

        // GET: Criterios
        public ActionResult Index()
        {
            return View();
        }

        // GET: CriteriosSST/Create
        public ActionResult Create()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "Debe estar autenticado para Continuar.";
                return RedirectToAction("Login", "Home");
            }
            var ListaCriterios = new List<EDCriteriosSST>();
            ListaCriterios = LNCriteriosSST.ObtenerCriteriosSST();
            var index_criterio = new List<EDProductoPorCriterioSSt>();
            ViewBag.Pk_Id_Criterios = new SelectList(ListaCriterios, "IdCriterioSST", "NombreCriterioSST", index_criterio);            
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("idEmpresa", usuarioActual.IdEmpresa);
            var ProductosPorCriterio = ServiceClient.ObtenerArrayJsonRestFul<EDServicioProducto>(urlAplicacion, CapacidadObtenerProductosPorCriterios, RestSharp.Method.GET);            
            return View(ProductosPorCriterio);
        }

        [HttpPost]
        public ActionResult Create(EDProductoCriterio productoCriterio)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "Debe estar autenticado para Continuar.";
                return RedirectToAction("Login", "Home");
            }
            bool resultGuardarProductoCriterio = false;
            ServiceClient.EliminarParametros();
            productoCriterio.Fk_Empresa = usuarioActual.IdEmpresa;
            resultGuardarProductoCriterio = ServiceClient.PeticionesPostJsonRestFulRespuetaBool<EDProductoCriterio>(urlAplicacion, CapacidadGuardarProductoCriterio, productoCriterio);
            ViewBag.guardadoConExito = resultGuardarProductoCriterio;
            var ListaCriterios = new List<EDCriteriosSST>();
            ListaCriterios = LNCriteriosSST.ObtenerCriteriosSST();
            //var index_criterio = new List<EDProductoPorCriterioSSt>();
            int index_criterio = 0;
            ViewBag.Pk_Id_Criterios = new SelectList(ListaCriterios, "IdCriterioSST", "NombreCriterioSST", index_criterio);
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("idEmpresa", usuarioActual.IdEmpresa);
            var ProductosPorCriterio = ServiceClient.ObtenerArrayJsonRestFul<EDServicioProducto>(urlAplicacion, CapacidadObtenerProductosPorCriterios, RestSharp.Method.GET);
            return View(ProductosPorCriterio);
        }

        // GET: CriteriosSST/Calificar
        public ActionResult Calificar()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "Debe estar autenticado para Continuar.";
                return RedirectToAction("Login", "Home");
            }
            //var ListaCriterios = new List<EDCriteriosSST>();
            //ListaCriterios = LNCriteriosSST.ObtenerCriteriosSST();
            //var index_criterio = new List<EDProductoPorCriterioSSt>();
            //ViewBag.Pk_Id_Criterios = new SelectList(ListaCriterios, "IdCriterioSST", "NombreCriterioSST", index_criterio);
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("idEmpresa", usuarioActual.IdEmpresa);
            var ProductosPorCriterio = ServiceClient.ObtenerArrayJsonRestFul<EDServicioProducto>(urlAplicacion, CapacidadObtenerProductosPorCriterios, RestSharp.Method.GET);
            ViewBag.Pk_Id_Productos = new SelectList(ProductosPorCriterio, "idServicioProducto", "DescripcionProducto");
            return View();
        }

        [HttpPost]
        public ActionResult Calificar(EDSeleccionYEvaluacion seleccionEvaluacion, List<HttpPostedFileBase> files)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "Debe estar autenticado para Continuar.";
                return RedirectToAction("Login", "Home");
            }
            var path = "";
            var ruta = rutaAnexosProv + usuarioActual.NitEmpresa;
            var rutaArchivo = "";
            List<string> archivos = new List<string>();
            List<string> archivosGuardar = new List<string>();
            for (int i = 0; i < Request.Files.Count; i++)
            {

                HttpPostedFileBase arch = Request.Files[i] as HttpPostedFileBase;

                if (arch != null && Path.GetExtension(arch.FileName).ToLower() == ".pdf" || Path.GetExtension(arch.FileName).ToLower() == ".xlsx"
                || Path.GetExtension(arch.FileName).ToLower() == ".doc" || Path.GetExtension(arch.FileName).ToLower() == ".docx"
                || Path.GetExtension(arch.FileName).ToLower() == ".ppt" || Path.GetExtension(arch.FileName).ToLower() == ".pptx"
                || Path.GetExtension(arch.FileName).ToLower() == ".xls" || Path.GetExtension(arch.FileName).ToLower() == ".png"
                || Path.GetExtension(arch.FileName).ToLower() == ".jpg")
                {
                    if (!Directory.Exists(ruta))
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath(ruta));
                    }
                    rutaArchivo = arch.FileName;
                    path = Path.Combine(Server.MapPath(ruta), rutaArchivo);

                    archivos.Add(rutaArchivo);
                    archivosGuardar.Add(path);                    
                }
            }
            seleccionEvaluacion.ListaArchivos = archivos;
            seleccionEvaluacion.IdEmpresa = usuarioActual.IdEmpresa;
            seleccionEvaluacion.vigencia = new DateTime(1900, 1, 1);
            bool resultGuardarSeleccionEvaluacion = false;
            ServiceClient.EliminarParametros();
            
            resultGuardarSeleccionEvaluacion = ServiceClient.PeticionesPostJsonRestFulRespuetaBool<EDSeleccionYEvaluacion>(urlAplicacion, CapacidadGuardarSeleccionYEvaluacion, seleccionEvaluacion);
            if (resultGuardarSeleccionEvaluacion)
            {
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    HttpPostedFileBase ima = Request.Files[i] as HttpPostedFileBase;
                    ima.SaveAs(archivosGuardar[i]);
                }
            }
            ViewBag.guardadoConExito = resultGuardarSeleccionEvaluacion;


            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("idEmpresa", usuarioActual.IdEmpresa);
            var ProductosPorCriterio = ServiceClient.ObtenerArrayJsonRestFul<EDServicioProducto>(urlAplicacion, CapacidadObtenerProductosPorCriterios, RestSharp.Method.GET);
            ViewBag.Pk_Id_Productos = new SelectList(ProductosPorCriterio, "idServicioProducto", "DescripcionProducto");
            
            return View();
        }
        
        public ActionResult VisualizarCalificacion()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "Debe estar autenticado para Continuar.";
                return RedirectToAction("Login", "Home");
            }
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("idEmpresa", usuarioActual.IdEmpresa);
            var ProveedorCalif = ServiceClient.ObtenerArrayJsonRestFul<EDSeleccionYEvaluacion>(urlAplicacion, CapacidadObtenerProveedorContratista, RestSharp.Method.GET);
            return View("ProveedoresCalificados", ProveedorCalif);
        }

        public ActionResult EditProveedorContratista(int idProveedor) 
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "Debe estar autenticado para Continuar.";
                return RedirectToAction("Login", "Home");
            }
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("idProveedor", idProveedor);
            var ProveedorContratista = ServiceClient.ObtenerObjetoJsonRestFul<EDProveedorContratista>(urlAplicacion, CapacidadObtenerProveedor, RestSharp.Method.GET);
            //return PartialView("EditProdVP", ProductosPorCriterio);
            return View(ProveedorContratista);
        }

        [HttpPost]
        public ActionResult EditProveedorContratista(EDProveedorContratista ProveedorContratista)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "Debe estar autenticado para Continuar.";
                return RedirectToAction("Login", "Home");
            }
            ProveedorContratista.idEmpresa = usuarioActual.IdEmpresa;
            bool resultEditarContrat = false;
            ServiceClient.EliminarParametros();
            resultEditarContrat = ServiceClient.PeticionesPostJsonRestFulRespuetaBool<EDProveedorContratista>(urlAplicacion, CapacidadEditarProveedorContratista, ProveedorContratista);
            ViewBag.editadoConExito = resultEditarContrat;

            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("idEmpresa", usuarioActual.IdEmpresa);
            var ProveedoresContratistas = ServiceClient.ObtenerArrayJsonRestFul<EDProveedorContratista>(urlAplicacion, CapacidadObtenerListaProveedores, RestSharp.Method.GET);

            return View("ProveedoresContratistas", ProveedoresContratistas);
        }

        ///<summary>
        ///Metodo que me carga la edicion del Producto y los CriteriosSST.
        ///</summary>
        ///<returns>vista</returns>
        
        [HttpPost]
        public ActionResult EditarProductoCriterioSel(int id)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "Debe estar autenticado para Continuar.";
                return RedirectToAction("Login", "Home");
            }
            var ListaCriterios = new List<EDCriteriosSST>();
            ListaCriterios = LNCriteriosSST.ObtenerCriteriosSST();           
            var index_criterio = new List<EDProductoPorCriterioSSt>(); 
            ViewBag.Pk_Id_Criterios = new SelectList(ListaCriterios, "IdCriterioSST", "NombreCriterioSST", index_criterio);
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("idProducto", id);
            var ProductosPorCriterio = ServiceClient.ObtenerObjetoJsonRestFul<EDServicioProducto>(urlAplicacion, CapacidadObtenerProducto, RestSharp.Method.GET);
            return PartialView("EditProdVP", ProductosPorCriterio);
        }

        public ActionResult EdicionProductoPorCriterios(EDProductoCriterio productocriterio)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "Debe estar autenticado para Continuar.";
                return RedirectToAction("Login", "Home");
            }
            bool resultEditarProductoCriterio = false;
            productocriterio.Fk_Empresa = usuarioActual.IdEmpresa;
            ServiceClient.EliminarParametros();
            resultEditarProductoCriterio = ServiceClient.PeticionesPostJsonRestFulRespuetaBool<EDProductoCriterio>(urlAplicacion, CapacidadEditarProductoCriterio, productocriterio);
            ViewBag.editadoConExito = resultEditarProductoCriterio;
            var ListaCriterios = new List<EDCriteriosSST>();
            ListaCriterios = LNCriteriosSST.ObtenerCriteriosSST();
            var index_criterio = new List<EDProductoPorCriterioSSt>();
            ViewBag.Pk_Id_Criterios = new SelectList(ListaCriterios, "IdCriterioSST", "NombreCriterioSST", index_criterio);
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("idEmpresa", usuarioActual.IdEmpresa);
            var ProductosPorCriterio = ServiceClient.ObtenerArrayJsonRestFul<EDServicioProducto>(urlAplicacion, CapacidadObtenerProductosPorCriterios, RestSharp.Method.GET);
            return View("Create", ProductosPorCriterio);
        }

        /// <summary>
        /// Metodo que obtiene el Producto y sus Criterios
        /// </summary>      
        /// <param name="">id del producto</param>
        /// <returns>Vista parcial con el Producto y sus criterios para la calificacion</returns>
        public ActionResult ObtenerProductoPorCriterios(int Pk_Id_Producto)
        {
            bool resultGuardado = false;

            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("idProducto", Pk_Id_Producto);
            var result = ServiceClient.ObtenerObjetoJsonRestFul<EDServicioProducto>(urlAplicacion, CapacidadObtenerProducto, RestSharp.Method.GET);
            //if (result.idServicioProducto > 0)
            //{
            //    resultGuardado = true;
            //}
            //return Json(new
            //{
            //    success = resultGuardado,
            //    ppc = result
            //}, JsonRequestBehavior.AllowGet);
            return PartialView("CalificarVP",result);
        }

        ///<summary>
        ///Metodo que me carga la vista para Editar la calificacion del proveedor.
        ///</summary>
        ///<returns>vista</returns>
        public ActionResult EditCalificacionProveedor(int idProveePorCalif)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "Debe estar autenticado para Continuar.";
                return RedirectToAction("Login", "Home");
            }
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("idProveePorCalif", idProveePorCalif);
            var ProveedorCalif = ServiceClient.ObtenerObjetoJsonRestFul<EDSeleccionYEvaluacion>(urlAplicacion, CapacidadObtenerProveedorContratistaEditar, RestSharp.Method.GET);
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("idEmpresa", usuarioActual.IdEmpresa);
            var ProductosPorCriterio = ServiceClient.ObtenerArrayJsonRestFul<EDServicioProducto>(urlAplicacion, CapacidadObtenerProductosPorCriterios, RestSharp.Method.GET);
            ViewBag.Pk_Id_Productos = new SelectList(ProductosPorCriterio, "idServicioProducto", "DescripcionProducto");            
            return View(ProveedorCalif);
        }

        [HttpPost]
        public ActionResult EditCalificacionProveedor (EDSeleccionYEvaluacion seleccionEvaluacion, List<HttpPostedFileBase> files)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "Debe estar autenticado para Continuar.";
                return RedirectToAction("Login", "Home");
            }
            var path = "";
            var ruta = rutaAnexosProv + usuarioActual.NitEmpresa;
            var rutaArchivo = "";
            List<string> archivos = new List<string>();
            List<string> archivosGuardar = new List<string>();
            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFileBase arch = Request.Files[i] as HttpPostedFileBase;
                if (arch != null && Path.GetExtension(arch.FileName).ToLower() == ".pdf" || Path.GetExtension(arch.FileName).ToLower() == ".xlsx"
                || Path.GetExtension(arch.FileName).ToLower() == ".doc" || Path.GetExtension(arch.FileName).ToLower() == ".docx"
                || Path.GetExtension(arch.FileName).ToLower() == ".ppt" || Path.GetExtension(arch.FileName).ToLower() == ".pptx"
                || Path.GetExtension(arch.FileName).ToLower() == ".xls" || Path.GetExtension(arch.FileName).ToLower() == ".png"
                || Path.GetExtension(arch.FileName).ToLower() == ".jpg")
                {
                    if (!Directory.Exists(ruta))
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath(ruta));
                    }
                    rutaArchivo = arch.FileName;
                    path = Path.Combine(Server.MapPath(ruta), rutaArchivo);
                    archivos.Add(rutaArchivo);
                    archivosGuardar.Add(path);
                }
            }
            seleccionEvaluacion.ListaArchivos = archivos;
            seleccionEvaluacion.vigencia = new DateTime(1900, 1, 1);
            seleccionEvaluacion.IdEmpresa = usuarioActual.IdEmpresa;
            bool resultGuardarSeleccionEvaluacion = false;
            ServiceClient.EliminarParametros();

            resultGuardarSeleccionEvaluacion = ServiceClient.PeticionesPostJsonRestFulRespuetaBool<EDSeleccionYEvaluacion>(urlAplicacion, CapacidadEditarSeleccionYEvaluacion, seleccionEvaluacion);
            if (resultGuardarSeleccionEvaluacion)
            {
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    HttpPostedFileBase ima = Request.Files[i] as HttpPostedFileBase;
                    ima.SaveAs(archivosGuardar[i]);
                }
            }
            ViewBag.guardadoConExito = resultGuardarSeleccionEvaluacion;


            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("idEmpresa", usuarioActual.IdEmpresa);
            var ProveedorCalif = ServiceClient.ObtenerArrayJsonRestFul<EDSeleccionYEvaluacion>(urlAplicacion, CapacidadObtenerProveedorContratista, RestSharp.Method.GET);
            return View("ProveedoresCalificados", ProveedorCalif);
        }

        /// <summary>
        /// Metodo que muestra los Contratistas o Proveedores
        /// </summary>      
        /// <param name=""></param>
        /// <returns></returns>
        public ActionResult MostrarProveedores()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "Debe estar autenticado para Continuar.";
                return RedirectToAction("Login", "Home");
            }
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("idEmpresa", usuarioActual.IdEmpresa);
            var ProveedoresContratistas = ServiceClient.ObtenerArrayJsonRestFul<EDProveedorContratista>(urlAplicacion, CapacidadObtenerListaProveedores, RestSharp.Method.GET);

            return View("ProveedoresContratistas", ProveedoresContratistas);
        }
         /// <summary>
        /// Metodo que realiza la eliminacion del Contratista o Proveedor
        /// </summary>      
        /// <param name=""></param>
        /// <returns></returns>
        public ActionResult DeleteConfirmedCalificacion(int idProveePorCalif)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "Debe estar autenticado para Continuar.";
                return RedirectToAction("Login", "Home");
            }
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("idProveePorCalif", idProveePorCalif);
            var doc = ServiceClient.ObtenerArrayJsonRestFul<string>(urlAplicacion, CapacidadConsultarAnexosProveedor, RestSharp.Method.GET);
            
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("idProveePorCalif", idProveePorCalif);
            bool respuestaEliminadoCalif = ServiceClient.PeticionesPostJsonRestFulRespuetaBool(urlAplicacion, CapacidadEliminarCalificacionProveedor, RestSharp.Method.DELETE);
            string ruta = rutaAnexosProv + usuarioActual.NitEmpresa; 
            if (respuestaEliminadoCalif)
            {                
                var path = Server.MapPath(ruta);
                foreach(var item in doc)
                {
                    var file = item;
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
                            registraLog.RegistrarError(typeof(CriteriosSSTController), string.Format("Error al eliminar Los anexos de los proveedores  {0}: {1}", DateTime.Now, e.StackTrace), e);
                        }
                    }
                }
                
            }
            ViewBag.respuestaEliminadoCalif = respuestaEliminadoCalif;

            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("idEmpresa", usuarioActual.IdEmpresa);
            var ProveedorCalif = ServiceClient.ObtenerArrayJsonRestFul<EDSeleccionYEvaluacion>(urlAplicacion, CapacidadObtenerProveedorContratista, RestSharp.Method.GET);
            return View("ProveedoresCalificados", ProveedorCalif); 
        }

        /// <summary>
        /// Metodo que realiza la eliminacion del Producto y Criterios
        /// </summary>      
        /// <param name=""></param>
        /// <returns></returns>
        public ActionResult DeleteConfirmed(int idProducto)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "Debe estar autenticado para Continuar.";
                return RedirectToAction("Login", "Home");
            }

            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("idProducto", idProducto);
            bool respuestaEliminado = ServiceClient.PeticionesPostJsonRestFulRespuetaBool(urlAplicacion, CapacidadEliminarProductoCriterio, RestSharp.Method.DELETE);
            ViewBag.respuestaEliminado = respuestaEliminado;
            var ListaCriterios = new List<EDCriteriosSST>();
            ListaCriterios = LNCriteriosSST.ObtenerCriteriosSST();
            var index_criterio = new List<EDProductoPorCriterioSSt>();
            ViewBag.Pk_Id_Criterios = new SelectList(ListaCriterios, "IdCriterioSST", "NombreCriterioSST", index_criterio);
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("idEmpresa", usuarioActual.IdEmpresa);
            var ProductosPorCriterio = ServiceClient.ObtenerArrayJsonRestFul<EDServicioProducto>(urlAplicacion, CapacidadObtenerProductosPorCriterios, RestSharp.Method.GET);
            return View("Create", ProductosPorCriterio);            
        }

        public ActionResult MostrarGrafico(int idProveedor)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "Debe estar autenticado para Continuar.";
                return RedirectToAction("Login", "Home");
            }
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("idProveedor", idProveedor);
            var ProveedorInfo = ServiceClient.ObtenerArrayJsonRestFul<EDSeleccionYEvaluacion>(urlAplicacion, CapacidadObtenerProveedorContratistaGraficar, RestSharp.Method.GET);

            return Json(new
            {
                success = ProveedorInfo
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DescargarAnexo(string nombEvide)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            string ruta = rutaAnexosProv + usuarioActual.NitEmpresa;
            var path = Server.MapPath(ruta);
            var file = nombEvide;
            var fullPath = Path.Combine(path, file);
            byte[] fileBytes = System.IO.File.ReadAllBytes(fullPath);
            MemoryStream ms = new MemoryStream(fileBytes, 0, 0, true, true);
            Response.AddHeader("content-disposition", "attachment;filename=" + nombEvide + "");
            Response.Buffer = true;
            Response.Clear();
            Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length);
            Response.OutputStream.Flush();
            Response.End();
            if (Path.GetExtension(nombEvide).ToLower() == ".pdf")
            {
                return new FileStreamResult(Response.OutputStream, "application/pdf");
            }
            else
            {
                return new FileStreamResult(Response.OutputStream, "application/vnd.ms-excel");
            }
        }
    }
}