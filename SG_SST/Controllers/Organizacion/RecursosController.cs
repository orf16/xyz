using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SG_SST.Models;
using SG_SST.Models.Organizacion;
using SG_SST.Services.Empresas.IServices;
using SG_SST.Services.Empresas.Services;
using SG_SST.Services.Organizacion.Iservices;
using SG_SST.Services.Organizacion.Services;
using System.Configuration;
using SG_SST.Services.General.IServices;
using SG_SST.Services.General.Services;
using System.Data.Entity;
using SG_SST.Dtos.Organizacion;
using System.IO;
using iTextSharp.text;
using ClosedXML.Excel;
using System.Data;
using SG_SST.Controllers.Base;



namespace SG_SST.Controllers.Organizacion
{


    public class RecursosController : BaseController
    {
        private SG_SSTContext db = new SG_SSTContext();
        private ISedeServicios sedeRepositorio = new SedeServicios();
        private IFaseServices faserepositorio = new FaseServices();
        private ITiporecursoServices tiporecursorepositorio = new TipoRecursoServices();
        private IRecursosSedesServices recursoporsede = new RecursoSedesServices();
        private ISedeServicios nombresede = new SedeServicios();
        private IRecursosServicios recursosServicios = new RecursosServicios();
        private int anioIncial = Int32.Parse(ConfigurationManager.AppSettings["anioInicial"]);
        private int anioFinal = Int32.Parse(ConfigurationManager.AppSettings["anioFinal"]);


        // GET: Recursos
        public ActionResult Index()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            var sedes = db.Tbl_Sede.Where(s => s.Pk_Id_Sede != null).ToList();

            if (sedes.Count == 0)
            {
                ViewBag.mensaje1 = "ALERTA: Para continuar con este modulo primero se deben Registrar las Sedes de la Empresa.";
                return View("Index", sedes);
            }

            var recurso = db.Tbl_Recurso.Where(r => r.Pk_Id_Recurso != null).ToList();

            if (recurso.Count == 0)
            {
                ViewBag.mensaje1 = "Estimado Usuario: aun no hay Recursos Asignados.";
                return View("Index", recurso);
            }
            //ViewBag.Periodo = recursosServicios.ObtenerPeriodosAnios(anioIncial, anioFinal);
            return View("Index");
        }


