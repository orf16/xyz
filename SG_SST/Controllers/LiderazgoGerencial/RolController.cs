
namespace SG_SST.Controllers.LiderazgoGerencial
{
    using SG_SST.Models;
    using SG_SST.Models.Empresas;
    using SG_SST.Models.LiderazgoGerencial;
    using SG_SST.Models.Organizacion;
    using SG_SST.Dtos.LiderazgoGerencial;
    using SG_SST.Services.Empresas.IServices;
    using SG_SST.Services.Empresas.Services;
    using SG_SST.Services.General.IServices;
    using SG_SST.Services.General.Services;
    using SG_SST.Services.LiderazgoGerencial.Iservices;
    using SG_SST.Services.LiderazgoGerencial.Services;
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Configuration; 
    using System.Net;
    using System.Web;
    using System.Web.Mvc;
    using System.Data.Entity;
    using System.IO;
    using Rotativa;
    using SG_SST.Controllers.Base;

    public class RolController : BaseController
    {
        private SG_SSTContext db = new SG_SSTContext();
        private IRolPorResponsabilidadServicio rolPorResponsabilidadServicio = new RolPorResponsabilidadServicio();


        /// <summary>
        /// Metodo que me realiza el guardado del rol 
        /// </summary>
        /// <param name="presupuesto">rol, responsabilidades y rendición de cuentas a guardar</param>       
        /// <returns></returns>
        [HttpPost]
        public ActionResult CrearRolResponsabilidad(Rol rol, List<Responsabilidades> responsabilidad, List<RendicionDeCuentas> rendicion)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            List<Rol> CompararRol = db.Tbl_Rol.Where(r => r.Fk_Id_Empresa == usuarioActual.IdEmpresa).ToList();
            foreach (var comparar in CompararRol)
            {
                if (comparar.Descripcion == rol.Descripcion.ToUpper())
                {
                    return null;
                }
            }

            responsabilidad.RemoveAll(x => x.Descripcion == null);
            rendicion.RemoveAll(x => x.Descripcion == null);
            bool respuestaGuardado = rolPorResponsabilidadServicio.GuardarRolYResponsabilidades(rol, responsabilidad, rendicion, usuarioActual.IdEmpresa);
            ViewBag.respuestaGuardado = respuestaGuardado;
            List<Rol> rolesPorEmpresa = new List<Rol>();
            rolesPorEmpresa = rolPorResponsabilidadServicio.RolesPorEmpresa(usuarioActual.IdEmpresa);
            return PartialView("RolPorResponsabilidadesVP", rolesPorEmpresa);
        }

