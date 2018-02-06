using SG_SST.Controllers.Base;
using SG_SST.EntidadesDominio.Empresas;
using SG_SST.EntidadesDominio.PlanTrabajoAnual;
using SG_SST.Logica.Usuarios;
using SG_SST.Models.Metodologia;
using SG_SST.Models.Planificacion;
using SG_SST.Models.PlanCapacitacion;
using SG_SST.ServiceRequest;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SG_SST.Models;
using System.Globalization;
using SG_SST.Models.PlanTrabajoAnual;
using System.Drawing;
using System.Drawing.Imaging;

namespace SG_SST.Controllers.Planificacion
{
    public class PlanTrabajoAnualController : BaseController
    {
        string UrlPlanEmpresas = ConfigurationManager.AppSettings["UrlPlanEmpresas"];
        string CapacidadObtenerAnoInicioEmpresa = ConfigurationManager.AppSettings["CapacidadObtenerAnoInicioEmpresa"];
        string CapacidadGuardarPlanEmpresa = ConfigurationManager.AppSettings["CapacidadGuardarPlanEmpresa"];
        //string CapacidadObtenerSedesPorNit = ConfigurationManager.AppSettings["CapacidadObtenerSedesPorNit"];
        private static string RutaImagenes = "~/Descargas/";
        private static string SrcWhite = "data:image/png;base64,R0lGODlhAQABAAAAACH5BAEKAAEALAAAAAABAAEAAAICTAEAOw==";

        private SG_SSTContext db = new SG_SSTContext();
        #region PLAN ARL CON GESTPOS
        /// <summary>
        /// GESTPOS PARA PLAN ARL
        /// </summary>
        /// <returns></returns>
        public ActionResult PlanArl()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "Debe estar autenticado para visualizar los Indicadores.";
                return View();
            }
            var lnUsuario = new LNUsuario();
            EvaluacionPositivaModel modelEvalPositiva = new EvaluacionPositivaModel();
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("NIT", usuarioActual.NitEmpresa);
            var resultAno = ServiceClient.ObtenerObjetoJsonRestFul<int>(urlServicioPlanificacion, CapacidadObtenerAnoInicioEmpresa, RestSharp.Method.GET);
            if (resultAno > 0)
            {
                modelEvalPositiva.Anios = GetAnios(resultAno);
            }
            else
                modelEvalPositiva.Anios = GetAnios(2010);

            modelEvalPositiva.RazonSocial = usuarioActual.RazonSocialEmpresa;


