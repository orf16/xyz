using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using SG_SST.Controllers.Base;
using System.Configuration;
using RestSharp;
using System.Net;
using iTextSharp.text;
using ClosedXML.Excel;
using SG_SST.ServiceRequest;
using SG_SST.Models.Participacion;
using SG_SST.EntidadesDominio.Participacion;

namespace SG_SST.Controllers.Participacion
{
    public class ConsultaController : BaseController
    {
        static EDConsultarConsultasSST EDconsultar;
        private static string RutaArchivosBDTemp = "~/Content/EvidenciasConsultasSST/EvidenciasTempConsultasSST/";
        private static string RutaArchivosBD = "~/Content/EvidenciasConsultasSST/EvidenciasConsultasSST/";
        string UrlServicioParticipacion = ConfigurationManager.AppSettings["UrlServicioParticipacion"];
        string GrabarConsulta = ConfigurationManager.AppSettings["GrabarConsulta"];
        string ObtenerConsultasSST = ConfigurationManager.AppSettings["ObtenerConsultasSST"];
        string ObtenerUnaConsultaSST = ConfigurationManager.AppSettings["ObtenerUnaConsultaSST"];
        string EditarGestionConsulta = ConfigurationManager.AppSettings["EditarGestionConsulta"];
        string EliminarEvidenciaConsultaSST = ConfigurationManager.AppSettings["EliminarEvidenciaConsultaSST"];
        string ConsultarConsultasSST = ConfigurationManager.AppSettings["ConsultarConsultasSST"];
        string DescargarConsultaSST = ConfigurationManager.AppSettings["DescargarConsultaSST"];
        string DescargarConsultaSSTSinFiltro = ConfigurationManager.AppSettings["DescargarConsultaSSTSinFiltro"];
        // GET: Consulta
        public ActionResult Index()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            return View();
        }


        public ActionResult GrabarConsultaSST(ConsultaVM consulta)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            var consultasst = new EDConsultaSST()
            {
                PkConsultaED = consulta.PkConsultaVM,
                FkEmpresaED = usuarioActual.IdEmpresa,
                TipoConsultaED=consulta.TipoConsultaVM,
                DescripcionConsultaED=consulta.DescripcionConsultaVM,
                IdUsuarioED=usuarioActual.IdUsuario,
                FechaConsultaED = DateTime.Now,
                FechaRevisionED = new DateTime(1900, 1, 1)
            };
            ServiceClient.EliminarParametros();
            var resultconsultasst = ServiceClient.RealizarPeticionesPostJsonRestFul<EDConsultaSST>(UrlServicioParticipacion, GrabarConsulta, consultasst);
            if (resultconsultasst != null)
            {
                return Json(new { Data = resultconsultasst, Mensaje = "OK" }, JsonRequestBehavior.AllowGet);

            }
            else 
            {
                return Json(new { Data = string.Empty, Mensaje = "ERROR" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult MostrarConsultasSST()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("idEmpresa", usuarioActual.IdEmpresa);
            var ConsultasSST = ServiceClient.ObtenerArrayJsonRestFul<EDConsultaSST>(UrlServicioParticipacion, ObtenerConsultasSST, RestSharp.Method.GET);
            ViewBag.tipoconsulta = "Seleccione";
            ViewBag.fechaInicio = "";
            ViewBag.fechaFinal = "";
            return View(ConsultasSST);
        }

        [HttpPost]
        public ActionResult MostrarConsultasSST(EDConsultarConsultasSST consultar)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            ServiceClient.EliminarParametros();
            consultar.idEmpresa = usuarioActual.IdEmpresa;
            var ConsultasSST = ServiceClient.RealizarPetiArrayPostJsonRestFul<EDConsultaSST>(UrlServicioParticipacion, ConsultarConsultasSST, consultar);
            EDconsultar = consultar;
            var fechainicSinH = Convert.ToDateTime(consultar.Fecha_ini);
            var fechafinalSinH = Convert.ToDateTime(consultar.Fecha_Fin);
            ViewBag.tipoconsulta = consultar.tipoConsult;
            ViewBag.fechaInicio = fechainicSinH.ToString("dd/MM/yyyy");
            ViewBag.fechaFinal = fechafinalSinH.ToString("dd/MM/yyyy");
            return View(ConsultasSST);
        }

        public ActionResult TrazabilidadConsultaSST(int idConsulta)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("idConsulta", idConsulta);
            var ConsultaSST = ServiceClient.ObtenerObjetoJsonRestFul<EDConsultaSST>(UrlServicioParticipacion, ObtenerUnaConsultaSST, RestSharp.Method.GET);
            return View(ConsultaSST);
        }


