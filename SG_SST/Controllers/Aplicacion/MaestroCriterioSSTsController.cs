using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SG_SST.Models;
using SG_SST.Models.Aplicacion;

namespace SG_SST.Controllers.Aplicacion
{
    public class MaestroCriterioSSTsController : Controller
    {
        private SG_SSTContext db = new SG_SSTContext();

        // GET: MaestroCriterioSSTs
        public ActionResult Index()
        {
            return View(db.Tbl_CriterioSST.ToList());
        }

        // GET: MaestroCriterioSSTs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CriterioSST criterioSST = db.Tbl_CriterioSST.Find(id);
            if (criterioSST == null)
            {
                return HttpNotFound();
            }
            return View(criterioSST);
        }

        // GET: MaestroCriterioSSTs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MaestroCriterioSSTs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PK_CriterioSST,Criterio")] CriterioSST criterioSST)
        {
            if (ModelState.IsValid)
            {
                db.Tbl_CriterioSST.Add(criterioSST);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(criterioSST);
        }

        // GET: MaestroCriterioSSTs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CriterioSST criterioSST = db.Tbl_CriterioSST.Find(id);
            if (criterioSST == null)
            {
                return HttpNotFound();
            }
            return View(criterioSST);
        }

        // POST: MaestroCriterioSSTs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PK_CriterioSST,Criterio")] CriterioSST criterioSST)
        {
            if (ModelState.IsValid)
            {
                db.Entry(criterioSST).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(criterioSST);
        }

        // GET: MaestroCriterioSSTs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CriterioSST criterioSST = db.Tbl_CriterioSST.Find(id);
            if (criterioSST == null)
            {
                return HttpNotFound();
            }
            return View(criterioSST);
        }

        // POST: MaestroCriterioSSTs/Delete/5     
        public ActionResult DeleteConfirmed(int id)
        {
            CriterioSST criterioSST = db.Tbl_CriterioSST.Find(id);
            db.Tbl_CriterioSST.Remove(criterioSST);
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
