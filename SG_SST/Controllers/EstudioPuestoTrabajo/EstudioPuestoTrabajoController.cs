using SG_SST.Audotoria;
using SG_SST.Controllers.Base;
using SG_SST.EntidadesDominio.Empresas;
using SG_SST.EntidadesDominio.EstudioPuestoTrabajo;
using SG_SST.Logica.EstudioPuestoTrabajo;
using SG_SST.Models;
using SG_SST.Models.EstudioPuestoTrabajo;
using SG_SST.ServiceRequest;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SG_SST.Controllers.MedicionEvaluacion
{
    public class EstudioPuestoTrabajoController : BaseController
    {
        RegistraLog registroLog = new RegistraLog();
        
        string rutaArchivosPT = ConfigurationManager.AppSettings["rutaArchivosEstudioPuestoTrabajo"];
        string GrabarEstudioPT = ConfigurationManager.AppSettings["CapacidadGrabarEstudioPT"];
        string GrabarSeguimientoPT = ConfigurationManager.AppSettings["CapacidadGrabarSeguimientoPT"];
        string MostrarSeguimientoPT = ConfigurationManager.AppSettings["CapacidadMostrarSeguimientoPT"];
        string ConsultarEstudioPTXNumIden = ConfigurationManager.AppSettings["CapacidadConsultarEstudioXNumIden"];
        string ConsultarCargosPT = ConfigurationManager.AppSettings["CapacidadConsultarCargosPT"];
        string ConsultarEstudioPTXCargo = ConfigurationManager.AppSettings["CapacidadConsultarEstudioXCargo"];
        string GrabarArchivoPT = ConfigurationManager.AppSettings["CapacidadGrabarArchivoPT"];
        string MostrarArchivosPT = ConfigurationManager.AppSettings["CapacidadMostrarArchivosPT"];
        public ActionResult Index()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                return View();
            }
            var estudio = new EstudioPuestoTrabajoModel();

            DateTime dateTime = DateTime.Now;
            estudio.FechaAnalisis = dateTime;
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("NIT", usuarioActual.NitEmpresa);
            var resultSede = ServiceClient.ObtenerArrayJsonRestFul<EDSede>(urlServicioEmpresas, CapacidadObtenerSedesPorNit, RestSharp.Method.GET);
            if (resultSede != null && resultSede.Count() > 0)
            {
                estudio.Sedes = resultSede.Select(c => new SelectListItem()
                {
                    Value = c.IdSede.ToString(),
                    Text = c.NombreSede
                }).ToList();
            }
            else
                estudio.Sedes = new List<SelectListItem>();

            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("NIT", usuarioActual.NitEmpresa);
            var resultProceso = ServiceClient.ObtenerArrayJsonRestFul<EDProceso>(urlServicioEmpresas, CapacidadObtenerprocesosEmpresa, RestSharp.Method.GET);
            if (resultProceso != null && resultProceso.Count() > 0)
            {
                estudio.Procesos = resultProceso.Select(p => new SelectListItem()
                {
                    Value = p.Id_Proceso.ToString(),
                    Text = p.Descripcion
                }).ToList();
            }

            ServiceClient.EliminarParametros();
            var resultObjetivoAnalisis = ServiceClient.ObtenerArrayJsonRestFul<EDObjetivoAnalisis>(urlServicioPlanificacion, CapacidadObtenerObjetivoAnalisis, RestSharp.Method.GET);
            if (resultObjetivoAnalisis != null && resultObjetivoAnalisis.Count() > 0)
            {
                estudio.ObjetivosAnalisis = resultObjetivoAnalisis.Select(d => new SelectListItem()
                {
                    Value = d.IdObjetivoAnlaisis.ToString(),
                    Text = d.NombreObjetivoAnalisis
                }).ToList();
            }

            ServiceClient.EliminarParametros();
            var resultTipoAnalisis = ServiceClient.ObtenerArrayJsonRestFul<EDTipoAnalisisPuestoTrabajo>(urlServicioPlanificacion, CapacidadObtenerTipoAnalisis, RestSharp.Method.GET);
            if (resultTipoAnalisis != null && resultTipoAnalisis.Count() > 0)
            {
                estudio.TipoAnalisisPT = resultTipoAnalisis.Select(f => new SelectListItem()
                {
                    Value = f.IdTipoAnalisisPT.ToString(),
                    Text = f.NombreTipoAnalisisPT
                }).ToList();
            }

            return View(estudio);
        }

        [HttpPost]
        public JsonResult RegistrarNuevoSeguimiento()
        {
            try
            {
                var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
                var objNuevo = new SeguimientoEstudioPTModel();
                objNuevo.Fecha = DateTime.Now;
                var datos = RenderRazorViewToString("_NuevoSeguimientoPT", objNuevo);

                return Json(new { Data = datos, Mensaje = "Success" });
            }
            catch (Exception ex)
            {
                registroLog.RegistrarError(typeof(EstudioPuestoTrabajoController), string.Format("Error en la accion RegistrarNuevaAusencia. Hora: {0}, Error: {1}", DateTime.Now, ex.StackTrace), ex);
                return Json(new { Data = "", Mensaje = "Error" });
            }
        }

        /// <summary>
        /// Se guarda un nuevo Estudio.
        /// </summary>
        /// <param name="objNuevoEstudio"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult NuevoEstudio(EstudioPuestoTrabajoModel objNuevoEstudio)
        {
            var Estudio = new EDEstudioPuestoTrabajo();

            Estudio.NumeroIdentificacion = objNuevoEstudio.Documento;
            Estudio.Apellido1 = objNuevoEstudio.Apellido1;
            Estudio.Apellido2 = objNuevoEstudio.Apellido2;
            Estudio.Nombre1 = objNuevoEstudio.Nombre1;
            Estudio.Nombre2 = objNuevoEstudio.Nombre2;
            Estudio.Cargo = objNuevoEstudio.Cargo;
            Estudio.IdSede = objNuevoEstudio.idSede;
            Estudio.IdProceso = objNuevoEstudio.idProceso;
            Estudio.IdDiagnostico = objNuevoEstudio.idDiagnostico;
            Estudio.IdObjetivoAnalisis = objNuevoEstudio.idObjetivo;
            Estudio.IdTipoAnalisis = objNuevoEstudio.idTipoAnalisisPT;
            var format = "ddMMyyyy";
            string fechaFin = objNuevoEstudio.FechaAnalisisStr.Replace("/", "");
            Estudio.FechaAnalisis = DateTime.ParseExact(fechaFin, format, System.Globalization.CultureInfo.InvariantCulture);

            ServiceClient.EliminarParametros();
            var resultEstudioPT = ServiceClient.RealizarPeticionesPostJsonRestFul<EDEstudioPuestoTrabajo>(urlServicioPlanificacion, GrabarEstudioPT, Estudio);

            if (resultEstudioPT != null)
            {
                if (resultEstudioPT.IdEstudioPuestoTrabajo > 0)
                {
                    ViewBag.IdEstudioPuestoTrabajo = resultEstudioPT.IdEstudioPuestoTrabajo;
                    return Json(new { status = "Success", Message = "El nuevo estudio de puesto de trabajo se registró con éxito.", Id = resultEstudioPT.IdEstudioPuestoTrabajo });
                }
                else
                    return Json(new { status = "Error", Message = "No fue posible registrar el nuevo estudio de puesto de trabajo. Intente nuevamente." });


            }
            else
                return Json(new { status = "Error", Message = "No fue posible registrar el nuevo estudio de puesto de trabajo. Intente nuevamente." });
        }

        /// <summary>
        /// Se guarda un nuevo Seguimiento.
        /// </summary>
        /// <param name="objNuevoSeguimiento"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult NuevoSeguimiento(SeguimientoEstudioPTModel objNuevoSeguimiento)
        {
            var Seguimiento = new EDSeguimientoEstudioPuestoTrabajo();

            Seguimiento.Actividad = objNuevoSeguimiento.Actividad;
            var format = "ddMMyyyy";
            string fechaFin = objNuevoSeguimiento.FechaStr.Replace("/", "");
            Seguimiento.Fecha = DateTime.ParseExact(fechaFin, format, System.Globalization.CultureInfo.InvariantCulture);
            Seguimiento.Responsable = objNuevoSeguimiento.Responsable;
            Seguimiento.IdEstudioPuestoTrabajo = objNuevoSeguimiento.IdEstudioPuestoTrabajo;

            ServiceClient.EliminarParametros();
            var resultSeguimientoPT = ServiceClient.RealizarPeticionesPostJsonRestFul<EDSeguimientoEstudioPuestoTrabajo>(urlServicioPlanificacion, GrabarSeguimientoPT, Seguimiento);

            if (resultSeguimientoPT != null)
            {
                if (resultSeguimientoPT.Result.Equals("SUCCESS"))
                {
                    return Json(new { status = "Success", Message = "El nuevo seguimiento de estudio de puesto de trabajo se registró con éxito." });
                }
                else
                    return Json(new { status = "Error", Message = "No fue posible registrar el nuevo seguimiento de estudio de puesto de trabajo. Intente nuevamente." });

            }
            else
                return Json(new { status = "Error", Message = "No fue posible registrar el nuevo seguimiento de estudio de puesto de trabajo. Intente nuevamente." });

        }

        [HttpPost]
        public JsonResult MostrarGridSeguimiento(SeguimientoEstudioPTModel objNuevoSeguimiento)
        {
            try
            {
                EDEstudioPuestoTrabajo EdEstudioPT = new EDEstudioPuestoTrabajo();
                EdEstudioPT.IdEstudioPuestoTrabajo = objNuevoSeguimiento.IdEstudioPuestoTrabajo;

                List<EDSeguimientoEstudioPuestoTrabajo> listSeguimiento = new List<EDSeguimientoEstudioPuestoTrabajo>();

                ServiceClient.EliminarParametros();
                var resultSegEstudioPT = ServiceClient.RealizarPeticionesArrayPostJsonRest<EDSeguimientoEstudioPuestoTrabajo>(urlServicioPlanificacion, MostrarSeguimientoPT, EdEstudioPT);
                if (resultSegEstudioPT != null)
                {
                    if (resultSegEstudioPT.Count() > 0)
                    {
                        if (resultSegEstudioPT[0] != null)
                        {
                            listSeguimiento = resultSegEstudioPT.Select(o => new EDSeguimientoEstudioPuestoTrabajo
                            {
                                Actividad = o.Actividad,
                                Fecha = o.Fecha,
                                Responsable = o.Responsable,
                                FechaStr = o.Fecha.ToString("dd/MM/yyyy")
                            }).ToList();
                        }
                    }
                }

                EdEstudioPT.listaSeguimiento = listSeguimiento;
                var datos = RenderRazorViewToString("GridSeguimiento", EdEstudioPT);

                return Json(new { Data = datos, Mensaje = "Success" });
            }
            catch (Exception ex)
            {
                registroLog.RegistrarError(typeof(EstudioPuestoTrabajoController), string.Format("Error en la accion RegistrarNuevaAusencia. Hora: {0}, Error: {1}", DateTime.Now, ex.StackTrace), ex);
                return Json(new { Data = "", Mensaje = "Error" });
            }
        }

        [HttpPost]
        public JsonResult GuardarArchivos()
        {
            try
            {
                var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
                var objNuevo = new SeguimientoEstudioPTModel();
                Int16 IdEstudio = 0;
                if (Request.Form.Count > 0)
                {
                    IdEstudio = Convert.ToInt16(Request.Form["Id"]);
                }
                if (Request.Files.Count > 0)
                {
                    HttpFileCollectionBase files = Request.Files;
                    HttpPostedFileBase file = files[0];
                    var Archivo = new EDArchivoEstudioPuestoTrabajo();
                    Archivo.NombreArchivo = file.FileName;
                    Archivo.RutaArchivo = Path.Combine(rutaArchivosPT, usuarioActual.NitEmpresa, file.FileName);
                    Archivo.IdEstudioPuestoTrabajo = IdEstudio;

                    ServiceClient.EliminarParametros();
                    var resultArchivoPT = ServiceClient.RealizarPeticionesPostJsonRestFul<EDArchivoEstudioPuestoTrabajo>(urlServicioPlanificacion, GrabarArchivoPT, Archivo);
                    if (resultArchivoPT != null)
                    {
                        if (resultArchivoPT.Result.Equals("SUCCESS"))
                        {
                            var img = Path.Combine(rutaArchivosPT, usuarioActual.NitEmpresa, file.FileName);
                            if (!Directory.Exists(Path.Combine(rutaArchivosPT, usuarioActual.NitEmpresa)))
                            {
                                Directory.CreateDirectory(Path.Combine(rutaArchivosPT, usuarioActual.NitEmpresa));
                            }
                            file.SaveAs(img);
                            return Json(new { status = "Success", Message = "El archivo se registró con éxito." });
                        }
                        else
                            return Json(new { status = "Error", Message = "No fue posible registrar el archivo. Intente nuevamente." });
                    }
                    else
                        return Json(new { status = "Error", Message = "No fue posible registrar el archivo. Intente nuevamente." });
                }

                return Json(new { Data = "", Mensaje = "Error" });

            }
            catch (Exception ex)
            {
                registroLog.RegistrarError(typeof(EstudioPuestoTrabajoController), string.Format("Error en la accion RegistrarNuevaAusencia. Hora: {0}, Error: {1}", DateTime.Now, ex.StackTrace), ex);
                return Json(new { Data = "", Mensaje = "Error" });
            }
        }

        [HttpPost]
        public JsonResult ConsultarEstudioXNumIden(EstudioPuestoTrabajoModel objEstudio)
        {
            try
            {
                EDEstudioPuestoTrabajo EdEstudioPT = new EDEstudioPuestoTrabajo();
                EdEstudioPT.NumeroIdentificacion = objEstudio.Documento;

                List<EDEstudioPuestoTrabajo> listEstudioPT = new List<EDEstudioPuestoTrabajo>();

                ServiceClient.EliminarParametros();
                var resultEstudioPT = ServiceClient.RealizarPeticionesArrayPostJsonRest<EDEstudioPuestoTrabajo>(urlServicioPlanificacion, ConsultarEstudioPTXNumIden, EdEstudioPT);
                if (resultEstudioPT != null)
                {
                    if (resultEstudioPT.Count() > 0)
                    {
                        if (resultEstudioPT[0] != null)
                        {
                            listEstudioPT = resultEstudioPT.Select(o => new EDEstudioPuestoTrabajo
                            {
                                NumeroIdentificacion = o.NumeroIdentificacion,
                                Apellido1 = o.Apellido1,
                                Apellido2 = o.Apellido2,
                                Nombre1 = o.Nombre1,
                                Nombre2 = o.Nombre2,
                                Cargo = o.Cargo,
                                IdSede = o.IdSede,
                                IdProceso = o.IdProceso,
                                IdDiagnostico = o.IdDiagnostico,
                                Diagnostico = o.Diagnostico,
                                IdObjetivoAnalisis = o.IdObjetivoAnalisis,
                                IdTipoAnalisis = o.IdTipoAnalisis,
                                FechaAnalisisStr = o.FechaAnalisis.ToString("dd/MM/yyyy"),
                                IdEstudioPuestoTrabajo = o.IdEstudioPuestoTrabajo
                            }).ToList();
                        }
                    }
                }

                var datos = listEstudioPT;

                return Json(new { Data = datos, status = "Success", Mensaje = "Success" });
            }
            catch (Exception ex)
            {
                registroLog.RegistrarError(typeof(EstudioPuestoTrabajoController), string.Format("Error en la accion RegistrarNuevaAusencia. Hora: {0}, Error: {1}", DateTime.Now, ex.StackTrace), ex);
                return Json(new { Data = "", Mensaje = "Error" });
            }
        }

        [HttpPost]
        public JsonResult BuscarCargo(string prefijo)
        {
            try
            {
                EDEstudioPuestoTrabajo EdEstudioPT = new EDEstudioPuestoTrabajo();
                EdEstudioPT.Cargo = prefijo;

                List<EDEstudioPuestoTrabajo> listCargos = new List<EDEstudioPuestoTrabajo>();

                ServiceClient.EliminarParametros();
                var resultEstudioPT = ServiceClient.RealizarPeticionesArrayPostJsonRest<EDEstudioPuestoTrabajo>(urlServicioPlanificacion, ConsultarCargosPT, EdEstudioPT);
                if (resultEstudioPT != null)
                {
                    if (resultEstudioPT.Count() > 0)
                    {
                        if (resultEstudioPT[0] != null)
                        {
                            listCargos = resultEstudioPT.Select(o => new EDEstudioPuestoTrabajo
                            {
                                Cargo = o.Cargo,
                                IdEstudioPuestoTrabajo = o.IdEstudioPuestoTrabajo
                            }).ToList();
                        }
                    }
                }

                var datos = listCargos;

                return Json(new { Data = datos, status = "Success", Mensaje = "Success" });
            }
            catch (Exception ex)
            {
                registroLog.RegistrarError(typeof(EstudioPuestoTrabajoController), string.Format("Error en la accion RegistrarNuevaAusencia. Hora: {0}, Error: {1}", DateTime.Now, ex.StackTrace), ex);
                return Json(new { Data = "", Mensaje = "Error" });
            }
        }

        [HttpPost]
        public JsonResult ConsultarEstudioXCargo(EstudioPuestoTrabajoModel objEstudio)
        {
            try
            {
                EDEstudioPuestoTrabajo EdEstudioPT = new EDEstudioPuestoTrabajo();
                EdEstudioPT.Cargo = objEstudio.Cargo;

                List<EDEstudioPuestoTrabajo> listEstudioPT = new List<EDEstudioPuestoTrabajo>();

                ServiceClient.EliminarParametros();
                var resultEstudioPT = ServiceClient.RealizarPeticionesArrayPostJsonRest<EDEstudioPuestoTrabajo>(urlServicioPlanificacion, ConsultarEstudioPTXCargo, EdEstudioPT);
                if (resultEstudioPT != null)
                {
                    if (resultEstudioPT.Count() > 0)
                    {
                        if (resultEstudioPT[0] != null)
                        {
                            listEstudioPT = resultEstudioPT.Select(o => new EDEstudioPuestoTrabajo
                            {
                                NumeroIdentificacion = o.NumeroIdentificacion,
                                Apellido1 = o.Apellido1,
                                Apellido2 = o.Apellido2,
                                Nombre1 = o.Nombre1,
                                Nombre2 = o.Nombre2,
                                Cargo = o.Cargo,
                                IdSede = o.IdSede,
                                IdProceso = o.IdProceso,
                                IdDiagnostico = o.IdDiagnostico,
                                IdObjetivoAnalisis = o.IdObjetivoAnalisis,
                                IdTipoAnalisis = o.IdTipoAnalisis,
                                FechaAnalisis = o.FechaAnalisis,
                                IdEstudioPuestoTrabajo = o.IdEstudioPuestoTrabajo
                            }).ToList();
                        }
                    }
                }

                var datos = listEstudioPT;

                return Json(new { Data = datos, status = "Success", Mensaje = "Success" });
            }
            catch (Exception ex)
            {
                registroLog.RegistrarError(typeof(EstudioPuestoTrabajoController), string.Format("Error en la accion RegistrarNuevaAusencia. Hora: {0}, Error: {1}", DateTime.Now, ex.StackTrace), ex);
                return Json(new { Data = "", Mensaje = "Error" });
            }
        }

        [HttpPost]
        public ActionResult MostrarArchivos(SeguimientoEstudioPTModel objNuevoSeguimiento)
        {
            try
            {
                EDEstudioPuestoTrabajo EdEstudioPT = new EDEstudioPuestoTrabajo();
                EdEstudioPT.IdEstudioPuestoTrabajo = objNuevoSeguimiento.IdEstudioPuestoTrabajo;

                List<EDArchivoEstudioPuestoTrabajo> listArchivos = new List<EDArchivoEstudioPuestoTrabajo>();

                ServiceClient.EliminarParametros();
                var resultArchivosEstudioPT = ServiceClient.RealizarPeticionesArrayPostJsonRest<EDArchivoEstudioPuestoTrabajo>(urlServicioPlanificacion, MostrarArchivosPT, EdEstudioPT);
                if (resultArchivosEstudioPT != null)
                {
                    if (resultArchivosEstudioPT.Count() > 0)
                    {
                        if (resultArchivosEstudioPT[0] != null)
                        {
                            listArchivos = resultArchivosEstudioPT.Select(o => new EDArchivoEstudioPuestoTrabajo
                            {
                                NombreArchivo = o.NombreArchivo,
                                RutaArchivo = o.RutaArchivo,
                                IdEstudioPuestoTrabajo = o.IdEstudioPuestoTrabajo

                            }).ToList();
                        }
                    }
                }

                EdEstudioPT.listaArchivos = listArchivos;
                //var datos = RenderRazorViewToString("GridArchivosPT", EdEstudioPT);

                //return Json(new { Data = datos, Mensaje = "Success" });
                return PartialView("GridArchivosPT", EdEstudioPT);
            }
            catch (Exception ex)
            {
                registroLog.RegistrarError(typeof(EstudioPuestoTrabajoController), string.Format("Error en la accion RegistrarNuevaAusencia. Hora: {0}, Error: {1}", DateTime.Now, ex.StackTrace), ex);
                return Json(new { Data = "", Mensaje = "Error" });
            }
        }

        public FileStreamResult MostrarOtInteraccionesPDF(string RutaArchivo)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
            }

            FileStream fs = new FileStream(RutaArchivo, FileMode.Open, FileAccess.Read);
            return File(fs, "application/pdf");
        }

        
    }
}