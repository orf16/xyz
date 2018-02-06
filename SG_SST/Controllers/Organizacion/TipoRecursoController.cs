using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SG_SST.Models;
using SG_SST.Models.Organizacion;
using SG_SST.Services.Organizacion.Services;
using SG_SST.Services.Organizacion.Iservices;
using SG_SST.Controllers.Base;

namespace SG_SST.Controllers
{
    public class TipoRecursoController : BaseController
    {
        private SG_SSTContext db = new SG_SSTContext();
        private ITiporecursoServices TipoRecursoServices = new TipoRecursoServices();

        // GET: TipoRecurso
        public ActionResult Index()
        {
            return View(db.Tbl_Tipo_Recurso.ToList());
        }

        // GET: TipoRecurso/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoRecurso tipoRecurso = db.Tbl_Tipo_Recurso.Find(id);
            if (tipoRecurso == null)
            {
                return HttpNotFound();
            }
            return View(tipoRecurso);
        }

        // GET: TipoRecurso/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoRecurso/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(TipoRecurso tipoRecurso)
        {
            bool restpuestaGuardado = TipoRecursoServices.GuardarTipoRecurso(tipoRecurso);

            if (restpuestaGuardado)
            {
                ViewBag.Mensajes = "Exito en el registro";
                return RedirectToAction("Index");
            }
            

            return RedirectToAction("Index");
        }

        private ActionResult RedirectResult(string p)
        {
            throw new NotImplementedException();
        }

        // GET: TipoRecurso/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoRecurso tipoRecurso = db.Tbl_Tipo_Recurso.Find(id);
            if (tipoRecurso == null)
            {
                return HttpNotFound();
            }
            return View(tipoRecurso);
        }

        // POST: TipoRecurso/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Pk_Id_TipoRecurso,Descripcion_Tipo_Recurso")] TipoRecurso tipoRecurso)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoRecurso).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipoRecurso);
        }

        // GET: TipoRecurso/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoRecurso tipoRecurso = db.Tbl_Tipo_Recurso.Find(id);
            if (tipoRecurso == null)
            {
                return HttpNotFound();
            }
            return View(tipoRecurso);
        }

        // POST: TipoRecurso/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoRecurso tipoRecurso = db.Tbl_Tipo_Recurso.Find(id);
            db.Tbl_Tipo_Recurso.Remove(tipoRecurso);
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
