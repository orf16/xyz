using SG_SST.Controllers.Base;
using SG_SST.Models;
using SG_SST.Models.Comunicaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace SG_SST.Controllers.Comunicaciones
{
    public class ComunicacionesInternasController : BaseController
    {
        private SG_SSTContext db = new SG_SSTContext();

        public ActionResult Index() {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
                return View();
            }

            return View();
        }

        [HttpGet]
        [ValidateInput(false)]
        public JsonResult GuardarEncuesta(int PK_Id_Encuesta, string Titulo, string CuerpoEncuesta, string EstadoEncuesta) 
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            var encuesta = db.Tbl_ComunicacionesInternas.Where(x => x.PK_Id_Encuesta == PK_Id_Encuesta).SingleOrDefault();
            string FechaEnvio = string.Empty;
            using (var Transaction = db.Database.BeginTransaction())
            {
            
            if (encuesta != null)
            {
                encuesta.Titulo = Titulo;
                encuesta.CuerpoHTML = CuerpoEncuesta;
                encuesta.FechaCreacion = DateTime.Now.ToString();
                encuesta.FechaEnvio = DateTime.Now.ToString();
                encuesta.EstadoEncuesta = "Enviado";
                db.Tbl_ComunicacionesInternas.Attach(encuesta);
                var entry = db.Entry(encuesta);
                entry.State = System.Data.Entity.EntityState.Modified;
                entry.Property(x => x.Titulo).IsModified = true;
                entry.Property(x => x.CuerpoHTML).IsModified = true;
                entry.Property(x => x.FechaCreacion).IsModified = true;
                entry.Property(x => x.FechaEnvio).IsModified = true;
                entry.Property(x => x.EstadoEncuesta).IsModified = true;
                db.SaveChanges();
            }
            else { 
                ComunicacionesInternas comunicados = new ComunicacionesInternas()
                {
                    Titulo = Titulo,
                    CuerpoHTML = CuerpoEncuesta,
                    EstadoEncuesta = EstadoEncuesta,
                    FechaCreacion = DateTime.Now.ToString(),
                    FechaEnvio = FechaEnvio,
                    NitEmpresa = usuarioActual.NitEmpresa
                };
                db.Tbl_ComunicacionesInternas.Add(comunicados);
                db.SaveChanges();
            }
                Transaction.Commit();
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }


       
        [HttpPost]
        public JsonResult ListarComunicacionesInternas() 
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            List<ComunicacionesInternas> comunicaciones = db.Tbl_ComunicacionesInternas.Where(x => x.NitEmpresa==usuarioActual.NitEmpresa).ToList();
            return Json(comunicaciones, JsonRequestBehavior.AllowGet);
        }

        
         [HttpGet]
        public JsonResult GenerarLink(int PK_Id_Encuesta) 
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            var comunicaciones = db.Tbl_ComunicacionesInternas.Where(x => (x.PK_Id_Encuesta == PK_Id_Encuesta && x.NitEmpresa == usuarioActual.NitEmpresa)).SingleOrDefault();
            using (var Transaction = db.Database.BeginTransaction())
            {
                comunicaciones.TokenPublico = RandomString(24);
                string baseUrl = Request.Url.Scheme;
                baseUrl = Request.Url.GetLeftPart(UriPartial.Authority) + "/Publicacion/PublicarEncuesta/?formdata=" + comunicaciones.TokenPublico;
                var regex = new Regex(@":\d+");
                var cleanUrl = regex.Replace(baseUrl, "");
                comunicaciones.URL = baseUrl;
                db.Tbl_ComunicacionesInternas.Attach(comunicaciones);
                var entry = db.Entry(comunicaciones);
                entry.State = System.Data.Entity.EntityState.Modified;
                entry.Property(x => x.URL).IsModified = true;
                entry.Property(x => x.TokenPublico).IsModified = true;
                db.SaveChanges();
                Transaction.Commit();
            }

            return Json(comunicaciones.URL, JsonRequestBehavior.AllowGet);
        }

         private static Random random = new Random();
         public static string RandomString(int length)
         {
             const string chars = "123456789abcdefghijklmnopqrstvwxyzABCDEFGHIJKLMOPQRSTVWXYZ";
             return new string(Enumerable.Repeat(chars, length)
               .Select(s => s[random.Next(s.Length)]).ToArray());
         }


        [HttpPost]
        public JsonResult GuardarEncuesta(string formulario)
        {
            string param = formulario.Replace("%20"," ");
            string[] frm = formulario.Split('&');
            string[] id_encuesta = frm[0].Split('=');
            int  fk_pk_id_encuesta = int.Parse(id_encuesta[1]);
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            using (var Transaction = db.Database.BeginTransaction())
            {
                ComunicacionesEncuestas objComunicacionesEncuestas = new ComunicacionesEncuestas()
                {
                    fk_pk_id_encuesta = fk_pk_id_encuesta,
                    contenido = param,
                    fechacreacion = DateTime.Now.ToString(),
                    NitEmpresa = usuarioActual.NitEmpresa
                };

                db.Tbl_ComunicacionesEncuestas.Add(objComunicacionesEncuestas);
                db.SaveChanges();
                Transaction.Commit();
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult EditarEncuesta(int PK_Id_Encuesta)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            var comunicaciones = db.Tbl_ComunicacionesInternas.Where(x => (x.PK_Id_Encuesta == PK_Id_Encuesta && x.NitEmpresa== usuarioActual.NitEmpresa)).SingleOrDefault();
            return Json(comunicaciones, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult EliminarEncuesta(int PK_Id_Encuesta)
        {
            var comunicaciones = db.Tbl_ComunicacionesInternas.Where(x => x.PK_Id_Encuesta == PK_Id_Encuesta).SingleOrDefault();
            using (var Transaction = db.Database.BeginTransaction())
            {
                db.Tbl_ComunicacionesInternas.Remove(comunicaciones);
                db.SaveChanges();
                Transaction.Commit();
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}
