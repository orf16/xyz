using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SG_SST.Logica.Planificacion;
using System.Configuration;
using RestSharp;
using System.Net;
using SG_SST.Dtos.Planificacion;
using SG_SST.Models;
using SG_SST.Models.Empleado;
using SG_SST.Models.Planificacion;
using SG_SST.EntidadesDominio.Planificacion;
 using SG_SST.Services.Planificacion.Services;
using SG_SST.Services.Planificacion.IServices;
using SG_SST.ServiceRequest;
using System.IO;
using SG_SST.EntidadesDominio.Empresas;
//using SG_SST.Utilidades.Traza;
using System.Threading;
using SG_SST.Controllers.Planificacion;
using iTextSharp.text;
using ClosedXML.Excel;
using System.Data.Entity;
using System.Data;
using SG_SST.Controllers.Base;
using SG_SST.Services.Empresas.IServices;
using SG_SST.Services.Empresas.Services;
using SG_SST.EntidadesDominio.Ausentismo;
using Newtonsoft.Json;
using SG_SST.Models.Empresas;
namespace SG_SST.Controllers.Planificacion
{
    public class PerfilSocioDemoGraficoController : BaseController
    {

        private IProcesoServicios procesoServicios = new ProcesoServicios();
        private SG_SSTContext db = new SG_SSTContext(); 
        private IPerfilSocioDemograficoServicios perfilservicio = new PerfilSocioDemograficoServicios();
        private LNPeligro peligro = new LNPeligro();
        private ISedeServicios sedeServicio = new SedeServicios();
       // string UrlServicioPlanificacion = ConfigurationManager.AppSettings["UrlServicioPlanificacion"];
        string CapacidadPerfilSociodemogarficoGuardar = ConfigurationManager.AppSettings["CapacidadPerfilSociodemogarficoGuardar"];
        string CapacidadObtenerLugares = ConfigurationManager.AppSettings["CapacidadLugares"];
        string CapacidadDescargarplatillaPerfilSocioDemografico = ConfigurationManager.AppSettings["CapacidadDescargarplatillaPerfilSocioDemografico"];
        string CapacidadDescargarplatillaPerfil = ConfigurationManager.AppSettings["CapacidadDescargarplatillaPerfil"];

        string capacidadObtenerPerfilSocioDemografico = ConfigurationManager.AppSettings["capacidadObtenerPerfilSocioDemografico"];
        string capacidadObtenerCondicionesPorPerfil = ConfigurationManager.AppSettings["capacidadObtenerCondicionesPorPerfil"];
        string CapacidadPerfilSociodemogarficoEditar = ConfigurationManager.AppSettings["CapacidadPerfilSociodemogarficoEditar"];
        string CapacidadDescargarExcelPorPerfil = ConfigurationManager.AppSettings["CapacidadDescargarExcelPorPerfil"];

        string capacidadObtenerPerfilSocioDemograficoEmpresa = ConfigurationManager.AppSettings["capacidadObtenerPerfilSocioDemograficoEmpresa"];
        string capacidadObtenerCondicionesPorEmpresa = ConfigurationManager.AppSettings["capacidadObtenerCondicionesPorEmpresa"];
        string CapacidadEliminarExposicion = ConfigurationManager.AppSettings["CapacidadEliminarExposicion"];
        string afiliadoempresaactivo = ConfigurationManager.AppSettings["afiliadoempresaactivo"];








        string capacidadObtenerCiudadPorSede = ConfigurationManager.AppSettings["capacidadObtenerCiudadPorSede"];
        private IMetodologiaServicios metodologiaServicios = new MetodologiaServicios();
        string CapacidadCargarPerfilSocioDemografico = ConfigurationManager.AppSettings["CapacidadCargarPerfilSocioDemografico"];
        string CapacidadEliminarPerfilSocioDemografio = ConfigurationManager.AppSettings["CapacidadEliminarPerfilSocioDemografio"];
        LNPerfilSocioDemografico lnPerfil = new LNPerfilSocioDemografico();
        DateTime fechaNacimiento;
        IZonaLugar zona = new ZonaLugarServicio();
        IClasificacionDePeligrosServicios clasificacionDePeligrosServicios = new ClasificacionDePeligrosServicios();
        EDCarguePerfil perfil = new EDCarguePerfil();
        static bool eliminar=false;
        int edad, meses, anyos,dias;
        // GET: PerfilSocioDemoGrafico
        static string nitEmpresa = "";
        DateTime fechaIngresoEmpresa;
        string primerNombre, segundoNombre, primerApellido, segundoAPellido, tipoDocumento,direccion,nitEmpresaU,eps,afp,ocupacion,cargo;
        string descripcionDelPeligro = "";
        string rutaPlantillaPerfilSocio = ConfigurationManager.AppSettings["rutaPlantillaPerfilSocio"];
        EDPerfilSocioDemografico per;
        public ActionResult DeleteConfirmed(int id)
        {
           
            var Transaction = db.Database.BeginTransaction();
            try
            {
                var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
                if (usuarioActual == null)
                {
                    ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
                    return RedirectToAction("Login", "Home");
                }

                PerfilSocioDemograficoPlanificacion perfil = db.Tbl_PerfilSocioDemograficoPlanificacion.Find(id);
                db.Tbl_PerfilSocioDemograficoPlanificacion.Remove(perfil);
              
                db.SaveChanges();
                Transaction.Commit();

                eliminar = true;
                var proceso = obtenerPerfilesSocioDemograficos(usuarioActual.NitEmpresa);
                ViewBag.MensajeExitoEliminar = "El perfil fué eliminado correctamente.";
                return View("Listado", proceso.ToList());
    
            }
            catch (Exception e)
            {
                Transaction.Rollback();
                var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
                var proceso = obtenerPerfilesSocioDemograficos(usuarioActual.NitEmpresa);
                ViewBag.MensajeErrorEliminar = "Se presentó un error, por favor intente más tarde";
                return View("Listado", proceso.ToList());
            }
            
            

        }


        public ActionResult EliminarExposicion(int idExposicion)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
                return View();
            }
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("idExposicion", idExposicion);

