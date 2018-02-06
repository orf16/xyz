
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
using SG_SST.Services.General.IServices;
using SG_SST.Services.General.Services;
using SG_SST.Services.Empresas.IServices;
using SG_SST.Services.Empresas.Services;
using System.Configuration;
using RestSharp;
using SG_SST.Dtos.Empresas;
using SG_SST.Controllers.Base;
using SG_SST.Audotoria;

namespace SG_SST.Controllers.Empresas
{

    public class SedeController : BaseController
    {
        private SG_SSTContext db = new SG_SSTContext();
        private IRecursosServicios recursosServicios = new RecursosServicios();
        private ISedeServicios sedeServicios = new SedeServicios();
        

    
        // GET: Sedes
        public ActionResult Index()
        {

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            //ViewBag.Fk_Id_Departamento = new SelectList(db.Tbl_Departamento, "Pk_Id_Departamento", "Nombre_Departamento");
            return View(db.Tbl_Sede.Where(s =>s.Fk_Id_Empresa == usuarioActual.IdEmpresa).ToList());
        }

        public ActionResult SedesPorMunicipios()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            var ms = TempData["shortMessage"];
            if (ms != null)
            {
                ViewBag.mensaje = ms;
            }
            var ms1 = TempData["shortMessage1"];
            if (ms1 != null)
            {
                ViewBag.mensaje1 = ms1;
            }
            List<SedeMunicipio> sedesMunicipio = sedeServicios.SedesMunicipioPorEmpresa(usuarioActual.IdEmpresa);

            return View(sedesMunicipio);
           
        }


        // GET: Sedes/Details/5
        public ActionResult Details(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sede sede = db.Tbl_Sede.Find(id);
            if (sede == null)
            {
                return HttpNotFound();
            }
            ViewBag.Fk_Id_Departamento = new SelectList(db.Tbl_Departamento, "Pk_Id_Departamento", "Nombre_Departamento");
            return View(sede);

        }

