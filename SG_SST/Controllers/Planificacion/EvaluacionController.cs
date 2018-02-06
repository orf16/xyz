using RestSharp;
using Rotativa;
using SG_SST.Models.Planificacion;
using SG_SST.ServiceRequest;
using SG_SST.Controllers.Base;
using SG_SST.EntidadesDominio.Planificacion;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SG_SST.Models.Login;
using SG_SST.EntidadesDominio.Empresas;

namespace SG_SST.Controllers.Planificacion
{
    public class EvaluacionController : BaseController
    {
        string capacidadEvaluacionInicial = ConfigurationManager.AppSettings["CapacidadEvaluacionInicial"];
        string CapacidadAspectosBase = ConfigurationManager.AppSettings["CapacidadAspectosBase"];
        string CapacidadEmpresaEvaluar = ConfigurationManager.AppSettings["CapacidadEmpresaEvaluar"];
        string responsableSGSST = ConfigurationManager.AppSettings["rolResponsableSGSST"];
        // GET: Evaluacion
        public ActionResult Index()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "Debe estar autenticado para realizar la Evaluación Inicial";
                return View();
            }
                
            var evaluacion = new EvaluacionModel();

            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("NIT", usuarioActual.NitEmpresa);
            ServiceClient.AdicionarParametro("Responsable", responsableSGSST);
            var resultEmpEval = ServiceClient.ObtenerObjetoJsonRestFul<EDEmpresaEvaluar>(urlServicioPlanificacion, CapacidadEmpresaEvaluar, RestSharp.Method.GET);
            if (resultEmpEval != null)
            {
                evaluacion.CodActividadeEconomica = resultEmpEval.CodActividadEconomica;
                evaluacion.ActividadEconomica = resultEmpEval.ActividadEconomica;
                evaluacion.ResponsableSGSST = resultEmpEval.ResponsableSGSST;
                evaluacion.Nit = resultEmpEval.Nit;
                evaluacion.RazonSocial = resultEmpEval.RazonSocial;
            }
            else
            {
                evaluacion.RazonSocial = usuarioActual.RazonSocialEmpresa;
                evaluacion.Nit = usuarioActual.NitEmpresa;
            }         
            

            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("NIT", usuarioActual.NitEmpresa);
            var resultSede = ServiceClient.ObtenerArrayJsonRestFul<EDSede>(urlServicioEmpresas, CapacidadObtenerSedesPorNit, RestSharp.Method.GET);
            if (resultSede != null && resultSede.Count() > 0 && resultSede.FirstOrDefault() != null)
            {
                evaluacion.CentrosDeTrabajo = resultSede.Select(c => new SelectListItem()
                {
                    Value = c.IdSede.ToString(),
                    Text = c.NombreSede
                }).ToList();
            }
            else
                evaluacion.CentrosDeTrabajo = new List<SelectListItem>();

            ServiceClient.EliminarParametros();           
            var resultAspectosBase = ServiceClient.ObtenerArrayJsonRestFul<EDAspecto>(urlServicioPlanificacion, CapacidadAspectosBase, RestSharp.Method.GET);
            if (resultAspectosBase != null && resultAspectosBase.Count() > 0 && resultAspectosBase.FirstOrDefault() != null)
            {
                evaluacion.AspectosBase = resultAspectosBase.Select(ab => new AspectosModel()
                {
                    IdAspecto = ab.IdAspecto,
                    AspectoEvaluar = ab.Aspecto
                }).ToList();
            }
            //if (resultAspectosBase != null && resultAspectosBase.Count() > 0)
            //{
            //    evaluacion.AspectosBase = resultAspectosBase.Select(ab => new SelectListItem()
            //    {
            //        Value = ab.IdAspecto.ToString (),
            //        Text = ab.Aspecto 
            //    }).ToList();
            //}
            else
                evaluacion.AspectosBase = new List<AspectosModel>();
                        
