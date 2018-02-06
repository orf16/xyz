using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SG_SST.Models;
using SG_SST.Models.Empleado;
using SG_SST.Controllers.Base;


namespace SG_SST.Controllers.Empleado
{
    public class EmpleadoController : BaseController
    {
        private SG_SSTContext db = new SG_SSTContext();

        // GET: Empleado
        public ActionResult Index()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
            }
            var tblEmpleado = db.tblEmpleado.Include(t => t.Estado_Empleado).Include(t => t.TipoCotizante).Include(t => t.TipoDocumento);
            return View(tblEmpleado.Where(e =>e.FK_Empresa == usuarioActual.IdEmpresa).ToList());
        }

        // GET: Empleado/Details/5
        public ActionResult Details(int? id)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblEmpleado tblEmpleado = db.tblEmpleado.Find(id);
            if (tblEmpleado == null)
            {
                return HttpNotFound();
            }
            return View(tblEmpleado);
        }

        // GET: Empleado/Create
        public ActionResult Create()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
            }
            ViewBag.FK_ID_Estado = new SelectList(db.Tbl_Estado_Empl, "PK_IDEmpleadoEst", "EstEmplead");
            ViewBag.FK_ID_Tipo_Cotizante = new SelectList(db.Tbl_TipoCotizante, "Pk_Id_Cotizante", "Descripcion");
            ViewBag.FK_Tipo_Documento_Empl = new SelectList(db.Tbl_TipoDocumentos, "PK_IDTipo_Documento", "Descripcion");
            return View();
        }

        // POST: Empleado/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Empleado,FK_Tipo_Documento_Empl,PK_Numero_Documento_Empl,Primer_Nombre_Empl,Segundo_Nombre_Empl,Primer_Apellido_Empl,Segundo_Apellido_Empl,FK_ID_Estado,FK_ID_Tipo_Cotizante")] tblEmpleado tblEmpleado)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
            }
            if (ModelState.IsValid)
            {
                db.tblEmpleado.Add(tblEmpleado);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FK_ID_Estado = new SelectList(db.Tbl_Estado_Empl, "PK_IDEmpleadoEst", "EstEmplead", tblEmpleado.FK_ID_Estado);
            ViewBag.FK_ID_Tipo_Cotizante = new SelectList(db.Tbl_TipoCotizante, "Pk_Id_Cotizante", "Descripcion", tblEmpleado.FK_ID_Tipo_Cotizante);
            ViewBag.FK_Tipo_Documento_Empl = new SelectList(db.Tbl_TipoDocumentos, "PK_IDTipo_Documento", "Descripcion", tblEmpleado.FK_Tipo_Documento_Empl);
            return View(tblEmpleado);
        }

        // GET: Empleado/Edit/5
        public ActionResult Edit(int? id)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblEmpleado tblEmpleado = db.tblEmpleado.Find(id);
            if (tblEmpleado == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_ID_Estado = new SelectList(db.Tbl_Estado_Empl, "PK_IDEmpleadoEst", "EstEmplead", tblEmpleado.FK_ID_Estado);
            ViewBag.FK_ID_Tipo_Cotizante = new SelectList(db.Tbl_TipoCotizante, "Pk_Id_Cotizante", "Descripcion", tblEmpleado.FK_ID_Tipo_Cotizante);
            ViewBag.FK_Tipo_Documento_Empl = new SelectList(db.Tbl_TipoDocumentos, "PK_IDTipo_Documento", "Descripcion", tblEmpleado.FK_Tipo_Documento_Empl);
            return View(tblEmpleado);
        }

        // POST: Empleado/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Empleado,FK_Tipo_Documento_Empl,PK_Numero_Documento_Empl,Primer_Nombre_Empl,Segundo_Nombre_Empl,Primer_Apellido_Empl,Segundo_Apellido_Empl,FK_ID_Estado,FK_ID_Tipo_Cotizante")] tblEmpleado tblEmpleado)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
            }
            if (ModelState.IsValid)
            {
                db.Entry(tblEmpleado).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FK_ID_Estado = new SelectList(db.Tbl_Estado_Empl, "PK_IDEmpleadoEst", "EstEmplead", tblEmpleado.FK_ID_Estado);
            ViewBag.FK_ID_Tipo_Cotizante = new SelectList(db.Tbl_TipoCotizante, "Pk_Id_Cotizante", "Descripcion", tblEmpleado.FK_ID_Tipo_Cotizante);
            ViewBag.FK_Tipo_Documento_Empl = new SelectList(db.Tbl_TipoDocumentos, "PK_IDTipo_Documento", "Descripcion", tblEmpleado.FK_Tipo_Documento_Empl);
            return View(tblEmpleado);
        }

        // GET: Empleado/Delete/5
        public ActionResult Delete(int? id)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblEmpleado tblEmpleado = db.tblEmpleado.Find(id);
            if (tblEmpleado == null)
            {
                return HttpNotFound();
            }
            return View(tblEmpleado);
        }

        // POST: Empleado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
            }
            tblEmpleado tblEmpleado = db.tblEmpleado.Find(id);
            db.tblEmpleado.Remove(tblEmpleado);
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

        public ActionResult Organigrama()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "Debe estar Autenticado para continuar en el sistema.";
            }
            int pkidempresa = usuarioActual.IdEmpresa;
            List<EmpleadoOrg> busca = (from p in db.Tbl_EmpleadoOrg
                                       join pe in db.Tbl_Organigrama on p.Fk_Id_Organigrama equals pe.Pk_Id_Organigrama
                                       where pe.Fk_Id_Empresa == pkidempresa
                                       select p).ToList();

            if (busca.Count==0)
            {
                ViewBag.Message1a = "No se han Registrado los cargos, Registrelos para ver el Organigrama en Linea.";
            }
            return View("organigrama");
        }

        public JsonResult JsonEmpleado()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
            }
            List<EmpleadoOrg> empleadosOrg = db.Tbl_EmpleadoOrg.Where(eo =>eo.Organigrama.Fk_Id_Empresa == usuarioActual.IdEmpresa).ToList();

            if (empleadosOrg.Count != 0)
            {
                return Json(
                   empleadosOrg.Select(EmpleadoOrg => new
                   {                       
                       Id_Empleado = EmpleadoOrg.Id_EmpleadoOrg,                      
                       Cargo_Empleado = EmpleadoOrg.Cargo_Empleado,
                       Id_Jefe = (EmpleadoOrg.Fk_Id_EmpleadoOrg == null) ? -1 : EmpleadoOrg.Fk_Id_EmpleadoOrg
                   })
                , JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult PDFOrganigrama()
        {

            return new Rotativa.ActionAsPdf("Organigrama");
        }

        
    }
}
