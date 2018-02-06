
namespace SG_SST.Controllers.LiderazgoGerencial
{
    using SG_SST.Models;
    using SG_SST.Models.LiderazgoGerencial;
    using SG_SST.Services.Empresas.IServices;
    using SG_SST.Services.Empresas.Services;
    using SG_SST.Services.General.IServices;
    using SG_SST.Services.General.Services;
    using SG_SST.Services.LiderazgoGerencial.Iservices;
    using SG_SST.Services.LiderazgoGerencial.Services;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Web.Mvc;
    using System.Data.Entity;
    using System.Linq;
    using System.IO;
    using SG_SST.Dtos.LiderazgoGerencial;
    using SG_SST.Controllers.Base;

    public class PresupuestoController : BaseController
    {


        private ISedeServicios sedeServicio = new SedeServicios();
        private IPresupuestoServicios presupuestoServicios = new PresupuestoServicios();
        private IRecursosServicios recursosServicios = new RecursosServicios();
        private int anioIncial = Int32.Parse(ConfigurationManager.AppSettings["anioInicial"]);
        private int anioFinal = Int32.Parse(ConfigurationManager.AppSettings["anioFinal"]);

        private SG_SSTContext db = new SG_SSTContext();
        /// <summary>
        /// Metodo que me retorna el menu donde se crear, edita, elimina.. el presupuesto
        /// </summary>
        /// <returns></returns>
        public ActionResult MenuPresupuesto()
        {
            return View();
        }

        /// <summary>
        /// Metodo que me retorna la vista para generar el presupuesto para cada sede
        /// </summary>
        /// <returns>vista</returns>
        // GET: Presupuesto
        public ActionResult CrearPresupuesto()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
                return View();
            }
            //ViewBag.Periodo = recursosServicios.ObtenerPeriodosAnios(anioIncial, anioFinal);

