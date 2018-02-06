using SG_SST.Controllers.Base;
using SG_SST.Models.Ausentismo;
using SG_SST.Logica.Ausentismos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SG_SST.ServiceRequest;
using SG_SST.EntidadesDominio.Empresas;
using System.Configuration;

namespace SG_SST.Controllers.Ausentismo
{
    public class IndicadoresController : BaseController
    {
        LNIndicadores lnindicadores = new LNIndicadores();
        string urlServicioEmpresas = ConfigurationManager.AppSettings["UrlServicioEmpresas"];
        string CapacidadObtenerEmpresasusuarias = ConfigurationManager.AppSettings["CapacidadObtenerEmpresasusuarias"];
        string UrlServicioPlanificacion = ConfigurationManager.AppSettings["UrlServicioPlanificacion"];
        string CapacidaddescargarexcelInidicadores = ConfigurationManager.AppSettings["CapacidaddescargarexcelInidicadores"];
        string CapacidaddescargarexcelAcumulado = ConfigurationManager.AppSettings["CapacidaddescargarexcelAcumulado"];
        string CapacidaddescargarexcelComparativo = ConfigurationManager.AppSettings["CapacidaddescargarexcelComparativo"];
        string CapacidadObtenerAnoInicioEmpresa = ConfigurationManager.AppSettings["CapacidadObtenerAnoInicioEmpresa"];

