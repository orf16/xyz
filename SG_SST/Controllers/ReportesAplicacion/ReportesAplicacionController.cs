using SG_SST.Controllers.Base;
using SG_SST.Logica.Ausentismo;
using SG_SST.Models.Participacion;
using SG_SST.Reportes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SG_SST.Models.Ausentismo;
using SG_SST.ServiceRequest;
using System.Configuration;
using SG_SST.EntidadesDominio.Empresas;
using SG_SST.Services.Empresas.IServices;
using SG_SST.Services.Empresas.Services;
using SG_SST.Models.Empresas;
namespace SG_SST.Controllers.ReportesAplicacion
{
    public class ReportesAplicacionController : BaseController
    {
        string UrlServicioPlanificacion = ConfigurationManager.AppSettings["UrlServicioPlanificacion"];
        string CapacidadObtenerAnoInicioEmpresa = ConfigurationManager.AppSettings["CapacidadObtenerAnoInicioEmpresa"];
        private ISedeServicios sedeServicio = new SedeServicios();
        private IProcesoServicios procesoServicios = new ProcesoServicios();

        // GET: Indicadores
        public ActionResult IndicadoresAunsentismo()
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

        // GET: ReportesAplicacion
        public ActionResult ReportesIndicadores()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
                return RedirectToAction("Login", "Home");
            }


            List<Proceso> procesos = procesoServicios.ObtenerProcesosPrincipales(usuarioActual.IdEmpresa);
            ViewBag.Procesos = new SelectList(procesos, "Pk_Id_Proceso", "Descripcion_Proceso");


