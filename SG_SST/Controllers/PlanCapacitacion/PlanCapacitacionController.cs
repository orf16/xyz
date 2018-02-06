using RestSharp;
using SG_SST.Controllers.Base;
using SG_SST.Dtos.Organizacion;
using SG_SST.EntidadesDominio.Empresas;
using SG_SST.Models;
using SG_SST.Models.Emergencias;
using SG_SST.Models.Empresas;
using SG_SST.Models.Organizacion;
using SG_SST.Models.PlanCapacitacion;
using SG_SST.Models.PlanTrabajoAnual;
using SG_SST.Repositories.Organizacion.IRepositories;
using SG_SST.Repositories.Organizacion.Repositories;
using SG_SST.ServiceRequest;
using SG_SST.Utilidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace SG_SST.Controllers.PlanCapacitacion
{
    public class PlanCapacitacionController : BaseController
    {
        string UrlPlanEmpresas = ConfigurationManager.AppSettings["UrlPlanEmpresas"];
        string CapacidadObtenerAnoInicioEmpresa = ConfigurationManager.AppSettings["CapacidadObtenerAnoInicioEmpresa"];
        string CapacidadGuardarPlanEmpresa = ConfigurationManager.AppSettings["CapacidadGuardarPlanEmpresa"];
        private static string RutaArchivos = "~/Descargas/";
        private ICompetenciaRepositorio CompetenciaRepositorio = new CompetenciaRepositorio();
        private SG_SSTContext db = new SG_SSTContext();
        //
        // GET: /PlanCapacitacion/
        public ActionResult Programacion()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
                return View();
            }

            SetDDL();
            return View();
        }

        public ActionResult Calendario()
        {
            return View();
        }

        private void SetDDL()
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            lst.Add(new SelectListItem() { Text = "-Seleccione Opcion-", Value = "0" });
            lst.Add(new SelectListItem() { Text = "Capacitacion", Value = "1" });
            lst.Add(new SelectListItem() { Text = "Entrenamiento", Value = "2" });
            lst.Add(new SelectListItem() { Text = "Induccion/RE induccion", Value = "3" });
            ViewBag.tipoactividad = new SelectList(lst, "Text", "Value");
            
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            var roles = db.Tbl_Rol.Join(db.Tbl_Empresa, a => a.Fk_Id_Empresa, b => b.Pk_Id_Empresa, (a, b) => new { a, b }).Where(x => x.b.Nit_Empresa == usuarioActual.NitEmpresa)
                .Select(x => new { Descripcion = x.a.Descripcion, pk_id_rol = x.a.Pk_Id_Rol }).OrderBy(x => x.Descripcion).ToList();
            List<SelectListItem> lst1 = new List<SelectListItem>();
            lst1.Add(new SelectListItem() { Text = "-Seleccione Opcion-", Value = "0" });
            foreach (var item in roles)
            {
                lst1.Add(new SelectListItem() { Text = item.Descripcion, Value = item.pk_id_rol.ToString() });
            }
            ViewBag.roles = new SelectList(lst1, "Text", "Value");

            
            var competencias = db.Tbl_Tematica_Por_Empresa
                .Join(db.Tbl_Empresa, a => a.Fk_Id_Empresa, b => b.Pk_Id_Empresa, (a, b) => new { a, b })
                .Join(db.Tbl_Tematica, c => c.a.Fk_Id_Tematica, d => d.Id_Tematica, (c, d) => new { c, d })
                .Where(x => (x.d.TipoTematica == 1 && x.c.b.Nit_Empresa == usuarioActual.NitEmpresa))
                .GroupBy(x => new { pk_id_tematica = x.d.Id_Tematica, nit_empresa = x.c.b.Nit_Empresa, tematica = x.d.Tematicas }).ToList();
            List<SelectListItem> lst2 = new List<SelectListItem>();
            lst2.Add(new SelectListItem() { Text = "-Seleccione Opcion-", Value = "0" });
            foreach (var item in competencias)
            {
                lst2.Add(new SelectListItem() { Text = item.Key.tematica, Value = item.Key.pk_id_tematica.ToString() });
            }
            ViewBag.competencia = new SelectList(lst2, "Text", "Value");
        }

        [HttpGet]
        public JsonResult CompetenciasxRol(int idRol) 
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            List<RolPorTematica> RolPorTematicaList = CompetenciaRepositorio.ObtenerRolPorTematicaPorRol(idRol);
           // List<CargoPorRol> CargoPorRolList = CompetenciaRepositorio.ObtenerCargoPorRolPorRol(idRol);
            List<SelectListItem> lst1 = new List<SelectListItem>();
            lst1.Add(new SelectListItem() { Text = "-Seleccione Opcion-", Value = "0" });
            foreach (var item in RolPorTematicaList)
            {
                lst1.Add(new SelectListItem() { Text = item.Tematica.Tematicas, Value = item.Tematica.Id_Tematica.ToString() });
            }
            return Json(lst1, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult ListarEmpleadosxTipo(int pk_id_plan_capacitacion)
        {
            var plancapa = db.Tbl_PlanCapacitacion.Where(x => x.pk_id_plan_capacitacion == pk_id_plan_capacitacion).SingleOrDefault();
            var empleado = db.Tbl_Empleado_Por_Tematica.Where(x => x.Fk_Id_Rol == plancapa.fk_id_rol).ToList();
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
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
            List<SelectListItem> lst2 = new List<SelectListItem>();
            foreach (var item in empleado)
            {
                var empleados = respuesta.Where(x => x.idPersona == item.EmpleadoTematica.Numero_Documento.ToString()).ToList();
                foreach (var item1 in empleados)
                {
                    string nombre = item1.nombre1+' '+item1.nombre2+' '+item1.apellido1+' '+item1.apellido2;
                    string idpersona = item1.idPersona;
                    lst2.Add(new SelectListItem() { Text = nombre, Value = idpersona });
                }
            }
            return Json(lst2, JsonRequestBehavior.AllowGet);
        }
        

        [HttpGet]
        public JsonResult ObtenerActividad()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            var planempresaactividad = db.Tbl_PlanCapacitacion.Where(x => x.NitEmpresa==usuarioActual.NitEmpresa).ToList();
            return Json(planempresaactividad, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ObtenerActividadxId(int pk_id_plan_capacitacion)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            var planempresaactividad = db.Tbl_PlanCapacitacion.Where(x => (x.pk_id_plan_capacitacion == pk_id_plan_capacitacion && x.NitEmpresa==usuarioActual.NitEmpresa)).SingleOrDefault();
            return Json(planempresaactividad, JsonRequestBehavior.AllowGet);
        }
        
        [HttpGet]
        public JsonResult ListarEmpleadosAsignados(int pk_id_plan_capacitacion)
        {
            var plan = db.Tbl_PlanCapacitacion_Asignaciones.Where(x => (x.fk_id_plan_capacitacion == pk_id_plan_capacitacion && x.Enviado == true && x.asistencia==false)).ToList();
            return Json(plan, JsonRequestBehavior.AllowGet);
        }
        
        [HttpGet]
        public JsonResult GuardarActividades(PlanCapacitacionModel plancapacitacion)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            using (var Transaction = db.Database.BeginTransaction())
            {
                Plan_Capacitacion planactividad = db.Tbl_PlanCapacitacion.Where(x => x.pk_id_plan_capacitacion == plancapacitacion.pk_id_plan_capacitacion).SingleOrDefault();
                if (planactividad == null)
                {
                    Plan_Capacitacion obj_planactividad = new Plan_Capacitacion()
                    {
                        pk_id_plan_capacitacion = plancapacitacion.pk_id_plan_capacitacion,
                        tema = plancapacitacion.tema,
                        fk_id_tipo_actividad = plancapacitacion.fk_id_tipo_actividad,
                        fk_id_rol = plancapacitacion.fk_id_rol,
                        fk_id_competencia = plancapacitacion.fk_id_competencia,
                        fecha_programada = plancapacitacion.fecha_programada,
                        hora_inicio = plancapacitacion.hora_inicio,
                        hora_fin = plancapacitacion.hora_fin,
                        NitEmpresa = usuarioActual.NitEmpresa
                    };

                    db.Tbl_PlanCapacitacion.Add(obj_planactividad);
                    db.SaveChanges();
                }
                else {
                    planactividad.pk_id_plan_capacitacion = plancapacitacion.pk_id_plan_capacitacion;
                    planactividad.tema = plancapacitacion.tema;
                    planactividad.fk_id_tipo_actividad = plancapacitacion.fk_id_tipo_actividad;
                    planactividad.fk_id_competencia = plancapacitacion.fk_id_competencia;
                    planactividad.fk_id_rol = plancapacitacion.fk_id_rol;
                    planactividad.fecha_programada = plancapacitacion.fecha_programada;
                    planactividad.hora_inicio = plancapacitacion.hora_inicio;
                    planactividad.hora_fin = plancapacitacion.hora_fin;
                    db.Tbl_PlanCapacitacion.Attach(planactividad);
                    var entry = db.Entry(planactividad);
                    entry.State = System.Data.Entity.EntityState.Modified;
                    entry.Property(x => x.pk_id_plan_capacitacion).IsModified = true;
                    entry.Property(x => x.fk_id_tipo_actividad).IsModified = true;
                    entry.Property(x => x.fk_id_rol).IsModified = true;
                    entry.Property(x => x.fk_id_competencia).IsModified = true;
                    entry.Property(x => x.fecha_programada).IsModified = true;
                    entry.Property(x => x.hora_inicio).IsModified = true;
                    entry.Property(x => x.hora_fin).IsModified = true;
                    db.SaveChanges();
                }

                Transaction.Commit();
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult EliminarActividad(int pk_id_plan_capacitacion)
        {
            Plan_Capacitacion planactividad = db.Tbl_PlanCapacitacion.Where(x => x.pk_id_plan_capacitacion == pk_id_plan_capacitacion).SingleOrDefault();
            using (var Transaction = db.Database.BeginTransaction())
            {
                db.Tbl_PlanCapacitacion.Remove(planactividad);
                db.SaveChanges();
                Transaction.Commit();
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GuardarAsignaciones(int pk_id_plan_capacitacion, string[] asignaciones)
        {
            using (var Transaction = db.Database.BeginTransaction())
            {
                var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
                var plan = db.Tbl_PlanCapacitacion_Asignaciones.Where(x => x.fk_id_plan_capacitacion == pk_id_plan_capacitacion).ToList();
                db.Tbl_PlanCapacitacion_Asignaciones.RemoveRange(plan);
                db.SaveChanges();
                for (int i = 0; i < asignaciones.Length; i++)
                {
                    string[] paramst = asignaciones[i].Split(',');
                    if (paramst.Length == 2)
                    {
                        PlanCapacitacion_Asignaciones objasignaciones = new PlanCapacitacion_Asignaciones()
                        {
                            fk_id_plan_capacitacion = pk_id_plan_capacitacion,
                            Enviado = true,
                            asistencia = false,
                            numero_documento = paramst[0],
                            nombre = paramst[1],
                            NitEmpresa = usuarioActual.NitEmpresa
                        };

                        db.Tbl_PlanCapacitacion_Asignaciones.Add(objasignaciones);
                        db.SaveChanges();
                    }
                }
                
                Transaction.Commit();
                Thread myNewThread = new Thread(() => SendEmail(pk_id_plan_capacitacion, asignaciones, usuarioActual.NitEmpresa));
                myNewThread.Start();
                
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        private void SendEmail(int pk_id_plan_capacitacion, string[] asignaciones, string NitEmpresa)
        {
            var parametros = db.Tbl_ParametrosSistema.ToList();
            var rutaHttpSitio = parametros.Where(p => p.IdParametro == (int)Enumeraciones.EnumAdministracionUsuarios.ParametrosSistema.RutaHttpSitio).Select(p => p).FirstOrDefault().Valor;
            var servidorSTMP = parametros.Where(p => p.IdParametro == (int)Enumeraciones.EnumAdministracionUsuarios.ParametrosSistema.ServidorStmp).Select(p => p).FirstOrDefault().Valor;
            var remitente = parametros.Where(p => p.IdParametro == (int)Enumeraciones.EnumAdministracionUsuarios.ParametrosSistema.RemitenteNotificaion).Select(p => p).FirstOrDefault().Valor;
            var correoRemitente = parametros.Where(p => p.IdParametro == (int)Enumeraciones.EnumAdministracionUsuarios.ParametrosSistema.CorreoRemitente).Select(p => p).FirstOrDefault().Valor;
            var puertoServidorStmp = parametros.Where(p => p.IdParametro == (int)Enumeraciones.EnumAdministracionUsuarios.ParametrosSistema.PuertoServidorStmp).Select(p => p).FirstOrDefault().Valor;
            var usuarioServidorStmp = parametros.Where(p => p.IdParametro == (int)Enumeraciones.EnumAdministracionUsuarios.ParametrosSistema.UsuarioServidorStmp).Select(p => p).FirstOrDefault().Valor;
            var passwordServidorStmp = parametros.Where(p => p.IdParametro == (int)Enumeraciones.EnumAdministracionUsuarios.ParametrosSistema.PasswordServidorStmp).Select(p => p).FirstOrDefault().Valor;
            var plantilla = db.Tbl_PlantillasCorreosSistema.Where(x => x.IdPlantilla == 5).SingleOrDefault();
            var cliente = new RestSharp.RestClient(ConfigurationManager.AppSettings["Url"]);
            var request = new RestRequest(ConfigurationManager.AppSettings["consultaAfiliados"], RestSharp.Method.GET);
            request.RequestFormat = DataFormat.Xml;
            request.Parameters.Clear();
            request.AddParameter("tpEm", "NI");
            request.AddParameter("docEm", NitEmpresa);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            //se omite la validación de certificado de SSL
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            IRestResponse<List<EmpleadosWSDTO>> response = cliente.Execute<List<EmpleadosWSDTO>>(request);
            var result = response.Content;
            var respuesta = Newtonsoft.Json.JsonConvert.DeserializeObject<List<EmpleadosWSDTO>>(result);
            for (int i = 0; i < asignaciones.Length; i++)
            {
                string[] paramst = asignaciones[i].Split(',');
                if (paramst.Length == 2)
                {
                    int cedula=0;
                    string documento = string.Empty;
                    cedula = Convert.ToInt32(paramst[0]);
                    documento = cedula.ToString();
                    var cargotemp = respuesta.Where(x => x.idPersona == documento).SingleOrDefault();
                    var plantillaHtml = plantilla.Plantilla.Replace("[[RutaHttpSitio]]", rutaHttpSitio);
                    plantillaHtml = plantillaHtml.Replace("[[NombreUsuario]]", string.Format("{0} {1}", cargotemp.nombre1 + ' ' + cargotemp.nombre2, cargotemp.apellido1 + ' ' + cargotemp.apellido2));
                    plantillaHtml = plantillaHtml.Replace("[[EmailUsuario]]", cargotemp.emailPersona);
                    plantillaHtml = plantillaHtml.Replace("[[RazonSocial]]", ObtenerRazonSocial(NitEmpresa));
                    plantillaHtml = plantillaHtml.Replace("[[Asunto]]", "Invitación Plan de Capacitación");
                    plantillaHtml = plantillaHtml.Replace("[[Cuerpo]]", "Se invita a los usuarios al plan de capacitación.");
                    var enviado = EnvioCorreos.EnviarCorreo(plantillaHtml, correoRemitente, remitente, true, passwordServidorStmp, Convert.ToInt32(puertoServidorStmp), servidorSTMP, "[ALISSTA Plan Capacitación] " + "Invitación Plan de Capacitación", cargotemp.emailPersona);
                }
            }
        }

        private string ObtenerRazonSocial(string NitEmpresa)
        {
            var empresa = db.Tbl_Empresa.Where(x => x.Nit_Empresa == NitEmpresa).SingleOrDefault();
            return empresa.Razon_Social;
        }
        
        [HttpGet]
        public JsonResult GuardarAsistentes(int pk_id_plan_capacitacion, string[] asignaciones)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            using (var Transaction = db.Database.BeginTransaction())
            {
                var plan = db.Tbl_PlanCapacitacion_Asignaciones.Where(x => (x.fk_id_plan_capacitacion == pk_id_plan_capacitacion && x.Enviado==true && x.asistencia==true)).ToList();
                db.Tbl_PlanCapacitacion_Asignaciones.RemoveRange(plan);
                db.SaveChanges();
                for (int i = 0; i < asignaciones.Length; i++)
                {
                    string[] paramst = asignaciones[i].Split(',');
                    if (paramst.Length == 2)
                    {
                        PlanCapacitacion_Asignaciones objasignaciones = new PlanCapacitacion_Asignaciones()
                        {
                            fk_id_plan_capacitacion = pk_id_plan_capacitacion,
                            Enviado = true,
                            asistencia = true,
                            numero_documento = paramst[0],
                            nombre = paramst[1],
                            NitEmpresa = usuarioActual.NitEmpresa
                        };

                        db.Tbl_PlanCapacitacion_Asignaciones.Add(objasignaciones);
                        db.SaveChanges();
                    }  
                }
                
                Transaction.Commit();
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarAsistentes(int pk_id_plan_capacitacion)
        {
            var cal = db.Tbl_PlanCapacitacion_Asignaciones.Where(x => (x.fk_id_plan_capacitacion == pk_id_plan_capacitacion && x.asistencia==true)).ToList();
            return Json(cal, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public virtual ActionResult SubirArchivo()
        {
            HttpPostedFileBase myFile = Request.Files["adjunto"];
            bool isUploaded = false;
            string message = "File upload failed";
            string fullpath = "error";

            if (myFile.FileName.Contains(".pdf"))
            {
                if (myFile != null && myFile.ContentLength != 0)
                {
                    string pathForSaving = Server.MapPath("~/Descargas");
                    if (this.CreateFolderIfNeeded(pathForSaving))
                    {
                        try
                        {
                            var mes = DateTime.Now.Month.ToString();
                            var dia = DateTime.Now.Day.ToString();
                            var anio = DateTime.Now.Year.ToString();
                            var currentmes = mes + dia + anio + "_";
                            string filen = currentmes + myFile.FileName;
                            myFile.SaveAs(Path.Combine(pathForSaving, filen));
                            isUploaded = true;
                            //message = "File uploaded successfully!";
                            fullpath = filen;//Path.Combine(pathForSaving, filen);
                        }
                        catch (Exception ex)
                        {
                            message = string.Format("File upload failed: {0}", ex.Message);
                        }
                    }
                } 
            }
            
            
            return Json(new { isUploaded = isUploaded, message = fullpath }, "text/html");
        }

        /// <summary>
        /// Creates the folder if needed.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        private bool CreateFolderIfNeeded(string path)
        {
            bool result = true;
            if (!Directory.Exists(path))
            {
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch (Exception)
                {
                    /*TODO: You must process this exception.*/
                    result = false;
                }
            }
            return result;
        }

        [HttpGet]
        public JsonResult ActualizarAdjuntos(int pk_id_plan_capacitacion, string adjunto)
        {
            int pk_id_soporte = 0;
            using (var Transaction = db.Database.BeginTransaction())
            {
                var del = db.Tbl_PlanCapacitacion_Soporte.Where(x => x.fk_id_plan_capacitacion == pk_id_plan_capacitacion).SingleOrDefault();
                if (del!=null)
                {
                    db.Tbl_PlanCapacitacion_Soporte.Remove(del);
                    db.SaveChanges();
                }
                var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
                PlanCapacitacion_Soporte adjuntos = new PlanCapacitacion_Soporte()
                {
                     fk_id_plan_capacitacion = pk_id_plan_capacitacion,
                     adjunto = adjunto,
                     NitEmpresa = usuarioActual.NitEmpresa
                };
                db.Tbl_PlanCapacitacion_Soporte.Add(adjuntos);
                db.SaveChanges();
                Transaction.Commit();
                pk_id_soporte = adjuntos.pk_id_soporte;
            }
            return Json(pk_id_soporte, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public string DescargarArchivo(int pk_id_soporte)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            var PlanCapacitacion_Soporte = db.Tbl_PlanCapacitacion_Soporte.Where(x => (x.fk_id_plan_capacitacion == pk_id_soporte && x.NitEmpresa == usuarioActual.NitEmpresa)).SingleOrDefault();
            return PlanCapacitacion_Soporte.adjunto;
        }

        [HttpGet]
        public virtual ActionResult Download(string file)
        {
            string contentType = string.Empty;
            string PathFile = Server.MapPath(Path.Combine(RutaArchivos, file));

            if (file.Contains(".txt"))
            {
                contentType = "text/plain";
            }
            if (file.Contains(".html"))
            {
                contentType = "text/html";
            }
            if (file.Contains(".pdf"))
            {
                contentType = "application/pdf";
            }
            else if (file.Contains(".docx"))
            {
                contentType = "application/docx";
            }
            else if (file.Contains(".xls"))
            {
                contentType = "application/xlsx";
            }
            else if (file.Contains(".xls"))
            {
                contentType = "application/xlsx";
            }
            else if (file.Contains(".jpeg"))
            {
                contentType = "image/jpeg";
            }
            else if (file.Contains(".png"))
            {
                contentType = "image/png";
            }
            else if (file.Contains(".gif "))
            {
                contentType = "image/gif ";
            }
            else if (file.Contains(".jpg"))
            {
                contentType = "image/jpeg";
            }

            return File(PathFile, contentType, file);
        }

        [HttpGet]
        public JsonResult ObtenerAgendaXFecha(string FechaInicio, string FechaFin)
        {
            try
            {
                DateTime fecini = FormatDate(FechaInicio);
                DateTime fecfin = FormatDate(FechaFin);
                var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
                List<Plan_Capacitacion> planactividad = db.Tbl_PlanCapacitacion.Where(x => x.NitEmpresa==usuarioActual.NitEmpresa).ToList();
                List<FechaProgModel> objfechaprog = new List<FechaProgModel>();
                foreach (var item in planactividad)
                {
                    FechaProgModel fechaprog = new FechaProgModel();
                    fechaprog.pk_id_actividad = item.pk_id_plan_capacitacion;
                    fechaprog.NombreActividad = ObtenerActividad(item.fk_id_tipo_actividad);
                    fechaprog.FechaProgTemp = FormatDate(item.fecha_programada);
                    fechaprog.HoraIni = item.hora_inicio;
                    fechaprog.HoraFin = item.hora_fin;
                    objfechaprog.Add(fechaprog);
                }

                var fechas = objfechaprog.Where(x => x.FechaProgTemp >= fecini && x.FechaProgTemp <= fecfin).ToList();
                List<PlanEmpresaModel> calmod = new List<PlanEmpresaModel>();

                foreach (var item in fechas)
                {
                    PlanEmpresaModel mod = new PlanEmpresaModel();
                    mod.Id = 1;
                    mod.name = item.NombreActividad;
                    mod.DateFrom = FormatDates(item.FechaProgTemp);
                    mod.DateTo = FormatDates(item.FechaProgTemp);
                    mod.HoraInicio = FormatHH(item.HoraIni);
                    mod.HoraFin = FormatHH(item.HoraFin);
                    calmod.Add(mod);
                }
                return Json(calmod, JsonRequestBehavior.AllowGet);
            }
            catch(Exception e){
                return Json(true, JsonRequestBehavior.AllowGet);
            }     
        }

        private string ObtenerActividad(int id_actividad)
        {
            string act = string.Empty;
            switch (id_actividad)
            {
                case 1: act = "Capacitacion";
                    break;
                case 2: act = "Entrenamiento";
                    break;
                case 3: act = "Induccion/RE induccion";
                    break;
            }

            return act;
        }


        private DateTime FormatDate(string Fecha)
        {
            string[] date = Fecha.Split('/');
            string fecpattern = date[0] + "-" + date[1] + "-" + date[2];
            return Convert.ToDateTime(fecpattern, System.Globalization.CultureInfo.GetCultureInfo("es-CO").DateTimeFormat);
        }

        private string FormatDates(DateTime Fecha)
        {
            Fecha = Convert.ToDateTime(Fecha, System.Globalization.CultureInfo.GetCultureInfo("es-CO").DateTimeFormat);
            string fecpattern = Fecha.Year + "-" + Fecha.Month + "-" + Fecha.Day;
            return fecpattern;
        }

        private string FormatHH(string Hora)
        {
            string pref = string.Empty;
            int digits = Hora.Length;

            if (digits > 6)
            {
                pref = Hora.Substring(5, 2);
                Hora = Hora.Substring(0, 5);
            }
            else
            {
                pref = Hora.Substring(4, 2);
                Hora = Hora.Substring(0, 4);
            }


            return Hora + ' ' + pref;
        }
    }
}
