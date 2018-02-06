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
using SG_SST.ServiceRequest;
using SG_SST.Controllers.Base;

namespace SG_SST.Controllers.Aplicacion
{
    public class MaestroTipoInspeccionsController : BaseController
    {
        private SG_SSTContext db = new SG_SSTContext();

        // GET: MaestroTipoInspeccions
        public ActionResult Index()
        {


            return View(db.Tbl_Maestro_Planeación_Inspeccion.ToList());
        }

        // GET: MaestroTipoInspeccions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MaestroTipoInspeccion maestroTipoInspeccion = db.Tbl_Maestro_Planeación_Inspeccion.Find(id);
            if (maestroTipoInspeccion == null)
            {
                return HttpNotFound();
            }
            return View(maestroTipoInspeccion);
        }

        // GET: MaestroTipoInspeccions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MaestroTipoInspeccions/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Pk_Id_Maestro_Tipo_Inspeccion,Descripcion_Tipo_Inspeccion")] MaestroTipoInspeccion maestroTipoInspeccion)
        {
            if (ModelState.IsValid)
            {
                db.Tbl_Maestro_Planeación_Inspeccion.Add(maestroTipoInspeccion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(maestroTipoInspeccion);
        }

        // GET: MaestroTipoInspeccions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MaestroTipoInspeccion maestroTipoInspeccion = db.Tbl_Maestro_Planeación_Inspeccion.Find(id);
            if (maestroTipoInspeccion == null)
            {
                return HttpNotFound();
            }
            return View(maestroTipoInspeccion);
        }

        // POST: MaestroTipoInspeccions/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Pk_Id_Maestro_Tipo_Inspeccion,Descripcion_Tipo_Inspeccion")] MaestroTipoInspeccion maestroTipoInspeccion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(maestroTipoInspeccion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(maestroTipoInspeccion);
        }

        // GET: MaestroTipoInspeccions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MaestroTipoInspeccion maestroTipoInspeccion = db.Tbl_Maestro_Planeación_Inspeccion.Find(id);
            if (maestroTipoInspeccion == null)
            {
                return HttpNotFound();
            }
            return View(maestroTipoInspeccion);
        }

        // POST: MaestroTipoInspeccions/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MaestroTipoInspeccion maestroTipoInspeccion = db.Tbl_Maestro_Planeación_Inspeccion.Find(id);
            db.Tbl_Maestro_Planeación_Inspeccion.Remove(maestroTipoInspeccion);
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
