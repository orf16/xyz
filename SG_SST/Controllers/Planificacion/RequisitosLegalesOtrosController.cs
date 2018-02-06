using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SG_SST.Models.Planificacion;
using System.IO;
using SG_SST.Models;
using System.Data.Entity;
using SG_SST.Services.Planificacion.IServices;
using SG_SST.Services.Planificacion.Services;

using SG_SST.Dtos.Planificacion;

using System.Net;
using System.Configuration;
using SG_SST.Services.General.IServices;
using SG_SST.Services.General.Services;
using System.Data.Entity;
using System.IO;
using System.Xml.Serialization;

using SG_SST.Repositories.Planificacion.IRepositories;
using SG_SST.Repositories.Planificacion.Repositories;
using SG_SST.Controllers.Base;
using SG_SST.EntidadesDominio.Ausentismo;
using SG_SST.EntidadesDominio.Planificacion;
using LinqToExcel;

using iTextSharp.text;
using ClosedXML.Excel;
using System.Data.Entity;
using System.Data;


namespace SG_SST.Controllers.Planificacion
{
    public class RequisitosLegalesOtrosController : BaseController
    {
        private SG_SSTContext db = new SG_SSTContext();
        RequisitosLegalesOtrosServicios obIReq_Leg;
        private IRecursosServicios recursosServicios = new RecursosServicios();
        public static string varnombrematyriz;
        public static int Static_fk_matriz;
        public static int static_ActEconomica;
        public bool var;
        public static string static_NombreMatriz = "";



        // GET: RequisitosLegalesOtros
        public ActionResult Index()
        {


            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                return View();
            }
            ViewBag.Excel = false;

            var Tbl_Requisitos_Legales_Posipedia = db.Tbl_Requisitos_Legales_Posipedia.Include(u => u.Descripcion).Include(u => u.Descripcion);
            //var tbl_Usuario = db.Tbl_Usuario.Include(u => u.Roles).Include(u => u.TipoDocumentos);

            if (Tbl_Requisitos_Legales_Posipedia != null)
            {
                ViewBag.Excel = true;

            }

            return View(db.Tbl_Requisitos_Legales_Posipedia.ToList());
        }

