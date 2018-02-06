
namespace SG_SST.Controllers
{
    using EntidadesDominio.Empresas;
    using Models.Login;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using SG_SST.Models.Empresa;
    using ServiceRequest;
    using System.Configuration;

    public class NotificarInconsistenciasController : Base.BaseController
    {

        //servicios
        string CapacidadRelacionlaboralTiposInconsistencias = ConfigurationManager.AppSettings["CapacidadRelacionlaboralTiposInconsistencias"];
        string CapacidadRelacionlaboralGrabarNotificarInconsistenciaLaboral = ConfigurationManager.AppSettings["CapacidadRelacionlaboralGrabarNotificarInconsistenciaLaboral"];
        string CapacidadRelacionlaboralEnviarNotificarInconsistenciaLaboral = ConfigurationManager.AppSettings["CapacidadRelacionlaboralEnviarNotificarInconsistenciaLaboral"];


        // GET: /NotificarInconsistencias/
        public ActionResult Index()
        {
            NotificarInconsistenciasModels notIncMd = new NotificarInconsistenciasModels();
            EDEmpresa_Usuaria eu = new EDEmpresa_Usuaria();

            //asigna la empresa logueada
            if (Session["UsuarioSession"] != null && !String.IsNullOrWhiteSpace(Session["UsuarioSession"].ToString()))
            {
                UsuarioSessionModel usuarioSesion = new UsuarioSessionModel();
                var datosUsuario = DesEncriptar(Session["UsuarioSession"].ToString());
                var usuario = JsonConvert.DeserializeObject<UsuarioSessionModel>(datosUsuario);
                if (usuarioSesion != null)
                {
                    eu.DocumentoEmpresa = usuario.NitEmpresa;
                    eu.RazonSocial = usuario.RazonSocialEmpresa;
                }
            }

            ServiceClient.EliminarParametros();
            var result = ServiceClient.ObtenerArrayJsonRestFul<EDTipos>(urlServicioEmpresas, CapacidadRelacionlaboralTiposInconsistencias, RestSharp.Method.GET);
            if (result.Count() > 0)
            {
                notIncMd.lstTiposInconsistencias = result.Select(c => new SelectListItem()
                {
                    Value = c.Id_Tipo.ToString(),
                    Text = c.Descripcion
                }).ToList();

            }


            notIncMd.Documento_Empresa = eu.DocumentoEmpresa;
            notIncMd.Nombre_Empresa = eu.RazonSocial;

            return View(notIncMd);
        }

        public ActionResult EnvioCorreo(string dd_tipoInconsistencia, string idObservacion)
        {

            EDNotificarInconsistencia notIncon = new EDNotificarInconsistencia();
            NotificarInconsistenciasModels notIncMd = new NotificarInconsistenciasModels();
            EDEmpresa_Usuaria eu = new EDEmpresa_Usuaria();
            UsuarioSessionModel usuarioSesion = new UsuarioSessionModel();
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            

            //asigna la empresa logueada
            if (usuarioActual != null)
            {
                eu.DocumentoEmpresa = usuarioActual.NitEmpresa;
                eu.RazonSocial = usuarioActual.RazonSocialEmpresa;
                notIncon.empresa_sistema = usuarioActual.NitEmpresa + " - " + usuarioActual.RazonSocialEmpresa;
                notIncon.usuario_sistema = usuarioActual.Login + " - " + usuarioActual.NombreUsuario;
                notIncon.empresa_nit_sistema = usuarioActual.NitEmpresa;
            }
            else
            {
                ViewBag.Mensaje = "Debe estar autenticado para realizar la evalación.";
                return RedirectToAction("Login", "Home");
            }

            ServiceClient.EliminarParametros();
            var resulta = ServiceClient.ObtenerArrayJsonRestFul<EDTipos>(urlServicioEmpresas, CapacidadRelacionlaboralTiposInconsistencias, RestSharp.Method.GET);
            if (resulta.Count() > 0)
            {
                notIncMd.lstTiposInconsistencias = resulta.Select(c => new SelectListItem()
                {
                    Value = c.Id_Tipo.ToString(),
                    Text = c.Descripcion
                }).ToList();

            }


            notIncMd.Documento_Empresa = eu.DocumentoEmpresa;
            notIncMd.Nombre_Empresa = eu.RazonSocial;


            if (dd_tipoInconsistencia != null)
                notIncon.IDTipoInconsistencia = int.Parse(dd_tipoInconsistencia);

            if (idObservacion != null)
                notIncon.Observacion = idObservacion;

            notIncon.nombrePlantilla = ConfigurationManager.AppSettings["RelacionlaboralNotificacionLaboral_NombrePlantilla"];

            ServiceClient.EliminarParametros();
            var result = ServiceClient.RealizarPeticionesPostJsonRestFul<EDNotificarInconsistencia>(urlServicioEmpresas, CapacidadRelacionlaboralGrabarNotificarInconsistenciaLaboral, notIncon);

            if (result != null)
            {
                notIncon = result;
                ServiceClient.EliminarParametros();
                var result_notif = ServiceClient.RealizarPeticionesPostJsonRestFul<EDNotificarInconsistencia>(urlServicioEmpresas, CapacidadRelacionlaboralEnviarNotificarInconsistenciaLaboral, notIncon);
                if (result_notif != null)
                {                    
                    if (!result_notif.Rta)
                    {
                        ViewBag.NotificacionLaboralStatusError = "Se registró inconsistencia laboral Nro: " + result.Id.ToString() + ", el envio de correo fallo al usar el servicio de correo...";
                        ViewBag.NotificacionLaboralStatus = "";
                    }
                    else
                        ViewBag.NotificacionLaboralStatus = "Se registró satisfactoriamente la inconsistencia laboral No. " + result.Id.ToString() + " enviándose al correo electrónico " + result_notif.Email_Gerente.ToUpper() + " Próximamente un responsable de POSITIVA COMPAÑÍA DE SEGUROS S.A se comunicará con ustedes";                    
                }
                else
                {
                    ViewBag.NotificacionLaboralStatusError = "Se registró inconsistencia laboral Nro: " + result.Id.ToString() + ", el envio de correo fallo al usar el servicio de correo...";
                    ViewBag.NotificacionLaboralStatus = "";
                }
            }
            else
            {
                ViewBag.NotificacionLaboralStatusError = "Error en el registro de la incosistencia";
                ViewBag.NotificacionLaboralStatus = "";
            }

            return View("Index", notIncMd);

        }
    }
}