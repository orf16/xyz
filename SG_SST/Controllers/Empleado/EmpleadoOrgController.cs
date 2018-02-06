using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SG_SST.Models;
using SG_SST.Services.Empresas.Services;
using SG_SST.Services.Empresas.IServices;
using SG_SST.Controllers.Base;

namespace SG_SST.Controllers.Empleado
{
    public class EmpleadoOrgController : BaseController
    {
        private SG_SSTContext db = new SG_SSTContext();

        private IEmpresaServicios EmpresaServicios = new EmpresaServicios();

        // GET: EmpleadoOrg
        public ActionResult Index()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
            }
            var tbl_EmpleadoOrg = db.Tbl_EmpleadoOrg.Include(e => e.Organigrama).Include(e => e.OrgChart);
            return View(tbl_EmpleadoOrg.Where(eo => eo.Organigrama.Fk_Id_Empresa == usuarioActual.IdEmpresa).ToList());
        }

        // GET: EmpleadoOrg/Details/5
        public ActionResult Details(int? id)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmpleadoOrg empleadoOrg = db.Tbl_EmpleadoOrg.Find(id);
            if (empleadoOrg == null)
            {
                return HttpNotFound();
            }
            return View(empleadoOrg);
        }

        // GET: EmpleadoOrg/Create
        public ActionResult Create()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
            }
            ViewBag.Fk_Id_Organigrama = new SelectList(db.Tbl_Organigrama.Where(o => o.Fk_Id_Empresa == usuarioActual.IdEmpresa), "Pk_Id_Organigrama", "Nombre_Organigrama");
            ViewBag.Fk_Id_EmpleadoOrg = new SelectList(db.Tbl_EmpleadoOrg.Where(eo => eo.Organigrama.Fk_Id_Empresa == usuarioActual.IdEmpresa), "Id_EmpleadoOrg", "Cargo_Empleado");
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_EmpleadoOrg,Jefe_Inmediato,Cargo_Empleado,Fk_Id_EmpleadoOrg,Fk_Id_Organigrama")] EmpleadoOrg empleadoOrg)
        {

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
            }
            int pkidempresa = usuarioActual.IdEmpresa;

            try
            {
                if (ModelState.IsValid)
                {
                    empleadoOrg.Cargo_Empleado = empleadoOrg.Cargo_Empleado.ToUpper();

                    empleadoOrg.Jefe_Inmediato = empleadoOrg.Fk_Id_EmpleadoOrg.ToString();
                    List<EmpleadoOrg> cargo = (from p in db.Tbl_EmpleadoOrg
                                               join pe in db.Tbl_Organigrama on p.Fk_Id_Organigrama equals pe.Pk_Id_Organigrama
                                               where pe.Fk_Id_Empresa == pkidempresa
                                               select p).ToList();
                    var sw = false;
                    foreach (var rs in cargo)
                    {
                        if (rs.Cargo_Empleado == empleadoOrg.Cargo_Empleado)
                        {
                            sw = true;
                        }
                    }
                    if (sw == true)
                    {
                        ViewBag.mensaje1 = "El Cargo Ingresado ya Existe.";
                        return View("Index", cargo);
                    }
                    bool respuestaguardado = EmpresaServicios.AgregarEmpleadoOrg(empleadoOrg, usuarioActual.IdEmpresa);
                    string mensaje = "";
                    string mensaje1 = "";
                    if (respuestaguardado)
                    {
                        mensaje = "Cargo Agregado Exitosamente.!";
                        ViewBag.mensaje = mensaje;
                    }
                    else
                    {
                        mensaje1 = "No fue Posible agregar el Cargo al Organigrama.";
                        ViewBag.mensaje1 = mensaje1;
                    }

                    ViewBag.respuesta = respuestaguardado;
                    ViewBag.Fk_Id_Organigrama = new SelectList(db.Tbl_Organigrama.Where(o => o.Fk_Id_Empresa == usuarioActual.IdEmpresa), "Pk_Id_Organigrama", "Fk_Id_Organigrama", empleadoOrg.Fk_Id_Organigrama);
                    ViewBag.Fk_Id_EmpleadoOrg = new SelectList(db.Tbl_EmpleadoOrg.Where(eo => eo.Organigrama.Fk_Id_Empresa == usuarioActual.IdEmpresa), "Id_EmpleadoOrg", "Cargo_Empleado", empleadoOrg.Fk_Id_EmpleadoOrg);
                    List<EmpleadoOrg> EmpleadosOrg = EmpresaServicios.ObtenerOrganigrama(usuarioActual.IdEmpresa).EmpleadosOrg.ToList();
                    return View("Index", EmpleadosOrg);

                }


            }
            catch
            {
                string mensaje1 = "";

                mensaje1 = "No existe Registro de la Empresa, Se debe realizar para poder continuar.";
                ViewBag.mensaje1 = mensaje1;
                var busca = db.Tbl_EmpleadoOrg.Where(eo => eo.Id_EmpleadoOrg > 0);
                return View("Index", busca.ToList());
            }



            ViewBag.Fk_Id_Organigrama = new SelectList(db.Tbl_Organigrama.Where(o => o.Fk_Id_Empresa == usuarioActual.IdEmpresa), "Pk_Id_Organigrama", "Fk_Id_Organigrama", empleadoOrg.Fk_Id_Organigrama);
            ViewBag.Fk_Id_EmpleadoOrg = new SelectList(db.Tbl_EmpleadoOrg.Where(eo => eo.Organigrama.Fk_Id_Empresa == usuarioActual.IdEmpresa), "Id_EmpleadoOrg", "Cargo_Empleado", empleadoOrg.Fk_Id_EmpleadoOrg);
            return View(empleadoOrg);
        }

        // GET: EmpleadoOrg/Edit/5
        public ActionResult Edit(int? id)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmpleadoOrg empleadoOrg = db.Tbl_EmpleadoOrg.Find(id);
            if (empleadoOrg == null)
            {
                return HttpNotFound();
            }
            ViewBag.Fk_Id_Organigrama = new SelectList(db.Tbl_Organigrama.Where(o => o.Fk_Id_Empresa == usuarioActual.IdEmpresa), "Pk_Id_Organigrama", "Fk_Id_Organigrama", empleadoOrg.Fk_Id_Organigrama);
            ViewBag.Fk_Id_EmpleadoOrg = new SelectList(db.Tbl_EmpleadoOrg.Where(eo => eo.Organigrama.Fk_Id_Empresa == usuarioActual.IdEmpresa), "Id_EmpleadoOrg", "Cargo_Empleado", empleadoOrg.Fk_Id_EmpleadoOrg);
            List<EmpleadoOrg> EmpleadosOrg = EmpresaServicios.ObtenerOrganigrama(usuarioActual.IdEmpresa).EmpleadosOrg.ToList();
            return View(empleadoOrg);
        }


        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_EmpleadoOrg,Jefe_Inmediato,Cargo_Empleado,Fk_Id_EmpleadoOrg,Fk_Id_Organigrama")] EmpleadoOrg empleadoOrg)
        {
            var cargo = false;

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            int pkidempresa = usuarioActual.IdEmpresa;
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
            }
            if (ModelState.IsValid)
            {

                if (empleadoOrg != null)
                {
                    empleadoOrg.Cargo_Empleado = empleadoOrg.Cargo_Empleado.ToUpper();

                    List<EmpleadoOrg> buscar = (from p in db.Tbl_EmpleadoOrg.AsNoTracking()
                                                join pe in db.Tbl_Organigrama on p.Fk_Id_Organigrama equals pe.Pk_Id_Organigrama
                                                where pe.Fk_Id_Empresa == pkidempresa
                                                select p).ToList();

                    
                    if (buscar != null)
                    {


                        foreach (var rs in buscar)
                        {
                            if (rs.Cargo_Empleado == empleadoOrg.Cargo_Empleado && rs.Id_EmpleadoOrg != empleadoOrg.Id_EmpleadoOrg)
                            {
                                cargo = true;
                            }
                        }
                        if (cargo != false)
                        {
                            ViewBag.mensaje1 = "El Cargo Ingresado ya existe.";
                            return View("Index", buscar);
                        }
                    }
                    db.Entry(empleadoOrg).State = EntityState.Modified;
                    db.SaveChanges();
                    ViewBag.mensaje = "Cargo Seleccionado Modificado Satisfactoriamente.";
                    List<EmpleadoOrg> busca = (from p in db.Tbl_EmpleadoOrg.AsNoTracking()
                                               join pe in db.Tbl_Organigrama on p.Fk_Id_Organigrama equals pe.Pk_Id_Organigrama
                                               where pe.Fk_Id_Empresa == pkidempresa
                                               select p).ToList();
                    return View("Index", busca);
                }
            }


            ViewBag.Fk_Id_Organigrama = new SelectList(db.Tbl_Organigrama.Where(o => o.Fk_Id_Empresa == usuarioActual.IdEmpresa), "Pk_Id_Organigrama", "Fk_Id_Organigrama", empleadoOrg.Fk_Id_Organigrama);
            ViewBag.Fk_Id_EmpleadoOrg = new SelectList(db.Tbl_EmpleadoOrg.Where(eo => eo.Organigrama.Fk_Id_Empresa == usuarioActual.IdEmpresa), "Id_EmpleadoOrg", "Cargo_Empleado", empleadoOrg.Fk_Id_EmpleadoOrg);

            return View(empleadoOrg);
        }

        // GET: EmpleadoOrg/Delete/5
        public ActionResult Delete(int? id)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmpleadoOrg empleadoOrg = db.Tbl_EmpleadoOrg.Find(id);
            if (empleadoOrg == null)
            {
                return HttpNotFound();
            }
            return View(empleadoOrg);
        }


        public ActionResult DeleteConfirmed(int id)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "Debe estar autenticado para continuar en el sistema.";
            }
            try
            {
                EmpleadoOrg empleadoOrg = db.Tbl_EmpleadoOrg.Find(id);
                db.Tbl_EmpleadoOrg.Remove(empleadoOrg);
                db.SaveChanges();

                int pkidempresa = usuarioActual.IdEmpresa;
                List<EmpleadoOrg> busca = (from p in db.Tbl_EmpleadoOrg
                                           join pe in db.Tbl_Organigrama on p.Fk_Id_Organigrama equals pe.Pk_Id_Organigrama
                                           where pe.Fk_Id_Empresa == pkidempresa
                                           select p).ToList();
                ViewBag.mensaje1 = "El Cargo Seleccionado se ha Eliminado. ";
                return View("Index", busca);

            }
            catch
            {
                EmpleadoOrg empleadoOrg = db.Tbl_EmpleadoOrg.Find(id);
                int pkidempresa = usuarioActual.IdEmpresa;
                List<EmpleadoOrg> busca = (from p in db.Tbl_EmpleadoOrg
                                           join pe in db.Tbl_Organigrama on p.Fk_Id_Organigrama equals pe.Pk_Id_Organigrama
                                           where pe.Fk_Id_Empresa == pkidempresa
                                           select p).ToList();
                ViewBag.mensaje1 = "El registro seleccionado no es permitido eliminarlo.";
                return View("Index", busca);
            }

        }

        protected override void Dispose(bool disposing)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
            }
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult AgregarEmpleadoOrg(EmpleadoOrg empleadoorg)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
            }
            bool respuestaguardado = EmpresaServicios.AgregarEmpleadoOrg(empleadoorg, usuarioActual.IdEmpresa);

            return RedirectToAction("Index", new { respuesta = respuestaguardado });

        }
    }
}
