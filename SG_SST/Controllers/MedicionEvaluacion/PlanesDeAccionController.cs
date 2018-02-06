using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SG_SST.Controllers.Base;
using SG_SST.EntidadesDominio.MedicionEvaluacion;
using System.Configuration;
using SG_SST.ServiceRequest;
using SG_SST.Models.MedicionEvaluacion;


namespace SG_SST.Controllers.MedicionEvaluacion
{
    public class PlanesDeAccionController : BaseController
    {

        string UrlServicioMedicionEvaluacion = ConfigurationManager.AppSettings["UrlServicioMedicionEvaluacion"];
        string CapacidadObtenerPlanesDeAccion = ConfigurationManager.AppSettings["CapacidadObtenerPlanesDeAccion"];
        string CapacidadGuardarPlanesDeAccion = ConfigurationManager.AppSettings["CapacidadGuardarPlanesDeAccion"];
        string CapacidadEliminarActividad = ConfigurationManager.AppSettings["CapacidadEliminarActividad"];
        string CapacidadEditarActividad = ConfigurationManager.AppSettings["CapacidadEditarActividad"];
        string CapacidadAdicionarActividad = ConfigurationManager.AppSettings["CapacidadAdicionarActividad"];
        string CapacidadBuscarPlanesDeAccion = ConfigurationManager.AppSettings["CapacidadBuscarPlanesDeAccion"];
        string CapacidadObtenerModulos = ConfigurationManager.AppSettings["CapacidadObtenerModulos"];

