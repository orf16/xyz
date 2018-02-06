using SG_SST.Models.Planificacion;
using SG_SST.ServiceRequest;
using SG_SST.EntidadesDominio.Planificacion;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using Newtonsoft.Json;
using SG_SST.Controllers.Base;
using Rotativa;
using System.Net;
using SG_SST.Logica.Usuarios;

namespace SG_SST.Controllers.Planificacion
{
    public class EvaluacionEstandarMinimoController : BaseController
    {
        string urlServicioPlanificacion = ConfigurationManager.AppSettings["UrlServicioPlanificacion"];
        string capacidadEvaluacionEstandaresMinimos = ConfigurationManager.AppSettings["CapacidadEvaluacionEstandaresMinimos"];
        string capacidadCalificacionEstandaresMinimos = ConfigurationManager.AppSettings["CapacidadCalificacionEstandaresMinimos"];
        string capacidadReporteEstandaresMinimos = ConfigurationManager.AppSettings["CapacidadReporteEstandaresMinimos"];
        string capacidadReporteRespuestasEstandaresMinimos = ConfigurationManager.AppSettings["CapacidadReporteRespuestasEstandaresMinimos"];
        string capacidadReportePuntajeEstandaresMinimos = ConfigurationManager.AppSettings["CapacidadReportePuntajeEstandaresMinimos"];
        string capacidadReporteCalificacionEstandaresMinimosFinal = ConfigurationManager.AppSettings["CapacidadReporteCalificacionEstandaresMinimosFinal"];
        string capacidadPlanAccion = ConfigurationManager.AppSettings["CapacidadPlanAccion"];
        string capacidadPlanAccionExcel = ConfigurationManager.AppSettings["CapacidadPlanAccionExcel"];
        string CapacidadReporteExcelEstandaresMinimosCiclo = ConfigurationManager.AppSettings["CapacidadReporteExcelEstandaresMinimosCiclo"];
        string CapacidadReporteExcelRespuestasEstandaresMinimos = ConfigurationManager.AppSettings["CapacidadReporteExcelRespuestasEstandaresMinimos"];
        string CapacidadReporteExcelPuntajeEstandaresMinimos = ConfigurationManager.AppSettings["CapacidadReporteExcelPuntajeEstandaresMinimos"];
        string CapacidadObtenerAnoInicioEmpresa = ConfigurationManager.AppSettings["CapacidadObtenerAnoInicioEmpresa"];
        // GET: EvaluacionEstandarMinimo
        public ActionResult Index()
        {
            //se consume el servicio rest para obtener los ciclos
            var objEvaluacion = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (objEvaluacion == null) {
                ViewBag.Mensaje = "Debe estar autenticado para realizar la evalación.";
                return RedirectToAction("Login", "Home");
            }
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("NIT", objEvaluacion.NitEmpresa );
            //ServiceClient.AdicionarParametro("NIT", "234567654");
            var result = ServiceClient.ObtenerArrayJsonRestFul<EDCiclo>(urlServicioPlanificacion, capacidadEvaluacionEstandaresMinimos, RestSharp.Method.GET);
            if (result != null && result.Count() > 0 && result.FirstOrDefault() != null)
            {
                var evaluacionEstMin = new EvaluacionEstandarMinimoModel();
                evaluacionEstMin.Ciclos = result.Select(c => new CicloModel()
                {
                    IdCiclo = c.Id_Ciclo,
                    Nombre = obtenernombreciclo(c.Id_Ciclo, c.Nombre),
                    Porcentaje = c.Porcentaje_Max,
                    CantidadCriterios = c.CantidadCriterios,
                    StandPorEvaluar = c.StandPorEvaluar
                }).ToList();

                var obtenerCalificacionFinal = evaluacionEstMin.Ciclos.Where(c => c.StandPorEvaluar > 0).Count();
                if (obtenerCalificacionFinal == 0)
                {

                    //Se consume el servicio rest para obtener la calificacion final
                    ServiceClient.EliminarParametros();
                    ServiceClient.AdicionarParametro("NIT", objEvaluacion.NitEmpresa);
                    //ServiceClient.AdicionarParametro("NIT", "234567654");
                    var calificacionFinal = ServiceClient.ObtenerObjetoJsonRestFul<EDValoracionFinal>(urlServicioPlanificacion, capacidadCalificacionEstandaresMinimos, RestSharp.Method.GET);
                    evaluacionEstMin.CalificacionFinal = calificacionFinal == null ? null : new ValoracionFinalModel()
                    {
                        IdValoracionFinal = calificacionFinal.IdValoracionFinal,
                        Accion = calificacionFinal.Accion,
                        CriterioValoracion = calificacionFinal.CriterioValoracion,
                        Valoracion = calificacionFinal.Valoracion
                    };
                }
                return View(evaluacionEstMin);
            }
            else
                return RedirectToAction("Index", "Home");
        }

