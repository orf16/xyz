using RestSharp;
using SG_SST.Audotoria;
using SG_SST.Controllers.Base;
using SG_SST.EntidadesDominio.Empresas;
using SG_SST.Logica.Empresas;
using SG_SST.Models.ReporteIncidente;
using SG_SST.Recursos.Incidente;
using SG_SST.ServiceRequest;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace SG_SST.Controllers.Empresas
{
    public class IncidenteController : BaseController
    {
        LNIncidente lnincidente = new LNIncidente();
        RegistraLog registroLog = new RegistraLog();

        string urlServicioEmpresas = ConfigurationManager.AppSettings["UrlServicioEmpresas"];
        string CapacidadObtenerSedesPorNit = ConfigurationManager.AppSettings["CapacidadObtenerSedesPorNit"];
        string CapacidadObtenerSedesPorIdSede = ConfigurationManager.AppSettings["CapacidadObtenerSedesPorIdSede"];
        string UrlServicioIncidenteInvestigacion = ConfigurationManager.AppSettings["UrlServicioIncidenteInvestigacion"];
        string CapacidadGuardarIncidente = ConfigurationManager.AppSettings["CapacidadGuardarIncidente"];
        string CapacidadConsultarIncidente = ConfigurationManager.AppSettings["CapacidadConsultarIncidente"];
        string CapacidadDescargarExcelIncidente = ConfigurationManager.AppSettings["CapacidadDescargarExcelIncidente"];
        string CapacidadMunicipiosPorDepto = ConfigurationManager.AppSettings["CapacidadObtenerEmpresaUsuariaMunicipiosXDepartamento"];
        string CapacidadConsultarIncidenteid = ConfigurationManager.AppSettings["CapacidadConsultarIncidenteid"];
        string CapacidadConsultarListas = ConfigurationManager.AppSettings["CapacidadConsultarListas"];
        /// <summary>
        /// Presentar el formulario para generar un nuevo registro de incidente.
        /// </summary>
        /// <returns></returns>
        //public ActionResult Cargar()
        //{
        //    var objUsuarioSesion = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
        //    if (objUsuarioSesion == null)
        //    {
        //        ViewBag.Mensaje = "La sesion a finalizado.";
        //        return RedirectToAction("Login", "Home");
        //    }

        //    //ViewBag.ListaBasicas = lnincidente.ObtenerListasBasicas("1");

        //    var DatosPersona = lnincidente.ConsultarIncidentes(new EDIncidente_Modelo_Consulta
        //    {
        //        PersonaNumeroIdentificacion = "94307617"
        //    });

        //    return View(DatosPersona.First());
        //}

        /// <summary>
        /// Presentar el formulario para ingresar los parámetros de consulta de Incidentes.
        /// </summary>
        /// <returns></returns>
        public ActionResult Consultar()
        {
            var objUsuarioSesion = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (objUsuarioSesion == null)
            {
                ViewBag.Mensaje = "La sesion a finalizado.";
                return RedirectToAction("Login", "Home");
            }

            IncidenteModel incModel = new IncidenteModel();
            #region Listas
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("Nit", incModel.NitEmpresa);
            var Listas = ServiceClient.ObtenerObjetoJsonRestFul<EDIncidente_Listas_Basicas>(UrlServicioIncidenteInvestigacion, CapacidadConsultarListas, RestSharp.Method.GET);
            if (Listas != null)
            {
                incModel.ConsecuenciasIncidente = Listas.IncidenteConsecuencias.Select(ic => new SelectListItem()
                {
                    Value = ic.Pk_Id_Incidente_Consecuencia.ToString(),
                    Text = ic.Nombre_consecuencia
                }).ToList();
                incModel.TiposIncidente = Listas.TiposIncidente.Select(ti => new SelectListItem()
                {
                    Value = ti.Pk_Id_Tipo_Incidente.ToString(),
                    Text = ti.Nombre_Incidente
                }).ToList();
                incModel.SitiosIncidente = Listas.SitiosIncidente.Select(si => new SelectListItem()
                {
                    Value = si.Pk_Id_Sitio_Incidente.ToString(),
                    Text = si.Nombre_Sitio
                }).ToList();

                incModel.LugaresIncidente = incModel.GetLugaresIncidente();
            }

            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("NIT", objUsuarioSesion.NitEmpresa);
            var resultSede = ServiceClient.ObtenerArrayJsonRestFul<EDSede>(urlServicioEmpresas, CapacidadObtenerSedesPorNit, RestSharp.Method.GET);
            if (resultSede != null && resultSede.Count() > 0)
            {
                incModel.Sedes = resultSede.Select(s => new SedeModel() { IdSede = s.IdSede, NombreSede = s.NombreSede }).ToList();
            }

            #endregion

            //var Resultado = lnincidente.ConsultarIncidentes(new EDIncidente_Modelo_Consulta
            //{
            //    PersonaNumeroIdentificacion = "94307617"
            //});

            return View(incModel);
        }

        [HttpPost]
        public JsonResult Consultar(IncidenteModel consultar)
        {
            EDIncidente_Modelo_Consulta parametros = new EDIncidente_Modelo_Consulta();
            var objUsuarioSesion = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (objUsuarioSesion == null)
            {
                ViewBag.Mensaje = "La sesion a finalizado.";
                return Json(new { Data = Recursos.GeneralApp.General.SessionFinalizada, Mensaje = "FinSession" });
            }
            parametros.Nit_Empresa = objUsuarioSesion.NitEmpresa;
            parametros.ConsultarTodo = false;

            if (string.IsNullOrEmpty(consultar.FechaInicial))
                parametros.IncidenteFechaInicial = null;
            else
                parametros.IncidenteFechaInicial = Convert.ToDateTime(consultar.FechaInicial);

            if (string.IsNullOrEmpty(consultar.FechaFinal))
                parametros.IncidenteFechaFinal = null;
            else
                parametros.IncidenteFechaFinal = Convert.ToDateTime(consultar.FechaFinal);
            parametros.PersonaNumeroIdentificacion = string.IsNullOrEmpty(consultar.DocumentoEmpleado) ? "" : consultar.DocumentoEmpleado;

            if (consultar.IdConsecuencia > 0)
                parametros.IncidentePosibleConsecuenciaID = consultar.IdConsecuencia;
            else
                parametros.IncidentePosibleConsecuenciaID = null;

            if (consultar.IdSede > 0)
                parametros.IncidenteSedeID = consultar.IdSede;
            else
                parametros.IncidenteSedeID = null;

            if (consultar.IdTipoIncidente > 0)
                parametros.IncidenteTipoIncidenteID = consultar.IdTipoIncidente;
            else
                parametros.IncidenteTipoIncidenteID = null;

            if (consultar.IdSitioIncidente > 0)
                parametros.IncidenteSitioID = consultar.IdSitioIncidente;
            else
                parametros.IncidenteSitioID = null;

            if (string.IsNullOrEmpty(consultar.idLugarIncidente))
                parametros.IncidenteLugarIncidente = null;
            else
            {
                if (consultar.idLugarIncidente.Equals("1"))
                    parametros.IncidenteLugarIncidente = true;
                else
                    parametros.IncidenteLugarIncidente = false;
            }

            parametros.numPagina = 0;
            parametros.cantidadPorPagina = Convert.ToInt16(ConfigurationManager.AppSettings["CantidadRegistrosPaginaIncidente"]);
            //TempData["EDIncidente_Modelo_Consulta"] = parametros;
            ServiceClient.EliminarParametros();
            var resultIncidentes = ServiceClient.RealizarPeticionesArrayPostJsonRestFul<EDIncidente>(UrlServicioIncidenteInvestigacion, CapacidadConsultarIncidente, parametros);
            if (resultIncidentes != null)
            {
                if (resultIncidentes.Count() > 0)
                {
                    List<IncidenteModel> modelIncidentes = new List<IncidenteModel>();
                    modelIncidentes = resultIncidentes.Select(rs => new IncidenteModel()
                    {
                        Id_Incidente = rs.Pk_Id_Incidente,
                        FechaInicial = string.Format("{0}/{1}/{2}", rs.IncidenteFechaInicial.Day, rs.IncidenteFechaInicial.Month, rs.IncidenteFechaInicial.Year),
                        FechaFinal = string.Format("{0}/{1}/{2}", rs.IncidenteFechaFinal.Day, rs.IncidenteFechaFinal.Month, rs.IncidenteFechaFinal.Year),
                        DocumentoEmpleado = rs.Persona_numero_identificacion,
                        Consecuencia = rs.Incidente_consecuencia.Nombre_consecuencia,
                        NombreSede = rs.General_sede.NombreSede,
                        lugarIncidente = rs.Incidente_ocurre_dentro_empresa ? "Dentro de la empresa" : "Fuera de la empresa",
                        TipoIncidente = rs.Incidente_tipo_incidente.Nombre_Incidente,
                        SitioIncidente = rs.Incidente_sitio_incidente.Nombre_Sitio

                    }).ToList();
                    if ((resultIncidentes[0].cantidadRegistros % parametros.cantidadPorPagina) == 0)
                        ViewBag.PageCount = resultIncidentes[0].cantidadRegistros / parametros.cantidadPorPagina;
                    else
                        ViewBag.PageCount = (resultIncidentes[0].cantidadRegistros / parametros.cantidadPorPagina) + 1;
                    

                    var datos = RenderRazorViewToString("_ConsultaIncidentes", modelIncidentes);
                    return Json(new { Data = datos, Mensaje = "OK" });
                }
                else
                    return Json(new { Data = MensajeIncidente.MsgConsultarInic, Mensaje = "ERROR" });
            }
            else
                return Json(new { Data = MensajeIncidente.MsgConsultarInic, Mensaje = "ERROR" });
        }

        [HttpPost]
        public JsonResult ListaIncidentes(IncidenteModel consultar)
        {
            EDIncidente_Modelo_Consulta parametros = new EDIncidente_Modelo_Consulta();
            var objUsuarioSesion = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (objUsuarioSesion == null)
            {
                ViewBag.Mensaje = "La sesion a finalizado.";
                return Json(new { Data = Recursos.GeneralApp.General.SessionFinalizada, Mensaje = "FinSession" });
            }
            parametros.Nit_Empresa = objUsuarioSesion.NitEmpresa;
            parametros.ConsultarTodo = false;

            if (string.IsNullOrEmpty(consultar.FechaInicial))
                parametros.IncidenteFechaInicial = null;
            else
                parametros.IncidenteFechaInicial = Convert.ToDateTime(consultar.FechaInicial);

            if (string.IsNullOrEmpty(consultar.FechaFinal))
                parametros.IncidenteFechaFinal = null;
            else
                parametros.IncidenteFechaFinal = Convert.ToDateTime(consultar.FechaFinal);
            parametros.PersonaNumeroIdentificacion = string.IsNullOrEmpty(consultar.DocumentoEmpleado) ? "" : consultar.DocumentoEmpleado;

            if (consultar.IdConsecuencia > 0)
                parametros.IncidentePosibleConsecuenciaID = consultar.IdConsecuencia;
            else
                parametros.IncidentePosibleConsecuenciaID = null;

            if (consultar.IdSede > 0)
                parametros.IncidenteSedeID = consultar.IdSede;
            else
                parametros.IncidenteSedeID = null;

            if (consultar.IdTipoIncidente > 0)
                parametros.IncidenteTipoIncidenteID = consultar.IdTipoIncidente;
            else
                parametros.IncidenteTipoIncidenteID = null;

            if (consultar.IdSitioIncidente > 0)
                parametros.IncidenteSitioID = consultar.IdSitioIncidente;
            else
                parametros.IncidenteSitioID = null;

            if (string.IsNullOrEmpty(consultar.idLugarIncidente))
                parametros.IncidenteLugarIncidente = null;
            else
            {
                if (consultar.idLugarIncidente.Equals("1"))
                    parametros.IncidenteLugarIncidente = true;
                else
                    parametros.IncidenteLugarIncidente = false;
            }

            if (consultar.numPagina > 0)
                parametros.numPagina = consultar.numPagina;
            else
                parametros.numPagina = 1;

            parametros.cantidadPorPagina = Convert.ToInt16(ConfigurationManager.AppSettings["CantidadRegistrosPaginaIncidente"]);
            
            ServiceClient.EliminarParametros();
            var resultIncidentes = ServiceClient.RealizarPeticionesArrayPostJsonRestFul<EDIncidente>(UrlServicioIncidenteInvestigacion, CapacidadConsultarIncidente, parametros);
            if (resultIncidentes != null)
            {
                if (resultIncidentes.Count() > 0)
                {
                    List<IncidenteModel> modelIncidentes = new List<IncidenteModel>();
                    modelIncidentes = resultIncidentes.Select(rs => new IncidenteModel()
                    {
                        Id_Incidente = rs.Pk_Id_Incidente,
                        FechaInicial = string.Format("{0}/{1}/{2}", rs.IncidenteFechaInicial.Day, rs.IncidenteFechaInicial.Month, rs.IncidenteFechaInicial.Year),
                        FechaFinal = string.Format("{0}/{1}/{2}", rs.IncidenteFechaFinal.Day, rs.IncidenteFechaFinal.Month, rs.IncidenteFechaFinal.Year),
                        DocumentoEmpleado = rs.Persona_numero_identificacion,
                        Consecuencia = rs.Incidente_consecuencia.Nombre_consecuencia,
                        NombreSede = rs.General_sede.NombreSede,
                        lugarIncidente = rs.Incidente_ocurre_dentro_empresa ? "Dentro de la empresa" : "Fuera de la empresa",
                        TipoIncidente = rs.Incidente_tipo_incidente.Nombre_Incidente,
                        SitioIncidente = rs.Incidente_sitio_incidente.Nombre_Sitio

                    }).ToList();
                    if ((resultIncidentes[0].cantidadRegistros % parametros.cantidadPorPagina) == 0)
                        ViewBag.PageCount = resultIncidentes[0].cantidadRegistros / parametros.cantidadPorPagina;
                    else
                        ViewBag.PageCount = (resultIncidentes[0].cantidadRegistros / parametros.cantidadPorPagina) + 1;                    
                    var datos = RenderRazorViewToString("_ConsultaIncidentes", modelIncidentes);
                    return Json(new { Data = datos, Mensaje = "OK" });
                }
                else
                    return Json(new { Data = MensajeIncidente.MsgConsultarInic, Mensaje = "ERROR" });
            }
            else
                return Json(new { Data = MensajeIncidente.MsgConsultarInic, Mensaje = "ERROR" });
        }



        public JsonResult ObtenerExcel(IncidenteModel consultar)
        {
            EDIncidente_Modelo_Consulta parametros = new EDIncidente_Modelo_Consulta();
            var objUsuarioSesion = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (objUsuarioSesion == null)
            {
                return Json(new { Data = Recursos.GeneralApp.General.SessionFinalizada, Mensaje = "FinSession" });
            }
            parametros.Nit_Empresa = objUsuarioSesion.NitEmpresa;
            parametros.ConsultarTodo = true;

            if (string.IsNullOrEmpty(consultar.FechaInicial))
                parametros.IncidenteFechaInicial = null;
            else
                parametros.IncidenteFechaInicial = Convert.ToDateTime(consultar.FechaInicial);

            if (string.IsNullOrEmpty(consultar.FechaFinal))
                parametros.IncidenteFechaFinal = null;
            else
                parametros.IncidenteFechaFinal = Convert.ToDateTime(consultar.FechaFinal);
            parametros.PersonaNumeroIdentificacion = string.IsNullOrEmpty(consultar.DocumentoEmpleado) ? "" : consultar.DocumentoEmpleado;

            if (consultar.IdConsecuencia > 0)
                parametros.IncidentePosibleConsecuenciaID = consultar.IdConsecuencia;
            else
                parametros.IncidentePosibleConsecuenciaID = null;

            if (consultar.IdSede > 0)
                parametros.IncidenteSedeID = consultar.IdSede;
            else
                parametros.IncidenteSedeID = null;

            if (consultar.IdTipoIncidente > 0)
                parametros.IncidenteTipoIncidenteID = consultar.IdTipoIncidente;
            else
                parametros.IncidenteTipoIncidenteID = null;

            if (consultar.IdSitioIncidente > 0)
                parametros.IncidenteSitioID = consultar.IdSitioIncidente;
            else
                parametros.IncidenteSitioID = null;

            if (string.IsNullOrEmpty(consultar.idLugarIncidente))
                parametros.IncidenteLugarIncidente = null;
            else
            {
                if (consultar.idLugarIncidente.Equals("1"))
                    parametros.IncidenteLugarIncidente = true;
                else
                    parametros.IncidenteLugarIncidente = false;
            }

            TempData["consultaincidente"] = parametros;
            return Json(new { Data = "../Incidente/DescargarExcelIncidentes", Mensaje = "OK" });
        }

        public FileResult DescargarExcelIncidentes()
        {
            EDIncidente_Modelo_Consulta parametros = new EDIncidente_Modelo_Consulta();
            if (TempData["consultaincidente"] != null)
            {
                parametros = (EDIncidente_Modelo_Consulta)TempData["consultaincidente"];
            }

            ServiceClient.EliminarParametros();
            var resultIncidentes = ServiceClient.ObtenerObjetoJsonRestFul<byte[]>(UrlServicioIncidenteInvestigacion, CapacidadDescargarExcelIncidente, parametros);
            return File(resultIncidentes, "application/vnd.ms-excel", "Incidentes.xlsx");
        }

        /// <summary>
        /// Comenzar un nuevo reporte en blanco.
        /// </summary>
        /// <returns></returns>
        public ActionResult Reportar()
        {
            var objUsuarioSesion = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (objUsuarioSesion == null)
            {
                ViewBag.Mensaje = "La sesion a finalizado.";
                return RedirectToAction("Login", "Home");
            }

            int resultadoEmp = 0;
            int resultadoAfil = 0;
            IncidenteModel incModel = new IncidenteModel();

            var modelo = ConsultarAfiliadoEmpresaActivos(objUsuarioSesion.SiglaTipoDocumentoEmpresa, objUsuarioSesion.NitEmpresa, objUsuarioSesion.SiglaTipoDocumentoEmpleado, objUsuarioSesion.Documento, out resultadoEmp, out resultadoAfil);

            #region I. Identificación general del empleador
            incModel.ActividadEconomica = modelo.actividadEconomica;
            incModel.CodActividadEconomica = modelo.idActividadEconomica;
            incModel.RazonSocial = modelo.razonSocial;
            incModel.TipoDocumentoEmpresa = modelo.tipoDocEmp;
            incModel.NitEmpresa = modelo.documentoEmp;
            incModel.EsSedePrincipal = true;
            incModel.DireccionEmpresa = modelo.dirEmpresa;
            incModel.TelefonoEmpresa = modelo.telefonoEmpresa;
            incModel.CorreoElectronico = modelo.emailEmpresa;
            incModel.DepartamentoEmp = modelo.nomDepEmpresa;
            incModel.MunicipioEmp = modelo.nomMunEmpresa;
            incModel.ZonaEmpresa = modelo.idZona;
            #endregion

            #region II. Información de la persona que reporta el incidente
            incModel.VinculacionLab = modelo.nomVinLaboral;
            incModel.TipoDocumentoEmpleado = modelo.tipoDoc;
            incModel.DocumentoEmpleado = modelo.idPersona;
            incModel.PrimerApellido = modelo.apellido1;
            incModel.SegundoApellido = modelo.apellido2;
            incModel.PrimerNombre = modelo.nombre1;
            incModel.SegundNombre = modelo.nombre2;
            incModel.FechaNacimiento = modelo.fechaNacimiento != null ? Convert.ToDateTime(modelo.fechaNacimiento) : incModel.FechaNacimiento;
            incModel.Genero = modelo.sexoPersona;
            incModel.DireccionEmpleado = modelo.dirPersona.Equals("N/A") ? "" : modelo.dirPersona;
            incModel.TelefonoEmpleado = modelo.telPersona;
            incModel.DepartamentoEmpleado = modelo.nomDepAfiliado;
            incModel.MunicipioEmpleado = modelo.nomMunAfiliado;
            incModel.IdOcupacion = modelo.idOcupacion;
            incModel.Ocupacion = modelo.ocupacion;
            incModel.FechaIngreso = modelo.fechaInicioVinculacion != null ? Convert.ToDateTime(modelo.fechaInicioVinculacion) : incModel.FechaIngreso;
            #endregion

            #region II. Información del incidente
            incModel.RealizabaLaborHabitual = true;
            incModel.EsDentroEmpresa = true;
            incModel.FechaCreacionIncidente = DateTime.Now;

            #endregion

            #region Listas
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("Nit", incModel.NitEmpresa);
            var Listas = ServiceClient.ObtenerObjetoJsonRestFul<EDIncidente_Listas_Basicas>(UrlServicioIncidenteInvestigacion, CapacidadConsultarListas, RestSharp.Method.GET);
            if (Listas != null)
            {
                incModel.TiposDeDocumento = Listas.TiposDocumento.Select(tp => new SelectListItem()
                {
                    Value = tp.Id_Tipo_Documento.ToString(),
                    Text = tp.Sigla
                }).ToList();
                incModel.Departamentos = Listas.Departamentos.Select(dept => new SelectListItem()
                {
                    Value = dept.Pk_Id_Departamento.ToString(),
                    Text = dept.Nombre_Departamento
                }).ToList();
                incModel.Sedes = new List<SedeModel>();

                incModel.Zonas = Listas.Zonas.Select(z => new SelectListItem()
                {
                    Value = z.PK_ZonaLugar.ToString(),
                    Text = z.Descripcion_ZonaLugar
                }).ToList();
                incModel.VinculacionLaboral = Listas.VinculacionLaboral.Select(v => new SelectListItem()
                {
                    Value = v.PK_VinculacionLaboral.ToString(),
                    Text = v.Descripcion_VinculacionLaboral
                }).ToList();
                incModel.Jornadas = Listas.Jornadas.Select(j => new SelectListItem()
                {
                    Value = j.Pk_Id_Tipo_Jornada.ToString(),
                    Text = j.Nombre_Jornada
                }).ToList();
                incModel.DiasSemana = Listas.DiasSemana.Select(d => new SelectListItem()
                {
                    Value = d.ToString(),
                    Text = d.ToString()
                }).ToList();
                incModel.TiposIncidente = Listas.TiposIncidente.Select(ti => new SelectListItem()
                {
                    Value = ti.Pk_Id_Tipo_Incidente.ToString(),
                    Text = ti.Nombre_Incidente
                }).ToList();
                incModel.SitiosIncidente = Listas.SitiosIncidente.Select(si => new SelectListItem()
                {
                    Value = si.Pk_Id_Sitio_Incidente.ToString(),
                    Text = si.Nombre_Sitio
                }).ToList();
                incModel.ConsecuenciasIncidente = Listas.IncidenteConsecuencias.Select(ic => new SelectListItem()
                {
                    Value = ic.Pk_Id_Incidente_Consecuencia.ToString(),
                    Text = ic.Nombre_consecuencia
                }).ToList();
                incModel.Municipios = new List<SelectListItem>();
            }

            #endregion


            return View(incModel);
        }

        [HttpPost]
        public JsonResult GuardarIncidente(IncidenteModel datosIncidente)
        {
            var objUsuarioSesion = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (objUsuarioSesion == null)
            {
                ViewBag.Mensaje = "La sesion a finalizado.";
                return Json(new { Data = Recursos.GeneralApp.General.SessionFinalizada, Mensaje = "FinSession" });
                //return RedirectToAction("Login", "Home");
            }

            EDIncidente edIncidente = new EDIncidente();

            #region I. Identificación general del empleador
            edIncidente.Persona_usuario_sistema = new EntidadesDominio.Usuario.EDUsuarioSistema();
            edIncidente.Persona_usuario_sistema.IdUsuarioSistema = objUsuarioSesion.IdUsuario;
            edIncidente.General_actividad_economica = datosIncidente.ActividadEconomica;
            edIncidente.General_codigo = datosIncidente.CodActividadEconomica.ToString();
            edIncidente.FK_id_tipo_documento_general = datosIncidente.IdTipoDocumentoEmpresa;
            edIncidente.General_numero_identificación = datosIncidente.NitEmpresa;
            edIncidente.General_sede_principal_direccion = datosIncidente.DireccionEmpresa;
            edIncidente.General_sede_principal_telefono = datosIncidente.TelefonoEmpresa;
            edIncidente.General_correo_electronico = datosIncidente.CorreoElectronico;
            edIncidente.General_sede_principal_municipio_id = datosIncidente.IdMunicipioEmp;
            edIncidente.General_sede_principal_zona = datosIncidente.IdZonaEmpresa == 1 ? "Urbano" : "Rural";
            edIncidente.General_sede_principal_zona_id = datosIncidente.IdZonaEmpresa;
            edIncidente.General_mismos_datos_sede_principal = datosIncidente.EsSedePrincipal;
            if (!datosIncidente.EsSedePrincipal)
            {
                edIncidente.General_sede = new EDSede();
                edIncidente.General_sede.IdSede = datosIncidente.IdSede;
                edIncidente.General_sede.DireccionSede = datosIncidente.DireccionSede;
                edIncidente.General_sede.Telefono = datosIncidente.TelefonoSede;
                edIncidente.General_sede.IdMunicipio = datosIncidente.IdMunicipioSede;
                edIncidente.General_sede.Sector = datosIncidente.IdZonaSede == 1 ? "Urbano" : "Rural";
            }
            #endregion
            #region II. Información de la persona que reporta el incidente
            edIncidente.FK_id_vinculacionlaboral_persona = datosIncidente.IdVinculacionLab;
            edIncidente.FK_id_tipo_documento_persona = datosIncidente.IdTipoDocumentoEmpleado;
            edIncidente.Persona_numero_identificacion = datosIncidente.DocumentoEmpleado;
            edIncidente.Persona_primer_apellido = datosIncidente.PrimerApellido;
            edIncidente.Persona_segundo_apellido = datosIncidente.SegundoApellido;
            edIncidente.Persona_primer_nombre = datosIncidente.PrimerNombre;
            edIncidente.Persona_segundo_nombre = datosIncidente.SegundNombre;
            edIncidente.Persona_fecha_nacimiento = datosIncidente.FechaNacimiento;
            edIncidente.Persona_genero = datosIncidente.Genero;
            edIncidente.Persona_direccion = datosIncidente.DireccionEmpleado;
            edIncidente.Persona_telefono = datosIncidente.TelefonoEmpleado;
            edIncidente.Persona_departamento_id = datosIncidente.IdDepartamentoEmpleado;
            edIncidente.Persona_municipio_id = datosIncidente.IdMunicipioEmpleado;
            edIncidente.FK_id_zonalugar_persona = datosIncidente.IdZonaEmpleado;
            edIncidente.Persona_ocupacion_habitual = datosIncidente.Ocupacion;
            edIncidente.Persona_fecha_ingreso_empresa = datosIncidente.FechaIngreso;
            edIncidente.FK_id_jornada_habitual_persona = datosIncidente.IdJornadaHabitual;
            #endregion

            edIncidente.Incidente_fecha = datosIncidente.fechaIncidente;
            edIncidente.strIncidente_fecha = datosIncidente.fechaIncidente.ToString();
            edIncidente.Incidente_dia_semana = datosIncidente.DiaSemanaIncidente;
            edIncidente.Incidente_jornada_normal = datosIncidente.EsJornadaNormal;
            edIncidente.Incidente_realizaba_labor_habitual = datosIncidente.IdJornadaHabitual == 1 ? true : false;
            edIncidente.Incidente_nombre_labor = datosIncidente.DescripcionLabor;
            edIncidente.Incidente_tiempo_previo_al_incidente = datosIncidente.TiempoPrevioIncidente;
            edIncidente.FK_id_incidente_tipo_incidente = datosIncidente.IdTipoIncidente;
            edIncidente.FK_id_departamento_incidente = datosIncidente.IdDepartamentoIncidente;
            edIncidente.FK_id_municipio_incidente = datosIncidente.IdMunicipioIncidente;
            edIncidente.FK_id_zonalugar_incidente = datosIncidente.IdZonaIncidente;
            edIncidente.Incidente_ocurre_dentro_empresa = datosIncidente.EsDentroEmpresa;
            edIncidente.FK_id_sitio_incidente = datosIncidente.IdSitioIncidente;
            edIncidente.Incidente_sitio_incidente_otro = datosIncidente.OtroSitio;
            edIncidente.FK_id_consecuencia_incidente = datosIncidente.IdConsecuencia;
            edIncidente.Incidente_descripcion = datosIncidente.DescripcionIncidente;
            edIncidente.Incidente_fecha_diligenciamiento = datosIncidente.FechaCreacionIncidente;
            edIncidente.Incidente_realizaba_labor_habitual = datosIncidente.RealizabaLaborHabitual;

            ServiceClient.EliminarParametros();
            var resultIncidente = ServiceClient.RealizarPeticionesPostJsonRestFul<EDIncidente>(UrlServicioIncidenteInvestigacion, CapacidadGuardarIncidente, edIncidente);
            if (resultIncidente.Pk_Id_Incidente > 0)
            {
                return Json(new { Datos = MensajeIncidente.MsgIncidenteOK, Mensaje = "OK" });
            }
            else
                return Json(new { Datos = MensajeIncidente.MgsIncidenteError, Mensaje = "ERROR" });
        }

        [HttpPost]
        public JsonResult ConsultarSedes()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return Json(new { Data = Recursos.GeneralApp.General.SessionFinalizada, Mensaje = "FinSession" });
            }
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("NIT", usuarioActual.NitEmpresa);
            var resultSede = ServiceClient.ObtenerArrayJsonRestFul<EDSede>(urlServicioEmpresas, CapacidadObtenerSedesPorNit, RestSharp.Method.GET);
            if (resultSede[0] != null && resultSede.Count() > 0)
            {
                return Json(new { Datos = resultSede, Mensaje = "OK" });
            }
            else
                return Json(new { Datos = MensajeIncidente.MsgNoTieneSedes, Mensaje = "ERROR" });
        }

        [HttpPost]
        public JsonResult ConsultarSedePorId(int idSede)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return Json(new { Data = Recursos.GeneralApp.General.SessionFinalizada, Mensaje = "FinSession" });
            }
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("IdSede", idSede);
            var resultSede = ServiceClient.ObtenerObjetoJsonRestFul<EDSede>(urlServicioEmpresas, CapacidadObtenerSedesPorIdSede, RestSharp.Method.GET);
            if (resultSede != null)
            {
                return Json(new { Datos = resultSede, Mensaje = "OK" });
            }
            else
                return Json(new { Datos = MensajeIncidente.MsgNoTieneSedes, Mensaje = "ERROR" });
        }

        /// <summary>
        /// Consulta la información de la empresa y el afiliado a SIARP
        /// para verficar que dicha empresa y afiliado existan y se encuentren
        /// en estado afiliado.
        /// </summary>
        /// <param name="tipoDocumentoEmp"></param>
        /// <param name="numDocumentoEmp"></param>
        /// <param name="tipoDocumento"></param>
        /// <param name="numDucumento"></param>
        /// <param name="resultadoAfi"></param>
        /// <returns></returns>
        private Incidente ConsultarAfiliadoEmpresaActivos(string tipoDocumentoEmp, string numDocumentoEmp, string tipoDocumento, string numDucumento, out int resultadoEmp, out int resultadoAfi)
        {
            try
            {
                Incidente objEmpresaAfi = null;
                //variable para manejar el resultado: 0: No existe la empresa,
                //1: Existe pero se encuentra inactiva, 2: Existe y se encuentra activa
                //3: No existe el afiliado, 4: Existe el afiliado pero se encuentra inactivo
                //5: Existe el afiliado y se encuentra activo.
                resultadoEmp = (int)Enumeraciones.EnumAdministracionUsuarios.ValidaEstadoEmpAfi.NoExisteEmp;
                resultadoAfi = (int)Enumeraciones.EnumAdministracionUsuarios.ValidaEstadoEmpAfi.NoExisteAfi;
                var cliente = new RestSharp.RestClient(ConfigurationManager.AppSettings["Url"]);
                var request = new RestRequest(consultaAfiliadoEmpresaActivo, RestSharp.Method.GET);
                request.RequestFormat = DataFormat.Xml;
                request.Parameters.Clear();
                request.AddParameter("tpEm", tipoDocumentoEmp);
                request.AddParameter("docEm", numDocumentoEmp);
                request.AddParameter("tpAfiliado", tipoDocumento);
                request.AddParameter("docAfiliado", numDucumento);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Accept", "application/json");

                //se omite la validación de certificado de SSL
                ServicePointManager.ServerCertificateValidationCallback = delegate
                { return true; };
                IRestResponse<List<Incidente>> response = cliente.Execute<List<Incidente>>(request);
                var result = response.Content;
                if (!string.IsNullOrWhiteSpace(result))
                {
                    var respuesta = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Incidente>>(result);
                    if (respuesta.Count == 0)
                        resultadoEmp = (int)Enumeraciones.EnumAdministracionUsuarios.ValidaEstadoEmpAfi.NoExisteEmp; //No existe
                    var EmpresaSystem = respuesta.Where(a => a.estadoEmpresa.ToUpper().Equals("ACTIVA")).FirstOrDefault();
                    if (EmpresaSystem == null)
                        resultadoEmp = (int)Enumeraciones.EnumAdministracionUsuarios.ValidaEstadoEmpAfi.ExisteInactivoEmp; //Existe y está Inactiva
                    else
                        resultadoEmp = (int)Enumeraciones.EnumAdministracionUsuarios.ValidaEstadoEmpAfi.ExisteActivoEmp; //Existe y está Activa
                    var AfilSystem = respuesta.Where(a => a.estadoPersona.ToUpper().Equals("ACTIVO")).FirstOrDefault();
                    if (AfilSystem == null)
                        resultadoAfi = (int)Enumeraciones.EnumAdministracionUsuarios.ValidaEstadoEmpAfi.ExisteInactivoAfi; //Existe y está Inactivo
                    else
                        resultadoAfi = (int)Enumeraciones.EnumAdministracionUsuarios.ValidaEstadoEmpAfi.ExisteActivoAfi; //Existe y está Activo

                    if (EmpresaSystem != null && AfilSystem != null)
                        objEmpresaAfi = AfilSystem;
                }
                return objEmpresaAfi;
            }
            catch (Exception ex)
            {
                registroLog.RegistrarError(typeof(IncidenteController), string.Format("Error en la Acción ConsultarAfiliadoEmpresaActivos: {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                resultadoEmp = (int)Enumeraciones.EnumAdministracionUsuarios.ValidaEstadoEmpAfi.NoExisteEmp;
                resultadoAfi = (int)Enumeraciones.EnumAdministracionUsuarios.ValidaEstadoEmpAfi.NoExisteAfi;
                return null;
            }

        }

        [HttpPost]
        public JsonResult PrepararDescarga(IncidenteModel datosIncidente, int imprimir)
        {
            TempData["datosIncidente"] = datosIncidente;
            if (imprimir == 1)
                return Json(new { Datos = "../Incidente/Descargar", Mensaje = "OK" });
            else
                return Json(new { Datos = "../Incidente/Imprimir", Mensaje = "OK" });
        }

        public ActionResult Imprimir()
        {
            IncidenteModel incModel = new Models.ReporteIncidente.IncidenteModel();
            if (TempData["datosIncidente"] != null)
            {
                incModel = (IncidenteModel)TempData["datosIncidente"];
            }

            ConstruirModelInicdente(incModel, false);


            return new Rotativa.ViewAsPdf("Formato", incModel)
            {
                PageMargins = new Rotativa.Options.Margins(15, 2, 10, 2),
                PageSize = Rotativa.Options.Size.Letter,
                CustomHeaders = new Dictionary<string, string>(),

            };
        }

        public ActionResult Descargar()
        {
            IncidenteModel incModel = new Models.ReporteIncidente.IncidenteModel();
            if (TempData["datosIncidente"] != null)
            {
                incModel = (IncidenteModel)TempData["datosIncidente"];
            }

            ConstruirModelInicdente(incModel, false);



            return new Rotativa.ViewAsPdf("Formato", incModel)
            {
                PageMargins = new Rotativa.Options.Margins(15, 2, 10, 2),
                PageSize = Rotativa.Options.Size.Letter,
                CustomHeaders = new Dictionary<string, string>(),
                FileName = "ReporteDeIncidente.pdf"
            };
        }


        private void ConstruirModelInicdente(IncidenteModel incModel, bool ver = true)
        {
            if (!ver)
            {
                incModel._HoraIncidente = string.Format("{0}:{1}", incModel.fechaIncidente.Hour < 10 ? "0" + incModel.fechaIncidente.Hour.ToString() : incModel.fechaIncidente.Hour.ToString(),
                    incModel.fechaIncidente.Minute < 10 ? "0" + incModel.fechaIncidente.Minute.ToString() : incModel.fechaIncidente.Minute.ToString());
                incModel.DiaSemanaIncidente = incModel.diaSemanaIncidente;
                incModel.MunicipioSede = incModel.MunicipioSede.Contains("-Municipios-") ? "" : incModel.MunicipioSede;
                incModel.NombreSede = incModel.NombreSede.Contains("-Sedes-") ? "" : incModel.NombreSede;
            }
            else
            {
                incModel._HoraIncidente = string.Format("{0}:{1}", incModel.FechaIncidente.Hour < 10 ? "0" + incModel.FechaIncidente.Hour.ToString() : incModel.FechaIncidente.Hour.ToString(),
                    incModel.FechaIncidente.Minute < 10 ? "0" + incModel.FechaIncidente.Minute.ToString() : incModel.FechaIncidente.Minute.ToString());
                incModel.DiaSemanaIncidente = incModel.diaSemanaIncidente;
            }
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("Nit", incModel.NitEmpresa);
            var Listas = ServiceClient.ObtenerObjetoJsonRestFul<EDIncidente_Listas_Basicas>(UrlServicioIncidenteInvestigacion, CapacidadConsultarListas, RestSharp.Method.GET);
            if (Listas != null)
            {
                incModel.TiposDeDocumento = Listas.TiposDocumento.Select(tp => new SelectListItem()
                {
                    Value = tp.Id_Tipo_Documento.ToString(),
                    Text = tp.Sigla
                }).ToList();

                incModel.DepartamentoEmp = Listas.Departamentos.Where(d => d.Pk_Id_Departamento.Equals(incModel.IdDepartamentoEmp)).Select(d => d.Nombre_Departamento).FirstOrDefault();
                ServiceClient.EliminarParametros();
                ServiceClient.AdicionarParametro("departamento", incModel.IdDepartamentoEmp);
                var resultMunicipio = ServiceClient.ObtenerArrayJsonRestFul<EDMunicipio>(urlServicioEmpresas, CapacidadMunicipiosPorDepto, RestSharp.Method.GET);
                if (resultMunicipio[0] != null && resultMunicipio.Length > 0)
                {
                    incModel.MunicipioEmp = resultMunicipio.Where(m => m.IdMunicipio == incModel.IdMunicipioEmp).Select(m => m.NombreMunicipio).FirstOrDefault();
                }

                if (!incModel.EsSedePrincipal)
                {
                    incModel.DepartamentoSede = Listas.Departamentos.Where(d => d.Pk_Id_Departamento.Equals(incModel.IdDepartamentoSede)).Select(d => d.Nombre_Departamento).FirstOrDefault();
                    ServiceClient.EliminarParametros();
                    ServiceClient.AdicionarParametro("departamento", incModel.IdDepartamentoSede);
                    resultMunicipio = ServiceClient.ObtenerArrayJsonRestFul<EDMunicipio>(urlServicioEmpresas, CapacidadMunicipiosPorDepto, RestSharp.Method.GET);
                    if (resultMunicipio[0] != null && resultMunicipio.Length > 0)
                    {
                        incModel.MunicipioSede = resultMunicipio.Where(m => m.IdMunicipio == incModel.IdMunicipioSede).Select(m => m.NombreMunicipio).FirstOrDefault();
                    }
                }

                incModel.DepartamentoEmpleado = Listas.Departamentos.Where(d => d.Pk_Id_Departamento.Equals(incModel.IdDepartamentoEmpleado)).Select(d => d.Nombre_Departamento).FirstOrDefault();
                ServiceClient.EliminarParametros();
                ServiceClient.AdicionarParametro("departamento", incModel.IdDepartamentoEmpleado);
                resultMunicipio = ServiceClient.ObtenerArrayJsonRestFul<EDMunicipio>(urlServicioEmpresas, CapacidadMunicipiosPorDepto, RestSharp.Method.GET);
                if (resultMunicipio[0] != null && resultMunicipio.Length > 0)
                {
                    incModel.MunicipioEmpleado = resultMunicipio.Where(m => m.IdMunicipio == incModel.IdMunicipioEmpleado).Select(m => m.NombreMunicipio).FirstOrDefault();
                }

                incModel.DepartamentoIncidente = Listas.Departamentos.Where(d => d.Pk_Id_Departamento.Equals(incModel.IdDepartamentoIncidente)).Select(d => d.Nombre_Departamento).FirstOrDefault();
                ServiceClient.EliminarParametros();
                ServiceClient.AdicionarParametro("departamento", incModel.IdDepartamentoIncidente);
                resultMunicipio = ServiceClient.ObtenerArrayJsonRestFul<EDMunicipio>(urlServicioEmpresas, CapacidadMunicipiosPorDepto, RestSharp.Method.GET);
                if (resultMunicipio[0] != null && resultMunicipio.Length > 0)
                {
                    incModel.MunicipioIncidente = resultMunicipio.Where(m => m.IdMunicipio == incModel.IdMunicipioIncidente).Select(m => m.NombreMunicipio).FirstOrDefault();
                }


                incModel.Departamentos = Listas.Departamentos.Select(dept => new SelectListItem()
                {
                    Value = dept.Pk_Id_Departamento.ToString(),
                    Text = dept.Nombre_Departamento
                }).ToList();
                incModel.Sedes = new List<SedeModel>();

                incModel.Zonas = Listas.Zonas.Select(z => new SelectListItem()
                {
                    Value = z.PK_ZonaLugar.ToString(),
                    Text = z.Descripcion_ZonaLugar
                }).ToList();
                incModel.VinculacionLaboral = Listas.VinculacionLaboral.Select(v => new SelectListItem()
                {
                    Value = v.PK_VinculacionLaboral.ToString(),
                    Text = v.Descripcion_VinculacionLaboral
                }).ToList();
                incModel.Jornadas = Listas.Jornadas.Select(j => new SelectListItem()
                {
                    Value = j.Pk_Id_Tipo_Jornada.ToString(),
                    Text = j.Nombre_Jornada
                }).ToList();
                incModel.DiasSemana = Listas.DiasSemana.Select(d => new SelectListItem()
                {
                    Value = d.ToString(),
                    Text = d.ToString()
                }).ToList();
                incModel.TiposIncidente = Listas.TiposIncidente.Select(ti => new SelectListItem()
                {
                    Value = ti.Pk_Id_Tipo_Incidente.ToString(),
                    Text = ti.Nombre_Incidente
                }).ToList();
                incModel.SitiosIncidente = Listas.SitiosIncidente.Select(si => new SelectListItem()
                {
                    Value = si.Pk_Id_Sitio_Incidente.ToString(),
                    Text = si.Nombre_Sitio
                }).ToList();
                incModel.ConsecuenciasIncidente = Listas.IncidenteConsecuencias.Select(ic => new SelectListItem()
                {
                    Value = ic.Pk_Id_Incidente_Consecuencia.ToString(),
                    Text = ic.Nombre_consecuencia
                }).ToList();
                incModel.Municipios = new List<SelectListItem>();
            }
        }

        public JsonResult ObtenerIncidente(int IdIncidente)
        {
            TempData["idincidente"] = IdIncidente;
            return Json(new { Data = "/VerIncidente", Mensaje = "OK" });
        }

        public ActionResult VerIncidente()
        {
            LNIncidente logica = new LNIncidente();
            EDIncidente_Modelo_Consulta parametros = new EDIncidente_Modelo_Consulta();
            if (TempData["idincidente"] != null)
            {
                parametros.IncidenteID = (int)TempData["idincidente"];
            }


            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("idincidente", parametros.IncidenteID);
            var resultIncidente = ServiceClient.ObtenerObjetoJsonRestFul<EDIncidente>(UrlServicioIncidenteInvestigacion, CapacidadConsultarIncidenteid, Method.GET);
            EDIncidente edIncidente = logica.ConsultarIncidente(parametros);
            IncidenteModel datosIncidente = new Models.ReporteIncidente.IncidenteModel();
            if (edIncidente.Pk_Id_Incidente > 0)
            {
                #region I. Identificación general del empleador

                datosIncidente.ActividadEconomica = edIncidente.General_actividad_economica;
                datosIncidente.CodActividadEconomica = int.Parse(edIncidente.General_codigo);
                datosIncidente.IdTipoDocumentoEmpresa = edIncidente.General_tipo_documento.Id_Tipo_Documento;
                datosIncidente.NitEmpresa = edIncidente.General_numero_identificación;
                datosIncidente.RazonSocial = edIncidente.General_razon_social;
                datosIncidente.DireccionEmpresa = edIncidente.General_sede_principal_direccion;
                datosIncidente.TelefonoEmpresa = edIncidente.General_sede_principal_telefono;
                datosIncidente.IdDepartamentoEmp = edIncidente.General_sede_principal_departamento_id;
                datosIncidente.CorreoElectronico = edIncidente.General_correo_electronico;
                datosIncidente.IdMunicipioEmp = edIncidente.General_sede_principal_municipio_id;
                datosIncidente.IdZonaEmpresa = edIncidente.General_sede_principal_zona.Equals("U") ? 1 : 0;
                datosIncidente.IdZonaEmpresa = edIncidente.General_sede_principal_zona_id;
                datosIncidente.MunicipioEmp = edIncidente.General_sede_principal_municipio;
                datosIncidente.EsSedePrincipal = edIncidente.General_mismos_datos_sede_principal;
                if (!datosIncidente.EsSedePrincipal)
                {
                    datosIncidente.IdSede = edIncidente.General_sede.IdSede;
                    datosIncidente.DireccionSede = edIncidente.General_sede.DireccionSede;
                    datosIncidente.NombreSede = edIncidente.General_sede.NombreSede;
                    datosIncidente.TelefonoSede = edIncidente.General_sede.Telefono;
                    datosIncidente.IdMunicipioSede = edIncidente.General_sede.IdMunicipio;
                    datosIncidente.IdDepartamentoSede = edIncidente.General_sede.IdDepto;
                    datosIncidente.IdZonaSede = edIncidente.General_sede.Sector.Equals("Urbano") ? 1 : 0;
                    datosIncidente.MunicipioSede = edIncidente.General_sede.NombreMunici;
                    datosIncidente.DepartamentoSede = edIncidente.General_sede.NombreDepto;
                }
                #endregion
                #region II. Información de la persona que reporta el incidente
                datosIncidente.IdVinculacionLab = edIncidente.Persona_vinculacion_laboral.PK_VinculacionLaboral;
                datosIncidente.IdTipoDocumentoEmpleado = edIncidente.Persona_tipo_documento.Id_Tipo_Documento;
                datosIncidente.DocumentoEmpleado = edIncidente.Persona_numero_identificacion;
                datosIncidente.PrimerApellido = edIncidente.Persona_primer_apellido;
                datosIncidente.SegundoApellido = edIncidente.Persona_segundo_apellido;
                datosIncidente.PrimerNombre = edIncidente.Persona_primer_nombre;
                datosIncidente.SegundNombre = edIncidente.Persona_segundo_nombre;
                datosIncidente.FechaNacimiento = edIncidente.Persona_fecha_nacimiento;
                datosIncidente.DescripcionLabor = edIncidente.Incidente_nombre_labor;
                datosIncidente.Genero = edIncidente.Persona_genero;
                datosIncidente.DireccionEmpleado = edIncidente.Persona_direccion;
                datosIncidente.TelefonoEmpleado = edIncidente.Persona_telefono;
                datosIncidente.IdDepartamentoEmpleado = edIncidente.Persona_departamento_id;
                datosIncidente.IdMunicipioEmpleado = edIncidente.Persona_municipio_id;
                datosIncidente.DepartamentoEmpleado = edIncidente.Persona_departamento;
                datosIncidente.MunicipioEmpleado = edIncidente.Persona_municipio;
                datosIncidente.IdZonaEmpleado = edIncidente.Persona_zona.PK_ZonaLugar;
                datosIncidente.Ocupacion = edIncidente.Persona_ocupacion_habitual;
                datosIncidente.FechaIngreso = edIncidente.Persona_fecha_ingreso_empresa;
                datosIncidente.IdJornadaHabitual = edIncidente.FK_id_jornada_habitual_persona;
                #endregion

                datosIncidente.FechaIncidente = edIncidente.Incidente_fecha;
                datosIncidente.DiaSemanaIncidente = edIncidente.Incidente_dia_semana;
                datosIncidente.EsJornadaNormal = edIncidente.Incidente_jornada_normal;
                datosIncidente.RealizabaLaborHabitual = edIncidente.Incidente_realizaba_labor_habitual;
                datosIncidente.DescripcionLabor = edIncidente.Incidente_nombre_labor;
                datosIncidente.TiempoPrevioIncidente = edIncidente.Incidente_tiempo_previo_al_incidente;
                datosIncidente.IdTipoIncidente = edIncidente.FK_id_incidente_tipo_incidente;
                datosIncidente.IdDepartamentoIncidente = edIncidente.Incidente_departamento.Pk_Id_Departamento;
                datosIncidente.IdMunicipioIncidente = edIncidente.Incidente_municipio.IdMunicipio;
                datosIncidente.IdZonaIncidente = edIncidente.Incidente_zona_incidente.PK_ZonaLugar;
                datosIncidente.DepartamentoIncidente = edIncidente.Incidente_departamento.Nombre_Departamento;
                datosIncidente.MunicipioIncidente = edIncidente.Incidente_municipio.NombreMunicipio;
                datosIncidente.EsDentroEmpresa = edIncidente.Incidente_ocurre_dentro_empresa;
                datosIncidente.SitioIncidente = edIncidente.Incidente_sitio_incidente.Nombre_Sitio;
                datosIncidente.OtroSitio = edIncidente.Incidente_sitio_incidente_otro;
                datosIncidente.Consecuencia = edIncidente.Incidente_consecuencia.Nombre_consecuencia;
                datosIncidente.DescripcionIncidente = edIncidente.Incidente_descripcion;
                datosIncidente.FechaCreacionIncidente = edIncidente.Incidente_fecha_diligenciamiento;
            }

            ConstruirModelInicdente(datosIncidente, true);

            return new Rotativa.ViewAsPdf("Formato", datosIncidente)
            {
                CustomSwitches = "--header-line",
                PageMargins = new Rotativa.Options.Margins(15, 2, 10, 2),
                PageSize = Rotativa.Options.Size.Letter,
                CustomHeaders = new Dictionary<string, string>(),
            };
        }

        /// <summary>
        /// Retorna el formato para impresión o descarga.
        /// </summary>
        /// <returns></returns>
        public ActionResult Formato(int incidenteId, string tipo)
        {
            //var Modelo = lnincidente.ConsultarIncidentes(new EDIncidente_Modelo_Consulta
            //{
            //    IncidenteID = incidenteId
            //});

            if (tipo == "descargar")
            {
                //return new RazorPDF.PdfResult(null, "Formato");
                return new Rotativa.ViewAsPdf("Formato", new { incidenteId = incidenteId })
                {

                    CustomSwitches = "--enable-forms ",
                    FileName = "ReporteDeIncidente.pdf",
                };
            }
            else
                return new Rotativa.ViewAsPdf("Formato", new { incidenteId = incidenteId });
        }

        /// <summary>
        /// Presentar el formulario de ayuda para el diligenciamiento del informe.
        /// </summary>
        /// <returns></returns>
        public ActionResult Ayuda()
        {
            return View();
        }


        #region Métodos utilitarios

        ///// <summary>
        ///// Obtener la lista de municipios para un departamento dado.
        ///// </summary>
        ///// <param name="departamentoid"></param>
        ///// <returns></returns>
        //[HttpPost]
        //public new ActionResult ObtenerMunicipios(int departamentoid)
        //{
        //    var Resultado = base.ObtenerMunicipios(departamentoid);
        //    if (Resultado != null && Resultado.Any())
        //    {
        //        var html = RenderRazorViewToString("_ListaMunicipios", Resultado);
        //        return Json(new { Data = html, Mensaje = "Success" });
        //    }
        //    else
        //        return Json(new { Data = string.Empty, Mensaje = "Fail" });
        //}

        //public string ObtenerHtmlListaMunicipios(int departamentoid)
        //{
        //    var Municipios = base.ObtenerMunicipios(departamentoid);
        //    string Resultado;
        //    if (Municipios != null && Municipios.Any())
        //        Resultado = RenderRazorViewToString("_ListaMunicipios", Municipios);
        //    else
        //        Resultado = string.Empty;

        //    return Resultado;
        //}

        #endregion
    }
}