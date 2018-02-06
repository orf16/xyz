using SG_SST.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Web;
using System.Web.Mvc;
using SG_SST.ServiceRequest;
using SG_SST.EntidadesDominio.Planificacion;
using SG_SST.Models.Planificacion;

namespace SG_SST.Controllers.Planificacion
{
    public class ObetivoSSTController : BaseController
    {
        string urlServicioPlanificacion = ConfigurationManager.AppSettings["UrlServicioPlanificacion"];
        string ObtenerObjetivosst = ConfigurationManager.AppSettings["ObtenerObjetivosst"];
        string GrabarObjetivosst = ConfigurationManager.AppSettings["GrabarObjetivosst"];
        string EliminarObjetivosst = ConfigurationManager.AppSettings["EliminarObjetivosst"];

        // GET: ObetivoSST
        public ActionResult ObjetivoSST()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "Debe estar autenticado para realizar la evalación.";
                return RedirectToAction("Login", "Home");
            }

            ObjetivoSSTModel model = new ObjetivoSSTModel();
            model.NitEmpresa = usuarioActual.NitEmpresa;

            return View(model);
        }

        [HttpPost]
        public JsonResult ObtenerObjetivos()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return Json(new { Data = "Su sesión ha finalizado, por favor ingrese nuevamente.", Mensaje = "FinSession" });
            }

            List<ObjetivoSSTModel> listModel = new List<ObjetivoSSTModel>();

            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("IdEmpresa", usuarioActual.IdEmpresa);
            var resultObjetivosst = ServiceClient.ObtenerArrayJsonRestFul<EDObjetivoSST>(urlServicioPlanificacion, ObtenerObjetivosst, RestSharp.Method.GET);
            if (resultObjetivosst != null)
            {
                if (resultObjetivosst.Count() > 0)
                {
                    if (resultObjetivosst[0] != null)
                    {
                        listModel = resultObjetivosst.Select(o => new ObjetivoSSTModel
                            {
                                IdObjetivo = o.Id_Objetivo_Empresa,
                                Descripcion = o.Objetivo,
                                Meta = o.Meta
                            }).ToList();

                        var datos = RenderRazorViewToString("_DetalleObjetivos", listModel);

                        return Json(new { Data = datos, Mensaje = "OK" });
                    }
                    else
                        return Json(new { Data = "", Mensaje = "ERROR" });
                }
                else
                    return Json(new { Data = "", Mensaje = "ERROR" });
            }
            else
                return Json(new { Data = "", Mensaje = "ERROR" });

        }

        [HttpPost]
        public JsonResult CrearObjetivo(ObjetivoSSTModel objetivo)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return Json(new { Data = "Su sesión ha finalizado, por favor ingrese nuevamente.", Mensaje = "FinSession" });
            }

            EDObjetivoSST Objetivo = new EDObjetivoSST();
            Objetivo.Id_Empresa = usuarioActual.IdEmpresa;
            Objetivo.Objetivo = objetivo.Descripcion;
            if (objetivo.EsPorcentaje)
                Objetivo.Meta = objetivo.Meta + "%";
            else
                Objetivo.Meta = objetivo.Meta;

            List<ObjetivoSSTModel> listModel = new List<ObjetivoSSTModel>();

            ServiceClient.EliminarParametros();
            var resultObjetivosst = ServiceClient.RealizarPeticionesArrayPostJsonRest<EDObjetivoSST>(urlServicioPlanificacion, GrabarObjetivosst, Objetivo);
            if (resultObjetivosst != null)
            {
                if (resultObjetivosst.Count() > 0)
                {
                    if (resultObjetivosst[0] != null)
                    {
                        listModel = resultObjetivosst.Select(o => new ObjetivoSSTModel
                        {
                            IdObjetivo = o.Id_Objetivo_Empresa,
                            Descripcion = o.Objetivo,
                            Meta = o.Meta
                        }).ToList();

                        var datos = RenderRazorViewToString("_DetalleObjetivos", listModel);
                        return Json(new { Data = datos, Mensaje = "OK" });
                    }
                    else
                        return Json(new { Data = "El proceso de creación del objetivo ha fallado", Mensaje = "ERROR" });
                }
                else
                    return Json(new { Data = "El proceso de creación del objetivo ha fallado", Mensaje = "ERROR" });
            }
            else
                return Json(new { Data = "El proceso de creación del objetivo ha fallado", Mensaje = "ERROR" });
        }

        [HttpPost]
        public JsonResult EliminarObjetivo(string listaObjetivos)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return Json(new { Data = "Su sesión ha finalizado, por favor ingrese nuevamente.", Mensaje = "FinSession" });
            }

            List<EDObjetivoSST> objetivos = new List<EDObjetivoSST>();

            objetivos = listaObjetivos.Split(';').Where(o => !string.IsNullOrEmpty(o)).Select(o => new EDObjetivoSST
            {
                Id_Objetivo_Empresa = Convert.ToInt32(o.ToString()),
                Id_Empresa = usuarioActual.IdEmpresa
            }).ToList();

            List<ObjetivoSSTModel> listModel = new List<ObjetivoSSTModel>();

            ServiceClient.EliminarParametros();
            var resultObjetivosst = ServiceClient.RealizarPeticionesArrayPostJsonRest<EDObjetivoSST>(urlServicioPlanificacion, EliminarObjetivosst, objetivos);
            if (resultObjetivosst != null)
            {
                if (resultObjetivosst.Count() > 0)
                {
                    if (resultObjetivosst[0] != null)
                    {
                        listModel = resultObjetivosst.Select(o => new ObjetivoSSTModel
                        {
                            IdObjetivo = o.Id_Objetivo_Empresa,
                            Descripcion = o.Objetivo,
                            Meta = o.Meta
                        }).ToList();

                        var datos = RenderRazorViewToString("_DetalleObjetivos", listModel);
                        return Json(new { Data = datos, Mensaje = "OK" });
                    }
                    else
                        return Json(new { Data = "El proceso de eliminación ha fallado", Mensaje = "ERROR" });
                }
                else
                    return Json(new { Data = "El proceso de eliminación ha fallado", Mensaje = "BORRADO" });
            }
            else
                return Json(new { Data = "El proceso de eliminación ha fallado", Mensaje = "ERROR" });
        }
    }
}