        private string obtenernombreciclo(int id_Ciclo, string nombre)
        {
            
            switch (id_Ciclo)
            {
                case 1:
                    return string.Format("I. {0}", nombre);
                case 2:
                    return string.Format("II. {0}", nombre);
                case 3:
                    return string.Format("III. {0}", nombre);
                case 4:
                    return string.Format("IV. {0}", nombre);
                default:
                    return nombre;
            }
        }

        /// <summary>
        /// Consulta y renderiza la información de los estándares,
        /// sub-estándares y criterios asociados al ciclo actual.
        /// </summary>
        /// <param name="idCiclo"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ObtenerCriteriosPorCiclo(int idCiclo)
        {
            var objEvaluacion = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            //se consume el servicio rest para obtener los ciclos
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("idCiclo", idCiclo);
            ServiceClient.AdicionarParametro("NIT", objEvaluacion.NitEmpresa);
            //ServiceClient.AdicionarParametro("NIT", "234567654");
            var result = ServiceClient.ObtenerObjetoJsonRestFul<EDCiclo>(urlServicioPlanificacion, capacidadEvaluacionEstandaresMinimos, RestSharp.Method.GET);
            if (result != null)
            {
                var cicloObtenido = result;
                var cicloActual = new CicloModel()
                {
                    IdCiclo = cicloObtenido.Id_Ciclo,
                    Nombre = obtenernombreciclo(cicloObtenido.Id_Ciclo, cicloObtenido.Nombre),
                    Porcentaje = cicloObtenido.Porcentaje_Max,
                    CantidadCriterios = cicloObtenido.CantidadCriterios,
                    StandPorEvaluar = cicloObtenido.StandPorEvaluar,
                    EstandarActual = new EstandarModel()
                    {
                        Id_Estandar = cicloObtenido.Estandar.Id_Estandar,
                        Descripcion = cicloObtenido.Estandar.Descripcion,
                        Porcentaje_Max = cicloObtenido.Estandar.Porcentaje_Max,
                        SubEstandarActual = new SubEstandarModel()
                        {
                            Id_SubEstandar = cicloObtenido.Estandar.SubEstandar.Id_SubEstandar,
                            Descripcion_Corta = cicloObtenido.Estandar.SubEstandar.Descripcion_Corta,
                            Descripcion = cicloObtenido.Estandar.SubEstandar.Descripcion,
                            Procentaje_Max = cicloObtenido.Estandar.SubEstandar.Procentaje_Max,
                            CriterioActual = new CriterioModel()
                            {
                                Id_Criterio = cicloObtenido.Estandar.SubEstandar.Criterio.Id_Criterio,
                                Descripcion = cicloObtenido.Estandar.SubEstandar.Criterio.Descripcion,
                                Numeral = cicloObtenido.Estandar.SubEstandar.Criterio.Numeral,
                                Marco_Legal = cicloObtenido.Estandar.SubEstandar.Criterio.Marco_Legal,
                                Modo_Verificacion = cicloObtenido.Estandar.SubEstandar.Criterio.Modo_Verificacion
                            }
                        }
                    }
                };
                var datos = RenderRazorViewToString("_ObtenerCriteriosPorCiclo", cicloActual);
                return Json(new { Datos = datos, Mensaje = "OK" });
            }
            else
                return Json(new { Datos = string.Empty, Mensaje = "ERROR" });
        }

