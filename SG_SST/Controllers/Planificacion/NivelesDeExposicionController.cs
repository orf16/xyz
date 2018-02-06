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
    public class NivelesDeExposicionController : BaseController
    {
        private SG_SSTContext db = new SG_SSTContext();
       

        // GET: NivelesDeExposicion
        public ActionResult Index()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                return View();
            }
            return View(db.Tbl_Nivel_De_Exposicion.ToList());
        }

        // GET: NivelesDeExposicion/Details/5
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
            NivelDeExposicion nivelDeExposicion = db.Tbl_Nivel_De_Exposicion.Find(id);
            if (nivelDeExposicion == null)
            {
                return HttpNotFound();
            }
            return View(nivelDeExposicion);
        }

        // GET: NivelesDeExposicion/Create
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

        // POST: NivelesDeExposicion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PK_Nivel_De_Exposicion,Valor_Exposicion,Descripcion_Exposicion")] NivelDeExposicion nivelDeExposicion)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                return View();
            }
            if (ModelState.IsValid)
            {
                db.Tbl_Nivel_De_Exposicion.Add(nivelDeExposicion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nivelDeExposicion);
        }

        // GET: NivelesDeExposicion/Edit/5
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
            NivelDeExposicion nivelDeExposicion = db.Tbl_Nivel_De_Exposicion.Find(id);
            if (nivelDeExposicion == null)
            {
                return HttpNotFound();
            }
            return View(nivelDeExposicion);
        }

        // POST: NivelesDeExposicion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PK_Nivel_De_Exposicion,Valor_Exposicion,Descripcion_Exposicion")] NivelDeExposicion nivelDeExposicion)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                return View();
            }
            if (ModelState.IsValid)
            {
                db.Entry(nivelDeExposicion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nivelDeExposicion);
        }

        // GET: NivelesDeExposicion/Delete/5
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
            NivelDeExposicion nivelDeExposicion = db.Tbl_Nivel_De_Exposicion.Find(id);
            if (nivelDeExposicion == null)
            {
                return HttpNotFound();
            }
            return View(nivelDeExposicion);
        }

        // POST: NivelesDeExposicion/Delete/5
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
            NivelDeExposicion nivelDeExposicion = db.Tbl_Nivel_De_Exposicion.Find(id);
            db.Tbl_Nivel_De_Exposicion.Remove(nivelDeExposicion);
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
