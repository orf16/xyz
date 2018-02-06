using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using SG_SST.Controllers.Base;
using SG_SST.EntidadesDominio.Aplicacion;
using System.Configuration;
using RestSharp;
using System.Net;
using SG_SST.Models.Ausentismo;
using SG_SST.EntidadesDominio.Empresas;
using SG_SST.Models.Empleado;
using SG_SST.Logica.MedicionEvaluacion;
using SG_SST.Logica.Empresas;
using SG_SST.EntidadesDominio.Planificacion;
using SG_SST.Logica.Planificacion;
using SG_SST.Logica.Aplicacion;
using SG_SST.Models.Aplicacion;
using System.Globalization;

namespace SG_SST.Controllers.Aplicacion
{
    public class AdmoEMHController : BaseController
    {
        LNAcciones LNAcciones = new LNAcciones();
        LNEmpresa LNEmpresa = new LNEmpresa();
        LNPeligro LNPeligro = new LNPeligro();
        LNAdmoEMH LNAdmoEMH = new LNAdmoEMH();
        LNMetodologia LNMetodologia = new LNMetodologia();
        LNProcesos LNProcesos = new LNProcesos();
        LNProcesos LNProcesos1 = new LNProcesos();

        private static string RutaImagenesTemp = "~/Content/ArchivosEquipos/ImagenesTempEquipos/";
        private static string RutaArchivosBDTemp = "~/Content/ArchivosEquipos/ArchivosTempEquipos/";
        private static string RutaImagenes = "~/Content/ArchivosEquipos/ImagenesEquipos/";
        private static string RutaArchivosBD = "~/Content/ArchivosEquipos/ArchivosEquipos/";
        private static string SrcWhite = "data:image/png;base64,R0lGODlhAQABAAAAACH5BAEKAAEALAAAAAABAAEAAAICTAEAOw==";
        [HttpGet]
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
        [HttpGet]
        public ActionResult NuevoEHM()
        {

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }
            List<EDSede> ListaSedes = new List<EDSede>();
            ListaSedes = LNEmpresa.ObtenerSedesPorNit(usuarioActual.NitEmpresa);

            List<EDTipoDePeligro> ListaTipoPeligros = LNMetodologia.ObtenerTiposDePeligro();
            List<EDTipoDePeligro> ListaTipoPeligros1 = new List<EDTipoDePeligro>();
            List<EDClasificacionDePeligro> ListaClasPeligros3 = new List<EDClasificacionDePeligro>();
            if (ListaTipoPeligros!=null)
            {
                ListaTipoPeligros1 = ListaTipoPeligros;
            }
            ViewBag.Pk_Id_Tipo_Peligro = new SelectList(ListaTipoPeligros1, "PK_Tipo_De_Peligro", "Descripcion_Del_Peligro", null);
            ViewBag.Pk_Id_Clasif_Peligro = new SelectList(ListaClasPeligros3, "IdClasificacionDePeligro", "DescripcionClaseDePeligro", null);
            
            List<EDClasificacionDePeligro> ListaClasPeligros = new List<EDClasificacionDePeligro>();
            List<EDClasificacionDePeligro> ListaClasPeligros2 = new List<EDClasificacionDePeligro>();
            
