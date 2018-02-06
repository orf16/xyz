using RestSharp;
using SG_SST.Audotoria;
using SG_SST.Controllers.Base;
using SG_SST.EntidadesDominio.Usuario;
using SG_SST.Helpers;
using SG_SST.Logica.Ausentismo;
using SG_SST.Logica.Empresas;
using SG_SST.Logica.Usuarios;
using SG_SST.Models.AdminUsuarios;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SG_SST.Controllers.AdminUsuarios
{
    [AllowAnonymous]
    public class AdminUsuariosController : BaseController
    {
        RegistraLog registroLog = new RegistraLog();
        private string claveTemporalUsuarios = ConfigurationManager.AppSettings["ClaveTemporalUsuarioSistema"];
        private string Usupuedeaprobar = ConfigurationManager.AppSettings["Usupuedeaprobar"];
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var lnUsuario = new LNUsuario();
            var adminSistema = new AdministrarUsuariosModel();
            adminSistema.TiposDocumento = lnUsuario.ObtenerTiposDocumento().Select(td => new SelectListItem()
            {
                Value = td.Sigla,
                Text = td.Descripcion
            }).ToList();
            adminSistema.ConfiguracionPreguntasSeguridad = new ConfiguracionPreguntasSeguridad()
            {
                PreguntasSeguridad = lnUsuario.ObtenerPreguntasSeguridad().Select(ps => new SelectListItem()
                {
                    Value = ps.IdPreguntaSeguridad.ToString(),
                    Text = ps.NombrePreguntaSeguridad
                }).ToList()
            };
            adminSistema.RolesRegistrados = lnUsuario.ObtenerRolesSistema().Select(rs => new SelectListItem()
            {
                Value = rs.IdRolSistema.ToString(),
                Text = rs.NombreRol
            }).ToList();
            return View(adminSistema);
        }

        /// <summary>
        /// Registra un nuevo usuario para ser aprobado. Previo a esto se valida
        /// la siguiente información:
        /// 1. Que la empresa exista y se encuentra activa
        /// 2. Que el empleado exista y se encuentre activo
        /// 3. si el rol del usuario a aprobar es diferente de Representante legal,
        /// se debe validar que dicha empresa tenga ya creado y aprobado un usuario
        /// con rol de representante legal.
        /// 4. Se debe validar que no se hayan superado el la cantidad de usuarios para
        /// el rol a crear.
        /// </summary>
        /// <param name="adminSistema"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(AdministrarUsuariosModel adminSistema)
        {
            var lnUsuario = new LNUsuario();
            if (!ModelState.IsValid)
            {
                adminSistema.TiposDocumento = lnUsuario.ObtenerTiposDocumento().Select(td => new SelectListItem()
                {
                    Value = td.Sigla,
                    Text = td.Descripcion
                }).ToList();
                adminSistema.ConfiguracionPreguntasSeguridad = new ConfiguracionPreguntasSeguridad()
                {
                    PreguntasSeguridad = lnUsuario.ObtenerPreguntasSeguridad().Select(ps => new SelectListItem()
                    {
                        Value = ps.IdPreguntaSeguridad.ToString(),
                        Text = ps.NombrePreguntaSeguridad
                    }).ToList()
                };
                adminSistema.RolesRegistrados = lnUsuario.ObtenerRolesSistema().Select(rs => new SelectListItem()
                {
                    Value = rs.IdRolSistema.ToString(),
                    Text = rs.NombreRol
                }).ToList();
                return View(adminSistema);
            }
            else
            {
                int resultEmpresa = (int)Enumeraciones.EnumAdministracionUsuarios.ValidaEstadoEmpAfi.NoExisteEmp;
                int resultEmpleado = (int)Enumeraciones.EnumAdministracionUsuarios.ValidaEstadoEmpAfi.NoExisteAfi;
                var objEmpAfil = ConsultaSIARP.ConsultarAfiliadoEmpresaActivos(adminSistema.TipoDocumentoEmpresa, adminSistema.DocumentoEmpresa, adminSistema.TipoDocumento, adminSistema.Documento, out resultEmpresa, out resultEmpleado);
                //la empresa existe y se encuentra activa
                if (resultEmpresa == (int)Enumeraciones.EnumAdministracionUsuarios.ValidaEstadoEmpAfi.ExisteActivoEmp)
                {
                    //el empleado existe y se encuentra activo
                    if (resultEmpleado == (int)Enumeraciones.EnumAdministracionUsuarios.ValidaEstadoEmpAfi.ExisteActivoAfi)
                    {
                        var resultExistenciaUsuarioRL = lnUsuario.ValidarExistenciaUsusarioReprLegal(adminSistema.TipoDocumentoEmpresa, adminSistema.DocumentoEmpresa, (int)Enumeraciones.EnumAdministracionUsuarios.RolesSistema.RepresentanteLegal);
                        //valida que el usuario con rol especificado exista y se encuentre activo
                        if ((int)Enumeraciones.EnumAdministracionUsuarios.ValidaEstadoObjeto.ExisteActivo == resultExistenciaUsuarioRL)
                        {
                            var resultCantUsuarios = lnUsuario.validarCantidadUsuariosPorRol(adminSistema.TipoDocumentoEmpresa, adminSistema.DocumentoEmpresa, adminSistema.IdRolSeleccionado);
                            //valida la cantidad máxima de usuarios por rol que se pueden crear para la empresa
                            if ((int)Enumeraciones.EnumAdministracionUsuarios.ValidaCantidadUsuariosPorRol.SePuedeCrear == resultCantUsuarios)
                            {
                                //se registra un nuevo usuario en el sistema
                                adminSistema.Nombres = string.Format("{0} {1}", objEmpAfil.nombre1, objEmpAfil.nombre2);
                                adminSistema.Apellidos = string.Format("{0} {1}", objEmpAfil.apellido1, objEmpAfil.apellido2);
                                ViewBag.Mensaje = CrearUsuarioSistema(adminSistema);
                            }
                            else if ((int)Enumeraciones.EnumAdministracionUsuarios.ValidaCantidadUsuariosPorRol.NoSePuedeCrearTodosAprobados == resultCantUsuarios)
                                ViewBag.Mensaje = Recursos.AdministracionUsuarios.MensajesAdministracionUsuarios.CantidadUsuariosAprobadosPorRolSuperada;
                            else
                                ViewBag.Mensaje = Recursos.AdministracionUsuarios.MensajesAdministracionUsuarios.CantidadUsuariosPorRolSuperada;
                        }
                        else if ((int)Enumeraciones.EnumAdministracionUsuarios.ValidaEstadoObjeto.NoExiste == resultExistenciaUsuarioRL)
                        {
                            if (adminSistema.IdRolSeleccionado == (int)Enumeraciones.EnumAdministracionUsuarios.RolesSistema.RepresentanteLegal)
                            {
                                //se registra un nuevo usuario en el sistema
                                adminSistema.Nombres = string.Format("{0} {1}", objEmpAfil.nombre1, objEmpAfil.nombre2);
                                adminSistema.Apellidos = string.Format("{0} {1}", objEmpAfil.apellido1, objEmpAfil.apellido2);
                                ViewBag.Mensaje = CrearUsuarioSistema(adminSistema);
                            }
                            else
                                ViewBag.Mensaje = Recursos.AdministracionUsuarios.MensajesAdministracionUsuarios.UsuarioPorRolNoExiste;
                        }
                        else if ((int)Enumeraciones.EnumAdministracionUsuarios.ValidaEstadoObjeto.ExistePorAprobar == resultExistenciaUsuarioRL)
                            ViewBag.Mensaje = Recursos.AdministracionUsuarios.MensajesAdministracionUsuarios.UsuarioPorRolPorAprobar;
                        else
                            ViewBag.Mensaje = Recursos.AdministracionUsuarios.MensajesAdministracionUsuarios.UsuarioPorRolInactivo;
                    }
                    else if ((int)Enumeraciones.EnumAdministracionUsuarios.ValidaEstadoEmpAfi.NoExisteAfi == resultEmpleado)
                        ViewBag.Mensaje = Recursos.AdministracionUsuarios.MensajesAdministracionUsuarios.EmpleadoNoExiste;
                    else
                        ViewBag.Mensaje = Recursos.AdministracionUsuarios.MensajesAdministracionUsuarios.EmpleadoInactivo;
                }
                else if ((int)Enumeraciones.EnumAdministracionUsuarios.ValidaEstadoEmpAfi.NoExisteEmp == resultEmpresa)
                    ViewBag.Mensaje = Recursos.AdministracionUsuarios.MensajesAdministracionUsuarios.EmpresaNoExiste;
                else
                    ViewBag.Mensaje = Recursos.AdministracionUsuarios.MensajesAdministracionUsuarios.EmpresaInactiva;
            }
            var usuario = new AdministrarUsuariosModel();
            usuario.TiposDocumento = lnUsuario.ObtenerTiposDocumento().Select(td => new SelectListItem()
            {
                Value = td.Sigla,
                Text = td.Descripcion
            }).ToList();
            usuario.ConfiguracionPreguntasSeguridad = new ConfiguracionPreguntasSeguridad()
            {
                PreguntasSeguridad = lnUsuario.ObtenerPreguntasSeguridad().Select(ps => new SelectListItem()
                {
                    Value = ps.IdPreguntaSeguridad.ToString(),
                    Text = ps.NombrePreguntaSeguridad
                }).ToList()
            };
            usuario.RolesRegistrados = lnUsuario.ObtenerRolesSistema().Select(rs => new SelectListItem()
            {
                Value = rs.IdRolSistema.ToString(),
                Text = rs.NombreRol
            }).ToList();
            return View(usuario);
        }

        /// <summary>
        /// Consulta información de la empresa y usuario ante SIARP
        /// y devuelve un json con los nombres y apellidos del usuario 
        /// asociado a la empresa consultada.
        /// </summary>
        /// <param name="tipoDocumentoEmp"></param>
        /// <param name="numDocumentoEmp"></param>
        /// <param name="tipoDocumento"></param>
        /// <param name="numDucumento"></param>
        /// <param name="resultadoEmp"></param>
        /// <param name="resultadoAfi"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ConsultarInformacionUsuarioEmpresaSiarp(string tipoDocumentoEmp, string numDocumentoEmp, string tipoDocumento, string numDucumento)
        {
            var resultadoEmp = (int)Enumeraciones.EnumAdministracionUsuarios.ValidaEstadoEmpAfi.NoExisteEmp;
            var resultadoAfi = (int)Enumeraciones.EnumAdministracionUsuarios.ValidaEstadoEmpAfi.NoExisteAfi;
            var resultado = ConsultaSIARP.ConsultarAfiliadoEmpresaActivos(tipoDocumentoEmp, numDocumentoEmp, tipoDocumento, numDucumento, out resultadoEmp, out resultadoAfi);
            //la empresa existe y se encuentra activa
            if (resultadoEmp == (int)Enumeraciones.EnumAdministracionUsuarios.ValidaEstadoEmpAfi.ExisteActivoEmp)
            {
                //el empleado existe y se encuentra activo
                if (resultadoAfi == (int)Enumeraciones.EnumAdministracionUsuarios.ValidaEstadoEmpAfi.ExisteActivoAfi)
                {
                    var lnUsuario = new LNUsuario();
                    var usuarioRecupClave = lnUsuario.ConsultarDatosUsuarioPorRelacionLaboral(tipoDocumentoEmp, numDocumentoEmp, tipoDocumento, numDucumento);
                    return Json(new
                    {
                        NombresUsuario = string.Format("{0} {1}", resultado.nombre1, resultado.nombre2),
                        ApellidosUsuario = string.Format("{0} {1}", resultado.apellido1, resultado.apellido2),
                        RazonSocialEmpresa = resultado.razonSocial,
                        MunicipioSedePpalEmrpresa = resultado.nomMunEmpresa,
                        PreguntasSeguridad = usuarioRecupClave.PreguntasSeguridad,
                        MensajeError = string.Empty,
                        Estado = "OK"
                    });
                }
                else if ((int)Enumeraciones.EnumAdministracionUsuarios.ValidaEstadoEmpAfi.NoExisteAfi == resultadoAfi)
                    return Json(new
                    {
                        NombresUsuario = string.Empty,
                        ApellidosUsuario = string.Empty,
                        RazonSocialEmpresa = string.Empty,
                        MunicipioSedePpalEmrpresa = string.Empty,
                        MensajeError = Recursos.AdministracionUsuarios.MensajesAdministracionUsuarios.EmpleadoNoExiste,
                        Estado = "OK"
                    });
                else
                    return Json(new { NombresUsuario = string.Empty, ApellidosUsuario = string.Empty, MensajeError = Recursos.AdministracionUsuarios.MensajesAdministracionUsuarios.EmpleadoInactivo, Estado = "OK" });
            }
            else if ((int)Enumeraciones.EnumAdministracionUsuarios.ValidaEstadoEmpAfi.NoExisteEmp == resultadoEmp)
                return Json(new
                {
                    NombresUsuario = string.Empty,
                    ApellidosUsuario = string.Empty,
                    RazonSocialEmpresa = string.Empty,
                    MunicipioSedePpalEmrpresa = string.Empty,
                    MensajeError = Recursos.AdministracionUsuarios.MensajesAdministracionUsuarios.EmpresaNoExiste,
                    Estado = "OK"
                });
            else
                return Json(new
                {
                    NombresUsuario = string.Empty,
                    ApellidosUsuario = string.Empty,
                    RazonSocialEmpresa = string.Empty,
                    MunicipioSedePpalEmrpresa = string.Empty,
                    MensajeError = Recursos.AdministracionUsuarios.MensajesAdministracionUsuarios.EmpresaInactiva,
                    Estado = "OK"
                });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult AprobarUsuariosSistema()
        {
            //var adminSistema = lnUsuario.ObtenerUsuariosParaAprobar(string.Empty, string.Empty, string.Empty).Select(ua => new AdministrarUsuariosModel() {
            //    IdUsuarioSistema = ua.IdUsuarioPorAprobar,
            //    IdRolSeleccionado = ua.IdUsuarioPorAprobar,
            //    Nombres = ua.Nombres,
            //    Apellidos = ua.Apellidos,
            //    TipoDocumentoEmpresa = ua.TipoDocumentoEmpresa,
            //    DocumentoEmpresa = ua.NumDocumentoEmpresa,
            //    RazonSocialEmpresa = ua.RazonSocialEmpresa,
            //    MunicipioSedePpalEmpresa = ua.MunicipioSedePpalEmpresa,
            //    TipoDocumento = ua.TipoDocumentoAfi,
            //    Documento = ua.NumDocumentoAfi,
            //    NombreRolSeleccionado = ua.NombreRol,
            //    EmailPersona = ua.Correo,
            //    CausalesRechazoUsuarioSistema = lnUsuario.ObtenerCausalesRechazoUsuariosSistema().Select(crs => new SelectListItem()
            //    {
            //        Value = crs.IdCausalRechazo.ToString(),
            //        Text = crs.NombreCausalRechazo
            //    }).ToList()
            //}).ToList();
            var objUsuarioSesion = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (objUsuarioSesion == null)
            {
                ViewBag.Mensaje = "La sesion a finalizado.";
                return RedirectToAction("Login", "Home");
            }

            string[] administradores = Usupuedeaprobar.Split(',');
            if (!administradores.Contains(objUsuarioSesion.Login))
            {
                ViewBag.Mensaje = "Lo sentimos, este usuario no cuenta con los permisos para ingresar a esta funcionalidad.";
                return View();
            }


            var lnUsuario = new LNUsuario();
            ViewBag.RolesSistema = lnUsuario.ObtenerRolesSistema().Select(rs => new SelectListItem()
            {
                Value = rs.IdRolSistema.ToString(),
                Text = rs.NombreRol
            }).ToList();

            //var adminSistema = new List<AdministrarUsuariosModel>()
            //{
            //    new AdministrarUsuariosModel()
            //    {
            //        BuscadorUsuariosSistema = new BuscardorModel()
            //        {
            //            RolesSistema = lnUsuario.ObtenerRolesSistema().Select(rs => new SelectListItem()
            //            {
            //                Value = rs.IdRolSistema.ToString(),
            //                Text = rs.NombreRol
            //            }).ToList()
            //        }
            //    }
            //};
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuariosAprobar"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AprobarUsuariosSistema(List<AdministrarUsuariosModel> usuariosAprobar)
        {
            var lnUsuario = new LNUsuario();
            var usuariosSistema = usuariosAprobar.Select(u => new EDUsuarioPorAprobar()
            {
                IdUsuarioPorAprobar = u.IdUsuarioSistema,
                TipoDocumentoEmpresa = u.TipoDocumentoEmpresa,
                NumDocumentoEmpresa = u.DocumentoEmpresa,
                TipoDocumentoAfi = u.TipoDocumento,
                NumDocumentoAfi = u.Documento,
                Aprobado = string.IsNullOrEmpty(u.IdCausalRechazoSeleccionada) ? u.Aprobado : false,
                Nombres = string.IsNullOrEmpty(u.Nombres) ? string.Empty : u.Nombres.TrimStart().TrimEnd(),
                Apellidos = string.IsNullOrEmpty(u.Apellidos) ? string.Empty : u.Apellidos.TrimStart().TrimEnd(),
                Correo = string.IsNullOrEmpty(u.EmailPersona) ? string.Empty : u.EmailPersona.TrimStart().TrimEnd(),
                NombreCausalRechazo = string.IsNullOrEmpty(u.CausalRechazoSeleccionada) ? string.Empty : u.CausalRechazoSeleccionada.TrimStart().TrimEnd(),
                PeriodoInactividad = u.PeriodoInactividad
            }).ToList();
            var result = lnUsuario.RegistrarUsusariosPorEmpresa(usuariosSistema);
            if (result)
            {
                var adminSistema = lnUsuario.ObtenerUsuariosParaAprobar(string.Empty, string.Empty, string.Empty, 1).Select(ua => new AdministrarUsuariosModel()
                {
                    IdUsuarioSistema = ua.IdUsuarioPorAprobar,
                    IdRolSeleccionado = ua.IdUsuarioPorAprobar,
                    Nombres = ua.Nombres,
                    Apellidos = ua.Apellidos,
                    TipoDocumentoEmpresa = ua.TipoDocumentoEmpresa,
                    DocumentoEmpresa = ua.NumDocumentoEmpresa,
                    TipoDocumento = ua.TipoDocumentoAfi,
                    Documento = ua.NumDocumentoAfi,
                    NombreRolSeleccionado = ua.NombreRol,
                    EmailPersona = ua.Correo,
                    CausalesRechazoUsuarioSistema = lnUsuario.ObtenerCausalesRechazoUsuariosSistema().Select(crs => new SelectListItem()
                    {
                        Value = crs.IdCausalRechazo.ToString(),
                        Text = crs.NombreCausalRechazo
                    }).ToList()
                }).ToList();
                var datos = RenderRazorViewToString("_AprobarUsuarioSistema", adminSistema);
                return Json(new { Datos = datos, Mensaje = "OK" });
            }
            else
                return Json(new { Datos = string.Empty, Mensaje = "NOTFOUND" });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="numDocEmp"></param>
        /// <param name="numDocUsu"></param>
        /// <param name="rolSeleccionado"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult BuscarUsuariosAprobarBuscador(string numDocEmp, string numDocUsu, string rolSeleccionado, int paginaActual = 1)
        {
            var lnUsuario = new LNUsuario();
            var cantRegistrosPag = Convert.ToInt32(cantidadRegistrosPagina);
            var adminSistema = lnUsuario.ObtenerUsuariosParaAprobar(numDocEmp, numDocUsu, rolSeleccionado, paginaActual);
            var totalRegistros = lnUsuario.ObtenerTotalUsuariosParaAprobar(numDocEmp, numDocUsu, rolSeleccionado);
            if (adminSistema != null && adminSistema.Count() > 0)
            {
                var usuariosAprobar = adminSistema.Select(ua => new AdministrarUsuariosModel()
                {
                    IdUsuarioSistema = ua.IdUsuarioPorAprobar,
                    IdRolSeleccionado = ua.IdUsuarioPorAprobar,
                    Nombres = ua.Nombres,
                    Apellidos = ua.Apellidos,
                    TipoDocumentoEmpresa = ua.TipoDocumentoEmpresa,
                    DocumentoEmpresa = ua.NumDocumentoEmpresa,
                    RazonSocialEmpresa = ua.RazonSocialEmpresa,
                    MunicipioSedePpalEmpresa = ua.MunicipioSedePpalEmpresa,
                    TipoDocumento = ua.TipoDocumentoAfi,
                    Documento = ua.NumDocumentoAfi,
                    NombreRolSeleccionado = ua.NombreRol,
                    EmailPersona = ua.Correo,
                    CausalesRechazoUsuarioSistema = lnUsuario.ObtenerCausalesRechazoUsuariosSistema().Select(crs => new SelectListItem()
                    {
                        Value = crs.IdCausalRechazo.ToString(),
                        Text = crs.NombreCausalRechazo
                    }).ToList()
                }).ToList();
                var totalPagPaginador = 0;
                var residuo = totalRegistros % cantRegistrosPag;
                if (residuo == 0)
                    totalPagPaginador = totalRegistros / cantRegistrosPag;
                else
                    totalPagPaginador = (totalRegistros / cantRegistrosPag) + 1;
                ViewBag.TotalPagPaginador = totalPagPaginador;
                ViewBag.PaginaActual = paginaActual;
                var datos = RenderRazorViewToString("_AprobarUsuarioSistema", usuariosAprobar);
                return Json(new { Datos = datos, TotalUsuarios = totalRegistros, RegistrosPorPag = cantRegistrosPag, PaginaActual = paginaActual, Mensaje = "OK" });
            }
            else
            {
                return Json(new { Datos = string.Empty, TotalUsuarios = totalRegistros, RegistrosPorPag = cantRegistrosPag, PaginaActual = paginaActual, Mensaje = "NOTFOUND" });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult CambiarClave()
        {
            var usuarioSession = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioSession != null)
            {
                var usuario = new CambiarClaveModel();
                usuario.IdUsuarioSession = usuarioSession.IdUsuario;
                usuario.AceptaTerminosCondiciones = false;
                return View(usuario);
            }
            else
                return RedirectToAction("CerrarSesion", "Home");
        }

        public ActionResult RecuperarClave()
        {
            var lnUsuario = new LNUsuario();
            var adminSistema = new AdministrarUsuariosModel();
            adminSistema.TiposDocumento = lnUsuario.ObtenerTiposDocumento().Select(td => new SelectListItem()
            {
                Value = td.Sigla,
                Text = td.Descripcion
            }).ToList();
            return View(adminSistema);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CambiarClave(CambiarClaveModel usuario)
        {
            if (ModelState.IsValid)
            {
                var result = false;
                var lnUsuario = new LNUsuario();
                if (!usuario.AceptaTerminosCondiciones)
                    return RedirectToAction("CerrarSesion", "Home");
                var usuarioSession = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
                if (usuarioSession != null)
                {
                    if (usuario.IdUsuarioSession == usuarioSession.IdUsuario)
                    {
                        var usuarioRegistrar = new EDUsuarioSistema()
                        {
                            IdUsuarioSistema = usuario.IdUsuarioSession,
                            Clave = usuario.Clave
                        };
                        result = lnUsuario.CambiarClaveUsuario(usuarioRegistrar);
                    }
                    if (result)
                        //return RedirectToAction("Index", "Home");
                        return RedirectToAction("CerrarSesion", "Home");
                    else
                        return RedirectToAction("CerrarSesion", "Home");
                }
                else
                    return RedirectToAction("CerrarSesion", "Home");
            }
            else
                return View(usuario);
        }

        [HttpPost]
        public ActionResult RecuperarClave(AdministrarUsuariosModel adminSistema)
        {
            var lnUsuario = new LNUsuario();
            if (string.IsNullOrEmpty(adminSistema.TipoDocumentoEmpresa) ||
                string.IsNullOrEmpty(adminSistema.DocumentoEmpresa) ||
                string.IsNullOrEmpty(adminSistema.TipoDocumento) ||
                string.IsNullOrEmpty(adminSistema.Documento))
            {
                adminSistema.TiposDocumento = lnUsuario.ObtenerTiposDocumento().Select(td => new SelectListItem()
                {
                    Value = td.Sigla,
                    Text = td.Descripcion
                }).ToList();
                adminSistema.RolesRegistrados = lnUsuario.ObtenerRolesSistema().Select(rs => new SelectListItem()
                {
                    Value = rs.IdRolSistema.ToString(),
                    Text = rs.NombreRol
                }).ToList();
                return View(adminSistema);
            }
            else
            {
                int resultEmpresa = (int)Enumeraciones.EnumAdministracionUsuarios.ValidaEstadoEmpAfi.NoExisteEmp;
                int resultEmpleado = (int)Enumeraciones.EnumAdministracionUsuarios.ValidaEstadoEmpAfi.NoExisteAfi;
                var objEmpAfil = ConsultaSIARP.ConsultarAfiliadoEmpresaActivos(adminSistema.TipoDocumentoEmpresa, adminSistema.DocumentoEmpresa, adminSistema.TipoDocumento, adminSistema.Documento, out resultEmpresa, out resultEmpleado);
                //la empresa existe y se encuentra activa
                if (resultEmpresa == (int)Enumeraciones.EnumAdministracionUsuarios.ValidaEstadoEmpAfi.ExisteActivoEmp)
                {
                    //el empleado existe y se encuentra activo
                    if (resultEmpleado == (int)Enumeraciones.EnumAdministracionUsuarios.ValidaEstadoEmpAfi.ExisteActivoAfi)
                    {
                        //obtiene el usuario que desea recuperar su clave
                        var respuesta = string.Empty;
                        var result = lnUsuario.RecuperarClaveUsuario(adminSistema.TipoDocumentoEmpresa, adminSistema.DocumentoEmpresa, adminSistema.TipoDocumento, adminSistema.Documento,
                            adminSistema.ConfiguracionPreguntasSeguridad.RespuestaUno,
                            adminSistema.ConfiguracionPreguntasSeguridad.RespuestaDos,
                            adminSistema.ConfiguracionPreguntasSeguridad.RespuestaTres, out respuesta);
                        if (result)
                            ViewBag.Mensaje = "El proceso de recuperación de clave fue exitoso. A su correo se le enviará una clave temporal. Con esta clave podrá acceder al sistema y realizar el proceso de cambio de clave.";
                        else
                        {
                            if (string.IsNullOrEmpty(respuesta))
                                ViewBag.Mensaje = "El proceso de recuperación de clave no se pudo completar. Por favor intente nuevamente.";
                            else
                                ViewBag.Mensaje = respuesta;
                        }
                    }
                    else if ((int)Enumeraciones.EnumAdministracionUsuarios.ValidaEstadoEmpAfi.NoExisteAfi == resultEmpleado)
                        ViewBag.Mensaje = Recursos.AdministracionUsuarios.MensajesAdministracionUsuarios.EmpleadoNoExiste;
                    else
                        ViewBag.Mensaje = Recursos.AdministracionUsuarios.MensajesAdministracionUsuarios.EmpleadoInactivo;
                }
                else if ((int)Enumeraciones.EnumAdministracionUsuarios.ValidaEstadoEmpAfi.NoExisteEmp == resultEmpresa)
                    ViewBag.Mensaje = Recursos.AdministracionUsuarios.MensajesAdministracionUsuarios.EmpresaNoExiste;
                else
                    ViewBag.Mensaje = Recursos.AdministracionUsuarios.MensajesAdministracionUsuarios.EmpresaInactiva;
            }
            var usuario = new AdministrarUsuariosModel();
            usuario.TiposDocumento = lnUsuario.ObtenerTiposDocumento().Select(td => new SelectListItem()
            {
                Value = td.Sigla,
                Text = td.Descripcion
            }).ToList();
            usuario.RolesRegistrados = lnUsuario.ObtenerRolesSistema().Select(rs => new SelectListItem()
            {
                Value = rs.IdRolSistema.ToString(),
                Text = rs.NombreRol
            }).ToList();
            return View(usuario);
        }

        /// <summary>
        /// Obtiene la ruta donde se encuentra el archivo de términos y condiciones
        /// </summary>
        /// <param name="identificador"></param>
        /// <param name="tipoSoporte"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult CargarTerminosCondiciones()
        {
            if (!string.IsNullOrEmpty(rutaArchivoTerminosCondiciones))
                return Json(new { status = 200, url = rutaArchivoTerminosCondiciones });
            else
                return Json(new { status = 400, url = string.Empty });
        }

        public ActionResult TerminosCodiciones()
        {
            return File(rutaArchivoTerminosCondiciones, "application/pdf", Server.UrlEncode(rutaArchivoTerminosCondiciones));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult CrearUsuarioArlPositiva()
        {
            var lnUsuario = new LNUsuario();
            var lnDepto = new LNDepartamento();
            var usuarioArl = new UsuarioArlPositivaModel();
            usuarioArl.TiposDocumento = lnUsuario.ObtenerTiposDocumento().Select(td => new SelectListItem()
            {
                Value = td.Sigla,
                Text = td.Descripcion
            }).ToList();
            usuarioArl.RolesSistema = lnUsuario.ObtenerRolesSistema().Select(rs => new SelectListItem()
            {
                Value = rs.IdRolSistema.ToString(),
                Text = rs.NombreRol
            }).ToList();
            usuarioArl.Departamentos = lnDepto.ObtenerListadoDepartamento().Select(dto => new SelectListItem()
            {
                Value = dto.IdDepartamento.ToString(),
                Text = dto.Nombre
            }).ToList();
            usuarioArl.Municipios = new List<SelectListItem>();
            usuarioArl.Estados = new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Value = Enumeraciones.EnumAdministracionUsuarios.EstadosUsuariosSistema.Activo.ToString(),
                    Text = Enumeraciones.EnumAdministracionUsuarios.NombresEstadosUsuariosSistema.Activo
                },
                new SelectListItem()
                {
                    Value = Enumeraciones.EnumAdministracionUsuarios.EstadosUsuariosSistema.Inactivo.ToString(),
                    Text = Enumeraciones.EnumAdministracionUsuarios.NombresEstadosUsuariosSistema.Inactivo
                },
                new SelectListItem()
                {
                    Value = Enumeraciones.EnumAdministracionUsuarios.EstadosUsuariosSistema.Bloqueado.ToString(),
                    Text = Enumeraciones.EnumAdministracionUsuarios.NombresEstadosUsuariosSistema.Bloqueado
                }
            };
            return View(usuarioArl);
        }

        [HttpPost]
        public ActionResult CrearUsuarioArlPositiva(UsuarioArlPositivaModel usuarioArl)
        {
            var lnUsuario = new LNUsuario();
            var lnDepto = new LNDepartamento();
            if (ModelState.IsValid)
            {
                var usuarioRegistrar = new EDUsuarioPorAprobar()
                {
                    NumDocumentoEmpresa = usuarioArl.DocumentoEmpresa,
                    TipoDocumentoEmpresa = usuarioArl.TipoDocumentoEmpresa,
                    RazonSocialEmpresa = usuarioArl.RazonSocialEmpresa,
                    DepartamentoSedePpalEmpresa = usuarioArl.DeptoSedePpalEmpresa,
                    MunicipioSedePpalEmpresa = usuarioArl.MunicipioSedePpalEmpresa,
                    TipoDocumentoAfi = usuarioArl.TipoDocumento,
                    NumDocumentoAfi = usuarioArl.Documento,
                    Nombres = usuarioArl.Nombres,
                    Apellidos = usuarioArl.Apellidos,
                    Telefono = usuarioArl.Telefono,
                    Correo = usuarioArl.EmailPersona,
                    RolUsuario = usuarioArl.IdRolSeleccionado,
                };
                ViewBag.Mensaje = lnUsuario.RegistrarUsusariosParaAprobar(usuarioRegistrar);
                return View(usuarioArl);
            }
            else
            {
                usuarioArl.TiposDocumento = lnUsuario.ObtenerTiposDocumento().Select(td => new SelectListItem()
                {
                    Value = td.Sigla,
                    Text = td.Descripcion
                }).ToList();
                usuarioArl.RolesSistema = lnUsuario.ObtenerRolesSistema().Select(rs => new SelectListItem()
                {
                    Value = rs.IdRolSistema.ToString(),
                    Text = rs.NombreRol
                }).ToList();
                usuarioArl.Departamentos = lnDepto.ObtenerListadoDepartamento().Select(dto => new SelectListItem()
                {
                    Value = dto.IdDepartamento.ToString(),
                    Text = dto.Nombre
                }).ToList();
                usuarioArl.Municipios = new List<SelectListItem>();
                usuarioArl.Estados = new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Value = Enumeraciones.EnumAdministracionUsuarios.EstadosUsuariosSistema.Activo.ToString(),
                    Text = Enumeraciones.EnumAdministracionUsuarios.NombresEstadosUsuariosSistema.Activo
                },
                new SelectListItem()
                {
                    Value = Enumeraciones.EnumAdministracionUsuarios.EstadosUsuariosSistema.Inactivo.ToString(),
                    Text = Enumeraciones.EnumAdministracionUsuarios.NombresEstadosUsuariosSistema.Inactivo
                },
                new SelectListItem()
                {
                    Value = Enumeraciones.EnumAdministracionUsuarios.EstadosUsuariosSistema.Bloqueado.ToString(),
                    Text = Enumeraciones.EnumAdministracionUsuarios.NombresEstadosUsuariosSistema.Bloqueado
                }
            };
                return View(usuarioArl);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idDepto"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ConsultarMunicipiosPorDepto(int idDepto)
        {
            var lnMpio = new LNMunicipio();
            var municipios = lnMpio.ObtenerListadoMunicipio(idDepto).Select(m => new SelectListItem()
            {
                Value = m.IdMunicipio.ToString(),
                Text = m.Nombre
            }).ToList();
            if (municipios.Count > 0)
                return Json(new { Data = municipios, Mensaje = "OK" });
            else
                return Json(new { Data = "No fue posible realizar obtener los municipios", Mensaje = "Fail" });
        }

        /// <summary>
        /// Registra un nuevo usuario del sistema
        /// </summary>
        /// <param name="adminSistema"></param>
        /// <returns></returns>
        private string CrearUsuarioSistema(AdministrarUsuariosModel adminSistema)
        {
            var result = string.Empty;
            var lnUsuario = new LNUsuario();
            var usuarioRegistrar = new EDUsuarioPorAprobar()
            {
                NumDocumentoEmpresa = adminSistema.DocumentoEmpresa,
                TipoDocumentoEmpresa = adminSistema.TipoDocumentoEmpresa,
                RazonSocialEmpresa = adminSistema.RazonSocialEmpresa,
                MunicipioSedePpalEmpresa = adminSistema.MunicipioSedePpalEmpresa,
                TipoDocumentoAfi = adminSistema.TipoDocumento,
                NumDocumentoAfi = adminSistema.Documento,
                Nombres = adminSistema.Nombres,
                Apellidos = adminSistema.Apellidos,
                Correo = adminSistema.EmailPersona,
                RolUsuario = adminSistema.IdRolSeleccionado,
                PreguntasSeguridadSeleccionadas = new List<PreguntasSeguridadSeleccionadas>()
                {
                    new PreguntasSeguridadSeleccionadas()
                    {
                        CodPreguntaSeguridad = adminSistema.ConfiguracionPreguntasSeguridad.CodPreguntaUno,
                        RespuestaSeguridad = adminSistema.ConfiguracionPreguntasSeguridad.RespuestaUno
                    },
                    new PreguntasSeguridadSeleccionadas()
                    {
                        CodPreguntaSeguridad = adminSistema.ConfiguracionPreguntasSeguridad.CodPreguntaDos,
                        RespuestaSeguridad = adminSistema.ConfiguracionPreguntasSeguridad.RespuestaDos
                    },
                    new PreguntasSeguridadSeleccionadas()
                    {
                        CodPreguntaSeguridad = adminSistema.ConfiguracionPreguntasSeguridad.CodPreguntaTres,
                        RespuestaSeguridad = adminSistema.ConfiguracionPreguntasSeguridad.RespuestaTres
                    }
                }
            };
            result = lnUsuario.RegistrarUsusariosParaAprobar(usuarioRegistrar);
            return result;
        }
    }
}