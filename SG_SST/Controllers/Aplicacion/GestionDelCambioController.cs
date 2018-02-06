using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SG_SST.Logica.Aplicacion;
using System.Configuration;
using RestSharp;
using System.Net;
using SG_SST.Models;
using SG_SST.Models.Aplicacion;
using SG_SST.EntidadesDominio.Aplicacion;
using SG_SST.ServiceRequest;
using System.IO;
using System.Data.Entity;
using System.Data;
using System.Threading;
using iTextSharp.text;
using System.Data.Entity;
using System.Data;
using SG_SST.Controllers.Base;
using SG_SST.Models.Planificacion;
using SG_SST.EntidadesDominio.Aplicacion;


using SG_SST.Services.Planificacion.Services;
using SG_SST.Services.Planificacion.IServices;

namespace SG_SST.Controllers.Aplicacion
{
    public class GestionDelCambioController : BaseController
    {
        // GET: GestionDelCambio
        private SG_SSTContext db = new SG_SSTContext();
        string UrlServicioAplicacion = ConfigurationManager.AppSettings["UrlServicioAplicacion"];
        string CapacidadCargarGestionDelCambio = ConfigurationManager.AppSettings["CapacidadCargarGestionDelCambio"];
        private IPerfilSocioDemograficoServicios perfilservicio = new PerfilSocioDemograficoServicios();
        IClasificacionDePeligrosServicios clasificacionDePeligrosServicios = new ClasificacionDePeligrosServicios();


        string CapacidadeEliminarGestionDelCambio = ConfigurationManager.AppSettings["CapacidadeEliminarGestionDelCambio"];


        public ActionResult AgregarGestionDelCambio()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesion el sistema";
                return View();
            }

