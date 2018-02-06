
namespace SG_SST.Controllers.Empresas
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;
    using SG_SST.Models;
    using SG_SST.Models.Empresas;
    using System.IO;
    using System.Threading.Tasks;
    using System.Threading;
    using SG_SST.Services.Empresas.Services;
    using SG_SST.Services.Empresas.IServices;
    using SG_SST.Controllers.Base;

    public class ProcesoController : BaseController
    {
        private SG_SSTContext db = new SG_SSTContext();
        private IProcesoServicios procesoServicios = new ProcesoServicios();
        // GET: Proceso
        public ActionResult Index()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }

            int pkidempresa = usuarioActual.IdEmpresa;
            List<Proceso> pr = (from p in db.Tbl_Procesos
                                join pe in db.Tbl_ProcesoEmpresa on p.Pk_Id_Proceso equals pe.Fk_Id_Proceso
                                where pe.Fk_Id_Empresa == pkidempresa
                                select p).ToList();

            ViewBag.Fk_Id_Proceso = new SelectList(pr, "Pk_Id_Proceso", "Descripcion_Proceso");
            return View(pr);
        }



        public ActionResult BusquedaProceso(string searchstring)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            int pkidempresa = usuarioActual.IdEmpresa;

            var data = (from p in db.Tbl_Procesos
                        join pe in db.Tbl_ProcesoEmpresa on p.Pk_Id_Proceso equals pe.Fk_Id_Proceso
                        where pe.Fk_Id_Empresa == pkidempresa && p.Descripcion_Proceso.Contains(searchstring)
                        select p).ToList();


            return PartialView("ProcesosBusqueda", data);
        }

        public ActionResult Details(int? id)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proceso proceso = db.Tbl_Procesos.Find(id);
            if (proceso == null)
            {
                return HttpNotFound();
            }
            return View(proceso);
        }

        //GET: Proceso/Create
        public ActionResult Create()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            int pkidempresa = usuarioActual.IdEmpresa;
            List<Proceso> pr = (from p in db.Tbl_Procesos
                                join pe in db.Tbl_ProcesoEmpresa on p.Pk_Id_Proceso equals pe.Fk_Id_Proceso
                                where pe.Fk_Id_Empresa == pkidempresa
                                select p).ToList();

            ViewBag.Fk_Id_Proceso = new SelectList(pr, "Pk_Id_Proceso", "Descripcion_Proceso");
            return View();
        }


        [HttpPost]

        public ActionResult Create(Proceso proceso)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            var sw = false;
            int pkempresa = usuarioActual.IdEmpresa;

            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        proceso.Descripcion_Proceso = proceso.Descripcion_Proceso.ToUpper();
                        List<Proceso> procesos = (from p in db.Tbl_Procesos
                                                  join pe in db.Tbl_ProcesoEmpresa on p.Pk_Id_Proceso equals pe.Fk_Id_Proceso
                                                  where pe.Fk_Id_Empresa == pkempresa
                                                  select p).ToList();
                        if (procesos != null)
                        {
                            for (int i = 0; i < procesos.Count; i++)
                            {
                                if (proceso.Descripcion_Proceso == procesos[i].Descripcion_Proceso)
                                {
                                    sw = true;
                                }
                            }
                            if (sw == true)
                            {
                                ViewBag.Message1 = "El Registro Ingresado ya Existe.";
                                return View("Index", procesos);
                            }

                            else
                            {
                                db.Tbl_Procesos.Add(proceso);
                                db.Tbl_ProcesoEmpresa.Add(new ProcesoEmpresa
                                {
                                    Fk_Id_Empresa = usuarioActual.IdEmpresa,
                                    Fk_Id_Proceso = proceso.Pk_Id_Proceso
                                });
                            }

                        }
                        db.SaveChanges();
                        transaction.Commit();

                    }

                }
                catch
                {
                    transaction.Rollback();

                }
            }

            List<Proceso> process = (from p in db.Tbl_Procesos
                                     join pe in db.Tbl_ProcesoEmpresa on p.Pk_Id_Proceso equals pe.Fk_Id_Proceso
                                     where pe.Fk_Id_Empresa == pkempresa
                                     select p).ToList();
            ViewBag.Fk_Id_Proceso = new SelectList(db.Tbl_Procesos, "Pk_Id_Proceso", "Descripcion_Proceso", proceso.Fk_Id_Proceso);
            ViewBag.Message = "Proceso Almacenado";
            return View("Index", process);
        }


        public ActionResult Edit(int? id)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proceso proceso = db.Tbl_Procesos.Find(id);
            if (proceso == null)
            {
                return HttpNotFound();
            }
            int pkidempresa = usuarioActual.IdEmpresa;
            List<Proceso> pr = (from p in db.Tbl_Procesos
                                join pe in db.Tbl_ProcesoEmpresa on p.Pk_Id_Proceso equals pe.Fk_Id_Proceso
                                where pe.Fk_Id_Empresa == pkidempresa
                                select p).ToList();
            ViewBag.Fk_Id_Proceso = new SelectList(pr, "Pk_Id_Proceso", "Descripcion_Proceso", proceso.Fk_Id_Proceso);
            return View(proceso);
        }

        [HttpPost]

        public ActionResult Edit(Proceso proceso)
        {
            var procesos = false;
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }


            if (ModelState.IsValid)
            {
                List<Proceso> buscarp = (from p in db.Tbl_Procesos.AsNoTracking()
                                         join pe in db.Tbl_ProcesoEmpresa on p.Pk_Id_Proceso equals pe.Fk_Id_Proceso
                                         where pe.Fk_Id_Empresa == usuarioActual.IdEmpresa
                                         select p).ToList();

                if (proceso != null)
                {
                    proceso.Descripcion_Proceso = proceso.Descripcion_Proceso.ToUpper();
                    if (buscarp != null)
                    {
                        foreach (var rs in buscarp)
                        {
                            if (rs.Descripcion_Proceso == proceso.Descripcion_Proceso && rs.Pk_Id_Proceso != proceso.Pk_Id_Proceso)
                            {
                                procesos = true;
                            }
                        }
                        if (procesos != false)
                        {
                            ViewBag.mensaje1 = "El Proceso Ingresado ya existe.";
                            return View("Index", buscarp);
                        }
                        else
                        {


                            db.Entry(proceso).State = EntityState.Modified;




                        }

                    }
                }


                db.SaveChanges();
                List<Proceso> buscar = (from p in db.Tbl_Procesos
                                        join pe in db.Tbl_ProcesoEmpresa on p.Pk_Id_Proceso equals pe.Fk_Id_Proceso
                                        where pe.Fk_Id_Empresa == usuarioActual.IdEmpresa
                                        select p).ToList();

                ViewBag.mensaje1 = "Proceso Modificado Correctamente.";
                return View("Index", buscar);
            }
            ViewBag.Fk_Id_Proceso = new SelectList(db.Tbl_Procesos, "Pk_Id_Proceso", "Descripcion_Proceso", proceso.Fk_Id_Proceso);
            return View(proceso);
        }

        // GET: Proceso/Delete/5
        public ActionResult Delete(int? id)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proceso proceso = db.Tbl_Procesos.Find(id);
            if (proceso == null)
            {
                return HttpNotFound();
            }
            return View(proceso);
        }


        public ActionResult DeleteConfirmed(int id)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            int pkidempresa = usuarioActual.IdEmpresa;
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    Proceso proceso = db.Tbl_Procesos.Find(id);
                    db.Tbl_Procesos.Remove(proceso);
                    db.SaveChanges();
                    transaction.Commit();

                    List<Proceso> pr = (from p in db.Tbl_Procesos
                                        join pe in db.Tbl_ProcesoEmpresa on p.Pk_Id_Proceso equals pe.Fk_Id_Proceso
                                        where pe.Fk_Id_Empresa == pkidempresa 
                                        select p).ToList();
                    if (pr != null)
                    {
                        ViewBag.Message1 = "Proceso fue eliminado.";

                    }
                    return View("index", pr);
                }

                catch (Exception ex)
                {
                    transaction.Rollback();
 
                    List<Proceso> pr = (from p in db.Tbl_Procesos
                                        join pe in db.Tbl_ProcesoEmpresa on p.Pk_Id_Proceso equals pe.Fk_Id_Proceso
                                        where pe.Fk_Id_Empresa == pkidempresa
                                        select p).ToList();
                    ViewBag.Fk_Id_Proceso = new SelectList(pr, "Pk_Id_Proceso", "Descripcion_Proceso");
                    ViewBag.Message1e = "El registro seleccionado no es permitido eliminarlo.";
                    return View("index", pr);
                }
                    
                   
                }
            }

        



        public ActionResult CrearProceso()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            int pkidempresa = usuarioActual.IdEmpresa;
            List<Proceso> pr = (from p in db.Tbl_Procesos
                                join pe in db.Tbl_ProcesoEmpresa on p.Pk_Id_Proceso equals pe.Fk_Id_Proceso
                                where pe.Fk_Id_Empresa == pkidempresa
                                select p).ToList();

            if (pr.Count == 0)
            {
                ViewBag.Message1a = "No se han Registrado los Procesos, Registrelos para ver el Mapa en Linea.";
            }
            return View();
        }



        public ActionResult MenuProcesos()
        {

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            ViewBag.espdf = false;

            try
            {
                Empresa ime = db.Tbl_Empresa.First(o => o.Pk_Id_Empresa == usuarioActual.IdEmpresa);
                if (ime != null && ime.imagen_proceso != null)
                {
                    if (Path.GetExtension(ime.imagen_proceso).ToLower() == ".pdf")
                    {
                        ViewBag.espdf = true;
                    }

                    else
                    {
                        ViewBag.espdf = false;
                    }
                }
                else
                {
                    //ViewBag.Message1 = "Debe estar Autenticado en el sistema para continuar.";
                }

            }
            catch (Exception e)
            {
                ViewBag.Message1 = "Para iniciar primero se debe registrar la Empresa.";
                return View("MenuProcesos");
            }
            int pkidempresa = usuarioActual.IdEmpresa;
            List<Proceso> pr = (from p in db.Tbl_Procesos
                                join pe in db.Tbl_ProcesoEmpresa on p.Pk_Id_Proceso equals pe.Fk_Id_Proceso
                                where pe.Fk_Id_Empresa == pkidempresa
                                select p).ToList();
            if (pr.Count > 0)
            {
                return View("MenuProcesos");
            }
            else
            {
                ViewBag.Message1a = "Para continuar se deben Registrar los Procesos.!";
            }

            return View("MenuProcesos");
        }








        public ActionResult CreateP()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            Empresa empresa = new Empresa();

            return View(empresa);
        }





        [HttpPost]
        public ActionResult CreateP(HttpPostedFileBase CargarImagenP)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            var db = new SG_SSTContext();
            Empresa empresa = db.Tbl_Empresa.Find(usuarioActual.IdEmpresa);
            var img = "";
            ViewBag.espdf = false;
            if (CargarImagenP != null && (Path.GetExtension(CargarImagenP.FileName).ToLower() == ".jpg" || Path.GetExtension(CargarImagenP.FileName).ToLower() == ".png" || Path.GetExtension(CargarImagenP.FileName).ToLower() == ".pdf") && CargarImagenP.ContentLength <= (6 * 1024 * 1024))
            {
                if (Path.GetExtension(CargarImagenP.FileName).ToLower() == ".pdf")
                {
                    ViewBag.espdf = true;
                }
                else
                {
                    ViewBag.pkidempresa = usuarioActual.IdEmpresa;
                }
                img = Path.Combine(Server.MapPath("~/Content/Images"), CargarImagenP.FileName);
                CargarImagenP.SaveAs(img);
                empresa.imagen_proceso = CargarImagenP.FileName;
                db.Entry(empresa).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                ViewBag.Message = "Carga de archivo se realizo correctamente.!";
            }
            else
            {
                Empresa ime = db.Tbl_Empresa.FirstOrDefault(o => o.Pk_Id_Empresa == usuarioActual.IdEmpresa && o.imagen_proceso != null);
                if (ime != null)
                {
                    if (Path.GetExtension(ime.imagen_proceso).ToLower() == ".pdf")
                    {
                        ViewBag.espdf = true;
                    }
                }
                ViewBag.pkidempresa = usuarioActual.IdEmpresa;
                ViewBag.Message1 = "No cargaste ningun archivo, o el archivo supera el maximo permitido que son 6 megabytes, y solo se pueden cargar archivos .PDF, .JPG o .PNG.";

            }
            return View("MenuProcesos");
        }


        public ActionResult GetImagenP()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            try
            {
                int pkidempresa = usuarioActual.IdEmpresa;
                Empresa ime = db.Tbl_Empresa.First(o => o.Pk_Id_Empresa == pkidempresa & o.imagen_proceso != null);
                if (ime != null)
                {
                    string nombreFirma = db.Tbl_Empresa.Find(pkidempresa).imagen_proceso;
                    var path = Server.MapPath("~/Content/Images/");
                    var file = nombreFirma;
                    var fullPath = Path.Combine(path, file);
                    return File(fullPath, "image/jpg", file);
                }
                else
                {
                    ViewBag.Message1 = "No hay Archivo Actualmente debes Cargarlo.";
                }
                ViewBag.pkidempresa = pkidempresa;
                return View();
            }
            catch (Exception)
            {
                return View("MenuProcesos");
            }
        }

        public FileStreamResult ProcesoPdf()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "Debe estar Autenticado para continuar en el sistema.";
               
            }
            Empresa ime = db.Tbl_Empresa.First(o => o.Pk_Id_Empresa == usuarioActual.IdEmpresa && o.imagen_proceso != null);
            var path = Server.MapPath("~/Content/Images/");
            var file = ime.imagen_proceso;
            var fullPath = Path.Combine(path, file);
            FileStream fs = new FileStream(fullPath, FileMode.Open, FileAccess.Read);
            return File(fs, "application/pdf");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }




        public JsonResult JsonProceso()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "Debe estar Autenticado en el sistema para continuar.";
            }
            int pkidempresa = usuarioActual.IdEmpresa;
            List<Proceso> procesos = (from p in db.Tbl_Procesos
                                      join pe in db.Tbl_ProcesoEmpresa on p.Pk_Id_Proceso equals pe.Fk_Id_Proceso
                                      where pe.Fk_Id_Empresa == pkidempresa
                                      select p).ToList();

            if (procesos.Count != 0)
            {
                return Json(
                   procesos.Select(Proceso => new
                   {
                       ProcesoPadre = Proceso.Pk_Id_Proceso,
                       Descripcion_Proceso = Proceso.Descripcion_Proceso,
                       ProcesoHijo = (Proceso.Fk_Id_Proceso == null) ? -1 : Proceso.Fk_Id_Proceso,

                   })
                , JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult obtenerSuprocesos(int Pk_ProcesoPrincipal)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            List<Proceso> procesos = procesoServicios.ObtenerSubProcesos(Pk_ProcesoPrincipal);
            if (procesos.Count != 0)
            {
                return Json(
                   procesos.Select(Proceso => new
                   {
                       Procesoid = Proceso.Pk_Id_Proceso,
                       Descripcion_Proceso = Proceso.Descripcion_Proceso
                   })
                , JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult MostrarProcesos()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "Debe estar Autenticado en el sistema para continuar.";
            }
            return View();
        }

    }






}
