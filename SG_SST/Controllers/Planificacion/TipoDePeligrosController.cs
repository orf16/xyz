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
    public class TipoDePeligrosController : BaseController
    {
        private SG_SSTContext db = new SG_SSTContext();

        public SG_SST.Models.SG_SSTContext SG_SSTContext
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        // GET: TipoDePeligros
        public ActionResult Index()
        {
            return View(db.Tbl_Tipo_De_Peligro.ToList());
        }

        // GET: TipoDePeligros/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoDePeligro tipoDePeligro = db.Tbl_Tipo_De_Peligro.Find(id);
            if (tipoDePeligro == null)
            {
                return HttpNotFound();
            }
            return View(tipoDePeligro);
        }

        // GET: TipoDePeligros/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoDePeligros/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PK_Tipo_De_Peligro,Descripcion_Del_Peligro")] TipoDePeligro tipoDePeligro)
        {
            if (ModelState.IsValid)
            {
                db.Tbl_Tipo_De_Peligro.Add(tipoDePeligro);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipoDePeligro);
        }

        // GET: TipoDePeligros/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoDePeligro tipoDePeligro = db.Tbl_Tipo_De_Peligro.Find(id);
            if (tipoDePeligro == null)
            {
                return HttpNotFound();
            }
            return View(tipoDePeligro);
        }

        // POST: TipoDePeligros/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PK_Tipo_De_Peligro,Descripcion_Del_Peligro")] TipoDePeligro tipoDePeligro)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoDePeligro).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipoDePeligro);
        }

        // GET: TipoDePeligros/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoDePeligro tipoDePeligro = db.Tbl_Tipo_De_Peligro.Find(id);
            if (tipoDePeligro == null)
            {
                return HttpNotFound();
            }
            return View(tipoDePeligro);
        }

        // POST: TipoDePeligros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoDePeligro tipoDePeligro = db.Tbl_Tipo_De_Peligro.Find(id);
            db.Tbl_Tipo_De_Peligro.Remove(tipoDePeligro);
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