        ///<summary>
        ///Metodo que me retorna la vista para crear un rol con responsabilidades y rendicion de cuentas.
        ///</summary>
        ///<returns>vista</returns>
        ///GET: Rol
        public ActionResult CrearRol()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            List<Rol> rolesPorEmpresa = new List<Rol>();
            rolesPorEmpresa = rolPorResponsabilidadServicio.RolesPorEmpresa(usuarioActual.IdEmpresa);
            RolResponsabilidad rolResponsabilidad = new RolResponsabilidad();
            rolResponsabilidad.RolesResponsabilidad = rolesPorEmpresa;
            List<ObligacionesEmpleadores> obligacionesEmpleadores = new List<ObligacionesEmpleadores>();
            obligacionesEmpleadores = rolPorResponsabilidadServicio.GetObligacionesEmpleadores();
            rolResponsabilidad.ObligacionesEmpleadoresRol = obligacionesEmpleadores;
            List<ObligacionesArl> obligacionesArl = new List<ObligacionesArl>();
            obligacionesArl = rolPorResponsabilidadServicio.GetObligacionesArl();
            rolResponsabilidad.ObligacionesArlRol = obligacionesArl;
            return View(rolResponsabilidad);
        }

        ///<summary>
        ///Metodo que me guarda la edicion del rol, las responsabilidades y rendicion de cuentas.
        ///</summary>
        ///<returns>vista</returns>
        ///HttpPost: Rol
        [HttpPost]
        public ActionResult CrearEditRolResponsabilidad(int id)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            List<ResponsabilidadesPorRol> responsabilidadesPorRol = new List<ResponsabilidadesPorRol>();
            List<RendicionDeCuentasPorRol> rendicionDeCuentasPorRol = new List<RendicionDeCuentasPorRol>();
            responsabilidadesPorRol = db.Tbl_Responsabilidades_Por_Rol.Where(s => s.Fk_Id_Rol == id).ToList();
            rendicionDeCuentasPorRol = db.Tbl_Rendicion_Cuenta_Por_Rol.Where(s => s.Fk_Id_Rol == id).ToList();
            List<Rol> rolesPorEmpresa = new List<Rol>();
            rolesPorEmpresa = rolPorResponsabilidadServicio.RolesPorEmpresa(usuarioActual.IdEmpresa);
            return PartialView("EditRolVp", responsabilidadesPorRol);
        }

        /// <summary>
        /// Metodo que realiza el guardado de la edición del rol
        /// </summary>       
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditatRol(Rol rol, List<Responsabilidades> responsabilidad, List<RendicionDeCuentas> rendicionDeCuenta,
            int[] responsaEliminadas, int[] rendiciEliminadas)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            bool respuestaEditado = rolPorResponsabilidadServicio.EditarRolYResponsabilidades(rol, responsabilidad, rendicionDeCuenta, responsaEliminadas, rendiciEliminadas, usuarioActual.IdEmpresa);
            ViewBag.respuestaEditado = respuestaEditado;           
            List<Rol> rolesPorEmpresa = new List<Rol>();
            rolesPorEmpresa = rolPorResponsabilidadServicio.RolesPorEmpresa(usuarioActual.IdEmpresa);
            RolResponsabilidad rolResponsabilidad = new RolResponsabilidad();
            rolResponsabilidad.RolesResponsabilidad = rolesPorEmpresa;
            List<ObligacionesEmpleadores> obligacionesEmpleadores = new List<ObligacionesEmpleadores>();
            obligacionesEmpleadores = rolPorResponsabilidadServicio.GetObligacionesEmpleadores();
            rolResponsabilidad.ObligacionesEmpleadoresRol = obligacionesEmpleadores;
            List<ObligacionesArl> obligacionesArl = new List<ObligacionesArl>();
            obligacionesArl = rolPorResponsabilidadServicio.GetObligacionesArl();
            rolResponsabilidad.ObligacionesArlRol = obligacionesArl;
            return View("CrearRol", rolResponsabilidad);
        }


        /// <summary>
        /// Metodo que realiza la eliminacion del rol
        /// </summary>      
        /// <param name=""></param>
        /// <returns></returns>
        public ActionResult DeleteConfirmed(int id)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            bool respuestaEliminado = rolPorResponsabilidadServicio.EliminarRolYResponsabilidades(id);
            ViewBag.respuestaEliminado = respuestaEliminado;
            List<Rol> rolesPorEmpresa = new List<Rol>();
            rolesPorEmpresa = rolPorResponsabilidadServicio.RolesPorEmpresa(usuarioActual.IdEmpresa);
            RolResponsabilidad rolResponsabilidad = new RolResponsabilidad();
            rolResponsabilidad.RolesResponsabilidad = rolesPorEmpresa;
            List<ObligacionesEmpleadores> obligacionesEmpleadores = new List<ObligacionesEmpleadores>();
            obligacionesEmpleadores = rolPorResponsabilidadServicio.GetObligacionesEmpleadores();
            rolResponsabilidad.ObligacionesEmpleadoresRol = obligacionesEmpleadores;
            List<ObligacionesArl> obligacionesArl = new List<ObligacionesArl>();
            obligacionesArl = rolPorResponsabilidadServicio.GetObligacionesArl();
            rolResponsabilidad.ObligacionesArlRol = obligacionesArl;
            return View("CrearRol", rolResponsabilidad);
        }

        /// <summary>
        /// Metodo que permite visualizar un PDF con los roles y las responsabilidades
        /// obligaciones ARL
        /// Obligaciones Empleador
        /// </summary>       
        /// <param name=""></param>
        /// <returns></returns>
         [AllowAnonymous]
         public ActionResult RolPorResponsabilidad_PDF()
         {
             var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
             if (usuarioActual == null)
             {
                 ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                 return RedirectToAction("Login", "Home");
             }

            string EncodedRazonSocial = System.Net.WebUtility.UrlEncode(usuarioActual.RazonSocialEmpresa);
            string EncodedNit = System.Net.WebUtility.UrlEncode(usuarioActual.NitEmpresa);
            string EncodedNombreInforme = System.Net.WebUtility.UrlEncode("Roles y Responsabilidades");
            var footurl = "https://alissta.gov.co/Acciones/Footer";
            var headerurl = "https://alissta.gov.co/Acciones/Header?NombreEmpresa=" + EncodedRazonSocial + "&NitEmpresa=" + EncodedNit + "&NombreInforme=" + EncodedNombreInforme;


            List<Rol> rolesPorEmpresa = new List<Rol>();
             rolesPorEmpresa = rolPorResponsabilidadServicio.RolesPorEmpresa(usuarioActual.IdEmpresa);
             RolResponsabilidad rolResponsabilidad = new RolResponsabilidad();
             rolResponsabilidad.RolesResponsabilidad = rolesPorEmpresa;
             List<ObligacionesEmpleadores> obligacionesEmpleadores = new List<ObligacionesEmpleadores>();
             obligacionesEmpleadores = rolPorResponsabilidadServicio.GetObligacionesEmpleadores();
             rolResponsabilidad.ObligacionesEmpleadoresRol = obligacionesEmpleadores;
             List<ObligacionesArl> obligacionesArl = new List<ObligacionesArl>();
             obligacionesArl = rolPorResponsabilidadServicio.GetObligacionesArl();
             rolResponsabilidad.ObligacionesArlRol = obligacionesArl;
             var data = rolResponsabilidad;

             //string SwitchNombreEmpresa = usuarioActual.RazonSocialEmpresa;
             //string SwitchNitEmpresa = usuarioActual.NitEmpresa;
             //string SwitchNombreDocumento = "Roles y Responsabilidades";
             //var uriFooter = new Uri(Url.Action("Footer", "Rol", null, Request.Url.Scheme));
             //var clean1 = uriFooter.GetComponents(UriComponents.AbsoluteUri & ~UriComponents.Port,UriFormat.UriEscaped);
             //var uriHeader = new Uri(Url.Action("Header", "Rol", new { NombreEmpresa = SwitchNombreEmpresa, NitEmpresa = SwitchNitEmpresa, NombreInforme = SwitchNombreDocumento }, Request.Url.Scheme));
             //var clean2 = uriHeader.GetComponents(UriComponents.AbsoluteUri & ~UriComponents.Port,UriFormat.UriEscaped);

             string cusomtSwitches = string.Format("--footer-line --print-media-type --allow {0} --footer-html {0} --header-line --print-media-type --allow {1} --header-html {1} --header-spacing 5", footurl, headerurl);
             return new Rotativa.PartialViewAsPdf("RolResponabilidadesPDF", data) { FileName = "Roles y Responsabilidades.pdf" ,CustomSwitches = cusomtSwitches };
         }

         /// <summary>
         /// Metodo que permite visualizar un PDF con el Acta de Responsabilidades
         /// </summary>
         /// <param name=""></param>        
         /// <returns></returns>
         [AllowAnonymous]
         public ActionResult ActaRolPorResponsabilidad_PDF()
         {
             var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
             if (usuarioActual == null)
             {
                 ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                 return RedirectToAction("Login", "Home");
             }
             Empresa empresa = db.Tbl_Empresa
             .Include(x => x.Roles.Select(j => j.ResponsabilidadesPorRoles))
             .Include(x => x.Roles.Select(j => j.RendicionDeCuentasPorRoles))
             .Include(y => y.Usuario.Select(g => g.UsuarioRoles))
             .Where(r => r.Pk_Id_Empresa == usuarioActual.IdEmpresa).FirstOrDefault();
             ActaRoles actaRol = new ActaRoles();
             Rol rol1 = empresa.Roles.Where(d => d.Descripcion == ConfigurationManager.AppSettings["rolResponsableSGSST"]).FirstOrDefault();
             actaRol.RolesResponsabilidadResponsable = rol1;
             Rol rol2 = empresa.Roles.Where(d => d.Descripcion == ConfigurationManager.AppSettings["rolRepresentanteLegal"]).FirstOrDefault();
             actaRol.RolesResponsabilidadRepresentante = rol2;
             if(rol1.UsuarioRoles == null || rol2.UsuarioRoles == null)
             {
                 List<Rol> rolesPorEmpresa = new List<Rol>();
                 rolesPorEmpresa = rolPorResponsabilidadServicio.RolesPorEmpresa(usuarioActual.IdEmpresa);
                 RolResponsabilidad rolResponsabilidad = new RolResponsabilidad();
                 rolResponsabilidad.RolesResponsabilidad = rolesPorEmpresa;
                 List<ObligacionesEmpleadores> obligacionesEmpleadores = new List<ObligacionesEmpleadores>();
                 obligacionesEmpleadores = rolPorResponsabilidadServicio.GetObligacionesEmpleadores();
                 rolResponsabilidad.ObligacionesEmpleadoresRol = obligacionesEmpleadores;
                 List<ObligacionesArl> obligacionesArl = new List<ObligacionesArl>();
                 obligacionesArl = rolPorResponsabilidadServicio.GetObligacionesArl();
                 rolResponsabilidad.ObligacionesArlRol = obligacionesArl;
                 if (rol1.UsuarioRoles == null)
                 {
                     ViewBag.NombreRol = "false";
                 }
                 if (rol2.UsuarioRoles == null)
                 {
                     ViewBag.NombreRol = "true";
                 }
                 if (rol1.UsuarioRoles == null && rol2.UsuarioRoles == null)
                 {
                     ViewBag.NombreRol = "";
                 }
                 ViewBag.NoexisteRol = true;
                 return View("CrearRol", rolResponsabilidad);
             }
             string EncodedRazonSocial = System.Net.WebUtility.UrlEncode(usuarioActual.RazonSocialEmpresa);
             string EncodedNit = System.Net.WebUtility.UrlEncode(usuarioActual.NitEmpresa);
             string EncodedNombreInforme = System.Net.WebUtility.UrlEncode("Acta Responsable");
             var footurl = "https://alissta.gov.co/Acciones/Footer";
             var headerurl = "https://alissta.gov.co/Acciones/Header?NombreEmpresa=" + EncodedRazonSocial + "&NitEmpresa=" + EncodedNit + "&NombreInforme=" + EncodedNombreInforme;


             var fileName = "Acta Responsable" + usuarioActual.NitEmpresa + ".pdf";
             //string SwitchNombreEmpresa = usuarioActual.RazonSocialEmpresa;
             //string SwitchNitEmpresa = usuarioActual.NitEmpresa;
             //string SwitchNombreDocumento = "NOMBRAMIENTO DE RESPONSABLE DE SEGURIDAD Y SALUD EN EL TRABAJO - SGSST";
             //var uriFooter = new Uri(Url.Action("Footer", "Rol", null, Request.Url.Scheme));
             //var clean1 = uriFooter.GetComponents(UriComponents.AbsoluteUri & ~UriComponents.Port, UriFormat.UriEscaped);
             //var uriHeader = new Uri(Url.Action("Header", "Rol", new { NombreEmpresa = SwitchNombreEmpresa, NitEmpresa = SwitchNitEmpresa, NombreInforme = SwitchNombreDocumento }, Request.Url.Scheme));
             //var clean2 = uriHeader.GetComponents(UriComponents.AbsoluteUri & ~UriComponents.Port, UriFormat.UriEscaped);
             //string cusomtSwitches = string.Format("--footer-line --print-media-type --allow {0} --footer-html {0} --header-line --print-media-type --allow {1} --header-html {1} --header-spacing 5", clean1, clean2);
             //string cusomtSwitches = string.Format("--footer-line --print-media-type --allow {0} --footer-html {0} --header-line --print-media-type --allow {1} --header-html {1} --header-spacing 5",
             //.AbsoluteUri, uriHeader.AbsoluteUri);
             var filePath = Path.Combine(Server.MapPath("~/Content/ArchivosDocumentacion"), fileName);
             Documentacion_Organizacion document = new Documentacion_Organizacion();
             document.FK_Empresa = usuarioActual.IdEmpresa;
             document.NombreArchivo_Documentacion = fileName;
             document.FK_TipoModuloOrganizacion = 2;
             document.FechaModificacion_Documentacion = DateTime.Now;
             Documentacion_Organizacion documentDel = db.Tbl_Documentacion_Organizacion.Where(p => p.FK_Empresa == usuarioActual.IdEmpresa && p.FK_TipoModuloOrganizacion==2).FirstOrDefault();
             if (documentDel!=null )
              db.Tbl_Documentacion_Organizacion.Remove(documentDel);
             db.Tbl_Documentacion_Organizacion.Add (document);
             db.SaveChanges();

             string cusomtSwitches = string.Format("--footer-line --print-media-type --allow {0} --footer-html {0} --header-line --print-media-type --allow {1} --header-html {1} --header-spacing 5", footurl, headerurl);
             return new Rotativa.PartialViewAsPdf("ActaNombramientoRepresentantePDF", actaRol) { FileName = "Acta Responsable.pdf",CustomSwitches = cusomtSwitches,  SaveOnServerPath = filePath };
         }


         [AllowAnonymous]
         public ActionResult Footer()
         {
             return View();
         }


         [AllowAnonymous]
         public ActionResult Header(string NombreEmpresa, string NitEmpresa, string NombreInforme)
         {
             ViewBag.NombreEmpresa = NombreEmpresa;
             ViewBag.NitEmpresa = NitEmpresa;
             ViewBag.NombreInforme = NombreInforme;
             return View();
         }

         /// <summary>
         /// Metodo que permite guardar un PDF con el Acta de Responsabilidades en el modulo Organizacion/Documentos
         /// </summary>
         /// <param name=""></param>         
         /// <returns></returns>
         public ActionResult GuardarActaRolPorResponsabilidad_PDF()
         {
             var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
             if (usuarioActual == null)
             {
                 ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                 return RedirectToAction("Login", "Home");
             }
             Empresa empresa = db.Tbl_Empresa
             .Include(x => x.Roles.Select(j => j.ResponsabilidadesPorRoles))
             .Include(x => x.Roles.Select(j => j.RendicionDeCuentasPorRoles))
             .Include(y => y.Usuario.Select(g => g.UsuarioRoles))
             .Where(r => r.Pk_Id_Empresa == usuarioActual.IdEmpresa).FirstOrDefault();
             ActaRoles actaRol = new ActaRoles();
             Rol rol1 = empresa.Roles.Where(d => d.Descripcion == ConfigurationManager.AppSettings["rolResponsableSGSST"]).FirstOrDefault();
             actaRol.RolesResponsabilidadResponsable = rol1;
             Rol rol2 = empresa.Roles.Where(d => d.Descripcion == ConfigurationManager.AppSettings["rolRepresentanteLegal"]).FirstOrDefault();
             actaRol.RolesResponsabilidadRepresentante = rol2;                          
             return View("ActaNombramientoRepresentantePDF", actaRol);
         }
          [AllowAnonymous]
         public ActionResult GetFirma(int Pk_Id_Usuario)
         {
            //var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            // if (usuarioActual == null)
            // {
            //     ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
            //     return View();
            // }
             using (var transaction = db.Database.BeginTransaction())
             {
                 try
                 {
                     string nombreFirma = db.Tbl_Usuario.Find(Pk_Id_Usuario).Imagen_Firma;
                     var path = Server.MapPath("~/Content/Images");                     
                     var file = nombreFirma;
                     var fullPath = Path.Combine(path, file);
                     return File(fullPath, "image/png", file);
                 }

                 catch (Exception)
                 {
                     transaction.Rollback();
                     return View();
                 }
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
