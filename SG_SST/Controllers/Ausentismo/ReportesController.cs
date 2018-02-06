using Newtonsoft.Json;
using SG_SST.Controllers.Base;
using SG_SST.EntidadesDominio.Ausentismo;
using SG_SST.EntidadesDominio.Empresas;
using SG_SST.Logica.Ausentismo;
using SG_SST.Models.Ausentismo;
using SG_SST.Reportes;
using SG_SST.ServiceRequest;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace SG_SST.Controllers.Ausentismo
{
    public class ReportesController : BaseController
    {
        string urlServicioEmpresas = ConfigurationManager.AppSettings["UrlServicioEmpresas"];
        string UrlServicioPlanificacion = ConfigurationManager.AppSettings["UrlServicioPlanificacion"];
        string CapacidadObtenerSedesPorNit = ConfigurationManager.AppSettings["CapacidadObtenerSedesPorNit"];
        string CapacidadObtenerEmpresasusuarias = ConfigurationManager.AppSettings["CapacidadObtenerEmpresasusuarias"];
        string CapacidadObtenerAnoInicioEmpresa = ConfigurationManager.AppSettings["CapacidadObtenerAnoInicioEmpresa"];

        string Capacidaddescargarexcelcontigencias = ConfigurationManager.AppSettings["Capacidaddescargarexcelcontigencias"];
        string Capacidaddescargarexcelevento = ConfigurationManager.AppSettings["Capacidaddescargarexcelevento"];
        string Capacidaddescargarexceldepartamento = ConfigurationManager.AppSettings["Capacidaddescargarexceldepartamento"];
        string Capacidaddescargarexcelenfermedades = ConfigurationManager.AppSettings["Capacidaddescargarexcelenfermedades"];
        string Capacidaddescargarexcelenfermedadeseven = ConfigurationManager.AppSettings["Capacidaddescargarexcelenfermedadeseven"];
        string Capacidaddescargarexcelproceso = ConfigurationManager.AppSettings["Capacidaddescargarexcelproceso"];
        string Capacidaddescargarexcelsede = ConfigurationManager.AppSettings["Capacidaddescargarexcelsede"];
        string Capacidaddescargarexcelpromediocontingencias = ConfigurationManager.AppSettings["Capacidaddescargarexcelpromediocontingencias"];
        string Capacidaddescargarexceleps = ConfigurationManager.AppSettings["Capacidaddescargarexceleps"];
        string Capacidaddescargarexcelsexo = ConfigurationManager.AppSettings["Capacidaddescargarexcelsexo"];
        string Capacidaddescargarexcelvinculacion = ConfigurationManager.AppSettings["Capacidaddescargarexcelvinculacion"];
        string Capacidaddescargarexcelocupacion = ConfigurationManager.AppSettings["Capacidaddescargarexcelocupacion"];
        string Capacidaddescargarexcelgrupoetarios = ConfigurationManager.AppSettings["Capacidaddescargarexcelgrupoetarios"];

       
        LNReportes lnreportes = new LNReportes();
        // GET: Reportes
        public ActionResult Reportes()
        {
            LNAusencia lnausencia = new LNAusencia();
            LNDepartamento lnDepartamento = new LNDepartamento();
            ReportesModel reporte = new ReportesModel();
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            reporte.RazonSocial = usuarioActual.RazonSocialEmpresa;
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("NIT", usuarioActual.NitEmpresa);
            var resultAno = ServiceClient.ObtenerObjetoJsonRestFul<int>(UrlServicioPlanificacion, CapacidadObtenerAnoInicioEmpresa, RestSharp.Method.GET);
            if (resultAno > 0)
            {
                reporte.Anios = GetAnios(resultAno);
            }
            else
                reporte.Anios = GetAnios(2010);

            reporte.Departamentos = lnDepartamento.ObtenerListadoDepartamento().Select(d => new SelectListItem() { Value = d.IdDepartamento.ToString(), Text = d.Nombre }).ToList();
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("NIT", usuarioActual.NitEmpresa);
            var resultEU = ServiceClient.ObtenerArrayJsonRestFul<EDEmpresa_Usuaria>(urlServicioEmpresas, CapacidadObtenerEmpresasusuarias, RestSharp.Method.GET);
            if (resultEU != null && resultEU.Count() > 0)
            {
                reporte.EmpresasUsuarias = resultEU.Select(c => new SelectListItem()
                {
                    Value = c.IdEmpresaUsuaria.ToString(),
                    Text = c.RazonSocial
                }).ToList();
            }
            else
                reporte.EmpresasUsuarias = new List<SelectListItem>();
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("NIT", usuarioActual.NitEmpresa);
            var resultSede = ServiceClient.ObtenerArrayJsonRestFul<EDSede>(urlServicioEmpresas, CapacidadObtenerSedesPorNit, RestSharp.Method.GET);
            if (resultSede != null && resultSede.Count() > 0)
            {
                reporte.Sedes = resultSede.Select(c => new SelectListItem()
                {
                    Value = c.IdSede.ToString(),
                    Text = c.NombreSede
                }).ToList();
            }
            else
                reporte.Sedes = new List<SelectListItem>();


            reporte.Reportes = reporte.GetResportes();

            return View(reporte);
        }
        public JsonResult ReportePorContingencia(ReportesModel dataReportes)
        {
            List<ReportesModel> objReporteContingencia = null;
            var objEvaluacion = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (objEvaluacion == null)
            {
                return Json(new { Data = "La sesión a finalizado", Mensaje = "FinSession" });
            }

            EDReportes edReporte = new EDReportes();
            edReporte.anio = dataReportes.anio;
            edReporte.idOrigen = dataReportes.idOrigen;
            edReporte.IdEmpresaUsuaria = dataReportes.IdEmpresaUsuaria;
            edReporte.idSede = dataReportes.idSede;
            edReporte.IdDepartamento = dataReportes.IdDepartamento;
            edReporte.nitEmpresa = objEvaluacion.NitEmpresa;

            var result = lnreportes.ConsultarPorContingencia(edReporte);
            if (result != null && result.Count() > 0)
            {
                objReporteContingencia = result.Select(r => new ReportesModel()
                {
                    CONTINGENCIA = r.CONTINGENCIA,
                    strEne = ObtenerValorConformato(r.Ene.ToString()),
                    strFeb = ObtenerValorConformato(r.Feb.ToString()),
                    strMar = ObtenerValorConformato(r.Mar.ToString()),
                    strAbr = ObtenerValorConformato(r.Abr.ToString()),
                    strMay = ObtenerValorConformato(r.May.ToString()),
                    strJun = ObtenerValorConformato(r.Jun.ToString()),
                    strJul = ObtenerValorConformato(r.Jul.ToString()),
                    strAgo = ObtenerValorConformato(r.Ago.ToString()),
                    strSep = ObtenerValorConformato(r.Sep.ToString()),
                    strOct = ObtenerValorConformato(r.Oct.ToString()),
                    strNov = ObtenerValorConformato(r.Nov.ToString()),
                    strDic = ObtenerValorConformato(r.Dic.ToString()),
                    strTotal = ObtenerValorConformato(r.Total.ToString())
                }).ToList();
            }
            else
                return Json(new { Data = string.Empty, Mensaje = "Fail" });
            if (objReporteContingencia != null && objReporteContingencia.Count() > 0)
            {
               
                var datos = RenderRazorViewToString("_ReportesContingencia", objReporteContingencia);
                return Json(new { Data = datos, Mensaje = "Success" });
            }
            else
                return Json(new { Data = string.Empty, Mensaje = "Fail" });
        }

        public JsonResult ReportePorEvento(ReportesModel dataReportes)
        {
            List<ReportesModel> objReporteevento = null;
            List<ReportesModel> objReporteContingencia = null;
            var objEvaluacion = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (objEvaluacion == null)
            {
                return Json(new { Data = "La sesión a finalizado", Mensaje = "FinSession" });
            }

            EDReportes edReporte = new EDReportes();
            edReporte.anio = dataReportes.anio;
            edReporte.idOrigen = dataReportes.idOrigen;
            edReporte.IdEmpresaUsuaria = dataReportes.IdEmpresaUsuaria;
            edReporte.idSede = dataReportes.idSede;
            edReporte.IdDepartamento = dataReportes.IdDepartamento;
            edReporte.nitEmpresa = objEvaluacion.NitEmpresa;

            var result = lnreportes.ConsultarPorEventos(edReporte);
            if (result != null && result.Count > 0)
            {
                objReporteevento = result.Select(r => new ReportesModel()
                {
                    CONTINGENCIA = r.CONTINGENCIA,
                    Evento = r.Evento,
                    strEne = ObtenerValorConformato(r.Ene.ToString()),
                    strFeb = ObtenerValorConformato(r.Feb.ToString()),
                    strMar = ObtenerValorConformato(r.Mar.ToString()),
                    strAbr = ObtenerValorConformato(r.Abr.ToString()),
                    strMay = ObtenerValorConformato(r.May.ToString()),
                    strJun = ObtenerValorConformato(r.Jun.ToString()),
                    strJul = ObtenerValorConformato(r.Jul.ToString()),
                    strAgo = ObtenerValorConformato(r.Ago.ToString()),
                    strSep = ObtenerValorConformato(r.Sep.ToString()),
                    strOct = ObtenerValorConformato(r.Oct.ToString()),
                    strNov = ObtenerValorConformato(r.Nov.ToString()),
                    strDic = ObtenerValorConformato(r.Dic.ToString()),
                    strTotal = ObtenerValorConformato(r.Total.ToString())
                }).ToList();
            }
            else
                return Json(new { Data = string.Empty, Mensaje = "Fail" });
            if (objReporteevento != null)
            {
                var datos = RenderRazorViewToString("_ReportesEvento", objReporteevento);
                return Json(new { Data = datos, Mensaje = "Success" });
            }
            else
                return Json(new { Data = string.Empty, Mensaje = "Fail" });
        }

        public JsonResult ReportePorDepartamento(ReportesModel dataReportes)
        {
            List<ReportesModel> objReporteDepartamento = null;
            
            var objEvaluacion = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (objEvaluacion == null)
            {
                return Json(new { Data = "La sesión a finalizado", Mensaje = "FinSession" });
            }

            EDReportes edReporte = new EDReportes();
            edReporte.anio = dataReportes.anio;
            edReporte.idOrigen = dataReportes.idOrigen;
            edReporte.IdEmpresaUsuaria = dataReportes.IdEmpresaUsuaria;
            edReporte.idSede = dataReportes.idSede;
            edReporte.IdDepartamento = dataReportes.IdDepartamento;
            edReporte.nitEmpresa = objEvaluacion.NitEmpresa;

            var result = lnreportes.ConsultarPorDepartamento(edReporte);
            if (result != null && result.Count() > 0)
            {
                objReporteDepartamento = result.Select(r => new ReportesModel()
                {
                    Departamento = r.Evento,
                    strEne = ObtenerValorConformato(r.Ene.ToString()),
                    strFeb = ObtenerValorConformato(r.Feb.ToString()),
                    strMar = ObtenerValorConformato(r.Mar.ToString()),
                    strAbr = ObtenerValorConformato(r.Abr.ToString()),
                    strMay = ObtenerValorConformato(r.May.ToString()),
                    strJun = ObtenerValorConformato(r.Jun.ToString()),
                    strJul = ObtenerValorConformato(r.Jul.ToString()),
                    strAgo = ObtenerValorConformato(r.Ago.ToString()),
                    strSep = ObtenerValorConformato(r.Sep.ToString()),
                    strOct = ObtenerValorConformato(r.Oct.ToString()),
                    strNov = ObtenerValorConformato(r.Nov.ToString()),
                    strDic = ObtenerValorConformato(r.Dic.ToString()),
                    strTotal = ObtenerValorConformato(r.Total.ToString())
                }).ToList();
            }
            else
                return Json(new { Data = string.Empty, Mensaje = "Fail" });
            if (objReporteDepartamento != null)
            {
                var datos = RenderRazorViewToString("_ReportesDepartamento", objReporteDepartamento);
                return Json(new { Data = datos, Mensaje = "Success" });
            }
            else
                return Json(new { Data = string.Empty, Mensaje = "Fail" });
        }

        public JsonResult ReportePorEnfermedades(ReportesModel dataReportes)
        {
            List<ReportesModel> objReporteEnfermedades = null;
            var objEvaluacion = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (objEvaluacion == null)
            {
                return Json(new { Data = "La sesión a finalizado", Mensaje = "FinSession" });
            }

            EDReportes edReporte = new EDReportes();
            edReporte.anio = dataReportes.anio;
            edReporte.idOrigen = dataReportes.idOrigen;
            edReporte.IdEmpresaUsuaria = dataReportes.IdEmpresaUsuaria;
            edReporte.idSede = dataReportes.idSede;
            edReporte.IdDepartamento = dataReportes.IdDepartamento;
            edReporte.nitEmpresa = objEvaluacion.NitEmpresa;

            var result = lnreportes.ConsultarPorEnfermedades(edReporte);
            if (result != null && result.Count() > 0)
            {
                objReporteEnfermedades = result.Select(r => new ReportesModel()
                {
                    Enfermedades = r.Evento,
                    strEne = ObtenerValorConformato(r.Ene.ToString()),
                    strFeb = ObtenerValorConformato(r.Feb.ToString()),
                    strMar = ObtenerValorConformato(r.Mar.ToString()),
                    strAbr = ObtenerValorConformato(r.Abr.ToString()),
                    strMay = ObtenerValorConformato(r.May.ToString()),
                    strJun = ObtenerValorConformato(r.Jun.ToString()),
                    strJul = ObtenerValorConformato(r.Jul.ToString()),
                    strAgo = ObtenerValorConformato(r.Ago.ToString()),
                    strSep = ObtenerValorConformato(r.Sep.ToString()),
                    strOct = ObtenerValorConformato(r.Oct.ToString()),
                    strNov = ObtenerValorConformato(r.Nov.ToString()),
                    strDic = ObtenerValorConformato(r.Dic.ToString()),
                    strTotal = ObtenerValorConformato(r.Total.ToString())
                }).ToList();
            }
            else
                return Json(new { Data = string.Empty, Mensaje = "Fail" });
            if (objReporteEnfermedades != null)
            {
                var datos = RenderRazorViewToString("_ReportesEnfermedad", objReporteEnfermedades);
                return Json(new { Data = datos, Mensaje = "Success" });
            }
            else
                return Json(new { Data = string.Empty, Mensaje = "Fail" });
        }
        public JsonResult ReportePorEps(ReportesModel dataReportes)
        {
            List<ReportesModel> objReporteEps = null;
            var objEvaluacion = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (objEvaluacion == null)
            {
                return Json(new { Data = "La sesión a finalizado", Mensaje = "FinSession" });
            }

            EDReportes edReporte = new EDReportes();
            edReporte.anio = dataReportes.anio;
            edReporte.idOrigen = dataReportes.idOrigen;
            edReporte.IdEmpresaUsuaria = dataReportes.IdEmpresaUsuaria;
            edReporte.idSede = dataReportes.idSede;
            edReporte.IdDepartamento = dataReportes.IdDepartamento;
            edReporte.nitEmpresa = objEvaluacion.NitEmpresa;

            var result = lnreportes.ConsultarPorEps(edReporte);
            if (result != null && result.Count() > 0)
            {
                objReporteEps = result.Select(r => new ReportesModel()
                {
                    Eps = r.Evento,
                    strEne = ObtenerValorConformato(r.Ene.ToString()),
                    strFeb = ObtenerValorConformato(r.Feb.ToString()),
                    strMar = ObtenerValorConformato(r.Mar.ToString()),
                    strAbr = ObtenerValorConformato(r.Abr.ToString()),
                    strMay = ObtenerValorConformato(r.May.ToString()),
                    strJun = ObtenerValorConformato(r.Jun.ToString()),
                    strJul = ObtenerValorConformato(r.Jul.ToString()),
                    strAgo = ObtenerValorConformato(r.Ago.ToString()),
                    strSep = ObtenerValorConformato(r.Sep.ToString()),
                    strOct = ObtenerValorConformato(r.Oct.ToString()),
                    strNov = ObtenerValorConformato(r.Nov.ToString()),
                    strDic = ObtenerValorConformato(r.Dic.ToString()),
                    strTotal = ObtenerValorConformato(r.Total.ToString())
                }).ToList();
            }
            else
                return Json(new { Data = string.Empty, Mensaje = "Fail" });
            if (objReporteEps != null)
            {
                var datos = RenderRazorViewToString("_ReporteDiasEps", objReporteEps);
                return Json(new { Data = datos, Mensaje = "Success" });
            }
            else
                return Json(new { Data = string.Empty, Mensaje = "Fail" });
        }

        public JsonResult ReporteAusenciasPorVinculacion(ReportesModel dataReportes)
        {
            List<ReportesModel> objReporteOcupacion = null;
            var objEvaluacion = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (objEvaluacion == null)
            {
                return Json(new { Data = "La sesión a finalizado", Mensaje = "FinSession" });
            }

            EDReportes edReporte = new EDReportes();
            edReporte.anio = dataReportes.anio;
            edReporte.idOrigen = dataReportes.idOrigen;
            edReporte.IdEmpresaUsuaria = dataReportes.IdEmpresaUsuaria;
            edReporte.idSede = dataReportes.idSede;
            edReporte.IdDepartamento = dataReportes.IdDepartamento;
            edReporte.nitEmpresa = objEvaluacion.NitEmpresa;

            var result = lnreportes.ConsultarAusenciasPorVinculacion(edReporte);
            if (result != null && result.Count() > 0)
            {
                objReporteOcupacion = result.Select(r => new ReportesModel()
                {
                    Ocupacion = r.Evento,
                    strEne = ObtenerValorConformato(r.Ene.ToString()),
                    strFeb = ObtenerValorConformato(r.Feb.ToString()),
                    strMar = ObtenerValorConformato(r.Mar.ToString()),
                    strAbr = ObtenerValorConformato(r.Abr.ToString()),
                    strMay = ObtenerValorConformato(r.May.ToString()),
                    strJun = ObtenerValorConformato(r.Jun.ToString()),
                    strJul = ObtenerValorConformato(r.Jul.ToString()),
                    strAgo = ObtenerValorConformato(r.Ago.ToString()),
                    strSep = ObtenerValorConformato(r.Sep.ToString()),
                    strOct = ObtenerValorConformato(r.Oct.ToString()),
                    strNov = ObtenerValorConformato(r.Nov.ToString()),
                    strDic = ObtenerValorConformato(r.Dic.ToString()),
                    strTotal = ObtenerValorConformato(r.Total.ToString())
                }).ToList();
            }
            else
                return Json(new { Data = string.Empty, Mensaje = "Fail" });
            if (objReporteOcupacion != null)
            {
                var datos = RenderRazorViewToString("_ReporteAusenciasPorTipoVinculacion", objReporteOcupacion);
                return Json(new { Data = datos, Mensaje = "Success" });
            }
            else
                return Json(new { Data = string.Empty, Mensaje = "Fail" });
        }

        public JsonResult ReportePorSede(ReportesModel dataReportes)
        {
            List<ReportesModel> objReporteSede = null;
            var objEvaluacion = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (objEvaluacion == null)
            {
                return Json(new { Data = "La sesión a finalizado", Mensaje = "FinSession" });
            }

            EDReportes edReporte = new EDReportes();
            edReporte.anio = dataReportes.anio;
            edReporte.idOrigen = dataReportes.idOrigen;
            edReporte.IdEmpresaUsuaria = dataReportes.IdEmpresaUsuaria;
            edReporte.idSede = dataReportes.idSede;
            edReporte.IdDepartamento = dataReportes.IdDepartamento;
            edReporte.nitEmpresa = objEvaluacion.NitEmpresa;

            var result = lnreportes.ConsultarPorSede(edReporte);
            if (result != null && result.Count() > 0)
            {
                objReporteSede = result.Select(r => new ReportesModel()
                {
                    Sede = r.Evento,
                    strEne = ObtenerValorConformato(r.Ene.ToString()),
                    strFeb = ObtenerValorConformato(r.Feb.ToString()),
                    strMar = ObtenerValorConformato(r.Mar.ToString()),
                    strAbr = ObtenerValorConformato(r.Abr.ToString()),
                    strMay = ObtenerValorConformato(r.May.ToString()),
                    strJun = ObtenerValorConformato(r.Jun.ToString()),
                    strJul = ObtenerValorConformato(r.Jul.ToString()),
                    strAgo = ObtenerValorConformato(r.Ago.ToString()),
                    strSep = ObtenerValorConformato(r.Sep.ToString()),
                    strOct = ObtenerValorConformato(r.Oct.ToString()),
                    strNov = ObtenerValorConformato(r.Nov.ToString()),
                    strDic = ObtenerValorConformato(r.Dic.ToString()),
                    strTotal = ObtenerValorConformato(r.Total.ToString())
                }).ToList();
            }
            else
                return Json(new { Data = string.Empty, Mensaje = "Fail" });
            if (objReporteSede != null)
            {
                var datos = RenderRazorViewToString("_ReporteSede", objReporteSede);
                return Json(new { Data = datos, Mensaje = "Success" });
            }
            else
                return Json(new { Data = string.Empty, Mensaje = "Fail" });
        }

        public JsonResult ReportePorSexo(ReportesModel dataReportes)
        {
            List<ReportesModel> objReporteSexo = null;
            var objEvaluacion = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (objEvaluacion == null)
            {
                return Json(new { Data = "La sesión a finalizado", Mensaje = "FinSession" });
            }

            EDReportes edReporte = new EDReportes();
            edReporte.anio = dataReportes.anio;
            edReporte.idOrigen = dataReportes.idOrigen;
            edReporte.IdEmpresaUsuaria = dataReportes.IdEmpresaUsuaria;
            edReporte.idSede = dataReportes.idSede;
            edReporte.IdDepartamento = dataReportes.IdDepartamento;
            edReporte.nitEmpresa = objEvaluacion.NitEmpresa;

            var result = lnreportes.ConsultarPorSexo(edReporte);
            if (result != null && result.Count() > 0)
            {
                objReporteSexo = result.Select(r => new ReportesModel()
                {
                    Sexo = r.Evento,
                    strEne = ObtenerValorConformato(r.Ene.ToString()),
                    strFeb = ObtenerValorConformato(r.Feb.ToString()),
                    strMar = ObtenerValorConformato(r.Mar.ToString()),
                    strAbr = ObtenerValorConformato(r.Abr.ToString()),
                    strMay = ObtenerValorConformato(r.May.ToString()),
                    strJun = ObtenerValorConformato(r.Jun.ToString()),
                    strJul = ObtenerValorConformato(r.Jul.ToString()),
                    strAgo = ObtenerValorConformato(r.Ago.ToString()),
                    strSep = ObtenerValorConformato(r.Sep.ToString()),
                    strOct = ObtenerValorConformato(r.Oct.ToString()),
                    strNov = ObtenerValorConformato(r.Nov.ToString()),
                    strDic = ObtenerValorConformato(r.Dic.ToString()),
                    strTotal = ObtenerValorConformato(r.Total.ToString())
                }).ToList();
            }
            else
                return Json(new { Data = string.Empty, Mensaje = "Fail" });
            if (objReporteSexo != null)
            {
                var datos = RenderRazorViewToString("_ReporteSexo", objReporteSexo);
                return Json(new { Data = datos, Mensaje = "Success" });
            }
            else
                return Json(new { Data = string.Empty, Mensaje = "Fail" });
        }

        public JsonResult ReportePorCosto(ReportesModel dataReportes)
        {
            List<ReportesModel> objReporteCosto = null;
            var objEvaluacion = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (objEvaluacion == null)
            {
                return Json(new { Data = "La sesión a finalizado", Mensaje = "FinSession" });
            }

            EDReportes edReporte = new EDReportes();
            edReporte.anio = dataReportes.anio;
            edReporte.idOrigen = dataReportes.idOrigen;
            edReporte.IdEmpresaUsuaria = dataReportes.IdEmpresaUsuaria;
            edReporte.idSede = dataReportes.idSede;
            edReporte.IdDepartamento = dataReportes.IdDepartamento;
            edReporte.nitEmpresa = objEvaluacion.NitEmpresa;

            var result = lnreportes.ConsultarPorCosto(edReporte);
            if (result != null && result.Count() > 0)
            {
                objReporteCosto = result.Select(r => new ReportesModel()
                {
                    Descripcion = r.CONTINGENCIA,
                    strEne = ObtenerValorConformato(r.Ene.ToString()),
                    strFeb = ObtenerValorConformato(r.Feb.ToString()),
                    strMar = ObtenerValorConformato(r.Mar.ToString()),
                    strAbr = ObtenerValorConformato(r.Abr.ToString()),
                    strMay = ObtenerValorConformato(r.May.ToString()),
                    strJun = ObtenerValorConformato(r.Jun.ToString()),
                    strJul = ObtenerValorConformato(r.Jul.ToString()),
                    strAgo = ObtenerValorConformato(r.Ago.ToString()),
                    strSep = ObtenerValorConformato(r.Sep.ToString()),
                    strOct = ObtenerValorConformato(r.Oct.ToString()),
                    strNov = ObtenerValorConformato(r.Nov.ToString()),
                    strDic = ObtenerValorConformato(r.Dic.ToString()),
                    strTotal = ObtenerValorConformato(r.Total.ToString())
                }).ToList();
            }
            else
                return Json(new { Data = string.Empty, Mensaje = "Fail" });
            if (objReporteCosto != null)
            {
                var datos = RenderRazorViewToString("_ReporteCostoContingencia", objReporteCosto);
                return Json(new { Data = datos, Mensaje = "Success" });
            }
            else
                return Json(new { Data = string.Empty, Mensaje = "Fail" });
        }

        /// <summary>
        /// Obtiene el reporte de Numero de eventos por capitulo de emfermedades
        /// </summary>
        /// <param name="dataReportes"></param>
        /// <returns></returns>
        public JsonResult ReporteNumEventosPorEnfermedad(ReportesModel dataReportes)
        {
            List<ReportesModel> objReporteevento = null;
            var objEvaluacion = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (objEvaluacion == null)
            {
                return Json(new { Data = "La sesión a finalizado", Mensaje = "FinSession" });
            }

            EDReportes edReporte = new EDReportes();
            edReporte.anio = dataReportes.anio;
            edReporte.idOrigen = dataReportes.idOrigen;
            edReporte.IdEmpresaUsuaria = dataReportes.IdEmpresaUsuaria;
            edReporte.idSede = dataReportes.idSede;
            edReporte.IdDepartamento = dataReportes.IdDepartamento;
            edReporte.nitEmpresa = objEvaluacion.NitEmpresa;

            var result = lnreportes.ReporteCantiEventPorEnfermedades(edReporte);
            if (result != null && result.Count() > 0)
            {
                objReporteevento = result.Select(r => new ReportesModel()
                {
                    CONTINGENCIA = r.CONTINGENCIA,
                    Evento = r.Evento,
                    strEne = ObtenerValorConformato(r.Ene.ToString()),
                    strFeb = ObtenerValorConformato(r.Feb.ToString()),
                    strMar = ObtenerValorConformato(r.Mar.ToString()),
                    strAbr = ObtenerValorConformato(r.Abr.ToString()),
                    strMay = ObtenerValorConformato(r.May.ToString()),
                    strJun = ObtenerValorConformato(r.Jun.ToString()),
                    strJul = ObtenerValorConformato(r.Jul.ToString()),
                    strAgo = ObtenerValorConformato(r.Ago.ToString()),
                    strSep = ObtenerValorConformato(r.Sep.ToString()),
                    strOct = ObtenerValorConformato(r.Oct.ToString()),
                    strNov = ObtenerValorConformato(r.Nov.ToString()),
                    strDic = ObtenerValorConformato(r.Dic.ToString()),
                    strTotal = ObtenerValorConformato(r.Total.ToString())
                }).ToList();
            }
            else
                return Json(new { Data = string.Empty, Mensaje = "Fail" });
            if (dataReportes != null)
            {
                var datos = RenderRazorViewToString("_ReporteCantidadEventosEnfermedades", objReporteevento);
                return Json(new { Data = datos, Mensaje = "Success" });
            }
            else
                return Json(new { Data = string.Empty, Mensaje = "Fail" });
        }

        /// <summary>
        /// Obtiene el reporte de dias ausentismo por proceso
        /// </summary>
        /// <param name="dataReportes"></param>
        /// <returns></returns>
        public JsonResult ReporteDiasAusentismoProceso(ReportesModel dataReportes)
        {
            List<ReportesModel> objReporteCosto = null;
            var objEvaluacion = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (objEvaluacion == null)
            {
                return Json(new { Data = "La sesión a finalizado", Mensaje = "FinSession" });
            }

            EDReportes edReporte = new EDReportes();
            edReporte.anio = dataReportes.anio;
            edReporte.idOrigen = dataReportes.idOrigen;
            edReporte.IdEmpresaUsuaria = dataReportes.IdEmpresaUsuaria;
            edReporte.idSede = dataReportes.idSede;
            edReporte.IdDepartamento = dataReportes.IdDepartamento;
            edReporte.nitEmpresa = objEvaluacion.NitEmpresa;

            var result = lnreportes.ReporteDiasAusentismoPorProceso(edReporte);
            if (result != null && result.Count() > 0)
            {
                objReporteCosto = result.Select(r => new ReportesModel()
                {
                    Descripcion = r.Evento,
                    strEne = ObtenerValorConformato(r.Ene.ToString()),
                    strFeb = ObtenerValorConformato(r.Feb.ToString()),
                    strMar = ObtenerValorConformato(r.Mar.ToString()),
                    strAbr = ObtenerValorConformato(r.Abr.ToString()),
                    strMay = ObtenerValorConformato(r.May.ToString()),
                    strJun = ObtenerValorConformato(r.Jun.ToString()),
                    strJul = ObtenerValorConformato(r.Jul.ToString()),
                    strAgo = ObtenerValorConformato(r.Ago.ToString()),
                    strSep = ObtenerValorConformato(r.Sep.ToString()),
                    strOct = ObtenerValorConformato(r.Oct.ToString()),
                    strNov = ObtenerValorConformato(r.Nov.ToString()),
                    strDic = ObtenerValorConformato(r.Dic.ToString()),
                    strTotal = ObtenerValorConformato(r.Total.ToString())
                }).ToList();
            }
            else
                return Json(new { Data = string.Empty, Mensaje = "Fail" });
            if (objReporteCosto != null)
            {
                var datos = RenderRazorViewToString("_ReporteAusenciasPorProcesos", objReporteCosto);
                return Json(new { Data = datos, Mensaje = "Success" });
            }
            else
                return Json(new { Data = string.Empty, Mensaje = "Fail" });
        }

        /// <summary>
        /// Obtiene el reporte de Ausentismo por ocuapcion
        /// </summary>
        /// <param name="dataReportes"></param>
        /// <returns></returns>
        public JsonResult ReporteAusentismoOcupacion(ReportesModel dataReportes)
        {
            List<ReportesModel> objReporteCosto = null;
            var objEvaluacion = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (objEvaluacion == null)
            {
                return Json(new { Data = "La sesión a finalizado", Mensaje = "FinSession" });
            }

            EDReportes edReporte = new EDReportes();
            edReporte.anio = dataReportes.anio;
            edReporte.idOrigen = dataReportes.idOrigen;
            edReporte.IdEmpresaUsuaria = dataReportes.IdEmpresaUsuaria;
            edReporte.idSede = dataReportes.idSede;
            edReporte.IdDepartamento = dataReportes.IdDepartamento;
            edReporte.nitEmpresa = objEvaluacion.NitEmpresa;

            var result = lnreportes.ReporteCantidadAusenciasPorOcupacion(edReporte);
            if (result != null && result.Count() > 0)
            {
                objReporteCosto = result.Select(r => new ReportesModel()
                {
                    Descripcion = r.Evento,
                    strEne = ObtenerValorConformato(r.Ene.ToString()),
                    strFeb = ObtenerValorConformato(r.Feb.ToString()),
                    strMar = ObtenerValorConformato(r.Mar.ToString()),
                    strAbr = ObtenerValorConformato(r.Abr.ToString()),
                    strMay = ObtenerValorConformato(r.May.ToString()),
                    strJun = ObtenerValorConformato(r.Jun.ToString()),
                    strJul = ObtenerValorConformato(r.Jul.ToString()),
                    strAgo = ObtenerValorConformato(r.Ago.ToString()),
                    strSep = ObtenerValorConformato(r.Sep.ToString()),
                    strOct = ObtenerValorConformato(r.Oct.ToString()),
                    strNov = ObtenerValorConformato(r.Nov.ToString()),
                    strDic = ObtenerValorConformato(r.Dic.ToString()),
                    strTotal = ObtenerValorConformato(r.Total.ToString())
                }).ToList();
            }
            else
                return Json(new { Data = string.Empty, Mensaje = "Fail" });
            if (objReporteCosto != null)
            {
                var datos = RenderRazorViewToString("_ReporteOcupacion", objReporteCosto);
                return Json(new { Data = datos, Mensaje = "Success" });
            }
            else
                return Json(new { Data = string.Empty, Mensaje = "Fail" });
        }

        /// <summary>
        /// Obtiene el reporte de Asentismo por grupos etarios
        /// </summary>
        /// <param name="dataReportes"></param>
        /// <returns></returns>
        public JsonResult ReporteAusentismoPorGrupoEtarios(ReportesModel dataReportes)
        {
            List<ReportesModel> objReporteCosto = null;
            var objEvaluacion = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (objEvaluacion == null)
            {
                return Json(new { Data = "La sesión a finalizado", Mensaje = "FinSession" });
            }

            EDReportes edReporte = new EDReportes();
            edReporte.anio = dataReportes.anio;
            edReporte.idOrigen = dataReportes.idOrigen;
            edReporte.IdEmpresaUsuaria = dataReportes.IdEmpresaUsuaria;
            edReporte.idSede = dataReportes.idSede;
            edReporte.IdDepartamento = dataReportes.IdDepartamento;
            edReporte.nitEmpresa = objEvaluacion.NitEmpresa;

            var result = lnreportes.ReporteCantidadAusenGrupEtarios(edReporte);
            if (result != null && result.Count() > 0)
            {
                objReporteCosto = result.Select(r => new ReportesModel()
                {
                    Descripcion = r.Evento,
                    strEne = ObtenerValorConformato(r.Ene.ToString()),
                    strFeb = ObtenerValorConformato(r.Feb.ToString()),
                    strMar = ObtenerValorConformato(r.Mar.ToString()),
                    strAbr = ObtenerValorConformato(r.Abr.ToString()),
                    strMay = ObtenerValorConformato(r.May.ToString()),
                    strJun = ObtenerValorConformato(r.Jun.ToString()),
                    strJul = ObtenerValorConformato(r.Jul.ToString()),
                    strAgo = ObtenerValorConformato(r.Ago.ToString()),
                    strSep = ObtenerValorConformato(r.Sep.ToString()),
                    strOct = ObtenerValorConformato(r.Oct.ToString()),
                    strNov = ObtenerValorConformato(r.Nov.ToString()),
                    strDic = ObtenerValorConformato(r.Dic.ToString()),
                    strTotal = ObtenerValorConformato(r.Total.ToString())
                }).ToList();
            }
            else
                return Json(new { Data = string.Empty, Mensaje = "Fail" });
            if (objReporteCosto != null)
            {
                var datos = RenderRazorViewToString("_ReporteAusenciasPorGruposEtarios", objReporteCosto);
                return Json(new { Data = datos, Mensaje = "Success" });
            }
            else
                return Json(new { Data = string.Empty, Mensaje = "Fail" });
        }

        //graficas
        public JsonResult GraficaDiasContingencia(ReportesModel dataReportes)
        {
            List<ReportesModel> graficaContingencia = null;
            var objEvaluacion = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (objEvaluacion == null)
            {
                return Json(new { Data = "La sesión a finalizado", Mensaje = "FinSession" });
            }

            EDReportes edReporte = new EDReportes();
            edReporte.anio = dataReportes.anio;
            edReporte.idOrigen = dataReportes.idOrigen;
            edReporte.IdEmpresaUsuaria = dataReportes.IdEmpresaUsuaria;
            edReporte.idSede = dataReportes.idSede;
            edReporte.IdDepartamento = dataReportes.IdDepartamento;
            edReporte.nitEmpresa = objEvaluacion.NitEmpresa;

            var result = lnreportes.ConsultarPorContingencia(edReporte);
            if (result != null && result.Count() > 0)
            {
                graficaContingencia = result.Select(c => new ReportesModel()
                {
                    CONTINGENCIA = c.CONTINGENCIA,
                    Total = c.Total
                }).ToList();
            }
            else
                return Json(new { Data = string.Empty, Mensaje = "Fail" });
            if (graficaContingencia != null)
            {
                return Json(new { Data = graficaContingencia, Mensaje = "Success" });
            }
            else
                return Json(new { Data = string.Empty, Mensaje = "Fail" });
        }
        public JsonResult GraficaEventos(ReportesModel dataReportes)
        {
            List<ReportesModel> graficaContingencia = null;
            var objEvaluacion = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (objEvaluacion == null)
            {
                return Json(new { Data = "La sesión a finalizado", Mensaje = "FinSession" });
            }

            EDReportes edReporte = new EDReportes();
            edReporte.anio = dataReportes.anio;
            edReporte.idOrigen = dataReportes.idOrigen;
            edReporte.IdEmpresaUsuaria = dataReportes.IdEmpresaUsuaria;
            edReporte.idSede = dataReportes.idSede;
            edReporte.IdDepartamento = dataReportes.IdDepartamento;
            edReporte.nitEmpresa = objEvaluacion.NitEmpresa;

            var result = lnreportes.ConsultarPorEventos(edReporte);
            if (result != null && result.Count() > 0)
            {
                graficaContingencia = result.Select(c => new ReportesModel()
                {
                    Evento = c.CONTINGENCIA,
                    Total = c.Total
                }).ToList();
            }
            else
                return Json(new { Data = string.Empty, Mensaje = "Fail" });
            if (graficaContingencia != null)
            {
                return Json(new { Data = graficaContingencia, Mensaje = "Success" });
            }
            else
                return Json(new { Data = string.Empty, Mensaje = "Fail" });
        }
        public JsonResult GraficaDepartamento(ReportesModel dataReportes)
        {
            List<ReportesModel> graficaDepartamento = null;
            var objEvaluacion = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (objEvaluacion == null)
            {
                return Json(new { Data = "La sesión a finalizado", Mensaje = "FinSession" });
            }

            EDReportes edReporte = new EDReportes();
            edReporte.anio = dataReportes.anio;
            edReporte.idOrigen = dataReportes.idOrigen;
            edReporte.IdEmpresaUsuaria = dataReportes.IdEmpresaUsuaria;
            edReporte.idSede = dataReportes.idSede;
            edReporte.IdDepartamento = dataReportes.IdDepartamento;
            edReporte.nitEmpresa = objEvaluacion.NitEmpresa;

            var result = lnreportes.ConsultarPorDepartamento(edReporte);
            if (result != null && result.Count() > 0)
            {
                graficaDepartamento = result.Select(c => new ReportesModel()
                {
                    Departamento = c.Evento,
                    Total = c.Total
                }).ToList();
            }
            else
                return Json(new { Data = string.Empty, Mensaje = "Fail" });
            if (graficaDepartamento != null)
            {
                return Json(new { Data = graficaDepartamento, Mensaje = "Success" });
            }
            else
                return Json(new { Data = string.Empty, Mensaje = "Fail" });
        }
        public JsonResult GraficaEnfermedades(ReportesModel dataReportes)
        {
            List<ReportesModel> graficaEnfermedades = null;
            var objEvaluacion = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (objEvaluacion == null)
            {
                return Json(new { Data = "La sesión a finalizado", Mensaje = "FinSession" });
            }

            EDReportes edReporte = new EDReportes();
            edReporte.anio = dataReportes.anio;
            edReporte.idOrigen = dataReportes.idOrigen;
            edReporte.IdEmpresaUsuaria = dataReportes.IdEmpresaUsuaria;
            edReporte.idSede = dataReportes.idSede;
            edReporte.IdDepartamento = dataReportes.IdDepartamento;
            edReporte.nitEmpresa = objEvaluacion.NitEmpresa;

            var result = lnreportes.ConsultarPorEnfermedades(edReporte);
            if (result != null && result.Count() > 0)
            {
                graficaEnfermedades = result.Select(c => new ReportesModel()
                {
                    Enfermedades = c.Evento,
                    Total = c.Total
                }).ToList();
            }
            else
                return Json(new { Data = string.Empty, Mensaje = "Fail" });
            if (graficaEnfermedades != null)
            {
                return Json(new { Data = graficaEnfermedades, Mensaje = "Success" });
            }
            else
                return Json(new { Data = string.Empty, Mensaje = "Fail" });
        }
        public JsonResult GraficaSede(ReportesModel dataReportes)
        {
            List<ReportesModel> graficaSede = null;
            var objEvaluacion = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (objEvaluacion == null)
            {
                return Json(new { Data = "La sesión a finalizado", Mensaje = "FinSession" });
            }

            EDReportes edReporte = new EDReportes();
            edReporte.anio = dataReportes.anio;
            edReporte.idOrigen = dataReportes.idOrigen;
            edReporte.IdEmpresaUsuaria = dataReportes.IdEmpresaUsuaria;
            edReporte.idSede = dataReportes.idSede;
            edReporte.IdDepartamento = dataReportes.IdDepartamento;
            edReporte.nitEmpresa = objEvaluacion.NitEmpresa;

            var result = lnreportes.ConsultarPorSede(edReporte);
            if (result != null && result.Count() > 0)
            {
                graficaSede = result.Select(c => new ReportesModel()
                {
                    Sede = c.Evento,
                    Total = c.Total
                }).ToList();
            }
            else
                return Json(new { Data = string.Empty, Mensaje = "Fail" });
            if (graficaSede != null)
            {
                return Json(new { Data = graficaSede, Mensaje = "Success" });
            }
            else
                return Json(new { Data = string.Empty, Mensaje = "Fail" });
        }
        //Costo
        public JsonResult GraficaCosto(ReportesModel dataReportes)
        {
            List<ReportesModel> graficaCosto = null;
            var objEvaluacion = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (objEvaluacion == null)
            {
                return Json(new { Data = "La sesión a finalizado", Mensaje = "FinSession" });
            }

            EDReportes edReporte = new EDReportes();
            edReporte.anio = dataReportes.anio;
            edReporte.idOrigen = dataReportes.idOrigen;
            edReporte.IdEmpresaUsuaria = dataReportes.IdEmpresaUsuaria;
            edReporte.idSede = dataReportes.idSede;
            edReporte.IdDepartamento = dataReportes.IdDepartamento;
            edReporte.nitEmpresa = objEvaluacion.NitEmpresa;

            var result = lnreportes.ConsultarPorCosto(edReporte);
            if (result != null && result.Count() > 0)
            {
                graficaCosto = result.Select(c => new ReportesModel()
                {
                    Descripcion = c.CONTINGENCIA,
                    Total = c.Total
                }).ToList();
            }
            else
                return Json(new { Data = string.Empty, Mensaje = "Fail" });
            if (graficaCosto != null )
            {
                return Json(new { Data = graficaCosto, Mensaje = "Success" });
            }
            else
                return Json(new { Data = string.Empty, Mensaje = "Fail" });
        }
        //EPS
        public JsonResult GraficaEps(ReportesModel dataReportes)
        {
            List<ReportesModel> graficaEps = null;
            var objEvaluacion = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (objEvaluacion == null)
            {
                return Json(new { Data = "La sesión a finalizado", Mensaje = "FinSession" });
            }

            EDReportes edReporte = new EDReportes();
            edReporte.anio = dataReportes.anio;
            edReporte.idOrigen = dataReportes.idOrigen;
            edReporte.IdEmpresaUsuaria = dataReportes.IdEmpresaUsuaria;
            edReporte.idSede = dataReportes.idSede;
            edReporte.IdDepartamento = dataReportes.IdDepartamento;
            edReporte.nitEmpresa = objEvaluacion.NitEmpresa;

            var result = lnreportes.ConsultarPorEps(edReporte);
            if (result != null && result.Count() > 0)
            {
                graficaEps = result.Select(c => new ReportesModel()
                {
                    Eps = c.Evento,
                    Total = c.Total
                }).ToList();
            }
            else
                return Json(new { Data = string.Empty, Mensaje = "Fail" });
            if (graficaEps != null)
            {
                return Json(new { Data = graficaEps, Mensaje = "Success" });
            }
            else
                return Json(new { Data = string.Empty, Mensaje = "Fail" });
        }
        //Sexo
        public JsonResult GraficaSexo(ReportesModel dataReportes)
        {
            List<ReportesModel> graficaSexo = null;
            var objEvaluacion = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (objEvaluacion == null)
            {
                return Json(new { Data = "La sesión a finalizado", Mensaje = "FinSession" });
            }

            EDReportes edReporte = new EDReportes();
            edReporte.anio = dataReportes.anio;
            edReporte.idOrigen = dataReportes.idOrigen;
            edReporte.IdEmpresaUsuaria = dataReportes.IdEmpresaUsuaria;
            edReporte.idSede = dataReportes.idSede;
            edReporte.IdDepartamento = dataReportes.IdDepartamento;
            edReporte.nitEmpresa = objEvaluacion.NitEmpresa;

            var result = lnreportes.ConsultarPorSexo(edReporte);
            if (result != null && result.Count() > 0)
            {
                graficaSexo = result.Select(c => new ReportesModel()
                {
                    Sexo = c.Evento,
                    Total = c.Total
                }).ToList();
            }
            else
                return Json(new { Data = string.Empty, Mensaje = "Fail" });
            if (graficaSexo != null)
            {
                return Json(new { Data = graficaSexo, Mensaje = "Success" });
            }
            else
                return Json(new { Data = string.Empty, Mensaje = "Fail" });
        }
        //Ocupacion
        public JsonResult GraficaAusenciasPorVinculacion(ReportesModel dataReportes)
        {
            List<ReportesModel> graficaOcupacion = null;
            var objEvaluacion = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (objEvaluacion == null)
            {
                return Json(new { Data = "La sesión a finalizado", Mensaje = "FinSession" });
            }

            EDReportes edReporte = new EDReportes();
            edReporte.anio = dataReportes.anio;
            edReporte.idOrigen = dataReportes.idOrigen;
            edReporte.IdEmpresaUsuaria = dataReportes.IdEmpresaUsuaria;
            edReporte.idSede = dataReportes.idSede;
            edReporte.IdDepartamento = dataReportes.IdDepartamento;
            edReporte.nitEmpresa = objEvaluacion.NitEmpresa;

            var result = lnreportes.ConsultarAusenciasPorVinculacion(edReporte);
            if (result != null && result.Count() > 0)
            {
                graficaOcupacion = result.Select(c => new ReportesModel()
                {
                    Ocupacion = c.Evento,
                    Total = c.Total
                }).ToList();
            }
            else
                return Json(new { Data = string.Empty, Mensaje = "Fail" });
            if (graficaOcupacion != null)
            {
                return Json(new { Data = graficaOcupacion, Mensaje = "Success" });
            }
            else
                return Json(new { Data = string.Empty, Mensaje = "Fail" });
        }

        /// <summary>
        /// Grafica del reporte Numero de eventos por capitulo de enfermedad
        /// </summary>
        /// <param name="dataReportes"></param>
        /// <returns></returns>
        public JsonResult GraficaNumEventosPorEnfermedad(ReportesModel dataReportes)
        {
            List<ReportesModel> graficaEventos = null;
            var objEvaluacion = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (objEvaluacion == null)
            {
                return Json(new { Data = "La sesión a finalizado", Mensaje = "FinSession" });
            }

            EDReportes edReporte = new EDReportes();
            edReporte.anio = dataReportes.anio;
            edReporte.idOrigen = dataReportes.idOrigen;
            edReporte.IdEmpresaUsuaria = dataReportes.IdEmpresaUsuaria;
            edReporte.idSede = dataReportes.idSede;
            edReporte.IdDepartamento = dataReportes.IdDepartamento;
            edReporte.nitEmpresa = objEvaluacion.NitEmpresa;

            var result = lnreportes.ReporteCantiEventPorEnfermedades(edReporte);
            if (result != null && result.Count() > 0)
            {
                graficaEventos = result.Select(c => new ReportesModel()
                {
                    Evento = c.Evento,
                    Total = c.Total
                }).ToList();
            }
            else
                return Json(new { Data = string.Empty, Mensaje = "Fail" });
            if (graficaEventos != null)
            {
                return Json(new { Data = graficaEventos, Mensaje = "Success" });
            }
            else
                return Json(new { Data = string.Empty, Mensaje = "Fail" });
        }

        /// <summary>
        /// Grafica del reporte dias de ausentismo por proceso
        /// </summary>
        /// <param name="dataReportes"></param>
        /// <returns></returns>
        public JsonResult GraficaDiasAusentismoProceso(ReportesModel dataReportes)
        {
            List<ReportesModel> grafica = null;
            var objEvaluacion = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (objEvaluacion == null)
            {
                return Json(new { Data = "La sesión a finalizado", Mensaje = "FinSession" });
            }

            EDReportes edReporte = new EDReportes();
            edReporte.anio = dataReportes.anio;
            edReporte.idOrigen = dataReportes.idOrigen;
            edReporte.IdEmpresaUsuaria = dataReportes.IdEmpresaUsuaria;
            edReporte.idSede = dataReportes.idSede;
            edReporte.IdDepartamento = dataReportes.IdDepartamento;
            edReporte.nitEmpresa = objEvaluacion.NitEmpresa;

            var result = lnreportes.ReporteDiasAusentismoPorProceso(edReporte);
            if (result != null && result.Count() > 0)
            {
                grafica = result.Select(c => new ReportesModel()
                {
                    Evento = c.Evento,
                    Total = c.Total
                }).ToList();
            }
            else
                return Json(new { Data = string.Empty, Mensaje = "Fail" });
            if (grafica != null)
            {
                return Json(new { Data = grafica, Mensaje = "Success" });
            }
            else
                return Json(new { Data = string.Empty, Mensaje = "Fail" });
        }

        /// <summary>
        /// Grafica de ausentismo por cupacion
        /// </summary>
        /// <param name="dataReportes"></param>
        /// <returns></returns>
        public JsonResult GraficaAusentismoOcupacion(ReportesModel dataReportes)
        {
            List<ReportesModel> grafica = null;
            var objEvaluacion = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (objEvaluacion == null)
            {
                return Json(new { Data = "La sesión a finalizado", Mensaje = "FinSession" });
            }

            EDReportes edReporte = new EDReportes();
            edReporte.anio = dataReportes.anio;
            edReporte.idOrigen = dataReportes.idOrigen;
            edReporte.IdEmpresaUsuaria = dataReportes.IdEmpresaUsuaria;
            edReporte.idSede = dataReportes.idSede;
            edReporte.IdDepartamento = dataReportes.IdDepartamento;
            edReporte.nitEmpresa = objEvaluacion.NitEmpresa;

            var result = lnreportes.ReporteCantidadAusenciasPorOcupacion(edReporte);
            if (result != null && result.Count() > 0)
            {
                grafica = result.Select(c => new ReportesModel()
                {
                    Ocupacion = c.Evento,
                    Total = c.Total
                }).ToList();
            }
            else
                return Json(new { Data = string.Empty, Mensaje = "Fail" });
            if (grafica != null)
            {
                return Json(new { Data = grafica, Mensaje = "Success" });
            }
            else
                return Json(new { Data = string.Empty, Mensaje = "Fail" });
        }

        /// <summary>
        /// Grafica de ausentismo por grupos estarios
        /// </summary>
        /// <param name="dataReportes"></param>
        /// <returns></returns>
        public JsonResult GraficaAusentismoPorGrupoEtarios(ReportesModel dataReportes)
        {
            List<ReportesModel> grafica = null;
            var objEvaluacion = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (objEvaluacion == null)
            {
                return Json(new { Data = "La sesión a finalizado", Mensaje = "FinSession" });
            }

            EDReportes edReporte = new EDReportes();
            edReporte.anio = dataReportes.anio;
            edReporte.idOrigen = dataReportes.idOrigen;
            edReporte.IdEmpresaUsuaria = dataReportes.IdEmpresaUsuaria;
            edReporte.idSede = dataReportes.idSede;
            edReporte.IdDepartamento = dataReportes.IdDepartamento;
            edReporte.nitEmpresa = objEvaluacion.NitEmpresa;

            var result = lnreportes.ReporteCantidadAusenGrupEtarios(edReporte);
            if (result != null && result.Count() > 0)
            {
                grafica = result.Select(c => new ReportesModel()
                {
                    Evento = c.Evento,
                    Total = c.Total
                }).ToList();
            }
            else
                return Json(new { Data = string.Empty, Mensaje = "Fail" });
            if (grafica != null)
            {
                return Json(new { Data = grafica, Mensaje = "Success" });
            }
            else
                return Json(new { Data = string.Empty, Mensaje = "Fail" });
        }
        
        public JsonResult ObtieneRporteExcelDescargar(ReportesModel dataReportes)
        {
            TempData["reportes"] = dataReportes;
            if(dataReportes.Reporte.Equals("AC"))
                return Json(new { Data = "/DescargarContingenciaExcel", Mensaje = "Success" });
            if (dataReportes.Reporte.Equals("NC"))
                return Json(new { Data = "/DescargarEventoExcel", Mensaje = "Success" });
            if (dataReportes.Reporte.Equals("ADP"))
                return Json(new { Data = "/DescargarDepartamentoExcel", Mensaje = "Success" });
            if (dataReportes.Reporte.Equals("DCIE"))
                return Json(new { Data = "/DescargarEnfermedadesExcel", Mensaje = "Success" });
            if (dataReportes.Reporte.Equals("NCIE"))
                return Json(new { Data = "/DescargarEnfermedadesEvtExcel", Mensaje = "Success" });
            if (dataReportes.Reporte.Equals("DP"))
                return Json(new { Data = "/DescargarProcesoExcel", Mensaje = "Success" });
            if (dataReportes.Reporte.Equals("DS"))
                return Json(new { Data = "/DescargarSedeExcel", Mensaje = "Success" });
            if (dataReportes.Reporte.Equals("PC"))
                return Json(new { Data = "/DescargarPromedioContigenciasExcel", Mensaje = "Success" });
            if (dataReportes.Reporte.Equals("AEPS"))
                return Json(new { Data = "/DescargarEPSExcel", Mensaje = "Success" });
            if (dataReportes.Reporte.Equals("ASX"))
                return Json(new { Data = "/DescargarSexoExcel", Mensaje = "Success" });
            if (dataReportes.Reporte.Equals("AV"))
                return Json(new { Data = "/DescargarVinculacionExcel", Mensaje = "Success" });
            if (dataReportes.Reporte.Equals("AO"))
                return Json(new { Data = "/DescargarOcupacionExcel", Mensaje = "Success" });
            if (dataReportes.Reporte.Equals("AET"))
                return Json(new { Data = "/DescargarGruposEtariosExcel", Mensaje = "Success" });
            else                 
                return Json(new { Data = "Debe elegir el informe a descargar.", Mensaje = "Fail" });

        }

        /// <summary>
        /// descar el excel del informe Días Ausentismo por contingencia
        /// </summary>
        /// <returns></returns>
        public ActionResult DescargarContingenciaExcel()
        {
            var objEvaluacion = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (objEvaluacion == null)
            {
                ViewBag.Mensaje = "La sesión a finalizado.";
                return RedirectToAction("Login", "Home");
            }
            EDReportes edReporte = new EDReportes();
            ReportesModel dataReportes = new ReportesModel();
            if (TempData["reportes"] != null)
            {
                dataReportes = (ReportesModel)TempData["reportes"];
            }                         

            edReporte.anio = dataReportes.anio;
            edReporte.idOrigen = dataReportes.idOrigen;
            edReporte.IdEmpresaUsuaria = dataReportes.IdEmpresaUsuaria;
            edReporte.idSede = dataReportes.idSede;
            edReporte.IdDepartamento = dataReportes.IdDepartamento;
            edReporte.nitEmpresa = objEvaluacion.NitEmpresa;

            //se consume el servicio post para guardar la información de la evaluación inicial
            ServiceClient.EliminarParametros();
            var result = ServiceClient.ObtenerObjetoJsonRestFul<byte[]>(UrlServicioPlanificacion, Capacidaddescargarexcelcontigencias, edReporte);
            return File(result, "application/vnd.ms-excel", "Días Ausentismo contingencia.xlsx");
        }
        
        /// <summary>
        /// descar el excel del informe: Número de eventos por contingencia
        /// </summary>
        /// <returns></returns>
        public ActionResult DescargarEventoExcel()
        {
            var objEvaluacion = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (objEvaluacion == null)
            {
                ViewBag.Mensaje = "La sesión a finalizado.";
                return RedirectToAction("Login", "Home");
            }
            EDReportes edReporte = new EDReportes();
            ReportesModel dataReportes = new ReportesModel();
            if (TempData["reportes"] != null)
            {
                dataReportes = (ReportesModel)TempData["reportes"];
            }

            edReporte.anio = dataReportes.anio;
            edReporte.idOrigen = dataReportes.idOrigen;
            edReporte.IdEmpresaUsuaria = dataReportes.IdEmpresaUsuaria;
            edReporte.idSede = dataReportes.idSede;
            edReporte.IdDepartamento = dataReportes.IdDepartamento;
            edReporte.nitEmpresa = objEvaluacion.NitEmpresa;

            ServiceClient.EliminarParametros();            
            //se consume el servicio post para guardar la información de la evaluación inicial
            var result = ServiceClient.ObtenerObjetoJsonRestFul<byte[]>(UrlServicioPlanificacion, Capacidaddescargarexcelevento, edReporte);
            return File(result, "application/vnd.ms-excel", "Número eventos contingencia.xlsx");
        }

        /// <summary>
        /// descar el excel del informe: Dias ausentismo por Departamentos
        /// </summary>
        /// <returns></returns>
        public ActionResult DescargarDepartamentoExcel()
        {
            var objEvaluacion = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (objEvaluacion == null)
            {
                ViewBag.Mensaje = "La sesión a finalizado.";
                return RedirectToAction("Login", "Home");
            }
            EDReportes edReporte = new EDReportes();
            ReportesModel dataReportes = new ReportesModel();
            if (TempData["reportes"] != null)
            {
                dataReportes = (ReportesModel)TempData["reportes"];
            }

            edReporte.anio = dataReportes.anio;
            edReporte.idOrigen = dataReportes.idOrigen;
            edReporte.IdEmpresaUsuaria = dataReportes.IdEmpresaUsuaria;
            edReporte.idSede = dataReportes.idSede;
            edReporte.IdDepartamento = dataReportes.IdDepartamento;
            edReporte.nitEmpresa = objEvaluacion.NitEmpresa;

            ServiceClient.EliminarParametros();
            //se consume el servicio post para guardar la información de la evaluación inicial
            var result = ServiceClient.ObtenerObjetoJsonRestFul<byte[]>(UrlServicioPlanificacion, Capacidaddescargarexceldepartamento, edReporte);
            return File(result, "application/vnd.ms-excel", "Dias ausentismo Departamentos.xlsx");
        }

        /// <summary>
        /// descar el excel del informe: Dias ausentismo por capitulos de enfermedades CIE10
        /// </summary>
        /// <returns></returns>
        public ActionResult DescargarEnfermedadesExcel()
        {
            var objEvaluacion = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (objEvaluacion == null)
            {
                ViewBag.Mensaje = "La sesión a finalizado.";
                return RedirectToAction("Login", "Home");
            }
            EDReportes edReporte = new EDReportes();
            ReportesModel dataReportes = new ReportesModel();
            if (TempData["reportes"] != null)
            {
                dataReportes = (ReportesModel)TempData["reportes"];
            }

            edReporte.anio = dataReportes.anio;
            edReporte.idOrigen = dataReportes.idOrigen;
            edReporte.IdEmpresaUsuaria = dataReportes.IdEmpresaUsuaria;
            edReporte.idSede = dataReportes.idSede;
            edReporte.IdDepartamento = dataReportes.IdDepartamento;
            edReporte.nitEmpresa = objEvaluacion.NitEmpresa;

            ServiceClient.EliminarParametros();
            //se consume el servicio post para guardar la información de la evaluación inicial
            var result = ServiceClient.ObtenerObjetoJsonRestFul<byte[]>(UrlServicioPlanificacion, Capacidaddescargarexcelenfermedades, edReporte);
            return File(result, "application/vnd.ms-excel", "Dias ausentismo enfermedades CIE10.xlsx");
        }

        /// <summary>
        /// descar el excel del informe: Número de eventos por capitulos de enfermedades CIE10
        /// </summary>
        /// <returns></returns>
        public ActionResult DescargarEnfermedadesEvtExcel()
        {
            var objEvaluacion = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (objEvaluacion == null)
            {
                ViewBag.Mensaje = "La sesión a finalizado.";
                return RedirectToAction("Login", "Home");
            }
            EDReportes edReporte = new EDReportes();
            ReportesModel dataReportes = new ReportesModel();
            if (TempData["reportes"] != null)
            {
                dataReportes = (ReportesModel)TempData["reportes"];
            }

            edReporte.anio = dataReportes.anio;
            edReporte.idOrigen = dataReportes.idOrigen;
            edReporte.IdEmpresaUsuaria = dataReportes.IdEmpresaUsuaria;
            edReporte.idSede = dataReportes.idSede;
            edReporte.IdDepartamento = dataReportes.IdDepartamento;
            edReporte.nitEmpresa = objEvaluacion.NitEmpresa;

            ServiceClient.EliminarParametros();
            //se consume el servicio post para guardar la información de la evaluación inicial
            var result = ServiceClient.ObtenerObjetoJsonRestFul<byte[]>(UrlServicioPlanificacion, Capacidaddescargarexcelenfermedadeseven, edReporte);
            return File(result, "application/vnd.ms-excel", "Número eventos enfermedades CIE10.xlsx");
        }

        /// <summary>
        /// descar el excel del informe: Dias de ausentismo por proceso
        /// </summary>
        /// <returns></returns>
        public ActionResult DescargarProcesoExcel()
        {
            var objEvaluacion = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (objEvaluacion == null)
            {
                ViewBag.Mensaje = "La sesión a finalizado.";
                return RedirectToAction("Login", "Home");
            }
            EDReportes edReporte = new EDReportes();
            ReportesModel dataReportes = new ReportesModel();
            if (TempData["reportes"] != null)
            {
                dataReportes = (ReportesModel)TempData["reportes"];
            }

            edReporte.anio = dataReportes.anio;
            edReporte.idOrigen = dataReportes.idOrigen;
            edReporte.IdEmpresaUsuaria = dataReportes.IdEmpresaUsuaria;
            edReporte.idSede = dataReportes.idSede;
            edReporte.IdDepartamento = dataReportes.IdDepartamento;
            edReporte.nitEmpresa = objEvaluacion.NitEmpresa;

            ServiceClient.EliminarParametros();
            //se consume el servicio post para guardar la información de la evaluación inicial
            var result = ServiceClient.ObtenerObjetoJsonRestFul<byte[]>(UrlServicioPlanificacion, Capacidaddescargarexcelproceso, edReporte);
            return File(result, "application/vnd.ms-excel", "Dias ausentismo proceso.xlsx");
        }

        /// <summary>
        /// descar el excel del informe: Dias de ausentismo por Sede
        /// </summary>
        /// <returns></returns>
        public ActionResult DescargarSedeExcel()
        {
            var objEvaluacion = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (objEvaluacion == null)
            {
                ViewBag.Mensaje = "La sesión a finalizado.";
                return RedirectToAction("Login", "Home");
            }
            EDReportes edReporte = new EDReportes();
            ReportesModel dataReportes = new ReportesModel();
            if (TempData["reportes"] != null)
            {
                dataReportes = (ReportesModel)TempData["reportes"];
            }

            edReporte.anio = dataReportes.anio;
            edReporte.idOrigen = dataReportes.idOrigen;
            edReporte.IdEmpresaUsuaria = dataReportes.IdEmpresaUsuaria;
            edReporte.idSede = dataReportes.idSede;
            edReporte.IdDepartamento = dataReportes.IdDepartamento;
            edReporte.nitEmpresa = objEvaluacion.NitEmpresa;

            ServiceClient.EliminarParametros();
            //se consume el servicio post para guardar la información de la evaluación inicial
            var result = ServiceClient.ObtenerObjetoJsonRestFul<byte[]>(UrlServicioPlanificacion, Capacidaddescargarexcelsede, edReporte);
            return File(result, "application/vnd.ms-excel", "Dias ausentismo Sede.xlsx");
        }

        /// <summary>
        /// descar el excel del informe: Promedio de Costos por contingencias
        /// </summary>
        /// <returns></returns>
        public ActionResult DescargarPromedioContigenciasExcel()
        {
            var objEvaluacion = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (objEvaluacion == null)
            {
                ViewBag.Mensaje = "La sesión a finalizado.";
                return RedirectToAction("Login", "Home");
            }
            EDReportes edReporte = new EDReportes();
            ReportesModel dataReportes = new ReportesModel();
            if (TempData["reportes"] != null)
            {
                dataReportes = (ReportesModel)TempData["reportes"];
            }

            edReporte.anio = dataReportes.anio;
            edReporte.idOrigen = dataReportes.idOrigen;
            edReporte.IdEmpresaUsuaria = dataReportes.IdEmpresaUsuaria;
            edReporte.idSede = dataReportes.idSede;
            edReporte.IdDepartamento = dataReportes.IdDepartamento;
            edReporte.nitEmpresa = objEvaluacion.NitEmpresa;

            ServiceClient.EliminarParametros();
            //se consume el servicio post para guardar la información de la evaluación inicial
            var result = ServiceClient.ObtenerObjetoJsonRestFul<byte[]>(UrlServicioPlanificacion, Capacidaddescargarexcelpromediocontingencias, edReporte);
            return File(result, "application/vnd.ms-excel", "Promedio Costos contingencias.xlsx");
        }

        /// <summary>
        /// descar el excel del informe: Ausentismos por EPS
        /// </summary>
        /// <returns></returns>
        public ActionResult DescargarEPSExcel()
        {
            var objEvaluacion = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (objEvaluacion == null)
            {
                ViewBag.Mensaje = "La sesión a finalizado.";
                return RedirectToAction("Login", "Home");
            }
            EDReportes edReporte = new EDReportes();
            ReportesModel dataReportes = new ReportesModel();
            if (TempData["reportes"] != null)
            {
                dataReportes = (ReportesModel)TempData["reportes"];
            }

            edReporte.anio = dataReportes.anio;
            edReporte.idOrigen = dataReportes.idOrigen;
            edReporte.IdEmpresaUsuaria = dataReportes.IdEmpresaUsuaria;
            edReporte.idSede = dataReportes.idSede;
            edReporte.IdDepartamento = dataReportes.IdDepartamento;
            edReporte.nitEmpresa = objEvaluacion.NitEmpresa;

            ServiceClient.EliminarParametros();
            //se consume el servicio post para guardar la información de la evaluación inicial
            var result = ServiceClient.ObtenerObjetoJsonRestFul<byte[]>(UrlServicioPlanificacion, Capacidaddescargarexceleps, edReporte);
            return File(result, "application/vnd.ms-excel", "Ausentismos por EPS.xlsx");
        }

        /// <summary>
        /// descar el excel del informe: Ausentismos por sexo
        /// </summary>
        /// <returns></returns>
        public ActionResult DescargarSexoExcel()
        {
            var objEvaluacion = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (objEvaluacion == null)
            {
                ViewBag.Mensaje = "La sesión a finalizado.";
                return RedirectToAction("Login", "Home");
            }
            EDReportes edReporte = new EDReportes();
            ReportesModel dataReportes = new ReportesModel();
            if (TempData["reportes"] != null)
            {
                dataReportes = (ReportesModel)TempData["reportes"];
            }

            edReporte.anio = dataReportes.anio;
            edReporte.idOrigen = dataReportes.idOrigen;
            edReporte.IdEmpresaUsuaria = dataReportes.IdEmpresaUsuaria;
            edReporte.idSede = dataReportes.idSede;
            edReporte.IdDepartamento = dataReportes.IdDepartamento;
            edReporte.nitEmpresa = objEvaluacion.NitEmpresa;

            ServiceClient.EliminarParametros();
            //se consume el servicio post para guardar la información de la evaluación inicial
            var result = ServiceClient.ObtenerObjetoJsonRestFul<byte[]>(UrlServicioPlanificacion, Capacidaddescargarexcelsexo, edReporte);
            return File(result, "application/vnd.ms-excel", "Ausentismos por sexo.xlsx");
        }

        /// <summary>
        /// descar el excel del informe: Ausentismos por tipo vinculacion
        /// </summary>
        /// <returns></returns>
        public ActionResult DescargarVinculacionExcel()
        {
            var objEvaluacion = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (objEvaluacion == null)
            {
                ViewBag.Mensaje = "La sesión a finalizado.";
                return RedirectToAction("Login", "Home");
            }
            EDReportes edReporte = new EDReportes();
            ReportesModel dataReportes = new ReportesModel();
            if (TempData["reportes"] != null)
            {
                dataReportes = (ReportesModel)TempData["reportes"];
            }

            edReporte.anio = dataReportes.anio;
            edReporte.idOrigen = dataReportes.idOrigen;
            edReporte.IdEmpresaUsuaria = dataReportes.IdEmpresaUsuaria;
            edReporte.idSede = dataReportes.idSede;
            edReporte.IdDepartamento = dataReportes.IdDepartamento;
            edReporte.nitEmpresa = objEvaluacion.NitEmpresa;

            ServiceClient.EliminarParametros();
            //se consume el servicio post para guardar la información de la evaluación inicial
            var result = ServiceClient.ObtenerObjetoJsonRestFul<byte[]>(UrlServicioPlanificacion, Capacidaddescargarexcelvinculacion, edReporte);
            return File(result, "application/vnd.ms-excel", "Ausentismos tipo vinculacion.xlsx");
        }

        /// <summary>
        /// descar el excel del informe: Ausentismos por ocupacion CIUO
        /// </summary>
        /// <returns></returns>
        public ActionResult DescargarOcupacionExcel()
        {
            var objEvaluacion = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (objEvaluacion == null)
            {
                ViewBag.Mensaje = "La sesión a finalizado.";
                return RedirectToAction("Login", "Home");
            }
            EDReportes edReporte = new EDReportes();
            ReportesModel dataReportes = new ReportesModel();
            if (TempData["reportes"] != null)
            {
                dataReportes = (ReportesModel)TempData["reportes"];
            }

            edReporte.anio = dataReportes.anio;
            edReporte.idOrigen = dataReportes.idOrigen;
            edReporte.IdEmpresaUsuaria = dataReportes.IdEmpresaUsuaria;
            edReporte.idSede = dataReportes.idSede;
            edReporte.IdDepartamento = dataReportes.IdDepartamento;
            edReporte.nitEmpresa = objEvaluacion.NitEmpresa;

            ServiceClient.EliminarParametros();
            //se consume el servicio post para guardar la información de la evaluación inicial
            var result = ServiceClient.ObtenerObjetoJsonRestFul<byte[]>(UrlServicioPlanificacion, Capacidaddescargarexcelocupacion, edReporte);
            return File(result, "application/vnd.ms-excel", "Ausentismos ocupacion CIUO.xlsx");
        }

        /// <summary>
        /// descar el excel del informe: Ausentismos por Grupos Etarios
        /// </summary>
        /// <returns></returns>
        public ActionResult DescargarGruposEtariosExcel()
        {
            var objEvaluacion = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (objEvaluacion == null)
            {
                ViewBag.Mensaje = "La sesión a finalizado.";
                return RedirectToAction("Login", "Home");
            }
            EDReportes edReporte = new EDReportes();
            ReportesModel dataReportes = new ReportesModel();
            if (TempData["reportes"] != null)
            {
                dataReportes = (ReportesModel)TempData["reportes"];
            }

            edReporte.anio = dataReportes.anio;
            edReporte.idOrigen = dataReportes.idOrigen;
            edReporte.IdEmpresaUsuaria = dataReportes.IdEmpresaUsuaria;
            edReporte.idSede = dataReportes.idSede;
            edReporte.IdDepartamento = dataReportes.IdDepartamento;
            edReporte.nitEmpresa = objEvaluacion.NitEmpresa;

            ServiceClient.EliminarParametros();
            //se consume el servicio post para guardar la información de la evaluación inicial
            var result = ServiceClient.ObtenerObjetoJsonRestFul<byte[]>(UrlServicioPlanificacion, Capacidaddescargarexcelgrupoetarios, edReporte);
            return File(result, "application/vnd.ms-excel", "Ausentismos Grupos Etarios.xlsx");
        }
    }
}