        public ActionResult AgregarRequisitosLegalesOtros()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                return View();
            }
            ViewBag.FK_Cumplimiento_Evaluacion = new SelectList(db.Tbl_Cumplimiento_Evaluacion, "PK_Cumplimiento_Evaluacion", "Descripcion_Cumplimiento_Evaluacion");

            ViewBag.FK_Estado_RequisitoslegalesOtros = new SelectList(db.Tbl_Estado_RequisitoslegalesOtros, "PK_Estado_RequisitoslegalesOtros", "Descripcion_Estado_RequisitoslegalesOtros");


            return View();
        }



        public ActionResult EditarRequisitosLegalesOtros(int id)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                return View();
            }
            obIReq_Leg = new RequisitosLegalesOtrosServicios();
            RequisitosLegalesOtros objre = obIReq_Leg.BuscarRequisitosLegalesOtros(id);

            ViewBag.FK_Cumplimiento_Evaluacion = new SelectList(db.Tbl_Cumplimiento_Evaluacion, "PK_Cumplimiento_Evaluacion", "Descripcion_Cumplimiento_Evaluacion", objre.FK_Cumplimiento_Evaluacion);

            ViewBag.FK_Estado_RequisitoslegalesOtros = new SelectList(db.Tbl_Estado_RequisitoslegalesOtros, "PK_Estado_RequisitoslegalesOtros", "Descripcion_Estado_RequisitoslegalesOtros", objre.FK_Estado_RequisitoslegalesOtros);

            return View(objre);
        }

        public ActionResult GuardarEdicionRequisitosLegalesOtros(RequisitosLegalesOtros objreq)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                return View();
            }
            obIReq_Leg = new RequisitosLegalesOtrosServicios();
            obIReq_Leg.ModficarRequisitosLegalesOtros(objreq);

            return RedirectToAction("Index");
        }



        public ActionResult Busqueda_RequisitosLegales_Peligro(string strPeligroBusqueda)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                return View();
            }
            int PK_Empresa;

            obIReq_Leg = new RequisitosLegalesOtrosServicios();

            List<RequisitosLegalesOtros> Objlist_RequisitosLegales = new List<RequisitosLegalesOtros>();

            PK_Empresa = usuarioActual.IdEmpresa;

            Objlist_RequisitosLegales = obIReq_Leg.Busqueda_RequisitosLegales_Peligro(strPeligroBusqueda, PK_Empresa);

            //return Objlist_RequisitosLegales;


            return PartialView("RequisitosLegalesBusquedaVP", Objlist_RequisitosLegales);

        }






        public ActionResult CrearMatriz()
        {



       
             //LeerExcelAGRICULTURA();
             //   LeerExcelCOMERCIO();
             //   LeerExcelCONSTRUCCION();
             //   LeerExcelActsInmobiliarias();
             //   LeerExcelMANUFACTURERAS();
             //   LeerExcelMINERIA();
             //   LeerExcelSALUD();
             //  LeerExcelTransporte();
             //   LeerExcelELECTRICIDADYGAS();
             //   LeerExcelSANEAMIENTO();
             //   LeerExcelALOJAMIENTO();
             //   LeerExcelCOMUNICACIONES();
             //   LeerExcelActFinancieras();
             //   LeerExcelActsProfesionales();
             //   LeerExcelActsAdministrativas();
             //   LeerExcelAdministracionpublica();
             //   LeerExcelEDUCACION();
             //   LeerExcelENTRETENIMIENTO();
             //   LeerExcelOtrasActsdeServicios();
             //   LeerExcelActsdelosHogares();
             //   LeerExcelExtraterritorial();
             //   LeerExcelTransversal();
            

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                return View();
            }
            ViewBag.Excel = false;

            var Tbl_Requisitos_Legales_Posipedia = db.Tbl_Requisitos_Legales_Posipedia.Include(u => u.Descripcion).Include(u => u.Descripcion);
            //var tbl_Usuario = db.Tbl_Usuario.Include(u => u.Roles).Include(u => u.TipoDocumentos);

            if (Tbl_Requisitos_Legales_Posipedia != null)
            {
                ViewBag.Excel = true;

            }
            ViewBag.FK_Actividad_Economica = new SelectList(db.Tbl_Actividad_Economica, "PK_Actividad_Economica", "Ente");
            return View(db.Tbl_Requisitos_Legales_Posipedia.ToList());


        }

        /// <summary>
        /// metodo que permite selecciionar la actividad economica para mostrar los requisitos legales que pertenecen a dicho requisito legal
        /// </summary>
        /// <param name="Actividad_Economica"></param>
        /// <returns></returns>
        public ActionResult Busqueda_PorActividadEconomica(int Actividad_Economica)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                return View();
            }

            if (Actividad_Economica != 0)
            {
                ViewBag.Messages = "Por favor seleccione una actividad económica para realizar la consulta";
            int PK_Empresa;
            obIReq_Leg = new RequisitosLegalesOtrosServicios();
            List<RequisitosLegalesPosipedia> Objlist_RequisitosLegales = new List<RequisitosLegalesPosipedia>();
            PK_Empresa = usuarioActual.IdEmpresa;
            Objlist_RequisitosLegales = obIReq_Leg.Busqueda_PorActividadEconomica(Actividad_Economica, PK_Empresa);
            //return Objlist_RequisitosLegales;
            return PartialView("CrearMatrizVP", Objlist_RequisitosLegales);
            }
            else {
                int PK_Empresa;
                obIReq_Leg = new RequisitosLegalesOtrosServicios();
                List<RequisitosLegalesPosipedia> Objlist_RequisitosLegales = new List<RequisitosLegalesPosipedia>();
                PK_Empresa = usuarioActual.IdEmpresa;
                Objlist_RequisitosLegales = obIReq_Leg.Busqueda_PorActividadEconomica(Actividad_Economica, PK_Empresa);
                return PartialView("CrearMatriz", Objlist_RequisitosLegales);
            
            }          

        }



        /// <summary>
        /// metodo con el que se crea la matriz
        /// </summary>
        /// <param name="NombreMatriz"></param>
        /// <returns></returns>
        public ActionResult AgregarRequisitoMatriz(string NombreMatriz)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                return View();
            }
               obIReq_Leg = new RequisitosLegalesOtrosServicios();
            try
            {

                if (obIReq_Leg.Busqueda_Matricesduplicado(NombreMatriz, usuarioActual.IdEmpresa) == false)
                {

                    MatrizRequisitosLegales objreqmat = new MatrizRequisitosLegales();

                    objreqmat.NombreMatriz = NombreMatriz;

                    varnombrematyriz = objreqmat.NombreMatriz;

                    objreqmat.FK_Empresa = usuarioActual.IdEmpresa;

                    obIReq_Leg = new RequisitosLegalesOtrosServicios();
                    if (obIReq_Leg.GrabarRequisitosLegalesOtros(objreqmat) == true)
                    {
                        return Json(new { success = true }
                                , JsonRequestBehavior.AllowGet);
                    }
                    else {
                        return Json(new { success = false }
                                    , JsonRequestBehavior.AllowGet);                    
                    }                
                }
                else
                {
                    return Json(new { success = false }
                                , JsonRequestBehavior.AllowGet);
                }    

            

            }
            catch (Exception)
            {
                ViewBag.Messages = "Error";
                return RedirectToAction("Index");
            }
        }






        /// <summary>
        /// metodo con el que se crea la matriz
        /// </summary>
        /// <param name="NombreMatriz"></param>
        /// <returns></returns>
        public ActionResult AgregarTablaRequisitos(Int32[] customerIDs, int idactividadeconomica)
        {
            try
            {
                int r = 0;
                bool valor;

                var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
                if (usuarioActual == null)
                {
                    ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                    return View();
                }


                foreach (Int32 customerID in customerIDs)
                {
                    if (customerID == 0)
                    {
                        r = 1;
                    }
                }

                if (r == 0)
                {

                    foreach (Int32 customerID in customerIDs)
                    {

                        RequisitosLegalesPosipedia modreq = db.Tbl_Requisitos_Legales_Posipedia.Find((customerID));

                        RequisitosLegalesOtros objre = new RequisitosLegalesOtros();
                        objre.Tipo_Norma = modreq.Tipo_Norma;
                        objre.Numero_Norma = modreq.Numero_Norma;
                        objre.FechaPublicacion = modreq.FechaPublicacion;
                        objre.Ente = modreq.Ente;
                        objre.Articulo = modreq.Articulo;
                        objre.Descripcion = modreq.Descripcion;
                        objre.Sugerencias = modreq.Sugerencias;
                        objre.Clase_De_Peligro = modreq.Clase_De_Peligro;
                        objre.Peligro = modreq.Peligro;
                        objre.Aspectos = modreq.Aspectos;
                        objre.Impactos = modreq.Impactos;
                        objre.Evidencia_Cumplimiento = "";
                        objre.FK_Cumplimiento_Evaluacion = 4;
                        objre.Hallazgo = "";
                        objre.FK_Estado_RequisitoslegalesOtros = 4;
                        objre.Responsable = "";
                        objre.Fecha_Seguimiento_Control = DateTime.Now;
                        //var fecha1 = Convert.ToString(objre.Fecha_Seguimiento_Control);
                        //fecha1 = "";
                        objre.Fecha_Actualizacion = DateTime.Now;
                        //var fecha2 = Convert.ToString(objre.Fecha_Actualizacion);
                        //fecha2 = "";
                        //objre.FK_Empresa = usuarioActual.IdEmpresa;
                        objre.FK_Actividad_Economica = idactividadeconomica;
                        string var = varnombrematyriz;
                        obIReq_Leg = new RequisitosLegalesOtrosServicios();
                        obIReq_Leg.GuardarRequisitos_Seleccionados(objre, Static_fk_matriz);


                    }
                    int mivalor = 1;
                    obIReq_Leg.valorvariable(mivalor);
                    return Json(new { success = true }
                            , JsonRequestBehavior.AllowGet);


                }



                else
                {
                    return Json(new { success = false }
                     , JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                ViewBag.Excel = false;

                var Tbl_Requisitos_Legales_Posipedia = db.Tbl_Requisitos_Legales_Posipedia.Include(u => u.Descripcion).Include(u => u.Descripcion);
                //var tbl_Usuario = db.Tbl_Usuario.Include(u => u.Roles).Include(u => u.TipoDocumentos);

                if (Tbl_Requisitos_Legales_Posipedia != null)
                {
                    ViewBag.Excel = true;
                }
                ViewBag.FK_Actividad_Economica = new SelectList(db.Tbl_Actividad_Economica, "PK_Actividad_Economica", "Ente");
                return View("CrearMatriz", db.Tbl_Requisitos_Legales_Posipedia.ToList());
            }
          
        }
           
           
          
        


        /*

        /// <summary>
        /// con este metodo se guardan los requisitos seleccionados
        /// </summary>
        /// <param name="customerIDs"></param>
        /// <param name="idactividadeconomica"></param>
        /// <returns></returns>
        public bool AgregarTablaRequisitos(Int32[] customerIDs, int idactividadeconomica)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);

            foreach (Int32 customerID in customerIDs)
            {
                RequisitosLegalesPosipedia modreq = db.Tbl_Requisitos_Legales_Posipedia.Find((customerID));

                RequisitosLegalesOtros objre = new RequisitosLegalesOtros();
                objre.Tipo_Norma = modreq.Tipo_Norma;
                objre.Numero_Norma = modreq.Numero_Norma;
                objre.FechaPublicacion = modreq.FechaPublicacion;
                objre.Ente = modreq.Ente;
                objre.Articulo = modreq.Articulo;
                objre.Descripcion = modreq.Descripcion;
                objre.Sugerencias = modreq.Sugerencias;
                objre.Clase_De_Peligro = modreq.Clase_De_Peligro;
                objre.Peligro = modreq.Peligro;
                objre.Aspectos = modreq.Aspectos;
                objre.Impactos = modreq.Impactos;
                objre.Evidencia_Cumplimiento = "";
                objre.FK_Cumplimiento_Evaluacion = 3;
                objre.Hallazgo = "";
                objre.FK_Estado_RequisitoslegalesOtros = 3;
                objre.Responsable = "";
                objre.Fecha_Seguimiento_Control = DateTime.Now;
                objre.Fecha_Actualizacion = DateTime.Now;
                //objre.FK_Empresa = usuarioActual.IdEmpresa;
                objre.FK_Actividad_Economica = idactividadeconomica;

                string var = varnombrematyriz;

                obIReq_Leg = new RequisitosLegalesOtrosServicios();
                obIReq_Leg.GuardarRequisitos_Seleccionados(objre, varnombrematyriz);

                return true;
            }


            return true;

        }
        */


        



        /// <summary>
        /// metodo que permite selecciionar la actividad economica para mostrar los requisitos legales que pertenecen a dicho requisito legal
        /// </summary>
        /// <param name="Actividad_Economica"></param>
        /// <returns></returns>
        public ActionResult ListadoMastrices()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
               
            }


           // return View(db.Tbl_Matriz_RequisitosLegales.GroupBy(x => x.PK_MatrizRequisitosLegales && x.FK_Empresa == usuarioActual.IdEmpresa).Select(x => x.FirstOrDefault()));//lista para mostrar los archivos cargados

            //return View(db.Tbl_Requisitos_Matriz.GroupBy(x => x.FK_MatrizRequisitosLegales).Select(x => x.FirstOrDefault()));//lista para mostrar los archivos cargados

            //return View(db.Tbl_Requisitos_Matriz.ToList());//lista para mostrar los archivos cargados


            List<MatrizRequisitosLegales> Objlist_RequisitosLegales = new List<MatrizRequisitosLegales>();
            //return Objlist_RequisitosLegales =db.Tbl_Matriz_RequisitosLegales.Where(s => s.FK_Empresa ==  usuarioActual.IdEmpresa).ToList();

            return View(Objlist_RequisitosLegales =db.Tbl_Matriz_RequisitosLegales.Where(s => s.FK_Empresa ==  usuarioActual.IdEmpresa).ToList());

        }






        /// <summary>
        /// metodo que permite mostrar los requisitos legales de la matriz seleccionada
        /// </summary>
        /// <param name="objm"></param>
        /// <returns></returns>
        public List<EDRequisitosLegalesOtros> MostrarRequiitosMatriz(int FK_MatrizRequisitosLegales)
        {

            //MatrizRequisitosLegales objmatreq = dbReqLeg.Tbl_Matriz_RequisitosLegales.Where(g => g.NombreMatriz == varnombrematyriz).FirstOrDefault();
            List<EDRequisitosLegalesOtros> varmatreq = new List<EDRequisitosLegalesOtros>();

            using (SG_SSTContext context = new SG_SSTContext())
            {
                var Varmatreq = (from a in context.Tbl_Requisitos_Matriz
                                 join b in context.Tbl_Requisitos_legales_Otros on
                                 a.FK_RequisitosLegalesOtros equals b.PK_RequisitosLegalesOtros
                                 join c in context.Tbl_Estado_RequisitoslegalesOtros on
                                 b.FK_Estado_RequisitoslegalesOtros equals c.PK_Estado_RequisitoslegalesOtros
                                 join d in context.Tbl_Cumplimiento_Evaluacion on
                                 b.FK_Cumplimiento_Evaluacion equals d.PK_Cumplimiento_Evaluacion


                                 where a.FK_MatrizRequisitosLegales == FK_MatrizRequisitosLegales
                                 //&& a.PK_Requisitos_Matriz == 1
                                 //                   select b).ToList();

                                 // varmatreq = Varmatreq;
                                 //  return varmatreq;
                                                                                       


                                 select new EDRequisitosLegalesOtros()
                                 {
                                     PKRequisitoLegal = b.PK_RequisitosLegalesOtros,
                                     TipoNorma = b.Tipo_Norma,
                                     NumeroNorma = b.Numero_Norma,
                                     FechaPublicacionReq = b.FechaPublicacion,
                                     EnteReq = b.Ente,
                                     ArticuloReq = b.Articulo,
                                     DescripcionReq = b.Descripcion,
                                     SugerenciasReq = b.Sugerencias,
                                     ClaseDePeligro = b.Clase_De_Peligro,
                                     PeligroReq = b.Peligro,
                                     AspectosReq = b.Aspectos,
                                     ImpactosReq = b.Impactos,
                                     EvidenciaCumplimiento = b.Evidencia_Cumplimiento,
                                     Descripcion_Cumplimiento_Evaluacion = d.Descripcion_Cumplimiento_Evaluacion,
                                     HallazgoReq = b.Hallazgo,
                                     Descripcion_Estado_RequisitoslegalesOtros = c.Descripcion_Estado_RequisitoslegalesOtros,
                                     ResponsableReq = b.Responsable,
                                     Fecha_Seguimiento_ControlReq = b.Fecha_Seguimiento_Control,
                                     Fecha_ActualizacionReq = b.Fecha_Actualizacion,
                                     FK_ActividadEconomica = b.FK_Actividad_Economica,
                                 }).ToList();
                                     
                //static_ActEconomica = Varmatreq;
                if (Varmatreq.Count != 0) {

                ViewBag.FK_Actividad_Economica = Varmatreq.FirstOrDefault().FK_ActividadEconomica;
                                return Varmatreq;

                }
                else
                {
                    var Varmatreq2 = (from a in context.Tbl_Requisitos_Matriz
                                     join b in context.Tbl_Requisitos_legales_Otros on
                                     a.FK_RequisitosLegalesOtros equals b.PK_RequisitosLegalesOtros
                                     join c in context.Tbl_Estado_RequisitoslegalesOtros on
                                     b.FK_Estado_RequisitoslegalesOtros equals c.PK_Estado_RequisitoslegalesOtros
                                     join d in context.Tbl_Cumplimiento_Evaluacion on
                                     b.FK_Cumplimiento_Evaluacion equals d.PK_Cumplimiento_Evaluacion

                                     where a.FK_MatrizRequisitosLegales == FK_MatrizRequisitosLegales
                                   
                                     select new EDRequisitosLegalesOtros()
                                     {
                                         PKRequisitoLegal = b.PK_RequisitosLegalesOtros,
                                         TipoNorma = b.Tipo_Norma,
                                         NumeroNorma = b.Numero_Norma,
                                         FechaPublicacionReq = b.FechaPublicacion,
                                         EnteReq = b.Ente,
                                         ArticuloReq = b.Articulo,
                                         DescripcionReq = b.Descripcion,
                                         SugerenciasReq = b.Sugerencias,
                                         ClaseDePeligro = b.Clase_De_Peligro,
                                         PeligroReq = b.Peligro,
                                         AspectosReq = b.Aspectos,
                                         ImpactosReq = b.Impactos,
                                         EvidenciaCumplimiento = b.Evidencia_Cumplimiento,
                                         Descripcion_Cumplimiento_Evaluacion = d.Descripcion_Cumplimiento_Evaluacion,
                                         HallazgoReq = b.Hallazgo,
                                         Descripcion_Estado_RequisitoslegalesOtros = c.Descripcion_Estado_RequisitoslegalesOtros,
                                         ResponsableReq = b.Responsable,
                                         Fecha_Seguimiento_ControlReq = b.Fecha_Seguimiento_Control,
                                         Fecha_ActualizacionReq = b.Fecha_Actualizacion,
                                         FK_ActividadEconomica = b.FK_Actividad_Economica,
                                     }).ToList();


                    

                    ViewBag.FK_Actividad_Economica = 0;
                    return Varmatreq2;


                }

            }

        } 
        
        
        
        
        
        
        
        /// vista que permite editar la matriz seleccionada
        /// </summary>
        /// <returns></returns>
        public ActionResult EditarMatriz(int FK_MatrizRequisitosLegales,string NombreMatriz)
        {
            List<EDRequisitosLegalesOtros> varmatreq = new List<EDRequisitosLegalesOtros>();
            Static_fk_matriz = FK_MatrizRequisitosLegales;

            //MostrarRequiitosMatriz(PK_Requisitos_Matriz);
            //ViewBag.EDRequisitosLegalesOtros = MostrarRequiitosMatriz(PK_Requisitos_Matriz);
            ViewBag.FK_MatrizRequisitosLegales = FK_MatrizRequisitosLegales;
            //return View(MostrarRequiitosMatriz(FK_MatrizRequisitosLegales));


            static_NombreMatriz = NombreMatriz;


           varmatreq = MostrarRequiitosMatriz(FK_MatrizRequisitosLegales);

           if (varmatreq.Count !=0)
           {
               return View(varmatreq);

           }
           else
           {
               ViewBag.FK_Actividad_Economica = new SelectList(db.Tbl_Actividad_Economica, "PK_Actividad_Economica", "Ente");
               return View("CrearMatrizSinDatos",db.Tbl_Requisitos_Legales_Posipedia.ToList());
              

           }
        }



        public ActionResult CrearMatrizSinDatos(int FK_MatrizRequisitosLegales)
        {           
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                return View();
            }
            ViewBag.Excel = false;

            var Tbl_Requisitos_Legales_Posipedia = db.Tbl_Requisitos_Legales_Posipedia.Include(u => u.Descripcion).Include(u => u.Descripcion);
            //var tbl_Usuario = db.Tbl_Usuario.Include(u => u.Roles).Include(u => u.TipoDocumentos);

            if (Tbl_Requisitos_Legales_Posipedia != null)
            {
                ViewBag.Excel = true;

            }
            ViewBag.FK_Actividad_Economica = new SelectList(db.Tbl_Actividad_Economica, "PK_Actividad_Economica", "Ente");
            return View(db.Tbl_Requisitos_Legales_Posipedia.ToList());



        }







        /// <summary>
        /// vista que permite editar la matriz seleccionada
        /// </summary>
        /// <returns></returns>
        public ActionResult AgregarNuevoRequisitoLegal(int FK_MatrizRequisitosLegales,int FKActividadEconomica)
        {
            //MostrarRequiitosMatriz(PK_Requisitos_Matriz);
            //ViewBag.EDRequisitosLegalesOtros = MostrarRequiitosMatriz(PK_Requisitos_Matriz);
            ViewBag.FK_MatrizRequisitosLegales = FK_MatrizRequisitosLegales;
            ViewBag.FKActividadEconomica = FKActividadEconomica;
            return View();
        }




        public void LeerExcelAGRICULTURA()
        {

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);

            var fileName = "Requisitos_Legales_Data_Integración.xlsx";
            var filePath = Path.Combine(HttpRuntime.AppDomainAppPath, "Content/ArchivosDocumentacion/" + fileName);
            //var filePath = Path.Combine(Server.MapPath("~/Content/ArchivosDocumentacion"), fileName);
            var serverPath = "ftp://ftp_manuelaB@190.26.216.110:990//Requisitos_Legales_Data_Integración.xlsx";
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(serverPath);
            request.KeepAlive = true;
            request.UsePassive = true;
            request.UseBinary = true;
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.Credentials = new NetworkCredential("ftp_manuelaB", "f7hUwGQs");
            request.EnableSsl = true;
            // Read the file from the server & write to destination   
            ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);
            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse()) // Error here
            using (Stream responseStream = response.GetResponseStream())
            //using (StreamReader reader = new StreamReader(responseStream))
            //using (StreamWriter destination = new StreamWriter(filePath))
            {
                int bytesRead = 0;
                byte[] buffer = new byte[2048];
                FileStream fileStream = new FileStream(filePath, FileMode.Create);
                while (true)
                {
                    bytesRead = responseStream.Read(buffer, 0, buffer.Length);

                    if (bytesRead == 0)
                        break;

                    fileStream.Write(buffer, 0, bytesRead);
                }
                fileStream.Close();
            }



            //creamos el libro a partir de la ruta
            var Book = new ExcelQueryFactory(filePath);

            //Consulta con Linq
            var resp = (from row in Book.Worksheet("AGRICULTURA")
                        let item = new RequisitosLegalesPosipedia
                        {

                            Tipo_Norma = row[0].Cast<string>(),
                            Numero_Norma = row[1].Cast<string>(),
                            FechaPublicacion = row[2].Cast<DateTime>(),
                            Ente = row[3].Cast<string>(),
                            Articulo = row[4].Cast<string>(),
                            Descripcion = row[5].Cast<string>(),
                            Sugerencias = row[6].Cast<string>(),
                            Clase_De_Peligro = row[7].Cast<string>(),
                            Peligro = row[8].Cast<string>(),
                            Aspectos = row[9].Cast<string>(),
                            Impactos = row[10].Cast<string>(),
                          
                            FK_Actividad_Economica = 1,

                        }
                        select item).ToList();




            Book.Dispose();
            db.Tbl_Requisitos_Legales_Posipedia.AddRange(resp);
            db.SaveChanges();


        }




      
        public void LeerExcelCOMERCIO()
        {

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);

            var fileName = "Requisitos_Legales_Data_Integración.xlsx";
            var filePath = Path.Combine(HttpRuntime.AppDomainAppPath, "Content/ArchivosDocumentacion/" + fileName);
            //var filePath = Path.Combine(Server.MapPath("~/Content/ArchivosDocumentacion"), fileName);
            var serverPath = "ftp://ftp_manuelaB@190.26.216.110:990//Requisitos_Legales_Data_Integración.xlsx";
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(serverPath);
            request.KeepAlive = true;
            request.UsePassive = true;
            request.UseBinary = true;
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.Credentials = new NetworkCredential("ftp_manuelaB", "f7hUwGQs");
            request.EnableSsl = true;
            // Read the file from the server & write to destination   
            ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);
            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse()) // Error here
            using (Stream responseStream = response.GetResponseStream())
            //using (StreamReader reader = new StreamReader(responseStream))
            //using (StreamWriter destination = new StreamWriter(filePath))
            {
                int bytesRead = 0;
                byte[] buffer = new byte[2048];
                FileStream fileStream = new FileStream(filePath, FileMode.Create);
                while (true)
                {
                    bytesRead = responseStream.Read(buffer, 0, buffer.Length);

                    if (bytesRead == 0)
                        break;

                    fileStream.Write(buffer, 0, bytesRead);
                }
                fileStream.Close();
            }



            //creamos el libro a partir de la ruta
            var Book = new ExcelQueryFactory(filePath);

            //Consulta con Linq
            var resp = (from row in Book.Worksheet("COMERCIO")
                        let item = new RequisitosLegalesPosipedia
                        {

                            Tipo_Norma = row[0].Cast<string>(),
                            Numero_Norma = row[1].Cast<string>(),
                            FechaPublicacion = row[2].Cast<DateTime>(),
                            Ente = row[3].Cast<string>(),
                            Articulo = row[4].Cast<string>(),
                            Descripcion = row[5].Cast<string>(),
                            Sugerencias = row[6].Cast<string>(),
                            Clase_De_Peligro = row[7].Cast<string>(),
                            Peligro = row[8].Cast<string>(),
                            Aspectos = row[9].Cast<string>(),
                            Impactos = row[10].Cast<string>(),
                          
                            FK_Actividad_Economica = 2,

                        }
                        select item).ToList();




            Book.Dispose();
            db.Tbl_Requisitos_Legales_Posipedia.AddRange(resp);
            db.SaveChanges();


        }
     



        public void LeerExcelCONSTRUCCION()
        {

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);

            var fileName = "Requisitos_Legales_Data_Integración.xlsx";
            var filePath = Path.Combine(HttpRuntime.AppDomainAppPath, "Content/ArchivosDocumentacion/" + fileName);
            //var filePath = Path.Combine(Server.MapPath("~/Content/ArchivosDocumentacion"), fileName);
            var serverPath = "ftp://ftp_manuelaB@190.26.216.110:990//Requisitos_Legales_Data_Integración.xlsx";
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(serverPath);
            request.KeepAlive = true;
            request.UsePassive = true;
            request.UseBinary = true;
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.Credentials = new NetworkCredential("ftp_manuelaB", "f7hUwGQs");
            request.EnableSsl = true;
            // Read the file from the server & write to destination   
            ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);
            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse()) // Error here
            using (Stream responseStream = response.GetResponseStream())
            //using (StreamReader reader = new StreamReader(responseStream))
            //using (StreamWriter destination = new StreamWriter(filePath))
            {
                int bytesRead = 0;
                byte[] buffer = new byte[2048];
                FileStream fileStream = new FileStream(filePath, FileMode.Create);
                while (true)
                {
                    bytesRead = responseStream.Read(buffer, 0, buffer.Length);

                    if (bytesRead == 0)
                        break;

                    fileStream.Write(buffer, 0, bytesRead);
                }
                fileStream.Close();
            }



            //creamos el libro a partir de la ruta
            var Book = new ExcelQueryFactory(filePath);

            //Consulta con Linq
            var resp = (from row in Book.Worksheet("CONSTRUCCION")
                        let item = new RequisitosLegalesPosipedia
                        {

                            Tipo_Norma = row[0].Cast<string>(),
                            Numero_Norma = row[1].Cast<string>(),
                            FechaPublicacion = row[2].Cast<DateTime>(),
                            Ente = row[3].Cast<string>(),
                            Articulo = row[4].Cast<string>(),
                            Descripcion = row[5].Cast<string>(),
                            Sugerencias = row[6].Cast<string>(),
                            Clase_De_Peligro = row[7].Cast<string>(),
                            Peligro = row[8].Cast<string>(),
                            Aspectos = row[9].Cast<string>(),
                            Impactos = row[10].Cast<string>(),
                          
                            FK_Actividad_Economica = 3,

                        }
                        select item).ToList();




            Book.Dispose();
            db.Tbl_Requisitos_Legales_Posipedia.AddRange(resp);
            db.SaveChanges();


        }



     

        public void LeerExcelActsInmobiliarias()
        {

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);

            var fileName = "Requisitos_Legales_Data_Integración.xlsx";
            var filePath = Path.Combine(HttpRuntime.AppDomainAppPath, "Content/ArchivosDocumentacion/" + fileName);
            //var filePath = Path.Combine(Server.MapPath("~/Content/ArchivosDocumentacion"), fileName);
            var serverPath = "ftp://ftp_manuelaB@190.26.216.110:990//Requisitos_Legales_Data_Integración.xlsx";
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(serverPath);
            request.KeepAlive = true;
            request.UsePassive = true;
            request.UseBinary = true;
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.Credentials = new NetworkCredential("ftp_manuelaB", "f7hUwGQs");
            request.EnableSsl = true;
            // Read the file from the server & write to destination   
            ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);
            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse()) // Error here
            using (Stream responseStream = response.GetResponseStream())
            //using (StreamReader reader = new StreamReader(responseStream))
            //using (StreamWriter destination = new StreamWriter(filePath))
            {
                int bytesRead = 0;
                byte[] buffer = new byte[2048];
                FileStream fileStream = new FileStream(filePath, FileMode.Create);
                while (true)
                {
                    bytesRead = responseStream.Read(buffer, 0, buffer.Length);

                    if (bytesRead == 0)
                        break;

                    fileStream.Write(buffer, 0, bytesRead);
                }
                fileStream.Close();
            }



            //creamos el libro a partir de la ruta
            var Book = new ExcelQueryFactory(filePath);

            //Consulta con Linq
            var resp = (from row in Book.Worksheet("ActividadesInmobiliarias")
                        let item = new RequisitosLegalesPosipedia
                        {

                            Tipo_Norma = row[0].Cast<string>(),
                            Numero_Norma = row[1].Cast<string>(),
                            FechaPublicacion = row[2].Cast<DateTime>(),
                            Ente = row[3].Cast<string>(),
                            Articulo = row[4].Cast<string>(),
                            Descripcion = row[5].Cast<string>(),
                            Sugerencias = row[6].Cast<string>(),
                            Clase_De_Peligro = row[7].Cast<string>(),
                            Peligro = row[8].Cast<string>(),
                            Aspectos = row[9].Cast<string>(),
                            Impactos = row[10].Cast<string>(),
                         
                            FK_Actividad_Economica = 4,

                        }
                        select item).ToList();




            Book.Dispose();
            db.Tbl_Requisitos_Legales_Posipedia.AddRange(resp);
            db.SaveChanges();


        }
      


        public void LeerExcelMANUFACTURERAS()
        {

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);

            var fileName = "Requisitos_Legales_Data_Integración.xlsx";
            var filePath = Path.Combine(HttpRuntime.AppDomainAppPath, "Content/ArchivosDocumentacion/" + fileName);
            //var filePath = Path.Combine(Server.MapPath("~/Content/ArchivosDocumentacion"), fileName);
            var serverPath = "ftp://ftp_manuelaB@190.26.216.110:990//Requisitos_Legales_Data_Integración.xlsx";
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(serverPath);
            request.KeepAlive = true;
            request.UsePassive = true;
            request.UseBinary = true;
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.Credentials = new NetworkCredential("ftp_manuelaB", "f7hUwGQs");
            request.EnableSsl = true;
            // Read the file from the server & write to destination   
            ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);
            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse()) // Error here
            using (Stream responseStream = response.GetResponseStream())
            //using (StreamReader reader = new StreamReader(responseStream))
            //using (StreamWriter destination = new StreamWriter(filePath))
            {
                int bytesRead = 0;
                byte[] buffer = new byte[2048];
                FileStream fileStream = new FileStream(filePath, FileMode.Create);
                while (true)
                {
                    bytesRead = responseStream.Read(buffer, 0, buffer.Length);

                    if (bytesRead == 0)
                        break;

                    fileStream.Write(buffer, 0, bytesRead);
                }
                fileStream.Close();
            }



            //creamos el libro a partir de la ruta
            var Book = new ExcelQueryFactory(filePath);

            //Consulta con Linq
            var resp = (from row in Book.Worksheet("MANUFACTURERAS")
                        let item = new RequisitosLegalesPosipedia
                        {

                            Tipo_Norma = row[0].Cast<string>(),
                            Numero_Norma = row[1].Cast<string>(),
                            FechaPublicacion = row[2].Cast<DateTime>(),
                            Ente = row[3].Cast<string>(),
                            Articulo = row[4].Cast<string>(),
                            Descripcion = row[5].Cast<string>(),
                            Sugerencias = row[6].Cast<string>(),
                            Clase_De_Peligro = row[7].Cast<string>(),
                            Peligro = row[8].Cast<string>(),
                            Aspectos = row[9].Cast<string>(),
                            Impactos = row[10].Cast<string>(),
                       
                            FK_Actividad_Economica = 5,

                        }
                        select item).ToList();




            Book.Dispose();
            db.Tbl_Requisitos_Legales_Posipedia.AddRange(resp);
            db.SaveChanges();


        }




        public void LeerExcelMINERIA()
        {

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);

            var fileName = "Requisitos_Legales_Data_Integración.xlsx";
            var filePath = Path.Combine(HttpRuntime.AppDomainAppPath, "Content/ArchivosDocumentacion/" + fileName);
            //var filePath = Path.Combine(Server.MapPath("~/Content/ArchivosDocumentacion"), fileName);
            var serverPath = "ftp://ftp_manuelaB@190.26.216.110:990//Requisitos_Legales_Data_Integración.xlsx";
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(serverPath);
            request.KeepAlive = true;
            request.UsePassive = true;
            request.UseBinary = true;
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.Credentials = new NetworkCredential("ftp_manuelaB", "f7hUwGQs");
            request.EnableSsl = true;
            // Read the file from the server & write to destination   
            ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);
            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse()) // Error here
            using (Stream responseStream = response.GetResponseStream())
            //using (StreamReader reader = new StreamReader(responseStream))
            //using (StreamWriter destination = new StreamWriter(filePath))
            {
                int bytesRead = 0;
                byte[] buffer = new byte[2048];
                FileStream fileStream = new FileStream(filePath, FileMode.Create);
                while (true)
                {
                    bytesRead = responseStream.Read(buffer, 0, buffer.Length);

                    if (bytesRead == 0)
                        break;

                    fileStream.Write(buffer, 0, bytesRead);
                }
                fileStream.Close();
            }



            //creamos el libro a partir de la ruta
            var Book = new ExcelQueryFactory(filePath);

            //Consulta con Linq
            var resp = (from row in Book.Worksheet("MINERIA")
                        let item = new RequisitosLegalesPosipedia
                        {

                            Tipo_Norma = row[0].Cast<string>(),
                            Numero_Norma = row[1].Cast<string>(),
                            FechaPublicacion = row[2].Cast<DateTime>(),
                            Ente = row[3].Cast<string>(),
                            Articulo = row[4].Cast<string>(),
                            Descripcion = row[5].Cast<string>(),
                            Sugerencias = row[6].Cast<string>(),
                            Clase_De_Peligro = row[7].Cast<string>(),
                            Peligro = row[8].Cast<string>(),
                            Aspectos = row[9].Cast<string>(),
                            Impactos = row[10].Cast<string>(),
                        
                            FK_Actividad_Economica = 6,

                        }
                        select item).ToList();




            Book.Dispose();
            db.Tbl_Requisitos_Legales_Posipedia.AddRange(resp);
            db.SaveChanges();


        }





        public void LeerExcelSALUD()
        {

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);

            var fileName = "Requisitos_Legales_Data_Integración.xlsx";
            var filePath = Path.Combine(HttpRuntime.AppDomainAppPath, "Content/ArchivosDocumentacion/" + fileName);
            //var filePath = Path.Combine(Server.MapPath("~/Content/ArchivosDocumentacion"), fileName);
            var serverPath = "ftp://ftp_manuelaB@190.26.216.110:990//Requisitos_Legales_Data_Integración.xlsx";
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(serverPath);
            request.KeepAlive = true;
            request.UsePassive = true;
            request.UseBinary = true;
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.Credentials = new NetworkCredential("ftp_manuelaB", "f7hUwGQs");
            request.EnableSsl = true;
            // Read the file from the server & write to destination   
            ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);
            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse()) // Error here
            using (Stream responseStream = response.GetResponseStream())
            //using (StreamReader reader = new StreamReader(responseStream))
            //using (StreamWriter destination = new StreamWriter(filePath))
            {
                int bytesRead = 0;
                byte[] buffer = new byte[2048];
                FileStream fileStream = new FileStream(filePath, FileMode.Create);
                while (true)
                {
                    bytesRead = responseStream.Read(buffer, 0, buffer.Length);

                    if (bytesRead == 0)
                        break;

                    fileStream.Write(buffer, 0, bytesRead);
                }
                fileStream.Close();
            }



            //creamos el libro a partir de la ruta
            var Book = new ExcelQueryFactory(filePath);

            //Consulta con Linq
            var resp = (from row in Book.Worksheet("SALUD")
                        let item = new RequisitosLegalesPosipedia
                        {

                            Tipo_Norma = row[0].Cast<string>(),
                            Numero_Norma = row[1].Cast<string>(),
                            FechaPublicacion = row[2].Cast<DateTime>(),
                            Ente = row[3].Cast<string>(),
                            Articulo = row[4].Cast<string>(),
                            Descripcion = row[5].Cast<string>(),
                            Sugerencias = row[6].Cast<string>(),
                            Clase_De_Peligro = row[7].Cast<string>(),
                            Peligro = row[8].Cast<string>(),
                            Aspectos = row[9].Cast<string>(),
                            Impactos = row[10].Cast<string>(),
                         
                            FK_Actividad_Economica = 7,

                        }
                        select item).ToList();




            Book.Dispose();
            db.Tbl_Requisitos_Legales_Posipedia.AddRange(resp);
            db.SaveChanges();


        }


        public void LeerExcelTransporte()
        {

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);

            var fileName = "Requisitos_Legales_Data_Integración.xlsx";
            var filePath = Path.Combine(HttpRuntime.AppDomainAppPath, "Content/ArchivosDocumentacion/" + fileName);
            //var filePath = Path.Combine(Server.MapPath("~/Content/ArchivosDocumentacion"), fileName);
            var serverPath = "ftp://ftp_manuelaB@190.26.216.110:990//Requisitos_Legales_Data_Integración.xlsx";
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(serverPath);
            request.KeepAlive = true;
            request.UsePassive = true;
            request.UseBinary = true;
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.Credentials = new NetworkCredential("ftp_manuelaB", "f7hUwGQs");
            request.EnableSsl = true;
            // Read the file from the server & write to destination   
            ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);
            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse()) // Error here
            using (Stream responseStream = response.GetResponseStream())
            //using (StreamReader reader = new StreamReader(responseStream))
            //using (StreamWriter destination = new StreamWriter(filePath))
            {
                int bytesRead = 0;
                byte[] buffer = new byte[2048];
                FileStream fileStream = new FileStream(filePath, FileMode.Create);
                while (true)
                {
                    bytesRead = responseStream.Read(buffer, 0, buffer.Length);

                    if (bytesRead == 0)
                        break;

                    fileStream.Write(buffer, 0, bytesRead);
                }
                fileStream.Close();
            }



            //creamos el libro a partir de la ruta
            var Book = new ExcelQueryFactory(filePath);

            //Consulta con Linq
            var resp = (from row in Book.Worksheet("TRANSPORTE")
                        let item = new RequisitosLegalesPosipedia
                        {

                            Tipo_Norma = row[0].Cast<string>(),
                            Numero_Norma = row[1].Cast<string>(),
                            FechaPublicacion = row[2].Cast<DateTime>(),
                            Ente = row[3].Cast<string>(),
                            Articulo = row[4].Cast<string>(),
                            Descripcion = row[5].Cast<string>(),
                            Sugerencias = row[6].Cast<string>(),
                            Clase_De_Peligro = row[7].Cast<string>(),
                            Peligro = row[8].Cast<string>(),
                            Aspectos = row[9].Cast<string>(),
                            Impactos = row[10].Cast<string>(),
                    
                            FK_Actividad_Economica = 8,

                        }
                        select item).ToList();




            Book.Dispose();
            db.Tbl_Requisitos_Legales_Posipedia.AddRange(resp);
            db.SaveChanges();


        }
     





        public void LeerExcelELECTRICIDADYGAS()
        {

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);

            var fileName = "Requisitos_Legales_Data_Integración.xlsx";
            var filePath = Path.Combine(HttpRuntime.AppDomainAppPath, "Content/ArchivosDocumentacion/" + fileName);
            //var filePath = Path.Combine(Server.MapPath("~/Content/ArchivosDocumentacion"), fileName);
            var serverPath = "ftp://ftp_manuelaB@190.26.216.110:990//Requisitos_Legales_Data_Integración.xlsx";
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(serverPath);
            request.KeepAlive = true;
            request.UsePassive = true;
            request.UseBinary = true;
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.Credentials = new NetworkCredential("ftp_manuelaB", "f7hUwGQs");
            request.EnableSsl = true;
            // Read the file from the server & write to destination   
            ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);
            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse()) // Error here
            using (Stream responseStream = response.GetResponseStream())
            //using (StreamReader reader = new StreamReader(responseStream))
            //using (StreamWriter destination = new StreamWriter(filePath))
            {
                int bytesRead = 0;
                byte[] buffer = new byte[2048];
                FileStream fileStream = new FileStream(filePath, FileMode.Create);
                while (true)
                {
                    bytesRead = responseStream.Read(buffer, 0, buffer.Length);

                    if (bytesRead == 0)
                        break;

                    fileStream.Write(buffer, 0, bytesRead);
                }
                fileStream.Close();
            }



            //creamos el libro a partir de la ruta
            var Book = new ExcelQueryFactory(filePath);

            //Consulta con Linq
            var resp = (from row in Book.Worksheet("ELECTRICIDADYGAS")
                        let item = new RequisitosLegalesPosipedia
                        {

                            Tipo_Norma = row[0].Cast<string>(),
                            Numero_Norma = row[1].Cast<string>(),
                            FechaPublicacion = row[2].Cast<DateTime>(),
                            Ente = row[3].Cast<string>(),
                            Articulo = row[4].Cast<string>(),
                            Descripcion = row[5].Cast<string>(),
                            Sugerencias = row[6].Cast<string>(),
                            Clase_De_Peligro = row[7].Cast<string>(),
                            Peligro = row[8].Cast<string>(),
                            Aspectos = row[9].Cast<string>(),
                            Impactos = row[10].Cast<string>(),
                        
                            FK_Actividad_Economica = 9,

                        }
                        select item).ToList();




            Book.Dispose();
            db.Tbl_Requisitos_Legales_Posipedia.AddRange(resp);
            db.SaveChanges();


        }
     




        public void LeerExcelSANEAMIENTO()
        {

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);

            var fileName = "Requisitos_Legales_Data_Integración.xlsx";
            var filePath = Path.Combine(HttpRuntime.AppDomainAppPath, "Content/ArchivosDocumentacion/" + fileName);
            //var filePath = Path.Combine(Server.MapPath("~/Content/ArchivosDocumentacion"), fileName);
            var serverPath = "ftp://ftp_manuelaB@190.26.216.110:990//Requisitos_Legales_Data_Integración.xlsx";
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(serverPath);
            request.KeepAlive = true;
            request.UsePassive = true;
            request.UseBinary = true;
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.Credentials = new NetworkCredential("ftp_manuelaB", "f7hUwGQs");
            request.EnableSsl = true;
            // Read the file from the server & write to destination   
            ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);
            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse()) // Error here
            using (Stream responseStream = response.GetResponseStream())
            //using (StreamReader reader = new StreamReader(responseStream))
            //using (StreamWriter destination = new StreamWriter(filePath))
            {
                int bytesRead = 0;
                byte[] buffer = new byte[2048];
                FileStream fileStream = new FileStream(filePath, FileMode.Create);
                while (true)
                {
                    bytesRead = responseStream.Read(buffer, 0, buffer.Length);

                    if (bytesRead == 0)
                        break;

                    fileStream.Write(buffer, 0, bytesRead);
                }
                fileStream.Close();
            }



            //creamos el libro a partir de la ruta
            var Book = new ExcelQueryFactory(filePath);

            //Consulta con Linq
            var resp = (from row in Book.Worksheet("SANEAMIENTO")
                        let item = new RequisitosLegalesPosipedia
                        {

                            Tipo_Norma = row[0].Cast<string>(),
                            Numero_Norma = row[1].Cast<string>(),
                            FechaPublicacion = row[2].Cast<DateTime>(),
                            Ente = row[3].Cast<string>(),
                            Articulo = row[4].Cast<string>(),
                            Descripcion = row[5].Cast<string>(),
                            Sugerencias = row[6].Cast<string>(),
                            Clase_De_Peligro = row[7].Cast<string>(),
                            Peligro = row[8].Cast<string>(),
                            Aspectos = row[9].Cast<string>(),
                            Impactos = row[10].Cast<string>(),
                   
                            FK_Actividad_Economica = 10,

                        }
                        select item).ToList();




            Book.Dispose();
            db.Tbl_Requisitos_Legales_Posipedia.AddRange(resp);
            db.SaveChanges();


        }




        public void LeerExcelALOJAMIENTO()
        {

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);

            var fileName = "Requisitos_Legales_Data_Integración.xlsx";
            var filePath = Path.Combine(HttpRuntime.AppDomainAppPath, "Content/ArchivosDocumentacion/" + fileName);
            //var filePath = Path.Combine(Server.MapPath("~/Content/ArchivosDocumentacion"), fileName);
            var serverPath = "ftp://ftp_manuelaB@190.26.216.110:990//Requisitos_Legales_Data_Integración.xlsx";
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(serverPath);
            request.KeepAlive = true;
            request.UsePassive = true;
            request.UseBinary = true;
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.Credentials = new NetworkCredential("ftp_manuelaB", "f7hUwGQs");
            request.EnableSsl = true;
            // Read the file from the server & write to destination   
            ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);
            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse()) // Error here
            using (Stream responseStream = response.GetResponseStream())
            //using (StreamReader reader = new StreamReader(responseStream))
            //using (StreamWriter destination = new StreamWriter(filePath))
            {
                int bytesRead = 0;
                byte[] buffer = new byte[2048];
                FileStream fileStream = new FileStream(filePath, FileMode.Create);
                while (true)
                {
                    bytesRead = responseStream.Read(buffer, 0, buffer.Length);

                    if (bytesRead == 0)
                        break;

                    fileStream.Write(buffer, 0, bytesRead);
                }
                fileStream.Close();
            }



            //creamos el libro a partir de la ruta
            var Book = new ExcelQueryFactory(filePath);

            //Consulta con Linq
            var resp = (from row in Book.Worksheet("ALOJAMIENTO")
                        let item = new RequisitosLegalesPosipedia
                        {

                            Tipo_Norma = row[0].Cast<string>(),
                            Numero_Norma = row[1].Cast<string>(),
                            FechaPublicacion = row[2].Cast<DateTime>(),
                            Ente = row[3].Cast<string>(),
                            Articulo = row[4].Cast<string>(),
                            Descripcion = row[5].Cast<string>(),
                            Sugerencias = row[6].Cast<string>(),
                            Clase_De_Peligro = row[7].Cast<string>(),
                            Peligro = row[8].Cast<string>(),
                            Aspectos = row[9].Cast<string>(),
                            Impactos = row[10].Cast<string>(),
                        
                            FK_Actividad_Economica = 11,

                        }
                        select item).ToList();




            Book.Dispose();
            db.Tbl_Requisitos_Legales_Posipedia.AddRange(resp);
            db.SaveChanges();


        }




        public void LeerExcelCOMUNICACIONES()
        {

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);

            var fileName = "Requisitos_Legales_Data_Integración.xlsx";
            var filePath = Path.Combine(HttpRuntime.AppDomainAppPath, "Content/ArchivosDocumentacion/" + fileName);
            //var filePath = Path.Combine(Server.MapPath("~/Content/ArchivosDocumentacion"), fileName);
            var serverPath = "ftp://ftp_manuelaB@190.26.216.110:990//Requisitos_Legales_Data_Integración.xlsx";
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(serverPath);
            request.KeepAlive = true;
            request.UsePassive = true;
            request.UseBinary = true;
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.Credentials = new NetworkCredential("ftp_manuelaB", "f7hUwGQs");
            request.EnableSsl = true;
            // Read the file from the server & write to destination   
            ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);
            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse()) // Error here
            using (Stream responseStream = response.GetResponseStream())
            //using (StreamReader reader = new StreamReader(responseStream))
            //using (StreamWriter destination = new StreamWriter(filePath))
            {
                int bytesRead = 0;
                byte[] buffer = new byte[2048];
                FileStream fileStream = new FileStream(filePath, FileMode.Create);
                while (true)
                {
                    bytesRead = responseStream.Read(buffer, 0, buffer.Length);

                    if (bytesRead == 0)
                        break;

                    fileStream.Write(buffer, 0, bytesRead);
                }
                fileStream.Close();
            }



            //creamos el libro a partir de la ruta
            var Book = new ExcelQueryFactory(filePath);

            //Consulta con Linq
            var resp = (from row in Book.Worksheet("COMUNICACIONES")
                        let item = new RequisitosLegalesPosipedia
                        {

                            Tipo_Norma = row[0].Cast<string>(),
                            Numero_Norma = row[1].Cast<string>(),
                            FechaPublicacion = row[2].Cast<DateTime>(),
                            Ente = row[3].Cast<string>(),
                            Articulo = row[4].Cast<string>(),
                            Descripcion = row[5].Cast<string>(),
                            Sugerencias = row[6].Cast<string>(),
                            Clase_De_Peligro = row[7].Cast<string>(),
                            Peligro = row[8].Cast<string>(),
                            Aspectos = row[9].Cast<string>(),
                            Impactos = row[10].Cast<string>(),
                         
                            FK_Actividad_Economica = 12,

                        }
                        select item).ToList();




            Book.Dispose();
            db.Tbl_Requisitos_Legales_Posipedia.AddRange(resp);
            db.SaveChanges();


        }


     
        public void LeerExcelActFinancieras()
        {

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);

            var fileName = "Requisitos_Legales_Data_Integración.xlsx";
            var filePath = Path.Combine(HttpRuntime.AppDomainAppPath, "Content/ArchivosDocumentacion/" + fileName);
            //var filePath = Path.Combine(Server.MapPath("~/Content/ArchivosDocumentacion"), fileName);
            var serverPath = "ftp://ftp_manuelaB@190.26.216.110:990//Requisitos_Legales_Data_Integración.xlsx";
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(serverPath);
            request.KeepAlive = true;
            request.UsePassive = true;
            request.UseBinary = true;
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.Credentials = new NetworkCredential("ftp_manuelaB", "f7hUwGQs");
            request.EnableSsl = true;
            // Read the file from the server & write to destination   
            ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);
            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse()) // Error here
            using (Stream responseStream = response.GetResponseStream())
            //using (StreamReader reader = new StreamReader(responseStream))
            //using (StreamWriter destination = new StreamWriter(filePath))
            {
                int bytesRead = 0;
                byte[] buffer = new byte[2048];
                FileStream fileStream = new FileStream(filePath, FileMode.Create);
                while (true)
                {
                    bytesRead = responseStream.Read(buffer, 0, buffer.Length);

                    if (bytesRead == 0)
                        break;

                    fileStream.Write(buffer, 0, bytesRead);
                }
                fileStream.Close();
            }



            //creamos el libro a partir de la ruta
            var Book = new ExcelQueryFactory(filePath);

            //Consulta con Linq
            var resp = (from row in Book.Worksheet("ActividadesFinancieras")
                        let item = new RequisitosLegalesPosipedia
                        {

                            Tipo_Norma = row[0].Cast<string>(),
                            Numero_Norma = row[1].Cast<string>(),
                            FechaPublicacion = row[2].Cast<DateTime>(),
                            Ente = row[3].Cast<string>(),
                            Articulo = row[4].Cast<string>(),
                            Descripcion = row[5].Cast<string>(),
                            Sugerencias = row[6].Cast<string>(),
                            Clase_De_Peligro = row[7].Cast<string>(),
                            Peligro = row[8].Cast<string>(),
                            Aspectos = row[9].Cast<string>(),
                            Impactos = row[10].Cast<string>(),
                       
                            FK_Actividad_Economica = 13,

                        }
                        select item).ToList();




            Book.Dispose();
            db.Tbl_Requisitos_Legales_Posipedia.AddRange(resp);
            db.SaveChanges();


        }
   




      
        public void LeerExcelActsProfesionales()
        {

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);

            var fileName = "Requisitos_Legales_Data_Integración.xlsx";
            var filePath = Path.Combine(HttpRuntime.AppDomainAppPath, "Content/ArchivosDocumentacion/" + fileName);
            //var filePath = Path.Combine(Server.MapPath("~/Content/ArchivosDocumentacion"), fileName);
            var serverPath = "ftp://ftp_manuelaB@190.26.216.110:990//Requisitos_Legales_Data_Integración.xlsx";
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(serverPath);
            request.KeepAlive = true;
            request.UsePassive = true;
            request.UseBinary = true;
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.Credentials = new NetworkCredential("ftp_manuelaB", "f7hUwGQs");
            request.EnableSsl = true;
            // Read the file from the server & write to destination   
            ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);
            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse()) // Error here
            using (Stream responseStream = response.GetResponseStream())
            //using (StreamReader reader = new StreamReader(responseStream))
            //using (StreamWriter destination = new StreamWriter(filePath))
            {
                int bytesRead = 0;
                byte[] buffer = new byte[2048];
                FileStream fileStream = new FileStream(filePath, FileMode.Create);
                while (true)
                {
                    bytesRead = responseStream.Read(buffer, 0, buffer.Length);

                    if (bytesRead == 0)
                        break;

                    fileStream.Write(buffer, 0, bytesRead);
                }
                fileStream.Close();
            }



            //creamos el libro a partir de la ruta
            var Book = new ExcelQueryFactory(filePath);

            //Consulta con Linq
            var resp = (from row in Book.Worksheet("ActividadesProfesionales")
                        let item = new RequisitosLegalesPosipedia
                        {

                            Tipo_Norma = row[0].Cast<string>(),
                            Numero_Norma = row[1].Cast<string>(),
                            FechaPublicacion = row[2].Cast<DateTime>(),
                            Ente = row[3].Cast<string>(),
                            Articulo = row[4].Cast<string>(),
                            Descripcion = row[5].Cast<string>(),
                            Sugerencias = row[6].Cast<string>(),
                            Clase_De_Peligro = row[7].Cast<string>(),
                            Peligro = row[8].Cast<string>(),
                            Aspectos = row[9].Cast<string>(),
                            Impactos = row[10].Cast<string>(),
                          
                            FK_Actividad_Economica = 14,

                        }
                        select item).ToList();




            Book.Dispose();
            db.Tbl_Requisitos_Legales_Posipedia.AddRange(resp);
            db.SaveChanges();


        }
    

  
        public void LeerExcelActsAdministrativas()
        {

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);

            var fileName = "Requisitos_Legales_Data_Integración.xlsx";
            var filePath = Path.Combine(HttpRuntime.AppDomainAppPath, "Content/ArchivosDocumentacion/" + fileName);
            //var filePath = Path.Combine(Server.MapPath("~/Content/ArchivosDocumentacion"), fileName);
            var serverPath = "ftp://ftp_manuelaB@190.26.216.110:990//Requisitos_Legales_Data_Integración.xlsx";
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(serverPath);
            request.KeepAlive = true;
            request.UsePassive = true;
            request.UseBinary = true;
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.Credentials = new NetworkCredential("ftp_manuelaB", "f7hUwGQs");
            request.EnableSsl = true;
            // Read the file from the server & write to destination   
            ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);
            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse()) // Error here
            using (Stream responseStream = response.GetResponseStream())
            //using (StreamReader reader = new StreamReader(responseStream))
            //using (StreamWriter destination = new StreamWriter(filePath))
            {
                int bytesRead = 0;
                byte[] buffer = new byte[2048];
                FileStream fileStream = new FileStream(filePath, FileMode.Create);
                while (true)
                {
                    bytesRead = responseStream.Read(buffer, 0, buffer.Length);

                    if (bytesRead == 0)
                        break;

                    fileStream.Write(buffer, 0, bytesRead);
                }
                fileStream.Close();
            }



            //creamos el libro a partir de la ruta
            var Book = new ExcelQueryFactory(filePath);

            //Consulta con Linq
            var resp = (from row in Book.Worksheet("ActividadesAdministrativas")
                        let item = new RequisitosLegalesPosipedia
                        {

                            Tipo_Norma = row[0].Cast<string>(),
                            Numero_Norma = row[1].Cast<string>(),
                            FechaPublicacion = row[2].Cast<DateTime>(),
                            Ente = row[3].Cast<string>(),
                            Articulo = row[4].Cast<string>(),
                            Descripcion = row[5].Cast<string>(),
                            Sugerencias = row[6].Cast<string>(),
                            Clase_De_Peligro = row[7].Cast<string>(),
                            Peligro = row[8].Cast<string>(),
                            Aspectos = row[9].Cast<string>(),
                            Impactos = row[10].Cast<string>(),
                        
                            FK_Actividad_Economica = 15,

                        }
                        select item).ToList();




            Book.Dispose();
            db.Tbl_Requisitos_Legales_Posipedia.AddRange(resp);
            db.SaveChanges();


        }
     


      
        public void LeerExcelAdministracionpublica()
        {

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);

            var fileName = "Requisitos_Legales_Data_Integración.xlsx";
            var filePath = Path.Combine(HttpRuntime.AppDomainAppPath, "Content/ArchivosDocumentacion/" + fileName);
            //var filePath = Path.Combine(Server.MapPath("~/Content/ArchivosDocumentacion"), fileName);
            var serverPath = "ftp://ftp_manuelaB@190.26.216.110:990//Requisitos_Legales_Data_Integración.xlsx";
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(serverPath);
            request.KeepAlive = true;
            request.UsePassive = true;
            request.UseBinary = true;
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.Credentials = new NetworkCredential("ftp_manuelaB", "f7hUwGQs");
            request.EnableSsl = true;
            // Read the file from the server & write to destination   
            ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);
            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse()) // Error here
            using (Stream responseStream = response.GetResponseStream())
            //using (StreamReader reader = new StreamReader(responseStream))
            //using (StreamWriter destination = new StreamWriter(filePath))
            {
                int bytesRead = 0;
                byte[] buffer = new byte[2048];
                FileStream fileStream = new FileStream(filePath, FileMode.Create);
                while (true)
                {
                    bytesRead = responseStream.Read(buffer, 0, buffer.Length);

                    if (bytesRead == 0)
                        break;

                    fileStream.Write(buffer, 0, bytesRead);
                }
                fileStream.Close();
            }



            //creamos el libro a partir de la ruta
            var Book = new ExcelQueryFactory(filePath);

            //Consulta con Linq
            var resp = (from row in Book.Worksheet("Administracionpublica")
                        let item = new RequisitosLegalesPosipedia
                        {

                            Tipo_Norma = row[0].Cast<string>(),
                            Numero_Norma = row[1].Cast<string>(),
                            FechaPublicacion = row[2].Cast<DateTime>(),
                            Ente = row[3].Cast<string>(),
                            Articulo = row[4].Cast<string>(),
                            Descripcion = row[5].Cast<string>(),
                            Sugerencias = row[6].Cast<string>(),
                            Clase_De_Peligro = row[7].Cast<string>(),
                            Peligro = row[8].Cast<string>(),
                            Aspectos = row[9].Cast<string>(),
                            Impactos = row[10].Cast<string>(),
                           
                            FK_Actividad_Economica = 16,

                        }
                        select item).ToList();




            Book.Dispose();
            db.Tbl_Requisitos_Legales_Posipedia.AddRange(resp);
            db.SaveChanges();


        }
   

        public void LeerExcelEDUCACION()
        {

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);

            var fileName = "Requisitos_Legales_Data_Integración.xlsx";
            var filePath = Path.Combine(HttpRuntime.AppDomainAppPath, "Content/ArchivosDocumentacion/" + fileName);
            //var filePath = Path.Combine(Server.MapPath("~/Content/ArchivosDocumentacion"), fileName);
            var serverPath = "ftp://ftp_manuelaB@190.26.216.110:990//Requisitos_Legales_Data_Integración.xlsx";
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(serverPath);
            request.KeepAlive = true;
            request.UsePassive = true;
            request.UseBinary = true;
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.Credentials = new NetworkCredential("ftp_manuelaB", "f7hUwGQs");
            request.EnableSsl = true;
            // Read the file from the server & write to destination   
            ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);
            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse()) // Error here
            using (Stream responseStream = response.GetResponseStream())
            //using (StreamReader reader = new StreamReader(responseStream))
            //using (StreamWriter destination = new StreamWriter(filePath))
            {
                int bytesRead = 0;
                byte[] buffer = new byte[2048];
                FileStream fileStream = new FileStream(filePath, FileMode.Create);
                while (true)
                {
                    bytesRead = responseStream.Read(buffer, 0, buffer.Length);

                    if (bytesRead == 0)
                        break;

                    fileStream.Write(buffer, 0, bytesRead);
                }
                fileStream.Close();
            }



            //creamos el libro a partir de la ruta
            var Book = new ExcelQueryFactory(filePath);

            //Consulta con Linq
            var resp = (from row in Book.Worksheet("Educacion")
                        let item = new RequisitosLegalesPosipedia
                        {

                            Tipo_Norma = row[0].Cast<string>(),
                            Numero_Norma = row[1].Cast<string>(),
                            FechaPublicacion = row[2].Cast<DateTime>(),
                            Ente = row[3].Cast<string>(),
                            Articulo = row[4].Cast<string>(),
                            Descripcion = row[5].Cast<string>(),
                            Sugerencias = row[6].Cast<string>(),
                            Clase_De_Peligro = row[7].Cast<string>(),
                            Peligro = row[8].Cast<string>(),
                            Aspectos = row[9].Cast<string>(),
                            Impactos = row[10].Cast<string>(),
                       
                            FK_Actividad_Economica = 17,

                        }
                        select item).ToList();




            Book.Dispose();
            db.Tbl_Requisitos_Legales_Posipedia.AddRange(resp);
            db.SaveChanges();


        }




        public void LeerExcelENTRETENIMIENTO()
        {

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);

            var fileName = "Requisitos_Legales_Data_Integración.xlsx";
            var filePath = Path.Combine(HttpRuntime.AppDomainAppPath, "Content/ArchivosDocumentacion/" + fileName);
            //var filePath = Path.Combine(Server.MapPath("~/Content/ArchivosDocumentacion"), fileName);
            var serverPath = "ftp://ftp_manuelaB@190.26.216.110:990//Requisitos_Legales_Data_Integración.xlsx";
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(serverPath);
            request.KeepAlive = true;
            request.UsePassive = true;
            request.UseBinary = true;
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.Credentials = new NetworkCredential("ftp_manuelaB", "f7hUwGQs");
            request.EnableSsl = true;
            // Read the file from the server & write to destination   
            ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);
            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse()) // Error here
            using (Stream responseStream = response.GetResponseStream())
            //using (StreamReader reader = new StreamReader(responseStream))
            //using (StreamWriter destination = new StreamWriter(filePath))
            {
                int bytesRead = 0;
                byte[] buffer = new byte[2048];
                FileStream fileStream = new FileStream(filePath, FileMode.Create);
                while (true)
                {
                    bytesRead = responseStream.Read(buffer, 0, buffer.Length);

                    if (bytesRead == 0)
                        break;

                    fileStream.Write(buffer, 0, bytesRead);
                }
                fileStream.Close();
            }



            //creamos el libro a partir de la ruta
            var Book = new ExcelQueryFactory(filePath);

            //Consulta con Linq
            var resp = (from row in Book.Worksheet("Entretenimiento")
                        let item = new RequisitosLegalesPosipedia
                        {

                            Tipo_Norma = row[0].Cast<string>(),
                            Numero_Norma = row[1].Cast<string>(),
                            FechaPublicacion = row[2].Cast<DateTime>(),
                            Ente = row[3].Cast<string>(),
                            Articulo = row[4].Cast<string>(),
                            Descripcion = row[5].Cast<string>(),
                            Sugerencias = row[6].Cast<string>(),
                            Clase_De_Peligro = row[7].Cast<string>(),
                            Peligro = row[8].Cast<string>(),
                            Aspectos = row[9].Cast<string>(),
                            Impactos = row[10].Cast<string>(),
                         
                            FK_Actividad_Economica = 18,

                        }
                        select item).ToList();




            Book.Dispose();
            db.Tbl_Requisitos_Legales_Posipedia.AddRange(resp);
            db.SaveChanges();


        }


     

        public void LeerExcelOtrasActsdeServicios()
        {

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);

            var fileName = "Requisitos_Legales_Data_Integración.xlsx";
            var filePath = Path.Combine(HttpRuntime.AppDomainAppPath, "Content/ArchivosDocumentacion/" + fileName);
            //var filePath = Path.Combine(Server.MapPath("~/Content/ArchivosDocumentacion"), fileName);
            var serverPath = "ftp://ftp_manuelaB@190.26.216.110:990//Requisitos_Legales_Data_Integración.xlsx";
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(serverPath);
            request.KeepAlive = true;
            request.UsePassive = true;
            request.UseBinary = true;
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.Credentials = new NetworkCredential("ftp_manuelaB", "f7hUwGQs");
            request.EnableSsl = true;
            // Read the file from the server & write to destination   
            ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);
            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse()) // Error here
            using (Stream responseStream = response.GetResponseStream())
            //using (StreamReader reader = new StreamReader(responseStream))
            //using (StreamWriter destination = new StreamWriter(filePath))
            {
                int bytesRead = 0;
                byte[] buffer = new byte[2048];
                FileStream fileStream = new FileStream(filePath, FileMode.Create);
                while (true)
                {
                    bytesRead = responseStream.Read(buffer, 0, buffer.Length);

                    if (bytesRead == 0)
                        break;

                    fileStream.Write(buffer, 0, bytesRead);
                }
                fileStream.Close();
            }



            //creamos el libro a partir de la ruta
            var Book = new ExcelQueryFactory(filePath);

            //Consulta con Linq
            var resp = (from row in Book.Worksheet("OtrasActividadesdeServicios")
                        let item = new RequisitosLegalesPosipedia
                        {

                            Tipo_Norma = row[0].Cast<string>(),
                            Numero_Norma = row[1].Cast<string>(),
                            FechaPublicacion = row[2].Cast<DateTime>(),
                            Ente = row[3].Cast<string>(),
                            Articulo = row[4].Cast<string>(),
                            Descripcion = row[5].Cast<string>(),
                            Sugerencias = row[6].Cast<string>(),
                            Clase_De_Peligro = row[7].Cast<string>(),
                            Peligro = row[8].Cast<string>(),
                            Aspectos = row[9].Cast<string>(),
                            Impactos = row[10].Cast<string>(),
                       
                            FK_Actividad_Economica = 19,

                        }
                        select item).ToList();




            Book.Dispose();
            db.Tbl_Requisitos_Legales_Posipedia.AddRange(resp);
            db.SaveChanges();


        }
        


     
        public void LeerExcelActsdelosHogares()
        {

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);

            var fileName = "Requisitos_Legales_Data_Integración.xlsx";
            var filePath = Path.Combine(HttpRuntime.AppDomainAppPath, "Content/ArchivosDocumentacion/" + fileName);
            //var filePath = Path.Combine(Server.MapPath("~/Content/ArchivosDocumentacion"), fileName);
            var serverPath = "ftp://ftp_manuelaB@190.26.216.110:990//Requisitos_Legales_Data_Integración.xlsx";
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(serverPath);
            request.KeepAlive = true;
            request.UsePassive = true;
            request.UseBinary = true;
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.Credentials = new NetworkCredential("ftp_manuelaB", "f7hUwGQs");
            request.EnableSsl = true;
            // Read the file from the server & write to destination   
            ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);
            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse()) // Error here
            using (Stream responseStream = response.GetResponseStream())
            //using (StreamReader reader = new StreamReader(responseStream))
            //using (StreamWriter destination = new StreamWriter(filePath))
            {
                int bytesRead = 0;
                byte[] buffer = new byte[2048];
                FileStream fileStream = new FileStream(filePath, FileMode.Create);
                while (true)
                {
                    bytesRead = responseStream.Read(buffer, 0, buffer.Length);

                    if (bytesRead == 0)
                        break;

                    fileStream.Write(buffer, 0, bytesRead);
                }
                fileStream.Close();
            }



            //creamos el libro a partir de la ruta
            var Book = new ExcelQueryFactory(filePath);

            //Consulta con Linq
            var resp = (from row in Book.Worksheet("ActividadesdelosHogares")
                        let item = new RequisitosLegalesPosipedia
                        {

                            Tipo_Norma = row[0].Cast<string>(),
                            Numero_Norma = row[1].Cast<string>(),
                            FechaPublicacion = row[2].Cast<DateTime>(),
                            Ente = row[3].Cast<string>(),
                            Articulo = row[4].Cast<string>(),
                            Descripcion = row[5].Cast<string>(),
                            Sugerencias = row[6].Cast<string>(),
                            Clase_De_Peligro = row[7].Cast<string>(),
                            Peligro = row[8].Cast<string>(),
                            Aspectos = row[9].Cast<string>(),
                            Impactos = row[10].Cast<string>(),
                       
                            FK_Actividad_Economica = 20,

                        }
                        select item).ToList();




            Book.Dispose();
            db.Tbl_Requisitos_Legales_Posipedia.AddRange(resp);
            db.SaveChanges();


        }
      


        public void LeerExcelExtraterritorial()
        {

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);

            var fileName = "Requisitos_Legales_Data_Integración.xlsx";
            var filePath = Path.Combine(HttpRuntime.AppDomainAppPath, "Content/ArchivosDocumentacion/" + fileName);
            //var filePath = Path.Combine(Server.MapPath("~/Content/ArchivosDocumentacion"), fileName);
            var serverPath = "ftp://ftp_manuelaB@190.26.216.110:990//Requisitos_Legales_Data_Integración.xlsx";
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(serverPath);
            request.KeepAlive = true;
            request.UsePassive = true;
            request.UseBinary = true;
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.Credentials = new NetworkCredential("ftp_manuelaB", "f7hUwGQs");
            request.EnableSsl = true;
            // Read the file from the server & write to destination   
            ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);
            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse()) // Error here
            using (Stream responseStream = response.GetResponseStream())
            //using (StreamReader reader = new StreamReader(responseStream))
            //using (StreamWriter destination = new StreamWriter(filePath))
            {
                int bytesRead = 0;
                byte[] buffer = new byte[2048];
                FileStream fileStream = new FileStream(filePath, FileMode.Create);
                while (true)
                {
                    bytesRead = responseStream.Read(buffer, 0, buffer.Length);

                    if (bytesRead == 0)
                        break;

                    fileStream.Write(buffer, 0, bytesRead);
                }
                fileStream.Close();
            }



            //creamos el libro a partir de la ruta
            var Book = new ExcelQueryFactory(filePath);

            //Consulta con Linq
            var resp = (from row in Book.Worksheet("Extraterritorial")
                        let item = new RequisitosLegalesPosipedia
                        {

                            Tipo_Norma = row[0].Cast<string>(),
                            Numero_Norma = row[1].Cast<string>(),
                            FechaPublicacion = row[2].Cast<DateTime>(),
                            Ente = row[3].Cast<string>(),
                            Articulo = row[4].Cast<string>(),
                            Descripcion = row[5].Cast<string>(),
                            Sugerencias = row[6].Cast<string>(),
                            Clase_De_Peligro = row[7].Cast<string>(),
                            Peligro = row[8].Cast<string>(),
                            Aspectos = row[9].Cast<string>(),
                            Impactos = row[10].Cast<string>(),
                         
                            FK_Actividad_Economica = 21,

                        }
                        select item).ToList();




            Book.Dispose();
            db.Tbl_Requisitos_Legales_Posipedia.AddRange(resp);
            db.SaveChanges();


        }


        public void LeerExcelTransversal()
        {

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);

            var fileName = "Requisitos_Legales_Data_Integración.xlsx";
            var filePath = Path.Combine(HttpRuntime.AppDomainAppPath, "Content/ArchivosDocumentacion/" + fileName);
            //var filePath = Path.Combine(Server.MapPath("~/Content/ArchivosDocumentacion"), fileName);
            var serverPath = "ftp://ftp_manuelaB@190.26.216.110:990//Requisitos_Legales_Data_Integración.xlsx";
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(serverPath);
            request.KeepAlive = true;
            request.UsePassive = true;
            request.UseBinary = true;
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.Credentials = new NetworkCredential("ftp_manuelaB", "f7hUwGQs");
            request.EnableSsl = true;
            // Read the file from the server & write to destination   
            ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);
            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse()) // Error here
            using (Stream responseStream = response.GetResponseStream())
            //using (StreamReader reader = new StreamReader(responseStream))
            //using (StreamWriter destination = new StreamWriter(filePath))
            {
                int bytesRead = 0;
                byte[] buffer = new byte[2048];
                FileStream fileStream = new FileStream(filePath, FileMode.Create);
                while (true)
                {
                    bytesRead = responseStream.Read(buffer, 0, buffer.Length);

                    if (bytesRead == 0)
                        break;

                    fileStream.Write(buffer, 0, bytesRead);
                }
                fileStream.Close();
            }



            //creamos el libro a partir de la ruta
            var Book = new ExcelQueryFactory(filePath);

            //Consulta con Linq
            var resp = (from row in Book.Worksheet("Transversal")
                        let item = new RequisitosLegalesPosipedia
                        {

                            Tipo_Norma = row[0].Cast<string>(),
                            Numero_Norma = row[1].Cast<string>(),
                            FechaPublicacion = row[2].Cast<DateTime>(),
                            Ente = row[3].Cast<string>(),
                            Articulo = row[4].Cast<string>(),
                            Descripcion = row[5].Cast<string>(),
                            Sugerencias = row[6].Cast<string>(),
                            Clase_De_Peligro = row[7].Cast<string>(),
                            Peligro = row[8].Cast<string>(),
                            Aspectos = row[9].Cast<string>(),
                            Impactos = row[10].Cast<string>(),
                      
                            FK_Actividad_Economica = 22,

                        }
                        select item).ToList();




            Book.Dispose();
            db.Tbl_Requisitos_Legales_Posipedia.AddRange(resp);
            db.SaveChanges();


        }




        public static bool AcceptAllCertifications(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certification, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }



        //--------------------------------------PROBAR
        public ActionResult BusquedaRequisitoLegalTipoNormafechapublicacion(string TipoNorma, DateTime? fechapublicacion,int? FK_ActividadEconomica)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                return View();
            }
            //List<Mod_OtrasInteracciones> ListOtrasinteraciones = db.Tbl_OtrasInteracciones.Where(p => p.FK_Empresa == usuarioActual.IdEmpresa && p.Archivo_OtrasInteracciones.Contains(Busqueda)).ToList();

            //List<RequisitosLegalesPosipedia> listreq = obIReq_Leg.BusquedaRequisitoLegal(TipoNorma);

            obIReq_Leg = new RequisitosLegalesOtrosServicios();
            List<RequisitosLegalesPosipedia> ListOtrasinteraciones = obIReq_Leg.BusquedaRequisitoLegalTipoNormafechapublicacion(TipoNorma, fechapublicacion, FK_ActividadEconomica);

            return PartialView("RequisitosLegalesBusquedaVP", ListOtrasinteraciones);
        }



        public ActionResult BusquedaRequisitoLegalTipoNormaEnteReq(string TipoNorma, string EnteReq,int FK_ActividadEconomica)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                return View();
            }
            //List<Mod_OtrasInteracciones> ListOtrasinteraciones = db.Tbl_OtrasInteracciones.Where(p => p.FK_Empresa == usuarioActual.IdEmpresa && p.Archivo_OtrasInteracciones.Contains(Busqueda)).ToList();

            //List<RequisitosLegalesPosipedia> listreq = obIReq_Leg.BusquedaRequisitoLegal(TipoNorma);

            obIReq_Leg = new RequisitosLegalesOtrosServicios();
            List<RequisitosLegalesPosipedia> ListOtrasinteraciones = obIReq_Leg.BusquedaRequisitoLegalTipoNormaEnteReq(TipoNorma, EnteReq, FK_ActividadEconomica);

            return PartialView("RequisitosLegalesBusquedaVP", ListOtrasinteraciones);
        }


        public ActionResult BusquedaRequisitoLegalTipoNormaDescripcionReq(string TipoNorma, string DescripcionReq, int FK_ActividadEconomica)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                return View();
            }
            //List<Mod_OtrasInteracciones> ListOtrasinteraciones = db.Tbl_OtrasInteracciones.Where(p => p.FK_Empresa == usuarioActual.IdEmpresa && p.Archivo_OtrasInteracciones.Contains(Busqueda)).ToList();

            //List<RequisitosLegalesPosipedia> listreq = obIReq_Leg.BusquedaRequisitoLegal(TipoNorma);

            obIReq_Leg = new RequisitosLegalesOtrosServicios();
            List<RequisitosLegalesPosipedia> ListOtrasinteraciones = obIReq_Leg.BusquedaRequisitoLegalTipoNormaDescripcionReq(TipoNorma, DescripcionReq, FK_ActividadEconomica);

            return PartialView("RequisitosLegalesBusquedaVP", ListOtrasinteraciones);
        }



        public ActionResult BusquedaRequisitoLegalfechapublicacionEnteReq(DateTime fechapublicacion, string EnteReq, int FK_ActividadEconomica)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                return View();
            }
            //List<Mod_OtrasInteracciones> ListOtrasinteraciones = db.Tbl_OtrasInteracciones.Where(p => p.FK_Empresa == usuarioActual.IdEmpresa && p.Archivo_OtrasInteracciones.Contains(Busqueda)).ToList();

            //List<RequisitosLegalesPosipedia> listreq = obIReq_Leg.BusquedaRequisitoLegal(TipoNorma);

            obIReq_Leg = new RequisitosLegalesOtrosServicios();
            List<RequisitosLegalesPosipedia> ListOtrasinteraciones = obIReq_Leg.BusquedaRequisitoLegalfechapublicacionEnteReq(fechapublicacion, EnteReq, FK_ActividadEconomica);

            return PartialView("RequisitosLegalesBusquedaVP", ListOtrasinteraciones);
        }




        public ActionResult BusquedaRequisitoLegalEnteReqDescripcionReq(string EnteReq, string DescripcionReq, int FK_ActividadEconomica)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                return View();
            }
            //List<Mod_OtrasInteracciones> ListOtrasinteraciones = db.Tbl_OtrasInteracciones.Where(p => p.FK_Empresa == usuarioActual.IdEmpresa && p.Archivo_OtrasInteracciones.Contains(Busqueda)).ToList();

            //List<RequisitosLegalesPosipedia> listreq = obIReq_Leg.BusquedaRequisitoLegal(TipoNorma);

            obIReq_Leg = new RequisitosLegalesOtrosServicios();
            List<RequisitosLegalesPosipedia> ListOtrasinteraciones = obIReq_Leg.BusquedaRequisitoLegalEnteReqDescripcionReq(EnteReq, DescripcionReq, FK_ActividadEconomica);

            return PartialView("RequisitosLegalesBusquedaVP", ListOtrasinteraciones);
        }


        public ActionResult BusquedaRequisitoLegalTipoNorma(string TipoNorma, int FK_ActividadEconomica)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                return View();
            }
            //List<Mod_OtrasInteracciones> ListOtrasinteraciones = db.Tbl_OtrasInteracciones.Where(p => p.FK_Empresa == usuarioActual.IdEmpresa && p.Archivo_OtrasInteracciones.Contains(Busqueda)).ToList();

            //List<RequisitosLegalesPosipedia> listreq = obIReq_Leg.BusquedaRequisitoLegal(TipoNorma);

            obIReq_Leg = new RequisitosLegalesOtrosServicios();
            List<RequisitosLegalesPosipedia> ListOtrasinteraciones = obIReq_Leg.BusquedaRequisitoLegal(TipoNorma, FK_ActividadEconomica);

            return PartialView("RequisitosLegalesBusquedaVP", ListOtrasinteraciones);
        }


        public ActionResult BusquedaRequisitoLegalfechapublicacion(DateTime fechapublicacion, int FK_ActividadEconomica)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                return View();
            }
            //List<Mod_OtrasInteracciones> ListOtrasinteraciones = db.Tbl_OtrasInteracciones.Where(p => p.FK_Empresa == usuarioActual.IdEmpresa && p.Archivo_OtrasInteracciones.Contains(Busqueda)).ToList();

            //List<RequisitosLegalesPosipedia> listreq = obIReq_Leg.BusquedaRequisitoLegal(TipoNorma);

            obIReq_Leg = new RequisitosLegalesOtrosServicios();
            List<RequisitosLegalesPosipedia> ListOtrasinteraciones = obIReq_Leg.BusquedaRequisitoLegalfechapublicacion(fechapublicacion, FK_ActividadEconomica);

            return PartialView("RequisitosLegalesBusquedaVP", ListOtrasinteraciones);
        }


        public ActionResult BusquedaRequisitoLegalEnteReq(string EnteReq, int FK_ActividadEconomica)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                return View();
            }
            //List<Mod_OtrasInteracciones> ListOtrasinteraciones = db.Tbl_OtrasInteracciones.Where(p => p.FK_Empresa == usuarioActual.IdEmpresa && p.Archivo_OtrasInteracciones.Contains(Busqueda)).ToList();

            //List<RequisitosLegalesPosipedia> listreq = obIReq_Leg.BusquedaRequisitoLegal(TipoNorma);

            obIReq_Leg = new RequisitosLegalesOtrosServicios();
            List<RequisitosLegalesPosipedia> ListOtrasinteraciones = obIReq_Leg.BusquedaRequisitoLegalEnteReq(EnteReq, FK_ActividadEconomica);

            return PartialView("RequisitosLegalesBusquedaVP", ListOtrasinteraciones);
        }


        public ActionResult BusquedaRequisitoLegalDescripcionReq(string DescripcionReq, int FK_ActividadEconomica)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                return View();
            }
            //List<Mod_OtrasInteracciones> ListOtrasinteraciones = db.Tbl_OtrasInteracciones.Where(p => p.FK_Empresa == usuarioActual.IdEmpresa && p.Archivo_OtrasInteracciones.Contains(Busqueda)).ToList();

            //List<RequisitosLegalesPosipedia> listreq = obIReq_Leg.BusquedaRequisitoLegal(TipoNorma);

            obIReq_Leg = new RequisitosLegalesOtrosServicios();
            List<RequisitosLegalesPosipedia> ListOtrasinteraciones = obIReq_Leg.BusquedaRequisitoLegalDescripcionReq(DescripcionReq, FK_ActividadEconomica);

            return PartialView("RequisitosLegalesBusquedaVP", ListOtrasinteraciones);
        }





        public ActionResult BusquedaRequisitoLegalfechapublicacionDescripcionReq(DateTime fechapublicacion, string DescripcionReq,int FK_ActividadEconomica)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                return View();
            }
            //List<Mod_OtrasInteracciones> ListOtrasinteraciones = db.Tbl_OtrasInteracciones.Where(p => p.FK_Empresa == usuarioActual.IdEmpresa && p.Archivo_OtrasInteracciones.Contains(Busqueda)).ToList();

            //List<RequisitosLegalesPosipedia> listreq = obIReq_Leg.BusquedaRequisitoLegal(TipoNorma);

            obIReq_Leg = new RequisitosLegalesOtrosServicios();
            List<RequisitosLegalesPosipedia> ListOtrasinteraciones = obIReq_Leg.BusquedaRequisitoLegalfechapublicacionDescripcionReq(fechapublicacion,DescripcionReq,FK_ActividadEconomica);

            return PartialView("RequisitosLegalesBusquedaVP", ListOtrasinteraciones);
        }

        //verificar
        public ActionResult BusquedaRequisitoLegalTipoNormafechapublicacionEnteReq(string TipoNorma, DateTime fechapublicacion, string EnteReq ,int FK_ActividadEconomica)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                return View();
            }
            //List<Mod_OtrasInteracciones> ListOtrasinteraciones = db.Tbl_OtrasInteracciones.Where(p => p.FK_Empresa == usuarioActual.IdEmpresa && p.Archivo_OtrasInteracciones.Contains(Busqueda)).ToList();

            //List<RequisitosLegalesPosipedia> listreq = obIReq_Leg.BusquedaRequisitoLegal(TipoNorma);

            obIReq_Leg = new RequisitosLegalesOtrosServicios();
            List<RequisitosLegalesPosipedia> ListOtrasinteraciones = obIReq_Leg.BusquedaRequisitoLegalTipoNormafechapublicacionEnteReq(TipoNorma, fechapublicacion, EnteReq, FK_ActividadEconomica);

            return PartialView("RequisitosLegalesBusquedaVP", ListOtrasinteraciones);
        }




        //verificar
        public ActionResult BusquedaRequisitoLegalTipoNormaEnteReqDescripcionReq(string TipoNorma, string EnteReq, string DescripcionReq, int FK_ActividadEconomica)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                return View();
            }
            //List<Mod_OtrasInteracciones> ListOtrasinteraciones = db.Tbl_OtrasInteracciones.Where(p => p.FK_Empresa == usuarioActual.IdEmpresa && p.Archivo_OtrasInteracciones.Contains(Busqueda)).ToList();

            //List<RequisitosLegalesPosipedia> listreq = obIReq_Leg.BusquedaRequisitoLegal(TipoNorma);

            obIReq_Leg = new RequisitosLegalesOtrosServicios();
            List<RequisitosLegalesPosipedia> ListOtrasinteraciones = obIReq_Leg.BusquedaRequisitoLegalTipoNormaEnteReqDescripcionReq(TipoNorma, EnteReq, DescripcionReq, FK_ActividadEconomica);

            return PartialView("RequisitosLegalesBusquedaVP", ListOtrasinteraciones);
        }





                    //verificar
        public ActionResult BusquedaRequisitoLegalTipoNormafechapublicacionDescripcionReq(string TipoNorma, DateTime fechapublicacion, string DescripcionReq, int FK_ActividadEconomica)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                return View();
            }
            //List<Mod_OtrasInteracciones> ListOtrasinteraciones = db.Tbl_OtrasInteracciones.Where(p => p.FK_Empresa == usuarioActual.IdEmpresa && p.Archivo_OtrasInteracciones.Contains(Busqueda)).ToList();

            //List<RequisitosLegalesPosipedia> listreq = obIReq_Leg.BusquedaRequisitoLegal(TipoNorma);

            obIReq_Leg = new RequisitosLegalesOtrosServicios();
            List<RequisitosLegalesPosipedia> ListOtrasinteraciones = obIReq_Leg.BusquedaRequisitoLegalTipoNormafechapublicacionDescripcionReq(TipoNorma, fechapublicacion, DescripcionReq, FK_ActividadEconomica);

            return PartialView("RequisitosLegalesBusquedaVP", ListOtrasinteraciones);
        }



        public ActionResult BusquedaRequisitoLegalfechapublicacionEnteReqDescripcionReq(DateTime fechapublicacion, string EnteReq, string DescripcionReq, int FK_ActividadEconomica)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                return View();
            }
            //List<Mod_OtrasInteracciones> ListOtrasinteraciones = db.Tbl_OtrasInteracciones.Where(p => p.FK_Empresa == usuarioActual.IdEmpresa && p.Archivo_OtrasInteracciones.Contains(Busqueda)).ToList();

            //List<RequisitosLegalesPosipedia> listreq = obIReq_Leg.BusquedaRequisitoLegal(TipoNorma);

            obIReq_Leg = new RequisitosLegalesOtrosServicios();
            List<RequisitosLegalesPosipedia> ListOtrasinteraciones = obIReq_Leg.BusquedaRequisitoLegalfechapublicacionEnteReqDescripcionReq(fechapublicacion, EnteReq, DescripcionReq, FK_ActividadEconomica);

            return PartialView("RequisitosLegalesBusquedaVP", ListOtrasinteraciones);
        }





        public ActionResult BusquedaRequisitoLegalTipoNormafechapublicacionEnteReqDescripcionReq(string TipoNorma, DateTime fechapublicacion, string EnteReq, string DescripcionReq, int FK_ActividadEconomica)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                return View();
            }
            //List<Mod_OtrasInteracciones> ListOtrasinteraciones = db.Tbl_OtrasInteracciones.Where(p => p.FK_Empresa == usuarioActual.IdEmpresa && p.Archivo_OtrasInteracciones.Contains(Busqueda)).ToList();

            //List<RequisitosLegalesPosipedia> listreq = obIReq_Leg.BusquedaRequisitoLegal(TipoNorma);

            obIReq_Leg = new RequisitosLegalesOtrosServicios();
            List<RequisitosLegalesPosipedia> ListOtrasinteraciones = obIReq_Leg.BusquedaRequisitoLegalTipoNormafechapublicacionEnteReqDescripcionReq(TipoNorma, fechapublicacion, EnteReq, DescripcionReq, FK_ActividadEconomica);

            return PartialView("RequisitosLegalesBusquedaVP", ListOtrasinteraciones);
        }







         /// <summary>
         /// con este metodo se guardan los requisitos seleccionados para agrgarlos a la matriz seleccionada
         /// </summary>
         /// <param name="customerIDs"></param>
         /// <param name="idactividadeconomica"></param>
         /// <returns></returns>
         public ActionResult AgregarNuevosRegistrosTablaRequisitos(Int32[] customerIDs, int PK_Matriz, int FK_ActividadEconomica)
         {
             var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);

             foreach (Int32 customerID in customerIDs)
             {
                 RequisitosLegalesPosipedia modreq = db.Tbl_Requisitos_Legales_Posipedia.Find((customerID));

                 RequisitosLegalesOtros objre = new RequisitosLegalesOtros();
                 objre.Tipo_Norma = modreq.Tipo_Norma;
                 objre.Numero_Norma = modreq.Numero_Norma;
                 objre.FechaPublicacion = modreq.FechaPublicacion;
                 objre.Ente = modreq.Ente;
                 objre.Articulo = modreq.Articulo;
                 objre.Descripcion = modreq.Descripcion;
                 objre.Sugerencias = modreq.Sugerencias;
                 objre.Clase_De_Peligro = modreq.Clase_De_Peligro;
                 objre.Peligro = modreq.Peligro;
                 objre.Aspectos = modreq.Aspectos;
                 objre.Impactos = modreq.Impactos;
                 objre.Evidencia_Cumplimiento = "";
                 objre.FK_Cumplimiento_Evaluacion = 4;
                 objre.Hallazgo = "";
                 objre.FK_Estado_RequisitoslegalesOtros = 4;
                 objre.Responsable = "";
                 objre.Fecha_Seguimiento_Control = DateTime.Now;
                 objre.Fecha_Actualizacion = DateTime.Now;
                 //objre.FK_Empresa = usuarioActual.IdEmpresa;
                 objre.FK_Actividad_Economica = FK_ActividadEconomica; //CORREGIR********************************

                 string var = varnombrematyriz;

                 obIReq_Leg = new RequisitosLegalesOtrosServicios();
                 obIReq_Leg.GuardarNuevoRequisitos_SeleccionadosMatriz(objre, PK_Matriz); //se relacionan los requisitos con la matriz - SE GUARDA EN tblRequisitosMatriz
             
             
             }

             return Json(new { success = true }
              , JsonRequestBehavior.AllowGet);


             //ViewBag.Messages2 = "Requisito legal agregado con éxito";
             //List<MatrizRequisitosLegales> Objlist_RequisitosLegales = new List<MatrizRequisitosLegales>();
             //return View("ListadoMastrices", Objlist_RequisitosLegales = db.Tbl_Matriz_RequisitosLegales.Where(s => s.FK_Empresa == usuarioActual.IdEmpresa).ToList());


           

         }




        //vista para agregar un nuevo requisito legal a la matriz creada
         public ActionResult AgregarRegistroMatrizCreada(int FKActividadEconomica)
         {
             var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
             if (usuarioActual == null)
             {
                 ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                 return View();
             }
             ViewBag.FK_Cumplimiento_Evaluacion = new SelectList(db.Tbl_Cumplimiento_Evaluacion, "PK_Cumplimiento_Evaluacion", "Descripcion_Cumplimiento_Evaluacion");

             ViewBag.FK_Estado_RequisitoslegalesOtros = new SelectList(db.Tbl_Estado_RequisitoslegalesOtros, "PK_Estado_RequisitoslegalesOtros", "Descripcion_Estado_RequisitoslegalesOtros");

             ViewBag.FKActividadEconomica = FKActividadEconomica;


             static_ActEconomica = FKActividadEconomica;

             return View();
         }






         public ActionResult GrabarRequisitosLegalesOtros_MatrizCreada(RequisitosLegalesOtros objreq)
         {
             var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
             if (usuarioActual == null)
             {
                 ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                 return View();
             }
             //objreq.FK_Empresa = usuarioActual.IdEmpresa;
             objreq.Evidencia_Cumplimiento = "";
             objreq.FK_Cumplimiento_Evaluacion = 3;
             objreq.Hallazgo = "";
             objreq.FK_Estado_RequisitoslegalesOtros = 3;
             objreq.Responsable = "";
             objreq.Fecha_Seguimiento_Control = DateTime.Now;
             objreq.Fecha_Actualizacion = DateTime.Now;
            // objreq.FK_Empresa = usuarioActual.IdEmpresa;
             objreq.FK_Actividad_Economica = 1;

         

             
             obIReq_Leg = new RequisitosLegalesOtrosServicios();




             //obIReq_Leg.GrabarRequisitosLegalesOtros(objreq);
             obIReq_Leg = null;
             ViewBag.Messages2 = "Registro guardado satisfactoriamente";
             return View("AgregarNuevoRequisitoLegal");
         }





         public ActionResult prueba( )
         {
           return View();
         }





         //vista para agregar un nuevo requisito legal a la matriz creada
         public ActionResult ModificarRequisitoMatriz(RequisitosLegalesOtros objre)
         {
             var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
             if (usuarioActual == null)
             {
                 ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                 return View();
             }
             ViewBag.FK_Cumplimiento_Evaluacion = new SelectList(db.Tbl_Cumplimiento_Evaluacion.Where(x => x.Descripcion_Cumplimiento_Evaluacion.Length>0), "PK_Cumplimiento_Evaluacion", "Descripcion_Cumplimiento_Evaluacion");

             ViewBag.FK_Estado_RequisitoslegalesOtros = new SelectList(db.Tbl_Estado_RequisitoslegalesOtros.Where(x => x.Descripcion_Estado_RequisitoslegalesOtros.Length > 0), "PK_Estado_RequisitoslegalesOtros", "Descripcion_Estado_RequisitoslegalesOtros");


             return View(objre);
         }






        

         public ActionResult Editar_RequisitosLegalesOtrosMatriz(int id,int FKActividadEconomica)
         {
             var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
             if (usuarioActual == null)
             {
                 ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                 return View();
             }
             obIReq_Leg = new RequisitosLegalesOtrosServicios();
             RequisitosLegalesOtros objre = obIReq_Leg.BuscarRequisitosLegalesOtros(id);


             ViewBag.FK_Cumplimiento_Evaluacion = new SelectList(db.Tbl_Cumplimiento_Evaluacion.Where(x => x.Descripcion_Cumplimiento_Evaluacion.Length > 0), "PK_Cumplimiento_Evaluacion", "Descripcion_Cumplimiento_Evaluacion", objre.FK_Cumplimiento_Evaluacion);

             ViewBag.FK_Estado_RequisitoslegalesOtros = new SelectList(db.Tbl_Estado_RequisitoslegalesOtros.Where(x => x.Descripcion_Estado_RequisitoslegalesOtros.Length > 0), "PK_Estado_RequisitoslegalesOtros", "Descripcion_Estado_RequisitoslegalesOtros", objre.FK_Estado_RequisitoslegalesOtros);
             


             ViewBag.PK_RequisitosLegalesOtros = objre.PK_RequisitosLegalesOtros;

             ViewBag.fechaseguimientocontrol = objre.Fecha_Seguimiento_Control;

             ViewBag.FKActividadEconomica = FKActividadEconomica;


             return View("ModificarRequisitoMatriz", objre);
         }




        /// <summary>
        /// metodo que se utilizar para grabar la edicion del requisto legal de la matriz (agregar los demas datos que no trae posipedia)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="objreq"></param>
        /// <returns></returns>

         public ActionResult GrabarRequisitosLegalesOtros(RequisitosLegalesOtros objreq)
         {
             var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
             if (usuarioActual == null)
             {
                 ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                 return View();
             }
             //objreq.FK_Empresa = usuarioActual.IdEmpresa;
             //objreq.PK_RequisitosLegalesOtros = varid;
             objreq.FK_Actividad_Economica = 1;
             obIReq_Leg = new RequisitosLegalesOtrosServicios();
             obIReq_Leg.ModficarRequisitosLegalesOtros(objreq);
             obIReq_Leg = null;
             ViewBag.Messages2 = "Registro guardado satisfactoriamente";
             List<MatrizRequisitosLegales> Objlist_RequisitosLegales = new List<MatrizRequisitosLegales>();
             return View("ListadoMastrices",Objlist_RequisitosLegales = db.Tbl_Matriz_RequisitosLegales.Where(s => s.FK_Empresa == usuarioActual.IdEmpresa).ToList());

          
             

         }



         public ActionResult ModificarRequisitosLegalesOtros_Matriz(RequisitosLegalesOtros objreq)
         {
             var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
             if (usuarioActual == null)
             {
                 ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                 return View();
             }          
             
             //objreq.FK_Empresa = usuarioActual.IdEmpresa;
             obIReq_Leg = new RequisitosLegalesOtrosServicios();
             obIReq_Leg.ModficarRequisitosLegalesOtros(objreq);

             obIReq_Leg = null;
             ViewBag.Messages2 = "Registro Modificado con éxito";
            

             return View("ListadoMastrices",db.Tbl_Requisitos_Matriz.GroupBy(x => x.FK_MatrizRequisitosLegales).Select(x => x.FirstOrDefault()));//lista para mostrar los archivos cargados
         }



  

         public ActionResult GrabarRequisitosLegalesOtros_Formulario(RequisitosLegalesOtros objreqformulario)
         {    
    
              var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
             if (usuarioActual == null)
             {
                 ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                 return View("AgregarRegistroMatrizCreada");
             }

             obIReq_Leg = new RequisitosLegalesOtrosServicios();
             objreqformulario.FK_Actividad_Economica = static_ActEconomica;
        

             obIReq_Leg.GrabarRequisitosLegalesOtros_Formulario(objreqformulario, Static_fk_matriz);
             Static_fk_matriz = 0;
             static_ActEconomica = 0;
             //List<MatrizRequisitosLegales> Objlist_RequisitosLegales = new List<MatrizRequisitosLegales>();
             //return View(Objlist_RequisitosLegales = db.Tbl_Matriz_RequisitosLegales.Where(s => s.FK_Empresa == usuarioActual.IdEmpresa).ToList());



             ViewBag.Messages2 = "Requisito legal agregado satisfactoriamente";           


             //List<MatrizRequisitosLegales> Objlist_RequisitosLegales = new List<MatrizRequisitosLegales>();
             //return View("ListadoMastrices", Objlist_RequisitosLegales = db.Tbl_Matriz_RequisitosLegales.Where(s => s.FK_Empresa == usuarioActual.IdEmpresa).ToList());       

        
             List<MatrizRequisitosLegales> Objlist_RequisitosLegales = new List<MatrizRequisitosLegales>();
             return View("ListadoMastrices", Objlist_RequisitosLegales = db.Tbl_Matriz_RequisitosLegales.Where(s => s.FK_Empresa == usuarioActual.IdEmpresa).ToList());


             }





        /// <summary>
        /// metodo para eliminar los requistos legales de la matriz
        /// </summary>
        /// <param name="PKMatriz"></param>
        /// <returns></returns>
             public ActionResult Eliminar_RequisitosLegalesOtros(Int32[] PK_RequisitosLegales)
             {
                 var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
                 if (usuarioActual == null)
                 {
                     ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                     return View();
                 }

                 try
                 {
                     foreach (Int32 PKRequisitosLegales in PK_RequisitosLegales)
                     {

                         obIReq_Leg = new RequisitosLegalesOtrosServicios();
                         obIReq_Leg.Eliminar_ReqLegalesOtros(PKRequisitosLegales);

                         // RequisitosLegalesOtros objreq = db.Tbl_Requisitos_legales_Otros.Find(customerID);
                         //db.Tbl_Requisitos_legales_Otros.Remove(objreq);               

                     }
                     db.SaveChanges();

                     //return RedirectToAction("Index");
                     return Json(new { success = true }
                         , JsonRequestBehavior.AllowGet);
                 }
                 catch (Exception)
                 {
                     ViewBag.Messages = "Error";
                     return RedirectToAction("Index");
                 }


             }



    


      

             public ActionResult ExportarExcelRequisitosLegales(int FK_MatrizRequisitosLegales)
             {
                 var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
                 if (usuarioActual == null)
                 {
                     ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                     return View("AgregarRegistroMatrizCreada");
                 }


                 SG_SSTContext entities = new SG_SSTContext();
                 DataTable dt = new DataTable("Requisitos Legales Otros");

                 try
                 {
                     dt.Columns.AddRange(new DataColumn[]{ 
                                            new DataColumn("Tipo Norma"),
                                            new DataColumn("N° Norma"),
                                            new DataColumn("Fecha Publicación"),
                                            new DataColumn("Ente"),
                                            new DataColumn("Artículos"),
                                            new DataColumn("Descripción"),
                                            new DataColumn("Sugerencias"),
                                            new DataColumn("Clase Peligro"),
                                            new DataColumn("Peligro"),
                                            new DataColumn("Aspectos"),
                                            new DataColumn("Impactos"),                                 
                                            new DataColumn("Evidencia de cumplimiento"),
                                            new DataColumn("Cumplimiento"),
                                            new DataColumn("Hallazgo"),
                                            new DataColumn("Estado"),
                                            new DataColumn("Responsable"),
                                            new DataColumn("Fecha de seguimiento del control"),
                                            new DataColumn("Fecha de actualizacion")             

                                          });

                     //var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
                     var procesos = ExportarExcel_RequiitosMatriz(FK_MatrizRequisitosLegales);            
                     
                     foreach (var proceso in procesos)
                     {                 

                             dt.Rows.Add(                             
                                 proceso.TipoNorma.ToUpper(),  
                                 proceso.NumeroNorma.ToUpper(),
                                 proceso.FechaPublicacionReq.ToShortDateString(),
                                 proceso.EnteReq.ToUpper(),
                                 proceso.ArticuloReq.ToUpper(),
                                 proceso.DescripcionReq.ToUpper(),
                                 proceso.SugerenciasReq.ToUpper(),
                                 proceso.ClaseDePeligro.ToUpper(),
                                 proceso.PeligroReq.ToUpper(),
                                 proceso.AspectosReq.ToUpper(),
                                 proceso.ImpactosReq.ToUpper(),
                                 proceso.EvidenciaCumplimiento.ToUpper(),
                                 proceso.Descripcion_Cumplimiento_Evaluacion.ToUpper(),
                                 proceso.HallazgoReq.ToUpper(),
                                 proceso.Descripcion_Estado_RequisitoslegalesOtros.ToUpper(),
                                 proceso.ResponsableReq.ToUpper(),
                                 proceso.Fecha_Seguimiento_ControlReq.ToShortDateString(),
                                 proceso.Fecha_ActualizacionReq.ToShortDateString()                             

                                 );                        
    }

                     using (XLWorkbook wb = new XLWorkbook())
                     {
                         wb.Worksheets.Add(dt);

                         using (MemoryStream stream = new MemoryStream())
                         {
                             wb.SaveAs(stream);
                             return File(stream.ToArray(),
                                 "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                                 "ReporteRequisitosLegalesOtros.xlsx");
                         }
                     }
                 }
                 catch (Exception e)
                 {

                     ViewBag.MensajeError = "No se puede descargar, por favor intente más tarde";
                     List<MatrizRequisitosLegales> Objlist_RequisitosLegales = new List<MatrizRequisitosLegales>();
                     return View("ListadoMastrices", Objlist_RequisitosLegales = db.Tbl_Matriz_RequisitosLegales.Where(s => s.FK_Empresa == usuarioActual.IdEmpresa).ToList());
                 }
             }




             /// <summary>
             /// metodo que permite mostrar los requisitos legales de la matriz seleccionada
             /// </summary>
             /// <param name="objm"></param>
             /// <returns></returns>
             public List<EDRequisitosLegalesOtros> ExportarExcel_RequiitosMatriz(int FK_MatrizRequisitosLegales)
             {
                 var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
                 if (usuarioActual == null)
                 {
                     ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                     //return View();
                 }
               
                 List<EDRequisitosLegalesOtros> varmatreq = new List<EDRequisitosLegalesOtros>();

                 using (SG_SSTContext context = new SG_SSTContext())
                 {
                     var Varmatreq = (from a in context.Tbl_Requisitos_Matriz
                                      join b in context.Tbl_Requisitos_legales_Otros on
                                      a.FK_RequisitosLegalesOtros equals b.PK_RequisitosLegalesOtros
                                      join e in context.Tbl_Matriz_RequisitosLegales on
                                      a.FK_MatrizRequisitosLegales equals e.PK_MatrizRequisitosLegales

                                      join c in context.Tbl_Estado_RequisitoslegalesOtros on
                                      b.FK_Estado_RequisitoslegalesOtros equals c.PK_Estado_RequisitoslegalesOtros
                                      join d in context.Tbl_Cumplimiento_Evaluacion on
                                      b.FK_Cumplimiento_Evaluacion equals d.PK_Cumplimiento_Evaluacion

                                      where a.FK_MatrizRequisitosLegales == FK_MatrizRequisitosLegales && e.FK_Empresa == usuarioActual.IdEmpresa
                                 
                                      select new EDRequisitosLegalesOtros()
                                      {
                                          PKRequisitoLegal = b.PK_RequisitosLegalesOtros,
                                          TipoNorma = b.Tipo_Norma,
                                          NumeroNorma = b.Numero_Norma,
                                          FechaPublicacionReq = b.FechaPublicacion,
                                          EnteReq = b.Ente,
                                          ArticuloReq = b.Articulo,
                                          DescripcionReq = b.Descripcion,
                                          SugerenciasReq = b.Sugerencias,
                                          ClaseDePeligro = b.Clase_De_Peligro,
                                          PeligroReq = b.Peligro,
                                          AspectosReq = b.Aspectos,
                                          ImpactosReq = b.Impactos,
                                          EvidenciaCumplimiento = b.Evidencia_Cumplimiento,
                                          Descripcion_Cumplimiento_Evaluacion = d.Descripcion_Cumplimiento_Evaluacion,
                                          HallazgoReq = b.Hallazgo,
                                          Descripcion_Estado_RequisitoslegalesOtros = c.Descripcion_Estado_RequisitoslegalesOtros,
                                          ResponsableReq = b.Responsable,
                                          Fecha_Seguimiento_ControlReq = b.Fecha_Seguimiento_Control,
                                          Fecha_ActualizacionReq = b.Fecha_Actualizacion
                                          //FK_ActividadEconomica 
                                      }).ToList();
                     return Varmatreq;
                 }

             } 




             /// <summary>
             /// metodo para eliminar los archivos seleccionados con el checkbox - ajax jquery - llama al metodo: EliminararchivoOtrasInteracciones_servidor, para eliminar fisicamente los archivos q se encuentran en la carpeta del servidor
             /// </summary>
             /// <param name="customerIDs"></param>
             /// <returns></returns>
             public ActionResult EliminarMatriz(Int32[] PKMatriz)
             {             

                 var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
                 if (usuarioActual == null)
                 {
                     ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                     return View();
                 }

                 try
                 {
                     foreach (Int32 PK_Matriz in PKMatriz)
                     {
                         obIReq_Leg = new RequisitosLegalesOtrosServicios();
                         var = obIReq_Leg.EliminarMatrices(PK_Matriz);                              

                     }
                     db.SaveChanges();

                     if (var == true)
                     {
                         //return RedirectToAction("Index");
                         return Json(new { success = true }
                             , JsonRequestBehavior.AllowGet);
                     }
                     else {
                         //return RedirectToAction("Index");
                         return Json(new { success = false }
                             , JsonRequestBehavior.AllowGet);                   
                     
                     }
                
                 }
                 catch (Exception)
                 {
                     ViewBag.Messages = "Error";
                     return RedirectToAction("Index");
                 }

             }








    }
}





