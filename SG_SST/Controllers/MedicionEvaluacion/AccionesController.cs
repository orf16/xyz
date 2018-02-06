using RestSharp;
using SG_SST.Controllers.Base;
using SG_SST.EntidadesDominio.MedicionEvaluacion;
using SG_SST.Logica.MedicionEvaluacion;
using SG_SST.Models.Ausentismo;
using SG_SST.Models.Empleado;
using SG_SST.Models.MedicionEvaluacion;
using SG_SST.Models.Organizacion;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SG_SST.EntidadesDominio.Empresas;
using SG_SST.Logica.Empresas;
using SG_SST.EntidadesDominio.Empleado;
using System.Threading.Tasks;
using SG_SST.Dtos.Empresas;

namespace SG_SST.Controllers.MedicionEvaluacion
{
    public class AccionesController : BaseController
    {

        LNAcciones LNAcciones = new LNAcciones();
        LNProcesos LNProcesos = new LNProcesos();
        LNEmpresa LNEmpresa = new LNEmpresa();
        private static string RutaFirmas = "~/Content/ArchivosAccionesCP/FirmasAcciones/";
        private static string RutaArchivosBD = "~/Content/ArchivosAccionesCP/ArchivosAcciones/";
        private static string RutaArchivosTemporales = "~/Content/ArchivosAccionesCP/ArchivosTemporales/";
        private static string SrcWhite = "data:image/png;base64,R0lGODlhAQABAAAAACH5BAEKAAEALAAAAAABAAEAAAICTAEAOw==";
        
        public void inicializar()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
            }
            var EDAccion = new EDAccion();
            if (Session["EDAccion"] == null)
            {
                DateTime dateToday = DateTime.Today;
                EDAccion.HallazgoLista = new List<EDHallazgo>();
                EDAccion.ActividadLista = new List<EDActividad>();
                EDAccion.SeguimientoLista = new List<EDSeguimiento>();
                EDAccion.AnalisisLista = new List<EDAnalisis>();
                EDAccion.ArchivosLista = new List<EDArchivosAcciones>();
                EDAccion.Fecha_ocurrencia = dateToday;
                Session["EDAccion"] = EDAccion;
            }
        }
        public void inicializarED(string Id, int IdEmpresa)
        {
            //var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            //if (usuarioActual == null)
            //{
            //    ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
            //}
            var EditarAccion = new EDAccion();
            if (TempData["EditarAccion" + Id] == null)
            {
                DateTime dateToday = DateTime.Today;
                EditarAccion.HallazgoLista = new List<EDHallazgo>();
                EditarAccion.ActividadLista = new List<EDActividad>();
                EditarAccion.SeguimientoLista = new List<EDSeguimiento>();
                EditarAccion.AnalisisLista = new List<EDAnalisis>();
                EditarAccion.ArchivosLista = new List<EDArchivosAcciones>();
                EditarAccion.Fecha_dil = dateToday;
                EditarAccion.Fecha_hall = dateToday;
                EditarAccion.Fecha_ocurrencia = dateToday;
                //Consultar Accion del repositorio
                int IdAccion = 0;
                if (int.TryParse(Id, out IdAccion))
                {
                    EditarAccion = LNAcciones.ConsultaAccion(IdAccion, IdEmpresa);
                    if (EditarAccion.Pk_Id_Accion != 0)
                    {
                        try
                        {
                            string ImagenBase64 = UrlToBase64(Server.MapPath(Path.Combine(EditarAccion.RutaArchivoAuditor, EditarAccion.NombreArchivoAuditor)));
                            EditarAccion.FirmaScrImageAud = "data:image/png;base64," + ImagenBase64;
                        }
                        catch (Exception)
                        {
                        }
                        try
                        {
                            string ImagenBase64 = UrlToBase64(Server.MapPath(Path.Combine(EditarAccion.RutaArchivoResp, EditarAccion.NombreArchivoResp)));
                            EditarAccion.FirmaScrImageRes = "data:image/png;base64," + ImagenBase64;
                        }
                        catch (Exception)
                        {
                        }
                        foreach (var item in EditarAccion.ActividadLista)
                        {
                            try
                            {
                                string ImagenBase64 = UrlToBase64(Server.MapPath(Path.Combine(item.RutaArchivoAct, item.NombreArchivoAct)));
                                item.FirmaScrImage = "data:image/png;base64," + ImagenBase64;
                            }
                            catch (Exception)
                            {
                            }
                        }
                        foreach (var item in EditarAccion.SeguimientoLista)
                        {
                            try
                            {
                                string ImagenBase64 = UrlToBase64(Server.MapPath(Path.Combine(item.RutaArchivoSeg, item.NombreArchivoSeg)));
                                item.FirmaScrImage = "data:image/png;base64," + ImagenBase64;
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }
                }
                TempData["EditarAccion" + Id] = EditarAccion;
            }
        }
        private EDAccion inicializarEDPDF(string Id, int idEmpresa)
        {
            var EditarAccion = new EDAccion();
            DateTime dateToday = DateTime.Today;
            EditarAccion.HallazgoLista = new List<EDHallazgo>();
            EditarAccion.ActividadLista = new List<EDActividad>();
            EditarAccion.SeguimientoLista = new List<EDSeguimiento>();
            EditarAccion.AnalisisLista = new List<EDAnalisis>();
            EditarAccion.ArchivosLista = new List<EDArchivosAcciones>();
            EditarAccion.Fecha_dil = dateToday;
            EditarAccion.Fecha_hall = dateToday;
            EditarAccion.Fecha_ocurrencia = dateToday;
            //Consultar Accion del repositorio
            int IdAccion = 0;
            if (int.TryParse(Id, out IdAccion))
            {
                EditarAccion = LNAcciones.ConsultaAccion(IdAccion, idEmpresa);
                if (EditarAccion.Pk_Id_Accion != 0)
                {
                    try
                    {
                        string ImagenBase64 = UrlToBase64(Server.MapPath(Path.Combine(EditarAccion.RutaArchivoAuditor, EditarAccion.NombreArchivoAuditor)));
                        EditarAccion.FirmaScrImageAud = "data:image/png;base64," + ImagenBase64;
                    }
                    catch (Exception)
                    {
                    }
                    try
                    {
                        string ImagenBase64 = UrlToBase64(Server.MapPath(Path.Combine(EditarAccion.RutaArchivoResp, EditarAccion.NombreArchivoResp)));
                        EditarAccion.FirmaScrImageRes = "data:image/png;base64," + ImagenBase64;
                    }
                    catch (Exception)
                    {
                    }
                    foreach (var item in EditarAccion.ActividadLista)
                    {
                        try
                        {
                            string ImagenBase64 = UrlToBase64(Server.MapPath(Path.Combine(item.RutaArchivoAct, item.NombreArchivoAct)));
                            item.FirmaScrImage = "data:image/png;base64," + ImagenBase64;
                        }
                        catch (Exception)
                        {
                        }
                    }
                    foreach (var item in EditarAccion.SeguimientoLista)
                    {
                        try
                        {
                            string ImagenBase64 = UrlToBase64(Server.MapPath(Path.Combine(item.RutaArchivoSeg, item.NombreArchivoSeg)));
                            item.FirmaScrImage = "data:image/png;base64," + ImagenBase64;
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
            }
            return EditarAccion;
        }
        public ActionResult Index()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión en el sistema";
                return View();
            }
            return View();
        }
        [HttpGet]
        public ActionResult NuevaAccion()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }
            inicializar();
            var AccionSession = Session["EDAccion"] as EDAccion;

            var ListaSedes = new List<EDSede>();
            ListaSedes = LNEmpresa.ObtenerSedesPorNit(usuarioActual.NitEmpresa);
            int index_sede = 0;
            if (ListaSedes.Count > 0)
            {
                bool trysede = int.TryParse(AccionSession.Halla_Sede, out index_sede);
            }

            List<EDCargo> ListaEDCargo = new List<EDCargo>();
            ListaEDCargo = LNAcciones.ListaCargos();

            ViewBag.IdAccion = LNAcciones.NuevoNumeroACP(usuarioActual.IdEmpresa);
            ViewBag.Pk_Id_Sede = new SelectList(ListaSedes, "IdSede", "NombreSede", index_sede);
            ViewBag.Pk_Id_Cargo = new SelectList(ListaEDCargo, "IDCargo", "NombreCargo", 0);

            bool[] ListaboolOrigen = new bool[9];
            string ValorOrigen = AccionSession.Origen;
            int valorIntOrigen = -1;
            if (ValorOrigen!=null)
            {
                if (ValorOrigen == "Incidente")
                {
                    valorIntOrigen = 0;
                }
                if (ValorOrigen == "Accidente")
                {
                    valorIntOrigen = 1;
                }
                if (ValorOrigen == "Auditoría Interna")
                {
                    valorIntOrigen = 2;
                }
                if (ValorOrigen == "Auditoría Externa")
                {
                    valorIntOrigen = 3;
                }
                if (ValorOrigen == "Inspección")
                {
                    valorIntOrigen = 4;
                }
                if (ValorOrigen == "Programa Gestión del Cambio")
                {
                    valorIntOrigen = 5;
                }
                if (ValorOrigen == "Actos y condiciones Inseguras")
                {
                    valorIntOrigen = 6;
                }
                if (ValorOrigen == "Revisión General")
                {
                    valorIntOrigen = 7;
                }
                if (ValorOrigen == "Otros")
                {
                    valorIntOrigen = 8;
                }
            }
            
            for (int i = 0; i < 9; i++)
            {
                ListaboolOrigen[i] = false;
                if (valorIntOrigen==i)
                {
                    ListaboolOrigen[i] = true;
                }
            }
            

            List<SelectListItem> ListaOrigen = new List<SelectListItem>();
            ListaOrigen.Add(new SelectListItem { Text = "Incidente", Value = "Incidente", Selected = ListaboolOrigen[0] });
            ListaOrigen.Add(new SelectListItem { Text = "Accidente", Value = "Accidente", Selected = ListaboolOrigen[1] });
            ListaOrigen.Add(new SelectListItem { Text = "Auditoría Interna", Value = "Auditoría Interna", Selected = ListaboolOrigen[2] });
            ListaOrigen.Add(new SelectListItem { Text = "Auditoría Externa", Value = "Auditoría Externa", Selected = ListaboolOrigen[3] });
            ListaOrigen.Add(new SelectListItem { Text = "Inspección", Value = "Inspección", Selected = ListaboolOrigen[4] });
            ListaOrigen.Add(new SelectListItem { Text = "Programa Gestión del Cambio", Value = "Programa Gestión del Cambio", Selected = ListaboolOrigen[5] });
            ListaOrigen.Add(new SelectListItem { Text = "Actos y condiciones Inseguras", Value = "Actos y condiciones Inseguras", Selected = ListaboolOrigen[6] });
            ListaOrigen.Add(new SelectListItem { Text = "Revisión General", Value = "Revisión General", Selected = ListaboolOrigen[7] });
            ListaOrigen.Add(new SelectListItem { Text = "Otros", Value = "Otros", Selected = ListaboolOrigen[8] });

            ViewBag.ListaOrigen = ListaOrigen;

            if (TempData["AnalisisArbol"] != null)
            {
                TempData.Remove("AnalisisArbol");
            }
            if (TempData["AnalisisCausa"] != null)
            {
                TempData.Remove("AnalisisCausa");
            }
            if (TempData["Analisis5porques"] != null)
            {
                TempData.Remove("Analisis5porques");
            }
            if (TempData["AnalisisLluvia"] != null)
            {
                TempData.Remove("AnalisisLluvia");
            }
            if (TempData["FirmaAct"] != null)
            {
                TempData.Remove("FirmaAct");
            }
            if (TempData["FirmaSeg"] != null)
            {
                TempData.Remove("FirmaSeg");
            }
            // Cargar Imagen Auditor
            ViewBag.SrcImgAud = SrcWhite;
            if (AccionSession.FirmaScrImageAud != null)
            {
                if (AccionSession.FirmaScrImageAud != "")
                {
                    ViewBag.SrcImgAud = AccionSession.FirmaScrImageAud;
                }
            }
            // Cargar Imagen Responsable
            ViewBag.SrcImgRes = SrcWhite;
            if (AccionSession.FirmaScrImageRes != null)
            {
                if (AccionSession.FirmaScrImageRes != "")
                {
                    ViewBag.SrcImgRes = AccionSession.FirmaScrImageRes;
                }
            }
            return View(AccionSession);
        }
        [HttpPost]
        public JsonResult CancelarNuevaAccion()
        {
            if (Session["EDAccion"] != null)
            {
                var AccionSession = Session["EDAccion"] as EDAccion;
                if (AccionSession.ArchivosLista.Count > 0)
                {
                    var ListaArchivos = AccionSession.ArchivosLista;
                    foreach (var item in ListaArchivos)
                    {
                        string NombreArchivo = item.NombreArchivo;
                        string RutaArchivo = item.Ruta;
                        try
                        {
                            System.IO.File.Delete(Server.MapPath(Path.Combine(RutaArchivo, NombreArchivo)));
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
                Session.Remove("EDAccion");
            }
            return Json(new { url = Url.Action("ConsultaACAP", "Acciones") },
            JsonRequestBehavior.AllowGet);
        }
        #region Hallazgos
        [HttpGet]
        public ActionResult NuevoHallazgo(string Edicion)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }
            if (Edicion != null)
            {
                TempData.Keep(Edicion);
                ViewBag.EdicionKey = Edicion;
            }
            else
            {
                inicializar();
                ViewBag.EdicionKey = "";
            }
            List<EDProceso> procesos = LNProcesos.ObtenerProcesosPorEmpresa(usuarioActual.NitEmpresa);
            procesos = procesos.Where(s => s.Id_Proceso_Padre != null).ToList();
            ViewBag.Pk_Id_Proceso = new SelectList(procesos, "Id_Proceso", "Descripcion");
            return View();
        }
        [HttpPost]
        public ActionResult NuevoHallazgo(Hallazgo Hallazgo, FormCollection frm)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                if (frm["Pk_Id_Proceso"] != null)
                {
                    int Id_Proceso = 0;
                    bool isNumeric = int.TryParse(frm["Pk_Id_Proceso"].ToString(), out Id_Proceso);
                    if (isNumeric == true)
                    {
                        List<EDProceso> Listaprocesos = LNProcesos.ObtenerProcesosPorEmpresa(usuarioActual.NitEmpresa);
                        EDProceso ProcesoBuscado = Listaprocesos.Find(s => s.Id_Proceso == Id_Proceso);
                        Hallazgo.Halla_Proceso = ProcesoBuscado.Descripcion.ToString();
                        Hallazgo.Fk_Id_Proceso = Id_Proceso;
                    }
                }

                EDHallazgo EDHallazgo = new EDHallazgo();
                EDHallazgo.Halla_Descripcion = Hallazgo.Halla_Descripcion;
                EDHallazgo.Halla_Norma = Hallazgo.Halla_Norma;
                EDHallazgo.Halla_Numeral = Hallazgo.Halla_Numeral;
                EDHallazgo.Halla_Proceso = Hallazgo.Halla_Proceso;
                EDHallazgo.Fk_Id_Proceso = Hallazgo.Fk_Id_Proceso;

                if (frm["EdicionKey"] != "")
                {
                    //Nuevo Hallazgo - Editar Accion
                    string IdEdicion = frm["EdicionKey"];
                    var AccionSession = TempData[IdEdicion] as EDAccion;
                    int IdAccion = AccionSession.Pk_Id_Accion;
                    List<EDHallazgo> ListaHallazgo = new List<EDHallazgo>();
                    ListaHallazgo = AccionSession.HallazgoLista;
                    int IdAct = 0;
                    bool Existe = true;
                    while (Existe)
                    {
                        Existe = false;
                        string Clave = RandomString(8);
                        bool probarInt = int.TryParse(Clave, out IdAct);
                        foreach (var item in ListaHallazgo)
                        {
                            if (item.Clave == IdAct)
                            {
                                Existe = true;
                            }
                        }
                    }
                    EDHallazgo.Clave = IdAct;
                    AccionSession.HallazgoLista.Add(EDHallazgo);
                    return RedirectToAction("EditarAccion", "Acciones", new { id = IdAccion.ToString() });
                }
                else
                {
                    //Nuevo Hallazgo - Nueva Accion
                    inicializar();
                    var AccionSession = Session["EDAccion"] as EDAccion;
                    List<EDHallazgo> ListaHallazgo = new List<EDHallazgo>();
                    ListaHallazgo = AccionSession.HallazgoLista;
                    int IdAct = 0;
                    bool Existe = true;
                    while (Existe)
                    {
                        Existe = false;
                        string Clave = RandomString(8);
                        bool probarInt = int.TryParse(Clave, out IdAct);
                        foreach (var item in ListaHallazgo)
                        {
                            if (item.Clave == IdAct)
                            {
                                Existe = true;
                            }
                        }
                    }
                    EDHallazgo.Clave = IdAct;
                    AccionSession.HallazgoLista.Add(EDHallazgo);
                    return RedirectToAction("NuevaAccion", "Acciones");
                }
            }
            if (frm["EdicionKey"] != "")
            {
                string IdEdicion = frm["EdicionKey"];
                if (IdEdicion != null)
                {
                    TempData.Keep(IdEdicion);
                    ViewBag.EdicionKey = IdEdicion;
                }
                else
                {
                    ViewBag.EdicionKey = "";
                }
            }
            List<EDProceso> EDprocesos = LNProcesos.ObtenerProcesosPorEmpresa(usuarioActual.NitEmpresa);
            EDprocesos = EDprocesos.Where(s => s.Id_Proceso_Padre != null).ToList();
            ViewBag.Pk_Id_Proceso = new SelectList(EDprocesos, "Id_Proceso", "Descripcion");
            return View(Hallazgo);
        }
        [HttpPost]
        public JsonResult EliminarHallazgo(string Values)
        {
            bool probar = false;
            string resultado = "El hallazgo no se ha eliminado. Puede que el hallazgo no exista, por favor consulte si existe este hallazgo y vuelva a intentar";
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                resultado = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
            }
            int IdAct = 0;
            bool probarNumero = int.TryParse(Values, out IdAct);
            if (IdAct != 0)
            {
                var AccionSession = Session["EDAccion"] as EDAccion;
                List<EDHallazgo> ListaHallazgo = new List<EDHallazgo>();
                ListaHallazgo = AccionSession.HallazgoLista;
                ListaHallazgo.RemoveAll(s => s.Clave == IdAct);
                AccionSession.HallazgoLista = ListaHallazgo;
                Session["EDAccion"] = AccionSession;
                probar = true;
                resultado = "El hallazgo se ha eliminado correctamente";
                return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult EliminarHallazgoEd(string Values, string id)
        {
            bool probar = false;
            string resultado = "El hallazgo no se ha eliminado. Puede que el hallazgo no exista, por favor consulte si existe este hallazgo y vuelva a intentar";
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                resultado = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
            }
            int IdAct = 0;
            bool probarNumero = int.TryParse(Values, out IdAct);
            if (IdAct != 0)
            {
                var AccionSession = TempData[id] as EDAccion;
                List<EDHallazgo> ListaHallazgo = new List<EDHallazgo>();
                ListaHallazgo = AccionSession.HallazgoLista;
                ListaHallazgo.RemoveAll(s => s.Clave == IdAct);
                AccionSession.HallazgoLista = ListaHallazgo;
                TempData[id] = AccionSession;
                probar = true;
                resultado = "El hallazgo se ha eliminado correctamente";
                return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult EditarHallazgo(string id, string Edicion)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }
            List<EDHallazgo> ListaHallazgo = new List<EDHallazgo>();
            int Id_hall = 0;
            bool probar = int.TryParse(id, out Id_hall);
            if (Edicion != null)
            {
                TempData.Keep(Edicion);
                ViewBag.EdicionKey = Edicion;
                var AccionSession = TempData[Edicion] as EDAccion;
                ListaHallazgo = AccionSession.HallazgoLista;
            }
            else
            {
                ViewBag.EdicionKey = "";
                inicializar();
                var AccionSession = Session["EDAccion"] as EDAccion;
                ListaHallazgo = AccionSession.HallazgoLista;
            }
            EDHallazgo RegresarAct = ListaHallazgo.Find(s => s.Clave == Id_hall);
            if (RegresarAct == null)
            {
                return HttpNotFound();
            }
            List<EDProceso> ListaProceso = LNProcesos.ObtenerProcesosPorEmpresa(usuarioActual.NitEmpresa);
            ListaProceso = ListaProceso.Where(s => s.Id_Proceso_Padre != null).ToList();
            int index_Proceso = RegresarAct.Fk_Id_Proceso;
            EDProceso ProcesoBuscado = ListaProceso.Find(s => s.Id_Proceso == index_Proceso);

            if (ProcesoBuscado != null)
            {
                ViewBag.Pk_Id_Proceso = new SelectList(ListaProceso, "Id_Proceso", "Descripcion", index_Proceso);
            }
            else
            {
                ViewBag.Pk_Id_Proceso = new SelectList(ListaProceso, "Id_Proceso", "Descripcion", 0);
            }
            Hallazgo HallazgoEncontrado = new Hallazgo();
            HallazgoEncontrado.Pk_Id_Hallazgo = RegresarAct.Pk_Id_Hallazgo;
            HallazgoEncontrado.Halla_Norma = RegresarAct.Halla_Norma;
            HallazgoEncontrado.Halla_Numeral = RegresarAct.Halla_Numeral;
            HallazgoEncontrado.Halla_Descripcion = RegresarAct.Halla_Descripcion;
            HallazgoEncontrado.Halla_Proceso = RegresarAct.Halla_Proceso;
            HallazgoEncontrado.Fk_Id_Accion = RegresarAct.Fk_Id_Accion;
            HallazgoEncontrado.Fk_Id_Proceso = RegresarAct.Fk_Id_Proceso;
            ViewBag.Clave = RegresarAct.Clave;
            return View(HallazgoEncontrado);
        }
        [HttpPost]
        public ActionResult EditarHallazgo(FormCollection frm, Hallazgo Coleccion)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }
            string str_Clave = frm["Clave"].ToString();
            int Id_Clave = 0;
            bool probar = int.TryParse(str_Clave, out Id_Clave);
            if (ModelState.IsValid)
            {
                List<EDHallazgo> ListaHallazgos = new List<EDHallazgo>();
                if (frm["EdicionKey"] != "")
                {
                    //Editar Hallazgo-Editar Accion
                    string IdEdicion = frm["EdicionKey"];
                    var AccionSession = TempData[IdEdicion] as EDAccion;
                    int IdAccion = AccionSession.Pk_Id_Accion;
                    ListaHallazgos = AccionSession.HallazgoLista;
                    foreach (var item in ListaHallazgos.Where(c => c.Clave == Id_Clave))
                    {
                        if (frm["Pk_Id_Proceso"] != null)
                        {
                            int Id_Proceso = 0;
                            bool isNumeric = int.TryParse(frm["Pk_Id_Proceso"].ToString(), out Id_Proceso);
                            if (isNumeric == true)
                            {
                                List<EDProceso> procesos = LNProcesos.ObtenerProcesosPorEmpresa(usuarioActual.NitEmpresa);
                                procesos = procesos.Where(s => s.Id_Proceso_Padre != null).ToList();
                                EDProceso Proceso = procesos.Find(s => s.Id_Proceso == Id_Proceso);

                                item.Halla_Proceso = Proceso.Descripcion;
                                item.Fk_Id_Proceso = Id_Proceso;
                            }
                        }
                        item.Halla_Descripcion = Coleccion.Halla_Descripcion;
                        item.Halla_Norma = Coleccion.Halla_Norma;
                        item.Halla_Numeral = Coleccion.Halla_Numeral;
                    }
                    AccionSession.HallazgoLista = ListaHallazgos;
                    TempData[IdEdicion] = AccionSession;
                    return RedirectToAction("EditarAccion", "Acciones", new { id = IdAccion.ToString() });
                }
                else
                {
                    //Editar Hallazgo-Nueva Accion
                    inicializar();
                    var AccionSession = Session["EDAccion"] as EDAccion;
                    ListaHallazgos = AccionSession.HallazgoLista;
                    foreach (var item in ListaHallazgos.Where(c => c.Clave == Id_Clave))
                    {
                        if (frm["Pk_Id_Proceso"] != null)
                        {
                            int Id_Proceso = 0;
                            bool isNumeric = int.TryParse(frm["Pk_Id_Proceso"].ToString(), out Id_Proceso);
                            if (isNumeric == true)
                            {
                                List<EDProceso> procesos = LNProcesos.ObtenerProcesosPorEmpresa(usuarioActual.NitEmpresa);
                                procesos = procesos.Where(s => s.Id_Proceso_Padre != null).ToList();
                                EDProceso Proceso = procesos.Find(s => s.Id_Proceso == Id_Proceso);
                                item.Halla_Proceso = Proceso.Descripcion;
                                item.Fk_Id_Proceso = Id_Proceso;
                            }
                        }
                        item.Halla_Descripcion = Coleccion.Halla_Descripcion;
                        item.Halla_Norma = Coleccion.Halla_Norma;
                        item.Halla_Numeral = Coleccion.Halla_Numeral;
                    }
                    AccionSession.HallazgoLista = ListaHallazgos;
                    Session["EDAccion"] = AccionSession;
                    return RedirectToAction("NuevaAccion", "Acciones");
                }
            }
            int index_Proceso = 0;
            if (frm["Pk_Id_Proceso"] != null)
            {
                int Id_Proceso = 0;
                bool isNumeric = int.TryParse(frm["Pk_Id_Proceso"].ToString(), out Id_Proceso);
                if (isNumeric == true)
                {
                    index_Proceso = Id_Proceso;
                }
            }

            if (frm["EdicionKey"] != "")
            {
                string Edicion = frm["EdicionKey"];
                TempData.Keep(Edicion);
                ViewBag.EdicionKey = Edicion;
            }
            else
            {
                ViewBag.EdicionKey = "";
            }
            List<EDProceso> ListaProceso = new List<EDProceso>();
            ListaProceso = LNProcesos.ObtenerProcesosPorEmpresa(usuarioActual.NitEmpresa);
            ListaProceso = ListaProceso.Where(s => s.Id_Proceso_Padre != null).ToList();
            EDProceso ProcesoBuscado = ListaProceso.Find(s => s.Id_Proceso == index_Proceso);
            if (ProcesoBuscado != null)
            {
                ViewBag.Pk_Id_Proceso = new SelectList(ListaProceso, "Id_Proceso", "Descripcion", index_Proceso);
            }
            else
            {
                ViewBag.Pk_Id_Proceso = new SelectList(ListaProceso, "Id_Proceso", "Descripcion", 0);
            }
            ViewBag.Clave = str_Clave;
            return View(Coleccion);
        }
        #endregion
        #region JsonBuscarPersonayformulario
        [HttpPost]
        public JsonResult GuardarFormulario(List<String> values)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {

                return Json(JsonRequestBehavior.AllowGet);
            }
            var values1 = values;
            inicializar();
            var EDAccion = Session["EDAccion"] as EDAccion;

            if (values1[0].ToString() != "null")
            {
                EDAccion.Tipo = values1[0].ToString();
            }
            if (values1[2].ToString() != "null")
            {
                EDAccion.Clase = values1[2].ToString();
            }
            if (values1[11].ToString() != "null")
            {
                EDAccion.Cambio_Doc = values1[11].ToString();
            }
            if (values1[14].ToString() != "null")
            {
                EDAccion.Eficacia = values1[14].ToString();
            }

            EDAccion.Halla_Num_Doc = values1[4].ToString();
            EDAccion.Halla_Nombre = values1[6].ToString();
            EDAccion.Halla_TipoDoc = values1[5].ToString();
            EDAccion.Correccion = values1[9].ToString();
            EDAccion.Causa_Raiz = values1[10].ToString();
            EDAccion.Des_Cambio_Doc = values1[12].ToString();
            EDAccion.Verificacion = values1[13].ToString();
            EDAccion.Nombre_Auditor = values1[15].ToString();
            EDAccion.Cargo_Auditor = values1[16].ToString();
            EDAccion.Nombre_Responsable = values1[17].ToString();
            EDAccion.Cargo_Responsable = values1[18].ToString();
            DateTime dt = DateTime.MinValue;
            try
            {
                dt = DateTime.ParseExact(values1[1].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                if (dt != DateTime.MinValue)
                {
                    EDAccion.Fecha_dil = dt;
                }
            }
            catch (Exception)
            {
                try
                {
                    dt = DateTime.ParseExact(values1[1].ToString(), "yyyy/MM/dd", CultureInfo.InvariantCulture);
                    if (dt != DateTime.MinValue)
                    {
                        EDAccion.Fecha_dil = dt;
                    }
                }
                catch (Exception)
                {

                }
            }

            dt = DateTime.MinValue;
            try
            {
                dt = DateTime.ParseExact(values1[3].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                if (dt != DateTime.MinValue)
                {
                    EDAccion.Fecha_hall = dt;
                }
            }
            catch (Exception)
            {
                try
                {
                    dt = DateTime.ParseExact(values1[3].ToString(), "yyyy/MM/dd", CultureInfo.InvariantCulture);
                    if (dt != DateTime.MinValue)
                    {
                        EDAccion.Fecha_hall = dt;
                    }
                }
                catch (Exception)
                {

                }
            }
            EDAccion.Halla_Sede = values1[8].ToString();
            EDAccion.Halla_Cargo = values1[7].ToString();
            //origen
            EDAccion.Origen = values1[19].ToString();
            EDAccion.Otro_Origen = values1[20].ToString();


            return Json(new { Result = String.Format("") },
                JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GuardarFormularioEd(List<String> values)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return Json(JsonRequestBehavior.AllowGet);
            }
            var values1 = values;
            bool probar = true;
            var EDAccion = TempData[values[19]] as EDAccion;

            if (EDAccion != null)
            {
                TempData.Keep(values[19]);
                if (values1[0].ToString() != "null")
                {
                    EDAccion.Tipo = values1[0].ToString();
                }
                if (values1[2].ToString() != "null")
                {
                    EDAccion.Clase = values1[2].ToString();
                }
                if (values1[11].ToString() != "null")
                {
                    EDAccion.Cambio_Doc = values1[11].ToString();
                }
                if (values1[14].ToString() != "null")
                {
                    EDAccion.Eficacia = values1[14].ToString();
                }
                EDAccion.Halla_Num_Doc = values1[4].ToString();
                EDAccion.Halla_Nombre = values1[6].ToString();
                EDAccion.Halla_TipoDoc = values1[5].ToString();
                EDAccion.Correccion = values1[9].ToString();
                EDAccion.Causa_Raiz = values1[10].ToString();
                EDAccion.Des_Cambio_Doc = values1[12].ToString();
                EDAccion.Verificacion = values1[13].ToString();
                EDAccion.Nombre_Auditor = values1[15].ToString();
                EDAccion.Cargo_Auditor = values1[16].ToString();
                EDAccion.Nombre_Responsable = values1[17].ToString();
                EDAccion.Cargo_Responsable = values1[18].ToString();
                DateTime dt = DateTime.MinValue;
                try
                {
                    dt = DateTime.ParseExact(values1[1].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    if (dt != DateTime.MinValue)
                    {
                        EDAccion.Fecha_dil = dt;
                    }
                }
                catch (Exception)
                {
                    try
                    {
                        dt = DateTime.ParseExact(values1[1].ToString(), "yyyy/MM/dd", CultureInfo.InvariantCulture);
                        if (dt != DateTime.MinValue)
                        {
                            EDAccion.Fecha_dil = dt;
                        }
                    }
                    catch (Exception)
                    {
                    }
                }

                dt = DateTime.MinValue;
                try
                {
                    dt = DateTime.ParseExact(values1[3].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    if (dt != DateTime.MinValue)
                    {
                        EDAccion.Fecha_hall = dt;
                    }
                }
                catch (Exception)
                {
                    try
                    {
                        dt = DateTime.ParseExact(values1[3].ToString(), "yyyy/MM/dd", CultureInfo.InvariantCulture);
                        if (dt != DateTime.MinValue)
                        {
                            EDAccion.Fecha_hall = dt;
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
                EDAccion.Halla_Sede = values1[8].ToString();
                EDAccion.Halla_Cargo = values1[7].ToString();

                EDAccion.Origen = values1[20].ToString();
                EDAccion.Otro_Origen = values1[21].ToString();
                TempData[values[19]] = EDAccion;
            }
            return Json(new { probar },
                JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult BuscarPersonaDocumento(string documento)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return Json(JsonRequestBehavior.AllowGet);
            }
            string nit = usuarioActual.NitEmpresa;
            List<EDTipoDocumento> EDTipoDoc = LNAcciones.BuscarTipoDocumento();
            List<TipoDocumento> ListaDocumentos = new List<TipoDocumento>();
            List<string> ListaDocumentosStr = new List<string>();
            List<string> ListaDocumentosTipoStr = new List<string>();
            foreach (var item in EDTipoDoc)
            {
                TipoDocumento td = new TipoDocumento();
                td.PK_IDTipo_Documento = item.Id_Tipo_Documento;
                td.Sigla = item.Sigla;
                td.Descripcion = item.Descripcion;
                ListaDocumentos.Add(td);
                ListaDocumentosTipoStr.Add(td.Descripcion);
                ListaDocumentosStr.Add(td.Sigla);
            }

            //ListaDocumentos = ListaDocumentos.Where(s => s.AplicaPersonas == true).ToList();
            string Nit = usuarioActual.NitEmpresa;
            string[] resultado = new string[3] { string.Empty, string.Empty, string.Empty };
            bool probar = false;

            try
            {
                int cont = 0;
                foreach (var item in ListaDocumentosStr)
                {
                    if (!string.IsNullOrEmpty(documento) && !string.IsNullOrEmpty(item))
                    {
                        var cliente = new RestSharp.RestClient(ConfigurationManager.AppSettings["Url"]);
                        var request = new RestRequest(ConfigurationManager.AppSettings["consultaAfiliado"], RestSharp.Method.GET);
                        request.RequestFormat = DataFormat.Xml;
                        request.Parameters.Clear();
                        request.AddParameter("tpDoc", item.ToString());
                        request.AddParameter("doc", documento);
                        request.AddHeader("Content-Type", "application/json");
                        request.AddHeader("Accept", "application/json");

                        //se omite la validación de certificado de SSL
                        ServicePointManager.ServerCertificateValidationCallback = delegate
                        { return true; };
                        IRestResponse<List<AfiliadoModel>> response = cliente.Execute<List<AfiliadoModel>>(request);
                        var result = response.Content;
                        if (!string.IsNullOrWhiteSpace(result))
                        {
                            var respuesta = Newtonsoft.Json.JsonConvert.DeserializeObject<List<AfiliadoModel>>(result);
                            var afiliado = respuesta.Where(a => a.Estado == "Activo").FirstOrDefault();
                            if (afiliado == null)
                            { }
                            else
                            {
                                if (nit == afiliado.IdEmpresa)
                                {
                                    resultado[0] = afiliado.Nombre1 + " " + afiliado.Nombre2 + " " + afiliado.Apellido1 + " " + afiliado.Apellido2;
                                    if (ListaDocumentosTipoStr[cont] != null)
                                    {
                                        resultado[1] = ListaDocumentosTipoStr[cont];
                                    }
                                    else
                                    {
                                        resultado[1] = "";
                                    }
                                    resultado[2] = afiliado.Ocupacion;
                                    probar = true;
                                    return Json(new { resultado, probar }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }
                    cont = cont + 1;
                }
                if (resultado[0] == string.Empty)
                {
                    if (!string.IsNullOrEmpty(documento))
                    {
                        var cliente = new RestSharp.RestClient(ConfigurationManager.AppSettings["Url"]);
                        var request = new RestRequest(ConfigurationManager.AppSettings["consultaAfiliado"], RestSharp.Method.GET);
                        request.RequestFormat = DataFormat.Xml;
                        request.Parameters.Clear();
                        request.AddParameter("tpDoc", "CC");
                        request.AddParameter("doc", documento);
                        request.AddHeader("Content-Type", "application/json");
                        request.AddHeader("Accept", "application/json");

                        //se omite la validación de certificado de SSL
                        ServicePointManager.ServerCertificateValidationCallback = delegate
                        { return true; };
                        IRestResponse<List<AfiliadoModel>> response = cliente.Execute<List<AfiliadoModel>>(request);
                        var result = response.Content;
                        if (!string.IsNullOrWhiteSpace(result))
                        {
                            var respuesta = Newtonsoft.Json.JsonConvert.DeserializeObject<List<AfiliadoModel>>(result);
                            if (respuesta.Count != 0)
                            {
                                var afiliado = respuesta.Where(a => a.Estado == "Activo").FirstOrDefault();
                                if (afiliado == null)
                                {

                                }
                                else
                                {
                                    if (nit == afiliado.IdEmpresa)
                                    {
                                        resultado[0] = afiliado.Nombre1 + " " + afiliado.Nombre2 + " " + afiliado.Apellido1 + " " + afiliado.Apellido2;
                                        resultado[1] = "Cédula de Ciudadanía";
                                        resultado[2] = afiliado.Ocupacion;
                                        probar = true;
                                        return Json(new { resultado, probar }, JsonRequestBehavior.AllowGet);
                                    }
                                }
                            }

                        }
                    }
                    return Json(new { resultado, probar }, JsonRequestBehavior.AllowGet);
                }
                else
                    return Json(new { resultado, probar }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { resultado, probar }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult BuscarPersonaDocumentoCargo(string documento)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return Json(JsonRequestBehavior.AllowGet);
            }
            string nit = usuarioActual.NitEmpresa;
            List<EDTipoDocumento> EDTipoDoc = LNAcciones.BuscarTipoDocumento();
            List<TipoDocumento> ListaDocumentos = new List<TipoDocumento>();
            List<string> ListaDocumentosStr = new List<string>();
            foreach (var item in EDTipoDoc)
            {
                TipoDocumento td = new TipoDocumento();
                td.PK_IDTipo_Documento = item.Id_Tipo_Documento;
                td.Sigla = item.Sigla;
                td.Descripcion = item.Descripcion;
                ListaDocumentos.Add(td);
                ListaDocumentosStr.Add(td.Descripcion);
                ListaDocumentosStr.Add(td.Sigla);
            }

            //ListaDocumentos = ListaDocumentos.Where(s => s.AplicaPersonas == true).ToList();
            string Nit = usuarioActual.NitEmpresa;
            string[] resultado = new string[2] { string.Empty, string.Empty };
            bool probar = false;

            try
            {
                foreach (var item in ListaDocumentosStr)
                {
                    if (!string.IsNullOrEmpty(documento) && !string.IsNullOrEmpty(item))
                    {
                        var cliente = new RestSharp.RestClient(ConfigurationManager.AppSettings["Url"]);
                        var request = new RestRequest(ConfigurationManager.AppSettings["consultaAfiliado"], RestSharp.Method.GET);
                        request.RequestFormat = DataFormat.Xml;
                        request.Parameters.Clear();
                        request.AddParameter("tpDoc", item.ToString());
                        request.AddParameter("doc", documento);
                        request.AddHeader("Content-Type", "application/json");
                        request.AddHeader("Accept", "application/json");

                        //se omite la validación de certificado de SSL
                        ServicePointManager.ServerCertificateValidationCallback = delegate
                        { return true; };
                        IRestResponse<List<AfiliadoModel>> response = cliente.Execute<List<AfiliadoModel>>(request);
                        var result = response.Content;
                        if (!string.IsNullOrWhiteSpace(result))
                        {
                            var respuesta = Newtonsoft.Json.JsonConvert.DeserializeObject<List<AfiliadoModel>>(result);
                            var afiliado = respuesta.Where(a => a.Estado == "Activo").FirstOrDefault();
                            if (afiliado == null)
                            { }
                            else
                            {
                                if (nit == afiliado.IdEmpresa)
                                {
                                    resultado[0] = afiliado.Nombre1 + " " + afiliado.Nombre2 + " " + afiliado.Apellido1 + " " + afiliado.Apellido2;
                                    resultado[1] = afiliado.Ocupacion;
                                    probar = true;
                                    return Json(new { resultado, probar }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }
                }



                if (resultado[0] == string.Empty)
                {
                    if (!string.IsNullOrEmpty(documento))
                    {
                        var cliente = new RestSharp.RestClient(ConfigurationManager.AppSettings["Url"]);
                        var request = new RestRequest(ConfigurationManager.AppSettings["consultaAfiliado"], RestSharp.Method.GET);
                        request.RequestFormat = DataFormat.Xml;
                        request.Parameters.Clear();
                        request.AddParameter("tpDoc", "CC");
                        request.AddParameter("doc", documento);
                        request.AddHeader("Content-Type", "application/json");
                        request.AddHeader("Accept", "application/json");

                        //se omite la validación de certificado de SSL
                        ServicePointManager.ServerCertificateValidationCallback = delegate
                        { return true; };
                        IRestResponse<List<AfiliadoModel>> response = cliente.Execute<List<AfiliadoModel>>(request);
                        var result = response.Content;
                        if (!string.IsNullOrWhiteSpace(result))
                        {
                            var respuesta = Newtonsoft.Json.JsonConvert.DeserializeObject<List<AfiliadoModel>>(result);
                            if (respuesta.Count != 0)
                            {
                                var afiliado = respuesta.Where(a => a.Estado == "Activo").FirstOrDefault();
                                if (afiliado == null)
                                {

                                }
                                else
                                {
                                    if (nit == afiliado.IdEmpresa)
                                    {
                                        resultado[0] = afiliado.Nombre1 + " " + afiliado.Nombre2 + " " + afiliado.Apellido1 + " " + afiliado.Apellido2;
                                        resultado[1] = afiliado.Ocupacion;
                                        probar = true;
                                        return Json(new { resultado, probar }, JsonRequestBehavior.AllowGet);
                                    }
                                }
                            }

                        }
                    }
                    return Json(new { resultado, probar }, JsonRequestBehavior.AllowGet);
                }
                else
                    return Json(new { resultado, probar }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { resultado, probar }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult BuscarPersonaDocumentoCargo1(string documento)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return Json(JsonRequestBehavior.AllowGet);
            }
            string nit = usuarioActual.NitEmpresa;
            List<EDTipoDocumento> EDTipoDoc = LNAcciones.BuscarTipoDocumento();
            List<TipoDocumento> ListaDocumentos = new List<TipoDocumento>();
            List<string> ListaDocumentosStr = new List<string>();
            foreach (var item in EDTipoDoc)
            {
                TipoDocumento td = new TipoDocumento();
                td.PK_IDTipo_Documento = item.Id_Tipo_Documento;
                td.Sigla = item.Sigla;
                td.Descripcion = item.Descripcion;
                ListaDocumentos.Add(td);
                ListaDocumentosStr.Add(td.Descripcion);
                ListaDocumentosStr.Add(td.Sigla);
            }

            //ListaDocumentos = ListaDocumentos.Where(s => s.AplicaPersonas == true).ToList();
            string Nit = usuarioActual.NitEmpresa;
            string[] resultado = new string[2] { string.Empty, string.Empty };
            List<EDRelacionesLaborales> RelacionLaboral = new List<EDRelacionesLaborales>();

            bool probar = false;

            try
            {
                foreach (var item in ListaDocumentosStr)
                {
                    if (!string.IsNullOrEmpty(documento) && !string.IsNullOrEmpty(item))
                    {
                        var cliente = new RestSharp.RestClient(ConfigurationManager.AppSettings["Url"]);
                        var request = new RestRequest(ConfigurationManager.AppSettings["consultaAfiliado"], RestSharp.Method.GET);
                        request.RequestFormat = DataFormat.Xml;
                        request.Parameters.Clear();
                        request.AddParameter("tpDoc", item.ToString());
                        request.AddParameter("doc", documento);
                        request.AddHeader("Content-Type", "application/json");
                        request.AddHeader("Accept", "application/json");

                        //se omite la validación de certificado de SSL
                        ServicePointManager.ServerCertificateValidationCallback = delegate
                        { return true; };
                        IRestResponse<List<AfiliadoModel>> response = cliente.Execute<List<AfiliadoModel>>(request);
                        var result = response.Content;
                        if (!string.IsNullOrWhiteSpace(result))
                        {
                            var respuesta = Newtonsoft.Json.JsonConvert.DeserializeObject<List<AfiliadoModel>>(result);
                            var afiliado = respuesta.Where(a => a.Estado == "Activo").ToList();
                            if (afiliado == null)
                            { }
                            else
                            {
                                foreach (var item1 in afiliado)
                                {
                                    if (nit == item1.IdEmpresa)
                                    {
                                        EDRelacionesLaborales EDRelacionesLaborales = new EDRelacionesLaborales();
                                        EDRelacionesLaborales.Nombre1 = item1.Nombre1 + " " + item1.Nombre2 + " " + item1.Apellido1 + " " + item1.Apellido2;
                                        EDRelacionesLaborales.Ocupacion = item1.Ocupacion;
                                        EDRelacionesLaborales.TipoDocumento = "Cédula de Ciudadanía";
                                        RelacionLaboral.Add(EDRelacionesLaborales);

                                        probar = true;
                                        return Json(new { resultado, probar, RelacionLaboral }, JsonRequestBehavior.AllowGet);
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                                
                            }
                        }
                    }
                }



                if (resultado[0] == string.Empty)
                {
                    if (!string.IsNullOrEmpty(documento))
                    {
                        var cliente = new RestSharp.RestClient(ConfigurationManager.AppSettings["Url"]);
                        var request = new RestRequest(ConfigurationManager.AppSettings["consultaAfiliado"], RestSharp.Method.GET);
                        request.RequestFormat = DataFormat.Xml;
                        request.Parameters.Clear();
                        request.AddParameter("tpDoc", "CC");
                        request.AddParameter("doc", documento);
                        request.AddHeader("Content-Type", "application/json");
                        request.AddHeader("Accept", "application/json");

                        //se omite la validación de certificado de SSL
                        ServicePointManager.ServerCertificateValidationCallback = delegate
                        { return true; };
                        IRestResponse<List<AfiliadoModel>> response = cliente.Execute<List<AfiliadoModel>>(request);
                        var result = response.Content;
                        if (!string.IsNullOrWhiteSpace(result))
                        {
                            var respuesta = Newtonsoft.Json.JsonConvert.DeserializeObject<List<AfiliadoModel>>(result);
                            if (respuesta.Count != 0)
                            {
                                var afiliado = respuesta.Where(a => a.Estado == "Activo").ToList();
                                if (afiliado == null)
                                {

                                }
                                else
                                {
                                    foreach (var item1 in afiliado)
                                    {
                                        if (nit == item1.IdEmpresa)
                                        {
                                            EDRelacionesLaborales EDRelacionesLaborales = new EDRelacionesLaborales();
                                            EDRelacionesLaborales.Nombre1 = item1.Nombre1 + " " + item1.Nombre2 + " " + item1.Apellido1 + " " + item1.Apellido2;
                                            EDRelacionesLaborales.Ocupacion = item1.Ocupacion;
                                            EDRelacionesLaborales.TipoDocumento = "Cédula de Ciudadanía";
                                            RelacionLaboral.Add(EDRelacionesLaborales);

                                            probar = true;
                                            return Json(new { resultado, probar, RelacionLaboral }, JsonRequestBehavior.AllowGet);
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                }
                            }

                        }
                    }
                    return Json(new { resultado, probar, RelacionLaboral }, JsonRequestBehavior.AllowGet);
                }
                else
                    return Json(new { resultado, probar, RelacionLaboral }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { resultado, probar, RelacionLaboral }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
        #region Analisis
        [HttpPost]
        public JsonResult CancelarAnalisis(List<String> values)
        {
            bool probar = true;
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return Json(new { url = Url.Action("Index", "Acciones"), probar },
                JsonRequestBehavior.AllowGet);
            }

            string TempDataOrigen = values[0];
            string TempDataAnalisis = values[1];

            if (TempData[TempDataAnalisis] != null)
            {
                TempData.Remove(TempDataAnalisis);
            }
            if (TempData["AnalisisArbol"] != null)
            {
                TempData.Remove("AnalisisArbol");
            }
            if (TempData["AnalisisCausa"] != null)
            {
                TempData.Remove("AnalisisCausa");
            }
            if (TempData["Analisis5porques"] != null)
            {
                TempData.Remove("Analisis5porques");
            }
            if (TempData["AnalisisLluvia"] != null)
            {
                TempData.Remove("AnalisisLluvia");
            }
            if (TempDataOrigen != "NuevaAccion")
            {
                string Origen = TempDataOrigen.Replace("EditarAccion", "");
                //Devuelve el resultado exitosamente
                return Json(new { url = Url.Action("EditarAccion", "Acciones", new { id = Origen }), probar },
            JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { url = Url.Action("NuevaAccion", "Acciones"), probar },
                JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult AnalisisArbol(string Edicion)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }
            ViewBag.SrcImg = SrcWhite;
            ViewBag.TextoProblema = "";
            string NombreTemp = "";
            if (Edicion != null)
            {
                TempData.Keep(Edicion);
                ViewBag.EdicionKey = Edicion;
                ViewBag.SessionKey = Edicion;
                ViewBag.TempData = "Arbol" + Edicion;
                NombreTemp = "Arbol" + Edicion;
            }
            else
            {
                ViewBag.EdicionKey = "";
                ViewBag.SessionKey = "NuevaAccion";
                ViewBag.TempData = "AnalisisArbol";
                NombreTemp = "AnalisisArbol";
            }
            List<EDAnalisis> ListaAnalisis = new List<EDAnalisis>();
            if (TempData[NombreTemp] == null)
            {
                List<EDAnalisis> ListaAnalisisNuevo = new List<EDAnalisis>();

                if (Edicion != null)
                {
                    var AccionSession = (EDAccion)TempData[Edicion];
                    AccionSession.AnalisisLista = LNAcciones.ConsultaAnalisisEdicion(AccionSession.Pk_Id_Accion, usuarioActual.IdEmpresa, 1);
                    ListaAnalisis = AccionSession.AnalisisLista;
                }
                else
                {
                    inicializar();
                    var AccionSession = Session["EDAccion"] as EDAccion;
                    ListaAnalisis = AccionSession.AnalisisLista;

                }
                foreach (var item in ListaAnalisis)
                {
                    if (item.Tipo == 1)
                    {
                        ListaAnalisisNuevo.Add(item);
                    }
                }
                TempData[NombreTemp] = ListaAnalisisNuevo;
                ListaAnalisis = (List<EDAnalisis>)TempData[NombreTemp];
            }
            else
            {
                ListaAnalisis = (List<EDAnalisis>)TempData[NombreTemp];
            }
            #region obtenerrutas
            List<string> Lista_Niveles = new List<string>();
            List<string> Lista_Valores = new List<string>();
            List<int> Lista_Id = new List<int>();
            if (ListaAnalisis.Count > 1)
            {
                List<EDAnalisis> ListaOrdenada = ListaAnalisis.OrderBy(x => x.Parent_Id).ToList();
                int padre_anterior = 0;
                int contador_nivel = 0;
                foreach (var item in ListaOrdenada)
                {
                    string texto = item.ValorTxt;
                    int Nivel = item.Parent_Id;
                    int IdElemento = item.Id_Analisis;
                    if (padre_anterior != Nivel)
                    {
                        contador_nivel = 1;
                    }
                    else
                    {
                        contador_nivel = contador_nivel + 1;
                    }
                    if (Nivel == 0)
                    {
                        ViewBag.TextoProblema = texto;
                        Lista_Niveles.Add("1");
                        Lista_Valores.Add(texto);
                        Lista_Id.Add(IdElemento);
                    }
                    else
                    {
                        string path_anterior = "";
                        int cont = 0;
                        //buscar padre
                        foreach (var item1 in Lista_Id)
                        {
                            if (item1 == item.Parent_Id)
                            {
                                path_anterior = Lista_Niveles[cont];
                            }
                            cont = cont + 1;
                        }
                        Lista_Niveles.Add(path_anterior + "/" + contador_nivel);
                        Lista_Valores.Add(texto);
                        Lista_Id.Add(IdElemento);
                    }
                    padre_anterior = Nivel;
                }
                string CrearDiagrama = DiagramaArbol(Lista_Valores, Lista_Niveles);
                ViewBag.SrcImg = CrearDiagrama;
            }
            else
            {
                ViewBag.SrcImg = SrcWhite;
            }
            
            #endregion
            return View();
        }
        [HttpGet]
        public ActionResult AnalisisLluvia(string Edicion)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }
            ViewBag.SrcImg = SrcWhite;
            ViewBag.TextoProblema = "";
            string NombreTemp = "";
            if (Edicion != null)
            {
                TempData.Keep(Edicion);
                ViewBag.EdicionKey = Edicion;
                ViewBag.SessionKey = Edicion;
                ViewBag.TempData = "Lluvia" + Edicion;
                NombreTemp = "Lluvia" + Edicion;
            }
            else
            {
                ViewBag.EdicionKey = "";
                ViewBag.SessionKey = "NuevaAccion";
                ViewBag.TempData = "AnalisisLluvia";
                NombreTemp = "AnalisisLluvia";
            }
            List<EDAnalisis> ListaAnalisis = new List<EDAnalisis>();
            if (TempData[NombreTemp] == null)
            {
                List<EDAnalisis> ListaAnalisisNuevo = new List<EDAnalisis>();
                if (Edicion != null)
                {
                    var AccionSession = (EDAccion)TempData[Edicion];
                    AccionSession.AnalisisLista = LNAcciones.ConsultaAnalisisEdicion(AccionSession.Pk_Id_Accion, usuarioActual.IdEmpresa, 4);
                    ListaAnalisis = AccionSession.AnalisisLista;
                }
                else
                {
                    inicializar();
                    var AccionSession = Session["EDAccion"] as EDAccion;
                    ListaAnalisis = AccionSession.AnalisisLista;

                }
                foreach (var item in ListaAnalisis)
                {
                    if (item.Tipo == 4)
                    {
                        ListaAnalisisNuevo.Add(item);
                    }
                }
                TempData[NombreTemp] = ListaAnalisisNuevo;
                ListaAnalisis = (List<EDAnalisis>)TempData[NombreTemp];
            }
            else
            {
                ListaAnalisis = (List<EDAnalisis>)TempData[NombreTemp];
            }

            #region obtenerrutas
            List<string> Lista_Niveles = new List<string>();
            List<string> Lista_Valores = new List<string>();
            List<int> Lista_Id = new List<int>();
            if (ListaAnalisis.Count >= 0)
            {
                List<EDAnalisis> ListaOrdenada = ListaAnalisis.OrderBy(x => x.Parent_Id).ToList();
                int padre_anterior = 0;
                int contador_nivel = 0;
                foreach (var item in ListaOrdenada)
                {
                    string texto = item.ValorTxt;
                    int Nivel = item.Parent_Id;
                    int IdElemento = item.Id_Analisis;
                    if (padre_anterior != Nivel)
                    {
                        contador_nivel = 1;
                    }
                    else
                    {
                        contador_nivel = contador_nivel + 1;
                    }
                    if (Nivel == 0)
                    {
                        ViewBag.TextoProblema = texto;
                        Lista_Niveles.Add("1");
                        Lista_Valores.Add(texto);
                        Lista_Id.Add(IdElemento);
                    }
                    else
                    {
                        string path_anterior = "";
                        int cont = 0;
                        //buscar padre
                        foreach (var item1 in Lista_Id)
                        {
                            if (item1 == item.Parent_Id)
                            {
                                path_anterior = Lista_Niveles[cont];
                            }
                            cont = cont + 1;
                        }
                        Lista_Niveles.Add(path_anterior + "/" + contador_nivel);
                        Lista_Valores.Add(texto);
                        Lista_Id.Add(IdElemento);
                    }
                    padre_anterior = Nivel;
                }
            }
            string CrearDiagrama = GenerarLluvia(Lista_Valores, Lista_Niveles);
            ViewBag.SrcImg = CrearDiagrama;
            #endregion

            return View();
        }
        [HttpGet]
        public ActionResult AnalisisCausa(string Edicion)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }
            ViewBag.SrcImg = SrcWhite;
            ViewBag.TextoProblema = "";
            string NombreTemp = "";
            if (Edicion != null)
            {
                TempData.Keep(Edicion);
                ViewBag.EdicionKey = Edicion;
                ViewBag.SessionKey = Edicion;
                ViewBag.TempData = "Causa" + Edicion;
                NombreTemp = "Causa" + Edicion;
            }
            else
            {
                ViewBag.EdicionKey = "";
                ViewBag.SessionKey = "NuevaAccion";
                ViewBag.TempData = "AnalisisCausa";
                NombreTemp = "AnalisisCausa";
            }
            List<EDAnalisis> ListaAnalisis = new List<EDAnalisis>();
            if (TempData[NombreTemp] == null)
            {
                List<EDAnalisis> ListaAnalisisNuevo = new List<EDAnalisis>();

                if (Edicion != null)
                {
                    var AccionSession = (EDAccion)TempData[Edicion];
                    AccionSession.AnalisisLista = LNAcciones.ConsultaAnalisisEdicion(AccionSession.Pk_Id_Accion, usuarioActual.IdEmpresa, 2);
                    ListaAnalisis = AccionSession.AnalisisLista;
                }
                else
                {
                    inicializar();
                    var AccionSession = Session["EDAccion"] as EDAccion;
                    ListaAnalisis = AccionSession.AnalisisLista;

                }
                foreach (var item in ListaAnalisis)
                {
                    if (item.Tipo == 2)
                    {
                        ListaAnalisisNuevo.Add(item);
                    }
                }
                TempData[NombreTemp] = ListaAnalisisNuevo;
                ListaAnalisis = (List<EDAnalisis>)TempData[NombreTemp];
            }
            else
            {
                ListaAnalisis = (List<EDAnalisis>)TempData[NombreTemp];
            }

            #region obtenerrutas
            List<string> Lista_Niveles = new List<string>();
            List<string> Lista_Valores = new List<string>();
            List<int> Lista_Id = new List<int>();
            if (ListaAnalisis.Count >= 0)
            {
                List<EDAnalisis> ListaOrdenada = ListaAnalisis.OrderBy(x => x.Parent_Id).ToList();
                int padre_anterior = 0;
                int contador_nivel = 0;
                foreach (var item in ListaOrdenada)
                {
                    string texto = item.ValorTxt;
                    int Nivel = item.Parent_Id;
                    int IdElemento = item.Id_Analisis;

                    if (padre_anterior != Nivel)
                    {
                        contador_nivel = 1;
                    }
                    else
                    {
                        contador_nivel = contador_nivel + 1;
                    }
                    if (Nivel == 0)
                    {
                        ViewBag.TextoProblema = texto;
                        Lista_Niveles.Add("1");
                        Lista_Valores.Add(texto);
                        Lista_Id.Add(IdElemento);
                    }
                    else
                    {
                        string path_anterior = "";
                        int cont = 0;
                        //buscar padre
                        foreach (var item1 in Lista_Id)
                        {
                            if (item1 == item.Parent_Id)
                            {
                                path_anterior = Lista_Niveles[cont];
                            }
                            cont = cont + 1;
                        }
                        Lista_Niveles.Add(path_anterior + "/" + contador_nivel);
                        Lista_Valores.Add(texto);
                        Lista_Id.Add(IdElemento);
                    }
                    padre_anterior = Nivel;
                }
            }
            string CrearDiagrama = DiagramaCausaEfecto(Lista_Valores, Lista_Niveles);
            ViewBag.SrcImg = CrearDiagrama;
            #endregion



            return View();
        }
        [HttpGet]
        public ActionResult Analisis5Porque(string Edicion)
        {

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }

            ViewBag.SrcImg = "";
            string NombreTemp = "";
            if (Edicion != null)
            {
                TempData.Keep(Edicion);
                ViewBag.EdicionKey = Edicion;
                ViewBag.SessionKey = Edicion;
                ViewBag.TempData = "5porque" + Edicion;
                NombreTemp = "5porque" + Edicion;
            }
            else
            {
                ViewBag.EdicionKey = "";
                ViewBag.SessionKey = "NuevaAccion";
                ViewBag.TempData = "Analisis5porques";
                NombreTemp = "Analisis5porques";
            }

            //Si existen nodos de tipo 1 es decir de tipo árbol de decisiones cargarlos
            List<EDAnalisis> ListaAnalisis = new List<EDAnalisis>();
            if (TempData[NombreTemp] == null)
            {
                List<EDAnalisis> ListaAnalisisNuevo = new List<EDAnalisis>();

                if (Edicion != null)
                {
                    var AccionSession = (EDAccion)TempData[Edicion];
                    AccionSession.AnalisisLista = LNAcciones.ConsultaAnalisisEdicion(AccionSession.Pk_Id_Accion, usuarioActual.IdEmpresa, 3);
                    ListaAnalisis = AccionSession.AnalisisLista;
                }
                else
                {
                    inicializar();
                    var AccionSession = Session["EDAccion"] as EDAccion;
                    ListaAnalisis = AccionSession.AnalisisLista;
                }
                foreach (var item in ListaAnalisis)
                {
                    if (item.Tipo == 3)
                    {
                        ListaAnalisisNuevo.Add(item);
                        ListaAnalisisNuevo.OrderBy(x => x.Id_Analisis);
                    }
                }
                TempData[NombreTemp] = ListaAnalisisNuevo;
                ListaAnalisis = (List<EDAnalisis>)TempData[NombreTemp];
                foreach (var item in ListaAnalisis)
                {
                    if (item.ValorTxt == "NullValueString")
                    {
                        item.ValorTxt = null;
                    }
                }
            }
            else
            {
                ListaAnalisis = (List<EDAnalisis>)TempData[NombreTemp];
                foreach (var item in ListaAnalisis)
                {
                    if (item.ValorTxt == "NullValueString")
                    {
                        item.ValorTxt = null;
                    }
                }
            }

            #region obtenerrutas
            List<EDAnalisis> ListaAnalisis1 = new List<EDAnalisis>();
            ListaAnalisis1 = ListaAnalisis;
            if (ListaAnalisis.Count < 36 && ListaAnalisis.Count >= 8)
            {
                int padre = ListaAnalisis[0].Pk_Id_Analisis;
                for (int i = 1; i < 37; i++)
                {
                    bool probar = false;

                    foreach (var item in ListaAnalisis)
                    {
                        if (item.Id_Analisis == i)
                        {
                            probar = true;
                        }
                    }

                    if (probar == false)
                    {
                        EDAnalisis EDAnalisis = new EDAnalisis();
                        EDAnalisis.Id_Analisis = i;
                        EDAnalisis.ValorTxt = "";
                        EDAnalisis.Parent_Id = padre;
                        ListaAnalisis1.Add(EDAnalisis);
                    }
                }
            }
            ListaAnalisis = ListaAnalisis1;


            List<string> Lista_Niveles = new List<string>();
            List<string> Lista_Valores = new List<string>();
            List<int> Lista_Id = new List<int>();
            if (ListaAnalisis.Count >= 0)
            {
                List<EDAnalisis> ListaOrdenada = ListaAnalisis.OrderBy(x => x.Parent_Id).ToList();
                int padre_anterior = 0;
                int contador_nivel = 0;
                foreach (var item in ListaOrdenada)
                {
                    string texto = item.ValorTxt;
                    int Nivel = item.Parent_Id;
                    int IdElemento = item.Id_Analisis;

                    if (padre_anterior != Nivel)
                    {
                        contador_nivel = 1;
                    }
                    else
                    {
                        contador_nivel = contador_nivel + 1;
                    }
                    if (Nivel == 0)
                    {
                        Lista_Niveles.Add("1");
                        Lista_Valores.Add(texto);
                        Lista_Id.Add(IdElemento);
                    }
                    else
                    {
                        string path_anterior = "";
                        int cont = 0;
                        //buscar padre
                        foreach (var item1 in Lista_Id)
                        {
                            if (item1 == item.Parent_Id)
                            {
                                path_anterior = Lista_Niveles[cont];
                            }
                            cont = cont + 1;
                        }
                        Lista_Niveles.Add(path_anterior + "/" + contador_nivel);
                        Lista_Valores.Add(texto);
                        Lista_Id.Add(IdElemento);
                    }
                    padre_anterior = Nivel;
                }
            }



            string CrearDiagrama = Diagrama5Porque(Lista_Valores, Lista_Niveles);
            ViewBag.SrcImg = CrearDiagrama;
            #endregion


            return View();
        }
        #region Arbol
        [HttpPost]
        public JsonResult AgregarNodoPadre(List<String> values)
        {
            //Values 0 -> Problema
            //Values 1 -> TempData        
            bool probar = false;
            string resultado = "";
            string resultado1 = "";
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                resultado = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { resultado1, resultado, probar, }, JsonRequestBehavior.AllowGet);
            }

            string TempDataNombre = values[1];
            if (TempData[TempDataNombre] == null)
            {
                List<EDAnalisis> ListaAnalisisNuevo = new List<EDAnalisis>();
                TempData[TempDataNombre] = ListaAnalisisNuevo;
            }
            // Verificar que tiene un Nodo Padre de 'Problema a solucionar'
            List<EDAnalisis> ListaAnalisis = (List<EDAnalisis>)TempData[TempDataNombre];

            if (values[0].ToString() != "")
            {
                if (ListaAnalisis.Count > 0)
                {
                    ListaAnalisis.Where(w => w.Parent_Id == 0 && w.Tipo == 1).ToList().ForEach(s => s.ValorTxt = values[0].ToString());
                    probar = true;
                    resultado = "Entrada principal actualizada";
                    resultado1 = values[0].ToString();
                }
                else
                {
                    EDAnalisis analisis = new EDAnalisis();
                    analisis.Id_Analisis = 1;
                    analisis.Tipo = 1;
                    analisis.ValorTxt = values[0].ToString();
                    analisis.Parent_Id = 0;
                    ListaAnalisis.Add(analisis);
                    probar = true;
                    resultado = "Entrada principal Creada";
                    resultado1 = values[0].ToString();
                }
            }
            TempData[TempDataNombre] = ListaAnalisis;
            TempData.Keep(TempDataNombre);
            if (probar == false)
            {
                resultado = "Digite un PROBLEMA O HALLAZGO, para actualizarlo";
            }

            //Devuelve el resultado exitosamente
            return Json(new { resultado1, resultado, probar, }, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult AgregarNodoHijo(List<String> values)
        {
            //Values 0 -> Id Nodo Padre  
            //Values 1 -> Id Nodo Hijo  
            //Values 2 -> TempData 

            // Variables de resultados Probar-> Hay exito en la operación, Resultado-> Texto de respuesta
            bool probar = false;
            string resultado = "";
            string TempDataNombre = values[2];

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                resultado = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { resultado, probar },
                JsonRequestBehavior.AllowGet);
            }

            int Id_Padre = -1;
            bool ProbarSeleccion = false;
            ProbarSeleccion = int.TryParse(values[0], out Id_Padre);

            if (ProbarSeleccion == false)
            {
                TempData.Keep(TempDataNombre);
                resultado = "Primero seleccione un elemento de la estructura del árbol, escriba la causa para agregar y haga click en 'Agregar Causa'";
                return Json(new { resultado, probar },
                JsonRequestBehavior.AllowGet);
            }

            if (values[1].ToString() == "")
            {
                TempData.Keep(TempDataNombre);
                resultado = "Ya seleccionó una causa de la estructra del árbol, pero no ha digitado la nueva causa";
                return Json(new { resultado, probar },
                JsonRequestBehavior.AllowGet);
            }
            // Instanciar el TempData Analisis Arbol si esta null
            if (TempData[TempDataNombre] == null)
            {
                List<EDAnalisis> ListaAnalisisNuevo = new List<EDAnalisis>();
                TempData[TempDataNombre] = ListaAnalisisNuevo;
            }
            // Verificar que tiene un Nodo Padre Seleccionado
            List<EDAnalisis> ListaAnalisis = (List<EDAnalisis>)TempData[TempDataNombre];
            if (ProbarSeleccion)
            {
                //probar si existe el IdPadre en la colección de Nodos
                Analisis AnalisisSeleccionado = new Analisis();

                var Ana = (from s in ListaAnalisis
                           where s.Id_Analisis == Id_Padre
                           select s).FirstOrDefault<EDAnalisis>();

                int NuevoAna = 0;
                if (Ana != null)
                {
                    //Generar un IdAnalisis
                    bool ProbarNumeroGenerado = false;
                    while (ProbarNumeroGenerado == false)
                    {
                        NuevoAna = NuevoAna + 1;
                        ProbarNumeroGenerado = true;
                        for (int i = 0; i < ListaAnalisis.Count; i++)
                        {
                            if (ListaAnalisis[i].Id_Analisis == NuevoAna)
                            {
                                ProbarNumeroGenerado = false;
                                break;
                            }
                            else
                            {

                            }
                        }
                    }
                    //Añadir opción a la colección
                    EDAnalisis analisis = new EDAnalisis();
                    analisis.Id_Analisis = NuevoAna;
                    analisis.Tipo = 1;
                    analisis.ValorTxt = values[1].ToString();
                    analisis.Parent_Id = Id_Padre;
                    ListaAnalisis.Add(analisis);
                    probar = true;
                    resultado = "Causa agregada: " + values[1].ToString();
                }
                else
                {
                    TempData.Keep(TempDataNombre);
                    resultado = "Primero seleccione un elemento de la estructura del árbol, escriba la causa para agregar y haga click en 'Agregar Causa'";
                    return Json(new { resultado, probar },
                    JsonRequestBehavior.AllowGet);
                }
            }
            TempData[TempDataNombre] = ListaAnalisis;
            TempData.Keep(TempDataNombre);
            //Devuelve el resultado exitosamente
            return Json(new { resultado, probar },
                JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult EliminarNodoHijo(List<String> values)
        {
            //Values 0 -> Id Nodo Padre  
            //Values 1 -> Tempdata
            bool probar = false;
            string resultado = "";

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                resultado = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
            }

            string TempDataNombre = values[1];
            int Id_Padre = -1;
            bool ProbarSeleccion = false;
            ProbarSeleccion = int.TryParse(values[0], out Id_Padre);
            if (ProbarSeleccion == false)
            {
                TempData.Keep(TempDataNombre);
                resultado = "No ha seleccionado una opción para eliminar";
                return Json(new { resultado, probar },
                JsonRequestBehavior.AllowGet);

            }
            // Instanciar el TempData Analisis Arbol
            if (TempData[TempDataNombre] == null)
            {
                List<EDAnalisis> ListaAnalisisNuevo = new List<EDAnalisis>();
                TempData[TempDataNombre] = ListaAnalisisNuevo;
            }
            // Verificar que tiene un Nodo Padre Seleccionado
            List<EDAnalisis> ListaAnalisis = (List<EDAnalisis>)TempData[TempDataNombre];
            if (ProbarSeleccion)
            {
                //probar si existe el IdPadre en la colección de Nodos
                EDAnalisis AnalisisSeleccionado = new EDAnalisis();

                var Ana = (from s in ListaAnalisis
                           where s.Id_Analisis == Id_Padre
                           select s).FirstOrDefault<EDAnalisis>();
                if (Ana != null)
                {
                    if (Ana.Parent_Id != 0)
                    {
                        //Eliminar Nodos Recursivamente
                        List<int> ListaEliminar = new List<int>();
                        ListaEliminar.Add(Id_Padre);
                        TempData["BorrarArbol"] = ListaEliminar;
                        //Llamado a Buscar lista de eliminados
                        EliminarNodosRec(ListaAnalisis, Id_Padre);
                        ListaEliminar = (List<int>)TempData["BorrarArbol"];
                        //Eliminar según lista
                        for (int i = 0; i < ListaEliminar.Count; i++)
                        {
                            ListaAnalisis.RemoveAll(s => s.Id_Analisis == ListaEliminar[i]);
                        }
                        TempData.Remove("BorrarArbol");
                        resultado = "Causa eliminada";
                        probar = true;
                    }
                    else
                    {
                        ListaAnalisis.RemoveAll(s => s.Fk_Id_Accion == Ana.Fk_Id_Accion);
                        resultado = "Causa eliminada";
                        probar = true;
                    }
                }
                else
                {
                    TempData.Keep(TempDataNombre);
                    resultado = "No ha seleccionado una opción para eliminar";
                    return Json(new { resultado, probar },
                    JsonRequestBehavior.AllowGet);
                }
            }
            TempData[TempDataNombre] = ListaAnalisis;
            TempData.Keep(TempDataNombre);
            return Json(new { resultado, probar },
                JsonRequestBehavior.AllowGet);
        }
        private void EliminarNodosRec(List<EDAnalisis> ListaAnalisis, int ParentId)
        {
            List<int> ListaBorrar = new List<int>();
            foreach (var i in ListaAnalisis.Where(a => a.Parent_Id.Equals(ParentId)))
            {
                int IdPadreSig = i.Id_Analisis;
                EliminarNodosRec(ListaAnalisis, IdPadreSig);
                List<int> ListaEliminar = (List<int>)TempData["BorrarArbol"];
                ListaEliminar.Add(i.Id_Analisis);
                TempData["BorrarArbol"] = ListaEliminar;
            }
        }
        [HttpPost]
        public JsonResult GuardarAnArbol(List<String> values)
        {
            bool probar = false;
            string mensaje = "Guardado de análisis exitoso";

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                mensaje = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { url = Url.Action("Index", "Acciones"), probar, mensaje },
            JsonRequestBehavior.AllowGet);
            }
            //Values 0 -> Tempdata
            //Values 1 -> Tempdata origen
            string TempDataOrigen = values[0];
            string TempDataNombre = values[1];
            TempData.Keep(TempDataNombre);
            TempData.Keep(TempDataOrigen);

            try
            {
                List<EDAnalisis> ListaAnalisis = new List<EDAnalisis>();
                if (TempData[TempDataNombre] == null)
                {
                    TempData[TempDataNombre] = ListaAnalisis;
                }
                ListaAnalisis = (List<EDAnalisis>)TempData[TempDataNombre];
                if (ListaAnalisis.Count == 0)
                {
                    TempData.Keep(TempDataOrigen);
                    probar = false;
                    mensaje = "No hay elemento que guardar de este análisis";
                }
                List<EDAnalisis> ListaAnalisisGuardar = new List<EDAnalisis>();
                foreach (var item in ListaAnalisis)
                {
                    EDAnalisis AnalisisCopiar = new EDAnalisis();
                    if (item.Tipo != 1)
                    {
                        AnalisisCopiar.Tipo = 1;
                    }
                    else
                    {
                        AnalisisCopiar.Tipo = item.Tipo;
                    }
                    if (item.ValorTxt == "" || item.ValorTxt == null)
                    {
                        AnalisisCopiar.ValorTxt = "Elemento sin Valor";
                    }
                    else
                    {
                        AnalisisCopiar.ValorTxt = item.ValorTxt;
                    }
                    AnalisisCopiar.Parent_Id = item.Parent_Id;
                    AnalisisCopiar.Id_Analisis = item.Id_Analisis;
                    if (item.Pk_Id_Analisis != 0)
                    {
                        AnalisisCopiar.Pk_Id_Analisis = item.Pk_Id_Analisis;
                    }
                    ListaAnalisisGuardar.Add(AnalisisCopiar);
                }
                if (TempDataOrigen != "NuevaAccion")
                {
                    var AccionSession = (EDAccion)TempData[TempDataOrigen];
                    int IdAccion = AccionSession.Pk_Id_Accion;
                    List<EDAnalisis> ListaActual = LNAcciones.ListaAnalisis(IdAccion);
                    List<EDAnalisis> ListaBorrar = new List<EDAnalisis>();
                    List<EDAnalisis> ListaActualizar = new List<EDAnalisis>();
                    foreach (var item in ListaActual)
                    {
                        bool Actualizar = false;
                        foreach (var item1 in ListaAnalisisGuardar)
                        {
                            item1.Pk_Id_Analisis = item.Pk_Id_Analisis;
                            item1.Tipo = 1;
                            item1.Fk_Id_Accion = IdAccion;
                            ListaActualizar.Add(item1);
                            ListaAnalisisGuardar.Remove(item1);
                            Actualizar = true;
                            break;
                        }
                        if (!Actualizar)
                        {
                            ListaBorrar.Add(item);
                        }
                    }
                    if (ListaAnalisis.Count != 0)
                    {
                        probar = LNAcciones.GuardarCambiosAnalisis(ListaAnalisisGuardar, ListaActualizar, ListaBorrar, 1, IdAccion);
                    }
                    TempData[TempDataOrigen] = AccionSession;
                }
                else
                {
                    inicializar();
                    var AccionSession = Session["EDAccion"] as EDAccion;
                    List<EDAnalisis> ListaBorrar = AccionSession.AnalisisLista;
                    ListaBorrar.RemoveAll(x => x.Pk_Id_Analisis == 0);
                    AccionSession.AnalisisLista = ListaBorrar;
                    foreach (var item in ListaAnalisisGuardar)
                    {
                        AccionSession.AnalisisLista.Add(item);
                    }
                    probar = true;
                    mensaje = "Guardado de análisis exitoso, El análisis del hallazgo se guardará junto a la acción";
                }
            }
            catch (Exception)
            {
                TempData.Keep(TempDataOrigen);
                TempData.Remove(TempDataNombre);
                probar = false;
                mensaje = "Ha ocurrido un error, por favor verifique y vuelva a intentar";
            }

            if (TempDataOrigen != "NuevaAccion")
            {
                TempData.Remove(TempDataNombre);
                string Origen = TempDataOrigen.Replace("EditarAccion", "");
                return Json(new { url = Url.Action("EditarAccion", "Acciones", new { id = Origen }), probar, mensaje },
            JsonRequestBehavior.AllowGet);
            }
            else
            {
                TempData.Remove(TempDataNombre);
                return Json(new { url = Url.Action("NuevaAccion", "Acciones"), probar, mensaje },
            JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
        #region Lluvia
        [HttpPost]
        public JsonResult AgregarNodoPadreLluvia(List<String> values)
        {
            bool probar = false;
            string resultado = "";
            string resultado1 = "";
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                resultado = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { resultado1, resultado, probar, }, JsonRequestBehavior.AllowGet);
            }
            //Values 0 -> Problema
            //Values 1 -> TempData        
            string TempDataNombre = values[1];
            if (TempData[TempDataNombre] == null)
            {
                List<EDAnalisis> ListaAnalisisNuevo = new List<EDAnalisis>();
                TempData[TempDataNombre] = ListaAnalisisNuevo;
            }
            List<EDAnalisis> ListaAnalisis = (List<EDAnalisis>)TempData[TempDataNombre];
            if (values[0].ToString() != "")
            {
                if (ListaAnalisis.Count > 0)
                {
                    ListaAnalisis.Where(w => w.Parent_Id == 0 && w.Tipo == 4).ToList().ForEach(s => s.ValorTxt = values[0].ToString());
                    probar = true;
                    resultado = "Entrada principal actualizada";
                    resultado1 = values[0].ToString();
                }
                else
                {
                    EDAnalisis analisis = new EDAnalisis();
                    analisis.Id_Analisis = 1;
                    analisis.Tipo = 4;
                    analisis.ValorTxt = values[0].ToString();
                    analisis.Parent_Id = 0;
                    ListaAnalisis.Add(analisis);
                    probar = true;
                    resultado = "Entrada principal Creada";
                    resultado1 = values[0].ToString();
                }
            }
            TempData[TempDataNombre] = ListaAnalisis;
            TempData.Keep(TempDataNombre);
            if (probar == false)
            {
                resultado = "Digite un PROBLEMA O HALLAZGO, para poder actualizarlo";
            }
            return Json(new { resultado1, resultado, probar, }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult AgregarNodoHijoLluvia(List<String> values)
        {
            bool probar = false;
            string resultado = "";
            //Values 0 -> Id Nodo Padre  
            //Values 1 -> Id Nodo Hijo  
            //Values 2 -> TempData 
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                resultado = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { resultado, probar },
                JsonRequestBehavior.AllowGet);
            }
            string TempDataNombre = values[2];
            int Id_Padre = -1;
            bool ProbarSeleccion = false;
            ProbarSeleccion = int.TryParse(values[0], out Id_Padre);
            if (ProbarSeleccion == false)
            {
                TempData.Keep(TempDataNombre);
                resultado = "Primero seleccione un elemento de la estructura de ideas, escriba la idea para agregar y haga click en 'Agregar Idea'";
                return Json(new { resultado, probar },
                JsonRequestBehavior.AllowGet);
            }
            if (values[1].ToString() == "")
            {
                TempData.Keep(TempDataNombre);
                resultado = "Ya seleccionó una idea de la estructra de ideas, pero no ha digitado la nueva idea";
                return Json(new { resultado, probar },
                JsonRequestBehavior.AllowGet);
            }
            if (TempData[TempDataNombre] == null)
            {
                List<EDAnalisis> ListaAnalisisNuevo = new List<EDAnalisis>();
                TempData[TempDataNombre] = ListaAnalisisNuevo;
            }
            List<EDAnalisis> ListaAnalisis = (List<EDAnalisis>)TempData[TempDataNombre];
            int Nivel = LNAcciones.NivelAnalisis(ListaAnalisis, Id_Padre);
            if (Nivel >= 3)
            {
                TempData.Keep(TempDataNombre);
                resultado = "No puede agregar más de dos niveles al análisis";
                return Json(new { resultado, probar },
                JsonRequestBehavior.AllowGet);
            }
            else
            {
                if (Nivel == 2)
                {
                    int contarNodos = 0;
                    foreach (var item in ListaAnalisis)
                    {
                        if (item.Parent_Id == Id_Padre)
                        {
                            contarNodos = contarNodos + 1;
                        }
                    }
                    if (contarNodos >= 7)
                    {
                        TempData.Keep(TempDataNombre);
                        resultado = "No puede agregar más de 7 ideas a la opción seleccionada";
                        return Json(new { resultado, probar },
                        JsonRequestBehavior.AllowGet);
                    }
                }
                //Examinar si al nodo que se va agregar si es de entrada ya tiene 8 ideas
                if (Nivel == 1)
                {
                    int contarNodos = 0;
                    foreach (var item in ListaAnalisis)
                    {
                        if (item.Parent_Id == Id_Padre)
                        {
                            contarNodos = contarNodos + 1;
                        }
                    }
                    if (contarNodos >= 8)
                    {
                        TempData.Keep(TempDataNombre);
                        resultado = "No puede agregar más de 8 ideas a la opción seleccionada";
                        return Json(new { resultado, probar },
                        JsonRequestBehavior.AllowGet);
                    }
                }
            }
            if (ProbarSeleccion)
            {
                Analisis AnalisisSeleccionado = new Analisis();
                var Ana = (from s in ListaAnalisis
                           where s.Id_Analisis == Id_Padre
                           select s).FirstOrDefault<EDAnalisis>();
                int NuevoAna = 0;
                if (Ana != null)
                {
                    bool ProbarNumeroGenerado = false;
                    while (ProbarNumeroGenerado == false)
                    {
                        NuevoAna = NuevoAna + 1;
                        ProbarNumeroGenerado = true;
                        for (int i = 0; i < ListaAnalisis.Count; i++)
                        {
                            if (ListaAnalisis[i].Id_Analisis == NuevoAna)
                            {
                                ProbarNumeroGenerado = false;
                                break;
                            }
                        }
                    }
                    EDAnalisis analisis = new EDAnalisis();
                    analisis.Id_Analisis = NuevoAna;
                    analisis.Tipo = 4;
                    analisis.ValorTxt = values[1].ToString();
                    analisis.Parent_Id = Id_Padre;
                    ListaAnalisis.Add(analisis);
                    probar = true;
                    resultado = "Idea agregada: " + values[1].ToString();
                }
                else
                {
                    if (TempData[TempDataNombre] != null)
                    {
                        TempData.Keep(TempDataNombre);
                    }
                    resultado = "Primero seleccione un elemento de la estructura de ideas, escriba la idea para agregar y haga click en 'Agregar Idea'";
                    return Json(new { resultado, probar },
                    JsonRequestBehavior.AllowGet);
                }
            }
            TempData[TempDataNombre] = ListaAnalisis;
            TempData.Keep(TempDataNombre);
            return Json(new { resultado, probar },
                JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult EliminarNodoHijoLluvia(List<String> values)
        {
            bool probar = false;
            string resultado = "";
            //Values 0 -> Id Nodo Padre  
            //Values 1 -> Tempdata
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                resultado = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
            }
            string TempDataNombre = values[1];
            int Id_Padre = -1;
            bool ProbarSeleccion = false;
            ProbarSeleccion = int.TryParse(values[0], out Id_Padre);
            if (ProbarSeleccion == false)
            {
                TempData.Keep(TempDataNombre);
                resultado = "No ha seleccionado una idea para eliminar";
                return Json(new { resultado, probar },
                JsonRequestBehavior.AllowGet);
            }
            if (TempData[TempDataNombre] == null)
            {
                List<EDAnalisis> ListaAnalisisNuevo = new List<EDAnalisis>();
                TempData[TempDataNombre] = ListaAnalisisNuevo;
            }
            List<EDAnalisis> ListaAnalisis = (List<EDAnalisis>)TempData[TempDataNombre];
            if (ProbarSeleccion)
            {
                EDAnalisis AnalisisSeleccionado = new EDAnalisis();

                var Ana = (from s in ListaAnalisis
                           where s.Id_Analisis == Id_Padre
                           select s).FirstOrDefault<EDAnalisis>();
                if (Ana != null)
                {
                    if (Ana.Parent_Id != 0)
                    {
                        //Eliminar Nodos Recursivamente
                        List<int> ListaEliminar = new List<int>();
                        ListaEliminar.Add(Id_Padre);
                        TempData["BorrarLluvia"] = ListaEliminar;
                        //Llamado a Buscar lista de eliminados
                        EliminarNodosRecLluvia(ListaAnalisis, Id_Padre);
                        ListaEliminar = (List<int>)TempData["BorrarLluvia"];
                        //Eliminar según lista
                        for (int i = 0; i < ListaEliminar.Count; i++)
                        {
                            ListaAnalisis.RemoveAll(s => s.Id_Analisis == ListaEliminar[i]);
                        }
                        TempData.Remove("BorrarLluvia");
                        resultado = "Idea eliminada";
                        probar = true;
                    }
                    else
                    {
                        ListaAnalisis.RemoveAll(s => s.Fk_Id_Accion == Ana.Fk_Id_Accion);
                        resultado = "Idea eliminada";
                        probar = true;
                    }
                }
                else
                {
                    TempData.Keep(TempDataNombre);
                    resultado = "No ha seleccionado una idea para eliminar";
                    return Json(new { resultado, probar },
                    JsonRequestBehavior.AllowGet);
                }
            }
            TempData[TempDataNombre] = ListaAnalisis;
            TempData.Keep(TempDataNombre);
            return Json(new { resultado, probar },
                JsonRequestBehavior.AllowGet);
        }
        private void EliminarNodosRecLluvia(List<EDAnalisis> ListaAnalisis, int ParentId)
        {
            List<int> ListaBorrar = new List<int>();
            foreach (var i in ListaAnalisis.Where(a => a.Parent_Id.Equals(ParentId)))
            {
                int IdPadreSig = i.Id_Analisis;
                EliminarNodosRec(ListaAnalisis, IdPadreSig);
                //Captar Lista de Nodos a Eliminar
                List<int> ListaEliminar = (List<int>)TempData["BorrarLluvia"];
                ListaEliminar.Add(i.Id_Analisis);
                TempData["BorrarLluvia"] = ListaEliminar;
            }
        }
        [HttpPost]
        public JsonResult GuardarAnLluvia(List<String> values)
        {
            bool probar = false;
            string mensaje = "Guardado de análisis exitoso";
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                mensaje = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { url = Url.Action("Index", "Acciones"), probar, mensaje },
            JsonRequestBehavior.AllowGet);
            }
            //Values 0 -> Tempdata
            //Values 1 -> Tempdata origen
            string TempDataOrigen = values[0];
            string TempDataNombre = values[1];
            try
            {
                List<EDAnalisis> ListaAnalisis = new List<EDAnalisis>();
                TempData.Keep(TempDataNombre);
                TempData.Keep(TempDataOrigen);
                ListaAnalisis = (List<EDAnalisis>)TempData[TempDataNombre];
                if (ListaAnalisis.Count == 0)
                {
                    TempData.Keep(TempDataOrigen);
                    probar = false;
                    mensaje = "No hay elemento que guardar de este análisis";
                }
                List<EDAnalisis> ListaAnalisisGuardar = new List<EDAnalisis>();
                foreach (var item in ListaAnalisis)
                {
                    EDAnalisis AnalisisCopiar = new EDAnalisis();
                    if (item.Tipo != 4)
                    {
                        AnalisisCopiar.Tipo = 4;
                    }
                    else
                    {
                        AnalisisCopiar.Tipo = item.Tipo;
                    }
                    if (item.ValorTxt == "" || item.ValorTxt == null)
                    {
                        AnalisisCopiar.ValorTxt = "Elemento sin Valor";
                    }
                    else
                    {
                        AnalisisCopiar.ValorTxt = item.ValorTxt;
                    }
                    AnalisisCopiar.Parent_Id = item.Parent_Id;
                    AnalisisCopiar.Id_Analisis = item.Id_Analisis;
                    if (item.Pk_Id_Analisis != 0)
                    {
                        AnalisisCopiar.Pk_Id_Analisis = item.Pk_Id_Analisis;
                    }
                    ListaAnalisisGuardar.Add(AnalisisCopiar);
                }
                if (TempDataOrigen != "NuevaAccion")
                {
                    var AccionSession = (EDAccion)TempData[TempDataOrigen];
                    int IdAccion = AccionSession.Pk_Id_Accion;
                    List<EDAnalisis> ListaActual = LNAcciones.ListaAnalisis(IdAccion);
                    List<EDAnalisis> ListaBorrar = new List<EDAnalisis>();
                    List<EDAnalisis> ListaActualizar = new List<EDAnalisis>();
                    foreach (var item in ListaActual)
                    {
                        bool Actualizar = false;
                        foreach (var item1 in ListaAnalisisGuardar)
                        {
                            item1.Pk_Id_Analisis = item.Pk_Id_Analisis;
                            item1.Tipo = 4;
                            item1.Fk_Id_Accion = IdAccion;
                            ListaActualizar.Add(item1);
                            ListaAnalisisGuardar.Remove(item1);
                            Actualizar = true;
                            break;
                        }
                        if (!Actualizar)
                        {
                            ListaBorrar.Add(item);
                        }
                    }
                    if (ListaAnalisis.Count != 0)
                    {
                        probar = LNAcciones.GuardarCambiosAnalisis(ListaAnalisisGuardar, ListaActualizar, ListaBorrar, 4, IdAccion);
                    }
                    TempData[TempDataOrigen] = AccionSession;
                }
                else
                {
                    inicializar();
                    var AccionSession = Session["EDAccion"] as EDAccion;
                    List<EDAnalisis> ListaBorrar = AccionSession.AnalisisLista;
                    ListaBorrar.RemoveAll(x => x.Fk_Id_Accion == AccionSession.Pk_Id_Accion);
                    AccionSession.AnalisisLista = ListaBorrar;
                    foreach (var item in ListaAnalisisGuardar)
                    {
                        AccionSession.AnalisisLista.Add(item);
                    }
                    probar = true;
                    mensaje = "Guardado de análisis exitoso, El análisis del hallazgo se guardará junto a la acción";
                }
            }
            catch (Exception)
            {
                TempData.Keep(TempDataNombre);
                TempData.Keep(TempDataOrigen);
                probar = false;
                mensaje = "Ha ocurrido un error, por favor verifique y vuelva a intentar";
            }

            if (TempDataOrigen != "NuevaAccion")
            {
                TempData.Keep(TempDataOrigen);
                TempData.Remove(TempDataNombre);
                string Origen = TempDataOrigen.Replace("EditarAccion", "");
                return Json(new { url = Url.Action("EditarAccion", "Acciones", new { id = Origen }), probar, mensaje },
            JsonRequestBehavior.AllowGet);
            }
            else
            {
                TempData.Remove(TempDataNombre);
                return Json(new { url = Url.Action("NuevaAccion", "Acciones"), probar, mensaje },
            JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
        #region CausaEfecto
        [HttpPost]
        public JsonResult AgregarNodoPadreCausa(List<String> values)
        {
            bool probar = false;
            string resultado = "";
            string resultado1 = "";
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                resultado = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { resultado1, resultado, probar, }, JsonRequestBehavior.AllowGet);
            }
            //Values 0 -> Problema 
            //Values 1 -> TempData 
            string TempDataNombre = values[1];
            if (TempData[TempDataNombre] == null)
            {
                List<EDAnalisis> ListaAnalisisNuevo = new List<EDAnalisis>();
                TempData[TempDataNombre] = ListaAnalisisNuevo;
            }
            List<EDAnalisis> ListaAnalisis = (List<EDAnalisis>)TempData[TempDataNombre];
            if (values[0].ToString() != "")
            {
                if (ListaAnalisis.Count > 0)
                {
                    ListaAnalisis.Where(w => w.Parent_Id == 0 && w.Tipo == 2).ToList().ForEach(s => s.ValorTxt = values[0].ToString());
                    probar = true;
                    resultado = "Problema Actualizado";
                    resultado1 = values[0].ToString();
                }
                else
                {
                    EDAnalisis analisis = new EDAnalisis();
                    analisis.Id_Analisis = 1;
                    analisis.Tipo = 2;
                    analisis.ValorTxt = values[0].ToString();
                    analisis.Parent_Id = 0;
                    ListaAnalisis.Add(analisis);
                    probar = true;
                    resultado = "Problema Actualizado";
                    resultado1 = values[0].ToString();
                }
                // Verificar si existen los nodos de segundo nivel:
                // Maquinaria, MO, Medio Ambiente, Materiales, Método y Mantenimiento.
                // Si no existen agregarlos a la colección.
                List<string> ListaOpciones = new List<string>(new string[] { "Maquinaria", "Mano de Obra", "Medio Ambiente",
                "Materiales", "Método", "Mantenimiento"});
                List<int> ListaVerifica6M = new List<int>(new int[] { 0, 0, 0, 0, 0, 0 });
                int Verificar6M = 0;
                foreach (var item in ListaAnalisis)
                {
                    if (item.Parent_Id == 1)
                    {
                        for (int i = 0; i < 6; i++)
                        {
                            if (item.ValorTxt == ListaOpciones[i])
                            {
                                ListaVerifica6M[i] = ListaVerifica6M[i] + 1;
                            }
                        }
                    }
                }
                for (int i = 0; i < 6; i++)
                {
                    if (ListaVerifica6M[i] == 1)
                    {
                        Verificar6M = Verificar6M + 1;
                    }
                }
                if (Verificar6M != 6)
                {
                    for (int i = 0; i < ListaOpciones.Count; i++)
                    {
                        EDAnalisis analisis = new EDAnalisis();
                        analisis.Id_Analisis = i + 2;
                        analisis.Tipo = 2;
                        analisis.ValorTxt = ListaOpciones[i].ToString();
                        analisis.Parent_Id = 1;
                        ListaAnalisis.Add(analisis);
                    }
                }
            }
            TempData[TempDataNombre] = ListaAnalisis;
            TempData.Keep(TempDataNombre);
            if (probar == false)
            {
                resultado = "Digite un PROBLEMA O HALLAZGO antes de actualizarlo";
            }
            return Json(new { resultado1, resultado, probar },
                JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult AgregarNodoHijoCausa(List<String> values)
        {
            bool probar = false;
            string resultado = "";
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                resultado = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { resultado, probar },
                JsonRequestBehavior.AllowGet);
            }
            //Values 0 -> Id Nodo Padre  
            //Values 1 -> Id Nodo Hijo  
            //Values 2 -> TempData
            string TempDataNombre = values[2];
            int Id_Padre = -1;
            bool ProbarSeleccion = false;
            ProbarSeleccion = int.TryParse(values[0], out Id_Padre);
            if (ProbarSeleccion == false)
            {
                if (TempData[TempDataNombre] != null)
                {
                    TempData.Keep(TempDataNombre);
                }
                TempData.Keep(TempDataNombre);
                resultado = "Primero seleccione un elemento de la estructura causa-efecto, escriba la opción a agregar y haga click en 'Agregar Causa o Efecto'";
                return Json(new { resultado, probar },
                JsonRequestBehavior.AllowGet);
            }
            if (values[1].ToString() == "")
            {
                if (TempData[TempDataNombre] != null)
                {
                    TempData.Keep(TempDataNombre);
                }
                TempData.Keep(TempDataNombre);
                resultado = "Ya seleccionó una causa de la estructra causa-efecto, pero no ha digitado una causa o efecto";
                return Json(new { resultado, probar },
                JsonRequestBehavior.AllowGet);
            }
            if (TempData[TempDataNombre] == null)
            {
                List<EDAnalisis> ListaAnalisisNuevo = new List<EDAnalisis>();
                TempData[TempDataNombre] = ListaAnalisisNuevo;
            }
            List<EDAnalisis> ListaAnalisis = (List<EDAnalisis>)TempData[TempDataNombre];
            if (ProbarSeleccion)
            {
                Analisis AnalisisSeleccionado = new Analisis();

                var Ana = (from s in ListaAnalisis
                           where s.Id_Analisis == Id_Padre
                           select s).FirstOrDefault<EDAnalisis>();

                int NuevoAna = 0;
                if (Ana != null)
                {
                    if (Ana.Parent_Id == 1)
                    {
                        bool ProbarNumeroGenerado = false;
                        while (ProbarNumeroGenerado == false)
                        {
                            NuevoAna = NuevoAna + 1;
                            ProbarNumeroGenerado = true;
                            for (int i = 0; i < ListaAnalisis.Count; i++)
                            {
                                if (ListaAnalisis[i].Id_Analisis == NuevoAna)
                                {
                                    ProbarNumeroGenerado = false;
                                    break;
                                }
                            }
                        }
                        EDAnalisis EDAnalisis = new EDAnalisis();
                        EDAnalisis.Id_Analisis = NuevoAna;
                        EDAnalisis.Tipo = 2;
                        EDAnalisis.ValorTxt = values[1].ToString();
                        EDAnalisis.Parent_Id = Id_Padre;
                        ListaAnalisis.Add(EDAnalisis);
                        probar = true;
                        resultado = "Causa-Efecto agregado: " + values[1].ToString();
                    }
                    else
                    {
                        TempData.Keep(TempDataNombre);
                        resultado = "No puede agregar más niveles a este análisis, por favor elija de los siguientes recursos: maquinaria, mano de obra, medio ambiente, materiales, método o mantenimiento ";
                        return Json(new { resultado, probar },
                        JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    TempData.Keep(TempDataNombre);
                    resultado = "Primero seleccione un elemento de la estructura causa-efecto, escriba la opción a agregar y haga click en 'Agregar Causa o Efecto'";
                    return Json(new { resultado, probar },
                    JsonRequestBehavior.AllowGet);
                }
            }
            TempData[TempDataNombre] = ListaAnalisis;
            TempData.Keep(TempDataNombre);
            return Json(new { resultado, probar },
                JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult EliminarNodoHijoCausa(List<String> values)
        {
            bool probar = false;
            string resultado = "";
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                resultado = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
            }
            //Values 0 -> Id Nodo Padre  
            //Values 1 -> Tempdata 
            string TempDataNombre = values[1];
            int Id_Padre = -1;
            bool ProbarSeleccion = false;
            ProbarSeleccion = int.TryParse(values[0], out Id_Padre);
            if (ProbarSeleccion == false)
            {
                TempData.Keep(TempDataNombre);
                resultado = "No ha seleccionado una opción para eliminar";
                return Json(new { resultado, probar },
                JsonRequestBehavior.AllowGet);
            }
            if (TempData[TempDataNombre] == null)
            {
                List<EDAnalisis> ListaAnalisisNuevo = new List<EDAnalisis>();
                TempData[TempDataNombre] = ListaAnalisisNuevo;
            }
            List<EDAnalisis> ListaAnalisis = (List<EDAnalisis>)TempData[TempDataNombre];
            if (ProbarSeleccion)
            {
                Analisis AnalisisSeleccionado = new Analisis();
                var Ana = (from s in ListaAnalisis
                           where s.Id_Analisis == Id_Padre
                           select s).FirstOrDefault<EDAnalisis>();
                if (Ana != null)
                {
                    if (Ana.Parent_Id != 1 && Ana.Parent_Id != 0)
                    {
                        ListaAnalisis.RemoveAll(s => s.Id_Analisis == Id_Padre);
                        probar = true;
                        resultado = "Causa-Efecto Eliminado";
                    }
                    else
                    {
                        TempData.Keep(TempDataNombre);
                        resultado = "No puede eliminar el problema o los siguientes recursos:maquinaria, mano de obra, medio ambiente, materiales, método y mantenimiento";
                        return Json(new { resultado, probar },
                        JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    TempData.Keep(TempDataNombre);
                    resultado = "No ha seleccionado una opción para eliminar";
                    return Json(new { resultado, probar },
                    JsonRequestBehavior.AllowGet);
                }
            }
            TempData[TempDataNombre] = ListaAnalisis;
            TempData.Keep(TempDataNombre);
            return Json(new { resultado, probar },
                JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GuardarAnCausa(List<String> values)
        {
            bool probar = false;
            string mensaje = "Guardado de análisis exitoso";
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                mensaje = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { url = Url.Action("Index", "Acciones"), probar, mensaje },
            JsonRequestBehavior.AllowGet);
            }
            //Values 0 -> Tempdata
            //Values 1 -> Tempdata origen
            string TempDataOrigen = values[0];
            string TempDataNombre = values[1];
            TempData.Keep(TempDataNombre);
            TempData.Keep(TempDataOrigen);
            try
            {
                List<EDAnalisis> ListaAnalisis = new List<EDAnalisis>();
                if (TempData[TempDataNombre] == null)
                {
                    TempData[TempDataNombre] = ListaAnalisis;
                }
                ListaAnalisis = (List<EDAnalisis>)TempData[TempDataNombre];
                if (ListaAnalisis.Count == 0)
                {
                    TempData.Keep(TempDataOrigen);
                    probar = false;
                    mensaje = "No hay elemento que guardar de este análisis";
                }
                List<EDAnalisis> ListaAnalisisGuardar = new List<EDAnalisis>();
                foreach (var item in ListaAnalisis)
                {
                    EDAnalisis AnalisisCopiar = new EDAnalisis();
                    if (item.Tipo != 2)
                    {
                        AnalisisCopiar.Tipo = 2;
                    }
                    else
                    {
                        AnalisisCopiar.Tipo = item.Tipo;
                    }
                    if (item.ValorTxt == "" || item.ValorTxt == null)
                    {
                        AnalisisCopiar.ValorTxt = "Elemento sin Valor";
                    }
                    else
                    {
                        AnalisisCopiar.ValorTxt = item.ValorTxt;
                    }
                    AnalisisCopiar.Parent_Id = item.Parent_Id;
                    AnalisisCopiar.Id_Analisis = item.Id_Analisis;
                    if (item.Pk_Id_Analisis != 0)
                    {
                        AnalisisCopiar.Pk_Id_Analisis = item.Pk_Id_Analisis;
                    }
                    ListaAnalisisGuardar.Add(AnalisisCopiar);
                }
                if (TempDataOrigen != "NuevaAccion")
                {
                    var AccionSession = (EDAccion)TempData[TempDataOrigen];
                    int IdAccion = AccionSession.Pk_Id_Accion;
                    List<EDAnalisis> ListaActual = LNAcciones.ListaAnalisis(IdAccion);
                    List<EDAnalisis> ListaBorrar = new List<EDAnalisis>();
                    List<EDAnalisis> ListaActualizar = new List<EDAnalisis>();
                    foreach (var item in ListaActual)
                    {
                        bool Actualizar = false;
                        foreach (var item1 in ListaAnalisisGuardar)
                        {
                            item1.Pk_Id_Analisis = item.Pk_Id_Analisis;
                            item1.Tipo = 2;
                            item1.Fk_Id_Accion = IdAccion;
                            ListaActualizar.Add(item1);
                            ListaAnalisisGuardar.Remove(item1);
                            Actualizar = true;
                            break;
                        }
                        if (!Actualizar)
                        {
                            ListaBorrar.Add(item);
                        }
                    }
                    if (ListaAnalisis.Count != 0)
                    {
                        probar = LNAcciones.GuardarCambiosAnalisis(ListaAnalisisGuardar, ListaActualizar, ListaBorrar, 2, IdAccion);
                    }
                    TempData[TempDataOrigen] = AccionSession;
                }
                else
                {
                    inicializar();
                    var AccionSession = Session["EDAccion"] as EDAccion;
                    List<EDAnalisis> ListaBorrar = AccionSession.AnalisisLista;
                    ListaBorrar.RemoveAll(x => x.Pk_Id_Analisis == 0);
                    AccionSession.AnalisisLista = ListaBorrar;
                    foreach (var item in ListaAnalisisGuardar)
                    {
                        AccionSession.AnalisisLista.Add(item);
                    }
                    probar = true;
                    mensaje = "Guardado de análisis exitoso, El análisis del hallazgo se guardará junto a la acción";
                }
            }
            catch (Exception)
            {
                TempData.Keep(TempDataOrigen);
                probar = false;
                mensaje = "Ha ocurrido un error, por favor verifique y vuelva a intentar";
            }

            if (TempDataOrigen != "NuevaAccion")
            {
                TempData.Remove(TempDataNombre);
                string Origen = TempDataOrigen.Replace("EditarAccion", "");
                return Json(new { url = Url.Action("EditarAccion", "Acciones", new { id = Origen }), probar, mensaje },
            JsonRequestBehavior.AllowGet);
            }
            else
            {
                TempData.Remove(TempDataNombre);
                return Json(new { url = Url.Action("NuevaAccion", "Acciones"), probar, mensaje },
            JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
        #region 5porque
        [HttpPost]
        public JsonResult Generar5Porque(List<String> values)
        {
            bool probar = false;
            string resultado = "";
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                resultado = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
            }
            //Values 0 -> Problema o entrada principal
            //Values 1-35 -> Datos de la tabla
            //Values 36 -> TempData 
            string TempDataNombre = values[36];
            if (TempData[TempDataNombre] == null)
            {
                List<EDAnalisis> ListaAnalisisNuevo = new List<EDAnalisis>();
                TempData[TempDataNombre] = ListaAnalisisNuevo;
            }
            List<EDAnalisis> ListaAnalisis = new List<EDAnalisis>();
            //instanciar matriz de guardado
            string[] vecTxt = new string[36];
            int[] Padre = new int[36];
            int[] Id = new int[36];

            //verificar que exista un problema o entrada principal
            // si existe guardar el digitado
            if (values[0].Replace(" ", "") != "")
            {
                vecTxt[0] = values[0].ToString();
                values[1] = values[0].ToString();
            }
            //si no existe devolver resultado
            else
            {
                TempData.Keep(TempDataNombre);
                probar = false;
                resultado = "No ha digitado la entrada principal, por favor escribala antes de generar el diagrama";
                return Json(new { resultado, probar },
                JsonRequestBehavior.AllowGet);
            }
            //verificar filas: Si hay registros en una fila estos deben tener su correspondientes que, quien, cuando, etc.
            #region verificarFilas

            string[] ListaPreguntas = new string[7];
            ListaPreguntas[0] = "'qué'";
            ListaPreguntas[1] = "'por qué'";
            ListaPreguntas[2] = "'quién'";
            ListaPreguntas[3] = "'dónde'";
            ListaPreguntas[4] = "'cuándo'";
            ListaPreguntas[5] = "'cómo'";
            ListaPreguntas[6] = "'cuánto'";

            int PreguntasCont = 0;
            int PorqueCont = 0;
            //Primera fila
            for (int i = 1; i < 8; i++)
            {
                if (values[i].Replace(" ", "") == "")
                {
                    TempData.Keep(TempDataNombre);
                    probar = false;
                    resultado = "En la fila 'Porque 1' y Columna " + ListaPreguntas[PreguntasCont] + ".La primera fila debe estar llena antes de agregar el registro a la estructura del análisis";
                    return Json(new { resultado, probar },
                    JsonRequestBehavior.AllowGet);
                }
                PreguntasCont = PreguntasCont + 1;
            }

            if (values[2].Replace(" ", "") != "")
            {
                values[8] = values[2].ToString();
                PorqueCont = PorqueCont + 1;
            }
            else
            {
                values[8] = "";
            }
            if (values[9].Replace(" ", "") != "")
            {
                values[15] = values[9].ToString();
                PorqueCont = PorqueCont + 1;
            }
            else
            {
                values[15] = "";
            }
            if (values[16].Replace(" ", "") != "")
            {
                values[22] = values[16].ToString();
                PorqueCont = PorqueCont + 1;
            }
            else
            {
                values[22] = "";
            }
            if (values[23].Replace(" ", "") != "")
            {
                values[29] = values[23].ToString();
                PorqueCont = PorqueCont + 1;
            }
            else
            {
                values[29] = "";
            }
            if (values[30].Replace(" ", "") != "")
            {
                PorqueCont = PorqueCont + 1;
            }


            #endregion
            // guardar los que tengan una cadena no vacia en listas
            #region guardarFilas
            List<string> col_que = new List<string>();
            List<string> col_porque = new List<string>();
            List<string> col_quien = new List<string>();
            List<string> col_donde = new List<string>();
            List<string> col_cuando = new List<string>();
            List<string> col_como = new List<string>();
            List<string> col_cuanto = new List<string>();

            PreguntasCont = 0;
            for (int i = 1; i < 36; i++)
            {
                if (PreguntasCont == 0)
                {
                    col_que.Add(values[i]);
                }
                if (PreguntasCont == 1)
                {
                    col_porque.Add(values[i]);
                }
                if (PreguntasCont == 2)
                {
                    col_quien.Add(values[i]);
                }
                if (PreguntasCont == 3)
                {
                    col_donde.Add(values[i]);
                }
                if (PreguntasCont == 4)
                {
                    col_cuando.Add(values[i]);
                }
                if (PreguntasCont == 5)
                {
                    col_como.Add(values[i]);
                }
                if (PreguntasCont == 6)
                {
                    col_cuanto.Add(values[i]);
                }
                if (PreguntasCont != 6)
                {
                    PreguntasCont = PreguntasCont + 1;
                }
                else
                {
                    PreguntasCont = 0;
                }
            }

            #endregion
            //eliminar información del tempdata
            TempData.Remove(TempDataNombre);
            //reemplazar tempdata con la información de la matriz
            #region reemplazar
            PreguntasCont = 0;
            int contador_que = 0;
            int contador_porque = 0;
            int contador_quien = 0;
            int contador_donde = 0;
            int contador_cuando = 0;
            int contador_como = 0;
            int contador_cuanto = 0;

            for (int i = 1; i < 36; i++)
            {
                if (PreguntasCont == 0)
                {
                    if (col_que.Count - 1 >= contador_que)
                    {
                        vecTxt[i] = col_que[contador_que];
                        contador_que = contador_que + 1;
                    }
                    else
                    {
                        vecTxt[i] = "";
                    }
                }
                if (PreguntasCont == 1)
                {
                    if (col_porque.Count - 1 >= contador_porque)
                    {
                        vecTxt[i] = col_porque[contador_porque];
                        contador_porque = contador_porque + 1;
                    }
                    else
                    {
                        vecTxt[i] = "";
                    }
                }
                if (PreguntasCont == 2)
                {
                    if (col_quien.Count - 1 >= contador_quien)
                    {
                        vecTxt[i] = col_quien[contador_quien];
                        contador_quien = contador_quien + 1;
                    }
                    else
                    {
                        vecTxt[i] = "";
                    }
                }
                if (PreguntasCont == 3)
                {
                    if (col_donde.Count - 1 >= contador_donde)
                    {
                        vecTxt[i] = col_donde[contador_donde];
                        contador_donde = contador_donde + 1;
                    }
                    else
                    {
                        vecTxt[i] = "";
                    }
                }
                if (PreguntasCont == 4)
                {
                    if (col_cuando.Count - 1 >= contador_cuando)
                    {
                        vecTxt[i] = col_cuando[contador_cuando];
                        contador_cuando = contador_cuando + 1;
                    }
                    else
                    {
                        vecTxt[i] = "";
                    }
                }
                if (PreguntasCont == 5)
                {
                    if (col_como.Count - 1 >= contador_como)
                    {
                        vecTxt[i] = col_como[contador_como];
                        contador_como = contador_como + 1;
                    }
                    else
                    {
                        vecTxt[i] = "";
                    }
                }
                if (PreguntasCont == 6)
                {
                    if (col_cuanto.Count - 1 >= contador_cuanto)
                    {
                        vecTxt[i] = col_cuanto[contador_cuanto];
                        contador_cuanto = contador_cuanto + 1;
                    }
                    else
                    {
                        vecTxt[i] = "";
                    }
                }

                if (PreguntasCont != 6)
                {
                    PreguntasCont = PreguntasCont + 1;
                }
                else
                {
                    PreguntasCont = 0;
                }
            }
            for (int i = 0; i < 36; i++)
            {
                Id[i] = i + 1;
                if (i == 0)
                {
                    Padre[i] = 0;
                }
                else
                {
                    Padre[i] = 1;
                }

                EDAnalisis ana = new EDAnalisis();
                ana.Id_Analisis = Id[i];
                ana.Parent_Id = Padre[i];
                ana.ValorTxt = vecTxt[i];
                ana.Tipo = 3;
                ListaAnalisis.Add(ana);
            }

            #endregion
            TempData[TempDataNombre] = ListaAnalisis;
            TempData.Keep(TempDataNombre);
            if (PorqueCont == 5)
            {
                resultado = "Analisis Generado y listo para guardar";
            }
            else
            {
                resultado = "Analisis Generado, pero aún falta completar " + (5 - PorqueCont).ToString() + " 'Por qué'";
            }
            probar = true;
            //Devuelve el resultado exitosamente
            return Json(new { resultado, probar },
                JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GuardarAn5Porque(List<String> values)
        {
            bool probar = false;
            bool valinicial = true;
            string mensaje = "Guardado de análisis exitoso";
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                mensaje = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { url = Url.Action("Index", "Acciones"), probar, mensaje },
            JsonRequestBehavior.AllowGet);
            }
            //Values 0 -> Tempdata
            //Values 1 -> Tempdata origen
            string TempDataOrigen = values[0];
            string TempDataNombre = values[1];
            TempData.Keep(TempDataNombre);
            TempData.Keep(TempDataOrigen);
            try
            {
                List<EDAnalisis> ListaAnalisis = new List<EDAnalisis>();
                if (TempData[TempDataNombre] == null)
                {
                    TempData[TempDataNombre] = ListaAnalisis;
                }
                ListaAnalisis = (List<EDAnalisis>)TempData[TempDataNombre];
                if (ListaAnalisis.Count == 0)
                {
                    TempData.Keep(TempDataOrigen);
                    probar = false;
                    mensaje = "No hay elemento que guardar de este análisis";
                }
                else
                {
                    //verificar reglas 5 por que
                    if (ListaAnalisis.Count==36)
                    {
                        
                        string[] vecTxt = new string[36];
                        int cont = 0;
                        foreach (var item in ListaAnalisis)
                        {
                            vecTxt[cont] = item.ValorTxt;
                            cont = cont + 1;
                        }
                        cont = 0;
                        if (vecTxt[0].Replace(" ", "") == "")
                        {
                            TempData.Keep(TempDataOrigen);
                            probar = false;
                            valinicial = false;
                            mensaje = "Falta agregar el problema o hallazgo al análisis, por favor escriba un problema y vuelva a intentar";
                        }
                        else
                        {
                            string[] ListaPreguntas = new string[7];
                            ListaPreguntas[0] = "'qué'";
                            ListaPreguntas[1] = "'por qué'";
                            ListaPreguntas[2] = "'quién'";
                            ListaPreguntas[3] = "'dónde'";
                            ListaPreguntas[4] = "'cuándo'";
                            ListaPreguntas[5] = "'cómo'";
                            ListaPreguntas[6] = "'cuánto'";
                            for (int i = 1; i < 8; i++)
                            {
                                if (vecTxt[i].Replace(" ", "") == "")
                                {
                                    TempData.Keep(TempDataNombre);
                                    valinicial = false;
                                    probar = false;
                                    mensaje = "En la fila 'Porque 1' y Columna " + ListaPreguntas[cont] + ".La primera fila debe estar llena antes de agregar el registro a la estructura del análisis";
                                    break;
                                }
                            }
                        }
                        if (valinicial)
                        {
                            int[] valporque = new int[4] {9,16,23,30};
                            for (int i = 0; i < 4; i++)
                            {
                                if (vecTxt[valporque[i]].Replace(" ", "") == "")
                                {
                                    TempData.Keep(TempDataNombre);
                                    valinicial = false;
                                    probar = false;
                                    mensaje = "Falta completar uno de los cinco 'por qué', por favor complete la información y vuelva a intentar ";
                                    break;
                                }
                            }
                            
                        }
                    }
                    else
                    {

                    }



                }
                List<EDAnalisis> ListaAnalisisGuardar = new List<EDAnalisis>();
                foreach (var item in ListaAnalisis)
                {
                    EDAnalisis AnalisisCopiar = new EDAnalisis();
                    if (item.Tipo != 3)
                    {
                        AnalisisCopiar.Tipo = 3;
                    }
                    else
                    {
                        AnalisisCopiar.Tipo = item.Tipo;
                    }
                    if (item.ValorTxt == "" || item.ValorTxt == null)
                    {
                        AnalisisCopiar.ValorTxt = "NullValueString";
                    }
                    else
                    {
                        AnalisisCopiar.ValorTxt = item.ValorTxt;
                    }
                    AnalisisCopiar.Parent_Id = item.Parent_Id;
                    AnalisisCopiar.Id_Analisis = item.Id_Analisis;
                    if (item.Pk_Id_Analisis != 0)
                    {
                        AnalisisCopiar.Pk_Id_Analisis = item.Pk_Id_Analisis;
                    }
                    ListaAnalisisGuardar.Add(AnalisisCopiar);
                }
                if (valinicial)
                {

                
                if (TempDataOrigen != "NuevaAccion")
                {
                    var AccionSession = (EDAccion)TempData[TempDataOrigen];
                    int IdAccion = AccionSession.Pk_Id_Accion;
                    List<EDAnalisis> ListaActual = LNAcciones.ListaAnalisis(IdAccion);
                    List<EDAnalisis> ListaBorrar = new List<EDAnalisis>();
                    List<EDAnalisis> ListaActualizar = new List<EDAnalisis>();
                    foreach (var item in ListaActual)
                    {
                        bool Actualizar = false;
                        foreach (var item1 in ListaAnalisisGuardar)
                        {
                            item1.Pk_Id_Analisis = item.Pk_Id_Analisis;
                            item1.Tipo = 3;
                            item1.Fk_Id_Accion = IdAccion;
                            ListaActualizar.Add(item1);
                            ListaAnalisisGuardar.Remove(item1);
                            Actualizar = true;
                            break;
                        }
                        if (!Actualizar)
                        {
                            ListaBorrar.Add(item);
                        }
                    }
                    if (ListaAnalisis.Count != 0)
                    {
                        probar = LNAcciones.GuardarCambiosAnalisis(ListaAnalisisGuardar, ListaActualizar, ListaBorrar, 3, IdAccion);
                    }
                    TempData[TempDataOrigen] = AccionSession;
                }
                else
                {
                    inicializar();
                    var AccionSession = Session["EDAccion"] as EDAccion;
                    List<EDAnalisis> ListaBorrar = AccionSession.AnalisisLista;
                    ListaBorrar.RemoveAll(x => x.Pk_Id_Analisis == 0);
                    AccionSession.AnalisisLista = ListaBorrar;
                    foreach (var item in ListaAnalisisGuardar)
                    {
                        AccionSession.AnalisisLista.Add(item);
                    }
                    probar = true;
                    mensaje = "Guardado de análisis exitoso, El análisis del hallazgo se guardará junto a la acción";
                }
                }
            }
            catch (Exception)
            {
                TempData.Keep(TempDataOrigen);
                probar = false;
                mensaje = "Ha ocurrido un error, por favor verifique y vuelva a intentar";
            }

            if (TempDataOrigen != "NuevaAccion")
            {
                TempData.Remove(TempDataNombre);
                string Origen = TempDataOrigen.Replace("EditarAccion", "");
                return Json(new { url = Url.Action("EditarAccion", "Acciones", new { id = Origen }), probar, mensaje },
            JsonRequestBehavior.AllowGet);
            }
            else
            {
                TempData.Remove(TempDataNombre);
                return Json(new { url = Url.Action("NuevaAccion", "Acciones"), probar, mensaje },
            JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult GuardarAn5Porque1(List<String> values)
        {
            bool probar = false;
            string mensaje = "Guardado de análisis exitoso";

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                mensaje = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { url = Url.Action("Index", "Acciones"), probar, mensaje },
            JsonRequestBehavior.AllowGet);
            }
            //Values 0 -> Problema o entrada principal
            //Values 1-35 -> Datos de la tabla
            //Values 36 - 37 -> TempData y TempDataOrigen
            string TempDataNombre = values[37];
            string TempDataOrigen = values[36];
            try
            {
                
                if (TempData[TempDataNombre] == null)
                {
                    List<EDAnalisis> ListaAnalisisNuevo = new List<EDAnalisis>();
                    TempData[TempDataNombre] = ListaAnalisisNuevo;
                }
                List<EDAnalisis> ListaAnalisis = new List<EDAnalisis>();
                //instanciar matriz de guardado
                string[] vecTxt = new string[36];
                int[] Padre = new int[36];
                int[] Id = new int[36];
                //verificar que exista un problema o entrada principal
                // si existe guardar el digitado
                if (values[0].Replace(" ", "") != "")
                {
                    vecTxt[0] = values[0].ToString();
                    values[1] = values[0].ToString();
                }
                //si no existe devolver resultado
                else
                {
                    TempData.Keep(TempDataOrigen);
                    probar = false;
                    mensaje = "Falta agregar el problema o hallazgo al análisis, por favor escriba un problema y vuelva a intentar";

                    if (TempDataOrigen != "NuevaAccion")
                    {
                        TempData.Remove(TempDataNombre);
                        string Origen = TempDataOrigen.Replace("EditarAccion", "");
                        return Json(new { url = Url.Action("EditarAccion", "Acciones", new { id = Origen }), probar, mensaje },
                    JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        TempData.Remove(TempDataNombre);
                        return Json(new { url = Url.Action("NuevaAccion", "Acciones"), probar, mensaje },
                    JsonRequestBehavior.AllowGet);
                    }
                }
                //verificar filas: Si hay registros en una fila estos deben tener su correspondientes que, quien, cuando, etc.
                #region verificarFilas

                string[] ListaPreguntas = new string[7];
                ListaPreguntas[0] = "'qué'";
                ListaPreguntas[1] = "'por qué'";
                ListaPreguntas[2] = "'quién'";
                ListaPreguntas[3] = "'dónde'";
                ListaPreguntas[4] = "'cuándo'";
                ListaPreguntas[5] = "'cómo'";
                ListaPreguntas[6] = "'cuánto'";

                int PreguntasCont = 0;
                int PorqueCont = 0;
                //Primera fila
                for (int i = 1; i < 8; i++)
                {
                    if (values[i].Replace(" ", "") == "")
                    {
                        TempData.Keep(TempDataOrigen);
                        probar = false;
                        mensaje = "En la fila 'Porque 1' y Columna " + ListaPreguntas[PreguntasCont] + ".La primera fila debe estar llena antes de agregar el registro a la estructura del análisis";
                        if (TempDataOrigen != "NuevaAccion")
                        {
                            TempData.Remove(TempDataNombre);
                            string Origen = TempDataOrigen.Replace("EditarAccion", "");
                            return Json(new { url = Url.Action("EditarAccion", "Acciones", new { id = Origen }), probar, mensaje },
                        JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            TempData.Remove(TempDataNombre);
                            return Json(new { url = Url.Action("NuevaAccion", "Acciones"), probar, mensaje },
                        JsonRequestBehavior.AllowGet);
                        }
                    }
                    PreguntasCont = PreguntasCont + 1;
                }

                int[] valporque = new int[4] { 9, 16, 23, 30 };
                for (int i = 0; i < 4; i++)
                {
                    if (values[valporque[i]].Replace(" ", "") == "")
                    {
                        TempData.Keep(TempDataNombre);
                        probar = false;
                        mensaje = "Falta completar uno de los cinco 'por qué', por favor complete la información y vuelva a intentar ";

                        if (TempDataOrigen != "NuevaAccion")
                        {
                            TempData.Remove(TempDataNombre);
                            string Origen = TempDataOrigen.Replace("EditarAccion", "");
                            return Json(new { url = Url.Action("EditarAccion", "Acciones", new { id = Origen }), probar, mensaje },
                        JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            TempData.Remove(TempDataNombre);
                            return Json(new { url = Url.Action("NuevaAccion", "Acciones"), probar, mensaje },
                        JsonRequestBehavior.AllowGet);
                        }
                    }
                }

                if (values[2].Replace(" ", "") != "")
                {
                    values[8] = values[2].ToString();
                    PorqueCont = PorqueCont + 1;
                }
                else
                {
                    values[8] = "";
                }
                if (values[9].Replace(" ", "") != "")
                {
                    values[15] = values[9].ToString();
                    PorqueCont = PorqueCont + 1;
                }
                else
                {
                    values[15] = "";
                }
                if (values[16].Replace(" ", "") != "")
                {
                    values[22] = values[16].ToString();
                    PorqueCont = PorqueCont + 1;
                }
                else
                {
                    values[22] = "";
                }
                if (values[23].Replace(" ", "") != "")
                {
                    values[29] = values[23].ToString();
                    PorqueCont = PorqueCont + 1;
                }
                else
                {
                    values[29] = "";
                }
                if (values[30].Replace(" ", "") != "")
                {
                    PorqueCont = PorqueCont + 1;
                }



                #endregion
                // guardar los que tengan una cadena no vacia en listas
                #region guardarFilas
                List<string> col_que = new List<string>();
                List<string> col_porque = new List<string>();
                List<string> col_quien = new List<string>();
                List<string> col_donde = new List<string>();
                List<string> col_cuando = new List<string>();
                List<string> col_como = new List<string>();
                List<string> col_cuanto = new List<string>();

                PreguntasCont = 0;
                for (int i = 1; i < 36; i++)
                {
                    if (PreguntasCont == 0)
                    {
                        col_que.Add(values[i]);
                    }
                    if (PreguntasCont == 1)
                    {
                        col_porque.Add(values[i]);
                    }
                    if (PreguntasCont == 2)
                    {
                        col_quien.Add(values[i]);
                    }
                    if (PreguntasCont == 3)
                    {
                        col_donde.Add(values[i]);
                    }
                    if (PreguntasCont == 4)
                    {
                        col_cuando.Add(values[i]);
                    }
                    if (PreguntasCont == 5)
                    {
                        col_como.Add(values[i]);
                    }
                    if (PreguntasCont == 6)
                    {
                        col_cuanto.Add(values[i]);
                    }
                    if (PreguntasCont != 6)
                    {
                        PreguntasCont = PreguntasCont + 1;
                    }
                    else
                    {
                        PreguntasCont = 0;
                    }
                }
                #endregion
                //eliminar información del tempdata
                TempData.Remove(TempDataNombre);
                //reemplazar tempdata con la información de la matriz
                #region reemplazar
                PreguntasCont = 0;
                int contador_que = 0;
                int contador_porque = 0;
                int contador_quien = 0;
                int contador_donde = 0;
                int contador_cuando = 0;
                int contador_como = 0;
                int contador_cuanto = 0;

                for (int i = 1; i < 36; i++)
                {
                    if (PreguntasCont == 0)
                    {
                        if (col_que.Count - 1 >= contador_que)
                        {
                            vecTxt[i] = col_que[contador_que];
                            contador_que = contador_que + 1;
                        }
                        else
                        {
                            vecTxt[i] = "";
                        }
                    }
                    if (PreguntasCont == 1)
                    {
                        if (col_porque.Count - 1 >= contador_porque)
                        {
                            vecTxt[i] = col_porque[contador_porque];
                            contador_porque = contador_porque + 1;
                        }
                        else
                        {
                            vecTxt[i] = "";
                        }
                    }
                    if (PreguntasCont == 2)
                    {
                        if (col_quien.Count - 1 >= contador_quien)
                        {
                            vecTxt[i] = col_quien[contador_quien];
                            contador_quien = contador_quien + 1;
                        }
                        else
                        {
                            vecTxt[i] = "";
                        }
                    }
                    if (PreguntasCont == 3)
                    {
                        if (col_donde.Count - 1 >= contador_donde)
                        {
                            vecTxt[i] = col_donde[contador_donde];
                            contador_donde = contador_donde + 1;
                        }
                        else
                        {
                            vecTxt[i] = "";
                        }
                    }
                    if (PreguntasCont == 4)
                    {
                        if (col_cuando.Count - 1 >= contador_cuando)
                        {
                            vecTxt[i] = col_cuando[contador_cuando];
                            contador_cuando = contador_cuando + 1;
                        }
                        else
                        {
                            vecTxt[i] = "";
                        }
                    }
                    if (PreguntasCont == 5)
                    {
                        if (col_como.Count - 1 >= contador_como)
                        {
                            vecTxt[i] = col_como[contador_como];
                            contador_como = contador_como + 1;
                        }
                        else
                        {
                            vecTxt[i] = "";
                        }
                    }
                    if (PreguntasCont == 6)
                    {
                        if (col_cuanto.Count - 1 >= contador_cuanto)
                        {
                            vecTxt[i] = col_cuanto[contador_cuanto];
                            contador_cuanto = contador_cuanto + 1;
                        }
                        else
                        {
                            vecTxt[i] = "";
                        }
                    }

                    if (PreguntasCont != 6)
                    {
                        PreguntasCont = PreguntasCont + 1;
                    }
                    else
                    {
                        PreguntasCont = 0;
                    }
                }
                for (int i = 0; i < 36; i++)
                {
                    Id[i] = i + 1;
                    if (i == 0)
                    {
                        Padre[i] = 0;
                    }
                    else
                    {
                        Padre[i] = 1;
                    }

                    EDAnalisis ana = new EDAnalisis();
                    ana.Id_Analisis = Id[i];
                    ana.Parent_Id = Padre[i];
                    ana.ValorTxt = vecTxt[i];
                    ana.Tipo = 3;
                    ListaAnalisis.Add(ana);
                }

                #endregion
                TempData[TempDataNombre] = ListaAnalisis;
                if (ListaAnalisis.Count == 0)
                {
                    TempData.Keep(TempDataOrigen);
                    probar = false;
                    mensaje = "No hay elemento que guardar de este análisis";
                }
                List<EDAnalisis> ListaAnalisisGuardar = new List<EDAnalisis>();
                foreach (var item in ListaAnalisis)
                {
                    EDAnalisis AnalisisCopiar = new EDAnalisis();
                    if (item.Tipo != 3)
                    {
                        AnalisisCopiar.Tipo = 3;
                    }
                    else
                    {
                        AnalisisCopiar.Tipo = item.Tipo;
                    }
                    if (item.ValorTxt == "" || item.ValorTxt == null)
                    {
                        AnalisisCopiar.ValorTxt = "NullValueString";
                    }
                    else
                    {
                        AnalisisCopiar.ValorTxt = item.ValorTxt;
                    }
                    AnalisisCopiar.Parent_Id = item.Parent_Id;
                    AnalisisCopiar.Id_Analisis = item.Id_Analisis;
                    if (item.Pk_Id_Analisis != 0)
                    {
                        AnalisisCopiar.Pk_Id_Analisis = item.Pk_Id_Analisis;
                    }
                    ListaAnalisisGuardar.Add(AnalisisCopiar);
                }
                    if (TempDataOrigen != "NuevaAccion")
                    {
                        var AccionSession = (EDAccion)TempData[TempDataOrigen];
                        int IdAccion = AccionSession.Pk_Id_Accion;
                        List<EDAnalisis> ListaActual = LNAcciones.ListaAnalisis(IdAccion);
                        List<EDAnalisis> ListaBorrar = new List<EDAnalisis>();
                        List<EDAnalisis> ListaActualizar = new List<EDAnalisis>();
                        foreach (var item in ListaActual)
                        {
                            bool Actualizar = false;
                            foreach (var item1 in ListaAnalisisGuardar)
                            {
                                item1.Pk_Id_Analisis = item.Pk_Id_Analisis;
                                item1.Tipo = 3;
                                item1.Fk_Id_Accion = IdAccion;
                                ListaActualizar.Add(item1);
                                ListaAnalisisGuardar.Remove(item1);
                                Actualizar = true;
                                break;
                            }
                            if (!Actualizar)
                            {
                                ListaBorrar.Add(item);
                            }
                        }
                        if (ListaAnalisis.Count != 0)
                        {
                            probar = LNAcciones.GuardarCambiosAnalisis(ListaAnalisisGuardar, ListaActualizar, ListaBorrar, 3, IdAccion);
                        }
                        TempData[TempDataOrigen] = AccionSession;
                    }
                    else
                    {
                        inicializar();
                        var AccionSession = Session["EDAccion"] as EDAccion;
                        List<EDAnalisis> ListaBorrar = AccionSession.AnalisisLista;
                        ListaBorrar.RemoveAll(x => x.Pk_Id_Analisis == 0);
                        AccionSession.AnalisisLista = ListaBorrar;
                        foreach (var item in ListaAnalisisGuardar)
                        {
                            AccionSession.AnalisisLista.Add(item);
                        }
                        probar = true;
                        mensaje = "Guardado de análisis exitoso, El análisis del hallazgo se guardará junto a la acción";
                    }
            }
            catch (Exception)
            {
                TempData.Keep(TempDataOrigen);
                probar = false;
                mensaje = "Ha ocurrido un error, por favor verifique y vuelva a intentar";
            }
            if (TempDataOrigen != "NuevaAccion")
            {
                TempData.Remove(TempDataNombre);
                string Origen = TempDataOrigen.Replace("EditarAccion", "");
                return Json(new { url = Url.Action("EditarAccion", "Acciones", new { id = Origen }), probar, mensaje },
            JsonRequestBehavior.AllowGet);
            }
            else
            {
                TempData.Remove(TempDataNombre);
                return Json(new { url = Url.Action("NuevaAccion", "Acciones"), probar, mensaje },
            JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #endregion
        #region Archivos
        [HttpGet]
        public ActionResult Archivos()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }
            inicializar();
            var AccionSession = Session["EDAccion"] as EDAccion;
            return View();
        }
        [HttpGet]
        public ActionResult ArchivosEd(string Edicion)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }
            ViewBag.EdicionKey = Edicion;
            List<EDArchivosAcciones> ListaArchivos = new List<EDArchivosAcciones>();
            var AccionSession = TempData[Edicion] as EDAccion;
            ListaArchivos = AccionSession.ArchivosLista;
            return View(ListaArchivos);
        }
        [HttpPost]
        public ActionResult UploadFiles()
        {
            bool probar = false;
            string resultado = "";
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                resultado = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { probar, resultado });
            }
            if (Request.Files.Count > 0)
            {
                try
                {
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        string fname;
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }
                        inicializar();
                        var AccionSession = Session["EDAccion"] as EDAccion;
                        EDArchivosAcciones Archivo = new EDArchivosAcciones();
                        string RandomStrName = FechaString() + RandomString(3);
                        Archivo.Pk_Id_Archivo = 1;
                        Archivo.NombreArchivo = "File_" + RandomStrName + "_" + file.FileName;
                        int tamañobytes = file.ContentLength;
                        Archivo.Tamaño = (tamañobytes / 1000).ToString() + " " + "KB";
                        Archivo.Extension = Path.GetExtension(file.FileName);
                        Archivo.Ruta = RutaArchivosTemporales;
                        try
                        {
                            CrearCarpeta(Archivo.Ruta);
                            file.SaveAs(Server.MapPath(Path.Combine(Archivo.Ruta, Archivo.NombreArchivo)));
                            AccionSession.ArchivosLista.Add(Archivo);
                        }
                        catch (Exception ex)
                        {
                            string mensaje = ex.ToString();
                        }
                        foreach (var item in AccionSession.ArchivosLista)
                        {
                            int NumeroArchivo = 0;
                            string RandomStr = RandomString(9);
                            bool ProbarRandom = int.TryParse(RandomStr, out NumeroArchivo);
                            while (ProbarRandom == false)
                            {
                                ProbarRandom = int.TryParse(RandomStr, out NumeroArchivo);
                            }
                            item.IdFile = NumeroArchivo;
                        }
                        Session["EDAccion"] = AccionSession;
                    }
                    probar = true;
                    return Json(new { probar, resultado });
                }
                catch (Exception)
                {
                    resultado = "No se pudo completar la operación, el tamaño del archivo probablemente es mayor a 4 MB ";
                    return Json(new { probar, resultado });
                }
            }
            else
            {
                resultado = "No ha seleccionado un archivo, antes de adjuntar asegurese que exista un archivo";
                return Json(new { probar, resultado });
            }
        }
        [HttpPost]
        public JsonResult EliminarArchivo(string Values)
        {

            bool probar = false;
            bool probarEncuentraArchivo = true;
            string resultado = "";
            string NombreArchivo = "";
            string RutaArchivo = "";

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                resultado = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { probar, resultado });
            }

            int numeroID = 0;

            if (int.TryParse(Values, out numeroID))
            {
                inicializar();
                var AccionSession = Session["EDAccion"] as EDAccion;
                var archivos = AccionSession.ArchivosLista;
                try
                {
                    EDArchivosAcciones archivo = archivos.Find(s => s.IdFile == numeroID);
                    if (archivo == null)
                    {
                        probar = false;
                        probarEncuentraArchivo = false;
                        resultado = "El archivo no se encontró";

                        return Json(new { probar, resultado, probarEncuentraArchivo },
                JsonRequestBehavior.AllowGet);
                    }
                    NombreArchivo = archivo.NombreArchivo;
                    RutaArchivo = archivo.Ruta;

                }
                catch (Exception)
                {
                    probar = false;
                    probarEncuentraArchivo = false;
                    resultado = "El archivo no se encontró";

                    return Json(new { probar, resultado, probarEncuentraArchivo },
            JsonRequestBehavior.AllowGet);
                }
                archivos.RemoveAll(s => s.IdFile == numeroID);
                try
                {
                    System.IO.File.Delete(Server.MapPath(Path.Combine(RutaArchivo, NombreArchivo)));
                }
                catch (Exception)
                {
                }
                probar = true;
                resultado = "Se eliminó el archivo correctamente";
                foreach (var item in archivos)
                {
                    int NumeroArchivo = 0;
                    string RandomStr = RandomString(9);
                    bool ProbarRandom = int.TryParse(RandomStr, out NumeroArchivo);
                    while (ProbarRandom == false)
                    {
                        ProbarRandom = int.TryParse(RandomStr, out NumeroArchivo);
                    }
                    item.IdFile = NumeroArchivo;
                }
            }
            else
            {
                resultado = "No se eliminó el archivo, por favor vuelva a intentar";
            }
            //Devuelve el resultado exitosamente
            return Json(new { probar, resultado },
            JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult UploadFilesEd(string TempdataID)
        {
            bool probar = false;
            string resultado = "";
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                resultado = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { probar, resultado });
            }
            if (Request.Files.Count > 0)
            {
                try
                {
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        string fname;
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }
                        var AccionSession = (EDAccion)TempData[TempdataID];
                        EDArchivosAcciones Archivo = new EDArchivosAcciones();
                        string RandomStrName = FechaString() + RandomString(3);
                        Archivo.IdFile = 1;
                        Archivo.NombreArchivo = "File_" + RandomStrName + "_" + file.FileName;
                        Archivo.Extension = Path.GetExtension(file.FileName);
                        Archivo.Ruta = RutaArchivosBD;
                        Archivo.Estado = 0;
                        Archivo.Fk_Id_Accion = AccionSession.Pk_Id_Accion;
                        Archivo.Pk_Id_Archivo = 0;
                        try
                        {
                            bool probarGuardado = LNAcciones.GuardarArchivosAccion(Archivo);
                            if (probarGuardado)
                            {
                                Archivo.Pk_Id_Archivo = LNAcciones.UltimoIdArchivo(AccionSession.Pk_Id_Accion, usuarioActual.IdEmpresa);
                                CrearCarpeta(Archivo.Ruta);
                                file.SaveAs(Server.MapPath(Path.Combine(Archivo.Ruta, Archivo.NombreArchivo)));
                                AccionSession.ArchivosLista.Add(Archivo);
                            }
                        }
                        catch (Exception ex)
                        {
                            string mensaje = ex.ToString();
                        }
                        foreach (var item in AccionSession.ArchivosLista)
                        {
                            int NumeroArchivo = 0;
                            string RandomStr = RandomString(9);
                            bool ProbarRandom = int.TryParse(RandomStr, out NumeroArchivo);
                            while (ProbarRandom == false)
                            {
                                ProbarRandom = int.TryParse(RandomStr, out NumeroArchivo);
                            }
                            item.IdFile = NumeroArchivo;
                        }
                        TempData[TempdataID] = AccionSession;
                    }
                    probar = true;
                    return Json(new { probar, resultado });
                }
                catch (Exception)
                {
                    TempData.Keep(TempdataID);
                    resultado = "No se pudo completar la operación, el tamaño del archivo probablemente es mayor a 4 MB ";
                    return Json(new { probar, resultado });
                }
            }
            else
            {
                TempData.Keep(TempdataID);
                resultado = "No ha seleccionado un archivo, antes de adjuntar asegurese que exista un archivo";
                return Json(new { probar, resultado });
            }
        }
        [HttpPost]
        public JsonResult EliminarArchivoEd(List<String> values)
        {
            //values[0] IdArchivo
            //values[1] TempDataId
            bool probar = false;
            bool probarEncuentraArchivo = true;
            string resultado = "";
            string NombreArchivo = "";
            string RutaArchivo = "";
            int numeroID = 0;

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                resultado = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { probar, resultado });
            }

            if (int.TryParse(values[0], out numeroID))
            {
                inicializar();
                var AccionSession = (EDAccion)TempData[values[1]];
                var archivos = AccionSession.ArchivosLista;
                EDArchivosAcciones EDArchivosAcciones = new EDArchivosAcciones();
                try
                {
                    EDArchivosAcciones archivo = archivos.Find(s => s.IdFile == numeroID);
                    if (archivo == null)
                    {
                        probar = false;
                        probarEncuentraArchivo = false;
                        resultado = "El archivo no se encontró";
                        TempData.Keep(values[1]);
                        return Json(new { probar, resultado, probarEncuentraArchivo },
                JsonRequestBehavior.AllowGet);
                    }
                    NombreArchivo = archivo.NombreArchivo;
                    RutaArchivo = archivo.Ruta;
                    EDArchivosAcciones = archivo;
                }
                catch (Exception)
                {
                    probar = false;
                    probarEncuentraArchivo = false;
                    resultado = "El archivo no se encontró";
                    TempData.Keep(values[1]);
                    return Json(new { probar, resultado, probarEncuentraArchivo },
            JsonRequestBehavior.AllowGet);
                }


                bool ProbarEliminar = LNAcciones.EliminarArchivos(EDArchivosAcciones);
                if (ProbarEliminar)
                {
                    archivos.RemoveAll(s => s.IdFile == numeroID);
                    try
                    {
                        System.IO.File.Delete(Server.MapPath(Path.Combine(RutaArchivo, NombreArchivo)));
                    }
                    catch (Exception)
                    {
                    }
                }

                AccionSession.ArchivosLista = archivos;
                TempData[values[1]] = AccionSession;

                probar = true;
                resultado = "Se eliminó el archivo correctamente";
                foreach (var item in archivos)
                {
                    int NumeroArchivo = 0;
                    string RandomStr = RandomString(9);
                    bool ProbarRandom = int.TryParse(RandomStr, out NumeroArchivo);
                    while (ProbarRandom == false)
                    {
                        ProbarRandom = int.TryParse(RandomStr, out NumeroArchivo);
                    }
                    item.IdFile = NumeroArchivo;
                }
            }
            else
            {
                resultado = "No se eliminó el archivo, por favor vuelva a intentar";
            }
            //Devuelve el resultado exitosamente
            return Json(new { probar, resultado },
            JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult DescargarArchivoEd(List<String> values)
        {
            //values[0] IdArchivo
            //values[1] TempDataId
            bool probar = false;
            bool probarEncuentraArchivo = true;
            string resultado = "";
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                resultado = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { probar, resultado });
            }
            int numeroID = 0;

            if (int.TryParse(values[0], out numeroID))
            {
                inicializar();
                var AccionSession = (EDAccion)TempData[values[1]];
                var archivos = AccionSession.ArchivosLista;
                EDArchivosAcciones EDArchivosAcciones = new EDArchivosAcciones();
                try
                {
                    EDArchivosAcciones archivo = archivos.Find(s => s.IdFile == numeroID);
                    if (archivo == null)
                    {
                        probar = false;
                        probarEncuentraArchivo = false;
                        resultado = "El archivo no se encontró";
                        TempData.Keep(values[1]);
                        return Json(new { probar, resultado, probarEncuentraArchivo },
                JsonRequestBehavior.AllowGet);
                    }
                    string RutaFinal = Server.MapPath(Path.Combine(archivo.Ruta, archivo.NombreArchivo));
                    byte[] fileBytes = System.IO.File.ReadAllBytes(RutaFinal);
                    probar = true;
                    return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, archivo.NombreArchivo);
                }
                catch (Exception)
                {
                    probar = false;
                    probarEncuentraArchivo = false;
                    resultado = "El archivo no se encontró";
                    TempData.Keep(values[1]);
                    return Json(new { probar, resultado, probarEncuentraArchivo },
            JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                resultado = "La descarga falló, por favor intentelo nuevamente";
            }
            return Json(new { probar, resultado },
            JsonRequestBehavior.AllowGet);
        }
        public FileResult Download(string IdFile, string IdAccion)
        {
            int Idfileint = 0;
            var AccionSession = (EDAccion)TempData["EditarAccion" + IdAccion];
            EDArchivosAcciones EDArchivosAcciones = new EDArchivosAcciones();
            List<EDArchivosAcciones> ListaArchivos = new List<EDArchivosAcciones>();
            if (AccionSession != null)
            {
                TempData.Keep("EditarAccion" + IdAccion);
                ListaArchivos = AccionSession.ArchivosLista;
                if (int.TryParse(IdFile, out Idfileint))
                {
                    EDArchivosAcciones = ListaArchivos.Find(s => s.IdFile == Idfileint);
                }
            }
            string Ruta = "";
            string NombreArchivo = "";
            if (EDArchivosAcciones.Ruta != null && EDArchivosAcciones.NombreArchivo != null)
            {
                Ruta = EDArchivosAcciones.Ruta;
                NombreArchivo = EDArchivosAcciones.NombreArchivo;
            }
            byte[] fileBytes = System.IO.File.ReadAllBytes(Server.MapPath(Path.Combine(Ruta, NombreArchivo)));
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, NombreArchivo);
        }
        #endregion
        #region Firmas
        [HttpPost]
        public async Task<ActionResult> UploadImgAud()
        {
            bool probar = false;
            string resultado = "";
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                probar = false;
                resultado = "El usuario no ha iniciado sesión el sistema";
                return Json(new { probar, resultado });
            }
            if (Request.Files.Count > 0)
            {
                try
                {
                    HttpFileCollectionBase files = Request.Files;
                    Task.Run(() => files = Request.Files);
                    for (int i = 0; i < files.Count; i++)
                    {

                        HttpPostedFileBase file = files[i];

                        if (file.ContentType.Contains("image"))
                        {
                            //Guardar la imagen en variable general 
                            inicializar();
                            var AccionSession = Session["EDAccion"] as EDAccion;
                            //Metodo para obtener una imagen base64 de la firma
                            if (file != null)
                            {
                                try
                                {
                                    string Imagen = SrcWhite;
                                    Image bit = System.Drawing.Image.FromStream(file.InputStream);
                                    using (var newImage = ScaleImage(bit, 400, 500))
                                    {
                                        Imagen = "data:image/png;base64," + ImageToBase64String(newImage, ImageFormat.Png);
                                    }
                                    AccionSession.FirmaScrImageAud = Imagen;

                                }
                                catch (Exception)
                                {
                                    AccionSession.FirmaScrImageAud = null;
                                }
                            }
                            Session["EDAccion"] = AccionSession;
                        }
                        else
                        {

                            probar = false;
                            resultado = "El archivo que se intento subir no es una imagen o no es un archivo soportado, por favor intente de nuevo con otra imagen";
                            return Json(new { probar, resultado });
                        }
                    }
                    probar = true;
                    return Json(new { probar, resultado });
                }
                catch (Exception)
                {
                    resultado = "No se pudo completar la operación, el tamaño del archivo probablemente es mayor a 4 MB ";
                    return Json(new { probar, resultado });
                }
            }
            else
            {
                resultado = "No ha seleccionado un archivo, antes de adjuntar asegurese que exista un archivo";
                return Json(new { probar, resultado });
            }
        }
        [HttpPost]
        public async Task<ActionResult> UploadImgRes()
        {
            bool probar = false;
            string resultado = "";
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                probar = false;
                resultado = "El usuario no ha iniciado sesión el sistema";
                return Json(new { probar, resultado });
            }
            if (Request.Files.Count > 0)
            {
                try
                {
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        if (file.ContentType.Contains("image"))
                        {
                            inicializar();
                            var AccionSession = Session["EDAccion"] as EDAccion;
                            if (file != null)
                            {
                                try
                                {
                                    string Imagen = SrcWhite;
                                    Image bit = System.Drawing.Image.FromStream(file.InputStream);
                                    using (var newImage = ScaleImage(bit, 400, 500))
                                    {
                                        Imagen = "data:image/png;base64," + ImageToBase64String(newImage, ImageFormat.Png);
                                    }
                                    AccionSession.FirmaScrImageRes = Imagen;
                                }
                                catch (Exception)
                                {
                                    AccionSession.FirmaScrImageRes = null;
                                }
                            }
                            Session["EDAccion"] = AccionSession;
                        }
                        else
                        {
                            probar = false;
                            resultado = "El archivo que se intento subir no es una imagen o no es un archivo soportado, por favor intente de nuevo con otra imagen";
                            return Json(new { probar, resultado });
                        }
                    }
                    probar = true;
                    return Json(new { probar, resultado });
                }
                catch (Exception)
                {
                    resultado = "No se pudo completar la operación, el tamaño del archivo probablemente es mayor a 4 MB ";
                    return Json(new { probar, resultado });
                }
            }
            else
            {
                resultado = "No ha seleccionado un archivo, antes de adjuntar asegurese que exista un archivo";
                return Json(new { probar, resultado });
            }
        }
        public ActionResult UploadImgAudEd(string TempdataID)
        {
            bool probar = false;
            string resultado = "";
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                probar = false;
                resultado = "El usuario no ha iniciado sesión el sistema";
                return Json(new { probar, resultado });
            }
            if (Request.Files.Count > 0)
            {
                try
                {
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        if (file.ContentType.Contains("image"))
                        {
                            var AccionSession = (EDAccion)TempData[TempdataID];
                            if (file != null)
                            {
                                try
                                {
                                    string Imagen = SrcWhite;
                                    Image bit = System.Drawing.Image.FromStream(file.InputStream);
                                    using (var newImage = ScaleImage(bit, 400, 500))
                                    {
                                        Imagen = "data:image/png;base64," + ImageToBase64String(newImage, ImageFormat.Png);
                                    }
                                    AccionSession.FirmaScrImageAud = Imagen;
                                }
                                catch (Exception)
                                {
                                    AccionSession.FirmaScrImageAud = null;
                                }
                            }
                            TempData[TempdataID] = AccionSession;
                        }
                        else
                        {
                            TempData.Keep(TempdataID);
                            probar = false;
                            resultado = "El archivo que se intento subir no es una imagen o no es un archivo soportado, por favor intente de nuevo con otra imagen";
                            return Json(new { probar, resultado });
                        }
                    }
                    probar = true;
                    return Json(new { probar, resultado });
                }
                catch (Exception)
                {
                    TempData.Keep(TempdataID);
                    resultado = "No se pudo completar la operación, el tamaño del archivo probablemente es mayor a 4 MB ";
                    return Json(new { probar, resultado });
                }
            }
            else
            {
                TempData.Keep(TempdataID);
                resultado = "No ha seleccionado un archivo, antes de adjuntar asegurese que exista un archivo";
                return Json(new { probar, resultado });
            }
        }
        [HttpPost]
        public ActionResult UploadImgResEd(string TempdataID)
        {
            bool probar = false;
            string resultado = "";
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                probar = false;
                resultado = "El usuario no ha iniciado sesión el sistema";
                return Json(new { probar, resultado });
            }
            if (Request.Files.Count > 0)
            {
                try
                {
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        if (file.ContentType.Contains("image"))
                        {
                            var AccionSession = (EDAccion)TempData[TempdataID];
                            if (file != null)
                            {
                                try
                                {
                                    string Imagen = SrcWhite;
                                    Image bit = System.Drawing.Image.FromStream(file.InputStream);
                                    using (var newImage = ScaleImage(bit, 400, 500))
                                    {
                                        Imagen = "data:image/png;base64," + ImageToBase64String(newImage, ImageFormat.Png);
                                    }
                                    AccionSession.FirmaScrImageRes = Imagen;
                                }
                                catch (Exception)
                                {
                                    AccionSession.FirmaScrImageRes = null;
                                }
                            }
                            TempData[TempdataID] = AccionSession;
                        }
                        else
                        {
                            TempData.Keep(TempdataID);
                            probar = false;
                            resultado = "El archivo que se intento subir no es una imagen o no es un archivo soportado, por favor intente de nuevo con otra imagen";
                            return Json(new { probar, resultado });
                        }
                    }
                    probar = true;
                    return Json(new { probar, resultado });
                }
                catch (Exception)
                {
                    TempData.Keep(TempdataID);
                    resultado = "No se pudo completar la operación, el tamaño del archivo probablemente es mayor a 4 MB ";
                    return Json(new { probar, resultado });
                }
            }
            else
            {
                TempData.Keep(TempdataID);
                resultado = "No ha seleccionado un archivo, antes de adjuntar asegurese que exista un archivo";
                return Json(new { probar, resultado });
            }
        }
        [HttpPost]
        public ActionResult QuitarImgAud()
        {
            bool probar = false;
            string resultado = "";
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                probar = false;
                resultado = "El usuario no ha iniciado sesión el sistema";
                return Json(new { probar, resultado });
            }
            try
            {
                inicializar();
                var AccionSession = Session["EDAccion"] as EDAccion;
                AccionSession.File_Auditor = null;
                AccionSession.RutaArchivoAuditor = null;
                AccionSession.NombreArchivoAuditor = null;
                AccionSession.FirmaScrImageAud = SrcWhite;
                resultado = "Firma del Auditor eliminada";
                probar = true;
                Session["EDAccion"] = AccionSession;
            }
            catch (Exception)
            {
                resultado = "No se pudo eliminar la imagen o no existe una imagen que eliminar";
                return Json(new { probar, resultado });
            }
            return Json(new { probar, resultado });
        }
        [HttpPost]
        public ActionResult QuitarImgRes()
        {
            bool probar = false;
            string resultado = "";
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                probar = false;
                resultado = "El usuario no ha iniciado sesión el sistema";
                return Json(new { probar, resultado });
            }
            try
            {
                inicializar();
                var AccionSession = Session["EDAccion"] as EDAccion;
                AccionSession.File_Responable = null;
                AccionSession.RutaArchivoResp = null;
                AccionSession.NombreArchivoResp = null;
                AccionSession.FirmaScrImageRes = SrcWhite;
                resultado = "Firma del Responsable eliminada";
                probar = true;
                Session["EDAccion"] = AccionSession;
            }
            catch (Exception)
            {
                resultado = "No se pudo eliminar la imagen o no existe una imagen que eliminar";
                return Json(new { probar, resultado });
            }
            return Json(new { probar, resultado });
        }
        public ActionResult QuitarImgAudEd(string TempdataID)
        {
            bool probar = false;
            string resultado = "";
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                probar = false;
                resultado = "El usuario no ha iniciado sesión el sistema";
                return Json(new { probar, resultado });
            }
            try
            {
                var AccionSession = TempData[TempdataID] as EDAccion;
                AccionSession.File_Auditor = null;
                AccionSession.RutaArchivoAuditor = null;
                AccionSession.NombreArchivoAuditor = null;
                AccionSession.FirmaScrImageAud = SrcWhite;
                TempData[TempdataID] = AccionSession;
                resultado = "Firma del Auditor eliminada";
                probar = true;
            }
            catch (Exception)
            {
                TempData.Keep(TempdataID);
                resultado = "No se pudo eliminar la imagen o no existe una imagen que eliminar";
                return Json(new { probar, resultado });
            }
            return Json(new { probar, resultado });
        }
        [HttpPost]
        public ActionResult QuitarImgResEd(string TempdataID)
        {
            bool probar = false;
            string resultado = "";
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                probar = false;
                resultado = "El usuario no ha iniciado sesión el sistema";
                return Json(new { probar, resultado });
            }
            try
            {
                var AccionSession = TempData[TempdataID] as EDAccion;
                AccionSession.File_Responable = null;
                AccionSession.RutaArchivoResp = null;
                AccionSession.NombreArchivoResp = null;
                AccionSession.FirmaScrImageRes = SrcWhite;
                TempData[TempdataID] = AccionSession;
                resultado = "Firma del Responsable eliminada";
                probar = true;
            }
            catch (Exception)
            {
                TempData.Keep(TempdataID);
                resultado = "No se pudo eliminar la imagen o no existe una imagen que eliminar";
                return Json(new { probar, resultado });
            }
            return Json(new { probar, resultado });
        }
        #endregion
        #region Actividadyseguimiento

        [HttpGet]
        public ActionResult NuevoSeguimiento(string Edicion)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }
            if (TempData["FirmaSeg"] != null)
            {
                TempData.Remove("FirmaSeg");
            }
            if (Edicion != null)
            {
                TempData.Keep(Edicion);
                ViewBag.EdicionKey = Edicion;
            }
            else
            {
                ViewBag.EdicionKey = "";
            }
            return View();
        }
        [HttpPost]
        public ActionResult NuevoSeguimiento(Seguimiento Coleccion, FormCollection frm)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }

            if (ModelState.IsValid)
            {
                if (frm["EdicionKey"] != "")
                {
                    //Nuevo Seguimiento - Editar Acción
                    string IdEdicion = frm["EdicionKey"];
                    var AccionSession = TempData[IdEdicion] as EDAccion;
                    int IdAccion = AccionSession.Pk_Id_Accion;


                    List<EDSeguimiento> ListaSeguimientos = new List<EDSeguimiento>();
                    ListaSeguimientos = AccionSession.SeguimientoLista;

                    int IdSeg = 0;
                    bool Existe = true;

                    while (Existe)
                    {
                        Existe = false;
                        string Clave = RandomString(8);
                        bool probarInt = int.TryParse(Clave, out IdSeg);
                        foreach (var item in ListaSeguimientos)
                        {
                            if (item.Clave == IdSeg)
                            {
                                Existe = true;
                            }
                        }

                    }

                    EDSeguimiento NuevoSeguimiento = new EDSeguimiento();
                    NuevoSeguimiento.Clave = IdSeg;

                    NuevoSeguimiento.Observaciones = Coleccion.Observaciones;
                    NuevoSeguimiento.Fecha_Seg = Coleccion.Fecha_Seg;


                    if (TempData["FirmaSeg"] != null)
                    {
                        EDSeguimiento FotoActividad = new EDSeguimiento();
                        FotoActividad = (EDSeguimiento)TempData["FirmaSeg"];
                        NuevoSeguimiento.File_Seguimiento = FotoActividad.File_Seguimiento;
                        NuevoSeguimiento.FirmaScrImage = FotoActividad.FirmaScrImage;
                        NuevoSeguimiento.RutaArchivoSeg = FotoActividad.RutaArchivoSeg;
                        NuevoSeguimiento.NombreArchivoSeg = FotoActividad.NombreArchivoSeg;
                        AccionSession.SeguimientoLista.Add(NuevoSeguimiento);
                    }
                    else
                    {
                        AccionSession.SeguimientoLista.Add(NuevoSeguimiento);
                    }
                    return RedirectToAction("EditarAccion", "Acciones", new { id = IdAccion.ToString() });
                }
                else
                {
                    //Nuevo Seguimiento - Nueva Acción
                    inicializar();
                    var AccionSession = Session["EDAccion"] as EDAccion;
                    List<EDSeguimiento> ListaSeguimientos = new List<EDSeguimiento>();
                    ListaSeguimientos = AccionSession.SeguimientoLista;

                    int IdSeg = 0;
                    bool Existe = true;

                    while (Existe)
                    {
                        Existe = false;
                        string Clave = RandomString(8);
                        bool probarInt = int.TryParse(Clave, out IdSeg);
                        foreach (var item in ListaSeguimientos)
                        {
                            if (item.Clave == IdSeg)
                            {
                                Existe = true;
                            }
                        }

                    }
                    EDSeguimiento NuevoSeguimiento = new EDSeguimiento();
                    NuevoSeguimiento.Observaciones = Coleccion.Observaciones;
                    NuevoSeguimiento.Fecha_Seg = Coleccion.Fecha_Seg;
                    NuevoSeguimiento.Clave = IdSeg;
                    if (TempData["FirmaSeg"] != null)
                    {
                        EDSeguimiento FotoActividad = new EDSeguimiento();
                        FotoActividad = (EDSeguimiento)TempData["FirmaSeg"];
                        NuevoSeguimiento.File_Seguimiento = FotoActividad.File_Seguimiento;
                        NuevoSeguimiento.FirmaScrImage = FotoActividad.FirmaScrImage;
                        NuevoSeguimiento.RutaArchivoSeg = FotoActividad.RutaArchivoSeg;
                        NuevoSeguimiento.NombreArchivoSeg = FotoActividad.NombreArchivoSeg;
                        AccionSession.SeguimientoLista.Add(NuevoSeguimiento);
                    }
                    else
                    {
                        AccionSession.SeguimientoLista.Add(NuevoSeguimiento);
                    }
                    return RedirectToAction("NuevaAccion", "Acciones");
                }

            }
            else
            {
                if (frm["EdicionKey"] != "")
                {
                    string IdEdicion = frm["EdicionKey"];
                    TempData.Keep(IdEdicion);
                    ViewBag.EdicionKey = IdEdicion;
                }
                else
                {
                    ViewBag.EdicionKey = "";
                }

            }


            return View(Coleccion);
        }
        [HttpGet]
        public ActionResult NuevaActividad(string Edicion)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }
            if (TempData["FirmaAct"] != null)
            {
                TempData.Remove("FirmaAct");
            }
            if (Edicion != null)
            {
                TempData.Keep(Edicion);
                ViewBag.EdicionKey = Edicion;
            }
            else
            {
                ViewBag.EdicionKey = "";
            }
            return View();
        }
        [HttpPost]
        public ActionResult NuevaActividad(FormCollection frm, ActividadAccion Coleccion)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                if (frm["EdicionKey"] != "")
                {
                    //Nueva Actividad - Editar Acción
                    string IdEdicion = frm["EdicionKey"];
                    var AccionSession = TempData[IdEdicion] as EDAccion;
                    int IdAccion = AccionSession.Pk_Id_Accion;
                    List<EDActividad> ListaActividad = new List<EDActividad>();
                    ListaActividad = AccionSession.ActividadLista;

                    int IdAct = 0;
                    bool Existe = true;

                    while (Existe)
                    {
                        Existe = false;
                        string Clave = RandomString(8);
                        bool probarInt = int.TryParse(Clave, out IdAct);
                        foreach (var item in ListaActividad)
                        {
                            if (item.Clave == IdAct)
                            {
                                Existe = true;
                            }
                        }

                    }

                    EDActividad NuevaActividad = new EDActividad();
                    NuevaActividad.Responsable = Coleccion.Responsable;
                    NuevaActividad.FechaFinalizacion = Coleccion.FechaFinalizacion;
                    NuevaActividad.Actividad = Coleccion.Actividad;
                    NuevaActividad.Estado_1 = 1;

                    NuevaActividad.Clave = IdAct;
                    if (TempData["FirmaAct"] != null)
                    {
                        EDActividad FotoActividad = new EDActividad();
                        FotoActividad = (EDActividad)TempData["FirmaAct"];
                        NuevaActividad.File_Actividad = FotoActividad.File_Actividad;
                        NuevaActividad.FirmaScrImage = FotoActividad.FirmaScrImage;
                        NuevaActividad.RutaArchivoAct = FotoActividad.RutaArchivoAct;
                        NuevaActividad.NombreArchivoAct = FotoActividad.NombreArchivoAct;
                        AccionSession.ActividadLista.Add(NuevaActividad);
                    }
                    else
                    {
                        AccionSession.ActividadLista.Add(NuevaActividad);
                    }
                    TempData[IdEdicion] = AccionSession;
                    return RedirectToAction("EditarAccion", "Acciones", new { id = IdAccion.ToString() });
                }
                else
                {
                    //Nueva Actividad - Nueva Acción
                    inicializar();
                    var AccionSession = Session["EDAccion"] as EDAccion;
                    List<EDActividad> ListaActividad = new List<EDActividad>();
                    ListaActividad = AccionSession.ActividadLista;

                    int IdAct = 0;
                    bool Existe = true;

                    while (Existe)
                    {
                        Existe = false;
                        string Clave = RandomString(8);
                        bool probarInt = int.TryParse(Clave, out IdAct);
                        foreach (var item in ListaActividad)
                        {
                            if (item.Clave == IdAct)
                            {
                                Existe = true;
                            }
                        }

                    }

                    EDActividad NuevaActividad = new EDActividad();
                    NuevaActividad.Clave = IdAct;
                    NuevaActividad.Responsable = Coleccion.Responsable;
                    NuevaActividad.FechaFinalizacion = Coleccion.FechaFinalizacion;
                    NuevaActividad.Actividad = Coleccion.Actividad;
                    NuevaActividad.Estado_1 = 1;
                    if (TempData["FirmaAct"] != null)
                    {
                        EDActividad FotoActividad = new EDActividad();
                        FotoActividad = (EDActividad)TempData["FirmaAct"];
                        NuevaActividad.File_Actividad = FotoActividad.File_Actividad;
                        NuevaActividad.FirmaScrImage = FotoActividad.FirmaScrImage;
                        NuevaActividad.RutaArchivoAct = FotoActividad.RutaArchivoAct;
                        NuevaActividad.NombreArchivoAct = FotoActividad.NombreArchivoAct;
                        AccionSession.ActividadLista.Add(NuevaActividad);
                    }
                    else
                    {
                        AccionSession.ActividadLista.Add(NuevaActividad);
                    }
                    Session["EDAccion"] = AccionSession;
                    return RedirectToAction("NuevaAccion", "Acciones");
                }
            }
            else
            {
                if (frm["EdicionKey"] != "")
                {
                    string IdEdicion = frm["EdicionKey"];
                    TempData.Keep(IdEdicion);
                    ViewBag.EdicionKey = IdEdicion;
                }
                else
                {
                    ViewBag.EdicionKey = "";
                }

            }
            return View(Coleccion);
        }
        [HttpGet]
        public ActionResult EditarActividad(string id, string Edicion)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }
            if (TempData["FirmaAct"] != null)
            {
                TempData.Remove("FirmaAct");
            }
            List<EDActividad> ListaActividad = new List<EDActividad>();
            if (Edicion != null)
            {
                TempData.Keep(Edicion);
                ViewBag.EdicionKey = Edicion;
                var AccionSession = TempData[Edicion] as EDAccion;
                ListaActividad = AccionSession.ActividadLista;
            }
            else
            {
                ViewBag.EdicionKey = "";
                inicializar();
                var AccionSession = Session["EDAccion"] as EDAccion;
                ListaActividad = AccionSession.ActividadLista;
            }
            int Id_Act = 0;
            bool probar = int.TryParse(id, out Id_Act);
            EDActividad RegresarAct = ListaActividad.Find(s => s.Clave == Id_Act);
            ActividadAccion ActividadConsulta = new ActividadAccion();

            if (RegresarAct == null)
            {
                return HttpNotFound();
            }
            TempData["FirmaAct"] = RegresarAct;
            ViewBag.ValorClave = id;
            ViewBag.Clave = RegresarAct.Clave;
            ActividadConsulta.Pk_Id_Actividad = RegresarAct.Pk_Id_Actividad;
            ActividadConsulta.Actividad = RegresarAct.Actividad;
            ActividadConsulta.Responsable = RegresarAct.Responsable;
            ActividadConsulta.FechaFinalizacion = RegresarAct.FechaFinalizacion;
            ActividadConsulta.NombreArchivoAct = RegresarAct.NombreArchivoAct;
            ActividadConsulta.RutaArchivoAct = RegresarAct.RutaArchivoAct;
            ActividadConsulta.Fk_Id_Accion = RegresarAct.Fk_Id_Accion;
            ActividadConsulta.Estado = RegresarAct.Estado_1;
            return View(ActividadConsulta);
        }
        [HttpPost]
        public ActionResult EditarActividad(FormCollection frm, ActividadAccion Coleccion)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }

            string str_Clave = frm["Clave"].ToString();
            int Id_Clave = 0;
            bool probar = int.TryParse(str_Clave, out Id_Clave);

            if (ModelState.IsValid)
            {
                if (frm["EdicionKey"] != "")
                {
                    //Editar Actividad - Editar Acción
                    string IdEdicion = frm["EdicionKey"];
                    var AccionSession = TempData[IdEdicion] as EDAccion;
                    int IdAccion = AccionSession.Pk_Id_Accion;
                    List<EDActividad> ListaActividades = new List<EDActividad>();
                    ListaActividades = AccionSession.ActividadLista;
                    foreach (var item in ListaActividades.Where(c => c.Clave == Id_Clave))
                    {
                        if (TempData["FirmaAct"] != null)
                        {
                            EDActividad FotoActividad = new EDActividad();
                            FotoActividad = (EDActividad)TempData["FirmaAct"];

                            item.File_Actividad = FotoActividad.File_Actividad;
                            item.FirmaScrImage = FotoActividad.FirmaScrImage;
                            item.NombreArchivoAct = FotoActividad.NombreArchivoAct;
                            item.RutaArchivoAct = FotoActividad.RutaArchivoAct;
                        }
                        else
                        {
                            item.File_Actividad = null;
                            item.FirmaScrImage = null;
                            item.NombreArchivoAct = null;
                            item.RutaArchivoAct = null;
                        }
                        item.Actividad = Coleccion.Actividad;
                        item.FechaFinalizacion = Coleccion.FechaFinalizacion;
                        item.Responsable = Coleccion.Responsable;
                        item.Estado_1 = Coleccion.Estado;
                    }
                    AccionSession.ActividadLista = ListaActividades;
                    TempData[IdEdicion] = AccionSession;
                    return RedirectToAction("EditarAccion", "Acciones", new { id = IdAccion.ToString() });
                }
                else
                {
                    //Editar Actividad - Nueva Acción
                    inicializar();
                    var AccionSession = Session["EDAccion"] as EDAccion;
                    List<EDActividad> ListaActividades = new List<EDActividad>();
                    ListaActividades = AccionSession.ActividadLista;
                    foreach (var item in ListaActividades.Where(c => c.Clave == Id_Clave))
                    {
                        if (TempData["FirmaAct"] != null)
                        {
                            EDActividad FotoActividad = new EDActividad();
                            FotoActividad = (EDActividad)TempData["FirmaAct"];

                            item.File_Actividad = FotoActividad.File_Actividad;
                            item.FirmaScrImage = FotoActividad.FirmaScrImage;
                            item.NombreArchivoAct = FotoActividad.NombreArchivoAct;
                            item.RutaArchivoAct = FotoActividad.RutaArchivoAct;
                        }
                        else
                        {
                            item.File_Actividad = null;
                            item.FirmaScrImage = null;
                            item.NombreArchivoAct = null;
                            item.RutaArchivoAct = null;
                        }
                        item.Actividad = Coleccion.Actividad;
                        item.FechaFinalizacion = Coleccion.FechaFinalizacion;
                        item.Responsable = Coleccion.Responsable;
                        item.Estado_1 = Coleccion.Estado;
                    }
                    AccionSession.ActividadLista = ListaActividades;
                    Session["EDAccion"] = AccionSession;
                    return RedirectToAction("NuevaAccion", "Acciones");
                }
            }
            else
            {
                if (frm["EdicionKey"] != "")
                {
                    string IdEdicion = frm["EdicionKey"];
                    TempData.Keep(IdEdicion);
                    ViewBag.EdicionKey = IdEdicion;
                }
                else
                {
                    ViewBag.EdicionKey = "";
                }

            }
            return View(Coleccion);
        }
        [HttpGet]
        public ActionResult EditarSeguimiento(string id, string Edicion)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }

            if (TempData["FirmaSeg"] != null)
            {
                TempData.Remove("FirmaSeg");
            }

            List<EDSeguimiento> ListaSeguimiento = new List<EDSeguimiento>();

            if (Edicion != null)
            {
                TempData.Keep(Edicion);
                ViewBag.EdicionKey = Edicion;
                var AccionSession = TempData[Edicion] as EDAccion;
                ListaSeguimiento = AccionSession.SeguimientoLista;
            }
            else
            {
                ViewBag.EdicionKey = "";
                inicializar();
                var AccionSession = Session["EDAccion"] as EDAccion;
                ListaSeguimiento = AccionSession.SeguimientoLista;
            }



            int Id_Act = 0;
            bool probar = int.TryParse(id, out Id_Act);

            EDSeguimiento RegresarSeg = ListaSeguimiento.Find(s => s.Clave == Id_Act);
            Seguimiento SeguimientoConsulta = new Seguimiento();


            SeguimientoConsulta.Pk_Id_Seguimiento = RegresarSeg.Pk_Id_Seguimiento;
            SeguimientoConsulta.Fk_Id_Accion = RegresarSeg.Fk_Id_Accion;
            SeguimientoConsulta.Fecha_Seg = RegresarSeg.Fecha_Seg;
            SeguimientoConsulta.Observaciones = RegresarSeg.Observaciones;
            SeguimientoConsulta.NombreArchivoSeg = RegresarSeg.NombreArchivoSeg;
            SeguimientoConsulta.RutaArchivoSeg = RegresarSeg.RutaArchivoSeg;

            if (RegresarSeg == null)
            {
                return HttpNotFound();
            }
            TempData["FirmaSeg"] = RegresarSeg;
            ViewBag.ValorClave = id;
            ViewBag.Clave = RegresarSeg.Clave;
            return View(SeguimientoConsulta);
        }
        [HttpPost]
        public ActionResult EditarSeguimiento(FormCollection frm, Seguimiento Coleccion)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }
            string str_Clave = frm["Clave"].ToString();
            int Id_Clave = 0;
            bool probar = int.TryParse(str_Clave, out Id_Clave);

            if (ModelState.IsValid)
            {

                if (frm["EdicionKey"] != "")
                {
                    //Editar Seguimiento - Editar Acción
                    string IdEdicion = frm["EdicionKey"];
                    var AccionSession = TempData[IdEdicion] as EDAccion;
                    int IdAccion = AccionSession.Pk_Id_Accion;
                    List<EDSeguimiento> ListaSeguimientos = new List<EDSeguimiento>();
                    ListaSeguimientos = AccionSession.SeguimientoLista;


                    foreach (var item in ListaSeguimientos.Where(c => c.Clave == Id_Clave))
                    {
                        if (TempData["FirmaSeg"] != null)
                        {
                            EDSeguimiento FotoActividad = new EDSeguimiento();
                            FotoActividad = (EDSeguimiento)TempData["FirmaSeg"];

                            item.File_Seguimiento = FotoActividad.File_Seguimiento;
                            item.FirmaScrImage = FotoActividad.FirmaScrImage;
                            item.NombreArchivoSeg = FotoActividad.NombreArchivoSeg;
                            item.RutaArchivoSeg = FotoActividad.RutaArchivoSeg;
                        }
                        else
                        {
                            item.File_Seguimiento = null;
                            item.FirmaScrImage = null;
                            item.NombreArchivoSeg = null;
                            item.RutaArchivoSeg = null;
                        }

                        item.Observaciones = Coleccion.Observaciones;
                        if (item.Estado != 1)
                        {
                            item.Fecha_Seg = Coleccion.Fecha_Seg;
                        }

                    }

                    AccionSession.SeguimientoLista = ListaSeguimientos;
                    TempData[IdEdicion] = AccionSession;

                    return RedirectToAction("EditarAccion", "Acciones", new { id = IdAccion.ToString() });
                }
                else
                {
                    //Editar Seguimiento - Nueva Acción
                    var AccionSession = Session["EDAccion"] as EDAccion;
                    List<EDSeguimiento> ListaSeguimientos = new List<EDSeguimiento>();
                    ListaSeguimientos = AccionSession.SeguimientoLista;


                    foreach (var item in ListaSeguimientos.Where(c => c.Clave == Id_Clave))
                    {
                        if (TempData["FirmaSeg"] != null)
                        {
                            EDSeguimiento FotoActividad = new EDSeguimiento();
                            FotoActividad = (EDSeguimiento)TempData["FirmaSeg"];

                            item.File_Seguimiento = FotoActividad.File_Seguimiento;
                            item.FirmaScrImage = FotoActividad.FirmaScrImage;
                            item.NombreArchivoSeg = FotoActividad.NombreArchivoSeg;
                            item.RutaArchivoSeg = FotoActividad.RutaArchivoSeg;
                        }
                        else
                        {
                            item.File_Seguimiento = null;
                            item.FirmaScrImage = null;
                            item.NombreArchivoSeg = null;
                            item.RutaArchivoSeg = null;
                        }

                        item.Observaciones = Coleccion.Observaciones;
                        item.Fecha_Seg = Coleccion.Fecha_Seg;
                    }

                    AccionSession.SeguimientoLista = ListaSeguimientos;
                    Session["EDAccion"] = AccionSession;

                    return RedirectToAction("NuevaAccion", "Acciones");
                }
            }
            else
            {
                if (frm["EdicionKey"] != "")
                {
                    string IdEdicion = frm["EdicionKey"];
                    TempData.Keep(IdEdicion);
                    ViewBag.EdicionKey = IdEdicion;
                }
                else
                {
                    ViewBag.EdicionKey = "";
                }

            }


            return View(Coleccion);
        }
        [HttpPost]
        public JsonResult EliminarActividad(string Values)
        {
            bool probar = false;
            string resultado = "La actividad no ha podido ser eliminada";
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                resultado = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
            }

            int IdAct = 0;
            bool probarNumero = int.TryParse(Values, out IdAct);
            if (IdAct != 0)
            {
                var AccionSession = Session["EDAccion"] as EDAccion;
                List<EDActividad> ListaActividad = new List<EDActividad>();
                ListaActividad = AccionSession.ActividadLista;
                ListaActividad.RemoveAll(s => s.Clave == IdAct);
                AccionSession.ActividadLista = ListaActividad;
                Session["EDAccion"] = AccionSession;

                probar = true;
                resultado = "La actividad se ha eliminado correctamente";
                return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
            }

            //Devuelve el resultado exitosamente
            return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult EliminarActividadEd(string Values, string id)
        {
            bool probar = false;
            string resultado = "La actividad no ha podido ser eliminada";
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                resultado = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
            }

            int IdAct = 0;
            bool probarNumero = int.TryParse(Values, out IdAct);
            if (IdAct != 0)
            {
                var AccionSession = TempData[id] as EDAccion;

                List<EDActividad> ListaActividad = new List<EDActividad>();
                ListaActividad = AccionSession.ActividadLista;
                ListaActividad.RemoveAll(s => s.Clave == IdAct);
                AccionSession.ActividadLista = ListaActividad;
                TempData[id] = AccionSession;

                probar = true;
                resultado = "La actividad se ha eliminado correctamente";
                return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
            }

            //Devuelve el resultado exitosamente
            return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult EliminarSeguimiento(string Values)
        {
            bool probar = false;
            string resultado = "El seguimiento no ha podido ser eliminado";
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                resultado = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
            }

            int IdAct = 0;
            bool probarNumero = int.TryParse(Values, out IdAct);
            if (IdAct != 0)
            {
                var AccionSession = Session["EDAccion"] as EDAccion;
                List<EDSeguimiento> ListaSeguimiento = new List<EDSeguimiento>();
                ListaSeguimiento = AccionSession.SeguimientoLista;
                ListaSeguimiento.RemoveAll(s => s.Clave == IdAct);
                AccionSession.SeguimientoLista = ListaSeguimiento;
                Session["EDAccion"] = AccionSession;

                probar = true;
                resultado = "El seguimiento se ha eliminado correctamente";
                return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
            }

            //Devuelve el resultado exitosamente
            return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult EliminarSeguimientoEd(string Values, string id)
        {
            bool probar = false;
            string resultado = "El seguimiento no ha podido ser eliminado";
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                resultado = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
            }

            int IdAct = 0;
            bool probarNumero = int.TryParse(Values, out IdAct);
            if (IdAct != 0)
            {
                var AccionSession = TempData[id] as EDAccion;
                List<EDSeguimiento> ListaSeguimiento = new List<EDSeguimiento>();
                ListaSeguimiento = AccionSession.SeguimientoLista;
                ListaSeguimiento.RemoveAll(s => s.Clave == IdAct);
                AccionSession.SeguimientoLista = ListaSeguimiento;
                TempData[id] = AccionSession;

                probar = true;
                resultado = "El seguimiento se ha eliminado correctamente";
                return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
            }

            //Devuelve el resultado exitosamente
            return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult FirmaAct()
        {
            bool probar = false;
            string resultado = "";
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                probar = false;
                resultado = "El usuario no ha iniciado sesión el sistema";
                return Json(new { probar, resultado });
            }
            string ImgScr = SrcWhite;
            if (Request.Files.Count > 0)
            {
                try
                {
                    HttpFileCollectionBase files = Request.Files;
                    string Edicion = string.Empty;
                    if (Request.UrlReferrer != null)
                    {
                        var q = HttpUtility.ParseQueryString(Request.UrlReferrer.Query);
                        Edicion = q["Edicion"];
                    }
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        if (file.ContentType.Contains("image"))
                        {
                            if (TempData["FirmaAct"] == null)
                            {
                                TempData["FirmaAct"] = new EDActividad();
                            }
                            EDActividad Actividad = new EDActividad();
                            Actividad = (EDActividad)TempData["FirmaAct"];
                            Actividad.FirmaScrImage = SrcWhite;
                            if (file != null)
                            {
                                try
                                {
                                    string Imagen = SrcWhite;
                                    Image bit = System.Drawing.Image.FromStream(file.InputStream);
                                    using (var newImage = ScaleImage(bit, 400, 500))
                                    {
                                        Imagen = "data:image/png;base64," + ImageToBase64String(newImage, ImageFormat.Png);
                                    }
                                    Actividad.FirmaScrImage = Imagen;
                                    ImgScr = Imagen;
                                }
                                catch (Exception)
                                {
                                    Actividad.FirmaScrImage = SrcWhite;
                                }
                            }
                            TempData["FirmaAct"] = Actividad;
                            TempData.Keep("FirmaAct");
                        }
                        else
                        {
                            probar = false;
                            resultado = "El archivo que se intento subir no es una imagen o no es un archivo soportado, por favor intente de nuevo con otra imagen";
                            return Json(new { probar, resultado, ImgScr });
                        }
                    }
                    probar = true;
                    resultado = "Se ha adjuntado la imagen correctamente";
                    var jsonResult = Json(new { probar, resultado, ImgScr }, JsonRequestBehavior.DenyGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                }
                catch (Exception)
                {
                    resultado = "No se pudo completar la operación, el tamaño del archivo probablemente es mayor a 4 MB ";
                    return Json(new { probar, resultado, ImgScr });
                }
            }
            else
            {
                resultado = "No ha seleccionado un archivo, antes de adjuntar asegurese que exista un archivo";
                return Json(new { probar, resultado, ImgScr });
            }
        }
        [HttpPost]
        public ActionResult FirmaSeg()
        {
            bool probar = false;
            string resultado = "";
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                probar = false;
                resultado = "El usuario no ha iniciado sesión el sistema";
                return Json(new { probar, resultado });
            }
            string ImgScr = SrcWhite;
            if (Request.Files.Count > 0)
            {
                try
                {
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        if (file.ContentType.Contains("image"))
                        {
                            if (TempData["FirmaSeg"] == null)
                            {
                                TempData["FirmaSeg"] = new EDSeguimiento();
                            }
                            string RandomStr = RandomString(9);
                            EDSeguimiento Seguimiento = new EDSeguimiento();
                            Seguimiento = (EDSeguimiento)TempData["FirmaSeg"];
                            Seguimiento.FirmaScrImage = SrcWhite;
                            if (file != null)
                            {
                                try
                                {
                                    string Imagen = SrcWhite;
                                    Image bit = System.Drawing.Image.FromStream(file.InputStream);
                                    using (var newImage = ScaleImage(bit, 400, 500))
                                    {
                                        Imagen = "data:image/png;base64," + ImageToBase64String(newImage, ImageFormat.Png);
                                    }
                                    Seguimiento.FirmaScrImage = Imagen;
                                    ImgScr = Imagen;
                                }
                                catch (Exception)
                                {
                                    Seguimiento.FirmaScrImage = SrcWhite;
                                }
                            }
                            TempData["FirmaSeg"] = Seguimiento;
                            TempData.Keep("FirmaSeg");
                        }
                        else
                        {
                            probar = false;
                            resultado = "El archivo que se intento subir no es una imagen o no es un archivo soportado, por favor intente de nuevo con otra imagen";
                            return Json(new { probar, resultado, ImgScr });
                        }
                    }
                    probar = true;
                    resultado = "Se ha adjuntado la imagen correctamente";
                    var jsonResult = Json(new { probar, resultado, ImgScr }, JsonRequestBehavior.DenyGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                }
                catch (Exception)
                {
                    resultado = "No se pudo completar la operación, el tamaño del archivo probablemente es mayor a 4 MB ";
                    return Json(new { probar, resultado, ImgScr });
                }
            }
            else
            {
                resultado = "No ha seleccionado un archivo, antes de adjuntar asegurese que exista un archivo";
                return Json(new { probar, resultado, ImgScr });
            }
        }
        [HttpPost]
        public ActionResult QuitarImgAct()
        {
            bool probar = false;
            string resultado = "";
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                probar = false;
                resultado = "El usuario no ha iniciado sesión el sistema";
                return Json(new { probar, resultado });
            }
            try
            {
                if (TempData["FirmaAct"] == null)
                {
                    TempData["FirmaAct"] = new EDActividad();
                }
                EDActividad Actividad = new EDActividad();
                Actividad = (EDActividad)TempData["FirmaAct"];
                Actividad.File_Actividad = null;
                Actividad.RutaArchivoAct = null;
                Actividad.NombreArchivoAct = null;
                Actividad.FirmaScrImage = SrcWhite;
                TempData["FirmaAct"] = Actividad;
                TempData.Keep("FirmaAct");
                resultado = "Firma Eliminada";
                probar = true;
            }
            catch (Exception)
            {
                resultado = "No se pudo eliminar la imagen o no existe una imagen que eliminar";
                return Json(new { probar, resultado });
            }
            return Json(new { probar, resultado });
        }
        [HttpPost]
        public ActionResult QuitarImgSeg()
        {
            bool probar = false;
            string resultado = "";
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                probar = false;
                resultado = "El usuario no ha iniciado sesión el sistema";
                return Json(new { probar, resultado });
            }
            try
            {
                if (TempData["FirmaSeg"] == null)
                {
                    TempData["FirmaSeg"] = new EDSeguimiento();
                }
                EDSeguimiento Seguimiento = new EDSeguimiento();
                Seguimiento = (EDSeguimiento)TempData["FirmaSeg"];
                Seguimiento.File_Seguimiento = null;
                Seguimiento.RutaArchivoSeg = null;
                Seguimiento.NombreArchivoSeg = null;
                Seguimiento.FirmaScrImage = SrcWhite;
                TempData["FirmaSeg"] = Seguimiento;
                TempData.Keep("FirmaSeg");
                resultado = "Firma Eliminada";
                probar = true;
            }
            catch (Exception)
            {
                resultado = "No se pudo eliminar la imagen o no existe una imagen que eliminar";
                return Json(new { probar, resultado });
            }
            return Json(new { probar, resultado });
        }
        #endregion
        #region ConsultarACP
        public ActionResult ConsultaACAP()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }
            List<EDAccion> Lista = new List<EDAccion>();
            //Cargar en el DropDown la lista de tipos de opciones
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "-- Seleccionar --", Value = "", Selected = true });
            items.Add(new SelectListItem { Text = "Cerrada", Value = "Cerrada" });
            items.Add(new SelectListItem { Text = "Abierta", Value = "Abierta" });
            ViewBag.TipoEntrada = items;

            List<EDSede> ListaSedes = new List<EDSede>();
            ListaSedes = LNEmpresa.ObtenerSedesPorNit(usuarioActual.NitEmpresa);
            ViewBag.Pk_Id_Sede = new SelectList(ListaSedes, "IdSede", "NombreSede", null);

            //Validar Estados de las acciones de la empresa usuaria
            int idEmpresa = usuarioActual.IdUsuario;
            LNAcciones.ConsultaAccionEstado(idEmpresa);
            return View(Lista);
        }
        [HttpPost]
        public ActionResult ConsultaACAP(List<EDAccion> Lista, FormCollection frm)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }
            string Estado = "";
            string Sede = "";
            if (frm["TipoEntrada"] != null)
            {
                Estado = frm["TipoEntrada"].ToString();
            }
            if (frm["Pk_Id_Sede1"] != null)
            {
                Sede = frm["Pk_Id_Sede1"].ToString();
            }
            string IdAccion = frm["IdAccionTxt"].ToString();
            string NombrePersona = frm["NombrePersona"].ToString();

            Lista = LNAcciones.ConsultarAcciones(IdAccion, NombrePersona, Estado, usuarioActual.IdEmpresa, Sede);
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "-- Seleccionar --", Value = "", Selected = true });
            items.Add(new SelectListItem { Text = "Cerrada", Value = "Cerrada" });
            items.Add(new SelectListItem { Text = "Abierta", Value = "Abierta" });
            ViewBag.TipoEntrada = items;
            List<EDSede> ListaSedes = new List<EDSede>();
            ListaSedes = LNEmpresa.ObtenerSedesPorNit(usuarioActual.NitEmpresa);
            ViewBag.Pk_Id_Sede = new SelectList(ListaSedes, "IdSede", "NombreSede", null);

            foreach (var item in Lista)
            {
                int index_sede = 0;
                if (ListaSedes.Count > 0)
                {
                    bool trysede = int.TryParse(item.Halla_Sede, out index_sede);
                }
                if (index_sede != 0)
                {
                    EDSede EDSede = ListaSedes.Where(s => s.IdSede == index_sede).FirstOrDefault();
                    if (EDSede != null)
                    {
                        item.Halla_Sede = EDSede.NombreSede;
                    }
                    else
                    {
                        item.Halla_Sede = "";
                    }

                }
            }

            return View(Lista);
        }
        public ActionResult EditarAccion(string id)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }
            //Consultar la acción      
            if (TempData["EditarAccion" + id] == null)
            {
                inicializarED(id, usuarioActual.IdEmpresa);
            }
            var AccionSession = TempData["EditarAccion" + id] as EDAccion;
            if (AccionSession.Pk_Id_Accion == 0)
            {
                return HttpNotFound();
            }
            List<EDSede> ListaSedes = new List<EDSede>();
            ListaSedes = LNEmpresa.ObtenerSedesPorNit(usuarioActual.NitEmpresa);
            int index_sede = 0;
            if (ListaSedes.Count > 0)
            {
                bool trysede = int.TryParse(AccionSession.Halla_Sede, out index_sede);
            }
            List<EDCargo> ListaEDCargo = new List<EDCargo>();
            ListaEDCargo = LNAcciones.ListaCargos();
            ViewBag.IdAccion = AccionSession.Id_Accion;
            ViewBag.Pk_Id_Sede = new SelectList(ListaSedes, "IdSede", "NombreSede", index_sede);
            ViewBag.Pk_Id_Cargo = new SelectList(ListaEDCargo, "IDCargo", "NombreCargo", 0);
            ViewBag.MostrarOtroOrigen = false;

            if (AccionSession.Origen!=null)
            {
                if (AccionSession.Origen == "Otros")
                {
                    ViewBag.MostrarOtroOrigen = true;

                }
                else
                {
                    AccionSession.Otro_Origen = "";
                }
            }
            else
            {
                AccionSession.Otro_Origen = "";
            }


            bool[] ListaboolOrigen = new bool[9];
            string ValorOrigen = AccionSession.Origen;
            int valorIntOrigen = -1;
            if (ValorOrigen != null)
            {
                if (ValorOrigen == "Incidente")
                {
                    valorIntOrigen = 0;
                }
                if (ValorOrigen == "Accidente")
                {
                    valorIntOrigen = 1;
                }
                if (ValorOrigen == "Auditoría Interna")
                {
                    valorIntOrigen = 2;
                }
                if (ValorOrigen == "Auditoría Externa")
                {
                    valorIntOrigen = 3;
                }
                if (ValorOrigen == "Inspección")
                {
                    valorIntOrigen = 4;
                }
                if (ValorOrigen == "Programa Gestión del Cambio")
                {
                    valorIntOrigen = 5;
                }
                if (ValorOrigen == "Actos y condiciones Inseguras")
                {
                    valorIntOrigen = 6;
                }
                if (ValorOrigen == "Revisión General")
                {
                    valorIntOrigen = 7;
                }
                if (ValorOrigen == "Otros")
                {
                    valorIntOrigen = 8;
                }
            }

            for (int i = 0; i < 9; i++)
            {
                ListaboolOrigen[i] = false;
                if (valorIntOrigen == i)
                {
                    ListaboolOrigen[i] = true;
                }
            }


            List<SelectListItem> ListaOrigen = new List<SelectListItem>();
            ListaOrigen.Add(new SelectListItem { Text = "Incidente", Value = "Incidente", Selected = ListaboolOrigen[0] });
            ListaOrigen.Add(new SelectListItem { Text = "Accidente", Value = "Accidente", Selected = ListaboolOrigen[1] });
            ListaOrigen.Add(new SelectListItem { Text = "Auditoría Interna", Value = "Auditoría Interna", Selected = ListaboolOrigen[2] });
            ListaOrigen.Add(new SelectListItem { Text = "Auditoría Externa", Value = "Auditoría Externa", Selected = ListaboolOrigen[3] });
            ListaOrigen.Add(new SelectListItem { Text = "Inspección", Value = "Inspección", Selected = ListaboolOrigen[4] });
            ListaOrigen.Add(new SelectListItem { Text = "Programa Gestión del Cambio", Value = "Programa Gestión del Cambio", Selected = ListaboolOrigen[5] });
            ListaOrigen.Add(new SelectListItem { Text = "Actos y condiciones Inseguras", Value = "Actos y condiciones Inseguras", Selected = ListaboolOrigen[6] });
            ListaOrigen.Add(new SelectListItem { Text = "Revisión General", Value = "Revisión General", Selected = ListaboolOrigen[7] });
            ListaOrigen.Add(new SelectListItem { Text = "Otros", Value = "Otros", Selected = ListaboolOrigen[8] });

            ViewBag.ListaOrigen = ListaOrigen;

            if (TempData["AnalisisArbol"] != null)
            {
                TempData.Remove("AnalisisArbol");
            }
            if (TempData["AnalisisCausa"] != null)
            {
                TempData.Remove("AnalisisCausa");
            }
            if (TempData["Analisis5porques"] != null)
            {
                TempData.Remove("Analisis5porques");
            }
            if (TempData["AnalisisLluvia"] != null)
            {
                TempData.Remove("AnalisisLluvia");
            }
            if (TempData["FirmaAct"] != null)
            {
                TempData.Remove("FirmaAct");
            }
            if (TempData["FirmaSeg"] != null)
            {
                TempData.Remove("FirmaSeg");
            }
            // Cargar Imagen Auditor
            ViewBag.SrcImgAud = SrcWhite;
            if (AccionSession.FirmaScrImageAud != null)
            {
                if (AccionSession.FirmaScrImageAud != "")
                {
                    ViewBag.SrcImgAud = AccionSession.FirmaScrImageAud;
                }
            }
            // Cargar Imagen Responsable
            ViewBag.SrcImgRes = SrcWhite;
            if (AccionSession.FirmaScrImageRes != null)
            {
                if (AccionSession.FirmaScrImageRes != "")
                {
                    ViewBag.SrcImgRes = AccionSession.FirmaScrImageRes;
                }
            }
            List<EDAnalisis> ListaAnalisis = LNAcciones.ListaAnalisis(AccionSession.Pk_Id_Accion);
            AccionSession.AnalisisLista = ListaAnalisis;
            ViewBag.TempDataID = "EditarAccion" + id;
            return View(AccionSession);
        }
        [HttpPost]
        public JsonResult CancelarEditarAccion(string id)
        {
            if (TempData[id] != null)
            {
                TempData.Remove(id);
            }
            return Json(new { url = Url.Action("ConsultaACAP", "Acciones") },
            JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        public ActionResult AccionPDF(string id, string NitEmpresa, int IdEmpresa)
        {


            bool SessionAut = false;
            var AccionSession = inicializarEDPDF(id, IdEmpresa);

            List<EDSede> ListaSedes = new List<EDSede>();
            List<Cargo> ListaCargo = new List<Cargo>();
            int index_sede = 0;
            string NombreSede = "";

            ViewBag.SrcImgAn1 = "";
            ViewBag.SrcImgAn2 = "";
            ViewBag.SrcImgAn3 = "";
            ViewBag.SrcImgAn4 = "";

            ViewBag.Pk_Id_Sede = "";
            ViewBag.Pk_Id_Cargo = AccionSession.Halla_Cargo;

            try
            {
                ListaSedes = LNEmpresa.ObtenerSedesPorNit(NitEmpresa);

                if (ListaSedes.Count > 0)
                {
                    bool trysede = int.TryParse(AccionSession.Halla_Sede, out index_sede);
                }
            }
            catch (Exception)
            {
            }

            try
            {
                EDSede SedeSeleccionada = ListaSedes.Where(s => s.IdSede == index_sede).FirstOrDefault();
                NombreSede = SedeSeleccionada.NombreSede;
            }
            catch (Exception)
            {

            }
            string ValorOrigen = AccionSession.Origen;
            ViewBag.ValorOrigen = "";
            if (ValorOrigen!=null)
            {
                if (ValorOrigen != "")
                {
                    if (ValorOrigen=="Otros")
                    {
                        if (AccionSession.Otro_Origen!=null)
                        {
                            ViewBag.ValorOrigen = ValorOrigen + " - " + AccionSession.Otro_Origen;
                        }
                        else
                        {
                            ViewBag.ValorOrigen = ValorOrigen;
                        }
                    }
                    else
                    {
                        ViewBag.ValorOrigen = ValorOrigen;
                    }
                }
            }

            ViewBag.Pk_Id_Sede = NombreSede;
            // Cargar Imagen Auditor
            ViewBag.SrcImgAud = SrcWhite;
            if (AccionSession.FirmaScrImageAud != null)
            {
                if (AccionSession.FirmaScrImageAud != "")
                {
                    ViewBag.SrcImgAud = AccionSession.FirmaScrImageAud;
                }
            }
            // Cargar Imagen Responsable
            ViewBag.SrcImgRes = SrcWhite;
            if (AccionSession.FirmaScrImageRes != null)
            {
                if (AccionSession.FirmaScrImageRes != "")
                {
                    ViewBag.SrcImgRes = AccionSession.FirmaScrImageRes;
                }
            }
            try
            {
                //Generar Analisis Arbol
                List<EDAnalisis> ListaAnalisisNuevo = new List<EDAnalisis>();
                List<EDAnalisis> ListaAnalisis1 = new List<EDAnalisis>();
                List<EDAnalisis> ListaAnalisis2 = new List<EDAnalisis>();
                List<EDAnalisis> ListaAnalisis3 = new List<EDAnalisis>();
                List<EDAnalisis> ListaAnalisis4 = new List<EDAnalisis>();
                ListaAnalisisNuevo = AccionSession.AnalisisLista;
                ListaAnalisis1 = ListaAnalisisNuevo.Where(s => s.Tipo == 1).ToList();
                ListaAnalisis2 = ListaAnalisisNuevo.Where(s => s.Tipo == 2).ToList();
                ListaAnalisis3 = ListaAnalisisNuevo.Where(s => s.Tipo == 3).ToList();
                ListaAnalisis4 = ListaAnalisisNuevo.Where(s => s.Tipo == 4).ToList();
                #region obtenerRutaAnalisis1
                List<string> Lista_Niveles = new List<string>();
                List<string> Lista_Valores = new List<string>();
                List<int> Lista_Id = new List<int>();

                if (ListaAnalisis1.Count > 0)
                {
                    List<EDAnalisis> ListaOrdenada = ListaAnalisis1.OrderBy(x => x.Parent_Id).ToList();


                    int padre_anterior = 0;
                    int contador_nivel = 0;

                    foreach (var item in ListaOrdenada)
                    {
                        string texto = item.ValorTxt;
                        int Nivel = item.Parent_Id;
                        int IdElemento = item.Id_Analisis;

                        if (padre_anterior != Nivel)
                        {
                            contador_nivel = 1;
                        }
                        else
                        {
                            contador_nivel = contador_nivel + 1;
                        }
                        if (Nivel == 0)
                        {
                            Lista_Niveles.Add("1");
                            Lista_Valores.Add(texto);
                            Lista_Id.Add(IdElemento);
                        }
                        else
                        {
                            string path_anterior = "";
                            int cont = 0;
                            //buscar padre
                            foreach (var item1 in Lista_Id)
                            {
                                if (item1 == item.Parent_Id)
                                {
                                    path_anterior = Lista_Niveles[cont];
                                }
                                cont = cont + 1;
                            }

                            Lista_Niveles.Add(path_anterior + "/" + contador_nivel);
                            Lista_Valores.Add(texto);
                            Lista_Id.Add(IdElemento);
                        }

                        padre_anterior = Nivel;

                    }
                    string CrearDiagrama = DiagramaArbol(Lista_Valores, Lista_Niveles);
                    ViewBag.SrcImgAn1 = CrearDiagrama;
                }
                #endregion
                #region obtenerRutaAnalisis2
                Lista_Niveles.Clear();
                Lista_Valores.Clear();
                Lista_Id.Clear();
                if (ListaAnalisis2.Count >= 0)
                {
                    List<EDAnalisis> ListaOrdenada = ListaAnalisis2.OrderBy(x => x.Parent_Id).ToList();
                    int padre_anterior = 0;
                    int contador_nivel = 0;
                    foreach (var item in ListaOrdenada)
                    {
                        string texto = item.ValorTxt;
                        int Nivel = item.Parent_Id;
                        int IdElemento = item.Id_Analisis;

                        if (padre_anterior != Nivel)
                        {
                            contador_nivel = 1;
                        }
                        else
                        {
                            contador_nivel = contador_nivel + 1;
                        }
                        if (Nivel == 0)
                        {
                            Lista_Niveles.Add("1");
                            Lista_Valores.Add(texto);
                            Lista_Id.Add(IdElemento);
                        }
                        else
                        {
                            string path_anterior = "";
                            int cont = 0;
                            //buscar padre
                            foreach (var item1 in Lista_Id)
                            {
                                if (item1 == item.Parent_Id)
                                {
                                    path_anterior = Lista_Niveles[cont];
                                }
                                cont = cont + 1;
                            }
                            Lista_Niveles.Add(path_anterior + "/" + contador_nivel);
                            Lista_Valores.Add(texto);
                            Lista_Id.Add(IdElemento);
                        }
                        padre_anterior = Nivel;
                    }
                    string CrearDiagrama2 = DiagramaCausaEfecto(Lista_Valores, Lista_Niveles);
                    ViewBag.SrcImgAn2 = CrearDiagrama2;
                }

                #endregion
                #region obtenerRutaAnalisis3
                foreach (var item in ListaAnalisis3)
                {
                    if (item.ValorTxt == "NullValueString")
                    {
                        item.ValorTxt = null;
                    }
                }
                List<EDAnalisis> ListaAnalisis3a = new List<EDAnalisis>();
                ListaAnalisis3a = ListaAnalisis3;
                if (ListaAnalisis3.Count < 36 && ListaAnalisis3.Count >= 8)
                {
                    int padre = ListaAnalisis3[0].Pk_Id_Analisis;
                    for (int i = 1; i < 37; i++)
                    {
                        bool probar = false;

                        foreach (var item in ListaAnalisis3)
                        {
                            if (item.Id_Analisis == i)
                            {
                                probar = true;
                            }
                        }

                        if (probar == false)
                        {
                            EDAnalisis EDAnalisis = new EDAnalisis();
                            EDAnalisis.Id_Analisis = i;
                            EDAnalisis.ValorTxt = "";
                            EDAnalisis.Parent_Id = padre;
                            ListaAnalisis3a.Add(EDAnalisis);
                        }
                    }
                }
                ListaAnalisis3 = ListaAnalisis3a;


                Lista_Niveles.Clear();
                Lista_Valores.Clear();
                Lista_Id.Clear();
                if (ListaAnalisis3.Count >= 0)
                {
                    List<EDAnalisis> ListaOrdenada = ListaAnalisis3.OrderBy(x => x.Parent_Id).ToList();
                    int padre_anterior = 0;
                    int contador_nivel = 0;
                    foreach (var item in ListaOrdenada)
                    {
                        string texto = item.ValorTxt;
                        int Nivel = item.Parent_Id;
                        int IdElemento = item.Id_Analisis;

                        if (padre_anterior != Nivel)
                        {
                            contador_nivel = 1;
                        }
                        else
                        {
                            contador_nivel = contador_nivel + 1;
                        }
                        if (Nivel == 0)
                        {
                            Lista_Niveles.Add("1");
                            Lista_Valores.Add(texto);
                            Lista_Id.Add(IdElemento);
                        }
                        else
                        {
                            string path_anterior = "";
                            int cont = 0;
                            //buscar padre
                            foreach (var item1 in Lista_Id)
                            {
                                if (item1 == item.Parent_Id)
                                {
                                    path_anterior = Lista_Niveles[cont];
                                }
                                cont = cont + 1;
                            }
                            Lista_Niveles.Add(path_anterior + "/" + contador_nivel);
                            Lista_Valores.Add(texto);
                            Lista_Id.Add(IdElemento);
                        }
                        padre_anterior = Nivel;
                    }
                    string CrearDiagrama3 = Diagrama5Porque(Lista_Valores, Lista_Niveles);
                    ViewBag.SrcImgAn3 = CrearDiagrama3;
                }
                #endregion
                #region obtenerRutaAnalisis4
                Lista_Niveles.Clear();
                Lista_Valores.Clear();
                Lista_Id.Clear();
                if (ListaAnalisis4.Count > 0)
                {
                    List<EDAnalisis> ListaOrdenada = ListaAnalisis4.OrderBy(x => x.Parent_Id).ToList();
                    int padre_anterior = 0;
                    int contador_nivel = 0;
                    foreach (var item in ListaOrdenada)
                    {
                        string texto = item.ValorTxt;
                        int Nivel = item.Parent_Id;
                        int IdElemento = item.Id_Analisis;
                        if (padre_anterior != Nivel)
                        {
                            contador_nivel = 1;
                        }
                        else
                        {
                            contador_nivel = contador_nivel + 1;
                        }
                        if (Nivel == 0)
                        {
                            ViewBag.TextoProblema = texto;
                            Lista_Niveles.Add("1");
                            Lista_Valores.Add(texto);
                            Lista_Id.Add(IdElemento);
                        }
                        else
                        {
                            string path_anterior = "";
                            int cont = 0;
                            //buscar padre
                            foreach (var item1 in Lista_Id)
                            {
                                if (item1 == item.Parent_Id)
                                {
                                    path_anterior = Lista_Niveles[cont];
                                }
                                cont = cont + 1;
                            }
                            Lista_Niveles.Add(path_anterior + "/" + contador_nivel);
                            Lista_Valores.Add(texto);
                            Lista_Id.Add(IdElemento);
                        }
                        padre_anterior = Nivel;
                    }
                    string CrearDiagrama4 = GenerarLluvia(Lista_Valores, Lista_Niveles);
                    ViewBag.SrcImgAn4 = CrearDiagrama4;
                }
                #endregion
            }
            catch (Exception)
            {

            }


            return View(AccionSession);
        }
        [HttpPost]
        public JsonResult EliminarAccion(string id)
        {


            bool probar = false;
            string resultado = "La acción no ha podido ser eliminada";

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                resultado = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
            }

            int IdAccion = 0;
            bool probarNumero = int.TryParse(id, out IdAccion);
            if (IdAccion != 0)
            {
                bool BorraAccion = LNAcciones.EliminarEncontrarAccion(IdAccion, usuarioActual.IdEmpresa);
                if (BorraAccion == false)
                {
                    return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
                }

                probar = true;
                resultado = "La acción se ha eliminado correctamente";
                return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult UrlAsPDF(string id)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }

            string EncodedRazonSocial = System.Net.WebUtility.UrlEncode(usuarioActual.RazonSocialEmpresa);
            string EncodedNit = System.Net.WebUtility.UrlEncode(usuarioActual.NitEmpresa);
            string EncodedNombreInforme = System.Net.WebUtility.UrlEncode("ACCIÓN PREVENTIVA O CORRECTIVA");


            var footurl = "https://alissta.gov.co/Acciones/Footer";
            var headerurl = "https://alissta.gov.co/Acciones/Header?NombreEmpresa="+ EncodedRazonSocial + "&NitEmpresa="+ EncodedNit + "&NombreInforme="+ EncodedNombreInforme;

            string cusomtSwitches = string.Format("--footer-line --print-media-type --allow {0} --footer-html {0} --header-line --print-media-type --allow {1} --header-html {1} --header-spacing 5",
            footurl, headerurl);
            int Id_Empresa = usuarioActual.IdEmpresa;
            int Id_Accion = 0;
            bool probar = int.TryParse(id, out Id_Accion);

            var ReporteUrl = "https://alissta.gov.co/Acciones/AccionPDF?id="+ Id_Accion.ToString() + "&NitEmpresa="+ EncodedNit+ "&IdEmpresa="+ usuarioActual.IdEmpresa.ToString();

            return new Rotativa.UrlAsPdf(ReporteUrl)
            {
                FileName = "AccionPreventivaCorrectiva" + DateTime.Now.ToString().Replace("/", "").Replace(":", "").Replace(" ", "") + ".pdf"
                ,
                PageSize = Rotativa.Options.Size.Letter
                ,
                PageMargins = new Rotativa.Options.Margins(20, 1, 10, 1)
                ,
                CustomSwitches = cusomtSwitches
            };
        }
        [AllowAnonymous]
        public ActionResult Footer()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult Header(string NombreEmpresa, string NitEmpresa, string NombreInforme)
        {
            ViewBag.NombreEmpresa = NombreEmpresa;
            ViewBag.NitEmpresa = NitEmpresa;
            ViewBag.NombreInforme = NombreInforme;
            return View();
        }
        #endregion
        #region Guardar/ActualizarACP
        [HttpPost]
        public JsonResult PostGuardar(Accion incomingAccion)
        {
            string status = "";
            bool probar = false;
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                status = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { status, probar });
            }


            if (Session["EDAccion"] == null)
            {
                return Json(new { url = Url.Action("ConsultaACAP", "Acciones") },
            JsonRequestBehavior.AllowGet);
            }

            bool[] Validacion = new bool[12];
            string[] ValidacionStr = new string[12];
            for (int i = 0; i < 12; i++)
            {
                Validacion[i] = false;
                ValidacionStr[i] = "";
            }
            try
            {
                if (ModelState.IsValid)
                {
                    if (incomingAccion.Cambio_Doc == "Si")
                    {
                        if (incomingAccion.Des_Cambio_Doc == null)
                        {
                            Validacion[10] = true;
                            ValidacionStr[10] = "Ha registrado que se requiere un cambio de documentación, pero la descripción del cambio es obligatorio";
                            status = "No ha suministrado la información completa, por favor revise el formulario y vuelva a intentar";
                            probar = false;
                            return Json(new { status, probar, Validacion, ValidacionStr });
                        }
                        else
                        {
                            if (incomingAccion.Des_Cambio_Doc == "")
                            {
                                Validacion[10] = true;
                                ValidacionStr[10] = "Ha registrado que se requiere un cambio de documentación, pero la descripción del cambio es obligatorio";
                                status = "No ha suministrado la información completa, por favor revise el formulario y vuelva a intentar";
                                probar = false;
                                return Json(new { status, probar, Validacion, ValidacionStr });
                            }
                        }
                    }
                    else
                    {
                        incomingAccion.Des_Cambio_Doc = null;
                    }
                    //Recuperar la variable Session
                    inicializar();
                    var AccionSession = Session["EDAccion"] as EDAccion;
                    var PostHallazgos = AccionSession.HallazgoLista;
                    var PostSeguimiento = AccionSession.SeguimientoLista;
                    var PostAnalisis = AccionSession.AnalisisLista;
                    var PostArchivos = AccionSession.ArchivosLista;
                    var PostActividad = AccionSession.ActividadLista;
                    var SrcAuditor = AccionSession.FirmaScrImageAud;
                    var SrcResponsable = AccionSession.FirmaScrImageRes;
                    string NombreArchivoAuditor = "AccFirmaAud" + FechaString() + RandomString(3) + ".png";
                    string NombreArchivoResponsable = "AccFirmaRes" + FechaString() + RandomString(3) + ".png";

                    //Validar que exista por lo menos 1 hallazgo
                    if (PostHallazgos.Count == 0)
                    {
                        Validacion[9] = true;
                        ValidacionStr[9] = "Verifique que haya suministrado la descripción del hallazgo y el proceso";
                        status = "No ha suministrado la información completa, por favor revise el formulario y vuelva a intentar";
                        probar = false;
                        return Json(new { status, probar, Validacion, ValidacionStr });
                    }
                    if (true)
                    {
                        Accion AccionGuardar = new Accion();
                        AccionGuardar.Tipo = incomingAccion.Tipo;
                        DateTime dateoDili = incomingAccion.Fecha_dil;
                        DateTime dateHallazgo = DateTime.Now;
                        dateHallazgo = incomingAccion.Fecha_hall ?? DateTime.Now;
                        AccionGuardar.Fecha_dil = dateoDili;
                        AccionGuardar.Fecha_ocurrencia = DateTime.Now;
                        AccionGuardar.Clase = incomingAccion.Clase;
                        AccionGuardar.Fecha_hall = dateHallazgo;
                        AccionGuardar.Halla_Num_Doc = incomingAccion.Halla_Num_Doc;
                        AccionGuardar.Halla_Nombre = incomingAccion.Halla_Nombre;
                        AccionGuardar.Halla_TipoDoc = incomingAccion.Halla_TipoDoc;
                        AccionGuardar.Halla_Cargo = incomingAccion.Halla_Cargo;
                        AccionGuardar.Origen = incomingAccion.Origen;
                        AccionGuardar.Otro_Origen = incomingAccion.Otro_Origen;

                        //Buscar Sede
                        int IdSede = 0;
                        bool isNumeric = int.TryParse(incomingAccion.Halla_Sede, out IdSede);
                        if (isNumeric == true)
                        {
                            List<EDSede> ListaSede = new List<EDSede>();
                            ListaSede = LNEmpresa.ObtenerSedesPorNit(usuarioActual.NitEmpresa);
                            EDSede EDSede = ListaSede.Find(s => s.IdSede == IdSede);
                            if (EDSede != null)
                            {
                                AccionGuardar.Halla_Sede = incomingAccion.Halla_Sede;
                            }
                            else
                            {
                                Validacion[8] = true;
                                ValidacionStr[8] = "La sede elegida no se encuentra registrada";
                                status = "La sede elegida no se encuentra registrada";
                                probar = false;
                                return Json(new { status, probar, Validacion, ValidacionStr });
                            }
                        }
                        else
                        {
                            status = "La sede elegida no se encuentra registrada";
                            probar = false;
                            return Json(new { status, probar });
                        }
                        AccionGuardar.Correccion = incomingAccion.Correccion;
                        AccionGuardar.Causa_Raiz = incomingAccion.Causa_Raiz;
                        AccionGuardar.Cambio_Doc = incomingAccion.Cambio_Doc;
                        AccionGuardar.Des_Cambio_Doc = incomingAccion.Des_Cambio_Doc;
                        AccionGuardar.Verificacion = incomingAccion.Verificacion;
                        AccionGuardar.Eficacia = incomingAccion.Eficacia;
                        if (incomingAccion.Estado != null)
                        {
                            AccionGuardar.Estado = incomingAccion.Estado;
                        }
                        else
                        {
                            AccionGuardar.Estado = "Abierta";
                        }
                        AccionGuardar.Nombre_Auditor = incomingAccion.Nombre_Auditor;
                        AccionGuardar.Cargo_Auditor = incomingAccion.Cargo_Auditor;
                        AccionGuardar.Nombre_Responsable = incomingAccion.Nombre_Responsable;
                        AccionGuardar.Cargo_Responsable = incomingAccion.Cargo_Responsable;
                        if (SrcAuditor != null)
                        {
                            if (SrcAuditor != "")
                            {
                                if (SrcAuditor != SrcWhite)
                                {
                                    AccionGuardar.NombreArchivoAuditor = NombreArchivoAuditor;
                                    AccionGuardar.RutaArchivoAuditor = RutaFirmas;
                                }
                            }
                        }
                        if (SrcResponsable != null)
                        {
                            if (SrcResponsable != "")
                            {
                                if (SrcResponsable != SrcWhite)
                                {
                                    AccionGuardar.NombreArchivoResp = NombreArchivoResponsable;
                                    AccionGuardar.RutaArchivoResp = RutaFirmas;
                                }
                            }
                        }
                        AccionGuardar.Fk_Id_Empresa = usuarioActual.IdEmpresa;
                        AccionGuardar.Id_Accion = LNAcciones.NuevoNumeroACP(usuarioActual.IdEmpresa);
                        int UltimoId = 0;
                        bool ProbarGuardar = false;
                        try
                        {
                            EDAccion EDAccion = new EDAccion();
                            EDAccion.Pk_Id_Accion = AccionGuardar.Pk_Id_Accion;
                            EDAccion.Id_Accion = AccionGuardar.Id_Accion;
                            EDAccion.Tipo = AccionGuardar.Tipo;
                            EDAccion.Fecha_dil = AccionGuardar.Fecha_dil;
                            EDAccion.Fecha_ocurrencia = AccionGuardar.Fecha_ocurrencia;
                            EDAccion.Clase = AccionGuardar.Clase;
                            EDAccion.Fecha_hall = AccionGuardar.Fecha_hall;
                            EDAccion.Halla_Num_Doc = AccionGuardar.Halla_Num_Doc;
                            EDAccion.Halla_Nombre = AccionGuardar.Halla_Nombre;
                            EDAccion.Halla_TipoDoc = AccionGuardar.Halla_TipoDoc;
                            EDAccion.Halla_Cargo = AccionGuardar.Halla_Cargo;
                            EDAccion.Halla_Sede = AccionGuardar.Halla_Sede;
                            EDAccion.Correccion = AccionGuardar.Correccion;
                            EDAccion.Causa_Raiz = AccionGuardar.Causa_Raiz;
                            EDAccion.Cambio_Doc = AccionGuardar.Cambio_Doc;
                            EDAccion.Des_Cambio_Doc = AccionGuardar.Des_Cambio_Doc;
                            EDAccion.Verificacion = AccionGuardar.Verificacion;
                            EDAccion.Eficacia = AccionGuardar.Eficacia;
                            EDAccion.Estado = AccionGuardar.Estado;
                            EDAccion.NombreArchivoAuditor = AccionGuardar.NombreArchivoAuditor;
                            EDAccion.RutaArchivoAuditor = AccionGuardar.RutaArchivoAuditor;
                            EDAccion.Nombre_Auditor = AccionGuardar.Nombre_Auditor;
                            EDAccion.Cargo_Auditor = AccionGuardar.Cargo_Auditor;
                            EDAccion.NombreArchivoResp = AccionGuardar.NombreArchivoResp;
                            EDAccion.RutaArchivoResp = AccionGuardar.RutaArchivoResp;
                            EDAccion.Nombre_Responsable = AccionGuardar.Nombre_Responsable;
                            EDAccion.Cargo_Responsable = AccionGuardar.Cargo_Responsable;
                            EDAccion.Fk_Id_Empresa = AccionGuardar.Fk_Id_Empresa;
                            EDAccion.Origen = AccionGuardar.Origen;
                            EDAccion.Otro_Origen = AccionGuardar.Otro_Origen;
                            ProbarGuardar = LNAcciones.GuardarAccionbool(EDAccion);
                            if (ProbarGuardar)
                            {
                                UltimoId = LNAcciones.GuardarAccion(usuarioActual.IdEmpresa);
                            }
                            else
                            {
                                status = "El proceso de guardado falló, por favor intente nuevamente";
                                probar = false;
                                return Json(new { status, probar });
                            }

                        }
                        catch (Exception)
                        {
                            status = "No se pudo guardar la 'Acción' en la aplicación, por favor intente de nuevo";
                            probar = false;
                            return Json(new { status, probar });
                        }
                        #region GuardarFirmas
                        if (SrcAuditor != null)
                        {
                            if (SrcAuditor != "")
                            {
                                if (SrcAuditor != SrcWhite)
                                {
                                    try
                                    {
                                        string b64 = SrcAuditor;
                                        b64 = b64.Replace("data:image/png;base64,", "");
                                        Image Imagen = Base64ToImage(b64);
                                        string outputPath = (Server.MapPath(Path.Combine(RutaFirmas, NombreArchivoAuditor)));
                                        CrearCarpeta(RutaFirmas);
                                        Imagen.Save(outputPath, ImageFormat.Png);
                                    }
                                    catch (Exception)
                                    {
                                    }
                                }
                            }
                        }
                        if (SrcResponsable != null)
                        {
                            if (SrcResponsable != "")
                            {
                                if (SrcResponsable != SrcWhite)
                                {
                                    try
                                    {
                                        string b64 = SrcResponsable;
                                        b64 = b64.Replace("data:image/png;base64,", "");
                                        Image Imagen = Base64ToImage(b64);
                                        string outputPath = (Server.MapPath(Path.Combine(RutaFirmas, NombreArchivoResponsable)));
                                        CrearCarpeta(RutaFirmas);
                                        Imagen.Save(outputPath, ImageFormat.Png);
                                    }
                                    catch (Exception)
                                    {
                                    }
                                }
                            }
                        }
                        #endregion
                        #region hallazgo&Analisis
                        foreach (var item in PostHallazgos)
                        {
                            item.Fk_Id_Accion = UltimoId;
                            LNAcciones.GuardarHallazgo(item);
                        }
                        foreach (var item in PostAnalisis)
                        {
                            item.Fk_Id_Accion = UltimoId;
                            LNAcciones.GuardarAnalisis(item);
                        }
                        #endregion
                        #region Actividad
                        int cont = 0;
                        foreach (var item in PostActividad)
                        {
                            bool ProbarGuardarAct = false;
                            string imagenSrc = item.FirmaScrImage;
                            string NombreArchivo = "AccActFirma" + cont.ToString() + FechaString() + RandomString(6) + ".png";
                            item.Fk_Id_Accion = UltimoId;
                            DateTime dateoFinalizacion = item.FechaFinalizacion;
                            item.FechaFinalizacion = dateoFinalizacion;
                            if (imagenSrc != null)
                            {
                                if (imagenSrc != "")
                                {
                                    if (imagenSrc != SrcWhite)
                                    {
                                        item.NombreArchivoAct = NombreArchivo;
                                        item.RutaArchivoAct = RutaFirmas;
                                    }
                                }
                            }

                            ProbarGuardarAct = LNAcciones.GuardarActividad(item);
                            if (imagenSrc != null && ProbarGuardarAct == true)
                            {
                                if (imagenSrc != "")
                                {
                                    if (imagenSrc != SrcWhite)
                                    {
                                        try
                                        {
                                            string b64 = imagenSrc;
                                            b64 = b64.Replace("data:image/png;base64,", "");
                                            Image ImagenAct = Base64ToImage(b64);
                                            string outputPath = (Server.MapPath(Path.Combine(RutaFirmas, NombreArchivo)));
                                            CrearCarpeta(RutaFirmas);
                                            ImagenAct.Save(outputPath, ImageFormat.Png);
                                        }
                                        catch (Exception)
                                        {
                                        }
                                    }
                                }
                            }
                            cont = cont + 1;
                        }
                        #endregion
                        #region Seguimiento
                        cont = 0;
                        foreach (var item in PostSeguimiento)
                        {
                            bool ProbarGuardarSeg = false;
                            string imagenSrc = item.FirmaScrImage;
                            string NombreArchivo = "AccSegFirma" + cont.ToString() + FechaString() + RandomString(6) + ".png";
                            item.Fk_Id_Accion = UltimoId;
                            DateTime dateSeg = item.Fecha_Seg;
                            item.Fecha_Seg = dateSeg;

                            if (imagenSrc != null)
                            {
                                if (imagenSrc != "")
                                {
                                    if (imagenSrc != SrcWhite)
                                    {
                                        item.NombreArchivoSeg = NombreArchivo;
                                        item.RutaArchivoSeg = RutaFirmas;
                                    }
                                }
                            }

                            ProbarGuardarSeg = LNAcciones.GuardarSeguimiento(item);
                            if (imagenSrc != null && ProbarGuardarSeg == true)
                            {
                                if (imagenSrc != "")
                                {
                                    if (imagenSrc != SrcWhite)
                                    {
                                        try
                                        {
                                            string b64 = imagenSrc;
                                            b64 = b64.Replace("data:image/png;base64,", "");
                                            Image ImagenAct = Base64ToImage(b64);
                                            string outputPath = (Server.MapPath(Path.Combine(RutaFirmas, NombreArchivo)));
                                            CrearCarpeta(RutaFirmas);
                                            ImagenAct.Save(outputPath, ImageFormat.Png);
                                        }
                                        catch (Exception)
                                        {
                                        }
                                    }
                                }
                            }
                        }
                        #endregion
                        #region Archivos
                        List<string> ArchivosTemporalesEliminar = new List<string>();
                        foreach (var item in PostArchivos)
                        {
                            ArchivosAccion ArchivoNuevo = new ArchivosAccion();
                            try
                            {
                                try
                                {
                                    string RutaTemporal = item.Ruta;
                                    string RutaNueva = RutaArchivosBD;
                                    string Nombre = item.NombreArchivo;
                                    string PathOrigen = Server.MapPath(Path.Combine(RutaTemporal, Nombre));
                                    string PathDestino = Server.MapPath(Path.Combine(RutaNueva, Nombre));
                                    if (System.IO.File.Exists(PathOrigen))
                                    {
                                        bool probarGuardado = false;
                                        CrearCarpeta(RutaNueva);
                                        System.IO.File.Move(PathOrigen, PathDestino);
                                        item.Fk_Id_Accion = UltimoId;
                                        item.Ruta = RutaNueva;
                                        item.NombreArchivo = item.NombreArchivo;
                                        probarGuardado = LNAcciones.GuardarArchivosAccion(item);
                                        if (probarGuardado == false)
                                        {
                                            try
                                            {
                                                ArchivosTemporalesEliminar.Add(Server.MapPath(Path.Combine(RutaNueva, Nombre)));
                                            }
                                            catch (Exception)
                                            {

                                            }
                                        }
                                        ArchivosTemporalesEliminar.Add(Server.MapPath(Path.Combine(RutaTemporal, Nombre)));

                                    }
                                }
                                catch (Exception)
                                {
                                }
                            }
                            catch (Exception)
                            {
                            }
                        }
                        foreach (var item in ArchivosTemporalesEliminar)
                        {
                            try
                            {
                                System.IO.File.Delete(item);
                            }
                            catch (Exception)
                            {
                            }
                        }

                        #endregion
                        Session.Remove("EDAccion");
                        probar = true;
                        status = "Acción Agregada";
                        return Json(new { status, probar });
                    }
                }
                else
                {
                    status = "No ha suministrado la información completa, por favor revise el formulario y vuelva a intentar";
                    if (incomingAccion.Fecha_hall == DateTime.MinValue)
                    {
                        Validacion[0] = true;
                        ValidacionStr[0] = "Verifique que haya suministrado la FECHA DE HALLAZGO";
                    }
                    if (incomingAccion.Fecha_hall == null)
                    {
                        Validacion[0] = true;
                        ValidacionStr[0] = "Verifique que haya suministrado la FECHA DE HALLAZGO";
                    }
                    if (incomingAccion.Fecha_dil == DateTime.MinValue)
                    {
                        Validacion[1] = true;
                        ValidacionStr[1] = "Verifique que haya suministrado la FECHA DE DILIGENCIAMIENTO";
                    }
                    if (incomingAccion.Tipo == null)
                    {
                        Validacion[2] = true;
                        ValidacionStr[2] = "Verifique que haya suministrado el TIPO de acción";
                    }
                    if (incomingAccion.Clase == null)
                    {
                        Validacion[3] = true;
                        ValidacionStr[3] = "Verifique que haya suministrado la CLASE de acción";
                    }
                    if (incomingAccion.Halla_Num_Doc == null)
                    {
                        Validacion[4] = true;
                        ValidacionStr[4] = "Verifique que haya suministrado el NÚMERO DE DOCUMENTO";
                    }
                    if (incomingAccion.Halla_Nombre == null)
                    {
                        Validacion[5] = true;
                        ValidacionStr[5] = "Verifique que haya suministrado el NOMBRE del que identificó el hallazgo";
                    }
                    if (incomingAccion.Halla_TipoDoc == null)
                    {
                        Validacion[6] = true;
                        ValidacionStr[6] = "Verifique que haya suministrado el TIPO DE DOCUMENTO";
                    }
                    if (incomingAccion.Halla_Cargo == null)
                    {
                        Validacion[7] = true;
                        ValidacionStr[7] = "Verifique que haya suministrado el CARGO del que identificó el hallazgo";
                    }
                    if (incomingAccion.Halla_Sede == null)
                    {
                        Validacion[8] = true;
                        ValidacionStr[8] = "Verifique que haya suministrado la SEDE del hallazgo";
                    }
                    if (incomingAccion.Origen == null)
                    {
                        Validacion[11] = true;
                        ValidacionStr[11] = "Verifique que haya suministrado el ORIGEN del hallazgo";
                    }
                    probar = false;
                    return Json(new { status, probar, Validacion, ValidacionStr });
                }
            }
            catch (Exception)
            {
                status = "El proceso de guardado fallo, por favor intente nuevamente";
                probar = false;
            }
            return Json(new { status, probar });
        }
        [HttpPost]
        public JsonResult postActualizarEdicion(Accion incomingAccion)
        {
            int IdAccion = incomingAccion.Pk_Id_Accion;
            string status = "";
            int ubicacion = 0;
            bool probar = false;
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                status = "El usuario no ha iniciado sesión en el sistema";
                ubicacion = 0;
                probar = false;
                TempData.Keep("EditarAccion" + IdAccion);
                return Json(new { status, probar, ubicacion });

            }
            bool[] Validacion = new bool[12];
            string[] ValidacionStr = new string[12];
            for (int i = 0; i < 12; i++)
            {
                Validacion[i] = false;
                ValidacionStr[i] = "";
            }
            try
            {
                if (ModelState.IsValid)
                {
                    if (incomingAccion.Cambio_Doc == "Si")
                    {
                        if (incomingAccion.Des_Cambio_Doc == null)
                        {
                            Validacion[10] = true;
                            ValidacionStr[10] = "Ha registrado que se requiere un cambio de documentación, pero la descripción del cambio es obligatorio";
                            status = "No ha suministrado la información completa, por favor revise el formulario y vuelva a intentar";
                            probar = false;
                            TempData.Keep("EditarAccion" + IdAccion);
                            return Json(new { status, probar, Validacion, ValidacionStr });
                        }
                        else
                        {
                            if (incomingAccion.Des_Cambio_Doc == "")
                            {
                                Validacion[10] = true;
                                ValidacionStr[10] = "Ha registrado que se requiere un cambio de documentación, pero la descripción del cambio es obligatorio";
                                status = "No ha suministrado la información completa, por favor revise el formulario y vuelva a intentar";
                                probar = false;
                                TempData.Keep("EditarAccion" + IdAccion);
                                return Json(new { status, probar, Validacion, ValidacionStr });
                            }
                        }
                    }
                    else
                    {
                        incomingAccion.Des_Cambio_Doc = null;
                    }
                    //Recuperar la variable Session
                    var AccionSession = TempData["EditarAccion" + IdAccion] as EDAccion;
                    var PostHallazgos = AccionSession.HallazgoLista;
                    var PostSeguimiento = AccionSession.SeguimientoLista;
                    var PostAnalisis = AccionSession.AnalisisLista;
                    var PostArchivos = AccionSession.ArchivosLista;
                    var PostActividad = AccionSession.ActividadLista;

                    var SrcAuditor = AccionSession.FirmaScrImageAud;
                    var SrcResponsable = AccionSession.FirmaScrImageRes;
                    if (SrcAuditor == null)
                    {
                        SrcAuditor = SrcWhite;
                    }
                    if (SrcResponsable == null)
                    {
                        SrcResponsable = SrcWhite;
                    }
                    string NombreArchivoAuditor = "AccFirmaAud" + FechaString() + RandomString(3) + ".png";
                    string NombreArchivoResponsable = "AccFirmaRes" + FechaString() + RandomString(3) + ".png";
                    //Validar que exista por lo menos 1 hallazgo
                    if (PostHallazgos.Count == 0)
                    {
                        Validacion[9] = true;
                        ValidacionStr[9] = "Verifique que haya suministrado la descripción del hallazgo y el proceso";
                        status = "No ha suministrado la información completa, por favor revise el formulario y vuelva a intentar";
                        TempData.Keep("EditarAccion" + IdAccion);
                        probar = false;
                        return Json(new { status, probar, Validacion, ValidacionStr });
                    }
                    //Empezar a actualizar
                    if (true)
                    {
                        Accion AccionGuardar = incomingAccion;
                        AccionGuardar.Id_Accion = AccionSession.Id_Accion;
                        AccionGuardar.Fk_Id_Empresa = usuarioActual.IdEmpresa;
                        AccionGuardar.Fecha_ocurrencia = DateTime.Today;
                        DateTime datedil = incomingAccion.Fecha_dil;
                        AccionGuardar.Fecha_dil = datedil;
                        AccionGuardar.Fecha_hall = incomingAccion.Fecha_hall ?? DateTime.Now;
                        AccionGuardar.Tipo = incomingAccion.Tipo;
                        AccionGuardar.Clase = incomingAccion.Clase;
                        AccionGuardar.Halla_Num_Doc = incomingAccion.Halla_Num_Doc;
                        AccionGuardar.Halla_Nombre = incomingAccion.Halla_Nombre;
                        AccionGuardar.Halla_TipoDoc = incomingAccion.Halla_TipoDoc;
                        AccionGuardar.Correccion = incomingAccion.Correccion;
                        AccionGuardar.Causa_Raiz = incomingAccion.Causa_Raiz;
                        AccionGuardar.Cambio_Doc = incomingAccion.Cambio_Doc;
                        AccionGuardar.Des_Cambio_Doc = incomingAccion.Des_Cambio_Doc;
                        AccionGuardar.Verificacion = incomingAccion.Verificacion;
                        AccionGuardar.Eficacia = incomingAccion.Eficacia;
                        AccionGuardar.Estado = incomingAccion.Estado;
                        AccionGuardar.Nombre_Auditor = incomingAccion.Nombre_Auditor;
                        AccionGuardar.Cargo_Auditor = incomingAccion.Cargo_Auditor;
                        AccionGuardar.Nombre_Responsable = incomingAccion.Nombre_Responsable;
                        AccionGuardar.Cargo_Responsable = incomingAccion.Cargo_Responsable;
                        AccionGuardar.Halla_Cargo = incomingAccion.Halla_Cargo;
                        AccionGuardar.Origen = incomingAccion.Origen;
                        AccionGuardar.Otro_Origen = incomingAccion.Otro_Origen;
                        int IdSede = 0;
                        bool isNumeric = int.TryParse(incomingAccion.Halla_Sede, out IdSede);
                        if (isNumeric == true)
                        {
                            List<EDSede> ListaSede = new List<EDSede>();
                            ListaSede = LNEmpresa.ObtenerSedesPorNit(usuarioActual.NitEmpresa);
                            EDSede EDSede = ListaSede.Find(s => s.IdSede == IdSede);
                            if (EDSede != null)
                            {
                                AccionGuardar.Halla_Sede = incomingAccion.Halla_Sede;
                            }
                            else
                            {
                                Validacion[8] = true;
                                ValidacionStr[8] = "La sede elegida no se encuentra registrada";
                                status = "La sede elegida no se encuentra registrada";
                                TempData.Keep("EditarAccion" + IdAccion);
                                probar = false;
                                return Json(new { status, probar, Validacion, ValidacionStr });
                            }
                        }
                        else
                        {
                            TempData.Keep("EditarAccion" + IdAccion);
                            status = "La sede elegida no se encuentra registrada";
                            probar = false;
                            return Json(new { status, probar });
                        }
                        EDAccion AccionFirmas = LNAcciones.ConsultaAccion(IdAccion, usuarioActual.IdEmpresa);
                        AccionGuardar.NombreArchivoAuditor = AccionFirmas.NombreArchivoAuditor;
                        AccionGuardar.NombreArchivoResp = AccionFirmas.NombreArchivoResp;
                        AccionGuardar.RutaArchivoAuditor = AccionFirmas.RutaArchivoAuditor;
                        AccionGuardar.RutaArchivoResp = AccionFirmas.RutaArchivoResp;

                        string RutaAudActual = AccionFirmas.RutaArchivoAuditor;
                        string NombreAudActual = AccionFirmas.NombreArchivoAuditor;

                        string RutaResActual = AccionFirmas.RutaArchivoResp;
                        string NombreResActual = AccionFirmas.NombreArchivoResp;
                        string pathAudActual = "";
                        string pathResActual = "";
                        string Image64ActualAud = "";
                        string Image64ActualRes = "";
                        if (RutaAudActual != null && NombreAudActual != null)
                        {
                            pathAudActual = Server.MapPath(Path.Combine(RutaAudActual, NombreAudActual));
                        }
                        if (RutaResActual != null && NombreResActual != null)
                        {
                            pathResActual = Server.MapPath(Path.Combine(RutaResActual, NombreResActual));
                        }

                        try
                        {
                            Image64ActualAud = UrlToBase64(pathAudActual);
                        }
                        catch (Exception)
                        {

                        }
                        try
                        {
                            Image64ActualRes = UrlToBase64(pathResActual);
                        }
                        catch (Exception)
                        {

                        }
                        bool ProbarCambioImagenAud = false;
                        bool ProbarCambioImagenRes = false;

                        if (SrcAuditor.Replace("data:image/png;base64,", "") != Image64ActualAud && Image64ActualAud != "")
                        {
                            ProbarCambioImagenAud = true;
                        }
                        if (SrcResponsable.Replace("data:image/png;base64,", "") != Image64ActualRes && Image64ActualRes != "")
                        {
                            ProbarCambioImagenRes = true;
                        }
                        if (Image64ActualAud == "")
                        {
                            ProbarCambioImagenAud = true;
                        }
                        if (Image64ActualRes == "")
                        {
                            ProbarCambioImagenRes = true;
                        }

                        List<string> ArchivosTemporalesEliminar = new List<string>();
                        if (ProbarCambioImagenAud)
                        {
                            ArchivosTemporalesEliminar.Add(pathAudActual);
                            if (SrcAuditor != null)
                            {
                                if (SrcAuditor != "")
                                {
                                    if (SrcAuditor != SrcWhite)
                                    {
                                        AccionGuardar.NombreArchivoAuditor = NombreArchivoAuditor;
                                        AccionGuardar.RutaArchivoAuditor = RutaFirmas;
                                    }
                                    else
                                    {
                                        AccionGuardar.NombreArchivoAuditor = null;
                                        AccionGuardar.RutaArchivoAuditor = null;
                                    }
                                }
                            }
                        }
                        if (ProbarCambioImagenRes)
                        {
                            ArchivosTemporalesEliminar.Add(pathResActual);
                            if (SrcResponsable != null)
                            {
                                if (SrcResponsable != "")
                                {
                                    if (SrcResponsable != SrcWhite)
                                    {
                                        AccionGuardar.NombreArchivoResp = NombreArchivoResponsable;
                                        AccionGuardar.RutaArchivoResp = RutaFirmas;
                                    }
                                    else
                                    {
                                        AccionGuardar.NombreArchivoResp = null;
                                        AccionGuardar.RutaArchivoResp = null;
                                    }
                                }
                            }
                        }

                        bool RealizaActualizacion = false;
                        try
                        {
                            EDAccion EDAccion = new EDAccion();
                            EDAccion.Pk_Id_Accion = AccionGuardar.Pk_Id_Accion;
                            EDAccion.Id_Accion = AccionGuardar.Id_Accion;
                            EDAccion.Tipo = AccionGuardar.Tipo;
                            EDAccion.Fecha_dil = AccionGuardar.Fecha_dil;
                            EDAccion.Fecha_ocurrencia = AccionGuardar.Fecha_ocurrencia;
                            EDAccion.Clase = AccionGuardar.Clase;
                            EDAccion.Fecha_hall = AccionGuardar.Fecha_hall;
                            EDAccion.Halla_Num_Doc = AccionGuardar.Halla_Num_Doc;
                            EDAccion.Halla_Nombre = AccionGuardar.Halla_Nombre;
                            EDAccion.Halla_TipoDoc = AccionGuardar.Halla_TipoDoc;
                            EDAccion.Halla_Cargo = AccionGuardar.Halla_Cargo;
                            EDAccion.Halla_Sede = AccionGuardar.Halla_Sede;
                            EDAccion.Correccion = AccionGuardar.Correccion;
                            EDAccion.Causa_Raiz = AccionGuardar.Causa_Raiz;
                            EDAccion.Cambio_Doc = AccionGuardar.Cambio_Doc;
                            EDAccion.Des_Cambio_Doc = AccionGuardar.Des_Cambio_Doc;
                            EDAccion.Verificacion = AccionGuardar.Verificacion;
                            EDAccion.Eficacia = AccionGuardar.Eficacia;
                            EDAccion.Estado = AccionGuardar.Estado;
                            EDAccion.NombreArchivoAuditor = AccionGuardar.NombreArchivoAuditor;
                            EDAccion.RutaArchivoAuditor = AccionGuardar.RutaArchivoAuditor;
                            EDAccion.Nombre_Auditor = AccionGuardar.Nombre_Auditor;
                            EDAccion.Cargo_Auditor = AccionGuardar.Cargo_Auditor;
                            EDAccion.NombreArchivoResp = AccionGuardar.NombreArchivoResp;
                            EDAccion.RutaArchivoResp = AccionGuardar.RutaArchivoResp;
                            EDAccion.Nombre_Responsable = AccionGuardar.Nombre_Responsable;
                            EDAccion.Cargo_Responsable = AccionGuardar.Cargo_Responsable;
                            EDAccion.Fk_Id_Empresa = AccionGuardar.Fk_Id_Empresa;
                            EDAccion.Origen = AccionGuardar.Origen;
                            EDAccion.Otro_Origen = AccionGuardar.Otro_Origen;
                            RealizaActualizacion = LNAcciones.EditarAccion(EDAccion);
                        }
                        catch (Exception)
                        {
                            //regresar error
                            status = "No se pudo actualizar la 'Accion' en la aplicación, por favor intente de nuevo";
                            ubicacion = 0;
                            probar = false;
                            TempData.Keep("EditarAccion" + IdAccion);
                            return Json(new { status, probar, ubicacion });
                        }

                        if (RealizaActualizacion)
                        {
                            if (SrcAuditor != null && ProbarCambioImagenAud)
                            {
                                if (SrcAuditor != "")
                                {
                                    if (SrcAuditor != SrcWhite)
                                    {
                                        try
                                        {
                                            string b64 = SrcAuditor;
                                            b64 = b64.Replace("data:image/png;base64,", "");
                                            Image Imagen = Base64ToImage(b64);
                                            string outputPath = (Server.MapPath(Path.Combine(RutaFirmas, NombreArchivoAuditor)));
                                            CrearCarpeta(RutaFirmas);
                                            Imagen.Save(outputPath, ImageFormat.Png);
                                        }
                                        catch (Exception)
                                        {
                                        }
                                    }
                                }
                            }
                            if (SrcResponsable != null && ProbarCambioImagenRes)
                            {
                                if (SrcResponsable != "")
                                {
                                    if (SrcResponsable != SrcWhite)
                                    {
                                        try
                                        {
                                            string b64 = SrcResponsable;
                                            b64 = b64.Replace("data:image/png;base64,", "");
                                            Image Imagen = Base64ToImage(b64);
                                            string outputPath = (Server.MapPath(Path.Combine(RutaFirmas, NombreArchivoResponsable)));
                                            CrearCarpeta(RutaFirmas);
                                            Imagen.Save(outputPath, ImageFormat.Png);
                                        }
                                        catch (Exception)
                                        {
                                        }
                                    }
                                }
                            }
                        }
                        foreach (var item in ArchivosTemporalesEliminar)
                        {
                            try
                            {
                                System.IO.File.Delete(item);
                            }
                            catch (Exception)
                            {
                            }
                        }
                        ArchivosTemporalesEliminar.Clear();
                        #region Hallazgos
                        EDHallazgo HallazgosActualizar = PostHallazgos.FirstOrDefault();
                        LNAcciones.EditarHallazgo(HallazgosActualizar);
                        #endregion
                        #region actividades
                        // Guardar Actividades Nuevas
                        int cont = 0;
                        foreach (var item in PostActividad)
                        {
                            if (item.Estado != 1)
                            {
                                bool ProbarGuardarAct = false;
                                string imagenSrc = item.FirmaScrImage;
                                string NombreArchivo = "AccActFirma" + cont.ToString() + FechaString() + RandomString(6) + ".png";
                                item.Fk_Id_Accion = IdAccion;
                                DateTime dateoFinalizacion = item.FechaFinalizacion;
                                item.FechaFinalizacion = dateoFinalizacion;
                                if (imagenSrc != null)
                                {
                                    if (imagenSrc != "")
                                    {
                                        if (imagenSrc != SrcWhite)
                                        {
                                            item.NombreArchivoAct = NombreArchivo;
                                            item.RutaArchivoAct = RutaFirmas;
                                        }
                                    }
                                }
                                ProbarGuardarAct = LNAcciones.GuardarActividad(item);
                                if (imagenSrc != null && ProbarGuardarAct == true)
                                {
                                    if (imagenSrc != "")
                                    {
                                        if (imagenSrc != SrcWhite)
                                        {
                                            try
                                            {
                                                string b64 = imagenSrc;
                                                b64 = b64.Replace("data:image/png;base64,", "");
                                                Image ImagenAct = Base64ToImage(b64);
                                                string outputPath = (Server.MapPath(Path.Combine(RutaFirmas, NombreArchivo)));
                                                CrearCarpeta(RutaFirmas);
                                                ImagenAct.Save(outputPath, ImageFormat.Png);
                                            }
                                            catch (Exception)
                                            {
                                            }
                                        }
                                    }
                                }
                                cont = cont + 1;
                            }
                        }
                        #endregion
                        #region Seguimientos
                        // Guardar Seguimiento
                        cont = 0;
                        foreach (var item in PostSeguimiento)
                        {
                            if (item.Estado != 1)
                            {
                                bool ProbarGuardarSeg = false;
                                string imagenSrc = item.FirmaScrImage;
                                string NombreArchivo = "AccSegFirma" + cont.ToString() + FechaString() + RandomString(6) + ".png";
                                item.Fk_Id_Accion = IdAccion;
                                DateTime dateSeg = item.Fecha_Seg;
                                item.Fecha_Seg = dateSeg;

                                if (imagenSrc != null)
                                {
                                    if (imagenSrc != "")
                                    {
                                        if (imagenSrc != SrcWhite)
                                        {
                                            item.NombreArchivoSeg = NombreArchivo;
                                            item.RutaArchivoSeg = RutaFirmas;
                                        }
                                    }
                                }
                                ProbarGuardarSeg = LNAcciones.GuardarSeguimiento(item);
                                if (imagenSrc != null && ProbarGuardarSeg == true)
                                {
                                    if (imagenSrc != "")
                                    {
                                        if (imagenSrc != SrcWhite)
                                        {
                                            try
                                            {
                                                string b64 = imagenSrc;
                                                b64 = b64.Replace("data:image/png;base64,", "");
                                                Image ImagenAct = Base64ToImage(b64);
                                                string outputPath = (Server.MapPath(Path.Combine(RutaFirmas, NombreArchivo)));
                                                CrearCarpeta(RutaFirmas);
                                                ImagenAct.Save(outputPath, ImageFormat.Png);
                                            }
                                            catch (Exception)
                                            {
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                EDSeguimiento EDActual = LNAcciones.ConsultarSeguimiento(item.Pk_Id_Seguimiento);
                                EDSeguimiento EDSeguimiento = item;
                                string NombreArchivo = "AccSegFirma" + cont.ToString() + FechaString() + RandomString(6) + ".png";
                                string Image64Seg = "";
                                bool CambioImagen = false;

                                if (EDActual.RutaArchivoSeg != null && EDActual.NombreArchivoSeg != null)
                                {
                                    try
                                    {
                                        Image64Seg = UrlToBase64(Server.MapPath(Path.Combine(EDActual.RutaArchivoSeg, EDActual.NombreArchivoSeg)));
                                    }
                                    catch (Exception)
                                    {
                                    }
                                    if (item.FirmaScrImage != null)
                                    {
                                        if (item.FirmaScrImage.Replace("data:image/png;base64,", "") != Image64Seg)
                                        {
                                            CambioImagen = true;
                                        }
                                    }
                                    else
                                    {
                                        CambioImagen = true;
                                    }
                                }
                                else
                                {
                                    CambioImagen = true;
                                }
                                if (CambioImagen)
                                {
                                    if (item.FirmaScrImage != null)
                                    {
                                        if (item.FirmaScrImage != "")
                                        {
                                            if (item.FirmaScrImage != SrcWhite)
                                            {
                                                EDSeguimiento.NombreArchivoSeg = NombreArchivo;
                                                EDSeguimiento.RutaArchivoSeg = RutaFirmas;
                                            }
                                            else
                                            {
                                                EDSeguimiento.NombreArchivoSeg = null;
                                                EDSeguimiento.RutaArchivoSeg = null;
                                                try
                                                {
                                                    ArchivosTemporalesEliminar.Add(Server.MapPath(Path.Combine(EDActual.RutaArchivoSeg, EDActual.NombreArchivoSeg)));
                                                }
                                                catch (Exception)
                                                {

                                                }
                                            }
                                        }
                                        else
                                        {
                                            EDSeguimiento.NombreArchivoSeg = null;
                                            EDSeguimiento.RutaArchivoSeg = null;
                                            try
                                            {
                                                ArchivosTemporalesEliminar.Add(Server.MapPath(Path.Combine(EDActual.RutaArchivoSeg, EDActual.NombreArchivoSeg)));
                                            }
                                            catch (Exception)
                                            {

                                            }
                                        }
                                    }
                                    else
                                    {
                                        EDSeguimiento.NombreArchivoSeg = null;
                                        EDSeguimiento.RutaArchivoSeg = null;
                                        try
                                        {
                                            ArchivosTemporalesEliminar.Add(Server.MapPath(Path.Combine(EDActual.RutaArchivoSeg, EDActual.NombreArchivoSeg)));
                                        }
                                        catch (Exception)
                                        {

                                        }
                                    }

                                }
                                bool EditarSeguimiento = LNAcciones.EditarSeguimiento(EDSeguimiento);
                                if (EditarSeguimiento && CambioImagen)
                                {

                                    if (item.FirmaScrImage != null)
                                    {
                                        if (item.FirmaScrImage != "")
                                        {
                                            if (item.FirmaScrImage != SrcWhite)
                                            {
                                                try
                                                {
                                                    string b64 = item.FirmaScrImage;
                                                    b64 = b64.Replace("data:image/png;base64,", "");
                                                    Image ImagenAct = Base64ToImage(b64);
                                                    string outputPath = (Server.MapPath(Path.Combine(RutaFirmas, NombreArchivo)));
                                                    CrearCarpeta(RutaFirmas);
                                                    ImagenAct.Save(outputPath, ImageFormat.Png);
                                                    ArchivosTemporalesEliminar.Add(Server.MapPath(Path.Combine(EDActual.RutaArchivoSeg, EDActual.NombreArchivoSeg)));
                                                }
                                                catch (Exception)
                                                {
                                                }
                                            }
                                        }
                                    }
                                }

                            }
                        }
                        #endregion
                        foreach (var item in ArchivosTemporalesEliminar)
                        {
                            try
                            {
                                System.IO.File.Delete(item);
                            }
                            catch (Exception)
                            {
                            }
                        }
                        ubicacion = 0;
                        probar = true;
                        status = "Acción Actualizada";
                        TempData.Remove("EditarAccion" + IdAccion);
                        return Json(new { status, probar, ubicacion });
                    }
                }
                else
                {
                    status = "No ha suministrado la información completa, por favor revise el formulario y vuelva a intentar";
                    if (incomingAccion.Fecha_hall == DateTime.MinValue)
                    {
                        Validacion[0] = true;
                        ValidacionStr[0] = "Verifique que haya suministrado la FECHA DE HALLAZGO";
                    }
                    if (incomingAccion.Fecha_hall == null)
                    {
                        Validacion[0] = true;
                        ValidacionStr[0] = "Verifique que haya suministrado la FECHA DE HALLAZGO";
                    }
                    if (incomingAccion.Fecha_dil == DateTime.MinValue)
                    {
                        Validacion[1] = true;
                        ValidacionStr[1] = "Verifique que haya suministrado la FECHA DE DILIGENCIAMIENTO";
                    }
                    if (incomingAccion.Tipo == null)
                    {
                        Validacion[2] = true;
                        ValidacionStr[2] = "Verifique que haya suministrado el TIPO de acción";
                    }
                    if (incomingAccion.Clase == null)
                    {
                        Validacion[3] = true;
                        ValidacionStr[3] = "Verifique que haya suministrado la CLASE de acción";
                    }
                    if (incomingAccion.Halla_Num_Doc == null)
                    {
                        Validacion[4] = true;
                        ValidacionStr[4] = "Verifique que haya suministrado el NÚMERO DE DOCUMENTO";
                    }
                    if (incomingAccion.Halla_Nombre == null)
                    {
                        Validacion[5] = true;
                        ValidacionStr[5] = "Verifique que haya suministrado el NOMBRE del que identificó el hallazgo";
                    }
                    if (incomingAccion.Halla_TipoDoc == null)
                    {
                        Validacion[6] = true;
                        ValidacionStr[6] = "Verifique que haya suministrado el TIPO DE DOCUMENTO";
                    }
                    if (incomingAccion.Halla_Cargo == null)
                    {
                        Validacion[7] = true;
                        ValidacionStr[7] = "Verifique que haya suministrado el CARGO del que identificó el hallazgo";
                    }
                    if (incomingAccion.Halla_Sede == null)
                    {
                        Validacion[8] = true;
                        ValidacionStr[8] = "Verifique que haya suministrado la SEDE del hallazgo";
                    }
                    if (incomingAccion.Origen == null)
                    {
                        Validacion[11] = true;
                        ValidacionStr[11] = "Verifique que haya suministrado el ORIGEN del hallazgo";
                    }
                    TempData.Keep("EditarAccion" + IdAccion);
                    probar = false;
                    return Json(new { status, probar, Validacion, ValidacionStr });
                }
            }
            catch (Exception)
            {
                status = "El proceso de actualización fallo, por favor intente nuevamente";
                ubicacion = 0;
                probar = false;
                TempData.Keep("EditarAccion" + IdAccion);
            }
            TempData.Keep("EditarAccion" + IdAccion);
            return Json(new { status, probar, ubicacion });
        }
        #endregion
        #region MetodosExtra
        private void CrearCarpeta(string path)
        {
            bool existeCarpeta = Directory.Exists(Server.MapPath(path));
            if (!existeCarpeta)
            {
                Directory.CreateDirectory(Server.MapPath(path));
            }

        }
        private string ImageToBase64String(System.Drawing.Image image, ImageFormat imageFormat)
        {
            using (MemoryStream memStream = new MemoryStream())
            {
                image.Save(memStream, imageFormat);
                string result = Convert.ToBase64String(memStream.ToArray());
                memStream.Close();

                return result;
            }

        }
        public Image Base64ToImage(string base64String)
        {
            base64String = base64String.Replace("data:image/png;base64,", "");
            // Convert Base64 String to byte[]
            byte[] imageBytes = Convert.FromBase64String(base64String);
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);

            // Convert byte[] to Image
            ms.Write(imageBytes, 0, imageBytes.Length);
            Image image = Image.FromStream(ms, true);

            return image;
        }
        public string UrlToBase64(string Path)
        {
            using (Image image = Image.FromFile(Path))
            {
                using (MemoryStream m = new MemoryStream())
                {
                    image.Save(m, image.RawFormat);
                    byte[] imageBytes = m.ToArray();
                    string base64String = Convert.ToBase64String(imageBytes);
                    return base64String;
                }
            }
        }
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        private string DiagramaArbol(List<string> lista_texto, List<string> Lista_Nivel)
        {
            Color color_fill_problema = new Color();
            Color color_fill_Opcion = new Color();
            Color color_Pen_Relaciones = new Color();
            Color color_fuente_Problema = new Color();
            Color color_fuente_Opcion = new Color();
            Color color_fondo = new Color();
            List<string> ListaOrdenada = new List<string>();
            List<string> ListaOrdenadaTexto = new List<string>();
            string[] NewList = Lista_Nivel.ToArray();
            Array.Sort(NewList);
            for (int i = 0; i < NewList.Length; i++)
            {
                string CodigoLista = NewList[i];
                ListaOrdenada.Add(CodigoLista);
                for (int i1 = 0; i1 < Lista_Nivel.Count; i1++)
                {
                    if (Lista_Nivel[i1] == CodigoLista)
                    {
                        ListaOrdenadaTexto.Add(lista_texto[i1]);
                    }
                }
            }
            Lista_Nivel = ListaOrdenada;
            lista_texto = ListaOrdenadaTexto;
            //dimension caja nivel 1 y 2
            int width_box12 = 200;
            int height_box12 = 150;
            int height_box3 = 90;
            //espacio horizontal entre cajas
            int esp_hor12 = 40;
            int esp_hor3 = 60;
            // desplazamiento horizontal entre niveles
            int desp_hor = 20;
            //espacio vertical entre cajas
            int esp_ver = 20;
            //tamaño fuente
            int t_fuente = 19;
            //ancho flechas
            int w_flecha = 2;
            //cuantos niveles tenemos en el treeview
            int niveles = 0;
            string[] vec_niveles = new string[Lista_Nivel.Count];
            string[] vec_niveles1 = new string[Lista_Nivel.Count];
            for (int i = 0; i < Lista_Nivel.Count; i++)
            {
                vec_niveles[i] = "";
                vec_niveles[i] = Lista_Nivel[i];
                vec_niveles1[i] = Lista_Nivel[i];
                char[] caracter = vec_niveles[i].ToCharArray();
                vec_niveles[i] = "";
                for (int i1 = 0; i1 < caracter.Length; i1++)
                {
                    int n1;
                    bool isNumeric = int.TryParse(caracter[i1].ToString(), out n1);
                    if (isNumeric != true)
                    {
                        vec_niveles[i] = vec_niveles[i] + caracter[i1].ToString();
                    }

                }
            }
            for (int i = 0; i < Lista_Nivel.Count; i++)
            {
                if (vec_niveles[i].Length + 1 > niveles)
                {
                    niveles = vec_niveles[i].Length + 1;
                }
            }
            if (niveles==2)
            {
                width_box12 = 400;
                height_box12 =90;
            }
            string[,] matriz_niveles = new string[Lista_Nivel.Count, niveles];
            int[] wb_vec3 = new int[niveles];
            int[] palabras_max = new int[niveles];
            for (int i = 0; i < niveles; i++)
            {
                wb_vec3[i] = 0;
                palabras_max[i] = 0;
            }
            //determinar tamaño de niveles 3 en adelante segun texto
            for (int i = 0; i < niveles; i++)
            {
                for (int i1 = 0; i1 < lista_texto.Count; i1++)
                {
                    if (vec_niveles[i1].Length + 1 == i + 1)
                    {
                        int numero_palabras = lista_texto[i1].Length;
                        if (palabras_max[i] < numero_palabras)
                        {
                            palabras_max[i] = numero_palabras;
                        }

                    }
                }
            }
            for (int i = 0; i < niveles; i++)
            {
                //0 a 20
                if (palabras_max[i] >= 0 && palabras_max[i] < 20)
                {
                    wb_vec3[i] = 300;
                }
                //20 a 40
                if (palabras_max[i] >= 20 && palabras_max[i] < 40)
                {
                    wb_vec3[i] = 400;
                }
                //40 a 60
                if (palabras_max[i] >= 40 && palabras_max[i] < 60)
                {
                    wb_vec3[i] = 470;
                }
                //60 a 70
                if (palabras_max[i] >= 60 && palabras_max[i] < 70)
                {
                    wb_vec3[i] = 300;
                }
                //70 a 80
                if (palabras_max[i] >= 70 && palabras_max[i] < 80)
                {
                    wb_vec3[i] = 320;
                }
                //80 a 90
                if (palabras_max[i] >= 80 && palabras_max[i] < 90)
                {
                    wb_vec3[i] = 350;
                }
                //90 a 100
                if (palabras_max[i] >= 90 && palabras_max[i] < 100)
                {
                    wb_vec3[i] = 400;
                }
                // Más de 100
                if (palabras_max[i] >= 100)
                {
                    wb_vec3[i] = 400;
                }

            }
            //definimos el desplazamiento horizontal de cada nivel 
            int[] despl = new int[niveles];
            for (int i = 0; i < niveles; i++)
            {
                despl[i] = desp_hor;
            }
            for (int i = 0; i < niveles; i++)
            {
                if (i >= 1 && i <= 2)
                {
                    despl[i] = despl[i - 1] + width_box12 + esp_hor12;
                }
                if (i >= 3)
                {
                    despl[i] = despl[i - 1] + wb_vec3[i - 1] + esp_hor3;
                }
            }
            //definimos la posición (x,y) y las dimensiones de cada caja
            int[] posicion_x = new int[lista_texto.Count];
            int[] posicion_y = new int[lista_texto.Count];
            int[] caja_w = new int[lista_texto.Count];
            int[] caja_h = new int[lista_texto.Count];

            int[] alturaxnivel = new int[niveles];
            int[] cajasxnivel = new int[niveles];
            for (int i = 0; i < niveles; i++)
            {
                alturaxnivel[i] = 0;
                cajasxnivel[i] = 0;
            }

            //Dimensión del canvas
            int diagrama_w = 0;
            int diagrama_h = 0;

            //  'nivel con mayor altura
            int nivel_mayor = 0;
            for (int i = 0; i < niveles; i++)
            {
                int pos_vertical = 40;
                for (int i1 = 0; i1 < lista_texto.Count; i1++)
                {
                    if (vec_niveles[i1].Length + 1 == i + 1)
                    {
                        if (i + 1 >= 1 && i + 1 <= 2)
                        {
                            cajasxnivel[i] = cajasxnivel[i] + 1;
                            pos_vertical = pos_vertical + height_box12;

                        }
                        if (i + 1 >= 3)
                        {
                            cajasxnivel[i] = cajasxnivel[i] + 1;
                            pos_vertical = pos_vertical + height_box3;
                        }
                    }
                }
                alturaxnivel[i] = pos_vertical;
            }
            int AlturaComp = 0;
            for (int i = 0; i < niveles; i++)
            {
                if (alturaxnivel[i]> AlturaComp)
                {
                    AlturaComp = alturaxnivel[i];
                    nivel_mayor = i + 1;
                }
            }

            int[] diferencia = new int[niveles];
            int[] redef_espacio_inicial1 = new int[niveles];
            int[] espacio_vertical_nuevo = new int[niveles];

            for (int i = 0; i < niveles; i++)
            {
                diferencia[i] = alturaxnivel[nivel_mayor - 1] - alturaxnivel[i];
                redef_espacio_inicial1[i] = diferencia[i]/2;
            }
            for (int i = 0; i < niveles; i++)
            {
                if (espacio_vertical_nuevo[i] == 0)
                {
                    espacio_vertical_nuevo[i] = 15;
                }
            }

            diagrama_w = 0;
            diagrama_h = 0;
            for (int i = 0; i < niveles; i++)
            {
                
                    int pos_vertical = redef_espacio_inicial1[i];
                    for (int i1 = 0; i1 < lista_texto.Count; i1++)
                    {
                        if (vec_niveles[i1].Length + 1 == i + 1)
                        {
                            if (i + 1 >= 1 && i + 1 <= 2)
                            {
                                posicion_y[i1] = pos_vertical+15;
                                posicion_x[i1] = despl[i];
                                pos_vertical = pos_vertical + height_box12 + espacio_vertical_nuevo[i]+30;
                                caja_w[i1] = width_box12;
                                caja_h[i1] = height_box12;
                            if (posicion_x[i1] + width_box12 > diagrama_w)
                            {
                                diagrama_w = posicion_x[i1] + width_box12;
                            }
                            if (posicion_y[i1] + height_box12+30 > diagrama_h)
                            {
                                diagrama_h = posicion_y[i1] + height_box12+30;
                            }
                        }
                            if (i + 1 >= 3)
                            {
                                posicion_y[i1] = pos_vertical+15;
                                posicion_x[i1] = despl[i];
                                pos_vertical = pos_vertical + height_box3 + espacio_vertical_nuevo[i];
                                caja_w[i1] = wb_vec3[i];
                                caja_h[i1] = height_box3;
                            if (posicion_x[i1] + width_box12 > diagrama_w)
                            {
                                diagrama_w = posicion_x[i1] + wb_vec3[i];
                            }
                            if (posicion_y[i1] + height_box3+30 > diagrama_h)
                            {
                                diagrama_h = posicion_y[i1] + height_box3+30;
                            }
                        }
                        }
                    }
               
            }
            //definir relaciones
            int numero_relaciones = 0;

            for (int i = 0; i < lista_texto.Count; i++)
            {
                if (vec_niveles[i].Length + 1 >= 2)
                {
                    numero_relaciones = numero_relaciones + 1;
                }
            }
            //  posicion inicio y final de la relación
            int[] rel_x1 = new int[numero_relaciones];
            int[] rel_y1 = new int[numero_relaciones];
            int[] rel_x2 = new int[numero_relaciones];
            int[] rel_y2 = new int[numero_relaciones];

            int i2 = 0;
            for (int i = 0; i < lista_texto.Count; i++)
            {
                if (vec_niveles[i].Length + 1 >= 2)
                {
                    //coordenadas hijo
                    rel_x1[i2] = posicion_x[i];
                    rel_y1[i2] = posicion_y[i] + (caja_h[i] / 2);
                    string value = vec_niveles1[i];
                    string[] substring = value.Split('/');
                    string comparar = "";

                    if (substring.Length != 0)
                    {
                        for (int i1 = 0; i1 < substring.Length - 1; i1++)
                        {
                            if (comparar == "")
                            {
                                comparar = comparar + substring[i1];
                            }
                            else
                            {
                                comparar = comparar + "/" + substring[i1];
                            }
                        }
                    }
                    int posicion_padre = 0;
                    for (int i1 = 0; i1 < Lista_Nivel.Count; i1++)
                    {
                        if (comparar == vec_niveles1[i1])
                        {
                            posicion_padre = i1;
                        }
                    }
                    //coordenadas padre
                    rel_x2[i2] = posicion_x[posicion_padre] + caja_w[posicion_padre];
                    rel_y2[i2] = posicion_y[posicion_padre] + (caja_h[posicion_padre] / 2);
                    i2 = i2 + 1;
                }
            }

            int mayor_h = 0;
            for (int i = 0; i < lista_texto.Count; i++)
            {
                if (posicion_y[i] > mayor_h)
                {
                    mayor_h = posicion_y[i];
                }

            }
            mayor_h = mayor_h + height_box12 + 10;


            color_fill_problema = Color.FromArgb(255, 106, 56);
            color_fill_Opcion = Color.FromArgb(255, 255, 255);
            color_Pen_Relaciones = Color.FromArgb(25, 72, 63);
            color_fuente_Problema = Color.FromArgb(255, 255, 255);
            color_fuente_Opcion = Color.FromArgb(0, 0, 0);
            color_fondo = Color.FromArgb(255, 255, 255);

            //Brushes
            SolidBrush BrushCajaProblema = new SolidBrush(color_fill_problema);
            SolidBrush LetraOpcion = new SolidBrush(color_fuente_Opcion);
            SolidBrush BrushOpcion = new SolidBrush(color_fill_Opcion);
            SolidBrush LetraProblema = new SolidBrush(color_fuente_Problema);
            SolidBrush BrushTriangulos = new SolidBrush(color_Pen_Relaciones);

            //Pens
            Pen pen_caja_Opcion = new Pen(color_fuente_Opcion);
            Pen pen_caja_Problema = new Pen(color_fill_problema);
            Pen pen_flechas = new Pen(color_Pen_Relaciones, w_flecha);

            //definir bitmap y fuente de letra
            Bitmap bitmap = new Bitmap(1, 1);

            Font FuenteProblema = new System.Drawing.Font("Century Gothic", t_fuente, FontStyle.Bold, GraphicsUnit.Pixel);
            Font FuenteOpcion = new System.Drawing.Font("Century Gothic", t_fuente, FontStyle.Bold, GraphicsUnit.Pixel);
            Graphics graphics = System.Drawing.Graphics.FromImage(bitmap);

            //tamaño del canvas
            bitmap = new Bitmap(bitmap, new Size(diagrama_w+15, diagrama_h));
            graphics = Graphics.FromImage(bitmap);
            //fondo del canvas
            graphics.Clear(color_fondo);

            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;

            //graficar cada caja
            for (int i = 0; i < lista_texto.Count; i++)
            {
                if (i == 0)
                {
                    StringFormat stringFormat = new StringFormat();
                    stringFormat.Alignment = StringAlignment.Center;
                    stringFormat.LineAlignment = StringAlignment.Center;
                    Rectangle r = new Rectangle(posicion_x[i], posicion_y[i], caja_w[i], caja_h[i]);
                    graphics.FillRectangle(BrushCajaProblema, r);
                    graphics.DrawRectangle(pen_caja_Problema, r);
                    graphics.DrawString(lista_texto[i], FuenteProblema, LetraProblema, r, stringFormat);
                    //Definir triangulo
                    Point[] points = { new Point(posicion_x[i], posicion_y[i] + caja_h[i]), new Point(posicion_x[i] + 10, posicion_y[i] + caja_h[i]), new Point(posicion_x[i], posicion_y[i] + caja_h[i] - 10) };
                    graphics.FillPolygon(BrushTriangulos, points);
                }
                else
                {
                    StringFormat stringFormat = new StringFormat();
                    stringFormat.Alignment = StringAlignment.Center;
                    stringFormat.LineAlignment = StringAlignment.Center;
                    Rectangle r = new Rectangle(posicion_x[i], posicion_y[i], caja_w[i], caja_h[i]);
                    graphics.FillRectangle(BrushOpcion, r);
                    graphics.DrawRectangle(pen_caja_Opcion, r);
                    graphics.DrawString(lista_texto[i], FuenteOpcion, LetraOpcion, r, stringFormat);
                    //Definir triangulo
                    Point[] points = { new Point(posicion_x[i], posicion_y[i] + caja_h[i]), new Point(posicion_x[i] + 10, posicion_y[i] + caja_h[i]), new Point(posicion_x[i], posicion_y[i] + caja_h[i] - 10) };
                    graphics.FillPolygon(BrushTriangulos, points);
                }
            }
            //graficar relaciones
            for (int i = 0; i < numero_relaciones; i++)
            {
                pen_flechas.EndCap = LineCap.DiamondAnchor;
                pen_flechas.StartCap = LineCap.ArrowAnchor;
                graphics.DrawLine(pen_flechas, rel_x1[i], rel_y1[i], rel_x2[i], rel_y2[i]);
            }
            graphics.Flush();
            graphics.Dispose();
            //copiar la información en base 64 del bitmap y ponerla en Imageurl del control
            string TextoFinal = "data:image/png;base64," + ImageToBase64String(bitmap, ImageFormat.Png);
            return TextoFinal;
        }
        private string DiagramaCausaEfecto(List<string> lista_texto, List<string> Lista_Nivel)
        {
            string TextoFinal = "";
            Color color_fill_problema = new Color();
            Color color_fill_Opcion = new Color();
            Color color_Pen_Relaciones = new Color();
            Color color_fuente_Problema = new Color();
            Color color_fuente_Opcion = new Color();
            Color color_fondo = new Color();
            List<string> ListaOrdenada = new List<string>();
            List<string> ListaOrdenadaTexto = new List<string>();
            string[] NewList = Lista_Nivel.ToArray();
            Array.Sort(NewList);
            for (int i = 0; i < NewList.Length; i++)
            {
                string CodigoLista = NewList[i];
                ListaOrdenada.Add(CodigoLista);
                for (int i1 = 0; i1 < Lista_Nivel.Count; i1++)
                {
                    if (Lista_Nivel[i1] == CodigoLista)
                    {
                        ListaOrdenadaTexto.Add(lista_texto[i1]);
                    }
                }
            }
            Lista_Nivel = ListaOrdenada;
            lista_texto = ListaOrdenadaTexto;

            //cuantos niveles tenemos en el Diagrama
            int niveles = 0;
            string[] vec_niveles = new string[Lista_Nivel.Count];
            string[] vec_niveles1 = new string[Lista_Nivel.Count];
            for (int i = 0; i < Lista_Nivel.Count; i++)
            {
                vec_niveles[i] = "";
                vec_niveles[i] = Lista_Nivel[i];
                vec_niveles1[i] = Lista_Nivel[i];
                char[] caracter = vec_niveles[i].ToCharArray();
                vec_niveles[i] = "";
                for (int i1 = 0; i1 < caracter.Length; i1++)
                {
                    int n1;
                    bool isNumeric = int.TryParse(caracter[i1].ToString(), out n1);
                    if (isNumeric != true)
                    {
                        vec_niveles[i] = vec_niveles[i] + caracter[i1].ToString();
                    }

                }
            }
            for (int i = 0; i < Lista_Nivel.Count; i++)
            {
                if (vec_niveles[i].Length + 1 > niveles)
                {
                    niveles = vec_niveles[i].Length + 1;
                }
            }

            if (Lista_Nivel.Count >= 7)
            {
                //Dimension Imagen
                int ImageWidth = 2000;
                int ImageHeight = 2000;
                //Dimension caja problema
                int width_box_problem = 350;
                int height_box_problem = 150;
                //Dimension caja segundo Nivel
                int width_box_2do = 350;
                int height_box_2do = 50;
                //Dimension caja tercer Nivel
                int width_box_3er = 450;
                int height_box_3er = 90;

                int diferencia = width_box_3er - width_box_2do;
                double esp_diferencia = diferencia / 2;
                int esp_resultado = (int)esp_diferencia;

                int desp_hor_desde_origen = 200;
                int desp_ver_desde_origen = 80;
                int desp_relacion = 30;

                int[] Ubicacion = new int[6];

                List<int> RelX = new List<int>();
                List<int> RelY = new List<int>();
                List<int> RelX1 = new List<int>();
                List<int> RelY1 = new List<int>();

                for (int i = 0; i < lista_texto.Count; i++)
                {
                    if (vec_niveles[i].Length == 1)
                    {
                        if (lista_texto[i] == "Maquinaria")
                        {
                            Ubicacion[0] = i;
                        }
                        if (lista_texto[i] == "Mano de Obra")
                        {
                            Ubicacion[1] = i;
                        }
                        if (lista_texto[i] == "Medio Ambiente")
                        {
                            Ubicacion[2] = i;
                        }
                        if (lista_texto[i] == "Materiales")
                        {
                            Ubicacion[3] = i;
                        }
                        if (lista_texto[i] == "Método")
                        {
                            Ubicacion[4] = i;
                        }
                        if (lista_texto[i] == "Mantenimiento")
                        {
                            Ubicacion[5] = i;
                        }
                    }
                }

                int[] box_w = new int[lista_texto.Count];
                int[] box_h = new int[lista_texto.Count];

                //Definir Dimensiones Cajas
                for (int i = 0; i < Lista_Nivel.Count; i++)
                {
                    if (Lista_Nivel[i] == "1")
                    {
                        box_w[i] = width_box_problem;
                        box_h[i] = height_box_problem;
                    }
                    if (vec_niveles[i].Length == 1)
                    {
                        box_w[i] = width_box_2do;
                        box_h[i] = height_box_2do;
                    }
                    if (vec_niveles[i].Length == 2)
                    {
                        string[] substring = Lista_Nivel[i].Split('/');
                        string Padre = substring[0] + "/" + substring[1];

                        box_w[i] = width_box_3er;
                        box_h[i] = height_box_3er;
                    }

                }


                int[] pos_x = new int[lista_texto.Count];
                int[] pos_y = new int[lista_texto.Count];

                // Definir Posicion Problema
                for (int i = 0; i < Lista_Nivel.Count; i++)
                {
                    if (vec_niveles[i].Length == 0)
                    {
                        pos_x[i] = 0;
                        pos_y[i] = 0;
                    }
                }
                // Definir Posicion segundo nivel


                pos_x[Ubicacion[0]] = -desp_hor_desde_origen - width_box_2do;
                pos_x[Ubicacion[1]] = 0;
                pos_x[Ubicacion[2]] = width_box_problem + desp_hor_desde_origen;
                pos_x[Ubicacion[3]] = -desp_hor_desde_origen - width_box_2do;
                pos_x[Ubicacion[4]] = 0;
                pos_x[Ubicacion[5]] = width_box_problem + desp_hor_desde_origen;

                int[] Altura_desp = new int[6];


                pos_y[Ubicacion[0]] = -desp_ver_desde_origen - height_box_2do;
                pos_y[Ubicacion[1]] = -desp_ver_desde_origen - height_box_2do;
                pos_y[Ubicacion[2]] = -desp_ver_desde_origen - height_box_2do;
                pos_y[Ubicacion[3]] = height_box_problem + desp_ver_desde_origen;
                pos_y[Ubicacion[4]] = height_box_problem + desp_ver_desde_origen;
                pos_y[Ubicacion[5]] = height_box_problem + desp_ver_desde_origen;

                Altura_desp[0] = -desp_ver_desde_origen - height_box_2do - desp_relacion;
                Altura_desp[1] = -desp_ver_desde_origen - height_box_2do - desp_relacion;
                Altura_desp[2] = -desp_ver_desde_origen - height_box_2do - desp_relacion;
                Altura_desp[3] = height_box_problem + desp_relacion + desp_ver_desde_origen;
                Altura_desp[4] = height_box_problem + desp_relacion + desp_ver_desde_origen;
                Altura_desp[5] = height_box_problem + desp_relacion + desp_ver_desde_origen;

                RelX.Add(pos_x[Ubicacion[0]] + (width_box_2do / 2)); RelY.Add(pos_y[Ubicacion[0]] + height_box_2do);
                RelX1.Add(0); RelY1.Add(5);
                RelX.Add(pos_x[Ubicacion[1]] + (width_box_2do / 2)); RelY.Add(pos_y[Ubicacion[1]] + height_box_2do);
                RelX1.Add(width_box_problem / 2); RelY1.Add(0);
                RelX.Add(pos_x[Ubicacion[2]] + (width_box_2do / 2)); RelY.Add(pos_y[Ubicacion[2]] + height_box_2do);
                RelX1.Add(width_box_problem); RelY1.Add(0);

                RelX.Add(pos_x[Ubicacion[3]] + (width_box_2do / 2)); RelY.Add(pos_y[Ubicacion[3]]);
                RelX1.Add(0); RelY1.Add(height_box_problem - 5);
                RelX.Add(pos_x[Ubicacion[4]] + (width_box_2do / 2)); RelY.Add(pos_y[Ubicacion[4]]);
                RelX1.Add(width_box_problem / 2); RelY1.Add(height_box_problem);
                RelX.Add(pos_x[Ubicacion[5]] + (width_box_2do / 2)); RelY.Add(pos_y[Ubicacion[5]]);
                RelX1.Add(width_box_problem); RelY1.Add(height_box_problem);


                int[] heightRel3Niv_X = new int[6];
                int[] heightRel3Niv_Y = new int[6];

                for (int i = 0; i < Lista_Nivel.Count; i++)
                {
                    if (vec_niveles[i].Length == 2)
                    {
                        string[] substring = Lista_Nivel[i].Split('/');
                        string Padre = substring[0] + "/" + substring[1];

                        for (int i1 = 0; i1 < 6; i1++)
                        {
                            if (Lista_Nivel[Ubicacion[i1]] == Padre)
                            {
                                if (i1 <= 2)
                                {
                                    pos_y[i] = Altura_desp[i1] - height_box_3er;



                                    RelX.Add(pos_x[Ubicacion[i1]] + (width_box_2do / 2) - 25);
                                    RelX1.Add(pos_x[Ubicacion[i1]] + (width_box_2do / 2));

                                    RelY.Add(pos_y[i] + (height_box_3er / 2));
                                    RelY1.Add(pos_y[i] + (height_box_3er / 2));

                                    Altura_desp[i1] = Altura_desp[i1] - height_box_3er - desp_relacion;
                                    heightRel3Niv_Y[i1] = pos_y[i] + (height_box_3er / 2);
                                }
                                else
                                {
                                    pos_y[i] = Altura_desp[i1] + height_box_2do;


                                    RelX.Add(pos_x[Ubicacion[i1]] + (width_box_2do / 2) - 25);
                                    RelX1.Add(pos_x[Ubicacion[i1]] + (width_box_2do / 2));

                                    RelY.Add(pos_y[i] + (height_box_3er / 2));
                                    RelY1.Add(pos_y[i] + (height_box_3er / 2));

                                    Altura_desp[i1] = Altura_desp[i1] + height_box_3er + desp_relacion;
                                    heightRel3Niv_Y[i1] = pos_y[i] + (height_box_3er / 2);
                                }
                                pos_x[i] = pos_x[Ubicacion[i1]] - esp_resultado - 250;
                            }
                        }
                    }
                }

                int contrel = RelX1.Count;


                if (heightRel3Niv_Y[0] != 0)
                {
                    RelX1.Add(pos_x[Ubicacion[0]] + (width_box_2do / 2)); RelY1.Add(pos_y[Ubicacion[0]]);
                    RelX.Add(pos_x[Ubicacion[0]] + (width_box_2do / 2)); RelY.Add(heightRel3Niv_Y[0]);
                }
                if (heightRel3Niv_Y[1] != 0)
                {
                    RelX1.Add(pos_x[Ubicacion[1]] + (width_box_2do / 2)); RelY1.Add(pos_y[Ubicacion[1]]);
                    RelX.Add(pos_x[Ubicacion[1]] + (width_box_2do / 2)); RelY.Add(heightRel3Niv_Y[1]);
                }
                if (heightRel3Niv_Y[2] != 0)
                {
                    RelX1.Add(pos_x[Ubicacion[2]] + (width_box_2do / 2)); RelY1.Add(pos_y[Ubicacion[2]]);
                    RelX.Add(pos_x[Ubicacion[2]] + (width_box_2do / 2)); RelY.Add(heightRel3Niv_Y[2]);
                }
                if (heightRel3Niv_Y[3] != 0)
                {
                    RelX1.Add(pos_x[Ubicacion[3]] + (width_box_2do / 2)); RelY1.Add(pos_y[Ubicacion[3]] + (height_box_2do));
                    RelX.Add(pos_x[Ubicacion[3]] + (width_box_2do / 2)); RelY.Add(heightRel3Niv_Y[3]);
                }
                if (heightRel3Niv_Y[4] != 0)
                {
                    RelX1.Add(pos_x[Ubicacion[4]] + (width_box_2do / 2)); RelY1.Add(pos_y[Ubicacion[4]] + (height_box_2do));
                    RelX.Add(pos_x[Ubicacion[4]] + (width_box_2do / 2)); RelY.Add(heightRel3Niv_Y[4]);
                }
                if (heightRel3Niv_Y[5] != 0)
                {
                    RelX1.Add(pos_x[Ubicacion[5]] + (width_box_2do / 2)); RelY1.Add(pos_y[Ubicacion[5]] + (height_box_2do));
                    RelX.Add(pos_x[Ubicacion[5]] + (width_box_2do / 2)); RelY.Add(heightRel3Niv_Y[5]);
                }
                int menor_y = 0;
                int menor_x = 0;
                //Definir menor posicion y
                for (int i = 0; i < Lista_Nivel.Count; i++)
                {
                    if (pos_y[i] < menor_y)
                    {
                        menor_y = pos_y[i];
                    }
                    if (pos_x[i] < menor_x)
                    {
                        menor_x = pos_x[i];
                    }
                }
                //desplazar todas las coordenadas
                menor_y = menor_y * (-1);
                menor_x = menor_x * (-1);

                for (int i = 0; i < Lista_Nivel.Count; i++)
                {
                    pos_x[i] = pos_x[i] + menor_x + 20;
                    pos_y[i] = pos_y[i] + menor_y + 20;
                }

                for (int i = 0; i < RelX.Count; i++)
                {
                    RelX[i] = RelX[i] + menor_x + 20;
                    RelX1[i] = RelX1[i] + menor_x + 20;
                    RelY[i] = RelY[i] + menor_y + 20;
                    RelY1[i] = RelY1[i] + menor_y + 20;
                }

                //definir tamaño de la imagen

                int mayor_y = 0;
                int mayor_x = 0;
                //Definir menor posicion y
                for (int i = 0; i < Lista_Nivel.Count; i++)
                {
                    if (pos_x[i] > mayor_x)
                    {
                        mayor_x = pos_x[i];
                    }
                    if (pos_y[i] > mayor_y)
                    {
                        mayor_y = pos_y[i];
                    }
                }

                ImageWidth = mayor_x + width_box_3er;
                ImageHeight = mayor_y + height_box_3er;

                color_fill_problema = Color.FromArgb(255, 106, 56);
                color_fill_Opcion = Color.FromArgb(92, 134, 142);
                color_Pen_Relaciones = Color.FromArgb(25, 72, 63);
                color_fuente_Problema = Color.FromArgb(255, 255, 255);
                color_fuente_Opcion = Color.FromArgb(0, 0, 0);
                color_fondo = Color.FromArgb(255, 255, 255);

                //Brushes
                SolidBrush BrushCajaProblema = new SolidBrush(color_fill_problema);
                SolidBrush LetraOpcion = new SolidBrush(color_fuente_Opcion);
                SolidBrush BrushOpcion = new SolidBrush(color_fondo);
                SolidBrush LetraProblema = new SolidBrush(color_fuente_Problema);
                SolidBrush BrushTriangulos = new SolidBrush(color_Pen_Relaciones);

                //Pens
                Pen pen_caja_Opcion = new Pen(color_fill_Opcion);
                Pen pen_caja_Problema = new Pen(color_fill_problema);
                Pen pen_flechas = new Pen(color_Pen_Relaciones, 4);
                Pen pen_flechas_2do = new Pen(color_Pen_Relaciones, 6);

                //definir bitmap y fuente de letra
                Bitmap bitmap = new Bitmap(1, 1);

                Font FuenteProblema = new System.Drawing.Font("Century Gothic", 22, FontStyle.Bold, GraphicsUnit.Pixel);
                Font FuenteOpcion = new System.Drawing.Font("Century Gothic", 21, FontStyle.Bold, GraphicsUnit.Pixel);
                Graphics graphics = System.Drawing.Graphics.FromImage(bitmap);

                //tamaño del canvas
                bitmap = new Bitmap(bitmap, new Size(ImageWidth + 10, ImageHeight + 50));
                graphics = Graphics.FromImage(bitmap);
                //fondo del canvas
                graphics.Clear(color_fondo);

                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;

                //graficar cada caja
                for (int i = 0; i < lista_texto.Count; i++)
                {
                    if (i == 0)
                    {
                        StringFormat stringFormat = new StringFormat();
                        stringFormat.Alignment = StringAlignment.Center;
                        stringFormat.LineAlignment = StringAlignment.Center;
                        Rectangle r = new Rectangle(pos_x[i], pos_y[i], box_w[i], box_h[i]);
                        graphics.FillRectangle(BrushCajaProblema, r);
                        graphics.DrawRectangle(pen_caja_Problema, r);
                        graphics.DrawString(lista_texto[i], FuenteProblema, LetraProblema, r, stringFormat);
                        //Definir triangulo
                        Point[] points = { new Point(pos_x[i], pos_y[i] + box_h[i]), new Point(pos_x[i] + 10, pos_y[i] + box_h[i]), new Point(pos_x[i], pos_y[i] + box_h[i] - 10) };
                        graphics.FillPolygon(BrushTriangulos, points);
                    }
                    else
                    {
                        StringFormat stringFormat = new StringFormat();
                        stringFormat.Alignment = StringAlignment.Center;
                        stringFormat.LineAlignment = StringAlignment.Center;
                        Rectangle r = new Rectangle(pos_x[i], pos_y[i], box_w[i], box_h[i]);
                        graphics.FillRectangle(BrushOpcion, r);
                        graphics.DrawRectangle(pen_caja_Opcion, r);
                        graphics.DrawString(lista_texto[i], FuenteOpcion, LetraOpcion, r, stringFormat);
                        //Definir triangulo
                        Point[] points = { new Point(pos_x[i], pos_y[i] + box_h[i]), new Point(pos_x[i] + 10, pos_y[i] + box_h[i]), new Point(pos_x[i], pos_y[i] + box_h[i] - 10) };
                        graphics.FillPolygon(BrushTriangulos, points);
                    }
                }
                //graficar relaciones
                for (int i = 0; i < 6; i++)
                {
                    pen_flechas_2do.EndCap = LineCap.ArrowAnchor;
                    pen_flechas_2do.StartCap = LineCap.ArrowAnchor;
                    graphics.DrawLine(pen_flechas_2do, RelX[i], RelY[i], RelX1[i], RelY1[i]);
                }
                for (int i = 6; i < contrel; i++)
                {
                    pen_flechas.EndCap = LineCap.NoAnchor;
                    pen_flechas.StartCap = LineCap.ArrowAnchor;
                    graphics.DrawLine(pen_flechas, RelX[i], RelY[i], RelX1[i], RelY1[i]);
                }
                for (int i = contrel; i < RelX.Count; i++)
                {
                    pen_flechas.EndCap = LineCap.NoAnchor;
                    pen_flechas.StartCap = LineCap.NoAnchor;
                    graphics.DrawLine(pen_flechas, RelX[i], RelY[i], RelX1[i], RelY1[i]);
                }
                graphics.Flush();
                graphics.Dispose();
                //copiar la información en base 64 del bitmap y ponerla en Imageurl del control
                TextoFinal = "data:image/png;base64," + ImageToBase64String(bitmap, ImageFormat.Png);
            }
            return TextoFinal;
        }
        private string Diagrama5Porque(List<string> lista_texto, List<string> Lista_Nivel)
        {
            string TextoFinal = "";
            if (lista_texto.Count == 36)
            {
                Color color_fill_problema = new Color();
                Color color_fill_Opcion = new Color();
                Color color_Pen_Relaciones = new Color();
                Color color_fuente_Problema = new Color();
                Color color_fuente_Opcion = new Color();
                Color color_fondo = new Color();

                Color color_borde_problema = new Color();
                Color color_borde_opcion = new Color();

                //Dimension Imagen
                int ImageWidth = 2000;
                int ImageHeight = 2000;

                int[] width_box_2do = new int[7] { 400, 400, 400, 400, 400, 400, 400 };
                int[] height_box_2do = new int[7] { 250, 250, 250, 250, 250, 250, 250 };

                List<string> Lista1 = new List<string>();
                List<string> Lista2 = new List<string>();
                List<string> Lista3 = new List<string>();
                List<string> Lista4 = new List<string>();
                List<string> Lista5 = new List<string>();
                List<string> Lista6 = new List<string>();
                List<string> Lista7 = new List<string>();

                int[] posiciones1 = new int[5] { 1, 8, 15, 22, 29 };
                int[] posiciones2 = new int[5] { 2, 9, 16, 23, 30 };
                int[] posiciones3 = new int[5] { 3, 10, 17, 24, 31 };
                int[] posiciones4 = new int[5] { 4, 11, 18, 25, 32 };
                int[] posiciones5 = new int[5] { 5, 12, 19, 26, 33 };
                int[] posiciones6 = new int[5] { 6, 13, 20, 27, 34 };
                int[] posiciones7 = new int[5] { 7, 14, 21, 28, 35 };


                string[] TextoPregunta = new string[7] { "QUÉ", "POR QUÉ", "QUIÉN", "DONDE", "CUÁNDO", "CÓMO", "CUÁNTO" };

                List<int> RelX = new List<int>();
                List<int> RelY = new List<int>();
                List<int> RelX1 = new List<int>();
                List<int> RelY1 = new List<int>();
                List<bool> Arrow = new List<bool>();

                for (int i = 0; i < 5; i++)
                {
                    Lista1.Add(lista_texto[posiciones1[i]]);
                    Lista2.Add(lista_texto[posiciones2[i]]);
                    Lista3.Add(lista_texto[posiciones3[i]]);
                    Lista4.Add(lista_texto[posiciones4[i]]);
                    Lista5.Add(lista_texto[posiciones5[i]]);
                    Lista6.Add(lista_texto[posiciones6[i]]);
                    Lista7.Add(lista_texto[posiciones7[i]]);
                }
                int Espacio_Entre_preguntas = 30;
                int Espacio_Entre_preguntas_hor = 70;

                int Espacio_vertical = 20;
                int Espacio_horizontal = 20;

                int PosProbX = Espacio_horizontal;
                int PosProbY = Espacio_vertical;

                int[] PosX1 = new int[Lista1.Count];
                int[] PosY1 = new int[Lista1.Count];
                int[] PosX2 = new int[Lista2.Count];
                int[] PosY2 = new int[Lista2.Count];
                int[] PosX3 = new int[Lista3.Count];
                int[] PosY3 = new int[Lista3.Count];
                int[] PosX4 = new int[Lista4.Count];
                int[] PosY4 = new int[Lista4.Count];
                int[] PosX5 = new int[Lista5.Count];
                int[] PosY5 = new int[Lista5.Count];
                int[] PosX6 = new int[Lista6.Count];
                int[] PosY6 = new int[Lista6.Count];
                int[] PosX7 = new int[Lista7.Count];
                int[] PosY7 = new int[Lista7.Count];

                bool[] Mostrar1 = new bool[Lista1.Count];
                bool[] Mostrar2 = new bool[Lista2.Count];
                bool[] Mostrar3 = new bool[Lista3.Count];
                bool[] Mostrar4 = new bool[Lista4.Count];
                bool[] Mostrar5 = new bool[Lista5.Count];
                bool[] Mostrar6 = new bool[Lista6.Count];
                bool[] Mostrar7 = new bool[Lista7.Count];
                for (int i = 0; i < Lista1.Count; i++)
                {
                    Mostrar1[i] = true;
                    Mostrar2[i] = true;
                    Mostrar3[i] = true;
                    Mostrar4[i] = true;
                    Mostrar5[i] = true;
                    Mostrar6[i] = true;
                    Mostrar7[i] = true;
                }

                int stringcount = 0;
                //determinar height preguntas
                for (int i = 0; i < Lista1.Count; i++)
                {
                    if (Lista1[i] != null)
                    {
                        if (Lista1[i].Length > stringcount)
                        {
                            stringcount = Lista1[i].Length;
                            if (stringcount > 0 && stringcount < 50)
                            {
                                height_box_2do[0] = 130;
                            }
                            if (stringcount >= 50 && stringcount < 100)
                            {
                                height_box_2do[0] = 180;
                            }
                            if (stringcount >= 100 && stringcount <= 200)
                            {
                                height_box_2do[0] = 250;
                            }
                        }
                    }
                }
                stringcount = 0;
                for (int i = 0; i < Lista2.Count; i++)
                {
                    if (Lista2[i] != null)
                    {
                        if (Lista2[i].Length > stringcount)
                        {
                            stringcount = Lista2[i].Length;
                            if (stringcount > 0 && stringcount < 50)
                            {
                                height_box_2do[1] = 130;
                            }
                            if (stringcount >= 50 && stringcount < 100)
                            {
                                height_box_2do[1] = 180;
                            }
                            if (stringcount >= 100 && stringcount <= 200)
                            {
                                height_box_2do[1] = 250;
                            }
                        }
                    }
                }
                stringcount = 0;
                for (int i = 0; i < Lista3.Count; i++)
                {
                    if (Lista3[i] != null)
                    {
                        if (Lista3[i].Length > stringcount)
                        {
                            stringcount = Lista3[i].Length;
                            if (stringcount > 0 && stringcount < 50)
                            {
                                height_box_2do[2] = 130;
                            }
                            if (stringcount >= 50 && stringcount < 100)
                            {
                                height_box_2do[2] = 180;
                            }
                            if (stringcount >= 100 && stringcount <= 200)
                            {
                                height_box_2do[2] = 250;
                            }
                        }
                    }
                }
                stringcount = 0;
                for (int i = 0; i < Lista4.Count; i++)
                {
                    if (Lista4[i] != null)
                    {
                        if (Lista4[i].Length > stringcount)
                        {
                            stringcount = Lista4[i].Length;
                            if (stringcount > 0 && stringcount < 50)
                            {
                                height_box_2do[3] = 130;
                            }
                            if (stringcount >= 50 && stringcount < 100)
                            {
                                height_box_2do[3] = 180;
                            }
                            if (stringcount >= 100 && stringcount <= 200)
                            {
                                height_box_2do[3] = 250;
                            }
                        }
                    }
                }
                stringcount = 0;
                for (int i = 0; i < Lista5.Count; i++)
                {
                    if (Lista5[i] != null)
                    {
                        if (Lista5[i].Length > stringcount)
                        {
                            stringcount = Lista5[i].Length;
                            if (stringcount > 0 && stringcount < 50)
                            {
                                height_box_2do[4] = 130;
                            }
                            if (stringcount >= 50 && stringcount < 100)
                            {
                                height_box_2do[4] = 180;
                            }
                            if (stringcount >= 100 && stringcount <= 200)
                            {
                                height_box_2do[4] = 250;
                            }
                        }
                    }
                }
                stringcount = 0;
                for (int i = 0; i < Lista6.Count; i++)
                {
                    if (Lista6[i] != null)
                    {
                        if (Lista6[i].Length > stringcount)
                        {
                            stringcount = Lista6[i].Length;
                            if (stringcount > 0 && stringcount < 50)
                            {
                                height_box_2do[5] = 130;
                            }
                            if (stringcount >= 50 && stringcount < 100)
                            {
                                height_box_2do[5] = 180;
                            }
                            if (stringcount >= 100 && stringcount <= 200)
                            {
                                height_box_2do[5] = 250;
                            }
                        }
                    }
                }
                stringcount = 0;
                for (int i = 0; i < Lista7.Count; i++)
                {
                    if (Lista7[i] != null)
                    {
                        if (Lista7[i].Length > stringcount)
                        {
                            stringcount = Lista7[i].Length;
                            if (stringcount > 0 && stringcount < 50)
                            {
                                height_box_2do[6] = 130;
                            }
                            if (stringcount >= 50 && stringcount < 100)
                            {
                                height_box_2do[6] = 180;
                            }
                            if (stringcount >= 100 && stringcount <= 200)
                            {
                                height_box_2do[6] = 250;
                            }
                        }
                    }
                }

                //Posicionar cajas Qué
                for (int i = 0; i < Lista1.Count; i++)
                {

                    PosX1[i] = Espacio_horizontal;
                    PosY1[i] = Espacio_vertical;

                    Espacio_horizontal = Espacio_horizontal + width_box_2do[0] + Espacio_Entre_preguntas_hor;

                    bool VerificarNull = false;
                    if (Lista1[i] == null)
                    {
                        VerificarNull = true;
                    }
                    else
                    {
                        if (Lista1[i] == "")
                        {
                            VerificarNull = true;
                        }
                    }

                    if (VerificarNull)
                    {
                        Mostrar1[i] = false;

                    }
                    else
                    {


                        if (i != Lista1.Count - 1)
                        {
                            RelX1.Add(PosX1[i] + width_box_2do[0]);
                            RelY1.Add(PosY1[i] + (height_box_2do[0] / 2));
                            RelX.Add(PosX1[i] + width_box_2do[0] + Espacio_Entre_preguntas_hor);
                            RelY.Add(PosY1[i] + (height_box_2do[0] / 2));
                            Arrow.Add(true);
                        }
                        else
                        {
                            RelX1.Add(PosX1[i] + width_box_2do[0]);
                            RelY1.Add(PosY1[i] + (height_box_2do[0] / 2));
                            RelX.Add(PosX1[i] + width_box_2do[0] + (Espacio_Entre_preguntas_hor / 2));
                            RelY.Add(PosY1[i] + (height_box_2do[0] / 2));
                            Arrow.Add(false);
                        }
                    }

                }
                Espacio_vertical = Espacio_vertical + Espacio_Entre_preguntas + height_box_2do[0];
                Espacio_horizontal = 20;
                //Posicionar cajas Porque

                for (int i = 0; i < Lista2.Count; i++)
                {

                    PosX2[i] = Espacio_horizontal;
                    PosY2[i] = Espacio_vertical;

                    Espacio_horizontal = Espacio_horizontal + width_box_2do[1] + Espacio_Entre_preguntas_hor;
                    bool VerificarNull = false;
                    if (Lista2[i] == null)
                    {
                        VerificarNull = true;
                    }
                    else
                    {
                        if (Lista2[i] == "")
                        {
                            VerificarNull = true;
                        }
                    }

                    if (VerificarNull)
                    {
                        Mostrar2[i] = false;
                    }
                    else
                    {

                        RelX.Add(PosX2[i] + width_box_2do[1]);
                        RelY.Add(PosY2[i] + (height_box_2do[1] / 2));
                        RelX1.Add(PosX2[i] + width_box_2do[1] + (Espacio_Entre_preguntas_hor / 2));
                        RelY1.Add(PosY2[i] + (height_box_2do[1] / 2));
                        Arrow.Add(true);
                        RelX.Add(PosX2[i] + width_box_2do[1] + (Espacio_Entre_preguntas_hor / 2));
                        RelY.Add(PosY2[i] + (height_box_2do[1] / 2));
                        RelX1.Add(PosX2[i] + width_box_2do[1] + (Espacio_Entre_preguntas_hor / 2));
                        RelY1.Add(PosY1[i] + (height_box_2do[0] / 2));
                        Arrow.Add(false);
                    }

                }
                Espacio_vertical = Espacio_vertical + Espacio_Entre_preguntas + height_box_2do[1];
                Espacio_horizontal = 20;
                //Posicionar cajas quien

                for (int i = 0; i < Lista3.Count; i++)
                {

                    PosX3[i] = Espacio_horizontal;
                    PosY3[i] = Espacio_vertical;

                    Espacio_horizontal = Espacio_horizontal + width_box_2do[2] + Espacio_Entre_preguntas_hor;
                    bool VerificarNull = false;
                    if (Lista3[i] == null)
                    {
                        VerificarNull = true;
                    }
                    else
                    {
                        if (Lista3[i] == "")
                        {
                            VerificarNull = true;
                        }
                    }

                    if (VerificarNull)
                    {
                        Mostrar3[i] = false;
                    }
                    else
                    {

                        RelX.Add(PosX3[i] + width_box_2do[2]);
                        RelY.Add(PosY3[i] + (height_box_2do[2] / 2));
                        RelX1.Add(PosX3[i] + width_box_2do[2] + (Espacio_Entre_preguntas_hor / 2));
                        RelY1.Add(PosY3[i] + (height_box_2do[2] / 2));
                        Arrow.Add(true);

                        RelX.Add(PosX3[i] + width_box_2do[2] + (Espacio_Entre_preguntas_hor / 2));
                        RelY.Add(PosY3[i] + (height_box_2do[2] / 2));
                        RelX1.Add(PosX3[i] + width_box_2do[2] + (Espacio_Entre_preguntas_hor / 2));
                        RelY1.Add(PosY1[i] + (height_box_2do[0] / 2));
                        Arrow.Add(false);
                    }

                }
                Espacio_vertical = Espacio_vertical + Espacio_Entre_preguntas + height_box_2do[2];
                Espacio_horizontal = 20;
                //Posicionar cajas donde

                for (int i = 0; i < Lista4.Count; i++)
                {

                    PosX4[i] = Espacio_horizontal;
                    PosY4[i] = Espacio_vertical;

                    Espacio_horizontal = Espacio_horizontal + width_box_2do[3] + Espacio_Entre_preguntas_hor;
                    bool VerificarNull = false;
                    if (Lista4[i] == null)
                    {
                        VerificarNull = true;
                    }
                    else
                    {
                        if (Lista4[i] == "")
                        {
                            VerificarNull = true;
                        }
                    }

                    if (VerificarNull)
                    {
                        Mostrar4[i] = false;
                    }
                    else
                    {

                        RelX.Add(PosX4[i] + width_box_2do[3]);
                        RelY.Add(PosY4[i] + (height_box_2do[3] / 2));
                        RelX1.Add(PosX4[i] + width_box_2do[3] + (Espacio_Entre_preguntas_hor / 2));
                        RelY1.Add(PosY4[i] + (height_box_2do[3] / 2));
                        Arrow.Add(true);
                        RelX.Add(PosX4[i] + width_box_2do[3] + (Espacio_Entre_preguntas_hor / 2));
                        RelY.Add(PosY4[i] + (height_box_2do[3] / 2));
                        RelX1.Add(PosX4[i] + width_box_2do[3] + (Espacio_Entre_preguntas_hor / 2));
                        RelY1.Add(PosY1[i] + (height_box_2do[0] / 2));
                        Arrow.Add(false);
                    }

                }
                Espacio_vertical = Espacio_vertical + Espacio_Entre_preguntas + height_box_2do[3];
                Espacio_horizontal = 20;
                //Posicionar cajas cuando

                for (int i = 0; i < Lista5.Count; i++)
                {

                    PosX5[i] = Espacio_horizontal;
                    PosY5[i] = Espacio_vertical;

                    Espacio_horizontal = Espacio_horizontal + width_box_2do[4] + Espacio_Entre_preguntas_hor;
                    bool VerificarNull = false;
                    if (Lista5[i] == null)
                    {
                        VerificarNull = true;
                    }
                    else
                    {
                        if (Lista5[i] == "")
                        {
                            VerificarNull = true;
                        }
                    }

                    if (VerificarNull)
                    {
                        Mostrar5[i] = false;
                    }
                    else
                    {

                        RelX.Add(PosX5[i] + width_box_2do[4]);
                        RelY.Add(PosY5[i] + (height_box_2do[4] / 2));
                        RelX1.Add(PosX5[i] + width_box_2do[4] + (Espacio_Entre_preguntas_hor / 2));
                        RelY1.Add(PosY5[i] + (height_box_2do[4] / 2));
                        Arrow.Add(true);
                        RelX.Add(PosX5[i] + width_box_2do[4] + (Espacio_Entre_preguntas_hor / 2));
                        RelY.Add(PosY5[i] + (height_box_2do[4] / 2));
                        RelX1.Add(PosX5[i] + width_box_2do[4] + (Espacio_Entre_preguntas_hor / 2));
                        RelY1.Add(PosY1[i] + (height_box_2do[0] / 2));
                        Arrow.Add(false);
                    }

                }
                Espacio_vertical = Espacio_vertical + Espacio_Entre_preguntas + height_box_2do[4];
                Espacio_horizontal = 20;
                //Posicionar cajas como

                for (int i = 0; i < Lista6.Count; i++)
                {

                    PosX6[i] = Espacio_horizontal;
                    PosY6[i] = Espacio_vertical;

                    Espacio_horizontal = Espacio_horizontal + width_box_2do[5] + Espacio_Entre_preguntas_hor;
                    bool VerificarNull = false;
                    if (Lista6[i] == null)
                    {
                        VerificarNull = true;
                    }
                    else
                    {
                        if (Lista6[i] == "")
                        {
                            VerificarNull = true;
                        }
                    }

                    if (VerificarNull)
                    {
                        Mostrar6[i] = false;
                    }
                    else
                    {

                        RelX.Add(PosX6[i] + width_box_2do[5]);
                        RelY.Add(PosY6[i] + (height_box_2do[5] / 2));
                        RelX1.Add(PosX6[i] + width_box_2do[5] + (Espacio_Entre_preguntas_hor / 2));
                        RelY1.Add(PosY6[i] + (height_box_2do[5] / 2));
                        Arrow.Add(true);
                        RelX.Add(PosX6[i] + width_box_2do[5] + (Espacio_Entre_preguntas_hor / 2));
                        RelY.Add(PosY6[i] + (height_box_2do[5] / 2));
                        RelX1.Add(PosX6[i] + width_box_2do[5] + (Espacio_Entre_preguntas_hor / 2));
                        RelY1.Add(PosY1[i] + (height_box_2do[0] / 2));
                        Arrow.Add(false);
                    }


                }
                Espacio_vertical = Espacio_vertical + Espacio_Entre_preguntas + height_box_2do[5];
                Espacio_horizontal = 20;
                //Posicionar cajas cuanto

                for (int i = 0; i < Lista7.Count; i++)
                {

                    PosX7[i] = Espacio_horizontal;
                    PosY7[i] = Espacio_vertical;

                    Espacio_horizontal = Espacio_horizontal + width_box_2do[6] + Espacio_Entre_preguntas_hor;

                    bool VerificarNull = false;
                    if (Lista7[i] == null)
                    {
                        VerificarNull = true;
                    }
                    else
                    {
                        if (Lista7[i] == "")
                        {
                            VerificarNull = true;
                        }
                    }

                    if (VerificarNull)
                    {
                        Mostrar7[i] = false;
                    }
                    else
                    {

                        RelX.Add(PosX7[i] + width_box_2do[6]);
                        RelY.Add(PosY7[i] + (height_box_2do[6] / 2));
                        RelX1.Add(PosX7[i] + width_box_2do[6] + (Espacio_Entre_preguntas_hor / 2));
                        RelY1.Add(PosY7[i] + (height_box_2do[6] / 2));
                        Arrow.Add(true);
                        RelX.Add(PosX7[i] + width_box_2do[6] + (Espacio_Entre_preguntas_hor / 2));
                        RelY.Add(PosY7[i] + (height_box_2do[6] / 2));
                        RelX1.Add(PosX7[i] + width_box_2do[6] + (Espacio_Entre_preguntas_hor / 2));
                        RelY1.Add(PosY1[i] + (height_box_2do[0] / 2));
                        Arrow.Add(false);
                    }

                }
                Espacio_vertical = Espacio_vertical + Espacio_Entre_preguntas + height_box_2do[6];
                Espacio_horizontal = 20;

                int SumaWidth = 0;
                for (int i = 0; i < 7; i++)
                {
                    SumaWidth = SumaWidth + width_box_2do[i];
                }

                ImageWidth = 20 + SumaWidth + (5 * Espacio_Entre_preguntas_hor);
                ImageHeight = Espacio_vertical;

                for (int i = 0; i < RelX.Count; i++)
                {
                    RelX[i] = RelX[i] + width_box_2do[0] + Espacio_Entre_preguntas_hor;
                    RelX1[i] = RelX1[i] + width_box_2do[0] + Espacio_Entre_preguntas_hor;
                }
                for (int i = 0; i < Lista1.Count; i++)
                {
                    PosX1[i] = PosX1[i] + width_box_2do[0] + Espacio_Entre_preguntas_hor;
                }
                for (int i = 0; i < Lista2.Count; i++)
                {
                    PosX2[i] = PosX2[i] + width_box_2do[1] + Espacio_Entre_preguntas_hor;
                }
                for (int i = 0; i < Lista3.Count; i++)
                {
                    PosX3[i] = PosX3[i] + width_box_2do[2] + Espacio_Entre_preguntas_hor;
                }
                for (int i = 0; i < Lista4.Count; i++)
                {
                    PosX4[i] = PosX4[i] + width_box_2do[3] + Espacio_Entre_preguntas_hor;
                }
                for (int i = 0; i < Lista5.Count; i++)
                {
                    PosX5[i] = PosX5[i] + width_box_2do[4] + Espacio_Entre_preguntas_hor;
                }
                for (int i = 0; i < Lista6.Count; i++)
                {
                    PosX6[i] = PosX6[i] + width_box_2do[5] + Espacio_Entre_preguntas_hor;
                }
                for (int i = 0; i < Lista7.Count; i++)
                {
                    PosX7[i] = PosX7[i] + width_box_2do[6] + Espacio_Entre_preguntas_hor;
                }

                color_fill_problema = Color.FromArgb(255, 106, 56);
                color_fill_Opcion = Color.FromArgb(255, 255, 255);
                color_Pen_Relaciones = Color.FromArgb(25, 72, 63);
                color_fuente_Problema = Color.FromArgb(255, 255, 255);
                color_fuente_Opcion = Color.FromArgb(0, 0, 0);
                color_fondo = Color.FromArgb(255, 255, 255);
                color_borde_problema = Color.FromArgb(255, 106, 56);
                color_borde_opcion = Color.FromArgb(0, 0, 0);
                //Brushes
                SolidBrush BrushCajaProblema = new SolidBrush(color_fill_problema);
                SolidBrush BrushPregunta = new SolidBrush(Color.FromArgb(255, 255, 255));
                SolidBrush LetraOpcion = new SolidBrush(color_fuente_Opcion);
                SolidBrush BrushOpcion = new SolidBrush(color_fill_Opcion);
                SolidBrush LetraProblema = new SolidBrush(color_fuente_Problema);
                SolidBrush LetraPregunta = new SolidBrush(Color.FromArgb(0, 0, 0));
                SolidBrush BrushTriangulos = new SolidBrush(color_Pen_Relaciones);
                //Pens
                Pen pen_caja_Opcion = new Pen(color_borde_opcion);
                Pen pen_caja_Problema = new Pen(color_borde_problema);
                Pen pen_flechas = new Pen(color_Pen_Relaciones, 6);
                Pen pen_flechas_2do = new Pen(color_Pen_Relaciones, 6);
                //definir bitmap y fuente de letra
                Bitmap bitmap = new Bitmap(1, 1);
                Font FuenteProblema = new System.Drawing.Font("Century Gothic", 25, FontStyle.Bold, GraphicsUnit.Pixel);
                Font FuenteOpcion = new System.Drawing.Font("Century Gothic", 30, FontStyle.Bold, GraphicsUnit.Pixel);
                Font FuentePregunta = new System.Drawing.Font("Century Gothic", 30, FontStyle.Bold, GraphicsUnit.Pixel);
                Graphics graphics = System.Drawing.Graphics.FromImage(bitmap);
                //tamaño del canvas
                bitmap = new Bitmap(bitmap, new Size(ImageWidth + 10, ImageHeight + 50));
                graphics = Graphics.FromImage(bitmap);
                //fondo del canvas
                graphics.Clear(color_fondo);
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;
                StringFormat stringFormat1 = new StringFormat();
                stringFormat1.Alignment = StringAlignment.Center;
                stringFormat1.LineAlignment = StringAlignment.Center;

                int[] EspP = new int[7];
                for (int i = 0; i < 7; i++)
                {
                    EspP[i] = width_box_2do[i] + Espacio_Entre_preguntas_hor;
                }

                int[] PosPregX = new int[7] { PosX1[0] - EspP[0], PosX2[0] - EspP[0], PosX3[0] - EspP[0], PosX4[0] - EspP[0], PosX5[0] - EspP[0], PosX6[0] - EspP[0], PosX7[0] - EspP[0] };
                int[] PosPregy = new int[7] { PosY1[0], PosY2[0], PosY3[0], PosY4[0], PosY5[0], PosY6[0], PosY7[0] };
                for (int i = 0; i < TextoPregunta.Length; i++)
                {
                    StringFormat stringFormat = new StringFormat();
                    stringFormat.Alignment = StringAlignment.Center;
                    stringFormat.LineAlignment = StringAlignment.Center;
                    Rectangle r = new Rectangle(PosPregX[i], PosPregy[i], width_box_2do[i], height_box_2do[i]);
                    graphics.FillRectangle(BrushPregunta, r);
                    graphics.DrawRectangle(pen_caja_Opcion, r);
                    graphics.DrawString(TextoPregunta[i], FuentePregunta, LetraPregunta, r, stringFormat);

                }

                for (int i = 0; i < Lista1.Count; i++)
                {
                    if (i != 0)
                    {
                        if (Mostrar1[i] == true)
                        {
                            StringFormat stringFormat = new StringFormat();
                            stringFormat.Alignment = StringAlignment.Center;
                            stringFormat.LineAlignment = StringAlignment.Center;
                            Rectangle r = new Rectangle(PosX1[i], PosY1[i], width_box_2do[0], height_box_2do[0]);
                            graphics.FillRectangle(BrushOpcion, r);
                            graphics.DrawRectangle(pen_caja_Opcion, r);
                            graphics.DrawString(Lista1[i], FuenteOpcion, LetraOpcion, r, stringFormat);
                            //Definir triangulo
                            Point[] points = { new Point(PosX1[i], PosY1[i] + height_box_2do[0]), new Point(PosX1[i] + 10, PosY1[i] + height_box_2do[0]), new Point(PosX1[i], PosY1[i] + height_box_2do[0] - 10) };
                            graphics.FillPolygon(BrushTriangulos, points);
                        }
                    }
                    else
                    {
                        if (Mostrar1[i] == true)
                        {
                            StringFormat stringFormat = new StringFormat();
                            stringFormat.Alignment = StringAlignment.Center;
                            stringFormat.LineAlignment = StringAlignment.Center;
                            Rectangle r = new Rectangle(PosX1[i], PosY1[i], width_box_2do[0], height_box_2do[0]);
                            graphics.FillRectangle(BrushCajaProblema, r);
                            graphics.DrawRectangle(pen_caja_Problema, r);
                            graphics.DrawString(Lista1[i], FuenteOpcion, LetraProblema, r, stringFormat);
                            //Definir triangulo
                            Point[] points = { new Point(PosX1[i], PosY1[i] + height_box_2do[0]), new Point(PosX1[i] + 10, PosY1[i] + height_box_2do[0]), new Point(PosX1[i], PosY1[i] + height_box_2do[0] - 10) };
                            graphics.FillPolygon(BrushTriangulos, points);
                        }
                    }

                }
                for (int i = 0; i < Lista2.Count; i++)
                {
                    if (Mostrar2[i] == true)
                    {
                        StringFormat stringFormat = new StringFormat();
                        stringFormat.Alignment = StringAlignment.Center;
                        stringFormat.LineAlignment = StringAlignment.Center;
                        Rectangle r = new Rectangle(PosX2[i], PosY2[i], width_box_2do[1], height_box_2do[1]);
                        graphics.FillRectangle(BrushOpcion, r);
                        graphics.DrawRectangle(pen_caja_Opcion, r);
                        graphics.DrawString(Lista2[i], FuenteOpcion, LetraOpcion, r, stringFormat);
                        //Definir triangulo
                        Point[] points = { new Point(PosX2[i], PosY2[i] + height_box_2do[1]), new Point(PosX2[i] + 10, PosY2[i] + height_box_2do[1]), new Point(PosX2[i], PosY2[i] + height_box_2do[1] - 10) };
                        graphics.FillPolygon(BrushTriangulos, points);
                    }
                }
                for (int i = 0; i < Lista3.Count; i++)
                {
                    if (Mostrar3[i] == true)
                    {
                        StringFormat stringFormat = new StringFormat();
                        stringFormat.Alignment = StringAlignment.Center;
                        stringFormat.LineAlignment = StringAlignment.Center;
                        Rectangle r = new Rectangle(PosX3[i], PosY3[i], width_box_2do[2], height_box_2do[2]);
                        graphics.FillRectangle(BrushOpcion, r);
                        graphics.DrawRectangle(pen_caja_Opcion, r);
                        graphics.DrawString(Lista3[i], FuenteOpcion, LetraOpcion, r, stringFormat);
                        //Definir triangulo
                        Point[] points = { new Point(PosX3[i], PosY3[i] + height_box_2do[2]), new Point(PosX3[i] + 10, PosY3[i] + height_box_2do[2]), new Point(PosX3[i], PosY3[i] + height_box_2do[2] - 10) };
                        graphics.FillPolygon(BrushTriangulos, points);
                    }
                }
                for (int i = 0; i < Lista4.Count; i++)
                {
                    if (Mostrar4[i] == true)
                    {
                        StringFormat stringFormat = new StringFormat();
                        stringFormat.Alignment = StringAlignment.Center;
                        stringFormat.LineAlignment = StringAlignment.Center;
                        Rectangle r = new Rectangle(PosX4[i], PosY4[i], width_box_2do[3], height_box_2do[3]);
                        graphics.FillRectangle(BrushOpcion, r);
                        graphics.DrawRectangle(pen_caja_Opcion, r);
                        graphics.DrawString(Lista4[i], FuenteOpcion, LetraOpcion, r, stringFormat);
                        //Definir triangulo
                        Point[] points = { new Point(PosX4[i], PosY4[i] + height_box_2do[3]), new Point(PosX4[i] + 10, PosY4[i] + height_box_2do[3]), new Point(PosX4[i], PosY4[i] + height_box_2do[3] - 10) };
                        graphics.FillPolygon(BrushTriangulos, points);
                    }
                }
                for (int i = 0; i < Lista5.Count; i++)
                {
                    if (Mostrar5[i] == true)
                    {
                        StringFormat stringFormat = new StringFormat();
                        stringFormat.Alignment = StringAlignment.Center;
                        stringFormat.LineAlignment = StringAlignment.Center;
                        Rectangle r = new Rectangle(PosX5[i], PosY5[i], width_box_2do[4], height_box_2do[4]);
                        graphics.FillRectangle(BrushOpcion, r);
                        graphics.DrawRectangle(pen_caja_Opcion, r);
                        graphics.DrawString(Lista5[i], FuenteOpcion, LetraOpcion, r, stringFormat);
                        //Definir triangulo
                        Point[] points = { new Point(PosX5[i], PosY5[i] + height_box_2do[4]), new Point(PosX5[i] + 10, PosY5[i] + height_box_2do[4]), new Point(PosX5[i], PosY5[i] + height_box_2do[4] - 10) };
                        graphics.FillPolygon(BrushTriangulos, points);
                    }
                }
                for (int i = 0; i < Lista6.Count; i++)
                {
                    if (Mostrar6[i] == true)
                    {
                        StringFormat stringFormat = new StringFormat();
                        stringFormat.Alignment = StringAlignment.Center;
                        stringFormat.LineAlignment = StringAlignment.Center;
                        Rectangle r = new Rectangle(PosX6[i], PosY6[i], width_box_2do[5], height_box_2do[5]);
                        graphics.FillRectangle(BrushOpcion, r);
                        graphics.DrawRectangle(pen_caja_Opcion, r);
                        graphics.DrawString(Lista6[i], FuenteOpcion, LetraOpcion, r, stringFormat);
                        //Definir triangulo
                        Point[] points = { new Point(PosX6[i], PosY6[i] + height_box_2do[5]), new Point(PosX6[i] + 10, PosY6[i] + height_box_2do[5]), new Point(PosX6[i], PosY6[i] + height_box_2do[5] - 10) };
                        graphics.FillPolygon(BrushTriangulos, points);
                    }
                }
                for (int i = 0; i < Lista7.Count; i++)
                {
                    if (Mostrar7[i] == true)
                    {
                        StringFormat stringFormat = new StringFormat();
                        stringFormat.Alignment = StringAlignment.Center;
                        stringFormat.LineAlignment = StringAlignment.Center;
                        Rectangle r = new Rectangle(PosX7[i], PosY7[i], width_box_2do[6], height_box_2do[6]);
                        graphics.FillRectangle(BrushOpcion, r);
                        graphics.DrawRectangle(pen_caja_Opcion, r);
                        graphics.DrawString(Lista7[i], FuenteOpcion, LetraOpcion, r, stringFormat);
                        //Definir triangulo
                        Point[] points = { new Point(PosX7[i], PosY7[i] + height_box_2do[6]), new Point(PosX7[i] + 10, PosY7[i] + height_box_2do[6]), new Point(PosX7[i], PosY7[i] + height_box_2do[6] - 10) };
                        graphics.FillPolygon(BrushTriangulos, points);
                    }
                }
                //graficar relaciones
                for (int i = 0; i < RelX.Count; i++)
                {
                    if (Arrow[i])
                    {
                        pen_flechas_2do.EndCap = LineCap.NoAnchor;
                        pen_flechas_2do.StartCap = LineCap.ArrowAnchor;
                        graphics.DrawLine(pen_flechas_2do, RelX[i], RelY[i], RelX1[i], RelY1[i]);
                    }
                    else
                    {
                        pen_flechas_2do.EndCap = LineCap.NoAnchor;
                        pen_flechas_2do.StartCap = LineCap.NoAnchor;
                        graphics.DrawLine(pen_flechas_2do, RelX[i], RelY[i], RelX1[i], RelY1[i]);
                    }

                }
                graphics.Flush();
                graphics.Dispose();
                //copiar la información en base 64 del bitmap y ponerla en Imageurl del control
                TextoFinal = "data:image/png;base64," + ImageToBase64String(bitmap, ImageFormat.Png);
            }
            return TextoFinal;
        }
        private Bitmap lluvia_ideas(List<string> list_enviar, int[] orden, Color color_fondo, Color color_flechas, Color color_caja, Color color_fuente)
        {

            List<string> list_mensajes = new List<string>();
            list_mensajes = list_enviar;
            int numero_mensajes = list_mensajes.Count;
            int numero_car = 0;

            for (int i = 0; i < numero_mensajes; i++)
            {
                if (list_mensajes[i].Length > numero_car)
                {
                    numero_car = list_mensajes[i].Length;
                }
            }

            // variable distancia horizontal caja
            int width_cajas = 0;
            // variable distancia verticial caja
            int height_cajas = 0;
            // Radio circunferencia que agrupa
            int radio = 200;


            Pen pen_flechas = new Pen(color_flechas, 4);
            Pen pen_caja = new Pen(color_caja, 3);
            System.Drawing.SolidBrush brush1 = new System.Drawing.SolidBrush(System.Drawing.Color.Transparent);
            System.Drawing.SolidBrush brush2 = new System.Drawing.SolidBrush(color_fuente);
            System.Drawing.SolidBrush brush3 = new System.Drawing.SolidBrush(color_flechas);
            System.Drawing.SolidBrush brush4 = new System.Drawing.SolidBrush(color_fondo);
            Font font = new System.Drawing.Font("Century Gothic", 17, FontStyle.Regular, GraphicsUnit.Pixel);
            font = new Font(font, FontStyle.Bold);
            pen_flechas.EndCap = LineCap.DiamondAnchor;
            pen_flechas.StartCap = LineCap.RoundAnchor;
            int despl = 60;
            double despl1 = despl * 0.9;
            int despl3 = Convert.ToInt32(despl1);
            int radio_punto = 20;
            int radio_punto1 = 15;
            int div_caja_hor = width_cajas / 2;
            int div_caja_alt = height_cajas / 2;
            int ajuste_nivel1 = 0;
            int ajuste_nivel2 = 0;
            int ajuste_nivel3 = 0;
            int ajuste_nivel4 = 0;
            // crear bitmap
            Bitmap bitmap = new Bitmap(1, 1);
            Graphics graphics = System.Drawing.Graphics.FromImage(bitmap);
            //tamaño del canvas
            bitmap = new Bitmap(bitmap, new Size(710, 485));
            graphics = Graphics.FromImage(bitmap);
            //fondo del canvas y calidad de la imagen
            graphics.Clear(Color.FromArgb(255, 255, 255));
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
            int desplazar_hor = 0;
            int desplazar_ver = 0;

            #region Caracteristicas_segun_caracteres

            if (numero_car >= 0 && numero_car < 10)
            {
                width_cajas = 150;
                height_cajas = 60;
                radio = 180;
                despl = 80;
                div_caja_hor = width_cajas / 2;
                div_caja_alt = height_cajas / 2;
                radio_punto = 20;
                radio_punto1 = 15;
                despl = 50;
                despl1 = despl * 0.9;
                despl3 = Convert.ToInt32(despl1);
                ajuste_nivel1 = 0;
                ajuste_nivel2 = -5;
                ajuste_nivel3 = 0;
                ajuste_nivel4 = 0;

                desplazar_hor = 90;
                desplazar_ver = 40;
            }
            if (numero_car >= 10 && numero_car < 20)
            {
                width_cajas = 150;
                height_cajas = 60;
                radio = 180;
                despl = 80;
                div_caja_hor = width_cajas / 2;
                div_caja_alt = height_cajas / 2;
                radio_punto = 20;
                radio_punto1 = 15;
                despl = 50;
                despl1 = despl * 0.9;
                despl3 = Convert.ToInt32(despl1);
                ajuste_nivel1 = 0;
                ajuste_nivel2 = -5;
                ajuste_nivel3 = 0;
                ajuste_nivel4 = 0;

                desplazar_hor = 90;
                desplazar_ver = 40;
            }
            if (numero_car >= 20 && numero_car < 30)
            {
                width_cajas = 150;
                height_cajas = 60;
                radio = 180;
                despl = 80;
                div_caja_hor = width_cajas / 2;
                div_caja_alt = height_cajas / 2;
                radio_punto = 20;
                radio_punto1 = 15;
                despl = 50;
                despl1 = despl * 0.9;
                despl3 = Convert.ToInt32(despl1);
                ajuste_nivel1 = 0;
                ajuste_nivel2 = -5;
                ajuste_nivel3 = 0;
                ajuste_nivel4 = 0;

                desplazar_hor = 90;
                desplazar_ver = 40;
            }
            if (numero_car >= 30 && numero_car < 40)
            {
                width_cajas = 150;
                height_cajas = 60;
                radio = 180;
                despl = 80;
                div_caja_hor = width_cajas / 2;
                div_caja_alt = height_cajas / 2;
                radio_punto = 20;
                radio_punto1 = 15;
                despl = 50;
                despl1 = despl * 0.9;
                despl3 = Convert.ToInt32(despl1);
                ajuste_nivel1 = 0;
                ajuste_nivel2 = -5;
                ajuste_nivel3 = 0;
                ajuste_nivel4 = 0;

                desplazar_hor = 90;
                desplazar_ver = 40;
            }
            if (numero_car >= 40 && numero_car < 50)
            {
                width_cajas = 240;
                height_cajas = 85;
                radio = 235;

                div_caja_hor = width_cajas / 2;
                div_caja_alt = height_cajas / 2;
                radio_punto = 20;
                radio_punto1 = 15;
                ajuste_nivel1 = 7;
                ajuste_nivel2 = -10;
                ajuste_nivel3 = -20;
                ajuste_nivel4 = -30;

                despl = 50;
                despl1 = despl * 0.9;
                despl3 = Convert.ToInt32(despl1);
            }
            if (numero_car >= 50 && numero_car < 60)
            {
                width_cajas = 240;
                height_cajas = 85;
                radio = 235;

                div_caja_hor = width_cajas / 2;
                div_caja_alt = height_cajas / 2;
                radio_punto = 20;
                radio_punto1 = 15;
                ajuste_nivel1 = 7;
                ajuste_nivel2 = -10;
                ajuste_nivel3 = -20;
                ajuste_nivel4 = -30;

                despl = 50;
                despl1 = despl * 0.9;
                despl3 = Convert.ToInt32(despl1);
            }
            if (numero_car >= 60 && numero_car < 70)
            {
                width_cajas = 240;
                height_cajas = 85;
                radio = 235;

                div_caja_hor = width_cajas / 2;
                div_caja_alt = height_cajas / 2;
                radio_punto = 20;
                radio_punto1 = 15;
                ajuste_nivel1 = 7;
                ajuste_nivel2 = -10;
                ajuste_nivel3 = -20;
                ajuste_nivel4 = -30;

                despl = 50;
                despl1 = despl * 0.9;
                despl3 = Convert.ToInt32(despl1);
            }
            if (numero_car >= 70 && numero_car < 80)
            {
                width_cajas = 240;
                height_cajas = 85;
                radio = 235;

                div_caja_hor = width_cajas / 2;
                div_caja_alt = height_cajas / 2;
                radio_punto = 20;
                radio_punto1 = 15;
                ajuste_nivel1 = 7;
                ajuste_nivel2 = -10;
                ajuste_nivel3 = -20;
                ajuste_nivel4 = -30;

                despl = 50;
                despl1 = despl * 0.9;
                despl3 = Convert.ToInt32(despl1);
            }
            if (numero_car >= 80 && numero_car < 90)
            {
                width_cajas = 240;
                height_cajas = 85;
                radio = 235;

                div_caja_hor = width_cajas / 2;
                div_caja_alt = height_cajas / 2;
                radio_punto = 20;
                radio_punto1 = 15;
                ajuste_nivel1 = 7;
                ajuste_nivel2 = -10;
                ajuste_nivel3 = -20;
                ajuste_nivel4 = -30;

                despl = 50;
                despl1 = despl * 0.9;
                despl3 = Convert.ToInt32(despl1);
            }
            if (numero_car >= 90 && numero_car <= 100)
            {
                width_cajas = 240;
                height_cajas = 85;
                radio = 235;

                div_caja_hor = width_cajas / 2;
                div_caja_alt = height_cajas / 2;
                radio_punto = 20;
                radio_punto1 = 15;
                ajuste_nivel1 = 7;
                ajuste_nivel2 = -10;
                ajuste_nivel3 = -20;
                ajuste_nivel4 = -30;

                despl = 50;
                despl1 = despl * 0.9;
                despl3 = Convert.ToInt32(despl1);
            }
            #endregion

            // Radio interno
            radio_punto1 = radio_punto - 5;
            double comp = radio * 0.7071;
            int comp1 = Convert.ToInt32(comp);

            int[,] puntos = new int[8, 2];
            int[,] puntos1 = new int[8, 4];

            //Circunferencia union de relaciones        
            graphics.FillEllipse(brush3, new System.Drawing.Rectangle(radio - radio_punto + div_caja_hor + desplazar_hor, radio - radio_punto + desplazar_ver, radio_punto * 2, radio_punto * 2));

            //coordenadas cajas
            puntos[0, 0] = (radio - 4); puntos[0, 1] = 0 + ajuste_nivel1;
            puntos[1, 0] = (radio - 4) - comp1; puntos[1, 1] = (radio - 4) - comp1 + div_caja_alt + ajuste_nivel2;
            puntos[2, 0] = 0; puntos[2, 1] = (radio - 4) - div_caja_alt; ;
            puntos[3, 0] = (radio - 4) - comp1; puntos[3, 1] = (radio - 4) + comp1 - div_caja_alt - div_caja_alt + ajuste_nivel3;

            puntos[4, 0] = (radio - 4); puntos[4, 1] = (radio - 4) + radio - div_caja_alt + ajuste_nivel4;
            puntos[5, 0] = (radio - 4) + comp1; puntos[5, 1] = (radio - 4) + comp1 - div_caja_alt - div_caja_alt + ajuste_nivel3;
            puntos[6, 0] = (radio - 4) + radio; puntos[6, 1] = (radio - 4) - div_caja_alt;
            puntos[7, 0] = (radio - 4) + comp1; puntos[7, 1] = (radio - 4) - comp1 + div_caja_alt + ajuste_nivel2;

            //coordenadas flechas
            puntos1[0, 0] = radio + div_caja_hor; puntos1[0, 1] = radio - despl - 20; puntos1[0, 2] = radio + div_caja_hor; puntos1[0, 3] = radio;
            puntos1[1, 0] = radio + div_caja_hor - despl3; puntos1[1, 1] = radio - despl3; puntos1[1, 2] = radio + div_caja_hor; puntos1[1, 3] = radio;
            puntos1[2, 0] = radio + div_caja_hor - despl; puntos1[2, 1] = radio; puntos1[2, 2] = radio + div_caja_hor; puntos1[2, 3] = radio;
            puntos1[3, 0] = radio + div_caja_hor - despl3; puntos1[3, 1] = radio + despl3; puntos1[3, 2] = radio + div_caja_hor; puntos1[3, 3] = radio;
            puntos1[4, 0] = radio + div_caja_hor; puntos1[4, 1] = radio + despl + 20; puntos1[4, 2] = radio + div_caja_hor; puntos1[4, 3] = radio;
            puntos1[5, 0] = radio + div_caja_hor + despl3; puntos1[5, 1] = radio + despl3; puntos1[5, 2] = radio + div_caja_hor; puntos1[5, 3] = radio;
            puntos1[6, 0] = radio + div_caja_hor + despl; puntos1[6, 1] = radio; puntos1[6, 2] = radio + div_caja_hor; puntos1[6, 3] = radio;
            puntos1[7, 0] = radio + div_caja_hor + despl3; puntos1[7, 1] = radio - despl3; puntos1[7, 2] = radio + div_caja_hor; puntos1[7, 3] = radio;

            // circunferencia
            graphics.FillEllipse(brush1, new System.Drawing.Rectangle(+desplazar_hor, +desplazar_ver, radio * 2, radio * 2));

            for (int i = 0; i < list_mensajes.Count; i++)
            {

                Rectangle r = new Rectangle(puntos[orden[i], 0] + desplazar_hor, puntos[orden[i], 1] + desplazar_ver, width_cajas, height_cajas);
                graphics.DrawRectangle(pen_caja, r);
                StringFormat stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Center;
                stringFormat.LineAlignment = StringAlignment.Center;
                graphics.DrawString(list_mensajes[i], font, brush2, r, stringFormat);
                graphics.DrawLine(pen_flechas, puntos1[orden[i], 0] + desplazar_hor, puntos1[orden[i], 1] + desplazar_ver, puntos1[orden[i], 2] + desplazar_hor, puntos1[orden[i], 3] + desplazar_ver);
            }

            //radio interno de union relaciones
            graphics.FillEllipse(brush4, new System.Drawing.Rectangle(radio - radio_punto1 + div_caja_hor + desplazar_hor, radio - radio_punto1 + desplazar_ver, radio_punto1 * 2, radio_punto1 * 2));
            brush1.Dispose();
            graphics.Dispose();
            return bitmap;
        }
        private string GenerarLluvia(List<string> lista_texto, List<string> Lista_Nivel)
        {

            List<string> lista_nodos = Lista_Nivel;
            string LluviaResultado = SrcWhite;
            if (lista_nodos.Count > 0)
            {
                string problema = lista_texto[0];
                int car_problema = problema.Length;
                Color color_fondo = new Color();
                Color color_flechas = new Color();
                Color color_caja = new Color();
                Color color_fuente = new Color();
                Color color_fuente1 = new Color();
                Color color_problema = new Color();
                color_fondo = Color.FromArgb(255, 255, 255);
                color_flechas = Color.FromArgb(25, 72, 63);
                color_fuente = Color.FromArgb(0, 0, 0);
                color_fuente1 = Color.FromArgb(255, 255, 255);
                color_problema = Color.FromArgb(255, 106, 56);
                color_caja = Color.FromArgb(0, 0, 0);

                // PEN
                Pen pen_caja = new Pen(color_caja, 3);
                Pen pen_caja_1 = new Pen(Color.Transparent);

                //BRUSH
                SolidBrush newBrush = new SolidBrush(color_flechas);
                SolidBrush brush = new SolidBrush(color_fuente1);

                // fuente caja titulo
                Font font = new System.Drawing.Font("Century Gothic", 25, FontStyle.Regular, GraphicsUnit.Pixel);
                // fuente primer nivel
                Font font1 = new System.Drawing.Font("Century Gothic", 18, FontStyle.Regular, GraphicsUnit.Pixel);
                font1 = new Font(font1, FontStyle.Bold);
                // fuente problema
                Font font2 = new System.Drawing.Font("Century Gothic", 24, FontStyle.Regular, GraphicsUnit.Pixel);
                font2 = new Font(font2, FontStyle.Bold);
                // alineación de texto (vertical - centro y horizontal - centro)
                StringFormat stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Center;
                stringFormat.LineAlignment = StringAlignment.Center;
                // alineación de texto (vertical - centro y horizontal - cerca del origen)
                StringFormat stringFormat1 = new StringFormat();
                stringFormat1.Alignment = StringAlignment.Center;
                stringFormat1.LineAlignment = StringAlignment.Near;

                List<string> lista_1nivel = new List<string>();
                List<string> lista_1nivel_texto = new List<string>();

                //recuperar la lista del primer nivel
                int numero_1nivel = 0;
                string[] vec_niveles = new string[lista_nodos.Count];
                for (int i = 0; i < lista_nodos.Count; i++)
                {
                    string[] substring = lista_nodos[i].Split('/');
                    if (substring.Length == 2)
                    {
                        numero_1nivel = numero_1nivel + 1;
                        lista_1nivel.Add(substring[1].ToString());
                        lista_1nivel_texto.Add(lista_texto[i]);
                    }
                }

                //variables para ordenar y posicionar los nodos de primer nivel
                int[] orden = new int[8];
                int[] orden1 = new int[8];

                // variables bitamp de cada nodo de segundo nivel
                Bitmap diagrama_hijos_0 = new Bitmap(1, 1);
                Bitmap diagrama_hijos_1 = new Bitmap(1, 1);
                Bitmap diagrama_hijos_2 = new Bitmap(1, 1);
                Bitmap diagrama_hijos_3 = new Bitmap(1, 1);
                Bitmap diagrama_hijos_4 = new Bitmap(1, 1);
                Bitmap diagrama_hijos_5 = new Bitmap(1, 1);
                Bitmap diagrama_hijos_6 = new Bitmap(1, 1);
                Bitmap diagrama_hijos_7 = new Bitmap(1, 1);
                // variable bitmap del segundo nivel
                Bitmap diagrama_central = new Bitmap(1, 1);
                int[] pos_rel_hijos_0 = new int[2];
                int[] pos_rel_hijos_1 = new int[2];
                int[] pos_rel_hijos_2 = new int[2];
                int[] pos_rel_hijos_3 = new int[2];
                int[] pos_rel_hijos_4 = new int[2];
                int[] pos_rel_hijos_5 = new int[2];
                int[] pos_rel_hijos_6 = new int[2];
                int[] pos_rel_hijos_7 = new int[2];

                // variable booleana conocer si el segundo nivel existe
                bool[] bool_hijo = new bool[8];

                //variable bitmap de todo el diagrama
                Bitmap diagrama_total = new Bitmap(1, 1);


                Graphics graphics = System.Drawing.Graphics.FromImage(diagrama_total);
                //tamaño del canvas
                diagrama_total = new Bitmap(diagrama_total, new Size(2700, 1600));
                //define el tamaño que se recorta 
                int Posx_recortar = 500;
                int Posy_recortar = 500;
                int width_recortar = 1500;
                int height_recortar = 700;


                graphics = Graphics.FromImage(diagrama_total);

                //fondo del canvas y calidad de la imagen
                graphics.Clear(color_fondo);
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.TextRenderingHint = TextRenderingHint.AntiAlias;


                #region segundo_nivel
                for (int i = 0; i < numero_1nivel; i++)
                {
                    // variable lista de los nodos de segundo nivel por cada nodo de primer nivel
                    List<string> list_enviar = new List<string>();
                    //orden estandar del diagrama
                    if (i == 0) { orden[0] = 0; orden[1] = 1; orden[2] = 7; orden[3] = 2; orden[4] = 6; orden[5] = 3; orden[6] = 5; orden[7] = 4; }
                    if (i == 1) { orden[0] = 0; orden[1] = 1; orden[2] = 7; orden[3] = 2; orden[4] = 6; orden[5] = 3; orden[6] = 4; orden[7] = 5; }
                    if (i == 2) { orden[0] = 2; orden[1] = 1; orden[2] = 3; orden[3] = 0; orden[4] = 4; orden[5] = 5; orden[6] = 7; orden[7] = 6; }
                    if (i == 3) { orden[0] = 0; orden[1] = 1; orden[2] = 7; orden[3] = 2; orden[4] = 6; orden[5] = 5; orden[6] = 4; orden[7] = 3; }
                    if (i == 4) { orden[0] = 2; orden[1] = 1; orden[2] = 3; orden[3] = 0; orden[4] = 5; orden[5] = 6; orden[6] = 4; orden[7] = 7; }
                    if (i == 5) { orden[0] = 4; orden[1] = 3; orden[2] = 5; orden[3] = 2; orden[4] = 6; orden[5] = 1; orden[6] = 7; orden[7] = 0; }
                    if (i == 6) { orden[0] = 6; orden[1] = 7; orden[2] = 5; orden[3] = 0; orden[4] = 3; orden[5] = 2; orden[6] = 4; orden[7] = 1; }
                    if (i == 7) { orden[0] = 6; orden[1] = 7; orden[2] = 5; orden[3] = 0; orden[4] = 4; orden[5] = 3; orden[6] = 1; orden[7] = 2; }


                    for (int i1 = 0; i1 < lista_nodos.Count; i1++)
                    {

                        string[] substring = lista_nodos[i1].Split('/');
                        if (substring.Length == 3)
                        {
                            if (substring[1] == lista_1nivel[i])
                            {
                                list_enviar.Add(lista_texto[i1]);
                            }
                        }

                    }

                    // guardar bitmaps de segundo nivel y posición de un punto para relacionar el primer y segundo nivel
                    if (list_enviar.Count > 0)
                    {
                        if (i == 0) { diagrama_hijos_0 = lluvia_ideas(list_enviar, orden, color_fondo, color_flechas, color_caja, color_fuente); pos_rel_hijos_0 = posicionar_hijos(list_enviar, i); }
                        if (i == 1) { diagrama_hijos_1 = lluvia_ideas(list_enviar, orden, color_fondo, color_flechas, color_caja, color_fuente); pos_rel_hijos_1 = posicionar_hijos(list_enviar, i); }
                        if (i == 2) { diagrama_hijos_2 = lluvia_ideas(list_enviar, orden, color_fondo, color_flechas, color_caja, color_fuente); pos_rel_hijos_2 = posicionar_hijos(list_enviar, i); }
                        if (i == 3) { diagrama_hijos_3 = lluvia_ideas(list_enviar, orden, color_fondo, color_flechas, color_caja, color_fuente); pos_rel_hijos_3 = posicionar_hijos(list_enviar, i); }
                        if (i == 4) { diagrama_hijos_4 = lluvia_ideas(list_enviar, orden, color_fondo, color_flechas, color_caja, color_fuente); pos_rel_hijos_4 = posicionar_hijos(list_enviar, i); }
                        if (i == 5) { diagrama_hijos_5 = lluvia_ideas(list_enviar, orden, color_fondo, color_flechas, color_caja, color_fuente); pos_rel_hijos_5 = posicionar_hijos(list_enviar, i); }
                        if (i == 6) { diagrama_hijos_6 = lluvia_ideas(list_enviar, orden, color_fondo, color_flechas, color_caja, color_fuente); pos_rel_hijos_6 = posicionar_hijos(list_enviar, i); }
                        if (i == 7) { diagrama_hijos_7 = lluvia_ideas(list_enviar, orden, color_fondo, color_flechas, color_caja, color_fuente); pos_rel_hijos_7 = posicionar_hijos(list_enviar, i); }

                    }

                }

                if (diagrama_hijos_0.Width > 1) { bool_hijo[0] = true; } else { bool_hijo[0] = false; }
                if (diagrama_hijos_1.Width > 1) { bool_hijo[1] = true; } else { bool_hijo[1] = false; }
                if (diagrama_hijos_2.Width > 1) { bool_hijo[2] = true; } else { bool_hijo[2] = false; }
                if (diagrama_hijos_3.Width > 1) { bool_hijo[3] = true; } else { bool_hijo[3] = false; }
                if (diagrama_hijos_4.Width > 1) { bool_hijo[4] = true; } else { bool_hijo[4] = false; }
                if (diagrama_hijos_5.Width > 1) { bool_hijo[5] = true; } else { bool_hijo[5] = false; }
                if (diagrama_hijos_6.Width > 1) { bool_hijo[6] = true; } else { bool_hijo[6] = false; }
                if (diagrama_hijos_7.Width > 1) { bool_hijo[7] = true; } else { bool_hijo[7] = false; }

                //variables para ubicar los bitmaps del segundo nivel generados
                int[] pos_x_hijos = new int[numero_1nivel];
                int[] pos_y_hijos = new int[numero_1nivel];
                // ajuste vertical de bitmaps del segundo nivel
                int despl_y = 0;
                // tamaño del bitmap del segundo nivel
                int height_diagrama_estandar = 470;
                int width_diag_est = 710;

                int Desplazamiento_12567 = 200;
                // posicionar el problema según el número de nodos en el nivel 1
                int[] centro_problema = new int[2];

                if (numero_1nivel == 1)
                {
                    pos_x_hijos[0] = Desplazamiento_12567 + width_diag_est; pos_y_hijos[0] = 0;

                }
                if (numero_1nivel == 2)
                {
                    pos_x_hijos[0] = Desplazamiento_12567 + width_diag_est; pos_y_hijos[0] = 0;
                    pos_x_hijos[1] = Desplazamiento_12567; pos_y_hijos[1] = 0;

                }
                if (numero_1nivel == 3)
                {
                    pos_x_hijos[0] = Desplazamiento_12567 + width_diag_est; pos_y_hijos[0] = 0;
                    pos_x_hijos[1] = Desplazamiento_12567; pos_y_hijos[1] = 0;
                    pos_x_hijos[2] = 0; pos_y_hijos[2] = height_diagrama_estandar + despl_y + 50;

                }
                if (numero_1nivel == 4)
                {
                    pos_x_hijos[0] = Desplazamiento_12567 + width_diag_est; pos_y_hijos[0] = 0;
                    pos_x_hijos[1] = Desplazamiento_12567; pos_y_hijos[1] = 0;
                    pos_x_hijos[2] = 0; pos_y_hijos[2] = height_diagrama_estandar + despl_y + 50;
                    pos_x_hijos[3] = Desplazamiento_12567 + (width_diag_est * 2); pos_y_hijos[3] = 0;

                }
                if (numero_1nivel == 5)
                {
                    pos_x_hijos[0] = Desplazamiento_12567 + width_diag_est; pos_y_hijos[0] = 0;
                    pos_x_hijos[1] = Desplazamiento_12567; pos_y_hijos[1] = 0;
                    pos_x_hijos[2] = 0; pos_y_hijos[2] = height_diagrama_estandar + despl_y + 50;
                    pos_x_hijos[3] = Desplazamiento_12567 + (width_diag_est * 2); pos_y_hijos[3] = 0;
                    pos_x_hijos[4] = Desplazamiento_12567; pos_y_hijos[4] = height_diagrama_estandar + height_diagrama_estandar + despl_y + despl_y + 100;

                }
                if (numero_1nivel == 6)
                {
                    pos_x_hijos[0] = Desplazamiento_12567 + width_diag_est; pos_y_hijos[0] = 0;
                    pos_x_hijos[1] = Desplazamiento_12567; pos_y_hijos[1] = 0;
                    pos_x_hijos[2] = 0; pos_y_hijos[2] = height_diagrama_estandar + despl_y + 50;
                    pos_x_hijos[3] = Desplazamiento_12567 + (width_diag_est * 2); pos_y_hijos[3] = 0;
                    pos_x_hijos[4] = Desplazamiento_12567; pos_y_hijos[4] = height_diagrama_estandar + height_diagrama_estandar + despl_y + despl_y + 100;
                    pos_x_hijos[5] = Desplazamiento_12567 + width_diag_est; pos_y_hijos[5] = height_diagrama_estandar + height_diagrama_estandar + despl_y + despl_y + 100;

                }
                if (numero_1nivel == 7)
                {
                    pos_x_hijos[0] = Desplazamiento_12567 + width_diag_est; pos_y_hijos[0] = 0;
                    pos_x_hijos[1] = Desplazamiento_12567; pos_y_hijos[1] = 0;
                    pos_x_hijos[2] = 0; pos_y_hijos[2] = height_diagrama_estandar + despl_y + 50;
                    pos_x_hijos[3] = Desplazamiento_12567 + (width_diag_est * 2); pos_y_hijos[3] = 0;
                    pos_x_hijos[4] = Desplazamiento_12567; pos_y_hijos[4] = height_diagrama_estandar + height_diagrama_estandar + despl_y + despl_y + 100;
                    pos_x_hijos[5] = Desplazamiento_12567 + width_diag_est; pos_y_hijos[5] = height_diagrama_estandar + height_diagrama_estandar + despl_y + despl_y + 100;
                    pos_x_hijos[6] = Desplazamiento_12567 + (width_diag_est * 2); pos_y_hijos[6] = height_diagrama_estandar + height_diagrama_estandar + despl_y + despl_y + 100;

                }
                if (numero_1nivel == 8)
                {
                    pos_x_hijos[0] = Desplazamiento_12567 + width_diag_est; pos_y_hijos[0] = 0;
                    pos_x_hijos[1] = Desplazamiento_12567; pos_y_hijos[1] = 0;
                    pos_x_hijos[2] = 0; pos_y_hijos[2] = height_diagrama_estandar + despl_y + 50;
                    pos_x_hijos[3] = Desplazamiento_12567 + (width_diag_est * 2); pos_y_hijos[3] = 0;
                    pos_x_hijos[4] = Desplazamiento_12567; pos_y_hijos[4] = height_diagrama_estandar + height_diagrama_estandar + despl_y + despl_y + 100;
                    pos_x_hijos[5] = Desplazamiento_12567 + width_diag_est; pos_y_hijos[5] = height_diagrama_estandar + height_diagrama_estandar + despl_y + despl_y + 100;
                    pos_x_hijos[6] = Desplazamiento_12567 + (width_diag_est * 2); pos_y_hijos[6] = height_diagrama_estandar + height_diagrama_estandar + despl_y + despl_y + 100;
                    pos_x_hijos[7] = (Desplazamiento_12567 * 2) + (width_diag_est * 2) + 40; pos_y_hijos[7] = height_diagrama_estandar + despl_y + 50;

                }
                // graficar segundo nivel
                int[] centro = new int[2];

                centro[0] = 940;
                centro[1] = 762;
                for (int i = 0; i < numero_1nivel; i++)
                {
                    if (i == 0)
                    {
                        graphics.DrawImage(diagrama_hijos_0, new Point(pos_x_hijos[i], pos_y_hijos[i]));

                        if (diagrama_hijos_0.Width > 1)
                        {

                            centro[0] = pos_x_hijos[i] + (diagrama_hijos_0.Width / 2) - 56;
                            Posx_recortar = 500;
                            Posy_recortar = 0;
                            width_recortar = 1500;
                            height_recortar = 1500;
                        }


                    }
                    if (i == 1)
                    {
                        graphics.DrawImage(diagrama_hijos_1, new Point(pos_x_hijos[i], pos_y_hijos[i]));

                        if (diagrama_hijos_1.Width > 1)
                        {
                            Posx_recortar = 0;
                            Posy_recortar = 0;
                            width_recortar = 2000;
                            height_recortar = 1500;
                        }
                    }
                    if (i == 2)
                    {
                        graphics.DrawImage(diagrama_hijos_2, new Point(pos_x_hijos[i], pos_y_hijos[i]));

                        if (diagrama_hijos_2.Width > 1)
                        {

                            centro[1] = pos_y_hijos[i] + (diagrama_hijos_2.Height / 2);
                            Posx_recortar = 0;
                            Posy_recortar = 0;
                            width_recortar = 2000;
                            height_recortar = 1500;
                        }
                    }
                    if (i == 3)
                    {
                        graphics.DrawImage(diagrama_hijos_3, new Point(pos_x_hijos[i], pos_y_hijos[i]));

                        if (diagrama_hijos_3.Width > 1)
                        {
                            Posx_recortar = 0;
                            Posy_recortar = 0;
                            width_recortar = 2400;
                            height_recortar = 1300;
                        }
                    }
                    if (i == 4)
                    {
                        graphics.DrawImage(diagrama_hijos_4, new Point(pos_x_hijos[i], pos_y_hijos[i]));

                        if (diagrama_hijos_4.Width > 1)
                        {
                            Posx_recortar = 0;
                            Posy_recortar = 0;
                            width_recortar = 2400;
                            height_recortar = 1600;
                        }
                    }
                    if (i == 5)
                    {
                        graphics.DrawImage(diagrama_hijos_5, new Point(pos_x_hijos[i], pos_y_hijos[i]));

                        if (diagrama_hijos_5.Width > 1)
                        {
                            Posx_recortar = 0;
                            Posy_recortar = 0;
                            width_recortar = 2400;
                            height_recortar = 1600;
                        }
                    }
                    if (i == 6)
                    {
                        graphics.DrawImage(diagrama_hijos_6, new Point(pos_x_hijos[i], pos_y_hijos[i]));

                        if (diagrama_hijos_6.Width > 1)
                        {
                            Posx_recortar = 0;
                            Posy_recortar = 0;
                            width_recortar = 2400;
                            height_recortar = 1600;
                        }
                    }
                    if (i == 7)
                    {
                        graphics.DrawImage(diagrama_hijos_7, new Point(pos_x_hijos[i], pos_y_hijos[i]));

                        if (diagrama_hijos_7.Width > 1)
                        {
                            Posx_recortar = 0;
                            Posy_recortar = 0;
                            width_recortar = 2600;
                            height_recortar = 1600;
                        }
                    }
                }

                #endregion

                orden1[0] = 0;
                orden1[1] = 1;
                orden1[2] = 2;
                orden1[3] = 3;
                orden1[4] = 4;
                orden1[5] = 5;
                orden1[6] = 6;
                orden1[7] = 7;

                #region primer_nivel

                int numero_car = 0;

                for (int i = 0; i < numero_1nivel; i++)
                {
                    if (lista_1nivel_texto[i].Length > numero_car)
                    {
                        numero_car = lista_1nivel_texto[i].Length;
                    }
                }
                // variable distancia horizontal caja
                int width_cajas = 0;
                // variable distancia verticial caja
                int height_cajas = 0;
                // Radio circunferencia que agrupa
                int radio = 50;
                Pen pen_flechas1 = new Pen(color_flechas, 5);
                Pen pen_flechas4 = new Pen(color_flechas, 5);
                Pen pen_caja1 = new Pen(color_caja);
                Pen pen_caja_principal = new Pen(color_flechas, 3);

                System.Drawing.SolidBrush brush1 = new System.Drawing.SolidBrush(System.Drawing.Color.Transparent);
                System.Drawing.SolidBrush brush2 = new System.Drawing.SolidBrush(color_caja);
                System.Drawing.SolidBrush brush3 = new System.Drawing.SolidBrush(color_flechas);
                System.Drawing.SolidBrush brush4 = new System.Drawing.SolidBrush(color_fondo);
                System.Drawing.SolidBrush brush5 = new System.Drawing.SolidBrush(color_problema);

                pen_flechas1.EndCap = LineCap.RoundAnchor;
                pen_flechas1.StartCap = LineCap.NoAnchor;
                pen_flechas4.EndCap = LineCap.RoundAnchor;
                pen_flechas4.StartCap = LineCap.RoundAnchor;
                int despl = 60;
                double despl1 = despl * 0.9;
                int despl3 = Convert.ToInt32(despl1);

                int correccionProblema_h = -50;
                int correccionProblema_v = 100;

                int div_caja_hor = width_cajas / 2;
                int div_caja_alt = height_cajas / 2;

                #region Caracteristicas_segun_caracteres

                if (numero_car >= 0 && numero_car < 10)
                {
                    width_cajas = 300;
                    height_cajas = 50;
                    radio = 200;
                    div_caja_hor = width_cajas / 2;
                    div_caja_alt = height_cajas / 2;
                    correccionProblema_h = -50;
                    correccionProblema_v = -100;


                }
                if (numero_car >= 10 && numero_car < 20)
                {
                    width_cajas = 300;
                    height_cajas = 50;
                    radio = 200;
                    div_caja_hor = width_cajas / 2;
                    div_caja_alt = height_cajas / 2;
                    correccionProblema_h = -50;
                    correccionProblema_v = -100;

                    despl = 45;
                    despl1 = despl * 0.9;
                    despl3 = Convert.ToInt32(despl1);

                }
                if (numero_car >= 20 && numero_car < 30)
                {
                    width_cajas = 300;
                    height_cajas = 50;
                    radio = 200;
                    div_caja_hor = width_cajas / 2;
                    div_caja_alt = height_cajas / 2;
                    correccionProblema_h = -50;
                    correccionProblema_v = -100;

                    despl = 60;
                    despl1 = despl * 0.9;
                    despl3 = Convert.ToInt32(despl1);

                }
                if (numero_car >= 30 && numero_car < 40)
                {
                    width_cajas = 300;
                    height_cajas = 55;
                    radio = 200;
                    despl = 60;
                    div_caja_hor = width_cajas / 2;
                    div_caja_alt = height_cajas / 2;
                    correccionProblema_h = -50;
                    correccionProblema_v = -100;

                    despl = 50;
                    despl1 = despl * 0.9;
                    despl3 = Convert.ToInt32(despl1);
                }
                if (numero_car >= 40 && numero_car < 50)
                {
                    width_cajas = 300;
                    height_cajas = 80;
                    radio = 200;

                    div_caja_hor = width_cajas / 2;
                    div_caja_alt = height_cajas / 2;
                    correccionProblema_h = -50;
                    correccionProblema_v = -100;

                    despl = 50;
                    despl1 = despl * 0.9;
                    despl3 = Convert.ToInt32(despl1);
                }
                if (numero_car >= 50 && numero_car < 60)
                {
                    width_cajas = 300;
                    height_cajas = 80;
                    radio = 200;

                    div_caja_hor = width_cajas / 2;
                    div_caja_alt = height_cajas / 2;
                    correccionProblema_h = -50;
                    correccionProblema_v = -100;

                    despl = 50;
                    despl1 = despl * 0.9;
                    despl3 = Convert.ToInt32(despl1);
                }
                if (numero_car >= 60 && numero_car < 70)
                {
                    width_cajas = 320;
                    height_cajas = 80;
                    radio = 220;

                    div_caja_hor = width_cajas / 2;
                    div_caja_alt = height_cajas / 2;
                    correccionProblema_h = -10;
                    correccionProblema_v = -80;

                    despl = 50;
                    despl1 = despl * 0.9;
                    despl3 = Convert.ToInt32(despl1);
                }
                if (numero_car >= 70 && numero_car < 80)
                {
                    width_cajas = 340;
                    height_cajas = 80;
                    radio = 250;

                    div_caja_hor = width_cajas / 2;
                    div_caja_alt = height_cajas / 2;
                    correccionProblema_h = 45;
                    correccionProblema_v = -60;

                    despl = 55;
                    despl1 = despl * 0.9;
                    despl3 = Convert.ToInt32(despl1);
                }
                if (numero_car >= 80 && numero_car < 90)
                {
                    width_cajas = 320;
                    height_cajas = 100;
                    radio = 210;

                    div_caja_hor = width_cajas / 2;
                    div_caja_alt = height_cajas / 2;
                    correccionProblema_h = -20;
                    correccionProblema_v = -60;

                    despl = 60;
                    despl1 = despl * 0.9;
                    despl3 = Convert.ToInt32(despl1);

                }
                if (numero_car >= 90 && numero_car <= 100)
                {
                    width_cajas = 320;
                    height_cajas = 100;
                    radio = 210;

                    div_caja_hor = width_cajas / 2;
                    div_caja_alt = height_cajas / 2;
                    correccionProblema_h = -20;
                    correccionProblema_v = -60;

                    despl = 60;
                    despl1 = despl * 0.9;
                    despl3 = Convert.ToInt32(despl1);
                }
                #endregion
                double comp = radio * 0.7071;
                int comp1 = Convert.ToInt32(comp);
                double comp2 = radio * (1 - 0.7071);
                int comp3 = Convert.ToInt32(comp2);
                int[,] puntos = new int[8, 2];
                int[,] puntos1 = new int[8, 4];
                int[,] puntos2 = new int[8, 4];
                //coordenadas cajas
                puntos[0, 0] = centro[0] + radio; puntos[0, 1] = centro[1] - 25;
                puntos[1, 0] = centro[0] + comp3 - 180; puntos[1, 1] = centro[1] + comp3 - 50;
                puntos[2, 0] = centro[0] - 200; puntos[2, 1] = centro[1] + radio;
                puntos[3, 0] = centro[0] + (2 * radio) - comp3 + 180; puntos[3, 1] = centro[1] + comp3 - 50;
                puntos[4, 0] = centro[0] + comp3 - 180; puntos[4, 1] = centro[1] + (2 * radio) - comp3 + 50;
                puntos[5, 0] = centro[0] + radio; puntos[5, 1] = centro[1] + (2 * radio) + 25;
                puntos[6, 0] = centro[0] + (2 * radio) - comp3 + 180; puntos[6, 1] = centro[1] + (2 * radio) - comp3 + 50;
                puntos[7, 0] = centro[0] + (2 * radio) + 200; puntos[7, 1] = centro[1] + radio;
                //coordenadas flechas
                puntos1[0, 0] = radio + div_caja_hor; puntos1[0, 1] = radio - despl - 20; puntos1[0, 2] = radio + div_caja_hor; puntos1[0, 3] = radio;
                puntos1[1, 0] = radio + div_caja_hor - despl3; puntos1[1, 1] = radio - despl3; puntos1[1, 2] = radio + div_caja_hor; puntos1[1, 3] = radio;
                puntos1[2, 0] = radio + div_caja_hor - despl; puntos1[2, 1] = radio; puntos1[2, 2] = radio + div_caja_hor; puntos1[2, 3] = radio;
                puntos1[3, 0] = radio + div_caja_hor - despl3; puntos1[3, 1] = radio + despl3; puntos1[3, 2] = radio + div_caja_hor; puntos1[3, 3] = radio;
                puntos1[4, 0] = radio + div_caja_hor; puntos1[4, 1] = radio + despl + 20; puntos1[4, 2] = radio + div_caja_hor; puntos1[4, 3] = radio;
                puntos1[5, 0] = radio + div_caja_hor + despl3; puntos1[5, 1] = radio + despl3; puntos1[5, 2] = radio + div_caja_hor; puntos1[5, 3] = radio;
                puntos1[6, 0] = radio + div_caja_hor + despl; puntos1[6, 1] = radio; puntos1[6, 2] = radio + div_caja_hor; puntos1[6, 3] = radio;
                puntos1[7, 0] = radio + div_caja_hor + despl3; puntos1[7, 1] = radio - despl3; puntos1[7, 2] = radio + div_caja_hor; puntos1[7, 3] = radio;
                //coordenadas flechas1
                puntos2[0, 0] = centro[0] + radio + div_caja_hor; puntos2[0, 1] = centro[1] - 25;
                puntos2[1, 0] = centro[0] + comp3 - 180 + div_caja_hor; puntos2[1, 1] = centro[1] + comp3 - 50;
                puntos2[2, 0] = centro[0] - 200; puntos2[2, 1] = centro[1] + radio + div_caja_alt;
                puntos2[3, 0] = centro[0] + (2 * radio) - comp3 + 180 + div_caja_hor; puntos2[3, 1] = centro[1] + comp3 - 50;
                puntos2[4, 0] = centro[0] + comp3 - 180 + div_caja_hor; puntos2[4, 1] = centro[1] + (2 * radio) - comp3 + 50 + height_cajas;
                puntos2[5, 0] = centro[0] + radio + div_caja_hor; puntos2[5, 1] = centro[1] + (2 * radio) + 25 + height_cajas;
                puntos2[6, 0] = centro[0] + (2 * radio) - comp3 + 180 + div_caja_hor; puntos2[6, 1] = centro[1] + (2 * radio) - comp3 + 50 + height_cajas;
                puntos2[7, 0] = centro[0] + (2 * radio) + 200 + width_cajas; puntos2[7, 1] = centro[1] + radio + div_caja_alt;
                centro_problema[0] = centro[0] - (radio / 2) + correccionProblema_h; centro_problema[1] = centro[1] + correccionProblema_v;
                int[,] puntosCentro = new int[8, 2];
                int[,] puntosCentro1 = new int[8, 2];
                puntosCentro[0, 0] = centro_problema[0] + 200; puntosCentro[0, 1] = centro_problema[1];
                puntosCentro[1, 0] = centro_problema[0] + 30; puntosCentro[1, 1] = centro_problema[1];
                puntosCentro[2, 0] = centro_problema[0]; puntosCentro[2, 1] = centro_problema[1] + 100;
                puntosCentro[3, 0] = centro_problema[0] + 400 - 30; puntosCentro[3, 1] = centro_problema[1];
                puntosCentro[4, 0] = centro_problema[0] + 30; puntosCentro[4, 1] = centro_problema[1] + 200;
                puntosCentro[5, 0] = centro_problema[0] + 200; puntosCentro[5, 1] = centro_problema[1] + 200;
                puntosCentro[6, 0] = centro_problema[0] + 400 - 30; puntosCentro[6, 1] = centro_problema[1] + 200;
                puntosCentro[7, 0] = centro_problema[0] + 400; puntosCentro[7, 1] = centro_problema[1] + 100;
                puntosCentro1[0, 0] = puntos[0, 0] + (width_cajas / 2); puntosCentro1[0, 1] = puntos[0, 1] + height_cajas;
                puntosCentro1[1, 0] = puntos[1, 0] + (width_cajas / 2); puntosCentro1[1, 1] = puntos[1, 1] + height_cajas;
                puntosCentro1[2, 0] = puntos[2, 0] + width_cajas; puntosCentro1[2, 1] = puntos[2, 1] + (height_cajas / 2);
                puntosCentro1[3, 0] = puntos[3, 0] + (width_cajas / 2); puntosCentro1[3, 1] = puntos[3, 1] + height_cajas;
                puntosCentro1[4, 0] = puntos[4, 0] + (width_cajas / 2); ; puntosCentro1[4, 1] = puntos[4, 1];
                puntosCentro1[5, 0] = puntos[5, 0] + (width_cajas / 2); ; puntosCentro1[5, 1] = puntos[5, 1];
                puntosCentro1[6, 0] = puntos[6, 0] + (width_cajas / 2); ; puntosCentro1[6, 1] = puntos[6, 1];
                puntosCentro1[7, 0] = puntos[7, 0]; puntosCentro1[7, 1] = puntos[7, 1] + (height_cajas / 2);

                Rectangle r3 = new Rectangle(centro_problema[0], centro_problema[1], 400, 200);

                graphics.DrawRectangle(pen_caja_principal, r3);
                graphics.FillRectangle(brush5, r3);
                graphics.DrawString(problema, font2, brush4, r3, stringFormat);


                for (int i = 0; i < numero_1nivel; i++)
                {
                    Rectangle r2 = new Rectangle(puntos[i, 0] - 300, puntos[i, 1] - 230, width_cajas, height_cajas);
                    graphics.DrawRectangle(pen_caja, r2);
                    graphics.DrawString(lista_1nivel_texto[i], font1, brush2, r2, stringFormat);

                    if (i == 0)
                    {
                        puntos2[i, 2] = pos_rel_hijos_0[0];
                        puntos2[i, 3] = pos_rel_hijos_0[1];
                    }
                    if (i == 1)
                    {
                        puntos2[i, 2] = pos_rel_hijos_1[0];
                        puntos2[i, 3] = pos_rel_hijos_1[1];
                    }
                    if (i == 2)
                    {
                        puntos2[i, 2] = pos_rel_hijos_2[0];
                        puntos2[i, 3] = pos_rel_hijos_2[1];
                    }
                    if (i == 3)
                    {
                        puntos2[i, 2] = pos_rel_hijos_3[0];
                        puntos2[i, 3] = pos_rel_hijos_3[1];
                    }
                    if (i == 4)
                    {
                        puntos2[i, 2] = pos_rel_hijos_4[0];
                        puntos2[i, 3] = pos_rel_hijos_4[1];
                    }
                    if (i == 5)
                    {
                        puntos2[i, 2] = pos_rel_hijos_5[0];
                        puntos2[i, 3] = pos_rel_hijos_5[1];
                    }
                    if (i == 6)
                    {
                        puntos2[i, 2] = pos_rel_hijos_6[0];
                        puntos2[i, 3] = pos_rel_hijos_6[1];
                    }
                    if (i == 7)
                    {
                        puntos2[i, 2] = pos_rel_hijos_7[0];
                        puntos2[i, 3] = pos_rel_hijos_7[1];
                    }
                    graphics.DrawLine(pen_flechas4, puntosCentro[i, 0], puntosCentro[i, 1], puntosCentro1[i, 0] - 300, puntosCentro1[i, 1] - 230);
                    // tiene segundo nivel?
                    if (bool_hijo[i] == true)
                    {
                        graphics.DrawLine(pen_flechas1, puntos2[i, 2] + pos_x_hijos[i], puntos2[i, 3] + pos_y_hijos[i], puntos2[i, 0] - 300, puntos2[i, 1] - 230);
                    }
                }
                #endregion
                Rectangle Recortar = new Rectangle(Posx_recortar, Posy_recortar, width_recortar, height_recortar);
                Bitmap nb = new Bitmap(Recortar.Width, Recortar.Height);
                Graphics g = Graphics.FromImage(nb);
                g.DrawImage(diagrama_total, -Recortar.X, -Recortar.Y);
                LluviaResultado = "data:image/png;base64," + ImageToBase64String(nb, ImageFormat.Png);
            }
            return LluviaResultado;
        }
        private int[] posicionar_hijos(List<string> list_enviar, int orden)
        {
            int[] vector_enviar = new int[2];
            List<string> list_mensajes = new List<string>();
            list_mensajes = list_enviar;
            int numero_mensajes = list_mensajes.Count;
            int numero_car = 0;

            for (int i = 0; i < numero_mensajes; i++)
            {
                if (list_mensajes[i].Length > numero_car)
                {
                    numero_car = list_mensajes[i].Length;
                }
            }
            int desplazar_hor = 0;
            int desplazar_ver = 0;
            // variable distancia horizontal caja
            int width_cajas = 0;
            // variable distancia verticial caja
            int height_cajas = 0;
            // Radio circunferencia que agrupa
            int radio = 200;

            int radio_punto = 20;

            int despl = 60;
            double despl1 = despl * 0.9;
            int despl3 = Convert.ToInt32(despl1);



            int div_caja_hor = width_cajas / 2;
            int div_caja_alt = height_cajas / 2;



            #region Caracteristicas_segun_caracteres

            if (numero_car >= 0 && numero_car < 10)
            {
                width_cajas = 150;
                height_cajas = 60;
                radio = 180;
                despl = 80;
                div_caja_hor = width_cajas / 2;
                div_caja_alt = height_cajas / 2;
                radio_punto = 20;

                despl = 50;
                despl1 = despl * 0.9;
                despl3 = Convert.ToInt32(despl1);

                desplazar_hor = 90;
                desplazar_ver = 40;

            }
            if (numero_car >= 10 && numero_car < 20)
            {
                width_cajas = 150;
                height_cajas = 60;
                radio = 180;
                despl = 80;
                div_caja_hor = width_cajas / 2;
                div_caja_alt = height_cajas / 2;
                radio_punto = 20;

                despl = 50;
                despl1 = despl * 0.9;
                despl3 = Convert.ToInt32(despl1);

                desplazar_hor = 90;
                desplazar_ver = 40;

            }
            if (numero_car >= 20 && numero_car < 30)
            {
                width_cajas = 150;
                height_cajas = 60;
                radio = 180;
                despl = 80;
                div_caja_hor = width_cajas / 2;
                div_caja_alt = height_cajas / 2;
                radio_punto = 20;

                despl = 50;
                despl1 = despl * 0.9;
                despl3 = Convert.ToInt32(despl1);

                desplazar_hor = 90;
                desplazar_ver = 40;

            }
            if (numero_car >= 30 && numero_car < 40)
            {

                width_cajas = 150;
                height_cajas = 60;
                radio = 180;
                despl = 80;
                div_caja_hor = width_cajas / 2;
                div_caja_alt = height_cajas / 2;
                radio_punto = 20;

                despl = 50;
                despl1 = despl * 0.9;
                despl3 = Convert.ToInt32(despl1);

                desplazar_hor = 90;
                desplazar_ver = 40;




            }
            if (numero_car >= 40 && numero_car < 50)
            {

                width_cajas = 240;
                height_cajas = 85;
                radio = 235;

                div_caja_hor = width_cajas / 2;
                div_caja_alt = height_cajas / 2;
                radio_punto = 20;


                despl = 50;
                despl1 = despl * 0.9;
                despl3 = Convert.ToInt32(despl1);



            }
            if (numero_car >= 50 && numero_car < 60)
            {
                width_cajas = 240;
                height_cajas = 85;
                radio = 235;

                div_caja_hor = width_cajas / 2;
                div_caja_alt = height_cajas / 2;
                radio_punto = 20;


                despl = 50;
                despl1 = despl * 0.9;
                despl3 = Convert.ToInt32(despl1);
            }
            if (numero_car >= 60 && numero_car < 70)
            {
                width_cajas = 240;
                height_cajas = 85;
                radio = 235;

                div_caja_hor = width_cajas / 2;
                div_caja_alt = height_cajas / 2;
                radio_punto = 20;


                despl = 50;
                despl1 = despl * 0.9;
                despl3 = Convert.ToInt32(despl1);
            }
            if (numero_car >= 70 && numero_car < 80)
            {
                width_cajas = 240;
                height_cajas = 85;
                radio = 235;

                div_caja_hor = width_cajas / 2;
                div_caja_alt = height_cajas / 2;
                radio_punto = 20;


                despl = 50;
                despl1 = despl * 0.9;
                despl3 = Convert.ToInt32(despl1);
            }
            if (numero_car >= 80 && numero_car < 90)
            {
                width_cajas = 240;
                height_cajas = 85;
                radio = 235;

                div_caja_hor = width_cajas / 2;
                div_caja_alt = height_cajas / 2;
                radio_punto = 20;


                despl = 50;
                despl1 = despl * 0.9;
                despl3 = Convert.ToInt32(despl1);
            }
            if (numero_car >= 90 && numero_car <= 100)
            {
                width_cajas = 240;
                height_cajas = 85;
                radio = 235;

                div_caja_hor = width_cajas / 2;
                div_caja_alt = height_cajas / 2;
                radio_punto = 20;


                despl = 50;
                despl1 = despl * 0.9;
                despl3 = Convert.ToInt32(despl1);


            }
            #endregion

            double comp = radio_punto * 0.7071;
            int comp1 = Convert.ToInt32(comp);


            if (orden == 0) { vector_enviar[0] = radio + div_caja_hor + desplazar_hor; vector_enviar[1] = radio + radio_punto + desplazar_ver; }
            if (orden == 1) { vector_enviar[0] = radio + div_caja_hor + comp1 + desplazar_hor; vector_enviar[1] = radio + comp1 + desplazar_ver; }
            if (orden == 2) { vector_enviar[0] = radio + div_caja_hor + radio_punto + desplazar_hor; vector_enviar[1] = radio + desplazar_ver; }
            if (orden == 3) { vector_enviar[0] = radio + div_caja_hor - comp1 + desplazar_hor; vector_enviar[1] = radio + comp1 + desplazar_ver; }
            if (orden == 4) { vector_enviar[0] = radio + div_caja_hor + comp1 + desplazar_hor; vector_enviar[1] = radio - comp1 + desplazar_ver; }
            if (orden == 5) { vector_enviar[0] = radio + div_caja_hor + desplazar_hor; vector_enviar[1] = radio - radio_punto + desplazar_ver; }
            if (orden == 6) { vector_enviar[0] = radio + div_caja_hor - comp1 + desplazar_hor; vector_enviar[1] = radio - comp1 + desplazar_ver; }
            if (orden == 7) { vector_enviar[0] = radio + div_caja_hor - radio_punto + desplazar_hor; vector_enviar[1] = radio + desplazar_ver; }



            return vector_enviar;
        }
        private string FechaString()
        {
            DateTime FechaHoraActual = DateTime.Now;
            string FechaHoraStr = FechaHoraActual.ToString().Replace(" ", "").Replace("p", "").Replace("a", "").Replace("m", "");
            FechaHoraStr = FechaHoraStr.Replace("/", "").Replace(":", "").Replace(".", "");
            return FechaHoraStr;
        }
        public static Image ScaleImage(Image image, int maxWidth, int maxHeight)
        {
            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);

            using (var graphics = Graphics.FromImage(newImage))
                graphics.DrawImage(image, 0, 0, newWidth, newHeight);

            return newImage;
        }

        private List<EDCargo> ConsultarWSRelacionesLaborales(string NitEmpresa, string estadoAfi, string TipoVinAfi)
        {
            int IdCargo = 0;
            var cliente = new RestSharp.RestClient(ConfigurationManager.AppSettings["Url"]);
            var request = new RestRequest(ConfigurationManager.AppSettings["consultaEstadoTipoAfiliado"], RestSharp.Method.GET);
            request.RequestFormat = DataFormat.Xml;

            var usuario = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);

            request.Parameters.Clear();
            request.AddParameter("tpEm", "ni");
            request.AddParameter("docEm", NitEmpresa);
            request.AddParameter("estadoAfi", estadoAfi);
            request.AddParameter("TipoVinAfi", TipoVinAfi);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");

            //se omite la validación de certificado de SSL
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            IRestResponse<List<EstadoTipoAfiliadoDTO>> response = cliente.Execute<List<EstadoTipoAfiliadoDTO>>(request);
            var result = response.Content;
            List<EDCargo> ListaCargos = new List<EDCargo>();
            var respuesta = Newtonsoft.Json.JsonConvert.DeserializeObject<List<EstadoTipoAfiliadoDTO>>(result);
            foreach (EstadoTipoAfiliadoDTO eta in respuesta)
            {
                EDCargo edERL = new EDCargo();
                edERL.IDCargo = IdCargo;
                edERL.NombreCargo = eta.ocupacion;
                ListaCargos.Add(edERL);
                IdCargo = IdCargo + 1;
            }
            return ListaCargos;
        }
        #endregion
    }
}