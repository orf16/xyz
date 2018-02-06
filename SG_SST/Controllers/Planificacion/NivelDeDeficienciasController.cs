
namespace SG_SST.Controllers.Planificacion
{
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using SG_SST.Models;
    using SG_SST.Models.Planificacion;
    using System.Collections.Generic;
using SG_SST.Services.Planificacion.IServices;
    using SG_SST.Services.Planificacion.Services;
    using SG_SST.Controllers.Base;
    public class NivelDeDeficienciasController : BaseController
    {
        private SG_SSTContext db = new SG_SSTContext();
        private INivelDeDeficienciaServicios nivelDeDeficienciaServicios = new NivelDeDeficienciaServicios();
        
        // GET: NivelDeDeficiencias
        public ActionResult Index()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                return View();
            }
            return View(db.Tbl_Nivel_De_Deficiencia.ToList());
        }

        // GET: NivelDeDeficiencias/Details/5
        public ActionResult Details(int? id)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                return View();
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NivelDeDeficiencia nivelDeDeficiencia = db.Tbl_Nivel_De_Deficiencia.Find(id);
            if (nivelDeDeficiencia == null)
            {
                return HttpNotFound();
            }
            return View(nivelDeDeficiencia);
        }

        // GET: NivelDeDeficiencias/Create
        public ActionResult Create()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                return View();
            }
            return View();
        }

        // POST: NivelDeDeficiencias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PK_Nivel_De_Deficiencia,FLAG_Cuantitativa,FLAG_Quimico,Valor_Deficiencia,Descripcion_Deficiciencia")] NivelDeDeficiencia nivelDeDeficiencia)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                return View();
            }
            if (ModelState.IsValid)
            {
                db.Tbl_Nivel_De_Deficiencia.Add(nivelDeDeficiencia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nivelDeDeficiencia);
        }

        // GET: NivelDeDeficiencias/Edit/5
        public ActionResult Edit(int? id)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                return View();
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NivelDeDeficiencia nivelDeDeficiencia = db.Tbl_Nivel_De_Deficiencia.Find(id);
            if (nivelDeDeficiencia == null)
            {
                return HttpNotFound();
            }
            return View(nivelDeDeficiencia);
        }

        // POST: NivelDeDeficiencias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PK_Nivel_De_Deficiencia,FLAG_Cuantitativa,FLAG_Quimico,Valor_Deficiencia,Descripcion_Deficiciencia")] NivelDeDeficiencia nivelDeDeficiencia)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                return View();
            }
            if (ModelState.IsValid)
            {
                db.Entry(nivelDeDeficiencia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nivelDeDeficiencia);
        }

        // GET: NivelDeDeficiencias/Delete/5
        public ActionResult Delete(int? id)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                return View();
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NivelDeDeficiencia nivelDeDeficiencia = db.Tbl_Nivel_De_Deficiencia.Find(id);
            if (nivelDeDeficiencia == null)
            {
                return HttpNotFound();
            }
            return View(nivelDeDeficiencia);
        }

        // POST: NivelDeDeficiencias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                return View();
            }
            NivelDeDeficiencia nivelDeDeficiencia = db.Tbl_Nivel_De_Deficiencia.Find(id);
            db.Tbl_Nivel_De_Deficiencia.Remove(nivelDeDeficiencia);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Controlador para consutlar los niveles de deficiencia consultado por si tipo cuantitativo y si es quimico
        /// </summary>
        /// <param name="FLAG_Cuantitativa"> verdadero para si es cuantitativo</param>
        /// <param name="FLAG_Quimico">verdadero para si es quimico</param>
        /// <returns></returns>
        public ActionResult ConsultarNivelesDeDeficiencia(bool FLAG_Cuantitativa)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                return View();
            }
            List<NivelDeDeficiencia> nivelesDeDeficienciaList = nivelDeDeficienciaServicios.ConsultarNivelesDeDeficiencia(FLAG_Cuantitativa);
            if (nivelesDeDeficienciaList.Count != 0)
            {
                return Json(
                   nivelesDeDeficienciaList.Select(nivelesDeDeficiencia => new
                   {
                       PK_NivelDeDeficiencia = nivelesDeDeficiencia.PK_Nivel_De_Deficiencia,
                       NivelDeDeficienciaDescription = nivelesDeDeficiencia.Descripcion_Deficiciencia,
                       ValorDeficiencia = nivelesDeDeficiencia.Valor_Deficiencia
                   })
                , JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
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
