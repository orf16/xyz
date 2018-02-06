
namespace SG_SST.Controllers.Empresas
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;
    using SG_SST.Models;
    using SG_SST.Models.Empresas;
    using SG_SST.Models.LiderazgoGerencial;
    using SG_SST.Services.LiderazgoGerencial.Iservices;
    using SG_SST.Services.LiderazgoGerencial.Services;
    using System.Threading;
    using System.Configuration;
    using RestSharp;
    using System.IO;
    using SG_SST.Dtos.Empresas;
    using SG_SST.Services.Empresas.IServices;
    using SG_SST.Services.Empresas.Services;
    using SG_SST.Controllers.Planificacion;
    using SG_SST.Models.Empresa;
    using SG_SST.EntidadesDominio.Empresas;
    using SG_SST.ServiceRequest;
    using SG_SST.EntidadesDominio.Ausentismo;
    using SG_SST.Controllers.Base;
    using System.Drawing;
    using SG_SST.Audotoria;

    public class EmpresasController : BaseController
    {
        private SG_SSTContext db = new SG_SSTContext();
        private IRolPorResponsabilidadServicio rolPorResponsabilidadServicio = new RolPorResponsabilidadServicio();
        private IEmpresaServicios EmpresaServices = new EmpresaServicios();

        string urlServicioEmpresas = ConfigurationManager.AppSettings["UrlServicioEmpresas"];
        string CapacidadGuardarEmpresa = ConfigurationManager.AppSettings["CapacidadGuardarEmpresa"];
        string logoEmpresa = ConfigurationManager.AppSettings["LogoEmpresa"];
        string GuardarLogoEmpresa = ConfigurationManager.AppSettings["GuardarLogoEmpresa"];
        string ObtenerLogoEmpresa = ConfigurationManager.AppSettings["ObtenerLogoEmpresa"];
       

        // GET: Empresas
        public ActionResult Index()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            var buscaruta = (from le in db.Tbl_Empresa where le.Pk_Id_Empresa == usuarioActual.IdEmpresa select le).SingleOrDefault();
            ViewBag.dato = buscaruta.Logo_Empresa;

            try
            {
                var tbl_Empresa = db.Tbl_Empresa.Where(e => e.Pk_Id_Empresa > 0 && e.Pk_Id_Empresa == usuarioActual.IdEmpresa);
                return View(tbl_Empresa.ToList());
            }
            catch
            {
                var tbl_Empresa = db.Tbl_Empresa.Where(e => e.Pk_Id_Empresa > 0 && e.Pk_Id_Empresa == usuarioActual.IdEmpresa);
                return View(tbl_Empresa.ToList());
            }
        }

        // GET: Estandares
        public ActionResult Estandares()
        {
            try
            {
                //Obtener todas las empresas
                var tbl_Empresa = db.Tbl_Empresa.Where(m => m.Pk_Id_Empresa > 0);

                return View(tbl_Empresa.ToList());
            }
            catch (Exception ex)
            {
                //RegistroInformacion.EnviarError<EmpresasController>(ex.Message);
                return View();
            }
        }

        public ActionResult EstandaresGetTexto()
        {
            try
            {
                //Se detiene la ejecución un poco para simular una demora en el servidor
                Thread.Sleep(2000);
                string texto = "Tiempo de Respuesta: " + DateTime.Now.ToString();

                //Se retorna error aleatorio
                if (DateTime.Now.Second % 3 == 0)
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = true, texto = texto }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Relaciones()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            else
            {
                return RedirectToAction("ConsultaRelacionesLaborales", "RelacionesLaborales");
            }

            
        }

        // GET: Empresas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empresa empresa = db.Tbl_Empresa.Find(id);
            if (empresa == null)
            {
                return HttpNotFound();
            }
            return View(empresa);
        }

        // GET: Empresas/Create
        public ActionResult Create(Empresa empresa)
        {
            var imagen = "";
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            ViewBag.Fk_Id_Departamento = new SelectList(db.Tbl_Departamento, "Pk_Id_Departamento", "Nombre_Departamento");
            ViewBag.nitEmpresa = usuarioActual.NitEmpresa;
            ViewBag.idEmpresa = usuarioActual.IdEmpresa;
            var buscalogo = (from le in db.Tbl_Empresa where le.Pk_Id_Empresa == usuarioActual.IdEmpresa select le).SingleOrDefault();
            if (buscalogo.Logo_Empresa!=null)
            {
               imagen = buscalogo.Logo_Empresa;
               ViewBag.imagenlogo = imagen;
            }
            
            return View();
        }

        /// <summary>
        /// Metodo para Mostrar Automaticamente el Logo de la empresa si se encuentra Cargado.
        /// </summary>
        /// <param name="pkempresa"></param>
        /// <returns></returns>
        public ActionResult MostrarLogoCargado(string nitempresa)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }

            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("nitempresa", nitempresa);
            var logo = ServiceClient.ObtenerObjetoJsonRestFul<EDEmpresas>(urlServicioEmpresas, ObtenerLogoEmpresa, Method.GET);

            //var logo = (from el in db.Tbl_Empresa
            //            where el.Pk_Id_Empresa == usuarioActual.IdEmpresa
            //            select el).SingleOrDefault();
            if(logo.NombreArchivo!=null)
            {
                var Archivo = logo.NombreArchivo;
                return Json(Archivo, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json(new { Data = string.Empty, Mensaje = "ERROR" }, JsonRequestBehavior.AllowGet);
            }
           
        }

      
        /// <summary>
        /// Funcion para Eliminar Logo de la Empresa.
        /// </summary>
        /// <param name="pkempresa"></param>
        /// <returns></returns>
        public ActionResult EliminarLogo(int pkempresa)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            using (var Transaction = db.Database.BeginTransaction())
            {
                RegistraLog registraLog = new RegistraLog();
                try
                {
                    var logo = (from el in db.Tbl_Empresa
                                where el.Pk_Id_Empresa == pkempresa
                                select el).SingleOrDefault();
                    var path = Server.MapPath(logoEmpresa);
                    var file = logo.Logo_Empresa;
                    var fullPath = Path.Combine(path, file);
                    if (logo.Logo_Empresa != null)
                    {
                        logo.Logo_Empresa = null;
                    }
                    db.SaveChanges();
                    Transaction.Commit();

                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }
                }
                catch(Exception ex)
                {
                    registraLog.RegistrarError(typeof(EmpresasController), string.Format("Error al Eliminar el logo de la Empresa  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                    Transaction.Rollback();
                }
            }
            return Json(new { Data = 1 }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Funcion para Redimensionar el tamaño de una Imagen.
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        private static Image RedimensionarImagen(Stream stream)
        {
            // Se crea un objeto Image, que contiene las propiedades de la imagen
            Image img = Image.FromStream(stream);
            int newH = 110;
            int newW = 280;
            // Si las dimensiones cambiaron, se modifica la imagen
            Bitmap newImg = new Bitmap(img, newW, newH);
            Graphics g = Graphics.FromImage(newImg);
            g.InterpolationMode =
                System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
            g.DrawImage(img, 0, 0, newImg.Width, newImg.Height);
            return newImg;
        }

     
        /// <summary>
        /// Metodo para cargar el Logo de la Empresa.
        /// </summary>
        /// <param name="archivo"></param>
        /// <param name="idEmpresa"></param>
        /// <returns></returns>
        public ActionResult AgregarAdjunto(HttpPostedFileBase archivo, int idEmpresa)
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
                    var Logo = new EDEmpresas()
                    {
                        NombreArchivo = idEmpresa+"_Logo_"+ archivo.FileName,
                        Id_Empresa = idEmpresa,
                    };
                    if (archivo != null && Path.GetExtension(archivo.FileName).ToLower() == ".jpg" || Path.GetExtension(archivo.FileName).ToLower() == ".png")
                    {
                        ServiceClient.EliminarParametros();
                        var resultlogo = ServiceClient.RealizarPeticionesPostJsonRestFul<EDEmpresas>(urlServicioEmpresas, GuardarLogoEmpresa, Logo);
                        if (resultlogo != null)
                        {
                            var img = Path.Combine(Server.MapPath(logoEmpresa), usuarioActual.IdEmpresa+"_Logo_"+ archivo.FileName);
                            Image imgRe = RedimensionarImagen(archivo.InputStream);
                            imgRe.Save(img);
                            return Json(new { Data = resultlogo, Mensaje = "OK" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new { Data = string.Empty, Mensaje = "ERROR" }, JsonRequestBehavior.AllowGet);
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

        public ActionResult ObtenerImagen(int pkempresa)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            string Archivo = "";
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    Archivo = db.Tbl_Empresa.Find(pkempresa).Logo_Empresa;
                }
                catch (Exception)
                {
                    transaction.Rollback();

                }
            }
            return Json(Archivo, JsonRequestBehavior.AllowGet);

        }



        [HttpPost]
        public ActionResult Create(GrabarEmpresaModel GbrEmpresa, Sede sede, SedeMunicipio sedeMunicipio)
        {

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "Debe estar autenticado para continuar.";
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                var empresas = db.Tbl_Empresa.Where(e => e.Pk_Id_Empresa == usuarioActual.IdEmpresa & e.Nit_Empresa.Equals(GbrEmpresa.Nit_Empresa)).FirstOrDefault();
                if (empresas != null)
                    ViewBag.mensaje1 = "La Empresa ya se encuentra registrada";

                else
                {
                    var empresaEvaluar = new EDEmpresas()
                     {
                         Razon_Social = GbrEmpresa.Razon_Social,
                         Nit_Empresa = GbrEmpresa.Nit_Empresa,
                         Codigo_Actividad = Convert.ToInt32(GbrEmpresa.Codigo_Actividad),
                         Tipo_Documento = GbrEmpresa.Tipo_Documento,
                         Telefono = GbrEmpresa.Telefono,
                         Fax = GbrEmpresa.Fax,
                         Identificacion_Representante = GbrEmpresa.Identificacion_Representante,
                         IdSeccional = GbrEmpresa.Id_Seccional,
                         IdSectorEconomico = GbrEmpresa.Id_SectorEconomico,
                         Riesgo = GbrEmpresa.Riesgo,
                         Fecha_Vigencia_Actual = GbrEmpresa.Fecha_Vigencia_Actual,
                         Flg_Estado = GbrEmpresa.Flg_Estado,
                         Zona = GbrEmpresa.Zona,
                         SitioWeb = GbrEmpresa.Sitio_Web,
                         Total_Empleados = GbrEmpresa.Total_Empleados,
                         Direccion = GbrEmpresa.Direccion,
                         Descripcion_Actividad = GbrEmpresa.Descripcion_Actividad,
                         Email = GbrEmpresa.Email,
                     };
                    ServiceClient.EliminarParametros();
                    var result = ServiceClient.RealizarPeticionesPostJsonRestFul<EDEmpresas>(urlServicioEmpresas, CapacidadGuardarEmpresa, empresaEvaluar);
                    if (result != null)
                    {
                        using (SG_SSTContext datos = new SG_SSTContext())
                        {
                            var empresa = new EDEmpresas();
                            sede.SedeMunicipios = new List<SedeMunicipio>();
                            sede.SedeMunicipios.Add(sedeMunicipio);
                            sede.Nombre_Sede = "Principal";
                            bool respuestaGuardado = rolPorResponsabilidadServicio.CrearRolYResponsabilidadesPreestablecidos(result.Id_Empresa);
                            ViewBag.respuestaGuardado = respuestaGuardado;
                            var busca = datos.Tbl_Empresa.Where(e => e.Pk_Id_Empresa > 0 & e.Pk_Id_Empresa == usuarioActual.IdEmpresa);
                            ViewBag.mensaje = "Empresa " + GbrEmpresa.Razon_Social + " Almacenada Correctamente.";
                            return View("Index", busca.ToList());
                        }
                    }
                    else
                    {
                        using (SG_SSTContext datos = new SG_SSTContext())
                        {
                            var busca = datos.Tbl_Empresa.Where(e => e.Pk_Id_Empresa > 0 & e.Pk_Id_Empresa == usuarioActual.IdEmpresa);
                            ViewBag.mensaje1 = "El Registro no se pudo Realizar.";
                            return View("Index", busca.ToList());
                        }
                    }

                }

            }

            else
            {
                using (SG_SSTContext datos = new SG_SSTContext())
                {
                    var busca = datos.Tbl_Empresa.Where(e => e.Pk_Id_Empresa > 0 & e.Pk_Id_Empresa == usuarioActual.IdEmpresa);
                    ViewBag.mensaje1 = "Alerta: Se presento un error en la transacción.";
                    return View("Index", busca.ToList());
                }
            }
            return View("Index");
        }


        // GET: Empresas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empresa empresa = db.Tbl_Empresa.Find(id);
            if (empresa == null)
            {
                return HttpNotFound();
            }
            ViewBag.Urbano = empresa.Zona.Equals("U") ? true : false;
            ViewBag.Activo = empresa.Flg_Estado.Equals("Activa") ? true : false;
            ViewBag.Fecha_Vigencia_Actual = empresa.Fecha_Vigencia_Actual;
            return View(empresa);
        }


        public ActionResult Editar(Empresa empresa)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            using (SG_SSTContext datos = new SG_SSTContext())
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        empresa.Razon_Social = empresa.Razon_Social.ToUpper();
                        db.Entry(empresa).State = EntityState.Modified;
                        db.SaveChanges();

                        var busca = datos.Tbl_Empresa.Where(e => e.Pk_Id_Empresa > 0 & e.Pk_Id_Empresa == usuarioActual.IdEmpresa);
                        ViewBag.mensaje = " Actualizacion de datos se realizo correctamente. ";
                        return View("index", busca.ToList());
                    }
                    ViewBag.Urbano = empresa.Zona.Equals("U") ? true : false;
                    ViewBag.Activo = empresa.Flg_Estado.Equals("Activa") ? true : false;
                    return View(empresa);
                }
                catch (Exception e)
                {
                    ViewBag.mensaje1 = "Se Presento un error en la Transacción.";
                }
                return View(empresa);
            }
        }

        // GET: Empresas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empresa empresa = db.Tbl_Empresa.Find(id);
            if (empresa == null)
            {
                return HttpNotFound();
            }
            return View(empresa);
        }

        public ActionResult DeleteConfirmed(int id)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            using (SG_SSTContext datos = new SG_SSTContext())
            {
                try
                {
                    Empresa empresa = db.Tbl_Empresa.Find(id);
                    db.Tbl_Empresa.Remove(empresa);
                    db.SaveChanges();
                    var busca = db.Tbl_Empresa.Where(e => e.Pk_Id_Empresa > 0 && e.Pk_Id_Empresa == usuarioActual.IdEmpresa);
                    ViewBag.mensaje1 = "Empresa  " + empresa.Razon_Social + "  ,fue eliminada del sistema.";
                    return View("Index", busca.ToList());
                }
                catch (Exception ex)
                {

                    var busca = db.Tbl_Empresa.Where(e => e.Pk_Id_Empresa > 0 & e.Pk_Id_Empresa == usuarioActual.IdEmpresa);
                    ViewBag.mensaje1 = "Registro Seleccionado no puede ser eliminado.";
                    return View("Index", busca.ToList());
                }

            }
        }

        /// <summary>
        /// Metodo para Obtener la informacion de la empresa
        /// proveniente del sistema de Positiva SIARP
        /// </summary>
        /// <param name="nitempresa"></param>
        /// <returns></returns>
        public ActionResult ObtenerSiarp(string nitempresa)
        {
            var datos = string.Empty;
            if (!string.IsNullOrEmpty(nitempresa))
            {
                var cliente = new RestSharp.RestClient(ConfigurationManager.AppSettings["Url"]);
                var request = new RestRequest(ConfigurationManager.AppSettings["consultaEmpresa"], RestSharp.Method.GET);
                request.RequestFormat = DataFormat.Xml;
                request.Parameters.Clear();
                request.AddParameter("tpDoc", "ni");
                request.AddParameter("doc", nitempresa);
                request.AddParameter("color", "orange");
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Accept", "application/json");

                //se omite la validación de certificado de SSL
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                IRestResponse<List<EmpresaSiarpDTO>> response = cliente.Execute<List<EmpresaSiarpDTO>>(request);
                var result = response.Content;

                var respuesta = Newtonsoft.Json.JsonConvert.DeserializeObject<List<EmpresaSiarpDTO>>(result);
                return Json(new { Data = respuesta, Mensaje = "OK" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Data = datos, Mensaje = "OK" });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
