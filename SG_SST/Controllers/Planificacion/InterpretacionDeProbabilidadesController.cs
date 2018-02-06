using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SG_SST.Models;
using SG_SST.Models.Planificacion;
using SG_SST.Controllers.Base;

namespace SG_SST.Controllers.Planificacion
{
    public class InterpretacionDeProbabilidadesController : BaseController
    {
        private SG_SSTContext db = new SG_SSTContext();
        

        // GET: InterpretacionDeProbabilidades
        public ActionResult Index()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                return View();
            }
            return View(db.Tbl_Interpretacion_De_Probabilidad.ToList());
        }

        // GET: InterpretacionDeProbabilidades/Details/5
        public ActionResult Details(int? id)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                return View();
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InterpretacionDeProbabilidad interpretacionDeProbabilidad = db.Tbl_Interpretacion_De_Probabilidad.Find(id);
            if (interpretacionDeProbabilidad == null)
            {
                return HttpNotFound();
            }
            return View(interpretacionDeProbabilidad);
        }

        // GET: InterpretacionDeProbabilidades/Create
        public ActionResult Create()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                return View();
            }
            return View();
        }

        // POST: InterpretacionDeProbabilidades/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PK_Interpretacion_De_Probabilidad,Nivel_Inferior,Nivel_Superior,Interpretacion")] InterpretacionDeProbabilidad interpretacionDeProbabilidad)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                return View();
            }
            if (ModelState.IsValid)
            {
                db.Tbl_Interpretacion_De_Probabilidad.Add(interpretacionDeProbabilidad);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(interpretacionDeProbabilidad);
        }

        // GET: InterpretacionDeProbabilidades/Edit/5
        public ActionResult Edit(int? id)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                return View();
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InterpretacionDeProbabilidad interpretacionDeProbabilidad = db.Tbl_Interpretacion_De_Probabilidad.Find(id);
            if (interpretacionDeProbabilidad == null)
            {
                return HttpNotFound();
            }
            return View(interpretacionDeProbabilidad);
        }

        // POST: InterpretacionDeProbabilidades/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PK_Interpretacion_De_Probabilidad,Nivel_Inferior,Nivel_Superior,Interpretacion")] InterpretacionDeProbabilidad interpretacionDeProbabilidad)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                return View();
            }
            if (ModelState.IsValid)
            {
                db.Entry(interpretacionDeProbabilidad).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(interpretacionDeProbabilidad);
        }

        // GET: InterpretacionDeProbabilidades/Delete/5
        public ActionResult Delete(int? id)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                return View();
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InterpretacionDeProbabilidad interpretacionDeProbabilidad = db.Tbl_Interpretacion_De_Probabilidad.Find(id);
            if (interpretacionDeProbabilidad == null)
            {
                return HttpNotFound();
            }
            return View(interpretacionDeProbabilidad);
        }

        // POST: InterpretacionDeProbabilidades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                return View();
            }
            InterpretacionDeProbabilidad interpretacionDeProbabilidad = db.Tbl_Interpretacion_De_Probabilidad.Find(id);
            db.Tbl_Interpretacion_De_Probabilidad.Remove(interpretacionDeProbabilidad);
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
