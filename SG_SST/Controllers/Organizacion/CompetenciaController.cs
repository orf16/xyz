
namespace SG_SST.Controllers.Organizacion
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
    using SG_SST.Models.Empleado;
    using System.Threading;
    using System.Configuration;
    using RestSharp;
    using System.IO;
    using SG_SST.Dtos.Organizacion;
    using SG_SST.Services.Empresas.IServices;
    using SG_SST.Services.Empresas.Services;
    using LinqToExcel;
    using SG_SST.Models.Organizacion;
    using System.Text;
    using SG_SST.Controllers.Base;
    using System.Collections;
    using SG_SST.Repositories.Organizacion.IRepositories;
    using SG_SST.Repositories.Organizacion.Repositories;
    public class CompetenciaController : BaseController
    {
        private SG_SSTContext db = new SG_SSTContext();
        private ICompetenciaRepositorio CompetenciaRepositorio = new CompetenciaRepositorio();
        /// <summary>
        /// Consulta y envia  la información de los roles, cargos, y tematicas.
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateTematica()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            var SessionEmp = usuarioActual.IdEmpresa;
            ViewBag.Fk_Id_Rol = new SelectList(CompetenciaRepositorio.ObtenerRolesLibres(SessionEmp), "Pk_Id_Rol", "Descripcion");
            var cargosEmpresa = CompetenciaRepositorio.ObtenerCargos(usuarioActual.NitEmpresa);
            if (cargosEmpresa == null)
            {
                bool respuesta = true;
                ViewBag.cargosEmpresa = respuesta;
                //ViewBag.Fk_Id_Cargo = new SelectList(CompetenciaRepositorio.ObtenerCargos(usuarioActual.NitEmpresa), "Pk_Id_Cargo", "Nombre_Cargo");
                ViewBag.Fk_Id_Cargo = new SelectList("", "");
                ViewBag.Tematicas = CompetenciaRepositorio.ObtenerTematicaPosipedia();
                ViewBag.TematicaEmpresa = CompetenciaRepositorio.ObtenerTematicaEmpresa(SessionEmp);
                //ViewBag.Fk_id_Tematica3 = db.Tbl_Tematica.Where(z => z.TipoTematica == 2);
                ViewBag.TematicaEmpresaSel = "";
                ViewBag.TematicaSel = "";
                ViewBag.CargoSel = new List<CargoPorRol>();
                ViewBag.Editar = 0;
                //SelectList RolSel = new SelectList(db.Tbl_Rol.Where(x => x.Fk_Id_Empresa == usuarioActual.IdEmpresa && x.CargoPorRol.Count == 0), "Pk_Id_Rol", "Descripcion");
                //SelectList RolSel = new SelectList(CompetenciaRepositorio.ObtenerRolesLibres(SessionEmp), "Pk_Id_Rol", "Descripcion");
                ViewBag.RolSel = new SelectList(CompetenciaRepositorio.ObtenerRolesLibres(SessionEmp), "Pk_Id_Rol", "Descripcion"); 
                return View();
                //return RedirectToAction("Index", "Home");
            }
            ViewBag.Fk_Id_Cargo = new SelectList(cargosEmpresa, "Pk_Id_Cargo", "Nombre_Cargo");
            ViewBag.Tematicas = CompetenciaRepositorio.ObtenerTematicaPosipedia();
            ViewBag.TematicaEmpresa = CompetenciaRepositorio.ObtenerTematicaEmpresa(SessionEmp);
            //ViewBag.Fk_id_Tematica3 = db.Tbl_Tematica.Where(z => z.TipoTematica == 2);
            ViewBag.TematicaEmpresaSel = "";
            ViewBag.TematicaSel = "";
            ViewBag.CargoSel = new List<CargoPorRol>();
            ViewBag.Editar = 0;
           // SelectList RolSel = new SelectList(db.Tbl_Rol.Where(x => x.Fk_Id_Empresa == usuarioActual.IdEmpresa && x.CargoPorRol.Count == 0), "Pk_Id_Rol", "Descripcion");
            SelectList RolSel = new SelectList(CompetenciaRepositorio.ObtenerRolesLibres(SessionEmp), "Pk_Id_Rol", "Descripcion");
            ViewBag.RolSel = RolSel;
            return View();
        }

        /// <summary>
        /// Metodo para guardar o editar  las competencias
        /// </summary>
        /// <param name="Fk_Id_Rol", name="Fk_Id_Cargo", name="idEmpleados", name="Id_TematicaPos", name="editar"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AsignarCompetencia(List<int> Fk_Id_Rol, List<int> Fk_Id_Cargo,
         string idEmpleados, List<Tematica> Id_TematicaPos, int editar)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            string mens = "";
            string tip = "";
            var SessionEmp = usuarioActual.IdEmpresa;
            bool restpuestaGuardado = CompetenciaRepositorio.GrabarCompetencia(Fk_Id_Rol, Fk_Id_Cargo, idEmpleados, Id_TematicaPos, editar, SessionEmp);
            if (restpuestaGuardado == true)
            {
                mens = "Competencia asignada correctamente";
                tip = "success";
            }
            else
            {
                mens = "No se guardo la competencia";
                tip = "error";
            }
            return RedirectToAction("ViewTematica", new { mensaje = mens, tipo = tip });

        }
        /// <summary>
        /// Para consultar las competencias asignadas por rol
        /// </summary>
        /// <param name="rol"></param>
        /// <returns>Competencias</returns>
        public ActionResult BuscarCompetenciaPorRol(int rol)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            int SessionEmp = usuarioActual.IdEmpresa;
            List<Rol> Rol = CompetenciaRepositorio.ObtenerRol(rol,SessionEmp);
            List<RolPorTematica> RolPorTematicaList = CompetenciaRepositorio.ObtenerRolPorTematicaPorRol(rol);
            List<CargoPorRol> CargoPorRolList = CompetenciaRepositorio.ObtenerCargoPorRolPorRol(rol);
            if (Rol.FirstOrDefault().RolPorTematica.Count() == 0)
            {
                return null;
            }
            return PartialView("RolPorTematicaVP", Rol);
        }
        /// <summary>
        /// Para consultar las competencias asignadas por cargo
        /// </summary>
        /// <param name="cargo"></param>
        /// <returns>Competencias</returns>
        public ActionResult BuscarCompetenciaPorCargo(int cargo)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            int SessionEmp = usuarioActual.IdEmpresa;
            List<CargoPorRol> CargoPorRolList = CompetenciaRepositorio.ObteneCargoPorRolPorCargo(cargo);
            List<Rol> Roles = CompetenciaRepositorio.ObtenerRolPorCargo(cargo,SessionEmp);
            List<RolPorTematica> RolPorTematicaList = CompetenciaRepositorio.ObtenerRolPorTematicaPorCargo(cargo);
            if (RolPorTematicaList.Count() == 0)
            {
                return null;
            }
            return PartialView("RolPorTematicaVP", Roles);
        }
        /// <summary>
        /// Para consultar las competencias asignadas por rol y cargo
        /// </summary>
        /// <param name="rol" name="cargo"></param>
        /// <returns>Competencias</returns>
        public ActionResult BuscarCompetenciaPorRolCargo(int rol, int cargo)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            List<CargoPorRol> CargoPorRolList = CompetenciaRepositorio.ObteneCargoPorRolPorRolCargo(rol, cargo);
            List<Rol> Roles = CompetenciaRepositorio.ObtenerRolPorRolCargo(rol, cargo);
            List<RolPorTematica> RolPorTematicaList = CompetenciaRepositorio.ObtenerRolPorTematicaPorRolCargo(rol, cargo);
            if (RolPorTematicaList.Count() == 0)
            {
                return null;
            }
            return PartialView("RolPorTematicaVP", Roles);
        }

        /// <summary>
        /// Para consultar las competencias asignadas por tematica
        /// </summary>
        /// <param name="idbusqueda"></param>
        /// <returns>Competencias</returns>
        public ActionResult BuscarCompetenciaPorTema(int idbusqueda)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            int SessionEmp = usuarioActual.IdEmpresa;
            List<RolPorTematica> Temat = CompetenciaRepositorio.ObtenerRolPorTematicaPorTematica(idbusqueda, SessionEmp);
            if (Temat.Count() == 0)
            {
                return null;
            }
            return PartialView("CompetenciaPorTematicaVP", Temat);
        }

        /// <summary>
        /// Para consultar las competencias asignadas por tematica y rol
        /// </summary>
        /// <param name="idbusqueda", name="rol"></param>
        /// <returns>Competencias</returns>
        public ActionResult BuscarCompetenciaPorTemaYRol(int idbusqueda, int rol)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            List<RolPorTematica> Temat = CompetenciaRepositorio.ObtenerRolPorTematicaPorTematicaRol(idbusqueda, rol);
           // List<RolPorTematica> RolPorTematicaList = db.Tbl_Rol_Por_Tematica.Where(s => s.Fk_Id_Rol == rol && s.Fk_Id_Tematica == idbusqueda).ToList();
            if (Temat.Count() == 0)
            {
                return null;
            }
            return PartialView("CompetenciaPorTematicaVP", Temat);
        }


        /// <summary>
        /// Para consultar las competencias asignadas por tematica, rol y cargo
        /// </summary>
        /// <param name="idbusqueda", name="rol", name="cargo"></param>
        /// <returns>Competencias</returns>
        public ActionResult BuscarCompetenciaPorTemaRolCargo(int idbusqueda, int rol, int cargo)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            List<RolPorTematica> Temat =CompetenciaRepositorio.ObtenerRolPorTematicaPorTematicaRol(idbusqueda,rol);
            List<RolPorTematica> RolPorTematicaList = CompetenciaRepositorio.ObtenerRolPorTematicaPorTematicaRol(idbusqueda, rol);
            List<CargoPorRol> CargoPorRolList = CompetenciaRepositorio.ObteneCargoPorRolPorRolCargo(rol, cargo);
            if (Temat.Count() == 0)
            {
                return null;
            }
            return PartialView("CompetenciaPorTematicaVP", Temat);
        }
        /// <summary>
        /// Para consultar las competencias asignadas por tematica y cargo
        /// </summary>
        /// <param name="idbusqueda", name="cargo"></param>
        /// <returns>Competencias</returns>
        public ActionResult BuscarCompetenciaPorTemaYCargo(int idbusqueda, int cargo)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            int SessionEmp = usuarioActual.IdEmpresa;
            List<CargoPorRol> CargoPorRolList = CompetenciaRepositorio.ObteneCargoPorRolPorCargo(cargo);
            List<Rol> Roles = CompetenciaRepositorio.ObtenerRolPorCargo(cargo, SessionEmp);
            List<RolPorTematica> RolPorTematicaList = CompetenciaRepositorio.ObtenerRolPorTematicaPorTemaCargo(idbusqueda, cargo,SessionEmp);
            if (RolPorTematicaList.Count() == 0)
            {
                return null;
            }
            return PartialView("CompetenciaPorTematicaVP", RolPorTematicaList);
        }
        /// <summary>
        ///  Consulta y envia  la información de los roles, cargos a la vista inicial de consultas.
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        public ActionResult MostrarTematica()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            int SessionEmp = usuarioActual.IdEmpresa;
            ViewBag.Fk_Id_Rol = new SelectList(CompetenciaRepositorio.ObtenerRoles(SessionEmp), "Pk_Id_Rol", "Descripcion");
            var cargosPorEmpresa = CompetenciaRepositorio.ObtenerCargos(usuarioActual.NitEmpresa);
            if(cargosPorEmpresa == null)
            {
                bool respuesta = true;
                ViewBag.cargosPorEmpresa = respuesta;
                ViewBag.Fk_Id_Rol = new SelectList(CompetenciaRepositorio.ObtenerRoles(SessionEmp), "Pk_Id_Rol", "Descripcion");
                ViewBag.Fk_Id_Cargo = new SelectList("", "");
                return View("ViewCompetencia");
            }
            ViewBag.Fk_Id_Cargo = new SelectList(cargosPorEmpresa, "Pk_Id_Cargo", "Nombre_Cargo");
            return View("ViewCompetencia");
        }

        /// <summary>
        /// Para aceptar el certificado de seguridad del ftp de Posipedia
        /// </summary>
        /// <param></param>
        /// <returns>true</returns>

        public static bool AcceptAllCertifications(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certification, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        /// <summary>
        /// Para consultar e ingresar las tematicas desde el FTP, de posipedia, se eliminan las tematicas anteriores que no se esten utilizando
        /// Este método se ejecuta automaticamente desde Hangfire,
        /// </summary>
        /// <param></param>
        /// <returns>Competencias</returns>
        public void LeerExcel()
        {
            //TipoTematica 1= Tematica activa de posipedia, 2= Tematica empresa, 3= Tematica inactiva posipedia
            var fileName = "FormaciónEducaVirtualPresencial.xlsx";
            var filePath = Path.Combine(HttpRuntime.AppDomainAppPath, "Content/ArchivosDocumentacion/"+fileName);
            var serverPath = "ftp://ftp_manuelaB@190.26.216.110:990//FormaciónEducaVirtualPresencial.xlsx";
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(serverPath);
            request.KeepAlive = true;
            request.UsePassive = true;
            request.UseBinary = true;
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.Credentials = new NetworkCredential("ftp_manuelaB", "f7hUwGQs");
            request.EnableSsl = true;
            // Read the file from the server & write to destination   
            ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);
            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse()) // Error here
            using (Stream responseStream = response.GetResponseStream())
            {
                int bytesRead = 0;
                byte[] buffer = new byte[2048];
                FileStream fileStream = new FileStream(filePath, FileMode.Create);
                while (true)
                {
                    bytesRead = responseStream.Read(buffer, 0, buffer.Length);

                    if (bytesRead == 0)
                        break;

                    fileStream.Write(buffer, 0, bytesRead);
                }
                fileStream.Close();
            }

            var tematica =from a in db.Tbl_Tematica
                            where (a.TipoTematica!=2 && !db.Tbl_Rol_Por_Tematica.Select(b => b.Fk_Id_Tematica).Contains(a.Id_Tematica)) select a;

            if (tematica != null)
            {
                List<Tematica> tema = tematica.ToList();
                db.Tbl_Tematica.RemoveRange(tema);
            }
            db.SaveChanges();
            var tematicaPosipedia = from a in db.Tbl_Tematica
                           where (a.TipoTematica != 2)
                           select a;
            //creamos el libro a partir de la ruta
            var Book = new ExcelQueryFactory(filePath);

            //Consulta con Linq
            var resp = (from row in Book.Worksheet("Hoja1")
                        let item = new Tematica
                        {
                            //Id_Tematica = row[0].Cast<string>();
                            Area = row[0].Cast<string>(),
                            Tematicas = row[1].Cast<string>(),
                            Diseno = row[2].Cast<string>(),
                            Objetivo = row[3].Cast<string>(),
                            DirigidoA = row[4].Cast<string>(),  
                            TipoTematica = 1,
                            NombreDocumento = null,
                            SesionEmpresa = null,
                        }
                        select item).ToList();
            Book.Dispose();
            foreach (Tematica p in tematicaPosipedia)
            {
                bool activarTematica = false;
                foreach (Tematica archivo in resp.ToList())
                {
                    if (p.Tematicas.Equals(archivo.Tematicas) && !activarTematica)
                    {
                        resp.Remove(archivo);
                        activarTematica = true;
                    }
                }
                if(activarTematica)
                    p.TipoTematica = 1;
                else
                    p.TipoTematica = 3;
            }

            db.Tbl_Tematica.AddRange(resp);
            db.SaveChanges();


        }

        /// <summary>
        /// Para consultar los empleados desde SIARP, de acuerdo al cargo seleccionado
        /// </summary>
        /// <param name="cargo"></param>
        /// <returns>EmpleadosWSDTO</returns>
        public ActionResult ObtenerSiarpAfilidos(List<string> cargo)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            var datos = string.Empty;
            if (!string.IsNullOrEmpty(usuarioActual.NitEmpresa) && cargo != null)
            {
                var cliente = new RestSharp.RestClient(ConfigurationManager.AppSettings["Url"]);
                var request = new RestRequest(ConfigurationManager.AppSettings["consultaAfiliados"], RestSharp.Method.GET);
                request.RequestFormat = DataFormat.Xml;
                request.Parameters.Clear();
                request.AddParameter("tpEm", "NI");
                request.AddParameter("docEm", usuarioActual.NitEmpresa);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Accept", "application/json");

                //se omite la validación de certificado de SSL
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                IRestResponse<List<EmpleadosWSDTO>> response = cliente.Execute<List<EmpleadosWSDTO>>(request);
                var result = response.Content;
                var respuesta = Newtonsoft.Json.JsonConvert.DeserializeObject<List<EmpleadosWSDTO>>(result);
                List<EmpleadosWSDTO> Filtrocargos = new List<EmpleadosWSDTO>();
                if(respuesta.Count() <= 0)
                {
                    return Json(new {  Mensaje = "No hay conexión con el servidor" }, JsonRequestBehavior.AllowGet);
                }
                foreach (var crg in cargo)
                {
                    List<EmpleadosWSDTO> FiltrocargosTemporal = new List<EmpleadosWSDTO>();
                    FiltrocargosTemporal = respuesta.Where(x => x.cargo == crg).ToList();
                    Filtrocargos.AddRange(FiltrocargosTemporal);
                }

                return Json(new { Data = Filtrocargos, Mensaje = "OK" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Data = datos, Mensaje = "OK" });
        }

        /// <summary>
        /// Para obtener el archivo pdf que se va a asignar a la temática
        /// </summary>
        /// <param name="archivo"></param>
        /// <returns></returns>
        public ActionResult ObtenerArchivo(HttpPostedFileBase archivo)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            var path = "";
            if (archivo != null)
            {
                if (archivo.ContentLength > 0)
                {
                    string nomb = archivo.FileName;
                    if (Path.GetExtension(archivo.FileName).ToLower() == ".pdf")
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath("~/Content/ArchivosDocumentacion/" + usuarioActual.NitEmpresa ));
                        path = Path.Combine(Server.MapPath("~/Content/ArchivosDocumentacion/" + usuarioActual.NitEmpresa+"/"), archivo.FileName);
                        archivo.SaveAs(path);
                        ViewBag.UploadSuccess = true;
                        return Content(nomb);
                    }
                }
                return View();
            }
            else
            {
                return Content("");
            }
        }

        /// <summary>
        /// Para guardar la temática creada por el usuario
        /// </summary>
        /// <param name="tematicasEmp", name="res"></param>
        /// <returns>tematicas</returns>

        public ActionResult GuardarTematicaE(string tematicasEmp, string res)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            Tematica tematic = new Tematica();
            tematic.Tematicas = tematicasEmp;
            tematic.TipoTematica = 2;
            if (res == null)
            {
                tematic.NombreDocumento = null;
            }
            else
            {
                tematic.NombreDocumento = res;
            }
            int SessionEmp= usuarioActual.IdEmpresa;
            List<Tematica> temExist =CompetenciaRepositorio.ObtenerTematicaEmpresa(SessionEmp);
            foreach (var tem in temExist) {
                if (tem.Tematicas.Equals(tematic.Tematicas))
                    return Json("", JsonRequestBehavior.AllowGet);
            }

            List<Tematica> tematicas = CompetenciaRepositorio.GuardarTematicaE(tematic, SessionEmp);
            if (tematicas.Count != 0)
            {
                return Json(
                   tematicas.Select(tematica => new
                   {
                       Id_Tematica = tematica.Id_Tematica,
                       Descripcio_Tematicas = tematica.Tematicas,
                       NombreDocumento = tematica.NombreDocumento

                   })
                , JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// Para buscar las tematicas que se ingresaron desde posipedia de acuerdo al texto ingresado por el usuario
        /// </summary>
        /// <param name="Busqueda"></param>
        /// <returns>tematicas</returns>
        public ActionResult BusquedaTematica(string Busqueda)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            List<Tematica> tematicas = CompetenciaRepositorio.ObtenerTematica(Busqueda);
            if (tematicas.Count != 0)
            {
                return Json(
                   tematicas.Select(tematica => new
                   {
                       Id_Tematica = tematica.Id_Tematica,
                       Descripcio_Tematicas = tematica.Tematicas,
                   })
                , JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Para buscar las tematicas que se ingresaron por el usuario de acuerdo al texto ingresado por el usuario
        /// </summary>
        /// <param name="Busqueda"></param>
        /// <returns>tematicas</returns>
        public ActionResult BusquedaTematicaEmp(string Busqueda)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            int SessionEmp = usuarioActual.IdEmpresa; ;
            List<Tematica> tematicas = CompetenciaRepositorio.ObtenerTematicaEmp(Busqueda, SessionEmp);
            if (tematicas.Count != 0)
            {
                return Json(
                   tematicas.Select(tematica => new
                   {
                       Id_Tematica = tematica.Id_Tematica,
                       Descripcio_Tematicas = tematica.Tematicas,
                       NombreDocumento = tematica.NombreDocumento

                   })
                , JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// Para buscar las tematicas que se ingresaron por el usuario 
        /// </summary>
        /// <param name="Busqueda"></param>
        /// <returns>tematicas</returns>
        public ActionResult TematicaEmp()
        {
            var data = db.Tbl_Tematica.Where(z => z.TipoTematica == 2);
            return Json(new { data = data }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Para buscar las tematicas de acuerdo al texto ingresado por el usuario
        /// </summary>
        /// <param name="prefijo"></param>
        /// <returns>tematicas</returns>
        [HttpPost]
        public ActionResult BuscarCompetenciaPorTematica(string prefijo)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            int SessionEmp = usuarioActual.IdEmpresa;
            List<Tematica> data = CompetenciaRepositorio.ObtenerCompetenciaPorTematica(prefijo, SessionEmp);
            if (data.Count != 0)
            {
                return Json(
                   data.Select(tematica => new
                   {
                       Id_Tematica = tematica.Id_Tematica,
                       Descripcio_Tematicas = tematica.Tematicas,
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
        /// <summary>
        /// Para mostrar el archivo pdf que tiene adjunto las tematicas ingresadas por el usuario
        /// </summary>
        /// <param name="Id_Tematica"></param>
        /// <returns>tematicas</returns>
        public FileStreamResult MostrarTematicaPDF(int Id_Tematica)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
            }
            string nombreArchivo = db.Tbl_Tematica.Find(Id_Tematica).NombreDocumento;
            if (nombreArchivo == null || nombreArchivo == "")
            {
                ViewBag.Messages = "La temática no tiene documento asociado";
                return null;
            }
            var path = Server.MapPath("~/Content/ArchivosDocumentacion/" + usuarioActual.NitEmpresa + "/");
            var fullPath = Path.Combine(path, nombreArchivo);
            if (System.IO.File.Exists(fullPath))
            {
                FileStream fs = new FileStream(fullPath, FileMode.Open, FileAccess.Read);
                return File(fs, "application/pdf");
            }
            else
            {
                ViewBag.Messages = "La temática no tiene documento asociado";
                return null;
            }


        }
        /// <summary>
        /// Para eliminar las competencias desde la vista de consulta de competencias
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public ActionResult DeleteConfirmed(int id)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            using (SG_SSTContext datos = new SG_SSTContext())
            {
                bool eliminar = CompetenciaRepositorio.EliminarCompetencia(id);
                int SessionEmp = usuarioActual.IdEmpresa;
                if (eliminar)
                {
                    ViewBag.mensaje = "La competencia ha sido eliminada.";
                }
                else
                {
                    ViewBag.Messages = "La competencia no  ha sido eliminada.";
                }
                ViewBag.Fk_Id_Rol = new SelectList(CompetenciaRepositorio.ObtenerRoles(SessionEmp), "Pk_Id_Rol", "Descripcion");
                ViewBag.Fk_Id_Cargo = new SelectList(CompetenciaRepositorio.ObtenerCargos(usuarioActual.NitEmpresa), "Pk_Id_Cargo", "Nombre_Cargo");

                return View("ViewCompetencia");
            }
        }

        /// <summary>
        /// Para editar las competencias desde la vista de consulta de competencias
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult EditTematica(int id)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            int SessionEmp = usuarioActual.IdEmpresa;
            ViewBag.Fk_Id_Rol = id;
            ViewBag.CargoSel = CompetenciaRepositorio.ObtenerCargosSeleccionados(id);
            ViewBag.RolSel = CompetenciaRepositorio.ObtenerRolesLibresEditar(id,SessionEmp);
            ViewBag.Fk_Id_Cargo = new SelectList(CompetenciaRepositorio.ObtenerCargos(usuarioActual.NitEmpresa), "Pk_Id_Cargo", "Nombre_Cargo");
            ViewBag.Tematicas = CompetenciaRepositorio.ObtenerTematicaPosipedia();
            ViewBag.TematicaEmpresa = CompetenciaRepositorio.ObtenerTematicaEmpresa(SessionEmp);
            ViewBag.TematicaEmpresaSel = CompetenciaRepositorio.ObtenerTematicaEmpresaSeleccionadas(id);
            ViewBag.TematicaSel = CompetenciaRepositorio.ObtenerTematicaPosipediaSeleccionadas(id);
            List<EmpleadoPorTematica> EmpleadoPorTematica = CompetenciaRepositorio.ObtenerEmpleadoPorRol(id);
            String idEmpleados = "";
            foreach (var empleado in EmpleadoPorTematica)
            {
                idEmpleados += empleado.EmpleadoTematica.Numero_Documento.ToString() + ",";
            }
            ViewBag.idEmpleados = idEmpleados;
            //ViewBag.Fk_id_Tematica3 = db.Tbl_Tematica.Where(z => z.TipoTematica == 2);
            ViewBag.Editar = id;
            return View("CreateTematica");
        }

        /// <summary>
        /// Para mostrar la ventana inicial luego de ingresar o editar las competencias
        /// </summary>
        /// <param name="id"></param>
        /// <returns>tematicas</returns>
        public ActionResult ViewTematica(String mensaje, String tipo)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (mensaje != "" && tipo =="error")
                ViewBag.Mensaje = mensaje;
            else if (mensaje != "" &&   tipo =="success")
                ViewBag.Messages = mensaje;
            int SessionEmp = usuarioActual.IdEmpresa;
            ViewBag.Fk_Id_Rol = new SelectList(CompetenciaRepositorio.ObtenerRolesLibres(SessionEmp), "Pk_Id_Rol", "Descripcion");
            ViewBag.Fk_Id_Cargo = new SelectList(CompetenciaRepositorio.ObtenerCargos(usuarioActual.NitEmpresa), "Pk_Id_Cargo", "Nombre_Cargo");
            ViewBag.Tematicas = CompetenciaRepositorio.ObtenerTematicaPosipedia();
            ViewBag.TematicaEmpresa = CompetenciaRepositorio.ObtenerTematicaEmpresa(SessionEmp);
            //ViewBag.Fk_id_Tematica3 = db.Tbl_Tematica.Where(z => z.TipoTematica == 2);
            ViewBag.TematicaEmpresaSel = "";
            ViewBag.TematicaSel = "";
            ViewBag.Editar = 0;
            ViewBag.CargoSel = new List<CargoPorRol>();
            SelectList RolSel = new SelectList(CompetenciaRepositorio.ObtenerRolesLibres(SessionEmp), "Pk_Id_Rol", "Descripcion");
            ViewBag.RolSel = RolSel;
            return View("CreateTematica");
        }
    }
}