            ViewBag.FKSede = new SelectList(sedeServicio.SedesPorEmpresa(usuarioActual.IdEmpresa), "Pk_Id_Sede", "Nombre_Sede");
            return View();
        }


        //Indicadores Estructura

        public ActionResult IndicadoresEstructura()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);

            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
                return RedirectToAction("Login", "Home");
            }

            List<Proceso> procesos = procesoServicios.ObtenerProcesosPrincipales(usuarioActual.IdEmpresa);
            ViewBag.Procesos = new SelectList(procesos, "Pk_Id_Proceso", "Descripcion_Proceso");


            ViewBag.FKSede = new SelectList(sedeServicio.SedesPorEmpresa(usuarioActual.IdEmpresa), "Pk_Id_Sede", "Nombre_Sede");
            return View();
        }



        //Indicadores Resultado

        public ActionResult IndicadoresResultado()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
                return RedirectToAction("Login", "Home");
            }
            ViewBag.FKSede = new SelectList(sedeServicio.SedesPorEmpresa(usuarioActual.IdEmpresa), "Pk_Id_Sede", "Nombre_Sede");
            return View();
        }



        //Indicadores Proceso

        public ActionResult IndicadoresProceso()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
                return RedirectToAction("Login", "Home");
            }
            ViewBag.FKSede = new SelectList(sedeServicio.SedesPorEmpresa(usuarioActual.IdEmpresa), "Pk_Id_Sede", "Nombre_Sede");
            return View();
        }

        public ActionResult ReportesAplicacion()
        {

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
                return RedirectToAction("Login", "Home");
            }
            ViewBag.FKSede = new SelectList(sedeServicio.SedesPorEmpresa(usuarioActual.IdEmpresa), "Pk_Id_Sede", "Nombre_Sede");


            return View();
        }

        public void ReporteAusentismo()
        {
           
            var nitEmpresa = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current).NitEmpresa;

            CrearActaConvivenciaVM accionARealizar = new CrearActaConvivenciaVM();
            accionARealizar.AccionARealizar = "AUSENTISMO";

            RecursoParametros.NombreReporte = "Reporte_au_por_proceso.rdlc";
            RecursoParametros.Reporte = "AUSENTISMO";
            RecursoParametros.NitEmpresa = nitEmpresa;

            RenderRazorViewToString("Reportes", accionARealizar);
        }

        public void ReportePresupuesto(int periodo, int idSede, string sedeTexto)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            CrearActaConvivenciaVM accionARealizar = new CrearActaConvivenciaVM();
            accionARealizar.AccionARealizar = "PRESUPUESTO";
            var nitEmpresa = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current).NitEmpresa;

            RecursoParametros.NombreReporte = "Reporte_Presupuesto.rdlc";
            RecursoParametros.Reporte = "PRESUPUESTO";
            RecursoParametros.NitEmpresa = nitEmpresa;
            RecursoParametros.SedeTexto = sedeTexto;
            RecursoParametros.SedeInd = idSede;
            RecursoParametros.Anio = periodo;


            RenderRazorViewToString("Reportes", accionARealizar);
        }
        public void ReporteCompetencias()
        {
            CrearActaConvivenciaVM accionARealizar = new CrearActaConvivenciaVM();
            accionARealizar.AccionARealizar = "COMPETENCIAS";
            var nitEmpresa = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current).NitEmpresa;

            RecursoParametros.NombreReporte = "COMPETENCIAS.rdlc";
            RecursoParametros.Reporte = "COMPETENCIAS";
            RecursoParametros.NitEmpresa = nitEmpresa;

            RenderRazorViewToString("Reportes", accionARealizar);
        }




        public ActionResult ReporteMetodologiaInsht()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }

            var nitEmpresa = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current).NitEmpresa;

            RecursoParametros.NombreReporte = "METODOLOGÍA_INSHT.rdlc";
            RecursoParametros.Reporte = "METODOLOGIAINSHT";
            RecursoParametros.NitEmpresa = nitEmpresa;
            return PartialView("Reportes");
        }
        public ActionResult ReportePlanTrabajo()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }

            var nitEmpresa = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current).NitEmpresa;

            RecursoParametros.NombreReporte = "PLANDETRABAJO.rdlc";
            RecursoParametros.Reporte = "PLANTRABAJO";
            RecursoParametros.NitEmpresa = nitEmpresa;
            return PartialView("Reportes");
        }
        public ActionResult ReporteDiagnosticoSalud()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }

            var nitEmpresa = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current).NitEmpresa;

            RecursoParametros.NombreReporte = "ReporteDiagnosticoCondicionesdeSalud.rdlc";
            RecursoParametros.Reporte = "DIAGNOSTICOSALUD";
            RecursoParametros.NitEmpresa = nitEmpresa;
            return PartialView("Reportes");
        }
        public ActionResult AccionesCorrectivas()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }

            var nitEmpresa = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current).NitEmpresa;

            RecursoParametros.NombreReporte = "ACCIONESCORRECTIVASYPREVENTIVAS.rdlc";
            RecursoParametros.Reporte = "AccionesCorrectivas";
            RecursoParametros.NitEmpresa = nitEmpresa;
            return PartialView("Reportes");
        }
        public ActionResult GestionCambio()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }

            var nitEmpresa = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current).NitEmpresa;

            RecursoParametros.NombreReporte = "GESTIONDELCAMBIO.rdlc";
            RecursoParametros.Reporte = "GestionCambio";
            RecursoParametros.NitEmpresa = nitEmpresa;
            return PartialView("Reportes");
        }
        public ActionResult Incidentes()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }

            var nitEmpresa = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current).NitEmpresa;

            RecursoParametros.NombreReporte = "REPORTEDEINCIDENTES.rdlc";
            RecursoParametros.Reporte = "Incidentes";
            RecursoParametros.NitEmpresa = nitEmpresa;
            return PartialView("Reportes");
        }
        public ActionResult InspeccionesSeguridad()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }

            var nitEmpresa = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current).NitEmpresa;

            RecursoParametros.NombreReporte = "INSPECCIONESDESEGURIDAD.rdlc";
            RecursoParametros.Reporte = "InspeccionesSeguridad";
            RecursoParametros.NitEmpresa = nitEmpresa;
            return PartialView("Reportes");
        }
        public ActionResult PerfilSocio()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }

            var nitEmpresa = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current).NitEmpresa;

            RecursoParametros.NombreReporte = "PERFILSOCIODEMOGRÁFICO.rdlc";
            RecursoParametros.Reporte = "PerfilSocio";
            RecursoParametros.NitEmpresa = nitEmpresa;
            return PartialView("Reportes");
        }
        public ActionResult IdentificacionPeligro()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }

            var nitEmpresa = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current).NitEmpresa;

            RecursoParametros.NombreReporte = "ReporteMetodologiaGTC45.rdlc";
            RecursoParametros.Reporte = "IdentificacionPeligro";
            RecursoParametros.NitEmpresa = nitEmpresa;
            return PartialView("Reportes");
        }
        public ActionResult MetodologiaRam()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }

            var nitEmpresa = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current).NitEmpresa;

            RecursoParametros.NombreReporte = "ReporteMetodologiaRAM.rdlc";
            RecursoParametros.Reporte = "MetodologiaRam";
            RecursoParametros.NitEmpresa = nitEmpresa;
            return PartialView("Reportes");
        }
        public ActionResult PuestosTrabajo()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }

            var nitEmpresa = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current).NitEmpresa;

            RecursoParametros.NombreReporte = "EstudioPTrabajo.rdlc";
            RecursoParametros.Reporte = "PuestosTrabajo";
            RecursoParametros.NitEmpresa = nitEmpresa;
            return PartialView("Reportes");
        }
        public ActionResult PlanEmergenciaAccion()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }

            var nitEmpresa = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current).NitEmpresa;

            RecursoParametros.NombreReporte = "ReportePlanEmergenciaFrentesAccion.rdlc";
            RecursoParametros.Reporte = "PlanEmergenciaAccion";
            RecursoParametros.NitEmpresa = nitEmpresa;
            return PartialView("Reportes");
        }
        public ActionResult PlanEmergenciaGeneral()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }

            var nitEmpresa = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current).NitEmpresa;

            RecursoParametros.NombreReporte = "ReportePlanEmergenciaInfoGeneral.rdlc";
            RecursoParametros.Reporte = "PlanEmergenciaGeneral";
            RecursoParametros.NitEmpresa = nitEmpresa;
            return PartialView("Reportes");
        }
        public ActionResult ActosCondicionesInseguras()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }

            var nitEmpresa = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current).NitEmpresa;

            RecursoParametros.NombreReporte = "ACTOSYCONDICIONESINSEGURAS.rdlc";
            RecursoParametros.Reporte = "ActosCondicionesInseguras";
            RecursoParametros.NitEmpresa = nitEmpresa;
            return PartialView("Reportes");
        }
        public ActionResult AdquisicionesBienes()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }

            var nitEmpresa = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current).NitEmpresa;

            RecursoParametros.NombreReporte = "ADQUISCIONESBIENESOCONTRATACION.rdlc";
            RecursoParametros.Reporte = "AdquisicionesBienes";
            RecursoParametros.NitEmpresa = nitEmpresa;
            return PartialView("Reportes");
        }
        public ActionResult RelacionesLaborales()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }

            var nitEmpresa = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current).NitEmpresa;

            RecursoParametros.NombreReporte = "Relacioneslaborales.rdlc";
            RecursoParametros.Reporte = "RelacionesLaborales";
            RecursoParametros.NitEmpresa = nitEmpresa;
            return PartialView("Reportes");
        }
        public ActionResult PlanCapacitacion()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }

            var nitEmpresa = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current).NitEmpresa;

            RecursoParametros.NombreReporte = "PLANDECAPACITACION.rdlc";
            RecursoParametros.Reporte = "PlanCapacitacion";
            RecursoParametros.NitEmpresa = nitEmpresa;
            return PartialView("Reportes");
        }


        //Nuevo

      

        public void MedidasDePrevencion()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            CrearActaConvivenciaVM accionARealizar = new CrearActaConvivenciaVM();
            accionARealizar.AccionARealizar = "MEDIDASPREV";
            var nitEmpresa = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current).NitEmpresa;

            RecursoParametros.NombreReporte = "MEDIDASDEPREVENCIONYCONTROL.rdlc";
            RecursoParametros.Reporte = "MEDIDASPREV";
            RecursoParametros.NitEmpresa = nitEmpresa;

            RenderRazorViewToString("Reportes", accionARealizar);
        }


        public void PlanesDeAccion()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            CrearActaConvivenciaVM accionARealizar = new CrearActaConvivenciaVM();
            accionARealizar.AccionARealizar = "PLANESACCION";
            var nitEmpresa = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current).NitEmpresa;

            RecursoParametros.NombreReporte = "PlanesAccion.rdlc";
            RecursoParametros.Reporte = "PLANESACCION";
            RecursoParametros.NitEmpresa = nitEmpresa;

            RenderRazorViewToString("Reportes", accionARealizar);
        }

        public void ReporteEnfermedadLaboral()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            CrearActaConvivenciaVM accionARealizar = new CrearActaConvivenciaVM();
            accionARealizar.AccionARealizar = "ENFERMEDADLAB";
            var nitEmpresa = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current).NitEmpresa;

            RecursoParametros.NombreReporte = "REPORTEENFERMEDADLABORAL.rdlc";
            RecursoParametros.Reporte = "ENFERMEDADLAB";
            RecursoParametros.NitEmpresa = nitEmpresa;

            RenderRazorViewToString("Reportes", accionARealizar);
        }

        public void ReporteIncidenteAT()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            CrearActaConvivenciaVM accionARealizar = new CrearActaConvivenciaVM();
            accionARealizar.AccionARealizar = "IncidenteAT";
            var nitEmpresa = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current).NitEmpresa;

            RecursoParametros.NombreReporte = "Incidentes_EL.rdlc";
            RecursoParametros.Reporte = "IncidenteAT";
            RecursoParametros.NitEmpresa = nitEmpresa;

            RenderRazorViewToString("Reportes", accionARealizar);
        }

        public void ReporteActividadesComunicaciones(int anio,string estado)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            CrearActaConvivenciaVM accionARealizar = new CrearActaConvivenciaVM();
            accionARealizar.AccionARealizar = "ActividadesComunicaciones";
            var nitEmpresa = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current).NitEmpresa;

            RecursoParametros.NombreReporte = "Comunicados_actividades.rdlc";
            RecursoParametros.Reporte = "ActividadesComunicaciones";
            RecursoParametros.NitEmpresa = nitEmpresa;
            RecursoParametros.Anio = anio;
            RecursoParametros.Estado = estado;

            RenderRazorViewToString("Reportes", accionARealizar);
        }

        

        #region graficasAunsentismo

        public ActionResult ReportesAusentismo()
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

        public void ReporteDiasAunsentismoPorContigencia(int anio, int? idorigen, int? idEmpresa, int? idSede, int? idDepartamento)
        {
            var nitEmpresa = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current).NitEmpresa;

            CrearActaConvivenciaVM accionARealizar = new CrearActaConvivenciaVM();
            accionARealizar.AccionARealizar = "DIASCONTIGENCIA";

            RecursoParametros.NombreReporte = "RepDiasContingencia.rdlc";
            RecursoParametros.Reporte = "DIASCONTIGENCIA";
            RecursoParametros.NitEmpresa = nitEmpresa;
            RecursoParametros.Anio = anio;
            RecursoParametros.Origen = idorigen;
            RecursoParametros.EmpresaUsuaria = idEmpresa;
            RecursoParametros.Sede = idSede;
            RecursoParametros.Departamento = idDepartamento;

            RenderRazorViewToString("Reportes", accionARealizar);
        }

        public void ReporteNumeroDeEventosPorContigencia(int anio, int? idorigen, int? idEmpresa, int? idSede, int? idDepartamento)
        {
            var nitEmpresa = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current).NitEmpresa;

            CrearActaConvivenciaVM accionARealizar = new CrearActaConvivenciaVM();
            accionARealizar.AccionARealizar = "EVENTOSCONTIGENCIA";

            RecursoParametros.NombreReporte = "RepNumEventosContingencia.rdlc";
            RecursoParametros.Reporte = "EVENTOSCONTIGENCIA";
            RecursoParametros.NitEmpresa = nitEmpresa;
            RecursoParametros.Anio = anio;
            RecursoParametros.Origen = idorigen;
            RecursoParametros.EmpresaUsuaria = idEmpresa;
            RecursoParametros.Sede = idSede;
            RecursoParametros.Departamento = idDepartamento;

            RenderRazorViewToString("Reportes", accionARealizar);
        }

        public void ReporteDiasAusentismoPorDepartamentos(int anio, int? idorigen, int? idEmpresa, int? idSede, int? idDepartamento)
        {
            var nitEmpresa = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current).NitEmpresa;

            CrearActaConvivenciaVM accionARealizar = new CrearActaConvivenciaVM();
            accionARealizar.AccionARealizar = "DEPARTAMENTOS";

            RecursoParametros.NombreReporte = "Rep_Departamento.rdlc";
            RecursoParametros.Reporte = "DEPARTAMENTOS";
            RecursoParametros.NitEmpresa = nitEmpresa;
            RecursoParametros.Anio = anio;
            RecursoParametros.Origen = idorigen;
            RecursoParametros.EmpresaUsuaria = idEmpresa;
            RecursoParametros.Sede = idSede;
            RecursoParametros.Departamento = idDepartamento;

            RenderRazorViewToString("Reportes", accionARealizar);
        }

        public void ReporteDiasAusentismoPorEnfermedadesCIE10(int anio, int? idorigen, int? idEmpresa, int? idSede, int? idDepartamento)
        {
            var nitEmpresa = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current).NitEmpresa;

            CrearActaConvivenciaVM accionARealizar = new CrearActaConvivenciaVM();
            accionARealizar.AccionARealizar = "DIASENFERMEDADES";

            RecursoParametros.NombreReporte = "DiasAusentismoEnfermedadesCIE10.rdlc";
            RecursoParametros.Reporte = "DIASENFERMEDADES";
            RecursoParametros.NitEmpresa = nitEmpresa;
            RecursoParametros.Anio = anio;
            RecursoParametros.Origen = idorigen;
            RecursoParametros.EmpresaUsuaria = idEmpresa;
            RecursoParametros.Sede = idSede;
            RecursoParametros.Departamento = idDepartamento;

            RenderRazorViewToString("Reportes", accionARealizar);
        }

        public void ReporteEventosPorEnfermedadesCIE10(int anio, int? idorigen, int? idEmpresa, int? idSede, int? idDepartamento)
        {
            var nitEmpresa = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current).NitEmpresa;

            CrearActaConvivenciaVM accionARealizar = new CrearActaConvivenciaVM();
            accionARealizar.AccionARealizar = "EVENTOENFERMEDADES";

            RecursoParametros.NombreReporte = "NumeroDeEventosEnfermedadesCIE10.rdlc";
            RecursoParametros.Reporte = "EVENTOENFERMEDADES";
            RecursoParametros.NitEmpresa = nitEmpresa;
            RecursoParametros.Anio = anio;
            RecursoParametros.Origen = idorigen;
            RecursoParametros.EmpresaUsuaria = idEmpresa;
            RecursoParametros.Sede = idSede;
            RecursoParametros.Departamento = idDepartamento;

            RenderRazorViewToString("Reportes", accionARealizar);
        }


        public void ReporteDiasAusentismoPorProceso(int anio, int? idorigen, int? idEmpresa, int? idSede, int? idDepartamento)
        {

            var nitEmpresa = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current).NitEmpresa;

            CrearActaConvivenciaVM accionARealizar = new CrearActaConvivenciaVM();
            accionARealizar.AccionARealizar = "PROCESO";

            RecursoParametros.NombreReporte = "ReportAusenciasProceso.rdlc";
            RecursoParametros.Reporte = "PROCESO";
            RecursoParametros.NitEmpresa = nitEmpresa;
            RecursoParametros.Anio = anio;
            RecursoParametros.Origen = idorigen;
            RecursoParametros.EmpresaUsuaria = idEmpresa;
            RecursoParametros.Sede = idSede;
            RecursoParametros.Departamento = idDepartamento;

            RenderRazorViewToString("Reportes", accionARealizar);
                
        }
        public void ReporteDiasAusentismoPorSede(int anio, int? idorigen, int? idEmpresa, int? idSede, int? idDepartamento)
        {
            var nitEmpresa = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current).NitEmpresa;

            CrearActaConvivenciaVM accionARealizar = new CrearActaConvivenciaVM();
            accionARealizar.AccionARealizar = "SEDE";

            RecursoParametros.NombreReporte = "ReportSedes.rdlc";
            RecursoParametros.Reporte = "SEDE";
            RecursoParametros.NitEmpresa = nitEmpresa;
            RecursoParametros.Anio = anio;
            RecursoParametros.Origen = idorigen;
            RecursoParametros.EmpresaUsuaria = idEmpresa;
            RecursoParametros.Sede = idSede;
            RecursoParametros.Departamento = idDepartamento;

            RenderRazorViewToString("Reportes", accionARealizar);
        }

        public void ReporteCostoPorContigencia(int anio, int? idorigen, int? idEmpresa, int? idSede, int? idDepartamento)
        {
            var nitEmpresa = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current).NitEmpresa;

            CrearActaConvivenciaVM accionARealizar = new CrearActaConvivenciaVM();
            accionARealizar.AccionARealizar = "COSTOCONTIGENCIA";

            RecursoParametros.NombreReporte = "RepCostosContingencia.rdlc";
            RecursoParametros.Reporte = "COSTOCONTIGENCIA";
            RecursoParametros.NitEmpresa = nitEmpresa;
            RecursoParametros.Anio = anio;
            RecursoParametros.Origen = idorigen;
            RecursoParametros.EmpresaUsuaria = idEmpresa;
            RecursoParametros.Sede = idSede;
            RecursoParametros.Departamento = idDepartamento;

            RenderRazorViewToString("Reportes", accionARealizar);
        }

        public void ReporteAusentismoPorEPS(int anio, int? idorigen, int? idEmpresa, int? idSede, int? idDepartamento)
         //public void ReporteAusentismoPorEPS()
       
        {
            var nitEmpresa = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current).NitEmpresa;

            CrearActaConvivenciaVM accionARealizar = new CrearActaConvivenciaVM();
            accionARealizar.AccionARealizar = "EPS";
    
            RecursoParametros.NombreReporte = "AusentismosPorEPS.rdlc";
            RecursoParametros.Reporte = "EPS";
            RecursoParametros.NitEmpresa = nitEmpresa;
            RecursoParametros.Anio = anio;
            RecursoParametros.Origen = idorigen;
            RecursoParametros.EmpresaUsuaria = idEmpresa;
            RecursoParametros.Sede = idSede;
            RecursoParametros.Departamento = idDepartamento;

            RenderRazorViewToString("Reportes", accionARealizar);
        }

        public void ReporteAusentismoPorSexo(int anio, int? idorigen, int? idEmpresa, int? idSede, int? idDepartamento)
        {
            var nitEmpresa = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current).NitEmpresa;

            CrearActaConvivenciaVM accionARealizar = new CrearActaConvivenciaVM();
            accionARealizar.AccionARealizar = "SEXO";

            RecursoParametros.NombreReporte = "RepSexo.rdlc";
            RecursoParametros.Reporte = "SEXO";
            RecursoParametros.NitEmpresa = nitEmpresa;
            RecursoParametros.Anio = anio;
            RecursoParametros.Origen = idorigen;
            RecursoParametros.EmpresaUsuaria = idEmpresa;
            RecursoParametros.Sede = idSede;
            RecursoParametros.Departamento = idDepartamento;

            RenderRazorViewToString("Reportes", accionARealizar);

        }

        public void ReporteAusentismoPorTipoVinculacion(int anio, int? idorigen, int? idEmpresa, int? idSede, int? idDepartamento)
        {
            var nitEmpresa = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current).NitEmpresa;

            CrearActaConvivenciaVM accionARealizar = new CrearActaConvivenciaVM();
            accionARealizar.AccionARealizar = "TIPOVINCULACION";

            RecursoParametros.NombreReporte = "TipoVinculacion.rdlc";
            RecursoParametros.Reporte = "TIPOVINCULACION";
            RecursoParametros.NitEmpresa = nitEmpresa;
            RecursoParametros.Anio = anio;
            RecursoParametros.Origen = idorigen;
            RecursoParametros.EmpresaUsuaria = idEmpresa;
            RecursoParametros.Sede = idSede;
            RecursoParametros.Departamento = idDepartamento;

            RenderRazorViewToString("Reportes", accionARealizar);

        }

        public void ReporteAusentismoPorOcupacionCIUO(int anio, int? idorigen, int? idEmpresa, int? idSede, int? idDepartamento)
        {

            var nitEmpresa = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current).NitEmpresa;

            CrearActaConvivenciaVM accionARealizar = new CrearActaConvivenciaVM();
            accionARealizar.AccionARealizar = "OCUPACION";

            RecursoParametros.NombreReporte = "RepOcupacion.rdlc";
            RecursoParametros.Reporte = "OCUPACION";
            RecursoParametros.NitEmpresa = nitEmpresa;
            RecursoParametros.Anio = anio;
            RecursoParametros.Origen = idorigen;
            RecursoParametros.EmpresaUsuaria = idEmpresa;
            RecursoParametros.Sede = idSede;
            RecursoParametros.Departamento = idDepartamento;

            RenderRazorViewToString("Reportes", accionARealizar);

        }

        public void ReporteAusentismoPorGrupoEtareo(int anio, int? idorigen, int? idEmpresa, int? idSede, int? idDepartamento)
        {
            var nitEmpresa = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current).NitEmpresa;

            CrearActaConvivenciaVM accionARealizar = new CrearActaConvivenciaVM();
            accionARealizar.AccionARealizar = "GRUPOETAREO";

            RecursoParametros.NombreReporte = "GruposEtarios.rdlc";
            RecursoParametros.Reporte = "GRUPOETAREO";
            RecursoParametros.NitEmpresa = nitEmpresa;
            RecursoParametros.Anio = anio;
            RecursoParametros.Origen = idorigen;
            RecursoParametros.EmpresaUsuaria = idEmpresa;
            RecursoParametros.Sede = idSede;
            RecursoParametros.Departamento = idDepartamento;

            RenderRazorViewToString("Reportes", accionARealizar);
           
        }



        #endregion




        #region indicadores


        public void ReporteAccionCorrectivaPreventiva(int anio)
        {
            var nitEmpresa = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current).NitEmpresa;

            CrearActaConvivenciaVM accionARealizar = new CrearActaConvivenciaVM();
            accionARealizar.AccionARealizar = "AccionCorrectivaPreventiva";

            RecursoParametros.NombreReporte = "ReporteIndAccionCorrectivaPreventiva.rdlc";
            RecursoParametros.Reporte = "AccionCorrectivaPreventiva";
            RecursoParametros.NitEmpresa = nitEmpresa;
            RecursoParametros.Anio = anio;

            RenderRazorViewToString("Reportes", accionARealizar);

        }

        public void IndicadorCondicionesInseguras(int anio)
        {
            var nitEmpresa = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current).NitEmpresa;

            CrearActaConvivenciaVM accionARealizar = new CrearActaConvivenciaVM();
            accionARealizar.AccionARealizar = "CondicionInsegura";

            RecursoParametros.NombreReporte = "ReporteIndCondicionInsegura.rdlc";
            RecursoParametros.Reporte = "CondicionInsegura";
            RecursoParametros.NitEmpresa = nitEmpresa;
            RecursoParametros.Anio = anio;

            RenderRazorViewToString("Reportes", accionARealizar);

        }

        public void IndicadorPlanTrabajoAnual(int anio, int idSede, string sedeTexto)
        {
            var nitEmpresa = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current).NitEmpresa;

            CrearActaConvivenciaVM accionARealizar = new CrearActaConvivenciaVM();
            accionARealizar.AccionARealizar = "PlanTrabajoAnual";

            RecursoParametros.NombreReporte = "ReporteIndPlanTrabajoAnual.rdlc";
            RecursoParametros.Reporte = "PlanTrabajoAnual";
            RecursoParametros.NitEmpresa = nitEmpresa;
            RecursoParametros.Anio = anio;
            RecursoParametros.SedeInd = idSede;
            RecursoParametros.SedeTexto = sedeTexto;
            RenderRazorViewToString("Reportes", accionARealizar);

        }

        public void IndicadorEstandaresMinimos(int anio)
        {
            var nitEmpresa = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current).NitEmpresa;

            CrearActaConvivenciaVM accionARealizar = new CrearActaConvivenciaVM();
            accionARealizar.AccionARealizar = "EstandaresMinimos";

            RecursoParametros.NombreReporte = "ReporteIndEvalEstandaresMinimos.rdlc";
            RecursoParametros.Reporte = "EstandaresMinimos";
            RecursoParametros.NitEmpresa = nitEmpresa;
            RecursoParametros.Anio = anio;

            RenderRazorViewToString("Reportes", accionARealizar);

        }

        public void IndicadorAccidentesDeTrabajo(int anio, string constanteK)
        {
            var nitEmpresa = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current).NitEmpresa;

            CrearActaConvivenciaVM accionARealizar = new CrearActaConvivenciaVM();
            accionARealizar.AccionARealizar = "AccidentesDeTrabajo";

            RecursoParametros.NombreReporte = "ReporteIndFrecuenciaAccidentesTrabajo.rdlc";
            RecursoParametros.Reporte = "AccidentesDeTrabajo";
            RecursoParametros.NitEmpresa = nitEmpresa;
            RecursoParametros.Anio = anio;
            RecursoParametros.ConstanteK = constanteK;
            RenderRazorViewToString("Reportes", accionARealizar);

        }

        public void IndicadorTasaAccidentalidad(int anio)
        {
            var nitEmpresa = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current).NitEmpresa;

            CrearActaConvivenciaVM accionARealizar = new CrearActaConvivenciaVM();
            accionARealizar.AccionARealizar = "TasaAccidentalidad";

            RecursoParametros.NombreReporte = "ReporteIndTasaAccidentalidad.rdlc";
            RecursoParametros.Reporte = "TasaAccidentalidad";
            RecursoParametros.NitEmpresa = nitEmpresa;
            RecursoParametros.Anio = anio;

            RenderRazorViewToString("Reportes", accionARealizar);

        }

        public void IndicadorCapacitacionEntrenamiento(int anio)
        {
            var nitEmpresa = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current).NitEmpresa;

            CrearActaConvivenciaVM accionARealizar = new CrearActaConvivenciaVM();
            accionARealizar.AccionARealizar = "CapacitacionEntrenamiento";

            RecursoParametros.NombreReporte = "ReporteIndPlanCapacitacion.rdlc";
            RecursoParametros.Reporte = "CapacitacionEntrenamiento";
            RecursoParametros.NitEmpresa = nitEmpresa;
            RecursoParametros.Anio = anio;

            RenderRazorViewToString("Reportes", accionARealizar);

        }


        public void IndicadorFrecuenciaAusentismo(int anio, string constanteK, int contigencia, string contigenciaTexto)
        {
            var nitEmpresa = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current).NitEmpresa;

            CrearActaConvivenciaVM accionARealizar = new CrearActaConvivenciaVM();
            accionARealizar.AccionARealizar = "FrecuenciaAusentismo";

            RecursoParametros.NombreReporte = "ReporteIndFrecuenciaAusentismo.rdlc";
            RecursoParametros.Reporte = "FrecuenciaAusentismo";
            RecursoParametros.NitEmpresa = nitEmpresa;
            RecursoParametros.Anio = anio;
            RecursoParametros.ConstanteK = constanteK;
            RecursoParametros.Contigencia = contigencia;
            RecursoParametros.ContigenciaTexto = contigenciaTexto;
           

            RenderRazorViewToString("Reportes", accionARealizar);

        }

        public void IndicadorSeveridadAusentismo(int anio, string constanteK, int contigencia, string contigenciaTexto)
        {
            var nitEmpresa = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current).NitEmpresa;

            CrearActaConvivenciaVM accionARealizar = new CrearActaConvivenciaVM();
            accionARealizar.AccionARealizar = "SeveridadAusentismo";

            RecursoParametros.NombreReporte = "ReporteIndSeveridadAusentismo.rdlc";
            RecursoParametros.Reporte = "SeveridadAusentismo";
            RecursoParametros.NitEmpresa = nitEmpresa;
            RecursoParametros.Anio = anio;
            RecursoParametros.ConstanteK = constanteK;
            RecursoParametros.Contigencia = contigencia;
            RecursoParametros.ContigenciaTexto = contigenciaTexto;
            RenderRazorViewToString("Reportes", accionARealizar);

        }


        public void IndicadorSeveridadAccidenteTrabajo(int anio, string constanteK)
        {
            var nitEmpresa = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current).NitEmpresa;

            CrearActaConvivenciaVM accionARealizar = new CrearActaConvivenciaVM();
            accionARealizar.AccionARealizar = "SeveridadAccidenteTrabajo";

            RecursoParametros.NombreReporte = "ÍndicedeSeveridaddeAccidentesdeTrabajo.rdlc";
            RecursoParametros.Reporte = "SeveridadAccidenteTrabajo";
            RecursoParametros.NitEmpresa = nitEmpresa;
            RecursoParametros.Anio = anio;
            RecursoParametros.ConstanteK = constanteK;
          
            RenderRazorViewToString("Reportes", accionARealizar);

        }

        public void IndicadorLesionesIncapacitantes(int anio, string constanteK)
        {
            var nitEmpresa = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current).NitEmpresa;

            CrearActaConvivenciaVM accionARealizar = new CrearActaConvivenciaVM();
            accionARealizar.AccionARealizar = "LesionesIncapacitantes";

            RecursoParametros.NombreReporte = "ÍndicedeLesionesIncapacitantesporAT.rdlc";
            RecursoParametros.Reporte = "LesionesIncapacitantes";
            RecursoParametros.NitEmpresa = nitEmpresa;
            RecursoParametros.Anio = anio;
            RecursoParametros.ConstanteK = constanteK;

            RenderRazorViewToString("Reportes", accionARealizar);

        }

        public void IndicadorCumplimientoRequisitosLegales(int anio)
        {
            var nitEmpresa = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current).NitEmpresa;

            CrearActaConvivenciaVM accionARealizar = new CrearActaConvivenciaVM();
            accionARealizar.AccionARealizar = "CumplimientoRequisitosLegales";

            RecursoParametros.NombreReporte = "Indicadordelcumplimientodelosrequisitoslegales.rdlc";
            RecursoParametros.Reporte = "CumplimientoRequisitosLegales";
            RecursoParametros.NitEmpresa = nitEmpresa;
            RecursoParametros.Anio = anio;

            RenderRazorViewToString("Reportes", accionARealizar);

        }


        public void IndicadorComiteCoppast(int anio, int? idSede, string sedeTexto)
        {
            var nitEmpresa = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current).NitEmpresa;

            CrearActaConvivenciaVM accionARealizar = new CrearActaConvivenciaVM();
            accionARealizar.AccionARealizar = "ComiteCoppast";

            RecursoParametros.NombreReporte = "RepIndicadorActas.rdlc";
            RecursoParametros.Reporte = "ComiteCoppast";
            RecursoParametros.NitEmpresa = nitEmpresa;
            RecursoParametros.Anio = anio;
            RecursoParametros.Sede = idSede;
            RecursoParametros.SedeTexto = sedeTexto;

            RenderRazorViewToString("Reportes", accionARealizar);

        }

        public void IndicadorDxCondicionesSalud(int anio, int? idSede, string sedeTexto, int? idProceso, string procesoTexto)
        {
            var nitEmpresa = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current).NitEmpresa;

            CrearActaConvivenciaVM accionARealizar = new CrearActaConvivenciaVM();
            accionARealizar.AccionARealizar = "DxCondicionesSalud";

            RecursoParametros.NombreReporte = "ReporteIndDxCondicionesSalud.rdlc";
            RecursoParametros.Reporte = "DxCondicionesSalud";
            RecursoParametros.NitEmpresa = nitEmpresa;
            RecursoParametros.Anio = anio;
            RecursoParametros.Sede = idSede;
            RecursoParametros.SedeTexto = sedeTexto;
            RecursoParametros.Proceso = idProceso;
            RecursoParametros.ProcesoTexto = procesoTexto;

            RenderRazorViewToString("Reportes", accionARealizar);

        }


        public void IndicadorPerfilSocioDemografico(int? idSede, string sedeTexto, int? idProceso, string procesoTexto)
        {
            var nitEmpresa = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current).NitEmpresa;

            CrearActaConvivenciaVM accionARealizar = new CrearActaConvivenciaVM();
            accionARealizar.AccionARealizar = "PerfilSocioDemografico";

            RecursoParametros.NombreReporte = "RepIndicadorPerfilSocioDemografico.rdlc";
            RecursoParametros.Reporte = "PerfilSocioDemografico";
            RecursoParametros.NitEmpresa = nitEmpresa;
            RecursoParametros.Sede = idSede;
            RecursoParametros.SedeTexto = sedeTexto;
            RecursoParametros.Proceso = idProceso;
            RecursoParametros.ProcesoTexto = procesoTexto;

            RenderRazorViewToString("Reportes", accionARealizar);

        }

        public void IndicadorComunicaciones(int anio, string estado)
        {
            var nitEmpresa = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current).NitEmpresa;

            CrearActaConvivenciaVM accionARealizar = new CrearActaConvivenciaVM();
            accionARealizar.AccionARealizar = "Comunicaciones";

            RecursoParametros.NombreReporte = "Comu_ini.rdlc";
            RecursoParametros.Reporte = "Comunicaciones";
            RecursoParametros.NitEmpresa = nitEmpresa;
            RecursoParametros.Anio = anio;
            RecursoParametros.Estado = estado;

            RenderRazorViewToString("Reportes", accionARealizar);

        }
     
        #endregion


        #region indicadores datos

        public void IndicadorEstandaresMinimosDatos(int anio)
        {
            var nitEmpresa = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current).NitEmpresa;

            CrearActaConvivenciaVM accionARealizar = new CrearActaConvivenciaVM();
            accionARealizar.AccionARealizar = "EstandaresMinimosDatos";

            RecursoParametros.NombreReporte = "ReporteIndEvalEstandaresMinimosDatos.rdlc";
            RecursoParametros.Reporte = "EstandaresMinimosDatos";
            RecursoParametros.NitEmpresa = nitEmpresa;
            RecursoParametros.Anio = anio;

            RenderRazorViewToString("Reportes", accionARealizar);

        }

        public void ReporteAccionCorrectivaPreventivaDatos(int anio)
        {
            var nitEmpresa = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current).NitEmpresa;

            CrearActaConvivenciaVM accionARealizar = new CrearActaConvivenciaVM();
            accionARealizar.AccionARealizar = "AccionCorrectivaPreventivaDatos";

            RecursoParametros.NombreReporte = "ReporteIndAccionCorrectivaPreventivaDatos.rdlc";
            RecursoParametros.Reporte = "AccionCorrectivaPreventivaDatos";
            RecursoParametros.NitEmpresa = nitEmpresa;
            RecursoParametros.Anio = anio;

            RenderRazorViewToString("Reportes", accionARealizar);

        }

        public void IndicadorCondicionesInsegurasDatos(int anio, int? tipoReporte, int? idSede)
        {
            var nitEmpresa = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current).NitEmpresa;

            CrearActaConvivenciaVM accionARealizar = new CrearActaConvivenciaVM();
            accionARealizar.AccionARealizar = "CondicionInseguraDatos";

            RecursoParametros.NombreReporte = "ReporteIndCondicionInseguraDatos.rdlc";
            RecursoParametros.Reporte = "CondicionInseguraDatos";
            RecursoParametros.NitEmpresa = nitEmpresa;
            RecursoParametros.Anio = anio;
            RecursoParametros.TipoReporte = tipoReporte;
            RecursoParametros.Sede = idSede;

            RenderRazorViewToString("Reportes", accionARealizar);

        }

        public void IndicadorAccidentesDeTrabajoDatos(int anio, int? idSede)
        {
            var nitEmpresa = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current).NitEmpresa;

            CrearActaConvivenciaVM accionARealizar = new CrearActaConvivenciaVM();
            accionARealizar.AccionARealizar = "AccidentesDeTrabajoDatos";

            RecursoParametros.NombreReporte = "ReporteIndFrecuenciaAccidentesTrabajoDatos.rdlc";
            RecursoParametros.Reporte = "AccidentesDeTrabajoDatos";
            RecursoParametros.NitEmpresa = nitEmpresa;
            RecursoParametros.Anio = anio;
            RecursoParametros.Sede = idSede;
            RenderRazorViewToString("Reportes", accionARealizar);

        }

        public void IndicadorFrecuenciaAusentismoDatos(int anio,int contigencia,int? idSede)
        {
            var nitEmpresa = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current).NitEmpresa;

            CrearActaConvivenciaVM accionARealizar = new CrearActaConvivenciaVM();
            accionARealizar.AccionARealizar = "FrecuenciaAusentismoDatos";

            RecursoParametros.NombreReporte = "ReporteIndFrecuenciaAusentismoDatos.rdlc";
            RecursoParametros.Reporte = "FrecuenciaAusentismoDatos";
            RecursoParametros.NitEmpresa = nitEmpresa;
            RecursoParametros.Anio = anio;
            RecursoParametros.Sede = idSede;
            RecursoParametros.Contigencia = contigencia;
           


            RenderRazorViewToString("Reportes", accionARealizar);

        }

        public void IndicadorCapacitacionEntrenamientoDatos(int anio)
        {
            var nitEmpresa = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current).NitEmpresa;

            CrearActaConvivenciaVM accionARealizar = new CrearActaConvivenciaVM();
            accionARealizar.AccionARealizar = "CapacitacionEntrenamientoDatos";

            RecursoParametros.NombreReporte = "ReporteIndPlanCapacitacionDatos.rdlc";
            RecursoParametros.Reporte = "CapacitacionEntrenamientoDatos";
            RecursoParametros.NitEmpresa = nitEmpresa;
            RecursoParametros.Anio = anio;

            RenderRazorViewToString("Reportes", accionARealizar);

        }

        public void IndicadorPlanTrabajoAnualDatos(int anio, int? idSede)
        {
            var nitEmpresa = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current).NitEmpresa;

            CrearActaConvivenciaVM accionARealizar = new CrearActaConvivenciaVM();
            accionARealizar.AccionARealizar = "PlanTrabajoAnualDatos";

            RecursoParametros.NombreReporte = "ReporteIndPlanTrabajoAnualDatos.rdlc";
            RecursoParametros.Reporte = "PlanTrabajoAnualDatos";
            RecursoParametros.NitEmpresa = nitEmpresa;
            RecursoParametros.Anio = anio;
            RecursoParametros.Sede = idSede;
            
            RenderRazorViewToString("Reportes", accionARealizar);

        }

        public void IndicadorSeveridadAusentismoDatos(int anio,  int contigencia, int? idSede)
        {
            var nitEmpresa = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current).NitEmpresa;

            CrearActaConvivenciaVM accionARealizar = new CrearActaConvivenciaVM();
            accionARealizar.AccionARealizar = "SeveridadAusentismoDatos";

            RecursoParametros.NombreReporte = "ReporteIndSeveridadAusentismoDatos.rdlc";
            RecursoParametros.Reporte = "SeveridadAusentismoDatos";
            RecursoParametros.NitEmpresa = nitEmpresa;
            RecursoParametros.Anio = anio;
            RecursoParametros.Sede = idSede;
            RecursoParametros.Contigencia = contigencia;
         
            RenderRazorViewToString("Reportes", accionARealizar);

        }

        public void IndicadorTasaAccidentalidadDatos(int anio, int? idSede)
        {
            var nitEmpresa = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current).NitEmpresa;

            CrearActaConvivenciaVM accionARealizar = new CrearActaConvivenciaVM();
            accionARealizar.AccionARealizar = "TasaAccidentalidadDatos";

            RecursoParametros.NombreReporte = "ReporteIndTasaAccidentalidadDatos.rdlc";
            RecursoParametros.Reporte = "TasaAccidentalidadDatos";
            RecursoParametros.NitEmpresa = nitEmpresa;
            RecursoParametros.Anio = anio;
            RecursoParametros.Sede = idSede;
            RenderRazorViewToString("Reportes", accionARealizar);

        }

        public void IndicadorCumplimientoRequisitosLegalesDatos(int anio)
        {
            var nitEmpresa = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current).NitEmpresa;

            CrearActaConvivenciaVM accionARealizar = new CrearActaConvivenciaVM();
            accionARealizar.AccionARealizar = "CumplimientoRequisitosLegalesDatos";

            RecursoParametros.NombreReporte = "ReporteIndRequisitosLegalesDatos.rdlc";
            RecursoParametros.Reporte = "CumplimientoRequisitosLegalesDatos";
            RecursoParametros.NitEmpresa = nitEmpresa;
            RecursoParametros.Anio = anio;

            RenderRazorViewToString("Reportes", accionARealizar);

        }

        public void IndicadorAccidenteDeTrabajoDatos(int anio, int? idSede)
        {
            var nitEmpresa = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current).NitEmpresa;

            CrearActaConvivenciaVM accionARealizar = new CrearActaConvivenciaVM();
            accionARealizar.AccionARealizar = "AccidenteDeTrabajoDatos";

            RecursoParametros.NombreReporte = "ReporteIndSeveridadAccidentesTrabajoDatos.rdlc";
            RecursoParametros.Reporte = "AccidenteDeTrabajoDatos";
            RecursoParametros.NitEmpresa = nitEmpresa;
            RecursoParametros.Anio = anio;
            RecursoParametros.Sede = idSede;

            RenderRazorViewToString("Reportes", accionARealizar);

        }

        public void IndicadorLesionesIncapacitantesDatos(int anio, int? idSede)
        {
            var nitEmpresa = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current).NitEmpresa;

            CrearActaConvivenciaVM accionARealizar = new CrearActaConvivenciaVM();
            accionARealizar.AccionARealizar = "LesionesIncapacitantesDatos";

            RecursoParametros.NombreReporte = "ReporteIndLesionesIncapacitantesATDatos.rdlc";
            RecursoParametros.Reporte = "LesionesIncapacitantesDatos";
            RecursoParametros.NitEmpresa = nitEmpresa;
            RecursoParametros.Anio = anio;
            RecursoParametros.Sede = idSede;

            RenderRazorViewToString("Reportes", accionARealizar);

        }


        public void IndicadorDxCondicionesSaludDatos(int anio, int? idSede,int? idProceso)
        {
            var nitEmpresa = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current).NitEmpresa;

            CrearActaConvivenciaVM accionARealizar = new CrearActaConvivenciaVM();
            accionARealizar.AccionARealizar = "DxCondicionesSaludDatos";

            RecursoParametros.NombreReporte = "ReporteIndDxCondicionesSaludDatos.rdlc";
            RecursoParametros.Reporte = "DxCondicionesSaludDatos";
            RecursoParametros.NitEmpresa = nitEmpresa;
            RecursoParametros.Anio = anio;
            RecursoParametros.Sede = idSede;
            RecursoParametros.Proceso = idProceso;

            RenderRazorViewToString("Reportes", accionARealizar);

        }

        #endregion

        #region indicadoresAusentismo


        public void IndicadoresAusentismo(int AnioSeleccionado, int IdContingencia,string contigenciaTexto, string ConstanteSeleccionada, int? IdEmpresaUsuaria)
        {
            var nitEmpresa = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current).NitEmpresa;

            CrearActaConvivenciaVM accionARealizar = new CrearActaConvivenciaVM();
         
            RecursoParametros.NombreReporte = "ReporteIndicadoresAusentismo.rdlc";
            RecursoParametros.Reporte = "IndicadorAusentismo";
            RecursoParametros.NitEmpresa = nitEmpresa;
            RecursoParametros.Anio = AnioSeleccionado;
            RecursoParametros.Contigencia = IdContingencia;
            RecursoParametros.ConstanteK = ConstanteSeleccionada;
            RecursoParametros.EmpresaUsuaria = IdEmpresaUsuaria;
            RecursoParametros.ContigenciaTexto = contigenciaTexto;
            accionARealizar.AccionARealizar = "IndicadorAusentismo";
            

            RenderRazorViewToString("Reportes", accionARealizar);

        }

        public void IndicadoresAusentismoComparacion(int PrimerAnio, int SegundoAnio, int IdContingencia, string contigenciaTexto,string ConstanteSeleccionada, int? IdEmpresaUsuaria)
        {
            var nitEmpresa = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current).NitEmpresa;

            CrearActaConvivenciaVM accionARealizar = new CrearActaConvivenciaVM();
            accionARealizar.AccionARealizar = "AusentismoComparacion";

            RecursoParametros.NombreReporte = "ReporteIndicadoresAusentismoComparacion.rdlc";
            RecursoParametros.Reporte = "AusentismoComparacion";
            RecursoParametros.NitEmpresa = nitEmpresa;
            RecursoParametros.Anio = PrimerAnio;
            RecursoParametros.Contigencia = IdContingencia;
            RecursoParametros.ConstanteK = ConstanteSeleccionada;
            RecursoParametros.EmpresaUsuaria = IdEmpresaUsuaria;
            RecursoParametros.AnioComparacion = SegundoAnio;
            RecursoParametros.ContigenciaTexto = contigenciaTexto;
            accionARealizar.AccionARealizar = "IndicadorAusentismo";


            RenderRazorViewToString("Reportes", accionARealizar);

        }
     

        #endregion


    }
}