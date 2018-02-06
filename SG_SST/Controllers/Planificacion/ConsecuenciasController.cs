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
    public class ConsecuenciasController : BaseController
    {
        private SG_SSTContext db = new SG_SSTContext();

       
        // GET: Consecuencias
        public ActionResult Index()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                return View();
            }
            var consecuencias = db.Tbl_Consecuencias.Include(c => c.Grupo);
            return View(consecuencias.ToList());
        }

        // GET: Consecuencias/Details/5
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
            Consecuencia consecuencia = db.Tbl_Consecuencias.Find(id);
            if (consecuencia == null)
            {
                return HttpNotFound();
            }
            return View(consecuencia);
        }

        // GET: Consecuencias/Create
        public ActionResult Create()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                return View();
            }
            ViewBag.FK_Grupo = new SelectList(db.Tbl_Grupos, "PK_Grupo", "Descripcion_Grupo");
            return View();
        }

        // POST: Consecuencias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PK_Consecuencia,Valor_Consecuencia,Descripcion_Consecuencia,FK_Grupo")] Consecuencia consecuencia)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                return View();
            }
            if (ModelState.IsValid)
            {
                db.Tbl_Consecuencias.Add(consecuencia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FK_Grupo = new SelectList(db.Tbl_Grupos, "PK_Grupo", "Descripcion_Grupo", consecuencia.FK_Grupo);
            return View(consecuencia);
        }

        // GET: Consecuencias/Edit/5
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
            Consecuencia consecuencia = db.Tbl_Consecuencias.Find(id);
            if (consecuencia == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_Grupo = new SelectList(db.Tbl_Grupos, "PK_Grupo", "Descripcion_Grupo", consecuencia.FK_Grupo);
            return View(consecuencia);
        }

        // POST: Consecuencias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PK_Consecuencia,Valor_Consecuencia,Descripcion_Consecuencia,FK_Grupo")] Consecuencia consecuencia)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                return View();
            }
            if (ModelState.IsValid)
            {
                db.Entry(consecuencia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FK_Grupo = new SelectList(db.Tbl_Grupos, "PK_Grupo", "Descripcion_Grupo", consecuencia.FK_Grupo);
            return View(consecuencia);
        }

        // GET: Consecuencias/Delete/5
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
            Consecuencia consecuencia = db.Tbl_Consecuencias.Find(id);
            if (consecuencia == null)
            {
                return HttpNotFound();
            }
            return View(consecuencia);
        }

        // POST: Consecuencias/Delete/5
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
            Consecuencia consecuencia = db.Tbl_Consecuencias.Find(id);
            db.Tbl_Consecuencias.Remove(consecuencia);
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
