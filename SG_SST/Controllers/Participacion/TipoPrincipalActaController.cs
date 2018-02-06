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
    public class TipoPrincipalActaController : BaseController
    {
        private SG_SSTContext db = new SG_SSTContext();

        // GET: TipoPrincipalActa
        public ActionResult Index()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe iniciar Sesión para Continuar.";
                return RedirectToAction("Login", "Home");
            }

            return View(db.Tbl_TipoPrincipalActa.ToList());
        }

        // GET: TipoPrincipalActa/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoPrincipalActa tipoPrincipalActa = db.Tbl_TipoPrincipalActa.Find(id);
            if (tipoPrincipalActa == null)
            {
                return HttpNotFound();
            }
            return View(tipoPrincipalActa);
        }

        // GET: TipoPrincipalActa/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoPrincipalActa/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PK_Id_TipoPrincipal,DescripcionTipoPrincipal")] TipoPrincipalActa tipoPrincipalActa)
        {
            if (ModelState.IsValid)
            {
                db.Tbl_TipoPrincipalActa.Add(tipoPrincipalActa);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipoPrincipalActa);
        }

        // GET: TipoPrincipalActa/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoPrincipalActa tipoPrincipalActa = db.Tbl_TipoPrincipalActa.Find(id);
            if (tipoPrincipalActa == null)
            {
                return HttpNotFound();
            }
            return View(tipoPrincipalActa);
        }

        // POST: TipoPrincipalActa/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PK_Id_TipoPrincipal,DescripcionTipoPrincipal")] TipoPrincipalActa tipoPrincipalActa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoPrincipalActa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipoPrincipalActa);
        }

        // GET: TipoPrincipalActa/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoPrincipalActa tipoPrincipalActa = db.Tbl_TipoPrincipalActa.Find(id);
            if (tipoPrincipalActa == null)
            {
                return HttpNotFound();
            }
            return View(tipoPrincipalActa);
        }

        // POST: TipoPrincipalActa/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoPrincipalActa tipoPrincipalActa = db.Tbl_TipoPrincipalActa.Find(id);
            db.Tbl_TipoPrincipalActa.Remove(tipoPrincipalActa);
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