            foreach (var item in ListaSedes)
            {
                List<EDClasificacionDePeligro> ListaClasPeligros1 = LNPeligro.ObtenerClasificaciónPorSede(item.IdSede);
                foreach (var item1 in ListaClasPeligros1)
                {
                    EDClasificacionDePeligro EDClasificacionDePeligroBuscar = new EDClasificacionDePeligro();
                    EDClasificacionDePeligroBuscar = ListaClasPeligros.Where(s => s.IdClasificacionDePeligro == item1.IdClasificacionDePeligro).FirstOrDefault();
                    if (EDClasificacionDePeligroBuscar==null)
                    {
                        ListaClasPeligros.Add(item1);
                    }
                }
            }
            List<EDClasificacionDePeligro> noDuplicados = ListaClasPeligros.Distinct().ToList();
            ViewBag.Pk_Id_Clas_Peligro = new SelectList(noDuplicados, "IdClasificacionDePeligro", "DescripcionClaseDePeligro", null);
            return View();
        }
        [HttpPost]
        public ActionResult GuardarNuevoEHM(AdmoEMH GuardarAdmoEMH, List<EDPeligroEMH> ListaPeligro)
        {
            bool ProbarNumero_fechas = true;
            bool Probar = false;
            string Estado = "No se guardó la hoja de vida, por favor revise la información suministrada";
            bool[] Validacion = new bool[15];
            string[] ValidacionStr = new string[15];
            for (int i = 0; i < 15; i++)
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

            if (GuardarAdmoEMH.HorasVida==0)
            {
                ProbarNumero_fechas = false;
            }
            //if (GuardarAdmoEMH.FechaIns == DateTime.MinValue)
            //{
            //    ProbarNumero_fechas = false;
            //}
            if (GuardarAdmoEMH.Fecha_Fab == DateTime.MinValue)
            {
                ProbarNumero_fechas = false;
            }


            ModelState.Clear();
            TryValidateModel(GuardarAdmoEMH);
            if (ModelState.IsValid && ProbarNumero_fechas)
            {
                if (ListaPeligro==null)
                {
                    ListaPeligro = new List<EDPeligroEMH>();
                }
                AdmoEMH NuevoAdmoEMH = new AdmoEMH();
                NuevoAdmoEMH = GuardarAdmoEMH;
                NuevoAdmoEMH.Fk_Id_Empresa = usuarioActual.IdEmpresa;

                CrearCarpeta(RutaImagenes);
                CrearCarpeta(RutaArchivosBD);

                DateTime datefab = GuardarAdmoEMH.Fecha_Fab;
                NuevoAdmoEMH.Fecha_Fab = datefab;

                List<string> ArchivosTemporalesEliminar = new List<string>();
                if (NuevoAdmoEMH.ArchivoImagen1 != null)
                {
                    string PathOrigen = Server.MapPath(Path.Combine(RutaImagenesTemp, NuevoAdmoEMH.ArchivoImagen1));
                    if (System.IO.File.Exists(PathOrigen))
                    {
                        try
                        {
                            NuevoAdmoEMH.RutaImage1 = RutaImagenes;
                            string pathsave = Server.MapPath(Path.Combine(NuevoAdmoEMH.RutaImage1, NuevoAdmoEMH.ArchivoImagen1));
                            System.IO.File.Move(PathOrigen, pathsave);
                            ArchivosTemporalesEliminar.Add(PathOrigen);
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
                if (NuevoAdmoEMH.ArchivoImagen2 != null)
                {
                    string PathOrigen = Server.MapPath(Path.Combine(RutaImagenesTemp, NuevoAdmoEMH.ArchivoImagen2));
                    if (System.IO.File.Exists(PathOrigen))
                    {
                        try
                        {
                            NuevoAdmoEMH.RutaImage2 = RutaImagenes;
                            string pathsave = Server.MapPath(Path.Combine(NuevoAdmoEMH.RutaImage2, NuevoAdmoEMH.ArchivoImagen2));
                            System.IO.File.Move(PathOrigen, pathsave);
                            ArchivosTemporalesEliminar.Add(PathOrigen);
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
                if (NuevoAdmoEMH.ArchivoImagen3 != null)
                {
                    string PathOrigen = Server.MapPath(Path.Combine(RutaImagenesTemp, NuevoAdmoEMH.ArchivoImagen3));
                    if (System.IO.File.Exists(PathOrigen))
                    {
                        try
                        {
                            NuevoAdmoEMH.RutaImage3 = RutaImagenes;
                            string pathsave = Server.MapPath(Path.Combine(NuevoAdmoEMH.RutaImage3, NuevoAdmoEMH.ArchivoImagen3));
                            System.IO.File.Move(PathOrigen, pathsave);
                            ArchivosTemporalesEliminar.Add(PathOrigen);
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
                if (NuevoAdmoEMH.ArchivoImagen4 != null)
                {
                    string PathOrigen = Server.MapPath(Path.Combine(RutaImagenesTemp, NuevoAdmoEMH.ArchivoImagen4));
                    if (System.IO.File.Exists(PathOrigen))
                    {
                        try
                        {
                            NuevoAdmoEMH.RutaImage4 = RutaImagenes;
                            string pathsave = Server.MapPath(Path.Combine(NuevoAdmoEMH.RutaImage4, NuevoAdmoEMH.ArchivoImagen4));
                            System.IO.File.Move(PathOrigen, pathsave);
                            ArchivosTemporalesEliminar.Add(PathOrigen);
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
                if (NuevoAdmoEMH.ArchivoImagen5 != null)
                {
                    string PathOrigen = Server.MapPath(Path.Combine(RutaImagenesTemp, NuevoAdmoEMH.ArchivoImagen5));
                    if (System.IO.File.Exists(PathOrigen))
                    {
                        try
                        {
                            NuevoAdmoEMH.RutaImage5 = RutaImagenes;
                            string pathsave = Server.MapPath(Path.Combine(NuevoAdmoEMH.RutaImage5, NuevoAdmoEMH.ArchivoImagen5));
                            System.IO.File.Move(PathOrigen, pathsave);
                            ArchivosTemporalesEliminar.Add(PathOrigen);
                        }
                        catch (Exception)
                        {
                        }
                    }
                }

                if (NuevoAdmoEMH.NombreArchivo1 != null)
                {
                    string PathOrigen = Server.MapPath(Path.Combine(RutaArchivosBDTemp, NuevoAdmoEMH.NombreArchivo1));
                    if (System.IO.File.Exists(PathOrigen))
                    {
                        try
                        {
                            NuevoAdmoEMH.Ruta1 = RutaArchivosBD;
                            string pathsave = Server.MapPath(Path.Combine(NuevoAdmoEMH.Ruta1, NuevoAdmoEMH.NombreArchivo1));
                            System.IO.File.Move(PathOrigen, pathsave);
                            ArchivosTemporalesEliminar.Add(PathOrigen);
                        }
                        catch (Exception)
                        {
                        }
                    }
                }

                if (NuevoAdmoEMH.NombreArchivo2 != null)
                {
                    string PathOrigen = Server.MapPath(Path.Combine(RutaArchivosBDTemp, NuevoAdmoEMH.NombreArchivo2));
                    if (System.IO.File.Exists(PathOrigen))
                    {
                        try
                        {
                            NuevoAdmoEMH.Ruta2 = RutaArchivosBD;
                            string pathsave = Server.MapPath(Path.Combine(NuevoAdmoEMH.Ruta2, NuevoAdmoEMH.NombreArchivo2));
                            System.IO.File.Move(PathOrigen, pathsave);
                            ArchivosTemporalesEliminar.Add(PathOrigen);
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
                if (NuevoAdmoEMH.NombreArchivo3 != null)
                {
                    string PathOrigen = Server.MapPath(Path.Combine(RutaArchivosBDTemp, NuevoAdmoEMH.NombreArchivo3));
                    if (System.IO.File.Exists(PathOrigen))
                    {
                        try
                        {
                            NuevoAdmoEMH.Ruta3 = RutaArchivosBD;
                            string pathsave = Server.MapPath(Path.Combine(NuevoAdmoEMH.Ruta3, NuevoAdmoEMH.NombreArchivo3));
                            System.IO.File.Move(PathOrigen, pathsave);
                            ArchivosTemporalesEliminar.Add(PathOrigen);
                        }
                        catch (Exception)
                        {
                        }
                    }
                }

                EDAdmoEMH EDAdmoEMH = new EDAdmoEMH();
                EDAdmoEMH.Pk_Id_AdmoEMH = NuevoAdmoEMH.Pk_Id_AdmoEMH;
                EDAdmoEMH.TipoElemento = NuevoAdmoEMH.TipoElemento;
                EDAdmoEMH.NombreElemento = NuevoAdmoEMH.NombreElemento;
                EDAdmoEMH.CodigoElemento = NuevoAdmoEMH.CodigoElemento;
                EDAdmoEMH.Marca = NuevoAdmoEMH.Marca;
                EDAdmoEMH.Modelo = NuevoAdmoEMH.Modelo;
                EDAdmoEMH.Fabricante = NuevoAdmoEMH.Fabricante;
                EDAdmoEMH.Fecha_Fab = NuevoAdmoEMH.Fecha_Fab;
                EDAdmoEMH.HorasVida = NuevoAdmoEMH.HorasVida;
                EDAdmoEMH.Ubicacion = NuevoAdmoEMH.Ubicacion;
                EDAdmoEMH.Caracteristicas = NuevoAdmoEMH.Caracteristicas;
                EDAdmoEMH.NombreResponsable = NuevoAdmoEMH.NombreResponsable;
                EDAdmoEMH.CargoResponsable = NuevoAdmoEMH.CargoResponsable;
                EDAdmoEMH.Estado = NuevoAdmoEMH.Estado;
                EDAdmoEMH.ArchivoImagen1 = NuevoAdmoEMH.ArchivoImagen1;
                EDAdmoEMH.RutaImage1 = NuevoAdmoEMH.RutaImage1;
                EDAdmoEMH.ArchivoImagen2 = NuevoAdmoEMH.ArchivoImagen2;
                EDAdmoEMH.RutaImage2 = NuevoAdmoEMH.RutaImage2;
                EDAdmoEMH.ArchivoImagen3 = NuevoAdmoEMH.ArchivoImagen3;
                EDAdmoEMH.RutaImage3 = NuevoAdmoEMH.RutaImage3;
                EDAdmoEMH.ArchivoImagen4 = NuevoAdmoEMH.ArchivoImagen4;
                EDAdmoEMH.RutaImage4 = NuevoAdmoEMH.RutaImage4;
                EDAdmoEMH.ArchivoImagen5 = NuevoAdmoEMH.ArchivoImagen5;
                EDAdmoEMH.RutaImage5 = NuevoAdmoEMH.RutaImage5;
                EDAdmoEMH.NombreArchivo1 = NuevoAdmoEMH.NombreArchivo1;
                EDAdmoEMH.Ruta1 = NuevoAdmoEMH.Ruta1;
                EDAdmoEMH.NombreArchivo2 = NuevoAdmoEMH.NombreArchivo2;
                EDAdmoEMH.Ruta2 = NuevoAdmoEMH.Ruta2;
                EDAdmoEMH.NombreArchivo3 = NuevoAdmoEMH.NombreArchivo3;
                EDAdmoEMH.Ruta3 = NuevoAdmoEMH.Ruta3;
                EDAdmoEMH.Filedownload1 = NuevoAdmoEMH.NombreArchivo1_download;
                EDAdmoEMH.Filedownload2 = NuevoAdmoEMH.NombreArchivo2_download;
                EDAdmoEMH.Filedownload3 = NuevoAdmoEMH.NombreArchivo3_download;
                EDAdmoEMH.Imgdownload1 = NuevoAdmoEMH.ArchivoImagen1_download;
                EDAdmoEMH.Imgdownload2 = NuevoAdmoEMH.ArchivoImagen2_download;
                EDAdmoEMH.Imgdownload3 = NuevoAdmoEMH.ArchivoImagen3_download;
                EDAdmoEMH.Imgdownload4 = NuevoAdmoEMH.ArchivoImagen4_download;
                EDAdmoEMH.Imgdownload5 = NuevoAdmoEMH.ArchivoImagen5_download;
                EDAdmoEMH.Fk_Id_Empresa = NuevoAdmoEMH.Fk_Id_Empresa;

                bool ProbarGuardado = LNAdmoEMH.GuardarHojaVidaEMH(EDAdmoEMH, ListaPeligro);
                if (ProbarGuardado)
                {

                    Probar = true;
                    return Json(new { Estado, Probar });
                }
            }

            for (int i = 0; i < 15; i++)
            {
                Validacion[i] = false;
                ValidacionStr[i] = "";
            }
            int cont = 0;

            bool FechaInsP = false;
            bool FechaFabP = false;
            bool VidaUtilP = false;

            

            foreach (var kvp in ModelState)
            {
                var key = kvp.Key;
                cont = cont + 1;
            }
            int[] ListaErroresSalida = new int[cont] ;
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
                if (key== "TipoElemento")
                {
                    ListaErroresSalida[cont] = 0;
                    ListaErroresSalidabool[cont] = true;
                }
                if (key == "NombreElemento")
                {
                    ListaErroresSalida[cont] = 1;
                    ListaErroresSalidabool[cont] = true;
                }
                if (key == "CodigoElemento")
                {
                    ListaErroresSalida[cont] = 2;
                    ListaErroresSalidabool[cont] = true;
                }
                if (key == "Marca")
                {
                    ListaErroresSalida[cont] = 3;
                    ListaErroresSalidabool[cont] = true;
                }
                if (key == "Modelo")
                {
                    ListaErroresSalida[cont] = 4;
                    ListaErroresSalidabool[cont] = true;
                }
                if (key == "Fabricante")
                {
                    ListaErroresSalida[cont] = 5;
                    ListaErroresSalidabool[cont] = true;
                }
                if (key == "Fecha_Fab")
                {
                    FechaFabP = true;
                    ListaErroresSalida[cont] = 6;
                    if (GuardarAdmoEMH.Fecha_Fab == DateTime.MinValue)
                    {
                        ListaErroresSalidabool[cont] = true;
                        ValidacionStr[cont] = "No ha digitado el valor de fecha de fabricación";
                    }
                }
                if (key == "HorasVida")
                {
                    VidaUtilP = true;
                    ListaErroresSalida[cont] = 7;
                    if (GuardarAdmoEMH.HorasVida == 0)
                    {
                        ListaErroresSalidabool[cont] = true;
                        ValidacionStr[cont] = "No ha digitado el valor de horas de vida útil";
                    }
                }
                if (key == "Ubicacion")
                {
                    ListaErroresSalida[cont] = 8;
                    ListaErroresSalidabool[cont] = true;
                }
                if (key == "Caracteristicas")
                {
                    ListaErroresSalida[cont] = 9;
                    ListaErroresSalidabool[cont] = true;
                }

                if (key == "NombreResponsable")
                {
                    ListaErroresSalida[cont] = 13;
                    ListaErroresSalidabool[cont] = true;
                }
                if (key == "CargoResponsable")
                {
                    ListaErroresSalida[cont] = 14;
                    ListaErroresSalidabool[cont] = true;
                }
                //if (key == "ActividadIns")
                //{
                //    ListaErroresSalida[cont] = 10;
                //    ListaErroresSalidabool[cont] = true;
                //}

                //if (key == "FechaIns")
                //{
                //    FechaInsP = true;
                //    ListaErroresSalida[cont] = 11;
                //    if (GuardarAdmoEMH.FechaIns == DateTime.MinValue)
                //    {
                //        ListaErroresSalidabool[cont] = true;
                //        ValidacionStr[cont] = "No ha digitado el valor de la fecha de inspección";
                //    }
                //}
                //if (key == "ResponsableIns")
                //{
                //    ListaErroresSalida[cont] = 12;
                //    ListaErroresSalidabool[cont] = true;
                //}
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

            //if (!FechaInsP)
            //{
            //    if (GuardarAdmoEMH.FechaIns == DateTime.MinValue)
            //    {
            //        Validacion[11] = true;
            //        ValidacionStr[11] = "Debe ingresar el valor de fecha de inspección";
            //    }
            //}
            if (!FechaFabP)
            {
                if (GuardarAdmoEMH.Fecha_Fab == DateTime.MinValue)
                {
                    Validacion[6] = true;
                    ValidacionStr[6] = "Debe ingresar el valor de fecha de fabricación";
                }             
            }
            if (!VidaUtilP)
            {
                if (GuardarAdmoEMH.HorasVida == 0)
                {
                    Validacion[7] = true;
                    ValidacionStr[7] = "Debe ingresar el valor de horas de vida útil";
                }
            }


            var Model = GuardarAdmoEMH;
            return Json(new { Model, Estado, Probar, Validacion, ValidacionStr }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GuardarEditarEHM(AdmoEMH GuardarAdmoEMH, List<EDPeligroEMH> ListaPeligro)
        {
            bool ProbarNumero_fechas = true;
            bool Probar = false;
            string Estado = "No se guardó la hoja de vida, por favor revise la información suministrada";
            bool[] Validacion = new bool[15];
            string[] ValidacionStr = new string[15];
            for (int i = 0; i < 15; i++)
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

            if (GuardarAdmoEMH.HorasVida == 0)
            {
                ProbarNumero_fechas = false;
            }
            //if (GuardarAdmoEMH.FechaIns == DateTime.MinValue)
            //{
            //    ProbarNumero_fechas = false;
            //}
            if (GuardarAdmoEMH.Fecha_Fab == DateTime.MinValue)
            {
                ProbarNumero_fechas = false;
            }

            ModelState.Clear();
            TryValidateModel(GuardarAdmoEMH);
            if (ModelState.IsValid && ProbarNumero_fechas)
            {
                if (ListaPeligro == null)
                {
                    ListaPeligro = new List<EDPeligroEMH>();
                }
                AdmoEMH EditarAdmoEMH = new AdmoEMH();
                EditarAdmoEMH = GuardarAdmoEMH;
                EditarAdmoEMH.Fk_Id_Empresa = usuarioActual.IdEmpresa;

                EDAdmoEMH EDAdmoEMHActual = new EDAdmoEMH();
                EDAdmoEMHActual = LNAdmoEMH.ConsultarEHM(EditarAdmoEMH.Pk_Id_AdmoEMH, usuarioActual.IdEmpresa);
                CrearCarpeta(RutaImagenes);
                CrearCarpeta(RutaArchivosBD);
                DateTime datefab = GuardarAdmoEMH.Fecha_Fab;
                EditarAdmoEMH.Fecha_Fab = datefab;
                List<string> ArchivosTemporalesEliminar = new List<string>();
                #region Imagenes
                if (EditarAdmoEMH.ArchivoImagen1 != null)
                {
                    if (EditarAdmoEMH.RutaImage1 != null)
                    {
                        if (EditarAdmoEMH.RutaImage1 != "")
                        {
                            EditarAdmoEMH.RutaImage1 = EDAdmoEMHActual.RutaImage1;
                            EditarAdmoEMH.ArchivoImagen1 = EDAdmoEMHActual.ArchivoImagen1;
                            EditarAdmoEMH.ArchivoImagen1_download = EDAdmoEMHActual.Imgdownload1;
                        }
                        else
                        {
                            string PathOrigen = Server.MapPath(Path.Combine(RutaImagenesTemp, EditarAdmoEMH.ArchivoImagen1));
                            if (System.IO.File.Exists(PathOrigen))
                            {
                                try
                                {
                                    EditarAdmoEMH.RutaImage1 = RutaImagenes;
                                    string pathsave = Server.MapPath(Path.Combine(EditarAdmoEMH.RutaImage1, EditarAdmoEMH.ArchivoImagen1));
                                    System.IO.File.Move(PathOrigen, pathsave);
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
                        string PathOrigen = Server.MapPath(Path.Combine(RutaImagenesTemp, EditarAdmoEMH.ArchivoImagen1));
                        if (System.IO.File.Exists(PathOrigen))
                        {
                            try
                            {
                                EditarAdmoEMH.RutaImage1 = RutaImagenes;
                                string pathsave = Server.MapPath(Path.Combine(EditarAdmoEMH.RutaImage1, EditarAdmoEMH.ArchivoImagen1));
                                System.IO.File.Move(PathOrigen, pathsave);
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
                    if (EDAdmoEMHActual.ArchivoImagen1!=null)
                    {
                        if (EDAdmoEMHActual.ArchivoImagen1 != "")
                        {
                            string PathEliminar = Server.MapPath(Path.Combine(EDAdmoEMHActual.RutaImage1, EDAdmoEMHActual.ArchivoImagen1));
                            ArchivosTemporalesEliminar.Add(PathEliminar);
                        }
                    }
                }
                if (EditarAdmoEMH.ArchivoImagen2 != null)
                {
                    if (EditarAdmoEMH.RutaImage2 != null)
                    {
                        if (EditarAdmoEMH.RutaImage2 != "")
                        {
                            EditarAdmoEMH.RutaImage2 = EDAdmoEMHActual.RutaImage2;
                            EditarAdmoEMH.ArchivoImagen2 = EDAdmoEMHActual.ArchivoImagen2;
                            EditarAdmoEMH.ArchivoImagen2_download = EDAdmoEMHActual.Imgdownload2;
                        }
                        else
                        {
                            string PathOrigen = Server.MapPath(Path.Combine(RutaImagenesTemp, EditarAdmoEMH.ArchivoImagen2));
                            if (System.IO.File.Exists(PathOrigen))
                            {
                                try
                                {
                                    EditarAdmoEMH.RutaImage2 = RutaImagenes;
                                    string pathsave = Server.MapPath(Path.Combine(EditarAdmoEMH.RutaImage2, EditarAdmoEMH.ArchivoImagen2));
                                    System.IO.File.Move(PathOrigen, pathsave);
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
                        string PathOrigen = Server.MapPath(Path.Combine(RutaImagenesTemp, EditarAdmoEMH.ArchivoImagen2));
                        if (System.IO.File.Exists(PathOrigen))
                        {
                            try
                            {
                                EditarAdmoEMH.RutaImage2 = RutaImagenes;
                                string pathsave = Server.MapPath(Path.Combine(EditarAdmoEMH.RutaImage2, EditarAdmoEMH.ArchivoImagen2));
                                System.IO.File.Move(PathOrigen, pathsave);
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
                    if (EDAdmoEMHActual.ArchivoImagen2 != null)
                    {
                        if (EDAdmoEMHActual.ArchivoImagen2 != "")
                        {
                            string PathEliminar = Server.MapPath(Path.Combine(EDAdmoEMHActual.RutaImage2, EDAdmoEMHActual.ArchivoImagen2));
                            ArchivosTemporalesEliminar.Add(PathEliminar);
                        }
                    }
                }
                if (EditarAdmoEMH.ArchivoImagen3 != null)
                {
                    if (EditarAdmoEMH.RutaImage3 != null)
                    {
                        if (EditarAdmoEMH.RutaImage3 != "")
                        {
                            EditarAdmoEMH.RutaImage3 = EDAdmoEMHActual.RutaImage3;
                            EditarAdmoEMH.ArchivoImagen3 = EDAdmoEMHActual.ArchivoImagen3;
                            EditarAdmoEMH.ArchivoImagen3_download = EDAdmoEMHActual.Imgdownload3;
                        }
                        else
                        {
                            string PathOrigen = Server.MapPath(Path.Combine(RutaImagenesTemp, EditarAdmoEMH.ArchivoImagen3));
                            if (System.IO.File.Exists(PathOrigen))
                            {
                                try
                                {
                                    EditarAdmoEMH.RutaImage3 = RutaImagenes;
                                    string pathsave = Server.MapPath(Path.Combine(EditarAdmoEMH.RutaImage3, EditarAdmoEMH.ArchivoImagen3));
                                    System.IO.File.Move(PathOrigen, pathsave);
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
                        string PathOrigen = Server.MapPath(Path.Combine(RutaImagenesTemp, EditarAdmoEMH.ArchivoImagen3));
                        if (System.IO.File.Exists(PathOrigen))
                        {
                            try
                            {
                                EditarAdmoEMH.RutaImage3 = RutaImagenes;
                                string pathsave = Server.MapPath(Path.Combine(EditarAdmoEMH.RutaImage3, EditarAdmoEMH.ArchivoImagen3));
                                System.IO.File.Move(PathOrigen, pathsave);
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
                    if (EDAdmoEMHActual.ArchivoImagen3 != null)
                    {
                        if (EDAdmoEMHActual.ArchivoImagen3 != "")
                        {
                            string PathEliminar = Server.MapPath(Path.Combine(EDAdmoEMHActual.RutaImage3, EDAdmoEMHActual.ArchivoImagen3));
                            ArchivosTemporalesEliminar.Add(PathEliminar);
                        }
                    }
                }
                if (EditarAdmoEMH.ArchivoImagen4 != null)
                {
                    if (EditarAdmoEMH.RutaImage4 != null)
                    {
                        if (EditarAdmoEMH.RutaImage4 != "")
                        {
                            EditarAdmoEMH.RutaImage4 = EDAdmoEMHActual.RutaImage4;
                            EditarAdmoEMH.ArchivoImagen4 = EDAdmoEMHActual.ArchivoImagen4;
                            EditarAdmoEMH.ArchivoImagen4_download = EDAdmoEMHActual.Imgdownload4;
                        }
                        else
                        {
                            string PathOrigen = Server.MapPath(Path.Combine(RutaImagenesTemp, EditarAdmoEMH.ArchivoImagen4));
                            if (System.IO.File.Exists(PathOrigen))
                            {
                                try
                                {
                                    EditarAdmoEMH.RutaImage4 = RutaImagenes;
                                    string pathsave = Server.MapPath(Path.Combine(EditarAdmoEMH.RutaImage4, EditarAdmoEMH.ArchivoImagen4));
                                    System.IO.File.Move(PathOrigen, pathsave);
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
                        string PathOrigen = Server.MapPath(Path.Combine(RutaImagenesTemp, EditarAdmoEMH.ArchivoImagen4));
                        if (System.IO.File.Exists(PathOrigen))
                        {
                            try
                            {
                                EditarAdmoEMH.RutaImage4 = RutaImagenes;
                                string pathsave = Server.MapPath(Path.Combine(EditarAdmoEMH.RutaImage4, EditarAdmoEMH.ArchivoImagen4));
                                System.IO.File.Move(PathOrigen, pathsave);
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
                    if (EDAdmoEMHActual.ArchivoImagen4 != null)
                    {
                        if (EDAdmoEMHActual.ArchivoImagen4 != "")
                        {
                            string PathEliminar = Server.MapPath(Path.Combine(EDAdmoEMHActual.RutaImage4, EDAdmoEMHActual.ArchivoImagen4));
                            ArchivosTemporalesEliminar.Add(PathEliminar);
                        }
                    }
                }
                if (EditarAdmoEMH.ArchivoImagen5 != null)
                {
                    if (EditarAdmoEMH.RutaImage5 != null)
                    {
                        if (EditarAdmoEMH.RutaImage5 != "")
                        {
                            EditarAdmoEMH.RutaImage5 = EDAdmoEMHActual.RutaImage5;
                            EditarAdmoEMH.ArchivoImagen5 = EDAdmoEMHActual.ArchivoImagen5;
                            EditarAdmoEMH.ArchivoImagen5_download = EDAdmoEMHActual.Imgdownload5;
                        }
                        else
                        {
                            string PathOrigen = Server.MapPath(Path.Combine(RutaImagenesTemp, EditarAdmoEMH.ArchivoImagen5));
                            if (System.IO.File.Exists(PathOrigen))
                            {
                                try
                                {
                                    EditarAdmoEMH.RutaImage5 = RutaImagenes;
                                    string pathsave = Server.MapPath(Path.Combine(EditarAdmoEMH.RutaImage5, EditarAdmoEMH.ArchivoImagen5));
                                    System.IO.File.Move(PathOrigen, pathsave);
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
                        string PathOrigen = Server.MapPath(Path.Combine(RutaImagenesTemp, EditarAdmoEMH.ArchivoImagen5));
                        if (System.IO.File.Exists(PathOrigen))
                        {
                            try
                            {
                                EditarAdmoEMH.RutaImage5 = RutaImagenes;
                                string pathsave = Server.MapPath(Path.Combine(EditarAdmoEMH.RutaImage5, EditarAdmoEMH.ArchivoImagen5));
                                System.IO.File.Move(PathOrigen, pathsave);
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
                    if (EDAdmoEMHActual.ArchivoImagen5 != null)
                    {
                        if (EDAdmoEMHActual.ArchivoImagen5 != "")
                        {
                            string PathEliminar = Server.MapPath(Path.Combine(EDAdmoEMHActual.RutaImage5, EDAdmoEMHActual.ArchivoImagen5));
                            ArchivosTemporalesEliminar.Add(PathEliminar);
                        }
                    }
                }
                #endregion
                #region Archivos
                if (EditarAdmoEMH.NombreArchivo1 != null)
                {
                    if (EditarAdmoEMH.Ruta1 != null)
                    {
                        if (EditarAdmoEMH.Ruta1 != "")
                        {
                            EditarAdmoEMH.Ruta1 = EDAdmoEMHActual.Ruta1;
                            EditarAdmoEMH.NombreArchivo1 = EDAdmoEMHActual.NombreArchivo1;
                            EditarAdmoEMH.NombreArchivo1_download = EDAdmoEMHActual.Filedownload1;
                        }
                        else
                        {
                            string PathOrigen = Server.MapPath(Path.Combine(RutaArchivosBDTemp, EditarAdmoEMH.NombreArchivo1));
                            if (System.IO.File.Exists(PathOrigen))
                            {
                                try
                                {
                                    EditarAdmoEMH.Ruta1 = RutaArchivosBD;
                                    string pathsave = Server.MapPath(Path.Combine(EditarAdmoEMH.Ruta1, EditarAdmoEMH.NombreArchivo1));
                                    System.IO.File.Move(PathOrigen, pathsave);
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
                        string PathOrigen = Server.MapPath(Path.Combine(RutaArchivosBDTemp, EditarAdmoEMH.NombreArchivo1));
                        if (System.IO.File.Exists(PathOrigen))
                        {
                            try
                            {
                                EditarAdmoEMH.Ruta1 = RutaArchivosBD;
                                string pathsave = Server.MapPath(Path.Combine(EditarAdmoEMH.Ruta1, EditarAdmoEMH.NombreArchivo1));
                                System.IO.File.Move(PathOrigen, pathsave);
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
                    if (EDAdmoEMHActual.NombreArchivo1 != null)
                    {
                        if (EDAdmoEMHActual.NombreArchivo1 != "")
                        {
                            string PathEliminar = Server.MapPath(Path.Combine(EDAdmoEMHActual.Ruta1, EDAdmoEMHActual.NombreArchivo1));
                            ArchivosTemporalesEliminar.Add(PathEliminar);
                        }
                    }
                }

                if (EditarAdmoEMH.NombreArchivo2 != null)
                {
                    if (EditarAdmoEMH.Ruta2 != null)
                    {
                        if (EditarAdmoEMH.Ruta2 != "")
                        {
                            EditarAdmoEMH.Ruta2 = EDAdmoEMHActual.Ruta2;
                            EditarAdmoEMH.NombreArchivo2 = EDAdmoEMHActual.NombreArchivo2;
                            EditarAdmoEMH.NombreArchivo2_download = EDAdmoEMHActual.Filedownload2;
                        }
                        else
                        {
                            string PathOrigen = Server.MapPath(Path.Combine(RutaArchivosBDTemp, EditarAdmoEMH.NombreArchivo2));
                            if (System.IO.File.Exists(PathOrigen))
                            {
                                try
                                {
                                    EditarAdmoEMH.Ruta2 = RutaArchivosBD;
                                    string pathsave = Server.MapPath(Path.Combine(EditarAdmoEMH.Ruta2, EditarAdmoEMH.NombreArchivo2));
                                    System.IO.File.Move(PathOrigen, pathsave);
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
                        string PathOrigen = Server.MapPath(Path.Combine(RutaArchivosBDTemp, EditarAdmoEMH.NombreArchivo2));
                        if (System.IO.File.Exists(PathOrigen))
                        {
                            try
                            {
                                EditarAdmoEMH.Ruta2 = RutaArchivosBD;
                                string pathsave = Server.MapPath(Path.Combine(EditarAdmoEMH.Ruta2, EditarAdmoEMH.NombreArchivo2));
                                System.IO.File.Move(PathOrigen, pathsave);
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
                    if (EDAdmoEMHActual.NombreArchivo2 != null)
                    {
                        if (EDAdmoEMHActual.NombreArchivo2 != "")
                        {
                            string PathEliminar = Server.MapPath(Path.Combine(EDAdmoEMHActual.Ruta2, EDAdmoEMHActual.NombreArchivo2));
                            ArchivosTemporalesEliminar.Add(PathEliminar);
                        }
                    }
                }
                if (EditarAdmoEMH.NombreArchivo3 != null)
                {
                    if (EditarAdmoEMH.Ruta3 != null)
                    {
                        if (EditarAdmoEMH.Ruta3 != "")
                        {
                            EditarAdmoEMH.Ruta3 = EDAdmoEMHActual.Ruta3;
                            EditarAdmoEMH.NombreArchivo3 = EDAdmoEMHActual.NombreArchivo3;
                            EditarAdmoEMH.NombreArchivo3_download = EDAdmoEMHActual.Filedownload3;
                        }
                        else
                        {
                            string PathOrigen = Server.MapPath(Path.Combine(RutaArchivosBDTemp, EditarAdmoEMH.NombreArchivo3));
                            if (System.IO.File.Exists(PathOrigen))
                            {
                                try
                                {
                                    EditarAdmoEMH.Ruta3 = RutaArchivosBD;
                                    string pathsave = Server.MapPath(Path.Combine(EditarAdmoEMH.Ruta3, EditarAdmoEMH.NombreArchivo3));
                                    System.IO.File.Move(PathOrigen, pathsave);
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
                        string PathOrigen = Server.MapPath(Path.Combine(RutaArchivosBDTemp, EditarAdmoEMH.NombreArchivo3));
                        if (System.IO.File.Exists(PathOrigen))
                        {
                            try
                            {
                                EditarAdmoEMH.Ruta3 = RutaArchivosBD;
                                string pathsave = Server.MapPath(Path.Combine(EditarAdmoEMH.Ruta3, EditarAdmoEMH.NombreArchivo3));
                                System.IO.File.Move(PathOrigen, pathsave);
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
                    if (EDAdmoEMHActual.NombreArchivo3 != null)
                    {
                        if (EDAdmoEMHActual.NombreArchivo3 != "")
                        {
                            string PathEliminar = Server.MapPath(Path.Combine(EDAdmoEMHActual.Ruta3, EDAdmoEMHActual.NombreArchivo3));
                            ArchivosTemporalesEliminar.Add(PathEliminar);
                        }
                    }
                }
                #endregion
                EDAdmoEMH EDAdmoEMH = new EDAdmoEMH();
                EDAdmoEMH.Pk_Id_AdmoEMH = EditarAdmoEMH.Pk_Id_AdmoEMH;
                EDAdmoEMH.TipoElemento = EditarAdmoEMH.TipoElemento;
                EDAdmoEMH.NombreElemento = EditarAdmoEMH.NombreElemento;
                EDAdmoEMH.CodigoElemento = EditarAdmoEMH.CodigoElemento;
                EDAdmoEMH.Marca = EditarAdmoEMH.Marca;
                EDAdmoEMH.Modelo = EditarAdmoEMH.Modelo;
                EDAdmoEMH.Fabricante = EditarAdmoEMH.Fabricante;
                EDAdmoEMH.Fecha_Fab = EditarAdmoEMH.Fecha_Fab;
                EDAdmoEMH.HorasVida = EditarAdmoEMH.HorasVida;
                EDAdmoEMH.Ubicacion = EditarAdmoEMH.Ubicacion;
                EDAdmoEMH.Caracteristicas = EditarAdmoEMH.Caracteristicas;
                EDAdmoEMH.NombreResponsable = EditarAdmoEMH.NombreResponsable;
                EDAdmoEMH.CargoResponsable = EditarAdmoEMH.CargoResponsable;
                EDAdmoEMH.Estado = EditarAdmoEMH.Estado;
                EDAdmoEMH.ArchivoImagen1 = EditarAdmoEMH.ArchivoImagen1;
                EDAdmoEMH.RutaImage1 = EditarAdmoEMH.RutaImage1;
                EDAdmoEMH.ArchivoImagen2 = EditarAdmoEMH.ArchivoImagen2;
                EDAdmoEMH.RutaImage2 = EditarAdmoEMH.RutaImage2;
                EDAdmoEMH.ArchivoImagen3 = EditarAdmoEMH.ArchivoImagen3;
                EDAdmoEMH.RutaImage3 = EditarAdmoEMH.RutaImage3;
                EDAdmoEMH.ArchivoImagen4 = EditarAdmoEMH.ArchivoImagen4;
                EDAdmoEMH.RutaImage4 = EditarAdmoEMH.RutaImage4;
                EDAdmoEMH.ArchivoImagen5 = EditarAdmoEMH.ArchivoImagen5;
                EDAdmoEMH.RutaImage5 = EditarAdmoEMH.RutaImage5;
                EDAdmoEMH.NombreArchivo1 = EditarAdmoEMH.NombreArchivo1;
                EDAdmoEMH.Ruta1 = EditarAdmoEMH.Ruta1;
                EDAdmoEMH.NombreArchivo2 = EditarAdmoEMH.NombreArchivo2;
                EDAdmoEMH.Ruta2 = EditarAdmoEMH.Ruta2;
                EDAdmoEMH.NombreArchivo3 = EditarAdmoEMH.NombreArchivo3;
                EDAdmoEMH.Ruta3 = EditarAdmoEMH.Ruta3;
                EDAdmoEMH.Filedownload1 = EditarAdmoEMH.NombreArchivo1_download;
                EDAdmoEMH.Filedownload2 = EditarAdmoEMH.NombreArchivo2_download;
                EDAdmoEMH.Filedownload3 = EditarAdmoEMH.NombreArchivo3_download;
                EDAdmoEMH.Imgdownload1 = EditarAdmoEMH.ArchivoImagen1_download;
                EDAdmoEMH.Imgdownload2 = EditarAdmoEMH.ArchivoImagen2_download;
                EDAdmoEMH.Imgdownload3 = EditarAdmoEMH.ArchivoImagen3_download;
                EDAdmoEMH.Imgdownload4 = EditarAdmoEMH.ArchivoImagen4_download;
                EDAdmoEMH.Imgdownload5 = EditarAdmoEMH.ArchivoImagen5_download;
                EDAdmoEMH.Fk_Id_Empresa = EditarAdmoEMH.Fk_Id_Empresa;

                bool ProbarGuardado = LNAdmoEMH.EditarHojaVidaEMH(EDAdmoEMH, ListaPeligro);
                if (ProbarGuardado)
                {
                    EliminarArchivos(ArchivosTemporalesEliminar);
                    Probar = true;
                    return Json(new { Estado, Probar });
                }
            }
            for (int i = 0; i < 15; i++)
            {
                Validacion[i] = false;
                ValidacionStr[i] = "";
            }
            int cont = 0;

            bool FechaInsP = false;
            bool FechaFabP = false;
            bool VidaUtilP = false;


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
                if (key == "TipoElemento")
                {
                    ListaErroresSalida[cont] = 0;
                    ListaErroresSalidabool[cont] = true;
                }
                if (key == "NombreElemento")
                {
                    ListaErroresSalida[cont] = 1;
                    ListaErroresSalidabool[cont] = true;
                }
                if (key == "CodigoElemento")
                {
                    ListaErroresSalida[cont] = 2;
                    ListaErroresSalidabool[cont] = true;
                }
                if (key == "Marca")
                {
                    ListaErroresSalida[cont] = 3;
                    ListaErroresSalidabool[cont] = true;
                }
                if (key == "Modelo")
                {
                    ListaErroresSalida[cont] = 4;
                    ListaErroresSalidabool[cont] = true;
                }
                if (key == "Fabricante")
                {
                    ListaErroresSalida[cont] = 5;
                    ListaErroresSalidabool[cont] = true;
                }
                if (key == "Fecha_Fab")
                {
                    FechaFabP = true;
                    ListaErroresSalida[cont] = 6;
                    if (GuardarAdmoEMH.Fecha_Fab == DateTime.MinValue)
                    {
                        ListaErroresSalidabool[cont] = true;
                        ValidacionStr[cont] = "No ha digitado el valor de fecha de fabricación";
                    }
                }

                if (key == "HorasVida")
                {
                    VidaUtilP = true;
                    ListaErroresSalida[cont] = 7;
                    if (GuardarAdmoEMH.HorasVida == 0)
                    {
                        ListaErroresSalidabool[cont] = true;
                        ValidacionStr[cont] = "No ha digitado el valor de horas de vida útil";
                    }
                }
                if (key == "Ubicacion")
                {
                    ListaErroresSalida[cont] = 8;
                    ListaErroresSalidabool[cont] = true;
                }
                if (key == "Caracteristicas")
                {
                    ListaErroresSalida[cont] = 9;
                    ListaErroresSalidabool[cont] = true;
                }

                if (key == "NombreResponsable")
                {
                    ListaErroresSalida[cont] = 13;
                    ListaErroresSalidabool[cont] = true;
                }
                if (key == "CargoResponsable")
                {
                    ListaErroresSalida[cont] = 14;
                    ListaErroresSalidabool[cont] = true;
                }
                if (key == "ActividadIns")
                {
                    ListaErroresSalida[cont] = 10;
                    ListaErroresSalidabool[cont] = true;
                }

                //if (key == "FechaIns")
                //{
                //    FechaInsP = true;
                //    ListaErroresSalida[cont] = 11;
                //    if (GuardarAdmoEMH.FechaIns == DateTime.MinValue)
                //    {
                //        ListaErroresSalidabool[cont] = true;
                //        ValidacionStr[cont] = "No ha digitado el valor de la fecha de inspección";
                //    }
                //}
                if (key == "ResponsableIns")
                {
                    ListaErroresSalida[cont] = 12;
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
            //if (!FechaInsP)
            //{
            //    if (GuardarAdmoEMH.FechaIns == DateTime.MinValue)
            //    {
            //        Validacion[11] = true;
            //        ValidacionStr[11] = "Debe ingresar el valor de fecha de inspección";
            //    }
            //}
            if (!FechaFabP)
            {
                if (GuardarAdmoEMH.Fecha_Fab == DateTime.MinValue)
                {
                    Validacion[6] = true;
                    ValidacionStr[6] = "Debe ingresar el valor de fecha de fabricación";
                }
            }
            if (!VidaUtilP)
            {
                if (GuardarAdmoEMH.HorasVida == 0)
                {
                    Validacion[7] = true;
                    ValidacionStr[7] = "Debe ingresar el valor de horas de vida útil";
                }
                else
                {
                    if (GuardarAdmoEMH.HorasVida > 999999999)
                    {
                        Validacion[7] = true;
                        ValidacionStr[7] = "La horas de vida útil no deben superar las 999999999";
                    }
                }
            }
            var Model = GuardarAdmoEMH;
            return Json(new { Model, Estado, Probar, Validacion, ValidacionStr }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult VerEHM(string IdEHM)
        {
            EDAdmoEMH EDAdmoEMH = new EDAdmoEMH();
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }
            List<EDSede> ListaSedes = new List<EDSede>();
            ListaSedes = LNEmpresa.ObtenerSedesPorNit(usuarioActual.NitEmpresa);
            List<EDProceso> ListaProcesos = new List<EDProceso>();
            ListaProcesos = LNProcesos.ObtenerProcesosPorEmpresa(usuarioActual.NitEmpresa);
            List<EDProceso> ListaProceso_tot = new List<EDProceso>();
            if (ListaProcesos != null)
            {
                foreach (var item in ListaProcesos)
                {
                    ListaProceso_tot.Add(item);
                    List<EDProceso> ListaProcesohijo = LNAdmoEMH.ConsultaSubProcesos(item.Id_Proceso, usuarioActual.IdEmpresa);
                    foreach (var item1 in ListaProcesohijo)
                    {
                        ListaProceso_tot.Add(item1);
                        List<EDProceso> ListaProcesohijo1 = LNAdmoEMH.ConsultaSubProcesos(item1.Id_Proceso, usuarioActual.IdEmpresa);
                        foreach (var item2 in ListaProcesohijo1)
                        {
                            ListaProceso_tot.Add(item2);
                        }
                    }
                }
            }
            int index = 0;

            string RutaImagen1 = string.Empty;
            string RutaImagen2 = string.Empty;
            string RutaImagen3 = string.Empty;
            string RutaImagen4 = string.Empty;
            string RutaImagen5 = string.Empty;

            string RutaArchivo1 = string.Empty;
            string RutaArchivo2 = string.Empty;
            string RutaArchivo3 = string.Empty;

            ViewBag.Imagen1E = "display:none";
            ViewBag.Imagen2E = "display:none";
            ViewBag.Imagen3E = "display:none";
            ViewBag.Imagen4E = "display:none";
            ViewBag.Imagen5E = "display:none";

            ViewBag.Archivo1E = false;
            ViewBag.Archivo2E = false;
            ViewBag.Archivo3E = false;

            ViewBag.Imagen1R = "";
            ViewBag.Imagen2R = "";
            ViewBag.Imagen3R = "";
            ViewBag.Imagen4R = "";
            ViewBag.Imagen5R = "";

            ViewBag.Archivo1R = "";
            ViewBag.Archivo2R = "";
            ViewBag.Archivo3R = "";

            ViewBag.ArchivosE = false;



            int IdEHMInt = 0;
            if (int.TryParse(IdEHM, out IdEHMInt))
            {
                EDAdmoEMH = LNAdmoEMH.ConsultarEHM(IdEHMInt, usuarioActual.IdEmpresa);
                if (EDAdmoEMH.ArchivoImagen1 != null)
                {
                    RutaImagen1 = Server.MapPath(Path.Combine(EDAdmoEMH.RutaImage1, EDAdmoEMH.ArchivoImagen1));
                    if (System.IO.File.Exists(RutaImagen1))
                    {
                        ViewBag.Imagen1E = "display:initial";
                        try
                        {
                            Bitmap bitmap;
                            using (var bmpTemp = new Bitmap(RutaImagen1))
                            {
                                bitmap = new Bitmap(bmpTemp);
                            }
                            using (var newImage = ScaleImage(bitmap, 100, 100))
                            {
                                ViewBag.Imagen1R = "data:image/png;base64," + ImageToBase64String(newImage, ImageFormat.Png);
                            }
                        }
                        catch (Exception)
                        {
                        }

                    }
                }
                if (EDAdmoEMH.ArchivoImagen2 != null)
                {
                    RutaImagen2 = Server.MapPath(Path.Combine(EDAdmoEMH.RutaImage2, EDAdmoEMH.ArchivoImagen2));
                    if (System.IO.File.Exists(RutaImagen2))
                    {
                        ViewBag.Imagen2E = "display:initial";
                        try
                        {
                            Bitmap bitmap;
                            using (var bmpTemp = new Bitmap(RutaImagen2))
                            {
                                bitmap = new Bitmap(bmpTemp);
                            }
                            using (var newImage = ScaleImage(bitmap, 100, 100))
                            {
                                ViewBag.Imagen2R = "data:image/png;base64," + ImageToBase64String(newImage, ImageFormat.Png);
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
                if (EDAdmoEMH.ArchivoImagen3 != null)
                {
                    RutaImagen3 = Server.MapPath(Path.Combine(EDAdmoEMH.RutaImage3, EDAdmoEMH.ArchivoImagen3));
                    if (System.IO.File.Exists(RutaImagen3))
                    {
                        ViewBag.Imagen3E = "display:initial";
                        try
                        {
                            Bitmap bitmap;
                            using (var bmpTemp = new Bitmap(RutaImagen3))
                            {
                                bitmap = new Bitmap(bmpTemp);
                            }
                            using (var newImage = ScaleImage(bitmap, 100, 100))
                            {
                                ViewBag.Imagen3R = "data:image/png;base64," + ImageToBase64String(newImage, ImageFormat.Png);
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
                if (EDAdmoEMH.ArchivoImagen4 != null)
                {
                    RutaImagen4 = Server.MapPath(Path.Combine(EDAdmoEMH.RutaImage4, EDAdmoEMH.ArchivoImagen4));
                    if (System.IO.File.Exists(RutaImagen4))
                    {
                        ViewBag.Imagen4E = "display:initial";
                        try
                        {
                            Bitmap bitmap;
                            using (var bmpTemp = new Bitmap(RutaImagen4))
                            {
                                bitmap = new Bitmap(bmpTemp);
                            }
                            using (var newImage = ScaleImage(bitmap, 100, 100))
                            {
                                ViewBag.Imagen4R = "data:image/png;base64," + ImageToBase64String(newImage, ImageFormat.Png);
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
                if (EDAdmoEMH.ArchivoImagen5 != null)
                {
                    RutaImagen5 = Server.MapPath(Path.Combine(EDAdmoEMH.RutaImage5, EDAdmoEMH.ArchivoImagen5));
                    if (System.IO.File.Exists(RutaImagen5))
                    {
                        ViewBag.Imagen5E = "display:initial";
                        try
                        {
                            Bitmap bitmap;
                            using (var bmpTemp = new Bitmap(RutaImagen5))
                            {
                                bitmap = new Bitmap(bmpTemp);
                            }
                            using (var newImage = ScaleImage(bitmap, 100, 100))
                            {
                                ViewBag.Imagen5R = "data:image/png;base64," + ImageToBase64String(newImage, ImageFormat.Png);
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
                if (EDAdmoEMH.NombreArchivo1 != null)
                {
                    RutaArchivo1 = Server.MapPath(Path.Combine(EDAdmoEMH.Ruta1, EDAdmoEMH.NombreArchivo1));
                    if (System.IO.File.Exists(RutaArchivo1))
                    {
                        ViewBag.ArchivosE = true;
                        ViewBag.Archivo1E = true;
                        ViewBag.Archivo1R = EDAdmoEMH.Ruta1.Replace("~", "") + EDAdmoEMH.NombreArchivo1;
                    }
                }
                if (EDAdmoEMH.NombreArchivo2 != null)
                {
                    RutaArchivo2 = Server.MapPath(Path.Combine(EDAdmoEMH.Ruta2, EDAdmoEMH.NombreArchivo2));
                    if (System.IO.File.Exists(RutaArchivo2))
                    {
                        ViewBag.ArchivosE = true;
                        ViewBag.Archivo2E = true;
                        ViewBag.Archivo2R = EDAdmoEMH.Ruta2.Replace("~", "") + EDAdmoEMH.NombreArchivo2;
                    }
                }
                if (EDAdmoEMH.NombreArchivo3 != null)
                {
                    RutaArchivo3 = Server.MapPath(Path.Combine(EDAdmoEMH.Ruta3, EDAdmoEMH.NombreArchivo3));
                    if (System.IO.File.Exists(RutaArchivo3))
                    {
                        ViewBag.ArchivosE = true;
                        ViewBag.Archivo3E = true;
                        ViewBag.Archivo3R = EDAdmoEMH.Ruta3.Replace("~", "") + EDAdmoEMH.NombreArchivo3;
                    }
                }
                index = EDAdmoEMH.FK_Clasificacion_De_Peligro;
            }
            List<string> ListaSedesPeligros = new List<string>();
            List<string> ListaProcesosPeligros = new List<string>();
            List<string> ListaClasificacionPeligros = new List<string>();

            if (EDAdmoEMH.ListaPeligros != null)
            {
                foreach (var item in EDAdmoEMH.ListaPeligros)
                {
                    string Sede = "";
                    string Proceso = "";
                    string Clasificacion = "";
                    EDSede EDSede = ListaSedes.Where(s => s.IdSede == item.FK_Sede).FirstOrDefault();
                    if (EDSede != null)
                    {
                        Sede = EDSede.NombreSede;
                    }
                    EDProceso EDProceso = ListaProceso_tot.Where(s => s.Id_Proceso == item.FK_Proceso).FirstOrDefault();
                    if (EDProceso != null)
                    {
                        Proceso = EDProceso.Descripcion;
                    }
                    Clasificacion = LNPeligro.ObtenerClasificación(item.FK_Clasificacion_De_Peligro);

                    ListaSedesPeligros.Add(Sede);
                    ListaProcesosPeligros.Add(Proceso);
                    ListaClasificacionPeligros.Add(Clasificacion);
                }
            }
            ViewBag.ListaSedeP = ListaSedesPeligros;
            ViewBag.ListaProcesosP = ListaProcesosPeligros;
            ViewBag.ListaClasificacionP = ListaClasificacionPeligros;
            return View(EDAdmoEMH);
        }
        [HttpGet]
        public ActionResult EditarEHM(string IdEHM)
        {
            EDAdmoEMH EDAdmoEMH = new EDAdmoEMH();
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }
            List<EDSede> ListaSedes = new List<EDSede>();
            ListaSedes = LNEmpresa.ObtenerSedesPorNit(usuarioActual.NitEmpresa);
            List<EDProceso> ListaProcesos = new List<EDProceso>();
            ListaProcesos = LNProcesos.ObtenerProcesosPorEmpresa(usuarioActual.NitEmpresa);
            List<EDProceso> ListaProceso_tot = new List<EDProceso>();
            if (ListaProcesos != null)
            {
                foreach (var item in ListaProcesos)
                {
                    ListaProceso_tot.Add(item);
                    List<EDProceso> ListaProcesohijo = LNAdmoEMH.ConsultaSubProcesos(item.Id_Proceso, usuarioActual.IdEmpresa);
                    foreach (var item1 in ListaProcesohijo)
                    {
                        ListaProceso_tot.Add(item1);
                        List<EDProceso> ListaProcesohijo1 = LNAdmoEMH.ConsultaSubProcesos(item1.Id_Proceso, usuarioActual.IdEmpresa);
                        foreach (var item2 in ListaProcesohijo1)
                        {
                            ListaProceso_tot.Add(item2);
                        }
                    }
                }
            }

            List<EDClasificacionDePeligro> ListaClasPeligros = new List<EDClasificacionDePeligro>();
            List<EDTipoDePeligro> ListaTipoPeligros = LNMetodologia.ObtenerTiposDePeligro();
            List<EDTipoDePeligro> ListaTipoPeligros1 = new List<EDTipoDePeligro>();
            if (ListaTipoPeligros != null)
            {
                ListaTipoPeligros1 = ListaTipoPeligros;
            }
            ViewBag.Pk_Id_Tipo_Peligro = new SelectList(ListaTipoPeligros1, "PK_Tipo_De_Peligro", "Descripcion_Del_Peligro", null);
            ViewBag.Pk_Id_Clasif_Peligro = new SelectList(ListaClasPeligros, "IdClasificacionDePeligro", "DescripcionClaseDePeligro", null);

            int index = 0;

            string RutaImagen1 = string.Empty;
            string RutaImagen2 = string.Empty;
            string RutaImagen3 = string.Empty;
            string RutaImagen4 = string.Empty;
            string RutaImagen5 = string.Empty;

            string RutaArchivo1 = string.Empty;
            string RutaArchivo2 = string.Empty;
            string RutaArchivo3 = string.Empty;

            ViewBag.Imagen1E = "display:none";
            ViewBag.Imagen2E = "display:none";
            ViewBag.Imagen3E = "display:none";
            ViewBag.Imagen4E = "display:none";
            ViewBag.Imagen5E = "display:none";

            ViewBag.Archivo1E = false;
            ViewBag.Archivo2E = false;
            ViewBag.Archivo3E = false;

            ViewBag.Imagen1R = "";
            ViewBag.Imagen2R = "";
            ViewBag.Imagen3R = "";
            ViewBag.Imagen4R = "";
            ViewBag.Imagen5R = "";

            ViewBag.Archivo1R = "";
            ViewBag.Archivo2R = "";
            ViewBag.Archivo3R = "";

            ViewBag.ArchivosE = false;



            int IdEHMInt = 0;
            if (int.TryParse(IdEHM, out IdEHMInt))
            {
                EDAdmoEMH = LNAdmoEMH.ConsultarEHM(IdEHMInt, usuarioActual.IdEmpresa);
                if (EDAdmoEMH.ArchivoImagen1!=null)
                {
                    RutaImagen1=Server.MapPath(Path.Combine(EDAdmoEMH.RutaImage1, EDAdmoEMH.ArchivoImagen1));
                    if (System.IO.File.Exists(RutaImagen1))
                    {
                        ViewBag.Imagen1E = "display:initial";
                        try
                        {
                            Bitmap bitmap;
                            using (var bmpTemp = new Bitmap(RutaImagen1))
                            {
                                bitmap = new Bitmap(bmpTemp);
                            }
                            using (var newImage = ScaleImage(bitmap, 100, 100))
                            {
                                ViewBag.Imagen1R = "data:image/png;base64," + ImageToBase64String(newImage, ImageFormat.Png);
                            }
                        }
                        catch (Exception)
                        {
                        }

                    }
                }
                if (EDAdmoEMH.ArchivoImagen2 != null)
                {
                    RutaImagen2 = Server.MapPath(Path.Combine(EDAdmoEMH.RutaImage2, EDAdmoEMH.ArchivoImagen2));
                    if (System.IO.File.Exists(RutaImagen2))
                    {
                        ViewBag.Imagen2E = "display:initial";
                        try
                        {
                            Bitmap bitmap;
                            using (var bmpTemp = new Bitmap(RutaImagen2))
                            {
                                bitmap = new Bitmap(bmpTemp);
                            }
                            using (var newImage = ScaleImage(bitmap, 100, 100))
                            {
                                ViewBag.Imagen2R = "data:image/png;base64," + ImageToBase64String(newImage, ImageFormat.Png);
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
                if (EDAdmoEMH.ArchivoImagen3 != null)
                {
                    RutaImagen3 = Server.MapPath(Path.Combine(EDAdmoEMH.RutaImage3, EDAdmoEMH.ArchivoImagen3));
                    if (System.IO.File.Exists(RutaImagen3))
                    {
                        ViewBag.Imagen3E = "display:initial";
                        try
                        {
                            Bitmap bitmap;
                            using (var bmpTemp = new Bitmap(RutaImagen3))
                            {
                                bitmap = new Bitmap(bmpTemp);
                            }
                            using (var newImage = ScaleImage(bitmap, 100, 100))
                            {
                                ViewBag.Imagen3R = "data:image/png;base64," + ImageToBase64String(newImage, ImageFormat.Png);
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
                if (EDAdmoEMH.ArchivoImagen4 != null)
                {
                    RutaImagen4 = Server.MapPath(Path.Combine(EDAdmoEMH.RutaImage4, EDAdmoEMH.ArchivoImagen4));
                    if (System.IO.File.Exists(RutaImagen4))
                    {
                        ViewBag.Imagen4E = "display:initial";
                        try
                        {
                            Bitmap bitmap;
                            using (var bmpTemp = new Bitmap(RutaImagen4))
                            {
                                bitmap = new Bitmap(bmpTemp);
                            }
                            using (var newImage = ScaleImage(bitmap, 100, 100))
                            {
                                ViewBag.Imagen4R = "data:image/png;base64," + ImageToBase64String(newImage, ImageFormat.Png);
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
                if (EDAdmoEMH.ArchivoImagen5 != null)
                {
                    RutaImagen5 = Server.MapPath(Path.Combine(EDAdmoEMH.RutaImage5, EDAdmoEMH.ArchivoImagen5));
                    if (System.IO.File.Exists(RutaImagen5))
                    {
                        ViewBag.Imagen5E = "display:initial";
                        try
                        {
                            Bitmap bitmap;
                            using (var bmpTemp = new Bitmap(RutaImagen5))
                            {
                                bitmap = new Bitmap(bmpTemp);
                            }
                            using (var newImage = ScaleImage(bitmap, 100, 100))
                            {
                                ViewBag.Imagen5R = "data:image/png;base64," + ImageToBase64String(newImage, ImageFormat.Png);
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
                if (EDAdmoEMH.NombreArchivo1 != null)
                {
                    RutaArchivo1 = Server.MapPath(Path.Combine(EDAdmoEMH.Ruta1, EDAdmoEMH.NombreArchivo1));
                    if (System.IO.File.Exists(RutaArchivo1))
                    {
                        ViewBag.ArchivosE = true;
                        ViewBag.Archivo1E = true;
                        ViewBag.Archivo1R = EDAdmoEMH.Ruta1.Replace("~", "") + EDAdmoEMH.NombreArchivo1;
                    }
                }
                if (EDAdmoEMH.NombreArchivo2 != null)
                {
                    RutaArchivo2 = Server.MapPath(Path.Combine(EDAdmoEMH.Ruta2, EDAdmoEMH.NombreArchivo2));
                    if (System.IO.File.Exists(RutaArchivo2))
                    {
                        ViewBag.ArchivosE = true;
                        ViewBag.Archivo2E = true;
                        ViewBag.Archivo2R = EDAdmoEMH.Ruta2.Replace("~", "") + EDAdmoEMH.NombreArchivo2;
                    }
                }
                if (EDAdmoEMH.NombreArchivo3 != null)
                {
                    RutaArchivo3 = Server.MapPath(Path.Combine(EDAdmoEMH.Ruta3, EDAdmoEMH.NombreArchivo3));
                    if (System.IO.File.Exists(RutaArchivo3))
                    {
                        ViewBag.ArchivosE = true;
                        ViewBag.Archivo3E = true;
                        ViewBag.Archivo3R = EDAdmoEMH.Ruta3.Replace("~", "") + EDAdmoEMH.NombreArchivo3;
                    }
                }
                index = EDAdmoEMH.FK_Clasificacion_De_Peligro;
            }

            List<string> ListaSedesPeligros = new List<string>();
            List<string> ListaProcesosPeligros = new List<string>();
            List<string> ListaClasificacionPeligros = new List<string>();

            if (EDAdmoEMH.ListaPeligros!=null)
            {
                foreach (var item in EDAdmoEMH.ListaPeligros)
                {
                    string Sede = "";
                    string Proceso = "";
                    string Clasificacion = "";
                    EDSede EDSede = ListaSedes.Where(s => s.IdSede == item.FK_Sede).FirstOrDefault();
                    if (EDSede!=null)
                    {
                        Sede = EDSede.NombreSede;
                    }
                    EDProceso EDProceso= ListaProceso_tot.Where(s => s.Id_Proceso == item.FK_Proceso).FirstOrDefault();
                    if (EDProceso != null)
                    {
                        Proceso = EDProceso.Descripcion;
                    }
                    Clasificacion = LNPeligro.ObtenerClasificación(item.FK_Clasificacion_De_Peligro);

                    ListaSedesPeligros.Add(Sede);
                    ListaProcesosPeligros.Add(Proceso);
                    ListaClasificacionPeligros.Add(Clasificacion);
                }
            }
            ViewBag.ListaSedeP = ListaSedesPeligros;
            ViewBag.ListaProcesosP = ListaProcesosPeligros;
            ViewBag.ListaClasificacionP = ListaClasificacionPeligros;

            return View(EDAdmoEMH);
        }
        [HttpGet]
        public ActionResult DardeBajaEHM(string IdEHM)
        {
            EDAdmoEMH EDAdmoEMH = new EDAdmoEMH();
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }
            int IdEHMInt = 0;
            if (int.TryParse(IdEHM, out IdEHMInt))
            {
                EDAdmoEMH = LNAdmoEMH.ConsultarEHM(IdEHMInt, usuarioActual.IdEmpresa);
            }

            return View(EDAdmoEMH);
        }
        [HttpPost]
        public ActionResult DardeBajaElemento(List<String> values)
        {
            bool Probar = false;
            string Estado = "No se dio de baja al elemento, por favor revise la información suministrada";
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
                Estado = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { Estado, Probar, Validacion, ValidacionStr });
            }
            if (values[2] == null)
            {
                return Json(new { Estado, Probar, Validacion, ValidacionStr });
            }
            else
            {
                if (values[2] == "")
                {
                    return Json(new { Estado, Probar, Validacion, ValidacionStr });
                }
            }
            if (values[0]==null)
            {
                Validacion[0] = true;
                ValidacionStr[0] = "Por favor digite la fecha";
            }
            else
            {
                if (values[0] == "")
                {
                    Validacion[0] = true;
                    ValidacionStr[0] = "Digite la fecha";
                }
            }

            if (values[1] == null)
            {
                Validacion[1] = true;
                ValidacionStr[1] = "Digite el motivo";
            }
            else
            {
                if (values[1] == "")
                {
                    Validacion[1] = true;
                    ValidacionStr[1] = "Digite el motivo";
                }
                else
                {
                    if (values[1].Length>50)
                    {
                        Validacion[1] = true;
                        ValidacionStr[1] = "El límite de caracteres del motivo no puede superar los 50";
                    }
                }
            }
            for (int i = 0; i < 2; i++)
            {
                if (Validacion[i]==true)
                {
                    return Json(new { Estado, Probar, Validacion, ValidacionStr });
                }
            }
            

            if (values[2]!=null)
            {
                int PkEHM = 0;
                if (int.TryParse(values[2], out PkEHM))
                {
                    EDAdmoEMH EDAdmoEMHActual = new EDAdmoEMH();
                    EDAdmoEMHActual = LNAdmoEMH.ConsultarEHM(PkEHM, usuarioActual.IdEmpresa);
                    if (EDAdmoEMHActual.Pk_Id_AdmoEMH==0)
                    {
                        Estado = "No se encuentra el registro de este elemento, por favor vuelva a consultar el registro puede que no exista";
                        return Json(new { Estado, Probar, Validacion, ValidacionStr });
                    }
                    DateTime dt = DateTime.MinValue;
                    try
                    {
                        dt = DateTime.ParseExact(values[0].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        if (dt != DateTime.MinValue)
                        {
                            EDAdmoEMHActual.Fecha_Baja = dt;
                        }
                    }
                    catch (Exception)
                    {
                        try
                        {
                            dt = DateTime.ParseExact(values[0].ToString(), "yyyy/MM/dd", CultureInfo.InvariantCulture);
                            if (dt != DateTime.MinValue)
                            {
                                EDAdmoEMHActual.Fecha_Baja = dt;
                            }
                        }
                        catch (Exception)
                        {
                            return Json(new { Estado, Probar, Validacion, ValidacionStr });
                        }
                    }
                    EDAdmoEMHActual.Motivo_Baja = values[1];
                    EDAdmoEMHActual.Estado = 1;
                    bool ProbarGuardado = LNAdmoEMH.DarBajaEMH(EDAdmoEMHActual);
                    if (ProbarGuardado)
                    {
                        Probar = true;
                        return Json(new { Estado, Probar });
                    }
                }
            }
            return Json(new {Estado, Probar, Validacion, ValidacionStr }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult SubirElemento(string IdEHM)
        {
            bool Probar = false;
            string Estado = "No se completo la operación de deshacer la baja del elemento";

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                Estado = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { Estado, Probar });
            }

            if (IdEHM != null)
            {
                int PkEHM = 0;
                if (int.TryParse(IdEHM, out PkEHM))
                {
                    EDAdmoEMH EDAdmoEMHActual = new EDAdmoEMH();
                    EDAdmoEMHActual = LNAdmoEMH.ConsultarEHM(PkEHM, usuarioActual.IdEmpresa);
                    bool ProbarGuardado = LNAdmoEMH.SubirEMH(EDAdmoEMHActual);
                    if (ProbarGuardado)
                    {
                        Probar = true;
                        return Json(new { Estado, Probar });
                    }

                }
            }
            return Json(new { Estado, Probar }, JsonRequestBehavior.AllowGet);
        }
        #region Consulta

        [HttpPost]
        public JsonResult EliminarEHM(string IdEHM)
        {


            bool probar = false;
            string resultado = "El elemento no ha podido ser eliminado";

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                resultado = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
            }

            int IdElemento = 0;
            bool probarNumero = int.TryParse(IdEHM, out IdElemento);
            if (IdElemento != 0)
            {
                EDAdmoEMH EDAdmoEMH = LNAdmoEMH.ConsultarEHM(IdElemento, usuarioActual.IdEmpresa);
                List<string> ListaArchivos = new List<string>();

                try
                {
                    ListaArchivos.Add(Server.MapPath(Path.Combine(EDAdmoEMH.Ruta1, EDAdmoEMH.NombreArchivo1)));
                }
                catch (Exception)
                {

                }
                try
                {
                    ListaArchivos.Add(Server.MapPath(Path.Combine(EDAdmoEMH.Ruta2, EDAdmoEMH.NombreArchivo2)));
                }
                catch (Exception)
                {

                }
                try
                {
                    ListaArchivos.Add(Server.MapPath(Path.Combine(EDAdmoEMH.Ruta3, EDAdmoEMH.NombreArchivo3)));
                }
                catch (Exception)
                {

                }
                try
                {
                    ListaArchivos.Add(Server.MapPath(Path.Combine(EDAdmoEMH.RutaImage1, EDAdmoEMH.ArchivoImagen1)));
                }
                catch (Exception)
                {

                }
                try
                {
                    ListaArchivos.Add(Server.MapPath(Path.Combine(EDAdmoEMH.RutaImage2, EDAdmoEMH.ArchivoImagen2)));
                }
                catch (Exception)
                {

                }
                try
                {
                    ListaArchivos.Add(Server.MapPath(Path.Combine(EDAdmoEMH.RutaImage3, EDAdmoEMH.ArchivoImagen3)));
                }
                catch (Exception)
                {

                }
                try
                {
                    ListaArchivos.Add(Server.MapPath(Path.Combine(EDAdmoEMH.RutaImage4, EDAdmoEMH.ArchivoImagen4)));
                }
                catch (Exception)
                {

                }
                try
                {
                    ListaArchivos.Add(Server.MapPath(Path.Combine(EDAdmoEMH.RutaImage5, EDAdmoEMH.ArchivoImagen5)));
                }
                catch (Exception)
                {

                }
                bool BorraElemento = LNAdmoEMH.EliminarEHM(IdElemento, usuarioActual.IdEmpresa);
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
                resultado = "El elemento se ha eliminado correctamente";
                return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult ConsultarEHM()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }
            List<EDAdmoEMH> Lista = new List<EDAdmoEMH>();

            //Cargar en el DropDown la lista de tipos de opciones
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "-- Seleccionar --", Value = "", Selected = true });
            items.Add(new SelectListItem { Text = "Máquina", Value = "Máquina" });
            items.Add(new SelectListItem { Text = "Equipo", Value = "Equipo" });
            items.Add(new SelectListItem { Text = "Herramienta", Value = "Herramienta" });
            ViewBag.TipoElemento = items;

            return View(Lista);
        }
        [HttpPost]
        public ActionResult ConsultarEHM(FormCollection frm)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }
            string Tipo = "";
            string Nombre = "";
            if (frm["TipoElemento"] != null)
            {
                Tipo = frm["TipoElemento"].ToString();
            }
            if (frm["NombreElemento"] != null)
            {
                Nombre = frm["NombreElemento"].ToString();
            }
            List<EDAdmoEMH> Lista = LNAdmoEMH.ConsultaAdmoEMH(Tipo, Nombre, usuarioActual.IdEmpresa);

            //Cargar en el DropDown la lista de tipos de opciones
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "-- Seleccionar --", Value = "", Selected = true });
            items.Add(new SelectListItem { Text = "Máquina", Value = "Máquina" });
            items.Add(new SelectListItem { Text = "Equipo", Value = "Equipo" });
            items.Add(new SelectListItem { Text = "Herramienta", Value = "Herramienta" });
            ViewBag.TipoElemento = items;

            return View(Lista);
        }
        [AllowAnonymous]
        public ActionResult EHMPDF(string id, string NitEmpresa, int IdEmpresa)
        {
            EDAdmoEMH EDAdmoEMH = new EDAdmoEMH();
            List<EDSede> ListaSedes = new List<EDSede>();
            ListaSedes = LNEmpresa.ObtenerSedesPorNit(NitEmpresa);
            List<EDClasificacionDePeligro> ListaClasPeligros = new List<EDClasificacionDePeligro>();
            List<EDClasificacionDePeligro> ListaClasPeligros2 = new List<EDClasificacionDePeligro>();
            foreach (var item in ListaSedes)
            {
                List<EDClasificacionDePeligro> ListaClasPeligros1 = LNPeligro.ObtenerClasificaciónPorSede(item.IdSede);
                foreach (var item1 in ListaClasPeligros1)
                {
                    EDClasificacionDePeligro EDClasificacionDePeligroBuscar = new EDClasificacionDePeligro();
                    EDClasificacionDePeligroBuscar = ListaClasPeligros.Where(s => s.IdClasificacionDePeligro == item1.IdClasificacionDePeligro).FirstOrDefault();
                    if (EDClasificacionDePeligroBuscar == null)
                    {
                        ListaClasPeligros.Add(item1);
                    }
                }
            }
            List<EDClasificacionDePeligro> noDuplicados = ListaClasPeligros.Distinct().ToList();
            int index = 0;

            string RutaImagen1 = string.Empty;
            string RutaImagen2 = string.Empty;
            string RutaImagen3 = string.Empty;
            string RutaImagen4 = string.Empty;
            string RutaImagen5 = string.Empty;

            string RutaArchivo1 = string.Empty;
            string RutaArchivo2 = string.Empty;
            string RutaArchivo3 = string.Empty;

            ViewBag.Imagen1E = "text-align:center;display:none";
            ViewBag.Imagen2E = "text-align:center;display:none";
            ViewBag.Imagen3E = "text-align:center;display:none";
            ViewBag.Imagen4E = "text-align:center;display:none";
            ViewBag.Imagen5E = "text-align:center;display:none";

            ViewBag.Archivo1E = false;
            ViewBag.Archivo2E = false;
            ViewBag.Archivo3E = false;

            ViewBag.Imagen1R = SrcWhite;
            ViewBag.Imagen2R = SrcWhite;
            ViewBag.Imagen3R = SrcWhite;
            ViewBag.Imagen4R = SrcWhite;
            ViewBag.Imagen5R = SrcWhite;

            ViewBag.Archivo1R = "";
            ViewBag.Archivo2R = "";
            ViewBag.Archivo3R = "";

            ViewBag.ArchivosE = false;

            ViewBag.ClasPel = "";
            ViewBag.DescPel = "";

            List<EDProceso> ListaProcesos = new List<EDProceso>();
            ListaProcesos = LNProcesos.ObtenerProcesosPorEmpresa(NitEmpresa);
            List<EDProceso> ListaProceso_tot = new List<EDProceso>();
            if (ListaProcesos != null)
            {
                foreach (var item in ListaProcesos)
                {
                    ListaProceso_tot.Add(item);
                    List<EDProceso> ListaProcesohijo = LNAdmoEMH.ConsultaSubProcesos(item.Id_Proceso, IdEmpresa);
                    foreach (var item1 in ListaProcesohijo)
                    {
                        ListaProceso_tot.Add(item1);
                        List<EDProceso> ListaProcesohijo1 = LNAdmoEMH.ConsultaSubProcesos(item1.Id_Proceso, IdEmpresa);
                        foreach (var item2 in ListaProcesohijo1)
                        {
                            ListaProceso_tot.Add(item2);
                        }
                    }
                }
            }

            List<string> ListaProcesosPeligros = new List<string>();
            List<string> ListaClasificacionPeligros = new List<string>();


            ViewBag.ListaProcesosP = ListaProcesosPeligros;
            ViewBag.ListaClasificacionP = ListaClasificacionPeligros;

            int IdEHMInt = 0;
            if (int.TryParse(id, out IdEHMInt))
            {
                EDAdmoEMH = LNAdmoEMH.ConsultarEHM(IdEHMInt, IdEmpresa);
                #region ImagenesPDF
                

                if (EDAdmoEMH.ArchivoImagen1 != null)
                {
                    RutaImagen1 = Server.MapPath(Path.Combine(EDAdmoEMH.RutaImage1, EDAdmoEMH.ArchivoImagen1));
                    
                    if (System.IO.File.Exists(RutaImagen1))
                    {
                        ViewBag.Imagen1E = "text-align:center;display:initial";
                        try
                        {
                            Bitmap bitmap;
                            using (var bmpTemp = new Bitmap(RutaImagen1))
                            {
                                bitmap = new Bitmap(bmpTemp);
                            }
                            using (var newImage = ScaleImage(bitmap, 400, 400))
                            {
                                ViewBag.Imagen1R = "data:image/png;base64," + ImageToBase64String(newImage, ImageFormat.Png);
                            }
                        }
                        catch (Exception)
                        {
                        }

                    }
                }
                if (EDAdmoEMH.ArchivoImagen2 != null)
                {
                    RutaImagen2 = Server.MapPath(Path.Combine(EDAdmoEMH.RutaImage2, EDAdmoEMH.ArchivoImagen2));
                    if (System.IO.File.Exists(RutaImagen2))
                    {
                        ViewBag.Imagen2E = "text-align:center;display:initial";
                        try
                        {
                            Bitmap bitmap;
                            using (var bmpTemp = new Bitmap(RutaImagen2))
                            {
                                bitmap = new Bitmap(bmpTemp);
                            }
                            using (var newImage = ScaleImage(bitmap, 400, 400))
                            {
                                ViewBag.Imagen2R = "data:image/png;base64," + ImageToBase64String(newImage, ImageFormat.Png);
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
                if (EDAdmoEMH.ArchivoImagen3 != null)
                {
                    RutaImagen3 = Server.MapPath(Path.Combine(EDAdmoEMH.RutaImage3, EDAdmoEMH.ArchivoImagen3));
                    if (System.IO.File.Exists(RutaImagen3))
                    {
                        ViewBag.Imagen3E = "text-align:center;display:initial";
                        try
                        {
                            Bitmap bitmap;
                            using (var bmpTemp = new Bitmap(RutaImagen3))
                            {
                                bitmap = new Bitmap(bmpTemp);
                            }
                            using (var newImage = ScaleImage(bitmap, 400, 400))
                            {
                                ViewBag.Imagen3R = "data:image/png;base64," + ImageToBase64String(newImage, ImageFormat.Png);
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
                if (EDAdmoEMH.ArchivoImagen4 != null)
                {
                    RutaImagen4 = Server.MapPath(Path.Combine(EDAdmoEMH.RutaImage4, EDAdmoEMH.ArchivoImagen4));
                    if (System.IO.File.Exists(RutaImagen4))
                    {
                        ViewBag.Imagen4E = "text-align:center;display:initial";
                        try
                        {
                            Bitmap bitmap;
                            using (var bmpTemp = new Bitmap(RutaImagen4))
                            {
                                bitmap = new Bitmap(bmpTemp);
                            }
                            using (var newImage = ScaleImage(bitmap, 400, 400))
                            {
                                ViewBag.Imagen4R = "data:image/png;base64," + ImageToBase64String(newImage, ImageFormat.Png);
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
                if (EDAdmoEMH.ArchivoImagen5 != null)
                {
                    RutaImagen5 = Server.MapPath(Path.Combine(EDAdmoEMH.RutaImage5, EDAdmoEMH.ArchivoImagen5));
                    if (System.IO.File.Exists(RutaImagen5))
                    {
                        ViewBag.Imagen5E = "text-align:center;display:initial";
                        try
                        {
                            Bitmap bitmap;
                            using (var bmpTemp = new Bitmap(RutaImagen5))
                            {
                                bitmap = new Bitmap(bmpTemp);
                            }
                            using (var newImage = ScaleImage(bitmap, 400, 400))
                            {
                                ViewBag.Imagen5R = "data:image/png;base64," + ImageToBase64String(newImage, ImageFormat.Png);
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
                #endregion

                if (EDAdmoEMH.ListaPeligros != null)
                {
                    foreach (var item in EDAdmoEMH.ListaPeligros)
                    {

                        string Proceso = "";
                        string Clasificacion = "";

                        EDProceso EDProceso = ListaProceso_tot.Where(s => s.Id_Proceso == item.FK_Proceso).FirstOrDefault();
                        if (EDProceso != null)
                        {
                            Proceso = EDProceso.Descripcion;
                        }
                        Clasificacion = LNPeligro.ObtenerClasificación(item.FK_Clasificacion_De_Peligro);

                        ListaProcesosPeligros.Add(Proceso);
                        ListaClasificacionPeligros.Add(Clasificacion);
                    }
                }

                ViewBag.ListaProcesosP = ListaProcesosPeligros;
                ViewBag.ListaClasificacionP = ListaClasificacionPeligros;
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


            return View(EDAdmoEMH);
        }
        public ActionResult UrlAsPDF(string id)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }


            //string SwitchNombreEmpresa = usuarioActual.RazonSocialEmpresa;
            //string SwitchNitEmpresa = usuarioActual.NitEmpresa;
            //string SwitchNombreDocumento = "HOJA DE VIDA EQUIPO, MAQUINARIA Y HERRAMIENTAS";

            //var fullFooter = Url.Action("Footer", "AdmoEMH", null, Request.Url.Scheme);
            //var fullHeader = Url.Action("Header", "AdmoEMH", new { NombreEmpresa = SwitchNombreEmpresa, NitEmpresa = SwitchNitEmpresa, NombreInforme = SwitchNombreDocumento }, Request.Url.Scheme);

            //var uriFooter = new Uri(Url.Action("Footer", "AdmoEMH", null, Request.Url.Scheme));
            //var clean1 = uriFooter.GetComponents(UriComponents.AbsoluteUri & ~UriComponents.Port,
            //                   UriFormat.UriEscaped);

            //var uriHeader = new Uri(Url.Action("Header", "AdmoEMH", new { NombreEmpresa = SwitchNombreEmpresa, NitEmpresa = SwitchNitEmpresa, NombreInforme = SwitchNombreDocumento }, Request.Url.Scheme));
            //var clean2 = uriHeader.GetComponents(UriComponents.AbsoluteUri & ~UriComponents.Port,
            //                   UriFormat.UriEscaped);




            int Id_Empresa = usuarioActual.IdEmpresa;
            int Id_EHM = 0;
            bool probar = int.TryParse(id, out Id_EHM);

            //var fullUrl = this.Url.Action("EHMPDF", "AdmoEMH", new { id = Id_EHM, NitEmpresa = SwitchNitEmpresa, IdEmpresa = usuarioActual.IdEmpresa }, this.Request.Url.Scheme);
            //var fullUrl1 = new Uri(this.Url.Action("EHMPDF", "AdmoEMH", new { id = Id_EHM, NitEmpresa = SwitchNitEmpresa, IdEmpresa = usuarioActual.IdEmpresa }, this.Request.Url.Scheme));
            //var clean0 = fullUrl1.GetComponents(UriComponents.AbsoluteUri & ~UriComponents.Port,
            //                   UriFormat.UriEscaped);

            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Host + Request.ApplicationPath.TrimEnd('/') + "/";
            string baseUrlPort = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
            string urlbase = "";
            if (baseUrl.Contains("localhost"))
            {
                urlbase = baseUrlPort;
            }
            else
            {
                urlbase = baseUrl;
            }

            string EncodedRazonSocial = System.Net.WebUtility.UrlEncode(usuarioActual.RazonSocialEmpresa);
            string EncodedNit = System.Net.WebUtility.UrlEncode(usuarioActual.NitEmpresa);
            string EncodedNombreInforme = System.Net.WebUtility.UrlEncode("HOJA DE VIDA EQUIPO, MAQUINARIA Y HERRAMIENTAS");

            var footurl = "https://alissta.gov.co/Acciones/Footer";
            var headerurl = "https://alissta.gov.co/Acciones/Header?NombreEmpresa=" + EncodedRazonSocial + "&NitEmpresa=" + EncodedNit + "&NombreInforme=" + EncodedNombreInforme;

            string cusomtSwitches = string.Format("--footer-line --print-media-type --allow {0} --footer-html {0} --header-line --print-media-type --allow {1} --header-html {1} --header-spacing 5",
            footurl, headerurl);
            var ReporteUrl = "https://alissta.gov.co/AdmoEMH/EHMPDF?id=" + Id_EHM.ToString() + "&NitEmpresa=" + EncodedNit + "&IdEmpresa=" + usuarioActual.IdEmpresa.ToString();

            return new Rotativa.UrlAsPdf(ReporteUrl)
            {
                FileName = "Alissta_EHM" + DateTime.Now.ToString().Replace("/", "").Replace(":", "").Replace(" ", "") + ".pdf"
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
        #region inspecciones
        [HttpGet]
        public ActionResult Inspecciones(string idEHM, string FechaA, string FechaD)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }
            List<EDEHMInspecciones> Lista = new List<EDEHMInspecciones>();
            EDAdmoEMH EDAdmoEMH = new EDAdmoEMH();

            ViewBag.NombreEHM = "";
            ViewBag.IdEHM = "0";
            int IdIntEHM = 0;
            if (int.TryParse(idEHM, out IdIntEHM))
            {
                Lista = LNAdmoEMH.ConsultaInspeccion("", "", IdIntEHM, usuarioActual.IdEmpresa);
                EDAdmoEMH = LNAdmoEMH.ConsultarEHM(IdIntEHM, usuarioActual.IdEmpresa);
            }
            if (EDAdmoEMH.NombreElemento!=null)
            {
                ViewBag.NombreEHM = EDAdmoEMH.NombreElemento;
                
            }
            if (EDAdmoEMH.Pk_Id_AdmoEMH!=0)
            {
                ViewBag.IdEHM = EDAdmoEMH.Pk_Id_AdmoEMH.ToString();
            }
            ViewBag.val_fecha1 = "";
            ViewBag.val_fecha2 = "";
            return View(Lista);
        }
        [HttpPost]
        public ActionResult Inspecciones(FormCollection frm)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }
            string IdEHM = "";
            string FechaAntes = "";
            string FechaDespues = "";
            DateTime FechaA_conv = DateTime.MinValue;
            DateTime FechaD_conv = DateTime.MinValue;
            if (frm["IdEHM_t"] != null)
            {
                IdEHM = frm["IdEHM_t"].ToString();
            }
            if (frm["FechaAntes"] != null)
            {
                FechaAntes = frm["FechaAntes"].ToString();
            }
            if (frm["FechaDespues"] != null)
            {
                FechaDespues = frm["FechaDespues"].ToString();
            }
            
            List<EDEHMInspecciones> Lista = new List<EDEHMInspecciones>();
            EDAdmoEMH EDAdmoEMH = new EDAdmoEMH();
            ViewBag.NombreEHM = "";
            ViewBag.IdEHM = "0";
            int IdIntEHM = 0;
            if (int.TryParse(IdEHM, out IdIntEHM))
            {
                if (FechaAntes!="" && FechaDespues != "")
                {
                    try
                    {
                        FechaA_conv = DateTime.Parse(FechaAntes);
                        FechaD_conv = DateTime.Parse(FechaDespues);
                    }
                    catch (Exception)
                    {
                    }
                }
                if (FechaA_conv!=DateTime.MinValue && FechaD_conv != DateTime.MinValue)
                {
                    Lista = LNAdmoEMH.ConsultaInspeccion(FechaAntes, FechaDespues, IdIntEHM, usuarioActual.IdEmpresa);
                }
                else
                {
                    Lista = LNAdmoEMH.ConsultaInspeccion("", "", IdIntEHM, usuarioActual.IdEmpresa);
                }
                EDAdmoEMH = LNAdmoEMH.ConsultarEHM(IdIntEHM, usuarioActual.IdEmpresa);
            }
            if (EDAdmoEMH.NombreElemento != null)
            {
                ViewBag.NombreEHM = EDAdmoEMH.NombreElemento;
            }
            if (EDAdmoEMH.Pk_Id_AdmoEMH != 0)
            {
                ViewBag.IdEHM = EDAdmoEMH.Pk_Id_AdmoEMH.ToString();
            }

            if (FechaA_conv == DateTime.MinValue)
            {
                FechaAntes = "";
            }
            if (FechaD_conv == DateTime.MinValue)
            {
                FechaDespues = "";
            }
            ViewBag.val_fecha1 = FechaAntes;
            ViewBag.val_fecha2 = FechaDespues;
            return View(Lista);
        }
        [AllowAnonymous]
        [HttpGet]
        public ActionResult ExportInspeccionPDF(string resultado)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }
            string SwitchNombreEmpresa = usuarioActual.RazonSocialEmpresa;
            string SwitchNitEmpresa = usuarioActual.NitEmpresa;
            string SwitchNombreDocumento = "INSPECCIONES POR ELEMENTO";

            var uriFooter = new Uri(Url.Action("Footer", "AdmoEMH", null, Request.Url.Scheme));
            var clean1 = uriFooter.GetComponents(UriComponents.AbsoluteUri & ~UriComponents.Port,
                               UriFormat.UriEscaped);

            var uriHeader = new Uri(Url.Action("Header", "AdmoEMH", new { NombreEmpresa = SwitchNombreEmpresa, NitEmpresa = SwitchNitEmpresa, NombreInforme = SwitchNombreDocumento }, Request.Url.Scheme));
            var clean2 = uriHeader.GetComponents(UriComponents.AbsoluteUri & ~UriComponents.Port,
                               UriFormat.UriEscaped);


            string cusomtSwitches = string.Format("--footer-line --print-media-type --allow {0} --footer-html {0} --header-line --print-media-type --allow {1} --header-html {1} --header-spacing 5",
            clean1, clean2);
            int Id_Empresa = usuarioActual.IdEmpresa;


            var fullUrl = this.Url.Action("InspeccionPDF", "AdmoEMH", new { id = resultado, NitEmpresa = SwitchNitEmpresa, IdEmpresa = usuarioActual.IdEmpresa }, this.Request.Url.Scheme);
            var fullUrl1 = new Uri(this.Url.Action("InspeccionPDF", "AdmoEMH", new { id = resultado, NitEmpresa = SwitchNitEmpresa, IdEmpresa = usuarioActual.IdEmpresa }, this.Request.Url.Scheme));
            var clean0 = fullUrl1.GetComponents(UriComponents.AbsoluteUri & ~UriComponents.Port,
                               UriFormat.UriEscaped);


            return new Rotativa.UrlAsPdf(clean0)
            {
                FileName = "Alissta_InspeccionEHM" + DateTime.Now.ToString().Replace("/", "").Replace(":", "").Replace(" ", "").Replace(".", "") + ".pdf"
                ,
                PageSize = Rotativa.Options.Size.Letter
                ,
                CustomSwitches = cusomtSwitches
            };



        }
        [AllowAnonymous]
        public ActionResult InspeccionPDF(string id, string NitEmpresa, int IdEmpresa)
        {
            List<EDEHMInspecciones> ListaInspecciones = new List<EDEHMInspecciones>();
            string[] EHMArray = id.Split('$');
            foreach (var item in EHMArray)
            {
                int IdintEHM = 0;
                if (int.TryParse(item, out IdintEHM))
                {
                    EDEHMInspecciones EDEHMIns = LNAdmoEMH.ConsultaEHMInspeccion(IdintEHM, IdEmpresa);
                    ListaInspecciones.Add(EDEHMIns);
                }
            }
            return View(ListaInspecciones);
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
        [HttpPost]
        public JsonResult ConsultarPelPorClas(string IdTipoPeligro)
        {
            string Resultado = "";
            bool Probar = false;
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return Json(new { Resultado, Probar }, JsonRequestBehavior.AllowGet);
            }

            if (IdTipoPeligro != null)
            {
                int IdPeligroInt = 0;
                if (int.TryParse(IdTipoPeligro, out IdPeligroInt))
                {
                    string ClasificacionDePeligro = LNPeligro.ObtenerClasificación(IdPeligroInt);
                    if (ClasificacionDePeligro != null)
                    {
                        Resultado = ClasificacionDePeligro;
                        Probar = true;
                        return Json(new { Resultado, Probar }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { Resultado, Probar }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { Resultado, Probar }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { Resultado, Probar }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult BuscarCaracteristicasPeligro(string IdPeligro)
        {
            string Resultado = "";
            bool Probar = false;
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return Json(new { Resultado, Probar }, JsonRequestBehavior.AllowGet);
            }

            if (IdPeligro != null)
            {
                int IdPeligroInt = 0;
                if (int.TryParse(IdPeligro, out IdPeligroInt))
                {
                    string ClasificacionDePeligro = LNPeligro.ObtenerClasificación(IdPeligroInt);
                    if (ClasificacionDePeligro != null)
                    {
                        Resultado = ClasificacionDePeligro;
                        Probar = true;
                        return Json(new { Resultado, Probar }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { Resultado, Probar }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { Resultado, Probar }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { Resultado, Probar }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult GenerarPeligros(EDAdmoEMH EDAdmoEMH)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {

            }
            List<EDPeligro> ObjEmp = new List<EDPeligro>();
            List<EDSede> ListaSedes = new List<EDSede>();
            ListaSedes = LNEmpresa.ObtenerSedesPorNit(usuarioActual.NitEmpresa);

            var Model1 = EDAdmoEMH;
            int IdClas = Model1.FK_Clasificacion_De_Peligro;

            if (IdClas != 0)
            {
                ObjEmp = LNPeligro.ObtenerPeligrosPorClaseyEmpresa(IdClas, usuarioActual.IdEmpresa);
            }
            var Model = ObjEmp;
            return Json(new { Model }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GenerarPeligros1(string IdClasificacion)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
            }
            List<EDPeligro> Model = new List<EDPeligro>();
            List<EDSede> ListaSedes = new List<EDSede>();
            List<EDProceso> ListaProceso = new List<EDProceso>();
            List<EDProceso> ListaProceso_tot = new List<EDProceso>();
            List<EDProceso> ListaProceso_emp = new List<EDProceso>();
            List<EDProceso> ListaProceso_emp1 = LNProcesos.ObtenerProcesosPorEmpresa(usuarioActual.NitEmpresa);
            if (ListaProceso_emp1!=null)
            {
                ListaProceso_emp = ListaProceso_emp1;
                foreach (var item in ListaProceso_emp)
                {
                    ListaProceso_tot.Add(item);
                    List<EDProceso> ListaProcesohijo = LNAdmoEMH.ConsultaSubProcesos(item.Id_Proceso, usuarioActual.IdEmpresa);
                    foreach (var item1 in ListaProcesohijo)
                    {
                        ListaProceso_tot.Add(item1);
                        List<EDProceso> ListaProcesohijo1 = LNAdmoEMH.ConsultaSubProcesos(item1.Id_Proceso, usuarioActual.IdEmpresa);
                        foreach (var item2 in ListaProcesohijo1)
                        {
                            ListaProceso_tot.Add(item2);
                        }
                    }
                }
            }

            int IdClasificacionInt = 0;
            if (!int.TryParse(IdClasificacion,out IdClasificacionInt))
            {
                return Json(new { Model, ListaSedes, ListaProceso }, JsonRequestBehavior.AllowGet);
            }
            //ListaSedes = LNEmpresa.ObtenerSedesPorNit(usuarioActual.NitEmpresa);
            int IdClas = IdClasificacionInt;
            if (IdClas != 0)
            {
                Model = LNPeligro.ObtenerPeligrosPorClaseyEmpresa(IdClas, usuarioActual.IdEmpresa);
                foreach (var item in Model)
                {
                    EDSede EDSede = new EDSede();
                    EDSede=LNEmpresa.ObtenerSedesPorIdSede(item.FK_Sede);
                    if (EDSede!=null)
                    {
                        ListaSedes.Add(EDSede);
                    }
                    else
                    {
                        EDSede.NombreSede = "";
                        ListaSedes.Add(EDSede);
                    }

                    EDProceso EDProceso = new EDProceso();
                    EDProceso EDProceso1 = new EDProceso();
                    EDProceso1 = ListaProceso_tot.Find(s => s.Id_Proceso == item.FK_Proceso);
                    if (EDProceso1 != null)
                    {
                        ListaProceso.Add(EDProceso1);
                    }
                    else
                    {
                        EDProceso.Descripcion = "";
                        ListaProceso.Add(EDProceso);
                    }
                }
            }
            return Json(new { Model, ListaSedes, ListaProceso }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GenerarPeligros2(List<String> values)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
            }
            List<EDPeligro> Model = new List<EDPeligro>();
            List<EDClasificacionDePeligro> ListaClasPeligro = new List<EDClasificacionDePeligro>();
            List<EDSede> ListaSedes = new List<EDSede>();
            List<EDProceso> ListaProceso = new List<EDProceso>();
            List<EDProceso> ListaProceso_emp = LNProcesos.ObtenerProcesosPorEmpresa(usuarioActual.NitEmpresa);
            List<EDProceso> ListaProceso_tot = new List<EDProceso>();
            if (ListaProceso_emp != null)
            {
                foreach (var item in ListaProceso_emp)
                {
                    ListaProceso_tot.Add(item);
                    List<EDProceso> ListaProcesohijo = LNAdmoEMH.ConsultaSubProcesos(item.Id_Proceso, usuarioActual.IdEmpresa);
                    foreach (var item1 in ListaProcesohijo)
                    {
                        ListaProceso_tot.Add(item1);
                        List<EDProceso> ListaProcesohijo1 = LNAdmoEMH.ConsultaSubProcesos(item1.Id_Proceso, usuarioActual.IdEmpresa);
                        foreach (var item2 in ListaProcesohijo1)
                        {
                            ListaProceso_tot.Add(item2);
                        }
                    }
                }
            }


            foreach (var item in values)
            {
                int IdValue = 0;
                try
                {
                    if (int.TryParse(item, out IdValue))
                    {
                        EDPeligro EDPeligro = LNPeligro.ObtenerPeligrosPorId(IdValue);
                        if (EDPeligro.PK_Peligro!=0)
                        {
                            Model.Add(EDPeligro);
                            EDSede EDSede = new EDSede();
                            EDSede EDSede1 = new EDSede();
                            EDSede1 = LNEmpresa.ObtenerSedesPorIdSede(EDPeligro.FK_Sede);
                            if (EDSede1 != null)
                            {
                                EDSede=EDSede1;
                                ListaSedes.Add(EDSede);
                            }
                            else
                            {
                                EDSede.NombreSede = "";
                                ListaSedes.Add(EDSede);
                            }
                            EDProceso EDProceso = new EDProceso();
                            EDProceso EDProceso1 = new EDProceso();
                            EDProceso1 = ListaProceso_tot.Find(s => s.Id_Proceso == EDPeligro.FK_Proceso);
                            if (EDProceso1 != null)
                            {
                                EDProceso = EDProceso1;
                                ListaProceso.Add(EDProceso);
                            }
                            else
                            {
                                EDProceso.Descripcion = "";
                                ListaProceso.Add(EDProceso);
                            }
                            EDClasificacionDePeligro EDClasificacionDePeligro = new EDClasificacionDePeligro();
                            string EDClas = LNPeligro.ObtenerClasificación(EDPeligro.FK_Clasificacion_De_Peligro);
                            if (EDClas != null)
                            {
                                EDClasificacionDePeligro.DescripcionClaseDePeligro = EDClas;
                                ListaClasPeligro.Add(EDClasificacionDePeligro);
                            }
                            else
                            {
                                EDClasificacionDePeligro.DescripcionClaseDePeligro = "";
                                ListaClasPeligro.Add(EDClasificacionDePeligro);
                            }


                        }
                    }
                }
                catch (Exception)
                {
                }
            }
            return Json(new { Model, ListaSedes, ListaProceso, ListaClasPeligro }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult PeligrosDetalles(int IdClas)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                //verificar y enviar a inicio si es necesario
            }
            List<EDSede> ListaSedes = new List<EDSede>();
            ListaSedes = LNEmpresa.ObtenerSedesPorNit(usuarioActual.NitEmpresa);
            List<EDPeligro> ObjEmp = new List<EDPeligro>();
            if (IdClas != 0)
            {
                ObjEmp = LNPeligro.ObtenerPeligrosPorClaseyEmpresa(IdClas, usuarioActual.IdEmpresa);
            }
            return Json(ObjEmp, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Imagenes&Archivos
        [HttpPost]
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
                    resultado = "El límite de manuales que el usuario puede cargar es de 3, por favor si desea agregar este archivo primero elimine un archivo ya cargado";
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
                            string substring = "EHMIMG_file_" + DateTime.Now.ToString().Replace(" ", "").Replace(".", "").Replace(":", "").Replace("/", "");
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
                            resultado = "El archivo que se intento subir no es una imagen o no es un archivo soportado, por favor intente de nuevo con otra imagen";
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
        [HttpPost]
        public ActionResult UploadFiles_ed()
        {
            bool probar = false;
            string resultado = "";
            string[] NombreArchivos = new string[3];
            string[] NombreArchivos_short = new string[3];
            string[] NuevoNombreArchivos = new string[3];
            string[] NuevoNombreArchivos_short = new string[3];
            string[] Carpeta = new string[3];
            string[] Paths = new string[3];
            bool[] display = new bool[3] { false, false, false };
            if (Request.Files.Count > 0)
            {
                string ValImagen1 = string.Empty;
                string ValImagen2 = string.Empty;
                string ValImagen3 = string.Empty;
                string ValImagen1s = string.Empty;
                string ValImagen2s = string.Empty;
                string ValImagen3s = string.Empty;
                string ValImagen1c = string.Empty;
                string ValImagen2c = string.Empty;
                string ValImagen3c = string.Empty;
                string IdEHM = string.Empty;
                try
                {
                    ValImagen1 = Request.Form[0].ToString();
                    ValImagen2 = Request.Form[1].ToString();
                    ValImagen3 = Request.Form[2].ToString();
                    ValImagen1s = Request.Form[3].ToString();
                    ValImagen2s = Request.Form[4].ToString();
                    ValImagen3s = Request.Form[5].ToString();
                    ValImagen1c = Request.Form[6].ToString();
                    ValImagen2c = Request.Form[7].ToString();
                    ValImagen3c = Request.Form[8].ToString();

                    IdEHM = Request.Form[9].ToString();
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
                Carpeta[0] = ValImagen1c;
                Carpeta[1] = ValImagen2c;
                Carpeta[2] = ValImagen3c;
                int IdEHMInt = 0;
                EDAdmoEMH EDAdmoEMH = new EDAdmoEMH();
                var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
                if (int.TryParse(IdEHM, out IdEHMInt))
                {
                    EDAdmoEMH = LNAdmoEMH.ConsultarEHM(IdEHMInt, usuarioActual.IdEmpresa);
                }
                for (int i = 0; i < 3; i++)
                {
                    if (Carpeta[i] != null)
                    {
                        if (Carpeta[i] != "")
                        {
                            if (i == 0)
                            {
                                Paths[i] = Server.MapPath(Path.Combine(EDAdmoEMH.Ruta1, NombreArchivos[i]));
                            }
                            if (i == 1)
                            {
                                Paths[i] = Server.MapPath(Path.Combine(EDAdmoEMH.Ruta2, NombreArchivos[i]));
                            }
                            if (i == 2)
                            {
                                Paths[i] = Server.MapPath(Path.Combine(EDAdmoEMH.Ruta3, NombreArchivos[i]));
                            }
                        }
                        else
                        {
                            Paths[i] = Server.MapPath(Path.Combine(RutaArchivosBDTemp, NombreArchivos[i]));
                        }
                    }
                    else
                    {
                        Paths[i] = Server.MapPath(Path.Combine(RutaArchivosBDTemp, NombreArchivos[i]));
                    }
                }

                int PosicionVacia = -1;
                for (int i = 0; i < 3; i++)
                {
                    if (NombreArchivos[i] == string.Empty)
                    {
                        PosicionVacia = i;
                    }
                    else
                    {
                        if (!System.IO.File.Exists(Paths[i]))
                        {
                            PosicionVacia = i;
                        }
                    }
                }
                if (PosicionVacia == -1)
                {
                    resultado = "El límite de manuales que el usuario puede cargar es de 3, por favor si desea agregar este archivo primero elimine un archivo ya cargado";
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
                            string substring = "EHMIMG_file_" + DateTime.Now.ToString().Replace(" ", "").Replace(".", "").Replace(":", "").Replace("/", "");
                            string ImgFileName = substring + file.FileName;
                            string pathsave = Server.MapPath(Path.Combine(RutaArchivosBDTemp, ImgFileName));
                            file.SaveAs(pathsave);
                            NombreArchivos[PosicionVacia] = ImgFileName;
                            Paths[PosicionVacia] = Server.MapPath(Path.Combine(RutaArchivosBDTemp, ImgFileName));
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
                                    string PathOrigen = Paths[i1];
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
                            resultado = "El archivo que se intento subir no es una imagen o no es un archivo soportado, por favor intente de nuevo con otra imagen";
                            return Json(new { probar, resultado });
                        }
                    }
                    probar = true;
                    return Json(new { probar, resultado, NuevoNombreArchivos, display, NuevoNombreArchivos_short, Carpeta });
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
        public ActionResult UploadImg()
        {
            bool probar = false;
            string resultado = "";
            string[] NombreArchivos = new string[5];
            string[] NuevoNombreArchivos = new string[5];
            string[] NombreArchivos_short = new string[5];
            string[] NuevoNombreArchivos_short = new string[5];
            string[] Thumbnails = new string[5] { SrcWhite, SrcWhite, SrcWhite, SrcWhite, SrcWhite };
            bool[] display = new bool[5] { false, false, false, false, false };
            if (Request.Files.Count > 0)
            {
                string ValImagen1 = string.Empty;
                string ValImagen2 = string.Empty;
                string ValImagen3 = string.Empty;
                string ValImagen4 = string.Empty;
                string ValImagen5 = string.Empty;
                string ValImagen1s = string.Empty;
                string ValImagen2s = string.Empty;
                string ValImagen3s = string.Empty;
                string ValImagen4s = string.Empty;
                string ValImagen5s = string.Empty;
                try
                {
                    ValImagen1 = Request.Form[0].ToString();
                    ValImagen2 = Request.Form[1].ToString();
                    ValImagen3 = Request.Form[2].ToString();
                    ValImagen4 = Request.Form[3].ToString();
                    ValImagen5 = Request.Form[4].ToString();
                    ValImagen1s = Request.Form[5].ToString();
                    ValImagen2s = Request.Form[6].ToString();
                    ValImagen3s = Request.Form[7].ToString();
                    ValImagen4s = Request.Form[8].ToString();
                    ValImagen5s = Request.Form[9].ToString();
                }
                catch (Exception)
                {
                    resultado = "";
                    return Json(new { probar, resultado });
                }
                NombreArchivos[0] = ValImagen1;
                NombreArchivos[1] = ValImagen2;
                NombreArchivos[2] = ValImagen3;
                NombreArchivos[3] = ValImagen4;
                NombreArchivos[4] = ValImagen5;
                NombreArchivos_short[0] = ValImagen1s;
                NombreArchivos_short[1] = ValImagen2s;
                NombreArchivos_short[2] = ValImagen3s;
                NombreArchivos_short[3] = ValImagen4s;
                NombreArchivos_short[4] = ValImagen5s;
                int PosicionVacia = -1;
                for (int i = 0; i < 5; i++)
                {
                    if (NombreArchivos[i] == string.Empty)
                    {
                        PosicionVacia = i;
                    }
                    else
                    {
                        string PathOrigen = Server.MapPath(Path.Combine(RutaImagenesTemp, NombreArchivos[i]));
                        if (!System.IO.File.Exists(PathOrigen))
                        {
                            PosicionVacia = i;
                        }
                    }
                }
                if (PosicionVacia == -1)
                {
                    resultado = "El límite de imagenes que el usuario puede cargar es de 5, por favor si desea agregar esta imagen primero elimine una imagen ya cargada";
                    return Json(new { probar, resultado });
                }
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
                                string ImgFileName = "EHMIMG_" + DateTime.Now.ToString().Replace(" ", "").Replace(".", "").Replace(":", "").Replace("/", "") + file.FileName;
                                string pathsave = Server.MapPath(Path.Combine(RutaImagenesTemp, ImgFileName));
                                file.SaveAs(pathsave);
                                NombreArchivos[PosicionVacia] = ImgFileName;
                                NombreArchivos_short[PosicionVacia] = file.FileName;
                                int IndexNuevo = 0;
                                for (int i1 = 0; i1 < 5; i1++)
                                {
                                    if (NombreArchivos[i1] == string.Empty)
                                    {
                                        display[i1] = false;
                                    }
                                    else
                                    {
                                        string PathOrigen = Server.MapPath(Path.Combine(RutaImagenesTemp, NombreArchivos[i1]));
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
                                                string PathImage = Server.MapPath(Path.Combine(RutaImagenesTemp, NombreArchivos[i1]));
                                                Bitmap bitmap;
                                                using (var bmpTemp = new Bitmap(PathImage))
                                                {
                                                    bitmap = new Bitmap(bmpTemp);
                                                }
                                                using (var newImage = ScaleImage(bitmap, 100, 100))
                                                {
                                                    Thumbnails[IndexNuevo] = "data:image/png;base64," + ImageToBase64String(newImage, ImageFormat.Png);

                                                }
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
                    return Json(new { probar, resultado, NuevoNombreArchivos, Thumbnails, display, NuevoNombreArchivos_short });
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
        public ActionResult UploadImg_ed()
        {
            bool probar = false;
            string resultado = "";
            string[] NombreArchivos = new string[5];
            string[] NuevoNombreArchivos = new string[5];
            string[] NombreArchivos_short = new string[5];
            string[] NuevoNombreArchivos_short = new string[5];
            string[] Carpeta = new string[5];
            string[] Paths = new string[5];
            string[] Thumbnails = new string[5] { SrcWhite, SrcWhite, SrcWhite, SrcWhite, SrcWhite };
            bool[] display = new bool[5] { false, false, false, false, false };
            if (Request.Files.Count > 0)
            {
                string ValImagen1 = string.Empty;
                string ValImagen2 = string.Empty;
                string ValImagen3 = string.Empty;
                string ValImagen4 = string.Empty;
                string ValImagen5 = string.Empty;
                string ValImagen1s = string.Empty;
                string ValImagen2s = string.Empty;
                string ValImagen3s = string.Empty;
                string ValImagen4s = string.Empty;
                string ValImagen5s = string.Empty;
                string ValImagen1c = string.Empty;
                string ValImagen2c = string.Empty;
                string ValImagen3c = string.Empty;
                string ValImagen4c = string.Empty;
                string ValImagen5c = string.Empty;
                string IdEHM = string.Empty;
                try
                {
                    ValImagen1 = Request.Form[0].ToString();
                    ValImagen2 = Request.Form[1].ToString();
                    ValImagen3 = Request.Form[2].ToString();
                    ValImagen4 = Request.Form[3].ToString();
                    ValImagen5 = Request.Form[4].ToString();
                    ValImagen1s = Request.Form[5].ToString();
                    ValImagen2s = Request.Form[6].ToString();
                    ValImagen3s = Request.Form[7].ToString();
                    ValImagen4s = Request.Form[8].ToString();
                    ValImagen5s = Request.Form[9].ToString();
                    ValImagen1c = Request.Form[10].ToString();
                    ValImagen2c = Request.Form[11].ToString();
                    ValImagen3c = Request.Form[12].ToString();
                    ValImagen4c = Request.Form[13].ToString();
                    ValImagen5c = Request.Form[14].ToString();
                    IdEHM = Request.Form[15].ToString();
                }
                catch (Exception)
                {
                    resultado = "";
                    return Json(new { probar, resultado });
                }
                NombreArchivos[0] = ValImagen1;
                NombreArchivos[1] = ValImagen2;
                NombreArchivos[2] = ValImagen3;
                NombreArchivos[3] = ValImagen4;
                NombreArchivos[4] = ValImagen5;
                NombreArchivos_short[0] = ValImagen1s;
                NombreArchivos_short[1] = ValImagen2s;
                NombreArchivos_short[2] = ValImagen3s;
                NombreArchivos_short[3] = ValImagen4s;
                NombreArchivos_short[4] = ValImagen5s;
                Carpeta[0] = ValImagen1c;
                Carpeta[1] = ValImagen2c;
                Carpeta[2] = ValImagen3c;
                Carpeta[3] = ValImagen4c;
                Carpeta[4] = ValImagen5c;
                int IdEHMInt = 0;
                EDAdmoEMH EDAdmoEMH = new EDAdmoEMH();
                var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
                if (int.TryParse(IdEHM, out IdEHMInt))
                {
                    EDAdmoEMH = LNAdmoEMH.ConsultarEHM(IdEHMInt, usuarioActual.IdEmpresa);
                }
                for (int i = 0; i < 5; i++)
                {
                    if (Carpeta[i] != null)
                    {
                        if (Carpeta[i] != "")
                        {
                            if (i == 0)
                            {
                                Paths[i] = Server.MapPath(Path.Combine(EDAdmoEMH.RutaImage1, NombreArchivos[i]));
                            }
                            if (i == 1)
                            {
                                Paths[i] = Server.MapPath(Path.Combine(EDAdmoEMH.RutaImage2, NombreArchivos[i]));
                            }
                            if (i == 2)
                            {
                                Paths[i] = Server.MapPath(Path.Combine(EDAdmoEMH.RutaImage3, NombreArchivos[i]));
                            }
                            if (i == 3)
                            {
                                Paths[i] = Server.MapPath(Path.Combine(EDAdmoEMH.RutaImage4, NombreArchivos[i]));
                            }
                            if (i == 4)
                            {
                                Paths[i] = Server.MapPath(Path.Combine(EDAdmoEMH.RutaImage5, NombreArchivos[i]));
                            }

                        }
                        else
                        {
                            Paths[i] = Server.MapPath(Path.Combine(RutaImagenesTemp, NombreArchivos[i]));
                        }
                    }
                    else
                    {
                        Paths[i] = Server.MapPath(Path.Combine(RutaImagenesTemp, NombreArchivos[i]));
                    }
                }
                int PosicionVacia = -1;
                for (int i = 0; i < 5; i++)
                {
                    if (NombreArchivos[i] == string.Empty)
                    {
                        PosicionVacia = i;
                    }
                    else
                    {
                        if (!System.IO.File.Exists(Paths[i]))
                        {
                            PosicionVacia = i;
                        }
                    }
                }
                if (PosicionVacia == -1)
                {
                    resultado = "El límite de imagenes que el usuario puede cargar es de 5, por favor si desea agregar esta imagen primero elimine una imagen ya cargada";
                    return Json(new { probar, resultado });
                }
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
                                string ImgFileName = "EHMIMG_" + DateTime.Now.ToString().Replace(" ", "").Replace(".", "").Replace(":", "").Replace("/", "") + file.FileName;
                                string pathsave = Server.MapPath(Path.Combine(RutaImagenesTemp, ImgFileName));
                                file.SaveAs(pathsave);
                                NombreArchivos[PosicionVacia] = ImgFileName;
                                Paths[PosicionVacia] = Server.MapPath(Path.Combine(RutaImagenesTemp, ImgFileName));
                                NombreArchivos_short[PosicionVacia] = file.FileName;
                                int IndexNuevo = 0;
                                for (int i1 = 0; i1 < 5; i1++)
                                {
                                    if (NombreArchivos[i1] == string.Empty)
                                    {
                                        display[i1] = false;
                                    }
                                    else
                                    {
                                        string PathOrigen = Paths[i1];
                                        if (!System.IO.File.Exists(Paths[i1]))
                                        {
                                            display[i1] = false;
                                        }
                                        else
                                        {
                                            NuevoNombreArchivos_short[IndexNuevo] = NombreArchivos_short[i1];
                                            NuevoNombreArchivos[IndexNuevo] = NombreArchivos[i1];
                                            try
                                            {
                                                string PathImage = Paths[i1];
                                                Bitmap bitmap;
                                                using (var bmpTemp = new Bitmap(PathImage))
                                                {
                                                    bitmap = new Bitmap(bmpTemp);
                                                }
                                                using (var newImage = ScaleImage(bitmap, 100, 100))
                                                {
                                                    Thumbnails[IndexNuevo] = "data:image/png;base64," + ImageToBase64String(newImage, ImageFormat.Png);

                                                }
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
                    return Json(new { probar, resultado, NuevoNombreArchivos, Thumbnails, display, NuevoNombreArchivos_short, Carpeta });
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
        [HttpPost]
        public JsonResult CargarImagenMostrar(List<String> values)
        {
            bool Probar = false;
            string resultado = "";
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {

                return Json(new { resultado = resultado, Probar = Probar },
                                JsonRequestBehavior.AllowGet);
            }
            if (values[0].ToString() != "null")
            {
                string NombreArchivo = values[0];
                string PathOrigen = Server.MapPath(Path.Combine(RutaImagenesTemp, NombreArchivo));
                if (System.IO.File.Exists(PathOrigen))
                {
                    resultado = "/Content/ArchivosEquipos/ImagenesTempEquipos/" + NombreArchivo;
                    Probar = true;
                    return Json(new { resultado = resultado, Probar = Probar },
                                JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { resultado = resultado, Probar = Probar },
                                JsonRequestBehavior.AllowGet);
            }
            return Json(new { resultado = resultado, Probar = Probar },
                                JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult CargarImagenMostrar_ed(List<String> values)
        {
            bool Probar = false;
            string resultado = "";
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {

                return Json(new { resultado = resultado, Probar = Probar },
                                JsonRequestBehavior.AllowGet);
            }
            if (values[0].ToString() != "null")
            {
                string NombreArchivo = values[0];
                string path = "";
                if (values[1].ToString() != null)
                {
                    if (values[1].ToString() != "")
                    {
                        int IdEHMInt = 0;
                        EDAdmoEMH EDAdmoEMH = new EDAdmoEMH();
                        if (int.TryParse(values[2], out IdEHMInt))
                        {
                            EDAdmoEMH = LNAdmoEMH.ConsultarEHM(IdEHMInt, usuarioActual.IdEmpresa);
                        }
                        if (values[3] == "1")
                        {
                            path = EDAdmoEMH.RutaImage1;
                        }
                        if (values[3] == "2")
                        {
                            path = EDAdmoEMH.RutaImage2;
                        }
                        if (values[3] == "3")
                        {
                            path = EDAdmoEMH.RutaImage3;
                        }
                        if (values[3] == "4")
                        {
                            path = EDAdmoEMH.RutaImage4;
                        }
                        if (values[3] == "5")
                        {
                            path = EDAdmoEMH.RutaImage5;
                        }
                    }
                    else
                    {
                        path = RutaImagenesTemp;
                    }
                }
                else
                {
                    path = RutaImagenesTemp;
                }

                if (System.IO.File.Exists(Server.MapPath(Path.Combine(path, NombreArchivo))))
                {
                    resultado = path.Replace("~", "") + NombreArchivo; ;
                    Probar = true;
                    return Json(new { resultado = resultado, Probar = Probar },
                                JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { resultado = resultado, Probar = Probar },
                                JsonRequestBehavior.AllowGet);
            }
            return Json(new { resultado = resultado, Probar = Probar },
                                JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult DescargarArchivo(List<String> values)
        {
            //values[0] IdArchivo
            //values[1] TempDataId
            bool probar = false;
            string resultado = "El archivo que desea descargar no se encuentra disponible";
            string nombre = "";
            string proviene = "";
            string nombre_download = values[1];

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                resultado = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { probar, resultado });
            }

            if (values[0].ToString() != "null")
            {
                string NombreArchivo = values[0];
                nombre = NombreArchivo;
                string path = "";
                if (values[4].ToString() != null)
                {
                    if (values[4].ToString() != "")
                    {
                        int IdEHMInt = 0;
                        EDAdmoEMH EDAdmoEMH = new EDAdmoEMH();
                        if (int.TryParse(values[2], out IdEHMInt))
                        {
                            EDAdmoEMH = LNAdmoEMH.ConsultarEHM(IdEHMInt, usuarioActual.IdEmpresa);
                        }
                        if (values[3] == "1")
                        {
                            
                            path = Server.MapPath(Path.Combine(EDAdmoEMH.Ruta1, NombreArchivo));
                            proviene = "db";
                        }
                        if (values[3] == "2")
                        {
                            path = Server.MapPath(Path.Combine(EDAdmoEMH.Ruta2, NombreArchivo));
                            proviene = "db";
                        }
                        if (values[3] == "3")
                        {
                            path = Server.MapPath(Path.Combine(EDAdmoEMH.Ruta3, NombreArchivo));
                            proviene = "db";
                        }
                    }
                    else
                    {
                        path = Server.MapPath(Path.Combine(RutaArchivosBDTemp, NombreArchivo));
                        proviene = "temp";
                    }
                }
                else
                {
                    path = Server.MapPath(Path.Combine(RutaArchivosBDTemp, NombreArchivo));
                    proviene = "temp";
                }


                if (System.IO.File.Exists(path))
                {
                    probar = true;
                    return Json(new { probar, resultado, nombre, nombre_download, proviene },JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                //no hay nombre de archivo
            }

            //int IdEHMInt = 0;
            //EDAdmoEMH EDAdmoEMH = new EDAdmoEMH();
            //if (int.TryParse(values[2], out IdEHMInt))
            //{
            //    EDAdmoEMH = LNAdmoEMH.ConsultarEHM(IdEHMInt, usuarioActual.IdEmpresa);
            //}



            return Json(new { probar, resultado },
            JsonRequestBehavior.AllowGet);
        }
        public ActionResult Download(string Nombre, string Proviene, string Numero, string Nreal, string IdEHM)
        {
            string Ruta = "";
            string NombreArchivo = Nombre;


            if (Proviene == "db")
            {
                var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
                if (usuarioActual != null)
                {
                    EDAdmoEMH EDAdmoEMH = new EDAdmoEMH();
                    int IdEHMInt = 0;
                    if (int.TryParse(IdEHM, out IdEHMInt))
                    {
                        EDAdmoEMH = LNAdmoEMH.ConsultarEHM(IdEHMInt, usuarioActual.IdEmpresa);

                        if (Numero == "1")
                        {
                            Ruta = EDAdmoEMH.Ruta1;
                        }
                        if (Numero == "2")
                        {
                            Ruta = EDAdmoEMH.Ruta2;
                        }
                        if (Numero == "3")
                        {
                            Ruta = EDAdmoEMH.Ruta3;
                        }
                    }
                }

            }
            if (Proviene == "temp")
            {
                Ruta = RutaArchivosBDTemp;
            }

            byte[] fileBytes_1 = System.IO.File.ReadAllBytes(Server.MapPath(Path.Combine(Ruta, NombreArchivo)));
            return File(fileBytes_1, System.Net.Mime.MediaTypeNames.Application.Octet, Nreal);
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
        private string FechaString()
        {
            DateTime FechaHoraActual = DateTime.Now;
            string FechaHoraStr = FechaHoraActual.ToString().Replace(" ", "").Replace("p", "").Replace("a", "").Replace("m", "");
            FechaHoraStr = FechaHoraStr.Replace("/", "").Replace(":", "").Replace(".", "");
            return FechaHoraStr;
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
        #endregion
    }
}