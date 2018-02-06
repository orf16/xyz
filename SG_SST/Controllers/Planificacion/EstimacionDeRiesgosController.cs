
namespace SG_SST.Controllers.Planificacion
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
    using SG_SST.Models.Planificacion;
    using SG_SST.Controllers.Base;

    public class EstimacionDeRiesgosController : BaseController
    {
        private SG_SSTContext db = new SG_SSTContext();
       

        // GET: EstimacionDeRiesgos
        public ActionResult Index()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                return View();
            }
            var tbl_Estimacion_De_Riesgo = db.Tbl_Estimacion_De_Riesgo.Include(e => e.Consecuencia).Include(e => e.Probabilidad);
            return View(tbl_Estimacion_De_Riesgo.ToList());
        }

        // GET: EstimacionDeRiesgos/Details/5
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
            EstimacionDeRiesgo estimacionDeRiesgo = db.Tbl_Estimacion_De_Riesgo.Find(id);
            if (estimacionDeRiesgo == null)
            {
                return HttpNotFound();
            }
            return View(estimacionDeRiesgo);
        }

        // GET: EstimacionDeRiesgos/Create
        public ActionResult Create()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                return View();
            }
            ViewBag.FK_Consecuencia = new SelectList(db.Tbl_Consecuencias, "PK_Consecuencia", "Descripcion_Consecuencia");
            ViewBag.FK_Probabilidad = new SelectList(db.Tbl_Probabilidades, "PK_Probabilidad", "Descripcion_Probabilidad");
            return View();
        }

        // POST: EstimacionDeRiesgos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PK_Estimacion_De_Riesgo,FK_Probabilidad,FK_Consecuencia,RiesgoNoAceptable,Detalle_Estimacion,ValorDelRiesgo")] EstimacionDeRiesgo estimacionDeRiesgo)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                return View();
            }
            if (ModelState.IsValid)
            {
                db.Tbl_Estimacion_De_Riesgo.Add(estimacionDeRiesgo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FK_Consecuencia = new SelectList(db.Tbl_Consecuencias, "PK_Consecuencia", "Descripcion_Consecuencia", estimacionDeRiesgo.FK_Consecuencia);
            ViewBag.FK_Probabilidad = new SelectList(db.Tbl_Probabilidades, "PK_Probabilidad", "Descripcion_Probabilidad", estimacionDeRiesgo.FK_Probabilidad);
            return View(estimacionDeRiesgo);
        }

        // GET: EstimacionDeRiesgos/Edit/5
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
            EstimacionDeRiesgo estimacionDeRiesgo = db.Tbl_Estimacion_De_Riesgo.Find(id);
            if (estimacionDeRiesgo == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_Consecuencia = new SelectList(db.Tbl_Consecuencias, "PK_Consecuencia", "Descripcion_Consecuencia", estimacionDeRiesgo.FK_Consecuencia);
            ViewBag.FK_Probabilidad = new SelectList(db.Tbl_Probabilidades, "PK_Probabilidad", "Descripcion_Probabilidad", estimacionDeRiesgo.FK_Probabilidad);
            return View(estimacionDeRiesgo);
        }

        // POST: EstimacionDeRiesgos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PK_Estimacion_De_Riesgo,FK_Probabilidad,FK_Consecuencia,RiesgoNoAceptable,Detalle_Estimacion,ValorDelRiesgo")] EstimacionDeRiesgo estimacionDeRiesgo)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                return View();
            }
            if (ModelState.IsValid)
            {
                db.Entry(estimacionDeRiesgo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FK_Consecuencia = new SelectList(db.Tbl_Consecuencias, "PK_Consecuencia", "Descripcion_Consecuencia", estimacionDeRiesgo.FK_Consecuencia);
            ViewBag.FK_Probabilidad = new SelectList(db.Tbl_Probabilidades, "PK_Probabilidad", "Descripcion_Probabilidad", estimacionDeRiesgo.FK_Probabilidad);
            return View(estimacionDeRiesgo);
        }

        // GET: EstimacionDeRiesgos/Delete/5
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
            EstimacionDeRiesgo estimacionDeRiesgo = db.Tbl_Estimacion_De_Riesgo.Find(id);
            if (estimacionDeRiesgo == null)
            {
                return HttpNotFound();
            }
            return View(estimacionDeRiesgo);
        }

        // POST: EstimacionDeRiesgos/Delete/5
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
            EstimacionDeRiesgo estimacionDeRiesgo = db.Tbl_Estimacion_De_Riesgo.Find(id);
            db.Tbl_Estimacion_De_Riesgo.Remove(estimacionDeRiesgo);
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