            return View(modelEvalPositiva);
        }

        [HttpPost]
        public ActionResult PlanArl(EvaluacionPositivaModel EvalPositiva)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "Debe estar autenticado.";
                return View();
            }

            EvaluacionPositivaModel modelEvalPositiva = new EvaluacionPositivaModel();
            if (!ModelState.IsValid)
            {
                var lnUsuario = new LNUsuario();
                ServiceClient.EliminarParametros();
                ServiceClient.AdicionarParametro("NIT", usuarioActual.NitEmpresa);
                var resultAno = ServiceClient.ObtenerObjetoJsonRestFul<int>(urlServicioPlanificacion, CapacidadObtenerAnoInicioEmpresa, RestSharp.Method.GET);
                if (resultAno > 0)
                {
                    modelEvalPositiva.Anios = GetAnios(resultAno);
                }
                else
                    modelEvalPositiva.Anios = GetAnios(2010);

                modelEvalPositiva.RazonSocial = usuarioActual.RazonSocialEmpresa;

                return View(modelEvalPositiva);
            }
            else
            {
                var lnUsuario = new LNUsuario();
                ServiceClient.EliminarParametros();
                ServiceClient.AdicionarParametro("NIT", usuarioActual.NitEmpresa);
                var resultAno = ServiceClient.ObtenerObjetoJsonRestFul<int>(urlServicioPlanificacion, CapacidadObtenerAnoInicioEmpresa, RestSharp.Method.GET);
                if (resultAno > 0)
                {
                    modelEvalPositiva.Anios = GetAnios(resultAno);
                }
                else
                    modelEvalPositiva.Anios = GetAnios(2010);

                modelEvalPositiva.RazonSocial = usuarioActual.RazonSocialEmpresa;

                var login = new GestposService.ws_loginSoapClient();
                var parametro = new GestposService.paramObtenerLink();

                parametro.codi_usu = usuarioActual.Documento;
                parametro.xml_params = string.Format("<rt><anho_gest>{0}</anho_gest><tdoc_emp>{1}</tdoc_emp><ndoc_emp>{2}</ndoc_emp></rt>", EvalPositiva.anioseleccionado, "NI", usuarioActual.NitEmpresa);
                parametro.modulo = GestposService.modulo.eval_plan_gestpos;
                var ruta = new GestposService.rtaObtenerLink();
                try
                {
                    ruta = login.obtenerLink(parametro);
                }
                catch
                {
                    ruta = null;
                }
                if (ruta == null)
                    modelEvalPositiva.url = "../Content/ErrorPage.html";
                else if (ruta.valido < 0)
                    modelEvalPositiva.url = "../Content/ErrorPage.html";
                else
                    modelEvalPositiva.url = ruta.url_sitio;

                return View(modelEvalPositiva);
            }
        }

        #endregion

        #region PLAN EMPRESA

        public ActionResult PlanEmpresa()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
                return View();
            }
            var planempresa = new PlanEmpresaModel();
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("NIT", usuarioActual.NitEmpresa);
            var resultSede = ServiceClient.ObtenerArrayJsonRestFul<EDSede>(urlServicioEmpresas, CapacidadObtenerSedesPorNit, RestSharp.Method.GET);
            if (resultSede != null && resultSede.Count() > 0)
            {
                planempresa.Sedes = resultSede.Select(c => new SelectListItem()
                {
                    Value = c.IdSede.ToString(),
                    Text = c.NombreSede
                }).ToList();
            }

            planempresa.Vigencia = ObtenerVigencias();
            planempresa.Estado = ObtenerEstado();
            planempresa.objetivosst = ObtenerObjetivos();
            return View(planempresa);
        }

        [HttpGet]
        public JsonResult ObtenerMeta(int pk_id_objetivo)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            var objetivo = db.Tbl_Objetivos_SST.Where(x => (x.PK_Id_Objetivo_Empresa==pk_id_objetivo && x.FK_Id_Empresa==usuarioActual.IdEmpresa)).SingleOrDefault();
            return Json(objetivo.Meta, JsonRequestBehavior.AllowGet);
        } 

        private List<SelectListItem> ObtenerObjetivos()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            List<SelectListItem> objetivos = new List<SelectListItem>();
            var objetivo = db.Tbl_Objetivos_SST.Where(x => x.FK_Id_Empresa == usuarioActual.IdEmpresa).ToList();
            foreach (var item in objetivo)
            {
                objetivos.Add(new SelectListItem { Text = item.Objetivo, Value = item.PK_Id_Objetivo_Empresa.ToString() });
            }
            return objetivos;
        } 

        private List<SelectListItem> ObtenerVigencias()
        {
            List<SelectListItem> vigencias = new List<SelectListItem>();
            for (int i = 2016; i <= 2050; i++)
            {
                vigencias.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
            }

            return vigencias;
        }

        private List<SelectListItem> ObtenerEstado()
        {
            List<SelectListItem> Estados = new List<SelectListItem>();
            Estados.Add(new SelectListItem { Text = "Programada", Value = "P" });
            Estados.Add(new SelectListItem { Text = "Reprogramada", Value = "R" });
            Estados.Add(new SelectListItem { Text = "Ejecutada", Value = "E" });
            return Estados;
        }


        [HttpGet]
        public JsonResult ObtenerActividad(int pk_id_actividad)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            PlanEmpresaActividad planempresactividad = db.Tbl_Plan_Empresa_Actividad.Where(x => (x.pk_id_actividad == pk_id_actividad && x.NitEmpresa==usuarioActual.NitEmpresa)).SingleOrDefault();
            PlanEmpresa planempresa = db.Tbl_Plan_Empresa.Where(x => (x.pk_id_plan_empresa == planempresactividad.pk_plan_empresa && x.NitEmpresa==usuarioActual.NitEmpresa)).SingleOrDefault();
            PlanEmpresaModel objplanmodel = new PlanEmpresaModel()
            {
                pk_id_actividad = planempresactividad.pk_id_actividad,
                pk_id_plan_empresa = planempresa.pk_id_plan_empresa,
                IdSede = planempresa.IdSede,
                FechaDesdeTemp = planempresa.FechaDesde,
                FechaHastaTemp = planempresa.FechaHasta,
                ObjetivosDescripcion = planempresactividad.ObjetivosDescripcion,
                ObjetivosMetas = planempresactividad.ObjetivosMetas,
                Actividad = planempresactividad.Actividad,
                Responsable = planempresactividad.Responsable,
                RecursosHumanos = planempresactividad.RecursosHumanos,
                RecursosTecnologico = planempresactividad.RecursosTecnologico,
                RecursosFinanciero = planempresactividad.RecursosFinanciero,
                FechaProg = planempresactividad.FechaProg,
                HoraProgIni = planempresactividad.HoraProgIni,
                HoraProgFin = planempresactividad.HoraProgFin,
                Estados = planempresactividad.Estado
            };
            return Json(objplanmodel, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ObtenerAgendaXFecha(int pk_id_plan_empresa, string FechaInicio, string FechaFin)
        {

            DateTime fecini = FormatDate(FechaInicio);
            DateTime fecfin = FormatDate(FechaFin);
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            List<PlanEmpresaActividad> planactividad = db.Tbl_Plan_Empresa_Actividad.Where(x =>(x.pk_plan_empresa==pk_id_plan_empresa && x.NitEmpresa == usuarioActual.NitEmpresa)).ToList();
            List<FechaProgModel> objfechaprog = new List<FechaProgModel>();
            foreach (var item in planactividad)
            {
                FechaProgModel fechaprog = new FechaProgModel();
                fechaprog.pk_id_actividad = item.pk_id_actividad;
                fechaprog.NombreActividad = item.Actividad;
                fechaprog.FechaProgTemp = FormatDate(item.FechaProg);
                fechaprog.HoraIni = item.HoraProgIni;
                fechaprog.HoraFin = item.HoraProgFin;
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


        [HttpGet]
        public JsonResult GuardaPlanEmpresa(PlanEmpresaModel planempresamodel)
        {
            int pk_plan_empresa = 0;
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            using (var Transaction = db.Database.BeginTransaction())
            {

                PlanEmpresa planempresas = new PlanEmpresa()
                {
                    IdSede = planempresamodel.IdSede,
                    FechaDesde = planempresamodel.FechaDesde,
                    FechaHasta = planempresamodel.FechaHasta,
                    Vigencia = planempresamodel.anioVigencia,
                    NitEmpresa = usuarioActual.NitEmpresa
                };

                db.Tbl_Plan_Empresa.Add(planempresas);
                db.SaveChanges();
                Transaction.Commit();
                pk_plan_empresa = planempresas.pk_id_plan_empresa;
            }

            return Json(pk_plan_empresa, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ObternerPlanAnualporSede(int IDSede)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            var planempresa = db.Tbl_Plan_Empresa.Where(x => (x.IdSede == IDSede && x.NitEmpresa==usuarioActual.NitEmpresa))
                .Join(db.Tbl_Sede,
                a => a.IdSede,
                b => b.Pk_Id_Sede,
                (a, b) => new { IDSede = a.IdSede, PkEmpresa = a.pk_id_plan_empresa, Sede = b.Nombre_Sede, FechaDesde = a.FechaDesde, FechaHasta = a.FechaHasta, Vigencia = a.Vigencia });
            return Json(planempresa.ToList(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GuardarActividades(PlanEmpresaModel planempresa)
        {
            using (var Transaction = db.Database.BeginTransaction())
            {
                string fprog = string.Empty, frepro = string.Empty, feje = string.Empty;
                PlanEmpresaActividad planactividad = db.Tbl_Plan_Empresa_Actividad.Where(x => x.pk_id_actividad == planempresa.pk_id_actividad).SingleOrDefault();
                switch (planempresa.Estados)
                {
                    case "P": fprog = planempresa.FechaProg;
                        break;
                    case "R": fprog = planactividad.FechaProg;
                        frepro = planempresa.FechaProg;
                        break;
                    case "E":
                        if (planactividad!=null)
                        {
                            fprog = planactividad.FechaProg;
                            frepro = planactividad.FechaReProg;
                            feje = planempresa.FechaProg;
                        }
                        else
                            fprog = planempresa.FechaProg;
                        
                        break;
                }

                if (planactividad != null)
                {
                    db.Tbl_Plan_Empresa_Actividad.Remove(planactividad);
                    db.SaveChanges();
                }

                var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
                PlanEmpresaActividad objPlanEmpresa = new PlanEmpresaActividad()
                {
                    pk_plan_empresa = planempresa.pk_id_plan_empresa,
                    ObjetivosDescripcion = planempresa.ObjetivosDescripcion,
                    ObjetivosMetas = planempresa.ObjetivosMetas,
                    Actividad = planempresa.Actividad,
                    Responsable = planempresa.Responsable,
                    RecursosHumanos = planempresa.RecursosHumanos,
                    RecursosTecnologico = planempresa.RecursosTecnologico,
                    RecursosFinanciero = planempresa.RecursosFinanciero,
                    FechaProg = fprog,
                    FechaReProg = frepro,
                    FechaEje = feje,
                    HoraProgIni = planempresa.HoraProgIni,
                    HoraProgFin = planempresa.HoraProgFin,
                    Estado = planempresa.Estados,
                    NitEmpresa = usuarioActual.NitEmpresa
                };

                db.Tbl_Plan_Empresa_Actividad.Add(objPlanEmpresa);
                db.SaveChanges();
                Transaction.Commit();
            }

            return Json(planempresa.pk_id_plan_empresa, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult ObtenerPlanPorIdPlan(int pk_plan_empresa)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            PlanEmpresa planempresa = db.Tbl_Plan_Empresa.Where(x => (x.pk_id_plan_empresa == pk_plan_empresa && x.NitEmpresa==usuarioActual.NitEmpresa)).SingleOrDefault();
            return Json(planempresa, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult EliminarPlanAnual(int pk_plan_empresa)
        {
            PlanEmpresa planempresa = db.Tbl_Plan_Empresa.Where(x => x.pk_id_plan_empresa == pk_plan_empresa).SingleOrDefault();
            db.Tbl_Plan_Empresa_Actividad.RemoveRange(db.Tbl_Plan_Empresa_Actividad.Where(x => x.pk_plan_empresa == planempresa.pk_id_plan_empresa));
            db.SaveChanges();

            using (var Transaction = db.Database.BeginTransaction())
            {
                db.Tbl_Plan_Empresa.Remove(planempresa);
                db.SaveChanges();
                Transaction.Commit();
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult EliminarActividad(int pk_id_actividad)
        {
            PlanEmpresaActividad planempresactividad = db.Tbl_Plan_Empresa_Actividad.Where(x => x.pk_id_actividad == pk_id_actividad).SingleOrDefault();
            PlanEmpresa planempresa = db.Tbl_Plan_Empresa.Where(x => x.pk_id_plan_empresa == planempresactividad.pk_plan_empresa).SingleOrDefault();
            using (var Transaction = db.Database.BeginTransaction())
            {
                db.Tbl_Plan_Empresa_Actividad.Remove(planempresactividad);
                db.SaveChanges();
                Transaction.Commit();
            }
            return Json(planempresa.pk_id_plan_empresa, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ObtenerActividadPorPlan(int pkPlanEmpresa)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            var planempresaactividad = db.Tbl_Plan_Empresa_Actividad.Where(x => (x.pk_plan_empresa == pkPlanEmpresa && x.NitEmpresa==usuarioActual.NitEmpresa))
                .Join(db.Tbl_Plan_Empresa,
                a => a.pk_plan_empresa,
                b => b.pk_id_plan_empresa,
                (a, b) => new { pk_id_actividad = a.pk_id_actividad, pk_id_plan_empresa = a.pk_plan_empresa, Estado = a.Estado, a.FechaProg, a.FechaReProg, a.FechaEje, a.HoraProgIni, a.HoraProgFin, b.FechaDesde, b.FechaHasta, b.Vigencia });
            int progt = 0;
            foreach (var item in planempresaactividad)
            {
                if (item.Estado == "P")
                    progt++;
            }

            List<PlanActividad> listPlanActividad = new List<PlanActividad>();
            foreach (var item in planempresaactividad)
            {
                PlanActividad objPlanActividad = new PlanActividad() { 
                    pk_id_actividad = item.pk_id_actividad,
                    pk_id_plan_empresa = item.pk_id_plan_empresa,
                    Estado = item.Estado,
                    FechaProg = item.FechaProg,
                    FechaReProg = item.FechaReProg,
                    FechaEje = item.FechaEje,
                    HoraProgIni = item.HoraProgIni,
                    HoraProgFin = item.HoraProgFin,
                    FechaDesde = item.FechaDesde,
                    FechaHasta = item.FechaHasta,
                    Vigencia = SwithVigencia(item.FechaProg)
                };

                listPlanActividad.Add(objPlanActividad);
            }

            var plan = new { planempresaactividad = listPlanActividad.OrderBy(x => x.Vigencia).ToList(), programadas = progt };
            return Json(plan, JsonRequestBehavior.AllowGet);
        }

        private int SwithVigencia(string vigencia)
        {
            int fecha = 0;
            if (vigencia != "") { 
                string[] vig = vigencia.Split('/');
                fecha = int.Parse(vig[2]);
            }
            
            return fecha;
        }

        [HttpGet]
        public JsonResult ActualizarAdjuntos(int pk_id_plan_empresa, string RepresentanteLegal, string RepresentanteSGSST)
        {
            var planempresa = db.Tbl_Plan_Empresa.Where(x => x.pk_id_plan_empresa == pk_id_plan_empresa).SingleOrDefault();
            using (var Transaction = db.Database.BeginTransaction())
            {
                planempresa.RepresentanteLegal = RepresentanteLegal;
                planempresa.RepresentanteSGSST = RepresentanteSGSST;
                db.Tbl_Plan_Empresa.Attach(planempresa);
                var entry = db.Entry(planempresa);
                entry.State = System.Data.Entity.EntityState.Modified;
                entry.Property(x => x.RepresentanteLegal).IsModified = true;
                entry.Property(x => x.RepresentanteSGSST).IsModified = true;
                db.SaveChanges();
                Transaction.Commit();
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ObtenerImagen(int pk_id_plan_empresa, string tipo)
        {
            string Thumbnails = SrcWhite;
            string NombreArchivos = string.Empty;
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            var planempresa = db.Tbl_Plan_Empresa.Where(x => (x.pk_id_plan_empresa == pk_id_plan_empresa && x.NitEmpresa==usuarioActual.NitEmpresa)).SingleOrDefault();
            if (tipo == "legal")
                NombreArchivos = planempresa.RepresentanteLegal;
            else
                NombreArchivos = planempresa.RepresentanteSGSST;


            if (NombreArchivos != "")
            {
                try
                {
                    string PathImage = Server.MapPath(Path.Combine(RutaImagenes, NombreArchivos));
                    Bitmap bitmap;
                    using (var bmpTemp = new Bitmap(PathImage))
                    {
                        bitmap = new Bitmap(bmpTemp);
                    }
                    using (var newImage = ScaleImage(bitmap, 300, 300))
                    {
                        Thumbnails = "data:image/png;base64," + ImageToBase64String(newImage, ImageFormat.Png);
                    }
                }
                catch (Exception)
                {
                }
            }
            else {
                Thumbnails = string.Empty;
            }


            return Json(Thumbnails, JsonRequestBehavior.AllowGet);
        }

        public static Image ScaleImage(Image image, int maxWidth, int maxHeight)
        {
            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);

            using (var graphics = Graphics.FromImage(newImage))
                graphics.DrawImage(image, 0, 0, newWidth, newHeight);

            return newImage;
        }
        private string ImageToBase64String(System.Drawing.Image image, ImageFormat imageFormat)
        {
            using (MemoryStream memStream = new MemoryStream())
            {
                image.Save(memStream, imageFormat);
                string result = Convert.ToBase64String(memStream.ToArray());
                memStream.Close();

                return result;
            }

        }

        [HttpPost]
        public virtual ActionResult SubirArchivo()
        {
            HttpPostedFileBase myFile = Request.Files["RepresentanteLegalFile"];
            if (myFile == null)
                myFile = Request.Files["RepresentanteSGSSTFile"];

            bool isUploaded = false;
            string message = "File upload failed";
            string fullpath = string.Empty;
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

        #endregion

        public ActionResult Calendario()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
                return View();
            }
            var planempresa = new PlanEmpresaModel();
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("NIT", usuarioActual.NitEmpresa);
            var resultSede = ServiceClient.ObtenerArrayJsonRestFul<EDSede>(urlServicioEmpresas, CapacidadObtenerSedesPorNit, RestSharp.Method.GET);
            if (resultSede != null && resultSede.Count() > 0)
            {
                planempresa.Sedes = resultSede.Select(c => new SelectListItem()
                {
                    Value = c.IdSede.ToString(),
                    Text = c.NombreSede
                }).ToList();
            }

            planempresa.Vigencia = ObtenerVigencias();
            planempresa.Estado = ObtenerEstado();
            return View(planempresa);
        }

    }
}