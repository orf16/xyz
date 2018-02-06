
namespace SG_SST.Controllers.Empresas
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
    using SG_SST.Models.Empresas;
    using SG_SST.Controllers.Base;
    public class TipoAnalisisController : BaseController
    {
        private SG_SSTContext db = new SG_SSTContext();

        // GET: TipoAnalisis
        public ActionResult Index()
        {
            return View(db.Tbl_Tipo_Analisis.ToList());
        }

        // GET: TipoAnalisis/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoAnalisis tipoAnalisis = db.Tbl_Tipo_Analisis.Find(id);
            if (tipoAnalisis == null)
            {
                return HttpNotFound();
            }
            return View(tipoAnalisis);
        }

        // GET: TipoAnalisis/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoAnalisis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PK_Tipo_Analisis,Descripcion")] TipoAnalisis tipoAnalisis)
        {
            if (ModelState.IsValid)
            {
                db.Tbl_Tipo_Analisis.Add(tipoAnalisis);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipoAnalisis);
        }

        // GET: TipoAnalisis/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoAnalisis tipoAnalisis = db.Tbl_Tipo_Analisis.Find(id);
            if (tipoAnalisis == null)
            {
                return HttpNotFound();
            }
            return View(tipoAnalisis);
        }

        // POST: TipoAnalisis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PK_Tipo_Analisis,Descripcion")] TipoAnalisis tipoAnalisis)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoAnalisis).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipoAnalisis);
        }

        // GET: TipoAnalisis/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoAnalisis tipoAnalisis = db.Tbl_Tipo_Analisis.Find(id);
            if (tipoAnalisis == null)
            {
                return HttpNotFound();
            }
            return View(tipoAnalisis);
        }

        // POST: TipoAnalisis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoAnalisis tipoAnalisis = db.Tbl_Tipo_Analisis.Find(id);
            db.Tbl_Tipo_Analisis.Remove(tipoAnalisis);
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
