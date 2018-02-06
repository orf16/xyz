using SG_SST.Controllers.Base;
using SG_SST.EntidadesDominio.EnfermedadLaboral;
using SG_SST.Helpers;
using SG_SST.Logica.Ausentismo;
using SG_SST.Logica.EnfermedadLaboral;
using SG_SST.Models.EnfermedadLaboral;
using SG_SST.ServiceRequest;
using SG_SST.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SG_SST.Controllers.Empresas
{
    public class EnfermedadLaboralDiagnosticadaController : BaseController
    {
        // GET: EnfermedadLaboralDiagnosticada
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Registrar()
        {
            EnfermedadLaboralModel enfermedadLaboral = null;
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }
            var tipoDocEmp = usuarioActual.SiglaTipoDocumentoEmpresa;
            var tipoDocAfil = usuarioActual.SiglaTipoDocumentoEmpleado;
            var documentoEmp = usuarioActual.NitEmpresa;
            var documentoAfil = usuarioActual.Documento;
            var resultadoEmp = 0;
            var resultadoAfil = 0;
            var datosTrabajador = ConsultaSIARP.ConsultarAfiliadoEmpresaActivos(tipoDocEmp, documentoEmp, tipoDocAfil, documentoAfil, out resultadoEmp, out resultadoAfil);
            if (datosTrabajador != null
                && resultadoEmp == (int)Enumeraciones.EnumAdministracionUsuarios.ValidaEstadoEmpAfi.ExisteActivoEmp
                && resultadoAfil == (int)Enumeraciones.EnumAdministracionUsuarios.ValidaEstadoEmpAfi.ExisteActivoAfi)
            {
                enfermedadLaboral = new EnfermedadLaboralModel();
                enfermedadLaboral.UsuarioQuienRegistraELD = usuarioActual.IdUsuario;
                enfermedadLaboral.NumeroDocumento = datosTrabajador.documentoEmp;
                enfermedadLaboral.NombreTrabajador = string.Format("{0} {1} {2} {3}", datosTrabajador.nombre1, datosTrabajador.nombre2, datosTrabajador.apellido1, datosTrabajador.apellido2);
                enfermedadLaboral.FechaNacimiento = datosTrabajador.fechaNacimiento;
                enfermedadLaboral.Geneero = datosTrabajador.sexoPersona;
                enfermedadLaboral.Direccion = datosTrabajador.dirPersona;
                enfermedadLaboral.Telefono = datosTrabajador.telPersona;
                enfermedadLaboral.Fax = datosTrabajador.faxEmpresa;
                enfermedadLaboral.Departamento = datosTrabajador.nomDepAfiliado;
                enfermedadLaboral.Municipio = datosTrabajador.nomMunAfiliado;
                enfermedadLaboral.Cargo = datosTrabajador.cargo;
                enfermedadLaboral.InstanciaARegistrar = new Models.EnfermedadLaboral.InstanciaRegistrada()
                {
                    EstadosInstancia = new List<SelectListItem>
                    {
                        new SelectListItem() { Value = "", Text = "Seleccione una opción" },
                        new SelectListItem() { Value = "1", Text = "En Estudio" },
                        new SelectListItem() { Value = "2", Text = "Laboral" },
                        new SelectListItem() { Value = "3", Text = "Común" }
                    }
                };
            }
            return View(enfermedadLaboral);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="enfermedadRegistrar"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Registrar(EnfermedadLaboralModel enfermedadRegistrar)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
                return RedirectToAction("Login", "Home");
            if (!ModelState.IsValid)
            {
                enfermedadRegistrar.InstanciaARegistrar.EstadosInstancia = new List<SelectListItem>
                    {
                        new SelectListItem() { Value = "", Text = "Seleccione una opción" },
                        new SelectListItem() { Value = "1", Text = "En Estudio" },
                        new SelectListItem() { Value = "2", Text = "Laboral" },
                        new SelectListItem() { Value = "3", Text = "Común" }
                    };
                enfermedadRegistrar.InstanciasRegistradas = Session["InstanciasRegistradas"] == null ? null : Session["InstanciasRegistradas"] as List<Models.EnfermedadLaboral.InstanciaRegistrada>;
                return View(enfermedadRegistrar);
            }
            else
            {
                var registrar = true;
                var enfermedadARegistrar = new EDEnfermedadLaboral();
                //se guardan archivos en el servidor y se obtienen las rutas
                enfermedadARegistrar.DocumentoFurel = ManejoArchivos.GuardarArchivos(enfermedadRegistrar.Furel, rutaFisicaDocumentosEnfLabFurel, usuarioActual.IdUsuario, usuarioImp, passwordImp, dominio);
                enfermedadARegistrar.CartaEPS = ManejoArchivos.GuardarArchivos(enfermedadRegistrar.CartaEPS, rutaFisicaDocumentosEnfLabCartaEPS, usuarioActual.IdUsuario, usuarioImp, passwordImp, dominio);
                if (Session["DocumentosAdjuntos"] != null)
                {
                    var archivosAdjuntos = Session["DocumentosAdjuntos"] as List<HttpPostedFileBase>;
                    enfermedadARegistrar.TiposDocumentosEnviadosEPS = new List<string>();
                    foreach (var archivo in archivosAdjuntos)
                    {
                        var rutaArchivo = string.Format(@"{0}\{1}\{2}", rutaFisicaDocumentosEnfLabTiposDoc, usuarioActual.IdUsuario, archivo.FileName);
                        enfermedadARegistrar.TiposDocumentosEnviadosEPS.Add(rutaArchivo);
                    }
                    Session["DocumentosAdjuntos"] = null;
                }
                else
                    registrar = false;
                if (Session["InstanciasRegistradas"] != null)
                {
                    var instancias = Session["InstanciasRegistradas"] as List<Models.EnfermedadLaboral.InstanciaRegistrada>;
                    enfermedadARegistrar.InstanciasRegistradas = instancias.Select(i => new EntidadesDominio.EnfermedadLaboral.InstanciaRegistrada()
                    {
                        IdInstancia = i.IdInstancia,
                        Nombre = i.Nombre,
                        EstadoInstancia = i.EstadoInstancia,
                        FechaCalificacion = i.FechaCalificacion,
                        QuienCalifica = i.QuienCalifica
                    }).ToList();
                    Session["InstanciasRegistradas"] = null;
                }
                else
                    registrar = false;
                enfermedadARegistrar.Diagnostico = enfermedadRegistrar.Diagnostico;
                enfermedadARegistrar.DiagnosticoCIIE10 = enfermedadRegistrar.DiagnosticoCIIE10;
                enfermedadARegistrar.FechaDocumentosCalificarEPS = enfermedadRegistrar.FechaDocumentosEPS;
                //ServiceClient.EliminarParametros();
                //var resultado = ServiceClient.RealizarPeticionesPostJsonRestFul<EDEnfermedadLaboral>(urlServicioEnfermedadLaboral, capacidadEnfermedadLaboral, enfermedadARegistrar);
                if (registrar)
                {
                    var lnEnfermedadLaboral = new LNEnfermedadLaboral();
                    enfermedadARegistrar = lnEnfermedadLaboral.RegistrarEnfermedadLaboralDiagnosticada(enfermedadARegistrar);
                }
                else
                    enfermedadARegistrar = null;
                if (enfermedadARegistrar != null)
                    ViewBag.Mensaje = "Información guardada correctamente!!![OK]";
                else
                    ViewBag.Mensaje = "No fue posible registrar la Información. Intente más tarde.";
                enfermedadRegistrar.InstanciaARegistrar.EstadosInstancia = new List<SelectListItem>
                    {
                        new SelectListItem() { Value = "", Text = "Seleccione una opción" },
                        new SelectListItem() { Value = "1", Text = "En Estudio" },
                        new SelectListItem() { Value = "2", Text = "Laboral" },
                        new SelectListItem() { Value = "3", Text = "Común" }
                    };
                enfermedadRegistrar.InstanciaARegistrar.FechaCalificacion = new DateTime();
                return View(enfermedadRegistrar);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefijo"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AutoCompletarDiagnostico(string prefijo)
        {
            LNDiagnostico lndiagnostico = new LNDiagnostico();
            return Json(lndiagnostico.AutoCompletarDiagnostico(prefijo), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefijo"></param>
        /// <returns></returns>
        public ActionResult AgregarNuevaInstancia(Models.EnfermedadLaboral.InstanciaRegistrada nuevaInstancia)
        {
            if(nuevaInstancia != null)
            {
                if (Session["InstanciasRegistradas"] != null) {
                    var instanciasRegistradas = Session["InstanciasRegistradas"] as List<Models.EnfermedadLaboral.InstanciaRegistrada>;
                    var existeInstancia = instanciasRegistradas.Where(a => a.IdInstancia == nuevaInstancia.IdInstancia)
                                            .Select(a => a).FirstOrDefault();
                    if (existeInstancia == null)
                    {
                        instanciasRegistradas.Add(nuevaInstancia);
                        Session["InstanciasRegistradas"] = instanciasRegistradas;
                    }
                }
                else
                {
                    var instanciasRegistradas = new List<Models.EnfermedadLaboral.InstanciaRegistrada>() { nuevaInstancia };
                    Session["InstanciasRegistradas"] = instanciasRegistradas;
                }
            }
            var resultado = RenderRazorViewToString("_NuevaInstanciaEnfermedadLaboral", nuevaInstancia);
            return Json(new { Datos = resultado, Estado = "OK" });
        }

        /// <summary>
        /// Guarda temporalmente los archivos adjuntos
        /// </summary>
        /// <param name="archivoAdjunto"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GuardarArchivoAdjunto(HttpPostedFileBase TipoDocumentoCalificacion)
        {
            if (TipoDocumentoCalificacion != null)
            {
                if (Session["DocumentosAdjuntos"] != null)
                {
                    var archivosAdjuntos = Session["DocumentosAdjuntos"] as List<HttpPostedFileBase>;
                    var existeArchivo = archivosAdjuntos.Where(a => a.ContentLength == TipoDocumentoCalificacion.ContentLength
                                            && a.FileName.Equals(TipoDocumentoCalificacion.FileName))
                                            .Select(a => a).FirstOrDefault();
                    if(existeArchivo == null)
                    {
                        var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
                        if (usuarioActual != null)
                        {
                            var result = ManejoArchivos.GuardarArchivos(TipoDocumentoCalificacion, rutaFisicaDocumentosEnfLabTiposDoc, usuarioActual.IdUsuario, usuarioImp, passwordImp, dominio);
                            if (!string.IsNullOrEmpty(result)) {
                                archivosAdjuntos.Add(TipoDocumentoCalificacion);
                                Session["DocumentosAdjuntos"] = archivosAdjuntos;
                            }
                        }
                    }
                }
                else {
                    var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
                    if (usuarioActual != null)
                    {
                        var result = ManejoArchivos.GuardarArchivos(TipoDocumentoCalificacion, rutaFisicaDocumentosEnfLabTiposDoc, usuarioActual.IdUsuario, usuarioImp, passwordImp, dominio);
                        if (!string.IsNullOrEmpty(result))
                        {
                            var archivosAdjuntos = new List<HttpPostedFileBase>() { TipoDocumentoCalificacion };
                            Session["DocumentosAdjuntos"] = archivosAdjuntos;
                        }
                    }
                }
                return Json(new { Nombre = TipoDocumentoCalificacion.FileName, Respuesta = "OK" });
            }else
                return Json(new { Nombre = string.Empty, Respuesta = "ERROR" });
        }

        /// <summary>
        /// Devuelve la informacion de una instancia registrada
        /// </summary>
        /// <param name="codInstancia"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ObtenerDatosInstancia(int codInstancia)
        {
            Models.EnfermedadLaboral.InstanciaRegistrada instancia = null;
            if (Session["InstanciasRegistradas"] != null) {
                var instanciasRegistradas = Session["InstanciasRegistradas"] as List<Models.EnfermedadLaboral.InstanciaRegistrada>;
                instancia = instanciasRegistradas.Where(inst => inst.IdInstancia == codInstancia).Select(inst => inst).FirstOrDefault();
            }
            if (instancia != null)
            {
                instancia.EstadosInstancia = new List<SelectListItem>
                    {
                        new SelectListItem() { Value = "", Text = "Seleccione una opción" },
                        new SelectListItem() { Value = "1", Text = "En Estudio" },
                        new SelectListItem() { Value = "2", Text = "Laboral" },
                        new SelectListItem() { Value = "3", Text = "Común" }
                    };
                var resultado = RenderRazorViewToString("_ModificarInstanciEnfermedadLaboral", instancia);
                return Json(new { Datos = resultado, Estado = "OK" });
            }
            else {
                return Json(new { Datos = string.Empty, Estado = "NOTFOUND" });
            }
        }
        /// <summary>
        /// Modifica la informacion de una instancia
        /// </summary>
        /// <param name="codInstancia"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ModificarInstancia(Models.EnfermedadLaboral.InstanciaRegistrada nuevaInstancia)
        {
            var result = false;
            if (nuevaInstancia != null)
            {
                if (Session["InstanciasRegistradas"] != null)
                {
                    var instanciasRegistradas = Session["InstanciasRegistradas"] as List<Models.EnfermedadLaboral.InstanciaRegistrada>;
                    var instancia = instanciasRegistradas.Where(inst => inst.IdInstancia == nuevaInstancia.IdInstancia).Select(inst => inst).SingleOrDefault();
                    if (instancia != null)
                    {
                        instanciasRegistradas.Remove(instancia);
                        instanciasRegistradas.Add(nuevaInstancia);
                        Session["InstanciasRegistradas"] = instanciasRegistradas;
                        result = true;
                    }
                }
            }
            if (result)
            {
                return Json(new { NombreInstancia = nuevaInstancia.Nombre, EstadoInstancia = nuevaInstancia.NombreEstadoInstancia, Estado = "OK" });
            }
            else
                return Json(new { NombreInstancia = string.Empty, EstadoInstancia = string.Empty, Estado = "ERROR" });
        }
    }
}