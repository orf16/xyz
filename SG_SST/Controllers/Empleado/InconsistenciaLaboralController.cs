using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SG_SST.Models;
using SG_SST.Models.Empleado;
using SG_SST.Controllers.Base;

namespace SG_SST.Controllers.Empleado
{
    public class InconsistenciaLaboralController : BaseController
    {
        private SG_SSTContext db = new SG_SSTContext();

        // GET: Inconsistecialaboral
        public ActionResult Index()
        {
            var tbl_InconsistenciasLaborales = db.Tbl_InconsistenciasLaborales.Include(i => i.TipoInconsistencia);
            return View(tbl_InconsistenciasLaborales.ToList());
        }

        // GET: Inconsistecialaboral/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inconsistecialaboral inconsistecialaboral = db.Tbl_InconsistenciasLaborales.Find(id);
            if (inconsistecialaboral == null)
            {
                return HttpNotFound();
            }
            return View(inconsistecialaboral);
        }

        // GET: Inconsistecialaboral/Create
        public ActionResult Create()
        {
            ViewBag.FKTipoInconsistencia = new SelectList(db.Tbl_TipoInconsistenciaLaboral, "PKTipoInconsistencia", "DescripcionTipInc");
            return View();
        }

        // POST: Inconsistecialaboral/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PKInconsistencia,FKTipoInconsistencia,DescripcionInconsistencia")] Inconsistecialaboral inconsistecialaboral)
        {
            if (ModelState.IsValid)
            {
                db.Tbl_InconsistenciasLaborales.Add(inconsistecialaboral);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FKTipoInconsistencia = new SelectList(db.Tbl_TipoInconsistenciaLaboral, "PKTipoInconsistencia", "DescripcionTipInc", inconsistecialaboral.FKTipoInconsistencia);
            return View(inconsistecialaboral);
        }

        // GET: Inconsistecialaboral/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inconsistecialaboral inconsistecialaboral = db.Tbl_InconsistenciasLaborales.Find(id);
            if (inconsistecialaboral == null)
            {
                return HttpNotFound();
            }
            ViewBag.FKTipoInconsistencia = new SelectList(db.Tbl_TipoInconsistenciaLaboral, "PKTipoInconsistencia", "DescripcionTipInc", inconsistecialaboral.FKTipoInconsistencia);
            return View(inconsistecialaboral);
        }

        // POST: Inconsistecialaboral/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PKInconsistencia,FKTipoInconsistencia,DescripcionInconsistencia")] Inconsistecialaboral inconsistecialaboral)
        {
            if (ModelState.IsValid)
            {
                db.Entry(inconsistecialaboral).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FKTipoInconsistencia = new SelectList(db.Tbl_TipoInconsistenciaLaboral, "PKTipoInconsistencia", "DescripcionTipInc", inconsistecialaboral.FKTipoInconsistencia);
            return View(inconsistecialaboral);
        }

        // GET: Inconsistecialaboral/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inconsistecialaboral inconsistecialaboral = db.Tbl_InconsistenciasLaborales.Find(id);
            if (inconsistecialaboral == null)
            {
                return HttpNotFound();
            }
            return View(inconsistecialaboral);
        }

        // POST: Inconsistecialaboral/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Inconsistecialaboral inconsistecialaboral = db.Tbl_InconsistenciasLaborales.Find(id);
            db.Tbl_InconsistenciasLaborales.Remove(inconsistecialaboral);
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