            ViewBag.FK_Tipo_De_Peligro = new SelectList(db.Tbl_Tipo_De_Peligro, "PK_Tipo_De_Peligro", "Descripcion_Del_Peligro");
            ViewBag.FK_Clasificacion_De_Peligro = new SelectList(db.Tbl_Clasificacion_De_Peligro, "PK_Clasificacion_De_Peligro", "Descripcion_Clase_De_Peligro");
            //ViewBag.FK_Id_Rol = new SelectList(db.Tbl_Rol, "PK_Id_Rol", "Descripcion");
            ViewBag.FK_Id_Rol = new SelectList(db.Tbl_Rol.Where(x => x.Fk_Id_Empresa == usuarioActual.IdEmpresa), "PK_Id_Rol", "Descripcion");
            return View();
        }

        SG_SSTContext dbReqLeg;
        public static int pk_ActividadEconomica;


        [HttpPost]

        public ActionResult GrabarGestionDelCambio(EDGestionDelCambio varperfilsocidemografico)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesion el sistema";
                return View();
            }
            //bool respuestaGuardado = perfilservicio.GrabarPerfilSocioDemografico(perfilsoc,pelsed,inflab);
            if (varperfilsocidemografico.FK_Tipo_De_Peligro == 8) {
                varperfilsocidemografico.FK_Clasificacion_De_Peligro = 46;           
            }
            varperfilsocidemografico.fkempresa = usuarioActual.IdEmpresa;
            ServiceClient.EliminarParametros();
            var result = ServiceClient.RealizarPeticionesPostJsonRestFul<EDGestionDelCambio>(UrlServicioAplicacion, CapacidadCargarGestionDelCambio, varperfilsocidemografico);
            ServiceClient.AdicionarParametro("idEmpresa", usuarioActual.IdEmpresa);
            ViewBag.FK_Id_Rol = new SelectList(db.Tbl_Rol, "PK_Id_Rol", "Descripcion");
            ViewBag.FK_Tipo_De_Peligro = new SelectList(db.Tbl_Tipo_De_Peligro, "PK_Tipo_De_Peligro", "Descripcion_Del_Peligro");
            ViewBag.FK_Clasificacion_De_Peligro = new SelectList(db.Tbl_Clasificacion_De_Peligro, "PK_Clasificacion_De_Peligro", "Descripcion_Clase_De_Peligro"); if (result!=null)
            {
                ViewBag.Messages2 = "Registro guardado correctamente";
            }
            else
            {
                ViewBag.Messages2 = "Se produjo un error al guardar el registro";

            }
            List<EDGestiondelcambioVista> Objlist_RequisitosLegales = new List<EDGestiondelcambioVista>();
            return View("ListadoGestionDelCambio", MostrarGestionDelCambioVista());
        }


        public ActionResult ListadoGestionDelCambio()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesion en el sistema";
            }
            List<EDGestiondelcambioVista> Objlist_RequisitosLegales = new List<EDGestiondelcambioVista>();         

            return View(MostrarGestionDelCambioVista());
        }


        public List<EDGestiondelcambioVista> MostrarGestionDelCambioVista()
        {

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesion en el sistema";
            }           
            List<EDGestiondelcambioVista> varmatreq = new List<EDGestiondelcambioVista>();
            List<EDGestiondelcambioVista> varmatreqOtro = new List<EDGestiondelcambioVista>();
            GestionDelCambio obj = new GestionDelCambio();
            using (var context = new SG_SSTContext())
            {

                varmatreq = (from a in context.Tbl_GestionDelCambio
                             join b in context.Tbl_Clasificacion_De_Peligro on
                             a.FK_Clasificacion_De_Peligro equals b.PK_Clasificacion_De_Peligro
                             join e in context.Tbl_Tipo_De_Peligro on
                             a.FK_Tipo_De_Peligro equals e.PK_Tipo_De_Peligro
                             join c in context.Tbl_Rol on
                             a.FK_Id_Rol equals c.Pk_Id_Rol                        

                             where a.FK_Empresa == usuarioActual.IdEmpresa
                             select new EDGestiondelcambioVista()
                             {
                                 PK_GestionDelCambio = a.PK_GestionDelCambio,
                                 Fecha = a.Fecha,
                                 DescripcionDeCambio = a.DescripcionDeCambio,
                                 RequisitoLegal = a.RequisitoLegal,
                                 Recomendaciones = a.Recomendaciones,
                                 FechaEjecucion = a.FechaEjecucion,
                                 FechaSeguimiento = a.FechaSeguimiento,
                                 Descripcion_Clase_De_Peligro = b.Descripcion_Clase_De_Peligro,
                                 Descripcion = c.Descripcion,
                                 Descripcion_Del_Peligro = e.Descripcion_Del_Peligro,
                                 Otro = a.Otro,
                                 fkClasificacionPeligro = a.FK_Clasificacion_De_Peligro,
                                 //fkempresa = a.FK_Empresa,
                                 //DescripcionOtro = b.Descripcion_Clase_De_Peligro
                             }).ToList();
                varmatreqOtro = (from a in context.Tbl_GestionDelCambio
                             join b in context.Tbl_Clasificacion_De_Peligro on
                             a.FK_Clasificacion_De_Peligro equals b.PK_Clasificacion_De_Peligro
                             join e in context.Tbl_Tipo_De_Peligro on
                             a.FK_Tipo_De_Peligro equals e.PK_Tipo_De_Peligro
                             join c in context.Tbl_Rol on
                             a.FK_Id_Rol equals c.Pk_Id_Rol
                             where a.FK_Empresa == usuarioActual.IdEmpresa
                             select new EDGestiondelcambioVista()
                             {
                                 PK_GestionDelCambio = a.PK_GestionDelCambio,
                                 Fecha = a.Fecha,
                                 DescripcionDeCambio = a.DescripcionDeCambio,
                                 RequisitoLegal = a.RequisitoLegal,
                                 Recomendaciones = a.Recomendaciones,
                                 FechaEjecucion = a.FechaEjecucion,
                                 FechaSeguimiento = a.FechaSeguimiento,
                                 Descripcion_Clase_De_Peligro = a.Otro,
                                 Descripcion = c.Descripcion,
                                 Descripcion_Del_Peligro = e.Descripcion_Del_Peligro,
                                 fkClasificacionPeligro = a.FK_Clasificacion_De_Peligro,                       

                             }).ToList();
                return varmatreq;
            }
        }


        public ActionResult EliminarGestionDelCambio(int idgestion)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesion en el sistema";
                return View();
            }
            ServiceClient.EliminarParametros();
            var resultEmpEval = ServiceClient.ObtenerObjetoJsonRestFul<bool>(UrlServicioAplicacion, CapacidadeEliminarGestionDelCambio, idgestion);
            return Json(new { Data = resultEmpEval, Mensaje = "OK" }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Eliminargestcabscript(Int32[] idgestion)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesion en el sistema";
                return View();
            }
            foreach (Int32 PK_Matriz in idgestion)
            {
                ServiceClient.EliminarParametros();
                ServiceClient.AdicionarParametro("PK_Matriz", PK_Matriz);
                var resultEmpEval = ServiceClient.ObtenerObjetoJsonRestFul<bool>(UrlServicioAplicacion, CapacidadeEliminarGestionDelCambio, RestSharp.Method.DELETE);

            }
            db.SaveChanges();
            return Json(new { success = true, Mensaje = "OK" }, JsonRequestBehavior.AllowGet);
        }



        public ActionResult ModificarGestioDelCambio(int PKGestionDelCambio)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesion en el sistema";
                return View();
            }
            GestionDelCambio objgestcambio = db.Tbl_GestionDelCambio.Where(g => g.PK_GestionDelCambio == PKGestionDelCambio).FirstOrDefault();
            ViewBag.PK_GestionDelCambio = objgestcambio.PK_GestionDelCambio;           
            GestionDelCambio gestcam = db.Tbl_GestionDelCambio.Find(PKGestionDelCambio);
             ViewBag.FK_Id_Rol = new SelectList(db.Tbl_Rol.Where(x => x.Fk_Id_Empresa == usuarioActual.IdEmpresa), "PK_Id_Rol", "Descripcion", gestcam.FK_Id_Rol);
            int tipoPeligro = gestcam.FK_Tipo_De_Peligro;
            ViewBag.FK_Tipo_De_Peligro = new SelectList(db.Tbl_Tipo_De_Peligro.Where(x => x.Descripcion_Del_Peligro.Length > 0), "PK_Tipo_De_Peligro", "Descripcion_Del_Peligro", gestcam.FK_Tipo_De_Peligro);
            List<ClasificacionDePeligro> clasesDePeligrosList = clasificacionDePeligrosServicios.ConsultarClasesDePeligros(tipoPeligro);
            ViewBag.FK_Clasificacion_De_Peligro = new SelectList(clasesDePeligrosList, "PK_Clasificacion_De_Peligro", "Descripcion_Clase_De_Peligro", gestcam.FK_Clasificacion_De_Peligro);
            return View(objgestcambio);
        }        
    
        public ActionResult BuscarClasificacionPeligroDescripcion(int PKGestionDelCambio)
        {            
            var varper = new EDBusqueTipoPeligro();    

            using (var context = new SG_SSTContext())
            {
                varper = (from d in context.Tbl_GestionDelCambio
                          join a in context.Tbl_Tipo_De_Peligro on
                          d.FK_Tipo_De_Peligro equals a.PK_Tipo_De_Peligro
                          join b in context.Tbl_Clasificacion_De_Peligro on
                          a.PK_Tipo_De_Peligro equals b.FK_Tipo_De_Peligro
                          where d.PK_GestionDelCambio == PKGestionDelCambio
                          select new EDBusqueTipoPeligro()
                          {
                              Descripcion_Clase_De_Peligro = b.Descripcion_Clase_De_Peligro,
                              Descripcion_Del_Peligro = a.Descripcion_Del_Peligro
                          }).FirstOrDefault();          

                    return Json(new { Municipio = varper }
                , JsonRequestBehavior.AllowGet);             
            }
        }
          


    }
}