            bool resultExposic = ServiceClient.PeticionesPostJsonRestFulRespuetaBool(urlServicioPlanificacion, CapacidadEliminarExposicion, RestSharp.Method.DELETE);
            //string ruta = rutaImagenesReportesCI + usuarioActual.NitEmpresa;

            if (resultExposic)
            {
                return Json(new
                {
                    success = resultExposic
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return null;

            }
        }








        public ActionResult Index()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
                return RedirectToAction("Login", "Home");
            }
            nitEmpresa = usuarioActual.NitEmpresa;
            try
            {
                ViewBag.Pk_Id_Sede = new SelectList(sedeServicio.SedesPorEmpresa(usuarioActual.IdEmpresa), "Pk_Id_Sede", "Nombre_Sede");
                ViewBag.Fk_Id_Departamento = new SelectList(db.Tbl_Departamento, "Pk_Id_Departamento", "Nombre_Departamento");
                ViewBag.PK_Peligro = new SelectList(db.Tbl_Peligro, "PK_Peligro", "Lugar");
                //  ViewBag.FK_Clasificacion_De_Peligro = new SelectList(db.Tbl_Clasificacion_De_Peligro, "PK_Clasificacion_De_Peligro", "Descripcion_Clase_De_Peligro");
                ViewBag.FK_Tipo_De_Peligro = new SelectList(db.Tbl_Tipo_De_Peligro, "PK_Tipo_De_Peligro", "Descripcion_Del_Peligro");
                ViewBag.PK_ID_Zona = new SelectList(db.Tbl_ZonaLugar, "PK_ZonaLugar", "Descripcion_ZonaLugar");
                ViewBag.FK_Estrato = new SelectList(db.Tbl_Estrato, "PK_Estrato", "Descripcion_Estrato");
                ViewBag.FK_Estado_Civil = new SelectList(db.Tbl_Estado_Civil, "PK_Estado_Civil", "Descripcion_EstadoCivil");
                ViewBag.FK_Etnia = new SelectList(db.Tbl_Etnia, "PK_Etnia", "Descripcion_Etnia");
                ViewBag.FK_VinculacionLaboral = new SelectList(db.Tbl_VinculacionLaboral, "PK_VinculacionLaboral", "Descripcion_VinculacionLaboral");
                ViewBag.NitEmpresa = nitEmpresa;
                ServiceClient.EliminarParametros();
                ServiceClient.AdicionarParametro("idEmpresa", usuarioActual.IdEmpresa);
                var resultLugares = ServiceClient.ObtenerArrayJsonRestFul<string>(urlServicioPlanificacion, CapacidadObtenerLugares, RestSharp.Method.GET);
                
                //ServiceClient.EliminarParametros();
                //ServiceClient.AdicionarParametro("NIT", usuarioActual.NitEmpresa);
                //var resultProceso = ServiceClient.ObtenerArrayJsonRestFul<EDProceso>(urlServicioEmpresas, CapacidadObtenerprocesosEmpresa, RestSharp.Method.GET);


                List<Proceso> procesos = procesoServicios.ObtenerProcesosPrincipales(usuarioActual.IdEmpresa);
                ViewBag.Procesos = new SelectList(procesos, "Pk_Id_Proceso", "Descripcion_Proceso");
               
                return View();
            }
            catch (Exception e) {

                return View();
            }
        }



     


        public FileResult DescargarReporteExcelSedesYProcesos()
        {

            EDProcesoSede infoProcesoSede = new EDProcesoSede();
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
            }

            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("NIT", usuarioActual.NitEmpresa);
            var resultSede = ServiceClient.ObtenerArrayJsonRestFul<EDSede>(urlServicioEmpresas, CapacidadObtenerSedesPorNit, RestSharp.Method.GET);

            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("NIT", usuarioActual.NitEmpresa);
            var resultProceso = ServiceClient.ObtenerArrayJsonRestFul<EDProceso>(urlServicioEmpresas, CapacidadObtenerprocesosEmpresa, RestSharp.Method.GET);

            infoProcesoSede.sedes = resultSede.ToList();
            infoProcesoSede.procesos = resultProceso.ToList();

            var result = lnPerfil.ObtenerReporteExcelProcesoYSede(infoProcesoSede);
            //ServiceClient.AdicionarParametro("rep", resultReporte.ToList());
            //var result = ServiceClient.ObtenerObjetoJsonRestFul<byte[]>(UrlServicioParticipacion, CapacidadDescargarExcelReportesCondicionesInseguras, RestSharp.Method.POST);

            return File(result, "application/vnd.ms-excel", "Códigos Plantilla Perfil sociodemográfico.xlsx");
        }

        public ActionResult CargueMasivo ()
        {
            EDProcesoSede infoProcesoSede = new EDProcesoSede();
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
                return RedirectToAction("Login", "Home");
            }
            else
            {
                ServiceClient.EliminarParametros();
                ServiceClient.AdicionarParametro("NIT", usuarioActual.NitEmpresa);
                var resultSede = ServiceClient.ObtenerArrayJsonRestFul<EDSede>(urlServicioEmpresas, CapacidadObtenerSedesPorNit, RestSharp.Method.GET);

                ServiceClient.EliminarParametros();
                ServiceClient.AdicionarParametro("NIT", usuarioActual.NitEmpresa);
                var resultProceso = ServiceClient.ObtenerArrayJsonRestFul<EDProceso>(urlServicioEmpresas, CapacidadObtenerprocesosEmpresa, RestSharp.Method.GET);

                infoProcesoSede.sedes = resultSede.ToList();
                infoProcesoSede.procesos = resultProceso.ToList();
                return View(infoProcesoSede);
            }
            
        }
        public FileResult DescargarPerfilEnExcel(int id)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
            }
            EDPerfilSocioDemografico edPerfil = new EDPerfilSocioDemografico();

            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("id", id);
            var perfilSocio = ServiceClient.ObtenerObjetoJsonRestFul<EDPerfilSocioDemografico>(urlServicioPlanificacion, capacidadObtenerPerfilSocioDemografico, RestSharp.Method.GET);

            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("id", id);
            var condicionesPerfil = ServiceClient.ObtenerArrayJsonRestFul<EDCondicionesRiesgoPerfil>(urlServicioPlanificacion, capacidadObtenerCondicionesPorPerfil, RestSharp.Method.GET);

            List<EDCondicionesRiesgoPerfil> condiciones = condicionesPerfil.ToList();

            var fk_sede = perfilSocio.Pk_Id_Sede;
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("fk_sede", fk_sede);
            var obtenerCiudadSede = ServiceClient.ObtenerObjetoJsonRestFul<EDBusquedaMunicipio>(urlServicioPlanificacion, capacidadObtenerCiudadPorSede, RestSharp.Method.GET);
            edPerfil = perfilSocio;
            edPerfil.condicionesRiesgo = condicionesPerfil.ToList();
            EDBusquedaMunicipio ciudadSede = new EDBusquedaMunicipio();
            ciudadSede = obtenerCiudadSede;

            List<EDCondicionesRiesgoPerfil> condicionesDeRiesgo = new List<EDCondicionesRiesgoPerfil>();
            TipoDePeligro tipoPeligro = new TipoDePeligro();
            foreach (var condicion in edPerfil.condicionesRiesgo)
            {
                object pelPrueba = ConsultartipoPeligro(condicion.FK_Tipo_De_Peligro);
                tipoPeligro = (TipoDePeligro)pelPrueba;
                EDCondicionesRiesgoPerfil condicionRiesgo = new EDCondicionesRiesgoPerfil();
                condicionRiesgo.PKCondicionesRiesgoPerfil = condicion.PKCondicionesRiesgoPerfil;
                condicionRiesgo.descripcionPeligro = tipoPeligro.Descripcion_Del_Peligro;
                condicionRiesgo.FK_Clasificacion_De_Peligro = condicion.FK_Clasificacion_De_Peligro;
                condicionRiesgo.ClasificacionPeligro = condicion.ClasificacionPeligro;
                condicionRiesgo.tiempoExpos = condicion.tiempoExpos;
                condicionRiesgo.OtroPeligro = condicion.OtroPeligro;
                condicionesDeRiesgo.Add(condicionRiesgo);

            }
            edPerfil.condicionesRiesgo = condicionesDeRiesgo;
            edPerfil.departamentoSede = ciudadSede.DescripcionDepartamento;
            edPerfil.ciudadSede = ciudadSede.DescripcionMunicipio;
       
    
            ConsultarDatosTrabajador(edPerfil.PK_Numero_Documento_Empl);
           
       
            calcularCamposEdad(fechaIngresoEmpresa, fechaNacimiento);
            edPerfil.razonSocialEmpresa = usuarioActual.RazonSocialEmpresa;
            edPerfil.Nombre1 = primerNombre;
            edPerfil.Nombre2 = segundoNombre;
            edPerfil.Apellido1 = primerApellido;
            edPerfil.Apellido2 = segundoAPellido;
            edPerfil.fechaIngresoEmpresa = fechaIngresoEmpresa;
            edPerfil.dia = dias;
            edPerfil.mes = meses;
            edPerfil.anyos = anyos;
            edPerfil.EdadPersona = edad;
            edPerfil.EPS = eps;
            edPerfil.AFP = afp;
            edPerfil.Direccion = direccion;
            edPerfil.OcupacionPerfil = ocupacion;
            edPerfil.Cargo = cargo;
            var result=lnPerfil.ObtenerRepExcelPorPerfil(edPerfil);

            return File(result, "application/vnd.ms-excel", "Perfil Sociodemográfico"+"_"+edPerfil.PK_Numero_Documento_Empl+".xlsx");
          
        }


        public JsonResult ConsultarDatosTrabajador(string Documento)
        {
            var datos = string.Empty;
            try
            {
                var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);

                if (!string.IsNullOrEmpty(Documento))
                {

                    var sigla = usuarioActual.SiglaTipoDocumentoEmpresa;
                    var cliente = new RestSharp.RestClient(ConfigurationManager.AppSettings["Url"]);
                    var request = new RestRequest(consultaAfiliadoEmpresaActivo, RestSharp.Method.GET);
                    request.RequestFormat = DataFormat.Xml;
                    request.Parameters.Clear();
                    request.AddParameter("tpEm", usuarioActual.SiglaTipoDocumentoEmpresa);
                    request.AddParameter("docEm", usuarioActual.NitEmpresa);
                    request.AddParameter("tpAfiliado", "cc");
                    request.AddParameter("docAfiliado", Documento);
                    request.AddHeader("Content-Type", "application/json");
                    request.AddHeader("Accept", "application/json");

                    ServicePointManager.ServerCertificateValidationCallback = delegate
                    { return true; };
                    IRestResponse<List<PerfilSocioDemograficoDTO>> response = cliente.Execute<List<PerfilSocioDemograficoDTO>>(request);
                    var result = response.Content;

                    var respuesta = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PerfilSocioDemograficoDTO>>(result);

                    nitEmpresa = usuarioActual.NitEmpresa;
                    nitEmpresaU = "";
                    nitEmpresaU = respuesta[0].documentoEmp;
                    if (nitEmpresaU.Equals(nitEmpresa))
                    {
                        primerNombre = respuesta[0].nombre1;
                        segundoNombre = respuesta[0].nombre2;
                        primerApellido = respuesta[0].apellido1;
                        segundoAPellido = respuesta[0].apellido2;
                        eps = respuesta[0].nombreEps;
                        afp = respuesta[0].nombreAfp;
                        direccion = respuesta[0].dirPersona;
                        ocupacion = respuesta[0].ocupacion;
                        cargo = respuesta[0].cargo;
                        fechaNacimiento = Convert.ToDateTime(respuesta[0].fechaNacimiento);
                        fechaIngresoEmpresa =Convert.ToDateTime(respuesta[0].fechaInicioVinculacion);



                        var esValido = lnPerfil.documentoExiste(Documento);

                        if (esValido)
                        {
                            return Json(new { Data = "El perfil sociodemográfico ya existe", Mensaje = "EXISTE" });

                        }
                        return Json(new { Data = respuesta, Mensaje = "OK" }, JsonRequestBehavior.AllowGet);
                    }


                    else
                    {
                        return Json(new { Data = "El Usuario no pertenece a la empresa", Mensaje = "ERROR" });

                    }

                }

                if (Documento.Equals(""))
                {

                    return Json(new { Data = "Por favor ingrese un documento", Mensaje = "VACIO" }, JsonRequestBehavior.AllowGet);
                }
            }

            catch (Exception e)
            {
                return Json(new { Data = "El usuario no existe en el sistema SIARP", Mensaje = "CONEXION" });
            }


            // return Json(new { Data = respuesta, Mensaje = "OK" }, JsonRequestBehavior.AllowGet);

            return Json(new { Data = datos, Mensaje = "ERROR" });
            
        }


        [HttpPost]

        public ActionResult GrabarPerfilSocioDemografico(EDPerfilSocioDemografico varperfilsocidemografico, List<EDCondicionesRiesgoPerfil> condicionesRiesgo)

        {


            varperfilsocidemografico.condicionesRiesgo = condicionesRiesgo;
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
                return RedirectToAction("Login", "Home");
            }
            //bool respuestaGuardado = perfilservicio.GrabarPerfilSocioDemografico(perfilsoc,pelsed,inflab);
            try
            {
                ServiceClient.EliminarParametros();

                var result = ServiceClient.RealizarPeticionesPostJsonRestFul<EDPerfilSocioDemografico>(urlServicioPlanificacion, CapacidadPerfilSociodemogarficoGuardar, varperfilsocidemografico);
                List<EDPerfilSocioDemografico> proceso;
             
                if (result != null)
                {
                   
                    proceso = obtenerPerfilesSocioDemograficos(usuarioActual.NitEmpresa);
                    ViewBag.MensajeExitoso = "Información Almacenada Correctamente.";
                    return View("Listado",proceso.ToList());

                }
                else
                {
                    List<EDPerfilSocioDemografico> proce;
                    proce = obtenerPerfilesSocioDemograficos(usuarioActual.NitEmpresa);
                    ViewBag.MensajeError = "No se pudo Almacenar por favor intente de nuevo.";
                    return View("Listado",proce.ToList());
                }
            }
            catch (Exception e)
            {
                return View("Listado");
            }
        }

        public ActionResult EditarPerfilSocioDemografico(EDPerfilSocioDemografico varperfilsocidemografico, List<EDCondicionesRiesgoPerfil> condicionesRiesgo)
        {


            varperfilsocidemografico.condicionesRiesgo = condicionesRiesgo;
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
                return RedirectToAction("Login", "Home");
            }
            //bool respuestaGuardado = perfilservicio.GrabarPerfilSocioDemografico(perfilsoc,pelsed,inflab);
            try
            {
                ServiceClient.EliminarParametros();

                var result = ServiceClient.RealizarPeticionesPostJsonRestFul<EDPerfilSocioDemografico>(urlServicioPlanificacion, CapacidadPerfilSociodemogarficoEditar, varperfilsocidemografico);

              var proceso = obtenerPerfilesSocioDemograficos(usuarioActual.NitEmpresa);
              ViewBag.MensajeExitosoE = "Información Actualizada Correctamente.";
              return View("Listado", proceso.ToList());

              
            }
            catch (Exception e)
            {
                var result = ServiceClient.RealizarPeticionesPostJsonRestFul<EDPerfilSocioDemografico>(urlServicioPlanificacion, CapacidadPerfilSociodemogarficoEditar, varperfilsocidemografico);
                var proceso = obtenerPerfilesSocioDemograficos(usuarioActual.NitEmpresa);
                ViewBag.MensajeErrorE = "No se pudo actualizar Correctamente.";
                return View("Listado", proceso.ToList());
            }
        }

        public ActionResult ConsultarClasesPeligrosPerfil(int Pk_Tipo_Peligro)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                return View();
            }

            List<ClasificacionDePeligro> clasesDePeligrosList = clasificacionDePeligrosServicios.ConsultarClasesDePeligros(Pk_Tipo_Peligro);
            if (clasesDePeligrosList.Count != 0)
            {
                return Json(
                   clasesDePeligrosList.Select(ClasesPeligros => new
                   {
                       PK_ClasesPeligros = ClasesPeligros.PK_Clasificacion_De_Peligro,
                       ClasesDescription = ClasesPeligros.Descripcion_Clase_De_Peligro
                   })
                , JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult ExportarExcel()
        {

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
            }
            EDPerfilSocioDemografico edPerfil = new EDPerfilSocioDemografico();

            var nitEmpresa = usuarioActual.NitEmpresa;
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("nitEmpresa", nitEmpresa);
            var perfilesSocio = ServiceClient.ObtenerArrayJsonRestFul<EDPerfilSocioDemografico>(urlServicioPlanificacion, capacidadObtenerPerfilSocioDemograficoEmpresa, RestSharp.Method.GET);

            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("nitEmpresa", nitEmpresa);
            var condicionesPerfil = ServiceClient.ObtenerArrayJsonRestFul<EDCondicionesRiesgoPerfil>(urlServicioPlanificacion, capacidadObtenerCondicionesPorEmpresa, RestSharp.Method.GET);

            List<EDCondicionesRiesgoPerfil> condiciones = condicionesPerfil.ToList();

            List<EDCondicionesRiesgoPerfil> condicionesDeRiesgo = new List<EDCondicionesRiesgoPerfil>();
            TipoDePeligro tipoPeligro = new TipoDePeligro();
            foreach (var condicion in condiciones)
            {
                object pelPrueba = ConsultartipoPeligro(condicion.FK_Tipo_De_Peligro);
                tipoPeligro = (TipoDePeligro)pelPrueba;
                EDCondicionesRiesgoPerfil condicionRiesgo = new EDCondicionesRiesgoPerfil();
                condicionRiesgo.PKCondicionesRiesgoPerfil = condicion.PKCondicionesRiesgoPerfil;
                condicionRiesgo.descripcionPeligro = tipoPeligro.Descripcion_Del_Peligro;
                condicionRiesgo.FK_Clasificacion_De_Peligro = condicion.FK_Clasificacion_De_Peligro;
                condicionRiesgo.ClasificacionPeligro = condicion.ClasificacionPeligro;
                condicionRiesgo.tiempoExpos = condicion.tiempoExpos;
                condicionRiesgo.FKPerfilSocioDemografico = condicion.FKPerfilSocioDemografico;
                condicionRiesgo.OtroPeligro = condicion.OtroPeligro;
                condicionesDeRiesgo.Add(condicionRiesgo);

            }
            edPerfil.condicionesRiesgo = condicionesDeRiesgo;
        
            List<EDCondicionesRiesgoPerfil> condicionesDeRiesgoPerfil = new List<EDCondicionesRiesgoPerfil>();

            condicionesDeRiesgoPerfil = condicionesDeRiesgo;
            List<EDPerfilSocioDemografico> perfiles = new List<EDPerfilSocioDemografico>();

            foreach (var perfilesS in perfilesSocio)
            {
                EDPerfilSocioDemografico nuevoPerfil = new EDPerfilSocioDemografico();

                ConsultarDatosTrabajador(perfilesS.PK_Numero_Documento_Empl);
                nuevoPerfil.caracteristicasFisicas = perfilesS.caracteristicasFisicas;
                nuevoPerfil.caracteristicasPsicologicas = perfilesS.caracteristicasPsicologicas;
                nuevoPerfil.evaluacionesMedicasRequeridas = perfilesS.evaluacionesMedicasRequeridas;
                nuevoPerfil.nombreProceso = perfilesS.nombreProceso;
                nuevoPerfil.Cargo = cargo;
                nuevoPerfil.EPS = eps;
                nuevoPerfil.AFP = afp;
                nuevoPerfil.OcupacionPerfil = ocupacion;
                nuevoPerfil.Nombre1 = primerNombre;
                nuevoPerfil.Nombre2 = segundoNombre;
                nuevoPerfil.Apellido1 = primerApellido;
                nuevoPerfil.Apellido2 = segundoAPellido;
                nuevoPerfil.nitEmpresa = perfilesS.nitEmpresa;
                nuevoPerfil.PK_Numero_Documento_Empl = perfilesS.PK_Numero_Documento_Empl;
                nuevoPerfil.Direccion = direccion;
                nuevoPerfil.fechaIngresoEmpresa = fechaIngresoEmpresa;
                nuevoPerfil.FechaIngresoUltimoCargo = perfilesS.FechaIngresoUltimoCargo;
                nuevoPerfil.nombreSede = perfilesS.nombreSede;
                nuevoPerfil.ZonaLugar = perfilesS.ZonaLugar;
                nuevoPerfil.GradoEscolaridad = perfilesS.GradoEscolaridad;
                nuevoPerfil.Ingresos = perfilesS.Ingresos;
                nuevoPerfil.departamento = perfilesS.departamento;
                nuevoPerfil.municipio = perfilesS.municipio;
                nuevoPerfil.Conyuge = perfilesS.Conyuge;
                nuevoPerfil.Hijos = perfilesS.Hijos;
                nuevoPerfil.estrato = perfilesS.estrato;
                nuevoPerfil.estadoCivil = perfilesS.estadoCivil;
                nuevoPerfil.etnia = perfilesS.etnia;
                nuevoPerfil.razonSocialEmpresa = usuarioActual.RazonSocialEmpresa;


                calcularCamposEdad(fechaIngresoEmpresa, fechaNacimiento);
                var fk_sede = perfilesS.Pk_Id_Sede;
                ServiceClient.EliminarParametros();
                ServiceClient.AdicionarParametro("fk_sede", fk_sede);
                nuevoPerfil.EdadPersona = edad;
                nuevoPerfil.Sexo = perfilesS.Sexo;
                nuevoPerfil.vinculacionLabotal = perfilesS.vinculacionLabotal;
                nuevoPerfil.TurnoTrabajo = perfilesS.TurnoTrabajo;
                nuevoPerfil.anyos = anyos;
                nuevoPerfil.mes = meses;
                nuevoPerfil.dia = dias;
                nuevoPerfil.IDEmpleado_PerfilSocioDemoGrafico = perfilesS.IDEmpleado_PerfilSocioDemoGrafico;
                var obtenerCiudadSede = ServiceClient.ObtenerObjetoJsonRestFul<EDBusquedaMunicipio>(urlServicioPlanificacion, capacidadObtenerCiudadPorSede, RestSharp.Method.GET);
                edPerfil.condicionesRiesgo = condicionesPerfil.ToList();
                EDBusquedaMunicipio ciudadSede = new EDBusquedaMunicipio();
                ciudadSede = obtenerCiudadSede;
                nuevoPerfil.ciudadSede = ciudadSede.DescripcionMunicipio;
                nuevoPerfil.departamentoSede = ciudadSede.DescripcionDepartamento;
                nuevoPerfil.estrato = perfilesS.estrato;
                perfiles.Add(nuevoPerfil);
            }



            var result = lnPerfil.ObtenerRepExcelPorEmpresa(perfiles, condicionesDeRiesgo);

            return File(result, "application/vnd.ms-excel", "Perfil Sociodemográfico" + "_"+ usuarioActual.NitEmpresa+ ".xlsx");
          
     
          
        }

       
       
        public List<EDPerfilSocioDemografico> obtenerPerfilesSocioDemograficos(string nit)
        {

            List<EDPerfilSocioDemografico> perfiles = new List<EDPerfilSocioDemografico>();
            using (SG_SSTContext contex = new SG_SSTContext())
            {

                perfiles = (from perfil in contex.Tbl_PerfilSocioDemograficoPlanificacion
    
                            where perfil.nitEmpresa == nit
                            select new EDPerfilSocioDemografico
                            {
                                IDEmpleado_PerfilSocioDemoGrafico = perfil.IDEmpleado_PerfilSocioDemoGrafico,
                                //idtipodoc = perfil.Tipo_Documento,

                                PK_Numero_Documento_Empl = perfil.PK_Numero_Documento_Empl,

                                //Nombre1 = perfil.Nombre1,
                                //Nombre2 = perfil.Nombre2,
                                //Apellido1 = perfil.Apellido1,
                                //Apellido2 = perfil.Apellido2,
                                GradoEscolaridad = perfil.GradoEscolaridad,
                                Ingresos = perfil.Ingresos,
                                departamento = perfil.municipios.Departamento.Nombre_Departamento,
                                municipio = perfil.municipios.Nombre_Municipio,
                                //Direccion = perfil.Direccion,
                                Conyuge = perfil.Conyuge,
                                Hijos = perfil.Hijos,
                                estrato = perfil.Tbl_Estrato.Descripcion_Estrato,
                                estadoCivil = perfil.Tbl_Estado_Civil.Descripcion_EstadoCivil,
                                Sexo = perfil.Sexo,
                                //GrupoEtarios = perfil.GrupoEtarios,
                                vinculacionLabotal = perfil.Tbl_VinculacionLaboral.Descripcion_VinculacionLaboral,
                                TurnoTrabajo = perfil.TurnoTrabajo,
                                //Cargo = perfil.Cargo,
                                //fechaIngresoEmpresa = perfil.FechaIngresoEmpresa,
                                FechaIngresoUltimoCargo = perfil.FechaIngresoUltimoCargo,
                                nombreProceso=perfil.Procesos.Descripcion_Proceso,
                                nombreSede=perfil.Sede.Nombre_Sede,
                                ZonaLugar=perfil.ZonaLugar,
                            
                            }).ToList();
            }

                return perfiles;
   
        }
        

        [HttpPost]
        public JsonResult ObtenerPlantilla()
        {
            return Json(new { Data = "", Mensaje = "Success" });
        }

        public JsonResult ObtenerPlantillaReporte()
        {
            return Json(new { Data = "", Mensaje = "Success" });
        }

        public JsonResult ObtenerPlantillaPerfil(EDPerfilSocioDemografico perfil)
        {
            return Json(new { Data =perfil, Mensaje = "Success" });
        }
        public ActionResult DescargarPlantilla()
        {
            string fileName = "PlantillaPerfilSocioDemográfico.xlsx";
            string path = System.AppDomain.CurrentDomain.BaseDirectory + "Plantillas/";
            byte[] fileBytes = System.IO.File.ReadAllBytes(path + "PlantillaPerfilSocioDemográfico.xlsx");
            return File(fileBytes, "application/vnd.ms-excel", fileName);

      
        }



        [HttpPost]
        public ActionResult CargueMasivo(object form_data)
        {
            try
            {
                var objEvaluacion = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);


          
                if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
                {
                    var pic = System.Web.HttpContext.Current.Request.Files["cargarArchivo"];

                    HttpPostedFileBase file = new HttpPostedFileWrapper(pic);
                    if (file.FileName.EndsWith("xls") || file.FileName.EndsWith("xlsx"))
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        string path = string.Empty;
                        //if (int.Parse(idempresausuaria) > 0)
                        //    path = Path.Combine(rutaPlantillaAusentismo, objEvaluacion.NitEmpresa, idempresausuaria);
                        //else
                        path = Path.Combine(rutaPlantillaAusentismo, objEvaluacion.NitEmpresa);


                        if (!Directory.Exists(path))
                            Directory.CreateDirectory(path);
                   

                        path = Path.Combine(path, fileName);
                        if (System.IO.File.Exists(path))
                            System.IO.File.Delete(path);
                        file.SaveAs(path);

                       EDCarguePerfil cargue = new EDCarguePerfil();
                        //cargue.Id_Empresa_Usuaria = int.Parse(idempresausuaria);




                     
                       cargue.SiglaTipoDocumentoEmpresa = objEvaluacion.SiglaTipoDocumentoEmpresa;
                        cargue.path = path;
                        cargue.NitEmpresa = objEvaluacion.NitEmpresa;
                        cargue.rutaServicio = afiliadoempresaactivo;
                        ServiceClient.EliminarParametros();
                        var result = ServiceClient.RealizarPeticionesPostJsonRestFul<EDCarguePerfil>(urlServicioPlanificacion, CapacidadCargarPerfilSocioDemografico, cargue);

                
                        if (result != null)
                        {
                            if (result.Message.Equals("OK"))
                                return Json(new { Data = "Plantilla cargada correctamente!.", Mensaje = "Success" });
                            else
                                return Json(new { Data = result.Message, Mensaje = "ERROR" });
                        }
                        else
                            return Json(new { Data = "Se presentó un error de comunicación con el servidor; por favor intente nuevamente o comuníquese con el administrador del sistema.", Mensaje = "ERROR" });

                    }
                    else
                    {
                        return Json(new { Data = "Debe seleccionar un archivo en formato Excel con extensión .xls o .xlsx", Mensaje = "ERROR" });
                    }
                }
                else
                    return Json(new { Data = "Se presentó un error en la carga del archivo; por favor intente ingresando nuevamente o comuníquese con el administrador del sistema.", Mensaje = "ERROR" });

            }
            catch (Exception e)
            {
                return Json(new { Data = "Se presentó un error con la conexión.", Mensaje = "CONEXION" });

            }
        }



        //vista que muestra el listado de perfiles sociodemograficos creados

      
  
        public ActionResult Listado(List<EDPerfilSocioDemografico> proceso)
        {


       
            
            try
            {
                var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
                return RedirectToAction("Login", "Home");
            }
            proceso = obtenerPerfilesSocioDemograficos(usuarioActual.NitEmpresa);

            return View(proceso.ToList());
            }
            catch
            {


                return View(proceso);
            }

        }

        // GET: Empresas/Edit/5
        public ActionResult Edit(int? id)
        {
           
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
                return RedirectToAction("Login", "Home");
            }
            EDPerfilSocioDemografico edPerfil = new EDPerfilSocioDemografico();

            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("id", id);
            var perfilSocio = ServiceClient.ObtenerObjetoJsonRestFul<EDPerfilSocioDemografico>(urlServicioPlanificacion, capacidadObtenerPerfilSocioDemografico, RestSharp.Method.GET);

            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("id", id);
            var condicionesPerfil = ServiceClient.ObtenerArrayJsonRestFul<EDCondicionesRiesgoPerfil>(urlServicioPlanificacion, capacidadObtenerCondicionesPorPerfil, RestSharp.Method.GET);

            List<EDCondicionesRiesgoPerfil> condiciones = condicionesPerfil.ToList();

            var fk_sede = perfilSocio.Pk_Id_Sede;
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("fk_sede", fk_sede);
            var obtenerCiudadSede = ServiceClient.ObtenerObjetoJsonRestFul<EDBusquedaMunicipio>(urlServicioPlanificacion, capacidadObtenerCiudadPorSede, RestSharp.Method.GET);


            edPerfil = perfilSocio;
            edPerfil.condicionesRiesgo = condicionesPerfil.ToList();
           
           

            EDBusquedaMunicipio ciudadSede = new EDBusquedaMunicipio();
            ciudadSede = obtenerCiudadSede;

            ViewBag.IdDepartamento_Sede = ciudadSede.DescripcionDepartamento;
            ViewBag.IdMunicipio_Sede = ciudadSede.DescripcionMunicipio;
            ConsultarDatosTrabajador(edPerfil.PK_Numero_Documento_Empl);
            DateTime prueba = fechaIngresoEmpresa;
            ViewBag.nitEmpresa = perfilSocio.nitEmpresa;
            calcularCamposEdad(fechaIngresoEmpresa, fechaNacimiento);
            ViewBag.fechaIngresoEmpresa = fechaIngresoEmpresa.ToString("dd/MM/yyyy");
            ViewBag.FechaIngresoUltimoCargo = edPerfil.FechaIngresoUltimoCargo.ToString("dd/MM/yyyy"); 
            ViewBag.Dia = dias;
            ViewBag.Mes = meses;
            ViewBag.Anyos = anyos;
            ViewBag.Edad = edad;
            //  //ViewBag.Otro = perfilSocioDemografico.OtroPeligro; 
            ViewBag.Pk_Id_Sede = new SelectList(sedeServicio.SedesPorEmpresa(usuarioActual.IdEmpresa), "Pk_Id_Sede", "Nombre_Sede",edPerfil.Pk_Id_Sede);


            if (edPerfil.Procesos != null)
            {
                var fkProceso = (edPerfil.Procesos == null) ? 0 : edPerfil.Procesos;
                List<Proceso> procesos = procesoServicios.ObtenerProcesosPrincipales(usuarioActual.IdEmpresa);
                Proceso proceso = procesoServicios.ObtenerProceso((int)fkProceso);
                ViewBag.Procesos = new SelectList(procesos, "Pk_Id_Proceso", "Descripcion_Proceso",perfilSocio.Procesos);

            }
            else
            {
              

                List<Proceso> procesos = procesoServicios.ObtenerProcesosPrincipales(usuarioActual.IdEmpresa);
                ViewBag.Procesos = new SelectList(procesos, "Pk_Id_Proceso", "Descripcion_Proceso");


            }
            ViewBag.FK_Estrato = new SelectList(db.Tbl_Estrato, "PK_Estrato", "Descripcion_Estrato", perfilSocio.FK_Estrato);
            ViewBag.FK_Estado_Civil = new SelectList(db.Tbl_Estado_Civil, "PK_Estado_Civil", "Descripcion_EstadoCivil", edPerfil.FK_Estado_Civil);
            ViewBag.FK_VinculacionLaboral = new SelectList(db.Tbl_VinculacionLaboral, "PK_VinculacionLaboral", "Descripcion_VinculacionLaboral", edPerfil.FK_VinculacionLaboral);

            ViewBag.Fk_Id_Municipio = new SelectList(db.Tbl_Municipio.Where(m => m.Departamento.Nombre_Departamento == edPerfil.departamento), "Pk_Id_Municipio", "Nombre_Municipio", edPerfil.Fk_Id_Municipio);
            
            ViewBag.FK_Etnia = new SelectList(db.Tbl_Etnia, "PK_Etnia", "Descripcion_Etnia", edPerfil.FK_Etnia);
         
            ViewBag.Fk_Id_Departamento = new SelectList(db.Tbl_Departamento, "Pk_Id_Departamento", "Nombre_Departamento", edPerfil.Fk_Id_Departamento);

            ViewBag.evaluacionMedica = edPerfil.evaluacionesMedicasRequeridas;
            ViewBag.caracteristicasPsicologicas = edPerfil.caracteristicasPsicologicas;
            ViewBag.caracteristicasFisicas = edPerfil.caracteristicasFisicas;
            ViewBag.Nombre1 = primerNombre;
            ViewBag.Nombre2 = segundoNombre;
            ViewBag.Apellido1 = primerApellido;
            ViewBag.Apellido2 = segundoAPellido;
            ViewBag.eps = eps;
            ViewBag.afp = afp;
            ViewBag.Direccion = direccion;
            ViewBag.Cargo = cargo;
            ViewBag.Ocupacion = ocupacion;
            ViewBag.FechaIngresoUltimoCargo = edPerfil.FechaIngresoUltimoCargo.ToString("dd/M/yyyy");

           // EDCondicionesRiesgoPerfil condicionRiesgo = new EDCondicionesRiesgoPerfil();
            List<EDCondicionesRiesgoPerfil> condicionesDeRiesgo = new List<EDCondicionesRiesgoPerfil>();
            TipoDePeligro tipoPeligro = new TipoDePeligro();
            foreach (var condicion in edPerfil.condicionesRiesgo)
            {
                var clasificacionPeligro = "";
                if (condicion.ClasificacionPeligro.Equals("Otro"))
                {
                    clasificacionPeligro = condicion.OtroPeligro;
                }
                else
                {
                    clasificacionPeligro = condicion.ClasificacionPeligro;
                }
                object pelPrueba = ConsultartipoPeligro(condicion.FK_Tipo_De_Peligro);
                tipoPeligro = (TipoDePeligro)pelPrueba;
                EDCondicionesRiesgoPerfil condicionRiesgo = new EDCondicionesRiesgoPerfil();
                condicionRiesgo.PKCondicionesRiesgoPerfil = condicion.PKCondicionesRiesgoPerfil;
                condicionRiesgo.descripcionPeligro = tipoPeligro.Descripcion_Del_Peligro;
                condicionRiesgo.FK_Clasificacion_De_Peligro = condicion.FK_Clasificacion_De_Peligro;
                //condicionRiesgo.ClasificacionPeligro = condicion.ClasificacionPeligro;
                condicionRiesgo.ClasificacionPeligro = clasificacionPeligro;


                condicionRiesgo.tiempoExpos = condicion.tiempoExpos;
                condicionRiesgo.OtroPeligro = condicion.OtroPeligro;
                condicionRiesgo.Otro = condicion.OtroPeligro;
                condicionesDeRiesgo.Add(condicionRiesgo);

            }
            edPerfil.condicionesRiesgo = condicionesDeRiesgo;

            ViewBag.FK_Tipo_De_Peligro = new SelectList(db.Tbl_Tipo_De_Peligro, "PK_Tipo_De_Peligro", "Descripcion_Del_Peligro");

  
            return View(edPerfil);
        }


        public ActionResult Editar(PerfilSocioDemograficoPlanificacion perfilSocioDemografico)
        {
            using (SG_SSTContext datos = new SG_SSTContext())
            {
              
                try
                {
                    if (ModelState.IsValid)
                    {
                        db.Entry(perfilSocioDemografico).State = EntityState.Modified;
                        db.SaveChanges();

                        //var busca = datos.Tbl_PerfilSocioDemograficoPlanificacion.Where(e => e.PK_Numero_Documento_Empl > 0);
                        //ViewBag.mensaje = " Actualización datos " + perfilSocioDemografico.PK_Numero_Documento_Empl + " Realizada correctamente. ";
                        return View();
                    }
                   

                    //ViewBag.Urbano = empresa.Zona.Equals("U") ? true : false;
                    //ViewBag.Activo = empresa.Flg_Estado.Equals("Activa") ? true : false;
                    return View(perfilSocioDemografico);
                }
                catch (Exception e)
                {
                    ViewBag.mensaje1 = "Se presentó un error en la Transacción.";
                }
                return View(perfilSocioDemografico);
            }
        }

    

      

        

      
      
        public void calcularCamposEdad(DateTime fechaIngreso, DateTime fechaNacimiento)
        {
            int dia = fechaIngreso.Day;
            int mes = fechaIngreso.Month;
            int anyo = fechaIngreso.Year;
            // Fecha Nacimiento

            int diaN = fechaNacimiento.Day;
            int mesN = fechaNacimiento.Month;
            int anyoN = fechaNacimiento.Year;
            //Fecha Actual
            int diaA = int.Parse(DateTime.Today.ToString("dd"));
            int mesA = int.Parse(DateTime.Today.ToString("MM"));
            int anyoA = int.Parse(DateTime.Today.ToString("yyyy"));
            //Años- mes-dia de fecha nacimiento
            //...................................//
            anyos = anyoA - anyo;

            if (anyos != 0)
            {
                if (mesA < mes)
                {
                    anyos--;
                }
                if ((mes == mesA) && (diaA < dia))
                {
                    anyos--;
                }

            }
            // calculamos los meses

            meses = 0;
            // calculamos los meses
            if (mesA > mes)
            {
                meses = mesA - mes;
            }
            if (mesA > mes && mesA >= dia)
            {
                meses = mesA - mes;
            }
            if (mesA < mes)
            {
                meses = 12 - (mes - mesA);
            }
            if (mesA > mes && diaA < dia)
            {
                meses--;
            }
            if (mesA == mes && dia > diaA)
            {
                meses = 11;
            }
            // calculamos los dias
            dias = 0;
            if (diaA > dia)
                dias = diaA - dia;
            if (diaA < dia)
            {
                int diasMes = System.DateTime.DaysInMonth(anyoA, mesA);
                dias = diasMes - (dia - diaA);
            }
            // Edad
            edad = anyoA - anyoN;

            if (mesA < mesN)
            {
                edad--;
            }
            if ((mesN == mesA) && (diaA < diaN))
            {
                edad--;
            }


        }

        public object ConsultartipoPeligro(int peligro)
        {

             object pel=db.Tbl_Tipo_De_Peligro.Where(tp => tp.PK_Tipo_De_Peligro == peligro).FirstOrDefault();
              
             return pel;
            }
        
        public ActionResult BuscarMunicipioPorSede(int Fk_Sede)
        {
            List<EDBusquedaMunicipio> varper = null;

            using (var context = new SG_SSTContext())
            {
                varper = (from s in context.Tbl_Sede
                          join b in context.Tbl_SedeMunicipio on
                          s.Pk_Id_Sede equals b.Fk_id_Sede

                          join i in context.Tbl_Municipio on b.Fk_Id_Municipio equals i.Pk_Id_Municipio


                          join d in context.Tbl_Departamento on i.Fk_Nombre_Departamento equals d.Pk_Id_Departamento

                          where s.Pk_Id_Sede == Fk_Sede
                          select new EDBusquedaMunicipio()
                          {
                              //IDMunicipio = i.Pk_Id_Municipio,
                              DescripcionMunicipio = i.Nombre_Municipio,
                              DescripcionDepartamento = d.Nombre_Departamento

                          }).ToList();

                if (varper.Count != 0)
                {

                    return Json(new { Municipio = varper.FirstOrDefault() }
                , JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }

        }
        [HttpPost]
        public ActionResult AutoCompletarOcupacion(string prefijo)
        {
            return Json(lnPerfil.AutoCompletarOcupacion(prefijo), JsonRequestBehavior.AllowGet);
        }

    }
}