        // GET: Sedes/Create
        public ActionResult Create()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            ViewBag.Fk_Id_Departamento = new SelectList(db.Tbl_Departamento, "Pk_Id_Departamento", "Nombre_Departamento");
            ViewBag.nitEmpresa = usuarioActual.NitEmpresa;
            return View();
        }

        [HttpPost]

        public ActionResult Create(Sede sede, SedeMunicipio sedePorMunicipio, List<CentroTrabajo> centro)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "Debe estar autenticado para poder continuar.";
                return View();
            }
            using (var tx = db.Database.BeginTransaction())
            {
                Empresa empresa = db.Tbl_Empresa.Find(usuarioActual.IdEmpresa);
                sede.Empresa = empresa;
                sede.Nombre_Sede = sede.Nombre_Sede.ToUpper();

                List<SedeMunicipio> buscarsede = db.Tbl_SedeMunicipio.Where(rs => rs.Sede.Fk_Id_Empresa == usuarioActual.IdEmpresa && rs.id_sedeMunicipio != null).ToList();
                if (buscarsede != null)
                {
                    List<SedeMunicipio> buscarsede1 = db.Tbl_SedeMunicipio.Where(rs => rs.Sede.Fk_Id_Empresa == usuarioActual.IdEmpresa && rs.Sede.Nombre_Sede == sede.Nombre_Sede).ToList();
                    if (buscarsede1.Count > 0)
                    {
                        TempData["shortMessage"] = "Sede Ingresada ya Existe.";
                    }
                    else
                    {
                        List<CentroTrabajo> centros = db.Tbl_Centro_de_Trabajo.Where(ct => ct.Sede.Fk_Id_Empresa == usuarioActual.IdEmpresa).ToList();
                  
                        if (ModelState.IsValid)
                        {
                            sede.SedeMunicipios = new List<SedeMunicipio>();
                            sede.SedeMunicipios.Add(sedePorMunicipio);
                            sede.CentrosTrabajo = centro;
                            db.Tbl_Sede.Add(sede);
                            db.SaveChanges();
                            tx.Commit();
                            TempData["shortMessage"] = "Sede Almacenada Correctamente.";
                            List<SedeMunicipio> buscarsedes = db.Tbl_SedeMunicipio.Where(rs => rs.Sede.Fk_Id_Empresa == usuarioActual.IdEmpresa && rs.id_sedeMunicipio != null).ToList();
                            ViewBag.mensaje = "La Sede: " + sede.Nombre_Sede + " Registrada con Exito.";
                            return RedirectToAction("SedesPorMunicipios", buscarsedes);
                        }
                    }

                }
            }
            return RedirectToAction("SedesPorMunicipios");
        }


        /// <summary>
        /// Metodo para eliminar una Actividad Economica Previamente Registrada.
        /// </summary>
        /// <param name="pkactividad"></param>
        /// <returns></returns>
        public ActionResult EliminarActividadEconomica(int pkactividad)
        { var actividad= new CentroTrabajo();
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarce para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            using (var Transaction = db.Database.BeginTransaction())
            {
                RegistraLog registraLog = new RegistraLog();
                try
                {
                    actividad = (from ae in db.Tbl_Centro_de_Trabajo
                                     where ae.Pk_Id_Centro_de_Trabajo == pkactividad
                                     select ae).SingleOrDefault();
                    if (actividad != null)
                    {
                        db.Tbl_Centro_de_Trabajo.Remove(actividad);
                    }
                    db.SaveChanges();
                    Transaction.Commit();
                }
                catch(Exception ex)
                {
                    registraLog.RegistrarError(typeof(SedeController), string.Format("Error al Eliminar la Actividad Economica  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                    Transaction.Rollback();
                }     
            }
            if(actividad!=null)
            {
                return Json(new { Data = actividad, Mesaje = "Ok" }, JsonRequestBehavior.AllowGet);             
            }
            else
            {
                return Json(new { Data = string.Empty, Mensaje = "ERROR" }, JsonRequestBehavior.AllowGet);
            }
        }
        
          
        
        // GET: Sedes/Edit/5
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
            Sede sede = db.Tbl_Sede.Find(id);
            if (sede == null)
            {
                return HttpNotFound();
            }          
            List<CentroTrabajo> centros = db.Tbl_Centro_de_Trabajo.Where(d => d.Fk_Id_Sede == sede.Pk_Id_Sede).ToList();
            List<Departamento> departamentos = recursosServicios.ObtenerDepartamentos();
            ViewBag.nitEmpresa = usuarioActual.NitEmpresa;
            SedeMunicipio sedeMunipio = sedeServicios.ObtenerSedePorMunicipio(sede.Pk_Id_Sede);
            ViewBag.Fk_Id_Departamento = new SelectList(departamentos, "Pk_Id_Departamento", "Nombre_Departamento", sedeMunipio.Municipio.Fk_Nombre_Departamento);
            List<Municipio> municipios = recursosServicios.ObtenetMunicipios(sedeMunipio.Municipio.Fk_Nombre_Departamento);
            ViewBag.Fk_Id_Municipio = new SelectList(municipios, "Pk_Id_Municipio", "Nombre_Municipio", sedeMunipio.Fk_Id_Municipio);
            ViewBag.Urbano = sede.Sector.Equals("Urbano") ? true : false;
            return View(sede);
        }

        // POST: Sedes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Pk_Id_Sede,Nombre_Sede,Direccion_Sede,Sector")] Sede sede, SedeMunicipio sedePorMunicipio, List<CentroTrabajo> centro, int pksede)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            sede.Nombre_Sede = sede.Nombre_Sede.ToUpper();
            sedeServicios = new SedeServicios(db);
            if (ModelState.IsValid)
            {
                using (var Transaction = db.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        List<CentroTrabajo> centros = db.Tbl_Centro_de_Trabajo.Where(ct => ct.Sede.Fk_Id_Empresa == usuarioActual.IdEmpresa && ct.Fk_Id_Sede == pksede).ToList();
                        if (centros != null)
                        {
                            if (centro != null)
                            {
                                var sw = false;
                                string centrosencontrados = "";
                                foreach (var sct in centros)
                                {
                                    for (int i = 0; i < centro.Count; i++)
                                    {
                                        if (sct.Codigo_Actividad == centro[i].Codigo_Actividad)
                                        {
                                            centrosencontrados += (centro[i].Codigo_Actividad.ToString() + " , ");
                                            sw = true;
                                        }
                                    }
                                }
                                if (sw != false)
                                {
                                    TempData["shortMessage1"] = "Actividad Economica: " + centrosencontrados + " ya fue registrada anteriormente a la sede, el registro no se realizo.";
                                    return RedirectToAction("SedesPorMunicipios");
                                }
                            }
                        }
                        //else
                        //{
                        SedeMunicipio sedeMunipio = sedeServicios.ObtenerSedePorMunicipio(sede.Pk_Id_Sede);
                        sede.Fk_Id_Empresa = usuarioActual.IdEmpresa;
                        if (sedeMunipio.Fk_Id_Municipio != sedePorMunicipio.Fk_Id_Municipio)
                        {
                            sedeMunipio.Fk_Id_Municipio = sedePorMunicipio.Fk_Id_Municipio;
                            sedeMunipio.Sede = sede;
                            db.Entry(sedeMunipio).State = EntityState.Modified;
                            List<CentroTrabajo> ActividadEcon = new List<CentroTrabajo>();
                            if (centro != null)
                            {

                                foreach (var ae in centro)
                                {

                                    CentroTrabajo axs = new CentroTrabajo();
                                    axs.Codigo_Actividad = ae.Codigo_Actividad;
                                    axs.Descripcion_Actividad = ae.Descripcion_Actividad;
                                    axs.Fk_Id_Sede = pksede;
                                    ActividadEcon.Add(axs);

                                }
                            }

                            db.Entry(sede).State = EntityState.Modified;
                            db.Tbl_Centro_de_Trabajo.AddRange(ActividadEcon);
                            TempData["shortMessage"] = "Informacion Registrada Correctamente.";
                        }
                        else
                        {
                            List<CentroTrabajo> ActividadEcon = new List<CentroTrabajo>();
                            sedeMunipio.Fk_Id_Municipio = sedePorMunicipio.Fk_Id_Municipio;
                            sedeMunipio.Sede = sede;
                            if (centro != null)
                            {
                                foreach (var ae in centro)
                                {
                                    CentroTrabajo axs = new CentroTrabajo();
                                    axs.Codigo_Actividad = ae.Codigo_Actividad;
                                    axs.Descripcion_Actividad = ae.Descripcion_Actividad;
                                    axs.Fk_Id_Sede = pksede;
                                    ActividadEcon.Add(axs);
                                }

                            }

                            db.Entry(sede).State = EntityState.Modified;
                            db.Tbl_Centro_de_Trabajo.AddRange(ActividadEcon);
                        }

                        db.SaveChanges();
                        Transaction.Commit();
                        TempData["shortMessage"] = "Informacion Registrada Correctamente.";

                       // }

                    }
                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(SedeController), string.Format("Error al realizar la transacción  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();

                    }
                }
                return RedirectToAction("SedesPorMunicipios");
            }
            return View(sede);
        }

        // GET: Sedes/Delete/5
        public ActionResult Delete(int? id)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            ViewBag.Fk_Id_Departamento = new SelectList(db.Tbl_Departamento, "Pk_Id_Departamento", "Nombre_Departamento");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sede sede = db.Tbl_Sede.Find(id);
            if (sede == null)
            {
                return HttpNotFound();
            }
            return View(sede);
        }


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

                ViewBag.Fk_Id_Departamento = new SelectList(db.Tbl_Departamento, "Pk_Id_Departamento", "Nombre_Departamento");
                Sede sede = db.Tbl_Sede.Find(id);
                db.Tbl_Sede.Remove(sede);
                db.SaveChanges();
                List<SedeMunicipio> buscarsedes = db.Tbl_SedeMunicipio.Where(rs => rs.Sede.Fk_Id_Empresa == usuarioActual.IdEmpresa && rs.id_sedeMunicipio != null).ToList();
                ViewBag.mensaje = "La Sede: " + sede.Nombre_Sede + " Eliminada.";
                return View("SedesPorMunicipios", buscarsedes);


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

        public ActionResult ObtenerSiarpCentro(string nitempresa)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            var datos = string.Empty;
            if (!string.IsNullOrEmpty(nitempresa))
            {
                var cliente = new RestSharp.RestClient(ConfigurationManager.AppSettings["Url"]);
                var request = new RestRequest(ConfigurationManager.AppSettings["consultaCentro"], RestSharp.Method.GET);
                request.RequestFormat = DataFormat.Xml;
                request.Parameters.Clear();
                request.AddParameter("tpDocEmp", "ni");
                request.AddParameter("docEmp", nitempresa);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Accept", "application/json");

                //se omite la validación de certificado de SSL
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

                IRestResponse<List<SiarpCentrosdeCostoDTO>> response = cliente.Execute<List<SiarpCentrosdeCostoDTO>>(request);
                var result = response.Content;
                var respuesta = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SiarpCentrosdeCostoDTO>>(result);
                return Json(new { Data = respuesta, Mensaje = "OK" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Data = datos, Mensaje = "OK" });
        }

    }
}
