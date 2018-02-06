using SG_SST.Controllers.Base;
using SG_SST.EntidadesDominio.Empleado;
using SG_SST.EntidadesDominio.Planificacion;
using SG_SST.EntidadesDominio.Aplicacion;
using SG_SST.Logica.Empresas;
using SG_SST.Logica.MedicionEvaluacion;
using SG_SST.Logica.Planificacion;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
//using DocumentFormat.OpenXml.Packaging;
//using DocumentFormat.OpenXml.Spreadsheet;
using ClosedXML.Excel;
using System.Configuration;
using RestSharp;
using System.Net;
using SG_SST.Dtos.Empresas;
using SG_SST.Models.Aplicacion;
using SG_SST.Logica.Aplicacion;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web.Configuration;
using SG_SST.EntidadesDominio.Empresas;
using SG_SST.Models.Ausentismo;
using SG_SST.Models.Empleado;
using System.Text;
using System.Text.RegularExpressions;
using OfficeOpenXml;
using SG_SST.ServiceRequest;

namespace SG_SST.Controllers.Aplicacion
{
    public class AdmoEPPController : BaseController
    {
        LNEmpresa LNEmpresa = new LNEmpresa();
        LNPeligro LNPeligro = new LNPeligro();
        LNAcciones LNAcciones = new LNAcciones();
        LNMetodologia LNMetodologia = new LNMetodologia();
        LNEPP LNEPP = new LNEPP();
        LNProcesos LNProcesos = new LNProcesos();

        private static string RutaExcelPlantilla = "~/Content/EPPArchivos/Plantilla/PlantillaEPP.xlsx";
        private static string RutaPdfManual = "~/Content/EPPArchivos/Plantilla/instrucciones_uso_EPP.pdf";
        private static string RutaExcelTemp = "~/Content/EPPArchivos/ExcelTemp/";
        private static string RutaImagenesTemp = "~/Content/EPPArchivos/ImagenesTemp/";
        private static string RutaArchivosBDTemp = "~/Content/EPPArchivos/ArchivosTemp/";
        private static string RutaImagenes = "~/Content/EPPArchivos/Imagenes/";
        private static string RutaArchivosBD = "~/Content/EPPArchivos/Archivos/";
        private static string SrcWhite = "data:image/png;base64,R0lGODlhAQABAAAAACH5BAEKAAEALAAAAAABAAEAAAICTAEAOw==";

        private static string UrlServicioAplicacion = ConfigurationManager.AppSettings["UrlServicioAplicacion"];
        private static string ObtenerInspeccionesEmpresa = ConfigurationManager.AppSettings["ObtenerElementosProteccionPersonal"];


        #region Nuevo&Editar
        [HttpGet]
        public ActionResult NuevoEPP()
        {
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

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }
            List<EDTipoDePeligro> ListaTipoPeligros = LNMetodologia.ObtenerTiposDePeligro();
            List<EDClasificacionDePeligro> ListaClasPeligros = new List<EDClasificacionDePeligro>();
            if (ListaTipoPeligros != null)
            {
                foreach (var item in ListaTipoPeligros)
                {
                    string DescripcionTipo = item.Descripcion_Del_Peligro;
                    List<EDClasificacionDePeligro> ListaClasPeligro = new List<EDClasificacionDePeligro>();
                    ListaClasPeligro = LNPeligro.ObtenerClasificaciónPorTipo(item.PK_Tipo_De_Peligro);
                    foreach (var item1 in ListaClasPeligro)
                    {
                        string DescripcionClasePeligro = DescripcionTipo + " - " + item1.DescripcionClaseDePeligro;
                        item1.DescripcionClaseDePeligro = DescripcionClasePeligro;
                        ListaClasPeligros.Add(item1);
                    }
                }

            }
            ViewBag.Pk_Id_Clasif_Peligro = new SelectList(ListaClasPeligros, "IdClasificacionDePeligro", "DescripcionClaseDePeligro", null);


            List<EDCargo> ListaEDCargo = new List<EDCargo>();
            try
            {
                ListaEDCargo = ConsultarCargos(usuarioActual.NitEmpresa);
                if (ListaEDCargo.Count == 0)
                {
                    ListaEDCargo = LNEPP.ListaCargos();
                }
            }
            catch (Exception)
            {
                ListaEDCargo = LNEPP.ListaCargos();
            }
            ViewBag.Pk_Id_Cargo = new SelectList(ListaEDCargo, "IDCargo", "NombreCargo", null);

            return View();
        }
        [HttpGet]
        public ActionResult NuevoEPP1()
        {
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

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }
            List<EDTipoDePeligro> ListaTipoPeligros = LNMetodologia.ObtenerTiposDePeligro();
            List<EDClasificacionDePeligro> ListaClasPeligros = new List<EDClasificacionDePeligro>();
            if (ListaTipoPeligros != null)
            {
                foreach (var item in ListaTipoPeligros)
                {
                    string DescripcionTipo = item.Descripcion_Del_Peligro;
                    List<EDClasificacionDePeligro> ListaClasPeligro = new List<EDClasificacionDePeligro>();
                    ListaClasPeligro = LNPeligro.ObtenerClasificaciónPorTipo(item.PK_Tipo_De_Peligro);
                    foreach (var item1 in ListaClasPeligro)
                    {
                        string DescripcionClasePeligro = DescripcionTipo + " - " + item1.DescripcionClaseDePeligro;
                        item1.DescripcionClaseDePeligro = DescripcionClasePeligro;
                        ListaClasPeligros.Add(item1);
                    }
                }

            }
            ViewBag.Pk_Id_Clasif_Peligro = new SelectList(ListaClasPeligros, "IdClasificacionDePeligro", "DescripcionClaseDePeligro", null);

            EDEPP EDEPP = new EDEPP();
            List<EDCargo> ListaEDCargo = new List<EDCargo>();
            try
            {
                ListaEDCargo = ConsultarCargos(usuarioActual.NitEmpresa);
            }
            catch (Exception ex)
            {
                EDEPP.EspecificacionTecnica = ex.ToString();
            }
            List<EDCargo> ListaEDCargo1 = new List<EDCargo>();
            List<EDCargo> ListaEDCargo2 = new List<EDCargo>();
            ListaEDCargo2 = LNEPP.ListaCargos();
            ListaEDCargo1 = ListaCargosWS1(usuarioActual.NitEmpresa, ListaEDCargo2);


            ViewBag.Pk_Id_Cargo = new SelectList(ListaEDCargo1, "IDCargo", "NombreCargo", null);




            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("Documento", usuarioActual.Documento);
            ServiceClient.AdicionarParametro("Nit", usuarioActual.NitEmpresa);
            var resultplaneacionporEM = ServiceClient.ObtenerArrayJsonRestFul<EDEPP>(UrlServicioAplicacion, ObtenerInspeccionesEmpresa, RestSharp.Method.GET);

            if (resultplaneacionporEM != null)
            {
                EDEPP = resultplaneacionporEM[0];
            }


