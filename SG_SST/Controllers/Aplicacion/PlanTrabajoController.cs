using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using OfficeOpenXml.Style;
using RestSharp;
using SG_SST.Controllers.Base;
using SG_SST.EntidadesDominio.Aplicacion;
using SG_SST.EntidadesDominio.Empresas;
using SG_SST.Logica.Aplicacion;
using SG_SST.Logica.MedicionEvaluacion;
using SG_SST.Models.Ausentismo;
using SG_SST.Models.Empleado;
using SG_SST.Models.Empresas;
using SG_SST.Repositories.Empresas.IRepositories;
using SG_SST.Repositories.Empresas.Repositories;
using SG_SST.Services.Empresas.IServices;
using SG_SST.Services.Empresas.Services;
using SG_SST.Services.General.IServices;
using SG_SST.Services.General.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace SG_SST.Controllers.Aplicacion
{
    public class PlanTrabajoController : BaseController
    {
        LNPlanTrabajo LNPlanTrabajo = new LNPlanTrabajo();
        LNAcciones LNAcciones = new LNAcciones();
        private ISedeServicios sedeServicio = new SedeServicios();
        private IRecursosServicios recursosServicios = new RecursosServicios();
        private ISedeRepositorio sedeRepositorio = new SedeRepositorio();
        private int anioIncial = Int32.Parse(ConfigurationManager.AppSettings["anioInicial"]);
        private int anioFinal = Int32.Parse(ConfigurationManager.AppSettings["anioFinal"]);
        private static string RutaImagenes = "~/Content/Plantrabajo/ImagenesRepositorio/";
        private static string RutaImagenesTemp = "~/Content/Plantrabajo/ImagenesTemp/";
        private static string SrcWhite = "data:image/png;base64,R0lGODlhAQABAAAAACH5BAEKAAEALAAAAAABAAEAAAICTAEAOw==";

        [HttpGet]
        public ActionResult GestionarPlanesTrabajo()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
                return RedirectToAction("Login", "Home");
            }
            List<EDAplicacionPlanTrabajo> planes = LNPlanTrabajo.ObtenerPlanesDeTrabajo(usuarioActual.IdEmpresa);
            ViewBag.Fk_Id_Sede = new SelectList(sedeServicio.SedesPorEmpresa(usuarioActual.IdEmpresa), "Pk_Id_Sede", "Nombre_Sede", 0);

            var ListaVigencia = recursosServicios.ObtenerPeriodosAnios(anioIncial, anioFinal);
            List<SelectListItem> ListaPeriodicidad = new List<SelectListItem>();
            foreach (var item in ListaVigencia)
            {
                ListaPeriodicidad.Add(new SelectListItem { Text = item.Text, Value = item.Value });
            }
            ViewBag.ListaPeriodo = ListaPeriodicidad;

            if (planes!=null)
            {
                foreach (var item in planes)
                {
                    string Origen = item.Tipo;
                    DateTime FechaApl = item.FechaAplicacion ?? DateTime.MinValue;
                    if (Origen!=null && FechaApl!= DateTime.MinValue)
                    {
                        Origen = Origen + " - Fecha de Aplicación: " + FechaApl.ToShortDateString();
                    }
                    item.Tipo = Origen;
                }
            }

            List<SelectListItem> ListaTipo = new List<SelectListItem>();
            ListaTipo.Add(new SelectListItem { Text = "Batería Psicosocial", Value = "Batería Psicosocial" });
            ListaTipo.Add(new SelectListItem { Text = "Plan Estratégico de Seguridad Vial", Value = "Plan Estratégico de Seguridad Vial" });
            ViewBag.ListaTipos = ListaTipo;

            return View(planes);
        }
        [HttpPost]
        public ActionResult GestionarPlanesTrabajo(FormCollection frm)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
                return RedirectToAction("Login", "Home");
            }

            string fantes = "";
            string fdespues = "";
            string Vigencia = "";
            string Sede = "";
            string Tipo = "";

            ViewBag.val_fecha1 = "";
            ViewBag.val_fecha2 = "";
            ViewBag.Vigencia = "";

            if (frm["FechaAntes"] != null)
            {
                fantes = frm["FechaAntes"].ToString();
                ViewBag.val_fecha1 = frm["FechaAntes"].ToString();
            }
            if (frm["FechaDespues"] != null)
            {
                fdespues = frm["FechaDespues"].ToString();
                ViewBag.val_fecha2 = frm["FechaDespues"].ToString();
            }
            if (frm["ListaPeriodo"] != null)
            {
                Vigencia = frm["ListaPeriodo"].ToString();
            }
            int idVigencia = 0;
            if (int.TryParse(Vigencia, out idVigencia))
            {
            }
            if (frm["Fk_Id_Sede"] != null)
            {
                Sede = frm["Fk_Id_Sede"].ToString();
            }
            int idSede = 0;
            if (int.TryParse(Sede, out idSede))
            {
            }
            if (frm["ListaTipos"] != null)
            {
                Tipo = frm["ListaTipos"].ToString();
            }


            List<EDAplicacionPlanTrabajo> planes = LNPlanTrabajo.ObtenerPlanesDeTrabajoFiltro(usuarioActual.IdEmpresa, fantes, fdespues, idVigencia, idSede, Tipo);
            var ListaVigencia = recursosServicios.ObtenerPeriodosAnios(anioIncial, anioFinal);
            List<SelectListItem> ListaPeriodicidad = new List<SelectListItem>();
            foreach (var item in ListaVigencia)
            {
                if (Vigencia == item.Value)
                {
                    ListaPeriodicidad.Add(new SelectListItem { Text = item.Text, Value = item.Value, Selected = true });
                }
                else
                {
                    ListaPeriodicidad.Add(new SelectListItem { Text = item.Text, Value = item.Value});
                }
            }
            ViewBag.ListaPeriodo = ListaPeriodicidad;


            List<SelectListItem> ListaTipo = new List<SelectListItem>();
            

            if (Tipo == null)
            {
                ListaTipo.Add(new SelectListItem { Text = "Plan Estratégico de Seguridad Vial", Value = "Plan Estratégico de Seguridad Vial" });
                ListaTipo.Add(new SelectListItem { Text = "Batería Psicosocial", Value = "Batería Psicosocial" });
            }
            else
            {
                if (Tipo == "")
                {
                    ListaTipo.Add(new SelectListItem { Text = "Plan Estratégico de Seguridad Vial", Value = "Plan Estratégico de Seguridad Vial"});
                    ListaTipo.Add(new SelectListItem { Text = "Batería Psicosocial", Value = "Batería Psicosocial" });
                }
                else
                {
                    if (Tipo == "Batería Psicosocial")
                    {
                        ListaTipo.Add(new SelectListItem { Text = "Batería Psicosocial", Value = "Batería Psicosocial", Selected = true });
                    }
                    else
                    {
                        ListaTipo.Add(new SelectListItem { Text = "Batería Psicosocial", Value = "Batería Psicosocial" });
                    }

                    if (Tipo == "Plan Estratégico de Seguridad Vial")
                    {
                        ListaTipo.Add(new SelectListItem { Text = "Plan Estratégico de Seguridad Vial", Value = "Plan Estratégico de Seguridad Vial", Selected = true });
                    }
                    else
                    {
                        ListaTipo.Add(new SelectListItem { Text = "Plan Estratégico de Seguridad Vial", Value = "Plan Estratégico de Seguridad Vial" });
                    }
                }
            }
            



            ViewBag.ListaTipos = ListaTipo;

            var ListaSedes = new SelectList(sedeServicio.SedesPorEmpresa(usuarioActual.IdEmpresa), "Pk_Id_Sede", "Nombre_Sede");
            foreach (var item in ListaSedes)
            {
                if (Sede == item.Value)
                {
                    item.Selected = true;
                }
            }
            ViewBag.Fk_Id_Sede = ListaSedes;

            if (planes != null)
            {
                foreach (var item in planes)
                {
                    string Origen = item.Tipo;
                    DateTime FechaApl = item.FechaAplicacion ?? DateTime.MinValue;
                    if (Origen != null && FechaApl != DateTime.MinValue)
                    {
                        Origen = Origen + " - Fecha de Aplicación: " + FechaApl.ToShortDateString();
                    }
                    item.Tipo = Origen;
                }
            }

            return View(planes);
        }
        [HttpGet]
        public ActionResult CrearPlanTrabajo()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
                return RedirectToAction("Login", "Home");
            }
            ViewBag.Vigencia = recursosServicios.ObtenerPeriodosAnios(anioIncial, anioFinal);
            ViewBag.Fk_Id_Sede = new SelectList(sedeServicio.SedesPorEmpresa(usuarioActual.IdEmpresa), "Pk_Id_Sede", "Nombre_Sede", 0);

            //var ListaSedes = new List<EDSede>();
            //ViewBag.Pk_Id_Sede = new SelectList(sedeRepositorio.SedesPorEmpresa(usuarioActual.IdEmpresa), "Pk_Id_Sede", "Nombre_Sede", 0);

            List<SelectListItem> ListaTipo = new List<SelectListItem>();
            ListaTipo.Add(new SelectListItem { Text = "Batería Psicosocial", Value = "Batería Psicosocial" });
            ListaTipo.Add(new SelectListItem { Text = "Plan Estratégico de Seguridad Vial", Value = "Plan Estratégico de Seguridad Vial" });
            ViewBag.ListaTipos = ListaTipo;




            return View();
        }
        [HttpPost]
        public ActionResult CrearPlanTrabajo(EDAplicacionPlanTrabajo EDAplicacionPlanTrabajo)
        {
            bool existeerror = false;
            bool respuesta = false;
            string Estado = "No se creó el plan de trabajo, por favor revise la información suministrada";
            string url = "0";


            string[] Validacion = new string[7] { "", "", "", "", "", "","" };
            bool[] boolValidacion = new bool[7] { false, false, false, false, false, false, false };

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                Estado = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { Estado, respuesta, Validacion, boolValidacion });
            }
            //validar sede
            List <Sede> ListaSedes= sedeServicio.SedesPorEmpresa(usuarioActual.IdEmpresa);
            if (ListaSedes!=null)
            {
                Sede Sede = ListaSedes.Where(s => s.Pk_Id_Sede == EDAplicacionPlanTrabajo.Fk_Id_Sede).FirstOrDefault();
                if (Sede==null)
                {
                    boolValidacion[0] = true;
                    Validacion[0] = "No ha seleccionado una sede para este plan de trabajo";
                    existeerror = true;
                }
            }
            else
            {
                return Json(new { Estado, respuesta, Validacion, boolValidacion });
            }
            //Validar Fechas
            if (EDAplicacionPlanTrabajo.FechaInicio == DateTime.MinValue)
            {
                boolValidacion[1] = true;
                Validacion[1] = "No ha seleccionado un fecha de inicio para este plan de trabajo";
                existeerror = true;
            }
           
            if (EDAplicacionPlanTrabajo.FechaFinal==DateTime.MinValue)
            {
                boolValidacion[2] = true;
                Validacion[2] = "No ha seleccionado un fecha de finalización para este plan de trabajo";
                existeerror = true;
            }
            if (EDAplicacionPlanTrabajo.Vigencia <= 2000)
            {
                boolValidacion[3] = true;
                Validacion[3] = "No ha seleccionado la vigencia de este plan de trabajo";
                existeerror = true;
            }
            else
            {
                string vigenciastr = EDAplicacionPlanTrabajo.Vigencia.ToString();
                if (vigenciastr.Length!=4)
                {
                    boolValidacion[3] = true;
                    Validacion[3] = "No ha seleccionado la vigencia de este plan de trabajo";
                    existeerror = true;
                }
            }

            if (EDAplicacionPlanTrabajo.FechaInicio != DateTime.MinValue && EDAplicacionPlanTrabajo.FechaFinal != DateTime.MinValue)
            {
                if (EDAplicacionPlanTrabajo.FechaFinal<= EDAplicacionPlanTrabajo.FechaInicio)
                {
                    boolValidacion[1] = true;
                    Validacion[1] = "La fecha de inicio no puede ser superior a la fecha de finalización del plan de trabajo";
                    existeerror = true;
                }
            }


            if (EDAplicacionPlanTrabajo.Tipo==null)
            {
                boolValidacion[4] = true;
                Validacion[4] = "No ha eligido el origen del plan de trabajo";
                existeerror = true;
            }
            else
            {
                if (EDAplicacionPlanTrabajo.Tipo== "Batería Psicosocial")
                {
                    if (EDAplicacionPlanTrabajo.FechaAplicacion == null)
                    {
                        boolValidacion[5] = true;
                        Validacion[5] = "Ha eligido el origen 'Batería Psicosocial', debe suministrar la fecha de aplicación";
                        existeerror = true;
                    }
                }
                
            }
            //Validar Vigencia
            int añovig = EDAplicacionPlanTrabajo.Vigencia;
            int añofechaini = EDAplicacionPlanTrabajo.FechaInicio.Year;
            if (añovig< añofechaini)
            {
                boolValidacion[6] = true;
                Validacion[6] = "El año de la vigencia no puede ser inferior al año de la fecha de inicio del plan de trabajo";
                existeerror = true;
            }



            List<EDAplicacionPlanTrabajo> tempValidar=LNPlanTrabajo.ObtenerPlanesDeTrabajo(usuarioActual.IdEmpresa);

            if (!existeerror)
            {
                bool incumplefechas = false;

                foreach (var item in tempValidar)
                {
                    if (item.Fk_Id_Sede == EDAplicacionPlanTrabajo.Fk_Id_Sede)
                    {
                        if (EDAplicacionPlanTrabajo.FechaInicio >= item.FechaInicio   && EDAplicacionPlanTrabajo.FechaInicio  <= item.FechaFinal && item.Tipo== EDAplicacionPlanTrabajo.Tipo)
                        {
                            incumplefechas = true;
                            existeerror = true;
                        }
                        if (EDAplicacionPlanTrabajo.FechaFinal >= item.FechaInicio && EDAplicacionPlanTrabajo.FechaFinal <= item.FechaFinal && item.Tipo == EDAplicacionPlanTrabajo.Tipo)
                        {
                            incumplefechas = true;
                            existeerror = true;
                        }
                    }
                }
                if (incumplefechas)
                {
                    Estado = "La fecha de inicio o finalización no pueden coincidir con el rango de fechas de otro plan de trabajo para la sede seleccionada y origen, por favor verifique los planes de trabajo ya creados";
                }
            }

            if (!existeerror)
            {
                respuesta = LNPlanTrabajo.CrearPlanTrabajo(EDAplicacionPlanTrabajo);
                if (respuesta)
                {
                    Estado = "El plan de trabajo fue creado exitosamente";
                    List<EDAplicacionPlanTrabajo> temp = LNPlanTrabajo.ObtenerPlanesDeTrabajo(usuarioActual.IdEmpresa);
                    url = temp.Last().Pk_Id_PlanTrabajo.ToString();
                    return Json(new { Estado, respuesta, Validacion, boolValidacion, url });
                }
            }
           
            return Json(new { Estado, respuesta, Validacion, boolValidacion });
        }
        [HttpGet]
        public ActionResult EditarPlanTrabajo(int Pk_Id_PlanTrabajo)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
                return RedirectToAction("Login", "Home");
            }
            int int_FileSizeLimit = 0;
            int int_MB = 0;
            // Set the maximum file size for uploads in bytes.
            HttpRuntimeSection section = ConfigurationManager.GetSection("system.web/httpRuntime") as HttpRuntimeSection;
            if (section != null)
            {
                // Retreive the maximum request length from the web.config and convert to bytes.
                int_FileSizeLimit = (section.MaxRequestLength * 1024);
                double megabytes1 = ConvertBytesToMegabytes((long)int_FileSizeLimit);
                int_MB = (int)Math.Floor(megabytes1);
            }
            else
            {
                int_FileSizeLimit = 31457280;
            }
            ViewBag.LimiteMB = int_MB;

            ViewBag.Imagen1E = "max-width: 100%;max-height: 100%;display:none";
            ViewBag.Imagen1D = "display:none";
            ViewBag.Imagen1R = "";

            ViewBag.Imagen2E = "max-width: 100%;max-height: 100%;display:none";
            ViewBag.Imagen2D = "display:none";
            ViewBag.Imagen2R = "";

            string RutaImagen1 = string.Empty;
            string RutaImagen2 = string.Empty;

            EDAplicacionPlanTrabajo plan = LNPlanTrabajo.ConsultarPlanTrabajo(Pk_Id_PlanTrabajo, usuarioActual.IdEmpresa);
            if (plan!=null)
            {
                List<Sede> ListaSedes = sedeServicio.SedesPorEmpresa(usuarioActual.IdEmpresa);
                plan.NombreSede = ListaSedes.Where(s => s.Pk_Id_Sede == plan.Fk_Id_Sede).FirstOrDefault().Nombre_Sede;
            }

            plan.ListaMeses = new List<EDPlanTrabajoMeses>();

            DateTime dt1 = plan.FechaInicio;
            DateTime dt2 = plan.FechaFinal;

            int cont = 0;
            while (dt1 < dt2)
            {
                dt1 = dt1.AddMonths(1);
                cont++;
            }
            dt1 = plan.FechaInicio;
            dt2 = plan.FechaFinal;

            int cont1 = 0;
            while (dt1 <= dt2)
            {
                EDPlanTrabajoMeses EDPlanTrabajoMeses = new EDPlanTrabajoMeses();
                if (cont1==0)
                {
                    EDPlanTrabajoMeses.fecha_inicio = dt1;
                    int diasxmes = DateTime.DaysInMonth(dt1.Year, dt1.Month);
                    EDPlanTrabajoMeses.fecha_despues = new DateTime(dt1.Year, dt1.Month, diasxmes);

                }
                if (cont1 == cont-1)
                {
                    DateTime dt0 = new DateTime(dt1.Year, dt1.Month, 1);
                    DateTime dt01 = new DateTime(dt1.Year, dt1.Month, 1);
                    EDPlanTrabajoMeses.fecha_inicio = dt01;
                    EDPlanTrabajoMeses.fecha_despues = dt2;
                }
                if (cont1 != cont-1 && cont1!=0)
                {
                    DateTime dt0 = new DateTime(dt1.Year, dt1.Month, 1);
                    int diasxmes = DateTime.DaysInMonth(dt1.Year, dt1.Month);
                    EDPlanTrabajoMeses.fecha_despues = new DateTime(dt1.Year, dt1.Month, diasxmes);
                    EDPlanTrabajoMeses.fecha_inicio = dt1;
                }
                EDPlanTrabajoMeses.mes = dt1.ToString("MMMM yyyy");
                EDPlanTrabajoMeses.mes_table = dt1.ToString("MMMM");
                dt1 = dt1.AddMonths(1);
                plan.ListaMeses.Add(EDPlanTrabajoMeses);
                cont1++;
            }


            DateTime FechaHoy = DateTime.Now;
            foreach (var item in plan.ListaDetalles)
            {
                if (item.ListaActividades!=null)
                {
                    foreach (var item1 in item.ListaActividades)
                    {
                        if (item1.Estado != 3)
                        {
                            if (FechaHoy > item1.FechaEstado)
                            {
                                item1.Estado = 4;
                            }
                        }
                    }
                }
                else
                {
                    item.ListaActividades = new List<EDAplicacionPlanTrabajoActividad>();
                }
                
            }

            if (plan!=null)
            {
                if (plan.RepLegalImagen != null && plan.RepLegalRuta != null)
                {
                    RutaImagen1 = Server.MapPath(Path.Combine(plan.RepLegalRuta, plan.RepLegalImagen));
                    if (System.IO.File.Exists(RutaImagen1))
                    {
                        ViewBag.Imagen1E = "max-width: 100%;max-height: 100%;display:initial";
                        ViewBag.Imagen1D = "display:";
                        try
                        {
                            Bitmap bitmap;
                            using (var bmpTemp = new Bitmap(RutaImagen1))
                            {
                                bitmap = new Bitmap(bmpTemp);
                            }
                            using (var newImage = ScaleImage(bitmap, 300, 300))
                            {
                                ViewBag.Imagen1R = "data:image/png;base64," + ImageToBase64String(newImage, ImageFormat.Png);
                            }
                        }
                        catch (Exception)
                        {
                        }

                    }
                }

                if (plan.RepSGSSTImagen != null && plan.RepSGSSTRuta != null)
                {
                    RutaImagen2 = Server.MapPath(Path.Combine(plan.RepSGSSTRuta, plan.RepSGSSTImagen));
                    if (System.IO.File.Exists(RutaImagen2))
                    {
                        ViewBag.Imagen2E = "max-width: 100%;max-height: 100%;display:initial";
                        ViewBag.Imagen2D = "display:";
                        try
                        {
                            Bitmap bitmap;
                            using (var bmpTemp = new Bitmap(RutaImagen2))
                            {
                                bitmap = new Bitmap(bmpTemp);
                            }
                            using (var newImage = ScaleImage(bitmap, 300, 300))
                            {
                                ViewBag.Imagen2R = "data:image/png;base64," + ImageToBase64String(newImage, ImageFormat.Png);
                            }
                        }
                        catch (Exception)
                        {
                        }

                    }
                }
            }

            if (plan != null)
            {
                    string Origen = plan.Tipo;
                    DateTime FechaApl = plan.FechaAplicacion ?? DateTime.MinValue;
                    if (Origen != null && FechaApl != DateTime.MinValue)
                    {
                        Origen = Origen + " - Fecha de Aplicación: " + FechaApl.ToShortDateString();
                    }
                    plan.Tipo = Origen;
            }

            return View(plan);
        }
        [HttpPost]
        public ActionResult GuardarEditarPlanTrabajo(EDAplicacionPlanTrabajo EDAplicacionPlanTrabajo)
        {
            bool Probar = false;
            string Estado = "No se guardó el plan de trabajo, por favor revise la información suministrada";
            bool[] Validacion = new bool[4];
            string[] ValidacionStr = new string[4];
            string url = EDAplicacionPlanTrabajo.Pk_Id_PlanTrabajo.ToString();
            for (int i = 0; i < 4; i++)
            {
                Validacion[i] = false;
                ValidacionStr[i] = "";
            }
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                Estado = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { Estado, Probar, Validacion, ValidacionStr, url });
            }


            if (ModelState.IsValid)
            {
                EDAplicacionPlanTrabajo ActaulEDPlan = LNPlanTrabajo.ConsultarPlanTrabajo(EDAplicacionPlanTrabajo.Pk_Id_PlanTrabajo, usuarioActual.IdEmpresa);
                CrearCarpeta(RutaImagenes);
                CrearCarpeta(RutaImagenesTemp);
                #region ImagenRepLegal
                List<string> ArchivosTemporalesEliminar = new List<string>();
                List<string> ArchivosMover = new List<string>();

                if (EDAplicacionPlanTrabajo.RepLegalImagen != null)
                {
                    if (ActaulEDPlan.RepLegalImagen != null)
                    {
                        if (EDAplicacionPlanTrabajo.RepLegalImagen == ActaulEDPlan.RepLegalImagen)
                        {
                            //Conservar Anterior
                            EDAplicacionPlanTrabajo.RepLegalRuta = ActaulEDPlan.RepLegalRuta;
                            EDAplicacionPlanTrabajo.RepLegalImagen = ActaulEDPlan.RepLegalImagen;
                        }
                        else
                        {
                            //Conservar recien subida
                            string PathActual = Server.MapPath(Path.Combine(ActaulEDPlan.RepLegalRuta, ActaulEDPlan.RepLegalImagen));
                            string PathOrigen = Server.MapPath(Path.Combine(RutaImagenesTemp, EDAplicacionPlanTrabajo.RepLegalImagen));
                            if (System.IO.File.Exists(PathActual))
                            {
                                ArchivosTemporalesEliminar.Add(PathActual);
                            }
                            if (System.IO.File.Exists(PathOrigen))
                            {
                                try
                                {
                                    EDAplicacionPlanTrabajo.RepLegalRuta = RutaImagenes;
                                    EDAplicacionPlanTrabajo.RepLegalImagen = EDAplicacionPlanTrabajo.RepLegalImagen;

                                    string pathsave = Server.MapPath(Path.Combine(EDAplicacionPlanTrabajo.RepLegalRuta, EDAplicacionPlanTrabajo.RepLegalImagen));
                                    ArchivosMover.Add(pathsave + "[stop]" + PathOrigen);
                                    ArchivosTemporalesEliminar.Add(PathOrigen);
                                }
                                catch (Exception)
                                {
                                }
                            }
                        }
                    }
                    else
                    {
                        //guardar unica imagen
                        string PathOrigen = Server.MapPath(Path.Combine(RutaImagenesTemp, EDAplicacionPlanTrabajo.RepLegalImagen));
                        if (System.IO.File.Exists(PathOrigen))
                        {
                            try
                            {
                                EDAplicacionPlanTrabajo.RepLegalRuta = RutaImagenes;
                                EDAplicacionPlanTrabajo.RepLegalImagen = EDAplicacionPlanTrabajo.RepLegalImagen;

                                string pathsave = Server.MapPath(Path.Combine(EDAplicacionPlanTrabajo.RepLegalRuta, EDAplicacionPlanTrabajo.RepLegalImagen));
                                ArchivosMover.Add(pathsave + "[stop]" + PathOrigen);
                                ArchivosTemporalesEliminar.Add(PathOrigen);
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }
                }
                else
                {
                    if (ActaulEDPlan.RepLegalImagen != null)
                    {
                        //Eliminar Imagen
                        string PathActual = Server.MapPath(Path.Combine(ActaulEDPlan.RepLegalRuta, ActaulEDPlan.RepLegalImagen));
                        EDAplicacionPlanTrabajo.RepLegalRuta = null;
                        EDAplicacionPlanTrabajo.RepLegalImagen = null;

                        if (System.IO.File.Exists(PathActual))
                        {
                            ArchivosTemporalesEliminar.Add(PathActual);
                        }
                    }
                    else
                    {
                        //ninguna accion
                        EDAplicacionPlanTrabajo.RepLegalRuta = null;
                        EDAplicacionPlanTrabajo.RepLegalImagen = null;

                    }
                }



                #endregion
                #region ImagenRepSgsst


                if (EDAplicacionPlanTrabajo.RepSGSSTImagen != null)
                {
                    if (ActaulEDPlan.RepSGSSTImagen != null)
                    {
                        if (EDAplicacionPlanTrabajo.RepSGSSTImagen == ActaulEDPlan.RepSGSSTImagen)
                        {
                            //Conservar Anterior
                            EDAplicacionPlanTrabajo.RepSGSSTRuta = ActaulEDPlan.RepSGSSTRuta;
                            EDAplicacionPlanTrabajo.RepSGSSTImagen = ActaulEDPlan.RepSGSSTImagen;
                        }
                        else
                        {
                            //Conservar recien subida
                            string PathActual = Server.MapPath(Path.Combine(ActaulEDPlan.RepSGSSTRuta, ActaulEDPlan.RepSGSSTImagen));
                            string PathOrigen = Server.MapPath(Path.Combine(RutaImagenesTemp, EDAplicacionPlanTrabajo.RepSGSSTImagen));
                            if (System.IO.File.Exists(PathActual))
                            {
                                ArchivosTemporalesEliminar.Add(PathActual);
                            }
                            if (System.IO.File.Exists(PathOrigen))
                            {
                                try
                                {
                                    EDAplicacionPlanTrabajo.RepSGSSTRuta = RutaImagenes;
                                    EDAplicacionPlanTrabajo.RepSGSSTImagen = EDAplicacionPlanTrabajo.RepSGSSTImagen;

                                    string pathsave = Server.MapPath(Path.Combine(EDAplicacionPlanTrabajo.RepSGSSTRuta, EDAplicacionPlanTrabajo.RepSGSSTImagen));
                                    ArchivosMover.Add(pathsave + "[stop]" + PathOrigen);
                                    ArchivosTemporalesEliminar.Add(PathOrigen);
                                }
                                catch (Exception)
                                {
                                }
                            }
                        }
                    }
                    else
                    {
                        //guardar unica imagen
                        string PathOrigen = Server.MapPath(Path.Combine(RutaImagenesTemp, EDAplicacionPlanTrabajo.RepSGSSTImagen));
                        if (System.IO.File.Exists(PathOrigen))
                        {
                            try
                            {
                                EDAplicacionPlanTrabajo.RepSGSSTRuta = RutaImagenes;
                                EDAplicacionPlanTrabajo.RepSGSSTImagen = EDAplicacionPlanTrabajo.RepSGSSTImagen;

                                string pathsave = Server.MapPath(Path.Combine(EDAplicacionPlanTrabajo.RepSGSSTRuta, EDAplicacionPlanTrabajo.RepSGSSTImagen));
                                ArchivosMover.Add(pathsave + "[stop]" + PathOrigen);
                                ArchivosTemporalesEliminar.Add(PathOrigen);
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }
                }
                else
                {
                    if (ActaulEDPlan.RepSGSSTImagen != null)
                    {
                        //Eliminar Imagen
                        string PathActual = Server.MapPath(Path.Combine(ActaulEDPlan.RepSGSSTRuta, ActaulEDPlan.RepSGSSTImagen));
                        EDAplicacionPlanTrabajo.RepSGSSTRuta = null;
                        EDAplicacionPlanTrabajo.RepSGSSTImagen = null;

                        if (System.IO.File.Exists(PathActual))
                        {
                            ArchivosTemporalesEliminar.Add(PathActual);
                        }
                    }
                    else
                    {
                        //ninguna accion
                        EDAplicacionPlanTrabajo.RepSGSSTRuta = null;
                        EDAplicacionPlanTrabajo.RepSGSSTImagen = null;

                    }
                }



                #endregion
                bool ProbarGuardado = LNPlanTrabajo.EditarPlan(EDAplicacionPlanTrabajo);
                if (ProbarGuardado)
                {
                    foreach (var item in ArchivosMover)
                    {
                        string[] stringSeparators = new string[] { "[stop]" };
                        string[] Resultado;
                        Resultado = item.Split(stringSeparators, StringSplitOptions.None);
                        try
                        {
                            System.IO.File.Move(Resultado[1], Resultado[0]);
                        }
                        catch (Exception)
                        {
                        }
                    }
                    foreach (var item in ArchivosTemporalesEliminar)
                    {
                        try
                        {
                            if (System.IO.File.Exists(item))
                            {
                                System.IO.File.Delete(item);
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }

                    Probar = ProbarGuardado;
                    return Json(new { Estado, Probar, url });
                }
            }

            
            return Json(new {Estado, Probar, Validacion, ValidacionStr, url }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult EliminarPlanDeTrabajo(int Pk_Id_PlanTrabajo)
        {
            bool probar = false;
            string resultado = "El plan de trabajo no pudo ser  eliminado";
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);

            if (usuarioActual == null)
            {
                resultado = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
            }

            probar = LNPlanTrabajo.EliminarPlanDeTrabajo(Pk_Id_PlanTrabajo);

            if (probar)
            {
                resultado = "El plan de trabajo se ha eliminado correctamente";
                return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
            }
            if (!probar)
            {
                resultado = "No se puede eliminar el plan de trabajo, por que existen objetivos registrados dentro de este plan de trabajo. Elimine primero objetivos y actividades de este plan de trabajo y vuelva a intentar";
                return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult EliminarObjetivoPlanDeTrabajo(int Pk_Id_ObjetivoPlanTrabajo)
        {
            bool probar = false;
            string resultado = "El objetivo del  plan de trabajo no pudo ser  eliminado";
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);

            if (usuarioActual == null)
            {
                resultado = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
            }

            probar = LNPlanTrabajo.EliminarObjetivoPlanDeTrabajo(Pk_Id_ObjetivoPlanTrabajo);

            if (probar)
            {
                resultado = "El objetivo del plan de trabajo se ha eliminado correctamente";
                return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
            }
            if (!probar)
            {
                resultado = "No se puede eliminar el objetivo del plan de trabajo, por que existen actividades dentro de este objetivo. Elimine primero las actividades que tiene este objetivo y vuelva a intentar";
                return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult EliminarActividadPlanDeTrabajo(int Pk_Id_ActividadPlanTrabajo)
        {
            bool probar = false;
            string resultado = "La actividad del plan de trabajo no pudo ser eliminado";
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);

            if (usuarioActual == null)
            {
                resultado = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
            }

            probar = LNPlanTrabajo.EliminarActividadPlanDeTrabajo(Pk_Id_ActividadPlanTrabajo);

            if (probar)
            {
                resultado = "La actividad del plan de trabajo se ha eliminado correctamente";
                return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
            }
            if (!probar)
            {
                resultado = "No se puede eliminar la actividad del plan de trabajo, la actividad tiene programación registrada. Elimine primero la programación de esta actividad antes de intentar eliminarla";
                return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult EliminarProgramaPlanDeTrabajo(int Pk_Id_ProgramaPlanTrabajo)
        {
            bool probar = false;
            string resultado = "La actividad del objetivo del  plan de trabajo no pudo ser  eliminado";
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);

            if (usuarioActual == null)
            {
                resultado = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
            }

            probar = LNPlanTrabajo.EliminarProgramaPlanDeTrabajo(Pk_Id_ProgramaPlanTrabajo);

            if (probar)
            {
                resultado = "La actividad del objetivo del plan de trabajo se ha eliminado correctamente";
                return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
            }
            if (!probar)
            {
                resultado = "No se puede eliminar la actividad del objetivo del plan de trabajo, por que existen objetivos";
                return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);

        }
        [HttpGet]
        public ActionResult ExportarExcel(string id)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
                return RedirectToAction("Login", "Home");
            }
            #region ConsultaPlan

            int IdInt = 0;
            if (int.TryParse(id, out IdInt))
            {
            }
            string RutaImagen1 = string.Empty;
            string RutaImagen2 = string.Empty;

            EDAplicacionPlanTrabajo plan = LNPlanTrabajo.ConsultarPlanTrabajo(IdInt, usuarioActual.IdEmpresa);
            if (plan != null)
            {
                List<Sede> ListaSedes = sedeServicio.SedesPorEmpresa(usuarioActual.IdEmpresa);
                plan.NombreSede = ListaSedes.Where(s => s.Pk_Id_Sede == plan.Fk_Id_Sede).FirstOrDefault().Nombre_Sede;
            }

            plan.ListaMeses = new List<EDPlanTrabajoMeses>();

            DateTime dt1 = plan.FechaInicio;
            DateTime dt2 = plan.FechaFinal;

            int cont = 0;
            while (dt1 < dt2)
            {
                dt1 = dt1.AddMonths(1);
                cont++;
            }
            dt1 = plan.FechaInicio;
            dt2 = plan.FechaFinal;

            int cont1 = 0;
            while (dt1 <= dt2)
            {
                EDPlanTrabajoMeses EDPlanTrabajoMeses = new EDPlanTrabajoMeses();
                if (cont1 == 0)
                {
                    EDPlanTrabajoMeses.fecha_inicio = dt1;
                    int diasxmes = DateTime.DaysInMonth(dt1.Year, dt1.Month);
                    EDPlanTrabajoMeses.fecha_despues = new DateTime(dt1.Year, dt1.Month, diasxmes);

                }
                if (cont1 == cont - 1)
                {
                    DateTime dt0 = new DateTime(dt1.Year, dt1.Month, 1);
                    DateTime dt01 = new DateTime(dt1.Year, dt1.Month, 1);
                    EDPlanTrabajoMeses.fecha_inicio = dt01;
                    EDPlanTrabajoMeses.fecha_despues = dt2;
                }
                if (cont1 != cont - 1 && cont1 != 0)
                {
                    DateTime dt0 = new DateTime(dt1.Year, dt1.Month, 1);
                    int diasxmes = DateTime.DaysInMonth(dt1.Year, dt1.Month);
                    EDPlanTrabajoMeses.fecha_despues = new DateTime(dt1.Year, dt1.Month, diasxmes);
                    EDPlanTrabajoMeses.fecha_inicio = dt1;
                }
                EDPlanTrabajoMeses.mes = dt1.ToString("MMMM yyyy");
                EDPlanTrabajoMeses.mes_table = dt1.ToString("MMMM");
                dt1 = dt1.AddMonths(1);
                plan.ListaMeses.Add(EDPlanTrabajoMeses);
                cont1++;
            }


            DateTime FechaHoy = DateTime.Now;
            foreach (var item in plan.ListaDetalles)
            {
                if (item.ListaActividades != null)
                {
                    foreach (var item1 in item.ListaActividades)
                    {
                        if (item1.Estado != 3)
                        {
                            if (FechaHoy > item1.FechaEstado)
                            {
                                item1.Estado = 4;
                            }
                        }
                    }
                }
                else
                {
                    item.ListaActividades = new List<EDAplicacionPlanTrabajoActividad>();
                }

            }

            if (plan != null)
            {
                if (plan.RepLegalImagen != null && plan.RepLegalRuta != null)
                {
                    RutaImagen1 = Server.MapPath(Path.Combine(plan.RepLegalRuta, plan.RepLegalImagen));
                    if (System.IO.File.Exists(RutaImagen1))
                    {
                        try
                        {
                            Bitmap bitmap;
                            using (var bmpTemp = new Bitmap(RutaImagen1))
                            {
                                bitmap = new Bitmap(bmpTemp);
                            }
                            using (var newImage = ScaleImage(bitmap, 300, 300))
                            {
                                string base64 = "data:image/png;base64," + ImageToBase64String(newImage, ImageFormat.Png);
                            }
                        }
                        catch (Exception)
                        {
                        }

                    }
                }

                if (plan.RepSGSSTImagen != null && plan.RepSGSSTRuta != null)
                {
                    RutaImagen2 = Server.MapPath(Path.Combine(plan.RepSGSSTRuta, plan.RepSGSSTImagen));
                    if (System.IO.File.Exists(RutaImagen2))
                    {
                        try
                        {
                            Bitmap bitmap;
                            using (var bmpTemp = new Bitmap(RutaImagen2))
                            {
                                bitmap = new Bitmap(bmpTemp);
                            }
                            using (var newImage = ScaleImage(bitmap, 300, 300))
                            {
                                string base64 = "data:image/png;base64," + ImageToBase64String(newImage, ImageFormat.Png);
                            }
                        }
                        catch (Exception)
                        {
                        }

                    }
                }
            }

            if (plan != null)
            {
                string Origen = plan.Tipo;
                DateTime FechaApl = plan.FechaAplicacion ?? DateTime.MinValue;
                if (Origen != null && FechaApl != DateTime.MinValue)
                {
                    Origen = Origen + " - Fecha de Aplicación: " + FechaApl.ToShortDateString();
                }
                plan.Tipo = Origen;
            }
            #endregion


            using (ExcelPackage wb = new ExcelPackage())
            {
                ExcelWorksheet ws0 = wb.Workbook.Worksheets.Add("PLAN DE TRABAJO");

                Color graycolor = ColorTranslator.FromHtml("#c1c7c4");
                Color Naranjacolor = ColorTranslator.FromHtml("#db943d");
                Color Azulcolor = ColorTranslator.FromHtml("#1ea8be");
                Color Verdecolor = ColorTranslator.FromHtml("#7ac851");


                int numerocol = 6 + plan.ListaMeses.Count();
                int numefilastotal = 0;
                int numeromeses= plan.ListaMeses.Count();
                int numerocolssegfila = 0;
                foreach (var item in plan.ListaDetalles)
                {
                    numefilastotal += item.ListaActividades.Count();
                }
                ws0.Row(1).Height = 27;
                ws0.Row(2).Height = 27;
                ws0.Row(3).Height = 32;

                
                #region SegundaFila:obj-rec-act
                ws0.Cells[2, 1].Style.Font.Bold = true;
                ws0.Cells[2, 1].Style.Font.Size = 12;
                ws0.Cells[2, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws0.Cells[2, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                ws0.Cells[2, 1].Value = "OBJETIVOS";
                ws0.Cells[2, 1, 2, 2].Merge = true;
                ws0.Cells[2, 1, 2, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws0.Cells[2, 1, 2, 2].Style.Fill.BackgroundColor.SetColor(graycolor);
                ws0.Cells[2, 1, 2, 2].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                ws0.Cells[2, 1, 2, 2].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                ws0.Cells[2, 1, 2, 2].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                ws0.Cells[2, 1, 2, 2].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                ws0.Cells[2, 3].Style.Font.Bold = true;
                ws0.Cells[2, 3].Style.Font.Size = 12;
                ws0.Cells[2, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws0.Cells[2, 3].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                ws0.Cells[2, 3].Value = "RECURSOS";
                ws0.Cells[2, 3, 2, 5].Merge = true;
                ws0.Cells[2, 3, 2, 5].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws0.Cells[2, 3, 2, 5].Style.Fill.BackgroundColor.SetColor(graycolor);
                ws0.Cells[2, 3, 2, 5].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                ws0.Cells[2, 3, 2, 5].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                ws0.Cells[2, 3, 2, 5].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                ws0.Cells[2, 3, 2, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                ws0.Cells[2, 6].Style.Font.Bold = true;
                ws0.Cells[2, 6].Style.Font.Size = 12;
                ws0.Cells[2, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws0.Cells[2, 6].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                ws0.Cells[2, 6].Value = "";
                ws0.Cells[2, 6, 2, 7].Merge = true;
                ws0.Cells[2, 6, 2, 7].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws0.Cells[2, 6, 2, 7].Style.Fill.BackgroundColor.SetColor(graycolor);
                ws0.Cells[2, 6, 2, 7].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                ws0.Cells[2, 6, 2, 7].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                ws0.Cells[2, 6, 2, 7].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                ws0.Cells[2, 6, 2, 7].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                numerocolssegfila += 1;
                #endregion
                int rowreg = 4;
                int rowregact = 4;
                #region meses


                int numcolmeses = 8;
                int numcolincial = 8;
                rowregact = 0;
                int colceldas = 8;
                int filceldas = 4;
                string lastRecord = plan.ListaMeses.Last().mes.ToUpper();
                int totalcols = 0;
                int rowfinal = 0;

                int numplaneadosT = 0;
                int numejecutadosT = 0;
                int numreprogramT = 0;
                decimal cumplimientoT = 0;

                foreach (var item in plan.ListaMeses)
                {
                    int numplaneados = 0;
                    int numejecutados = 0;
                    int numreprogram = 0;
                    decimal cumplimiento = 0;

                    int filaDetalle = 4;
                    rowregact = 4;
                    int numdivmes = 0;
                    foreach (var item1 in plan.ListaDetalles)
                    {
                        //rowregact = rowregact+ filaDetalle;
                        filaDetalle = 0;
                        foreach (var item2 in item1.ListaActividades)
                        {
                            numcolmeses = numcolincial;
                            int intentos = 0;
                            foreach (var item3 in item2.ListaProgramacion)
                            {
                                if (item3.FechaProgramacionIncial>= item.fecha_inicio && item3.FechaProgramacionIncial <= item.fecha_despues)
                                {
                                    numplaneados++;
                                    numplaneadosT++;
                                    ws0.Cells[rowregact, numcolmeses].Style.Font.Size = 12;
                                    ws0.Cells[rowregact, numcolmeses].Style.Font.Bold = true;
                                    ws0.Cells[rowregact, numcolmeses].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    ws0.Cells[rowregact, numcolmeses].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                    ws0.Cells[rowregact, numcolmeses].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                    ws0.Cells[rowregact, numcolmeses].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                    ws0.Cells[rowregact, numcolmeses].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                    ws0.Cells[rowregact, numcolmeses].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                                    ExcelComment cmd = ws0.Cells[rowregact, numcolmeses].AddComment("Fecha inicial:" + item3.FechaProgramacionIncial.ToShortDateString() + " - Fecha Programada:" + item3.FechaEstado.ToShortDateString(), "REF1");
                                    cmd.AutoFit = true;
                                    //ws0.Cells[rowregact, numcolmeses].AddComment("Fecha inicial:"+ item3.FechaProgramacionIncial.ToShortDateString()+" - Fecha Programada:"+ item3.FechaEstado.ToShortDateString(), "REF");
                                    if (item3.Estado==1)
                                    {
                                        ws0.Cells[rowregact, numcolmeses].Value = "P";
                                        ws0.Cells[rowregact, numcolmeses].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                        ws0.Cells[rowregact, numcolmeses].Style.Fill.BackgroundColor.SetColor(Azulcolor);
                                    }
                                    if (item3.Estado == 2)
                                    {
                                        numreprogramT++;
                                        numreprogram++;
                                        ws0.Cells[rowregact, numcolmeses].Value = "R";
                                        ws0.Cells[rowregact, numcolmeses].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                        ws0.Cells[rowregact, numcolmeses].Style.Fill.BackgroundColor.SetColor(Naranjacolor);
                                    }
                                    if (item3.Estado == 3)
                                    {
                                        numejecutadosT++;
                                        numejecutados++;
                                        ws0.Cells[rowregact, numcolmeses].Value = "E";
                                        ws0.Cells[rowregact, numcolmeses].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                        ws0.Cells[rowregact, numcolmeses].Style.Fill.BackgroundColor.SetColor(Verdecolor);
                                    }
                                    ws0.Cells[rowregact, numcolmeses].Style.WrapText = true;



                                    numdivmes++;
                                    numcolmeses++;
                                    intentos++;
                                }
                                
                            }
                            if (intentos >= 1)
                            {
                                numdivmes = intentos;
                            }
                            rowregact++;
                        }
                        filaDetalle= item1.ListaActividades.Count()-1;
                        filceldas = rowregact;

                        


                    }
                    if (numplaneados!=0)
                    {
                        cumplimiento = (decimal)numejecutados / (decimal)numplaneados;
                    }
                    
                    if (item.mes.ToUpper()== lastRecord)
                    {
                        rowregact++;
                        if (numdivmes < 4)
                        {
                            int restante = 4 - numdivmes;
                            totalcols = numcolincial + 3;
                        }
                        else
                        {
                            totalcols = numcolincial + numdivmes;
                        }
                    }

                    if (numdivmes<4)
                    {
                        int restante = 4 - numdivmes;
                        int poscol = numcolmeses+1;
                        for (int i = 0; i < restante; i++)
                        {
                            for (int i1 = 4; i1 < rowregact-1; i1++)
                            {
                                ws0.Cells[i1, poscol].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                ws0.Cells[i1, poscol].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                ws0.Cells[i1, poscol].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                ws0.Cells[i1, poscol].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                
                            }
                            poscol++;
                        }
                    }
                    numdivmes = 4;


                    int rangoI = numcolincial;
                    int rangoF = 0;
                    colceldas = numcolincial;
                    if (numdivmes==0)
                    {
                        numcolincial = numcolincial + 1;
                        rangoF = numcolincial-1;
                    }
                    else
                    {
                        numcolincial = numcolincial + numdivmes;
                        rangoF = numcolincial - 1;
                    }
                    int widthmes = 0;
                    if (numdivmes==0 || numdivmes == 1)
                    {
                        widthmes = 12;
                    }
                    if (numdivmes == 2)
                    {
                        widthmes = 6;
                    }
                    if (numdivmes>=3)
                    {
                        widthmes = 4;
                    }

                    ws0.Cells[3, rangoI].Style.Font.Size = 11;
                    ws0.Cells[3, rangoI].Style.Font.Bold = true;
                    ws0.Cells[3, rangoI].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ws0.Cells[3, rangoI].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    ws0.Cells[3, rangoI, 3, rangoF].Merge = true;
                    ws0.Cells[3, rangoI, 3, rangoF].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    ws0.Cells[3, rangoI, 3, rangoF].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    ws0.Cells[3, rangoI, 3, rangoF].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    ws0.Cells[3, rangoI, 3, rangoF].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    ws0.Cells[3, rangoI, 3, rangoF].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    ws0.Cells[3, rangoI, 3, rangoF].Style.Fill.BackgroundColor.SetColor(graycolor);
                    ws0.Cells[3, rangoI].Style.WrapText = true;
                    ws0.Cells[3, rangoI].Value = item.mes.ToUpper();

                    if (item.mes.ToUpper() == lastRecord)
                    {
                        for (int i = rowregact - 1; i < rowregact + 3; i++)
                        {
                            ws0.Cells[i, rangoI].Style.Font.Size = 11;
                            ws0.Cells[i, rangoI].Style.Font.Bold = true;
                            ws0.Cells[i, rangoI].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            ws0.Cells[i, rangoI].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                            ws0.Cells[i, rangoI, i, rangoF].Merge = true;
                            ws0.Cells[i, rangoI, i, rangoF].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            ws0.Cells[i, rangoI, i, rangoF].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                            ws0.Cells[i, rangoI, i, rangoF].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            ws0.Cells[i, rangoI, i, rangoF].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                            ws0.Cells[i, rangoI].Style.WrapText = true;
                        }
                        rowfinal = rowregact - 1;
                        ws0.Cells[rowregact - 1, rangoI].Value = numplaneados;
                        ws0.Cells[rowregact, rangoI].Value = numejecutados;
                        ws0.Cells[rowregact + 1, rangoI].Value = numreprogram;
                        ws0.Cells[rowregact + 2, rangoI].Value = cumplimiento;
                        ws0.Cells[rowregact + 2, rangoI].Style.Numberformat.Format = "#0.00%";


                    }
                    else
                    {
                        for (int i = rowregact; i < rowregact + 4; i++)
                        {
                            ws0.Cells[i, rangoI].Style.Font.Size = 11;
                            ws0.Cells[i, rangoI].Style.Font.Bold = true;
                            ws0.Cells[i, rangoI].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            ws0.Cells[i, rangoI].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                            ws0.Cells[i, rangoI, i, rangoF].Merge = true;
                            ws0.Cells[i, rangoI, i, rangoF].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            ws0.Cells[i, rangoI, i, rangoF].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                            ws0.Cells[i, rangoI, i, rangoF].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            ws0.Cells[i, rangoI, i, rangoF].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                            ws0.Cells[i, rangoI].Style.WrapText = true;
                        }

                        ws0.Cells[rowregact, rangoI].Value = numplaneados;
                        ws0.Cells[rowregact + 1, rangoI].Value = numejecutados;
                        ws0.Cells[rowregact + 2, rangoI].Value = numreprogram;
                        ws0.Cells[rowregact + 3, rangoI].Value = cumplimiento;
                        ws0.Cells[rowregact + 3, rangoI].Style.Numberformat.Format = "#0.00%";
                    }
                    

                    for (int i = rangoI; i < rangoF+1; i++)
                    {
                        ws0.Column(i).Width = widthmes;
                    }
                }

                if (numplaneadosT != 0)
                {
                    cumplimientoT = (decimal)numejecutadosT / (decimal)numplaneadosT;
                }


                if (filceldas!=4)
                {
                    ws0.Cells[4, 8, filceldas - 1, colceldas].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    ws0.Cells[4, 8, filceldas - 1, colceldas].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    ws0.Cells[4, 8, filceldas - 1, colceldas].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    ws0.Cells[4, 8, filceldas - 1, colceldas].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                }
                
                #endregion
                #region resumen
                
                string[] TitulosResumen = new string[4] { "TOTAL ACTIVIDADES PROGRAMADAS", "TOTAL ACTIVIDADES EJECUTADAS", "TOTAL ACTIVIDADES REPROGRAMADAS", "PORCENTAJE DE EJECUCIÓN MES" };
                int cont2 = 0;
                for (int i = rowfinal; i < rowfinal+4; i++)
                {
                    ws0.Cells[i, 7].Style.Font.Bold = true;
                    ws0.Cells[i, 7].Style.Font.Size = 12;
                    ws0.Cells[i, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ws0.Cells[i, 7].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    ws0.Cells[i, 7].Value = TitulosResumen[cont2];
                    ws0.Cells[i, 7].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    ws0.Cells[i, 7].Style.Fill.BackgroundColor.SetColor(graycolor);
                    ws0.Cells[i, 7].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    ws0.Cells[i, 7].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    ws0.Cells[i, 7].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    ws0.Cells[i, 7].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                    ws0.Cells[i, totalcols+1].Style.Font.Bold = true;
                    ws0.Cells[i, totalcols+1].Style.Font.Size = 12;
                    ws0.Cells[i, totalcols+1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ws0.Cells[i, totalcols+1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    ws0.Cells[i, totalcols+1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    ws0.Cells[i, totalcols+1].Style.Fill.BackgroundColor.SetColor(graycolor);
                    ws0.Cells[i, totalcols+1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    ws0.Cells[i, totalcols+1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    ws0.Cells[i, totalcols+1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    ws0.Cells[i, totalcols+1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                    if (i == rowfinal) { ws0.Cells[i, totalcols+1].Value = numplaneadosT; }
                    if (i == rowfinal+1) { ws0.Cells[i, totalcols+1].Value = numejecutadosT; }
                    if (i == rowfinal+2) { ws0.Cells[i, totalcols+1].Value = numreprogramT; }
                    if (i == rowfinal+3) { ws0.Cells[i, totalcols+1].Value = cumplimientoT;
                        ws0.Cells[i, totalcols + 1].Style.Numberformat.Format = "#0.00%";
                    }
                    

                    cont2++;
                }

                ws0.Cells[2, 8].Style.Font.Bold = true;
                ws0.Cells[2, 8].Style.Font.Size = 12;
                ws0.Cells[2, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws0.Cells[2, 8].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                ws0.Cells[2, 8].Value = "CRONOGRAMA";
                ws0.Cells[2, 8, 2, totalcols].Merge = true;
                ws0.Cells[2, 8, 2, totalcols].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws0.Cells[2, 8, 2, totalcols].Style.Fill.BackgroundColor.SetColor(graycolor);
                ws0.Cells[2, 8, 2, totalcols].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                ws0.Cells[2, 8, 2, totalcols].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                ws0.Cells[2, 8, 2, totalcols].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                ws0.Cells[2, 8, 2, totalcols].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                #endregion
                #region Encabezado
                //Encabezado
                ws0.Cells[1, 1].Style.Font.Bold = true;
                ws0.Cells[1, 1].Style.Font.Size = 16;
                ws0.Cells[1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws0.Cells[1, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                ws0.Cells[1, 1].Value = "PLAN DE TRABAJO";
                ws0.Cells[1, 1, 1, totalcols+1].Merge = true;
                #endregion
                #region registros1

                rowreg = 4;
                rowregact = 4;
                foreach (var item in plan.ListaDetalles)
                {
                    int numact = item.ListaActividades.Count();
                    if (numact == 0)
                    {

                    }
                    if (numact == 1)
                    {
                        for (int i = 1; i < 6; i++)
                        {
                            ws0.Cells[rowreg, i].Style.Font.Size = 11;
                            ws0.Cells[rowreg, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            ws0.Cells[rowreg, i].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                            ws0.Cells[rowreg, i, rowreg, i].Merge = true;
                            ws0.Cells[rowreg, i, rowreg, i].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            ws0.Cells[rowreg, i, rowreg, i].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                            ws0.Cells[rowreg, i, rowreg, i].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            ws0.Cells[rowreg, i, rowreg, i].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                            ws0.Cells[rowreg, i].Style.WrapText = true;
                            if (i == 1) { ws0.Cells[rowreg, i].Value = item.Objetivo; }
                            if (i == 2) { ws0.Cells[rowreg, i].Value = item.Metas; }
                            if (i == 3) { ws0.Cells[rowreg, i].Value = item.RecursoHumano; }
                            if (i == 4) { ws0.Cells[rowreg, i].Value = item.RecursoTec; }
                            if (i == 5) { ws0.Cells[rowreg, i].Value = item.RecursoFinanciero; }
                        }
                        rowreg += 1;
                    }
                    if (numact > 1)
                    {
                        for (int i = 1; i < 6; i++)
                        {
                            ws0.Cells[rowreg, i].Style.Font.Size = 11;
                            ws0.Cells[rowreg, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            ws0.Cells[rowreg, i].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                            ws0.Cells[rowreg, i, rowreg + numact - 1, i].Merge = true;
                            ws0.Cells[rowreg, i, rowreg + numact - 1, i].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            ws0.Cells[rowreg, i, rowreg + numact - 1, i].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                            ws0.Cells[rowreg, i, rowreg + numact - 1, i].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            ws0.Cells[rowreg, i, rowreg + numact - 1, i].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                            ws0.Cells[rowreg, i].Style.WrapText = true;
                            if (i == 1) { ws0.Cells[rowreg, i].Value = item.Objetivo; }
                            if (i == 2) { ws0.Cells[rowreg, i].Value = item.Metas; }
                            if (i == 3) { ws0.Cells[rowreg, i].Value = item.RecursoHumano; }
                            if (i == 4) { ws0.Cells[rowreg, i].Value = item.RecursoTec; }
                            if (i == 5) { ws0.Cells[rowreg, i].Value = item.RecursoFinanciero; }
                        }
                        rowreg += numact;
                    }
                    foreach (var item1 in item.ListaActividades)
                    {
                        int numplaneadosA = 0;
                        int numejecutadosA = 0;
                        int numreprogramA = 0;
                        decimal cumplimientoA = 0;

                        foreach (var item2 in plan.ListaMeses)
                        {
                            foreach (var item3 in item1.ListaProgramacion)
                            {
                                if (item3.FechaProgramacionIncial >= item2.fecha_inicio && item3.FechaProgramacionIncial <= item2.fecha_despues)
                                {
                                    numplaneadosA++;
                                    if (item3.Estado == 2)
                                    {
                                        numreprogramA++;
                                    }
                                    if (item3.Estado == 3)
                                    {
                                        numejecutadosA++;
                                    }

                                }
                            }
                        }

                        if (numplaneadosA != 0)
                        {
                            cumplimientoA = (decimal)numejecutadosA / (decimal)numplaneadosA;
                        }

                        ws0.Cells[rowregact, 6].Style.Font.Size = 11;
                        ws0.Cells[rowregact, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        ws0.Cells[rowregact, 6].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        ws0.Cells[rowregact, 6].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        ws0.Cells[rowregact, 6].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        ws0.Cells[rowregact, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        ws0.Cells[rowregact, 6].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        ws0.Cells[rowregact, 6].Style.WrapText = true;
                        ws0.Cells[rowregact, 6].Value = item1.Descripcion;

                        if (item1.Observaciones!=null)
                        {
                            ExcelComment cmd = ws0.Cells[rowregact, 6].AddComment("OBSERVACIONES: " + item1.Observaciones, "REF1");
                            cmd.AutoFit = true;
                        }
                        

                        ws0.Cells[rowregact, 7].Style.Font.Size = 11;
                        ws0.Cells[rowregact, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        ws0.Cells[rowregact, 7].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        ws0.Cells[rowregact, 7].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        ws0.Cells[rowregact, 7].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        ws0.Cells[rowregact, 7].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        ws0.Cells[rowregact, 7].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        ws0.Cells[rowregact, 7].Style.WrapText = true;
                        ws0.Cells[rowregact, 7].Value = item1.ResponsableNombre;

                        ws0.Cells[rowregact, totalcols + 1].Style.Font.Size = 11;
                        ws0.Cells[rowregact, totalcols + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        ws0.Cells[rowregact, totalcols + 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        ws0.Cells[rowregact, totalcols + 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        ws0.Cells[rowregact, totalcols + 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        ws0.Cells[rowregact, totalcols + 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        ws0.Cells[rowregact, totalcols + 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        ws0.Cells[rowregact, totalcols + 1].Style.WrapText = true;
                        ws0.Cells[rowregact, totalcols + 1].Value = cumplimientoA;
                        ws0.Cells[rowregact, totalcols + 1].Style.Numberformat.Format = "#0.00%";


                        rowregact++;
                    }
                }
                #endregion
                #region Tercerafila:obj-rec-act
                for (int i = 1; i < 8; i++)
                {
                    ws0.Cells[3, i].Style.Font.Bold = true;
                    ws0.Cells[3, i].Style.Font.Size = 11;
                    ws0.Cells[3, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ws0.Cells[3, i].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    ws0.Cells[3, i].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    ws0.Cells[3, i].Style.Fill.BackgroundColor.SetColor(graycolor);
                    ws0.Cells[3, i].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    ws0.Cells[3, i].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    ws0.Cells[3, i].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    ws0.Cells[3, i].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                }
                ws0.Row(3).Style.WrapText = true;
                ws0.Cells[3, 1].Value = "DESCRIPCIÓN";
                ws0.Cells[3, 2].Value = "METAS";
                ws0.Cells[3, 3].Value = "HUMANO";
                ws0.Cells[3, 4].Value = "TECNOLÓGICO";
                ws0.Cells[3, 5].Value = "FINANCIERO";
                ws0.Cells[3, 6].Value = "ACTIVIDAD";
                ws0.Cells[3, 7].Value = "RESPONSABLE";


                ws0.Cells[3, totalcols + 1].Style.Font.Bold = true;
                ws0.Cells[3, totalcols + 1].Style.Font.Size = 11;
                ws0.Cells[3, totalcols + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws0.Cells[3, totalcols + 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                ws0.Cells[3, totalcols + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws0.Cells[3, totalcols + 1].Style.Fill.BackgroundColor.SetColor(graycolor);
                ws0.Cells[3, totalcols + 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                ws0.Cells[3, totalcols + 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                ws0.Cells[3, totalcols + 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                ws0.Cells[3, totalcols + 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                ws0.Cells[3, totalcols + 1].Value = "PORCENTAJE DE EJECUCIÓN";
                #endregion
                #region imagenes

                if (plan != null)
                {
                    if (plan.RepLegalImagen != null && plan.RepLegalRuta != null)
                    {
                        RutaImagen1 = Server.MapPath(Path.Combine(plan.RepLegalRuta, plan.RepLegalImagen));
                        if (System.IO.File.Exists(RutaImagen1))
                        {
                            try
                            {
                                Bitmap bitmap;
                                Image bitmapExcel;
                                using (var bmpTemp = new Bitmap(RutaImagen1))
                                {
                                    bitmap = new Bitmap(bmpTemp);
                                }

                                using (var newImage = ScaleImage(bitmap, 300, 300))
                                {
                                    bitmapExcel = newImage;
                                    int rowIndex = rowfinal + 4;

                                    ExcelPicture pic = ws0.Drawings.AddPicture("Sample", bitmapExcel);
                                    pic.SetPosition(rowIndex+3, 0, 1, 0);
                                    pic.SetSize(191, 60);
                                    //pic.SetPosition(PixelTop, PixelLeft); 

                                    ws0.Cells[rowIndex+4, 2].Style.Font.Bold = true;
                                    ws0.Cells[rowIndex+4, 2].Style.Font.Size = 11;
                                    ws0.Cells[rowIndex+4, 2].Value = plan.RepLegalNombre;

                                    ws0.Cells[rowIndex + 5, 2].Style.Font.Bold = true;
                                    ws0.Cells[rowIndex + 5, 2].Style.Font.Size = 11;
                                    ws0.Cells[rowIndex + 5, 2].Value = "REPRESENTANTE LEGAL";
                                }
                            }
                            catch (Exception)
                            {
                            }

                        }
                    }
                    if (plan.RepSGSSTImagen != null && plan.RepSGSSTRuta != null)
                    {
                        RutaImagen1 = Server.MapPath(Path.Combine(plan.RepSGSSTRuta, plan.RepSGSSTImagen));
                        if (System.IO.File.Exists(RutaImagen1))
                        {
                            try
                            {
                                Bitmap bitmap;
                                Image bitmapExcel;
                                using (var bmpTemp = new Bitmap(RutaImagen1))
                                {
                                    bitmap = new Bitmap(bmpTemp);
                                }

                                using (var newImage = ScaleImage(bitmap, 300, 300))
                                {
                                    bitmapExcel = newImage;
                                    int rowIndex = rowfinal + 4;

                                    ExcelPicture pic1 = ws0.Drawings.AddPicture("Sample1", bitmapExcel);
                                    pic1.SetPosition(rowIndex+3, 0, 4, 0);
                                    pic1.SetSize(191, 60);
                                    //pic.SetPosition(PixelTop, PixelLeft); 

                                    ws0.Cells[rowIndex + 4, 5].Style.Font.Bold = true;
                                    ws0.Cells[rowIndex + 4, 5].Style.Font.Size = 11;
                                    ws0.Cells[rowIndex + 4, 5].Value = plan.RepSGSSTNombre;

                                    ws0.Cells[rowIndex + 5, 5].Style.Font.Bold = true;
                                    ws0.Cells[rowIndex + 5, 5].Style.Font.Size = 11;
                                    ws0.Cells[rowIndex + 5, 5].Value = "REPRESENTANTE SGSST";
                                }
                            }
                            catch (Exception)
                            {
                            }

                        }
                    }
                }

                #endregion

                ws0.InsertRow(1, 3);

                for (int i = 1; i < 4; i++)
                {
                    for (int i1 = 1; i1 < 6; i1++)
                    {
                        ws0.Cells[i, i1].Style.Font.Bold = true;
                        ws0.Cells[i, i1].Style.Font.Size = 11;
                    }
                }
                
                ws0.Cells[1, 1].Value = "RAZÓN SOCIAL:";
                ws0.Cells[2, 1].Value = "SEDE:";

                ws0.Cells[1, 2].Value = usuarioActual.RazonSocialEmpresa;
                ws0.Cells[2, 2].Value = plan.NombreSede;

                ws0.Cells[1, 4].Value = "PERÍODO:";
                ws0.Cells[2, 4].Value = "VIGENCIA:";
                ws0.Cells[3, 4].Value = "ORIGEN:";

                ws0.Cells[1, 5].Value = plan.FechaInicio.ToShortDateString() + " - " + plan.FechaFinal.ToShortDateString();
                ws0.Cells[2, 5].Value = plan.Vigencia.ToString();
                ws0.Cells[3, 5].Value = plan.Tipo.ToUpper();

                ws0.Column(1).Width = 20;
                ws0.Column(2).Width = 20;
                ws0.Column(3).Width = 20;
                ws0.Column(4).Width = 20;
                ws0.Column(5).Width = 20;
                ws0.Column(6).Width = 25;
                ws0.Column(7).Width = 40;
                ws0.Column(totalcols + 1).Width = 16;

                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    string Fecha = DateTime.Now.ToString().Replace(" ", "").Replace(".", "").Replace("/", "").Replace(":", "");
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "AlisstaPlandetrabajo" + Fecha + ".xlsx");
                }
            }










        }
        #region Objetivos
        [HttpPost]
        public ActionResult CrearObjetivo(EDAplicacionPlanTrabajoDetalle EDAplicacionPlanTrabajoDetalle)
        {
            bool existeerror = false;
            bool respuesta = false;
            string Estado = "No se creó el objetivo de este plan de trabajo, por favor revise la información suministrada";
            string url = EDAplicacionPlanTrabajoDetalle.Fk_Id_PlanTrabajo.ToString();


            string[] Validacion = new string[5] { "", "", "", "", "" };
            bool[] boolValidacion = new bool[5] { false, false, false, false, false };

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                Estado = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { Estado, respuesta, Validacion, boolValidacion });
            }
            //validar descripcion
            if (EDAplicacionPlanTrabajoDetalle.Objetivo == null)
            {
                boolValidacion[0] = true;
                Validacion[0] = "No ha digitado la descripción del objetivo";
                existeerror = true;
            }
            else
            {
                if (EDAplicacionPlanTrabajoDetalle.Objetivo.Replace(" ","") == string.Empty)
                {
                    boolValidacion[0] = true;
                    Validacion[0] = "No ha digitado la descripción del objetivo";
                    existeerror = true;
                }
                else
                {
                    if (EDAplicacionPlanTrabajoDetalle.Objetivo.Length>2000)
                    {
                        boolValidacion[0] = true;
                        Validacion[0] = "La descripción del objetivo no puede tener más de 2000 caracteres";
                        existeerror = true;
                    }
                }
            }
            //validar metas
            if (EDAplicacionPlanTrabajoDetalle.Metas == null)
            {
                boolValidacion[1] = true;
                Validacion[1] = "No ha digitado las metas";
                existeerror = true;
            }
            else
            {
                if (EDAplicacionPlanTrabajoDetalle.Metas.Replace(" ", "") == string.Empty)
                {
                    boolValidacion[1] = true;
                    Validacion[1] = "No ha digitado las metas";
                    existeerror = true;
                }
                else
                {
                    if (EDAplicacionPlanTrabajoDetalle.Metas.Length > 2000)
                    {
                        boolValidacion[1] = true;
                        Validacion[1] = "Las metas no pueden tener más de 2000 caracteres";
                        existeerror = true;
                    }
                }
            }


            //validar recurso humano
            if (EDAplicacionPlanTrabajoDetalle.RecursoHumano == null)
            {
                boolValidacion[2] = true;
                Validacion[2] = "No ha digitado los recursos humanos";
                existeerror = true;
            }
            else
            {
                if (EDAplicacionPlanTrabajoDetalle.RecursoHumano.Replace(" ", "") == string.Empty)
                {
                    boolValidacion[2] = true;
                    Validacion[2] = "No ha digitado los recursos humanos";
                    existeerror = true;
                }
                else
                {
                    if (EDAplicacionPlanTrabajoDetalle.RecursoHumano.Length > 2000)
                    {
                        boolValidacion[2] = true;
                        Validacion[2] = "Los recursos humanos no pueden tener más de 2000 caracteres";
                        existeerror = true;
                    }
                }
            }

            //validar recurso tecnologico
            if (EDAplicacionPlanTrabajoDetalle.RecursoTec == null)
            {
                boolValidacion[3] = true;
                Validacion[3] = "No ha digitado los recursos tecnológicos";
                existeerror = true;
            }
            else
            {
                if (EDAplicacionPlanTrabajoDetalle.RecursoTec.Replace(" ", "") == string.Empty)
                {
                    boolValidacion[3] = true;
                    Validacion[3] = "No ha digitado los recursos tecnológicos";
                    existeerror = true;
                }
                else
                {
                    if (EDAplicacionPlanTrabajoDetalle.RecursoTec.Length > 2000)
                    {
                        boolValidacion[3] = true;
                        Validacion[3] = "Los recursos tecnológicos no pueden tener más de 2000 caracteres";
                        existeerror = true;
                    }
                }
            }

            //validar recurso tecnologico
            if (EDAplicacionPlanTrabajoDetalle.RecursoFinanciero == null)
            {
                boolValidacion[4] = true;
                Validacion[4] = "No ha digitado los recursos financieros";
                existeerror = true;
            }
            else
            {
                if (EDAplicacionPlanTrabajoDetalle.RecursoFinanciero.Replace(" ", "") == string.Empty)
                {
                    boolValidacion[4] = true;
                    Validacion[4] = "No ha digitado los recursos financieros";
                    existeerror = true;
                }
                else
                {
                    if (EDAplicacionPlanTrabajoDetalle.RecursoFinanciero.Length > 2000)
                    {
                        boolValidacion[4] = true;
                        Validacion[4] = "Los recursos financieros no pueden tener más de 2000 caracteres";
                        existeerror = true;
                    }
                }
            }


            if (!existeerror)
            {
                respuesta = LNPlanTrabajo.crearobjetivo(EDAplicacionPlanTrabajoDetalle);
                if (respuesta)
                {
                    Estado = "El objetivo del plan de trabajo fue creado exitosamente";
                    return Json(new { Estado, respuesta, Validacion, boolValidacion, url });
                }
            }

            return Json(new { Estado, respuesta, Validacion, boolValidacion });
        }
        [HttpPost]
        public ActionResult ConsultarObjetivo(string IdObjetivo, string IdPlanTrabajo)
        {

            bool respuesta = false;
            string Estado = "No se creó el objetivo de este plan de trabajo, por favor revise la información suministrada";

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                Estado = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { Estado, respuesta });
            }
            int IdObjInt = 0;
            int IdPlanInt = 0;


            if (int.TryParse(IdPlanTrabajo, out IdPlanInt))
            {
                if (int.TryParse(IdObjetivo, out IdObjInt))
                {
                    EDAplicacionPlanTrabajoDetalle Model = new EDAplicacionPlanTrabajoDetalle();
                    EDAplicacionPlanTrabajo plan = LNPlanTrabajo.ConsultarPlanTrabajo(IdPlanInt, usuarioActual.IdEmpresa);
                    Model = plan.ListaDetalles.Where(s => s.Pk_Id_PlanTrabajoDetalle == IdObjInt).FirstOrDefault();
                    if (Model!=null)
                    {
                        respuesta = true;
                        Estado = "El objetivo se cargó exitosamente";
                        return Json(new { Estado, respuesta, Model });
                    }
                }
            }

            


            return Json(new { Estado, respuesta});
        }
        [HttpPost]
        public ActionResult ActualizarObjetivo(EDAplicacionPlanTrabajoDetalle EDAplicacionPlanTrabajoDetalle)
        {
            bool existeerror = false;
            bool respuesta = false;
            string Estado = "No se actualizó el objetivo de este plan de trabajo, por favor revise la información suministrada";
            string url = EDAplicacionPlanTrabajoDetalle.Fk_Id_PlanTrabajo.ToString();


            string[] Validacion = new string[5] { "", "", "", "", "" };
            bool[] boolValidacion = new bool[5] { false, false, false, false, false };

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                Estado = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { Estado, respuesta, Validacion, boolValidacion });
            }
            //validar descripcion
            if (EDAplicacionPlanTrabajoDetalle.Objetivo == null)
            {
                boolValidacion[0] = true;
                Validacion[0] = "No ha digitado la descripción del objetivo";
                existeerror = true;
            }
            else
            {
                if (EDAplicacionPlanTrabajoDetalle.Objetivo.Replace(" ", "") == string.Empty)
                {
                    boolValidacion[0] = true;
                    Validacion[0] = "No ha digitado la descripción del objetivo";
                    existeerror = true;
                }
                else
                {
                    if (EDAplicacionPlanTrabajoDetalle.Objetivo.Length > 2000)
                    {
                        boolValidacion[0] = true;
                        Validacion[0] = "La descripción del objetivo no puede tener más de 2000 caracteres";
                        existeerror = true;
                    }
                }
            }
            //validar metas
            if (EDAplicacionPlanTrabajoDetalle.Metas == null)
            {
                boolValidacion[1] = true;
                Validacion[1] = "No ha digitado las metas";
                existeerror = true;
            }
            else
            {
                if (EDAplicacionPlanTrabajoDetalle.Metas.Replace(" ", "") == string.Empty)
                {
                    boolValidacion[1] = true;
                    Validacion[1] = "No ha digitado las metas";
                    existeerror = true;
                }
                else
                {
                    if (EDAplicacionPlanTrabajoDetalle.Metas.Length > 2000)
                    {
                        boolValidacion[1] = true;
                        Validacion[1] = "Las metas no pueden tener más de 2000 caracteres";
                        existeerror = true;
                    }
                }
            }


            //validar recurso humano
            if (EDAplicacionPlanTrabajoDetalle.RecursoHumano == null)
            {
                boolValidacion[2] = true;
                Validacion[2] = "No ha digitado los recursos humanos";
                existeerror = true;
            }
            else
            {
                if (EDAplicacionPlanTrabajoDetalle.RecursoHumano.Replace(" ", "") == string.Empty)
                {
                    boolValidacion[2] = true;
                    Validacion[2] = "No ha digitado los recursos humanos";
                    existeerror = true;
                }
                else
                {
                    if (EDAplicacionPlanTrabajoDetalle.RecursoHumano.Length > 2000)
                    {
                        boolValidacion[2] = true;
                        Validacion[2] = "Los recursos humanos no pueden tener más de 2000 caracteres";
                        existeerror = true;
                    }
                }
            }

            //validar recurso tecnologico
            if (EDAplicacionPlanTrabajoDetalle.RecursoTec == null)
            {
                boolValidacion[3] = true;
                Validacion[3] = "No ha digitado los recursos tecnológicos";
                existeerror = true;
            }
            else
            {
                if (EDAplicacionPlanTrabajoDetalle.RecursoTec.Replace(" ", "") == string.Empty)
                {
                    boolValidacion[3] = true;
                    Validacion[3] = "No ha digitado los recursos tecnológicos";
                    existeerror = true;
                }
                else
                {
                    if (EDAplicacionPlanTrabajoDetalle.RecursoTec.Length > 2000)
                    {
                        boolValidacion[3] = true;
                        Validacion[3] = "Los recursos tecnológicos no pueden tener más de 2000 caracteres";
                        existeerror = true;
                    }
                }
            }

            //validar recurso tecnologico
            if (EDAplicacionPlanTrabajoDetalle.RecursoFinanciero == null)
            {
                boolValidacion[4] = true;
                Validacion[4] = "No ha digitado los recursos financieros";
                existeerror = true;
            }
            else
            {
                if (EDAplicacionPlanTrabajoDetalle.RecursoFinanciero.Replace(" ", "") == string.Empty)
                {
                    boolValidacion[4] = true;
                    Validacion[4] = "No ha digitado los recursos financieros";
                    existeerror = true;
                }
                else
                {
                    if (EDAplicacionPlanTrabajoDetalle.RecursoFinanciero.Length > 2000)
                    {
                        boolValidacion[4] = true;
                        Validacion[4] = "Los recursos financieros no pueden tener más de 2000 caracteres";
                        existeerror = true;
                    }
                }
            }


            if (!existeerror)
            {
                respuesta = LNPlanTrabajo.actualizarobjetivo(EDAplicacionPlanTrabajoDetalle);
                if (respuesta)
                {
                    Estado = "El objetivo del plan de trabajo fue actualizado exitosamente";
                    return Json(new { Estado, respuesta, Validacion, boolValidacion, url });
                }
            }

            return Json(new { Estado, respuesta, Validacion, boolValidacion });
        }
        #endregion
        #region Actividades
        [HttpPost]
        public ActionResult CrearActividad(EDAplicacionPlanTrabajoActividad EDAplicacionPlanTrabajoActividad)
        {
            bool existeerror = false;
            bool respuesta = false;
            string Estado = "No se creó la actividad de este plan de trabajo, por favor revise la información suministrada";
            string url = EDAplicacionPlanTrabajoActividad.Pk_Id_PlanTrabajoActividad.ToString();


            string[] Validacion = new string[5] { "", "", "", "", "" };
            bool[] boolValidacion = new bool[5] { false, false, false, false, false };

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                Estado = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { Estado, respuesta, Validacion, boolValidacion });
            }
            int IdPlanInt = 0;
            if (!int.TryParse(url, out IdPlanInt))
            {
                return Json(new { Estado, respuesta, Validacion, boolValidacion });
            }
            EDAplicacionPlanTrabajo plan = LNPlanTrabajo.ConsultarPlanTrabajo(IdPlanInt, usuarioActual.IdEmpresa);

            

            //validar descripcion
            if (EDAplicacionPlanTrabajoActividad.Descripcion == null)
            {
                boolValidacion[0] = true;
                Validacion[0] = "No ha digitado la descripción de la actividad";
                existeerror = true;
            }
            else
            {
                if (EDAplicacionPlanTrabajoActividad.Descripcion.Replace(" ", "") == string.Empty)
                {
                    boolValidacion[0] = true;
                    Validacion[0] = "No ha digitado la descripción de la actividad";
                    existeerror = true;
                }
                else
                {
                    if (EDAplicacionPlanTrabajoActividad.Descripcion.Length > 2000)
                    {
                        boolValidacion[0] = true;
                        Validacion[0] = "La descripción de la actividad no puede tener más de 2000 caracteres";
                        existeerror = true;
                    }
                }
            }
            //validar Documento Responsable
            if (EDAplicacionPlanTrabajoActividad.ResponsableDocumento == null)
            {
                boolValidacion[1] = true;
                Validacion[1] = "No ha digitado el número del documento del responsable";
                existeerror = true;
            }
            else
            {
                if (EDAplicacionPlanTrabajoActividad.ResponsableDocumento.Replace(" ", "") == string.Empty)
                {
                    boolValidacion[1] = true;
                    Validacion[1] = "No ha digitado el número del documento del responsable";
                    existeerror = true;
                }
                else
                {
                    if (EDAplicacionPlanTrabajoActividad.ResponsableDocumento.Length > 250)
                    {
                        boolValidacion[1] = true;
                        Validacion[1] = "el número del documento del responsable no puede tener más de 250 caracteres";
                        existeerror = true;
                    }
                }
            }


            //validar Nombre Responsable
            if (EDAplicacionPlanTrabajoActividad.ResponsableNombre == null)
            {
                boolValidacion[2] = true;
                Validacion[2] = "No ha digitado el nombre del responsable";
                existeerror = true;
            }
            else
            {
                if (EDAplicacionPlanTrabajoActividad.ResponsableNombre.Replace(" ", "") == string.Empty)
                {
                    boolValidacion[2] = true;
                    Validacion[2] = "No ha digitado el nombre del responsable";
                    existeerror = true;
                }
                else
                {
                    if (EDAplicacionPlanTrabajoActividad.ResponsableNombre.Length > 1000)
                    {
                        boolValidacion[2] = true;
                        Validacion[2] = "el nombre del responsable no puede tener más de 1000 caracteres";
                        existeerror = true;
                    }
                }
            }

            //validar observaciones
            if (EDAplicacionPlanTrabajoActividad.Observaciones == null)
            {
            }
            else
            {
                if (EDAplicacionPlanTrabajoActividad.Observaciones.Replace(" ", "") == string.Empty)
                {
                }
                else
                {
                    if (EDAplicacionPlanTrabajoActividad.Observaciones.Length > 2000)
                    {
                        boolValidacion[3] = true;
                        Validacion[3] = "Las observaciones no pueden tener más de 2000 caracteres";
                        existeerror = true;
                    }
                }
            }
            EDAplicacionPlanTrabajoActividad.FechaProgramacionIncial = DateTime.Now;
            //validar fecha programacion
            //DateTime fechainicio = plan.FechaInicio;
            //DateTime fechafin = plan.FechaFinal;
            //if (EDAplicacionPlanTrabajoActividad.FechaProgramacionIncial == DateTime.MinValue)
            //{
            //    boolValidacion[4] = true;
            //    Validacion[4] = "No ha seleccionado un fecha de programación para esta actividad";
            //    existeerror = true;
            //}
            //else
            //{
            //    bool probarfechas = false;
            //    if (EDAplicacionPlanTrabajoActividad.FechaProgramacionIncial>= fechainicio && EDAplicacionPlanTrabajoActividad.FechaProgramacionIncial <= fechafin)
            //    {
            //        probarfechas = true;

            //        string fecha_ed = EDAplicacionPlanTrabajoActividad.FechaProgramacionIncial.ToShortDateString();
            //        string hora = EDAplicacionPlanTrabajoActividad.Horas;
            //        string parsedate = fecha_ed + " " + hora;
            //        DateTime dt;
            //        if (DateTime.TryParse(parsedate, out dt))
            //        {
            //            EDAplicacionPlanTrabajoActividad.FechaProgramacionIncial = dt;
            //        }

            //    }
            //    if (!probarfechas)
            //    {
            //        boolValidacion[4] = true;
            //        Validacion[4] = "La fecha seleccionada no esta en el rango de fechas establecidas en el plan. la fehca de programación de esta actividad debe estar entre " + fechainicio.ToShortDateString() + " y " + fechafin.ToShortDateString();
            //        existeerror = true;
            //    }

            //}


            if (!existeerror)
            {
                respuesta = LNPlanTrabajo.crearactividad(EDAplicacionPlanTrabajoActividad);
                if (respuesta)
                {
                    Estado = "La actividad del plan de trabajo fue creada exitosamente";
                    return Json(new { Estado, respuesta, Validacion, boolValidacion, url });
                }
            }

            return Json(new { Estado, respuesta, Validacion, boolValidacion });
        }
        public ActionResult ConsultarActividad(string IdActividad, string IdPlanTrabajo)
        {
            DateTime FechaHoy = DateTime.Now;
            bool respuesta = false;
            string Estado = "";
            string Fecha = "";
            string Fecha1 = "";
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                Estado = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { Estado, respuesta });
            }
            int IdactInt = 0;
            int IdPlanInt = 0;
            if (int.TryParse(IdPlanTrabajo, out IdPlanInt))
            {
                if (int.TryParse(IdActividad, out IdactInt))
                {
                    EDAplicacionPlanTrabajoActividad Model = new EDAplicacionPlanTrabajoActividad();
                    EDAplicacionPlanTrabajo plan = LNPlanTrabajo.ConsultarPlanTrabajo(IdPlanInt, usuarioActual.IdEmpresa);
                    foreach (var item in plan.ListaDetalles)
                    {
                        if (item.ListaActividades!=null)
                        {
                            foreach (var item1 in item.ListaActividades)
                            {
                                if (item1.Estado != 3)
                                {
                                    if (FechaHoy > item1.FechaEstado)
                                    {
                                        item1.Estado = 4;
                                    }
                                }
                            }
                        }
                    }
                    foreach (var item in plan.ListaDetalles)
                    {

                        Model = item.ListaActividades.Where(s => s.Pk_Id_PlanTrabajoActividad == IdactInt).FirstOrDefault();
                        if (Model!=null)
                        {
                            break;
                        }
                        
                    }
                    if (Model != null)
                    {
                        if (Model.Estado==1)
                        {
                            Estado = "Fecha Inicial";
                        }
                        if (Model.Estado == 2)
                        {
                            Estado = "Actividad Reprogramada";
                        }
                        if (Model.Estado == 3)
                        {
                            Estado = "Ejecutada";
                        }
                        if (Model.Estado == 4)
                        {
                            Estado = "No ejecutada a tiempo (Reprograme la actividad)";
                        }
                        Fecha = Model.FechaEstado.ToString();
                        Fecha1 = Model.FechaProgramacionIncial.ToString();
                        respuesta = true;
                        return Json(new { Estado, respuesta, Model, Fecha, Fecha1 });
                    }
                }
            }
            return Json(new { Estado, respuesta });
        }
        [HttpPost]
        public ActionResult ActualizarActividad(EDAplicacionPlanTrabajoActividad EDAplicacionPlanTrabajoActividad, EDAplicacionPlanTrabajoActividad Control)
        {
            bool existeerror = false;
            bool respuesta = false;
            string Estado = "No se actualizó la actividad de este plan de trabajo, por favor revise la información suministrada";
            string url = Control.Pk_Id_PlanTrabajoActividad.ToString();


            string[] Validacion = new string[5] { "", "", "", "", "" };
            bool[] boolValidacion = new bool[5] { false, false, false, false, false };

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                Estado = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { Estado, respuesta, Validacion, boolValidacion });
            }
            int IdPlanInt = 0;
            if (!int.TryParse(url, out IdPlanInt))
            {
                return Json(new { Estado, respuesta, Validacion, boolValidacion });
            }
            EDAplicacionPlanTrabajo plan = LNPlanTrabajo.ConsultarPlanTrabajo(IdPlanInt, usuarioActual.IdEmpresa);
            //validar descripcion
            if (EDAplicacionPlanTrabajoActividad.Descripcion == null)
            {
                boolValidacion[0] = true;
                Validacion[0] = "No ha digitado la descripción de la actividad";
                existeerror = true;
            }
            else
            {
                if (EDAplicacionPlanTrabajoActividad.Descripcion.Replace(" ", "") == string.Empty)
                {
                    boolValidacion[0] = true;
                    Validacion[0] = "No ha digitado la descripción de la actividad";
                    existeerror = true;
                }
                else
                {
                    if (EDAplicacionPlanTrabajoActividad.Descripcion.Length > 2000)
                    {
                        boolValidacion[0] = true;
                        Validacion[0] = "La descripción de la actividad no puede tener más de 2000 caracteres";
                        existeerror = true;
                    }
                }
            }
            //validar Documento Responsable
            if (EDAplicacionPlanTrabajoActividad.ResponsableDocumento == null)
            {
                boolValidacion[1] = true;
                Validacion[1] = "No ha digitado el número del documento del responsable";
                existeerror = true;
            }
            else
            {
                if (EDAplicacionPlanTrabajoActividad.ResponsableDocumento.Replace(" ", "") == string.Empty)
                {
                    boolValidacion[1] = true;
                    Validacion[1] = "No ha digitado el número del documento del responsable";
                    existeerror = true;
                }
                else
                {
                    if (EDAplicacionPlanTrabajoActividad.ResponsableDocumento.Length > 250)
                    {
                        boolValidacion[1] = true;
                        Validacion[1] = "el número del documento del responsable no puede tener más de 250 caracteres";
                        existeerror = true;
                    }
                }
            }


            //validar Nombre Responsable
            if (EDAplicacionPlanTrabajoActividad.ResponsableNombre == null)
            {
                boolValidacion[2] = true;
                Validacion[2] = "No ha digitado el nombre del responsable";
                existeerror = true;
            }
            else
            {
                if (EDAplicacionPlanTrabajoActividad.ResponsableNombre.Replace(" ", "") == string.Empty)
                {
                    boolValidacion[2] = true;
                    Validacion[2] = "No ha digitado el nombre del responsable";
                    existeerror = true;
                }
                else
                {
                    if (EDAplicacionPlanTrabajoActividad.ResponsableNombre.Length > 1000)
                    {
                        boolValidacion[2] = true;
                        Validacion[2] = "el nombre del responsable no puede tener más de 1000 caracteres";
                        existeerror = true;
                    }
                }
            }

            //validar observaciones
            if (EDAplicacionPlanTrabajoActividad.Observaciones == null)
            {
            }
            else
            {
                if (EDAplicacionPlanTrabajoActividad.Observaciones.Replace(" ", "") == string.Empty)
                {
                }
                else
                {
                    if (EDAplicacionPlanTrabajoActividad.Observaciones.Length > 2000)
                    {
                        boolValidacion[3] = true;
                        Validacion[3] = "Las observaciones no pueden tener más de 2000 caracteres";
                        existeerror = true;
                    }
                }
            }

            //validar fecha programacion
            DateTime fechainicio = plan.FechaInicio;
            DateTime fechafin = plan.FechaFinal;
            if (EDAplicacionPlanTrabajoActividad.FechaProgramacionIncial != DateTime.MinValue)
            {
                bool probarfechas = false;
                if (EDAplicacionPlanTrabajoActividad.FechaProgramacionIncial >= fechainicio && EDAplicacionPlanTrabajoActividad.FechaProgramacionIncial <= fechafin)
                {
                    probarfechas = true;
                    
                }
                if (!probarfechas)
                {
                    boolValidacion[4] = true;
                    Validacion[4] = "La fecha de reprogramación no esta en el rango de fechas establecidas en el plan. la fecha de programación de esta actividad debe estar entre " + fechainicio.ToShortDateString() + " y " + fechafin.ToShortDateString();
                    existeerror = true;
                }
                else
                {
                    EDAplicacionPlanTrabajoActividad.FechaEstado = EDAplicacionPlanTrabajoActividad.FechaProgramacionIncial;
                    string fecha_ed = EDAplicacionPlanTrabajoActividad.FechaEstado.ToShortDateString();
                    string hora = EDAplicacionPlanTrabajoActividad.Horas;
                    string parsedate = fecha_ed + " " + hora;
                    DateTime dt;
                    if (DateTime.TryParse(parsedate, out dt))
                    {
                        EDAplicacionPlanTrabajoActividad.FechaEstado = dt;
                    }
                }
            }



            if (!existeerror)
            {
                respuesta = LNPlanTrabajo.actualizaractividad(EDAplicacionPlanTrabajoActividad);
                if (respuesta)
                {
                    Estado = "La actividad del plan de trabajo fue creada exitosamente";
                    return Json(new { Estado, respuesta, Validacion, boolValidacion, url });
                }
            }

            return Json(new { Estado, respuesta, Validacion, boolValidacion });
        }
        #endregion
        #region Programacion
        [HttpPost]
        public ActionResult CrearPrograma(EDAplicacionPlanTrabajoProgramacion Programa)
        {
            bool existeerror = false;
            bool respuesta = false;
            string Estado = "No se creó el programa de esta actividad, por favor revise la información suministrada";
            string url = Programa.Pk_Id_AplicacionPlanTrabajoProgramacion.ToString();


            string[] Validacion = new string[1] { "" };
            bool[] boolValidacion = new bool[1] { false };

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                Estado = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { Estado, respuesta, Validacion, boolValidacion });
            }
            int IdPlanInt = 0;
            if (!int.TryParse(url, out IdPlanInt))
            {
                return Json(new { Estado, respuesta, Validacion, boolValidacion });
            }
            EDAplicacionPlanTrabajo plan = LNPlanTrabajo.ConsultarPlanTrabajo(IdPlanInt, usuarioActual.IdEmpresa);


            //validar fecha programacion
            DateTime fechainicio = plan.FechaInicio;
            DateTime fechafin = plan.FechaFinal;
            if (Programa.FechaProgramacionIncial == DateTime.MinValue)
            {
                boolValidacion[0] = true;
                Validacion[0] = "No ha seleccionado un fecha de programación para esta actividad";
                existeerror = true;
            }
            else
            {
                bool probarfechas = false;
                if (Programa.FechaProgramacionIncial >= fechainicio && Programa.FechaProgramacionIncial <= fechafin)
                {
                    probarfechas = true;

                    string fecha_ed = Programa.FechaProgramacionIncial.ToShortDateString();
                    string hora = Programa.Horas;
                    string parsedate = fecha_ed + " " + hora;
                    DateTime dt;
                    if (DateTime.TryParse(parsedate, out dt))
                    {
                        Programa.FechaProgramacionIncial = dt;
                    }

                }
                if (!probarfechas)
                {
                    boolValidacion[0] = true;
                    Validacion[0] = "La fecha seleccionada no esta en el rango de fechas establecidas en el plan. la fecha de programación de esta actividad debe estar entre " + fechainicio.ToShortDateString() + " y " + fechafin.ToShortDateString();
                    existeerror = true;
                }

            }


            if (!existeerror)
            {
                respuesta = LNPlanTrabajo.crearprograma(Programa);
                if (respuesta)
                {
                    Estado = "el programa de la actividad fue creado exitosamente";
                    return Json(new { Estado, respuesta, Validacion, boolValidacion, url });
                }
            }

            return Json(new { Estado, respuesta, Validacion, boolValidacion });
        }
        public ActionResult ConsultarPrograma(string IdPrograma, string IdPlanTrabajo)
        {
            DateTime FechaHoy = DateTime.Now;
            bool respuesta = false;
            string Estado = "";
            string Fecha = "";
            string Fecha1 = "";
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                Estado = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { Estado, respuesta });
            }
            int IdprogInt = 0;
            int IdPlanInt = 0;
            if (int.TryParse(IdPlanTrabajo, out IdPlanInt))
            {
                if (int.TryParse(IdPrograma, out IdprogInt))
                {
                    EDAplicacionPlanTrabajoProgramacion Model = new EDAplicacionPlanTrabajoProgramacion();
                    EDAplicacionPlanTrabajo plan = LNPlanTrabajo.ConsultarPlanTrabajo(IdPlanInt, usuarioActual.IdEmpresa);
                    foreach (var item in plan.ListaDetalles)
                    {
                        if (item.ListaActividades != null)
                        {
                            foreach (var item1 in item.ListaActividades)
                            {
                                if (item1.Estado != 3)
                                {
                                    if (FechaHoy > item1.FechaEstado)
                                    {
                                        item1.Estado = 4;
                                    }
                                }
                            }
                        }
                    }
                    foreach (var item in plan.ListaDetalles)
                    {
                        bool breaklist = false;
                        foreach (var item1 in item.ListaActividades)
                        {
                            
                            Model = item1.ListaProgramacion.Where(s => s.Pk_Id_AplicacionPlanTrabajoProgramacion == IdprogInt).FirstOrDefault();
                            if (Model != null)
                            {
                                breaklist = true;
                                break;
                            }
                        }
                        if (breaklist)
                        {
                            break;
                        }
                    }
                    if (Model != null)
                    {
                        if (Model.Estado == 1)
                        {
                            Estado = "Fecha Inicial";
                        }
                        if (Model.Estado == 2)
                        {
                            Estado = "Actividad Reprogramada";
                        }
                        if (Model.Estado == 3)
                        {
                            Estado = "Ejecutada";
                        }
                        if (Model.Estado == 4)
                        {
                            Estado = "No ejecutada a tiempo (Reprograme la actividad)";
                        }
                        Fecha = Model.FechaEstado.ToString();
                        Fecha1 = Model.FechaProgramacionIncial.ToString();
                        respuesta = true;
                        return Json(new { Estado, respuesta, Model, Fecha, Fecha1 });
                    }
                }
            }
            return Json(new { Estado, respuesta });
        }
        [HttpPost]
        public ActionResult ActualizarPrograma(EDAplicacionPlanTrabajoProgramacion EDAplicacionPlanTrabajoProgramacion, EDAplicacionPlanTrabajoProgramacion Control)
        {
            bool existeerror = false;
            bool respuesta = false;
            string Estado = "No se actualizó el programa de esta actividad de trabajo, por favor revise la información suministrada";
            string url = Control.Pk_Id_AplicacionPlanTrabajoProgramacion.ToString();


            string[] Validacion = new string[1] { "" };
            bool[] boolValidacion = new bool[1] { false };

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                Estado = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { Estado, respuesta, Validacion, boolValidacion });
            }
            int IdPlanInt = 0;
            if (!int.TryParse(url, out IdPlanInt))
            {
                return Json(new { Estado, respuesta, Validacion, boolValidacion });
            }
            EDAplicacionPlanTrabajo plan = LNPlanTrabajo.ConsultarPlanTrabajo(IdPlanInt, usuarioActual.IdEmpresa);






            //validar fecha programacion
            DateTime fechainicio = plan.FechaInicio;
            DateTime fechafin = plan.FechaFinal;
            if (EDAplicacionPlanTrabajoProgramacion.FechaProgramacionIncial != DateTime.MinValue)
            {
                bool probarfechas = false;
                if (EDAplicacionPlanTrabajoProgramacion.FechaProgramacionIncial >= fechainicio && EDAplicacionPlanTrabajoProgramacion.FechaProgramacionIncial <= fechafin)
                {
                    probarfechas = true;

                }
                if (!probarfechas)
                {
                    boolValidacion[0] = true;
                    Validacion[0] = "La fecha de reprogramación no esta en el rango de fechas establecidas en el plan. la fecha de programación de esta actividad debe estar entre " + fechainicio.ToShortDateString() + " y " + fechafin.ToShortDateString();
                    existeerror = true;
                }
                else
                {
                    EDAplicacionPlanTrabajoProgramacion.FechaEstado = EDAplicacionPlanTrabajoProgramacion.FechaProgramacionIncial;
                    string fecha_ed = EDAplicacionPlanTrabajoProgramacion.FechaEstado.ToShortDateString();
                    string hora = EDAplicacionPlanTrabajoProgramacion.Horas;
                    string parsedate = fecha_ed + " " + hora;
                    DateTime dt;
                    if (DateTime.TryParse(parsedate, out dt))
                    {
                        EDAplicacionPlanTrabajoProgramacion.FechaEstado = dt;
                    }
                }
            }



            if (!existeerror)
            {
                respuesta = LNPlanTrabajo.actualizarprograma(EDAplicacionPlanTrabajoProgramacion);
                if (respuesta)
                {
                    Estado = "La actividad del plan de trabajo fue creada exitosamente";
                    return Json(new { Estado, respuesta, Validacion, boolValidacion, url });
                }
            }

            return Json(new { Estado, respuesta, Validacion, boolValidacion });
        }
        #endregion
        #region MetodosExtra
        static double ConvertBytesToMegabytes(long bytes)
        {
            return (bytes / 1024f) / 1024f;
        }
        [HttpPost]
        public ActionResult UploadImg()
        {
            bool probar = false;
            string resultado = "";
            string NombreArchivos = string.Empty;
            string NombreArchivos_short = string.Empty;
            string NombreArchivos_borrar = string.Empty;
            bool display = false;

            string Thumbnails = SrcWhite;
            if (Request.Files.Count > 0)
            {

                string ValImagen1 = string.Empty;
                string ValImagen1s = string.Empty;
                try
                {
                    ValImagen1 = Request.Form[0].ToString();
                    ValImagen1s = Request.Form[1].ToString();
                }
                catch (Exception)
                {
                    resultado = "";
                    return Json(new { probar, resultado });
                }

                NombreArchivos = ValImagen1;
                NombreArchivos_short = ValImagen1s;
                NombreArchivos_borrar = ValImagen1;

                try
                {
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        if (file.ContentType.Contains("image"))
                        {
                            string fname;
                            if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                            {
                                string[] testfiles = file.FileName.Split(new char[] { '\\' });
                                fname = testfiles[testfiles.Length - 1];
                            }
                            else
                            {
                                fname = file.FileName;
                            }
                            if (file != null)
                            {
                                CrearCarpeta(RutaImagenesTemp);
                                string ImgFileName = "EPPIMG_" + DateTime.Now.ToString().Replace(" ", "").Replace(".", "").Replace(":", "").Replace("/", "") + file.FileName;
                                string pathsave = Server.MapPath(Path.Combine(RutaImagenesTemp, ImgFileName));
                                file.SaveAs(pathsave);
                                try
                                {
                                    string PathOrigen = Server.MapPath(Path.Combine(RutaImagenesTemp, NombreArchivos_borrar));
                                    if (System.IO.File.Exists(PathOrigen))
                                    {
                                        System.IO.File.Delete(PathOrigen);
                                    }
                                }
                                catch (Exception)
                                {

                                }
                                NombreArchivos = ImgFileName;
                                NombreArchivos_short = file.FileName;
                                try
                                {
                                    string PathImage = Server.MapPath(Path.Combine(RutaImagenesTemp, NombreArchivos));
                                    Bitmap bitmap;
                                    using (var bmpTemp = new Bitmap(PathImage))
                                    {
                                        bitmap = new Bitmap(bmpTemp);
                                    }
                                    using (var newImage = ScaleImage(bitmap, 300, 300))
                                    {
                                        Thumbnails = "data:image/png;base64," + ImageToBase64String(newImage, ImageFormat.Png);
                                        display = true;
                                    }
                                }
                                catch (Exception)
                                {
                                }
                            }
                            else
                            {
                                probar = false;
                                resultado = "El archivo que se intento subir no es una imagen o no es un archivo soportado, por favor intente de nuevo con otra imagen";
                                return Json(new { probar, resultado });
                            }
                        }
                        else
                        {
                            probar = false;
                            resultado = "El archivo que se intento subir no es una imagen o no es un archivo soportado, por favor intente de nuevo con otra imagen";
                            return Json(new { probar, resultado });
                        }
                    }
                    probar = true;
                    return Json(new { probar, resultado, Thumbnails, NombreArchivos, NombreArchivos_short, display });
                }
                catch (Exception ex)
                {
                    resultado = "No se pudo completar la operación, el tamaño del archivo probablemente es mayor a 4 MB ";
                    return Json(new { probar, resultado });
                }
            }
            else
            {
                resultado = "No ha seleccionado un archivo, antes de adjuntar asegurese que exista un archivo";
                return Json(new { probar, resultado });
            }
        }
        [HttpPost]
        public ActionResult EliminarImg(string ruta)
        {
            bool probar = true;
            if (ruta != null)
            {
                try
                {
                    string PathOrigen = Server.MapPath(Path.Combine(RutaImagenesTemp, ruta));
                    if (System.IO.File.Exists(PathOrigen))
                    {
                        System.IO.File.Delete(PathOrigen);
                    }
                }
                catch (Exception)
                {
                }
            }
            return Json(new { probar });
        }
        private void CrearCarpeta(string path)
        {
            bool existeCarpeta = Directory.Exists(Server.MapPath(path));
            if (!existeCarpeta)
            {
                Directory.CreateDirectory(Server.MapPath(path));
            }

        }
        [HttpPost]
        public JsonResult BuscarPersonaDocumentoCargo1(string documento)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return Json(JsonRequestBehavior.AllowGet);
            }
            string nit = usuarioActual.NitEmpresa;
            List<EDTipoDocumento> EDTipoDoc = LNAcciones.BuscarTipoDocumento();
            List<TipoDocumento> ListaDocumentos = new List<TipoDocumento>();
            List<string> ListaDocumentosStr = new List<string>();
            foreach (var item in EDTipoDoc)
            {
                TipoDocumento td = new TipoDocumento();
                td.PK_IDTipo_Documento = item.Id_Tipo_Documento;
                td.Sigla = item.Sigla;
                td.Descripcion = item.Descripcion;
                ListaDocumentos.Add(td);
                ListaDocumentosStr.Add(td.Descripcion);
                ListaDocumentosStr.Add(td.Sigla);
            }

            //ListaDocumentos = ListaDocumentos.Where(s => s.AplicaPersonas == true).ToList();
            string Nit = usuarioActual.NitEmpresa;
            string[] resultado = new string[2] { string.Empty, string.Empty };
            List<EDRelacionesLaborales> RelacionLaboral = new List<EDRelacionesLaborales>();

            bool probar = false;

            try
            {
                foreach (var item in ListaDocumentosStr)
                {
                    if (!string.IsNullOrEmpty(documento) && !string.IsNullOrEmpty(item))
                    {
                        var cliente = new RestSharp.RestClient(ConfigurationManager.AppSettings["Url"]);
                        var request = new RestRequest(ConfigurationManager.AppSettings["consultaAfiliado"], RestSharp.Method.GET);
                        request.RequestFormat = DataFormat.Xml;
                        request.Parameters.Clear();
                        request.AddParameter("tpDoc", item.ToString());
                        request.AddParameter("doc", documento);
                        request.AddHeader("Content-Type", "application/json");
                        request.AddHeader("Accept", "application/json");

                        //se omite la validación de certificado de SSL
                        ServicePointManager.ServerCertificateValidationCallback = delegate
                        { return true; };
                        IRestResponse<List<AfiliadoModel>> response = cliente.Execute<List<AfiliadoModel>>(request);
                        var result = response.Content;
                        if (!string.IsNullOrWhiteSpace(result))
                        {
                            var respuesta = Newtonsoft.Json.JsonConvert.DeserializeObject<List<AfiliadoModel>>(result);
                            var afiliado = respuesta.Where(a => a.Estado == "Activo").ToList();
                            if (afiliado == null)
                            { }
                            else
                            {
                                foreach (var item1 in afiliado)
                                {
                                    if (nit == item1.IdEmpresa)
                                    {
                                        EDRelacionesLaborales EDRelacionesLaborales = new EDRelacionesLaborales();
                                        EDRelacionesLaborales.Nombre1 = item1.Nombre1 + " " + item1.Nombre2 + " " + item1.Apellido1 + " " + item1.Apellido2;
                                        EDRelacionesLaborales.Ocupacion = item1.Ocupacion;
                                        EDRelacionesLaborales.TipoDocumento = "Cédula de Ciudadanía";
                                        EDRelacionesLaborales.Email = item1.EmailPersona;
                                        RelacionLaboral.Add(EDRelacionesLaborales);

                                        probar = true;
                                        return Json(new { resultado, probar, RelacionLaboral }, JsonRequestBehavior.AllowGet);
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }

                            }
                        }
                    }
                }



                if (resultado[0] == string.Empty)
                {
                    if (!string.IsNullOrEmpty(documento))
                    {
                        var cliente = new RestSharp.RestClient(ConfigurationManager.AppSettings["Url"]);
                        var request = new RestRequest(ConfigurationManager.AppSettings["consultaAfiliado"], RestSharp.Method.GET);
                        request.RequestFormat = DataFormat.Xml;
                        request.Parameters.Clear();
                        request.AddParameter("tpDoc", "CC");
                        request.AddParameter("doc", documento);
                        request.AddHeader("Content-Type", "application/json");
                        request.AddHeader("Accept", "application/json");

                        //se omite la validación de certificado de SSL
                        ServicePointManager.ServerCertificateValidationCallback = delegate
                        { return true; };
                        IRestResponse<List<AfiliadoModel>> response = cliente.Execute<List<AfiliadoModel>>(request);
                        var result = response.Content;
                        if (!string.IsNullOrWhiteSpace(result))
                        {
                            var respuesta = Newtonsoft.Json.JsonConvert.DeserializeObject<List<AfiliadoModel>>(result);
                            if (respuesta.Count != 0)
                            {
                                var afiliado = respuesta.Where(a => a.Estado == "Activo").ToList();
                                if (afiliado == null)
                                {

                                }
                                else
                                {
                                    foreach (var item1 in afiliado)
                                    {
                                        if (nit == item1.IdEmpresa)
                                        {
                                            EDRelacionesLaborales EDRelacionesLaborales = new EDRelacionesLaborales();
                                            EDRelacionesLaborales.Nombre1 = item1.Nombre1 + " " + item1.Nombre2 + " " + item1.Apellido1 + " " + item1.Apellido2;
                                            EDRelacionesLaborales.Ocupacion = item1.Ocupacion;
                                            EDRelacionesLaborales.TipoDocumento = "Cédula de Ciudadanía";
                                            EDRelacionesLaborales.Email = item1.EmailPersona;
                                            RelacionLaboral.Add(EDRelacionesLaborales);

                                            probar = true;
                                            return Json(new { resultado, probar, RelacionLaboral }, JsonRequestBehavior.AllowGet);
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                }
                            }

                        }
                    }
                    return Json(new { resultado, probar, RelacionLaboral }, JsonRequestBehavior.AllowGet);
                }
                else
                    return Json(new { resultado, probar, RelacionLaboral }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { resultado, probar, RelacionLaboral }, JsonRequestBehavior.AllowGet);
            }
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
        public string UrlToBase64(string Path)
        {
            using (Image image = Image.FromFile(Path))
            {
                using (MemoryStream m = new MemoryStream())
                {
                    image.Save(m, image.RawFormat);
                    byte[] imageBytes = m.ToArray();

                    // Convert byte[] to Base64 String
                    string base64String = Convert.ToBase64String(imageBytes);
                    return base64String;
                }
            }
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
        public Image Base64ToImage(string base64String)
        {
            base64String = base64String.Replace("data:image/png;base64,", "");
            // Convert Base64 String to byte[]
            byte[] imageBytes = Convert.FromBase64String(base64String);
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);

            // Convert byte[] to Image
            ms.Write(imageBytes, 0, imageBytes.Length);
            Image image = Image.FromStream(ms, true);

            return image;
        }
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        #endregion
    }
}

