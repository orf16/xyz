using SG_SST.Controllers.Base;
using SG_SST.EntidadesDominio.Ausentismo;
using SG_SST.Utilidades;
using SG_SST.Logica.Ausentismo;
using SG_SST.Logica.Empresas;
using SG_SST.Models.Ausentismo;
using SG_SST.ServiceRequest;
using System;
using System.Collections.Generic;
using System.Configuration;

using System.Web.Mvc;

namespace SG_SST.Controllers.Ausentismo
{
    public class Configuracion_HHTController : BaseController
    {
        string UrlServicioPlanificacion = ConfigurationManager.AppSettings["UrlServicioPlanificacion"];
        string CapacidadObtenerAnoInicioEmpresa = ConfigurationManager.AppSettings["CapacidadObtenerAnoInicioEmpresa"];
        string CapacidadObtenerConfiguraciones = ConfigurationManager.AppSettings["CapacidadObtenerConfiguraciones"];
        string CapacidadEliminarConfiguracion = ConfigurationManager.AppSettings["CapacidadEliminarConfiguracion"];

        LNConfiguracion lnConfiguracion = new LNConfiguracion();
        LNEmpresa lnEmpresa = new LNEmpresa();
        public ActionResult ConfiguracionHHT()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "Debe estar autenticado para visualizar los Indicadores.";
                return View();
            }

            var objConfiguracion = new ConfiguracionModel();
            objConfiguracion.RazonSocial = usuarioActual.RazonSocialEmpresa;
            var usuario = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            //se quema el id de la empresa en "1" si no hay session. !!!!Corregir cuando se tenga Autenticacion!!!!
            objConfiguracion.IdEmpresaSeleccionada = usuario == null ? "1" : usuario.NitEmpresa;
            objConfiguracion.Meses = objConfiguracion.ConfigurarMeses();
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("NIT", usuarioActual.NitEmpresa);
            var resultAno = ServiceClient.ObtenerObjetoJsonRestFul<int>(UrlServicioPlanificacion, CapacidadObtenerAnoInicioEmpresa, RestSharp.Method.GET);
            if (resultAno > 0)
            {
                objConfiguracion.Anos = GetAnios(resultAno);
            }
            else
                objConfiguracion.Anos = GetAnios(2010);

