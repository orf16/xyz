
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

    public class ProbabilidadesController : BaseController
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

        // GET: Probabilidades
        public ActionResult Index()
        {
            var tbl_Probabilidades = db.Tbl_Probabilidades.Include(p => p.Metodologia);
            return View(tbl_Probabilidades.ToList());
        }

        // GET: Probabilidades/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Probabilidad probabilidad = db.Tbl_Probabilidades.Find(id);
            if (probabilidad == null)
            {
                return HttpNotFound();
            }
            return View(probabilidad);
        }

        // GET: Probabilidades/Create
        public ActionResult Create()
        {
            ViewBag.FK_Metodologia = new SelectList(db.Tbl_Metodologia, "PK_Metodologia", "Descripcion_Metodologia");
            return View();
        }

        // POST: Probabilidades/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PK_Probabilidad,Descripcion_Probabilidad,FK_Metodologia")] Probabilidad probabilidad)
        {
            if (ModelState.IsValid)
            {
                db.Tbl_Probabilidades.Add(probabilidad);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FK_Metodologia = new SelectList(db.Tbl_Metodologia, "PK_Metodologia", "Descripcion_Metodologia", probabilidad.FK_Metodologia);
            return View(probabilidad);
        }

        // GET: Probabilidades/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Probabilidad probabilidad = db.Tbl_Probabilidades.Find(id);
            if (probabilidad == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_Metodologia = new SelectList(db.Tbl_Metodologia, "PK_Metodologia", "Descripcion_Metodologia", probabilidad.FK_Metodologia);
            return View(probabilidad);
        }

        // POST: Probabilidades/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PK_Probabilidad,Descripcion_Probabilidad,FK_Metodologia")] Probabilidad probabilidad)
        {
            if (ModelState.IsValid)
            {
                db.Entry(probabilidad).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FK_Metodologia = new SelectList(db.Tbl_Metodologia, "PK_Metodologia", "Descripcion_Metodologia", probabilidad.FK_Metodologia);
            return View(probabilidad);
        }

        // GET: Probabilidades/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Probabilidad probabilidad = db.Tbl_Probabilidades.Find(id);
            if (probabilidad == null)
            {
                return HttpNotFound();
            }
            return View(probabilidad);
        }

        // POST: Probabilidades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Probabilidad probabilidad = db.Tbl_Probabilidades.Find(id);
            db.Tbl_Probabilidades.Remove(probabilidad);
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
