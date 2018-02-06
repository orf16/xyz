using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SG_SST.Models;
using SG_SST.Models.Participacion;
using SG_SST.Controllers.Base;

namespace SG_SST.Controllers.Participacion
{
    public class TipoPrioridadMiembroController : BaseController
    {
        private SG_SSTContext db = new SG_SSTContext();

        // GET: TipoPrioridadMiembro
        public ActionResult Index()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe iniciar Sesión para Continuar.";
                return RedirectToAction("Login", "Home");
            }

            return View(db.Tbl_TipoPrioridadMiembroComite.ToList());
        }

        // GET: TipoPrioridadMiembro/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoPrioridadMiembro tipoPrioridadMiembro = db.Tbl_TipoPrioridadMiembroComite.Find(id);
            if (tipoPrioridadMiembro == null)
            {
                return HttpNotFound();
            }
            return View(tipoPrioridadMiembro);
        }

        // GET: TipoPrioridadMiembro/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoPrioridadMiembro/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PK_Id_TipoPrioridadMiembro,DescripcionTipoMiembro")] TipoPrioridadMiembro tipoPrioridadMiembro)
        {
            if (ModelState.IsValid)
            {
                db.Tbl_TipoPrioridadMiembroComite.Add(tipoPrioridadMiembro);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipoPrioridadMiembro);
        }

        // GET: TipoPrioridadMiembro/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoPrioridadMiembro tipoPrioridadMiembro = db.Tbl_TipoPrioridadMiembroComite.Find(id);
            if (tipoPrioridadMiembro == null)
            {
                return HttpNotFound();
            }
            return View(tipoPrioridadMiembro);
        }

        // POST: TipoPrioridadMiembro/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PK_Id_TipoPrioridadMiembro,DescripcionTipoMiembro")] TipoPrioridadMiembro tipoPrioridadMiembro)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoPrioridadMiembro).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipoPrioridadMiembro);
        }

        // GET: TipoPrioridadMiembro/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoPrioridadMiembro tipoPrioridadMiembro = db.Tbl_TipoPrioridadMiembroComite.Find(id);
            if (tipoPrioridadMiembro == null)
            {
                return HttpNotFound();
            }
            return View(tipoPrioridadMiembro);
        }

        // POST: TipoPrioridadMiembro/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoPrioridadMiembro tipoPrioridadMiembro = db.Tbl_TipoPrioridadMiembroComite.Find(id);
            db.Tbl_TipoPrioridadMiembroComite.Remove(tipoPrioridadMiembro);
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
