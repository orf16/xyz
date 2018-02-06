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
using SG_SST.Controllers.Base;

namespace SG_SST.Controllers.Empresas
{
    public class TipoElementoAnalisisController : BaseController
    {
        private SG_SSTContext db = new SG_SSTContext();

        // GET: TipoElementoAnalisis
        public ActionResult Index()
        {
            var tbl_Tipo_Elemento_Analisis = db.Tbl_Tipo_Elemento_Analisis.Include(t => t.TipoAnalisis);
            return View(tbl_Tipo_Elemento_Analisis.ToList());
        }

        // GET: TipoElementoAnalisis/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoElementoAnalisis tipoElementoAnalisis = db.Tbl_Tipo_Elemento_Analisis.Find(id);
            if (tipoElementoAnalisis == null)
            {
                return HttpNotFound();
            }
            return View(tipoElementoAnalisis);
        }

        // GET: TipoElementoAnalisis/Create
        public ActionResult Create()
        {
            ViewBag.FK_Tipo_Analisis = new SelectList(db.Tbl_Tipo_Analisis, "PK_Tipo_Analisis", "Descripcion");
            return View();
        }

        // POST: TipoElementoAnalisis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PK_Tipo_Elemneto_Analisis,Descripcion_Elemento,FK_Tipo_Analisis")] TipoElementoAnalisis tipoElementoAnalisis)
        {
            if (ModelState.IsValid)
            {
                db.Tbl_Tipo_Elemento_Analisis.Add(tipoElementoAnalisis);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FK_Tipo_Analisis = new SelectList(db.Tbl_Tipo_Analisis, "PK_Tipo_Analisis", "Descripcion", tipoElementoAnalisis.FK_Tipo_Analisis);
            return View(tipoElementoAnalisis);
        }

        // GET: TipoElementoAnalisis/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoElementoAnalisis tipoElementoAnalisis = db.Tbl_Tipo_Elemento_Analisis.Find(id);
            if (tipoElementoAnalisis == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_Tipo_Analisis = new SelectList(db.Tbl_Tipo_Analisis, "PK_Tipo_Analisis", "Descripcion", tipoElementoAnalisis.FK_Tipo_Analisis);
            return View(tipoElementoAnalisis);
        }

        // POST: TipoElementoAnalisis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PK_Tipo_Elemneto_Analisis,Descripcion_Elemento,FK_Tipo_Analisis")] TipoElementoAnalisis tipoElementoAnalisis)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoElementoAnalisis).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FK_Tipo_Analisis = new SelectList(db.Tbl_Tipo_Analisis, "PK_Tipo_Analisis", "Descripcion", tipoElementoAnalisis.FK_Tipo_Analisis);
            return View(tipoElementoAnalisis);
        }

        // GET: TipoElementoAnalisis/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoElementoAnalisis tipoElementoAnalisis = db.Tbl_Tipo_Elemento_Analisis.Find(id);
            if (tipoElementoAnalisis == null)
            {
                return HttpNotFound();
            }
            return View(tipoElementoAnalisis);
        }

        // POST: TipoElementoAnalisis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoElementoAnalisis tipoElementoAnalisis = db.Tbl_Tipo_Elemento_Analisis.Find(id);
            db.Tbl_Tipo_Elemento_Analisis.Remove(tipoElementoAnalisis);
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