        // GET: PlanesDeAccion
        public ActionResult Index()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                return View();
            }
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("NIT", usuarioActual.NitEmpresa);
            var resultEmpEval = ServiceClient.ObtenerObjetoJsonRestFul<List<EDPlanDeAccion>>(UrlServicioMedicionEvaluacion, CapacidadObtenerPlanesDeAccion, RestSharp.Method.GET);
            var resultModulo = ServiceClient.ObtenerObjetoJsonRestFul<List<ModulosPlanAccion>>(UrlServicioMedicionEvaluacion, CapacidadObtenerModulos, RestSharp.Method.GET);
            var result = (resultModulo.Select(x => new { Pk_Id_PlanDeAccion = x.Pk_Id_ModuloPlanAccion, Origen = x.Modulo })).Distinct().ToList();
            ViewBag.modulo = new SelectList(result, "Pk_Id_PlanDeAccion", "Origen");
            return View(resultEmpEval);
        }

        public ActionResult GuardarAccion(EDActividadPlanDeAccion actividadPlanDeAccion)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                return View();
            }
            ServiceClient.EliminarParametros();
            var resultEmpEval = ServiceClient.ObtenerObjetoJsonRestFul<EDActividadPlanDeAccion>(UrlServicioMedicionEvaluacion, CapacidadGuardarPlanesDeAccion, actividadPlanDeAccion);
            return Json(new { Data = resultEmpEval, Mensaje = "OK" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EliminarActividad(EDActividadPlanDeAccion actividadPlanDeAccion)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                return View();
            }
            ServiceClient.EliminarParametros();
            var resultEmpEval = ServiceClient.ObtenerObjetoJsonRestFul<bool>(UrlServicioMedicionEvaluacion, CapacidadEliminarActividad, actividadPlanDeAccion);
            return Json(new { Data = resultEmpEval, Mensaje = "OK" }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult EditarActividad(EDActividadPlanDeAccion actividadPlanDeAccion)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                return View();
            }
            ServiceClient.EliminarParametros();
            var resultEmpEval = ServiceClient.ObtenerObjetoJsonRestFul<bool>(UrlServicioMedicionEvaluacion, CapacidadEditarActividad, actividadPlanDeAccion);
            return Json(new { Data = resultEmpEval, Mensaje = "OK" }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AdicionarActividad(EDActividadPlanDeAccion actividadPlanDeAccion)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                return View();
            }
            ServiceClient.EliminarParametros();
            var resultEmpEval = ServiceClient.ObtenerObjetoJsonRestFul<bool>(UrlServicioMedicionEvaluacion, CapacidadAdicionarActividad, actividadPlanDeAccion);
            return Json(new { Data = resultEmpEval, Mensaje = "OK" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Graficar()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                return View();
            }
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("NIT", usuarioActual.NitEmpresa);
            var resultEmpEval = ServiceClient.ObtenerObjetoJsonRestFul<List<EDPlanDeAccion>>(UrlServicioMedicionEvaluacion, CapacidadObtenerPlanesDeAccion, RestSharp.Method.GET);
            return Json(( new {
               planesAccion = resultEmpEval.Count(),
               planesAccionAbiertos = resultEmpEval.Where(x=> x.Estado==1).Count(),
               planesAccionCerradosNoCumple = resultEmpEval.Where(x => x.Estado == 2 ).Count(),
               planesAccionEvaluacion = resultEmpEval.Where(x=> x.Pk_Id_PlanDeAccion==1).Count(),
               planesAccionEvaluacionAbiertos = resultEmpEval.Where(x => x.Pk_Id_PlanDeAccion == 1 && x.Estado==1).Count(),
               planesAccionAccion = resultEmpEval.Where(x => x.Pk_Id_PlanDeAccion == 2).Count(),
               planesAccionAccionAbiertos = resultEmpEval.Where(x => x.Pk_Id_PlanDeAccion == 2 && x.Estado == 1).Count(),
               planesAccionAuditoria = resultEmpEval.Where(x => x.Pk_Id_PlanDeAccion == 3).Count(),
               planesAccionAuditoriaAbiertos = resultEmpEval.Where(x => x.Pk_Id_PlanDeAccion == 3 && x.Estado == 1).Count(),
               planesAccionInspecciones = resultEmpEval.Where(x => x.Pk_Id_PlanDeAccion == 4).Count(),
               planesAccionInspeccionesAbiertos = resultEmpEval.Where(x => x.Pk_Id_PlanDeAccion == 4 && x.Estado == 1).Count(),
               planesAccionReportes = resultEmpEval.Where(x => x.Pk_Id_PlanDeAccion == 5).Count(),
               planesAccionReportesAbiertos = resultEmpEval.Where(x => x.Pk_Id_PlanDeAccion == 5 && x.Estado == 1).Count(),

               //incluir planes 46 y 47

               planesAccionCoppast = resultEmpEval.Where(x => x.Pk_Id_PlanDeAccion == 6).Count(),
               planesAccionCoppastAbiertos = resultEmpEval.Where(x => x.Pk_Id_PlanDeAccion == 6 && x.Estado == 1).Count(),
               planesAccionConvivencia = resultEmpEval.Where(x => x.Pk_Id_PlanDeAccion == 7).Count(),
               planesAccionConvivenciaAbiertos = resultEmpEval.Where(x => x.Pk_Id_PlanDeAccion == 7 && x.Estado == 1).Count(),

               //incluir plan 48
               planesAccionRevisionSGSST = resultEmpEval.Where(x => x.Pk_Id_PlanDeAccion == 8).Count(),
               planesAccionRevisionSGSSTAbiertos = resultEmpEval.Where(x => x.Pk_Id_PlanDeAccion == 8 && x.Estado == 1).Count(),




           })
        , JsonRequestBehavior.AllowGet);
        }

        public ActionResult Consultar(int modulo,string fechaInicial, string fechaFinal)
        {
            try
            {

             
                var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
                if (usuarioActual == null)
                {
                    ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                    return View();
                }
                ServiceClient.EliminarParametros();
                ServiceClient.AdicionarParametro("NIT", usuarioActual.NitEmpresa);
                ServiceClient.AdicionarParametro("Pk_Id_ModuloPlanAccion", modulo);
                ServiceClient.AdicionarParametro("fechaInicial", fechaInicial);
                ServiceClient.AdicionarParametro("fechaFinal", fechaFinal);
                var resultEmpEval = ServiceClient.ObtenerObjetoJsonRestFul<List<EDPlanDeAccion>>(UrlServicioMedicionEvaluacion, CapacidadBuscarPlanesDeAccion, RestSharp.Method.GET);
                var resultModulo = ServiceClient.ObtenerObjetoJsonRestFul<List<ModulosPlanAccion>>(UrlServicioMedicionEvaluacion, CapacidadObtenerModulos, RestSharp.Method.GET);
                var result = (resultModulo.Select(x => new { Pk_Id_PlanDeAccion = x.Pk_Id_ModuloPlanAccion, Origen = x.Modulo })).Distinct().ToList();
                ViewBag.modulo = new SelectList(result, "Pk_Id_PlanDeAccion", "Origen");
                if (resultEmpEval.Count == 0)
                    ViewBag.mensaje = 0;
                return View("Index",resultEmpEval);
            }
            catch (Exception ex)
            {

                return null;
            }
        }
    }
}