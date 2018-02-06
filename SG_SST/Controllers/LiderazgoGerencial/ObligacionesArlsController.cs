using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SG_SST.Models;
using SG_SST.Models.LiderazgoGerencial;
using SG_SST.Controllers.Base;

namespace SG_SST.Controllers.LiderazgoGerencial
{
    public class ObligacionesArlsController : BaseController
    {
        private SG_SSTContext db = new SG_SSTContext();

        // GET: ObligacionesArls
        public ActionResult Index()
        {
            return View(db.Tbl_Obligaciones_Arl.ToList());
        }

        // GET: ObligacionesArls/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ObligacionesArl obligacionesArl = db.Tbl_Obligaciones_Arl.Find(id);
            if (obligacionesArl == null)
            {
                return HttpNotFound();
            }
            return View(obligacionesArl);
        }

        // GET: ObligacionesArls/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ObligacionesArls/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Pk_Id_Obligaciones_Arl,Descripcion")] ObligacionesArl obligacionesArl)
        {
            if (ModelState.IsValid)
            {
                db.Tbl_Obligaciones_Arl.Add(obligacionesArl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(obligacionesArl);
        }

        // GET: ObligacionesArls/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ObligacionesArl obligacionesArl = db.Tbl_Obligaciones_Arl.Find(id);
            if (obligacionesArl == null)
            {
                return HttpNotFound();
            }
            return View(obligacionesArl);
        }

        // POST: ObligacionesArls/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Pk_Id_Obligaciones_Arl,Descripcion")] ObligacionesArl obligacionesArl)
        {
            if (ModelState.IsValid)
            {
                db.Entry(obligacionesArl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obligacionesArl);
        }

        // GET: ObligacionesArls/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ObligacionesArl obligacionesArl = db.Tbl_Obligaciones_Arl.Find(id);
            if (obligacionesArl == null)
            {
                return HttpNotFound();
            }
            return View(obligacionesArl);
        }

        // POST: ObligacionesArls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ObligacionesArl obligacionesArl = db.Tbl_Obligaciones_Arl.Find(id);
            db.Tbl_Obligaciones_Arl.Remove(obligacionesArl);
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
