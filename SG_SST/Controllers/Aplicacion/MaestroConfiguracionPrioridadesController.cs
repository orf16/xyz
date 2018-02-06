using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SG_SST.Models;
using SG_SST.Models.Aplicacion;
using SG_SST.Controllers.Base;

namespace SG_SST.Controllers.Aplicacion
{
    public class MaestroConfiguracionPrioridadesController : BaseController
    {
        private SG_SSTContext db = new SG_SSTContext();

        // GET: MaestroConfiguracionPrioridades
        public ActionResult Index()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "Debe iniciar Session para Continuar.";
                return View();
            }

            return View(db.Tbl_Maestro_Configuracion_Prioridad.ToList());
        }

        // GET: MaestroConfiguracionPrioridades/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MaestroConfiguracionPrioridades maestroConfiguracionPrioridades = db.Tbl_Maestro_Configuracion_Prioridad.Find(id);
            if (maestroConfiguracionPrioridades == null)
            {
                return HttpNotFound();
            }
            return View(maestroConfiguracionPrioridades);
        }

        // GET: MaestroConfiguracionPrioridades/Create
        public ActionResult Create()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "Debe iniciar Session para Continuar.";
                return View();
            }

            return View();
        }

        // POST: MaestroConfiguracionPrioridades/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Pk_Id_MaestroConfiguracion,DescripcionPrioridad,DiasDesde,DiasHasta")] MaestroConfiguracionPrioridades maestroConfiguracionPrioridades)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "Debe iniciar Session para Continuar.";
                return View();
            }

            if (ModelState.IsValid)
            {
                db.Tbl_Maestro_Configuracion_Prioridad.Add(maestroConfiguracionPrioridades);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(maestroConfiguracionPrioridades);
        }

        // GET: MaestroConfiguracionPrioridades/Edit/5
        public ActionResult Edit(int? id)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "Debe iniciar Session para Continuar.";
                return View();
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MaestroConfiguracionPrioridades maestroConfiguracionPrioridades = db.Tbl_Maestro_Configuracion_Prioridad.Find(id);
            if (maestroConfiguracionPrioridades == null)
            {
                return HttpNotFound();
            }
            return View(maestroConfiguracionPrioridades);
        }

        // POST: MaestroConfiguracionPrioridades/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Pk_Id_MaestroConfiguracion,DescripcionPrioridad,DiasDesde,DiasHasta")] MaestroConfiguracionPrioridades maestroConfiguracionPrioridades)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "Debe iniciar Session para Continuar.";
                return View();
            }
            if (ModelState.IsValid)
            {
                db.Entry(maestroConfiguracionPrioridades).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(maestroConfiguracionPrioridades);
        }

        // GET: MaestroConfiguracionPrioridades/Delete/5
        public ActionResult Delete(int? id)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "Debe iniciar Session para Continuar.";
                return View();
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MaestroConfiguracionPrioridades maestroConfiguracionPrioridades = db.Tbl_Maestro_Configuracion_Prioridad.Find(id);
            if (maestroConfiguracionPrioridades == null)
            {
                return HttpNotFound();
            }
            return View(maestroConfiguracionPrioridades);
        }

        // POST: MaestroConfiguracionPrioridades/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "Debe iniciar Session para Continuar.";
                return View();
            }

            MaestroConfiguracionPrioridades maestroConfiguracionPrioridades = db.Tbl_Maestro_Configuracion_Prioridad.Find(id);
            db.Tbl_Maestro_Configuracion_Prioridad.Remove(maestroConfiguracionPrioridades);
            db.SaveChanges();
            return RedirectToAction("Index");
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
