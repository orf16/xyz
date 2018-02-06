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
    public class TipoMetodologiasController : BaseController
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

        // GET: TipoMetodologias
        public ActionResult Index()
        {
            return View(db.Tbl_Metodologia.ToList());
        }

        // GET: TipoMetodologias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoMetodologia tipoMetodologia = db.Tbl_Metodologia.Find(id);
            if (tipoMetodologia == null)
            {
                return HttpNotFound();
            }
            return View(tipoMetodologia);
        }

        // GET: TipoMetodologias/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoMetodologias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PK_Metodologia,Descripcion_Metodologia")] TipoMetodologia tipoMetodologia)
        {
            if (ModelState.IsValid)
            {
                db.Tbl_Metodologia.Add(tipoMetodologia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipoMetodologia);
        }

        // GET: TipoMetodologias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoMetodologia tipoMetodologia = db.Tbl_Metodologia.Find(id);
            if (tipoMetodologia == null)
            {
                return HttpNotFound();
            }
            return View(tipoMetodologia);
        }

        // POST: TipoMetodologias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PK_Metodologia,Descripcion_Metodologia")] TipoMetodologia tipoMetodologia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoMetodologia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipoMetodologia);
        }

        // GET: TipoMetodologias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoMetodologia tipoMetodologia = db.Tbl_Metodologia.Find(id);
            if (tipoMetodologia == null)
            {
                return HttpNotFound();
            }
            return View(tipoMetodologia);
        }

        // POST: TipoMetodologias/Delete/5   
        public ActionResult DeleteConfirmed(int id)
        {
            TipoMetodologia tipoMetodologia = db.Tbl_Metodologia.Find(id);
            db.Tbl_Metodologia.Remove(tipoMetodologia);
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
