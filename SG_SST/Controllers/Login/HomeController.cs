// <copyright file="HomeController.cs" company="Ada SA">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Cristian Mauricio Arenas Gomez</author>
// <date>02/01/2017</date>
// <summary>Controlador que me gestiona el home de la aplicacion</summary>

namespace SG_SST.Controllers
{
    using Audotoria;
    using Base;
    using EntidadesDominio.Usuario;
    using Logica.Usuarios;
    using Models;
    using Models.AdminUsuarios;
    using Models.Empresas;
    using Models.Login;
    using RestSharp;
    using ServiceRequest;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;


    /// <summary>
    /// Controlador del Home
    /// </summary>

    public class HomeController : BaseController
    {
        SG_SSTContext context= new SG_SSTContext ();
        RegistraLog registroLog = new RegistraLog();
        string LogoEmpresa = ConfigurationManager.AppSettings["LogoEmpresa"];
        public ActionResult Mantenimiento() 
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns> 
        public ActionResult Index()
        {
            
            var usuario = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuario != null)
            {
                Session["pkempresa"] = usuario.IdEmpresa;
                ViewBag.idempresa = usuario.IdEmpresa;   
                ViewBag.Message = "Herramienta web de apoyo para la gestión realizada por las empresas afiliadas a POSITIVA frente a una auditoría del SG-SST, basado en el Decreto 1072/2015 y Resolución 1111/2017";
                ViewBag.MetaKeywords = "Herramienta web de apoyo para la gestión realizada por las empresas afiliadas a POSITIVA frente a una auditoría del SG-SST, basado en el Decreto 1072/2015 y Resolución 1111/2017";
                ViewBag.MetaDescription ="Herramienta web de apoyo para la gestión realizada por las empresas afiliadas a POSITIVA frente a una auditoría del SG-SST, basado en el Decreto 1072/2015 y Resolución 1111/2017";
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
                
        }
       

