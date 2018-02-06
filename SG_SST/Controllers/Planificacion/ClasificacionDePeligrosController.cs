
namespace SG_SST.Controllers.Planificacion
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using SG_SST.Models;
    using SG_SST.Models.Planificacion;
    using SG_SST.Services.Planificacion.IServices;
    using SG_SST.Services.Planificacion.Services;
    using SG_SST.Controllers.Base;

    public class ClasificacionDePeligrosController : BaseController
    {
        private SG_SSTContext db = new SG_SSTContext();
        IClasificacionDePeligrosServicios clasificacionDePeligrosServicios = new ClasificacionDePeligrosServicios();
        private IMetodologiaServicios metodologiaServicios = new MetodologiaServicios();
        // GET: ClasificacionDePeligros
        public ActionResult Index()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                return View();
            }
            var tbl_Clasificacion_De_Peligro = db.Tbl_Clasificacion_De_Peligro.Include(c => c.TipoDePeligro);
            return View(tbl_Clasificacion_De_Peligro.ToList());
        }

        // GET: ClasificacionDePeligros/Details/5
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
            ClasificacionDePeligro clasificacionDePeligro = db.Tbl_Clasificacion_De_Peligro.Find(id);
            if (clasificacionDePeligro == null)
            {
                return HttpNotFound();
            }
            return View(clasificacionDePeligro);
        }

        // GET: ClasificacionDePeligros/Create
        public ActionResult Create()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                return View();
            }
            ViewBag.FK_Tipo_De_Peligro = new SelectList(db.Tbl_Tipo_De_Peligro, "PK_Tipo_De_Peligro", "Descripcion_Del_Peligro");
            return View();
        }

        // POST: ClasificacionDePeligros/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PK_Clasificacion_De_Peligro,Descripcion_Clase_De_Peligro,Detalle_Clase_De_Peligro,FK_Tipo_De_Peligro")] ClasificacionDePeligro clasificacionDePeligro)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                return View();
            }
            if (ModelState.IsValid)
            {
                db.Tbl_Clasificacion_De_Peligro.Add(clasificacionDePeligro);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FK_Tipo_De_Peligro = new SelectList(db.Tbl_Tipo_De_Peligro, "PK_Tipo_De_Peligro", "Descripcion_Del_Peligro", clasificacionDePeligro.FK_Tipo_De_Peligro);
            return View(clasificacionDePeligro);
        }

        // GET: ClasificacionDePeligros/Edit/5
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
            ClasificacionDePeligro clasificacionDePeligro = db.Tbl_Clasificacion_De_Peligro.Find(id);
            if (clasificacionDePeligro == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_Tipo_De_Peligro = new SelectList(db.Tbl_Tipo_De_Peligro, "PK_Tipo_De_Peligro", "Descripcion_Del_Peligro", clasificacionDePeligro.FK_Tipo_De_Peligro);
            return View(clasificacionDePeligro);
        }

        // POST: ClasificacionDePeligros/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PK_Clasificacion_De_Peligro,Descripcion_Clase_De_Peligro,Detalle_Clase_De_Peligro,FK_Tipo_De_Peligro")] ClasificacionDePeligro clasificacionDePeligro)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                return View();
            }
            if (ModelState.IsValid)
            {
                db.Entry(clasificacionDePeligro).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FK_Tipo_De_Peligro = new SelectList(db.Tbl_Tipo_De_Peligro, "PK_Tipo_De_Peligro", "Descripcion_Del_Peligro", clasificacionDePeligro.FK_Tipo_De_Peligro);
            return View(clasificacionDePeligro);
        }

        // GET: ClasificacionDePeligros/Delete/5
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
            ClasificacionDePeligro clasificacionDePeligro = db.Tbl_Clasificacion_De_Peligro.Find(id);
            if (clasificacionDePeligro == null)
            {
                return HttpNotFound();
            }
            return View(clasificacionDePeligro);
        }

        // POST: ClasificacionDePeligros/Delete/5
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
            ClasificacionDePeligro clasificacionDePeligro = db.Tbl_Clasificacion_De_Peligro.Find(id);
            db.Tbl_Clasificacion_De_Peligro.Remove(clasificacionDePeligro);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


      
        /// <summary>
        /// Controlador que obtiene las clases de peligros consultadas por el id/pk del tipo de peligro
        /// </summary>
        /// <param name="Pk_Tipo_Peligro">clave primaria  del tipo de peligro</param>
        /// <returns>Json de las clases de peligros</returns>
        public ActionResult ConsultarClasesPeligros(int Pk_Tipo_Peligro)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                return View();
            }

            List<ClasificacionDePeligro> clasesDePeligrosList = clasificacionDePeligrosServicios.ConsultarClasesDePeligros(Pk_Tipo_Peligro);
            if (clasesDePeligrosList.Count != 0)
            {
                return Json(
                   clasesDePeligrosList.Select(ClasesPeligros => new
                   {
                       PK_ClasesPeligros = ClasesPeligros.PK_Clasificacion_De_Peligro,
                       ClasesDescription = ClasesPeligros.Descripcion_Clase_De_Peligro
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
