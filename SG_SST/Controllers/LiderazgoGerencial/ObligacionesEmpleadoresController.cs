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
    public class ObligacionesEmpleadoresController : BaseController
    {
        private SG_SSTContext db = new SG_SSTContext();

        // GET: ObligacionesEmpleadores
        public ActionResult Index()
        {
            return View(db.Tbl_Obligaciones_Empleadores.ToList());
        }

        // GET: ObligacionesEmpleadores/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ObligacionesEmpleadores obligacionesEmpleadores = db.Tbl_Obligaciones_Empleadores.Find(id);
            if (obligacionesEmpleadores == null)
            {
                return HttpNotFound();
            }
            return View(obligacionesEmpleadores);
        }

        // GET: ObligacionesEmpleadores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ObligacionesEmpleadores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Pk_Id_Obligaciones_Empleadores,Descripcion")] ObligacionesEmpleadores obligacionesEmpleadores)
        {
            if (ModelState.IsValid)
            {
                db.Tbl_Obligaciones_Empleadores.Add(obligacionesEmpleadores);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(obligacionesEmpleadores);
        }

        // GET: ObligacionesEmpleadores/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ObligacionesEmpleadores obligacionesEmpleadores = db.Tbl_Obligaciones_Empleadores.Find(id);
            if (obligacionesEmpleadores == null)
            {
                return HttpNotFound();
            }
            return View(obligacionesEmpleadores);
        }

        // POST: ObligacionesEmpleadores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Pk_Id_Obligaciones_Empleadores,Descripcion")] ObligacionesEmpleadores obligacionesEmpleadores)
        {
            if (ModelState.IsValid)
            {
                db.Entry(obligacionesEmpleadores).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obligacionesEmpleadores);
        }

        // GET: ObligacionesEmpleadores/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ObligacionesEmpleadores obligacionesEmpleadores = db.Tbl_Obligaciones_Empleadores.Find(id);
            if (obligacionesEmpleadores == null)
            {
                return HttpNotFound();
            }
            return View(obligacionesEmpleadores);
        }

        // POST: ObligacionesEmpleadores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ObligacionesEmpleadores obligacionesEmpleadores = db.Tbl_Obligaciones_Empleadores.Find(id);
            db.Tbl_Obligaciones_Empleadores.Remove(obligacionesEmpleadores);
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