        public ActionResult ObtenerLogo(int pkempresa)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
                return View("Login", "Home");
            }
            string Archivo = "";
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Archivo = context.Tbl_Empresa.Find(pkempresa).Logo_Empresa;
                    if(Archivo!=null)
                    {
                        return Json(Archivo, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { Data = string.Empty, Mensaje = "No Existe Logo" }, JsonRequestBehavior.AllowGet);
                    }
                        
                }
                catch (Exception)
                {
                    transaction.Rollback();

                }
            }
            return Json(new { Data = string.Empty, Mensaje = "ERROR" }, JsonRequestBehavior.AllowGet);
          
        }






        [AllowAnonymous]
        [ActionName("crear-usuario-sistema")]
        public ActionResult CrearUsuarioSistema()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Login()
        {
            CerrarSessionUsuario();
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        /// 
       
        [HttpPost]
        public ActionResult Login(UsuarioSessionModel usuario)
        {
            SG_SSTContext db = new SG_SSTContext();
            if (ModelState.IsValid)
            {
                var lnUsuario = new LNUsuario();
                var objUsuario = new EDUsuarioSistema();
                objUsuario.Documento = usuario.Login;
                objUsuario.Clave = usuario.ClaveAcceso;
                objUsuario.DocumentoEmpresa = usuario.NitEmpresa;
                ServiceClient.EliminarParametros();
                //var resultado = ServiceClient.RealizarPeticionesPostJsonRestFul<EDUsuarioSistema>(urlServicioAutenticacion, capacidadAutenticacion, objUsuario);
                var resultado = lnUsuario.AutenticarUsuario(objUsuario);
                if (resultado != null)
                {
                    usuario.IdEmpresa = resultado.CodEmpresa;
                    usuario.NitEmpresa = resultado.DocumentoEmpresa;
                    usuario.RazonSocialEmpresa = resultado.RazonSocial;
                    usuario.IdUsuario = resultado.IdUsuarioSistema;
                    usuario.Login = resultado.Documento;
                    usuario.NombreUsuario = resultado.Nombres + " " + resultado.Apellidos;
                    usuario.Documento = resultado.Documento;
                    usuario.CantidadDiasLaborales = 5;
                    usuario.PrimerAcceso = resultado.PrimerAcceso;
                    usuario.SiglaTipoDocumentoEmpleado = resultado.TipoDocumentoSigla;
                    usuario.SiglaTipoDocumentoEmpresa = resultado.TipoDocumentoSiglaEmpresa;
                    GuardarSesionUsuario(usuario);
                    return RedirectToAction("Index");
                    if (resultado.Activo)
                    {
                        var resultadoEmp = 0;
                        var resultadoAfil = 0;
                     ConsultarAfiliadoEmpresaActivos(resultado.TipoDocumentoSiglaEmpresa, resultado.DocumentoEmpresa, resultado.TipoDocumentoSigla, resultado.Documento, out resultadoEmp, out resultadoAfil);
                        //la empresa existe y se encuentra activa
                        if (resultadoEmp == (int)Enumeraciones.EnumAdministracionUsuarios.ValidaEstadoEmpAfi.ExisteActivoEmp)
                        {
                            //el empleado existe y se encuentra activo
                            if (resultadoAfil == (int)Enumeraciones.EnumAdministracionUsuarios.ValidaEstadoEmpAfi.ExisteActivoAfi)
                            
                            {
                                SG_SST.Reportes.RecursoParametros.NitEmpresa = "";
                        
                               
                                usuario.IdEmpresa = resultado.CodEmpresa;
                                usuario.NitEmpresa = resultado.DocumentoEmpresa;
                                usuario.RazonSocialEmpresa = resultado.RazonSocial;
                                usuario.IdUsuario = resultado.IdUsuarioSistema;
                                usuario.Login = resultado.Documento;
                                usuario.NombreUsuario = resultado.Nombres + " " + resultado.Apellidos;
                                usuario.Documento = resultado.Documento;
                                usuario.CantidadDiasLaborales = 5;
                                usuario.PrimerAcceso = resultado.PrimerAcceso;
                                usuario.SiglaTipoDocumentoEmpleado = resultado.TipoDocumentoSigla;
                                usuario.SiglaTipoDocumentoEmpresa = resultado.TipoDocumentoSiglaEmpresa;

                                GuardarSesionUsuario(usuario);
                                if (usuario.PrimerAcceso)
                                    return RedirectToAction("CambiarClave", "AdminUsuarios");
                                else
                                    
                                    return RedirectToAction("Index");
                            }
                            else if ((int)Enumeraciones.EnumAdministracionUsuarios.ValidaEstadoEmpAfi.NoExisteAfi == resultadoAfil)
                                TempData["mensajeAutenticacion"] = Recursos.AdministracionUsuarios.MensajesAdministracionUsuarios.EmpleadoNoExisteLogin;
                            else
                                TempData["mensajeAutenticacion"] = Recursos.AdministracionUsuarios.MensajesAdministracionUsuarios.EmpleadoInactivoLogin;
                        }
                        else if ((int)Enumeraciones.EnumAdministracionUsuarios.ValidaEstadoEmpAfi.NoExisteEmp == resultadoEmp)
                            TempData["mensajeAutenticacion"] = Recursos.AdministracionUsuarios.MensajesAdministracionUsuarios.EmpresaNoExisteLogin;
                        else
                            TempData["mensajeAutenticacion"] = Recursos.AdministracionUsuarios.MensajesAdministracionUsuarios.EmpresaInactivaLogin;
                        return View("Login");
                    }
                    else
                    {
                        TempData["mensajeAutenticacion"] = Recursos.AdministracionUsuarios.MensajesAdministracionUsuarios.UsuarioInactivoPorSistema;
                        return View("Login");
                    }
                }
                else
                {
                    TempData["mensajeAutenticacion"] = "No existe un usuario asociado al documento y clave ingresados.";
                    return RedirectToAction("Login");
                }
            }
            else
            {
                return View(usuario);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult CerrarSesion()
        {
            CerrarSessionUsuario();
            return RedirectToAction("Login");
        }

        /// <summary>
        /// 
        /// </summary>
        private void CerrarSessionUsuario()
        {
            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tipoDocumentoEmp"></param>
        /// <param name="numDocumentoEmp"></param>
        /// <param name="tipoDocumento"></param>
        /// <param name="numDucumento"></param>
        /// <param name="resultadoEmp"></param>
        /// <param name="resultadoAfi"></param>
        /// <returns></returns>
        private EmpresaAfiliadoModel ConsultarAfiliadoEmpresaActivos(string tipoDocumentoEmp, string numDocumentoEmp, string tipoDocumento, string numDucumento, out int resultadoEmp, out int resultadoAfi)
        {
            try
            {
                EmpresaAfiliadoModel objEmpresaAfi = null;
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
                IRestResponse<List<EmpresaAfiliadoModel>> response = cliente.Execute<List<EmpresaAfiliadoModel>>(request);
                var result = response.Content;
                if (!string.IsNullOrWhiteSpace(result))
                {
                    var respuesta = Newtonsoft.Json.JsonConvert.DeserializeObject<List<EmpresaAfiliadoModel>>(result);
                    if (respuesta.Count == 0)
                        resultadoEmp = (int)Enumeraciones.EnumAdministracionUsuarios.ValidaEstadoEmpAfi.NoExisteEmp; //No existe
                    else
                    {
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
                }
                return objEmpresaAfi;
            }
            catch (Exception ex)
            {
                registroLog.RegistrarError(typeof(HomeController), string.Format("Error en la Acción ConsultarAfiliadoEmpresaActivos: {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                resultadoEmp = (int)Enumeraciones.EnumAdministracionUsuarios.ValidaEstadoEmpAfi.NoExisteEmp;
                resultadoAfi = (int)Enumeraciones.EnumAdministracionUsuarios.ValidaEstadoEmpAfi.NoExisteAfi;
                return null;
            }

        }
    }
}