            return View(objConfiguracion);
        }

        [HttpPost]
        public JsonResult CargarConfiguraciones(int ano)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return Json(new { Data = "Su sesion a finalizado, por favor ingrese nuevamente", Mensaje = "FinSesion" });
            }

            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("NitEmpresa", usuarioActual.NitEmpresa);
            ServiceClient.AdicionarParametro("Ano", ano);
            var resultConfiguraciones = ServiceClient.ObtenerArrayJsonRestFul<EDConfiguracion>(UrlServicioPlanificacion, CapacidadObtenerConfiguraciones, RestSharp.Method.GET);
            List<ConfiguracionModel> ConfiguracionesModel = new List<ConfiguracionModel>();
            if (resultConfiguraciones != null)
            {
                if (resultConfiguraciones.Length > 0 )
                {
                    foreach (var item in resultConfiguraciones)
                    {
                        ConfiguracionModel config = new ConfiguracionModel()
                        {
                            idConfiguracion = item.IdConfiguracion,
                            Anio = item.Anio.ToString(),
                            Mes = Utilitarios.ObtenerStrMes(item.Mes),
                            XT = Convert.ToInt32(item.NumeroTrabajadoresXT),
                            DTM = Convert.ToInt32(item.DiasTrabajadosDTM),
                            HTD = Convert.ToInt32(item.HorasHombreHTD),
                            NHE = Convert.ToInt32(item.HorasExtrasNHE),
                            NHA = Convert.ToInt32(item.HorasAusentismoNHA),
                            Total = Convert.ToInt32(item.Total),
                            FechaModificacion = string.Format("{0}/{1}/{2}", item.FechaModificacion.Day, item.FechaModificacion.Month, item.FechaModificacion.Year)
                        };
                        ConfiguracionesModel.Add(config);
                    }

                    if (ConfiguracionesModel.Count < 1)
                    {
                        ConfiguracionModel config = new ConfiguracionModel()
                        {
                            Anio = ano.ToString(),
                            Mes = ""
                        };
                        ConfiguracionesModel.Add(config);
                    }

                    var datos = RenderRazorViewToString("_Configuraciones", ConfiguracionesModel);
                    return Json(new { Data = datos, Mensaje = "Success" });
                }
                else
                {
                    ConfiguracionModel config = new ConfiguracionModel()
                    {
                        Anio = ano.ToString(),
                        Mes = ""
                    };
                    ConfiguracionesModel.Add(config);
                    var datos = RenderRazorViewToString("_Configuraciones", ConfiguracionesModel);
                    return Json(new { Data = datos, Mensaje = "Success" });
                }
            }
            else
            {
                ConfiguracionModel config = new ConfiguracionModel()
                {
                    Anio = ano.ToString(),
                    Mes = ""
                };
                ConfiguracionesModel.Add(config);
                var datos = RenderRazorViewToString("_Configuraciones", ConfiguracionesModel);
                return Json(new { Data = datos, Mensaje = "Success" });
            }


        }

        [HttpPost]
        public JsonResult GenerarNuevaConfiguracion(int Ano, string Mes)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "Debe estar autenticado para visualizar los Indicadores.";
                return Json(new { Data = "", Mensaje = "Success" });
            }
            if (!string.IsNullOrEmpty(Mes))
            {
                ServiceClient.EliminarParametros();
                ServiceClient.AdicionarParametro("NitEmpresa", usuarioActual.NitEmpresa);
                ServiceClient.AdicionarParametro("Ano", Ano);
                var resultConfiguraciones = ServiceClient.ObtenerArrayJsonRestFul<EDConfiguracion>(UrlServicioPlanificacion, CapacidadObtenerConfiguraciones, RestSharp.Method.GET);

                if (resultConfiguraciones[0] != null)
                {
                    int i = 1;
                    if (resultConfiguraciones[resultConfiguraciones.Length - 1].Mes == 12)
                        i = 0;


                    ConfiguracionModel config = new ConfiguracionModel()
                    {
                        idConfiguracion = resultConfiguraciones[resultConfiguraciones.Length - 1].IdConfiguracion,
                        Anio = resultConfiguraciones[resultConfiguraciones.Length - 1].Anio.ToString(),
                        XT = Convert.ToInt32(resultConfiguraciones[resultConfiguraciones.Length - 1].NumeroTrabajadoresXT),
                        DTM = Convert.ToInt32(resultConfiguraciones[resultConfiguraciones.Length - 1].DiasTrabajadosDTM),
                        HTD = Convert.ToInt32(resultConfiguraciones[resultConfiguraciones.Length - 1].HorasHombreHTD),
                        NHE = Convert.ToInt32(resultConfiguraciones[resultConfiguraciones.Length - 1].HorasExtrasNHE),
                        NHA = Convert.ToInt32(resultConfiguraciones[resultConfiguraciones.Length - 1].HorasAusentismoNHA),
                        Total = Convert.ToInt32(resultConfiguraciones[resultConfiguraciones.Length - 1].Total),
                        FechaModificacion = string.Format("{0}/{1}/{2}", resultConfiguraciones[resultConfiguraciones.Length - 1].FechaModificacion.Day, resultConfiguraciones[resultConfiguraciones.Length - 1].FechaModificacion.Month, resultConfiguraciones[resultConfiguraciones.Length - 1].FechaModificacion.Year)

                    };
                    var result = lnConfiguracion.AusenciasPorMes(config.Anio, resultConfiguraciones[resultConfiguraciones.Length - 1].Mes + i, int.Parse(usuarioActual.NitEmpresa));
                    config.NHA = Convert.ToInt32(result);
                    config.Mes = Utilitarios.ObtenerStrMes(resultConfiguraciones[resultConfiguraciones.Length - 1].Mes + i);
                    config.Meses = config.ConfigurarMeses();


                    var datos = RenderRazorViewToString("_ConfiguracionHHTModal", config);
                    return Json(new { Data = datos, Mensaje = "Success" });
                }
                else
                    return Json(new { Data = "", Mensaje = "FAILD" });
            }
            else
            {
                ConfiguracionModel config = new ConfiguracionModel();
                config.Meses = config.ConfigurarMeses();
                config.Anio = Ano.ToString();
                config.Mes = Utilitarios.ObtenerStrMes(1);

                var result = lnConfiguracion.AusenciasPorMes(config.Anio, 1, int.Parse(usuarioActual.NitEmpresa));
                config.NHA = Convert.ToInt32(result);

                var datos = RenderRazorViewToString("_ConfiguracionHHTModal", config);
                return Json(new { Data = datos, Mensaje = "Success" });
            }
        }


        public JsonResult EliminarConfiguracion(int idConfiguracion)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return Json(new { Data = "", Mensaje = "FinSesion" });
            }

            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("NitEmpresa", usuarioActual.NitEmpresa);
            ServiceClient.AdicionarParametro("idConfiguracion", idConfiguracion);
            var resultConfiguraciones = ServiceClient.ObtenerObjetoJsonRestFul<bool>(UrlServicioPlanificacion, CapacidadEliminarConfiguracion, RestSharp.Method.GET);
            if (resultConfiguraciones)
                return Json(new { Data = "Se eliminaron con éxito las variables seleccionadas", Mensaje = "Success" });
            else
                return Json(new { Data = "El proceso ha fallado por favor intente mas tarde.", Mensaje = "FAIL" });
        }

        [HttpPost]
        public ActionResult AusenciasMes(string mes, string ano)
        {
            var objConfiguracion = new ConfiguracionModel();

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            var result = lnConfiguracion.AusenciasPorMes(ano, int.Parse(mes), int.Parse(usuarioActual.NitEmpresa));
            if (result != null)
                return Json(new { Data = result, Mensaje = "Success" });
            else
                return Json(new { Data = string.Empty, Mensaje = "Fail" });

        }

        [HttpPost]
        public ActionResult ConfiguracionHHT(ConfiguracionModel objConfiguracion)
        {
            var configuracion = new EDConfiguracion();
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual != null)
                configuracion.IdEmpresa = usuarioActual.NitEmpresa.ToString();

            configuracion.Anio = int.Parse(objConfiguracion.Anio);
            configuracion.Mes = objConfiguracion.MesSeleccionado;
            configuracion.HorasLaborales = objConfiguracion.HTD;
            configuracion.NumeroTrabajadoresXT = objConfiguracion.XT;
            configuracion.DiasTrabajadosDTM = objConfiguracion.DTM;
            configuracion.HorasHombreHTD = objConfiguracion.HTD;
            configuracion.HorasExtrasNHE = objConfiguracion.NHE;
            configuracion.HorasAusentismoNHA = objConfiguracion.NHA;
            configuracion.FechaModificacion = DateTime.Now;
            configuracion.Total = objConfiguracion.Total;
            if (objConfiguracion.IsLunesViernes)
            {
                configuracion.IdDiasLaborables = 1;
                configuracion.DiasLaborales = 5;
            }
            else if (objConfiguracion.IsLunesSabado)
            {
                configuracion.IdDiasLaborables = 2;
                configuracion.DiasLaborales = 6;
            }
            else
            {
                configuracion.IdDiasLaborables = 1;
                configuracion.DiasLaborales = 5;
            }

            //llamado a la capa de negocio para guardado de informacion
            var result = lnConfiguracion.GuardarConfiguracion(configuracion);
            if (result)
                return Json(new { Data = "", Mensaje = "OK" });
            else
                return Json(new { Data = "", Mensaje = "FAILD" });
        }
    }
}