        [HttpPost]
        public JsonResult AgregarNuevaActividad(ActividadModel nuevaActividad)
        {
            if (ModelState.IsValid)
            {
                var datos = RenderRazorViewToString("_NuevaActividad", nuevaActividad);
                return Json(new { Data = datos, Mensaje = "OK" });
            }
            else
                return Json(new { Data = string.Empty, Mensaje = "ERROR" });
        }

        #region Plan de accion
        /// <summary>
        /// Obtiene las Actividades generadas durante la evaluacion y se presentan como 
        /// plan de accion
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult PlanDeAccion()
        {
            var objEvaluacion = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (objEvaluacion == null)
                return Json(new { Data = "Debe estar autenticado para realizar la evalación.", Mensaje = "ERROR" });
            ServiceClient.EliminarParametros();
            //ServiceClient.AdicionarParametro("NIT", "860123006");
            ServiceClient.AdicionarParametro("NIT", objEvaluacion.NitEmpresa);
            var result = ServiceClient.ObtenerArrayJsonRestFul<EDActividad>(urlServicioPlanificacion, capacidadPlanAccion, RestSharp.Method.GET);

            if (result != null && result.Count() > 0)
            {
                var Actividades = result.Select(c => new ActividadModel
                {
                    Descripcion = c.Descripcion,
                    Responsable = c.Responsable,
                    strFechaFin = string.Format("{0}/{1}/{2}", c.FechaFin.Year, c.FechaFin.Month, c.FechaFin.Day)
                }).ToList();

                var datos = RenderRazorViewToString("_PlanAccionEstandaresMinimos", Actividades);
                return Json(new { Data = datos, Mensaje = "OK" });
            }
            else
                return Json(new { Data = string.Empty, Mensaje = "ERROR" });
        }

        /// <summary>
        /// Descarga el plan de accion a archivo excel
        /// </summary>
        /// <returns></returns>
        public FileResult DescargarPlanAccionExccel()
        {
            var objEvaluacion = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            ServiceClient.EliminarParametros();
            //ServiceClient.AdicionarParametro("NIT", "860123006");
            ServiceClient.AdicionarParametro("NIT", objEvaluacion.NitEmpresa);
            var result = ServiceClient.ObtenerObjetoJsonRestFul<byte[]>(urlServicioPlanificacion, capacidadPlanAccionExcel, RestSharp.Method.GET);
            return File(result, "application/vnd.ms-excel", "PlanDeAccion.xlsx");
        }


