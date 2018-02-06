
namespace SG_SST.Controllers
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
    public class CIUController : BaseController
    {
        private SG_SSTContext db = new SG_SSTContext();

        // GET: CIUs
        public ActionResult Index()
        {
            return View(db.Tbl_CIU.ToList());
        }

        // GET: CIUs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CIU cIU = db.Tbl_CIU.Find(id);
            if (cIU == null)
            {
                return HttpNotFound();
            }
            return View(cIU);
        }

        // GET: CIUs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CIUs/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Pk_Id_CIU,Codigo_CIU,Descripcion")] CIU cIU)
        {
            if (ModelState.IsValid)
            {
                db.Tbl_CIU.Add(cIU);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cIU);
        }

        // GET: CIUs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CIU cIU = db.Tbl_CIU.Find(id);
            if (cIU == null)
            {
                return HttpNotFound();
            }
            return View(cIU);
        }

        // POST: CIUs/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Pk_Id_CIU,Codigo_CIU,Descripcion")] CIU cIU)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cIU).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cIU);
        }

        // GET: CIUs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CIU cIU = db.Tbl_CIU.Find(id);
            if (cIU == null)
            {
                return HttpNotFound();
            }
            return View(cIU);
        }

        // POST: CIUs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CIU cIU = db.Tbl_CIU.Find(id);
            db.Tbl_CIU.Remove(cIU);
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
