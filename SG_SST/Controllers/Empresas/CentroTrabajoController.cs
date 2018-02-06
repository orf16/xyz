
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
    using System.Configuration;
    using RestSharp;
    using SG_SST.Dtos.Empresas;
    using SG_SST.Controllers.Base;
    public class CentroTrabajoController :  BaseController
    {
        private SG_SSTContext db = new SG_SSTContext();

        // GET: CentroTrabajo
        public ActionResult Index()
        {
            var tbl_Centro_de_Trabajo = db.Tbl_Centro_de_Trabajo.Include(c => c.Sede);
            return View(tbl_Centro_de_Trabajo.ToList());
        }

        // GET: CentroTrabajo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CentroTrabajo centroTrabajo = db.Tbl_Centro_de_Trabajo.Find(id);
            if (centroTrabajo == null)
            {
                return HttpNotFound();
            }
            return View(centroTrabajo);
        }

        // GET: CentroTrabajo/Create
        public ActionResult Create()
        {
            ViewBag.Fk_Id_Sede = new SelectList(db.Tbl_Sede, "Pk_Id_Sede", "Nombre_Sede");
            return View();
        }

        // POST: CentroTrabajo/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Pk_Id_Centro_de_Trabajo,Fk_Id_Sede,Descripcion_Actividad,Codigo_Actividad,Numero_Trabajadores")] CentroTrabajo centroTrabajo)
        {
            if (ModelState.IsValid)
            {
                db.Tbl_Centro_de_Trabajo.Add(centroTrabajo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Fk_Id_Sede = new SelectList(db.Tbl_Sede, "Pk_Id_Sede", "Nombre_Sede", centroTrabajo.Fk_Id_Sede);
            return View(centroTrabajo);
        }

        // GET: CentroTrabajo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CentroTrabajo centroTrabajo = db.Tbl_Centro_de_Trabajo.Find(id);
            if (centroTrabajo == null)
            {
                return HttpNotFound();
            }
            ViewBag.Fk_Id_Sede = new SelectList(db.Tbl_Sede, "Pk_Id_Sede", "Nombre_Sede", centroTrabajo.Fk_Id_Sede);
            return View(centroTrabajo);
        }

        // POST: CentroTrabajo/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Pk_Id_Centro_de_Trabajo,Fk_Id_Sede,Descripcion_Actividad,Codigo_Actividad,Numero_Trabajadores")] CentroTrabajo centroTrabajo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(centroTrabajo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Fk_Id_Sede = new SelectList(db.Tbl_Sede, "Pk_Id_Sede", "Nombre_Sede", centroTrabajo.Fk_Id_Sede);
            return View(centroTrabajo);
        }

        // GET: CentroTrabajo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CentroTrabajo centroTrabajo = db.Tbl_Centro_de_Trabajo.Find(id);
            if (centroTrabajo == null)
            {
                return HttpNotFound();
            }
            return View(centroTrabajo);
        }

        // POST: CentroTrabajo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CentroTrabajo centroTrabajo = db.Tbl_Centro_de_Trabajo.Find(id);
            db.Tbl_Centro_de_Trabajo.Remove(centroTrabajo);
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