        // GET: Indicadores
        public ActionResult Indicadores()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "Debe estar autenticado para visualizar los Indicadores.";
                return View();
            }
            var objIndicadores = new IndicadoresModel();
            objIndicadores.IdEmpresa = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current) == null ? string.Empty : ObtenerUsuarioEnSesion(System.Web.HttpContext.Current).NitEmpresa;
            objIndicadores.RazonSocial = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current) == null ? string.Empty : ObtenerUsuarioEnSesion(System.Web.HttpContext.Current).RazonSocialEmpresa;
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("NIT", usuarioActual.NitEmpresa);
            var result = ServiceClient.ObtenerArrayJsonRestFul<EDEmpresa_Usuaria>(urlServicioEmpresas, CapacidadObtenerEmpresasusuarias, RestSharp.Method.GET);
            if (result != null && result.Count() > 0)
            {
                objIndicadores.EmpresasUsuarias = result.Select(c => new SelectListItem()
                {
                    Value = c.IdEmpresaUsuaria.ToString(),
                    Text = c.RazonSocial
                }).ToList();
            }
            else
                objIndicadores.EmpresasUsuarias = new List<SelectListItem>();

            objIndicadores.Constante = objIndicadores.Configurconstante();
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("NIT", usuarioActual.NitEmpresa);
            var resultAno = ServiceClient.ObtenerObjetoJsonRestFul<int>(UrlServicioPlanificacion, CapacidadObtenerAnoInicioEmpresa, RestSharp.Method.GET);
            if (resultAno > 0)
            {
                objIndicadores.Anios = GetAnios(resultAno);
            }
            else
                objIndicadores.Anios = GetAnios(2010);

            return View(objIndicadores);
        }

        /// <summary>
        /// Renderiza los valores de las variables IF, IS, e ILI 
        /// para ser mostrados en pantalla.
        /// </summary>
        /// <param name="indicadores"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult IndicadorIF(IndicadoresModel indicadores)
        {
            var objEvaluacion = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            List<IndicadoresModel> objIndicadores = null;
            if (ModelState.IsValid)
            {
                int idEmpresaUsuaria = int.Parse(string.IsNullOrEmpty(indicadores.IdEmpresaUsuaria) ? "0" : indicadores.IdEmpresaUsuaria);
                int respuesta = 0;
                var result = lnindicadores.CalcularIndicadoresPorPeriodo(indicadores.AnioSeleccionado, indicadores.ConstanteSeleccionada, out respuesta, objEvaluacion.NitEmpresa, idEmpresaUsuaria, indicadores.IdContingencia);
                if (respuesta == 1)
                    return Json(new { Data = "No se encontraron registros de Ausencias para generar generar los indicadores.", Mensaje = "NOTFOUND" });
                else if (respuesta == 2)
                    return Json(new { Data = "No se ha realizado la configuración HHT necesaria para el cálculo de las variables.", Mensaje = "NOTFOUND" });
                else if (respuesta == 0 && result != null && result.Count() > 0)
                {
                    objIndicadores = result.Select(r => new IndicadoresModel()
                    {
                        AnioSeleccionado = indicadores.AnioSeleccionado,
                        IdContingencia = r.Idcontingencia,
                        Contingencia = r.Contingencia,
                        Ene = r.Ene == null ? new VariablesIndicadoresModel() { VariableIF = 0, VariableIS = 0, VaribleILI = 0, Tasa = 0 } : new VariablesIndicadoresModel() { VariableIF = r.Ene.VariableIF, VariableIS = r.Ene.VariableIS, VaribleILI = r.Ene.VariableILI, Tasa = r.Ene.Tasa },
                        Feb = r.Feb == null ? new VariablesIndicadoresModel() { VariableIF = 0, VariableIS = 0, VaribleILI = 0, Tasa = 0 } : new VariablesIndicadoresModel() { VariableIF = r.Feb.VariableIF, VariableIS = r.Feb.VariableIS, VaribleILI = r.Feb.VariableILI, Tasa = r.Feb.Tasa },
                        Mar = r.Mar == null ? new VariablesIndicadoresModel() { VariableIF = 0, VariableIS = 0, VaribleILI = 0, Tasa = 0 } : new VariablesIndicadoresModel() { VariableIF = r.Mar.VariableIF, VariableIS = r.Mar.VariableIS, VaribleILI = r.Mar.VariableILI, Tasa = r.Mar.Tasa },
                        Abr = r.Abr == null ? new VariablesIndicadoresModel() { VariableIF = 0, VariableIS = 0, VaribleILI = 0, Tasa = 0 } : new VariablesIndicadoresModel() { VariableIF = r.Abr.VariableIF, VariableIS = r.Abr.VariableIS, VaribleILI = r.Abr.VariableILI, Tasa = r.Abr.Tasa },
                        May = r.May == null ? new VariablesIndicadoresModel() { VariableIF = 0, VariableIS = 0, VaribleILI = 0, Tasa = 0 } : new VariablesIndicadoresModel() { VariableIF = r.May.VariableIF, VariableIS = r.May.VariableIS, VaribleILI = r.May.VariableILI, Tasa = r.May.Tasa },
                        Jun = r.Jun == null ? new VariablesIndicadoresModel() { VariableIF = 0, VariableIS = 0, VaribleILI = 0, Tasa = 0 } : new VariablesIndicadoresModel() { VariableIF = r.Jun.VariableIF, VariableIS = r.Jun.VariableIS, VaribleILI = r.Jun.VariableILI, Tasa = r.Jun.Tasa },
                        Jul = r.Jul == null ? new VariablesIndicadoresModel() { VariableIF = 0, VariableIS = 0, VaribleILI = 0, Tasa = 0 } : new VariablesIndicadoresModel() { VariableIF = r.Jul.VariableIF, VariableIS = r.Jul.VariableIS, VaribleILI = r.Jul.VariableILI, Tasa = r.Jul.Tasa },
                        Ago = r.Ago == null ? new VariablesIndicadoresModel() { VariableIF = 0, VariableIS = 0, VaribleILI = 0, Tasa = 0 } : new VariablesIndicadoresModel() { VariableIF = r.Ago.VariableIF, VariableIS = r.Ago.VariableIS, VaribleILI = r.Ago.VariableILI, Tasa = r.Ago.Tasa },
                        Sep = r.Sep == null ? new VariablesIndicadoresModel() { VariableIF = 0, VariableIS = 0, VaribleILI = 0, Tasa = 0 } : new VariablesIndicadoresModel() { VariableIF = r.Sep.VariableIF, VariableIS = r.Sep.VariableIS, VaribleILI = r.Sep.VariableILI, Tasa = r.Sep.Tasa },
                        Oct = r.Oct == null ? new VariablesIndicadoresModel() { VariableIF = 0, VariableIS = 0, VaribleILI = 0, Tasa = 0 } : new VariablesIndicadoresModel() { VariableIF = r.Oct.VariableIF, VariableIS = r.Oct.VariableIS, VaribleILI = r.Oct.VariableILI, Tasa = r.Oct.Tasa },
                        Nov = r.Nov == null ? new VariablesIndicadoresModel() { VariableIF = 0, VariableIS = 0, VaribleILI = 0, Tasa = 0 } : new VariablesIndicadoresModel() { VariableIF = r.Nov.VariableIF, VariableIS = r.Nov.VariableIS, VaribleILI = r.Nov.VariableILI, Tasa = r.Nov.Tasa },
                        Dic = r.Dic == null ? new VariablesIndicadoresModel() { VariableIF = 0, VariableIS = 0, VaribleILI = 0, Tasa = 0 } : new VariablesIndicadoresModel() { VariableIF = r.Dic.VariableIF, VariableIS = r.Dic.VariableIS, VaribleILI = r.Dic.VariableILI, Tasa = r.Dic.Tasa }

                    }).ToList();
                }
            }
            else
                return Json(new { Data = "Los valores ingresados están erróneos. Verifíquelos e intente nuevamente.", Mensaje = "INVALID" });
            if (objIndicadores != null && objIndicadores.Count > 0)
            {
                var datos = RenderRazorViewToString("_TblIndicadores", objIndicadores);
                return Json(new { Data = datos, Mensaje = "OK" });
                //var grafica = new IndicadoresController();
                //grafica.GraficaIndicadores(objIndicadores);
            }
            else
                return Json(new { Data = "No existe información para este periodo.", Mensaje = "NOTFOUND" });
        }

        public JsonResult GenerarExcelIndicadorIF(IndicadoresModel indicadores)
        {
            if (indicadores != null)
            {
                TempData["indicadores"] = indicadores;
                return Json(new { Data = "/Indicadores/ObtenerExcelIndicadorIF", Mensaje = "Succes" });
            }
            return Json(new { Data = "No fue posible generar el archivo solicitado", Mensaje = "Fail" });
        }


        public FileResult ObtenerExcelIndicadorIF()
        {
            IndicadoresModel indicadores = null;
            byte[] result = new byte[] { };
            var objEvaluacion = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (TempData["indicadores"] != null)
            {
                indicadores = (IndicadoresModel)TempData["indicadores"];
                int idEmpresaUsuaria = int.Parse(string.IsNullOrEmpty(indicadores.IdEmpresaUsuaria) ? "0" : indicadores.IdEmpresaUsuaria);
                ServiceClient.EliminarParametros();
                ServiceClient.AdicionarParametro("anio", indicadores.AnioSeleccionado);
                ServiceClient.AdicionarParametro("valorK", indicadores.ConstanteSeleccionada);
                ServiceClient.AdicionarParametro("Nit", objEvaluacion.NitEmpresa);
                ServiceClient.AdicionarParametro("idEmpresaUsuaria", idEmpresaUsuaria);
                ServiceClient.AdicionarParametro("IdContingencia", indicadores.IdContingencia);

                //se consume el servicio post para guardar la información de la evaluación inicial
                result = ServiceClient.ObtenerObjetoJsonRestFul<byte[]>(UrlServicioPlanificacion, CapacidaddescargarexcelInidicadores, RestSharp.Method.GET);
            }
            return File(result, "application/vnd.ms-excel", "IndicaresIFISILI.xlsx");
        }

        /// <summary>
        /// Renderiza la vista con la informacion de los acumulados totales para las
        /// variables IF, IS, ILI, HHT, XT
        /// </summary>
        /// <param name="indicadores"></param>
        /// <returns></returns>
        public JsonResult TotalAcumuladoContingencias(IndicadoresModel indicadores)
        {
            var objEvaluacion = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            List<AcumuladoTotalContingenciasModel> acumuladoTotal = null;
            if (ModelState.IsValid)
            {
                int idEmpresaUsuaria = int.Parse(string.IsNullOrEmpty(indicadores.IdEmpresaUsuaria) ? "0" : indicadores.IdEmpresaUsuaria);
                var result = lnindicadores.ObtenerAcumuladoTotalContingencias(indicadores.AnioSeleccionado, indicadores.ConstanteSeleccionada, objEvaluacion.NitEmpresa, idEmpresaUsuaria, indicadores.IdContingencia);
                if (result != null && result.Count() > 0)
                {
                    acumuladoTotal = result.Select(r => new AcumuladoTotalContingenciasModel()
                    {
                        Mes = ObtenerNombreMes(indicadores.AnioSeleccionado, r.Mes),
                        VariableIF = r.VariableIF,
                        VariableIS = r.VariableIS,
                        VariableILI = r.VariableILI,
                        Tasa = r.Tasa,
                        HorasTrabajadas = r.HorasTrabajadas,
                        NumeroTrabajadores = r.NumeroTrabajadores,
                        TotalPeriodo = r.TotalPeriodo == null ? null : new TotalAcumuladoModel()
                        {
                            TotalVariableIF = r.TotalPeriodo.TotalVariableIF,
                            TotalVariableIS = r.TotalPeriodo.TotalVariableIS,
                            TotalVariableILI = r.TotalPeriodo.TotalVariableILI,
                            TotalHorasTrabajadas = r.TotalPeriodo.TotalHorasTrabajadas,
                            TotalNumeroTrabajadores = r.TotalPeriodo.TotalNumeroTrabajadores,
                            TotalTasa = r.TotalPeriodo.TotalTasa
                        }
                    }).ToList();
                }
            }
            else
                return Json(new { Data = "Los valores ingresados están erróneos. Verifíquelos e intente nuevamente.", Mensaje = "INVALID" });
            if (acumuladoTotal != null && acumuladoTotal.Count > 0)
            {
                var datos = RenderRazorViewToString("_TotalAcumuladoIndicadores", acumuladoTotal);
                return Json(new { Data = datos, Mensaje = "OK" });
            }
            else
                return Json(new { Data = "No existe información para este periodo.", Mensaje = "NOTFOUND" });

        }

        public JsonResult GenerarExcelAcumulado(IndicadoresModel indicadores)
        {
            if (indicadores != null)
            {
                TempData["indicadores"] = indicadores;
                return Json(new { Data = "/Indicadores/ObtenerExcelAcumulado", Mensaje = "Succes" });
            }
            return Json(new { Data = "No fue posible generar el archivo solicitado", Mensaje = "Fail" });
        }

        public FileResult ObtenerExcelAcumulado()
        {
            IndicadoresModel indicadores = null;
            byte[] result = new byte[] { };
            var objEvaluacion = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (TempData["indicadores"] != null)
            {
                indicadores = (IndicadoresModel)TempData["indicadores"];
                int idEmpresaUsuaria = int.Parse(string.IsNullOrEmpty(indicadores.IdEmpresaUsuaria) ? "0" : indicadores.IdEmpresaUsuaria);
                ServiceClient.EliminarParametros();
                ServiceClient.AdicionarParametro("anio", indicadores.AnioSeleccionado);
                ServiceClient.AdicionarParametro("valorK", indicadores.ConstanteSeleccionada);
                ServiceClient.AdicionarParametro("Nit", objEvaluacion.NitEmpresa);
                ServiceClient.AdicionarParametro("idEmpresaUsuaria", idEmpresaUsuaria);
                ServiceClient.AdicionarParametro("tipoContingenciaComparar", indicadores.IdContingencia);
                //se consume el servicio post para guardar la información de la evaluación inicial
                result = ServiceClient.ObtenerObjetoJsonRestFul<byte[]>(UrlServicioPlanificacion, CapacidaddescargarexcelAcumulado, RestSharp.Method.GET);
            }
            return File(result, "application/vnd.ms-excel", "AcumuladoContingencias.xlsx");
        }

        /// <summary>
        /// Renderiza la información de los indicadores para los dos años selecionados
        /// </summary>
        /// <param name="indicadores"></param>
        /// <returns></returns>
        [HttpPost]
        //public JsonResult ObtenerDatosComparacionIndicadores(IndicadoresModel indicadores)
        //{
        //    var objEvaluacion = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
        //    List<AcumuladoTotalContingenciasModel> acumuladoTotalPrimerAnio = null;
        //    List<AcumuladoTotalContingenciasModel> acumuladoTotalSegundoAnio = null;
        //    if (ModelState.IsValid)
        //    {
        //        int idEmpresaUusaria = string.IsNullOrEmpty(indicadores.IdEmpresaUsuaria) ? 0 : int.Parse(indicadores.IdEmpresaUsuaria);
        //        var indicadoresPrimerAnio = lnindicadores.ObtenerAcumuladoTotalContingencias(indicadores.PrimerAnio, indicadores.ConstanteSeleccionada, objEvaluacion.NitEmpresa, idEmpresaUusaria, indicadores.TipoContingeciaComparar);
        //        var indicadoresSegundoAnio = lnindicadores.ObtenerAcumuladoTotalContingencias(indicadores.SegundoAnio, indicadores.ConstanteSeleccionada, objEvaluacion.NitEmpresa, idEmpresaUusaria, indicadores.TipoContingeciaComparar);

        //        var ano1 = indicadoresPrimerAnio.Where(r => r.VariableIF > 0).Select(r => r).FirstOrDefault();
        //        var ano2 = indicadoresPrimerAnio.Where(r => r.VariableIF > 0).Select(r => r).FirstOrDefault();
        //        if (ano1 == null && ano1 == null)
        //            return Json(new { Data = "No se encontro informacion para los parametros ingresados.", Mensaje = "INVALID" });


        //        if (indicadoresPrimerAnio != null && indicadoresPrimerAnio.Count() > 0)
        //        {
        //            acumuladoTotalPrimerAnio = indicadoresPrimerAnio.Select(r => new AcumuladoTotalContingenciasModel()
        //            {
        //                Mes = ObtenerNombreMes(indicadores.PrimerAnio, r.Mes),
        //                VariableIF = r.VariableIF,
        //                VariableIS = r.VariableIS,
        //                VariableILI = r.VariableILI,
        //                Tasa = r.Tasa,
        //                HorasTrabajadas = r.HorasTrabajadas,
        //                NumeroTrabajadores = r.NumeroTrabajadores,
        //                TotalPeriodo = r.TotalPeriodo == null ? null : new TotalAcumuladoModel()
        //                {
        //                    TotalVariableIF = r.TotalPeriodo.TotalVariableIF,
        //                    TotalVariableIS = r.TotalPeriodo.TotalVariableIS,
        //                    TotalVariableILI = r.TotalPeriodo.TotalVariableILI,
        //                    TotalHorasTrabajadas = r.TotalPeriodo.TotalHorasTrabajadas,
        //                    TotalNumeroTrabajadores = r.TotalPeriodo.TotalNumeroTrabajadores,
        //                    TotalTasa = r.TotalPeriodo.TotalTasa
        //                }
        //            }).ToList();
        //        }
        //        if (indicadoresSegundoAnio != null && indicadoresSegundoAnio.Count() > 0)
        //        {
        //            acumuladoTotalSegundoAnio = indicadoresSegundoAnio.Select(r => new AcumuladoTotalContingenciasModel()
        //            {
        //                Mes = ObtenerNombreMes(indicadores.SegundoAnio, r.Mes),
        //                VariableIF = r.VariableIF,
        //                VariableIS = r.VariableIS,
        //                VariableILI = r.VariableILI,
        //                Tasa = r.Tasa,
        //                HorasTrabajadas = r.HorasTrabajadas,
        //                NumeroTrabajadores = r.NumeroTrabajadores,
        //                TotalPeriodo = r.TotalPeriodo == null ? null : new TotalAcumuladoModel()
        //                {
        //                    TotalVariableIF = r.TotalPeriodo.TotalVariableIF,
        //                    TotalVariableIS = r.TotalPeriodo.TotalVariableIS,
        //                    TotalVariableILI = r.TotalPeriodo.TotalVariableILI,
        //                    TotalHorasTrabajadas = r.TotalPeriodo.TotalHorasTrabajadas,
        //                    TotalNumeroTrabajadores = r.TotalPeriodo.TotalNumeroTrabajadores,
        //                    TotalTasa = r.TotalPeriodo.TotalTasa
        //                }
        //            }).ToList();
        //        }
        //    }
        //    else
        //        return Json(new { Data = "Los valores ingresados están erróneos. Verifíquelos e intente nuevamente.", Mensaje = "INVALID" });

        //    if ((acumuladoTotalPrimerAnio != null && acumuladoTotalPrimerAnio.Count > 0) && (acumuladoTotalSegundoAnio != null && acumuladoTotalSegundoAnio.Count > 0))
        //    {
        //        return Json(new { Data = new[] { new { Datos = acumuladoTotalPrimerAnio }, new { Datos = acumuladoTotalSegundoAnio } }, Mensaje = "OK" });
        //    }
        //    else
        //        return Json(new { Data = "No existe información suficiente para realizar la comparación de los indicadores en los años seleccionados.", Mensaje = "NOTFOUND" });
        //}


        ///// <summary>
        ///// Renderiza la información de los indicadores para los dos años selecionados
        ///// </summary>
        ///// <param name="indicadores"></param>
        ///// <returns></returns>
        //[HttpPost]
        //public JsonResult ObtenerDatosGraficaAcumulado(IndicadoresModel indicadores)
        //{
        //    var objEvaluacion = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
        //    List<AcumuladoTotalContingenciasModel> acumuladoTotalPrimerAnio = null;
        //    if (ModelState.IsValid)
        //    {
        //        int idEmpresaUsuaria = int.Parse(string.IsNullOrEmpty(indicadores.IdEmpresaUsuaria) ? "0" : indicadores.IdEmpresaUsuaria);
        //        var AcumuladoAnual = lnindicadores.ObtenerAcumuladoTotalContingencias(indicadores.AnioSeleccionado, indicadores.ConstanteSeleccionada, objEvaluacion.NitEmpresa, idEmpresaUsuaria, indicadores.IdContingencia);

        //        if (AcumuladoAnual != null && AcumuladoAnual.Count() > 0)
        //        {
        //            acumuladoTotalPrimerAnio = AcumuladoAnual.Select(r => new AcumuladoTotalContingenciasModel()
        //            {
        //                Mes = ObtenerNombreMes(indicadores.AnioSeleccionado, r.Mes),
        //                VariableIF = r.VariableIF,
        //                VariableIS = r.VariableIS,
        //                VariableILI = r.VariableILI,
        //                Tasa = r.Tasa,
        //                HorasTrabajadas = r.HorasTrabajadas,
        //                NumeroTrabajadores = r.NumeroTrabajadores,
        //                TotalPeriodo = r.TotalPeriodo == null ? null : new TotalAcumuladoModel()
        //                {
        //                    TotalVariableIF = r.TotalPeriodo.TotalVariableIF,
        //                    TotalVariableIS = r.TotalPeriodo.TotalVariableIS,
        //                    TotalVariableILI = r.TotalPeriodo.TotalVariableILI,
        //                    TotalHorasTrabajadas = r.TotalPeriodo.TotalHorasTrabajadas,
        //                    TotalNumeroTrabajadores = r.TotalPeriodo.TotalNumeroTrabajadores,
        //                    TotalTasa = r.TotalPeriodo.TotalTasa
        //                }
        //            }).ToList();
        //        }
        //    }
        //    else
        //        return Json(new { Data = "Los valores ingresados están erróneos. Verifíquelos e intente nuevamente.", Mensaje = "INVALID" });

        //    if ((acumuladoTotalPrimerAnio != null && acumuladoTotalPrimerAnio.Count > 0))
        //    {
        //        return Json(new { Data = acumuladoTotalPrimerAnio, Mensaje = "OK" });
        //    }
        //    else
        //        return Json(new { Data = "No existe información suficiente para realizar la grafica.", Mensaje = "NOTFOUND" });
        //}

        public JsonResult GenerarExcelComparativo(IndicadoresModel indicadores)
        {
            if (indicadores != null)
            {
                TempData["indicadores"] = indicadores;
                return Json(new { Data = "/Indicadores/ObtenerExcelComparativo", Mensaje = "Succes" });
            }
            return Json(new { Data = "No fue posible generar el archivo solicitado", Mensaje = "Fail" });
        }

        public FileResult ObtenerExcelComparativo()
        {
            IndicadoresModel indicadores = null;
            byte[] result = new byte[] { };
            var objEvaluacion = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (TempData["indicadores"] != null)
            {
                indicadores = (IndicadoresModel)TempData["indicadores"];
                int idEmpresaUsuaria = int.Parse(string.IsNullOrEmpty(indicadores.IdEmpresaUsuaria) ? "0" : indicadores.IdEmpresaUsuaria);
                ServiceClient.EliminarParametros();
                ServiceClient.AdicionarParametro("anio1", indicadores.PrimerAnio);
                ServiceClient.AdicionarParametro("anio2", indicadores.PrimerAnio);
                ServiceClient.AdicionarParametro("valorK", indicadores.ConstanteSeleccionada);
                ServiceClient.AdicionarParametro("Nit", objEvaluacion.NitEmpresa);
                ServiceClient.AdicionarParametro("idEmpresaUsuaria", idEmpresaUsuaria);
                ServiceClient.AdicionarParametro("tipoContingenciaComparar", indicadores.TipoContingeciaComparar);
                //se consume el servicio post para guardar la información de la evaluación inicial
                result = ServiceClient.ObtenerObjetoJsonRestFul<byte[]>(UrlServicioPlanificacion, CapacidaddescargarexcelComparativo, RestSharp.Method.GET);
            }
            return File(result, "application/vnd.ms-excel", "ComparativoIndicadores.xlsx");
        }

        /// <summary>
        /// Obtiene el nombre del mes pasado por parametro
        /// </summary>
        /// <param name="anio"></param>
        /// <param name="mes"></param>
        /// <returns></returns>
        private string ObtenerNombreMes(int anio, int mes)
        {
            var nombreMes = string.Empty;
            var fecha = new DateTime(anio, mes, 1);
            var mesObtenido = fecha.ToString("MMMM", System.Globalization.CultureInfo.GetCultureInfo("es-CO"));
            nombreMes = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(mesObtenido);
            return nombreMes;
        }



    }
}