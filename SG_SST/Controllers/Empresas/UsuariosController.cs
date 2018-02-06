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
using System.IO;
using System.Drawing;
using SG_SST.Controllers.Base;
using SG_SST.Models.Ausentismo;
using System.Configuration;
using RestSharp;
using SG_SST.Audotoria;
using SG_SST.Models.AdminUsuarios;

namespace SG_SST.Controllers.Empresas
{
    public class UsuariosController : BaseController
    {
        private SG_SSTContext db = new SG_SSTContext();
        RegistraLog registroLog = new RegistraLog();

        // GET: Usuarios
        public ActionResult Index()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            var usuarios = db.Tbl_Usuario
                .Include(u => u.UsuarioRoles)
                .Include(u => u.TipoDocumentos)
                .Where(y => y.Fk_Id_Empresa == usuarioActual.IdEmpresa).ToList();
            var ms = TempData["shortMessage"];
            if (ms != null)
            {
                ViewBag.mensaje = ms;
            }
            return View(usuarios);
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(int? id)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }  
            Usuario usuario = db.Tbl_Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }



        [HttpPost]
        public JsonResult ConsultarDatosTrabajador(string numeroDocumento)
        {
            try
            {
                EmpresaAfiliadoModel datos = null;
                if (!string.IsNullOrEmpty(numeroDocumento))
                {
                    var cliente = new RestSharp.RestClient(ConfigurationManager.AppSettings["Url"]);
                    var request = new RestRequest(ConfigurationManager.AppSettings["consultaAfiliado"], RestSharp.Method.GET);
                    request.RequestFormat = DataFormat.Xml;
                    request.Parameters.Clear();
                    request.AddParameter("tpDoc", "cc");
                    request.AddParameter("doc", numeroDocumento);
                    request.AddHeader("Content-Type", "application/json");
                    request.AddHeader("Accept", "application/json");

                    //se omite la validación de certificado de SSL
                    ServicePointManager.ServerCertificateValidationCallback = delegate
                    { return true; };
                    IRestResponse<List<EmpresaAfiliadoModel>> response = cliente.Execute<List<EmpresaAfiliadoModel>>(request);
                    var result = response.Content;
                    if (!string.IsNullOrWhiteSpace(result))
                    {
                        var respuesta = Newtonsoft.Json.JsonConvert.DeserializeObject<List<EmpresaAfiliadoModel>>(result);
                        if (respuesta.Count == 0)
                            return Json(new { Data = "No se encontró ningun Trabajador asociado al documento ingresado.", Mensaje = "NOTFOUND" });
                        var afiliado = respuesta.Where(a => a.Estado == "Activo").FirstOrDefault();
                        if (afiliado == null)
                            return Json(new { Data = "El documento ingresado esta inactivo.", Mensaje = "INACTIVO" });
                        else
                        {
                           GuardarSesionAfiliado(afiliado);
                            datos = afiliado;
                        }
                    }
                }
                if (datos != null)
                {
                    return Json(new { Data = datos, Mensaje = "OK" });
                }
                else
                    return Json(new { Data = "No se encontró ningun trabajador asociado al documento ingresado", Mensaje = "NOTFOUND" });
            }
            catch (Exception ex)
            {
                registroLog.RegistrarError(typeof(UsuariosController), string.Format("Error en la Acción ConsultarDatosTrabajador: {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                return Json(new { Data = "No se logró consultar la información del Trabajador. Intente más tarde.", Mensaje = "ERROR" });
            }
        }

       

      
        // GET: Usuarios/Create
        public ActionResult Create()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            //ViewBag.nit_Empresa = SessionDataSG_SST.EmpresaSession.Nit_Empresa.FirstOrDefault();

            var resulta = (from rol in db.Tbl_Rol
                           where (rol.Descripcion == "REPRESENTANTE LEGAL" || rol.Descripcion == "RESPONSABLE DE SGSST" || rol.Descripcion == "PROFESIONAL SST") & rol.Fk_Id_Empresa == usuarioActual.IdEmpresa
                           select rol).ToList();
            ViewBag.Fk_Id_Rol = new SelectList(resulta, "Pk_Id_Rol", "Descripcion");
            ViewBag.Fk_Tipo_Documento = new SelectList(db.Tbl_TipoDocumentos, "PK_IDTipo_Documento", "Descripcion");
            return View();
        }




       //public ActionResult Create()
        // POST: Usuarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
     //   [ValidateAntiForgeryToken]
        public ActionResult Create(Usuario usuario, HttpPostedFileBase Firma, List<int> Fk_Id_Rol)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            try
            {
                usuario.Nombre_Usuario = usuario.Nombre_Usuario.ToUpper();
                List<Usuario> usuarios = db.Tbl_Usuario.Where(u => u.Pk_Id_Usuario > 0).ToList();
                if (usuarios.Count > 0)
                {
                    foreach (var sct in usuarios)
                    {
                        if (sct.Nombre_Usuario == usuario.Nombre_Usuario & sct.Numero_Documento == usuario.Numero_Documento & sct.Fk_Id_Empresa==usuarioActual.IdEmpresa)
                        {
                            ViewBag.mensaje = "Usuario: " + usuario.Nombre_Usuario + "  Ya Existe.";
                            var usuariosL = db.Tbl_Usuario
                             .Include(u => u.UsuarioRoles)
                             .Include(u => u.TipoDocumentos)
                             .Where(y => y.Fk_Id_Empresa == usuarioActual.IdEmpresa).ToList();
                            //return View(usuariosL);
                            //List<Usuario> listUsuarioRolS = db.Tbl_Usuario.Where(ur => ur.Pk_Id_Usuario > 0).ToList();
                            return View("index", usuariosL);
                        }
                    }

                }

                var path = "";
                List<UsuarioRol> listUsuarioRol = new List<UsuarioRol>();
                foreach (int cc in Fk_Id_Rol)
                {
                    UsuarioRol userRol = new UsuarioRol();
                    userRol.Fk_Id_Rol = cc;
                    listUsuarioRol.Add(userRol);
                }
                if (Firma != null)
                {
                    if (Firma.ContentLength > 0)
                    {
                        if (Path.GetExtension(Firma.FileName).ToLower() == ".jpg"

                          || Path.GetExtension(Firma.FileName).ToLower() == ".png")
                        {
                            path = Path.Combine(Server.MapPath("~/Content/Images"), usuario.Numero_Documento + Firma.FileName);
                            Image img = RedimensionarImagen(Firma.InputStream);
                            //img.b
                            //img.SaveAs(path);
                            img.Save(path);
                            ViewBag.UploadSuccess = true;
                            usuario.Imagen_Firma = usuario.Numero_Documento + Firma.FileName;
                        }
                    }
                }
                usuario.Nombre_Usuario = usuario.Nombre_Usuario.ToUpper();
                usuario.nit_Empresa = usuarioActual.NitEmpresa;
                usuario.Fk_Id_Empresa = usuarioActual.IdEmpresa;
                usuario.UsuarioRoles = listUsuarioRol;
                if (ModelState.IsValid)
                {
                    db.Tbl_Usuario.Add(usuario);

                }
                db.SaveChanges();
                TempData["shortMessage"] = "Usuario Creado Satisfactoriamente.";
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View("Index");

            }

            ViewBag.Fk_Id_Rol = new SelectList(db.Tbl_Rol.Where(r => r.Fk_Id_Empresa == usuarioActual.IdUsuario && r.Descripcion == "REPRESENTANTE LEGAL" || r.Descripcion == "RESPONSABLE DE SGSST" || r.Descripcion == "PROFESIONAL SST"), "Pk_Id_Rol", "Descripcion", usuario.UsuarioRoles);
            ViewBag.Fk_Tipo_Documento = new SelectList(db.Tbl_TipoDocumentos, "PK_IDTipo_Documento", "Descripcion", usuario.Fk_Tipo_Documento);
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(int? id)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Tbl_Usuario.Include(p => p.UsuarioRoles).Where(x => x.Pk_Id_Usuario == id).FirstOrDefault();
            if(usuario == null)
            {
                return HttpNotFound();
            }
             var resulta = (from rol in db.Tbl_Rol
                           where (rol.Descripcion == "REPRESENTANTE LEGAL" || rol.Descripcion == "RESPONSABLE DE SGSST" || rol.Descripcion == "PROFESIONAL SST") & rol.Fk_Id_Empresa == usuarioActual.IdEmpresa
                            select rol).ToList();
            //ViewBag.Fk_Id_Rol = new SelectList(from rolls in db.Tbl_Rol.Where(r => r.Fk_Id_Empresa == usuarioActual.IdEmpresa)
            //                                   select new
            //                                   {
            //                                       Value = rolls.Pk_Id_Rol,
            //                                       Text  = rolls.Descripcion
            //                                   },
            //                                   "Value","Text",  usuario.Pk_Id_Usuario);

             usuario.SelectedRolCode = (from p in usuario.UsuarioRoles select p.Fk_Id_Rol).ToArray<int>();
             ViewBag.Fk_Id_Rol = new SelectList(resulta, "Pk_Id_Rol", "Descripcion",usuario.UsuarioRoles.First().Fk_Id_Rol );
            //, usuario.UsuarioRoles.FirstOrDefault().Fk_Id_Rol
            //usuario.SelectedRolCode = (from p in usuario.UsuarioRoles select p.Fk_Id_Rol).ToArray<int>();
            ViewBag.Fk_Tipo_Documento = new SelectList(db.Tbl_TipoDocumentos, "PK_IDTipo_Documento", "Descripcion", usuario.Fk_Tipo_Documento);
            ViewBag.firma = usuario.Imagen_Firma;
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
     //   [ValidateAntiForgeryToken]
        public ActionResult Edit(Usuario usuario, HttpPostedFileBase Firma, List<int> Fk_Id_Rol)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            var path = "";                                  
            List<UsuarioRol> usuarioRolList = db.Tbl_UsuarioRol.Where(l => l.Fk_Id_Usuario == usuario.Pk_Id_Usuario).ToList();           
            Usuario user = db.Tbl_Usuario.Find(usuario.Pk_Id_Usuario);
            usuario.UsuarioRoles = usuarioRolList;
            if (ModelState.IsValid)
            {
                if (Fk_Id_Rol != null)
                {
                    foreach (var item in usuario.UsuarioRoles)
                    {
                        db.Entry(item).State = EntityState.Deleted;
                    }
                    foreach (int stc in Fk_Id_Rol)
                    {
                        UsuarioRol userRol = new UsuarioRol();
                        userRol.Fk_Id_Rol = stc;
                        userRol.Fk_Id_Usuario = usuario.Pk_Id_Usuario;
                        db.Tbl_UsuarioRol.Add(userRol);
                    }
                    if (Firma != null)
                    {

                        if (Firma.ContentLength > 0)
                        {
                            if (Path.GetExtension(Firma.FileName).ToLower() == ".jpg"

                              || Path.GetExtension(Firma.FileName).ToLower() == ".png")
                            {
                                path = Path.Combine(Server.MapPath("~/Content/Images"), usuario.Numero_Documento + Firma.FileName);
                                Image img = RedimensionarImagen(Firma.InputStream);                                
                                img.Save(path);
                                ViewBag.UploadSuccess = true;
                                //Firma.SaveAs(path);
                                ViewBag.UploadSuccess = true;
                                user.Imagen_Firma = usuario.Numero_Documento + Firma.FileName;
                            }
                        }
                    }
                    user.Nombre_Usuario = usuario.Nombre_Usuario.ToUpper();
                    user.Numero_Documento = usuario.Numero_Documento;
                    user.Fk_Tipo_Documento = usuario.Fk_Tipo_Documento;
                    user.nit_Empresa = usuarioActual.NitEmpresa;
                    user.Fk_Id_Empresa = usuarioActual.IdEmpresa; 
                    db.Entry(user).State = EntityState.Modified;

                }
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.Fk_Id_Rol = new SelectList(db.Tbl_Rol.Where(r => r.Fk_Id_Empresa == usuarioActual.IdEmpresa), "Pk_Id_Rol", "Descripcion");
            ViewBag.Fk_Tipo_Documento = new SelectList(db.Tbl_TipoDocumentos, "PK_IDTipo_Documento", "Descripcion", usuario.Fk_Tipo_Documento);           
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(int? id)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Tbl_Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

   
        public ActionResult DeleteConfirmed(int id)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            Usuario usuario = db.Tbl_Usuario.Find(id);
            db.Tbl_Usuario.Remove(usuario);
            db.SaveChanges();
            List<Usuario> buscauser = db.Tbl_Usuario.Include(u => u.UsuarioRoles).Where(us => us.Pk_Id_Usuario != 0 & us.Fk_Id_Empresa==usuarioActual.IdEmpresa).ToList();
            ViewBag.mensaje = "Usuario " +usuario.Nombre_Usuario+ " Eliminado.!";
            return View("Index", buscauser);
           
        }

        public ActionResult GetImage(int Pk_Id_Usuario)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
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

        private static Image RedimensionarImagen(Stream stream)
        {
            
            // Se crea un objeto Image, que contiene las propiedades de la imagen
            Image img = Image.FromStream(stream);

            // Tamaño máximo de la imagen (altura o anchura)
            //const int max = 200;

            //int h = img.Height;
            //int w = img.Width;
            //int newH, newW;

            //if (h > w && h > max)
            //{
            //    // Si la imagen es vertical y la altura es mayor que max,
            //    // se redefinen las dimensiones.
            //    newH = max;
            //    newW = (w * max) / h;
            //}
            //else if (w > h && w > max)
            //{
            //    // Si la imagen es horizontal y la anchura es mayor que max,
            //    // se redefinen las dimensiones.
            //    newW = max;
            //    newH = (h * max) / w;
            //}
            //else
            //{
            //    newH = h;
            //    newW = w;
            //}
            //if (h != newH && w != newW)
            //{
            int newH  = 100;
            int newW = 250;
                // Si las dimensiones cambiaron, se modifica la imagen
            Bitmap newImg = new Bitmap(img, newW, newH);
            Graphics g = Graphics.FromImage(newImg);
            g.InterpolationMode =
                System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
            g.DrawImage(img, 0, 0, newImg.Width, newImg.Height);
            return newImg;
            //}
            //else
            //    return img;
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