        [HttpPost]
        public ActionResult GuardarConsultasSST(EDConsultaTrazabilidad GuardarAdmonCTZB)
        {
            //bool ProbarNumero_fechas = true;
            bool Probar = false;
            string Estado = "No se guardó la gestión de la consulta, por favor revise la información suministrada";
            bool[] Validacion = new bool[2];
            string[] ValidacionStr = new string[2];
            for (int i = 0; i < 2; i++)
            {
                Validacion[i] = false;
                ValidacionStr[i] = "";
            }
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            ModelState.Clear();
            TryValidateModel(GuardarAdmonCTZB);
            if (ModelState.IsValid && GuardarAdmonCTZB.ObservacionesED != null)
            {

                EDConsultaTrazabilidad NuevoAdmonCTZB = new EDConsultaTrazabilidad();
                NuevoAdmonCTZB = GuardarAdmonCTZB;
                //NuevoAdmonCTZB.Fk_Id_Empresa = usuarioActual.IdEmpresa;

                CrearCarpeta(RutaArchivosBD);
                List<string> ArchivosTemporalesEliminar = new List<string>();                
                if (NuevoAdmonCTZB.NombreArchivo1 != null)
                {
                    string PathOrigen = Server.MapPath(Path.Combine(RutaArchivosBDTemp, NuevoAdmonCTZB.NombreArchivo1));
                    if (System.IO.File.Exists(PathOrigen))
                    {
                        try
                        {
                            NuevoAdmonCTZB.Ruta1 = RutaArchivosBD;
                            string pathsave = Server.MapPath(Path.Combine(NuevoAdmonCTZB.Ruta1, NuevoAdmonCTZB.NombreArchivo1));
                            System.IO.File.Move(PathOrigen, pathsave);
                            ArchivosTemporalesEliminar.Add(PathOrigen);
                        }
                        catch (Exception)
                        {
                        }
                    }
                }

                if (NuevoAdmonCTZB.NombreArchivo2 != null)
                {
                    string PathOrigen = Server.MapPath(Path.Combine(RutaArchivosBDTemp, NuevoAdmonCTZB.NombreArchivo2));
                    if (System.IO.File.Exists(PathOrigen))
                    {
                        try
                        {
                            NuevoAdmonCTZB.Ruta2 = RutaArchivosBD;
                            string pathsave = Server.MapPath(Path.Combine(NuevoAdmonCTZB.Ruta2, NuevoAdmonCTZB.NombreArchivo2));
                            System.IO.File.Move(PathOrigen, pathsave);
                            ArchivosTemporalesEliminar.Add(PathOrigen);
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
                if (NuevoAdmonCTZB.NombreArchivo3 != null)
                {
                    string PathOrigen = Server.MapPath(Path.Combine(RutaArchivosBDTemp, NuevoAdmonCTZB.NombreArchivo3));
                    if (System.IO.File.Exists(PathOrigen))
                    {
                        try
                        {
                            NuevoAdmonCTZB.Ruta3 = RutaArchivosBD;
                            string pathsave = Server.MapPath(Path.Combine(NuevoAdmonCTZB.Ruta3, NuevoAdmonCTZB.NombreArchivo3));
                            System.IO.File.Move(PathOrigen, pathsave);
                            ArchivosTemporalesEliminar.Add(PathOrigen);
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
                //var result = NuevoAdmonCTZB;
                bool ProbarGuardado = false;
                ServiceClient.EliminarParametros();
                ProbarGuardado = ServiceClient.PeticionesPostJsonRestFulRespuetaBool<EDConsultaTrazabilidad>(UrlServicioParticipacion, EditarGestionConsulta, NuevoAdmonCTZB);                
                if (ProbarGuardado)
                {

                    Probar = true;
                    return Json(new { Estado, Probar });
                }
            }

            for (int i = 0; i < 2; i++)
            {
                Validacion[i] = false;
                ValidacionStr[i] = "";
            }
            int cont = 0;

            
            bool FechaFabP = false;
           
            foreach (var kvp in ModelState)
            {
                var key = kvp.Key;
                cont = cont + 1;
            }
            int[] ListaErroresSalida = new int[cont];
            bool[] ListaErroresSalidabool = new bool[cont];

            for (int i = 0; i < cont; i++)
            {
                ListaErroresSalida[i] = -1;
                ListaErroresSalidabool[i] = false;
            }
            cont = 0;
            foreach (var kvp in ModelState)
            {
                var key = kvp.Key;                
                if (key == "Fecha_Fab")
                {
                    FechaFabP = true;
                    ListaErroresSalida[cont] = 0;
                    if (GuardarAdmonCTZB.Fecha_Fab == DateTime.MinValue)
                    {
                        ListaErroresSalidabool[cont] = true;
                        ValidacionStr[cont] = "No ha digitado el valor de fecha de revisión";
                    }
                }

                if (key == "ObservacionesED")
                {
                    ListaErroresSalida[cont] = 1;
                    ListaErroresSalidabool[cont] = true;
                }

                cont = cont + 1;
            }
            cont = 0;
            foreach (var kvp in ModelState)
            {
                var value = kvp.Value;
                if (value.Errors.Count > 0)
                {
                    string valorError = value.Errors[0].ErrorMessage.ToString();
                    if (ListaErroresSalidabool[cont])
                    {
                        Validacion[ListaErroresSalida[cont]] = true;
                        ValidacionStr[ListaErroresSalida[cont]] = valorError;
                    }

                }
                cont = cont + 1;
            }
            if (!FechaFabP)
            {
                if (GuardarAdmonCTZB.Fecha_Fab == DateTime.MinValue)
                {
                    Validacion[0] = true;
                    ValidacionStr[0] = "Debe ingresar el valor de fecha de Gestión de la consulta";
                }
            }
            if(GuardarAdmonCTZB.ObservacionesED == null)
            {
                Validacion[1] = true;
                ValidacionStr[1] = "Debe ingresar una observación";
            }
            var Model = GuardarAdmonCTZB;
            return Json(new { Model, Estado, Probar, Validacion, ValidacionStr }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult UploadFiles()
        {
            bool probar = false;
            string resultado = "";
            string[] NombreArchivos = new string[3];
            string[] NombreArchivos_short = new string[3];
            string[] NuevoNombreArchivos = new string[3];
            string[] NuevoNombreArchivos_short = new string[3];
            bool[] display = new bool[3] { false, false, false };
            if (Request.Files.Count > 0)
            {
                string ValImagen1 = string.Empty;
                string ValImagen2 = string.Empty;
                string ValImagen3 = string.Empty;
                string ValImagen1s = string.Empty;
                string ValImagen2s = string.Empty;
                string ValImagen3s = string.Empty;
                try
                {
                    ValImagen1 = Request.Form[0].ToString();
                    ValImagen2 = Request.Form[1].ToString();
                    ValImagen3 = Request.Form[2].ToString();
                    ValImagen1s = Request.Form[3].ToString();
                    ValImagen2s = Request.Form[4].ToString();
                    ValImagen3s = Request.Form[5].ToString();
                }
                catch (Exception)
                {
                    resultado = "";
                    return Json(new { probar, resultado });
                }
                NombreArchivos[0] = ValImagen1;
                NombreArchivos[1] = ValImagen2;
                NombreArchivos[2] = ValImagen3;
                NombreArchivos_short[0] = ValImagen1s;
                NombreArchivos_short[1] = ValImagen2s;
                NombreArchivos_short[2] = ValImagen3s;
                int PosicionVacia = -1;
                for (int i = 0; i < 3; i++)
                {
                    if (NombreArchivos[i] == string.Empty)
                    {
                        PosicionVacia = i;
                    }
                    else
                    {
                        string PathOrigen = Server.MapPath(Path.Combine(RutaArchivosBDTemp, NombreArchivos[i]));
                        if (!System.IO.File.Exists(PathOrigen))
                        {
                            PosicionVacia = i;
                        }
                    }
                }
                if (PosicionVacia == -1)
                {
                    resultado = "El límite de Evidencias que el usuario puede cargar es 3, por favor si desea agregar este archivo primero elimine un archivo ya cargado";
                    return Json(new { probar, resultado });
                }
                try
                {
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
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
                            CrearCarpeta(RutaArchivosBDTemp);
                            string substring = "CTZB_file_" + DateTime.Now.ToString().Replace(" ", "").Replace(".", "").Replace(":", "").Replace("/", "");
                            string ImgFileName = substring + file.FileName;
                            string pathsave = Server.MapPath(Path.Combine(RutaArchivosBDTemp, ImgFileName));
                            file.SaveAs(pathsave);
                            NombreArchivos[PosicionVacia] = ImgFileName;
                            NombreArchivos_short[PosicionVacia] = file.FileName;
                            int IndexNuevo = 0;
                            for (int i1 = 0; i1 < 3; i1++)
                            {
                                if (NombreArchivos[i1] == string.Empty)
                                {
                                    display[i1] = false;
                                }
                                else
                                {
                                    string PathOrigen = Server.MapPath(Path.Combine(RutaArchivosBDTemp, NombreArchivos[i1]));
                                    if (!System.IO.File.Exists(PathOrigen))
                                    {
                                        display[i1] = false;
                                    }
                                    else
                                    {
                                        NuevoNombreArchivos_short[IndexNuevo] = NombreArchivos_short[i1];
                                        NuevoNombreArchivos[IndexNuevo] = NombreArchivos[i1];
                                        try
                                        {
                                            display[IndexNuevo] = true;
                                        }
                                        catch (Exception)
                                        {
                                        }
                                        IndexNuevo = IndexNuevo + 1;
                                    }
                                }
                            }
                        }
                        else
                        {
                            probar = false;
                            resultado = "El archivo que se intento subir no es una imagen o no es un archivo soportado, por favor intente de nuevo con otro archivo";
                            return Json(new { probar, resultado });
                        }
                    }
                    probar = true;
                    return Json(new { probar, resultado, NuevoNombreArchivos, display, NuevoNombreArchivos_short });
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

        public ActionResult UploadFilesEditar()
        {
            bool probar = false;
            string resultado = "";
            string[] NombreArchivos = new string[3];
            string[] NombreArchivos_short = new string[3];
            string[] NuevoNombreArchivos = new string[3];
            string[] NuevoNombreArchivos_short = new string[3];
            bool[] display = new bool[3] { false, false, false };
            if (Request.Files.Count > 0)
            {
                string ValImagen1 = string.Empty;
                string ValImagen2 = string.Empty;
                string ValImagen3 = string.Empty;
                string ValImagen1s = string.Empty;
                string ValImagen2s = string.Empty;
                string ValImagen3s = string.Empty;
                try
                {
                    ValImagen1 = Request.Form[0].ToString();
                    ValImagen2 = Request.Form[1].ToString();
                    ValImagen3 = Request.Form[2].ToString();
                    ValImagen1s = Request.Form[3].ToString();
                    ValImagen2s = Request.Form[4].ToString();
                    ValImagen3s = Request.Form[5].ToString();
                }
                catch (Exception)
                {
                    resultado = "";
                    return Json(new { probar, resultado });
                }
                NombreArchivos[0] = ValImagen1;
                NombreArchivos[1] = ValImagen2;
                NombreArchivos[2] = ValImagen3;
                NombreArchivos_short[0] = ValImagen1s;
                NombreArchivos_short[1] = ValImagen2s;
                NombreArchivos_short[2] = ValImagen3s;
                int PosicionVacia = -1;
                for (int i = 0; i < 3; i++)
                {
                    if (NombreArchivos[i] == string.Empty)
                    {
                        PosicionVacia = i;
                    }
                    else
                    {
                        string PathOrigen = Server.MapPath(Path.Combine(RutaArchivosBD, NombreArchivos[i]));
                        string PathTemp = Server.MapPath(Path.Combine(RutaArchivosBDTemp, NombreArchivos[i]));
                        if (!System.IO.File.Exists(PathOrigen) && !System.IO.File.Exists(PathTemp))
                        {
                            PosicionVacia = i;
                        }
                    }
                }
                if (PosicionVacia == -1)
                {
                    resultado = "El límite de Evidencias que el usuario puede cargar es 3, por favor si desea agregar este archivo primero elimine un archivo ya cargado";
                    return Json(new { probar, resultado });
                }
                try
                {
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
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
                            CrearCarpeta(RutaArchivosBDTemp);
                            string substring = "CTZB_file_" + DateTime.Now.ToString().Replace(" ", "").Replace(".", "").Replace(":", "").Replace("/", "");
                            string ImgFileName = substring + file.FileName;
                            string pathsave = Server.MapPath(Path.Combine(RutaArchivosBDTemp, ImgFileName));
                            file.SaveAs(pathsave);
                            NombreArchivos[PosicionVacia] = ImgFileName;
                            NombreArchivos_short[PosicionVacia] = file.FileName;
                            int IndexNuevo = 0;
                            for (int i1 = 0; i1 < 3; i1++)
                            {
                                if (NombreArchivos[i1] == string.Empty)
                                {
                                    display[i1] = false;
                                }
                                else
                                {
                                    string PathOrigen = Server.MapPath(Path.Combine(RutaArchivosBD, NombreArchivos[i1]));
                                    string PathTemp = Server.MapPath(Path.Combine(RutaArchivosBDTemp, NombreArchivos[i1]));
                                    if (!System.IO.File.Exists(PathOrigen) && !System.IO.File.Exists(PathTemp))
                                    {
                                        display[i1] = false;
                                    }
                                    else
                                    {
                                        NuevoNombreArchivos_short[IndexNuevo] = NombreArchivos_short[i1];
                                        NuevoNombreArchivos[IndexNuevo] = NombreArchivos[i1];
                                        try
                                        {
                                            display[IndexNuevo] = true;
                                        }
                                        catch (Exception)
                                        {
                                        }
                                        IndexNuevo = IndexNuevo + 1;
                                    }
                                }
                            }
                        }
                        else
                        {
                            probar = false;
                            resultado = "El archivo que se intento subir no es una imagen o no es un archivo soportado, por favor intente de nuevo con otro archivo";
                            return Json(new { probar, resultado });
                        }
                    }
                    probar = true;
                    return Json(new { probar, resultado, NuevoNombreArchivos, display, NuevoNombreArchivos_short });
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

        private void CrearCarpeta(string path)
        {
            bool existeCarpeta = Directory.Exists(Server.MapPath(path));
            if (!existeCarpeta)
            {
                Directory.CreateDirectory(Server.MapPath(path));
            }

        }

        [HttpPost]
        public ActionResult EliminarArchivo(string ruta)
        {
            bool probar = true;
            if (ruta != null)
            {
                try
                {
                    string PathOrigen = Server.MapPath(Path.Combine(RutaArchivosBDTemp, ruta));
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

        [HttpPost]
        public ActionResult EliminarArchivoEdit(string ruta, int id, int dato)
        {
            bool probar = true;
            if (ruta != null)
            {
                try
                {
                    ServiceClient.AdicionarParametro("ruta", ruta);
                    ServiceClient.AdicionarParametro("id", id);
                    ServiceClient.AdicionarParametro("dato", dato);
                    bool resultMetodologias = ServiceClient.PeticionesPostJsonRestFulRespuetaBool(UrlServicioParticipacion, EliminarEvidenciaConsultaSST, RestSharp.Method.DELETE);
                    if (resultMetodologias)
                    {
                        string PathOrigen = Server.MapPath(Path.Combine(RutaArchivosBD, ruta));
                        if (System.IO.File.Exists(PathOrigen))
                        {
                            System.IO.File.Delete(PathOrigen);
                        }
                    }
                }
                catch (Exception)
                {
                }
            }
            return Json(new { probar });
        }
        
        private void EliminarArchivos(List<string> ArchivosTemporalesEliminar)
        {
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
        }

        public ActionResult DescargarEvidencia(string nombEvide, string nombReal)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            //{ File.Move(“D:HolaMundo.txt”, “D:AdiosMundo.txt”); }
            var path = Server.MapPath(RutaArchivosBD);
            var file = nombEvide;
            var fullPath = Path.Combine(path, file);
            byte[] fileBytes = System.IO.File.ReadAllBytes(fullPath);
            MemoryStream ms = new MemoryStream(fileBytes, 0, 0, true, true);
            Response.AddHeader("content-disposition", "attachment;filename=" + nombReal + "");
            Response.Buffer = true;
            Response.Clear();
            Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length);
            Response.OutputStream.Flush();
            Response.End();
            if (Path.GetExtension(nombEvide).ToLower() == ".pdf")
            {
                return new FileStreamResult(Response.OutputStream, "application/pdf");
            }
            else
            {
                return new FileStreamResult(Response.OutputStream, "application/vnd.ms-excel");
            }
        }

        public FileResult ExportarExcel(EDConsultarConsultasSST consultar)
        {         
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
            }
            consultar = EDconsultar;
            //EDConsultarConsultasSST consultar = new EDConsultarConsultasSST();
            //consultar.tipoConsult = tipoConsult;
            //consultar.Fecha_ini = Fecha_ini;
            //consultar.Fecha_Fin = Fecha_Fin;
            //consultar.idEmpresa = usuarioActual.IdEmpresa;
            //ServiceClient.AdicionarParametro("idEmpresa", usuarioActual.IdEmpresa);
            var result = new byte[100000];
           
            if (consultar == null)
            {
                ServiceClient.EliminarParametros();
                ServiceClient.AdicionarParametro("idEmpresa", usuarioActual.IdEmpresa);
                result = ServiceClient.ObtenerObjetoJsonRestFul<byte[]>(UrlServicioParticipacion, DescargarConsultaSSTSinFiltro, RestSharp.Method.GET);
            }
            else {
                ServiceClient.EliminarParametros();
                result = ServiceClient.ObtenerObjetoJsonRestFul<byte[]>(UrlServicioParticipacion, DescargarConsultaSST, consultar);
            }
            return File(result, "application/vnd.ms-excel", "ConsultasSST.xlsx");                 
        }
    }
}