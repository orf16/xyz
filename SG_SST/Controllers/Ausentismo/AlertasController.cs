using SG_SST.Controllers.Base;
using SG_SST.EntidadesDominio.Empresas;
using SG_SST.Logica.Ausentismo;
using SG_SST.Models.Ausentismo;
using SG_SST.ServiceRequest;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SG_SST.Controllers.Ausentismo
{
    public class AlertasController : BaseController
    {
        string urlServicioEmpresas = ConfigurationManager.AppSettings["UrlServicioEmpresas"];
        string CapacidadObtenerEmpresasusuarias = ConfigurationManager.AppSettings["CapacidadObtenerEmpresasusuarias"];
        LNAlertas lnalertas = new LNAlertas();
        public ActionResult Alertas()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            var objAlerta = new AlertasModel();
            objAlerta.Anios = objAlerta.ConfigurarAnios();
            objAlerta.EmpresasUsuarias = ObtenerListaEmpresasUsuarias();
            objAlerta.RazonSocial = usuarioActual.RazonSocialEmpresa;

            return View(objAlerta);
        }

        /// <summary>
        /// Retorna la lista de las empresas usuarias.
        /// </summary>
        /// <param name="nitEmpresa"></param>
        /// <returns></returns>
        List<SelectListItem> ObtenerListaEmpresasUsuarias()
        {
            var nitEmpresa = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current).NitEmpresa;
            var Resultado = new List<SelectListItem>();
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("NIT", nitEmpresa);
            var result = ServiceClient.ObtenerArrayJsonRestFul<EDEmpresa_Usuaria>(urlServicioEmpresas, CapacidadObtenerEmpresasusuarias, RestSharp.Method.GET);
            if (result != null && result.Count() > 0)
            {
                Resultado = result.Select(c => new SelectListItem()
                {
                    Value = c.IdEmpresaUsuaria.ToString(),
                    Text = c.RazonSocial
                }).OrderBy(x => x.Text.ToLower()).ToList();
            }

            // Si solo hay una empresa, dejarla seleccionada.
            //if (Resultado.Count() == 1)
            //    Resultado.First().Selected = true;

            return Resultado;
        }

        [HttpPost]
        public JsonResult ConsultarAlertas(int anio)
        {
            List<AlertasModel> objAlertas = null;
            if (ModelState.IsValid)
            {
                var result = lnalertas.ConsultarAusencias(anio);
                if (result != null)
                {
                    objAlertas = result.Select(a => new AlertasModel()
                    {
                        FechaRegistro = a.FechaRegistro,
                        Documento = a.Documento,
                        Departamento = a.Departamento,
                        Municipio = a.Municipio,
                        Contingencia = a.Contingencia,
                        Diagnostico = a.Diagnostico,
                        Dias_Ausencia = a.Dias_Ausencia,
                        Costo = a.costo
                    }).ToList();
                }
            }else
                return Json(new { Data = string.Empty, Mensaje = "Fail" });
            if(objAlertas != null )
            {
                var datos = RenderRazorViewToString("_AlertasAusentismo", objAlertas);
                return Json(new { Data = datos, Mensaje = "Success" });
            }
            else
                return Json(new { Data = string.Empty, Mensaje = "Fail" });
        }
    }
}