            ViewBag.Periodo = new SelectList(recursosServicios.ObtenerPeriodosAnios(anioIncial, anioFinal), "Text", "Value", DateTime.Now.Year);
            ViewBag.FK_Sede = new SelectList(sedeServicio.SedesPorEmpresa(usuarioActual.IdEmpresa), "Pk_Id_Sede", "Nombre_Sede");
            return View();
        }

        /// <summary>
        /// Metodo que me realiza el guardado del presupuesto 
        /// </summary>
        /// <param name="presupuesto">presupuesto para guardar</param>
        /// <param name="prespuestoPorAño">lista del presupusto por año</param>
        /// <param name="actividadesPresupuesto">actividades del presupuesto</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CrearPresupuesto(Presupuesto presupuesto, PresupuestoPorAnio prespuestoPorAño, List<ActividadPresupuesto> actividadesPresupuesto)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
                return View();
            }
            presupuesto.presupuestosPorAnio = new List<PresupuestoPorAnio>();
            presupuesto.presupuestosPorAnio.Add(prespuestoPorAño);

            foreach (ActividadPresupuesto ap in actividadesPresupuesto)
            {
                if (ap.actividadesPresupuesto != null)
                {
                    foreach (ActividadPresupuesto aphijas in ap.actividadesPresupuesto)
                    {
                        foreach (PresupuestoPorMes ppm in aphijas.presupuestosPorMes)
                        {
                            ppm.Presupuesto = presupuesto;
                        }
                    }
                }
                else
                {
                    foreach (PresupuestoPorMes ppm in ap.presupuestosPorMes)
                    {
                        ppm.Presupuesto = presupuesto;
                    }
                }
            }

          //  ViewBag.FK_Sede = new SelectList(sedeServicio.SedesPorEmpresa(usuarioActual.IdEmpresa), "Pk_Id_Sede", "Nombre_Sede");
            bool respuestaGuardado = presupuestoServicios.GuardarPresupuesto(actividadesPresupuesto);
            ViewBag.respuestaGuardado = respuestaGuardado;
      //      ViewBag.Periodo = recursosServicios.ObtenerPeriodosAnios(anioIncial, anioFinal);
            ViewBag.Periodo = new SelectList(recursosServicios.ObtenerPeriodosAnios(anioIncial, anioFinal), "Text", "Value", DateTime.Now.Year);
            ViewBag.FK_Sede = new SelectList(sedeServicio.SedesPorEmpresa(usuarioActual.IdEmpresa), "Pk_Id_Sede", "Nombre_Sede");
            return View("BuscarPresupuestos");

        }

        /// <summary>
        /// Metodo que me retorna una vista parcial para crerar el presupuesto para las sedes
        /// </summary>
        /// <returns></returns>
        public ActionResult GenerarPresupuesto()
        {

            return PartialView("GenerarPresupuestoVP");
        }

        /// <summary>
        /// Metodo que me retorna una vista donde se puede realizar la busqueda de los presupuestos por sede
        /// </summary>
        /// <returns></returns>
        public ActionResult BuscarPresupuestos()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
                return View();
            }
            ViewBag.Periodo = new SelectList(recursosServicios.ObtenerPeriodosAnios(anioIncial, anioFinal), "Text", "Value", DateTime.Now.Year);
            ViewBag.FK_Sede = new SelectList(sedeServicio.SedesPorEmpresa(usuarioActual.IdEmpresa), "Pk_Id_Sede", "Nombre_Sede");
            return View();
        }

        /// <summary>
        /// Metodo que me retorna una vista donde se puede realizar la busqueda de los presupuestos por sede
        /// </summary>
        /// <returns></returns>
        public ActionResult BuscarPresupuestoInforme()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
                return View();
            }
            ViewBag.Periodo = new SelectList(recursosServicios.ObtenerPeriodosAnios(anioIncial, anioFinal), "Text", "Value", DateTime.Now.Year);
            ViewBag.FK_Sede = new SelectList(sedeServicio.SedesPorEmpresa(usuarioActual.IdEmpresa), "Pk_Id_Sede", "Nombre_Sede");
            return View();
        }

        /// <summary>
        /// Metodo que retonar una vista parcial donde se muestra los presupuestos por periodo
        /// </summary>
        /// <param name="Pk_Id_Sede">id o clave primaria de la sede</param>
        /// <param name="Periodo">Año en el cual se realizó el presupuesto</param>
        /// <returns></returns>
        public ActionResult BuscarPresupuestoSedePorPeriodo(int Pk_Id_Sede, int Periodo, bool informe)
        {
            if (informe)
            {
                return PartialView("PresupuestoSedePorPeriodoInformeVP", presupuestoServicios.ObtenerPresupuestosSedePorAnio(Pk_Id_Sede, Periodo));
            }
            else
            {
                return PartialView("PresupuestoSedePorPeriodoVP", presupuestoServicios.ObtenerPresupuestosSedePorAnio(Pk_Id_Sede, Periodo));
            }
        }

        /// <summary>
        /// Metodo que retonar una vista parcial donde se muestra los presupuestos por periodo para eliminar
        /// </summary>
        /// <param name="Pk_Id_Sede">id o clave primaria de la sede</param>
        /// <param name="Periodo">Año en el cual se realizó el presupuesto</param>
        /// <returns></returns>
        public ActionResult BuscarPresupuestoSede(int Pk_Id_Sede, int Periodo)
        {
            return PartialView("EliminarPresupuestoVp", presupuestoServicios.ObtenerPresupuestosSedePorAnio(Pk_Id_Sede, Periodo));
        }

        /// <summary>
        /// Metodo que me carga la vista editarPresupuesto con toda la información para editar el presupuesto
        /// </summary>
        /// <param name="PK_PresupuestoPorAnio">pk del presupuesto por año</param>
        /// <returns></returns>
        public ActionResult EditarPresupuesto(int PK_PresupuestoPorAnio)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
                return View();
            }
            PresupuestoPorAnio presupuestoAnio = presupuestoServicios.ObtenerPresupuestoPorAnio(PK_PresupuestoPorAnio);
            List<SelectListItem> Periodos = recursosServicios.ObtenerPeriodosAnios(anioIncial, anioFinal);
            ViewBag.Periodo = new SelectList(Periodos, "Value", "Text", presupuestoAnio.Periodo);
            ViewBag.Presupuesto = presupuestoAnio.Presupuesto;
            ViewBag.PresupuestoPorAnio = presupuestoAnio;
            List<ActividadPresupuesto> actividades = presupuestoServicios.ObtenerActividadesPorPresupuesto(PK_PresupuestoPorAnio);
            ViewBag.FK_Sede = new SelectList(sedeServicio.SedesPorEmpresa(usuarioActual.IdEmpresa), "Pk_Id_Sede", "Nombre_Sede", presupuestoAnio.FK_Sede);
            return View(actividades);
        }

        public ActionResult EliminarPresupuesto(int PK_PresupuestoPorAnio, int PK_Presupuesto)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
                return View();
            }
            PresupuestoPorAnio presupuestoAnio = presupuestoServicios.ObtenerPresupuestoPorAnio(PK_PresupuestoPorAnio);
            List<SelectListItem> Periodos = recursosServicios.ObtenerPeriodosAnios(anioIncial, anioFinal);
            List<ActividadPresupuesto> actividades = presupuestoServicios.ObtenerActividadesPorPresupuesto(PK_PresupuestoPorAnio);
            bool respuestaEliminado = presupuestoServicios.EliminarPresupuesto(actividades, PK_Presupuesto);
            ViewBag.FK_Sede = new SelectList(sedeServicio.SedesPorEmpresa(usuarioActual.IdEmpresa), "Pk_Id_Sede", "Nombre_Sede", presupuestoAnio.FK_Sede);
            ViewBag.Periodo = new SelectList(Periodos, "Value", "Text", presupuestoAnio.Periodo);
            ViewBag.respuestaEliminado = respuestaEliminado;
            return View("BuscarPresupuestos");

        }

        /// <summary>
        /// Metodo que me carga la vista editarPresupuesto con toda la información para editar el presupuesto
        /// </summary>
        /// <param name="PK_PresupuestoPorAnio">pk del presupuesto por año</param>
        /// <returns></returns>
        public ActionResult EjecutarPresupuesto(int PK_PresupuestoPorAnio)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
                return View();
            }
            PresupuestoPorAnio presupuestoAnio = presupuestoServicios.ObtenerPresupuestoPorAnio(PK_PresupuestoPorAnio);
            List<SelectListItem> Periodos = recursosServicios.ObtenerPeriodosAnios(anioIncial, anioFinal);
            ViewBag.Periodo = new SelectList(Periodos, "Value", "Text", presupuestoAnio.Periodo);
            ViewBag.Presupuesto = presupuestoAnio.Presupuesto;
            ViewBag.PresupuestoPorAnio = presupuestoAnio;
            List<ActividadPresupuesto> actividades = presupuestoServicios.ObtenerActividadesPorPresupuesto(PK_PresupuestoPorAnio);
            ViewBag.FK_Sede = new SelectList(sedeServicio.SedesPorEmpresa(usuarioActual.IdEmpresa), "Pk_Id_Sede", "Nombre_Sede", presupuestoAnio.FK_Sede);
            return View(actividades);
        }


        /// <summary>
        /// Metodo que me retorna la vista para generar el infore del presupuesto para cada sede
        /// </summary>
        /// <returns>vista</returns>
        // GET: Presupuesto
        public ActionResult CrearInformePresupuesto(int PK_PresupuestoPorAnio)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
                return View();
            }
            ViewBag.Periodo = new SelectList(recursosServicios.ObtenerPeriodosAnios(anioIncial, anioFinal), "Text", "Value", DateTime.Now.Year);
            ViewBag.FK_Sede = new SelectList(sedeServicio.SedesPorEmpresa(usuarioActual.IdEmpresa), "Pk_Id_Sede", "Nombre_Sede");
            ViewBag.IdPresupuesto = PK_PresupuestoPorAnio;
            return View();
        }
        /// <summary>
        /// Metodo que me retorna la vista parcial  con el inforne del presupuesto por cada sede
        /// </summary>
        /// <returns>vista</returns>
        // GET: Presupuesto
        public ActionResult GenerarInformePresupuesto(int IDPresupuestoAnio, int fecha, int intervaloDeTiempo, string nombreIntervaloTiempo)
        {
            ViewBag.IDPresupuestoAnio = IDPresupuestoAnio;
            ViewBag.fecha = fecha;
            ViewBag.intervaloDeTiempo = intervaloDeTiempo;
            ViewBag.nombreIntervaloTiempo = nombreIntervaloTiempo;
            return PartialView("InformePresupuestoVP", presupuestoServicios.CrearInformePresupuesto(IDPresupuestoAnio, fecha, intervaloDeTiempo));            
        }

        /// <summary>
        /// metodo que me retorna un json para crear la grafica del presupuesto disponible ejecutado y planeado
        /// </summary>
        /// <param name="IDPresupuestoAnio"></param>
        /// <param name="fecha"></param>
        /// <param name="intervaloDeTiempo"></param>
        /// <returns></returns>
        public ActionResult obtenerDataInformePresupuesto(int IDPresupuestoAnio, int fecha, int intervaloDeTiempo)
        {

            List<ActividadPresupuesto> aps = presupuestoServicios.CrearInformePresupuesto(IDPresupuestoAnio, fecha, intervaloDeTiempo);
            List<ActividadPresupuesto> apsAux = new List<ActividadPresupuesto>();
            foreach (ActividadPresupuesto ap in aps)
            {
                if (ap.actividadesPresupuesto != null)
                {
                    apsAux.AddRange(ap.actividadesPresupuesto);
                }
                else
                {
                    apsAux.Add(ap);
                }
            }

            return Json(
                       apsAux.Select(ap => new
                       {
                           nombreActividada = ap.DescripcionActividad,
                           presupuesto = ap.presupuestosPorMes.FirstOrDefault().PresupuestoMes,
                           prespuestoEjecutado = ap.presupuestosPorMes.FirstOrDefault().PresupuestoEjecutadoPorMes,
                           presupuestoDisponible = ap.presupuestosPorMes.FirstOrDefault().PresupuestoMes - ap.presupuestosPorMes.FirstOrDefault().PresupuestoEjecutadoPorMes
                       })
                    , JsonRequestBehavior.AllowGet);

        }

        /// <summary>
        /// Controlador que me exporta a excel el informa del presupuesto,la disponibilidad, ejecucion y planeacion
        /// </summary>
        /// <param name="IDPresupuestoAnio">Pk o id del presupuesto por año</param>
        /// <param name="fecha">entero que me representa el tipo de periodo ejemplo mensual,trimestra, semestral  y anual</param>
        /// <param name="intervaloDeTiempo">entero que me represtan cual tipo de periodo ejemplo enero o primera trimestre o segundo semestre</param>
        /// <param name="nombreIntervaloTiempo"> nombre del intervalo de tiempo ejemplo enero o segundo semestre</param>
        /// <returns>un archivo de excel</returns>
        public ActionResult ExpotarInformeExcel(int IDPresupuestoAnio, int fecha, int intervaloDeTiempo, string nombreIntervaloTiempo) 
        {
            MemoryStream stream = new MemoryStream();
            List<InformePresupuestoDTO> aps = presupuestoServicios.GenerarExcel(IDPresupuestoAnio, fecha, intervaloDeTiempo,nombreIntervaloTiempo);

            stream = recursosServicios.ExportarAExcel(aps);
            return File(stream, "application/vnd.ms-excel", "Informe Presupuesto.xls");
        }

        public ActionResult EliminarActividad(int pkActividad)
        {
            bool restpuestaGuardado = presupuestoServicios.EliminarActividad(pkActividad);
            string mensaje = "";
            if (restpuestaGuardado)
            {
                mensaje = "La actividad fue eliminada";
            }
            else
            {
                mensaje = "No fue posible eliminar la actividad";
            }

            return Json(new
            {
                success = restpuestaGuardado,
                mesansaje = mensaje,
            }
               , JsonRequestBehavior.AllowGet);
        }
    }
}