            return View(evaluacion);
        }

        /// <summary>
        /// Guarda la evaluación inicial
        /// </summary>
        /// <param name="objEvaluacion"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Index(EvaluacionModel objEvaluacion)
        {         
            
            if (ModelState.IsValid)
            {
                var empresaEvaluar = new EDEmpresaEvaluar()
                {
                    IdEmpresaEvaluar = objEvaluacion.IdEmpresa,
                    RazonSocial = objEvaluacion.RazonSocial,
                    Nit = objEvaluacion.Nit.ToString(),
                    CodActividadEconomica = Convert.ToInt32(objEvaluacion.CodActividadeEconomica),
                    ActividadEconomica = objEvaluacion.ActividadEconomica,
                    ResponsableSGSST = objEvaluacion.ResponsableSGSST,
                    ElaboradoPor = objEvaluacion.ElaboradoPor,
                    NumLicenciaSOSL = objEvaluacion.LicenciaSOSL,
                    CodSede = Convert.ToInt32(objEvaluacion.SedeCentroTrabajo),
                    FechaDiligencia = objEvaluacion.FechaDiligenciamiento,
                    Aspectos = objEvaluacion.AspectosCreados.Select(asp => new EDAspecto()
                    {
                        IdAspecto = asp.IdAspecto,
                        Aspecto = asp.AspectoEvaluar,
                        IdValorizacion = ObtenerValoraciones(asp),
                        Observacion = asp.Observaciones
                    }).ToList()
                };
                //se consume el servicio post para guardar la información de la evaluación inicial
                ServiceClient.EliminarParametros();
                var result = ServiceClient.RealizarPeticionesPostJsonRestFul<EDEmpresaEvaluar>(urlServicioPlanificacion, capacidadEvaluacionInicial, empresaEvaluar);
                if (result != null)
                {
                    return Json(new { Data = result.CalificacionFinal, Mensaje = "OK" });
                }
                else
                    return Json(new { Data = string.Empty, Mensaje = "ERROR" });
            }
            else
                return Json(new { Data = string.Empty, Mensaje = "ERROR" });
        }

        [HttpPost]
        public JsonResult ObtenerViewToPdf(EvaluacionModel objEvaluacion)
        {
            TempData["EvaluacionModel"] = objEvaluacion;
            return Json(new { Data = objEvaluacion, Mensaje = "OK" });
        }

        /// <summary>
        /// Descarga en PDF el formulario de evaluacion Inicial
        /// </summary>
        /// <returns></returns>
        //public ActionResult GeneradorPdf()
        //{
        //    var objEvaluacion = new EvaluacionModel();
        //    if (TempData["EvaluacionModel"] != null)
        //    {
        //        var objtemp = (EvaluacionModel)TempData["EvaluacionModel"];

        //        objEvaluacion.RazonSocial = objtemp.RazonSocial;
        //        objEvaluacion.ResponsableSGSST = objtemp.ResponsableSGSST;
        //        objEvaluacion.Nit = objtemp.Nit;
        //        objEvaluacion.ElaboradoPor = objtemp.ElaboradoPor;
        //        objEvaluacion.ActividadEconomica = "Agricultura";
        //        objEvaluacion.LicenciaSOSL = objtemp.LicenciaSOSL;
        //        objEvaluacion.SedeCentroTrabajo = objtemp.SedeCentroTrabajo;
        //        objEvaluacion.FechaDiligenciamiento = objtemp.FechaDiligenciamiento;
        //        objEvaluacion.AspectosCreados = objtemp.AspectosCreados;

        //    }
        //    return new  ViewAsPdf("GeneradorPdf", objEvaluacion)
        //    {
        //        FileName = "FormularioEvaluacionIninial.pdf"
        //    }; 
            //return View(objEvaluacion);
        //}

        /// <summary>
        /// Agraga un nuevo aspecto para la empresa en sesión
        /// </summary>
        /// <param name="aspecto"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult AgregarNuevoAspecto(string aspecto, int idAspecto)
        {
            if (!string.IsNullOrEmpty(aspecto))
            {
                var nuevoAspecto = new AspectosModel();
                nuevoAspecto.IdAspecto = idAspecto;
                nuevoAspecto.AspectoEvaluar = aspecto;
                var datos = RenderRazorViewToString("_NuevoAspecto", nuevoAspecto);
                return Json(new { Data = datos, Mensaje = "OK" });
            }
            else
                return Json(new { Data = string.Empty, Mensaje = "ERROR" });
        }

        /// <summary>
        /// Renderiza una vista en formato string
        /// </summary>
        /// <param name="viewName"></param>
        /// <param name="model"></param>
        /// <returns></returns>


        private int ObtenerValoraciones(AspectosModel aspectos)
        {
            if (aspectos != null)
            {
                var result = 0;
                if (aspectos.Cumple)
                    result = (int)Enumeraciones.EnumPlanificacion.ValoracionEvalInicial.Cumple;
                else if (aspectos.NoCumple)
                    result = (int)Enumeraciones.EnumPlanificacion.ValoracionEvalInicial.NoCumple;
                else if (aspectos.CumpleParcial)
                    result = (int)Enumeraciones.EnumPlanificacion.ValoracionEvalInicial.CumpleParcial;
                return result;
            }
            else
                return (int)Enumeraciones.EnumPlanificacion.ValoracionEvalInicial.CumpleParcial;
        }

    }
}