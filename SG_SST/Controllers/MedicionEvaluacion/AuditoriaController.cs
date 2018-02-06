using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SG_SST.EntidadesDominio.MedicionEvaluacion;
using SG_SST.Models.MedicionEvaluacion;
using SG_SST.Models.Empleado;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;
using SG_SST.Controllers.Base;
using SG_SST.Logica.MedicionEvaluacion;
using SG_SST.Logica.Empresas;
using SG_SST.EntidadesDominio.Empresas;
using System.Configuration;
using SG_SST.Models.Ausentismo;
using System.Net;
using RestSharp;
using System.Web.Script.Serialization;

namespace SG_SST.Controllers.MedicionEvaluacion
{
    public class AuditoriaController : BaseController
    {
        LNAcciones LNAcciones = new LNAcciones();
        LNProcesos LNProcesos = new LNProcesos();
        LNEmpresa LNEmpresa = new LNEmpresa();
        LNAuditoria LNAuditoria = new LNAuditoria();
        private static string RutaFirmasAud = "~/Content/ArchivosAuditoria/FirmasAuditoria/";
        public ActionResult Index()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                return View();
            }
            return View();
        }
        #region Programa
        [HttpGet]
        public ActionResult Programa(string Año, string Nombre, string Sede)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }
            var ListaSedes = new List<EDSede>();
            ListaSedes = LNEmpresa.ObtenerSedesPorNit(usuarioActual.NitEmpresa);
            int index_sede = 0;
            ViewBag.Pk_Id_Sede = new SelectList(ListaSedes, "IdSede", "NombreSede", index_sede);
            List<EDAuditoriaPrograma> Lista = new List<EDAuditoriaPrograma>();
            Lista = LNAuditoria.ConsultaProgramaFiltros(Año, Nombre, Sede, usuarioActual.IdEmpresa);
            Lista = Lista.OrderBy(s => s.Fecha_Programacion).ToList();
            return View(Lista);
        }
        [HttpPost]
        public ActionResult ConsultarPrograma(List<String> values)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
                return RedirectToAction("Login", "Home");
            }
            string Año = values[0].ToString();
            string Nombre = values[1].ToString();
            string Sede = values[2].ToString();
            return Json(new { url = Url.Action("Programa", "Auditoria", new { Año = Año, Nombre = Nombre, Sede = Sede }) });
        }
        [HttpGet]
        public ActionResult CrearPrograma()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }
            var ListaSedes = new List<EDSede>();
            ListaSedes = LNEmpresa.ObtenerSedesPorNit(usuarioActual.NitEmpresa);
            int index_sede = 0;
            ViewBag.Pk_Id_Sede = new SelectList(ListaSedes, "IdSede", "NombreSede", index_sede);
            List<SelectListItem> ListaPeriodicidad = new List<SelectListItem>();
            ListaPeriodicidad.Add(new SelectListItem { Text = "Anual", Value = "Anual" });
            ListaPeriodicidad.Add(new SelectListItem { Text = "Semestral", Value = "Semestral" });
            ListaPeriodicidad.Add(new SelectListItem { Text = "Trimestral", Value = "Trimestral", Selected = false });
            ListaPeriodicidad.Add(new SelectListItem { Text = "Mensual", Value = "Mensual" });
            ViewBag.ListaPeriodo = ListaPeriodicidad;

            List<SelectListItem> ListaAños = new List<SelectListItem>();
            for (int i = 2010; i < 2051; i++)
            {
                ListaAños.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
            }
            ViewBag.SelectAnio = ListaAños;

            return View();
        }
        [HttpPost]
        public ActionResult CrearPrograma(AuditoriaPrograma ProgramaAuditoria)
        {
            bool Probar = false;
            string Estado = "No se guardó el programa de auditorias, por favor revise la información suministrada";
            bool[] Validacion = new bool[10];
            string[] ValidacionStr = new string[10];
            for (int i = 0; i < 10; i++)
            {
                Validacion[i] = false;
                ValidacionStr[i] = "";
            }
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                Estado = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { Estado, Probar, Validacion, ValidacionStr });
            }

            ModelState.Remove("FirmaScrImageRes");
            ModelState.Remove("FirmaScrImagePres");
            if (ModelState.IsValid)
            {
                AuditoriaPrograma NuevoPrograma = new AuditoriaPrograma();
                NuevoPrograma = ProgramaAuditoria;
                NuevoPrograma.Fk_Id_Empresa = usuarioActual.IdEmpresa;
                if (ProgramaAuditoria.Fk_Id_Sede > 0)
                {
                    //Consultar Sede
                    List<EDSede> ListEDSede = LNEmpresa.ObtenerSedesPorNit(usuarioActual.NitEmpresa);
                    EDSede EDSede = ListEDSede.Find(s => s.IdSede == ProgramaAuditoria.Fk_Id_Sede);
                    NuevoPrograma.Fk_Id_Sede = EDSede.IdSede;
                    NuevoPrograma.SedeAuditoria = EDSede.NombreSede;
                }
                else
                {
                    Estado = "Verifique que haya suministrado la SEDE";
                    return Json(new { Estado, Probar });
                }
                if (ProgramaAuditoria.Año.ToString().Length>4)
                {
                    Estado = "Verifique que el año tenga 4 dígitos";
                    Validacion[1] = true;
                    ValidacionStr[1] = "el año debe tener 4 dígitos";
                    return Json(new { Estado, Probar, Validacion, ValidacionStr }, JsonRequestBehavior.AllowGet);
                }
                DateTime datePrograma = ProgramaAuditoria.Fecha_Programacion;
                NuevoPrograma.Fecha_Programacion = datePrograma;
                if (NuevoPrograma.FirmaScrImagePres != "")
                {
                    try
                    {
                        string Nombre = "Fpres" + RandomString(9) + ".png";
                        string b64 = NuevoPrograma.FirmaScrImagePres;
                        b64 = b64.Replace("data:image/png;base64,", "");
                        Image Imagen = Base64ToImage(b64);
                        string outputPath = (Server.MapPath(Path.Combine(RutaFirmasAud, Nombre)));
                        CrearCarpeta(RutaFirmasAud);
                        Imagen.Save(outputPath, ImageFormat.Png);
                        NuevoPrograma.NombreArchivoCopasst = Nombre;
                        NuevoPrograma.RutaArchivoPres = RutaFirmasAud;
                    }
                    catch (Exception)
                    {
                    }
                }
                if (NuevoPrograma.FirmaScrImageRes != "")
                {
                    try
                    {
                        string Nombre = "Fres" + RandomString(9) + ".png";
                        string b64 = NuevoPrograma.FirmaScrImageRes;
                        b64 = b64.Replace("data:image/png;base64,", "");
                        Image Imagen = Base64ToImage(b64);
                        string outputPath = (Server.MapPath(Path.Combine(RutaFirmasAud, Nombre)));
                        CrearCarpeta(RutaFirmasAud);
                        Imagen.Save(outputPath, ImageFormat.Png);
                        NuevoPrograma.NombreArchivoRes = Nombre;
                        NuevoPrograma.RutaArchivoRes = RutaFirmasAud;
                    }
                    catch (Exception)
                    {
                    }
                }
                EDAuditoriaPrograma EDAuditoriaPrograma = new EDAuditoriaPrograma();
                EDAuditoriaPrograma.Pk_Id_Programa = NuevoPrograma.Pk_Id_Programa;
                EDAuditoriaPrograma.Titulo = NuevoPrograma.Titulo;
                EDAuditoriaPrograma.Objetivo = NuevoPrograma.Objetivo;
                EDAuditoriaPrograma.Alcance = NuevoPrograma.Alcance;
                EDAuditoriaPrograma.Metodologia = NuevoPrograma.Metodologia;
                EDAuditoriaPrograma.Competencia = NuevoPrograma.Competencia;
                EDAuditoriaPrograma.Recursos = NuevoPrograma.Recursos;
                EDAuditoriaPrograma.Fecha_Programacion = NuevoPrograma.Fecha_Programacion;
                EDAuditoriaPrograma.Año = NuevoPrograma.Año;
                EDAuditoriaPrograma.Periodicidad = NuevoPrograma.Periodicidad;
                EDAuditoriaPrograma.NombreArchivoRes = NuevoPrograma.NombreArchivoRes;
                EDAuditoriaPrograma.RutaArchivoRes = NuevoPrograma.RutaArchivoRes;
                EDAuditoriaPrograma.Nombre_Responsable = NuevoPrograma.Nombre_Responsable;
                EDAuditoriaPrograma.Numero_Id_Responsable = NuevoPrograma.Numero_Id_Responsable;
                EDAuditoriaPrograma.FirmaScrImageRes = null;
                EDAuditoriaPrograma.NombreArchivoCopasst = NuevoPrograma.NombreArchivoCopasst;
                EDAuditoriaPrograma.RutaArchivoPres = NuevoPrograma.RutaArchivoPres;
                EDAuditoriaPrograma.Nombre_Copasst = NuevoPrograma.Nombre_Copasst;
                EDAuditoriaPrograma.Numero_Id_Copasst = NuevoPrograma.Numero_Id_Copasst;
                EDAuditoriaPrograma.FirmaScrImagePres = null;
                EDAuditoriaPrograma.Fk_Id_Empresa = NuevoPrograma.Fk_Id_Empresa;
                EDAuditoriaPrograma.SedeAuditoria = NuevoPrograma.SedeAuditoria;
                EDAuditoriaPrograma.Fk_Id_Sede = NuevoPrograma.Fk_Id_Sede;
                bool ProbarGuardado = LNAuditoria.GuardarPrograma(EDAuditoriaPrograma);
                if (ProbarGuardado)
                {
                    Probar = true;
                    return Json(new { Estado, Probar });
                }
            }
            
            for (int i = 0; i < 10; i++)
            {
                Validacion[i] = false;
                ValidacionStr[i] = "";
            }
            if (ProgramaAuditoria.Fk_Id_Sede == 0)
            {
                Validacion[0] = true;
                ValidacionStr[0] = "No ha seleccionado una sede";
            }
            if (ProgramaAuditoria.Año == 0)
            {
                Validacion[1] = true;
                ValidacionStr[1] = "No ha digitado el año";
            }
            else
            {
                if (ProgramaAuditoria.Año.ToString().Length != 4)
                {
                    Validacion[1] = true;
                    ValidacionStr[1] = "el año debe tener 4 digitos";
                }
            }
            if (ProgramaAuditoria.Fecha_Programacion == DateTime.MinValue)
            {
                Validacion[2] = true;
                ValidacionStr[2] = "No ha digitado una fecha valida";
            }
            if (ProgramaAuditoria.Periodicidad == null)
            {
                Validacion[3] = true;
                ValidacionStr[3] = "No ha elegido la periodicidad";
            }
            if (ProgramaAuditoria.Titulo == null)
            {
                Validacion[4] = true;
                ValidacionStr[4] = "No ha digitado el nombre del programa";
            }
            if (ProgramaAuditoria.Objetivo == null)
            {
                Validacion[5] = true;
                ValidacionStr[5] = "No ha digitado el objetivo del programa";
            }
            if (ProgramaAuditoria.Alcance == null)
            {
                Validacion[6] = true;
                ValidacionStr[6] = "No ha digitado el alcance del programa";
            }
            if (ProgramaAuditoria.Metodologia == null)
            {
                Validacion[7] = true;
                ValidacionStr[7] = "No ha digitado la metodología";
            }
            if (ProgramaAuditoria.Competencia == null)
            {
                Validacion[8] = true;
                ValidacionStr[8] = "No ha digitado las competencias";
            }
            if (ProgramaAuditoria.Recursos == null)
            {
                Validacion[9] = true;
                ValidacionStr[9] = "No ha digitado los recursos";
            }
            var Model = ProgramaAuditoria;
            return Json(new { Model, Estado, Probar, Validacion, ValidacionStr }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult EditarPrograma(string IdPrograma)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }
            EDAuditoriaPrograma EDProgramaAuditoria = new EDAuditoriaPrograma();
            ViewBag.SrcPres = "";
            ViewBag.SrcRes = "";
            ViewBag.IdSede = "";
            int IdProgramaInt = 0;
            if (int.TryParse(IdPrograma, out IdProgramaInt))
            {
                EDProgramaAuditoria = LNAuditoria.Consultaprograma(IdProgramaInt);
            }
            if (EDProgramaAuditoria.RutaArchivoPres != null && EDProgramaAuditoria.NombreArchivoCopasst != null)
            {
                string fileName = Server.MapPath(Path.Combine(EDProgramaAuditoria.RutaArchivoPres, EDProgramaAuditoria.NombreArchivoCopasst));
                if (System.IO.File.Exists(fileName))
                {
                    string ImagenBase64 = UrlToBase64(fileName);
                    ImagenBase64 = "data:image/png;base64," + ImagenBase64;
                    ViewBag.SrcPres = ImagenBase64;
                }
            }
            if (EDProgramaAuditoria.RutaArchivoRes != null && EDProgramaAuditoria.NombreArchivoRes != null)
            {
                string fileName = Server.MapPath(Path.Combine(EDProgramaAuditoria.RutaArchivoRes, EDProgramaAuditoria.NombreArchivoRes));
                if (System.IO.File.Exists(fileName))
                {
                    string ImagenBase64 = UrlToBase64(fileName);
                    ImagenBase64 = "data:image/png;base64," + ImagenBase64;
                    ViewBag.SrcRes = ImagenBase64;
                }
            }
            List<EDSede> ListaSedes = LNEmpresa.ObtenerSedesPorNit(usuarioActual.NitEmpresa);
            EDSede SedeInforme = new EDSede();
            string SedeNombre = "";
            try
            {
                SedeInforme = ListaSedes.Where(s => s.IdSede == EDProgramaAuditoria.Fk_Id_Sede).FirstOrDefault();
                SedeNombre = SedeInforme.NombreSede;
                EDProgramaAuditoria.SedeAuditoria = SedeNombre;
            }
            catch (Exception)
            {

            }
            ViewBag.IdSede = EDProgramaAuditoria.Fk_Id_Sede;
            List<SelectListItem> ListaPeriodicidad = new List<SelectListItem>();
            if (EDProgramaAuditoria.Periodicidad == "Anual")
            {
                ListaPeriodicidad.Add(new SelectListItem { Text = "Anual", Value = "Anual", Selected = true });
            }
            else
            {
                ListaPeriodicidad.Add(new SelectListItem { Text = "Anual", Value = "Anual" });
            }
            if (EDProgramaAuditoria.Periodicidad == "Semestral")
            {
                ListaPeriodicidad.Add(new SelectListItem { Text = "Semestral", Value = "Semestral", Selected = true });
            }
            else
            {
                ListaPeriodicidad.Add(new SelectListItem { Text = "Semestral", Value = "Semestral" });
            }
            if (EDProgramaAuditoria.Periodicidad == "Trimestral")
            {
                ListaPeriodicidad.Add(new SelectListItem { Text = "Trimestral", Value = "Trimestral", Selected = true });
            }
            else
            {
                ListaPeriodicidad.Add(new SelectListItem { Text = "Trimestral", Value = "Trimestral" });
            }
            if (EDProgramaAuditoria.Periodicidad == "Mensual")
            {
                ListaPeriodicidad.Add(new SelectListItem { Text = "Mensual", Value = "Mensual", Selected = true });
            }
            else
            {
                ListaPeriodicidad.Add(new SelectListItem { Text = "Mensual", Value = "Mensual" });
            }
            ViewBag.ListaPeriodo = ListaPeriodicidad;
            return View(EDProgramaAuditoria);
        }
        [HttpPost]
        public ActionResult EditarPrograma(AuditoriaPrograma ProgramaAuditoria)
        {
            bool Probar = false;
            string Estado = "No se editó el programa de auditorias, por favor revise la información suministrada";
            bool[] Validacion = new bool[10];
            string[] ValidacionStr = new string[10];
            for (int i = 0; i < 10; i++)
            {
                Validacion[i] = false;
                ValidacionStr[i] = "";
            }
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                Estado = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { Estado, Probar, Validacion, ValidacionStr });
            }
            ModelState.Remove("FirmaScrImageRes");
            ModelState.Remove("FirmaScrImagePres");
            if (ModelState.IsValid)
            {
                EDAuditoriaPrograma NuevoProgramaAnterior = LNAuditoria.Consultaprograma(ProgramaAuditoria.Pk_Id_Programa);
                string FilePresAnterior = null;
                string FileResAnterior = null;

                string RutaPresAnterior = null;
                string RutaResAnterior = null;

                if (NuevoProgramaAnterior.NombreArchivoCopasst != null && NuevoProgramaAnterior.RutaArchivoPres != null)
                {
                    FilePresAnterior = NuevoProgramaAnterior.NombreArchivoCopasst;
                    RutaPresAnterior = NuevoProgramaAnterior.RutaArchivoPres;
                }
                if (NuevoProgramaAnterior.NombreArchivoRes != null && NuevoProgramaAnterior.RutaArchivoRes != null)
                {
                    FileResAnterior = NuevoProgramaAnterior.NombreArchivoRes;
                    RutaResAnterior = NuevoProgramaAnterior.RutaArchivoRes;
                }
                AuditoriaPrograma NuevoPrograma = new AuditoriaPrograma();
                NuevoPrograma = ProgramaAuditoria;
                NuevoPrograma.Fk_Id_Empresa = usuarioActual.IdEmpresa;
                if (ProgramaAuditoria.Fk_Id_Sede > 0)
                {
                    //Consultar Sede
                    NuevoPrograma.Fk_Id_Sede = ProgramaAuditoria.Fk_Id_Sede;
                    NuevoPrograma.SedeAuditoria = "Sede Consultada";
                }
                else
                {
                    Estado = "<li>Verifique que haya suministrado la SEDE</li>";
                    return Json(new { Estado, Probar });
                }
                if (ProgramaAuditoria.Año.ToString().Length > 4)
                {
                    Estado = "Verifique que el año tenga 4 dígitos";
                    Validacion[1] = true;
                    ValidacionStr[1] = "el año debe tener 4 dígitos";
                    return Json(new { Estado, Probar, Validacion, ValidacionStr }, JsonRequestBehavior.AllowGet);
                }

                DateTime datePrograma = ProgramaAuditoria.Fecha_Programacion;
                NuevoPrograma.Fecha_Programacion = datePrograma;

                if (NuevoPrograma.FirmaScrImagePres != "")
                {
                    try
                    {
                        string Nombre = "Fpres" + RandomString(9) + ".png";
                        string b64 = NuevoPrograma.FirmaScrImagePres;
                        b64 = b64.Replace("data:image/png;base64,", "");
                        Image Imagen = Base64ToImage(b64);
                        string outputPath = (Server.MapPath(Path.Combine(RutaFirmasAud, Nombre)));
                        CrearCarpeta(RutaFirmasAud);
                        Imagen.Save(outputPath, ImageFormat.Png);
                        NuevoPrograma.NombreArchivoCopasst = Nombre;
                        NuevoPrograma.RutaArchivoPres = RutaFirmasAud;
                        //Eliminar Firma anterior
                        try
                        {
                            if (NuevoProgramaAnterior.NombreArchivoCopasst != null && NuevoProgramaAnterior.RutaArchivoPres != null)
                            {
                                string fileName = Server.MapPath(Path.Combine(RutaPresAnterior, FilePresAnterior));
                                if (fileName != null || fileName != string.Empty)
                                {
                                    if (System.IO.File.Exists(fileName))
                                    {
                                        System.IO.File.Delete(fileName);
                                    }
                                }
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }
                    catch (Exception)
                    {
                        NuevoPrograma.NombreArchivoCopasst = null;
                        NuevoPrograma.RutaArchivoPres = null;
                    }
                }
                if (NuevoPrograma.FirmaScrImageRes != "")
                {
                    try
                    {
                        string Nombre = "Fres" + RandomString(9) + ".png";
                        string b64 = NuevoPrograma.FirmaScrImageRes;
                        b64 = b64.Replace("data:image/png;base64,", "");
                        Image Imagen = Base64ToImage(b64);
                        string outputPath = (Server.MapPath(Path.Combine(RutaFirmasAud, Nombre)));
                        CrearCarpeta(RutaFirmasAud);
                        Imagen.Save(outputPath, ImageFormat.Png);
                        NuevoPrograma.NombreArchivoRes = Nombre;
                        NuevoPrograma.RutaArchivoRes = RutaFirmasAud;
                        //Eliminar Firma anterior
                        try
                        {
                            if (NuevoProgramaAnterior.NombreArchivoRes != null && NuevoProgramaAnterior.RutaArchivoRes != null)
                            {
                                string fileName = Server.MapPath(Path.Combine(RutaResAnterior, FileResAnterior));
                                if (fileName != null || fileName != string.Empty)
                                {
                                    if (System.IO.File.Exists(fileName))
                                    {
                                        System.IO.File.Delete(fileName);
                                    }
                                }
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }
                    catch (Exception)
                    {
                        NuevoPrograma.NombreArchivoRes = null;
                        NuevoPrograma.RutaArchivoRes = null;
                    }

                }
                EDAuditoriaPrograma EDAuditoriaPrograma = new EDAuditoriaPrograma();
                EDAuditoriaPrograma.Pk_Id_Programa = NuevoPrograma.Pk_Id_Programa;
                EDAuditoriaPrograma.Titulo = NuevoPrograma.Titulo;
                EDAuditoriaPrograma.Objetivo = NuevoPrograma.Objetivo;
                EDAuditoriaPrograma.Alcance = NuevoPrograma.Alcance;
                EDAuditoriaPrograma.Metodologia = NuevoPrograma.Metodologia;
                EDAuditoriaPrograma.Competencia = NuevoPrograma.Competencia;
                EDAuditoriaPrograma.Recursos = NuevoPrograma.Recursos;
                EDAuditoriaPrograma.Fecha_Programacion = NuevoPrograma.Fecha_Programacion;
                EDAuditoriaPrograma.Año = NuevoPrograma.Año;
                EDAuditoriaPrograma.Periodicidad = NuevoPrograma.Periodicidad;
                EDAuditoriaPrograma.NombreArchivoRes = NuevoPrograma.NombreArchivoRes;
                EDAuditoriaPrograma.RutaArchivoRes = NuevoPrograma.RutaArchivoRes;
                EDAuditoriaPrograma.Nombre_Responsable = NuevoPrograma.Nombre_Responsable;
                EDAuditoriaPrograma.Numero_Id_Responsable = NuevoPrograma.Numero_Id_Responsable;
                EDAuditoriaPrograma.FirmaScrImageRes = null;
                EDAuditoriaPrograma.NombreArchivoCopasst = NuevoPrograma.NombreArchivoCopasst;
                EDAuditoriaPrograma.RutaArchivoPres = NuevoPrograma.RutaArchivoPres;
                EDAuditoriaPrograma.Nombre_Copasst = NuevoPrograma.Nombre_Copasst;
                EDAuditoriaPrograma.Numero_Id_Copasst = NuevoPrograma.Numero_Id_Copasst;
                EDAuditoriaPrograma.FirmaScrImagePres = null;
                EDAuditoriaPrograma.Fk_Id_Empresa = NuevoPrograma.Fk_Id_Empresa;
                EDAuditoriaPrograma.SedeAuditoria = NuevoPrograma.SedeAuditoria;
                EDAuditoriaPrograma.Fk_Id_Sede = NuevoPrograma.Fk_Id_Sede;
                bool ProbarGuardado = LNAuditoria.ActualizarPrograma(EDAuditoriaPrograma);
                if (ProbarGuardado)
                {
                    Probar = true;
                    return Json(new { Estado, Probar });
                }

            }
            for (int i = 0; i < 10; i++)
            {
                Validacion[i] = false;
                ValidacionStr[i] = "";
            }
            if (ProgramaAuditoria.Fk_Id_Sede == 0)
            {
                Validacion[0] = true;
                ValidacionStr[0] = "No ha seleccionado una sede";
            }

            if (ProgramaAuditoria.Año == 0)
            {
                Validacion[1] = true;
                ValidacionStr[1] = "No ha digitado el año";
            }
            else
            {
                if (ProgramaAuditoria.Año.ToString().Length != 4)
                {
                    Validacion[1] = true;
                    ValidacionStr[1] = "el año debe tener 4 digitos";
                }
            }
            if (ProgramaAuditoria.Fecha_Programacion == DateTime.MinValue)
            {
                Validacion[2] = true;
                ValidacionStr[2] = "No ha digitado una fecha valida";
            }
            if (ProgramaAuditoria.Periodicidad == null)
            {
                Validacion[3] = true;
                ValidacionStr[3] = "No ha elegido la periodicidad";
            }
            if (ProgramaAuditoria.Titulo == null)
            {
                Validacion[4] = true;
                ValidacionStr[4] = "No ha digitado el nombre del programa";
            }
            if (ProgramaAuditoria.Objetivo == null)
            {
                Validacion[5] = true;
                ValidacionStr[5] = "No ha digitado el objetivo del programa";
            }
            if (ProgramaAuditoria.Alcance == null)
            {
                Validacion[6] = true;
                ValidacionStr[6] = "No ha digitado el alcance del programa";
            }
            if (ProgramaAuditoria.Metodologia == null)
            {
                Validacion[7] = true;
                ValidacionStr[7] = "No ha digitado la metodología";
            }
            if (ProgramaAuditoria.Competencia == null)
            {
                Validacion[8] = true;
                ValidacionStr[8] = "No ha digitado las competencias";
            }
            if (ProgramaAuditoria.Recursos == null)
            {
                Validacion[9] = true;
                ValidacionStr[9] = "No ha digitado los recursos";
            }
            var Model = ProgramaAuditoria;
            return Json(new { Model, Estado, Probar, Validacion, ValidacionStr }, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult EliminarPrograma(string IdPrograma)
        {
            bool probar = false;
            string resultado = "El PROGRAMA no ha podido ser eliminado";
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                resultado = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { probar, resultado });
            }
            if (usuarioActual.IdEmpresa != 0)
            {
                string rutaPres = null;
                string rutaRes = null;
                string filePres = null;
                string fileRes = null;
                List<string> ListaRutaFilePlan = new List<string>();
                int IdProgramaInt = 0;
                bool probarNumero = int.TryParse(IdPrograma, out IdProgramaInt);
                if (IdProgramaInt != 0)
                {
                    EDAuditoriaPrograma EDAuditoriaPrograma = new EDAuditoriaPrograma();
                    EDAuditoriaPrograma = LNAuditoria.Consultaprograma(IdProgramaInt);

                    rutaPres = EDAuditoriaPrograma.RutaArchivoPres;
                    rutaRes = EDAuditoriaPrograma.RutaArchivoRes;
                    filePres = EDAuditoriaPrograma.NombreArchivoCopasst;
                    fileRes = EDAuditoriaPrograma.NombreArchivoRes;

                    List<EDAuditoria> ListaAuditorias = LNAuditoria.ConsultaAuditorias(IdProgramaInt, usuarioActual.NitEmpresa);
                    if (ListaAuditorias.Count == 0)
                    {
                        bool BorraProg = LNAuditoria.EliminarPrograma(IdProgramaInt, usuarioActual.IdEmpresa);
                        if (BorraProg == false)
                        {
                            return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
                        }
                        //Eliminar firmas de programa
                        try
                        {
                            if (fileRes != null && rutaRes != null)
                            {
                                string fileName = Server.MapPath(Path.Combine(rutaRes, fileRes));
                                if (fileName != null || fileName != string.Empty)
                                {
                                    if (System.IO.File.Exists(fileName))
                                    {
                                        System.IO.File.Delete(fileName);
                                    }
                                }
                            }
                        }
                        catch (Exception)
                        {
                        }
                        try
                        {
                            if (filePres != null && rutaPres != null)
                            {
                                string fileName = Server.MapPath(Path.Combine(rutaPres, filePres));
                                if (fileName != null || fileName != string.Empty)
                                {
                                    if (System.IO.File.Exists(fileName))
                                    {
                                        System.IO.File.Delete(fileName);
                                    }
                                }
                            }
                        }
                        catch (Exception)
                        {
                        }
                        probar = true;
                        resultado = "El PROGRAMA se ha eliminado correctamente";
                        return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        resultado = "No se ha eliminado el PROGRAMA, asegurese por favor que no existan auditorias en este PROGRAMA antes de eliminar";
                        return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            else
            {
                resultado = "No se ha eliminado el PROGRAMA, El usuario no ha iniciado sesión en el sistema";
                return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Auditoria
        [HttpGet]
        public ActionResult Auditoria(string IdPrograma)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }
            List<EDAuditoria> EDAuditorias = new List<EDAuditoria>();
            int IdProgramaInt = 0;
            if (int.TryParse(IdPrograma, out IdProgramaInt))
            {
                EDAuditorias = LNAuditoria.ConsultaAuditorias(IdProgramaInt, usuarioActual.NitEmpresa);
            }
            EDAuditorias = EDAuditorias.OrderBy(s => s.Periodo).ToList();
            ViewBag.IdPrograma = IdPrograma;
            return View(EDAuditorias);
        }
        [HttpGet]
        public ActionResult PlanAuditoria(string IdPrograma)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }
            int IdProgramaInt = 0;
            List<EDProceso> procesos = LNProcesos.ObtenerProcesosPorEmpresa(usuarioActual.NitEmpresa);
            ViewBag.Pk_Id_Proceso = new SelectList(procesos, "Id_Proceso", "Descripcion");
            ViewBag.IdPrograma = IdPrograma;
            EDAuditoriaPrograma AuditoriaPrograma = new EDAuditoriaPrograma();
            if (int.TryParse(IdPrograma, out IdProgramaInt))
            {
                AuditoriaPrograma = LNAuditoria.Consultaprograma(IdProgramaInt);
            }
            List<string> ListaOpciones = new List<string>();
            string anio = AuditoriaPrograma.Año.ToString();
            if (AuditoriaPrograma.Periodicidad == "Anual")
            {
                ListaOpciones.Add("Año " + anio);
            }
            if (AuditoriaPrograma.Periodicidad == "Semestral")
            {
                ListaOpciones.Add("Primer Semestre");
                ListaOpciones.Add("Segundo Semestre");
            }
            if (AuditoriaPrograma.Periodicidad == "Trimestral")
            {
                ListaOpciones.Add("Primer Trimestre");
                ListaOpciones.Add("Segundo Trimestre");
                ListaOpciones.Add("Tercer Trimestre");
                ListaOpciones.Add("Cuarto Trimestre");
            }
            if (AuditoriaPrograma.Periodicidad == "Mensual")
            {
                ListaOpciones.Add("Enero");
                ListaOpciones.Add("Febrero");
                ListaOpciones.Add("Marzo");
                ListaOpciones.Add("Abril");
                ListaOpciones.Add("Mayo");
                ListaOpciones.Add("Junio");
                ListaOpciones.Add("Julio");
                ListaOpciones.Add("Agosto");
                ListaOpciones.Add("Septiembre");
                ListaOpciones.Add("Octubre");
                ListaOpciones.Add("Noviembre");
                ListaOpciones.Add("Diciembre");
            }
            List<SelectListItem> ListaPeriodicidad = new List<SelectListItem>();
            foreach (var item in ListaOpciones)
            {
                ListaPeriodicidad.Add(new SelectListItem { Text = item.ToString(), Value = item.ToString() });
            }
            ViewBag.ListaPeriodo = ListaPeriodicidad;
            return View();
        }
        [HttpPost]
        public ActionResult PlanAuditoria(EDAuditoria EDAuditorias)
        {
            bool Probar = false;
            string Estado = "No se guardó el plan de auditoría, por favor revise la información suministrada";
            bool[] Validacion = new bool[10];
            string[] ValidacionStr = new string[10];
            for (int i = 0; i < 10; i++)
            {
                Validacion[i] = false;
                ValidacionStr[i] = "";
            }
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                Estado = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { Estado, Probar, Validacion, ValidacionStr });
            }
            if (ModelState.IsValid)
            {
                EDAuditoria NuevaAuditoria = new EDAuditoria();
                NuevaAuditoria = EDAuditorias;
                if (EDAuditorias.Fk_Id_Proceso > 0)
                {
                    //Consultar Sede
                    NuevaAuditoria.Fk_Id_Proceso = EDAuditorias.Fk_Id_Proceso;
                    NuevaAuditoria.NombreProceso1 = "Proceso Consultado";
                }
                else
                {
                    Estado = "Verifique que haya suministrado el PROCESO";
                    return Json(new { Estado, Probar });
                }
                DateTime dateRealiz = EDAuditorias.FechaRealizacion;
                NuevaAuditoria.FechaRealizacion = dateRealiz;
                bool ProbarGuardado = LNAuditoria.CrearAuditoria(NuevaAuditoria);
                if (ProbarGuardado)
                {
                    Probar = true;
                    return Json(new { url = Url.Action("Auditoria", "Auditoria", new { IdPrograma = NuevaAuditoria.Fk_Id_Programa.ToString() }), Estado, Probar });
                }
            }
            for (int i = 0; i < 10; i++)
            {
                Validacion[i] = false;
                ValidacionStr[i] = "";
            }
            if (EDAuditorias.Periodo == null)
            {
                Validacion[0] = true;
                ValidacionStr[0] = "No ha seleccionado un periodo";
            }

            if (EDAuditorias.Fk_Id_Proceso == 0)
            {
                Validacion[1] = true;
                ValidacionStr[1] = "No ha seleccionado un proceso";
            }
            if (EDAuditorias.Objetivo == null)
            {
                Validacion[2] = true;
                ValidacionStr[2] = "No ha digitado el objetivo";
            }
            if (EDAuditorias.Alcance == null)
            {
                Validacion[3] = true;
                ValidacionStr[3] = "No ha digitado el alcance";
            }
            if (EDAuditorias.Criterios == null)
            {
                Validacion[4] = true;
                ValidacionStr[4] = "No ha digitado los criterios";
            }
            if (EDAuditorias.Auditado == null)
            {
                Validacion[5] = true;
                ValidacionStr[5] = "No ha digitado el auditado";
            }
            if (EDAuditorias.Auditador == null)
            {
                Validacion[6] = true;
                ValidacionStr[6] = "No ha digitado el auditor";
            }
            if (EDAuditorias.Requisito == null)
            {
                Validacion[7] = true;
                ValidacionStr[7] = "No ha digitado el requisito";
            }
            if (EDAuditorias.Duracion == null)
            {
                Validacion[8] = true;
                ValidacionStr[8] = "No ha digitado la duración";
            }
            if (EDAuditorias.FechaRealizacion == DateTime.MinValue)
            {
                Validacion[9] = true;
                ValidacionStr[9] = "No ha digitado una fecha valida";
            }
            var Model = EDAuditorias;
            return Json(new { Model, Estado, Probar, Validacion, ValidacionStr }, JsonRequestBehavior.AllowGet);

        }
        [HttpGet]
        public ActionResult EditarPlanAuditoria(string IdAuditoria)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }
            EDAuditoria EDAuditoria = new EDAuditoria();
            int IdAuditoriaInt = 0;
            if (int.TryParse(IdAuditoria, out IdAuditoriaInt))
            {
                EDAuditoria = LNAuditoria.ConsultaAuditoria(IdAuditoriaInt, usuarioActual.NitEmpresa);
            }
            if (IdAuditoriaInt == 0)
            {
                return HttpNotFound();
            }
            ViewBag.IdPrograma = EDAuditoria.Fk_Id_Programa;

            EDAuditoriaPrograma AuditoriaPrograma = new EDAuditoriaPrograma();
            AuditoriaPrograma = LNAuditoria.Consultaprograma(EDAuditoria.Fk_Id_Programa);

            List<EDProceso> ListaProceso = LNProcesos.ObtenerProcesosPorEmpresa(usuarioActual.NitEmpresa);
            int index_Proceso = EDAuditoria.Fk_Id_Proceso;
            EDProceso ProcesoBuscado = ListaProceso.Find(s => s.Id_Proceso == index_Proceso);

            if (ProcesoBuscado != null)
            {
                ViewBag.Pk_Id_Proceso = new SelectList(ListaProceso, "Id_Proceso", "Descripcion", index_Proceso);
            }
            else
            {
                ViewBag.Pk_Id_Proceso = new SelectList(ListaProceso, "Id_Proceso", "Descripcion", 0);
            }

            List<string> ListaOpciones = new List<string>();
            if (AuditoriaPrograma.Periodicidad == "Anual")
            {
                ListaOpciones.Add("Año");
            }
            if (AuditoriaPrograma.Periodicidad == "Semestral")
            {
                ListaOpciones.Add("Primer Semestre");
                ListaOpciones.Add("Segundo Semestre");
            }
            if (AuditoriaPrograma.Periodicidad == "Trimestral")
            {
                ListaOpciones.Add("Primer Trimestre");
                ListaOpciones.Add("Segundo Trimestre");
                ListaOpciones.Add("Tercer Trimestre");
                ListaOpciones.Add("Cuarto Trimestre");
            }
            if (AuditoriaPrograma.Periodicidad == "Mensual")
            {
                ListaOpciones.Add("Enero");
                ListaOpciones.Add("Febrero");
                ListaOpciones.Add("Marzo");
                ListaOpciones.Add("Abril");
                ListaOpciones.Add("Mayo");
                ListaOpciones.Add("Junio");
                ListaOpciones.Add("Julio");
                ListaOpciones.Add("Agosto");
                ListaOpciones.Add("Septiembre");
                ListaOpciones.Add("Octubre");
                ListaOpciones.Add("Noviembre");
                ListaOpciones.Add("Diciembre");
            }
            List<SelectListItem> ListaPeriodicidad = new List<SelectListItem>();
            foreach (var item in ListaOpciones)
            {
                if (EDAuditoria.Periodo == item.ToString())
                {
                    ListaPeriodicidad.Add(new SelectListItem { Text = item.ToString(), Value = item.ToString(), Selected = true });
                }
                else
                {
                    ListaPeriodicidad.Add(new SelectListItem { Text = item.ToString(), Value = item.ToString() });
                }
            }
            ViewBag.ListaPeriodo = ListaPeriodicidad;

            return View(EDAuditoria);
        }
        [HttpPost]
        public ActionResult EditarPlanAuditoria(EDAuditoria EDAuditorias)
        {
            bool Probar = false;
            string Estado = "No se editó el plan de auditoría, por favor revise la información suministrada";
            bool[] Validacion = new bool[10];
            string[] ValidacionStr = new string[10];
            for (int i = 0; i < 10; i++)
            {
                Validacion[i] = false;
                ValidacionStr[i] = "";
            }
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                Estado = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { Estado, Probar, Validacion, ValidacionStr });
            }
            if (ModelState.IsValid)
            {
                EDAuditoria NuevaAuditoria = new EDAuditoria();
                NuevaAuditoria = EDAuditorias;
                if (EDAuditorias.Fk_Id_Proceso > 0)
                {
                    //Consultar Proceso
                    NuevaAuditoria.Fk_Id_Proceso = EDAuditorias.Fk_Id_Proceso;
                    NuevaAuditoria.NombreProceso1 = "Proceso Consultado";
                }
                else
                {
                    Estado = "<li>Verifique que haya suministrado el PROCESO</li>";
                    return Json(new { Estado, Probar });
                }
                DateTime dateRealiz = EDAuditorias.FechaRealizacion;
                NuevaAuditoria.FechaRealizacion = dateRealiz;
                bool ProbarGuardado = LNAuditoria.ActualizarAuditoria(NuevaAuditoria);
                if (ProbarGuardado)
                {
                    Probar = true;
                    return Json(new { url = Url.Action("Auditoria", "Auditoria", new { IdPrograma = NuevaAuditoria.Fk_Id_Programa.ToString() }), Estado, Probar });
                }
            }
            
            if (EDAuditorias.Periodo == null)
            {
                Validacion[0] = true;
                ValidacionStr[0] = "No ha seleccionado un periodo";
            }

            if (EDAuditorias.Fk_Id_Proceso == 0)
            {
                Validacion[1] = true;
                ValidacionStr[1] = "No ha seleccionado un proceso";
            }
            if (EDAuditorias.Objetivo == null)
            {
                Validacion[2] = true;
                ValidacionStr[2] = "No ha digitado el objetivo";
            }
            if (EDAuditorias.Alcance == null)
            {
                Validacion[3] = true;
                ValidacionStr[3] = "No ha digitado el alcance";
            }
            if (EDAuditorias.Criterios == null)
            {
                Validacion[4] = true;
                ValidacionStr[4] = "No ha digitado los criterios";
            }
            if (EDAuditorias.Auditado == null)
            {
                Validacion[5] = true;
                ValidacionStr[5] = "No ha digitado el auditado";
            }
            if (EDAuditorias.Auditador == null)
            {
                Validacion[6] = true;
                ValidacionStr[6] = "No ha digitado el auditor";
            }
            if (EDAuditorias.Requisito == null)
            {
                Validacion[7] = true;
                ValidacionStr[7] = "No ha digitado el requisito";
            }
            if (EDAuditorias.Duracion == null)
            {
                Validacion[8] = true;
                ValidacionStr[8] = "No ha digitado la duración";
            }
            if (EDAuditorias.FechaRealizacion == DateTime.MinValue)
            {
                Validacion[9] = true;
                ValidacionStr[9] = "No ha digitado una fecha valida";
            }
            var Model = EDAuditorias;
            return Json(new { Model, Estado, Probar, Validacion, ValidacionStr }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Actividades(string IdAuditoria)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }
            EDAuditoria EDAuditoria = new EDAuditoria();
            int IdAuditoriaInt = 0;
            if (int.TryParse(IdAuditoria, out IdAuditoriaInt))
            {
                EDAuditoria = LNAuditoria.ConsultaAuditoria(IdAuditoriaInt, usuarioActual.NitEmpresa);
            }
            if (IdAuditoriaInt == 0)
            {
                return HttpNotFound();
            }
            ViewBag.IdPrograma = EDAuditoria.Fk_Id_Programa;
            ViewBag.IdAuditoria = IdAuditoria;
            ViewBag.Proceso = EDAuditoria.NombreProceso1;
            ViewBag.Periodo = EDAuditoria.Periodo;
            ViewBag.FechaPlaneada = EDAuditoria.FechaRealizacion;
            EDAuditoriaActividad EDAuditoriaActividad = new EDAuditoriaActividad();
            EDAuditoriaActividad = LNAuditoria.ConsultaAuditoriaActividades(IdAuditoriaInt);
            return View(EDAuditoriaActividad);
        }
        [HttpPost]
        public ActionResult AgregarActividad(EDAuditoriaActividad EDAuditoriaActividad)
        {
            bool Probar = false;
            string Estado = "No se guardó la actividad";
            bool[] Validacion = new bool[10];
            string[] ValidacionStr = new string[10];
            for (int i = 0; i < 10; i++)
            {
                Validacion[i] = false;
                ValidacionStr[i] = "";
            }
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                Estado = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { Estado, Probar, Validacion, ValidacionStr });
            }
            if (ModelState.IsValid)
            {
                EDAuditoriaActividad NuevaEDActividad = new EDAuditoriaActividad();
                NuevaEDActividad = EDAuditoriaActividad;
                if (NuevaEDActividad.Fk_Id_Auditoria == 0)
                {
                    Estado = "<li>Verifique que haya suministrado el Id de Auditoria</li>";
                    return Json(new { Estado, Probar });
                }
                DateTime date = EDAuditoriaActividad.Fecha_Hora;
                NuevaEDActividad.Fecha_Hora = date;
                bool ProbarGuardado = LNAuditoria.GuardarActividad(NuevaEDActividad);
                if (ProbarGuardado)
                {
                    Probar = true;
                    return Json(new { Estado, Probar });
                }
            }
            if (EDAuditoriaActividad.Fk_Id_Auditoria == 0)
            {
                Estado += "<li>Verifique que haya suministrado el Id de la AUDITORIA</li>";
            }
            if (EDAuditoriaActividad.Tema == null)
            {
                Estado += "<li>Verifique que haya suministrado el TEMA</li>";
            }
            if (EDAuditoriaActividad.Responsable == null)
            {
                Estado += "<li>Verifique que haya suministrado el RESPONSABLE</li>";
            }
            if (EDAuditoriaActividad.Lugar == null)
            {
                Estado += "<li>Verifique que haya suministrado el LUGAR</li>";
            }
            if (EDAuditoriaActividad.TiempoEstimado == null)
            {
                Estado += "<li>Verifique que haya suministrado el TIEMPO ESTIMADO</li>";
            }
            Estado = "<ul>" + Estado + "</ul>";
            var Model = EDAuditoriaActividad;
            return Json(new { Model, Estado, Probar }, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult MostrarEdicionActividad(string Values)
        {
            bool probar = false;
            string resultado = "No exite el elemento";
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                resultado = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { probar, resultado });
            }
            EDAuditoriaActividad EDAuditoriaActividad = new EDAuditoriaActividad();
            int IdAct = 0;
            bool probarNumero = int.TryParse(Values, out IdAct);
            if (IdAct != 0)
            {
                //consultar base de datos con un bool
                EDAuditoriaActividad = LNAuditoria.ConsultaAuditoriaActividad(IdAct);
                if (EDAuditoriaActividad != null)
                {
                    probar = true;
                    resultado = "La actividad existe, puede continuar con la edición de este elemento";
                    string Fecha_s = EDAuditoriaActividad.Fecha_Hora.ToString("dd/MM/yyyy HH:mm:ss");
                    var Model = EDAuditoriaActividad;
                    return Json(new { Model, probar, resultado, Fecha_s }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult EliminarActividad(string Values)
        {
            bool probar = false;
            string resultado = "No exite el elemento";
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                resultado = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { probar, resultado });
            }
            EDAuditoriaActividad EDAuditoriaActividad = new EDAuditoriaActividad();
            int IdAct = 0;
            bool probarNumero = int.TryParse(Values, out IdAct);
            if (IdAct != 0)
            {
                //consultar base de datos con un bool
                EDAuditoriaActividad = LNAuditoria.ConsultaAuditoriaActividad(IdAct);
                if (EDAuditoriaActividad != null)
                {
                    probar = LNAuditoria.EliminarActividad(EDAuditoriaActividad);
                    resultado = "La Actividad se ha eliminado correctamente";

                    return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult EditarActividad(EDAuditoriaActividad EDAuditoriaActividad)
        {
            bool Probar = false;
            string Estado = "No se pudo completar la edicion de la actividad";
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                Estado = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { Probar, Estado });
            }
            if (ModelState.IsValid)
            {
                EDAuditoriaActividad NuevaEDActividad = new EDAuditoriaActividad();
                NuevaEDActividad = EDAuditoriaActividad;
                if (NuevaEDActividad.Fk_Id_Auditoria == 0)
                {
                    Estado = "<li>Verifique que haya suministrado el Id de Auditoria</li>";
                    return Json(new { Estado, Probar });
                }
                if (NuevaEDActividad.Pk_Id_Cronograma_Auditoria == 0)
                {
                    Estado = "<li>Verifique que haya suministrado el Id de la actividad Puede ser que no exista</li>";
                    return Json(new { Estado, Probar });
                }
                DateTime date = EDAuditoriaActividad.Fecha_Hora;
                NuevaEDActividad.Fecha_Hora = date;
                bool ProbarGuardado = LNAuditoria.ActualizarActividad(NuevaEDActividad);
                if (ProbarGuardado)
                {
                    Probar = true;
                    return Json(new { Estado, Probar });
                }
                else
                {
                    return Json(new { Estado, Probar });
                }
            }
            if (EDAuditoriaActividad.Fk_Id_Auditoria == 0)
            {
                Estado += "<li>Verifique que haya suministrado el Id de la AUDITORIA</li>";
            }
            if (EDAuditoriaActividad.Tema == null)
            {
                Estado += "<li>Verifique que haya suministrado el TEMA</li>";
            }
            if (EDAuditoriaActividad.Responsable == null)
            {
                Estado += "<li>Verifique que haya suministrado el RESPONSABLE</li>";
            }
            if (EDAuditoriaActividad.Lugar == null)
            {
                Estado += "<li>Verifique que haya suministrado el LUGAR</li>";
            }
            if (EDAuditoriaActividad.TiempoEstimado == null)
            {
                Estado += "<li>Verifique que haya suministrado el TIEMPO ESTIMADO</li>";
            }
            Estado = "<ul>" + Estado + "</ul>";
            var Model = EDAuditoriaActividad;
            return Json(new { Model, Estado, Probar }, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult EliminarAuditoria(string IdAuditoria)
        {
            bool probar = false;
            string resultado = "La auditoría no ha podido ser eliminada";
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                resultado = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { probar, resultado });
            }
            if (usuarioActual.IdEmpresa != 0)
            {
                int IdAuditoriaInt = 0;
                bool probarNumero = int.TryParse(IdAuditoria, out IdAuditoriaInt);
                if (IdAuditoriaInt != 0)
                {
                    string rutaAud = null;
                    string rutaRes = null;
                    string fileAud = null;
                    string fileRes = null;
                    EDAuditoriaInforme EDAuditoriaInforme = new EDAuditoriaInforme();
                    EDAuditoriaInforme = LNAuditoria.ConsultaConclusiones(IdAuditoriaInt);
                    rutaAud = EDAuditoriaInforme.RutaArchivoAuditor;
                    rutaRes = EDAuditoriaInforme.RutaArchivoRes;
                    fileAud = EDAuditoriaInforme.NombreArchivoAuditor;
                    fileRes = EDAuditoriaInforme.NombreArchivoRes;
                    bool BorraAud = LNAuditoria.EliminarPlanAuditoria(IdAuditoriaInt);
                    if (BorraAud == false)
                    {
                        return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
                    }
                    try
                    {
                        if (fileRes != null && rutaRes != null)
                        {
                            string fileName = Server.MapPath(Path.Combine(rutaRes, fileRes));
                            if (fileName != null || fileName != string.Empty)
                            {
                                if (System.IO.File.Exists(fileName))
                                {
                                    System.IO.File.Delete(fileName);
                                }
                            }
                        }
                    }
                    catch (Exception)
                    {

                    }
                    try
                    {
                        if (fileAud != null && rutaAud != null)
                        {
                            string fileName = Server.MapPath(Path.Combine(rutaAud, fileAud));
                            if (fileName != null || fileName != string.Empty)
                            {
                                if (System.IO.File.Exists(fileName))
                                {
                                    System.IO.File.Delete(fileName);
                                }
                            }
                        }
                    }
                    catch (Exception)
                    {

                    }
                    probar = true;
                    resultado = "La auditoría se ha eliminado correctamente";
                    return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Lista_Verificacion
        [HttpGet]
        public ActionResult ListaVerificacion(string IdAuditoria)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }
            EDAuditoria EDAuditoria = new EDAuditoria();
            int IdAuditoriaInt = 0;
            if (int.TryParse(IdAuditoria, out IdAuditoriaInt))
            {
                EDAuditoria = LNAuditoria.ConsultaAuditoria(IdAuditoriaInt, usuarioActual.NitEmpresa);
            }
            if (IdAuditoriaInt == 0)
            {
                return HttpNotFound();
            }
            ViewBag.IdPrograma = EDAuditoria.Fk_Id_Programa;
            ViewBag.IdAuditoria = IdAuditoria;

            ViewBag.Proceso = EDAuditoria.NombreProceso1;
            ViewBag.Periodo = EDAuditoria.Periodo;
            try
            {
                ViewBag.FechaPlaneada = EDAuditoria.FechaRealizacion.ToString("dd/MM/yyyy");
            }
            catch (Exception)
            {
                ViewBag.FechaPlaneada = EDAuditoria.FechaRealizacion;
            }



            EDAuditoriaVerificacion EDAuditoriaVerificacion = new EDAuditoriaVerificacion();
            EDAuditoriaVerificacion = LNAuditoria.ConsultaListavers(IdAuditoriaInt);

            List<SelectListItem> ListaTipoHallazgo = new List<SelectListItem>();
            ListaTipoHallazgo.Add(new SelectListItem { Text = "No Conformidad", Value = "No Conformidad" });
            ListaTipoHallazgo.Add(new SelectListItem { Text = "Observación", Value = "Observación" });
            ListaTipoHallazgo.Add(new SelectListItem { Text = "Fortaleza", Value = "Fortaleza" });
            ListaTipoHallazgo.Add(new SelectListItem { Text = "Oportunidad de Mejora", Value = "Oportunidad de Mejora" });

            ViewBag.TipoHallazgo = ListaTipoHallazgo;

            return View(EDAuditoriaVerificacion);
        }
        [HttpPost]
        public ActionResult AgregarListaVerificacion(EDAuditoriaVerificacion EDAuditoriaVerificacion)
        {
            bool Probar = false;
            string Estado = "No se pudo completar el guardado de la lista de verificación";
            bool[] Validacion = new bool[10];
            string[] ValidacionStr = new string[10];
            for (int i = 0; i < 10; i++)
            {
                Validacion[i] = false;
                ValidacionStr[i] = "";
            }
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                Estado = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { Estado, Probar, Validacion, ValidacionStr });
            }
            if (ModelState.IsValid && EDAuditoriaVerificacion.Pregunta != null && EDAuditoriaVerificacion.Requisito != null)
            {
                EDAuditoriaVerificacion NuevaEDAuditoriaVerificacion = new EDAuditoriaVerificacion();
                NuevaEDAuditoriaVerificacion = EDAuditoriaVerificacion;
                NuevaEDAuditoriaVerificacion.FechaCierre = DateTime.Today;
                if (NuevaEDAuditoriaVerificacion.Fk_Id_Auditoria == 0)
                {
                    Estado = "<li>Verifique que haya suministrado el Id de Auditoria</li>";
                    return Json(new { Estado, Probar });
                }

                bool ProbarGuardado = LNAuditoria.GuardarListaVer(NuevaEDAuditoriaVerificacion);
                if (ProbarGuardado)
                {
                    Probar = true;
                    return Json(new { Estado, Probar });
                }
            }
            else
            {
                for (int i = 0; i < 10; i++)
                {
                    Validacion[i] = false;
                    ValidacionStr[i] = "";
                }
                if (EDAuditoriaVerificacion.Pregunta == null)
                {
                    Validacion[0] = true;
                    ValidacionStr[0] = "No ha digitado la pregunta";
                }
                if (EDAuditoriaVerificacion.Requisito == null)
                {
                    Validacion[1] = true;
                    ValidacionStr[1] = "No ha digitado el requisito";
                }
                var Model = EDAuditoriaVerificacion;
                return Json(new { Model, Estado, Probar, Validacion, ValidacionStr }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Estado, Probar }, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult MostrarEdicionListaVerificacion(string Values)
        {
            bool probar = false;
            string resultado = "No exite el elemento";
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                resultado = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { probar, resultado });
            }
            EDAuditoriaVerificacion EDAuditoriaVerificacion = new EDAuditoriaVerificacion();
            int IdAct = 0;
            bool probarNumero = int.TryParse(Values, out IdAct);
            if (IdAct != 0)
            {
                //consultar base de datos con un bool
                EDAuditoriaVerificacion = LNAuditoria.ConsultaListaver(IdAct);
                if (EDAuditoriaVerificacion != null)
                {
                    probar = true;
                    resultado = "El elemento de la lista de verificación existe, puede continuar con la edición de este elemento";
                    var Model = EDAuditoriaVerificacion;
                    return Json(new { Model, probar, resultado }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult EliminarListaVerificacion(string Values)
        {
            bool probar = false;
            string resultado = "No exite el elemento";
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                resultado = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { probar, resultado });
            }
            EDAuditoriaVerificacion EDAuditoriaActividad = new EDAuditoriaVerificacion();
            int Idver = 0;
            bool probarNumero = int.TryParse(Values, out Idver);
            if (Idver != 0)
            {
                EDAuditoriaActividad = LNAuditoria.ConsultaListaver(Idver);
                if (EDAuditoriaActividad != null)
                {
                    probar = LNAuditoria.EliminarListaVer(EDAuditoriaActividad);
                    resultado = "El elemento se ha eliminado correctamente de la lista de verificación";

                    return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult EditarListaVerificacion(EDAuditoriaVerificacion EDAuditoriaVerificacion)
        {
            bool Probar = false;
            string Estado = "No se pudo completar la edición del elemento de la lista de verificación";
            bool[] Validacion = new bool[10];
            string[] ValidacionStr = new string[10];
            for (int i = 0; i < 10; i++)
            {
                Validacion[i] = false;
                ValidacionStr[i] = "";
            }
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                Estado = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { Estado, Probar, Validacion, ValidacionStr });
            }
            if (ModelState.IsValid && EDAuditoriaVerificacion.Pregunta != null && EDAuditoriaVerificacion.Requisito != null)
            {
                EDAuditoriaVerificacion NuevaEDAuditoriaVerificacion = new EDAuditoriaVerificacion();
                EDAuditoriaVerificacion ConsultaEDAuditoriaVerificacion = new EDAuditoriaVerificacion();
                NuevaEDAuditoriaVerificacion = EDAuditoriaVerificacion;
                if (NuevaEDAuditoriaVerificacion.Fk_Id_Auditoria == 0)
                {
                    Estado = "<li>Verifique que haya suministrado el Id de Auditoria</li>";
                    return Json(new { Estado, Probar });
                }
                if (NuevaEDAuditoriaVerificacion.Pk_Id_Lista_Verificacion == 0)
                {
                    Estado = "<li>Verifique que haya suministrado el Id del Elemento o Puede ser que no exista el elemento</li>";
                    return Json(new { Estado, Probar });
                }
                ConsultaEDAuditoriaVerificacion = LNAuditoria.ConsultaListaver(NuevaEDAuditoriaVerificacion.Pk_Id_Lista_Verificacion);
                NuevaEDAuditoriaVerificacion.FechaCierre = ConsultaEDAuditoriaVerificacion.FechaCierre;
                if (NuevaEDAuditoriaVerificacion.FechaCierre == null)
                {
                    NuevaEDAuditoriaVerificacion.FechaCierre = DateTime.Today;
                }
                bool ProbarGuardado = LNAuditoria.ActualizarListaVer(NuevaEDAuditoriaVerificacion);
                if (ProbarGuardado)
                {
                    Probar = true;
                    return Json(new { Estado, Probar });
                }
                else
                {
                    return Json(new { Estado, Probar });
                }
            }
            else
            {
                for (int i = 0; i < 10; i++)
                {
                    Validacion[i] = false;
                    ValidacionStr[i] = "";
                }
                if (EDAuditoriaVerificacion.Pregunta == null)
                {
                    Validacion[0] = true;
                    ValidacionStr[0] = "No ha digitado la pregunta";
                }
                if (EDAuditoriaVerificacion.Requisito == null)
                {
                    Validacion[1] = true;
                    ValidacionStr[1] = "No ha digitado el requisito";
                }
                var Model = EDAuditoriaVerificacion;
                return Json(new { Model, Estado, Probar, Validacion, ValidacionStr }, JsonRequestBehavior.AllowGet);
            }

        }
        #endregion
        #region Compromiso
        [HttpPost]
        public JsonResult MostrarEdicionCompromiso(string Values)
        {
            bool probar = false;
            string resultado = "No exite el elemento";
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                resultado = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { probar, resultado });
            }
            EDAuditoriaVerificacion EDAuditoriaVerificacion = new EDAuditoriaVerificacion();
            int IdAct = 0;
            bool probarNumero = int.TryParse(Values, out IdAct);
            if (IdAct != 0)
            {
                //consultar base de datos con un bool
                EDAuditoriaVerificacion = LNAuditoria.ConsultaListaver(IdAct);
                if (EDAuditoriaVerificacion != null)
                {
                    probar = true;
                    resultado = "El elemento de la lista de compromisos existe, puede continuar con la edición de este elemento";
                    var Model = EDAuditoriaVerificacion;
                    string Fecha_s = EDAuditoriaVerificacion.FechaCierre.ToString("dd/MM/yyyy");
                    return Json(new { Model, probar, resultado, Fecha_s }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult EditarCompromiso(EDAuditoriaVerificacion EDAuditoriaVerificacion)
        {
            bool Probar = false;
            string Estado = "No se editó el compromiso, por favor verifique la información suministrada";
            bool[] Validacion = new bool[10];
            string[] ValidacionStr = new string[10];
            for (int i = 0; i < 10; i++)
            {
                Validacion[i] = false;
                ValidacionStr[i] = "";
            }
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                Estado = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { Estado, Probar, Validacion, ValidacionStr });
            }
            if (ModelState.IsValid && EDAuditoriaVerificacion.Compromiso != null && EDAuditoriaVerificacion.Responsable != null)
            {
                EDAuditoriaVerificacion NuevaEDAuditoriaVerificacion = new EDAuditoriaVerificacion();
                EDAuditoriaVerificacion ConsultaEDAuditoriaVerificacion = new EDAuditoriaVerificacion();
                NuevaEDAuditoriaVerificacion = EDAuditoriaVerificacion;
                if (NuevaEDAuditoriaVerificacion.Fk_Id_Auditoria == 0)
                {
                    Estado = "<li>Verifique que haya suministrado el Id de Auditoria</li>";
                    return Json(new { Estado, Probar });
                }
                if (NuevaEDAuditoriaVerificacion.Pk_Id_Lista_Verificacion == 0)
                {
                    Estado = "<li>Verifique que haya suministrado el Id del Elemento o Puede ser que no exista el elemento</li>";
                    return Json(new { Estado, Probar });
                }
                ConsultaEDAuditoriaVerificacion = LNAuditoria.ConsultaListaver(NuevaEDAuditoriaVerificacion.Pk_Id_Lista_Verificacion);
                ConsultaEDAuditoriaVerificacion.Compromiso = NuevaEDAuditoriaVerificacion.Compromiso;
                ConsultaEDAuditoriaVerificacion.Responsable = NuevaEDAuditoriaVerificacion.Responsable;
                ConsultaEDAuditoriaVerificacion.FechaCierre = NuevaEDAuditoriaVerificacion.FechaCierre;
                bool ProbarGuardado = LNAuditoria.ActualizarListaVer(ConsultaEDAuditoriaVerificacion);
                if (ProbarGuardado)
                {
                    Probar = true;
                    return Json(new { Estado, Probar });
                }
                else
                {
                    return Json(new { Estado, Probar });
                }
            }
            else
            {
                for (int i = 0; i < 10; i++)
                {
                    Validacion[i] = false;
                    ValidacionStr[i] = "";
                }
                if (EDAuditoriaVerificacion.Compromiso == null)
                {
                    Validacion[0] = true;
                    ValidacionStr[0] = "No ha digitado el compromiso";
                }
                if (EDAuditoriaVerificacion.Responsable == null)
                {
                    Validacion[1] = true;
                    ValidacionStr[1] = "No ha digitado el nombre del responsable";
                }
                if (EDAuditoriaVerificacion.FechaCierre == DateTime.MinValue)
                {
                    Validacion[2] = true;
                    ValidacionStr[2] = "No ha digitado la fecha de cierre";
                }
                var Model = EDAuditoriaVerificacion;
                return Json(new { Model, Estado, Probar, Validacion, ValidacionStr }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
        #region PlanAccion
        [HttpPost]
        public ActionResult AgregarPlanAccion(EDActividadAuditoria EDActividadAuditoria)
        {
            bool Probar = false;
            string Estado = "No se guardó el plan de acción, por favor revise la información suministrada";
            bool[] Validacion = new bool[10];
            string[] ValidacionStr = new string[10];
            for (int i = 0; i < 10; i++)
            {
                Validacion[i] = false;
                ValidacionStr[i] = "";
            }
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                Estado = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { Estado, Probar, Validacion, ValidacionStr });
            }
            if (ModelState.IsValid && EDActividadAuditoria.Actividad != null && EDActividadAuditoria.Responsable != null)
            {
                EDActividadAuditoria NuevaEDActividad = new EDActividadAuditoria();
                NuevaEDActividad = EDActividadAuditoria;

                if (NuevaEDActividad.Fk_Id_Auditoria == 0)
                {
                    Estado = "<li>Verifique que haya suministrado el Id de Auditoria</li>";
                    return Json(new { Estado, Probar });
                }
                DateTime date = EDActividadAuditoria.FechaFinalizacion;
                NuevaEDActividad.FechaFinalizacion = date;
                bool ProbarGuardado = LNAuditoria.GuardarActividadPlan(NuevaEDActividad);
                if (ProbarGuardado)
                {
                    Probar = true;
                    return Json(new { Estado, Probar });
                }
            }
            else
            {
                for (int i = 0; i < 10; i++)
                {
                    Validacion[i] = false;
                    ValidacionStr[i] = "";
                }
                if (EDActividadAuditoria.Actividad == null)
                {
                    Validacion[0] = true;
                    ValidacionStr[0] = "No ha digitado la actividad";
                }
                if (EDActividadAuditoria.Responsable == null)
                {
                    Validacion[1] = true;
                    ValidacionStr[1] = "No ha digitado el nombre del responsable";
                }
                if (EDActividadAuditoria.FechaFinalizacion == DateTime.MinValue)
                {
                    Validacion[2] = true;
                    ValidacionStr[2] = "No ha digitado la fecha de finalización";
                }
                var Model = EDActividadAuditoria;
                return Json(new { Model, Estado, Probar, Validacion, ValidacionStr }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Estado, Probar }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult MostrarEdicionPlan(string Values)
        {
            bool probar = false;
            string resultado = "No exite el elemento";
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                resultado = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { probar, resultado });
            }
            EDActividadAuditoria EDAuditoriaActividad = new EDActividadAuditoria();
            int IdAct = 0;
            bool probarNumero = int.TryParse(Values, out IdAct);
            if (IdAct != 0)
            {
                //consultar base de datos con un bool
                EDAuditoriaActividad = LNAuditoria.ConsultaAuditoriaActividadPlan(IdAct);
                if (EDAuditoriaActividad != null)
                {
                    probar = true;
                    resultado = "La actividad existe, puede continuar con la edición de este elemento";
                    string Fecha_s = EDAuditoriaActividad.FechaFinalizacion.ToString("dd/MM/yyyy");
                    var Model = EDAuditoriaActividad;
                    return Json(new { Model, probar, resultado, Fecha_s }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult EditarPlan(EDActividadAuditoria EDActividadAuditoria)
        {
            bool Probar = false;
            string Estado = "No se editó el plan de acción, por favor revise la información suministrada";
            bool[] Validacion = new bool[10];
            string[] ValidacionStr = new string[10];
            for (int i = 0; i < 10; i++)
            {
                Validacion[i] = false;
                ValidacionStr[i] = "";
            }
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                Estado = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { Estado, Probar, Validacion, ValidacionStr });
            }
            if (ModelState.IsValid && EDActividadAuditoria.Actividad != null && EDActividadAuditoria.Responsable != null)
            {
                EDActividadAuditoria NuevaEDActividad = new EDActividadAuditoria();
                NuevaEDActividad = EDActividadAuditoria;
                if (NuevaEDActividad.Fk_Id_Auditoria == 0)
                {
                    Estado = "<li>Verifique que haya suministrado el Id de Auditoria</li>";
                    return Json(new { Estado, Probar });
                }
                if (NuevaEDActividad.Pk_Id_Actividad == 0)
                {
                    Estado = "<li>Verifique que haya suministrado el Id de la actividad Puede ser que no exista</li>";
                    return Json(new { Estado, Probar });
                }
                DateTime date = EDActividadAuditoria.FechaFinalizacion;
                NuevaEDActividad.FechaFinalizacion = date;
                bool ProbarGuardado = LNAuditoria.ActualizarActividadPlan(NuevaEDActividad);
                if (ProbarGuardado)
                {
                    Probar = true;
                    return Json(new { Estado, Probar });
                }
                else
                {
                    return Json(new { Estado, Probar });
                }
            }
            else
            {
                for (int i = 0; i < 10; i++)
                {
                    Validacion[i] = false;
                    ValidacionStr[i] = "";
                }
                if (EDActividadAuditoria.Actividad == null)
                {
                    Validacion[0] = true;
                    ValidacionStr[0] = "No ha digitado la actividad";
                }
                if (EDActividadAuditoria.Responsable == null)
                {
                    Validacion[1] = true;
                    ValidacionStr[1] = "No ha digitado el nombre del responsable";
                }
                if (EDActividadAuditoria.FechaFinalizacion == DateTime.MinValue)
                {
                    Validacion[2] = true;
                    ValidacionStr[2] = "No ha digitado la fecha de finalización";
                }
                var Model = EDActividadAuditoria;
                return Json(new { Model, Estado, Probar, Validacion, ValidacionStr }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
        #region Informes
        [HttpPost]
        public ActionResult ActualizarConclusiones(AuditoriaInforme AuditoriaInforme)
        {
            bool Probar = false;
            string Estado = "No se guardaron las conclusiones del informe de auditoría, por favor revise la información suministrada";
            bool[] Validacion = new bool[10];
            string[] ValidacionStr = new string[10];
            for (int i = 0; i < 10; i++)
            {
                Validacion[i] = false;
                ValidacionStr[i] = "";
            }
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                Estado = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { Estado, Probar, Validacion, ValidacionStr });
            }
            AuditoriaInforme.FechaRealizacion = DateTime.Today.ToString();
            ModelState.Remove("FirmaScrImageRes");
            ModelState.Remove("FirmaScrImageAuditor");
            if (ModelState.IsValid)
            {
                EDAuditoriaInforme NuevoConclusionesAnterior = LNAuditoria.ConsultaConclusiones(AuditoriaInforme.Pk_Id_Auditoria);
                string FilePresAnterior = null;
                string FileResAnterior = null;

                string RutaPresAnterior = null;
                string RutaResAnterior = null;

                if (NuevoConclusionesAnterior.NombreArchivoAuditor != null && NuevoConclusionesAnterior.RutaArchivoAuditor != null)
                {
                    FilePresAnterior = NuevoConclusionesAnterior.NombreArchivoAuditor;
                    RutaPresAnterior = NuevoConclusionesAnterior.RutaArchivoAuditor;
                }
                if (NuevoConclusionesAnterior.NombreArchivoRes != null && NuevoConclusionesAnterior.RutaArchivoRes != null)
                {
                    FileResAnterior = NuevoConclusionesAnterior.NombreArchivoRes;
                    RutaResAnterior = NuevoConclusionesAnterior.RutaArchivoRes;
                }
                AuditoriaInforme NuevoInforme = new AuditoriaInforme();
                NuevoInforme = AuditoriaInforme;

                if (NuevoInforme.FirmaScrImageAuditor != "")
                {
                    try
                    {
                        string Nombre = "FAud" + RandomString(9) + ".png";
                        string b64 = NuevoInforme.FirmaScrImageAuditor;
                        b64 = b64.Replace("data:image/png;base64,", "");
                        Image Imagen = Base64ToImage(b64);
                        string outputPath = (Server.MapPath(Path.Combine(RutaFirmasAud, Nombre)));
                        CrearCarpeta(RutaFirmasAud);
                        Imagen.Save(outputPath, ImageFormat.Png);
                        NuevoInforme.NombreArchivoAuditor = Nombre;
                        NuevoInforme.RutaArchivoAuditor = RutaFirmasAud;
                        //Eliminar Firma anterior
                        try
                        {
                            if (NuevoConclusionesAnterior.NombreArchivoAuditor != null && NuevoConclusionesAnterior.RutaArchivoAuditor != null)
                            {
                                string fileName = Server.MapPath(Path.Combine(RutaPresAnterior, FilePresAnterior));
                                if (fileName != null || fileName != string.Empty)
                                {
                                    if (System.IO.File.Exists(fileName))
                                    {
                                        System.IO.File.Delete(fileName);
                                    }
                                }
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }
                    catch (Exception)
                    {
                        NuevoInforme.NombreArchivoAuditor = null;
                        NuevoInforme.RutaArchivoAuditor = null;
                    }
                }
                if (NuevoInforme.FirmaScrImageRes != "")
                {
                    try
                    {
                        string Nombre = "Fres" + RandomString(9) + ".png";
                        string b64 = NuevoInforme.FirmaScrImageRes;
                        b64 = b64.Replace("data:image/png;base64,", "");
                        Image Imagen = Base64ToImage(b64);
                        string outputPath = (Server.MapPath(Path.Combine(RutaFirmasAud, Nombre)));
                        CrearCarpeta(RutaFirmasAud);
                        Imagen.Save(outputPath, ImageFormat.Png);
                        NuevoInforme.NombreArchivoRes = Nombre;
                        NuevoInforme.RutaArchivoRes = RutaFirmasAud;
                        //Eliminar Firma anterior
                        try
                        {
                            if (NuevoConclusionesAnterior.NombreArchivoRes != null && NuevoConclusionesAnterior.RutaArchivoRes != null)
                            {
                                string fileName = Server.MapPath(Path.Combine(RutaResAnterior, FileResAnterior));
                                if (fileName != null || fileName != string.Empty)
                                {
                                    if (System.IO.File.Exists(fileName))
                                    {
                                        System.IO.File.Delete(fileName);
                                    }
                                }
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }
                    catch (Exception)
                    {
                        NuevoInforme.NombreArchivoRes = null;
                        NuevoInforme.RutaArchivoRes = null;
                    }

                }
                EDAuditoriaInforme EDAuditoriaInforme = new EDAuditoriaInforme();
                EDAuditoriaInforme.Pk_Id_Auditoria = NuevoInforme.Pk_Id_Auditoria;
                EDAuditoriaInforme.Conclusiones = NuevoInforme.Conclusiones;
                EDAuditoriaInforme.NombreArchivoRes = NuevoInforme.NombreArchivoRes;
                EDAuditoriaInforme.RutaArchivoRes = NuevoInforme.RutaArchivoRes;
                EDAuditoriaInforme.NombreArchivoAuditor = NuevoInforme.NombreArchivoAuditor;
                EDAuditoriaInforme.RutaArchivoAuditor = NuevoInforme.RutaArchivoAuditor;
                bool ProbarGuardado = LNAuditoria.ActualizarConclusiones(EDAuditoriaInforme);
                if (ProbarGuardado)
                {
                    Probar = true;
                    return Json(new { Estado, Probar });
                }
            }
            else
            {
                if (AuditoriaInforme.Conclusiones == null)
                {
                    Validacion[0] = true;
                    ValidacionStr[0] = "No ha digitado las conclusiones";
                }
                var Model = AuditoriaInforme;
                return Json(new { Model, Estado, Probar, Validacion, ValidacionStr }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Estado, Probar }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult InformeAuditoria(string IdAuditoria)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }
            EDAuditoriaInforme EDAuditoriaInforme = new EDAuditoriaInforme();
            int IdAuditoriaInt = 0;
            if (int.TryParse(IdAuditoria, out IdAuditoriaInt))
            {
                EDAuditoriaInforme = LNAuditoria.ConsultaInforme(IdAuditoriaInt, usuarioActual.NitEmpresa);
            }
            if (IdAuditoriaInt == 0)
            {
                return HttpNotFound();
            }
            ViewBag.IdPrograma = EDAuditoriaInforme.Informe_EDAuditoria.Fk_Id_Programa;
            ViewBag.IdAuditoria = IdAuditoria;
            ViewBag.Proceso = EDAuditoriaInforme.Informe_EDAuditoria.NombreProceso1;
            ViewBag.Periodo = EDAuditoriaInforme.Informe_EDAuditoria.Periodo;
            ViewBag.SrcAud = "";
            ViewBag.SrcRes = "";
            if (EDAuditoriaInforme.RutaArchivoAuditor != null && EDAuditoriaInforme.NombreArchivoAuditor != null)
            {
                string fileName = Server.MapPath(Path.Combine(EDAuditoriaInforme.RutaArchivoAuditor, EDAuditoriaInforme.NombreArchivoAuditor));
                if (System.IO.File.Exists(fileName))
                {
                    string ImagenBase64 = UrlToBase64(fileName);
                    ImagenBase64 = "data:image/png;base64," + ImagenBase64;
                    ViewBag.SrcAud = ImagenBase64;
                }
            }
            if (EDAuditoriaInforme.RutaArchivoRes != null && EDAuditoriaInforme.NombreArchivoRes != null)
            {
                string fileName = Server.MapPath(Path.Combine(EDAuditoriaInforme.RutaArchivoRes, EDAuditoriaInforme.NombreArchivoRes));
                if (System.IO.File.Exists(fileName))
                {
                    string ImagenBase64 = UrlToBase64(fileName);
                    ImagenBase64 = "data:image/png;base64," + ImagenBase64;
                    ViewBag.SrcRes = ImagenBase64;
                }
            }
            try
            {
                ViewBag.FechaPlaneada = EDAuditoriaInforme.Informe_EDAuditoria.FechaRealizacion.ToString("dd/MM/yyyy");
            }
            catch (Exception)
            {
                ViewBag.FechaPlaneada = EDAuditoriaInforme.Informe_EDAuditoria.FechaRealizacion;
            }
            List<EDSede> ListaSedes = LNEmpresa.ObtenerSedesPorNit(usuarioActual.NitEmpresa);
            EDSede SedeInforme = new EDSede();
            string SedeNombre = "";
            try
            {
                SedeInforme = ListaSedes.Where(s => s.IdSede == EDAuditoriaInforme.Informe_EDAuditoriaPrograma.Fk_Id_Sede).FirstOrDefault();
                SedeNombre = SedeInforme.NombreSede;
                EDAuditoriaInforme.Informe_EDAuditoriaPrograma.SedeAuditoria = SedeNombre;
            }
            catch (Exception)
            {
            }
            return View(EDAuditoriaInforme);
        }
        [AllowAnonymous]
        public ActionResult AuditoriaPDF(string IdAuditoria, string NitEmpresa)
        {
            EDAuditoriaInforme EDAuditoriaInforme = new EDAuditoriaInforme();
            int IdAuditoriaInt = 0;
            if (int.TryParse(IdAuditoria, out IdAuditoriaInt))
            {
                EDAuditoriaInforme = LNAuditoria.ConsultaInforme(IdAuditoriaInt, NitEmpresa);
                EDAuditoriaInforme.NitEmpresa = NitEmpresa;
                List<EDSede> ListaSedes = LNEmpresa.ObtenerSedesPorNit(NitEmpresa);
                EDSede SedeInforme = new EDSede();
                string SedeNombre = "";
                try
                {
                    SedeInforme = ListaSedes.Where(s => s.IdSede == EDAuditoriaInforme.Informe_EDAuditoriaPrograma.Fk_Id_Sede).FirstOrDefault();
                    SedeNombre = SedeInforme.NombreSede;
                    EDAuditoriaInforme.Informe_EDAuditoriaPrograma.SedeAuditoria = SedeNombre;
                }
                catch (Exception)
                {

                }
                EDAuditoriaInforme.RutaPres = "";
                EDAuditoriaInforme.RutaRes = "";

                if (EDAuditoriaInforme.RutaArchivoAuditor != null && EDAuditoriaInforme.NombreArchivoAuditor != null)
                {
                    string fileName = Server.MapPath(Path.Combine(EDAuditoriaInforme.RutaArchivoAuditor, EDAuditoriaInforme.NombreArchivoAuditor));
                    if (System.IO.File.Exists(fileName))
                    {
                        string ImagenBase64 = UrlToBase64(fileName);
                        ImagenBase64 = "data:image/png;base64," + ImagenBase64;
                        ViewBag.SrcAud = ImagenBase64;
                    }
                }
                if (EDAuditoriaInforme.RutaArchivoRes != null && EDAuditoriaInforme.NombreArchivoRes != null)
                {
                    string fileName = Server.MapPath(Path.Combine(EDAuditoriaInforme.RutaArchivoRes, EDAuditoriaInforme.NombreArchivoRes));
                    if (System.IO.File.Exists(fileName))
                    {
                        string ImagenBase64 = UrlToBase64(fileName);
                        ImagenBase64 = "data:image/png;base64," + ImagenBase64;
                        ViewBag.SrcRes = ImagenBase64;
                    }
                }
                ViewBag.Compromiso = false;
                foreach (var item in EDAuditoriaInforme.ListaVerficiacionInforme)
                {
                    if (item.Tipo_Hallazgo == "No Conformidad")
                    {
                        ViewBag.Compromiso = true;
                    }
                }
            }
            if (IdAuditoriaInt == 0)
            {
                return HttpNotFound();
            }
            return View(EDAuditoriaInforme);
        }
        [AllowAnonymous]
        public ActionResult PlanPDF(string IdAuditoria, string NitEmpresa)
        {
            EDAuditoriaInforme EDAuditoriaInforme = new EDAuditoriaInforme();
            int IdAuditoriaInt = 0;
            if (int.TryParse(IdAuditoria, out IdAuditoriaInt))
            {
                EDAuditoriaInforme = LNAuditoria.ConsultaInforme(IdAuditoriaInt, NitEmpresa);
                EDAuditoriaInforme.NitEmpresa = NitEmpresa;
                List<EDSede> ListaSedes = LNEmpresa.ObtenerSedesPorNit(NitEmpresa);
                EDSede SedeInforme = new EDSede();
                string SedeNombre = "";
                try
                {
                    SedeInforme = ListaSedes.Where(s => s.IdSede == EDAuditoriaInforme.Informe_EDAuditoriaPrograma.Fk_Id_Sede).FirstOrDefault();
                    SedeNombre = SedeInforme.NombreSede;
                    EDAuditoriaInforme.Informe_EDAuditoriaPrograma.SedeAuditoria = SedeNombre;
                }
                catch (Exception)
                {

                }
                EDAuditoriaInforme.RutaPres = "";
                EDAuditoriaInforme.RutaRes = "";
                if (EDAuditoriaInforme.Informe_EDAuditoriaPrograma.RutaArchivoPres != null && EDAuditoriaInforme.Informe_EDAuditoriaPrograma.NombreArchivoCopasst != null)
                {
                    string fileName = Server.MapPath(Path.Combine(EDAuditoriaInforme.Informe_EDAuditoriaPrograma.RutaArchivoPres, EDAuditoriaInforme.Informe_EDAuditoriaPrograma.NombreArchivoCopasst));
                    if (System.IO.File.Exists(fileName))
                    {
                        EDAuditoriaInforme.RutaPres = fileName;
                    }
                }
                if (EDAuditoriaInforme.Informe_EDAuditoriaPrograma.RutaArchivoRes != null && EDAuditoriaInforme.Informe_EDAuditoriaPrograma.NombreArchivoRes != null)
                {
                    string fileName = Server.MapPath(Path.Combine(EDAuditoriaInforme.Informe_EDAuditoriaPrograma.RutaArchivoRes, EDAuditoriaInforme.Informe_EDAuditoriaPrograma.NombreArchivoRes));
                    if (System.IO.File.Exists(fileName))
                    {
                        EDAuditoriaInforme.RutaRes = fileName;
                    }
                }
            }
            if (IdAuditoriaInt == 0)
            {
                return HttpNotFound();
            }
            return View(EDAuditoriaInforme);
        }
        [AllowAnonymous]
        public ActionResult ListaPDF(string IdAuditoria, string NitEmpresa)
        {
            EDAuditoriaInforme EDAuditoriaInforme = new EDAuditoriaInforme();
            int IdAuditoriaInt = 0;
            if (int.TryParse(IdAuditoria, out IdAuditoriaInt))
            {
                EDAuditoriaInforme = LNAuditoria.ConsultaInforme(IdAuditoriaInt, NitEmpresa);
                EDAuditoriaInforme.NitEmpresa = NitEmpresa;
                List<EDSede> ListaSedes = LNEmpresa.ObtenerSedesPorNit(NitEmpresa);
                EDSede SedeInforme = new EDSede();
                string SedeNombre = "";
                try
                {
                    SedeInforme = ListaSedes.Where(s => s.IdSede == EDAuditoriaInforme.Informe_EDAuditoriaPrograma.Fk_Id_Sede).FirstOrDefault();
                    SedeNombre = SedeInforme.NombreSede;
                    EDAuditoriaInforme.Informe_EDAuditoriaPrograma.SedeAuditoria = SedeNombre;
                }
                catch (Exception)
                {

                }
                EDAuditoriaInforme.RutaPres = "";
                EDAuditoriaInforme.RutaRes = "";
                if (EDAuditoriaInforme.Informe_EDAuditoriaPrograma.RutaArchivoPres != null && EDAuditoriaInforme.Informe_EDAuditoriaPrograma.NombreArchivoCopasst != null)
                {
                    string fileName = Server.MapPath(Path.Combine(EDAuditoriaInforme.Informe_EDAuditoriaPrograma.RutaArchivoPres, EDAuditoriaInforme.Informe_EDAuditoriaPrograma.NombreArchivoCopasst));
                    if (System.IO.File.Exists(fileName))
                    {
                        EDAuditoriaInforme.RutaPres = fileName;
                    }
                }
                if (EDAuditoriaInforme.Informe_EDAuditoriaPrograma.RutaArchivoRes != null && EDAuditoriaInforme.Informe_EDAuditoriaPrograma.NombreArchivoRes != null)
                {
                    string fileName = Server.MapPath(Path.Combine(EDAuditoriaInforme.Informe_EDAuditoriaPrograma.RutaArchivoRes, EDAuditoriaInforme.Informe_EDAuditoriaPrograma.NombreArchivoRes));
                    if (System.IO.File.Exists(fileName))
                    {
                        EDAuditoriaInforme.RutaRes = fileName;
                    }
                }
            }
            if (IdAuditoriaInt == 0)
            {
                return HttpNotFound();
            }
            return View(EDAuditoriaInforme);
        }
        [AllowAnonymous]
        public ActionResult ProgramaPDF(string IdPrograma, string NitEmpresa, int Idempresa)
        {
            EDAuditoriaPrograma EDAuditoriaPrograma = new EDAuditoriaPrograma();
            int IdProgramaInt = 0;
            if (int.TryParse(IdPrograma, out IdProgramaInt))
            {
                EDAuditoriaPrograma = LNAuditoria.ConsultaprogramaEmpresa(IdProgramaInt, Idempresa);
                List<EDSede> ListaSedes = LNEmpresa.ObtenerSedesPorNit(NitEmpresa);
                EDSede SedeInforme = new EDSede();
                string SedeNombre = "";
                try
                {
                    SedeInforme = ListaSedes.Where(s => s.IdSede == EDAuditoriaPrograma.Fk_Id_Sede).FirstOrDefault();
                    SedeNombre = SedeInforme.NombreSede;
                    EDAuditoriaPrograma.SedeAuditoria = SedeNombre;
                }
                catch (Exception)
                {
                }
                ViewBag.RutaPres = "";
                ViewBag.RutaRes = "";
                if (EDAuditoriaPrograma.RutaArchivoPres != null && EDAuditoriaPrograma.NombreArchivoCopasst != null)
                {
                    string fileName = Server.MapPath(Path.Combine(EDAuditoriaPrograma.RutaArchivoPres, EDAuditoriaPrograma.NombreArchivoCopasst));
                    if (System.IO.File.Exists(fileName))
                    {
                        ViewBag.RutaPres = fileName;
                    }
                }
                if (EDAuditoriaPrograma.RutaArchivoRes != null && EDAuditoriaPrograma.NombreArchivoRes != null)
                {
                    string fileName = Server.MapPath(Path.Combine(EDAuditoriaPrograma.RutaArchivoRes, EDAuditoriaPrograma.NombreArchivoRes));
                    if (System.IO.File.Exists(fileName))
                    {
                        ViewBag.RutaRes = fileName;
                    }
                }
            }
            if (IdProgramaInt == 0)
            {
                return HttpNotFound();
            }
            return View(EDAuditoriaPrograma);
        }
        [AllowAnonymous]
        public ActionResult ReporteInforme(string IdAuditoria)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }
            if (IdAuditoria != null)
            {
                //var fullUrl = this.Url.Action("AuditoriaPDF", "Auditoria", new { IdAuditoria = IdAuditoria, NitEmpresa = usuarioActual.NitEmpresa }, this.Request.Url.Scheme);
                //var fullUrl1 = new Uri(this.Url.Action("AuditoriaPDF", "Auditoria", new { IdAuditoria = IdAuditoria, NitEmpresa = usuarioActual.NitEmpresa }, this.Request.Url.Scheme));
                //var clean0 = fullUrl1.GetComponents(UriComponents.AbsoluteUri & ~UriComponents.Port,
                //                   UriFormat.UriEscaped);



                //string SwitchNombreEmpresa = usuarioActual.RazonSocialEmpresa;
                //string SwitchNitEmpresa = usuarioActual.NitEmpresa;
                //string SwitchNombreDocumento = "INFORME DE AUDITORÍA";

                //var uriFooter = new Uri(Url.Action("Footer", "Auditoria", null, Request.Url.Scheme));
                //var clean1 = uriFooter.GetComponents(UriComponents.AbsoluteUri & ~UriComponents.Port,
                //                   UriFormat.UriEscaped);

                //var uriHeader = new Uri(Url.Action("header", "Auditoria", new { NombreEmpresa = SwitchNombreEmpresa, NitEmpresa = SwitchNitEmpresa, NombreInforme = SwitchNombreDocumento }, Request.Url.Scheme));
                //var clean2 = uriHeader.GetComponents(UriComponents.AbsoluteUri & ~UriComponents.Port,
                //                   UriFormat.UriEscaped);

                //string cusomtSwitches = string.Format("--footer-line --print-media-type --allow {0} --footer-html {0} --header-line --print-media-type --allow {1} --header-html {1} --header-spacing 5",
                //clean1, clean2);

                string EncodedRazonSocial = System.Net.WebUtility.UrlEncode(usuarioActual.RazonSocialEmpresa);
                string EncodedNit = System.Net.WebUtility.UrlEncode(usuarioActual.NitEmpresa);
                string EncodedNombreInforme = System.Net.WebUtility.UrlEncode("INFORME DE AUDITORÍA");
                var footurl = "https://alissta.gov.co/Acciones/Footer";
                var headerurl = "https://alissta.gov.co/Acciones/Header?NombreEmpresa=" + EncodedRazonSocial + "&NitEmpresa=" + EncodedNit + "&NombreInforme=" + EncodedNombreInforme;
                string cusomtSwitches = string.Format("--footer-line --print-media-type --allow {0} --footer-html {0} --header-line --print-media-type --allow {1} --header-html {1} --header-spacing 5",
                footurl, headerurl);
                var ReporteUrl = "https://alissta.gov.co/Auditoria/AuditoriaPDF?IdAuditoria=" + IdAuditoria.ToString() + "&NitEmpresa=" + EncodedNit ;

                return new Rotativa.UrlAsPdf(ReporteUrl) { FileName = "Alissta_InformeAuditoria" + DateTime.Now.ToString().Replace("/", "").Replace(":", "").Replace(" ", "") + ".pdf"
                    ,
                    CustomSwitches = cusomtSwitches
                    ,
                    PageSize = Rotativa.Options.Size.Letter };
            }
            return View();

        }
        [AllowAnonymous]
        public ActionResult ReportePlan(string IdAuditoria)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }
            if (IdAuditoria != null)
            {

                string EncodedRazonSocial = System.Net.WebUtility.UrlEncode(usuarioActual.RazonSocialEmpresa);
                string EncodedNit = System.Net.WebUtility.UrlEncode(usuarioActual.NitEmpresa);
                string EncodedNombreInforme = System.Net.WebUtility.UrlEncode("REPORTE PLAN DE AUDITORIA");

                var footurl = "https://alissta.gov.co/Acciones/Footer";
                var headerurl = "https://alissta.gov.co/Acciones/Header?NombreEmpresa=" + EncodedRazonSocial + "&NitEmpresa=" + EncodedNit + "&NombreInforme=" + EncodedNombreInforme;
                string cusomtSwitches = string.Format("--footer-line --print-media-type --allow {0} --footer-html {0} --header-line --print-media-type --allow {1} --header-html {1} --header-spacing 5",
                footurl, headerurl);
                var ReporteUrl = "https://alissta.gov.co/Auditoria/PlanPDF?IdAuditoria=" + IdAuditoria.ToString() + "&NitEmpresa=" + EncodedNit;

                return new Rotativa.UrlAsPdf(ReporteUrl) { FileName = "Alissta_ReportePlanAuditoria" + DateTime.Now.ToString().Replace("/", "").Replace(":", "").Replace(" ", "") + ".pdf"
                    ,
                    CustomSwitches = cusomtSwitches
                    , PageSize = Rotativa.Options.Size.Letter };
            }
            return View();
        }
        [AllowAnonymous]
        public ActionResult ReporteLista(string IdAuditoria)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }
            if (IdAuditoria != null)
            {

                string EncodedRazonSocial = System.Net.WebUtility.UrlEncode(usuarioActual.RazonSocialEmpresa);
                string EncodedNit = System.Net.WebUtility.UrlEncode(usuarioActual.NitEmpresa);
                string EncodedNombreInforme = System.Net.WebUtility.UrlEncode("REPORTE LISTA DE VERIFICACIÓN");

                var footurl = "https://alissta.gov.co/Acciones/Footer";
                var headerurl = "https://alissta.gov.co/Acciones/Header?NombreEmpresa=" + EncodedRazonSocial + "&NitEmpresa=" + EncodedNit + "&NombreInforme=" + EncodedNombreInforme;
                string cusomtSwitches = string.Format("--footer-line --print-media-type --allow {0} --footer-html {0} --header-line --print-media-type --allow {1} --header-html {1} --header-spacing 5",
                footurl, headerurl);
                var ReporteUrl = "https://alissta.gov.co/Auditoria/ListaPDF?IdAuditoria=" + IdAuditoria.ToString() + "&NitEmpresa=" + EncodedNit;

                return new Rotativa.UrlAsPdf(ReporteUrl) { FileName = "ReporteListaVerificacion" + DateTime.Now.ToString().Replace("/", "").Replace(":", "").Replace(" ", "") + ".pdf"
                    ,
                    CustomSwitches = cusomtSwitches
                    , PageSize = Rotativa.Options.Size.Letter };
            }
            return View();
        }
        [AllowAnonymous]
        public ActionResult ReportePrograma(string IdPrograma)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }
            if (IdPrograma != null)
            {

                string EncodedRazonSocial = System.Net.WebUtility.UrlEncode(usuarioActual.RazonSocialEmpresa);
                string EncodedNit = System.Net.WebUtility.UrlEncode(usuarioActual.NitEmpresa);
                string EncodedNombreInforme = System.Net.WebUtility.UrlEncode("REPORTE PROGRAMA DE AUDITORIAS");

                var footurl = "https://alissta.gov.co/Acciones/Footer";
                var headerurl = "https://alissta.gov.co/Acciones/Header?NombreEmpresa=" + EncodedRazonSocial + "&NitEmpresa=" + EncodedNit + "&NombreInforme=" + EncodedNombreInforme;
                string cusomtSwitches = string.Format("--footer-line --print-media-type --allow {0} --footer-html {0} --header-line --print-media-type --allow {1} --header-html {1} --header-spacing 5",
                footurl, headerurl);
                var ReporteUrl = "https://alissta.gov.co/Auditoria/ProgramaPDF?IdPrograma=" + IdPrograma.ToString() + "&NitEmpresa=" + EncodedNit + "&IdEmpresa=" + usuarioActual.IdEmpresa.ToString();

                return new Rotativa.UrlAsPdf(ReporteUrl) { FileName = "Alissta_ProgramaAuditoria" + DateTime.Now.ToString().Replace("/", "").Replace(":", "").Replace(" ", "") + ".pdf"
                    ,
                    CustomSwitches = cusomtSwitches
                    , PageSize = Rotativa.Options.Size.Letter };
            }
            return View();
        }
        [AllowAnonymous]
        public ActionResult Footer()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult header(string NombreEmpresa, string NitEmpresa, string NombreInforme)
        {
            ViewBag.NombreEmpresa = NombreEmpresa;
            ViewBag.NitEmpresa = NitEmpresa;
            ViewBag.NombreInforme = NombreInforme;
            return View();
        }
        #endregion
        #region Firmas
        [HttpPost]
        public ActionResult UploadImgPres()
        {
            bool probar = false;
            string resultado = "";
            string ImgScr = "";
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                resultado = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { probar, resultado, ImgScr });
            }
            if (Request.Files.Count > 0)
            {
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

                            //Metodo para obtener una imagen base64 de la firma
                            if (file != null)
                            {
                                try
                                {
                                    string Imagen = "";
                                    Image bit = System.Drawing.Image.FromStream(file.InputStream);
                                    using (var newImage = ScaleImage(bit, 400, 500))
                                    {
                                        //newImage.Save(@"c:\test.png", ImageFormat.Png);
                                        Imagen = "data:image/png;base64," + ImageToBase64String(newImage, ImageFormat.Png);
                                    }


                                    //Imagen = "data:image/png;base64," + ImageToBase64String(bit, ImageFormat.Png);
                                    ImgScr = Imagen;
                                }
                                catch (Exception)
                                {
                                    ImgScr = "";
                                }
                            }
                        }
                        else
                        {
                            probar = false;
                            resultado = "El archivo que se intento subir no es una imagen o no es un archivo soportado, por favor intente de nuevo con otra imagen";
                            return Json(new { probar, resultado, ImgScr });
                        }
                    }
                    probar = true;
                    var jsonResult = Json(new { probar, resultado, ImgScr }, JsonRequestBehavior.DenyGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;

                }
                catch (Exception)
                {
                    resultado = "No se pudo completar la operación, el tamaño del archivo probablemente es mayor a 4 MB ";
                    return Json(new { probar, resultado, ImgScr });
                }
            }
            else
            {
                resultado = "No ha seleccionado un archivo, antes de adjuntar asegurese que exista un archivo";
                return Json(new { probar, resultado, ImgScr });
            }
        }
        [HttpPost]
        public ActionResult UploadImgRes()
        {
            bool probar = false;
            string resultado = "";
            string ImgScr = "";
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                resultado = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { probar, resultado, ImgScr });
            }
            
            if (Request.Files.Count > 0)
            {
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

                            //Metodo para obtener una imagen base64 de la firma
                            if (file != null)
                            {
                                try
                                {
                                    string Imagen = "";
                                    Image bit = System.Drawing.Image.FromStream(file.InputStream);
                                    using (var newImage = ScaleImage(bit, 400, 500))
                                    {
                                        Imagen = "data:image/png;base64," + ImageToBase64String(newImage, ImageFormat.Png);
                                    }
                                    ImgScr = Imagen;
                                }
                                catch (Exception)
                                {
                                    ImgScr = "";
                                }
                            }
                        }
                        else
                        {
                            probar = false;
                            resultado = "El archivo que se intento subir no es una imagen o no es un archivo soportado, por favor intente de nuevo con otra imagen";
                            return Json(new { probar, resultado, ImgScr });
                        }
                    }
                    probar = true;


                    var jsonResult = Json(new { probar, resultado, ImgScr } , JsonRequestBehavior.DenyGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;


                    //return Json(new { probar, resultado, ImgScr, JsonRequestBehavior.AllowGet, MaxJsonLength = 50000000 });
                }
                catch (Exception)
                {
                    resultado = "No se pudo completar la operación, el tamaño del archivo probablemente es mayor a 4 MB ";
                    return Json(new { probar, resultado, ImgScr });
                }
            }
            else
            {
                resultado = "No ha seleccionado un archivo, antes de adjuntar asegurese que exista un archivo";
                return Json(new { probar, resultado, ImgScr });
            }
        }
        #endregion
        #region Funciones
        private void CrearCarpeta(string path)
        {
            bool existeCarpeta = Directory.Exists(Server.MapPath(path));
            if (!existeCarpeta)
            {
                Directory.CreateDirectory(Server.MapPath(path));
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
        [HttpPost]
        public JsonResult BuscarPersonaDocumento(string documento)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
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
                            var afiliado = respuesta.Where(a => a.Estado == "Activo").FirstOrDefault();
                            if (afiliado == null)
                            { }
                            else
                            {
                                if (nit == afiliado.IdEmpresa)
                                {
                                    resultado[0] = afiliado.Nombre1 + " " + afiliado.Nombre2 + " " + afiliado.Apellido1 + " " + afiliado.Apellido2;
                                    resultado[1] = afiliado.Ocupacion;
                                    probar = true;
                                    return Json(new { resultado, probar }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    break;
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
                                var afiliado = respuesta.Where(a => a.Estado == "Activo").FirstOrDefault();
                                if (afiliado == null)
                                {

                                }
                                else
                                {
                                    if (nit == afiliado.IdEmpresa)
                                    {
                                        resultado[0] = afiliado.Nombre1 + " " + afiliado.Nombre2 + " " + afiliado.Apellido1 + " " + afiliado.Apellido2;
                                        resultado[1] = afiliado.Ocupacion;
                                        probar = true;
                                        return Json(new { resultado, probar }, JsonRequestBehavior.AllowGet);
                                    }
                                }
                            }

                        }
                    }
                    return Json(new { resultado, probar }, JsonRequestBehavior.AllowGet);
                }
                else
                    return Json(new { resultado, probar }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { resultado, probar }, JsonRequestBehavior.AllowGet);
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



        [HttpGet]
        public ActionResult PruebaAuditoria()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }
        [HttpPost]
        public ActionResult PruebaAuditoria(FormCollection frm)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }
            string cusomtSwitches = string.Format("--footer-line --print-media-type --allow {0} --footer-html {0} --header-line --print-media-type --allow {1} --header-html {1} --header-spacing 5",
                 frm["FOOTER"].ToString(), frm["HEADER"].ToString());
            return new Rotativa.UrlAsPdf(frm["URLPRINCIPAL"].ToString())
            {
                FileName = "ReporteProgramaAuditoria" + DateTime.Now.ToString().Replace("/", "").Replace(":", "").Replace(" ", "") + ".pdf"
                ,
                CustomSwitches = cusomtSwitches
                ,
                PageSize = Rotativa.Options.Size.Letter
            };
        }


        #endregion
    }
}