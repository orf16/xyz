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
    public class InterpretacionNivelDeRiesgosController : BaseController
    {
        private SG_SSTContext db = new SG_SSTContext();
       
        // GET: InterpretacionNivelDeRiesgoes
        public ActionResult Index()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                return View();
            }
            return View(db.Tbl_Interpretacion_Nivel_Riesgo.ToList());
        }

        // GET: InterpretacionNivelDeRiesgoes/Details/5
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
            InterpretacionNivelDeRiesgo interpretacionNivelDeRiesgo = db.Tbl_Interpretacion_Nivel_Riesgo.Find(id);
            if (interpretacionNivelDeRiesgo == null)
            {
                return HttpNotFound();
            }
            return View(interpretacionNivelDeRiesgo);
        }

        // GET: InterpretacionNivelDeRiesgoes/Create
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

        // POST: InterpretacionNivelDeRiesgoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PK_Interpretacion_Nivel_Riesgo,Nivel_Inferior,Nivel_Superior,Resultado,Interpretacion")] InterpretacionNivelDeRiesgo interpretacionNivelDeRiesgo)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                return View();
            }
            if (ModelState.IsValid)
            {
                db.Tbl_Interpretacion_Nivel_Riesgo.Add(interpretacionNivelDeRiesgo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(interpretacionNivelDeRiesgo);
        }

        // GET: InterpretacionNivelDeRiesgoes/Edit/5
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
            InterpretacionNivelDeRiesgo interpretacionNivelDeRiesgo = db.Tbl_Interpretacion_Nivel_Riesgo.Find(id);
            if (interpretacionNivelDeRiesgo == null)
            {
                return HttpNotFound();
            }
            return View(interpretacionNivelDeRiesgo);
        }

        // POST: InterpretacionNivelDeRiesgoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PK_Interpretacion_Nivel_Riesgo,Nivel_Inferior,Nivel_Superior,Resultado,Interpretacion")] InterpretacionNivelDeRiesgo interpretacionNivelDeRiesgo)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                return View();
            }
            if (ModelState.IsValid)
            {
                db.Entry(interpretacionNivelDeRiesgo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(interpretacionNivelDeRiesgo);
        }

        // GET: InterpretacionNivelDeRiesgoes/Delete/5
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
            InterpretacionNivelDeRiesgo interpretacionNivelDeRiesgo = db.Tbl_Interpretacion_Nivel_Riesgo.Find(id);
            if (interpretacionNivelDeRiesgo == null)
            {
                return HttpNotFound();
            }
            return View(interpretacionNivelDeRiesgo);
        }

        // POST: InterpretacionNivelDeRiesgoes/Delete/5
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
            InterpretacionNivelDeRiesgo interpretacionNivelDeRiesgo = db.Tbl_Interpretacion_Nivel_Riesgo.Find(id);
            db.Tbl_Interpretacion_Nivel_Riesgo.Remove(interpretacionNivelDeRiesgo);
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
