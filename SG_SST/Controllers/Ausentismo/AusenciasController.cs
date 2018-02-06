using RestSharp;
using SG_SST.Audotoria;
using SG_SST.Controllers.Base;
using SG_SST.EntidadesDominio.Ausentismo;
using SG_SST.EntidadesDominio.Empresas;
using SG_SST.Logica.Ausentismo;
using SG_SST.Models.AdminUsuarios;
using SG_SST.Models.Ausentismo;
using SG_SST.Models.Login;
using SG_SST.ServiceRequest;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace SG_SST.Controllers.Ausentismo
{
    public class AusenciasController : BaseController
    {
        LNContingencia lncontingencia = new LNContingencia();
        LNDiagnostico lndiagnostico = new LNDiagnostico();
        LNAusencia lnausencia = new LNAusencia();
        LNDepartamento lnDepartamento = new LNDepartamento();
        LNMunicipio lnMunicipio = new LNMunicipio();
        RegistraLog registroLog = new RegistraLog();
        #region Registrar Ausentismo
        public ActionResult RegistrarAusencia()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "Debe estar autenticado para registrar una Ausencia.";
                return View();
            }
            var ausenccia = new AusenciaModel();
            ausenccia.DatosTrabajor = new AfiliadoModel();
            ausenccia.IdEmpresa = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current) == null ? string.Empty : ObtenerUsuarioEnSesion(System.Web.HttpContext.Current).NitEmpresa;
            ausenccia.RazonSocial = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current) == null ? string.Empty : ObtenerUsuarioEnSesion(System.Web.HttpContext.Current).RazonSocialEmpresa;
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("NIT", usuarioActual.NitEmpresa);
            var result = ServiceClient.ObtenerArrayJsonRestFul<EDEmpresa_Usuaria>(urlServicioEmpresas, CapacidadObtenerEmpresasusuarias, RestSharp.Method.GET);
            if (result != null && result.Count() > 0)
            {
                if (result[0] != null)
                {
                    ausenccia.EmpresasUsuarias = result.Select(c => new SelectListItem()
                    {
                        Value = c.IdEmpresaUsuaria.ToString(),
                        Text = c.RazonSocial
                    }).ToList();
                }
            }
            else
                ausenccia.EmpresasUsuarias = new List<SelectListItem>();

            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("NIT", usuarioActual.NitEmpresa);
            var resultSede = ServiceClient.ObtenerArrayJsonRestFul<EDSede>(urlServicioEmpresas, CapacidadObtenerSedesPorNit, RestSharp.Method.GET);
            if (resultSede != null && resultSede.Count() > 0)
            {
                ausenccia.Sedes = resultSede.Select(c => new SelectListItem()
                {
                    Value = c.IdSede.ToString(),
                    Text = c.NombreSede
                }).ToList();
            }
            else
                ausenccia.Sedes = new List<SelectListItem>();
            ausenccia.Departamentos = lnDepartamento.ObtenerListadoDepartamento().Select(d => new SelectListItem() { Value = d.IdDepartamento.ToString(), Text = d.Nombre }).ToList();
            ausenccia.Municipios = new List<SelectListItem>();// lnMunicipio.ObtenerListadoMunicipio().Select(m => new SelectListItem() { Value = m.IdMunicipio.ToString(), Text = m.Nombre }).ToList();
            return View(ausenccia);
        }

        [HttpPost]
        public JsonResult ConsultarMunicipiosPorDepto(int idDepto)
        {
            var ausenccia = new AusenciaModel();
            ausenccia.Municipios = lnMunicipio.ObtenerListadoMunicipio(idDepto).Select(m => new SelectListItem() { Value = m.IdMunicipio.ToString(), Text = m.Nombre }).ToList();
            if (ausenccia.Municipios.Count > 0)
                return Json(new { Data = ausenccia.Municipios, Mensaje = "Success" });
            else
                return Json(new { Data = "No fue posible realizar obtener los municipios", Mensaje = "Fail" });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="numeroDocumento"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ConsultarDatosTrabajador(string numeroDocumento)
        {
            try
            {
                var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
                //if (usuarioActual == null)
                //{
                //    ViewBag.Mensaje = "Debe estar autenticado para registrar una Ausencia.";
                //    return View();
                //}
                EmpresaAfiliadoModel datos = null;
                if (!string.IsNullOrEmpty(numeroDocumento))
                {
                    var cliente = new RestSharp.RestClient(ConfigurationManager.AppSettings["Url"]);
                    var request = new RestRequest(consultaAfiliadoEmpresaActivo, RestSharp.Method.GET);
                    request.RequestFormat = DataFormat.Xml;
                    request.Parameters.Clear();
                    request.AddParameter("tpEm", usuarioActual.SiglaTipoDocumentoEmpresa);
                    request.AddParameter("docEm", usuarioActual.NitEmpresa);
                    request.AddParameter("tpAfiliado", "cc");
                    request.AddParameter("docAfiliado", numeroDocumento);
                    request.AddHeader("Content-Type", "application/json");
                    request.AddHeader("Accept", "application/json");

                    ServicePointManager.ServerCertificateValidationCallback = delegate
                    { return true; };
                    IRestResponse<List<EmpresaAfiliadoModel>> response = cliente.Execute<List<EmpresaAfiliadoModel>>(request);
                    var result = response.Content;

                    if (!string.IsNullOrWhiteSpace(result))
                    {
                        var respuesta = Newtonsoft.Json.JsonConvert.DeserializeObject<List<EmpresaAfiliadoModel>>(result);
                        if (respuesta.Count == 0)
                            return Json(new { Data = "No existe relación laboral entre el documento ingresado y la empresa", Mensaje = "NOTFOUND" });
                        var EmpresaSystem = respuesta.Where(a => a.estadoEmpresa.ToUpper().Equals("ACTIVA")).FirstOrDefault();
                        if (EmpresaSystem == null)
                            return Json(new { Data = "No existe relación laboral entre el documento ingresado y la empresa", Mensaje = "NOTFOUND" });

                        var AfilSystem = respuesta.Where(a => a.estadoPersona.ToUpper().Equals("ACTIVO")).FirstOrDefault();
                        if (AfilSystem == null)
                            return Json(new { Data = "No existe relación laboral entre el documento ingresado y la empresa", Mensaje = "NOTFOUND" });

                        GuardarSesionAfiliado(AfilSystem);
                        datos = AfilSystem;

                        return Json(new { Data = datos, Mensaje = "OK" });

                    }
                    else
                        return Json(new { Data = "No se obtuvo respuesta del servicio.", Mensaje = "NOTFOUND" });
                }
                else
                    return Json(new { Data = "Debe Ingresar Un muero de documento.", Mensaje = "NOTFOUND" });


                //    var cliente = new RestSharp.RestClient(ConfigurationManager.AppSettings["Url"]);
                //    var request = new RestRequest(ConfigurationManager.AppSettings["consultaAfiliado"], RestSharp.Method.GET);
                //    request.RequestFormat = DataFormat.Xml;
                //    request.Parameters.Clear();
                //    request.AddParameter("tpDoc", "cc");
                //    request.AddParameter("doc", numeroDocumento);
                //    request.AddHeader("Content-Type", "application/json");
                //    request.AddHeader("Accept", "application/json");

                //    //se omite la validación de certificado de SSL
                //    ServicePointManager.ServerCertificateValidationCallback = delegate
                //    { return true; };
                //    IRestResponse<List<AfiliadoModel>> response = cliente.Execute<List<AfiliadoModel>>(request);
                //    var result = response.Content;
                //    if (!string.IsNullOrWhiteSpace(result))
                //    {
                //        var respuesta = Newtonsoft.Json.JsonConvert.DeserializeObject<List<AfiliadoModel>>(result);
                //        if (respuesta.Count == 0)
                //            return Json(new { Data = "No se encontró ningun Trabajador asociado al documento ingresado.", Mensaje = "NOTFOUND" });
                //        var afiliado = respuesta.Where(a => a.Estado == "Activo").FirstOrDefault();
                //        if (afiliado == null)
                //            return Json(new { Data = "El afiliado asociado al documento ingresado se encuentra inactivo.", Mensaje = "INACTIVO" });
                //        else
                //        {
                //            GuardarSesionAfiliado(afiliado);
                //            datos = afiliado;
                //        }
                //    }
                //}
                //if (datos != null)
                //{
                //    return Json(new { Data = datos, Mensaje = "OK" });
                //}
                //else
                //    return Json(new { Data = "No se encontró ningun trabajador asociado al documento ingresado", Mensaje = "NOTFOUND" });
            }
            catch (Exception ex)
            {
                registroLog.RegistrarError(typeof(AusenciasController), string.Format("Error en la Acción ConsultarDatosTrabajador: {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                return Json(new { Data = "No se logró consultar la información del Trabajador. Intente más tarde.", Mensaje = "ERROR" });
            }
        }

        [HttpPost]
        public ActionResult AutoCompletarContingencia(string prefijo)
        {
            return Json(lncontingencia.AutoCompletarContingencia(prefijo), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AutoCompletarDiagnostico(string prefijo)
        {
            return Json(lndiagnostico.AutoCompletarDiagnostico(prefijo), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AutoCompletarDocumentos(string prefijo)
        {
            return Json(lnausencia.AutoCompletarBuscarDocumentos(prefijo), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult RegistrarNuevaAusencia()
        {
            try
            {
                var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
                var objNuevaAusencia = new AusenciaModel();
                var diagnostico = lndiagnostico.ObtenerListadoDisagnostico();
                objNuevaAusencia.Contingecias = lncontingencia.ObtenerListadoContingencia().Select(c => new SelectListItem() { Value = c.IdContingencia.ToString(), Text = c.Detalle }).ToList();
                objNuevaAusencia.HorasConfigurables = new List<SelectListItem>() {
                    new SelectListItem() { Value = "0", Text = "6:00 am"},
                    new SelectListItem() { Value = "1", Text = "7:00 am"},
                    new SelectListItem() { Value = "2", Text = "8:00 am"},
                    new SelectListItem() { Value = "3", Text = "9:00 am"},
                    new SelectListItem() { Value = "4", Text = "10:00 am"},
                    new SelectListItem() { Value = "5", Text = "11:00 am"},
                    new SelectListItem() { Value = "6", Text = "12:00 pm"},
                    new SelectListItem() { Value = "7", Text = "1:00 pm"},
                    new SelectListItem() { Value = "8", Text = "2:00 pm"},
                    new SelectListItem() { Value = "9", Text = "3:00 pm"},
                    new SelectListItem() { Value = "10", Text = "4:00 pm"},
                    new SelectListItem() { Value = "11", Text = "5:00 pm"},
                    new SelectListItem() { Value = "12", Text = "6:00 pm"},
                    new SelectListItem() { Value = "13", Text = "7:00 pm"},
                    new SelectListItem() { Value = "14", Text = "8:00 pm"},
                    new SelectListItem() { Value = "15", Text = "9:00 pm"}
                };

                ServiceClient.EliminarParametros();
                ServiceClient.AdicionarParametro("NIT", usuarioActual.NitEmpresa);
                var resultProceso = ServiceClient.ObtenerArrayJsonRestFul<EDProceso>(urlServicioEmpresas, CapacidadObtenerprocesosEmpresa, RestSharp.Method.GET);
                if (resultProceso != null && resultProceso.Count() > 0)
                {
                    objNuevaAusencia.Procesos = resultProceso.Select(c => new SelectListItem()
                    {
                        Value = c.Id_Proceso.ToString(),
                        Text = c.Descripcion
                    }).ToList();
                }
                else
                    objNuevaAusencia.Procesos = new List<SelectListItem>();


                objNuevaAusencia.Diagnostico = new DiagnosticoModel()
                {
                    Diagnosticos = diagnostico.Select(d => new SelectListItem()
                    {
                        Value = d.IdDiagnostico.ToString(),
                        Text = d.Descripcion
                    }).ToList()
                };
                var datos = RenderRazorViewToString("_NuevaAusencia", objNuevaAusencia);

                return Json(new { Data = datos, Mensaje = "Success" });
            }
            catch (Exception ex)
            {
                registroLog.RegistrarError(typeof(AusenciasController), string.Format("Error en la accion RegistrarNuevaAusencia. Hora: {0}, Error: {1}", DateTime.Now, ex.StackTrace), ex);
                return Json(new { Data = "", Mensaje = "Error" });
            }
        }

        [HttpPost]
        public JsonResult CalcularDiasAusenciaTrabajador(string fechaInicio, string fechaFin, int tipoContingencia, string horaInicio, string horaFin)
        {
            var diasLaborales = 5;
            fechaInicio = fechaInicio.Replace("/", "");
            fechaFin = fechaFin.Replace("/", "");
            var format = "ddMMyyyy";
            DateTime fechaI = DateTime.ParseExact(fechaInicio, format, System.Globalization.CultureInfo.InvariantCulture);
            DateTime fechaF = DateTime.ParseExact(fechaFin, format, System.Globalization.CultureInfo.InvariantCulture);
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual != null)
            {
                diasLaborales = lnausencia.ObtenerDiasLaborablesEmpresa(usuarioActual.NitEmpresa);
            }
            double cantidadDias = 0;
            if ((int)Enumeraciones.EnumAusentismo.Contingencias.PermisoPorHorasDia != tipoContingencia)
                cantidadDias = lnausencia.CalcularDiasLaborales(fechaI, fechaF, diasLaborales, tipoContingencia);
            else
            {
                var cantidadHoras = Math.Abs(Convert.ToDouble(horaInicio) - Convert.ToDouble(horaFin));                
                var horasLaborales = ConfigurationManager.AppSettings["HorasLaborales"] == null ? 8 : Convert.ToDouble(ConfigurationManager.AppSettings["HorasLaborales"].ToString());
                if (Convert.ToInt32(horaInicio) >= 2 && Convert.ToInt32(horaInicio) < 7 && Convert.ToInt32(horaFin) > 7)
                    cantidadHoras -= 1;                

                cantidadDias = cantidadHoras / horasLaborales;
            }
            return Json(new { Data = cantidadDias, Mensaje = "OK" });
        }

        /// <summary>
        /// Se guarda una nueva Ausencia.
        /// </summary>
        /// <param name="objNuevaAusencia"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult NuevaAusencia(AusenciaModel objNuevaAusencia)
        {
            var Ausencia = new EDAusencia();
            var objAfiliado = ObtenerAfiliadoEnSesion();

            Ausencia.NombrePersona = objNuevaAusencia.DatosTrabajor == null ? string.Empty : objNuevaAusencia.DatosTrabajor.Nombre1;
            Ausencia.Documento = objNuevaAusencia.Documento;
            Ausencia.IdEmpresa = objNuevaAusencia.IdEmpresa;
            Ausencia.IdEmpresaUsuaria = Convert.ToInt32(objNuevaAusencia.IdEmpresaUsuaria);
            Ausencia.idDepartamento = objNuevaAusencia.idDepartamento;
            Ausencia.idMunicipio = objNuevaAusencia.idMunicipio;
            Ausencia.IdContingencia = objNuevaAusencia.Contingencia.IdContingenciaSeleccionada;
            Ausencia.IdDiagnostico = objNuevaAusencia.Diagnostico.IdDiagnoticoSeleccionado;
            Ausencia.IdSede = objNuevaAusencia.idSede;
            Ausencia.IdProceso = objNuevaAusencia.idProceso;
            Ausencia.FechaInicio = DateTime.ParseExact(objNuevaAusencia.FechaInicio, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None);
            Ausencia.FechaFin = DateTime.ParseExact(objNuevaAusencia.FechaFin, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None);
            Ausencia.DiasAusencia = Convert.ToDecimal(objNuevaAusencia.DiasAusencia, CultureInfo.InvariantCulture);
            Ausencia.Costo = Convert.ToDecimal(objNuevaAusencia.Costo, CultureInfo.InvariantCulture);
            Ausencia.FactorPrestacional = Convert.ToDecimal(objNuevaAusencia.FactorPrestacional, CultureInfo.InvariantCulture);
            Ausencia.Observaciones = objNuevaAusencia.Observaciones;
            Ausencia.IdOcupacion = objAfiliado.IdOcupacion;
            Ausencia.Sexo = objNuevaAusencia.Sexo;
            Ausencia.Edad = objNuevaAusencia.Edad;
            Ausencia.Eps = objNuevaAusencia.Eps;
            Ausencia.TipoVinculacion = objNuevaAusencia.TipoVinculacion;
            var result = lnausencia.GuardarAusencia(Ausencia);
            if (result != null)
                if (result.Result.Equals("OK"))
                    return Json(new { status = "Success", Message = "La nueva Ausencia se registró con éxito." });
                else if (result.Result.Equals("CRUCE"))
                    return Json(new { status = "CRUCE", Message = "El afiliado ya presenta ausetimos registrado en las fechas ingresadas." });
                else
                    return Json(new { status = "Error", Message = "No fue posible registrar la nueva Ausencia. Intente nuevamente." });
            else
                return Json(new { status = "Error", Message = "No fue posible registrar la nueva Ausencia. Intente nuevamente." });
        }

        [HttpPost]
        public JsonResult ValidarCruceDeFechas(AusenciaModel objNuevaAusencia)
        {
            var Ausencia = new EDAusencia();
            var objAfiliado = ObtenerAfiliadoEnSesion();

            Ausencia.Documento = objNuevaAusencia.Documento;
            Ausencia.IdEmpresa = objNuevaAusencia.IdEmpresa;           
            Ausencia.FechaInicio = DateTime.ParseExact(objNuevaAusencia.FechaInicio, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None);
            Ausencia.FechaFin = DateTime.ParseExact(objNuevaAusencia.FechaFin, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None);

            bool cruce = lnausencia.ValidarCruceDeAusencias(Ausencia);
            if(!cruce)
                return Json(new { status = "CRUCE", Message = "" });
            else
                return Json(new { status = "Success", Message = "" });
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult ConsultarAusencia()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);

            var Ausencia = new AusenciaModel();
            var diagnostico = lndiagnostico.ObtenerListadoDisagnostico(usuarioActual.NitEmpresa);

            Ausencia.Diagnostico = new DiagnosticoModel()
            {
                Diagnosticos = diagnostico.Select(d => new SelectListItem()
                {
                    Value = d.IdDiagnostico.ToString(),
                    Text = string.Format("{0} {1}", d.Codigo, d.Descripcion)
                }).ToList()
            };
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("NIT", usuarioActual.NitEmpresa);
            var result = ServiceClient.ObtenerArrayJsonRestFul<EDEmpresa_Usuaria>(urlServicioEmpresas, CapacidadObtenerEmpresasusuarias, RestSharp.Method.GET);
            if (result != null && result.Count() > 0)
            {
                Ausencia.EmpresasUsuarias = result.Select(c => new SelectListItem()
                {
                    Value = c.IdEmpresaUsuaria.ToString(),
                    Text = c.RazonSocial
                }).ToList();
            }
            else
                Ausencia.EmpresasUsuarias = new List<SelectListItem>();

            Ausencia.RazonSocial = usuarioActual.RazonSocialEmpresa;

            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("NIT", usuarioActual.NitEmpresa);
            var resultSede = ServiceClient.ObtenerArrayJsonRestFul<EDSede>(urlServicioEmpresas, CapacidadObtenerSedesPorNit, RestSharp.Method.GET);
            if (resultSede != null && resultSede.Count() > 0)
            {
                Ausencia.Sedes = resultSede.Select(c => new SelectListItem()
                {
                    Value = c.IdSede.ToString(),
                    Text = c.NombreSede
                }).ToList();
            }
            else
                Ausencia.Sedes = new List<SelectListItem>();

            var datos = RenderRazorViewToString("ConsultarAusencia", Ausencia);

            return View();
        }

        /// <summary>
        /// Consulta los datos del usuario a partir de su número de 
        /// documento
        /// </summary>
        /// <param name="numeroDocumento"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ConsultarAusencia(AusenciaModel Ausencia, int tipoVIsta = 0)
        {
            List<AusenciaModel> objAusencias = null;
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);


            EDAusencia edAusencia = new EDAusencia
            {
                Documento = string.IsNullOrEmpty(Ausencia.Documento) ? "0" : Ausencia.Documento,

                strFechaInicio = string.IsNullOrEmpty(Ausencia.FechaInicio) ? "" : Ausencia.FechaInicio,
                strFechaFin = string.IsNullOrEmpty(Ausencia.FechaFin) ? "" : Ausencia.FechaFin,
                IdSede = Ausencia.idSede,
                IdDiagnostico = Ausencia.IdDiagnostico,
                IdEmpresaUsuaria = string.IsNullOrEmpty(Ausencia.IdEmpresaUsuaria) ? 0 : Convert.ToInt32(Ausencia.IdEmpresaUsuaria),
                IdEmpresa = usuarioActual.NitEmpresa
            };
            var result = lnausencia.ConsultarDatosAusencia(edAusencia);
            if (result != null)
            {
                objAusencias = result.Select(d => new AusenciaModel()
                {
                    IdAusencia = d.IdAusencias,
                    NombrePersona = d.NombrePersona,
                    Documento = d.Documento,
                    FechaRegistro = string.Format("{0}/{1}/{2}", d.FechaModificacion.Value.Day, d.FechaModificacion.Value.Month, d.FechaModificacion.Value.Year),
                    Departamento = d.Departamento,
                    Municipio = d.Municipio,
                    Sede = d.nombreRegional,
                    Tipo = d.TipoRegistro,
                    FechaInicio = string.Format("{0}/{1}/{2}", d.fechainicio.Day, d.fechainicio.Month, d.fechainicio.Year),
                    FechaFin = string.Format("{0}/{1}/{2}", d.fechafin.Day, d.fechafin.Month, d.fechafin.Year),
                    Contingencia = new ContingeciaModel()
                    {
                        IdContingenciaSeleccionada = 0,
                        TipoContingencia = d.Detalle
                    },
                    DiasAusencia = ObtenerValorConformato(d.diasausencia.ToString()),
                    Diagnostico = new DiagnosticoModel()
                    {
                        TipoDiagnostico = d.Descripcion
                    },
                    Costo = d.costo.ToString(),
                    FactorPrestacional = d.FactorPrestacional.ToString(),
                    Observaciones = d.Observaciones
                }).ToList();
            }

            if (objAusencias != null)
            {
                var datos = string.Empty;
                if (tipoVIsta == 0)
                {
                    datos = RenderRazorViewToString("_DetallesAusencias", objAusencias);
                    if (objAusencias.Count > 0)
                        return Json(new { Data = datos, Mensaje = "Success" });
                    else
                        return Json(new { Data = datos, Mensaje = "" });
                }
                else
                {
                    if (objAusencias.Count < 1)
                        return Json(new { Data = "El afiliado aun no presenta registros de ausetnismo, para generar un registro de ausencia seleccecione la opcion Si.", Mensaje = "Fail" });
                    else
                    {
                        datos = RenderRazorViewToString("_ProrrogasAusencias", objAusencias);
                        return Json(new { Data = datos, Mensaje = "Success" });
                    }
                }
            }
            else
                return Json(new { Data = "El afiliado aun no presenta registros de ausetnismo", Mensaje = "Fail" });
        }

        public JsonResult GenerarExcelAusencias(AusenciaModel Ausencia, int tipoVIsta = 0)
        {
            TempData["Informeausen"] = Ausencia;
            return Json(new { Data = "/DescargarExcelAusencias", Mensaje = "Success" });

        }

        public FileResult DescargarExcelAusencias()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            AusenciaModel Ausmodel = new AusenciaModel();
            if (TempData["Informeausen"] != null)
            {
                Ausmodel = (AusenciaModel)TempData["Informeausen"];
            }

            EDAusencia edAusencia = new EDAusencia
            {
                Documento = string.IsNullOrEmpty(Ausmodel.Documento) ? "0" : Ausmodel.Documento,
                strFechaInicio = string.IsNullOrEmpty(Ausmodel.FechaInicio) ? "" : Ausmodel.FechaInicio,
                strFechaFin = string.IsNullOrEmpty(Ausmodel.FechaFin) ? "" : Ausmodel.FechaFin,
                IdSede = Ausmodel.idSede,
                IdDiagnostico = Ausmodel.IdDiagnostico,
                IdEmpresaUsuaria = string.IsNullOrEmpty(Ausmodel.IdEmpresaUsuaria) ? 0 : Convert.ToInt32(Ausmodel.IdEmpresaUsuaria),
                IdEmpresa = usuarioActual.NitEmpresa

            };

            var bufer = lnausencia.GenararExcelConsultaAusencias(edAusencia);

            return File(bufer, "application/vnd.ms-excel", "Informe Ausencias.xlsx");
        }


        [HttpPost]
        public ActionResult Prorrogar(AusenciaModel objprorroga)
        {
            if (objprorroga != null)
            {
                var Prorroga = new EDAusencia();
                Prorroga.IdAusencia = objprorroga.IdAusencia;
                Prorroga.FechaInicio = DateTime.ParseExact(objprorroga.FechaInicio, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None);
                Prorroga.FechaFin = DateTime.ParseExact(objprorroga.FechaFin, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None);
                Prorroga.DiasAusencia = Convert.ToInt32(objprorroga.DiasAusencia);
                Prorroga.Costo = Convert.ToInt32(objprorroga.Costo, CultureInfo.InvariantCulture);
                Prorroga.FactorPrestacional = Convert.ToDecimal(objprorroga.FactorPrestacional);
                var result = lnausencia.ProrrogarAusencia(Prorroga);
                if (result != null)
                    if (result.Result.Equals("OK"))
                        return Json(new { status = "Success", Message = "La información se actualizó con éxito." });
                    else if (result.Result.Equals("CRUCE"))
                        return Json(new { status = "CRUCE", Message = "El afiliado ya presenta ausetimos registrado en las fechas ingresadas." });
                    else
                        return Json(new { status = "Error", Message = "No fue posible registrar la prorroga. Intente nuevamente." });
                else
                    return Json(new { status = "Error", Message = "No fue posible registrar la prorroga. Intente nuevamente." });
            }
            else
                return Json(new { status = "Error", Message = "No fue posible registrar la prorroga. Intente nuevamente." });
        }

        #region Alertas de Ausencias


        #region cargue masivo
        public ActionResult CargueMasivo()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            var ausenccia = new AusenciaModel();
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("NIT", usuarioActual.NitEmpresa);
            var result = ServiceClient.ObtenerArrayJsonRestFul<EDEmpresa_Usuaria>(urlServicioEmpresas, CapacidadObtenerEmpresasusuarias, RestSharp.Method.GET);
            if (result != null && result.Count() > 0)
            {
                ausenccia.EmpresasUsuarias = result.Select(c => new SelectListItem()
                {
                    Value = c.IdEmpresaUsuaria.ToString(),
                    Text = c.RazonSocial
                }).ToList();
            }
            else
                ausenccia.EmpresasUsuarias = new List<SelectListItem>();

            ausenccia.RazonSocial = usuarioActual.RazonSocialEmpresa;

            return View(ausenccia);
        }

        [HttpPost]
        public JsonResult ObtenerPlantilla()
        {
            return Json(new { Data = "", Mensaje = "Success" });
        }


        public FileResult DescargarPlantilla()
        {
            var objEvaluacion = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            ServiceClient.EliminarParametros();
            //ServiceClient.AdicionarParametro("NIT", "860123006");
            ServiceClient.AdicionarParametro("NIT", objEvaluacion.NitEmpresa);
            //se consume el servicio post para guardar la información de la evaluación inicial
            var result = ServiceClient.ObtenerObjetoJsonRestFul<byte[]>(urlServicioPlanificacion, CapacidadDescaragarplatillaausentismo, RestSharp.Method.GET);

            return File(result, "application/vnd.ms-excel", "PlantillaAusentismo.xlsx");
        }

        [HttpPost]
        public ActionResult CargueMasivo(object form_data)
        {
            var objEvaluacion = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var pic = System.Web.HttpContext.Current.Request.Files["cargarArchivo"];
                var idempresausuaria = System.Web.HttpContext.Current.Request.Params["IdEmpresaUsuaria"];
                HttpPostedFileBase file = new HttpPostedFileWrapper(pic);
                if (file.FileName.EndsWith("xls") || file.FileName.EndsWith("xlsx"))
                {
                    var fileName = Path.GetFileName(file.FileName);
                    string path = string.Empty;
                    if (int.Parse(idempresausuaria) > 0)
                        path = Path.Combine(rutaPlantillaAusentismo, objEvaluacion.NitEmpresa, idempresausuaria);
                    else
                        path = Path.Combine(rutaPlantillaAusentismo, objEvaluacion.NitEmpresa);

                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);

                    path = Path.Combine(path, fileName);
                    if (System.IO.File.Exists(path))
                        System.IO.File.Delete(path);
                    file.SaveAs(path);

                    EDCargueMasivo cargue = new EDCargueMasivo();
                    cargue.Id_Empresa_Usuaria = int.Parse(idempresausuaria);
                    cargue.path = path;
                    cargue.NitEmpresa = objEvaluacion.NitEmpresa;

                    ServiceClient.EliminarParametros();
                    var result = ServiceClient.RealizarPeticionesPostJsonRestFul<EDCargueMasivo>(urlServicioPlanificacion, CapacidadCargarplatillaausentismo, cargue);
                    if (result != null)
                    {
                        if (result.Message.Equals("OK"))
                            return Json(new { Data = "El registro del ausentismo laboral fue cargado satisfactoriamente.", Mensaje = "Success" });
                        else
                            return Json(new { Data = result.Message, Mensaje = "ERROR" });
                    }
                    else
                        return Json(new { Data = "Se presento un error de comunicacion con el servidor; por favor intente nuevamente o comuniquese con el administrador del sistema.", Mensaje = "ERROR" });

                }
                else
                {
                    return Json(new { Data = "Debe seleccionar un archivo en formato excel con extencion .xls o .xlsx", Mensaje = "ERROR" });
                }
            }
            else
                return Json(new { Data = "Se presento un error en la carga del archivo; por favor intente nuevamente o comuniquese con el administrador del sistema.", Mensaje = "ERROR" });

        }

        #endregion

        /// <summary>
        /// Alertas de Ausentismo.
        /// Visualiza una alerta de ausentismo en rangos de días de 60 a 120 y mayores a 120 días.
        /// </summary>
        /// <returns></returns>
        /// <remarks>Caso de uso 22.5</remarks>
        public ActionResult AlertarAusencia()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);

            // Llenar la información que requiere el modelo.
            var Ausencia = new AusenciaModel()
            {
                IdEmpresa = ObtenerEmpresaIdUsuario(),
                RazonSocial = ObtenerEmpresaRazonSocialUsuario(),
                EmpresasUsuarias = ObtenerListaEmpresasUsuarias(),
                Anios = (new AlertasModel()).ConfigurarAnios()
            };

            // Dejar siempre seleccionado el año más reciente.
            Ausencia.AnioSeleccionado = Convert.ToInt32(Ausencia.Anios.First().Value);

            return View(Ausencia);
        }

        /// <summary>
        /// Ejecutar la consulta de alertas de ausencias.
        /// Retorna la lista de las ausencias sumarizadas en dos grupos: 
        /// (60 &lt;= x &lt;= 120) y (x &gt; 120)
        /// </summary>
        /// <param name="anioGestion"></param>
        /// <param name="idEmpresaUsuaria"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult AlertarAusenciaConsultar(int anioGestion, int idEmpresaUsuaria)
        {
            string HTML = string.Empty;
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            try
            {
                var Resultado = lnausencia.ConsultarAlertaAusencia(
                    new EDAlertaAusentismoParametros
                    {
                        AnioGestion = anioGestion,
                        IdEmpresaUsuaria = usuarioActual.IdEmpresa
                    });

                if (Resultado.Count > 0)
                {
                    HTML = RenderRazorViewToString("AlertarAusenciaConsultar", Resultado);
                    return Json(new { Data = HTML, Mensaje = "Success" });
                }
                else
                    return Json(new { Data = "", Mensaje = "Fail" });

            }
            catch (Exception)
            {
                return Json(new { Data = "", Mensaje = "Fail" });
            }

        }

        #endregion

        #region Configurar dias

        public ActionResult DiasLaborables()
        {
            var objEvaluacion = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            List<EDDiasLaborables> Dias = lnausencia.ConsultarDiasLaborables(objEvaluacion.NitEmpresa);
            DiasLaborables dias = new Models.Ausentismo.DiasLaborables();
            dias.LtsDiasLaborables = Dias.Select(d => new SelectListItem()
            {
                Value = d.IdDiaLaborable.ToString(),
                Text = d.Dia,
                Selected = d.Seleccionado
            }).ToList();

            return View(dias);
        }

        [HttpPost]
        public ActionResult DiasLaborables(DiasLaborables configuracion)
        {
            DiasLaborables dias = new Models.Ausentismo.DiasLaborables();
            if (configuracion.idSeleccionado > 0)
            {
                var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
                bool result = lnausencia.GuardarDiasLaborables(usuarioActual.NitEmpresa, configuracion.idSeleccionado);
                if (result)
                    ViewBag.Mensaje = "El proceso se ejecuto correctamente.";
                else
                    ViewBag.MensajeError = "El proceso ha fallado, por favor intente mas tarde.";
            }
            else
                ViewBag.MensajeError = "Debe seleccionar una de las opciones.";

            var objEvaluacion = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            List<EDDiasLaborables> Dias = lnausencia.ConsultarDiasLaborables(objEvaluacion.NitEmpresa);            
            dias.LtsDiasLaborables = Dias.Select(d => new SelectListItem()
            {
                Value = d.IdDiaLaborable.ToString(),
                Text = d.Dia,
                Selected = d.Seleccionado
            }).ToList();

            return View(dias);            
        }

        #endregion

        #region Métodos comunes

        /// <summary>
        /// Retorna la razón social de la empresa del usuario.
        /// </summary>
        /// <returns></returns>
        string ObtenerEmpresaRazonSocialUsuario()
        {
            var Usuario = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            return Usuario == null ? string.Empty : Usuario.RazonSocialEmpresa;
        }

        /// <summary>
        /// Retorna el id de la empresa del usuario.
        /// </summary>
        /// <returns></returns>
        string ObtenerEmpresaIdUsuario()
        {
            var Usuario = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            return Usuario == null ? string.Empty : Usuario.NitEmpresa;
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

        #endregion

        //Modificación de código INICIO
        //Título: json para eliminar ausencia o prórroga
        //Creado: 2/02/2018
        //Autor: Javier García <javier.garcia@kerocorp.com> - Kerocorp
        //Descripción: JSON que verifica y elimina la ausencia
        [HttpPost]
        public JsonResult EliminarAusencia(string IdAus)
        {
            bool probar = false;
            string resultado = "El registro de ausentismo no se ha eliminado";

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                resultado = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
            }

            int IdElemento = 0;
            bool probarNumero = int.TryParse(IdAus, out IdElemento);
            if (probarNumero && IdElemento!=0)
            {
                EDAusencia EDAusencia = lnausencia.ConsultarAusenciaEliminar(usuarioActual.NitEmpresa, IdElemento);
                if (EDAusencia!=null && EDAusencia.IdAusencia!=0)
                {
                    //Examinar si es padre
                    if (EDAusencia.consecutivoPadre!=0)
                    {
                        bool eliminacion = lnausencia.EliminarAusencia(usuarioActual.NitEmpresa, IdElemento);
                        if (eliminacion)
                        {
                            probar = true;
                            resultado = "El registro de esta prórroga de austentismo se ha eliminado correctamente";
                            return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            resultado = "Ha ocurrido un error en el proceso de eliminación, por favor vuelva a intentar";
                            return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        //Examinar si tiene prórrogas
                        List<EDAusencia> ListaProrrogas= lnausencia.ConsultarAusenciaProrrogas(usuarioActual.NitEmpresa, IdElemento);
                        if (ListaProrrogas!=null)
                        {
                            if (ListaProrrogas.Count>0)
                            {
                                resultado = "No se ha eliminado este registro por que tiene prórrogas registradas, por favor elimine todas las prórrogas de este registro y vuelva a intentar";
                                return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                bool eliminacion = lnausencia.EliminarAusencia(usuarioActual.NitEmpresa, IdElemento);
                                if (eliminacion)
                                {
                                    probar = true;
                                    resultado = "El registro de esta prórroga de austentismo se ha eliminado correctamente";
                                    return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    resultado = "Ha ocurrido un error en el proceso de eliminación, por favor vuelva a intentar";
                                    return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                        else
                        {
                            bool eliminacion = lnausencia.EliminarAusencia(usuarioActual.NitEmpresa, IdElemento);
                            if (eliminacion)
                            {
                                probar = true;
                                resultado = "El registro de esta prórroga de austentismo se ha eliminado correctamente";
                                return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                resultado = "Ha ocurrido un error en el proceso de eliminación, por favor vuelva a intentar";
                                return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                }
                else
                {
                    resultado = "El registro no se ha eliminado compruebe que el registro exista, es posible que haya sido eliminado con anterioridad";
                    return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
                }
            }
            
            return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
        }
        //Modificación de código FIN
    }
}