        public ActionResult DescargarPlanAccionPDF()
        {
            var objEvaluacion = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            
            ServiceClient.EliminarParametros();
            //ServiceClient.AdicionarParametro("NIT", "860123006");
            ServiceClient.AdicionarParametro("NIT", objEvaluacion.NitEmpresa);
            var result = ServiceClient.ObtenerArrayJsonRestFul<EDActividad>(urlServicioPlanificacion, capacidadPlanAccion, RestSharp.Method.GET);

            if (result != null && result.Count() > 0)
            {
                var Actividades = result.Select(c => new ActividadModel
                {
                    Descripcion = c.Descripcion,
                    Responsable = c.Responsable,
                    strFechaFin = string.Format("{0}/{1}/{2}", c.FechaFin.Year, c.FechaFin.Month, c.FechaFin.Day)
                }).ToList();

                return new ViewAsPdf("DescargarPlanAccionPDF", Actividades)
                {
                    FileName = "PlanDeAccion.pdf"
                };

            }
            else
            {
                List<ActividadModel> Actividades = new List<ActividadModel>();
                return new ViewAsPdf("DescargarPlanAccionPDF", Actividades)
                {
                    FileName = "PlanDeAccion.pdf"
                };
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actividades"></param>
        /// <returns></returns>
        public JsonResult Renderizarctividades(ActividadModel[] actividades)
        {
            var datos = new StringBuilder();
            if (actividades != null && actividades.Count() > 0)
            {
                foreach (var actividad in actividades)
                    datos.Append(RenderRazorViewToString("_NuevaActividad", actividad));
                return Json(new { Data = datos.ToString(), Mensaje = "OK" });
            }
            else
                return Json(new { Data = string.Empty, Mensaje = "ERROR" });
        }
        [HttpPost]
        public JsonResult CalificarCriterioPorCiclo(CalificacionCriterioModel objCalificacion)
        {
            if (ModelState.IsValid)
            {
                var objEvaluacion = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
                if (objEvaluacion == null)
                    return Json(new { Data = "Debe estar autenticado para realizar la evalación.", Mensaje = "ERROR" });
                var criterioCalificado = new EDEvaluacionEstandarMinimo()
                {
                    Nit = objEvaluacion.NitEmpresa,
                    IdCiclo = objCalificacion.IdCiclo,
                    IdCriterio = objCalificacion.IdCriterio,
                    IdEmpresaEvaluar = objCalificacion.IdEmpresaEvaluar,
                    //IdEvalEstandarMinimo = objCalificacion.IdEvalEstandarMinimo,
                    IdValoracionCriterio = objCalificacion.IdValoracionCriterio,
                    Justificacion = objCalificacion.Justificacion,
                    Actividades = objCalificacion.Actividades == null ? null : objCalificacion.Actividades.Select(a => new EDActividad()
                    {
                        Id_Actividad = a.Id_Actividad,
                        Descripcion = a.Descripcion,
                        Responsable = a.Responsable,
                        FechaFin = a.FechaFin
                    }).ToList()
                };
                //se consume el servicio post para guardar la información de los estandares minimos
                ServiceClient.EliminarParametros();
                var result = ServiceClient.SolicitarGuaardadoCriterioPorCiclo<EDCiclo>(urlServicioPlanificacion, capacidadEvaluacionEstandaresMinimos, criterioCalificado);
                if (result != null)
                {
                    var cicloObtenido = result;
                    var cicloActual = new CicloModel()
                    {
                        IdCiclo = cicloObtenido.Id_Ciclo,
                        Nombre = cicloObtenido.Nombre,
                        Porcentaje = cicloObtenido.Porcentaje_Max,
                        CantidadCriterios = cicloObtenido.CantidadCriterios,
                        StandPorEvaluar = cicloObtenido.StandPorEvaluar,
                        EstandarActual = cicloObtenido.Estandar == null ? null : new EstandarModel()
                        {
                            Id_Estandar = cicloObtenido.Estandar.Id_Estandar,
                            Descripcion = cicloObtenido.Estandar.Descripcion,
                            Porcentaje_Max = cicloObtenido.Estandar.Porcentaje_Max,
                            SubEstandarActual = cicloObtenido.Estandar.SubEstandar == null ? null : new SubEstandarModel()
                            {
                                Id_SubEstandar = cicloObtenido.Estandar.SubEstandar.Id_SubEstandar,
                                Descripcion_Corta = cicloObtenido.Estandar.SubEstandar.Descripcion_Corta,
                                Descripcion = cicloObtenido.Estandar.SubEstandar.Descripcion,
                                Procentaje_Max = cicloObtenido.Estandar.SubEstandar.Procentaje_Max,
                                CriterioActual = cicloObtenido.Estandar.SubEstandar.Criterio == null ? null : new CriterioModel()
                                {
                                    Id_Criterio = cicloObtenido.Estandar.SubEstandar.Criterio.Id_Criterio,
                                    Descripcion_Corta = cicloObtenido.Estandar.SubEstandar.Criterio.Descripcion_Corta,
                                    Descripcion = cicloObtenido.Estandar.SubEstandar.Criterio.Descripcion,
                                    Numeral = cicloObtenido.Estandar.SubEstandar.Criterio.Numeral,
                                    Marco_Legal = cicloObtenido.Estandar.SubEstandar.Criterio.Marco_Legal,
                                    Modo_Verificacion = cicloObtenido.Estandar.SubEstandar.Criterio.Modo_Verificacion
                                }
                            }
                        }
                    };
                    var datos = string.Empty;
                    var cicloCalificado = false;
                    var terminaCalfEstMin = false;
                    if (cicloActual.StandPorEvaluar == 0)
                    {
                        datos = cicloActual.IdCiclo.ToString();
                        cicloCalificado = true;
                    }
                    if (cicloActual.EstandarActual == null)
                        terminaCalfEstMin = true;
                    else
                        datos = RenderRazorViewToString("_ObtenerCriteriosPorCiclo", cicloActual);
                    return Json(new { Datos = datos, Mensaje = "OK", CicloCalificado = cicloCalificado, TerminaCalfEstMin = terminaCalfEstMin });
                }
                else
                    return Json(new { Datos = string.Empty, Mensaje = "ERROR" });
            }
            else
                return Json(new { Datos = string.Empty, Mensaje = "ERROR" });
        }


        [HttpPost]
        public JsonResult ObtenerInformeExccel()
        {
            return Json(new { Datos = "", Mensaje = "OK" });
        }

        /// <summary>
        /// Descarga el excel de todos los estandares con su respectiva calificacion
        /// </summary>
        /// <returns></returns>
        public FileResult DescargarInformeExccel()
        {
            var objEvaluacion = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            ServiceClient.EliminarParametros();
            //ServiceClient.AdicionarParametro("NIT", "860123006");
            ServiceClient.AdicionarParametro("NIT", objEvaluacion.NitEmpresa);
            //se consume el servicio post para guardar la información de la evaluación inicial
            var result = ServiceClient.ObtenerObjetoJsonRestFul<byte[]>(urlServicioPlanificacion, capacidadReporteEstandaresMinimos, RestSharp.Method.GET);
            return File(result, "application/vnd.ms-excel", "EstandaresMinimos.xlsx");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ObtenerInformeParcial()
        {
            var objEvaluacion = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (objEvaluacion == null)
                return Json(new { Data = "Debe estar autenticado para realizar la evalación.", Mensaje = "ERROR" });
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("NIT", objEvaluacion.NitEmpresa);
            //ServiceClient.AdicionarParametro("NIT", "234567654");
            //se consume el servicio post para guardar la información de la evaluación inicial
            var result = ServiceClient.ObtenerArrayJsonRestFul<EDCiclo>(urlServicioPlanificacion, capacidadReporteRespuestasEstandaresMinimos, RestSharp.Method.GET);
            if (result != null && result.Count() > 0)
            {
                var ciclos = result.Select(c => new CicloModel()
                {
                    IdCiclo = c.Id_Ciclo,
                    Nombre = c.Nombre,
                    PorcenObtenido = c.PorcenObtenido,
                    PorcenRespondido = c.PorcenRespondido,
                    Porcentaje = c.Porcentaje_Max
                }).ToList();
                var datos = RenderRazorViewToString("_InformeParcial", ciclos);
                return Json(new { Datos = datos, Mensaje = "OK" });
            }
            else
                return Json(new { Datos = string.Empty, Mensaje = "ERROR" });
        }

        [HttpPost]
        public JsonResult ObtenerInformeFinal()
        {
            var objEvaluacion = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (objEvaluacion == null)
                return Json(new { Data = "Debe estar autenticado para realizar la evalación.", Mensaje = "ERROR" });
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("NIT", objEvaluacion.NitEmpresa);
            //ServiceClient.AdicionarParametro("NIT", "234567654");
            //se consume el servicio post para guardar la información de la evaluación inicial
            var result = ServiceClient.ObtenerArrayJsonRestFul<EDCiclo>(urlServicioPlanificacion, capacidadReporteRespuestasEstandaresMinimos, RestSharp.Method.GET);
            if (result != null && result.Count() > 0)
            {
                var ciclos = result.Select(c => new CicloModel()
                {
                    IdCiclo = c.Id_Ciclo,
                    Nombre = c.Nombre,
                    PorcenObtenido = c.PorcenObtenido,
                    PorcenRespondido = c.PorcenRespondido,
                    Porcentaje = c.Porcentaje_Max
                }).ToList();
                var datos = RenderRazorViewToString("_InformeFinal", ciclos);
                return Json(new { Datos = datos, Mensaje = "OK" });
            }
            else
                return Json(new { Datos = string.Empty, Mensaje = "ERROR" });
        }


        public JsonResult ObtenerCalificacionEstandares(int idCiclo)
        {
            var objEvaluacion = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (objEvaluacion == null)
                return Json(new { Data = "Debe estar autenticado para realizar la evalación.", Mensaje = "NOTFOUND" });
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("NIT", objEvaluacion.NitEmpresa);
            //ServiceClient.AdicionarParametro("NIT", "234567654");
            ServiceClient.AdicionarParametro("IdCiclo", idCiclo); 
            //se consume el servicio post para guardar la información de la evaluación inicial
            var result = ServiceClient.ObtenerObjetoJsonRestFul<EDCiclo>(urlServicioPlanificacion, capacidadReporteCalificacionEstandaresMinimosFinal, RestSharp.Method.GET);
            if (result != null)
            {
                var ciclo = new CicloModel()
                {
                    IdCiclo = result.Id_Ciclo,
                    Nombre = result.Nombre,
                    PorcenObtenido = result.PorcenObtenido,
                    PorcenRespondido = result.PorcenRespondido,
                    Porcentaje = result.Porcentaje_Max,
                    Estandares = result.Estandares == null ? null :
                    result.Estandares.Select(e => new EstandarModel()
                    {
                        Id_Estandar = e.Id_Estandar,
                        Descripcion = e.Descripcion,
                        CalificacionFinal = e.Calificacion
                    }).ToList()
                };
                return Json(new { Datos = ciclo, Mensaje = "OK" });
            }
            else
                return Json(new { Datos = string.Empty, Mensaje = "NOTFOUND" });
        }
        #region Informes excel calificacion final

        public JsonResult CalificacionFinal(int idCiclo)
        {
            TempData["idciclo"] = idCiclo;
            return Json(new { Datos = "", Mensaje = "OK" });
        }

        public FileResult ObtenerCalificacionFinalExcel()
        {
            int idCiclo = 1;
            if (TempData["idciclo"] != null)
            {
                idCiclo = int.Parse(TempData["idciclo"].ToString());
            }
            var objEvaluacion = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            ServiceClient.EliminarParametros();
            //ServiceClient.AdicionarParametro("NIT", "860123006");
            ServiceClient.AdicionarParametro("NIT", objEvaluacion.NitEmpresa);
            ServiceClient.AdicionarParametro("IdCiclo", idCiclo);
            //se consume el servicio post para guardar la información de la evaluación inicial
            var result = ServiceClient.ObtenerObjetoJsonRestFul<byte[]>(urlServicioPlanificacion, CapacidadReporteExcelEstandaresMinimosCiclo, RestSharp.Method.GET);

            return File(result, "application/vnd.ms-excel", "CalificacionCiclo.xlsx");

        }

        

        #endregion

        #region Informes calificacion parcial
        /// <summary>
        /// obtiene el porcentaje de avance del puntaje obtenido
        /// respecto al puntaje total
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ObtenerPorcentajePuntajeDePuntajeTotal()
        {
            var objEvaluacion = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (objEvaluacion == null)
                return Json(new { Data = "Debe estar autenticado para realizar la evalación.", Mensaje = "ERROR" });
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("NIT", objEvaluacion.NitEmpresa);
            //ServiceClient.AdicionarParametro("NIT", "234567654");
            //se consume el servicio post para guardar la información de la evaluación inicial
            var result = ServiceClient.ObtenerArrayJsonRestFul<EDCiclo>(urlServicioPlanificacion, capacidadReportePuntajeEstandaresMinimos, RestSharp.Method.GET);
            if (result != null && result.Count() > 0)
            {
                var ciclos = result.Select(c => new CicloModel()
                {
                    IdCiclo = c.Id_Ciclo,
                    Nombre = c.Nombre,
                    PorcenObtenido = Math.Round(c.PorcenObtenido, 2),
                    PorcenRespondido = Math.Round(c.PorcenRespondido, 2),
                    Porcentaje = c.Porcentaje_Max
                }).ToList();
                return Json(new { Datos = ciclos, Mensaje = "OK" });
            }
            else
                return Json(new { Datos = string.Empty, Mensaje = "ERROR" });
        }


        /// <summary>
        /// Obtiene el excel del informe parcial de porcentaje obtenido al momento por ciclo
        /// </summary>
        /// <returns></returns>
        public FileResult ObtenerExcelPorcentajeObtenido()
        {
            var objEvaluacion = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            ServiceClient.EliminarParametros();
            //ServiceClient.AdicionarParametro("NIT", "860123006");
            ServiceClient.AdicionarParametro("NIT", objEvaluacion.NitEmpresa);
            //se consume el servicio post para guardar la información de la evaluación inicial
            var result = ServiceClient.ObtenerObjetoJsonRestFul<byte[]>(urlServicioPlanificacion, CapacidadReporteExcelPuntajeEstandaresMinimos, RestSharp.Method.GET);

            return File(result, "application/vnd.ms-excel", "PorcentajeObtenido.xlsx");

        }

        /// <summary>
        /// obtiene el porcentaje de avance sobre las respuestas dadas
        /// respecto al total de preguntas
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ObtenerPorcentajeRespuestasDeTotalPreguntas()
        {
            var objEvaluacion = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (objEvaluacion == null)
                return Json(new { Data = "Debe estar autenticado para realizar la evalación.", Mensaje = "ERROR" });
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("NIT", objEvaluacion.NitEmpresa);
            //ServiceClient.AdicionarParametro("NIT", "234567654");
            //se consume el servicio post para guardar la información de la evaluación inicial
            var result = ServiceClient.ObtenerArrayJsonRestFul<EDCiclo>(urlServicioPlanificacion, capacidadReporteRespuestasEstandaresMinimos, RestSharp.Method.GET);
            if (result != null && result.Count() > 0)
            {
                var ciclos = result.Select(c => new CicloModel()
                {
                    IdCiclo = c.Id_Ciclo,
                    Nombre = c.Nombre,
                    PorcenObtenido = Math.Round(c.PorcenObtenido, 2),
                    PorcenRespondido = Math.Round(c.PorcenRespondido, 2),
                    Porcentaje = c.Porcentaje_Max
                }).ToList();
                return Json(new { Datos = ciclos, Mensaje = "OK" });
            }
            else
                return Json(new { Datos = string.Empty, Mensaje = "ERROR" });
        }

        /// <summary>
        /// Obtiene el excel del informe parcial de porcentaje obtenido al momento por ciclo
        /// </summary>
        /// <returns></returns>
        public FileResult ObtenerExcelPorcentajeDeRespuestas()
        {
            var objEvaluacion = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            ServiceClient.EliminarParametros();
            //ServiceClient.AdicionarParametro("NIT", "860123006");
            ServiceClient.AdicionarParametro("NIT", objEvaluacion.NitEmpresa);
            //se consume el servicio post para guardar la información de la evaluación inicial
            var result = ServiceClient.ObtenerObjetoJsonRestFul<byte[]>(urlServicioPlanificacion, CapacidadReporteExcelRespuestasEstandaresMinimos, RestSharp.Method.GET);
            return File(result, "application/vnd.ms-excel", "PorcentajeRespuestas.xlsx");
        }

        public ActionResult EvaluacionPositiva()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "Debe estar autenticado para visualizar los Indicadores.";
                return View();
            }
            var lnUsuario = new LNUsuario();
            EvaluacionPositivaModel modelEvalPositiva = new EvaluacionPositivaModel();
            //ServiceClient.EliminarParametros();
            //ServiceClient.AdicionarParametro("NIT", usuarioActual.NitEmpresa);
            //var resultAno = ServiceClient.ObtenerObjetoJsonRestFul<int>(urlServicioPlanificacion, CapacidadObtenerAnoInicioEmpresa, RestSharp.Method.GET);
            //if (resultAno > 0)
            //{
            //    modelEvalPositiva.Anios = GetAnios(resultAno);
            //}
            //else
            modelEvalPositiva.Anios = GetAnios(2011);
            modelEvalPositiva.RazonSocial = usuarioActual.RazonSocialEmpresa;
            return View(modelEvalPositiva);
        }

        [HttpPost]
        public ActionResult EvaluacionPositiva(EvaluacionPositivaModel EvalPositiva)
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
                if(ruta == null)
                    modelEvalPositiva.url = "../Content/ErrorPage.html";
                else if (ruta.valido < 0)
                    modelEvalPositiva.url = "../Content/ErrorPage.html";
                else
                    modelEvalPositiva.url = ruta.url_sitio;

                return View(modelEvalPositiva);
            }
        }

        #endregion
        #endregion
    }
}