        public ActionResult CrearRecurso()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            ViewBag.Periodo = recursosServicios.ObtenerPeriodosAnios(anioIncial, anioFinal);
            ViewBag.Fk_Id_Fase = new SelectList(faserepositorio.ObtenerFase(), "Pk_Id_Fase", "Descripcion_Fase");
            ViewBag.Fk_Id_Sede = new SelectList(sedeRepositorio.SedesPorEmpresa(usuarioActual.IdEmpresa), "Pk_Id_Sede", "Nombre_Sede");
            ViewBag.Fk_Id_TipoRecurso = new SelectList(tiporecursorepositorio.ObtenerTipoRecurso(), "Pk_Id_TipoRecurso", "Descripcion_Tipo_Recurso");
            var ms = TempData["shortMessage"];
            if (ms != null)
            {
                ViewBag.mensaje = ms;
            }
            return View();
        }

  

        /// <summary>
        /// Metodo para Grabar un recurso que se le asigno a una sede.
        /// </summary>
        /// <param name="recurso"></param>
        /// <param name="recursosede"></param>
        /// <param name="recursofase"></param>
        /// <param name="recursotiporecurso"></param>
        /// <returns></returns>
        public ActionResult GrabarRecurso
            (
            Recurso recurso,
            RecursoporSede recursosede,
            RecursoFase recursofase,
            RecursoTipoRecurso recursotiporecurso)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    recurso.Nombre_Recurso = recurso.Nombre_Recurso.ToUpper();
                    List<RecursoporSede> recsede = (from rs in db.Tbl_RecursoporSede
                                                    join sede in db.Tbl_Sede on rs.Fk_Id_Sede equals sede.Pk_Id_Sede
                                                   where sede.Fk_Id_Empresa==usuarioActual.IdEmpresa
                                                    select rs).ToList();
                                                     
                                                         
                    //List<RecursoporSede> recsede = db.Tbl_RecursoporSede.Where(rs => rs.Pk_Id_RecursoporSede > 0).ToList();

                    foreach (var rs in recsede)
                    {

                        if (rs.Recurso.Nombre_Recurso == recurso.Nombre_Recurso
                            && rs.Recurso.Periodo == recurso.Periodo
                              && rs.Fk_Id_Sede == recursosede.Fk_Id_Sede
                              )
                        {
                            TempData["shortMessage"] = "El Recurso ingresado ya se encuentra en la Sede y el Periodo Seleccionado.";

                            return RedirectToAction("CrearRecurso");
                        }
                    }
                    recurso.RecursosporSede = new List<RecursoporSede>();
                    recurso.RecursosporSede.Add(recursosede);

                    recurso.RecursosFase = new List<RecursoFase>();
                    recurso.RecursosFase.Add(recursofase);

                    recurso.RecursosTipoRecursos = new List<RecursoTipoRecurso>();
                    recurso.RecursosTipoRecursos.Add(recursotiporecurso);


                    recurso.Nombre_Recurso = recurso.Nombre_Recurso.ToUpper();
                    db.Tbl_Recurso.Add(recurso);

                    db.SaveChanges();
                    transaction.Commit();

                    List<RecursoporSede> recsedes = db.Tbl_RecursoporSede.Where(rs => rs.Pk_Id_RecursoporSede > 0).ToList();
                    TempData["shortMessage"] = "Recurso Asignado Satisfactoriamente.";

                    return RedirectToAction("CrearRecurso", recsedes);

                }
                catch (Exception e)
                {
                    TempData["shortMessage"] = "la Asignacion del Recurso no se pudo realizar.";
                    transaction.Rollback();
                    return RedirectToAction("CrearRecurso");
                }


            }
            return RedirectToAction("CrearRecurso");
        }

        //Metodo que envia a la vista la lista de las Sedes Registradas.
        public ActionResult ListadoRecursos()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            ViewBag.Periodo = recursosServicios.ObtenerPeriodosAnios(anioIncial, anioFinal);
            ViewBag.Pk_Id_Sede = new SelectList(sedeRepositorio.SedesPorEmpresa(usuarioActual.IdEmpresa), "Pk_Id_Sede", "Nombre_Sede");
            var ms = TempData["shortMessage"];
            if (ms != null)
            {
                ViewBag.mensaje = ms;
            }
            return View("ListadoRecursos");
        }





        public ActionResult BuscarRecursosPorSede(int Pk_Id_Sede, int Periodo)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }

            var data = recursoporsede.ObtenerRecursoSede(Pk_Id_Sede, Periodo);
           
            ViewBag.nulo = false;
            if (data.Count == 0)
            {

                ViewBag.mensaje = "La Sede aun no tiene recursos registrados en el periodo " + Periodo;
                ViewBag.nulo = true;
            }

           

            return PartialView("RecursosPorSedeVP", data);
        }


        public ActionResult EditarRecursos(int Pk_Id_RecursoporSede, int Pk_Id_RecursoFase, int Pk_Id_RecursoTipoRecurso, int Pk_Id_Recurso)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            RecursoporSede recursosede = db.Tbl_RecursoporSede.Find(Pk_Id_RecursoporSede);

            if (recursosede == null)
            {
                return HttpNotFound();
            }

            RecursoFase recursofase = db.Tbl_RecursoFase.Find(Pk_Id_RecursoFase);

            if (recursofase == null)
            {
                return HttpNotFound();
            }

            RecursoTipoRecurso recursotipo = db.Tbl_RecursoTipoRecurso.Find(Pk_Id_RecursoTipoRecurso);

            if (recursotipo == null)
            {
                return HttpNotFound();
            }

            ViewBag.Fk_Id_Sede = new SelectList(sedeRepositorio.SedesPorEmpresa(usuarioActual.IdEmpresa), "Pk_Id_Sede", "Nombre_Sede", recursosede.Fk_Id_Sede);

            ViewBag.Fk_Id_Fase = new SelectList(faserepositorio.ObtenerFase(), "Pk_Id_Fase", "Descripcion_Fase", recursofase.Fk_Id_Fase);

            ViewBag.Fk_Id_TipoRecurso = new SelectList(tiporecursorepositorio.ObtenerTipoRecurso(), "Pk_Id_TipoRecurso", "Descripcion_Tipo_Recurso", recursotipo.Fk_Id_TipoRecurso);

            var recurso = db.Tbl_Recurso.Find(Pk_Id_Recurso);

            List<SelectListItem> Periodos = recursosServicios.ObtenerPeriodosAnios(anioIncial, anioFinal);
            ViewBag.Periodo = new SelectList(Periodos, "Value", "Text", recurso.Periodo);
            return View("EditarRecursos", recursosede);


        }

        [HttpPost]
        public ActionResult GrabarRecursosEditados(
            Recurso recurso,
            RecursoporSede recursoporsede,
            RecursoFase recursofase,
            RecursoTipoRecurso recursotiporecurso)
        {

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    recurso.RecursosporSede = new List<RecursoporSede>();
                    recurso.RecursosporSede.Add(recursoporsede);
                    recurso.RecursosFase = new List<RecursoFase>();
                    recurso.RecursosFase.Add(recursofase);

                    recurso.RecursosTipoRecursos = new List<RecursoTipoRecurso>();
                    recurso.RecursosTipoRecursos.Add(recursotiporecurso);
                    recurso.Nombre_Recurso = recurso.Nombre_Recurso.ToUpper();
                    db.Entry(recurso).State = EntityState.Modified;
                    db.Entry(recursofase).State = EntityState.Modified;
                    db.Entry(recursotiporecurso).State = EntityState.Modified;
                    db.SaveChanges();
                    transaction.Commit();
                    TempData["shortMessage"] = "Recurso Modificado Satisfactoriamente.";
                }
                catch (Exception e)
                {
                    TempData["shortMessage"] = "No se pudo realizar la Modificacion.";
                    transaction.Rollback();
                    return RedirectToAction("ListadoRecursos");

                }
            }


            return RedirectToAction("ListadoRecursos");


        }

        public ActionResult EliminarRecursos(int Pk_Id_Recurso)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {

                    Recurso recursos = db.Tbl_Recurso.Find(Pk_Id_Recurso);
                    db.Tbl_Recurso.Remove(recursos);
                    db.SaveChanges();
                    transaction.Commit();
                    TempData["shortMessage"] = "El Recurso se ha Eliminado.";
                }
                catch (Exception e)
                {
                    TempData["shortMessage"] = "No se pudo realizar la Eliminacion.";
                    transaction.Rollback();
                    return RedirectToAction("ListadoRecursos");
                }

                return RedirectToAction("ListadoRecursos");
            }
        }

       
        public FileResult ExportarExcel(int Pk_Id_Sede, int Periodo)
        {
           
            SG_SSTContext entities = new SG_SSTContext();
            DataTable dt = new DataTable(string.Concat(nombresede.ObtenerSedePorMunicipio(Pk_Id_Sede).Sede.Nombre_Sede));
            dt.Columns.AddRange(new DataColumn[]{ 
                                            new DataColumn("SEDE"),
                                            new DataColumn("NOMBRE RECURSO"), 
                                            new DataColumn("PERIODO ASIGNADO"), 
                                            new DataColumn("FASE RECURSO"),
                                            new DataColumn("TIPO RECURSO")
                                          });
           var recursos = recursoporsede.ObtenerRecursoSede(Pk_Id_Sede, Periodo);
           foreach (var recurso in recursos)
            {
                dt.Rows.Add(
                    recurso.Sede.Nombre_Sede.ToUpper(),
                    recurso.Recurso.Nombre_Recurso.ToUpper(), 
                    recurso.Recurso.Periodo,
                    recurso.Recurso.RecursosFase.FirstOrDefault().Fase.Descripcion_Fase.ToUpper(),
                    recurso.Recurso.RecursosTipoRecursos.FirstOrDefault().TipoRecurso.Descripcion_Tipo_Recurso.ToUpper());
            }
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    //return File(stream.ToArray(), 
                    //    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", 
                    //    "RecursosAsignados.xlsx");
                    return File(stream.ToArray(), "application/vnd.ms-excel", "RecursosAsignados.xlsx");
                }
            }
            

        }
  
       
        public ActionResult Recursos_PDF(int Pk_Id_Sede, int Periodo)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            var data = recursoporsede.ObtenerRecursoSede(Pk_Id_Sede, Periodo);
            var total = data;
            string SwitchNombreEmpresa = usuarioActual.RazonSocialEmpresa;
            string SwitchNitEmpresa = usuarioActual.NitEmpresa;
            string SwitchNombreDocumento = "Recursos Asignados Sede";
            //  var fullFooter = Url.Action("Footer", "Recursos", null, Request.Url.Scheme);
            //  var fullHeader = Url.Action("Header", "Recursos", new { NombreEmpresa = SwitchNombreEmpresa, NitEmpresa = SwitchNitEmpresa, NombreInforme = SwitchNombreDocumento }, Request.Url.Scheme);
            var uriFooter = new Uri(Url.Action("Footer", "Recursos", null, Request.Url.Scheme));
            var clean1 = uriFooter.GetComponents(UriComponents.AbsoluteUri & ~UriComponents.Port, UriFormat.UriEscaped);
            var uriHeader = new Uri(Url.Action("Header", "Recursos", new { NombreEmpresa = SwitchNombreEmpresa, NitEmpresa = SwitchNitEmpresa, NombreInforme = SwitchNombreDocumento }, Request.Url.Scheme));
            var clean2 = uriHeader.GetComponents(UriComponents.AbsoluteUri & ~UriComponents.Port, UriFormat.UriEscaped);
            string cusomtSwitches = string.Format("--footer-line --print-media-type --allow {0} --footer-html {0} --header-line --print-media-type --allow {1} --header-html {1} --header-spacing 5", clean1, clean2);
            //string cusomtSwitches = string.Format("--footer-line --print-media-type --allow {0} --footer-html {0} --header-line --print-media-type --allow {1} --header-html {1} --header-spacing 5", fullFooter, fullHeader);
            return new Rotativa.PartialViewAsPdf("RecursosPorSedePDF", data) { FileName = "RecursosAsignados.pdf", CustomSwitches = cusomtSwitches };

            //var data = recursoporsede.ObtenerRecursoSede(Pk_Id_Sede, Periodo);
            //var total = data;
            //string SwitchNombreEmpresa = usuarioActual.RazonSocialEmpresa;
            //string SwitchNitEmpresa = usuarioActual.NitEmpresa;
            //string SwitchNombreDocumento = "RECURSOS SEDE: " + string.Concat(data.FirstOrDefault().Sede.Nombre_Sede);
            //string cusomtSwitches = string.Format("--footer-line --print-media-type --allow {0} --footer-html {0} --header-line --print-media-type --allow {1} --header-html {1} --header-spacing 5",
            //Url.Action("Footer", "Recursos", null, Request.Url.Scheme), Url.Action("Header", "Recursos", new { NombreEmpresa = SwitchNombreEmpresa, NitEmpresa = SwitchNitEmpresa, NombreInforme = SwitchNombreDocumento }, Request.Url.Scheme));

            //ViewBag.total = total.Count();
            //ViewBag.periodo = Periodo;
           
            //return new Rotativa.PartialViewAsPdf("RecursosPorSedePDF", data) { FileName = "RecursosAsignados.pdf", CustomSwitches = cusomtSwitches };

        }

        [AllowAnonymous]
        public ActionResult Header(string NombreEmpresa, string NitEmpresa, string NombreInforme)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            ViewBag.NombreEmpresa = NombreEmpresa;
            ViewBag.NitEmpresa = NitEmpresa;
            ViewBag.NombreInforme = NombreInforme;
            return View();
        }
        [AllowAnonymous]
        public ActionResult Footer()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.mensaje1 = "Debe Registrarse para Ingresar a este Modulo.";
                return RedirectToAction("Login", "Home");
            }
            return View();
        }







    }
}