            return View(EDEPP);
        }
        [HttpPost]
        public ActionResult GuardarNuevoEPP(EPP GuardarEPP)
        {
            bool Probar = false;
            string Estado = "No se guardó el elemento de protección personal, por favor revise la información suministrada";
            bool[] Validacion = new bool[13];
            string[] ValidacionStr = new string[13];
            for (int i = 0; i < 13; i++)
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
                EPP NuevoEPP = new EPP();
                EDEPP NuevoEDEpp = new EDEPP();
                NuevoEPP = GuardarEPP;
                NuevoEPP.Fk_Id_Empresa = usuarioActual.IdEmpresa;

                CrearCarpeta(RutaImagenes);
                CrearCarpeta(RutaArchivosBD);
                CrearCarpeta(RutaImagenesTemp);
                CrearCarpeta(RutaArchivosBDTemp);
                #region nuevoepparchivos
                List<string> ArchivosTemporalesEliminar = new List<string>();
                if (NuevoEPP.ArchivoImagen != null)
                {
                    string PathOrigen = Server.MapPath(Path.Combine(RutaImagenesTemp, NuevoEPP.ArchivoImagen));
                    if (System.IO.File.Exists(PathOrigen))
                    {
                        try
                        {
                            NuevoEDEpp.RutaImage = RutaImagenes;
                            NuevoEDEpp.ArchivoImagen = NuevoEPP.ArchivoImagen;
                            NuevoEDEpp.ArchivoImagen_download = NuevoEPP.ArchivoImagen_download;
                            string pathsave = Server.MapPath(Path.Combine(NuevoEDEpp.RutaImage, NuevoEDEpp.ArchivoImagen));
                            System.IO.File.Move(PathOrigen, pathsave);
                            ArchivosTemporalesEliminar.Add(PathOrigen);
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
                if (NuevoEPP.NombreArchivo != null)
                {
                    string PathOrigen = Server.MapPath(Path.Combine(RutaArchivosBDTemp, NuevoEPP.NombreArchivo));
                    if (System.IO.File.Exists(PathOrigen))
                    {
                        try
                        {
                            NuevoEDEpp.RutaArchivo = RutaArchivosBD;
                            NuevoEDEpp.NombreArchivo = NuevoEPP.NombreArchivo;
                            NuevoEDEpp.NombreArchivo_download = NuevoEPP.NombreArchivo_download;
                            string pathsave = Server.MapPath(Path.Combine(NuevoEDEpp.RutaArchivo, NuevoEDEpp.NombreArchivo));
                            System.IO.File.Move(PathOrigen, pathsave);
                            ArchivosTemporalesEliminar.Add(PathOrigen);
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
                #endregion

                List<EPPCargo> ListaCargos = new List<EPPCargo>();
                List<EDEPPCargo> EDListaCargos = new List<EDEPPCargo>();
                NuevoEDEpp.Pk_Id_EPP = NuevoEPP.Pk_Id_EPP;
                NuevoEDEpp.NombreEPP = NuevoEPP.NombreEPP;
                NuevoEDEpp.ParteCuerpo = NuevoEPP.ParteCuerpo;
                NuevoEDEpp.EspecificacionTecnica = NuevoEPP.EspecificacionTecnica;
                NuevoEDEpp.Uso = NuevoEPP.Uso;
                NuevoEDEpp.Mantenimiento = NuevoEPP.Mantenimiento;
                NuevoEDEpp.VidaUtil = NuevoEPP.VidaUtil;
                NuevoEDEpp.Reposicion = NuevoEPP.Reposicion;
                NuevoEDEpp.DisposicionFinal = NuevoEPP.DisposicionFinal;
                NuevoEDEpp.Fk_Id_Clasificacion_De_Peligro = NuevoEPP.Fk_Id_Clasificacion_De_Peligro;
                NuevoEDEpp.Fk_Id_Empresa = usuarioActual.IdEmpresa;

                try
                {
                    ListaCargos = NuevoEPP.Cargos.ToList();
                }
                catch (Exception)
                {
                }
                foreach (var item1 in ListaCargos)
                {
                    EDEPPCargo EDEPPCargo = new EDEPPCargo();
                    EDEPPCargo.Cantidad = item1.Cantidad;
                    EDEPPCargo.Fk_Id_Cargo = item1.Fk_Id_Cargo;
                    EDListaCargos.Add(EDEPPCargo);
                }
                NuevoEDEpp.Cargos = EDListaCargos;

                bool ProbarGuardado = LNEPP.GuardarEPP(NuevoEDEpp);
                if (ProbarGuardado)
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
                    Probar = true;
                    return Json(new { Estado, Probar });
                }
            }
            for (int i = 0; i < 13; i++)
            {
                Validacion[i] = false;
                ValidacionStr[i] = "";
            }
            int cont = 0;
            foreach (var kvp in ModelState)
            {
                var key = kvp.Key;
                cont++;
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
                if (key == "GuardarEPP.NombreEPP")
                {
                    ListaErroresSalida[cont] = 0;
                    ListaErroresSalidabool[cont] = true;
                }
                if (key == "GuardarEPP.ParteCuerpo")
                {
                    ListaErroresSalida[cont] = 1;
                    ListaErroresSalidabool[cont] = true;
                }
                if (key == "GuardarEPP.EspecificacionTecnica")
                {
                    ListaErroresSalida[cont] = 2;
                    ListaErroresSalidabool[cont] = true;
                }
                if (key == "GuardarEPP.Uso")
                {
                    ListaErroresSalida[cont] = 3;
                    ListaErroresSalidabool[cont] = true;
                }
                if (key == "GuardarEPP.Mantenimiento")
                {
                    ListaErroresSalida[cont] = 4;
                    ListaErroresSalidabool[cont] = true;
                }
                if (key == "GuardarEPP.VidaUtil")
                {
                    ListaErroresSalida[cont] = 5;
                    ListaErroresSalidabool[cont] = true;
                }
                if (key == "GuardarEPP.Reposicion")
                {
                    ListaErroresSalida[cont] = 6;
                    ListaErroresSalidabool[cont] = true;
                }
                if (key == "GuardarEPP.DisposicionFinal")
                {
                    ListaErroresSalida[cont] = 7;
                    ListaErroresSalidabool[cont] = true;
                }
                if (key == "GuardarEPP.ArchivoImagen")
                {
                    ListaErroresSalida[cont] = 8;
                    ListaErroresSalidabool[cont] = false;
                }
                if (key == "GuardarEPP.ArchivoImagen_download")
                {
                    ListaErroresSalida[cont] = 9;
                    ListaErroresSalidabool[cont] = false;
                }
                if (key == "GuardarEPP.NombreArchivo")
                {
                    ListaErroresSalida[cont] = 10;
                    ListaErroresSalidabool[cont] = false;
                }
                if (key == "GuardarEPP.NombreArchivo_download")
                {
                    ListaErroresSalida[cont] = 11;
                    ListaErroresSalidabool[cont] = false;
                }
                if (key == "GuardarEPP.Fk_Id_Clasificacion_De_Peligro")
                {
                    ListaErroresSalida[cont] = 12;
                    ListaErroresSalidabool[cont] = true;
                }
                cont++;
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
                cont++;
            }
            var Model = GuardarEPP;
            return Json(new { Model, Estado, Probar, Validacion, ValidacionStr }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult EditarEPP(string IdEPP)
        {
            EDEPP EDEPP = new EDEPP();
            string RutaImagen1 = string.Empty;
            string RutaArchivo1 = string.Empty;

            //Estilo imagen y boton eliminar
            ViewBag.Imagen1E = "max-width: 100%;max-height: 100%;display:none";
            ViewBag.Imagen1R = "";

            ViewBag.Archivo1E = false;
            ViewBag.Archivo1R = "";
            ViewBag.ArchivosE = false;

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

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }
            List<EDTipoDePeligro> ListaTipoPeligros = LNMetodologia.ObtenerTiposDePeligro();
            List<EDClasificacionDePeligro> ListaClasPeligros = new List<EDClasificacionDePeligro>();
            if (ListaTipoPeligros != null)
            {
                foreach (var item in ListaTipoPeligros)
                {
                    string DescripcionTipo = item.Descripcion_Del_Peligro;
                    List<EDClasificacionDePeligro> ListaClasPeligro = new List<EDClasificacionDePeligro>();
                    ListaClasPeligro = LNPeligro.ObtenerClasificaciónPorTipo(item.PK_Tipo_De_Peligro);
                    foreach (var item1 in ListaClasPeligro)
                    {
                        string DescripcionClasePeligro = DescripcionTipo + " - " + item1.DescripcionClaseDePeligro;
                        item1.DescripcionClaseDePeligro = DescripcionClasePeligro;
                        ListaClasPeligros.Add(item1);
                    }
                }

            }


            //Lista de cargos
            List<EDCargo> ListaEDCargo = new List<EDCargo>();
            try
            {
                ListaEDCargo = ConsultarCargos(usuarioActual.NitEmpresa);
                if (ListaEDCargo.Count == 0)
                {
                    ListaEDCargo = LNEPP.ListaCargos();
                }
            }
            catch (Exception)
            {
                ListaEDCargo = LNEPP.ListaCargos();
            }
            ViewBag.Pk_Id_Cargo = new SelectList(ListaEDCargo, "IDCargo", "NombreCargo", null);
            int IdEPPInt = 0;
            if (int.TryParse(IdEPP, out IdEPPInt))
            {
                EDEPP = LNEPP.ConsultarEPP(IdEPPInt, usuarioActual.IdEmpresa);
                if (EDEPP.EspecificacionTecnica == "-- Información no Disponible --")
                {
                    EDEPP.EspecificacionTecnica = "";
                }
                if (EDEPP.Uso == "-- Información no Disponible --")
                {
                    EDEPP.Uso = "";
                }
                if (EDEPP.Mantenimiento == "-- Información no Disponible --")
                {
                    EDEPP.Mantenimiento = "";
                }
                if (EDEPP.VidaUtil == "-- Información no Disponible --")
                {
                    EDEPP.VidaUtil = "";
                }
                if (EDEPP.Reposicion == "-- Información no Disponible --")
                {
                    EDEPP.Reposicion = "";
                }
                if (EDEPP.DisposicionFinal == "-- Información no Disponible --")
                {
                    EDEPP.DisposicionFinal = "";
                }





                if (EDEPP.ArchivoImagen != null && EDEPP.RutaImage != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(Path.Combine(EDEPP.RutaImage, EDEPP.ArchivoImagen))))
                    {
                        string Ruta = EDEPP.RutaImage.Replace("~", "") + EDEPP.ArchivoImagen; ;
                        EDEPP.RutaAbsolutaImagen = Ruta;
                    }
                }

            }

            if (EDEPP.ArchivoImagen != null && EDEPP.ArchivoImagen_download != null && EDEPP.RutaImage != null)
            {
                RutaImagen1 = Server.MapPath(Path.Combine(EDEPP.RutaImage, EDEPP.ArchivoImagen));
                if (System.IO.File.Exists(RutaImagen1))
                {
                    ViewBag.Imagen1E = "max-width: 100%;max-height: 100%;display:initial";
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

            if (EDEPP.NombreArchivo != null && EDEPP.NombreArchivo_download != null && EDEPP.RutaArchivo != null)
            {
                RutaArchivo1 = Server.MapPath(Path.Combine(EDEPP.RutaArchivo, EDEPP.NombreArchivo));
                if (System.IO.File.Exists(RutaArchivo1))
                {
                    ViewBag.ArchivosE = true;
                    ViewBag.Archivo1E = true;
                    ViewBag.Archivo1R = EDEPP.RutaArchivo.Replace("~", "") + EDEPP.NombreArchivo;
                }
            }

            int id_riesgo = EDEPP.Fk_Id_Clasificacion_De_Peligro;
            ViewBag.Pk_Id_Clasif_Peligro = new SelectList(ListaClasPeligros, "IdClasificacionDePeligro", "DescripcionClaseDePeligro", id_riesgo);

            return View(EDEPP);


        }
        [HttpPost]
        public ActionResult GuardarEditarEPP(EPP GuardarEPP)
        {
            bool Probar = false;
            string Estado = "No se guardó el elemento de protección personal, por favor revise la información suministrada";
            bool[] Validacion = new bool[13];
            string[] ValidacionStr = new string[13];
            for (int i = 0; i < 13; i++)
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
                EPP NuevoEPP = new EPP();
                EDEPP NuevoEDEpp = new EDEPP();
                EDEPP ActaulEDEpp = LNEPP.ConsultarEPP(GuardarEPP.Pk_Id_EPP, usuarioActual.IdEmpresa);


                NuevoEPP = GuardarEPP;
                NuevoEPP.Fk_Id_Empresa = usuarioActual.IdEmpresa;

                CrearCarpeta(RutaImagenes);
                CrearCarpeta(RutaArchivosBD);
                CrearCarpeta(RutaImagenesTemp);
                CrearCarpeta(RutaArchivosBDTemp);
                #region nuevoepparchivos
                List<string> ArchivosTemporalesEliminar = new List<string>();
                List<string> ArchivosMover = new List<string>();

                if (NuevoEPP.ArchivoImagen != null)
                {
                    if (ActaulEDEpp.ArchivoImagen != null)
                    {
                        if (NuevoEPP.ArchivoImagen == ActaulEDEpp.ArchivoImagen)
                        {
                            //Conservar Anterior
                            NuevoEDEpp.RutaImage = ActaulEDEpp.RutaImage;
                            NuevoEDEpp.ArchivoImagen = ActaulEDEpp.ArchivoImagen;
                            NuevoEDEpp.ArchivoImagen_download = ActaulEDEpp.ArchivoImagen_download;
                        }
                        else
                        {
                            //Conservar recien subida
                            string PathActual = Server.MapPath(Path.Combine(ActaulEDEpp.RutaImage, ActaulEDEpp.ArchivoImagen));
                            string PathOrigen = Server.MapPath(Path.Combine(RutaImagenesTemp, NuevoEPP.ArchivoImagen));
                            if (System.IO.File.Exists(PathActual))
                            {
                                ArchivosTemporalesEliminar.Add(PathActual);
                            }
                            if (System.IO.File.Exists(PathOrigen))
                            {
                                try
                                {
                                    NuevoEDEpp.RutaImage = RutaImagenes;
                                    NuevoEDEpp.ArchivoImagen = NuevoEPP.ArchivoImagen;
                                    NuevoEDEpp.ArchivoImagen_download = NuevoEPP.ArchivoImagen_download;
                                    string pathsave = Server.MapPath(Path.Combine(NuevoEDEpp.RutaImage, NuevoEDEpp.ArchivoImagen));
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
                        string PathOrigen = Server.MapPath(Path.Combine(RutaImagenesTemp, NuevoEPP.ArchivoImagen));
                        if (System.IO.File.Exists(PathOrigen))
                        {
                            try
                            {
                                NuevoEDEpp.RutaImage = RutaImagenes;
                                NuevoEDEpp.ArchivoImagen = NuevoEPP.ArchivoImagen;
                                NuevoEDEpp.ArchivoImagen_download = NuevoEPP.ArchivoImagen_download;
                                string pathsave = Server.MapPath(Path.Combine(NuevoEDEpp.RutaImage, NuevoEDEpp.ArchivoImagen));
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
                    if (ActaulEDEpp.ArchivoImagen != null)
                    {
                        //Eliminar Imagen
                        string PathActual = Server.MapPath(Path.Combine(ActaulEDEpp.RutaImage, ActaulEDEpp.ArchivoImagen));
                        NuevoEDEpp.RutaImage = null;
                        NuevoEDEpp.ArchivoImagen = null;
                        NuevoEDEpp.ArchivoImagen_download = null;
                        if (System.IO.File.Exists(PathActual))
                        {
                            ArchivosTemporalesEliminar.Add(PathActual);
                        }
                    }
                    else
                    {
                        //ninguna accion
                        NuevoEDEpp.RutaImage = null;
                        NuevoEDEpp.ArchivoImagen = null;
                        NuevoEDEpp.ArchivoImagen_download = null;
                    }
                }


                //Archivo
                if (NuevoEPP.NombreArchivo != null)
                {
                    if (ActaulEDEpp.NombreArchivo != null)
                    {
                        if (NuevoEPP.NombreArchivo == ActaulEDEpp.NombreArchivo)
                        {
                            //Conservar Anterior
                            NuevoEDEpp.RutaArchivo = ActaulEDEpp.RutaArchivo;
                            NuevoEDEpp.NombreArchivo = ActaulEDEpp.NombreArchivo;
                            NuevoEDEpp.NombreArchivo_download = ActaulEDEpp.NombreArchivo_download;
                        }
                        else
                        {
                            //Conservar recien subida
                            string PathActual = Server.MapPath(Path.Combine(ActaulEDEpp.RutaArchivo, ActaulEDEpp.NombreArchivo));
                            string PathOrigen = Server.MapPath(Path.Combine(RutaArchivosBDTemp, NuevoEPP.NombreArchivo));
                            if (System.IO.File.Exists(PathActual))
                            {
                                ArchivosTemporalesEliminar.Add(PathActual);
                            }
                            if (System.IO.File.Exists(PathOrigen))
                            {
                                try
                                {
                                    NuevoEDEpp.RutaArchivo = RutaArchivosBD;
                                    NuevoEDEpp.NombreArchivo = NuevoEPP.NombreArchivo;
                                    NuevoEDEpp.NombreArchivo_download = NuevoEPP.NombreArchivo_download;
                                    string pathsave = Server.MapPath(Path.Combine(NuevoEDEpp.RutaArchivo, NuevoEDEpp.NombreArchivo));
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
                        //guardar unico archivo
                        string PathOrigen = Server.MapPath(Path.Combine(RutaArchivosBDTemp, NuevoEPP.NombreArchivo));
                        if (System.IO.File.Exists(PathOrigen))
                        {
                            try
                            {
                                NuevoEDEpp.RutaArchivo = RutaArchivosBD;
                                NuevoEDEpp.NombreArchivo = NuevoEPP.NombreArchivo;
                                NuevoEDEpp.NombreArchivo_download = NuevoEPP.NombreArchivo_download;
                                string pathsave = Server.MapPath(Path.Combine(NuevoEDEpp.RutaArchivo, NuevoEDEpp.NombreArchivo));
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
                    if (ActaulEDEpp.NombreArchivo != null)
                    {
                        //Eliminar archivo
                        string PathActual = Server.MapPath(Path.Combine(ActaulEDEpp.RutaArchivo, ActaulEDEpp.NombreArchivo));
                        NuevoEDEpp.RutaArchivo = null;
                        NuevoEDEpp.NombreArchivo = null;
                        NuevoEDEpp.NombreArchivo_download = null;
                        if (System.IO.File.Exists(PathActual))
                        {
                            ArchivosTemporalesEliminar.Add(PathActual);
                        }
                    }
                    else
                    {
                        //ninguna accion
                        NuevoEDEpp.RutaArchivo = null;
                        NuevoEDEpp.NombreArchivo = null;
                        NuevoEDEpp.NombreArchivo_download = null;
                    }
                }

                #endregion
                List<EPPCargo> ListaCargos = new List<EPPCargo>();
                List<EDEPPCargo> EDListaCargos = new List<EDEPPCargo>();
                NuevoEDEpp.Pk_Id_EPP = NuevoEPP.Pk_Id_EPP;
                NuevoEDEpp.NombreEPP = NuevoEPP.NombreEPP;
                NuevoEDEpp.ParteCuerpo = NuevoEPP.ParteCuerpo;
                NuevoEDEpp.EspecificacionTecnica = NuevoEPP.EspecificacionTecnica;
                NuevoEDEpp.Uso = NuevoEPP.Uso;
                NuevoEDEpp.Mantenimiento = NuevoEPP.Mantenimiento;
                NuevoEDEpp.VidaUtil = NuevoEPP.VidaUtil;
                NuevoEDEpp.Reposicion = NuevoEPP.Reposicion;
                NuevoEDEpp.DisposicionFinal = NuevoEPP.DisposicionFinal;
                NuevoEDEpp.Fk_Id_Clasificacion_De_Peligro = NuevoEPP.Fk_Id_Clasificacion_De_Peligro;
                NuevoEDEpp.Fk_Id_Empresa = usuarioActual.IdEmpresa;

                try
                {
                    ListaCargos = NuevoEPP.Cargos.ToList();
                }
                catch (Exception)
                {
                }
                foreach (var item1 in ListaCargos)
                {
                    EDEPPCargo EDEPPCargo = new EDEPPCargo();
                    EDEPPCargo.Cantidad = item1.Cantidad;
                    EDEPPCargo.Fk_Id_Cargo = item1.Fk_Id_Cargo;
                    EDListaCargos.Add(EDEPPCargo);
                }
                NuevoEDEpp.Cargos = EDListaCargos;

                bool ProbarGuardado = LNEPP.EditarEPP(NuevoEDEpp);
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


                    Probar = true;
                    return Json(new { Estado, Probar });
                }
            }
            for (int i = 0; i < 13; i++)
            {
                Validacion[i] = false;
                ValidacionStr[i] = "";
            }
            int cont = 0;
            foreach (var kvp in ModelState)
            {
                var key = kvp.Key;
                cont++;
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
                if (key == "GuardarEPP.NombreEPP")
                {
                    ListaErroresSalida[cont] = 0;
                    ListaErroresSalidabool[cont] = true;
                }
                if (key == "GuardarEPP.ParteCuerpo")
                {
                    ListaErroresSalida[cont] = 1;
                    ListaErroresSalidabool[cont] = true;
                }
                if (key == "GuardarEPP.EspecificacionTecnica")
                {
                    ListaErroresSalida[cont] = 2;
                    ListaErroresSalidabool[cont] = true;
                }
                if (key == "GuardarEPP.Uso")
                {
                    ListaErroresSalida[cont] = 3;
                    ListaErroresSalidabool[cont] = true;
                }
                if (key == "GuardarEPP.Mantenimiento")
                {
                    ListaErroresSalida[cont] = 4;
                    ListaErroresSalidabool[cont] = true;
                }
                if (key == "GuardarEPP.VidaUtil")
                {
                    ListaErroresSalida[cont] = 5;
                    ListaErroresSalidabool[cont] = true;
                }
                if (key == "GuardarEPP.Reposicion")
                {
                    ListaErroresSalida[cont] = 6;
                    ListaErroresSalidabool[cont] = true;
                }
                if (key == "GuardarEPP.DisposicionFinal")
                {
                    ListaErroresSalida[cont] = 7;
                    ListaErroresSalidabool[cont] = true;
                }
                if (key == "GuardarEPP.ArchivoImagen")
                {
                    ListaErroresSalida[cont] = 8;
                    ListaErroresSalidabool[cont] = false;
                }
                if (key == "GuardarEPP.ArchivoImagen_download")
                {
                    ListaErroresSalida[cont] = 9;
                    ListaErroresSalidabool[cont] = false;
                }
                if (key == "GuardarEPP.NombreArchivo")
                {
                    ListaErroresSalida[cont] = 10;
                    ListaErroresSalidabool[cont] = false;
                }
                if (key == "GuardarEPP.NombreArchivo_download")
                {
                    ListaErroresSalida[cont] = 11;
                    ListaErroresSalidabool[cont] = false;
                }
                if (key == "GuardarEPP.Fk_Id_Clasificacion_De_Peligro")
                {
                    ListaErroresSalida[cont] = 12;
                    ListaErroresSalidabool[cont] = true;
                }
                cont++;
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
                cont++;
            }
            var Model = GuardarEPP;
            return Json(new { Model, Estado, Probar, Validacion, ValidacionStr }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region carguemasivo
        public ActionResult CargueMasivoEPP(EDEPP EDEPP)
        {
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

            List<EDEPP> ListaCargue = new List<EDEPP>();
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }
            List<EDTipoDePeligro> ListaTipoPeligros = LNMetodologia.ObtenerTiposDePeligro();
            List<EDClasificacionDePeligro> ListaClasPeligros = new List<EDClasificacionDePeligro>();
            if (ListaTipoPeligros != null)
            {
                foreach (var item in ListaTipoPeligros)
                {
                    string DescripcionTipo = item.Descripcion_Del_Peligro;
                    List<EDClasificacionDePeligro> ListaClasPeligro = new List<EDClasificacionDePeligro>();
                    ListaClasPeligro = LNPeligro.ObtenerClasificaciónPorTipo(item.PK_Tipo_De_Peligro);
                    foreach (var item1 in ListaClasPeligro)
                    {
                        string DescripcionClasePeligro = DescripcionTipo + " - " + item1.DescripcionClaseDePeligro;
                        item1.DescripcionClaseDePeligro = DescripcionClasePeligro;
                        ListaClasPeligros.Add(item1);
                    }
                }

            }
            ViewBag.Pk_Id_Clasif_Peligro = new SelectList(ListaClasPeligros, "IdClasificacionDePeligro", "DescripcionClaseDePeligro", null);
            ViewBag.NumeroFilas = 0;

            List<EDCargo> ListaCargos = new List<EDCargo>();
            ViewBag.Pk_Id_Cargo = new SelectList(ListaCargos, "IDCargo", "NombreCargo", null);
            ViewBag.LimiteMB = int_MB;

            if (EDEPP != null)
            {
                return View(ListaCargue);
            }
            else
            {
                return View(ListaCargue);
            }
        }
        public FileResult Download()
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(Server.MapPath(RutaExcelPlantilla));
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, "PlantillaEPP.xlsx");
        }
        public FileResult Download_Manual()
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(Server.MapPath(RutaPdfManual));
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, "instrucciones_uso_EPP.pdf");
        }
        public ActionResult Upload(FormCollection formCollection)
        {
            int int_FileSizeLimit = 0;
            int int_MB = 0;
            HttpRuntimeSection section = ConfigurationManager.GetSection("system.web/httpRuntime") as HttpRuntimeSection;
            if (section != null)
            {
                int_FileSizeLimit = (section.MaxRequestLength * 1024);
                double megabytes1 = ConvertBytesToMegabytes((long)int_FileSizeLimit);
                int_MB = (int)Math.Floor(megabytes1);
            }
            else
            {
                int_FileSizeLimit = 31457280;
            }

            List<EDEPP> ListaCargue = new List<EDEPP>();
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }
            if (Request != null)
            {
                HttpPostedFileBase file = Request.Files["UploadedFile"];
                if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
                {
                    if (file.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                    {
                        string Fname = file.FileName;
                        CrearCarpeta(RutaExcelTemp);
                        string RandomName = RandomString(4) + DateTime.Now.ToString();
                        RandomName = RandomName.Replace(".", "").Replace(" ", "").Replace("/", "").Replace("-", "").Replace(":", "");
                        Fname = RandomName + Fname;
                        file.SaveAs(Server.MapPath(Path.Combine(RutaExcelTemp, Fname)));
                        string ruracargue = Path.Combine(RutaExcelTemp, Fname);

                        DataTable dtEPP = new DataTable();
                        try
                        {
                            //dtEPP = ReadAsDataTable(Server.MapPath(ruracargue));
                            dtEPP = ReadAsDataTable1(Server.MapPath(ruracargue), true);
                        }
                        catch (Exception ex)
                        {
                            //dtEPP = ReadAsDataTable(Server.MapPath(ruracargue));
                            //dtEPP = ReadAsDataTable1(Server.MapPath(ruracargue), true);
                        }



                        int cont = 0;
                        foreach (DataRow row in dtEPP.Rows)
                        {
                            bool filaConRegistro = false;
                            EDEPP EDEPP = new EDEPP();
                            EDEPP.Pk_Id_EPP = cont;
                            EDEPP.NombreEPP = row[0].ToString();
                            EDEPP.ParteCuerpo = row[1].ToString();

                            for (int i = 0; i < 7; i++)
                            {
                                if (row[i].ToString().Replace(" ","") != "")
                                {
                                    filaConRegistro = true;
                                }
                            }


                            if (row[2].ToString() == "")
                            {
                                EDEPP.EspecificacionTecnica = "-- Información no Disponible --";
                            }
                            else
                            {
                                EDEPP.EspecificacionTecnica = row[2].ToString();
                            }
                            if (row[3].ToString() == "")
                            {
                                EDEPP.Uso = "-- Información no Disponible --";
                            }
                            else
                            {
                                EDEPP.Uso = row[3].ToString();
                            }
                            if (row[4].ToString() == "")
                            {
                                EDEPP.Mantenimiento = "-- Información no Disponible --";
                            }
                            else
                            {
                                EDEPP.Mantenimiento = row[4].ToString();
                            }
                            if (row[5].ToString() == "")
                            {
                                EDEPP.VidaUtil = "-- Información no Disponible --";
                            }
                            else
                            {
                                EDEPP.VidaUtil = row[5].ToString();
                            }
                            if (row[6].ToString() == "")
                            {
                                EDEPP.Reposicion = "-- Información no Disponible --";
                            }
                            else
                            {
                                EDEPP.Reposicion = row[6].ToString();
                            }
                            if (row[7].ToString() == "")
                            {
                                EDEPP.DisposicionFinal = "-- Información no Disponible --";
                            }
                            else
                            {
                                EDEPP.DisposicionFinal = row[7].ToString();
                            }
                            if (filaConRegistro)
                            {
                                ListaCargue.Add(EDEPP);
                            }
                            
                            cont = cont + 1;
                        }
                        //try
                        //{

                        //}
                        //catch (Exception)
                        //{

                        //}
                    }
                    else
                    {
                        if (true)
                        {
                            try
                            {
                                string Fname = file.FileName;
                                CrearCarpeta(RutaExcelTemp);
                                string RandomName = RandomString(4) + DateTime.Now.ToString();
                                RandomName = RandomName.Replace(".", "").Replace(" ", "").Replace("/", "").Replace("-", "").Replace(":", "");
                                Fname = RandomName + Fname;
                                file.SaveAs(Server.MapPath(Path.Combine(RutaExcelTemp, Fname)));
                                string filePath = Server.MapPath(Path.Combine(RutaExcelTemp, Fname));
                                DataTable fgfd = workbookProcessing1(filePath);


                            }
                            catch (Exception ex)
                            {

                            }


                        }
                        //no es xml
                    }
                }
            }
            List<EDTipoDePeligro> ListaTipoPeligros = LNMetodologia.ObtenerTiposDePeligro();
            List<EDClasificacionDePeligro> ListaClasPeligros = new List<EDClasificacionDePeligro>();
            if (ListaTipoPeligros != null)
            {
                foreach (var item in ListaTipoPeligros)
                {
                    string DescripcionTipo = item.Descripcion_Del_Peligro;
                    List<EDClasificacionDePeligro> ListaClasPeligro = new List<EDClasificacionDePeligro>();
                    ListaClasPeligro = LNPeligro.ObtenerClasificaciónPorTipo(item.PK_Tipo_De_Peligro);
                    foreach (var item1 in ListaClasPeligro)
                    {
                        string DescripcionClasePeligro = DescripcionTipo + " - " + item1.DescripcionClaseDePeligro;
                        item1.DescripcionClaseDePeligro = DescripcionClasePeligro;
                        ListaClasPeligros.Add(item1);
                    }
                }

            }
            ViewBag.Pk_Id_Clasif_Peligro = new SelectList(ListaClasPeligros, "IdClasificacionDePeligro", "DescripcionClaseDePeligro", null);
            List<EDCargo> ListaEDCargo = new List<EDCargo>();

            try
            {
                ListaEDCargo = ConsultarCargos(usuarioActual.NitEmpresa);
                if (ListaEDCargo.Count == 0)
                {
                    ListaEDCargo = LNEPP.ListaCargos();
                }
            }
            catch (Exception)
            {
                ListaEDCargo = LNEPP.ListaCargos();
            }
            ViewBag.Pk_Id_Cargo = new SelectList(ListaEDCargo, "IDCargo", "NombreCargo", null);
            ViewBag.LimiteMB = int_MB;
            return View("CargueMasivoEPP", ListaCargue);
        }
        private DataTable workbookProcessing1(string workbookname)
        {
            DataTable dt = new DataTable();
            using (XLWorkbook workBook = new XLWorkbook(workbookname))
            {


                //Read the first Sheet from Excel file.
                IXLWorksheet workSheet = workBook.Worksheet(1);

                //Create a new DataTable.


                //Loop through the Worksheet rows.
                bool firstRow = true;
                foreach (IXLRow row in workSheet.Rows())
                {
                    //Use the first row to add columns to DataTable.
                    if (firstRow)
                    {
                        foreach (IXLCell cell in row.Cells())
                        {
                            dt.Columns.Add(cell.Value.ToString());
                        }
                        firstRow = false;
                    }
                    else
                    {
                        //Add rows to DataTable.
                        dt.Rows.Add();
                        int i = 0;
                        foreach (IXLCell cell in row.Cells())
                        {
                            dt.Rows[dt.Rows.Count - 1][i] = cell.Value.ToString();
                            i++;
                        }
                    }

                }
            }
            return dt;
        }
        //public static DataTable ReadAsDataTable(string fileName)
        //{
        //    DataTable dataTable = new DataTable();
        //    using (SpreadsheetDocument spreadSheetDocument = SpreadsheetDocument.Open(fileName, false))
        //    {
        //        WorkbookPart workbookPart = spreadSheetDocument.WorkbookPart;
        //        IEnumerable<Sheet> sheets = spreadSheetDocument.WorkbookPart.Workbook.GetFirstChild<Sheets>().Elements<Sheet>();
        //        string relationshipId = sheets.First().Id.Value;
        //        WorksheetPart worksheetPart = (WorksheetPart)spreadSheetDocument.WorkbookPart.GetPartById(relationshipId);
        //        Worksheet workSheet = worksheetPart.Worksheet;
        //        SheetData sheetData = workSheet.GetFirstChild<SheetData>();
        //        IEnumerable<Row> rows = sheetData.Descendants<Row>();


        //        dataTable.Columns.Add("Nombre", typeof(String));
        //        dataTable.Columns.Add("ParteCuerpo", typeof(String));
        //        dataTable.Columns.Add("EspTec", typeof(String));
        //        dataTable.Columns.Add("Uso", typeof(String));
        //        dataTable.Columns.Add("Mantenimiento", typeof(String));
        //        dataTable.Columns.Add("VidaUtil", typeof(String));
        //        dataTable.Columns.Add("Reposicion", typeof(String));
        //        dataTable.Columns.Add("Disposicion", typeof(String));

        //        int contRows = 0;
        //        int numeroColumnas = 8;

        //        foreach (Row row in rows)
        //        {
        //            int contVacia = 0;
        //            if (contRows >= 2)
        //            {
        //                DataRow dataRow = dataTable.NewRow();
        //                for (int i = 0; i < numeroColumnas; i++)
        //                {
        //                    try
        //                    {
        //                        if (GetCellValue(spreadSheetDocument, row.Descendants<Cell>().ElementAt(i)) == "")
        //                        {
        //                            contVacia = contVacia + 1;
        //                        }

        //                        dataRow[i] = GetCellValue(spreadSheetDocument, row.Descendants<Cell>().ElementAt(i));
        //                    }
        //                    catch (Exception)
        //                    {
        //                        contVacia = contVacia + 1;
        //                        dataRow[i] = "-- Información no Disponible --";
        //                    }

        //                }
        //                if (contVacia == numeroColumnas)
        //                {
        //                    break;
        //                }
        //                else
        //                {
        //                    dataTable.Rows.Add(dataRow);
        //                }
        //            }
        //            contRows = contRows + 1;
        //        }
        //    }


        //    return dataTable;
        //}

        public static DataTable ReadAsDataTable1(string fileName, bool hasHeader)
        {

            DataTable DataTable = new DataTable();
            using (var pck = new OfficeOpenXml.ExcelPackage())
            {
                using (var stream = System.IO.File.OpenRead(fileName))
                {
                    pck.Load(stream);
                }
                var ws = pck.Workbook.Worksheets.First();
                DataTable tbl = new DataTable();
                //tbl.Columns.Add("Nombre", typeof(String));
                //tbl.Columns.Add("ParteCuerpo", typeof(String));
                //tbl.Columns.Add("EspTec", typeof(String));
                //tbl.Columns.Add("Uso", typeof(String));
                //tbl.Columns.Add("Mantenimiento", typeof(String));
                //tbl.Columns.Add("VidaUtil", typeof(String));
                //tbl.Columns.Add("Reposicion", typeof(String));
                //tbl.Columns.Add("Disposicion", typeof(String));
                //Fila Headers
                foreach (var firstRowCell in ws.Cells[2, 1, 2, 8])
                {
                    //tbl.Columns.Add(hasHeader ? firstRowCell.Text : string.Format("Column {0}", firstRowCell.Start.Column));

                }
                tbl.Columns.Add("Nombre", typeof(String));
                tbl.Columns.Add("ParteCuerpo", typeof(String));
                tbl.Columns.Add("EspTec", typeof(String));
                tbl.Columns.Add("Uso", typeof(String));
                tbl.Columns.Add("Mantenimiento", typeof(String));
                tbl.Columns.Add("VidaUtil", typeof(String));
                tbl.Columns.Add("Reposicion", typeof(String));
                tbl.Columns.Add("Disposicion", typeof(String));
                var startRow = hasHeader ? 3 : 2;
                for (int rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
                {
                    var wsRow = ws.Cells[rowNum, 1, rowNum, 8];
                    DataRow row = tbl.Rows.Add();
                    int contCol = 0;
                    foreach (var cell in wsRow)
                    {

                        if (contCol >= 2)
                        {
                            if (cell.Text == null)
                            {
                                row[cell.Start.Column - 1] = "-- Información no Disponible --";
                            }
                            else
                            {
                                if (cell.Text == "")
                                {
                                    row[cell.Start.Column - 1] = "-- Información no Disponible --";
                                }
                                else
                                {
                                    row[cell.Start.Column - 1] = cell.Text;
                                }
                            }
                        }
                        else
                        {
                            if (cell.Text == null)
                            {
                                row[cell.Start.Column - 1] = "";
                            }
                            else
                            {
                                if (cell.Text == "")
                                {
                                    row[cell.Start.Column - 1] = "";
                                }
                                else
                                {
                                    row[cell.Start.Column - 1] = cell.Text;
                                }
                            }
                        }
                        //row[cell.Start.Column - 1] = cell.Text;
                        contCol++;
                    }

                }
                return tbl;
            }

            return DataTable;
        }
        //private static string GetCellValue(SpreadsheetDocument document, Cell cell)
        //{
        //    SharedStringTablePart stringTablePart = document.WorkbookPart.SharedStringTablePart;
        //    if (cell.CellValue == null)
        //    {
        //        return "";
        //    }
        //    string value = cell.CellValue.InnerXml;

        //    if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
        //    {
        //        return stringTablePart.SharedStringTable.ChildElements[Int32.Parse(value)].InnerText;
        //    }
        //    else
        //    {
        //        return value;
        //    }
        //}
        [HttpPost]
        public ActionResult GuardarMasivoNuevoEPP(EPP Control, List<EPP> ListaEPP)
        {
            int NumeroFilas = ListaEPP.Count();
            int NumeroColumnas = 8;

            string[,] ListaImagenes = new string[NumeroFilas, 2];
            string[,] ListaArchivos = new string[NumeroFilas, 2];

            for (int i = 0; i < NumeroFilas; i++)
            {
                for (int i1 = 0; i1 < 2; i1++)
                {
                    ListaImagenes[i, i1] = string.Empty;
                    ListaArchivos[i, i1] = string.Empty;
                }
            }
            List<string> ArchivosTemporalesEliminar = new List<string>();
            List<EDEPP> ListaGuardarEPP = new List<EDEPP>();
            bool[,] Validacion = new bool[NumeroFilas, NumeroColumnas];
            string[,] ValidacionStr = new string[NumeroFilas, NumeroColumnas];
            string[] ValidacionMensaje = new string[NumeroFilas];
            string Estado = "No se guardó el cargue de los elementos de protección personal, por favor revise la información suministrada";
            bool Probar = false;
            bool Probar_validacion = false;

            for (int i = 0; i < NumeroFilas; i++)
            {
                for (int i1 = 0; i1 < NumeroColumnas; i1++)
                {
                    Validacion[i, i1] = false;
                    ValidacionStr[i, i1] = "";
                }
            }
            if (NumeroFilas == 0)
            {
                Estado = "No hay elementos de protección personal para guardar";
                return Json(new { Estado, Probar, Validacion, ValidacionStr });
            }
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                Estado = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { Estado, Probar, Validacion, ValidacionStr });
            }
            if (ModelState.IsValid)
            {
                Probar_validacion = true;
                int cont = 0;
                foreach (var item in ListaEPP)
                {
                    List<EPPCargo> ListaCargos = new List<EPPCargo>();
                    List<EDEPPCargo> EDListaCargos = new List<EDEPPCargo>();
                    EDEPP NuevoEpp = new EDEPP();
                    NuevoEpp.Pk_Id_EPP = item.Pk_Id_EPP;
                    NuevoEpp.NombreEPP = item.NombreEPP;
                    NuevoEpp.ParteCuerpo = item.ParteCuerpo;
                    NuevoEpp.EspecificacionTecnica = item.EspecificacionTecnica;
                    NuevoEpp.Uso = item.Uso;
                    NuevoEpp.Mantenimiento = item.Mantenimiento;
                    NuevoEpp.VidaUtil = item.VidaUtil;
                    NuevoEpp.Reposicion = item.Reposicion;
                    NuevoEpp.DisposicionFinal = item.DisposicionFinal;
                    NuevoEpp.Fk_Id_Clasificacion_De_Peligro = item.Fk_Id_Clasificacion_De_Peligro;
                    NuevoEpp.Fk_Id_Empresa = usuarioActual.IdEmpresa;

                    //imagenes y archivos
                    CrearCarpeta(RutaImagenes);
                    CrearCarpeta(RutaArchivosBD);

                    #region imagenes&archivos
                    if (item.ArchivoImagen != null && item.ArchivoImagen_download != null)
                    {
                        if (item.ArchivoImagen != string.Empty && item.ArchivoImagen_download != string.Empty)
                        {
                            if (System.IO.File.Exists(Server.MapPath(Path.Combine(RutaImagenesTemp, item.ArchivoImagen))))
                            {
                                NuevoEpp.ArchivoImagen = item.ArchivoImagen;
                                NuevoEpp.ArchivoImagen_download = item.ArchivoImagen_download;
                                NuevoEpp.RutaImage = RutaImagenes;
                            }
                            else
                            {
                                NuevoEpp.ArchivoImagen = null;
                                NuevoEpp.ArchivoImagen_download = null;
                                NuevoEpp.RutaImage = null;
                            }
                        }
                        else
                        {
                            NuevoEpp.ArchivoImagen = null;
                            NuevoEpp.ArchivoImagen_download = null;
                            NuevoEpp.RutaImage = null;
                        }
                    }
                    else
                    {
                        NuevoEpp.ArchivoImagen = null;
                        NuevoEpp.ArchivoImagen_download = null;
                        NuevoEpp.RutaImage = null;
                    }
                    if (item.NombreArchivo != null && item.NombreArchivo_download != null)
                    {
                        if (item.NombreArchivo != string.Empty && item.NombreArchivo_download != string.Empty)
                        {
                            if (System.IO.File.Exists(Server.MapPath(Path.Combine(RutaArchivosBDTemp, item.NombreArchivo))))
                            {
                                NuevoEpp.NombreArchivo = item.NombreArchivo;
                                NuevoEpp.NombreArchivo_download = item.NombreArchivo_download;
                                NuevoEpp.RutaArchivo = RutaArchivosBD;
                            }
                            else
                            {
                                NuevoEpp.NombreArchivo = null;
                                NuevoEpp.NombreArchivo_download = null;
                                NuevoEpp.RutaArchivo = null;
                            }
                        }
                        else
                        {
                            NuevoEpp.NombreArchivo = null;
                            NuevoEpp.NombreArchivo_download = null;
                            NuevoEpp.RutaArchivo = null;
                        }
                    }
                    else
                    {
                        NuevoEpp.NombreArchivo = null;
                        NuevoEpp.NombreArchivo_download = null;
                        NuevoEpp.RutaArchivo = null;
                    }

                    if (item.ArchivoImagen != null)
                    {
                        ListaImagenes[cont, 0] = item.ArchivoImagen;
                    }
                    if (item.ArchivoImagen_download != null)
                    {
                        ListaImagenes[cont, 1] = item.ArchivoImagen_download;
                    }
                    if (item.NombreArchivo != null)
                    {
                        ListaArchivos[cont, 0] = item.NombreArchivo;
                    }
                    if (item.NombreArchivo_download != null)
                    {
                        ListaArchivos[cont, 1] = item.NombreArchivo_download;
                    }
                    #endregion

                    try
                    {
                        ListaCargos = item.Cargos.ToList();
                    }
                    catch (Exception)
                    {

                    }
                    foreach (var item1 in ListaCargos)
                    {
                        EDEPPCargo EDEPPCargo = new EDEPPCargo();
                        EDEPPCargo.Cantidad = item1.Cantidad;
                        EDEPPCargo.Fk_Id_Cargo = item1.Fk_Id_Cargo;
                        EDListaCargos.Add(EDEPPCargo);
                    }
                    NuevoEpp.Cargos = EDListaCargos;
                    ListaGuardarEPP.Add(NuevoEpp);
                    cont++;
                }
            }
            else
            {
                int cont = 0;
                foreach (var kvp in ModelState)
                {
                    var key = kvp.Key;
                    cont++;
                }
                int[] ListaErroresSalida = new int[cont];
                bool[] ListaErroresSalidabool = new bool[cont];
                int[] ListaErroresFila = new int[cont];
                try
                {
                    for (int i = 0; i < NumeroFilas; i++)
                    {
                        cont = 0;
                        string numerolista = "ListaEPP[" + i + "].";
                        foreach (var kvp in ModelState)
                        {
                            var key = kvp.Key;
                            if (key == numerolista + "NombreEPP")
                            {
                                ListaErroresSalida[cont] = 0;
                                ListaErroresSalidabool[cont] = true;
                                ListaErroresFila[cont] = i;
                            }
                            if (key == numerolista + "ParteCuerpo")
                            {
                                ListaErroresSalida[cont] = 1;
                                ListaErroresSalidabool[cont] = true;
                                ListaErroresFila[cont] = i;
                            }
                            if (key == numerolista + "Fk_Id_Clasificacion_De_Peligro")
                            {
                                ListaErroresSalida[cont] = 2;
                                ListaErroresSalidabool[cont] = true;
                                ListaErroresFila[cont] = i;
                            }
                            if (key == numerolista + "EspecificacionTecnica")
                            {
                                ListaErroresSalida[cont] = 3;
                                ListaErroresSalidabool[cont] = true;
                                ListaErroresFila[cont] = i;
                            }
                            if (key == numerolista + "Uso")
                            {
                                ListaErroresSalida[cont] = 4;
                                ListaErroresSalidabool[cont] = true;
                                ListaErroresFila[cont] = i;
                            }
                            if (key == numerolista + "Mantenimiento")
                            {
                                ListaErroresSalida[cont] = 5;
                                ListaErroresSalidabool[cont] = true;
                                ListaErroresFila[cont] = i;
                            }
                            if (key == numerolista + "VidaUtil")
                            {
                                ListaErroresSalida[cont] = 6;
                                ListaErroresSalidabool[cont] = true;
                                ListaErroresFila[cont] = i;
                            }
                            if (key == numerolista + "Reposicion")
                            {
                                ListaErroresSalida[cont] = 7;
                                ListaErroresSalidabool[cont] = true;
                                ListaErroresFila[cont] = i;
                            }
                            if (key == numerolista + "DisposicionFinal")
                            {
                                ListaErroresSalida[cont] = 8;
                                ListaErroresSalidabool[cont] = true;
                                ListaErroresFila[cont] = i;
                            }
                            cont++;
                        }
                    }
                }
                catch (Exception ex)
                {
                }
                int numero_errores = 0;
                cont = 0;
                foreach (var kvp in ModelState)
                {
                    var value = kvp.Value;
                    if (value.Errors.Count > 0)
                    {
                        string valorError = value.Errors[0].ErrorMessage.ToString();
                        if (ListaErroresSalidabool[cont])
                        {
                            numero_errores++;
                            Validacion[ListaErroresFila[cont], ListaErroresSalida[cont]] = true;
                            ValidacionStr[ListaErroresFila[cont], ListaErroresSalida[cont]] = valorError;
                            Estado = numero_errores.ToString() + " Error(es) al intentar guardar la lista de EPP, por favor revise las areas sombreadas para ello ubiquese en ellas y se generará un mensaje de validación, corrija la información consignada y vuelva a intentar";
                        }
                    }
                    cont++;
                }
            }

            if (Probar_validacion)
            {
                Probar = LNEPP.GuardarMasivoEPP(ListaGuardarEPP);
                if (Probar)
                {
                    for (int i = 0; i < NumeroFilas; i++)
                    {
                        if (ListaImagenes[i, 0] != string.Empty && ListaImagenes[i, 1] != string.Empty)
                        {
                            string PathOrigen = Server.MapPath(Path.Combine(RutaImagenesTemp, ListaImagenes[i, 0]));
                            string PathDestino = Server.MapPath(Path.Combine(RutaImagenes, ListaImagenes[i, 0]));

                            if (System.IO.File.Exists(PathOrigen))
                            {
                                try
                                {
                                    System.IO.File.Move(PathOrigen, PathDestino);
                                    ArchivosTemporalesEliminar.Add(PathOrigen);
                                }
                                catch (Exception)
                                {
                                }
                            }
                        }

                        if (ListaArchivos[i, 0] != string.Empty && ListaArchivos[i, 1] != string.Empty)
                        {
                            string PathOrigen = Server.MapPath(Path.Combine(RutaArchivosBDTemp, ListaArchivos[i, 0]));
                            string PathDestino = Server.MapPath(Path.Combine(RutaArchivosBD, ListaArchivos[i, 0]));

                            if (System.IO.File.Exists(PathOrigen))
                            {
                                try
                                {
                                    System.IO.File.Move(PathOrigen, PathDestino);
                                    ArchivosTemporalesEliminar.Add(PathOrigen);
                                }
                                catch (Exception)
                                {
                                }
                            }
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
                }
            }

            return Json(new { Estado, Probar, Validacion, ValidacionStr }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult ValidarModeloEPP(EPP Control, List<EPP> ListaEPP)
        {

            int numero_errores = 0;
            int NumeroFilas = 0;
            int NumeroColumnas = 8;
            bool Estado_get = false;
            if (ListaEPP != null)
            {
                NumeroFilas = ListaEPP.Count;
            }
            else
            {
                Estado_get = true;
                return Json(new { Estado_get });
            }
            List<EDEPP> ListaGuardarEPP = new List<EDEPP>();
            bool[,] Validacion = new bool[NumeroFilas, NumeroColumnas];
            string[,] ValidacionStr = new string[NumeroFilas, NumeroColumnas];
            string[] ValidacionMensaje = new string[NumeroFilas];
            string Estado = "Cargue exitoso, los campos del cargue son validos.";
            bool Probar = true;
            for (int i = 0; i < NumeroFilas; i++)
            {
                for (int i1 = 0; i1 < NumeroColumnas; i1++)
                {
                    Validacion[i, i1] = false;
                    ValidacionStr[i, i1] = "";
                }
            }

            if (NumeroFilas == 0)
            {
                Estado = "No hay elementos de protección a cargar";
                return Json(new { Estado, Probar, Validacion, ValidacionStr });
            }

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                Estado = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { Estado, Probar, Validacion, ValidacionStr });
            }

            if (ModelState.IsValid)
            {
            }
            else
            {
                int cont = 0;
                foreach (var kvp in ModelState)
                {
                    var key = kvp.Key;
                    cont++;
                }
                int[] ListaErroresSalida = new int[cont];
                bool[] ListaErroresSalidabool = new bool[cont];
                int[] ListaErroresFila = new int[cont];
                try
                {

                    for (int i = 0; i < NumeroFilas; i++)
                    {
                        cont = 0;
                        string numerolista = "ListaEPP[" + i + "].";
                        foreach (var kvp in ModelState)
                        {
                            var key = kvp.Key;
                            if (key == numerolista + "NombreEPP")
                            {
                                ListaErroresSalida[cont] = 0;
                                ListaErroresSalidabool[cont] = true;
                                ListaErroresFila[cont] = i;
                            }
                            if (key == numerolista + "ParteCuerpo")
                            {
                                ListaErroresSalida[cont] = 1;
                                ListaErroresSalidabool[cont] = true;
                                ListaErroresFila[cont] = i;
                            }
                            if (key == numerolista + "Fk_Id_Clasificacion_De_Peligro")
                            {
                                ListaErroresSalida[cont] = 2;
                                ListaErroresSalidabool[cont] = true;
                                ListaErroresFila[cont] = i;
                            }
                            if (key == numerolista + "EspecificacionTecnica")
                            {
                                ListaErroresSalida[cont] = 3;
                                ListaErroresSalidabool[cont] = true;
                                ListaErroresFila[cont] = i;
                            }
                            if (key == numerolista + "Uso")
                            {
                                ListaErroresSalida[cont] = 4;
                                ListaErroresSalidabool[cont] = true;
                                ListaErroresFila[cont] = i;
                            }
                            if (key == numerolista + "Mantenimiento")
                            {
                                ListaErroresSalida[cont] = 5;
                                ListaErroresSalidabool[cont] = true;
                                ListaErroresFila[cont] = i;
                            }
                            if (key == numerolista + "VidaUtil")
                            {
                                ListaErroresSalida[cont] = 6;
                                ListaErroresSalidabool[cont] = true;
                                ListaErroresFila[cont] = i;
                            }
                            if (key == numerolista + "Reposicion")
                            {
                                ListaErroresSalida[cont] = 7;
                                ListaErroresSalidabool[cont] = true;
                                ListaErroresFila[cont] = i;
                            }
                            if (key == numerolista + "DisposicionFinal")
                            {
                                ListaErroresSalida[cont] = 8;
                                ListaErroresSalidabool[cont] = true;
                                ListaErroresFila[cont] = i;
                            }
                            cont++;
                        }
                    }
                }
                catch (Exception ex)
                {
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
                            if (ListaErroresSalida[cont] != 2)
                            {
                                numero_errores++;
                                Validacion[ListaErroresFila[cont], ListaErroresSalida[cont]] = true;
                                ValidacionStr[ListaErroresFila[cont], ListaErroresSalida[cont]] = valorError;
                                Probar = false;
                                Estado = numero_errores.ToString() + " Error(es). El cargue contiene errores. por favor verifique en la tabla los mensajes de error para ello ubiquese en las areas sombreadas y se generará el mensaje de la validación. Si intenta guardar con errores el sistema no guardará los cambios. Revise la hoja de cálculo, modifique el archivo y vuelva a intentar";
                            }
                        }
                    }
                    cont++;
                }
            }
            return Json(new { Estado, Probar, Validacion, ValidacionStr }, JsonRequestBehavior.AllowGet);
        }
        //static void XLSFileStreamReader(string filePath)
        //{
        //    FileStream stream = new FileStream(filePath, FileMode.Open);

        //    // Reading from a binary Excel file ('97-2003 format; *.xls)
        //    IExcelDataReader excelReader2003 = ExcelReaderFactory.CreateBinaryReader(stream);

        //    // DataSet - The result of each spreadsheet will be created in the result.Tables
        //    DataSet result = excelReader2003.AsDataSet();

        //    // Data Reader methods
        //    foreach (DataTable table in result.Tables)
        //    {
        //        for (int i = 0; i < table.Rows.Count; i++)
        //        {
        //            for (int j = 0; j < table.Columns.Count; j++)
        //                Console.Write("\"" + table.Rows[i].ItemArray[j] + "\";");
        //            Console.WriteLine();
        //        }
        //    }

        //    // Free resources (IExcelDataReader is IDisposable)
        //    excelReader2003.Close();
        //}
        //static void ConvertXlsToXlsx(string xlsFilePath, string xlsxFilePath)
        //{
        //    Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
        //    excelApp.Visible = false;

        //    Microsoft.Office.Interop.Excel.Workbook eWorkbook = excelApp.Workbooks.Open(xlsFilePath, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
        //    eWorkbook.SaveAs(xlsxFilePath, Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
        //    eWorkbook.Close(false, Type.Missing, Type.Missing);
        //}
        #endregion
        #region AsignarEpp
        public ActionResult AsignarEPP()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }
            List<EDEPP> ListaEPP = new List<EDEPP>();
            List<EDCargo> ListaCargos = new List<EDCargo>();
            List<EDProceso> ListaProcesos = new List<EDProceso>();
            List<EDSede> ListaSedes = new List<EDSede>();
            ListaSedes = LNEmpresa.ObtenerSedesPorNit(usuarioActual.NitEmpresa);
            ListaProcesos = LNProcesos.ObtenerProcesosPorEmpresa(usuarioActual.NitEmpresa);
            ListaProcesos = ListaProcesos.Where(s => s.Id_Proceso_Padre != null).ToList();
            List<EDCargo> ListaEDCargo = new List<EDCargo>();
            try
            {
                ListaEDCargo = ConsultarCargos(usuarioActual.NitEmpresa);
                if (ListaEDCargo.Count == 0)
                {
                    ListaEDCargo = LNEPP.ListaCargos();
                }
            }
            catch (Exception)
            {
                ListaEDCargo = LNEPP.ListaCargos();
            }
            ViewBag.Pk_Cargos = new SelectList(ListaEDCargo, "IDCargo", "NombreCargo", null);
            ViewBag.Pk_Procesos = new SelectList(ListaProcesos, "Id_Proceso", "Descripcion", null);
            ViewBag.Pk_Sede = new SelectList(ListaSedes, "IdSede", "NombreSede", null);
            ViewBag.Pk_EPP = new SelectList(ListaEPP, "Pk_Id_EPP", "NombreEPP", null);
            return View();
        }
        [HttpPost]
        public ActionResult GuardarAsignarEPP()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }

            List<EDCargo> ListaCargos = new List<EDCargo>();
            List<EDProceso> ListaProcesos = new List<EDProceso>();
            List<EDSede> ListaSede = new List<EDSede>();

            ViewBag.Pk_Cargos = new SelectList(ListaCargos, "IDCargo", "NombreCargo", null);
            ViewBag.Pk_Procesos = new SelectList(ListaProcesos, "Id_Proceso", "Descripcion", null);
            ViewBag.Pk_Sede = new SelectList(ListaSede, "IdSede", "NombreSede", null);



            return View();
        }
        [HttpPost]
        public JsonResult BuscarPersonaDocumentoCargo(string documento)
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
            string resultado_n = string.Empty;
            string resultado_c = string.Empty;


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
                                    resultado_n = afiliado.Nombre1 + " " + afiliado.Nombre2 + " " + afiliado.Apellido1 + " " + afiliado.Apellido2;
                                    if (afiliado.Ocupacion != null)
                                    {
                                        string valorcargo = afiliado.Ocupacion;
                                        valorcargo = afiliado.Ocupacion.Replace("á", "A");
                                        valorcargo = afiliado.Ocupacion.Replace("é", "E");
                                        valorcargo = afiliado.Ocupacion.Replace("í", "I");
                                        valorcargo = afiliado.Ocupacion.Replace("ó", "O");
                                        valorcargo = afiliado.Ocupacion.Replace("ú", "U");
                                        valorcargo = afiliado.Ocupacion.Replace("Á", "A");
                                        valorcargo = afiliado.Ocupacion.Replace("É", "E");
                                        valorcargo = afiliado.Ocupacion.Replace("Í", "I");
                                        valorcargo = afiliado.Ocupacion.Replace("Ó", "O");
                                        valorcargo = afiliado.Ocupacion.Replace("Ú", "U");
                                        resultado_c = valorcargo;
                                    }




                                    probar = true;
                                    return Json(new { resultado_n, resultado_c, probar }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }
                }



                if (resultado_n == string.Empty)
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
                                        resultado_n = afiliado.Nombre1 + " " + afiliado.Nombre2 + " " + afiliado.Apellido1 + " " + afiliado.Apellido2;
                                        if (afiliado.Ocupacion != null)
                                        {
                                            string valorcargo = afiliado.Ocupacion;
                                            valorcargo = afiliado.Ocupacion.Replace("á", "A");
                                            valorcargo = afiliado.Ocupacion.Replace("é", "E");
                                            valorcargo = afiliado.Ocupacion.Replace("í", "I");
                                            valorcargo = afiliado.Ocupacion.Replace("ó", "O");
                                            valorcargo = afiliado.Ocupacion.Replace("ú", "U");
                                            valorcargo = afiliado.Ocupacion.Replace("Á", "A");
                                            valorcargo = afiliado.Ocupacion.Replace("É", "E");
                                            valorcargo = afiliado.Ocupacion.Replace("Í", "I");
                                            valorcargo = afiliado.Ocupacion.Replace("Ó", "O");
                                            valorcargo = afiliado.Ocupacion.Replace("Ú", "U");
                                            resultado_c = valorcargo;
                                        }
                                        probar = true;
                                        return Json(new { resultado_n, resultado_c, probar }, JsonRequestBehavior.AllowGet);
                                    }
                                }
                            }

                        }
                    }
                    return Json(new { resultado_n, resultado_c, probar }, JsonRequestBehavior.AllowGet);
                }
                else
                    return Json(new { resultado_n, resultado_c, probar }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { resultado_n, resultado_c, probar }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult EPPporCargo(string IdCargo)
        {
            List<EDEPP> ListaEPPCargo = new List<EDEPP>();
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return Json(JsonRequestBehavior.AllowGet);
            }
            int IdIntCargo = 0;
            if (int.TryParse(IdCargo, out IdIntCargo))
            {
                ListaEPPCargo = LNEPP.ConsultaMatrizEppCargo(IdIntCargo, usuarioActual.IdEmpresa);
            }
            return Json(new { ListaEPPCargo });
        }
        [HttpPost]
        public ActionResult GuardarControlSuministro(EPPSuministro GuardarSuministro, List<EDEPPSuministroEPP> ListaSuministrosEPP)
        {

            bool Probar = false;
            string Estado = "No se guardó el control de suministro, por favor revise la información suministrada";
            bool[] Validacion = new bool[5];
            string[] ValidacionStr = new string[5];
            for (int i = 0; i < 5; i++)
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
                EPPSuministro EPPSuministro = new EPPSuministro();
                EPPSuministro = GuardarSuministro;

                EPPSuministro.Fk_Id_Empresa = usuarioActual.IdEmpresa;
                EDEPPSuministro EDEPPSuministro = new EDEPPSuministro();
                EDEPPSuministro.CedulaTrabajador = EPPSuministro.CedulaTrabajador;
                EDEPPSuministro.NombreTrabajador = EPPSuministro.NombreTrabajador;
                EDEPPSuministro.Fk_Id_Cargo = EPPSuministro.Fk_Id_Cargo;
                EDEPPSuministro.Fk_Id_Proceso = EPPSuministro.Fk_Id_Proceso;
                EDEPPSuministro.Fk_Id_Sede = EPPSuministro.Fk_Id_Sede;
                EDEPPSuministro.Fk_Id_Empresa = EPPSuministro.Fk_Id_Empresa;
                bool ProbarGuardado = LNEPP.GuardarControlSuministro(EDEPPSuministro, ListaSuministrosEPP);
                if (ProbarGuardado)
                {

                    Probar = true;
                    EDEPPSuministro EDEPPSuministro_mostrar = LNEPP.UltimoSuministro(usuarioActual.IdEmpresa);
                    int IdUltimoRegistro = 0;
                    if (EDEPPSuministro_mostrar.Pk_Id_SuministroEPP != 0)
                    {
                        IdUltimoRegistro = EDEPPSuministro_mostrar.Pk_Id_SuministroEPP;
                    }
                    return Json(new { Estado, Probar, IdUltimoRegistro });
                }
            }
            for (int i = 0; i < 5; i++)
            {
                Validacion[i] = false;
                ValidacionStr[i] = "";
            }
            int cont = 0;

            foreach (var kvp in ModelState)
            {
                var key = kvp.Key;
                cont++;
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
                if (key == "GuardarSuministro.CedulaTrabajador")
                {
                    ListaErroresSalida[cont] = 0;
                    ListaErroresSalidabool[cont] = true;
                }
                if (key == "GuardarSuministro.NombreTrabajador")
                {
                    ListaErroresSalida[cont] = 1;
                    ListaErroresSalidabool[cont] = true;
                }
                if (key == "GuardarSuministro.Fk_Id_Proceso")
                {
                    ListaErroresSalida[cont] = 2;
                    ListaErroresSalidabool[cont] = true;
                }
                if (key == "GuardarSuministro.Fk_Id_Sede")
                {
                    ListaErroresSalida[cont] = 3;
                    ListaErroresSalidabool[cont] = true;
                }
                if (key == "GuardarSuministro.Fk_Id_Cargo")
                {
                    ListaErroresSalida[cont] = 4;
                    ListaErroresSalidabool[cont] = true;
                }

                cont++;
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
            var Model = GuardarSuministro;
            return Json(new { Model, Estado, Probar, Validacion, ValidacionStr }, JsonRequestBehavior.AllowGet);
        }


        #endregion
        #region ConsultaMatriz
        [HttpGet]
        public ActionResult MatrizEPP()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }
            ViewBag.MensajeBusqueda = "";
            List<EDEPP> Lista = new List<EDEPP>();
            List<EDTipoDePeligro> ListaTipoPeligros = LNMetodologia.ObtenerTiposDePeligro();
            List<EDClasificacionDePeligro> ListaClasPeligros = new List<EDClasificacionDePeligro>();
            if (ListaTipoPeligros != null)
            {
                foreach (var item in ListaTipoPeligros)
                {
                    string DescripcionTipo = item.Descripcion_Del_Peligro;
                    List<EDClasificacionDePeligro> ListaClasPeligro = new List<EDClasificacionDePeligro>();
                    ListaClasPeligro = LNPeligro.ObtenerClasificaciónPorTipo(item.PK_Tipo_De_Peligro);
                    foreach (var item1 in ListaClasPeligro)
                    {
                        string DescripcionClasePeligro = DescripcionTipo + " - " + item1.DescripcionClaseDePeligro;
                        item1.DescripcionClaseDePeligro = DescripcionClasePeligro;
                        ListaClasPeligros.Add(item1);
                    }
                }

            }
            ViewBag.Pk_Id_Clasif_Peligro = new SelectList(ListaClasPeligros, "IdClasificacionDePeligro", "DescripcionClaseDePeligro", null);
            List<EDCargo> ListaEDCargo = new List<EDCargo>();
            try
            {
                ListaEDCargo = ConsultarCargos(usuarioActual.NitEmpresa);
                if (ListaEDCargo.Count == 0)
                {
                    ListaEDCargo = LNEPP.ListaCargos();
                }
            }
            catch (Exception)
            {
                ListaEDCargo = LNEPP.ListaCargos();
            }
            ViewBag.Pk_Id_Cargo = new SelectList(ListaEDCargo, "IDCargo", "NombreCargo", null);
            return View(Lista);
        }
        [HttpPost]
        public ActionResult MatrizEPP(FormCollection frm)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }
            List<EDEPP> ListaEDEPP = new List<EDEPP>();
            string Nombre = "";
            int IdClasPel = 0;
            int IdCargo = 0;

            if (frm["NombreEPP_C"] != null)
            {
                Nombre = frm["NombreEPP_C"].ToString();
            }
            if (frm["Pk_Id_Clasif_Peligro"] != null)
            {
                var pel = frm["Pk_Id_Clasif_Peligro"];
                if (pel != null)
                {
                    if (int.TryParse(pel.ToString(), out IdClasPel))
                    {
                    }
                }
            }
            if (frm["Pk_Id_Cargo"] != null)
            {
                var car = frm["Pk_Id_Cargo"];
                if (car != null)
                {
                    if (int.TryParse(car.ToString(), out IdCargo))
                    {
                    }
                }
            }
            ListaEDEPP = LNEPP.ConsultaMatrizEpp(Nombre, IdClasPel, IdCargo, usuarioActual.IdEmpresa);

            List<EDTipoDePeligro> ListaTipoPeligros = LNMetodologia.ObtenerTiposDePeligro();
            List<EDClasificacionDePeligro> ListaClasPeligros = new List<EDClasificacionDePeligro>();
            if (ListaTipoPeligros != null)
            {
                foreach (var item in ListaTipoPeligros)
                {
                    string DescripcionTipo = item.Descripcion_Del_Peligro;
                    List<EDClasificacionDePeligro> ListaClasPeligro = new List<EDClasificacionDePeligro>();
                    ListaClasPeligro = LNPeligro.ObtenerClasificaciónPorTipo(item.PK_Tipo_De_Peligro);
                    foreach (var item1 in ListaClasPeligro)
                    {
                        string DescripcionClasePeligro = DescripcionTipo + " - " + item1.DescripcionClaseDePeligro;
                        item1.DescripcionClaseDePeligro = DescripcionClasePeligro;
                        ListaClasPeligros.Add(item1);
                    }
                }

            }
            ViewBag.Pk_Id_Clasif_Peligro = new SelectList(ListaClasPeligros, "IdClasificacionDePeligro", "DescripcionClaseDePeligro", null);
            List<EDCargo> ListaEDCargo = new List<EDCargo>();
            try
            {
                ListaEDCargo = ConsultarCargos(usuarioActual.NitEmpresa);
                if (ListaEDCargo.Count == 0)
                {
                    ListaEDCargo = LNEPP.ListaCargos();
                }
            }
            catch (Exception)
            {
                ListaEDCargo = LNEPP.ListaCargos();
            }
            ViewBag.Pk_Id_Cargo = new SelectList(ListaEDCargo, "IDCargo", "NombreCargo", null);

            if (ListaEDEPP.Count == 0)
            {
                ViewBag.MensajeBusqueda = "No hay EPP que cumplan con los criterios de busqueda";
            }
            else
            {
                ViewBag.MensajeBusqueda = "";
            }

            foreach (var item in ListaEDEPP)
            {
                if (item.Fk_Id_Clasificacion_De_Peligro != 0)
                {
                    var peligro = (from pel in ListaClasPeligros
                                   where pel.IdClasificacionDePeligro == item.Fk_Id_Clasificacion_De_Peligro
                                   select pel.DescripcionClaseDePeligro).FirstOrDefault();

                    item.Clasificacion_De_Peligro = peligro;
                }
            }

            foreach (var item in ListaEDEPP)
            {
                if (item.ArchivoImagen != null && item.RutaImage != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(Path.Combine(item.RutaImage, item.ArchivoImagen))))
                    {
                        string Ruta = item.RutaImage.Replace("~", "") + item.ArchivoImagen; ;
                        item.RutaAbsolutaImagen = Ruta;
                    }
                }
            }
            foreach (var item in ListaEDEPP)
            {
                if (item.EspecificacionTecnica == "-- Información no Disponible --")
                {
                    item.EspecificacionTecnica = "";
                }
                if (item.Uso == "-- Información no Disponible --")
                {
                    item.Uso = "";
                }
                if (item.Mantenimiento == "-- Información no Disponible --")
                {
                    item.Mantenimiento = "";
                }
                if (item.VidaUtil == "-- Información no Disponible --")
                {
                    item.VidaUtil = "";
                }
                if (item.Reposicion == "-- Información no Disponible --")
                {
                    item.Reposicion = "";
                }
                if (item.DisposicionFinal == "-- Información no Disponible --")
                {
                    item.DisposicionFinal = "";
                }
            }


            return View(ListaEDEPP);
        }
        [HttpPost]
        public ActionResult DescargarArchivo(string IdEPP)
        {
            EDEPP EDEPP = new EDEPP();
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }
            int IdEHMInt = 0;
            if (int.TryParse(IdEPP, out IdEHMInt))
            {
                EDEPP = LNEPP.ConsultarEPPDownload(IdEHMInt, usuarioActual.IdEmpresa);
                if (EDEPP.NombreArchivo != null && EDEPP.NombreArchivo_download != null && EDEPP.RutaArchivo != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(Path.Combine(EDEPP.RutaArchivo, EDEPP.NombreArchivo))))
                    {
                        byte[] fileBytes = System.IO.File.ReadAllBytes(Server.MapPath(Path.Combine(EDEPP.RutaArchivo, EDEPP.NombreArchivo)));
                        return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, EDEPP.NombreArchivo_download);
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }

            return null;
        }
        [HttpPost]
        public JsonResult EliminarEPP(string IdEPP)
        {
            bool probar = false;
            string resultado = "El EPP no ha podido ser eliminado";

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                resultado = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
            }

            int IdElemento = 0;
            bool probarNumero = int.TryParse(IdEPP, out IdElemento);
            if (IdElemento != 0)
            {
                EDEPP EDEPP = LNEPP.ConsultarEPP(IdElemento, usuarioActual.IdEmpresa);
                List<string> ListaArchivos = new List<string>();
                try
                {
                    ListaArchivos.Add(Server.MapPath(Path.Combine(EDEPP.RutaArchivo, EDEPP.NombreArchivo)));
                }
                catch (Exception)
                {

                }
                try
                {
                    ListaArchivos.Add(Server.MapPath(Path.Combine(EDEPP.RutaImage, EDEPP.ArchivoImagen)));
                }
                catch (Exception)
                {

                }

                if (LNEPP.ComprobarAsignacionEPP(IdElemento, usuarioActual.IdEmpresa))
                {
                    probar = false;
                    resultado = "El EPP no se ha eliminado. Existen asignaciones de este elemento de protección personal a personas, compruebe que el elemento no tenga asignaciones y vuelva a intentar";
                    return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
                }
                bool BorraElemento = LNEPP.EliminarEPP(IdElemento, usuarioActual.IdEmpresa);
                if (BorraElemento == false)
                {
                    return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
                }
                foreach (var item in ListaArchivos)
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
                probar = true;
                resultado = "El EPP se ha eliminado correctamente";
                return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region ConsultaAsignacion
        [HttpGet]
        public ActionResult ConsultarAsignacion()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }

            List<EDEPPSuministro> ListaSuministro = new List<EDEPPSuministro>();
            //Cargar en el DropDown la lista de tipos de opciones

            List<EDTipoDePeligro> ListaTipoPeligros = LNMetodologia.ObtenerTiposDePeligro();
            List<EDClasificacionDePeligro> ListaClasPeligros = new List<EDClasificacionDePeligro>();
            if (ListaTipoPeligros != null)
            {
                foreach (var item in ListaTipoPeligros)
                {
                    string DescripcionTipo = item.Descripcion_Del_Peligro;
                    List<EDClasificacionDePeligro> ListaClasPeligro = new List<EDClasificacionDePeligro>();
                    ListaClasPeligro = LNPeligro.ObtenerClasificaciónPorTipo(item.PK_Tipo_De_Peligro);
                    foreach (var item1 in ListaClasPeligro)
                    {
                        string DescripcionClasePeligro = DescripcionTipo + " - " + item1.DescripcionClaseDePeligro;
                        item1.DescripcionClaseDePeligro = DescripcionClasePeligro;
                        ListaClasPeligros.Add(item1);
                    }
                }

            }


            List<EDSede> ListaSedes = new List<EDSede>();
            ListaSedes = LNEmpresa.ObtenerSedesPorNit(usuarioActual.NitEmpresa);
            List<EDCargo> ListaEDCargo = new List<EDCargo>();
            try
            {
                ListaEDCargo = ConsultarCargos(usuarioActual.NitEmpresa);
                if (ListaEDCargo.Count == 0)
                {
                    ListaEDCargo = LNEPP.ListaCargos();
                }
            }
            catch (Exception)
            {
                ListaEDCargo = LNEPP.ListaCargos();
            }
            ViewBag.Pk_Cargos = new SelectList(ListaEDCargo, "IDCargo", "NombreCargo", null);
            ViewBag.RiesgoBusqueda = new SelectList(ListaClasPeligros, "IdClasificacionDePeligro", "DescripcionClaseDePeligro", null);
            ViewBag.Pk_Sede = new SelectList(ListaSedes, "IdSede", "NombreSede", null);

            ViewBag.val_fecha1 = "";
            ViewBag.val_fecha2 = "";
            ViewBag.Cedula = "";
            return View(ListaSuministro);
        }
        [HttpPost]
        public ActionResult ConsultarAsignacion(FormCollection frm)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }
            List<EDEPPSuministro> ListaSuministro = new List<EDEPPSuministro>();
            string FechaAntes = "";
            string FechaDespues = "";
            int Cargo = 0;
            string Cedula = "";
            int Riesgo = 0;
            int Sede = 0;

            ViewBag.val_fecha1 = "";
            ViewBag.val_fecha2 = "";
            ViewBag.Cedula = "";

            if (frm["FechaAntes"] != null)
            {
                FechaAntes = frm["FechaAntes"].ToString();
                ViewBag.val_fecha1 = frm["FechaAntes"].ToString();
            }
            if (frm["FechaDespues"] != null)
            {
                FechaDespues = frm["FechaDespues"].ToString();
                ViewBag.val_fecha2 = frm["FechaDespues"].ToString();
            }

            DateTime FechaA_conv = DateTime.MinValue;
            DateTime FechaD_conv = DateTime.MinValue;
            if (FechaAntes != null && FechaDespues != null)
            {
                if (FechaAntes != string.Empty && FechaDespues != string.Empty)
                {
                    try
                    {
                        FechaA_conv = DateTime.Parse(FechaAntes);
                        FechaD_conv = DateTime.Parse(FechaDespues);
                        if (FechaA_conv > FechaD_conv)
                        {
                            FechaAntes = FechaD_conv.ToString();
                            FechaDespues = FechaA_conv.ToString();
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }

            if (frm["Pk_Cargos"] != null)
            {
                var car = frm["Pk_Cargos"];
                if (car != null)
                {
                    if (int.TryParse(car.ToString(), out Cargo))
                    {
                    }
                }
            }
            if (frm["DocumentoBusqueda"] != null)
            {
                Cedula = frm["DocumentoBusqueda"].ToString();
                ViewBag.Cedula = frm["DocumentoBusqueda"].ToString();
            }
            if (frm["RiesgoBusqueda"] != null)
            {
                var pel = frm["RiesgoBusqueda"];
                if (pel != null)
                {
                    if (int.TryParse(pel.ToString(), out Riesgo))
                    {
                    }
                }
            }
            if (frm["Pk_Sede"] != null)
            {
                var sed = frm["Pk_Sede"];
                if (sed != null)
                {
                    if (int.TryParse(sed.ToString(), out Sede))
                    {
                    }
                }
            }

            ListaSuministro = LNEPP.ConsultaListaAsignacion(FechaAntes, FechaDespues, Cargo, Cedula, Riesgo, Sede, usuarioActual.IdEmpresa);
            List<EDTipoDePeligro> ListaTipoPeligros = LNMetodologia.ObtenerTiposDePeligro();
            List<EDClasificacionDePeligro> ListaClasPeligros = new List<EDClasificacionDePeligro>();
            if (ListaTipoPeligros != null)
            {
                foreach (var item in ListaTipoPeligros)
                {
                    string DescripcionTipo = item.Descripcion_Del_Peligro;
                    List<EDClasificacionDePeligro> ListaClasPeligro = new List<EDClasificacionDePeligro>();
                    ListaClasPeligro = LNPeligro.ObtenerClasificaciónPorTipo(item.PK_Tipo_De_Peligro);
                    foreach (var item1 in ListaClasPeligro)
                    {
                        string DescripcionClasePeligro = DescripcionTipo + " - " + item1.DescripcionClaseDePeligro;
                        item1.DescripcionClaseDePeligro = DescripcionClasePeligro;
                        ListaClasPeligros.Add(item1);
                    }
                }
            }

            List<EDSede> ListaSedes = new List<EDSede>();
            List<EDProceso> ListaProcesos = LNProcesos.ObtenerProcesosPorEmpresa(usuarioActual.NitEmpresa);
            ListaSedes = LNEmpresa.ObtenerSedesPorNit(usuarioActual.NitEmpresa);
            List<EDCargo> ListaEDCargo = new List<EDCargo>();
            try
            {
                ListaEDCargo = ConsultarCargos(usuarioActual.NitEmpresa);
                if (ListaEDCargo.Count == 0)
                {
                    ListaEDCargo = LNEPP.ListaCargos();
                }
            }
            catch (Exception)
            {
                ListaEDCargo = LNEPP.ListaCargos();
            }
            ViewBag.Pk_Cargos = new SelectList(ListaEDCargo, "IDCargo", "NombreCargo", Cargo);
            ViewBag.RiesgoBusqueda = new SelectList(ListaClasPeligros, "IdClasificacionDePeligro", "DescripcionClaseDePeligro", Riesgo);
            ViewBag.Pk_Sede = new SelectList(ListaSedes, "IdSede", "NombreSede", Sede);

            foreach (var item in ListaSuministro)
            {
                EDCargo EDCargo = LNEPP.ListaCargos().Where(s => s.IDCargo == item.Fk_Id_Cargo).FirstOrDefault();
                EDSede EDSede = ListaSedes.Where(s => s.IdSede == item.Fk_Id_Sede).FirstOrDefault();
                EDProceso EDProceso = ListaProcesos.Where(s => s.Id_Proceso == item.Fk_Id_Proceso).FirstOrDefault();
                item.ProcesoNombre = EDProceso.Descripcion;
                item.SedeNombre = EDSede.NombreSede;
                item.CargoNombre = EDCargo.NombreCargo;
            }



            return View(ListaSuministro);
        }
        [HttpPost]
        public JsonResult EliminarAsignacionEPP(string IdAsigEPP)
        {
            bool probar = false;
            string resultado = "La asignación del EPP no ha podido ser eliminada";

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                resultado = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
            }

            int IdAsignacion = 0;
            bool probarNumero = int.TryParse(IdAsigEPP, out IdAsignacion);
            if (IdAsignacion != 0)
            {
                EDEPP EDEPP = LNEPP.ConsultarEPP(IdAsignacion, usuarioActual.IdEmpresa);
                bool BorraElemento = LNEPP.EliminarAsigEPP(IdAsignacion, usuarioActual.IdEmpresa);
                if (BorraElemento == false)
                {
                    return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
                }

                probar = true;
                resultado = "La asignación de EPP se ha eliminado correctamente";
                return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Peligros
        [HttpPost]
        public JsonResult ConsultarClasPorTipo(string IdTipoPeligro)
        {
            bool Probar = false;
            List<string> ListaValor = new List<string>();
            List<string> ListaTexto = new List<string>();

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
            }
            int IdTipoPeligroint = 0;
            if (!int.TryParse(IdTipoPeligro, out IdTipoPeligroint))
            {
                return Json(new { Probar, ListaValor, ListaTexto }, JsonRequestBehavior.AllowGet);
            }
            List<EDClasificacionDePeligro> ObjEmp2 = new List<EDClasificacionDePeligro>();
            int IdClas = IdTipoPeligroint;
            if (IdClas != 0)
            {
                ObjEmp2 = LNPeligro.ObtenerClasificaciónPorTipo(IdClas);
                foreach (var item in ObjEmp2)
                {
                    Probar = true;
                    ListaValor.Add(item.IdClasificacionDePeligro.ToString());
                    ListaTexto.Add(item.DescripcionClaseDePeligro);
                }
            }
            return Json(new { Probar, ListaValor, ListaTexto }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Funciones
        static double ConvertBytesToMegabytes(long bytes)
        {
            return (bytes / 1024f) / 1024f;
        }
        private void CrearCarpeta(string path)
        {
            bool existeCarpeta = Directory.Exists(Server.MapPath(path));
            if (!existeCarpeta)
            {
                Directory.CreateDirectory(Server.MapPath(path));
            }

        }
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        private List<EDCargo> ConsultarCargos(string Nit)
        {
            List<EDCargo> ListaEDCargo = new List<EDCargo>();
            List<EDCargo> ListaEDCargoMaestro = LNEPP.ListaCargos();

            if (Session["CargosUsuario"] == null)
            {
                Session["CargosUsuario"] = ListaCargosWS(Nit, ListaEDCargoMaestro);
                try
                {
                    ListaEDCargo = (List<EDCargo>)Session["CargosUsuario"];
                }
                catch (Exception)
                {

                }

            }
            else
            {
                List<EDCargo> RevisarLista = new List<EDCargo>();
                try
                {
                    RevisarLista = (List<EDCargo>)Session["CargosUsuario"];
                    if (RevisarLista.Count == 0)
                    {
                        Session["CargosUsuario"] = ListaCargosWS(Nit, ListaEDCargoMaestro);
                        try
                        {
                            ListaEDCargo = (List<EDCargo>)Session["CargosUsuario"];
                        }
                        catch (Exception)
                        {

                        }
                    }
                    else
                    {
                        ListaEDCargo = RevisarLista;
                    }
                }
                catch (Exception)
                {
                    Session["CargosUsuario"] = ListaCargosWS(Nit, ListaEDCargoMaestro);
                    try
                    {
                        ListaEDCargo = (List<EDCargo>)Session["CargosUsuario"];
                    }
                    catch (Exception)
                    {

                    }
                }
            }
            if (ListaEDCargo.Count > 0)
            {
                ListaEDCargo = ListaEDCargo.OrderBy(s => s.NombreCargo).ToList();
                ListaEDCargo = ListaEDCargo.Distinct().ToList();
            }
            return ListaEDCargo;
        }
        private List<EDCargo> ListaCargosWS(string NIT, List<EDCargo> ListaEDCargoMaestro)
        {
            List<string> ListaNombresCargosWS = new List<string>();
            List<string> NuevaListaCargos = new List<string>();
            List<EDCargo> NuevaListaCargosMaestro = new List<EDCargo>();
            List<EDCargo> NuevaListaCargosMaestro1 = new List<EDCargo>();
            foreach (var item in ListaEDCargoMaestro)
            {
                EDCargo EDCargo = new EDCargo();
                EDCargo.IDCargo = item.IDCargo;
                EDCargo.NombreCargo = item.NombreCargo;
                NuevaListaCargosMaestro1.Add(EDCargo);
            }
            string[] ListaEstados = new string[2] { "1", "2" };
            string[] ListaVinc = new string[2] { "1", "2" };

            for (int i = 0; i < 2; i++)
            {
                for (int i1 = 0; i1 < 2; i1++)
                {
                    try
                    {
                        var cliente = new RestSharp.RestClient(ConfigurationManager.AppSettings["Url"]);
                        var request = new RestRequest(ConfigurationManager.AppSettings["consultaEstadoTipoAfiliado"], RestSharp.Method.GET);
                        request.RequestFormat = DataFormat.Xml;
                        var usuario = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
                        request.Parameters.Clear();
                        request.AddParameter("tpEm", "ni");
                        request.AddParameter("docEm", NIT);
                        request.AddParameter("estadoAfi", ListaEstados[i]);
                        request.AddParameter("TipoVinAfi", ListaVinc[i1]);
                        request.AddHeader("Content-Type", "application/json");
                        request.AddHeader("Accept", "application/json");

                        ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

                        IRestResponse<List<EstadoTipoAfiliadoDTO>> response = cliente.Execute<List<EstadoTipoAfiliadoDTO>>(request);
                        var result = response.Content;

                        var respuesta = Newtonsoft.Json.JsonConvert.DeserializeObject<List<EstadoTipoAfiliadoDTO>>(result);
                        if (respuesta != null)
                        {
                            foreach (var item in respuesta)
                            {
                                if (item != null)
                                {
                                    if (item.cargo != null)
                                    {
                                        ListaNombresCargosWS.Add(item.cargo);
                                    }
                                }
                            }
                        }
                        var ListaNombresCargosNoDuples = ListaNombresCargosWS.Distinct();
                        if (ListaNombresCargosNoDuples != null)
                        {
                            foreach (var item in ListaNombresCargosNoDuples)
                            {
                                string textoNormalizado = item.Normalize(NormalizationForm.FormD);
                                string textoSinAcentos = Regex.Replace(textoNormalizado, @"[^a-zA-z0-9 ]+", "");
                                textoSinAcentos = textoSinAcentos.Replace(" ", "");
                                NuevaListaCargos.Add(textoSinAcentos);
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }

            //Comparar con maestros
            foreach (var item in ListaEDCargoMaestro)
            {
                string textoNormalizado = item.NombreCargo.Normalize(NormalizationForm.FormD);
                string textoSinAcentos = Regex.Replace(textoNormalizado, @"[^a-zA-z0-9 ]+", "");
                textoSinAcentos = textoSinAcentos.Replace(" ", "");
                item.NombreCargo = textoSinAcentos;
            }

            foreach (var item in NuevaListaCargos)
            {
                string NombreCargo = item;
                int cont = 0;
                foreach (var item1 in ListaEDCargoMaestro)
                {
                    if (NombreCargo == item1.NombreCargo)
                    {
                        EDCargo EDCargo = new EDCargo();
                        EDCargo = NuevaListaCargosMaestro1.Where(s => s.IDCargo == item1.IDCargo).FirstOrDefault();
                        if (EDCargo != null)
                        {
                            NuevaListaCargosMaestro.Add(EDCargo);
                        }
                    }
                    else
                    {
                        cont++;
                    }
                }
            }
            return NuevaListaCargosMaestro;
        }

        private List<EDCargo> ListaCargosWS1(string NIT, List<EDCargo> ListaEDCargoMaestro)
        {
            List<string> ListaNombresCargosWS = new List<string>();
            List<string> NuevaListaCargos = new List<string>();
            List<EDCargo> NuevaListaCargosMaestro = new List<EDCargo>();
            List<EDCargo> NuevaListaCargosMaestro1 = new List<EDCargo>();
            foreach (var item in ListaEDCargoMaestro)
            {
                EDCargo EDCargo = new EDCargo();
                EDCargo.IDCargo = item.IDCargo;
                EDCargo.NombreCargo = item.NombreCargo;
                NuevaListaCargosMaestro1.Add(EDCargo);
            }
            string[] ListaEstados = new string[2] { "1", "2" };
            string[] ListaVinc = new string[2] { "1", "2" };

            for (int i = 0; i < 2; i++)
            {
                for (int i1 = 0; i1 < 2; i1++)
                {
                    try
                    {
                        var cliente = new RestSharp.RestClient(ConfigurationManager.AppSettings["Url"]);
                        var request = new RestRequest(ConfigurationManager.AppSettings["consultaEstadoTipoAfiliado"], RestSharp.Method.GET);
                        request.RequestFormat = DataFormat.Xml;
                        var usuario = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
                        request.Parameters.Clear();
                        request.AddParameter("tpEm", "ni");
                        request.AddParameter("docEm", NIT);
                        request.AddParameter("estadoAfi", ListaEstados[i]);
                        request.AddParameter("TipoVinAfi", ListaVinc[i1]);
                        request.AddHeader("Content-Type", "application/json");
                        request.AddHeader("Accept", "application/json");

                        ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

                        IRestResponse<List<EstadoTipoAfiliadoDTO>> response = cliente.Execute<List<EstadoTipoAfiliadoDTO>>(request);
                        var result = response.Content;

                        var respuesta = Newtonsoft.Json.JsonConvert.DeserializeObject<List<EstadoTipoAfiliadoDTO>>(result);
                        if (respuesta != null)
                        {
                            foreach (var item in respuesta)
                            {
                                if (item != null)
                                {
                                    if (item.cargo != null)
                                    {
                                        ListaNombresCargosWS.Add(item.cargo);
                                    }
                                }
                            }
                        }
                        var ListaNombresCargosNoDuples = ListaNombresCargosWS.Distinct();
                        if (ListaNombresCargosNoDuples != null)
                        {
                            foreach (var item in ListaNombresCargosNoDuples)
                            {
                                string textoNormalizado = item.Normalize(NormalizationForm.FormD);
                                string textoSinAcentos = Regex.Replace(textoNormalizado, @"[^a-zA-z0-9 ]+", "");
                                textoSinAcentos = textoSinAcentos.Replace(" ", "");
                                NuevaListaCargos.Add(textoSinAcentos);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        EDCargo CargoError = new EDCargo();
                        CargoError.NombreCargo= ex.ToString();
                        NuevaListaCargosMaestro.Add(CargoError);
                    }
                }
            }

            //Comparar con maestros
            foreach (var item in ListaEDCargoMaestro)
            {
                string textoNormalizado = item.NombreCargo.Normalize(NormalizationForm.FormD);
                string textoSinAcentos = Regex.Replace(textoNormalizado, @"[^a-zA-z0-9 ]+", "");
                textoSinAcentos = textoSinAcentos.Replace(" ", "");
                item.NombreCargo = textoSinAcentos;
            }

            foreach (var item in NuevaListaCargos)
            {
                string NombreCargo = item;
                int cont = 0;
                foreach (var item1 in ListaEDCargoMaestro)
                {
                    if (NombreCargo == item1.NombreCargo)
                    {
                        EDCargo EDCargo = new EDCargo();
                        EDCargo = NuevaListaCargosMaestro1.Where(s => s.IDCargo == item1.IDCargo).FirstOrDefault();
                        if (EDCargo != null)
                        {
                            NuevaListaCargosMaestro.Add(EDCargo);
                        }
                    }
                    else
                    {
                        cont++;
                    }
                }
            }
            return NuevaListaCargosMaestro;
        }

        //[HttpPost]
        //public JsonResult BuscarPersonaDocumentoCargo1(string documento)
        //{
        //    var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
        //    if (usuarioActual == null)
        //    {
        //        return Json(JsonRequestBehavior.AllowGet);
        //    }
        //    string nit = usuarioActual.NitEmpresa;
        //    List<EDTipoDocumento> EDTipoDoc = LNAcciones.BuscarTipoDocumento();
        //    List<TipoDocumento> ListaDocumentos = new List<TipoDocumento>();
        //    List<string> ListaDocumentosStr = new List<string>();
        //    foreach (var item in EDTipoDoc)
        //    {
        //        TipoDocumento td = new TipoDocumento();
        //        td.PK_IDTipo_Documento = item.Id_Tipo_Documento;
        //        td.Sigla = item.Sigla;
        //        td.Descripcion = item.Descripcion;
        //        ListaDocumentos.Add(td);
        //        ListaDocumentosStr.Add(td.Descripcion);
        //        ListaDocumentosStr.Add(td.Sigla);
        //    }

        //    //ListaDocumentos = ListaDocumentos.Where(s => s.AplicaPersonas == true).ToList();
        //    string Nit = usuarioActual.NitEmpresa;
        //    string[] resultado = new string[2] { string.Empty, string.Empty };
        //    bool probar = false;

        //    try
        //    {
        //        foreach (var item in ListaDocumentosStr)
        //        {
        //            if (!string.IsNullOrEmpty(documento) && !string.IsNullOrEmpty(item))
        //            {
        //                var cliente = new RestSharp.RestClient(ConfigurationManager.AppSettings["Url"]);
        //                var request = new RestRequest(ConfigurationManager.AppSettings["consultaAfiliado"], RestSharp.Method.GET);
        //                request.RequestFormat = DataFormat.Xml;
        //                request.Parameters.Clear();
        //                request.AddParameter("tpDoc", item.ToString());
        //                request.AddParameter("doc", documento);
        //                request.AddHeader("Content-Type", "application/json");
        //                request.AddHeader("Accept", "application/json");

        //                //se omite la validación de certificado de SSL
        //                ServicePointManager.ServerCertificateValidationCallback = delegate
        //                { return true; };
        //                IRestResponse<List<CargosDTO>> response = cliente.Execute<List<CargosDTO>>(request);
        //                var result = response.Content;
        //                if (!string.IsNullOrWhiteSpace(result))
        //                {
        //                    var respuesta = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CargosDTO>>(result);
        //                    var afiliado = respuesta.Where(a => a.Estado == "Activo").FirstOrDefault();
        //                    if (afiliado == null)
        //                    { }
        //                    else
        //                    {
        //                        if (nit == afiliado.IdEmpresa)
        //                        {
        //                            resultado[0] = afiliado.Nombre1 + " " + afiliado.Nombre2 + " " + afiliado.Apellido1 + " " + afiliado.Apellido2;
        //                            resultado[1] = afiliado.Ocupacion;
        //                            probar = true;
        //                            return Json(new { resultado, probar }, JsonRequestBehavior.AllowGet);
        //                        }
        //                        else
        //                        {
        //                            break;
        //                        }
        //                    }
        //                }
        //            }
        //        }



        //        if (resultado[0] == string.Empty)
        //        {
        //            if (!string.IsNullOrEmpty(documento))
        //            {
        //                var cliente = new RestSharp.RestClient(ConfigurationManager.AppSettings["Url"]);
        //                var request = new RestRequest(ConfigurationManager.AppSettings["consultaAfiliado"], RestSharp.Method.GET);
        //                request.RequestFormat = DataFormat.Xml;
        //                request.Parameters.Clear();
        //                request.AddParameter("tpDoc", "CC");
        //                request.AddParameter("doc", documento);
        //                request.AddHeader("Content-Type", "application/json");
        //                request.AddHeader("Accept", "application/json");

        //                //se omite la validación de certificado de SSL
        //                ServicePointManager.ServerCertificateValidationCallback = delegate
        //                { return true; };
        //                IRestResponse<List<CargosDTO>> response = cliente.Execute<List<CargosDTO>>(request);
        //                var result = response.Content;
        //                if (!string.IsNullOrWhiteSpace(result))
        //                {
        //                    var respuesta = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CargosDTO>>(result);
        //                    if (respuesta.Count != 0)
        //                    {
        //                        var afiliado = respuesta.Where(a => a.Estado == "Activo").FirstOrDefault();
        //                        if (afiliado == null)
        //                        {

        //                        }
        //                        else
        //                        {
        //                            if (nit == afiliado.IdEmpresa)
        //                            {
        //                                resultado[0] = afiliado.Nombre1 + " " + afiliado.Nombre2 + " " + afiliado.Apellido1 + " " + afiliado.Apellido2;
        //                                resultado[1] = afiliado.Ocupacion;
        //                                probar = true;
        //                                return Json(new { resultado, probar }, JsonRequestBehavior.AllowGet);
        //                            }
        //                        }
        //                    }

        //                }
        //            }
        //            return Json(new { resultado, probar }, JsonRequestBehavior.AllowGet);
        //        }
        //        else
        //            return Json(new { resultado, probar }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { resultado, probar }, JsonRequestBehavior.AllowGet);
        //    }
        //}

        #endregion
        #region Imagenes
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
        private static System.Drawing.Image ScaleImage(System.Drawing.Image image, int maxWidth, int maxHeight)
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
        #endregion
        #region Archivos
        [HttpPost]
        public ActionResult UploadArchivo()
        {
            bool probar = false;
            string resultado = "";
            string NombreArchivos = string.Empty;
            string NombreArchivos_short = string.Empty;
            string NombreArchivos_borrar = string.Empty;
            bool display = false;

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
                            string ImgFileName = "EPPTEC_" + DateTime.Now.ToString().Replace(" ", "").Replace(".", "").Replace(":", "").Replace("/", "") + file.FileName;
                            string pathsave = Server.MapPath(Path.Combine(RutaArchivosBDTemp, ImgFileName));
                            file.SaveAs(pathsave);
                            try
                            {
                                string PathOrigen = Server.MapPath(Path.Combine(RutaArchivosBDTemp, NombreArchivos_borrar));
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
                            display = true;
                        }
                        else
                        {
                            probar = false;
                            resultado = "El archivo que se intento subir no es una imagen o no es un archivo soportado, por favor intente de nuevo con otra imagen";
                            return Json(new { probar, resultado });
                        }

                    }
                    probar = true;
                    return Json(new { probar, resultado, NombreArchivos, NombreArchivos_short, display });
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
        public ActionResult EliminarArchivo(string ruta)
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
        #endregion
        #region Reportes
        [AllowAnonymous]
        public ActionResult EPPPDF(string id, string NitEmpresa, int IdEmpresa)
        {
            EDEPP EDEPP = new EDEPP();

            int index = 0;
            string RutaImagen1 = string.Empty;
            string RutaArchivo1 = string.Empty;

            ViewBag.Imagen1E = "display:none";
            ViewBag.Archivo1E = false;

            ViewBag.Imagen1R = "";
            ViewBag.Archivo1R = "";


            ViewBag.ArchivosE = false;

            ViewBag.ClasPel = "";
            ViewBag.DescPel = "";

            int IdEPPInt = 0;
            if (int.TryParse(id, out IdEPPInt))
            {
                EDEPP = LNEPP.ConsultarEPP(IdEPPInt, IdEmpresa);
                #region ImagenesPDF


                if (EDEPP.ArchivoImagen != null && EDEPP.RutaImage != null)
                {
                    RutaImagen1 = Server.MapPath(Path.Combine(EDEPP.RutaImage, EDEPP.ArchivoImagen));
                    if (System.IO.File.Exists(RutaImagen1))
                    {
                        ViewBag.Imagen1E = "display:initial;max-height:800px;";
                        try
                        {
                            Bitmap bitmap;
                            using (var bmpTemp = new Bitmap(RutaImagen1))
                            {
                                bitmap = new Bitmap(bmpTemp);
                            }
                            using (var newImage = ScaleImage(bitmap, 800, 800))
                            {
                                ViewBag.Imagen1R = "data:image/png;base64," + ImageToBase64String(newImage, ImageFormat.Png);
                            }
                        }
                        catch (Exception)
                        {
                        }

                    }
                }
                #endregion
                #region Peligros
                List<EDTipoDePeligro> ListaTipoPeligros = LNMetodologia.ObtenerTiposDePeligro();
                List<EDClasificacionDePeligro> ListaClasPeligros = new List<EDClasificacionDePeligro>();
                if (ListaTipoPeligros != null)
                {
                    foreach (var item in ListaTipoPeligros)
                    {
                        string DescripcionTipo = item.Descripcion_Del_Peligro;
                        List<EDClasificacionDePeligro> ListaClasPeligro = new List<EDClasificacionDePeligro>();
                        ListaClasPeligro = LNPeligro.ObtenerClasificaciónPorTipo(item.PK_Tipo_De_Peligro);
                        foreach (var item1 in ListaClasPeligro)
                        {
                            string DescripcionClasePeligro = DescripcionTipo + " - " + item1.DescripcionClaseDePeligro;
                            item1.DescripcionClaseDePeligro = DescripcionClasePeligro;
                            ListaClasPeligros.Add(item1);
                        }
                    }
                }
                var peligro = (from pel in ListaClasPeligros
                               where pel.IdClasificacionDePeligro == EDEPP.Fk_Id_Clasificacion_De_Peligro
                               select pel.DescripcionClaseDePeligro).FirstOrDefault();
                if (peligro != null)
                {
                    EDEPP.Clasificacion_De_Peligro = peligro;
                }
                #endregion

            }
            if (index != 0)
            {
                //List<EDPeligro> ObjEmp = new List<EDPeligro>();
                //EDClasificacionDePeligro ClasPel = noDuplicados.Where(s => s.IdClasificacionDePeligro == index).FirstOrDefault();
                //ObjEmp = LNPeligro.ObtenerPeligrosPorClaseyEmpresa(ClasPel.IdClasificacionDePeligro, IdEmpresa);
                //EDAdmoEMH.ListaPeligros = ObjEmp;
                //string ClasificacionDePeligro = LNPeligro.ObtenerClasificación(ClasPel.IdClasificacionDePeligro);
                //ViewBag.ClasPel = ClasPel.DescripcionClaseDePeligro;
                //ViewBag.DescPel = ClasificacionDePeligro;
            }


            return View(EDEPP);
        }
        [AllowAnonymous]
        public ActionResult MatrizEPPPDF(string id, string NitEmpresa, int IdEmpresa)
        {
            List<EDEPP> ListaEPP = new List<EDEPP>();
            string[] EPPArray = id.Split('@');
            foreach (var item in EPPArray)
            {
                int IdintEPP = 0;
                if (int.TryParse(item, out IdintEPP))
                {
                    EDEPP EDEPP = LNEPP.ConsultarEPP(IdintEPP, IdEmpresa);
                    ListaEPP.Add(EDEPP);
                }
            }
            List<EDTipoDePeligro> ListaTipoPeligros = LNMetodologia.ObtenerTiposDePeligro();
            List<EDClasificacionDePeligro> ListaClasPeligros = new List<EDClasificacionDePeligro>();
            if (ListaTipoPeligros != null)
            {
                foreach (var item in ListaTipoPeligros)
                {
                    string DescripcionTipo = item.Descripcion_Del_Peligro;
                    List<EDClasificacionDePeligro> ListaClasPeligro = new List<EDClasificacionDePeligro>();
                    ListaClasPeligro = LNPeligro.ObtenerClasificaciónPorTipo(item.PK_Tipo_De_Peligro);
                    foreach (var item1 in ListaClasPeligro)
                    {
                        string DescripcionClasePeligro = DescripcionTipo + " - " + item1.DescripcionClaseDePeligro;
                        item1.DescripcionClaseDePeligro = DescripcionClasePeligro;
                        ListaClasPeligros.Add(item1);
                    }
                }

            }
            foreach (var item in ListaEPP)
            {
                if (item.Fk_Id_Clasificacion_De_Peligro != 0)
                {
                    var peligro = (from pel in ListaClasPeligros
                                   where pel.IdClasificacionDePeligro == item.Fk_Id_Clasificacion_De_Peligro
                                   select pel.DescripcionClaseDePeligro).FirstOrDefault();

                    item.Clasificacion_De_Peligro = peligro;
                }
            }
            foreach (var item in ListaEPP)
            {
                if (item.ArchivoImagen != null && item.RutaImage != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(Path.Combine(item.RutaImage, item.ArchivoImagen))))
                    {
                        string Ruta = item.RutaImage.Replace("~", "") + item.ArchivoImagen; ;
                        item.RutaAbsolutaImagen = Ruta;
                    }
                }
            }
            return View(ListaEPP);
        }
        [AllowAnonymous]
        public ActionResult AsignacionEPPPDF(string id, string NitEmpresa, int IdEmpresa)
        {
            int IdInt = 0;
            if (int.TryParse(id, out IdInt))
            {
            }
            EDEPPSuministro EPPSuministro = new EDEPPSuministro();
            EPPSuministro = LNEPP.ConsultaListaAsignacionId(IdInt, IdEmpresa);
            List<EDCargo> ListaCargos = new List<EDCargo>();
            List<EDSede> ListaSedes = new List<EDSede>();
            List<EDProceso> ListaProcesos = LNProcesos.ObtenerProcesosPorEmpresa(NitEmpresa);
            ListaSedes = LNEmpresa.ObtenerSedesPorNit(NitEmpresa);
            List<EDCargo> ListaEDCargo = LNEPP.ListaCargos();
            EDCargo EDCargo = ListaEDCargo.Where(s => s.IDCargo == EPPSuministro.Fk_Id_Cargo).FirstOrDefault();
            EDSede EDSede = ListaSedes.Where(s => s.IdSede == EPPSuministro.Fk_Id_Sede).FirstOrDefault();
            EDProceso EDProceso = ListaProcesos.Where(s => s.Id_Proceso == EPPSuministro.Fk_Id_Proceso).FirstOrDefault();
            EPPSuministro.ProcesoNombre = EDProceso.Descripcion;
            EPPSuministro.SedeNombre = EDSede.NombreSede;
            EPPSuministro.CargoNombre = EDCargo.NombreCargo;

            ViewBag.Nombre = EPPSuministro.NombreTrabajador;
            ViewBag.Cedula = EPPSuministro.CedulaTrabajador;
            ViewBag.Cargo = EPPSuministro.CargoNombre;
            ViewBag.Sede = EPPSuministro.SedeNombre;
            ViewBag.Proceso = EPPSuministro.ProcesoNombre;
            ViewBag.Fecha = EPPSuministro.Fecha;
            ViewBag.MensajeAceptacion1 = "En calidad de trabajador de la empresa, me comprometo a utilizar los elementos de protección personal arriba listados y entregados en óptimas condiciones de calidad, dando el correcto uso, porte y mantenimiento en cada una de las operaciones donde corresponda mi trabajo. Al igual que regresarlos cuando sea necesario reponer,  reemplazar y/o finalice contrato. Su incumplimiento será enmarcado y sancionado como falta grave, en cumplimiento con el Reglamento Interno de Trabajo de la empresa y en lo respectivo al Decreto Ley 1295/1994 Cap. X Art. 91 Literal (b) y Código Sustantivo del Trabajo.";
            ViewBag.MensajeAceptacion2 = "Soy consciente que el incumplimiento de lo establecido anteriormente acarreará sanciones establecidas por la empresa, las cuales acataré por lo que en constancia de lo antes mencionado firmo la presente a los _______________ días del mes ___________________________ del año _________________________ .";
            List<EDEPP> ListaEPP1 = new List<EDEPP>();
            List<EDEPP> ListaEPP = new List<EDEPP>();
            foreach (var item in EPPSuministro.ListaEPPSuministros)
            {
                EDEPP EDEPP = LNEPP.ConsultarEPP(item.Fk_Id_EPP, IdEmpresa);
                EDEPP.Cantidad = item.Cantidad.ToString();
                ListaEPP.Add(EDEPP);
            }

            foreach (var item in ListaEPP)
            {
                int pkAsig = item.Pk_Id_EPP;
                var Epp = ListaEPP1.Where(s => s.Pk_Id_EPP == pkAsig).FirstOrDefault();
                if (Epp != null)
                {
                    string cant = Epp.Cantidad;
                    int cantInt = 0;
                    string cant1 = item.Cantidad;
                    int cantInt1 = 0;
                    int cantInt2 = 0;
                    if (int.TryParse(cant, out cantInt))
                    {
                        if (int.TryParse(cant1, out cantInt1))
                        {
                            cantInt2 = cantInt + cantInt1;
                            Epp.Cantidad = cantInt2.ToString();
                        }
                    }

                }
                else
                {
                    ListaEPP1.Add(item);
                }
            }

            List<EDTipoDePeligro> ListaTipoPeligros = LNMetodologia.ObtenerTiposDePeligro();
            List<EDClasificacionDePeligro> ListaClasPeligros = new List<EDClasificacionDePeligro>();
            if (ListaTipoPeligros != null)
            {
                foreach (var item in ListaTipoPeligros)
                {
                    string DescripcionTipo = item.Descripcion_Del_Peligro;
                    List<EDClasificacionDePeligro> ListaClasPeligro = new List<EDClasificacionDePeligro>();
                    ListaClasPeligro = LNPeligro.ObtenerClasificaciónPorTipo(item.PK_Tipo_De_Peligro);
                    foreach (var item1 in ListaClasPeligro)
                    {
                        string DescripcionClasePeligro = DescripcionTipo + " - " + item1.DescripcionClaseDePeligro;
                        item1.DescripcionClaseDePeligro = DescripcionClasePeligro;
                        ListaClasPeligros.Add(item1);
                    }
                }

            }
            foreach (var item in ListaEPP1)
            {
                if (item.Fk_Id_Clasificacion_De_Peligro != 0)
                {
                    var peligro = (from pel in ListaClasPeligros
                                   where pel.IdClasificacionDePeligro == item.Fk_Id_Clasificacion_De_Peligro
                                   select pel.DescripcionClaseDePeligro).FirstOrDefault();

                    item.Clasificacion_De_Peligro = peligro;
                }
            }
            foreach (var item in ListaEPP1)
            {
                if (item.ArchivoImagen != null && item.RutaImage != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(Path.Combine(item.RutaImage, item.ArchivoImagen))))
                    {
                        string Ruta = item.RutaImage.Replace("~", "") + item.ArchivoImagen; ;
                        item.RutaAbsolutaImagen = Ruta;
                    }
                }
            }
            return View(ListaEPP1);
        }

        [HttpPost]
        public JsonResult ValidarMatriz(List<String> values)
        {
            bool probar = true;
            int IdEmpresa = 0;
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return Json(new { probar });
            }
            string resultado = string.Empty;
            foreach (var item in values)
            {
                if (item != null)
                {
                    if (resultado == string.Empty)
                    {
                        resultado = item;
                    }
                    else
                    {
                        resultado = resultado + "@" + item;
                    }
                }
            }
            IdEmpresa = usuarioActual.IdEmpresa;
            return Json(new { probar, resultado, IdEmpresa });
        }
        [HttpGet]
        public ActionResult ExportMatrizExcel(string resultado, int IdEmpresa)
        {
            List<EDEPP> ListaEPP = new List<EDEPP>();
            List<EDEPP> ListaEPP1 = new List<EDEPP>();
            string[] EPPArray = resultado.Split('@');
            foreach (var item in EPPArray)
            {
                int IdintEPP = 0;
                if (int.TryParse(item, out IdintEPP))
                {
                    EDEPP EDEPP = LNEPP.ConsultarEPP(IdintEPP, IdEmpresa);
                    ListaEPP.Add(EDEPP);
                }
            }
            ListaEPP1 = ListaEPP;

            List<EDTipoDePeligro> ListaTipoPeligros = LNMetodologia.ObtenerTiposDePeligro();
            List<EDClasificacionDePeligro> ListaClasPeligros = new List<EDClasificacionDePeligro>();
            if (ListaTipoPeligros != null)
            {
                foreach (var item in ListaTipoPeligros)
                {
                    string DescripcionTipo = item.Descripcion_Del_Peligro;
                    List<EDClasificacionDePeligro> ListaClasPeligro = new List<EDClasificacionDePeligro>();
                    ListaClasPeligro = LNPeligro.ObtenerClasificaciónPorTipo(item.PK_Tipo_De_Peligro);
                    foreach (var item1 in ListaClasPeligro)
                    {
                        string DescripcionClasePeligro = DescripcionTipo + " - " + item1.DescripcionClaseDePeligro;
                        item1.DescripcionClaseDePeligro = DescripcionClasePeligro;
                        ListaClasPeligros.Add(item1);
                    }
                }

            }
            foreach (var item in ListaEPP1)
            {
                if (item.Fk_Id_Clasificacion_De_Peligro != 0)
                {
                    var peligro = (from pel in ListaClasPeligros
                                   where pel.IdClasificacionDePeligro == item.Fk_Id_Clasificacion_De_Peligro
                                   select pel.DescripcionClaseDePeligro).FirstOrDefault();

                    item.Clasificacion_De_Peligro = peligro;
                }
            }



            DataTable dt = new DataTable("MatrizEPP");
            dt.Columns.AddRange(new DataColumn[9] { new DataColumn("Nombre EPP"),
                                            new DataColumn("Parte del Cuerpo a Proteger"),
                                            new DataColumn("Peligro"),
                                            new DataColumn("Especificación Técnica"),
                                            new DataColumn("Uso"),
                                            new DataColumn("Mantenimiento"),
                                            new DataColumn("Vida Útil"),
                                            new DataColumn("Reposición"),
                                            new DataColumn("Disposición Final") });
            int cont = 0;
            foreach (var EPPItem in ListaEPP1)
            {
                dt.Rows.Add(EPPItem.NombreEPP, EPPItem.ParteCuerpo, EPPItem.Clasificacion_De_Peligro, EPPItem.EspecificacionTecnica, EPPItem.Uso, EPPItem.Mantenimiento, EPPItem.VidaUtil, EPPItem.Reposicion, EPPItem.DisposicionFinal);
                cont++;
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add("MatrizEPP_Reporte");
                ws.Range(1, 1, 1, 9).Merge().AddToNamed("Titles");
                ws.Range(1, 1, 1, 9).Style.Fill.BackgroundColor = XLColor.FromArgb(172, 179, 168);
                ws.Range(1, 1, 1, 9).Style.Font.Bold = true;
                ws.Range(1, 1, 1, 9).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Range(1, 1, 1, 9).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ws.Cell(1, 1).Value = "MATRIZ DE INDENTIFICACIÓN DE ELEMENTOS DE PROTECCIÓN PERSONAL";
                ws.Cell(2, 1).Value = "Nombre EPP";
                ws.Cell(2, 2).Value = "Parte del Cuerpo a Proteger";
                ws.Cell(2, 3).Value = "Riesgo Controlado";
                ws.Cell(2, 4).Value = "Especificación Técnica";
                ws.Cell(2, 5).Value = "Uso";
                ws.Cell(2, 6).Value = "Mantenimiento";
                ws.Cell(2, 7).Value = "Vida Útil";
                ws.Cell(2, 8).Value = "Reposición";
                ws.Cell(2, 9).Value = "Disposición Final";
                var col1 = ws.Column("A");
                var col2 = ws.Column("B");
                var col3 = ws.Column("C");
                var col4 = ws.Column("D");
                var col5 = ws.Column("E");
                var col6 = ws.Column("F");
                var col7 = ws.Column("G");
                var col8 = ws.Column("H");
                var col9 = ws.Column("I");
                col1.Width = 20;
                col2.Width = 20;
                col3.Width = 20;
                col4.Width = 35;
                col5.Width = 35;
                col6.Width = 35;
                col7.Width = 35;
                col8.Width = 35;
                col9.Width = 35;
                var fil1 = ws.Row(1);
                fil1.Height = 40;
                fil1.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                var fil2 = ws.Row(2);
                fil2.Height = 40;
                fil2.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                fil2.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                fil2.Style.Alignment.WrapText = true;
                fil2.Style.Font.Bold = true;
                for (int i = 3; i < cont + 3; i++)
                {
                    var filx = ws.Row(i);
                    filx.Height = 60;
                    filx.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    filx.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Justify;
                    filx.Style.Alignment.WrapText = true;
                }
                int total_filas = cont + 1;
                for (int i = 2; i < total_filas + 2; i++)
                {
                    for (int i1 = 1; i1 < 10; i1++)
                    {
                        ws.Cell(i, i1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    }
                }
                var rangeWithData = ws.Cell(3, 1).InsertData(dt.AsEnumerable());
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    string Fecha = DateTime.Now.ToString().Replace(" ", "").Replace(".", "").Replace("/", "").Replace(":", "");
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "AlisstaMatrizEPP" + Fecha + ".xlsx");
                }
            }
        }
        [HttpGet]
        public ActionResult AsignacionExcel(int id)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }

            EDEPPSuministro EPPSuministro = new EDEPPSuministro();
            EPPSuministro = LNEPP.ConsultaListaAsignacionId(id, usuarioActual.IdEmpresa);

            List<EDCargo> ListaCargos = new List<EDCargo>();
            List<EDSede> ListaSedes = new List<EDSede>();
            List<EDProceso> ListaProcesos = LNProcesos.ObtenerProcesosPorEmpresa(usuarioActual.NitEmpresa);
            ListaSedes = LNEmpresa.ObtenerSedesPorNit(usuarioActual.NitEmpresa);
            List<EDCargo> ListaEDCargo = LNEPP.ListaCargos();
            EDCargo EDCargo = ListaEDCargo.Where(s => s.IDCargo == EPPSuministro.Fk_Id_Cargo).FirstOrDefault();
            EDSede EDSede = ListaSedes.Where(s => s.IdSede == EPPSuministro.Fk_Id_Sede).FirstOrDefault();
            EDProceso EDProceso = ListaProcesos.Where(s => s.Id_Proceso == EPPSuministro.Fk_Id_Proceso).FirstOrDefault();
            EPPSuministro.ProcesoNombre = EDProceso.Descripcion;
            EPPSuministro.SedeNombre = EDSede.NombreSede;
            EPPSuministro.CargoNombre = EDCargo.NombreCargo;

            List<EDTipoDePeligro> ListaTipoPeligros = LNMetodologia.ObtenerTiposDePeligro();
            List<EDClasificacionDePeligro> ListaClasPeligros = new List<EDClasificacionDePeligro>();
            if (ListaTipoPeligros != null)
            {
                foreach (var item in ListaTipoPeligros)
                {
                    string DescripcionTipo = item.Descripcion_Del_Peligro;
                    List<EDClasificacionDePeligro> ListaClasPeligro = new List<EDClasificacionDePeligro>();
                    ListaClasPeligro = LNPeligro.ObtenerClasificaciónPorTipo(item.PK_Tipo_De_Peligro);
                    foreach (var item1 in ListaClasPeligro)
                    {
                        string DescripcionClasePeligro = DescripcionTipo + " - " + item1.DescripcionClaseDePeligro;
                        item1.DescripcionClaseDePeligro = DescripcionClasePeligro;
                        ListaClasPeligros.Add(item1);
                    }
                }

            }

            DataTable dt = new DataTable("AsignacionEPP");
            dt.Columns.AddRange(new DataColumn[10] { new DataColumn("Nombre EPP"),
                                            new DataColumn("Parte del Cuerpo a Proteger"),
                                            new DataColumn("Peligro"),
                                            new DataColumn("Especificación Técnica"),
                                            new DataColumn("Uso"),
                                            new DataColumn("Mantenimiento"),
                                            new DataColumn("Vida Útil"),
                                            new DataColumn("Reposición"),
                                            new DataColumn("Disposición Final"),
                                            new DataColumn("Cantidad")
            });
            int cont = 0;

            List<EDEPP> ListaEPP1 = new List<EDEPP>();
            List<EDEPP> ListaEPP = new List<EDEPP>();

            foreach (var EPPItem in EPPSuministro.ListaEPPSuministros)
            {
                EDEPP EDEPP = LNEPP.ConsultarEPP(EPPItem.Fk_Id_EPP, usuarioActual.IdEmpresa);
                EDEPP.Cantidad = EPPItem.Cantidad.ToString();
                ListaEPP.Add(EDEPP);
            }

            foreach (var item in ListaEPP)
            {
                int pkAsig = item.Pk_Id_EPP;
                var Epp = ListaEPP1.Where(s => s.Pk_Id_EPP == pkAsig).FirstOrDefault();
                if (Epp != null)
                {
                    string cant = Epp.Cantidad;
                    int cantInt = 0;
                    string cant1 = item.Cantidad;
                    int cantInt1 = 0;
                    int cantInt2 = 0;
                    if (int.TryParse(cant, out cantInt))
                    {
                        if (int.TryParse(cant1, out cantInt1))
                        {
                            cantInt2 = cantInt + cantInt1;
                            Epp.Cantidad = cantInt2.ToString();
                        }
                    }

                }
                else
                {
                    ListaEPP1.Add(item);
                }
            }

            foreach (var EPPItem in ListaEPP1)
            {
                if (EPPItem.Fk_Id_Clasificacion_De_Peligro != 0)
                {
                    var peligro = (from pel in ListaClasPeligros
                                   where pel.IdClasificacionDePeligro == EPPItem.Fk_Id_Clasificacion_De_Peligro
                                   select pel.DescripcionClaseDePeligro).FirstOrDefault();

                    EPPItem.Clasificacion_De_Peligro = peligro;
                }
                dt.Rows.Add(EPPItem.NombreEPP, EPPItem.ParteCuerpo, EPPItem.Clasificacion_De_Peligro, EPPItem.EspecificacionTecnica, EPPItem.Uso, EPPItem.Mantenimiento, EPPItem.VidaUtil, EPPItem.Reposicion, EPPItem.DisposicionFinal, EPPItem.Cantidad);
                cont++;
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add("ControlSuministro");
                var fil1 = ws.Row(1);
                fil1.Height = 27;

                ws.Cell(1, 1).Style.Font.Bold = true;
                ws.Cell(1, 1).Style.Font.FontSize = 16;
                ws.Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                ws.Cell(1, 1).Value = "CERTIFICADO DE ENTREGA DE ELEMENTOS DE PROTECCIÓN PERSONAL";

                ws.Cell(2, 1).Value = "Cédula del Trabajador:";
                ws.Cell(2, 1).Style.Font.Bold = true;
                ws.Cell(2, 2).Value = EPPSuministro.CedulaTrabajador;
                ws.Cell(2, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;

                ws.Cell(3, 1).Value = "Nombre del Trabajador:";
                ws.Cell(3, 1).Style.Font.Bold = true;
                ws.Cell(3, 2).Value = EPPSuministro.NombreTrabajador;
                ws.Cell(3, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;

                ws.Cell(4, 1).Value = "Proceso:";
                ws.Cell(4, 1).Style.Font.Bold = true;
                ws.Cell(4, 2).Value = EPPSuministro.ProcesoNombre;
                ws.Cell(4, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;

                ws.Cell(5, 1).Value = "Sede:";
                ws.Cell(5, 1).Style.Font.Bold = true;
                ws.Cell(5, 2).Value = EPPSuministro.SedeNombre;
                ws.Cell(5, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;

                ws.Cell(2, 3).Value = "Fecha de Registro:";
                ws.Cell(2, 3).Style.Font.Bold = true;
                ws.Cell(2, 4).Value = EPPSuministro.Fecha;
                ws.Cell(2, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;

                ws.Cell(3, 3).Value = "Cargo:";
                ws.Cell(3, 3).Style.Font.Bold = true;
                ws.Cell(3, 4).Value = EPPSuministro.CargoNombre;
                ws.Cell(3, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;

                ws.Cell(4, 3).Value = "Razón Social:";
                ws.Cell(4, 3).Style.Font.Bold = true;
                ws.Cell(4, 4).Value = usuarioActual.RazonSocialEmpresa;
                ws.Cell(4, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;

                ws.Cell(5, 3).Value = "NIT:";
                ws.Cell(5, 3).Style.Font.Bold = true;
                ws.Cell(5, 4).Value = usuarioActual.NitEmpresa;
                ws.Cell(5, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;


                ws.Cell(6, 1).Value = "Nombre EPP";
                ws.Cell(6, 2).Value = "Parte del Cuerpo a Proteger";
                ws.Cell(6, 3).Value = "Riesgo Controlado";
                ws.Cell(6, 4).Value = "Especificación Técnica";
                ws.Cell(6, 5).Value = "Uso";
                ws.Cell(6, 6).Value = "Mantenimiento";
                ws.Cell(6, 7).Value = "Vida Útil";
                ws.Cell(6, 8).Value = "Reposición";
                ws.Cell(6, 9).Value = "Disposición Final";
                ws.Cell(6, 10).Value = "Cantidad";


                ws.Range(6, 1, 6, 10).Style.Fill.BackgroundColor = XLColor.FromArgb(172, 179, 168);

                var fil5 = ws.Row(6);
                fil5.Height = 40;
                fil5.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                fil5.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                fil5.Style.Alignment.WrapText = true;
                fil5.Style.Font.Bold = true;

                for (int i = 7; i < cont + 7; i++)
                {
                    int row = i;

                    var filx = ws.Row(i);
                    filx.Height = 60;
                    filx.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    for (int i1 = 1; i1 < 11; i1++)
                    {
                        if (i1 == 1 || i1 == 2 || i1 == 3 || i1 == 10)
                        {
                            ws.Cell(row, i1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        }
                        if (i1 == 4 || i1 == 5 || i1 == 6 || i1 == 7 || i1 == 8 || i1 == 9)
                        {
                            ws.Cell(row, i1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Justify;
                        }


                    }
                    filx.Style.Alignment.WrapText = true;
                }
                int total_filas = 8 + cont;

                ws.Range(total_filas, 1, total_filas + 3, 10).Merge();
                ws.Range(total_filas, 1, total_filas + 3, 10).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                ws.Range(total_filas, 1, total_filas + 3, 10).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                ws.Range(total_filas, 1, total_filas + 3, 10).Style.Alignment.WrapText = true;
                ws.Range(total_filas, 1, total_filas + 3, 10).Style.Alignment.WrapText = true;
                ws.Range(total_filas, 1, total_filas + 3, 10).Style.Border.OutsideBorder = XLBorderStyleValues.Double;
                ws.Cell(total_filas, 1).Value = "En calidad de trabajador de la empresa, me comprometo a utilizar los elementos de protección personal arriba listados y entregados en óptimas condiciones de calidad, dando el correcto uso, porte y mantenimiento en cada una de las operaciones donde corresponda mi trabajo. Al igual que regresarlos cuando sea necesario reponer,  reemplazar y/o finalice contrato. Su incumplimiento será enmarcado y sancionado como falta grave, en cumplimiento con el Reglamento Interno de Trabajo de la empresa y en lo respectivo al Decreto Ley 1295/1994 Cap. X Art. 91 Literal (b) y Código Sustantivo del Trabajo." + System.Environment.NewLine + "Soy consciente que el incumplimiento de lo establecido anteriormente acarreará sanciones establecidas por la empresa, las cuales acataré por lo que en constancia de lo antes mencionado firmo la presente a los _______________ días del mes ___________________________ del año _________________________ .";


                ws.Cell(total_filas + 6, 2).Value = "Firma/Huella Quien Recibe";
                ws.Cell(total_filas + 6, 6).Value = "Firma/Huella Quien Entrega";
                ws.Cell(total_filas + 6, 2).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                ws.Cell(total_filas + 6, 6).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                ws.Cell(total_filas + 6, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Cell(total_filas + 6, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;


                var col1 = ws.Column("A");
                var col2 = ws.Column("B");
                var col3 = ws.Column("C");
                var col4 = ws.Column("D");
                var col5 = ws.Column("E");
                var col6 = ws.Column("F");
                var col7 = ws.Column("G");
                var col8 = ws.Column("H");
                var col9 = ws.Column("I");
                var col10 = ws.Column("J");

                col1.Width = 22;
                col2.Width = 35;
                col3.Width = 22;
                col4.Width = 35;
                col5.Width = 20;
                col6.Width = 20;
                col7.Width = 20;
                col8.Width = 20;
                col9.Width = 20;
                col10.Width = 30;

                var rangeWithData = ws.Cell(7, 1).InsertData(dt.AsEnumerable());
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    string Fecha = DateTime.Now.ToString().Replace(" ", "").Replace(".", "").Replace("/", "").Replace(":", "");
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "AlisstaControlSuministroEPP" + Fecha + ".xlsx");
                }
            }
        }
        
        [HttpGet]
        public ActionResult ExportMatrizPDF(string resultado, int IdEmpresa)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }
            //string SwitchNombreEmpresa = usuarioActual.RazonSocialEmpresa;
            //string SwitchNitEmpresa = usuarioActual.NitEmpresa;
            //string SwitchNombreDocumento = "MATRIZ DE ELEMENTOS DE PROTECCIÓN PERSONAL";

            //var fullFooter = Url.Action("Footer", "AdmoEPP", null, Request.Url.Scheme);
            //var fullHeader = Url.Action("Header", "AdmoEPP", new { NombreEmpresa = SwitchNombreEmpresa, NitEmpresa = SwitchNitEmpresa, NombreInforme = SwitchNombreDocumento }, Request.Url.Scheme);

            //var uriFooter = new Uri(Url.Action("Footer", "AdmoEPP", null, Request.Url.Scheme));
            //var clean1 = uriFooter.GetComponents(UriComponents.AbsoluteUri & ~UriComponents.Port,
            //                   UriFormat.UriEscaped);

            //var uriHeader = new Uri(Url.Action("Header", "AdmoEPP", new { NombreEmpresa = SwitchNombreEmpresa, NitEmpresa = SwitchNitEmpresa, NombreInforme = SwitchNombreDocumento }, Request.Url.Scheme));
            //var clean2 = uriHeader.GetComponents(UriComponents.AbsoluteUri & ~UriComponents.Port,
            //                   UriFormat.UriEscaped);


            //string cusomtSwitches = string.Format("--footer-line --print-media-type --allow {0} --footer-html {0} --header-line --print-media-type --allow {1} --header-html {1} --header-spacing 5",
            //clean1, clean2);
            //int Id_Empresa = usuarioActual.IdEmpresa;


            //var fullUrl = this.Url.Action("MatrizEPPPDF", "AdmoEPP", new { id = resultado, NitEmpresa = SwitchNitEmpresa, IdEmpresa = usuarioActual.IdEmpresa }, this.Request.Url.Scheme);
            //var fullUrl1 = new Uri(this.Url.Action("MatrizEPPPDF", "AdmoEPP", new { id = resultado, NitEmpresa = SwitchNitEmpresa, IdEmpresa = usuarioActual.IdEmpresa }, this.Request.Url.Scheme));
            //var clean0 = fullUrl1.GetComponents(UriComponents.AbsoluteUri & ~UriComponents.Port,
            //                   UriFormat.UriEscaped);

            string EncodedRazonSocial = System.Net.WebUtility.UrlEncode(usuarioActual.RazonSocialEmpresa);
            string EncodedNit = System.Net.WebUtility.UrlEncode(usuarioActual.NitEmpresa);
            string EncodedNombreInforme = System.Net.WebUtility.UrlEncode("CONTROL DE SUMINISTROS");
            var footurl = "https://alissta.gov.co/Acciones/Footer";
            var headerurl = "https://alissta.gov.co/Acciones/Header?NombreEmpresa=" + EncodedRazonSocial + "&NitEmpresa=" + EncodedNit + "&NombreInforme=" + EncodedNombreInforme;
            string cusomtSwitches = string.Format("--footer-line --print-media-type --allow {0} --footer-html {0} --header-line --print-media-type --allow {1} --header-html {1} --header-spacing 5",
            footurl, headerurl);
            var ReporteUrl = "https://alissta.gov.co/AdmoEPP/MatrizEPPPDF?id=" + resultado + "&NitEmpresa=" + EncodedNit + "&IdEmpresa=" + usuarioActual.IdEmpresa.ToString();

            return new Rotativa.UrlAsPdf(ReporteUrl)
            {
                FileName = "Alissta_MatrizEPP" + DateTime.Now.ToString().Replace("/", "").Replace(":", "").Replace(" ", "").Replace(".", "") + ".pdf"
                ,
                PageSize = Rotativa.Options.Size.Letter
                ,
                PageOrientation = Rotativa.Options.Orientation.Landscape
                ,
                CustomSwitches = cusomtSwitches
            };
        }
        [AllowAnonymous]
        [HttpGet]
        public ActionResult AsignacionPDF(string id)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }
            //string SwitchNombreEmpresa = usuarioActual.RazonSocialEmpresa;
            string SwitchNitEmpresa = usuarioActual.NitEmpresa;
            //string SwitchNombreDocumento = "CONTROL DE SUMINISTROS";

            //var fullFooter = Url.Action("Footer", "AdmoEPP", null, Request.Url.Scheme);
            //var fullHeader = Url.Action("Header", "AdmoEPP", new { NombreEmpresa = SwitchNombreEmpresa, NitEmpresa = SwitchNitEmpresa, NombreInforme = SwitchNombreDocumento }, Request.Url.Scheme);

            //var uriFooter = new Uri(Url.Action("Footer", "AdmoEPP", null, Request.Url.Scheme));
            //var clean1 = uriFooter.GetComponents(UriComponents.AbsoluteUri & ~UriComponents.Port,
            //                   UriFormat.UriEscaped);

            //var uriHeader = new Uri(Url.Action("Header", "AdmoEPP", new { NombreEmpresa = SwitchNombreEmpresa, NitEmpresa = SwitchNitEmpresa, NombreInforme = SwitchNombreDocumento }, Request.Url.Scheme));
            //var clean2 = uriHeader.GetComponents(UriComponents.AbsoluteUri & ~UriComponents.Port,
            //                   UriFormat.UriEscaped);


            //string cusomtSwitches = string.Format("--footer-line --print-media-type --allow {0} --footer-html {0} --header-line --print-media-type --allow {1} --header-html {1} --header-spacing 5",
            //clean1, clean2);
            //int Id_Empresa = usuarioActual.IdEmpresa;


            var fullUrl = this.Url.Action("AsignacionEPPPDF", "AdmoEPP", new { id = id, NitEmpresa = SwitchNitEmpresa, IdEmpresa = usuarioActual.IdEmpresa }, this.Request.Url.Scheme);
            //var fullUrl1 = new Uri(this.Url.Action("AsignacionEPPPDF", "AdmoEPP", new { id = id, NitEmpresa = SwitchNitEmpresa, IdEmpresa = usuarioActual.IdEmpresa }, this.Request.Url.Scheme));
            //var clean0 = fullUrl1.GetComponents(UriComponents.AbsoluteUri & ~UriComponents.Port,
            //                   UriFormat.UriEscaped);


            
            string EncodedRazonSocial = System.Net.WebUtility.UrlEncode(usuarioActual.RazonSocialEmpresa);
            string EncodedNit = System.Net.WebUtility.UrlEncode(usuarioActual.NitEmpresa);
            string EncodedNombreInforme = System.Net.WebUtility.UrlEncode("CONTROL DE SUMINISTROS");
            var footurl = "https://alissta.gov.co/Acciones/Footer";
            var headerurl = "https://alissta.gov.co/Acciones/Header?NombreEmpresa=" + EncodedRazonSocial + "&NitEmpresa=" + EncodedNit + "&NombreInforme=" + EncodedNombreInforme;
            string cusomtSwitches = string.Format("--footer-line --print-media-type --allow {0} --footer-html {0} --header-line --print-media-type --allow {1} --header-html {1} --header-spacing 5",
            footurl, headerurl);
            var ReporteUrl = "https://alissta.gov.co/AdmoEPP/AsignacionEPPPDF?id=" + id.ToString() + "&NitEmpresa=" + EncodedNit + "&IdEmpresa=" + usuarioActual.IdEmpresa.ToString();

            return new Rotativa.UrlAsPdf(ReporteUrl)
            {
                FileName = "Alissta_ControlSuministroEPP" + DateTime.Now.ToString().Replace("/", "").Replace(":", "").Replace(" ", "").Replace(".", "") + ".pdf"
                ,
                PageSize = Rotativa.Options.Size.Letter
                ,
                PageOrientation = Rotativa.Options.Orientation.Landscape
                ,
                CustomSwitches = cusomtSwitches
                ,
                PageMargins = new Rotativa.Options.Margins(20, 3, 15, 3)
            };
        }
        [AllowAnonymous]
        [HttpGet]
        public ActionResult EPP_PDF(string id)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }
            string SwitchNombreEmpresa = usuarioActual.RazonSocialEmpresa;
            string SwitchNitEmpresa = usuarioActual.NitEmpresa;
            string SwitchNombreDocumento = "ELEMENTOS DE PROTECCIÓN PERSONAL";

            int Id_Empresa = usuarioActual.IdEmpresa;
            int Id_EPP = 0;
            bool probar = int.TryParse(id, out Id_EPP);

            string EncodedRazonSocial = System.Net.WebUtility.UrlEncode(usuarioActual.RazonSocialEmpresa);
            string EncodedNit = System.Net.WebUtility.UrlEncode(usuarioActual.NitEmpresa);
            string EncodedNombreInforme = System.Net.WebUtility.UrlEncode("ELEMENTOS DE PROTECCIÓN PERSONAL");
            var footurl = "https://alissta.gov.co/Acciones/Footer";
            var headerurl = "https://alissta.gov.co/Acciones/Header?NombreEmpresa=" + EncodedRazonSocial + "&NitEmpresa=" + EncodedNit + "&NombreInforme=" + EncodedNombreInforme;
            string cusomtSwitches = string.Format("--footer-line --print-media-type --allow {0} --footer-html {0} --header-line --print-media-type --allow {1} --header-html {1} --header-spacing 5",
            footurl, headerurl);
            var ReporteUrl = "https://alissta.gov.co/AdmoEPP/EPPPDF?id=" + Id_EPP.ToString() + "&NitEmpresa=" + EncodedNit + "&IdEmpresa=" + usuarioActual.IdEmpresa.ToString();

            ////Urls limpiar
            //var fullFooter = Url.Action("Footer", "AdmoEPP", null, Request.Url.Scheme);
            //var fullHeader = Url.Action("Header", "AdmoEPP", new { NombreEmpresa = SwitchNombreEmpresa, NitEmpresa = SwitchNitEmpresa, NombreInforme = SwitchNombreDocumento }, Request.Url.Scheme);
            //var fullUrl = this.Url.Action("EPPPDF", "AdmoEPP", new { id = Id_EPP, NitEmpresa = SwitchNitEmpresa, IdEmpresa = usuarioActual.IdEmpresa }, this.Request.Url.Scheme);

            ////Urls sin configuracion de puertos
            //var uriFooter = new Uri(Url.Action("Footer", "AdmoEPP", null, Request.Url.Scheme));
            //var clean1 = uriFooter.GetComponents(UriComponents.AbsoluteUri & ~UriComponents.Port,
            //                   UriFormat.UriEscaped);
            //var uriHeader = new Uri(Url.Action("Header", "AdmoEPP", new { NombreEmpresa = SwitchNombreEmpresa, NitEmpresa = SwitchNitEmpresa, NombreInforme = SwitchNombreDocumento }, Request.Url.Scheme));
            //var clean2 = uriHeader.GetComponents(UriComponents.AbsoluteUri & ~UriComponents.Port,
            //                  UriFormat.UriEscaped);

            //var fullUrl1 = new Uri(this.Url.Action("EPPPDF", "AdmoEPP", new { id = Id_EPP, NitEmpresa = SwitchNitEmpresa, IdEmpresa = usuarioActual.IdEmpresa }, this.Request.Url.Scheme));
            //var clean0 = fullUrl1.GetComponents(UriComponents.AbsoluteUri & ~UriComponents.Port,
            //                   UriFormat.UriEscaped);

            ////CustomSwitches
            //string cusomtSwitches_Produccion = string.Format("--footer-line --print-media-type --allow {0} --footer-html {0} --header-line --print-media-type --allow {1} --header-html {1} --header-spacing 5",
            //fullFooter, fullHeader);


            return new Rotativa.UrlAsPdf(ReporteUrl)
            {
                FileName = "Alissta_EPP" + DateTime.Now.ToString().Replace("/", "").Replace(":", "").Replace(" ", "").Replace(".", "") + ".pdf"
                                                ,
                PageSize = Rotativa.Options.Size.Letter
                                                ,
                CustomSwitches = cusomtSwitches
            };

        }
        [AllowAnonymous]
        public ActionResult Footer()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult Header(string NombreEmpresa, string NitEmpresa, string NombreInforme)
        {
            ViewBag.NombreEmpresa = NombreEmpresa;
            ViewBag.NitEmpresa = NitEmpresa;
            ViewBag.NombreInforme = NombreInforme;
            return View();
        }
        #endregion
    }
}
