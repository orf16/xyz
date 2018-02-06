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
    public class GruposController : BaseController
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

        // GET: Grupos
        public ActionResult Index()
        {
            var grupoes = db.Tbl_Grupos.Include(g => g.Metodologia);
            return View(grupoes.ToList());
        }

        // GET: Grupos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grupo grupo = db.Tbl_Grupos.Find(id);
            if (grupo == null)
            {
                return HttpNotFound();
            }
            return View(grupo);
        }

        // GET: Grupos/Create
        public ActionResult Create()
        {
            ViewBag.FK_Metodologia = new SelectList(db.Tbl_Metodologia, "PK_Metodologia", "Descripcion_Metodologia");
            return View();
        }

        // POST: Grupos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PK_Grupo,Descripcion_Grupo,FK_Metodologia")] Grupo grupo)
        {
            if (ModelState.IsValid)
            {
                db.Tbl_Grupos.Add(grupo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FK_Metodologia = new SelectList(db.Tbl_Metodologia, "PK_Metodologia", "Descripcion_Metodologia", grupo.FK_Metodologia);
            return View(grupo);
        }

        // GET: Grupos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grupo grupo = db.Tbl_Grupos.Find(id);
            if (grupo == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_Metodologia = new SelectList(db.Tbl_Metodologia, "PK_Metodologia", "Descripcion_Metodologia", grupo.FK_Metodologia);
            return View(grupo);
        }

        // POST: Grupos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PK_Grupo,Descripcion_Grupo,FK_Metodologia")] Grupo grupo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(grupo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FK_Metodologia = new SelectList(db.Tbl_Metodologia, "PK_Metodologia", "Descripcion_Metodologia", grupo.FK_Metodologia);
            return View(grupo);
        }

        // GET: Grupos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grupo grupo = db.Tbl_Grupos.Find(id);
            if (grupo == null)
            {
                return HttpNotFound();
            }
            return View(grupo);
        }

        // POST: Grupos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Grupo grupo = db.Tbl_Grupos.Find(id);
            db.Tbl_Grupos.Remove(grupo);
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
