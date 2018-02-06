using RestSharp;
using SG_SST.Controllers.Base;
using SG_SST.Dtos.Empresas;
using SG_SST.EntidadesDominio.Aplicacion;
using SG_SST.EntidadesDominio.Empleado;
using SG_SST.EntidadesDominio.Empresas;
using SG_SST.Logica.Aplicacion;
using SG_SST.Logica.Empresas;
using SG_SST.Logica.MedicionEvaluacion;
using SG_SST.Models.Ausentismo;
using SG_SST.Models.Empleado;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using SG_SST.EntidadesDominio.Usuario;
using System.Net.Mail;
using Hangfire;
using OfficeOpenXml;
using System.Drawing;
using OfficeOpenXml.Style;

namespace SG_SST.Controllers.Aplicacion
{
    public class BateriaGestionController : BaseController
    {
        bool invalid = false;

        LNBateria LNBateria = new LNBateria();
        LNEmpresa LNEmpresa = new LNEmpresa();
        LNAcciones LNAcciones = new LNAcciones();

        private static string RutaExcelPlantilla = "~/Content/Bateria/Plantilla/AlisstaPlantillaConvocados.xlsx";
        private static string RutaExcelTemp = "~/Content/Bateria/Archivos/ExcelTemp/";

        [HttpGet]
        public ActionResult Index()
        {
            



            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }

            ViewBag.val_fecha1 = null;
            ViewBag.val_fecha2 = null;
            ViewBag.Tipo = null;


            List<EDBateriaGestion> ListaGestion = LNBateria.ConsultarListaGestion(usuarioActual.IdEmpresa);
            List<EDBateria> ListaBaterias = LNBateria.ConsultarBaterias();
            ViewBag.Tipo = new SelectList(ListaBaterias, "Pk_Id_Bateria", "Nombre", 0);

            List<string> ListaOpciones = new List<string>();

            ListaOpciones.Add("Cuestionario de Factores de Riesgo Psicosocial Intralaboral FORMA A");
            ListaOpciones.Add("Cuestionario de Factores de Riesgo Psicosocial Intralaboral FORMA B");
            ListaOpciones.Add("Cuestionario de Factores de Riesgo Psicosocial Intralaboral FORMA A y Cuestionario Extralaboral");
            ListaOpciones.Add("Cuestionario de Factores de Riesgo Psicosocial Intralaboral FORMA B y Cuestionario Extralaboral");
            ListaOpciones.Add("Cuestionario de Factores de Estres");

            int cont = 1;
            List<SelectListItem> ListaCuestionarios = new List<SelectListItem>();
            foreach (var item in ListaOpciones)
            {
                ListaCuestionarios.Add(new SelectListItem { Text = item.ToString(), Value = cont.ToString() });
                cont++;
            }

            ViewBag.Tipo = ListaCuestionarios;

            return View(ListaGestion);
        }
        [HttpPost]
        public ActionResult Index(FormCollection frm)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }
            string fantes = "";
            string fdespues = "";
            string Tipo = "";

            ViewBag.val_fecha1 = "";
            ViewBag.val_fecha2 = "";
            ViewBag.Tipo = "";

            if (frm["FechaAntes"] != null)
            {
                fantes = frm["FechaAntes"].ToString();
                ViewBag.val_fecha1 = frm["FechaAntes"].ToString();
            }
            if (frm["FechaDespues"] != null)
            {
                fdespues = frm["FechaDespues"].ToString();
                ViewBag.val_fecha2 = frm["FechaDespues"].ToString();
            }
            if (frm["Tipo"] != null)
            {
                Tipo = frm["Tipo"].ToString();
                ViewBag.Tipo = frm["Tipo"].ToString();
            }
            int idTipo1 = 0;
            if (int.TryParse(Tipo, out idTipo1))
            {
                
            }

            List<EDBateriaGestion> ListaGestion = LNBateria.ConsultarListaGestionFiltros(usuarioActual.IdEmpresa, fantes, fdespues, idTipo1);
            List<EDBateria> ListaBaterias = LNBateria.ConsultarBaterias();
            

            
            List<string> ListaOpciones = new List<string>();
            ListaOpciones.Add("Cuestionario de Factores de Riesgo Psicosocial Intralaboral FORMA A");
            ListaOpciones.Add("Cuestionario de Factores de Riesgo Psicosocial Intralaboral FORMA B");
            ListaOpciones.Add("Cuestionario de Factores de Riesgo Psicosocial Intralaboral FORMA A y Cuestionario Extralaboral");
            ListaOpciones.Add("Cuestionario de Factores de Riesgo Psicosocial Intralaboral FORMA B y Cuestionario Extralaboral");
            ListaOpciones.Add("Cuestionario de Factores de Estres");

            int cont = 1;
            List<SelectListItem> ListaCuestionarios = new List<SelectListItem>();

            int idTipo = 0;
            if (Tipo != null)
            {
                if (int.TryParse(Tipo, out idTipo))
                {
                    foreach (var item in ListaOpciones)
                    {
                        if (cont== idTipo)
                        {
                            ListaCuestionarios.Add(new SelectListItem { Text = item.ToString(), Value = cont.ToString(), Selected=true });
                        }
                        else
                        {
                            ListaCuestionarios.Add(new SelectListItem { Text = item.ToString(), Value = cont.ToString() });
                        }
                        
                        cont++;
                    }
                }
                else
                {
                    foreach (var item in ListaOpciones)
                    {
                        ListaCuestionarios.Add(new SelectListItem { Text = item.ToString(), Value = cont.ToString() });
                        cont++;
                    }
                }
            }
            else
            {
                foreach (var item in ListaOpciones)
                {
                    ListaCuestionarios.Add(new SelectListItem { Text = item.ToString(), Value = cont.ToString() });
                    cont++;
                }
            }

            

            ViewBag.Tipo = ListaCuestionarios;

            return View(ListaGestion);
        }
        [HttpGet]
        public ActionResult NuevaAplicacionCuestionario()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }

            EDBateriaGestion EDBateriaGestion = new EDBateriaGestion();
            List<EDBateria> ListaBaterias = LNBateria.ConsultarBaterias();
            ViewBag.Tipo = new SelectList(ListaBaterias, "Pk_Id_Bateria", "Nombre", 0);
            var ListaSedes = new List<EDSede>();
            ListaSedes = LNEmpresa.ObtenerSedesPorNit(usuarioActual.NitEmpresa);
            ViewBag.Pk_Id_Sede = new SelectList(ListaSedes, "IdSede", "NombreSede", 0);



            List<string> ListaOpciones = new List<string>();

            ListaOpciones.Add("Cuestionario de Factores de Riesgo Psicosocial Intralaboral FORMA A");
            ListaOpciones.Add("Cuestionario de Factores de Riesgo Psicosocial Intralaboral FORMA B");
            ListaOpciones.Add("Cuestionario de Factores de Riesgo Psicosocial Intralaboral FORMA A y Cuestionario Extralaboral");
            ListaOpciones.Add("Cuestionario de Factores de Riesgo Psicosocial Intralaboral FORMA B y Cuestionario Extralaboral");
            ListaOpciones.Add("Cuestionario de Factores de Estres");

            int cont = 1;
            List<SelectListItem> ListaCuestionarios = new List<SelectListItem>();
            foreach (var item in ListaOpciones)
            {
                ListaCuestionarios.Add(new SelectListItem { Text = item.ToString(), Value = cont.ToString() });
                cont++;
            }

            ViewBag.Tipo = ListaCuestionarios;


            return View(EDBateriaGestion);
        }
        [HttpGet]
        public ActionResult AplicacionCuestionario(string IdGestion)
        {
            string Nombre = "";
            ViewBag.Nombre = "";
            ViewBag.link = "";
            ViewBag.nit = "";
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }
            ViewBag.nit = usuarioActual.NitEmpresa;
            ViewBag.Empresa = usuarioActual.RazonSocialEmpresa;
            EDBateriaGestion EDBateriaGestion = new EDBateriaGestion();
            List<EDBateria> ListaBaterias = LNBateria.ConsultarBaterias();
            int IdGestionInt = 0;
            if (int.TryParse(IdGestion, out IdGestionInt))
            {
                EDBateriaGestion = LNBateria.ConsultarGestion(IdGestionInt, usuarioActual.IdEmpresa);
                Nombre = EDBateriaGestion.NombreBateria;
                ViewBag.Nombre = Nombre;
            }
            
            try
            {
                if (EDBateriaGestion.NombreBateria == "Cuestionario de Factores de Riesgo Psicosocial Intralaboral FORMA A")
                {
                    var fullHeader = new Uri(Url.Action("InicioPublico", "Bateria", new { formdata = EDBateriaGestion.TokenPublico, form = "1" }, Request.Url.Scheme));
                    var clean1 = fullHeader.GetComponents(UriComponents.AbsoluteUri & ~UriComponents.Port, UriFormat.UriEscaped);
                    ViewBag.link = clean1;
                }
                if (EDBateriaGestion.NombreBateria == "Cuestionario de Factores de Riesgo Psicosocial Intralaboral FORMA B")
                {
                    var fullHeader = new Uri(Url.Action("InicioPublico", "Bateria", new { formdata = EDBateriaGestion.TokenPublico, form = "2" }, Request.Url.Scheme));
                    var clean1 = fullHeader.GetComponents(UriComponents.AbsoluteUri & ~UriComponents.Port, UriFormat.UriEscaped);
                    ViewBag.link = clean1;
                }
                if (EDBateriaGestion.NombreBateria == "Cuestionario de Factores de Riesgo Psicosocial Intralaboral FORMA A y Cuestionario Extralaboral")
                {
                    var fullHeader = new Uri(Url.Action("InicioPublico", "Bateria", new { formdata = EDBateriaGestion.TokenPublico, form = "1" }, Request.Url.Scheme));
                    var clean1 = fullHeader.GetComponents(UriComponents.AbsoluteUri & ~UriComponents.Port, UriFormat.UriEscaped);
                    ViewBag.link = clean1;
                }
                if (EDBateriaGestion.NombreBateria == "Cuestionario de Factores de Riesgo Psicosocial Intralaboral FORMA B y Cuestionario Extralaboral")
                {
                    var fullHeader = new Uri(Url.Action("InicioPublico", "Bateria", new { formdata = EDBateriaGestion.TokenPublico, form = "2" }, Request.Url.Scheme));
                    var clean1 = fullHeader.GetComponents(UriComponents.AbsoluteUri & ~UriComponents.Port, UriFormat.UriEscaped);
                    ViewBag.link = clean1;
                }
                if (EDBateriaGestion.NombreBateria == "Cuestionario de Factores de Riesgo Psicosocial Extralaboral")
                {
                    var fullHeader = new Uri(Url.Action("InicioPublico", "Bateria", new { formdata = EDBateriaGestion.TokenPublico, form = "3" }, Request.Url.Scheme));
                    var clean1 = fullHeader.GetComponents(UriComponents.AbsoluteUri & ~UriComponents.Port, UriFormat.UriEscaped);
                    ViewBag.link = clean1;
                }
                if (EDBateriaGestion.NombreBateria == "Cuestionario de Factores de Estres")
                {
                    var fullHeader = new Uri(Url.Action("InicioPublico", "Bateria", new { formdata = EDBateriaGestion.TokenPublico, form = "4" }, Request.Url.Scheme));
                    var clean1 = fullHeader.GetComponents(UriComponents.AbsoluteUri & ~UriComponents.Port, UriFormat.UriEscaped);
                    ViewBag.link = clean1;
                }
            }
            catch (Exception)
            {

            }

            ViewBag.Tipo = new SelectList(ListaBaterias, "Pk_Id_Bateria", "Nombre", 0);
            var ListaSedes = new List<EDSede>();
            var ListaRoles = LNBateria.ConsultarRolesEmpresa(usuarioActual.IdEmpresa);
            ListaSedes = LNEmpresa.ObtenerSedesPorNit(usuarioActual.NitEmpresa);
            ViewBag.Pk_Id_Sede = new SelectList(ListaSedes, "IdSede", "NombreSede", 0);
            ViewBag.Pk_Id_Rol = new SelectList(ListaRoles, "Pk_Id_Rol", "Descripcion", 0);


            List<EDCargo> ListaEDCargo = new List<EDCargo>();
            try
            {
                ListaEDCargo = ConsultarCargos(usuarioActual.NitEmpresa);
            }
            catch (Exception)
            {
            }
            ViewBag.Pk_Id_Cargo = new SelectList(ListaEDCargo, "IDCargo", "NombreCargo", null);
            List<EDRelacionesLaborales> Personas= ListaPersonasWS(usuarioActual.NitEmpresa, "OTRAS OCUPACIONES");

            return View(EDBateriaGestion);
        }
        [HttpPost]
        public ActionResult CrearCuestionario(EDBateriaGestion EDBateriaGestion)
        {
            EDBateriaGestion EDBateriaGestion1 = new EDBateriaGestion();
            List<EDBateria> ListaBaterias = LNBateria.ConsultarBaterias();
            string Nombre = "";
            string FechaRegistro = "";
            bool Probar = false;
            string Estado = "Por favor seleccione la sede";
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                Estado = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { Estado, Probar, EDBateriaGestion1, Nombre, FechaRegistro });
            }
            int Fk_Id_Bateria = EDBateriaGestion.Fk_Id_Bateria;
            if (Fk_Id_Bateria != 0)
            {
                if (Fk_Id_Bateria==1)
                {
                    EDBateriaGestion.Fk_Id_Bateria = 1;
                    EDBateriaGestion.bateriaExtra = 0;
                }
                if (Fk_Id_Bateria == 2)
                {
                    EDBateriaGestion.Fk_Id_Bateria = 2;
                    EDBateriaGestion.bateriaExtra = 0;
                }
                if (Fk_Id_Bateria == 3)
                {
                    EDBateriaGestion.Fk_Id_Bateria = 1;
                    EDBateriaGestion.bateriaExtra = 3;
                }
                if (Fk_Id_Bateria == 4)
                {
                    EDBateriaGestion.Fk_Id_Bateria = 2;
                    EDBateriaGestion.bateriaExtra = 3;
                }
                if (Fk_Id_Bateria == 5)
                {
                    EDBateriaGestion.Fk_Id_Bateria = 4;
                    EDBateriaGestion.bateriaExtra = 0;
                }


                EDBateriaGestion.Fk_Id_Empresa = usuarioActual.IdEmpresa;
                EDBateriaGestion.FechaFinalizacion = DateTime.Now.AddDays(30);
                EDBateriaGestion.FechaRegistro = DateTime.Now;
                EDBateriaGestion.Finalizada = false;
                
                EDBateriaGestion.TokenPublico = RandomString(24);
                EDBateriaGestion.EstadoInt = 1;
                string link = Server.MapPath("fdgdfg/"+ EDBateriaGestion.TokenPublico);
                string baseUrl = Request.Url.Scheme;
                string baseUrl1 = Request.Url.GetLeftPart(UriPartial.Authority)+ "/Bateria/IntralaboralA/?"+ EDBateriaGestion.TokenPublico;
                
                EDBateriaGestion.TokenPublico = RandomString(24);

                EDBateriaGestion1 = LNBateria.CrearGestionNuevo(EDBateriaGestion);

                if (EDBateriaGestion1.Pk_Id_BateriaGestion!=0)
                {
                    Nombre = ListaBaterias.Where(s => s.Pk_Id_Bateria == EDBateriaGestion1.Fk_Id_Bateria).FirstOrDefault().Nombre;
                    FechaRegistro = EDBateriaGestion1.FechaRegistro.ToShortDateString();
                    EDBateriaGestion1.TokenPublico = baseUrl1;
                    Probar = true;
                    Estado = "Cuestionario guardado";
                    return Json(new { Estado, Probar, EDBateriaGestion1, Nombre, FechaRegistro });
                }
            }
            else
            {
                return Json(new { Estado, Probar, EDBateriaGestion1, Nombre, FechaRegistro });
            }
            return Json(new { Estado, Probar, EDBateriaGestion1, Nombre, FechaRegistro });
        }
        [HttpPost]
        public ActionResult CrearUsuario(EDBateriaUsuario EDBateriaUsuario)
        {
            EDBateriaUsuario EDBateriaUsuarioExtra = new EDBateriaUsuario();
            EDBateriaGestion EDBateriaGestion = new EDBateriaGestion();
            List<EDBateriaUsuario> ListaUsuariosCreados = new List<EDBateriaUsuario>();
            bool Probar = false;
            string Estado = "No se ha guardado la información del convocado revise la información suministrada e intente de nuevo";
            int TipoConv = 0;
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                Estado = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { Estado, Probar, ListaUsuariosCreados });
            }
            string a = EDBateriaUsuario.CorreoElectronico;
            string trimmed = a.Trim();
            EDBateriaUsuario.CorreoElectronico= trimmed;
            string correo = EDBateriaUsuario.CorreoElectronico;
            bool ProbarCorreoMismo = LNBateria.VerificarCorreoExistente(correo, EDBateriaUsuario.NumeroIdentificacion, EDBateriaUsuario.Fk_Id_BateriaGestion);
            if (ProbarCorreoMismo)
            {
                Estado = "El correo que desea guardar ya se encuentra registrado, por favor digite otro correo o verifique la información del usuario al cual pertenece esta cuenta de correo";
                return Json(new { Estado, Probar, ListaUsuariosCreados });
            }
            bool ProbarCorreo = ValidarEmail(correo);
            if (!ProbarCorreo)
            {
                Estado = "El correo no tiene el formato válido de un correo electrónico, por favor revise la información del correo eléctronico";
                return Json(new { Estado, Probar, ListaUsuariosCreados });
            }
            EDBateriaUsuario.Id_Empresa = usuarioActual.IdEmpresa;
            EDBateriaUsuario.TokenPrivado =RandomString(24);
            EDBateriaUsuario.EstadoEnvio = 0;
            EDBateriaUsuarioExtra = EDBateriaUsuario;
            
            Probar = LNBateria.CrearConvocado(EDBateriaUsuario);
            if (Probar)
            {
                EDBateriaGestion = LNBateria.ConsultarGestion(EDBateriaUsuario.Fk_Id_BateriaGestion, usuarioActual.IdEmpresa);
                if (EDBateriaGestion.bateriaExtra == 3)
                {
                    EDBateriaUsuarioExtra.NumeroIntentos = 1;
                    Probar = LNBateria.CrearConvocado(EDBateriaUsuarioExtra);
                }
                TipoConv = EDBateriaUsuario.TipoConv;
                ListaUsuariosCreados=LNBateria.ConsultarUsuariosCorreos(EDBateriaUsuario.Id_Empresa, TipoConv, EDBateriaUsuario.Fk_Id_BateriaGestion);
            }
            return Json(new { Estado, Probar, ListaUsuariosCreados });
        }
        [HttpPost]
        public JsonResult EliminarConvocado(string IdConvocado)
        {
            List<EDBateriaUsuario> ListaUsuariosCreados = new List<EDBateriaUsuario>();
            bool probar = false;
            string resultado = "El convocado no ha podido ser eliminado, es posible que no exista este registro";

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                resultado = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { probar, resultado, ListaUsuariosCreados }, JsonRequestBehavior.AllowGet);
            }

            int IdConv = 0;
            bool probarNumero = int.TryParse(IdConvocado, out IdConv);
            if (IdConv != 0)
            {
                EDBateriaUsuario EDBateriaUsuario = LNBateria.ConsultarConvocado(IdConv, usuarioActual.IdEmpresa);
                EDBateriaUsuario EDBateriaUsuario1 = LNBateria.ConsultarConvocadoExtra(IdConv, usuarioActual.IdEmpresa);
                bool BorraElemento = LNBateria.EliminarConvocado(EDBateriaUsuario.Pk_Id_BateriaUsuario, usuarioActual.IdEmpresa);
                if (BorraElemento == false)
                {
                    
                    return Json(new { probar, resultado, ListaUsuariosCreados }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    if (EDBateriaUsuario1 != null)
                    {
                        BorraElemento = LNBateria.EliminarConvocado(EDBateriaUsuario1.Pk_Id_BateriaUsuario, usuarioActual.IdEmpresa);
                    }
                }

                probar = true;
                resultado = "El convocado se ha eliminado correctamente";
                EDBateriaGestion EDBateriaGestion = new EDBateriaGestion();
                EDBateriaGestion = LNBateria.ConsultarGestion(EDBateriaUsuario.Fk_Id_BateriaGestion, usuarioActual.IdEmpresa);
                ListaUsuariosCreados = EDBateriaGestion.ListaUsuarios;
                return Json(new { probar, resultado, ListaUsuariosCreados }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { probar, resultado, ListaUsuariosCreados }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult EliminarConvocado1(string IdConvocado)
        {
            List<EDBateriaUsuario> ListaUsuariosCreados = new List<EDBateriaUsuario>();
            bool probar = false;
            string resultado = "El usuario no ha podido ser eliminado, es posible que no exista este registro";

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                resultado = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { probar, resultado, ListaUsuariosCreados }, JsonRequestBehavior.AllowGet);
            }

            int IdConv = 0;
            bool probarNumero = int.TryParse(IdConvocado, out IdConv);
            if (IdConv != 0)
            {
                EDBateriaUsuario EDBateriaUsuario = LNBateria.ConsultarConvocado(IdConv, usuarioActual.IdEmpresa);
                EDBateriaUsuario EDBateriaUsuario1 = LNBateria.ConsultarConvocadoExtra(IdConv, usuarioActual.IdEmpresa);
                bool BorraElemento = LNBateria.EliminarConvocado(EDBateriaUsuario.Pk_Id_BateriaUsuario, usuarioActual.IdEmpresa);
                if (BorraElemento == false)
                {

                    return Json(new { probar, resultado, ListaUsuariosCreados }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    if (EDBateriaUsuario1 != null)
                    {
                        BorraElemento = LNBateria.EliminarConvocado(EDBateriaUsuario1.Pk_Id_BateriaUsuario, usuarioActual.IdEmpresa);
                    }
                }

                probar = true;
                resultado = "El usuario se ha eliminado correctamente";
                EDBateriaGestion EDBateriaGestion = new EDBateriaGestion();
                EDBateriaGestion = LNBateria.ConsultarGestion(EDBateriaUsuario.Fk_Id_BateriaGestion, usuarioActual.IdEmpresa);
                ListaUsuariosCreados = EDBateriaGestion.ListaUsuarios;
                return Json(new { probar, resultado, ListaUsuariosCreados }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { probar, resultado, ListaUsuariosCreados }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult EnviarCorreo1(EDBateriaUsuario EDBateriaUsuario)
        {
            bool probar = false;
            string resultado = "No se ha enviado el correo";
            string Decode = System.Net.WebUtility.HtmlDecode(EDBateriaUsuario.NumeroIdentificacion);
            
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                resultado = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
            }
            List<EDBateria> ListaBaterias = LNBateria.ConsultarBaterias();
            EDBateriaGestion EDBateriaGestion = LNBateria.ConsultarGestion(EDBateriaUsuario.Pk_Id_BateriaUsuario, usuarioActual.IdEmpresa);
            List<EDBateriaUsuario> ListaUsuario = EDBateriaGestion.ListaUsuarios;
            ListaUsuario = ListaUsuario.Where(s => s.EstadoEnvio != 1 && s.NumeroIntentos==0).ToList();

            if (ListaUsuario.Count==0)
            {
                resultado = "No existen correos para enviar, agregue convocados para enviar el correo";
                return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
            }
            string Nombre = ListaBaterias.Where(s => s.Pk_Id_Bateria == EDBateriaGestion.Fk_Id_Bateria).FirstOrDefault().Nombre;
            string link = "";
            EDBateriaGestion.NombreBateria = Nombre;

            try
            {
                if (Nombre == "Cuestionario de Factores de Riesgo Psicosocial Intralaboral FORMA A")
                {
                    var fullHeader = new Uri(Url.Action("IntralaboralA", "Bateria", new { formdata = "FORMDATA", pagina = "1", form = "1" }, Request.Url.Scheme));
                    var clean1 = fullHeader.GetComponents(UriComponents.AbsoluteUri & ~UriComponents.Port, UriFormat.UriEscaped);
                    link = clean1;
                }
                if (Nombre == "Cuestionario de Factores de Riesgo Psicosocial Intralaboral FORMA B")
                {
                    var fullHeader = new Uri(Url.Action("IntralaboralB", "Bateria", new { formdata = "FORMDATA", pagina = "1", form = "2" }, Request.Url.Scheme));
                    var clean1 = fullHeader.GetComponents(UriComponents.AbsoluteUri & ~UriComponents.Port, UriFormat.UriEscaped);
                    link = clean1;
                }
                if (Nombre == "Cuestionario de Factores de Riesgo Psicosocial Intralaboral FORMA A y Cuestionario Extralaboral")
                {
                    var fullHeader = new Uri(Url.Action("IntralaboralA", "Bateria", new { formdata = "FORMDATA", pagina = "1", form = "1" }, Request.Url.Scheme));
                    var clean1 = fullHeader.GetComponents(UriComponents.AbsoluteUri & ~UriComponents.Port, UriFormat.UriEscaped);
                    link = clean1;
                }
                if (Nombre == "Cuestionario de Factores de Riesgo Psicosocial Intralaboral FORMA B y Cuestionario Extralaboral")
                {
                    var fullHeader = new Uri(Url.Action("IntralaboralB", "Bateria", new { formdata = "FORMDATA", pagina = "1", form = "2" }, Request.Url.Scheme));
                    var clean1 = fullHeader.GetComponents(UriComponents.AbsoluteUri & ~UriComponents.Port, UriFormat.UriEscaped);
                    link = clean1;
                }
                if (Nombre == "Cuestionario de Factores de Riesgo Psicosocial Extralaboral")
                {
                    var fullHeader = new Uri(Url.Action("Extralaboral", "Bateria", new { formdata = "FORMDATA", pagina = "1", form = "3" }, Request.Url.Scheme));
                    var clean1 = fullHeader.GetComponents(UriComponents.AbsoluteUri & ~UriComponents.Port, UriFormat.UriEscaped);
                    link = clean1;
                }
                if (Nombre == "Cuestionario de Factores de Estres")
                {
                    var fullHeader = new Uri(Url.Action("Estres", "Bateria", new { formdata = "FORMDATA", pagina = "1", form = "4" }, Request.Url.Scheme));
                    var clean1 = fullHeader.GetComponents(UriComponents.AbsoluteUri & ~UriComponents.Port, UriFormat.UriEscaped);
                    link = clean1;
                }
            }
            catch (Exception)
            {
            }
            string url = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            var clean2 = new Uri(url).GetComponents(UriComponents.AbsoluteUri & ~UriComponents.Port, UriFormat.UriEscaped);

            var jobId = BackgroundJob.Schedule(() => EnviarCorreo(Decode, EDBateriaUsuario.Pk_Id_BateriaUsuario, usuarioActual.IdEmpresa, link, clean2), TimeSpan.FromSeconds(15));
            //EnviarCorreo(Decode, EDBateriaUsuario.Pk_Id_BateriaUsuario, usuarioActual.IdEmpresa, link, url);
            probar = true;
            resultado = "El envió de los correos para esta convocatoria ha empezado, por favor dirijase a la sección ADMINISTRACION DE USUARIOS de este módulo para revisar el estado del envió del presente correo";
            return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);

        }
        public void EnviarCorreo(string cuerpomailPersonal, int IdGestion, int IdEmpresa, string link, string UrlImagen)
        {
            string date = DateTime.Today.ToShortDateString();
            link = System.Net.WebUtility.HtmlDecode(link);
            string Decode = System.Net.WebUtility.HtmlDecode(cuerpomailPersonal);
            List<EDBateria> ListaBaterias = LNBateria.ConsultarBaterias();
            EDBateriaGestion EDBateriaGestion = LNBateria.ConsultarGestion(IdGestion, IdEmpresa);
            List<EDBateriaUsuario> ListaUsuario1 = EDBateriaGestion.ListaUsuarios;
            string Plantilla1 = LNBateria.PlantillaCorreo("NotificacionBateria");
            Plantilla1 = Plantilla1.Replace("[[CuerpoMensaje]]", Decode);
            Plantilla1 = Plantilla1.Replace("[[RutaHttpSitio]]", UrlImagen);
            Plantilla1 = Plantilla1.Replace("[[DATE]]", date);
            LNBateria.EnviarCorreo(Plantilla1, ListaUsuario1, link);
        }
        public void ReenviarCorreo(EDBateriaUsuario EDBateriaUsuario)
        {
            LNBateria.ReenviarCorreo(EDBateriaUsuario);
        }
        [HttpPost]
        public JsonResult ReenviarCorreo(string IdConvocado)
        {
            bool probar = false;
            string resultado = "El sistema no pudo reenviar el correo, por favor recargue la página y vuelva a intentar";
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                resultado = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
            }
            int IdConv = 0;
            bool probarNumero = int.TryParse(IdConvocado, out IdConv);
            if (IdConv != 0)
            {
                EDBateriaUsuario EDBateriaUsuario = LNBateria.ConsultarConvocado(IdConv, usuarioActual.IdEmpresa);
                if (EDBateriaUsuario!=null)
                {
                    if (EDBateriaUsuario.EstadoEnvio==1)
                    {
                        var jobId = BackgroundJob.Schedule(() => ReenviarCorreo(EDBateriaUsuario), TimeSpan.FromSeconds(3));
                        probar = true;
                        resultado = "El envió del correo se programó correctamente";
                        return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        resultado = "El envió del correo no se realizó, compruebe si el correo se ha enviado desde la función de convocatoria de usuarios antes de reenviarlo";
                        return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);

                    }
                    
                }
                else
                {
                    return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
                }
                
            }
            return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Configurar(string IdGestion)
        {
            string Nombre = "";
            ViewBag.Nombre = "";
            ViewBag.link = "";
            ViewBag.nit = "";
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }
            ViewBag.nit = usuarioActual.NitEmpresa;
            EDBateriaGestion EDBateriaGestion = new EDBateriaGestion();
            List<EDBateria> ListaBaterias = LNBateria.ConsultarBaterias();
            int IdGestionInt = 0;
            if (int.TryParse(IdGestion, out IdGestionInt))
            {
                EDBateriaGestion = LNBateria.ConsultarGestion(IdGestionInt, usuarioActual.IdEmpresa);
                ViewBag.Nombre = EDBateriaGestion.NombreBateria;
            }

            try
            {
                if (EDBateriaGestion.NombreBateria == "Cuestionario de Factores de Riesgo Psicosocial Intralaboral FORMA A")
                {
                    var fullHeader = new Uri(Url.Action("InicioPublico", "Bateria", new { formdata = EDBateriaGestion.TokenPublico, form = "1" }, Request.Url.Scheme));
                    var clean1 = fullHeader.GetComponents(UriComponents.AbsoluteUri & ~UriComponents.Port, UriFormat.UriEscaped);
                    ViewBag.link = clean1;
                }
                if (EDBateriaGestion.NombreBateria == "Cuestionario de Factores de Riesgo Psicosocial Intralaboral FORMA B")
                {
                    var fullHeader = new Uri(Url.Action("InicioPublico", "Bateria", new { formdata = EDBateriaGestion.TokenPublico, form = "2" }, Request.Url.Scheme));
                    var clean1 = fullHeader.GetComponents(UriComponents.AbsoluteUri & ~UriComponents.Port, UriFormat.UriEscaped);
                    ViewBag.link = clean1;
                }
                if (EDBateriaGestion.NombreBateria == "Cuestionario de Factores de Riesgo Psicosocial Intralaboral FORMA A y Cuestionario Extralaboral")
                {
                    var fullHeader = new Uri(Url.Action("InicioPublico", "Bateria", new { formdata = EDBateriaGestion.TokenPublico, form = "1" }, Request.Url.Scheme));
                    var clean1 = fullHeader.GetComponents(UriComponents.AbsoluteUri & ~UriComponents.Port, UriFormat.UriEscaped);
                    ViewBag.link = clean1;
                }
                if (EDBateriaGestion.NombreBateria == "Cuestionario de Factores de Riesgo Psicosocial Intralaboral FORMA B y Cuestionario Extralaboral")
                {
                    var fullHeader = new Uri(Url.Action("InicioPublico", "Bateria", new { formdata = EDBateriaGestion.TokenPublico, form = "2" }, Request.Url.Scheme));
                    var clean1 = fullHeader.GetComponents(UriComponents.AbsoluteUri & ~UriComponents.Port, UriFormat.UriEscaped);
                    ViewBag.link = clean1;
                }
                if (EDBateriaGestion.NombreBateria == "Cuestionario de Factores de Riesgo Psicosocial Extralaboral")
                {
                    var fullHeader = new Uri(Url.Action("InicioPublico", "Bateria", new { formdata = EDBateriaGestion.TokenPublico, form = "3" }, Request.Url.Scheme));
                    var clean1 = fullHeader.GetComponents(UriComponents.AbsoluteUri & ~UriComponents.Port, UriFormat.UriEscaped);
                    ViewBag.link = clean1;
                }
                if (EDBateriaGestion.NombreBateria == "Cuestionario de Factores de Estres")
                {
                    var fullHeader = new Uri(Url.Action("InicioPublico", "Bateria", new { formdata = EDBateriaGestion.TokenPublico, form = "4" }, Request.Url.Scheme));
                    var clean1 = fullHeader.GetComponents(UriComponents.AbsoluteUri & ~UriComponents.Port, UriFormat.UriEscaped);
                    ViewBag.link = clean1;
                }
            }
            catch (Exception)
            {

            }
            
            

            List<string> ListaOpciones = new List<string>();

            ListaOpciones.Add("Convocatoria - Inactivo");
            ListaOpciones.Add("Activo - Aplicación en curso");
            ListaOpciones.Add("Finalización");
            List<SelectListItem> ListaEstados = new List<SelectListItem>();
            int cont = 1;
            int index = 0;
            foreach (var item in ListaOpciones)
            {
                if (EDBateriaGestion.EstadoInt==cont)
                {
                    ListaEstados.Add(new SelectListItem { Text = item.ToString(), Value = cont.ToString(), Selected = true });
                    index = cont;
                }
                else
                {
                    ListaEstados.Add(new SelectListItem { Text = item.ToString(), Value = cont.ToString() });
                    index = cont;
                }
                cont++;
            }
            ViewBag.Estado = ListaEstados;
            return View(EDBateriaGestion);
        }
        [HttpGet]
        public ActionResult AdmoUsuarios(string IdGestion, string orden, string filtro, string doc)
        {

            ViewBag.Nombre = "";
            ViewBag.FechaRegistro = "";
            ViewBag.FechaFinalizacion = "";
            ViewBag.IdGestion = "";
            ViewBag.EstadoStr = "";
            ViewBag.EstadoNumber = 0;
            ViewBag.link = "";
            ViewBag.IdGestion = "0";


            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }
            EDBateriaGestion EDBateriaGestion = new EDBateriaGestion();
            List<EDBateria> ListaBaterias = LNBateria.ConsultarBaterias();
            int IdGestionInt = 0;
            if (int.TryParse(IdGestion, out IdGestionInt))
            {
                EDBateriaGestion = LNBateria.ConsultarGestion(IdGestionInt, usuarioActual.IdEmpresa);

                ViewBag.Nombre = EDBateriaGestion.NombreBateria;

                ViewBag.FechaRegistro = EDBateriaGestion.FechaRegistro.ToShortDateString();
                ViewBag.FechaFinalizacion = EDBateriaGestion.FechaFinalizacion.ToShortDateString();
                ViewBag.IdGestion = EDBateriaGestion.Pk_Id_BateriaGestion;
                ViewBag.EstadoStr = EDBateriaGestion.Estado;
                ViewBag.EstadoNumber = EDBateriaGestion.EstadoInt;
                ViewBag.IdGestion = IdGestion;
            }
            List<EDBateriaUsuario> ListaUsuarios = new List<EDBateriaUsuario>();
            List<EDBateriaUsuario> ListaUsuariosCons = new List<EDBateriaUsuario>();
            ListaUsuarios = EDBateriaGestion.ListaUsuarios;

            if (doc != null)
            {
                ListaUsuarios = ListaUsuarios.Where(s => s.NumeroIdentificacion.ToLower().Contains(doc.ToLower())).ToList();
            }

            if (orden != null)
            {
                if (filtro != null)
                {
                    if (filtro == "1")
                    {
                        ListaUsuariosCons = ListaUsuarios.ToList();
                    }
                    if (filtro == "2")
                    {
                        ListaUsuariosCons = ListaUsuarios.Where(s => s.EstadoEnvio == 0).ToList();
                    }
                    if (filtro == "3")
                    {
                        ListaUsuariosCons = ListaUsuarios.Where(s => s.EstadoEnvio == 1 && s.CorreoElectronico != null).ToList();
                    }
                    if (filtro == "4")
                    {
                        ListaUsuariosCons = ListaUsuarios.Where(s => s.RegistroOperacion != "Fin" && s.EstadoEnvio == 1).ToList();
                    }
                    if (filtro == "5")
                    {
                        ListaUsuariosCons = ListaUsuarios.Where(s => s.RegistroOperacion == "Fin" && s.EstadoEnvio == 1 && s.ConfirmacionParticipacion == "Aceptado").ToList();
                    }
                    if (filtro == "6")
                    {
                        ListaUsuariosCons = ListaUsuarios.Where(s => s.RegistroOperacion == "Fin" && s.EstadoEnvio == 1 && s.ConfirmacionParticipacion == null).ToList();
                    }
                    if (filtro == "7")
                    {
                        ListaUsuariosCons = ListaUsuarios.Where(s => s.EstadoEnvio == 1 && s.CorreoElectronico==null).ToList();
                    }

                    if (orden == "1")
                    {
                        ListaUsuariosCons = ListaUsuariosCons.OrderBy(s => s.Nombre).ToList();
                    }
                    if (orden == "2")
                    {
                        ListaUsuariosCons = ListaUsuariosCons.OrderBy(s => s.NumeroIdentificacion).ToList();
                    }
                }
            }

            List<string> ListaOpcionesOrden = new List<string>();
            ListaOpcionesOrden.Add("Nombre");
            ListaOpcionesOrden.Add("Documento de Identidad");

            List<string> ListaOpcionesFiltro = new List<string>();
            ListaOpcionesFiltro.Add("Todos");
            ListaOpcionesFiltro.Add("No se les ha enviado correo");
            ListaOpcionesFiltro.Add("Se les ha enviado correo");
            ListaOpcionesFiltro.Add("No han diligenciado el cuestionario");
            ListaOpcionesFiltro.Add("Han diligenciado aceptando de los terminos");
            ListaOpcionesFiltro.Add("Han diligenciado rechazando de los terminos");
            ListaOpcionesFiltro.Add("Ha sido invitado por el link público");

            List<SelectListItem> ListaOrden = new List<SelectListItem>();
            List<SelectListItem> ListaFiltro = new List<SelectListItem>();
            int cont = 1;
            foreach (var item in ListaOpcionesOrden)
            {
                if (cont.ToString()==orden)
                {
                    ListaOrden.Add(new SelectListItem { Text = item.ToString(), Value = cont.ToString(),Selected=true});
                }
                else
                {
                    ListaOrden.Add(new SelectListItem { Text = item.ToString(), Value = cont.ToString() });
                }
                
                cont++;
            }
            cont = 1;
            foreach (var item in ListaOpcionesFiltro)
            {
                if (cont.ToString() == filtro)
                {
                    ListaFiltro.Add(new SelectListItem { Text = item.ToString(), Value = cont.ToString(), Selected = true });
                }
                else
                {
                    ListaFiltro.Add(new SelectListItem { Text = item.ToString(), Value = cont.ToString() });
                }
                
                cont++;
            }
            ViewBag.Ordenar = ListaOrden;
            ViewBag.Filtrar = ListaFiltro;
            ViewBag.doc = doc;


            

            return View(ListaUsuariosCons);
        }
        [HttpPost]
        public ActionResult CambiarEstado(List<String> values)
        {
            bool Probar = false;
            string Estado = "";
            string Estado1 = values[1];
            string Id = values[0];
            int IdInt = 0;
            int EstadoInt = 0;
            if (int.TryParse(Id, out IdInt) && int.TryParse(Estado1, out EstadoInt))
            {
                if (EstadoInt!=0)
                {
                    bool terminar = LNBateria.EditarEstadoGestion(IdInt, EstadoInt);
                }
            }
            return Json(new { Estado, Probar });
        }
        [HttpPost]
        public JsonResult EliminarAplicacion(string IdGestion)
        {
            bool probar = false;
            string resultado = "El registro de la aplicacion del cuestionario no ha podido ser eliminado";

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                resultado = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
            }
            int IdElemento = 0;
            bool probarNumero = int.TryParse(IdGestion, out IdElemento);
            if (IdElemento != 0)
            {
                bool conResultados = LNBateria.GestionConResultados(IdElemento, usuarioActual.IdEmpresa);
                if (conResultados)
                {
                    resultado = "No se puede eliminar este registro, por que existen cuestionarios ya diligenciados";
                    return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
                }
                bool BorraElemento = LNBateria.EliminarGestion(IdElemento, usuarioActual.IdEmpresa);
                if (BorraElemento)
                {
                    probar = true;
                    resultado = "El registro de la aplicación del cuestionario se ha eliminado correctamente";
                    return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { probar, resultado }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult ResultadoGeneral(string IdGestion, int form)
        {
            ViewBag.Nombre = "";
            ViewBag.NombreBat = "";
            ViewBag.link = "";
            ViewBag.nit = "";
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }
            ViewBag.nit = usuarioActual.NitEmpresa;
            EDBateriaGestion EDBateriaGestion = new EDBateriaGestion();
            EDBateriaGestion EDBateriaGestionConsulta = new EDBateriaGestion();
            List<EDBateria> ListaBaterias = LNBateria.ConsultarBaterias();


            //Consulta Gestion
            int IdGestionInt = 0;
            if (int.TryParse(IdGestion, out IdGestionInt))
            {
                EDBateriaGestionConsulta = LNBateria.ConsultarGestion(IdGestionInt, usuarioActual.IdEmpresa);
                ViewBag.Nombre = EDBateriaGestionConsulta.NombreBateria.Replace("Estres","Estrés");
            }
            //Consulta Usuarios terminados
            List<EDBateriaUsuario> ListaUsuariosTerminados = new List<EDBateriaUsuario>();
            List<EDBateriaUsuario> ListaUsuariosTerminados1 = new List<EDBateriaUsuario>();
            int usuariototal = 0;
            int termtotal = 0;
            foreach (var item in EDBateriaGestionConsulta.ListaUsuarios)
            {
                int pkIdUsuario = item.Pk_Id_BateriaUsuario;
                EDBateriaUsuario Usuario = new EDBateriaUsuario();
                Usuario = LNBateria.ConsultarConvocadoId(pkIdUsuario, usuarioActual.IdEmpresa);
                if (Usuario.NumeroIntentos==form)
                {
                    if (Usuario.RegistroOperacion== "Fin" && Usuario.ConfirmacionParticipacion=="Aceptado")
                    {
                        ListaUsuariosTerminados.Add(Usuario);
                    }
                    usuariototal++;
                }

                if (Usuario.RegistroOperacion == "Fin" && Usuario.ConfirmacionParticipacion == "Aceptado")
                {
                    termtotal++;
                }
                
                
            }
            ViewBag.Total = usuariototal.ToString();
            EDBateriaGestionConsulta.ListaUsuarios = ListaUsuariosTerminados;
            //Calculo de valores transformados y nivel de riesgo por usuario
            int contTer = EDBateriaGestionConsulta.ListaUsuarios.Count();
            ViewBag.Terminados = contTer;

            decimal ejecutado = 0;
            if (usuariototal!=0)
            {
                ejecutado = Math.Round((decimal)contTer / (decimal)usuariototal, 1);
                ejecutado = ejecutado * 100;
            }

            ViewBag.Ejecutado = ejecutado.ToString()+"%";


            foreach (var itemUs in EDBateriaGestionConsulta.ListaUsuarios)
            {
                #region CalculoValores&riesgo

                #region Dominios&Dimensiones
                
                List<EDBateriaDominio> ListaDominios = new List<EDBateriaDominio>();
                if (EDBateriaGestionConsulta.Fk_Id_Bateria == 1 && itemUs.NumeroIntentos == 0)
                {
                    itemUs.NombreEncuestaTotal = "TOTAL GENERAL FACTORES DE RIESGO PSICOSOCIAL INTRALABORAL";
                    itemUs.NombreEncuesta = "Cuestionario de Factores de Riesgo Psicosocial Intralaboral FORMA A";
                    itemUs.FactorTransformacion = 492;
                    EDBateriaDominio EDBateriaDominio = new EDBateriaDominio();
                    EDBateriaDominio.Nombre = "Liderazgo y relaciones sociales en el trabajo";
                    EDBateriaDominio.Pk_Id_BateriaDimension = 1;
                    EDBateriaDominio.ListaDimensiones = LNBateria.ListaDimensiones(1, EDBateriaGestionConsulta.Fk_Id_Bateria);
                    EDBateriaDominio.FactorTransformacion = 164;

                    EDBateriaDominio EDBateriaDominio1 = new EDBateriaDominio();
                    EDBateriaDominio1.Nombre = "Control sobre el trabajo";
                    EDBateriaDominio1.Pk_Id_BateriaDimension = 2;
                    EDBateriaDominio1.ListaDimensiones = LNBateria.ListaDimensiones(2, EDBateriaGestionConsulta.Fk_Id_Bateria);
                    EDBateriaDominio1.FactorTransformacion = 84;

                    EDBateriaDominio EDBateriaDominio2 = new EDBateriaDominio();
                    EDBateriaDominio2.Nombre = "Demandas del trabajo";
                    EDBateriaDominio2.Pk_Id_BateriaDimension = 3;
                    EDBateriaDominio2.ListaDimensiones = LNBateria.ListaDimensiones(3, EDBateriaGestionConsulta.Fk_Id_Bateria);
                    EDBateriaDominio2.FactorTransformacion = 200;

                    EDBateriaDominio EDBateriaDominio3 = new EDBateriaDominio();
                    EDBateriaDominio3.Nombre = "Recompensas";
                    EDBateriaDominio3.Pk_Id_BateriaDimension = 4;
                    EDBateriaDominio3.ListaDimensiones = LNBateria.ListaDimensiones(4, EDBateriaGestionConsulta.Fk_Id_Bateria);
                    EDBateriaDominio3.FactorTransformacion = 44;

                    ListaDominios.Add(EDBateriaDominio);
                    ListaDominios.Add(EDBateriaDominio1);
                    ListaDominios.Add(EDBateriaDominio2);
                    ListaDominios.Add(EDBateriaDominio3);
                }
                if (EDBateriaGestionConsulta.Fk_Id_Bateria == 2 && itemUs.NumeroIntentos == 0)
                {
                    itemUs.NombreEncuestaTotal = "TOTAL GENERAL FACTORES DE RIESGO PSICOSOCIAL INTRALABORAL";
                    itemUs.NombreEncuesta = "Cuestionario de Factores de Riesgo Psicosocial Intralaboral FORMA B";
                    itemUs.FactorTransformacion = 388;
                    EDBateriaDominio EDBateriaDominio = new EDBateriaDominio();
                    EDBateriaDominio.Nombre = "Liderazgo y relaciones sociales en el trabajo";
                    EDBateriaDominio.Pk_Id_BateriaDimension = 1;
                    EDBateriaDominio.ListaDimensiones = LNBateria.ListaDimensiones(1, EDBateriaGestionConsulta.Fk_Id_Bateria);
                    EDBateriaDominio.FactorTransformacion = 120;

                    EDBateriaDominio EDBateriaDominio1 = new EDBateriaDominio();
                    EDBateriaDominio1.Nombre = "Control sobre el trabajo";
                    EDBateriaDominio1.Pk_Id_BateriaDimension = 2;
                    EDBateriaDominio1.ListaDimensiones = LNBateria.ListaDimensiones(2, EDBateriaGestionConsulta.Fk_Id_Bateria);
                    EDBateriaDominio1.FactorTransformacion = 72;

                    EDBateriaDominio EDBateriaDominio2 = new EDBateriaDominio();
                    EDBateriaDominio2.Nombre = "Demandas del trabajo";
                    EDBateriaDominio2.Pk_Id_BateriaDimension = 3;
                    EDBateriaDominio2.ListaDimensiones = LNBateria.ListaDimensiones(3, EDBateriaGestionConsulta.Fk_Id_Bateria);
                    EDBateriaDominio2.FactorTransformacion = 156;

                    EDBateriaDominio EDBateriaDominio3 = new EDBateriaDominio();
                    EDBateriaDominio3.Nombre = "Recompensas";
                    EDBateriaDominio3.Pk_Id_BateriaDimension = 4;
                    EDBateriaDominio3.ListaDimensiones = LNBateria.ListaDimensiones(4, EDBateriaGestionConsulta.Fk_Id_Bateria);
                    EDBateriaDominio3.FactorTransformacion = 40;

                    ListaDominios.Add(EDBateriaDominio);
                    ListaDominios.Add(EDBateriaDominio1);
                    ListaDominios.Add(EDBateriaDominio2);
                    ListaDominios.Add(EDBateriaDominio3);

                }
                if (EDBateriaGestionConsulta.Fk_Id_Bateria == 1 && itemUs.NumeroIntentos == 1)
                {
                    itemUs.NombreEncuestaTotal = "TOTAL GENERAL FACTORES DE RIESGO PSICOSOCIAL EXTRALABORAL";
                    itemUs.NombreEncuesta = "Cuestionario de Factores de Riesgo Psicosocial Extralaboral";
                    itemUs.FactorTransformacion = 124;
                    EDBateriaDominio EDBateriaDominio = new EDBateriaDominio();
                    EDBateriaDominio.Nombre = "N/A";
                    EDBateriaDominio.Pk_Id_BateriaDimension = 1;
                    EDBateriaDominio.ListaDimensiones = LNBateria.ListaDimensiones(1, 3);
                    EDBateriaDominio.FactorTransformacion = 1;

                    ListaDominios.Add(EDBateriaDominio);
                }
                if (EDBateriaGestionConsulta.Fk_Id_Bateria == 2 && itemUs.NumeroIntentos == 1)
                {
                    itemUs.NombreEncuestaTotal = "TOTAL GENERAL FACTORES DE RIESGO PSICOSOCIAL EXTRALABORAL";
                    itemUs.NombreEncuesta = "Cuestionario de Factores de Riesgo Psicosocial Extralaboral";
                    itemUs.FactorTransformacion = 124;
                    EDBateriaDominio EDBateriaDominio = new EDBateriaDominio();
                    EDBateriaDominio.Nombre = "N/A";
                    EDBateriaDominio.Pk_Id_BateriaDimension = 1;
                    EDBateriaDominio.ListaDimensiones = LNBateria.ListaDimensiones(1, 3);
                    EDBateriaDominio.FactorTransformacion = 1;

                    ListaDominios.Add(EDBateriaDominio);
                }
                if (EDBateriaGestionConsulta.Fk_Id_Bateria == 4)
                {
                    itemUs.NombreEncuestaTotal = "TOTAL GENERAL SÍNTOMAS DE ESTRÉS";
                    itemUs.NombreEncuesta = "Cuestionario de Factores de Estrés";
                    itemUs.FactorTransformacion = 61.16;
                    EDBateriaDominio EDBateriaDominio = new EDBateriaDominio();
                    EDBateriaDominio.Nombre = "N/A";
                    EDBateriaDominio.Pk_Id_BateriaDimension = 1;
                    EDBateriaDominio.ListaDimensiones = LNBateria.ListaDimensiones(1, 4);
                    EDBateriaDominio.FactorTransformacion = 1;

                    ListaDominios.Add(EDBateriaDominio);
                }
                #endregion
                #region ResultadosIntraFormA

                if (EDBateriaGestionConsulta.Fk_Id_Bateria == 1 && itemUs.NumeroIntentos == 0)
                {
                    decimal[,,] evalriesgo = baremosIntraA();
                    decimal[,,] evalriesgoDom = baremosDominioIntraA();
                    foreach (var item in itemUs.ListaResultados)
                    {
                        int iddom = item.DominioInt;
                        int iddim = item.DimensionInt;

                        EDBateriaDominio EDBateriaDominio = ListaDominios.Where(s => s.Pk_Id_BateriaDimension == iddom).FirstOrDefault();
                        List<EDBateriaDimension> Listas = EDBateriaDominio.ListaDimensiones;
                        EDBateriaDimension EDBateriaDimension = Listas.Where(s => s.Pk_Id_BateriaDimension == iddim).FirstOrDefault();

                        EDBateriaDominio.Puntaje += item.ValorResultado;
                        EDBateriaDimension.Puntaje += item.ValorResultado;
                        itemUs.Puntaje += item.ValorResultado;
                    }
                    int cont = 1;
                    int cont1 = 1;
                    foreach (var item in ListaDominios)
                    {
                        item.PuntajeTrans = (item.Puntaje / (decimal)item.FactorTransformacion) * 100;
                        for (int i1 = 1; i1 < 6; i1++)
                        {
                            decimal CotaA = evalriesgoDom[cont1, i1, 1];
                            decimal CotaB = evalriesgoDom[cont1, i1, 2];
                            if (item.PuntajeTrans >= evalriesgoDom[cont1, i1, 1] && item.PuntajeTrans <= evalriesgoDom[cont1, i1, 2])
                            {
                                if (i1 == 1)
                                {
                                    item.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                }
                                if (i1 == 2)
                                {
                                    item.NivelRiesgoDesc = "Riesgo bajo";
                                }
                                if (i1 == 3)
                                {
                                    item.NivelRiesgoDesc = "Riesgo medio";
                                }
                                if (i1 == 4)
                                {
                                    item.NivelRiesgoDesc = "Riesgo alto";
                                }
                                if (i1 == 5)
                                {
                                    item.NivelRiesgoDesc = "Riesgo muy alto";
                                }
                            }
                        }
                        if (item.NivelRiesgoDesc == null)
                        {
                            decimal round = Math.Round(item.PuntajeTrans, 1);
                            for (int i1 = 1; i1 < 6; i1++)
                            {
                                decimal CotaA = evalriesgoDom[cont1, i1, 1];
                                decimal CotaB = evalriesgoDom[cont1, i1, 2];
                                if (round >= CotaA && round <= CotaB)
                                {
                                    if (i1 == 1)
                                    {
                                        item.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                    }
                                    if (i1 == 2)
                                    {
                                        item.NivelRiesgoDesc = "Riesgo bajo";
                                    }
                                    if (i1 == 3)
                                    {
                                        item.NivelRiesgoDesc = "Riesgo medio";
                                    }
                                    if (i1 == 4)
                                    {
                                        item.NivelRiesgoDesc = "Riesgo alto";
                                    }
                                    if (i1 == 5)
                                    {
                                        item.NivelRiesgoDesc = "Riesgo muy alto";
                                    }
                                }
                            }
                        }


                        foreach (var item1 in item.ListaDimensiones)
                        {
                            item1.PuntajeTrans = (item1.Puntaje / (decimal)item1.FactorTransformacion) * 100;

                            for (int i1 = 1; i1 < 6; i1++)
                            {
                                decimal CotaA = evalriesgo[cont, i1, 1];
                                decimal CotaB = evalriesgo[cont, i1, 2];
                                if (item1.PuntajeTrans >= evalriesgo[cont, i1, 1] && item1.PuntajeTrans <= evalriesgo[cont, i1, 2])
                                {
                                    if (i1 == 1)
                                    {
                                        item1.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                    }
                                    if (i1 == 2)
                                    {
                                        item1.NivelRiesgoDesc = "Riesgo bajo";
                                    }
                                    if (i1 == 3)
                                    {
                                        item1.NivelRiesgoDesc = "Riesgo medio";
                                    }
                                    if (i1 == 4)
                                    {
                                        item1.NivelRiesgoDesc = "Riesgo alto";
                                    }
                                    if (i1 == 5)
                                    {
                                        item1.NivelRiesgoDesc = "Riesgo muy alto";
                                    }
                                }
                            }
                            if (item1.NivelRiesgoDesc == null)
                            {
                                decimal round = Math.Round(item1.PuntajeTrans, 1);
                                for (int i1 = 1; i1 < 6; i1++)
                                {
                                    decimal CotaA = evalriesgo[cont, i1, 1];
                                    decimal CotaB = evalriesgo[cont, i1, 2];
                                    if (round >= CotaA && round <= CotaB)
                                    {
                                        if (i1 == 1)
                                        {
                                            item1.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                        }
                                        if (i1 == 2)
                                        {
                                            item1.NivelRiesgoDesc = "Riesgo bajo";
                                        }
                                        if (i1 == 3)
                                        {
                                            item1.NivelRiesgoDesc = "Riesgo medio";
                                        }
                                        if (i1 == 4)
                                        {
                                            item1.NivelRiesgoDesc = "Riesgo alto";
                                        }
                                        if (i1 == 5)
                                        {
                                            item1.NivelRiesgoDesc = "Riesgo muy alto";
                                        }
                                    }
                                }
                            }
                            cont++;
                        }
                        cont1++;
                    }
                    cont1 = 1;
                    itemUs.PuntajeTrans = (itemUs.Puntaje / (decimal)itemUs.FactorTransformacion) * 100;
                    itemUs.Listadominios = new List<EDBateriaDominio>();
                    itemUs.Listadominios = ListaDominios;
                    decimal[,,] evalriesgoTotal = new decimal[10, 10, 10];

                    string formdata = itemUs.TokenPrivado;

                    EDBateriaUsuario EDBateriaUsuarioExtra = LNBateria.ConsultarConvocadoKeyExtra(formdata, 1);
                    if (EDBateriaUsuarioExtra != null)
                    {
                        if (EDBateriaUsuarioExtra.Pk_Id_BateriaUsuario == 0)
                        {
                            evalriesgoTotal = baremosTotalIntraA();
                        }
                        else
                        {
                            if (EDBateriaUsuarioExtra.RegistroOperacion == "Fin")
                            {
                                evalriesgoTotal = baremosTotalExtraIntraA();
                            }
                            else
                            {
                                evalriesgoTotal = baremosTotalIntraA();
                            }
                        }
                    }
                    else
                    {
                        evalriesgoTotal = baremosTotalIntraA();
                    }

                    for (int i1 = 1; i1 < 6; i1++)
                    {
                        decimal CotaA = evalriesgoTotal[cont1, i1, 1];
                        decimal CotaB = evalriesgoTotal[cont1, i1, 2];
                        if (itemUs.PuntajeTrans >= evalriesgoTotal[cont1, i1, 1] && itemUs.PuntajeTrans <= evalriesgoTotal[cont1, i1, 2])
                        {
                            if (i1 == 1)
                            {
                                itemUs.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                            }
                            if (i1 == 2)
                            {
                                itemUs.NivelRiesgoDesc = "Riesgo bajo";
                            }
                            if (i1 == 3)
                            {
                                itemUs.NivelRiesgoDesc = "Riesgo medio";
                            }
                            if (i1 == 4)
                            {
                                itemUs.NivelRiesgoDesc = "Riesgo alto";
                            }
                            if (i1 == 5)
                            {
                                itemUs.NivelRiesgoDesc = "Riesgo muy alto";
                            }
                        }
                    }
                    if (itemUs.NivelRiesgoDesc == null)
                    {
                        decimal round = Math.Round(itemUs.PuntajeTrans, 1);
                        for (int i1 = 1; i1 < 6; i1++)
                        {
                            decimal CotaA = evalriesgoTotal[cont, i1, 1];
                            decimal CotaB = evalriesgoTotal[cont, i1, 2];
                            if (round >= CotaA && round <= CotaB)
                            {
                                if (i1 == 1)
                                {
                                    itemUs.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                }
                                if (i1 == 2)
                                {
                                    itemUs.NivelRiesgoDesc = "Riesgo bajo";
                                }
                                if (i1 == 3)
                                {
                                    itemUs.NivelRiesgoDesc = "Riesgo medio";
                                }
                                if (i1 == 4)
                                {
                                    itemUs.NivelRiesgoDesc = "Riesgo alto";
                                }
                                if (i1 == 5)
                                {
                                    itemUs.NivelRiesgoDesc = "Riesgo muy alto";
                                }
                            }
                        }
                    }

                }
                #endregion
                #region ResultadosIntraFormB


                if (EDBateriaGestionConsulta.Fk_Id_Bateria == 2 && itemUs.NumeroIntentos == 0)
                {
                    decimal[,,] evalriesgo = baremosIntraB();
                    decimal[,,] evalriesgoDom = baremosDominioIntraB();
                    foreach (var item in itemUs.ListaResultados)
                    {
                        int iddom = item.DominioInt;
                        int iddim = item.DimensionInt;

                        EDBateriaDominio EDBateriaDominio = ListaDominios.Where(s => s.Pk_Id_BateriaDimension == iddom).FirstOrDefault();
                        List<EDBateriaDimension> Listas = EDBateriaDominio.ListaDimensiones;
                        EDBateriaDimension EDBateriaDimension = Listas.Where(s => s.Pk_Id_BateriaDimension == iddim).FirstOrDefault();

                        EDBateriaDominio.Puntaje += item.ValorResultado;
                        EDBateriaDimension.Puntaje += item.ValorResultado;
                        itemUs.Puntaje += item.ValorResultado;
                    }
                    int cont = 1;
                    int cont1 = 1;
                    foreach (var item in ListaDominios)
                    {
                        item.PuntajeTrans = (item.Puntaje / (decimal)item.FactorTransformacion) * 100;
                        for (int i1 = 1; i1 < 6; i1++)
                        {
                            decimal CotaA = evalriesgoDom[cont1, i1, 1];
                            decimal CotaB = evalriesgoDom[cont1, i1, 2];
                            if (item.PuntajeTrans >= evalriesgoDom[cont1, i1, 1] && item.PuntajeTrans <= evalriesgoDom[cont1, i1, 2])
                            {
                                if (i1 == 1)
                                {
                                    item.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                }
                                if (i1 == 2)
                                {
                                    item.NivelRiesgoDesc = "Riesgo bajo";
                                }
                                if (i1 == 3)
                                {
                                    item.NivelRiesgoDesc = "Riesgo medio";
                                }
                                if (i1 == 4)
                                {
                                    item.NivelRiesgoDesc = "Riesgo alto";
                                }
                                if (i1 == 5)
                                {
                                    item.NivelRiesgoDesc = "Riesgo muy alto";
                                }
                            }
                        }
                        if (item.NivelRiesgoDesc == null)
                        {
                            decimal round = Math.Round(item.PuntajeTrans, 1);
                            for (int i1 = 1; i1 < 6; i1++)
                            {
                                decimal CotaA = evalriesgoDom[cont1, i1, 1];
                                decimal CotaB = evalriesgoDom[cont1, i1, 2];
                                if (round >= CotaA && round <= CotaB)
                                {
                                    if (i1 == 1)
                                    {
                                        item.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                    }
                                    if (i1 == 2)
                                    {
                                        item.NivelRiesgoDesc = "Riesgo bajo";
                                    }
                                    if (i1 == 3)
                                    {
                                        item.NivelRiesgoDesc = "Riesgo medio";
                                    }
                                    if (i1 == 4)
                                    {
                                        item.NivelRiesgoDesc = "Riesgo alto";
                                    }
                                    if (i1 == 5)
                                    {
                                        item.NivelRiesgoDesc = "Riesgo muy alto";
                                    }
                                }
                            }
                        }


                        foreach (var item1 in item.ListaDimensiones)
                        {
                            item1.PuntajeTrans = (item1.Puntaje / (decimal)item1.FactorTransformacion) * 100;

                            for (int i1 = 1; i1 < 6; i1++)
                            {
                                decimal CotaA = evalriesgo[cont, i1, 1];
                                decimal CotaB = evalriesgo[cont, i1, 2];
                                if (item1.PuntajeTrans >= CotaA && item1.PuntajeTrans <= CotaB)
                                {
                                    if (i1 == 1)
                                    {
                                        item1.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                    }
                                    if (i1 == 2)
                                    {
                                        item1.NivelRiesgoDesc = "Riesgo bajo";
                                    }
                                    if (i1 == 3)
                                    {
                                        item1.NivelRiesgoDesc = "Riesgo medio";
                                    }
                                    if (i1 == 4)
                                    {
                                        item1.NivelRiesgoDesc = "Riesgo alto";
                                    }
                                    if (i1 == 5)
                                    {
                                        item1.NivelRiesgoDesc = "Riesgo muy alto";
                                    }
                                }
                            }
                            if (item1.NivelRiesgoDesc == null)
                            {
                                decimal round = Math.Round(item1.PuntajeTrans, 1);
                                for (int i1 = 1; i1 < 6; i1++)
                                {
                                    decimal CotaA = evalriesgo[cont, i1, 1];
                                    decimal CotaB = evalriesgo[cont, i1, 2];
                                    if (round >= CotaA && round <= CotaB)
                                    {
                                        if (i1 == 1)
                                        {
                                            item1.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                        }
                                        if (i1 == 2)
                                        {
                                            item1.NivelRiesgoDesc = "Riesgo bajo";
                                        }
                                        if (i1 == 3)
                                        {
                                            item1.NivelRiesgoDesc = "Riesgo medio";
                                        }
                                        if (i1 == 4)
                                        {
                                            item1.NivelRiesgoDesc = "Riesgo alto";
                                        }
                                        if (i1 == 5)
                                        {
                                            item1.NivelRiesgoDesc = "Riesgo muy alto";
                                        }
                                    }
                                }
                            }
                            cont++;



                        }
                        cont1++;
                    }
                    cont1 = 1;
                    itemUs.PuntajeTrans = (itemUs.Puntaje / (decimal)itemUs.FactorTransformacion) * 100;
                    itemUs.Listadominios = new List<EDBateriaDominio>();
                    itemUs.Listadominios = ListaDominios;
                    decimal[,,] evalriesgoTotal = new decimal[10, 10, 10];

                    string formdata = itemUs.TokenPrivado;

                    EDBateriaUsuario EDBateriaUsuarioExtra = LNBateria.ConsultarConvocadoKeyExtra(formdata, 1);
                    if (EDBateriaUsuarioExtra != null)
                    {
                        if (EDBateriaUsuarioExtra.Pk_Id_BateriaUsuario == 0)
                        {
                            evalriesgoTotal = baremosTotalIntraB();
                        }
                        else
                        {
                            if (EDBateriaUsuarioExtra.RegistroOperacion == "Fin")
                            {
                                evalriesgoTotal = baremosTotalExtraIntraB();
                            }
                            else
                            {
                                evalriesgoTotal = baremosTotalIntraB();
                            }
                        }
                    }
                    else
                    {
                        evalriesgoTotal = baremosTotalIntraB();
                    }

                    for (int i1 = 1; i1 < 6; i1++)
                    {
                        decimal CotaA = evalriesgoTotal[cont1, i1, 1];
                        decimal CotaB = evalriesgoTotal[cont1, i1, 2];
                        if (itemUs.PuntajeTrans >= evalriesgoTotal[cont1, i1, 1] && itemUs.PuntajeTrans <= evalriesgoTotal[cont1, i1, 2])
                        {
                            if (i1 == 1)
                            {
                                itemUs.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                            }
                            if (i1 == 2)
                            {
                                itemUs.NivelRiesgoDesc = "Riesgo bajo";
                            }
                            if (i1 == 3)
                            {
                                itemUs.NivelRiesgoDesc = "Riesgo medio";
                            }
                            if (i1 == 4)
                            {
                                itemUs.NivelRiesgoDesc = "Riesgo alto";
                            }
                            if (i1 == 5)
                            {
                                itemUs.NivelRiesgoDesc = "Riesgo muy alto";
                            }
                        }
                    }
                    if (itemUs.NivelRiesgoDesc == null)
                    {
                        decimal round = Math.Round(itemUs.PuntajeTrans, 1);
                        for (int i1 = 1; i1 < 6; i1++)
                        {
                            decimal CotaA = evalriesgoTotal[cont, i1, 1];
                            decimal CotaB = evalriesgoTotal[cont, i1, 2];
                            if (round >= CotaA && round <= CotaB)
                            {
                                if (i1 == 1)
                                {
                                    itemUs.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                }
                                if (i1 == 2)
                                {
                                    itemUs.NivelRiesgoDesc = "Riesgo bajo";
                                }
                                if (i1 == 3)
                                {
                                    itemUs.NivelRiesgoDesc = "Riesgo medio";
                                }
                                if (i1 == 4)
                                {
                                    itemUs.NivelRiesgoDesc = "Riesgo alto";
                                }
                                if (i1 == 5)
                                {
                                    itemUs.NivelRiesgoDesc = "Riesgo muy alto";
                                }
                            }
                        }
                    }

                }
                #endregion
                #region ResultadosextraFormA


                if (EDBateriaGestionConsulta.Fk_Id_Bateria == 1 && itemUs.NumeroIntentos == 1)
                {
                    int tipo = 0;
                    decimal[,,] evalriesgo = new decimal[10, 10, 10];
                    decimal[,,] evalriesgoDom = new decimal[10, 10, 10];
                    if (itemUs.BateriaInicial.TipoCargo == "Jefatura - tiene personal a cargo" || itemUs.BateriaInicial.TipoCargo == "Profesional, analista, técnico, tecnólogo")
                    {
                        tipo = 1;
                        evalriesgo = baremosExtra1();
                        evalriesgoDom = baremosExtra1();
                    }
                    if (itemUs.BateriaInicial.TipoCargo == "Auxiliar, asistente administrativo, asistente técnico" || itemUs.BateriaInicial.TipoCargo == "Operario, operador, ayudante, servicios generales")
                    {
                        tipo = 2;
                        evalriesgo = baremosExtra2();
                        evalriesgoDom = baremosExtra2();
                    }
                    foreach (var item in itemUs.ListaResultados)
                    {
                        int iddom = item.DominioInt;
                        int iddim = item.DimensionInt;

                        EDBateriaDominio EDBateriaDominio = ListaDominios.Where(s => s.Pk_Id_BateriaDimension == iddom).FirstOrDefault();
                        List<EDBateriaDimension> Listas = EDBateriaDominio.ListaDimensiones;
                        EDBateriaDimension EDBateriaDimension = Listas.Where(s => s.Pk_Id_BateriaDimension == iddim).FirstOrDefault();

                        EDBateriaDominio.Puntaje += item.ValorResultado;
                        EDBateriaDimension.Puntaje += item.ValorResultado;
                        itemUs.Puntaje += item.ValorResultado;
                    }
                    int cont = 1;
                    int cont1 = 1;
                    foreach (var item in ListaDominios)
                    {
                        item.PuntajeTrans = (item.Puntaje / (decimal)item.FactorTransformacion) * 100;
                        for (int i1 = 1; i1 < 6; i1++)
                        {
                            decimal CotaA = evalriesgoDom[cont1, i1, 1];
                            decimal CotaB = evalriesgoDom[cont1, i1, 2];
                            if (item.PuntajeTrans >= evalriesgoDom[cont1, i1, 1] && item.PuntajeTrans <= evalriesgoDom[cont1, i1, 2])
                            {
                                if (i1 == 1)
                                {
                                    item.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                }
                                if (i1 == 2)
                                {
                                    item.NivelRiesgoDesc = "Riesgo bajo";
                                }
                                if (i1 == 3)
                                {
                                    item.NivelRiesgoDesc = "Riesgo medio";
                                }
                                if (i1 == 4)
                                {
                                    item.NivelRiesgoDesc = "Riesgo alto";
                                }
                                if (i1 == 5)
                                {
                                    item.NivelRiesgoDesc = "Riesgo muy alto";
                                }
                            }
                        }
                        if (item.NivelRiesgoDesc == null)
                        {
                            decimal round = Math.Round(item.PuntajeTrans, 1);
                            for (int i1 = 1; i1 < 6; i1++)
                            {
                                decimal CotaA = evalriesgoDom[cont1, i1, 1];
                                decimal CotaB = evalriesgoDom[cont1, i1, 2];
                                if (round >= CotaA && round <= CotaB)
                                {
                                    if (i1 == 1)
                                    {
                                        item.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                    }
                                    if (i1 == 2)
                                    {
                                        item.NivelRiesgoDesc = "Riesgo bajo";
                                    }
                                    if (i1 == 3)
                                    {
                                        item.NivelRiesgoDesc = "Riesgo medio";
                                    }
                                    if (i1 == 4)
                                    {
                                        item.NivelRiesgoDesc = "Riesgo alto";
                                    }
                                    if (i1 == 5)
                                    {
                                        item.NivelRiesgoDesc = "Riesgo muy alto";
                                    }
                                }
                            }
                        }



                        foreach (var item1 in item.ListaDimensiones)
                        {
                            item1.PuntajeTrans = (item1.Puntaje / (decimal)item1.FactorTransformacion) * 100;

                            for (int i1 = 1; i1 < 6; i1++)
                            {
                                decimal CotaA = evalriesgo[cont, i1, 1];
                                decimal CotaB = evalriesgo[cont, i1, 2];
                                if (item1.PuntajeTrans >= evalriesgo[cont, i1, 1] && item1.PuntajeTrans <= evalriesgo[cont, i1, 2])
                                {
                                    if (i1 == 1)
                                    {
                                        item1.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                    }
                                    if (i1 == 2)
                                    {
                                        item1.NivelRiesgoDesc = "Riesgo bajo";
                                    }
                                    if (i1 == 3)
                                    {
                                        item1.NivelRiesgoDesc = "Riesgo medio";
                                    }
                                    if (i1 == 4)
                                    {
                                        item1.NivelRiesgoDesc = "Riesgo alto";
                                    }
                                    if (i1 == 5)
                                    {
                                        item1.NivelRiesgoDesc = "Riesgo muy alto";
                                    }
                                }
                            }
                            if (item1.NivelRiesgoDesc == null)
                            {
                                decimal round = Math.Round(item1.PuntajeTrans, 1);
                                for (int i1 = 1; i1 < 6; i1++)
                                {
                                    decimal CotaA = evalriesgo[cont, i1, 1];
                                    decimal CotaB = evalriesgo[cont, i1, 2];
                                    if (round >= CotaA && round <= CotaB)
                                    {
                                        if (i1 == 1)
                                        {
                                            item1.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                        }
                                        if (i1 == 2)
                                        {
                                            item1.NivelRiesgoDesc = "Riesgo bajo";
                                        }
                                        if (i1 == 3)
                                        {
                                            item1.NivelRiesgoDesc = "Riesgo medio";
                                        }
                                        if (i1 == 4)
                                        {
                                            item1.NivelRiesgoDesc = "Riesgo alto";
                                        }
                                        if (i1 == 5)
                                        {
                                            item1.NivelRiesgoDesc = "Riesgo muy alto";
                                        }
                                    }
                                }
                            }
                            cont++;
                        }
                        cont1++;
                    }
                    cont1 = 1;
                    itemUs.PuntajeTrans = (itemUs.Puntaje / (decimal)itemUs.FactorTransformacion) * 100;
                    itemUs.Listadominios = new List<EDBateriaDominio>();
                    itemUs.Listadominios = ListaDominios;
                    decimal[,,] evalriesgoTotal = new decimal[10, 10, 10];

                    evalriesgoTotal = baremosTotalExtraIntraA();

                    for (int i1 = 1; i1 < 6; i1++)
                    {
                        decimal CotaA = evalriesgoTotal[cont1, i1, 1];
                        decimal CotaB = evalriesgoTotal[cont1, i1, 2];
                        if (itemUs.PuntajeTrans >= evalriesgoTotal[cont1, i1, 1] && itemUs.PuntajeTrans <= evalriesgoTotal[cont1, i1, 2])
                        {
                            if (i1 == 1)
                            {
                                itemUs.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                            }
                            if (i1 == 2)
                            {
                                itemUs.NivelRiesgoDesc = "Riesgo bajo";
                            }
                            if (i1 == 3)
                            {
                                itemUs.NivelRiesgoDesc = "Riesgo medio";
                            }
                            if (i1 == 4)
                            {
                                itemUs.NivelRiesgoDesc = "Riesgo alto";
                            }
                            if (i1 == 5)
                            {
                                itemUs.NivelRiesgoDesc = "Riesgo muy alto";
                            }
                        }
                    }
                    if (itemUs.NivelRiesgoDesc == null)
                    {
                        decimal round = Math.Round(itemUs.PuntajeTrans, 1);
                        for (int i1 = 1; i1 < 6; i1++)
                        {
                            decimal CotaA = evalriesgoTotal[cont, i1, 1];
                            decimal CotaB = evalriesgoTotal[cont, i1, 2];
                            if (round >= CotaA && round <= CotaB)
                            {
                                if (i1 == 1)
                                {
                                    itemUs.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                }
                                if (i1 == 2)
                                {
                                    itemUs.NivelRiesgoDesc = "Riesgo bajo";
                                }
                                if (i1 == 3)
                                {
                                    itemUs.NivelRiesgoDesc = "Riesgo medio";
                                }
                                if (i1 == 4)
                                {
                                    itemUs.NivelRiesgoDesc = "Riesgo alto";
                                }
                                if (i1 == 5)
                                {
                                    itemUs.NivelRiesgoDesc = "Riesgo muy alto";
                                }
                            }
                        }
                    }

                }
                #endregion
                #region ResultadosextraFormB


                if (EDBateriaGestionConsulta.Fk_Id_Bateria == 2 && itemUs.NumeroIntentos == 1)
                {
                    int tipo = 0;
                    decimal[,,] evalriesgo = new decimal[10, 10, 10];
                    decimal[,,] evalriesgoDom = new decimal[10, 10, 10];
                    if (itemUs.BateriaInicial.TipoCargo == "Jefatura - tiene personal a cargo" || itemUs.BateriaInicial.TipoCargo == "Profesional, analista, técnico, tecnólogo")
                    {
                        tipo = 1;
                        evalriesgo = baremosExtra1();
                        evalriesgoDom = baremosExtra1();
                    }
                    if (itemUs.BateriaInicial.TipoCargo == "Auxiliar, asistente administrativo, asistente técnico" || itemUs.BateriaInicial.TipoCargo == "Operario, operador, ayudante, servicios generales")
                    {
                        tipo = 2;
                        evalriesgo = baremosExtra2();
                        evalriesgoDom = baremosExtra2();
                    }
                    foreach (var item in itemUs.ListaResultados)
                    {
                        int iddom = item.DominioInt;
                        int iddim = item.DimensionInt;

                        EDBateriaDominio EDBateriaDominio = ListaDominios.Where(s => s.Pk_Id_BateriaDimension == iddom).FirstOrDefault();
                        List<EDBateriaDimension> Listas = EDBateriaDominio.ListaDimensiones;
                        EDBateriaDimension EDBateriaDimension = Listas.Where(s => s.Pk_Id_BateriaDimension == iddim).FirstOrDefault();

                        EDBateriaDominio.Puntaje += item.ValorResultado;
                        EDBateriaDimension.Puntaje += item.ValorResultado;
                        itemUs.Puntaje += item.ValorResultado;
                    }
                    int cont = 1;
                    int cont1 = 1;
                    foreach (var item in ListaDominios)
                    {
                        item.PuntajeTrans = (item.Puntaje / (decimal)item.FactorTransformacion) * 100;
                        for (int i1 = 1; i1 < 6; i1++)
                        {
                            decimal CotaA = evalriesgoDom[cont1, i1, 1];
                            decimal CotaB = evalriesgoDom[cont1, i1, 2];
                            if (item.PuntajeTrans >= evalriesgoDom[cont1, i1, 1] && item.PuntajeTrans <= evalriesgoDom[cont1, i1, 2])
                            {
                                if (i1 == 1)
                                {
                                    item.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                }
                                if (i1 == 2)
                                {
                                    item.NivelRiesgoDesc = "Riesgo bajo";
                                }
                                if (i1 == 3)
                                {
                                    item.NivelRiesgoDesc = "Riesgo medio";
                                }
                                if (i1 == 4)
                                {
                                    item.NivelRiesgoDesc = "Riesgo alto";
                                }
                                if (i1 == 5)
                                {
                                    item.NivelRiesgoDesc = "Riesgo muy alto";
                                }
                            }
                        }
                        if (item.NivelRiesgoDesc == null)
                        {
                            decimal round = Math.Round(item.PuntajeTrans, 1);
                            for (int i1 = 1; i1 < 6; i1++)
                            {
                                decimal CotaA = evalriesgoDom[cont1, i1, 1];
                                decimal CotaB = evalriesgoDom[cont1, i1, 2];
                                if (round >= CotaA && round <= CotaB)
                                {
                                    if (i1 == 1)
                                    {
                                        item.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                    }
                                    if (i1 == 2)
                                    {
                                        item.NivelRiesgoDesc = "Riesgo bajo";
                                    }
                                    if (i1 == 3)
                                    {
                                        item.NivelRiesgoDesc = "Riesgo medio";
                                    }
                                    if (i1 == 4)
                                    {
                                        item.NivelRiesgoDesc = "Riesgo alto";
                                    }
                                    if (i1 == 5)
                                    {
                                        item.NivelRiesgoDesc = "Riesgo muy alto";
                                    }
                                }
                            }
                        }



                        foreach (var item1 in item.ListaDimensiones)
                        {
                            item1.PuntajeTrans = (item1.Puntaje / (decimal)item1.FactorTransformacion) * 100;

                            for (int i1 = 1; i1 < 6; i1++)
                            {
                                decimal CotaA = evalriesgo[cont, i1, 1];
                                decimal CotaB = evalriesgo[cont, i1, 2];
                                if (item1.PuntajeTrans >= evalriesgo[cont, i1, 1] && item1.PuntajeTrans <= evalriesgo[cont, i1, 2])
                                {
                                    if (i1 == 1)
                                    {
                                        item1.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                    }
                                    if (i1 == 2)
                                    {
                                        item1.NivelRiesgoDesc = "Riesgo bajo";
                                    }
                                    if (i1 == 3)
                                    {
                                        item1.NivelRiesgoDesc = "Riesgo medio";
                                    }
                                    if (i1 == 4)
                                    {
                                        item1.NivelRiesgoDesc = "Riesgo alto";
                                    }
                                    if (i1 == 5)
                                    {
                                        item1.NivelRiesgoDesc = "Riesgo muy alto";
                                    }
                                }
                            }
                            if (item1.NivelRiesgoDesc == null)
                            {
                                decimal round = Math.Round(item1.PuntajeTrans, 1);
                                for (int i1 = 1; i1 < 6; i1++)
                                {
                                    decimal CotaA = evalriesgo[cont, i1, 1];
                                    decimal CotaB = evalriesgo[cont, i1, 2];
                                    if (round >= CotaA && round <= CotaB)
                                    {
                                        if (i1 == 1)
                                        {
                                            item1.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                        }
                                        if (i1 == 2)
                                        {
                                            item1.NivelRiesgoDesc = "Riesgo bajo";
                                        }
                                        if (i1 == 3)
                                        {
                                            item1.NivelRiesgoDesc = "Riesgo medio";
                                        }
                                        if (i1 == 4)
                                        {
                                            item1.NivelRiesgoDesc = "Riesgo alto";
                                        }
                                        if (i1 == 5)
                                        {
                                            item1.NivelRiesgoDesc = "Riesgo muy alto";
                                        }
                                    }
                                }
                            }
                            cont++;
                        }
                        cont1++;
                    }
                    cont1 = 1;
                    itemUs.PuntajeTrans = (itemUs.Puntaje / (decimal)itemUs.FactorTransformacion) * 100;
                    itemUs.Listadominios = new List<EDBateriaDominio>();
                    itemUs.Listadominios = ListaDominios;
                    decimal[,,] evalriesgoTotal = new decimal[10, 10, 10];

                    evalriesgoTotal = baremosTotalExtraIntraB();

                    for (int i1 = 1; i1 < 6; i1++)
                    {
                        decimal CotaA = evalriesgoTotal[cont1, i1, 1];
                        decimal CotaB = evalriesgoTotal[cont1, i1, 2];
                        if (itemUs.PuntajeTrans >= evalriesgoTotal[cont1, i1, 1] && itemUs.PuntajeTrans <= evalriesgoTotal[cont1, i1, 2])
                        {
                            if (i1 == 1)
                            {
                                itemUs.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                            }
                            if (i1 == 2)
                            {
                                itemUs.NivelRiesgoDesc = "Riesgo bajo";
                            }
                            if (i1 == 3)
                            {
                                itemUs.NivelRiesgoDesc = "Riesgo medio";
                            }
                            if (i1 == 4)
                            {
                                itemUs.NivelRiesgoDesc = "Riesgo alto";
                            }
                            if (i1 == 5)
                            {
                                itemUs.NivelRiesgoDesc = "Riesgo muy alto";
                            }
                        }
                    }
                    if (itemUs.NivelRiesgoDesc == null)
                    {
                        decimal round = Math.Round(itemUs.PuntajeTrans, 1);
                        for (int i1 = 1; i1 < 6; i1++)
                        {
                            decimal CotaA = evalriesgoTotal[cont, i1, 1];
                            decimal CotaB = evalriesgoTotal[cont, i1, 2];
                            if (round >= CotaA && round <= CotaB)
                            {
                                if (i1 == 1)
                                {
                                    itemUs.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                }
                                if (i1 == 2)
                                {
                                    itemUs.NivelRiesgoDesc = "Riesgo bajo";
                                }
                                if (i1 == 3)
                                {
                                    itemUs.NivelRiesgoDesc = "Riesgo medio";
                                }
                                if (i1 == 4)
                                {
                                    itemUs.NivelRiesgoDesc = "Riesgo alto";
                                }
                                if (i1 == 5)
                                {
                                    itemUs.NivelRiesgoDesc = "Riesgo muy alto";
                                }
                            }
                        }
                    }
                }
                #endregion
                #region ResultadosEstres


                if (EDBateriaGestionConsulta.Fk_Id_Bateria == 4)
                {

                    decimal[,,] evalriesgo = new decimal[10, 10, 10];
                    if (itemUs.BateriaInicial.TipoCargo == "Jefatura - tiene personal a cargo" || itemUs.BateriaInicial.TipoCargo == "Profesional, analista, técnico, tecnólogo")
                    {
                        evalriesgo = baremosEstres1();
                    }
                    if (itemUs.BateriaInicial.TipoCargo == "Auxiliar, asistente administrativo, asistente técnico" || itemUs.BateriaInicial.TipoCargo == "Operario, operador, ayudante, servicios generales")
                    {
                        evalriesgo = baremosEstres2();
                    }
                    decimal promedio1 = 0;
                    decimal promedio2 = 0;
                    decimal promedio3 = 0;
                    decimal promedio4 = 0;
                    int contp1 = 0;
                    int contp2 = 0;
                    int contp3 = 0;
                    int contp4 = 0;

                    int comienza = itemUs.ListaResultados.First().Fk_Id_BateriaCuestionario;
                    foreach (var item in itemUs.ListaResultados)
                    {
                        //int iddom = item.DominioInt;
                        //int iddim = item.DimensionInt;

                        //EDBateriaDominio EDBateriaDominio = ListaDominios.Where(s => s.Pk_Id_BateriaDimension == iddom).FirstOrDefault();
                        //List<EDBateriaDimension> Listas = EDBateriaDominio.ListaDimensiones;
                        //EDBateriaDimension EDBateriaDimension = Listas.Where(s => s.Pk_Id_BateriaDimension == iddim).FirstOrDefault();

                        //EDBateriaDominio.Puntaje += item.ValorResultado;
                        //EDBateriaDimension.Puntaje += item.ValorResultado;
                        //EDBateriaUsuario.Puntaje += item.ValorResultado;

                        if (item.Fk_Id_BateriaCuestionario >= comienza && item.Fk_Id_BateriaCuestionario <= comienza + 7)
                        {
                            promedio1 += item.ValorResultado;
                            contp1++;
                        }
                        if (item.Fk_Id_BateriaCuestionario >= comienza + 8 && item.Fk_Id_BateriaCuestionario <= comienza + 11)
                        {
                            promedio2 += item.ValorResultado;
                            contp2++;
                        }
                        if (item.Fk_Id_BateriaCuestionario >= comienza + 12 && item.Fk_Id_BateriaCuestionario <= comienza + 21)
                        {
                            promedio3 += item.ValorResultado;
                            contp3++;
                        }
                        if (item.Fk_Id_BateriaCuestionario >= comienza + 22 && item.Fk_Id_BateriaCuestionario <= comienza + 30)
                        {
                            promedio4 += item.ValorResultado;
                            contp4++;
                        }
                    }
                    promedio1 = promedio1 / contp1;
                    promedio2 = promedio2 / contp2;
                    promedio3 = promedio3 / contp3;
                    promedio4 = promedio4 / contp4;

                    promedio1 = promedio1 * 4;
                    promedio2 = promedio2 * 3;
                    promedio3 = promedio3 * 2;

                    decimal puntajetotal = promedio1 + promedio2 + promedio3 + promedio4;
                    itemUs.Puntaje = puntajetotal;
                    int cont1 = 1;
                    itemUs.PuntajeTrans = (itemUs.Puntaje / (decimal)itemUs.FactorTransformacion) * 100;
                    itemUs.Listadominios = new List<EDBateriaDominio>();
                    itemUs.Listadominios = ListaDominios;
                    decimal[,,] evalriesgoTotal = new decimal[10, 10, 10];
                    evalriesgoTotal = evalriesgo;

                    for (int i1 = 1; i1 < 6; i1++)
                    {
                        decimal CotaA = evalriesgoTotal[cont1, i1, 1];
                        decimal CotaB = evalriesgoTotal[cont1, i1, 2];
                        if (itemUs.PuntajeTrans >= evalriesgoTotal[cont1, i1, 1] && itemUs.PuntajeTrans <= evalriesgoTotal[cont1, i1, 2])
                        {
                            if (i1 == 1)
                            {
                                itemUs.NivelRiesgoDesc = "Muy bajo";
                            }
                            if (i1 == 2)
                            {
                                itemUs.NivelRiesgoDesc = "Bajo";
                            }
                            if (i1 == 3)
                            {
                                itemUs.NivelRiesgoDesc = "Medio";
                            }
                            if (i1 == 4)
                            {
                                itemUs.NivelRiesgoDesc = "Alto";
                            }
                            if (i1 == 5)
                            {
                                itemUs.NivelRiesgoDesc = "Muy alto";
                            }
                        }
                    }
                    if (itemUs.NivelRiesgoDesc == null)
                    {
                        decimal round = Math.Round(itemUs.PuntajeTrans, 1);
                        for (int i1 = 1; i1 < 6; i1++)
                        {
                            decimal CotaA = evalriesgoTotal[cont1, i1, 1];
                            decimal CotaB = evalriesgoTotal[cont1, i1, 2];
                            if (itemUs.PuntajeTrans >= evalriesgoTotal[cont1, i1, 1] && itemUs.PuntajeTrans <= evalriesgoTotal[cont1, i1, 2])
                            {
                                if (i1 == 1)
                                {
                                    itemUs.NivelRiesgoDesc = "Muy bajo";
                                }
                                if (i1 == 2)
                                {
                                    itemUs.NivelRiesgoDesc = "Bajo";
                                }
                                if (i1 == 3)
                                {
                                    itemUs.NivelRiesgoDesc = "Medio";
                                }
                                if (i1 == 4)
                                {
                                    itemUs.NivelRiesgoDesc = "Alto";
                                }
                                if (i1 == 5)
                                {
                                    itemUs.NivelRiesgoDesc = "Muy alto";
                                }
                            }
                        }
                    }

                }
                #endregion
                #endregion
            }
            EDBateriaGestionConsulta.Informe = new EDBateriaUsuario();
            #region Dominios&Dimensiones

            List<EDBateriaDominio> ListaDominios1 = new List<EDBateriaDominio>();
            if (EDBateriaGestionConsulta.Fk_Id_Bateria == 1 && form == 0)
            {
                ViewBag.NombreBat = "INTRALABORAL FORMA A";
                EDBateriaGestionConsulta.Informe.NombreEncuestaTotal = "TOTAL GENERAL FACTORES DE RIESGO PSICOSOCIAL INTRALABORAL";
                EDBateriaGestionConsulta.Informe.NombreEncuesta = "Cuestionario de Factores de Riesgo Psicosocial Intralaboral FORMA A";
                EDBateriaGestionConsulta.Informe.FactorTransformacion = 492;
                EDBateriaDominio EDBateriaDominio = new EDBateriaDominio();
                EDBateriaDominio.Nombre = "Liderazgo y relaciones sociales en el trabajo";
                EDBateriaDominio.Pk_Id_BateriaDimension = 1;
                EDBateriaDominio.ListaDimensiones = LNBateria.ListaDimensiones(1, EDBateriaGestionConsulta.Fk_Id_Bateria);
                EDBateriaDominio.FactorTransformacion = 164;

                EDBateriaDominio EDBateriaDominio1 = new EDBateriaDominio();
                EDBateriaDominio1.Nombre = "Control sobre el trabajo";
                EDBateriaDominio1.Pk_Id_BateriaDimension = 2;
                EDBateriaDominio1.ListaDimensiones = LNBateria.ListaDimensiones(2, EDBateriaGestionConsulta.Fk_Id_Bateria);
                EDBateriaDominio1.FactorTransformacion = 84;

                EDBateriaDominio EDBateriaDominio2 = new EDBateriaDominio();
                EDBateriaDominio2.Nombre = "Demandas del trabajo";
                EDBateriaDominio2.Pk_Id_BateriaDimension = 3;
                EDBateriaDominio2.ListaDimensiones = LNBateria.ListaDimensiones(3, EDBateriaGestionConsulta.Fk_Id_Bateria);
                EDBateriaDominio2.FactorTransformacion = 200;

                EDBateriaDominio EDBateriaDominio3 = new EDBateriaDominio();
                EDBateriaDominio3.Nombre = "Recompensas";
                EDBateriaDominio3.Pk_Id_BateriaDimension = 4;
                EDBateriaDominio3.ListaDimensiones = LNBateria.ListaDimensiones(4, EDBateriaGestionConsulta.Fk_Id_Bateria);
                EDBateriaDominio3.FactorTransformacion = 44;

                ListaDominios1.Add(EDBateriaDominio);
                ListaDominios1.Add(EDBateriaDominio1);
                ListaDominios1.Add(EDBateriaDominio2);
                ListaDominios1.Add(EDBateriaDominio3);
            }
            if (EDBateriaGestionConsulta.Fk_Id_Bateria == 2 && form == 0)
            {
                ViewBag.NombreBat = "INTRALABORAL FORMA B";
                EDBateriaGestionConsulta.Informe.NombreEncuestaTotal = "TOTAL GENERAL FACTORES DE RIESGO PSICOSOCIAL INTRALABORAL";
                EDBateriaGestionConsulta.Informe.NombreEncuesta = "Cuestionario de Factores de Riesgo Psicosocial Intralaboral FORMA B";
                EDBateriaGestionConsulta.Informe.FactorTransformacion = 388;
                EDBateriaDominio EDBateriaDominio = new EDBateriaDominio();
                EDBateriaDominio.Nombre = "Liderazgo y relaciones sociales en el trabajo";
                EDBateriaDominio.Pk_Id_BateriaDimension = 1;
                EDBateriaDominio.ListaDimensiones = LNBateria.ListaDimensiones(1, EDBateriaGestionConsulta.Fk_Id_Bateria);
                EDBateriaDominio.FactorTransformacion = 120;

                EDBateriaDominio EDBateriaDominio1 = new EDBateriaDominio();
                EDBateriaDominio1.Nombre = "Control sobre el trabajo";
                EDBateriaDominio1.Pk_Id_BateriaDimension = 2;
                EDBateriaDominio1.ListaDimensiones = LNBateria.ListaDimensiones(2, EDBateriaGestionConsulta.Fk_Id_Bateria);
                EDBateriaDominio1.FactorTransformacion = 72;

                EDBateriaDominio EDBateriaDominio2 = new EDBateriaDominio();
                EDBateriaDominio2.Nombre = "Demandas del trabajo";
                EDBateriaDominio2.Pk_Id_BateriaDimension = 3;
                EDBateriaDominio2.ListaDimensiones = LNBateria.ListaDimensiones(3, EDBateriaGestionConsulta.Fk_Id_Bateria);
                EDBateriaDominio2.FactorTransformacion = 156;

                EDBateriaDominio EDBateriaDominio3 = new EDBateriaDominio();
                EDBateriaDominio3.Nombre = "Recompensas";
                EDBateriaDominio3.Pk_Id_BateriaDimension = 4;
                EDBateriaDominio3.ListaDimensiones = LNBateria.ListaDimensiones(4, EDBateriaGestionConsulta.Fk_Id_Bateria);
                EDBateriaDominio3.FactorTransformacion = 40;

                ListaDominios1.Add(EDBateriaDominio);
                ListaDominios1.Add(EDBateriaDominio1);
                ListaDominios1.Add(EDBateriaDominio2);
                ListaDominios1.Add(EDBateriaDominio3);

            }
            if (EDBateriaGestionConsulta.Fk_Id_Bateria == 1 && form == 1)
            {
                ViewBag.NombreBat = "EXTRALABORAL";
                EDBateriaGestionConsulta.Informe.NombreEncuestaTotal = "TOTAL GENERAL FACTORES DE RIESGO PSICOSOCIAL EXTRALABORAL";
                EDBateriaGestionConsulta.Informe.NombreEncuesta = "Cuestionario de Factores de Riesgo Psicosocial Extralaboral";
                EDBateriaGestionConsulta.Informe.FactorTransformacion = 124;
                EDBateriaDominio EDBateriaDominio = new EDBateriaDominio();
                EDBateriaDominio.Nombre = "N/A";
                EDBateriaDominio.Pk_Id_BateriaDimension = 1;
                EDBateriaDominio.ListaDimensiones = LNBateria.ListaDimensiones(1, 3);
                EDBateriaDominio.FactorTransformacion = 1;

                ListaDominios1.Add(EDBateriaDominio);
            }
            if (EDBateriaGestionConsulta.Fk_Id_Bateria == 2 && form == 1)
            {
                ViewBag.NombreBat = "EXTRALABORAL";
                EDBateriaGestionConsulta.Informe.NombreEncuestaTotal = "TOTAL GENERAL FACTORES DE RIESGO PSICOSOCIAL EXTRALABORAL";
                EDBateriaGestionConsulta.Informe.NombreEncuesta = "Cuestionario de Factores de Riesgo Psicosocial Extralaboral";
                EDBateriaGestionConsulta.Informe.FactorTransformacion = 124;
                EDBateriaDominio EDBateriaDominio = new EDBateriaDominio();
                EDBateriaDominio.Nombre = "N/A";
                EDBateriaDominio.Pk_Id_BateriaDimension = 1;
                EDBateriaDominio.ListaDimensiones = LNBateria.ListaDimensiones(1, 3);
                EDBateriaDominio.FactorTransformacion = 1;

                ListaDominios1.Add(EDBateriaDominio);
            }
            if (EDBateriaGestionConsulta.Fk_Id_Bateria == 4)
            {
                ViewBag.NombreBat = "FACTORES DE ESTRÉS";
                EDBateriaGestionConsulta.Informe.NombreEncuestaTotal = "TOTAL GENERAL SÍNTOMAS DE ESTRÉS";
                EDBateriaGestionConsulta.Informe.NombreEncuesta = "Cuestionario de Factores de Estrés";
                EDBateriaGestionConsulta.Informe.FactorTransformacion = 61.16;
                EDBateriaDominio EDBateriaDominio = new EDBateriaDominio();
                EDBateriaDominio.Nombre = "N/A";
                EDBateriaDominio.Pk_Id_BateriaDimension = 1;
                EDBateriaDominio.ListaDimensiones = LNBateria.ListaDimensiones(1, 4);
                EDBateriaDominio.FactorTransformacion = 1;

                ListaDominios1.Add(EDBateriaDominio);
            }
            #endregion
            EDBateriaGestionConsulta.Informe.Listadominios = ListaDominios1;
            if (contTer!=0)
            {
                foreach (var item in EDBateriaGestionConsulta.Informe.Listadominios)
                {
                    int IdDominio = item.Pk_Id_BateriaDimension;
                    foreach (var item1 in EDBateriaGestionConsulta.ListaUsuarios)
                    {
                        EDBateriaDimension EDBateriaDimension = new EDBateriaDimension();
                        var dominio = item1.Listadominios.Where(s => s.Pk_Id_BateriaDimension == IdDominio).FirstOrDefault();
                        if (dominio != null)
                        {
                            //grabar factor transformado
                            item.PuntajeTrans += dominio.PuntajeTrans / contTer;
                            var dimensiones = dominio.ListaDimensiones.ToList();
                            foreach (var item2 in dimensiones)
                            {
                                int Iddim = item2.Pk_Id_BateriaDimension;
                                var dimension = item.ListaDimensiones.Where(s => s.Pk_Id_BateriaDimension == Iddim).FirstOrDefault();
                                if (dimension != null)
                                {
                                    dimension.PuntajeTrans += item2.PuntajeTrans / contTer;
                                }
                            }
                        }
                    }
                }
            }
            foreach (var item in EDBateriaGestionConsulta.ListaUsuarios)
            {
                EDBateriaGestionConsulta.Informe.PuntajeTrans+=item.PuntajeTrans / contTer;
            }


            EDBateriaGestion = EDBateriaGestionConsulta;
            if (form==0)
            {
                ViewBag.form = "1";
            }
            if (form == 1)
            {
                ViewBag.form = "0";
            }
            ViewBag.bateriaextra = false;
            if (EDBateriaGestion.bateriaExtra==3)
            {
                ViewBag.bateriaextra = true;
            }

            return View(EDBateriaGestion);
        }

        #region Excel
        [HttpPost]
        public ActionResult ResultadoExcel(string IdGestion, int form)
        {
            string nombrePrincipal = "";
            ViewBag.Nombre = "";
            ViewBag.NombreBat = "";
            ViewBag.link = "";
            ViewBag.nit = "";
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }
            ViewBag.nit = usuarioActual.NitEmpresa;
            EDBateriaGestion EDBateriaGestion = new EDBateriaGestion();
            EDBateriaGestion EDBateriaGestionConsulta = new EDBateriaGestion();
            List<EDBateria> ListaBaterias = LNBateria.ConsultarBaterias();


            //Consulta Gestion
            int IdGestionInt = 0;
            if (int.TryParse(IdGestion, out IdGestionInt))
            {
                EDBateriaGestionConsulta = LNBateria.ConsultarGestion(IdGestionInt, usuarioActual.IdEmpresa);
                ViewBag.Nombre = EDBateriaGestionConsulta.NombreBateria.Replace("Estres", "Estrés");
            }
            //Consulta Usuarios terminados
            List<EDBateriaUsuario> ListaUsuariosTerminados = new List<EDBateriaUsuario>();
            List<EDBateriaUsuario> ListaUsuariosTerminados1 = new List<EDBateriaUsuario>();
            int usuariototal = 0;
            int termtotal = 0;
            foreach (var item in EDBateriaGestionConsulta.ListaUsuarios)
            {
                int pkIdUsuario = item.Pk_Id_BateriaUsuario;
                EDBateriaUsuario Usuario = new EDBateriaUsuario();
                Usuario = LNBateria.ConsultarConvocadoId1(pkIdUsuario, usuarioActual.IdEmpresa);
                if (Usuario.NumeroIntentos == form)
                {
                    if (Usuario.RegistroOperacion == "Fin" && Usuario.ConfirmacionParticipacion == "Aceptado")
                    {
                        ListaUsuariosTerminados.Add(Usuario);
                    }
                    usuariototal++;
                }

                if (Usuario.RegistroOperacion == "Fin" && Usuario.ConfirmacionParticipacion == "Aceptado")
                {
                    termtotal++;
                }


            }
            ViewBag.Total = usuariototal.ToString();
            EDBateriaGestionConsulta.ListaUsuarios = ListaUsuariosTerminados;
            //Calculo de valores transformados y nivel de riesgo por usuario
            int contTer = EDBateriaGestionConsulta.ListaUsuarios.Count();
            ViewBag.Terminados = contTer;

            decimal ejecutado = 0;
            if (usuariototal != 0)
            {
                ejecutado = Math.Round((decimal)contTer / (decimal)usuariototal, 1);
                ejecutado = ejecutado * 100;
            }

            ViewBag.Ejecutado = ejecutado.ToString() + "%";


            
            

            using (ExcelPackage wb = new ExcelPackage())
            {
                ExcelWorksheet ws0 = wb.Workbook.Worksheets.Add("GENERAL");
                Color graycolor = ColorTranslator.FromHtml("#dbded9");

                ws0.Row(1).Height = 27;
                ws0.Row(2).Height = 27;
                ws0.Row(3).Height = 27;
                

                ws0.Cells[3, 1].Style.Font.Bold = true;
                ws0.Cells[3, 1].Style.Font.Size = 14;
                ws0.Cells[3, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws0.Cells[3, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                
                ws0.Cells[3, 1, 3, 8].Merge = true;
                ws0.Cells[3, 1, 3, 8].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws0.Cells[3, 1, 3, 8].Style.Fill.BackgroundColor.SetColor(graycolor);

                ws0.Cells[3, 1, 3, 8].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                ws0.Cells[3, 1, 3, 8].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                ws0.Cells[3, 1, 3, 8].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                ws0.Cells[3, 1, 3, 8].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                for (int i = 1; i < 9; i++)
                {
                    ws0.Cells[4, i].Style.Font.Bold = true;
                    ws0.Cells[4, i].Style.Font.Size = 11;
                    ws0.Cells[4, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ws0.Cells[4, i].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    
                    ws0.Cells[4, i].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    ws0.Cells[4, i].Style.Fill.BackgroundColor.SetColor(graycolor);

                    ws0.Cells[4, i].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    ws0.Cells[4, i].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    ws0.Cells[4, i].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    ws0.Cells[4, i].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                }

                ws0.Cells[1, 1].Style.Font.Bold = true;
                ws0.Cells[2, 1].Style.Font.Bold = true;
                ws0.Cells[1, 1].Value = "EMPRESA:"+usuarioActual.RazonSocialEmpresa.ToUpper() + " - NIT:" + usuarioActual.NitEmpresa.ToUpper();
                ws0.Cells[2, 1].Value = "FECHA DE REGISTRO:" + EDBateriaGestionConsulta.FechaRegistro.ToShortDateString();

                ws0.Cells[4, 1].Value = "No Documento";
                ws0.Cells[4, 2].Value = "Nombre";
                ws0.Cells[4, 3].Value = "No Pregunta";
                ws0.Cells[4, 4].Value = "Pregunta";
                ws0.Cells[4, 5].Value = "Respuesta";
                ws0.Cells[4, 6].Value = "Valor";
                ws0.Cells[4, 7].Value = "Dominio";
                ws0.Cells[4, 8].Value = "Dimensión";

                ws0.Column(1).Width = 25;
                ws0.Column(2).Width = 35;
                ws0.Column(3).Width = 12;
                ws0.Column(4).Width = 40;
                ws0.Column(5).Width = 25;
                ws0.Column(6).Width = 12;
                ws0.Column(7).Width = 32;
                ws0.Column(8).Width = 32;

                

                int filawb = 5;
                foreach (var itemUs in EDBateriaGestionConsulta.ListaUsuarios)
                {
                    string Nombre = itemUs.Nombre;
                    string NumeroId = itemUs.NumeroIdentificacion;

                    #region CalculoValores&riesgo

                    #region Dominios&Dimensiones

                    List<EDBateriaDominio> ListaDominios = new List<EDBateriaDominio>();
                    List<EDBateriaCuestionario> ListaCuestionario = new List<EDBateriaCuestionario>();
                    if (EDBateriaGestionConsulta.Fk_Id_Bateria == 1 && itemUs.NumeroIntentos == 0)
                    {
                        ListaCuestionario = LNBateria.ConsultarFormulario(1, EDBateriaGestionConsulta.Fk_Id_Bateria);
                        itemUs.NombreEncuestaTotal = "TOTAL GENERAL FACTORES DE RIESGO PSICOSOCIAL INTRALABORAL";
                        nombrePrincipal = "Cuestionario de Factores de Riesgo Psicosocial Intralaboral FORMA A";
                        itemUs.NombreEncuesta = "Cuestionario de Factores de Riesgo Psicosocial Intralaboral FORMA A";
                        itemUs.FactorTransformacion = 492;
                        EDBateriaDominio EDBateriaDominio = new EDBateriaDominio();
                        EDBateriaDominio.Nombre = "Liderazgo y relaciones sociales en el trabajo";
                        EDBateriaDominio.Pk_Id_BateriaDimension = 1;
                        EDBateriaDominio.ListaDimensiones = LNBateria.ListaDimensiones(1, EDBateriaGestionConsulta.Fk_Id_Bateria);
                        EDBateriaDominio.FactorTransformacion = 164;

                        EDBateriaDominio EDBateriaDominio1 = new EDBateriaDominio();
                        EDBateriaDominio1.Nombre = "Control sobre el trabajo";
                        EDBateriaDominio1.Pk_Id_BateriaDimension = 2;
                        EDBateriaDominio1.ListaDimensiones = LNBateria.ListaDimensiones(2, EDBateriaGestionConsulta.Fk_Id_Bateria);
                        EDBateriaDominio1.FactorTransformacion = 84;

                        EDBateriaDominio EDBateriaDominio2 = new EDBateriaDominio();
                        EDBateriaDominio2.Nombre = "Demandas del trabajo";
                        EDBateriaDominio2.Pk_Id_BateriaDimension = 3;
                        EDBateriaDominio2.ListaDimensiones = LNBateria.ListaDimensiones(3, EDBateriaGestionConsulta.Fk_Id_Bateria);
                        EDBateriaDominio2.FactorTransformacion = 200;

                        EDBateriaDominio EDBateriaDominio3 = new EDBateriaDominio();
                        EDBateriaDominio3.Nombre = "Recompensas";
                        EDBateriaDominio3.Pk_Id_BateriaDimension = 4;
                        EDBateriaDominio3.ListaDimensiones = LNBateria.ListaDimensiones(4, EDBateriaGestionConsulta.Fk_Id_Bateria);
                        EDBateriaDominio3.FactorTransformacion = 44;

                        ListaDominios.Add(EDBateriaDominio);
                        ListaDominios.Add(EDBateriaDominio1);
                        ListaDominios.Add(EDBateriaDominio2);
                        ListaDominios.Add(EDBateriaDominio3);
                    }
                    if (EDBateriaGestionConsulta.Fk_Id_Bateria == 2 && itemUs.NumeroIntentos == 0)
                    {
                        ListaCuestionario = LNBateria.ConsultarFormulario(1, EDBateriaGestionConsulta.Fk_Id_Bateria);
                        itemUs.NombreEncuestaTotal = "TOTAL GENERAL FACTORES DE RIESGO PSICOSOCIAL INTRALABORAL";
                        nombrePrincipal = "Cuestionario de Factores de Riesgo Psicosocial Intralaboral FORMA B";
                        itemUs.NombreEncuesta = "Cuestionario de Factores de Riesgo Psicosocial Intralaboral FORMA B";
                        itemUs.FactorTransformacion = 388;
                        EDBateriaDominio EDBateriaDominio = new EDBateriaDominio();
                        EDBateriaDominio.Nombre = "Liderazgo y relaciones sociales en el trabajo";
                        EDBateriaDominio.Pk_Id_BateriaDimension = 1;
                        EDBateriaDominio.ListaDimensiones = LNBateria.ListaDimensiones(1, EDBateriaGestionConsulta.Fk_Id_Bateria);
                        EDBateriaDominio.FactorTransformacion = 120;

                        EDBateriaDominio EDBateriaDominio1 = new EDBateriaDominio();
                        EDBateriaDominio1.Nombre = "Control sobre el trabajo";
                        EDBateriaDominio1.Pk_Id_BateriaDimension = 2;
                        EDBateriaDominio1.ListaDimensiones = LNBateria.ListaDimensiones(2, EDBateriaGestionConsulta.Fk_Id_Bateria);
                        EDBateriaDominio1.FactorTransformacion = 72;

                        EDBateriaDominio EDBateriaDominio2 = new EDBateriaDominio();
                        EDBateriaDominio2.Nombre = "Demandas del trabajo";
                        EDBateriaDominio2.Pk_Id_BateriaDimension = 3;
                        EDBateriaDominio2.ListaDimensiones = LNBateria.ListaDimensiones(3, EDBateriaGestionConsulta.Fk_Id_Bateria);
                        EDBateriaDominio2.FactorTransformacion = 156;

                        EDBateriaDominio EDBateriaDominio3 = new EDBateriaDominio();
                        EDBateriaDominio3.Nombre = "Recompensas";
                        EDBateriaDominio3.Pk_Id_BateriaDimension = 4;
                        EDBateriaDominio3.ListaDimensiones = LNBateria.ListaDimensiones(4, EDBateriaGestionConsulta.Fk_Id_Bateria);
                        EDBateriaDominio3.FactorTransformacion = 40;

                        ListaDominios.Add(EDBateriaDominio);
                        ListaDominios.Add(EDBateriaDominio1);
                        ListaDominios.Add(EDBateriaDominio2);
                        ListaDominios.Add(EDBateriaDominio3);

                    }
                    if (EDBateriaGestionConsulta.Fk_Id_Bateria == 1 && itemUs.NumeroIntentos == 1)
                    {
                        ListaCuestionario = LNBateria.ConsultarFormulario(1, 3);
                        itemUs.NombreEncuestaTotal = "TOTAL GENERAL FACTORES DE RIESGO PSICOSOCIAL EXTRALABORAL";
                        itemUs.NombreEncuesta = "Cuestionario de Factores de Riesgo Psicosocial Extralaboral";
                        nombrePrincipal = "Cuestionario de Factores de Riesgo Psicosocial Extralaboral";
                        itemUs.FactorTransformacion = 124;
                        EDBateriaDominio EDBateriaDominio = new EDBateriaDominio();
                        EDBateriaDominio.Nombre = "N/A";
                        EDBateriaDominio.Pk_Id_BateriaDimension = 1;
                        EDBateriaDominio.ListaDimensiones = LNBateria.ListaDimensiones(1, 3);
                        EDBateriaDominio.FactorTransformacion = 1;

                        ListaDominios.Add(EDBateriaDominio);
                    }
                    if (EDBateriaGestionConsulta.Fk_Id_Bateria == 2 && itemUs.NumeroIntentos == 1)
                    {
                        ListaCuestionario = LNBateria.ConsultarFormulario(1, 3);
                        itemUs.NombreEncuestaTotal = "TOTAL GENERAL FACTORES DE RIESGO PSICOSOCIAL EXTRALABORAL";
                        itemUs.NombreEncuesta = "Cuestionario de Factores de Riesgo Psicosocial Extralaboral";
                        nombrePrincipal = "Cuestionario de Factores de Riesgo Psicosocial Extralaboral";
                        itemUs.FactorTransformacion = 124;
                        EDBateriaDominio EDBateriaDominio = new EDBateriaDominio();
                        EDBateriaDominio.Nombre = "N/A";
                        EDBateriaDominio.Pk_Id_BateriaDimension = 1;
                        EDBateriaDominio.ListaDimensiones = LNBateria.ListaDimensiones(1, 3);
                        EDBateriaDominio.FactorTransformacion = 1;

                        ListaDominios.Add(EDBateriaDominio);
                    }
                    if (EDBateriaGestionConsulta.Fk_Id_Bateria == 4)
                    {
                        ListaCuestionario = LNBateria.ConsultarFormulario(1, 4);
                        itemUs.NombreEncuestaTotal = "TOTAL GENERAL SÍNTOMAS DE ESTRÉS";
                        itemUs.NombreEncuesta = "Cuestionario de Factores de Estrés";
                        nombrePrincipal = "Cuestionario de Factores de Estrés";
                        itemUs.FactorTransformacion = 61.16;
                        EDBateriaDominio EDBateriaDominio = new EDBateriaDominio();
                        EDBateriaDominio.Nombre = "N/A";
                        EDBateriaDominio.Pk_Id_BateriaDimension = 1;
                        EDBateriaDominio.ListaDimensiones = LNBateria.ListaDimensiones(1, 4);
                        EDBateriaDominio.FactorTransformacion = 1;

                        ListaDominios.Add(EDBateriaDominio);
                    }
                    #endregion
                    
                    #region ResultadosIntraFormA

                    if (EDBateriaGestionConsulta.Fk_Id_Bateria == 1 && itemUs.NumeroIntentos == 0)
                    {
                        decimal[,,] evalriesgo = baremosIntraA();
                        decimal[,,] evalriesgoDom = baremosDominioIntraA();
                        foreach (var item in itemUs.ListaResultados)
                        {
                            int iddom = item.DominioInt;
                            int iddim = item.DimensionInt;

                            EDBateriaDominio EDBateriaDominio = ListaDominios.Where(s => s.Pk_Id_BateriaDimension == iddom).FirstOrDefault();
                            List<EDBateriaDimension> Listas = EDBateriaDominio.ListaDimensiones;
                            EDBateriaDimension EDBateriaDimension = Listas.Where(s => s.Pk_Id_BateriaDimension == iddim).FirstOrDefault();

                            EDBateriaCuestionario EDBateriaCuestionario = ListaCuestionario.Where(s => s.Pk_Id_BateriaCuestionario == item.Fk_Id_BateriaCuestionario).FirstOrDefault();
                            string NombreDom = "";
                            string NombreDim = "";
                            decimal Resultado = item.ValorResultado;
                            string Resultado_S = "";
                            if (item.Valor==1)
                            {
                                Resultado_S = "SIEMPRE";
                            }
                            if (item.Valor == 2)
                            {
                                Resultado_S = "CASI SIEMPRE";
                            }
                            if (item.Valor == 3)
                            {
                                Resultado_S = "ALGUNAS VECES";
                            }
                            if (item.Valor == 4)
                            {
                                Resultado_S = "CASI NUNCA";
                            }
                            if (item.Valor == 5)
                            {
                                Resultado_S = "NUNCA";
                            }

                            NombreDom = EDBateriaDominio.Nombre;
                            NombreDim = EDBateriaDimension.Nombre;

                            string pregunta = EDBateriaCuestionario.Pregunta;
                            int orden = EDBateriaCuestionario.Orden;

                            EDBateriaDominio.Puntaje += item.ValorResultado;
                            EDBateriaDimension.Puntaje += item.ValorResultado;
                            itemUs.Puntaje += item.ValorResultado;

                            ws0.Cells[filawb, 1].Value = NumeroId;
                            ws0.Cells[filawb, 2].Value = Nombre;
                            ws0.Cells[filawb, 3].Value = orden;
                            ws0.Cells[filawb, 4].Value = pregunta;
                            ws0.Cells[filawb, 5].Value = Resultado_S;
                            ws0.Cells[filawb, 6].Value = Resultado;
                            ws0.Cells[filawb, 7].Value = NombreDom;
                            ws0.Cells[filawb, 8].Value = NombreDim;

                            filawb++;
                        }
                        int cont = 1;
                        int cont1 = 1;
                        foreach (var item in ListaDominios)
                        {
                            item.PuntajeTrans = (item.Puntaje / (decimal)item.FactorTransformacion) * 100;
                            for (int i1 = 1; i1 < 6; i1++)
                            {
                                decimal CotaA = evalriesgoDom[cont1, i1, 1];
                                decimal CotaB = evalriesgoDom[cont1, i1, 2];
                                if (item.PuntajeTrans >= evalriesgoDom[cont1, i1, 1] && item.PuntajeTrans <= evalriesgoDom[cont1, i1, 2])
                                {
                                    if (i1 == 1)
                                    {
                                        item.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                    }
                                    if (i1 == 2)
                                    {
                                        item.NivelRiesgoDesc = "Riesgo bajo";
                                    }
                                    if (i1 == 3)
                                    {
                                        item.NivelRiesgoDesc = "Riesgo medio";
                                    }
                                    if (i1 == 4)
                                    {
                                        item.NivelRiesgoDesc = "Riesgo alto";
                                    }
                                    if (i1 == 5)
                                    {
                                        item.NivelRiesgoDesc = "Riesgo muy alto";
                                    }
                                }
                            }
                            if (item.NivelRiesgoDesc == null)
                            {
                                decimal round = Math.Round(item.PuntajeTrans, 1);
                                for (int i1 = 1; i1 < 6; i1++)
                                {
                                    decimal CotaA = evalriesgoDom[cont1, i1, 1];
                                    decimal CotaB = evalriesgoDom[cont1, i1, 2];
                                    if (round >= CotaA && round <= CotaB)
                                    {
                                        if (i1 == 1)
                                        {
                                            item.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                        }
                                        if (i1 == 2)
                                        {
                                            item.NivelRiesgoDesc = "Riesgo bajo";
                                        }
                                        if (i1 == 3)
                                        {
                                            item.NivelRiesgoDesc = "Riesgo medio";
                                        }
                                        if (i1 == 4)
                                        {
                                            item.NivelRiesgoDesc = "Riesgo alto";
                                        }
                                        if (i1 == 5)
                                        {
                                            item.NivelRiesgoDesc = "Riesgo muy alto";
                                        }
                                    }
                                }
                            }


                            foreach (var item1 in item.ListaDimensiones)
                            {
                                item1.PuntajeTrans = (item1.Puntaje / (decimal)item1.FactorTransformacion) * 100;

                                for (int i1 = 1; i1 < 6; i1++)
                                {
                                    decimal CotaA = evalriesgo[cont, i1, 1];
                                    decimal CotaB = evalriesgo[cont, i1, 2];
                                    if (item1.PuntajeTrans >= evalriesgo[cont, i1, 1] && item1.PuntajeTrans <= evalriesgo[cont, i1, 2])
                                    {
                                        if (i1 == 1)
                                        {
                                            item1.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                        }
                                        if (i1 == 2)
                                        {
                                            item1.NivelRiesgoDesc = "Riesgo bajo";
                                        }
                                        if (i1 == 3)
                                        {
                                            item1.NivelRiesgoDesc = "Riesgo medio";
                                        }
                                        if (i1 == 4)
                                        {
                                            item1.NivelRiesgoDesc = "Riesgo alto";
                                        }
                                        if (i1 == 5)
                                        {
                                            item1.NivelRiesgoDesc = "Riesgo muy alto";
                                        }
                                    }
                                }
                                if (item1.NivelRiesgoDesc == null)
                                {
                                    decimal round = Math.Round(item1.PuntajeTrans, 1);
                                    for (int i1 = 1; i1 < 6; i1++)
                                    {
                                        decimal CotaA = evalriesgo[cont, i1, 1];
                                        decimal CotaB = evalriesgo[cont, i1, 2];
                                        if (round >= CotaA && round <= CotaB)
                                        {
                                            if (i1 == 1)
                                            {
                                                item1.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                            }
                                            if (i1 == 2)
                                            {
                                                item1.NivelRiesgoDesc = "Riesgo bajo";
                                            }
                                            if (i1 == 3)
                                            {
                                                item1.NivelRiesgoDesc = "Riesgo medio";
                                            }
                                            if (i1 == 4)
                                            {
                                                item1.NivelRiesgoDesc = "Riesgo alto";
                                            }
                                            if (i1 == 5)
                                            {
                                                item1.NivelRiesgoDesc = "Riesgo muy alto";
                                            }
                                        }
                                    }
                                }
                                cont++;
                            }
                            cont1++;
                        }
                        cont1 = 1;
                        itemUs.PuntajeTrans = (itemUs.Puntaje / (decimal)itemUs.FactorTransformacion) * 100;
                        itemUs.Listadominios = new List<EDBateriaDominio>();
                        itemUs.Listadominios = ListaDominios;
                        decimal[,,] evalriesgoTotal = new decimal[10, 10, 10];

                        string formdata = itemUs.TokenPrivado;

                        EDBateriaUsuario EDBateriaUsuarioExtra = LNBateria.ConsultarConvocadoKeyExtra(formdata, 1);
                        if (EDBateriaUsuarioExtra != null)
                        {
                            if (EDBateriaUsuarioExtra.Pk_Id_BateriaUsuario == 0)
                            {
                                evalriesgoTotal = baremosTotalIntraA();
                            }
                            else
                            {
                                if (EDBateriaUsuarioExtra.RegistroOperacion == "Fin")
                                {
                                    evalriesgoTotal = baremosTotalExtraIntraA();
                                }
                                else
                                {
                                    evalriesgoTotal = baremosTotalIntraA();
                                }
                            }
                        }
                        else
                        {
                            evalriesgoTotal = baremosTotalIntraA();
                        }

                        for (int i1 = 1; i1 < 6; i1++)
                        {
                            decimal CotaA = evalriesgoTotal[cont1, i1, 1];
                            decimal CotaB = evalriesgoTotal[cont1, i1, 2];
                            if (itemUs.PuntajeTrans >= evalriesgoTotal[cont1, i1, 1] && itemUs.PuntajeTrans <= evalriesgoTotal[cont1, i1, 2])
                            {
                                if (i1 == 1)
                                {
                                    itemUs.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                }
                                if (i1 == 2)
                                {
                                    itemUs.NivelRiesgoDesc = "Riesgo bajo";
                                }
                                if (i1 == 3)
                                {
                                    itemUs.NivelRiesgoDesc = "Riesgo medio";
                                }
                                if (i1 == 4)
                                {
                                    itemUs.NivelRiesgoDesc = "Riesgo alto";
                                }
                                if (i1 == 5)
                                {
                                    itemUs.NivelRiesgoDesc = "Riesgo muy alto";
                                }
                            }
                        }
                        if (itemUs.NivelRiesgoDesc == null)
                        {
                            decimal round = Math.Round(itemUs.PuntajeTrans, 1);
                            for (int i1 = 1; i1 < 6; i1++)
                            {
                                decimal CotaA = evalriesgoTotal[cont, i1, 1];
                                decimal CotaB = evalriesgoTotal[cont, i1, 2];
                                if (round >= CotaA && round <= CotaB)
                                {
                                    if (i1 == 1)
                                    {
                                        itemUs.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                    }
                                    if (i1 == 2)
                                    {
                                        itemUs.NivelRiesgoDesc = "Riesgo bajo";
                                    }
                                    if (i1 == 3)
                                    {
                                        itemUs.NivelRiesgoDesc = "Riesgo medio";
                                    }
                                    if (i1 == 4)
                                    {
                                        itemUs.NivelRiesgoDesc = "Riesgo alto";
                                    }
                                    if (i1 == 5)
                                    {
                                        itemUs.NivelRiesgoDesc = "Riesgo muy alto";
                                    }
                                }
                            }
                        }

                    }
                    #endregion
                    #region ResultadosIntraFormB


                    if (EDBateriaGestionConsulta.Fk_Id_Bateria == 2 && itemUs.NumeroIntentos == 0)
                    {
                        decimal[,,] evalriesgo = baremosIntraB();
                        decimal[,,] evalriesgoDom = baremosDominioIntraB();
                        foreach (var item in itemUs.ListaResultados)
                        {
                            int iddom = item.DominioInt;
                            int iddim = item.DimensionInt;

                            EDBateriaDominio EDBateriaDominio = ListaDominios.Where(s => s.Pk_Id_BateriaDimension == iddom).FirstOrDefault();
                            List<EDBateriaDimension> Listas = EDBateriaDominio.ListaDimensiones;
                            EDBateriaDimension EDBateriaDimension = Listas.Where(s => s.Pk_Id_BateriaDimension == iddim).FirstOrDefault();
                            EDBateriaCuestionario EDBateriaCuestionario = ListaCuestionario.Where(s => s.Pk_Id_BateriaCuestionario == item.Fk_Id_BateriaCuestionario).FirstOrDefault();
                            string NombreDom = "";
                            string NombreDim = "";
                            decimal Resultado = item.ValorResultado;
                            string Resultado_S = "";
                            if (item.Valor == 1)
                            {
                                Resultado_S = "SIEMPRE";
                            }
                            if (item.Valor == 2)
                            {
                                Resultado_S = "CASI SIEMPRE";
                            }
                            if (item.Valor == 3)
                            {
                                Resultado_S = "ALGUNAS VECES";
                            }
                            if (item.Valor == 4)
                            {
                                Resultado_S = "CASI NUNCA";
                            }
                            if (item.Valor == 5)
                            {
                                Resultado_S = "NUNCA";
                            }

                            NombreDom = EDBateriaDominio.Nombre;
                            NombreDim = EDBateriaDimension.Nombre;

                            string pregunta = EDBateriaCuestionario.Pregunta;
                            int orden = EDBateriaCuestionario.Orden;

                            EDBateriaDominio.Puntaje += item.ValorResultado;
                            EDBateriaDimension.Puntaje += item.ValorResultado;
                            itemUs.Puntaje += item.ValorResultado;

                            ws0.Cells[filawb, 1].Value = NumeroId;
                            ws0.Cells[filawb, 2].Value = Nombre;
                            ws0.Cells[filawb, 3].Value = orden;
                            ws0.Cells[filawb, 4].Value = pregunta;
                            ws0.Cells[filawb, 5].Value = Resultado_S;
                            ws0.Cells[filawb, 6].Value = Resultado;
                            ws0.Cells[filawb, 7].Value = NombreDom;
                            ws0.Cells[filawb, 8].Value = NombreDim;

                            filawb++;

                            EDBateriaDominio.Puntaje += item.ValorResultado;
                            EDBateriaDimension.Puntaje += item.ValorResultado;
                            itemUs.Puntaje += item.ValorResultado;
                        }
                        int cont = 1;
                        int cont1 = 1;
                        foreach (var item in ListaDominios)
                        {
                            item.PuntajeTrans = (item.Puntaje / (decimal)item.FactorTransformacion) * 100;
                            for (int i1 = 1; i1 < 6; i1++)
                            {
                                decimal CotaA = evalriesgoDom[cont1, i1, 1];
                                decimal CotaB = evalriesgoDom[cont1, i1, 2];
                                if (item.PuntajeTrans >= evalriesgoDom[cont1, i1, 1] && item.PuntajeTrans <= evalriesgoDom[cont1, i1, 2])
                                {
                                    if (i1 == 1)
                                    {
                                        item.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                    }
                                    if (i1 == 2)
                                    {
                                        item.NivelRiesgoDesc = "Riesgo bajo";
                                    }
                                    if (i1 == 3)
                                    {
                                        item.NivelRiesgoDesc = "Riesgo medio";
                                    }
                                    if (i1 == 4)
                                    {
                                        item.NivelRiesgoDesc = "Riesgo alto";
                                    }
                                    if (i1 == 5)
                                    {
                                        item.NivelRiesgoDesc = "Riesgo muy alto";
                                    }
                                }
                            }
                            if (item.NivelRiesgoDesc == null)
                            {
                                decimal round = Math.Round(item.PuntajeTrans, 1);
                                for (int i1 = 1; i1 < 6; i1++)
                                {
                                    decimal CotaA = evalriesgoDom[cont1, i1, 1];
                                    decimal CotaB = evalriesgoDom[cont1, i1, 2];
                                    if (round >= CotaA && round <= CotaB)
                                    {
                                        if (i1 == 1)
                                        {
                                            item.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                        }
                                        if (i1 == 2)
                                        {
                                            item.NivelRiesgoDesc = "Riesgo bajo";
                                        }
                                        if (i1 == 3)
                                        {
                                            item.NivelRiesgoDesc = "Riesgo medio";
                                        }
                                        if (i1 == 4)
                                        {
                                            item.NivelRiesgoDesc = "Riesgo alto";
                                        }
                                        if (i1 == 5)
                                        {
                                            item.NivelRiesgoDesc = "Riesgo muy alto";
                                        }
                                    }
                                }
                            }


                            foreach (var item1 in item.ListaDimensiones)
                            {
                                item1.PuntajeTrans = (item1.Puntaje / (decimal)item1.FactorTransformacion) * 100;

                                for (int i1 = 1; i1 < 6; i1++)
                                {
                                    decimal CotaA = evalriesgo[cont, i1, 1];
                                    decimal CotaB = evalriesgo[cont, i1, 2];
                                    if (item1.PuntajeTrans >= CotaA && item1.PuntajeTrans <= CotaB)
                                    {
                                        if (i1 == 1)
                                        {
                                            item1.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                        }
                                        if (i1 == 2)
                                        {
                                            item1.NivelRiesgoDesc = "Riesgo bajo";
                                        }
                                        if (i1 == 3)
                                        {
                                            item1.NivelRiesgoDesc = "Riesgo medio";
                                        }
                                        if (i1 == 4)
                                        {
                                            item1.NivelRiesgoDesc = "Riesgo alto";
                                        }
                                        if (i1 == 5)
                                        {
                                            item1.NivelRiesgoDesc = "Riesgo muy alto";
                                        }
                                    }
                                }
                                if (item1.NivelRiesgoDesc == null)
                                {
                                    decimal round = Math.Round(item1.PuntajeTrans, 1);
                                    for (int i1 = 1; i1 < 6; i1++)
                                    {
                                        decimal CotaA = evalriesgo[cont, i1, 1];
                                        decimal CotaB = evalriesgo[cont, i1, 2];
                                        if (round >= CotaA && round <= CotaB)
                                        {
                                            if (i1 == 1)
                                            {
                                                item1.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                            }
                                            if (i1 == 2)
                                            {
                                                item1.NivelRiesgoDesc = "Riesgo bajo";
                                            }
                                            if (i1 == 3)
                                            {
                                                item1.NivelRiesgoDesc = "Riesgo medio";
                                            }
                                            if (i1 == 4)
                                            {
                                                item1.NivelRiesgoDesc = "Riesgo alto";
                                            }
                                            if (i1 == 5)
                                            {
                                                item1.NivelRiesgoDesc = "Riesgo muy alto";
                                            }
                                        }
                                    }
                                }
                                cont++;



                            }
                            cont1++;
                        }
                        cont1 = 1;
                        itemUs.PuntajeTrans = (itemUs.Puntaje / (decimal)itemUs.FactorTransformacion) * 100;
                        itemUs.Listadominios = new List<EDBateriaDominio>();
                        itemUs.Listadominios = ListaDominios;
                        decimal[,,] evalriesgoTotal = new decimal[10, 10, 10];

                        string formdata = itemUs.TokenPrivado;

                        EDBateriaUsuario EDBateriaUsuarioExtra = LNBateria.ConsultarConvocadoKeyExtra(formdata, 1);
                        if (EDBateriaUsuarioExtra != null)
                        {
                            if (EDBateriaUsuarioExtra.Pk_Id_BateriaUsuario == 0)
                            {
                                evalriesgoTotal = baremosTotalIntraB();
                            }
                            else
                            {
                                if (EDBateriaUsuarioExtra.RegistroOperacion == "Fin")
                                {
                                    evalriesgoTotal = baremosTotalExtraIntraB();
                                }
                                else
                                {
                                    evalriesgoTotal = baremosTotalIntraB();
                                }
                            }
                        }
                        else
                        {
                            evalriesgoTotal = baremosTotalIntraB();
                        }

                        for (int i1 = 1; i1 < 6; i1++)
                        {
                            decimal CotaA = evalriesgoTotal[cont1, i1, 1];
                            decimal CotaB = evalriesgoTotal[cont1, i1, 2];
                            if (itemUs.PuntajeTrans >= evalriesgoTotal[cont1, i1, 1] && itemUs.PuntajeTrans <= evalriesgoTotal[cont1, i1, 2])
                            {
                                if (i1 == 1)
                                {
                                    itemUs.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                }
                                if (i1 == 2)
                                {
                                    itemUs.NivelRiesgoDesc = "Riesgo bajo";
                                }
                                if (i1 == 3)
                                {
                                    itemUs.NivelRiesgoDesc = "Riesgo medio";
                                }
                                if (i1 == 4)
                                {
                                    itemUs.NivelRiesgoDesc = "Riesgo alto";
                                }
                                if (i1 == 5)
                                {
                                    itemUs.NivelRiesgoDesc = "Riesgo muy alto";
                                }
                            }
                        }
                        if (itemUs.NivelRiesgoDesc == null)
                        {
                            decimal round = Math.Round(itemUs.PuntajeTrans, 1);
                            for (int i1 = 1; i1 < 6; i1++)
                            {
                                decimal CotaA = evalriesgoTotal[cont, i1, 1];
                                decimal CotaB = evalriesgoTotal[cont, i1, 2];
                                if (round >= CotaA && round <= CotaB)
                                {
                                    if (i1 == 1)
                                    {
                                        itemUs.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                    }
                                    if (i1 == 2)
                                    {
                                        itemUs.NivelRiesgoDesc = "Riesgo bajo";
                                    }
                                    if (i1 == 3)
                                    {
                                        itemUs.NivelRiesgoDesc = "Riesgo medio";
                                    }
                                    if (i1 == 4)
                                    {
                                        itemUs.NivelRiesgoDesc = "Riesgo alto";
                                    }
                                    if (i1 == 5)
                                    {
                                        itemUs.NivelRiesgoDesc = "Riesgo muy alto";
                                    }
                                }
                            }
                        }

                    }
                    #endregion
                    #region ResultadosextraFormA


                    if (EDBateriaGestionConsulta.Fk_Id_Bateria == 1 && itemUs.NumeroIntentos == 1)
                    {
                        int tipo = 0;
                        decimal[,,] evalriesgo = new decimal[10, 10, 10];
                        decimal[,,] evalriesgoDom = new decimal[10, 10, 10];
                        if (itemUs.BateriaInicial.TipoCargo == "Jefatura - tiene personal a cargo" || itemUs.BateriaInicial.TipoCargo == "Profesional, analista, técnico, tecnólogo")
                        {
                            tipo = 1;
                            evalriesgo = baremosExtra1();
                            evalriesgoDom = baremosExtra1();
                        }
                        if (itemUs.BateriaInicial.TipoCargo == "Auxiliar, asistente administrativo, asistente técnico" || itemUs.BateriaInicial.TipoCargo == "Operario, operador, ayudante, servicios generales")
                        {
                            tipo = 2;
                            evalriesgo = baremosExtra2();
                            evalriesgoDom = baremosExtra2();
                        }
                        foreach (var item in itemUs.ListaResultados)
                        {
                            int iddom = item.DominioInt;
                            int iddim = item.DimensionInt;

                            EDBateriaDominio EDBateriaDominio = ListaDominios.Where(s => s.Pk_Id_BateriaDimension == iddom).FirstOrDefault();
                            List<EDBateriaDimension> Listas = EDBateriaDominio.ListaDimensiones;
                            EDBateriaDimension EDBateriaDimension = Listas.Where(s => s.Pk_Id_BateriaDimension == iddim).FirstOrDefault();

                            EDBateriaCuestionario EDBateriaCuestionario = ListaCuestionario.Where(s => s.Pk_Id_BateriaCuestionario == item.Fk_Id_BateriaCuestionario).FirstOrDefault();
                            string NombreDom = "";
                            string NombreDim = "";
                            decimal Resultado = item.ValorResultado;
                            string Resultado_S = "";
                            if (item.Valor == 1)
                            {
                                Resultado_S = "SIEMPRE";
                            }
                            if (item.Valor == 2)
                            {
                                Resultado_S = "CASI SIEMPRE";
                            }
                            if (item.Valor == 3)
                            {
                                Resultado_S = "ALGUNAS VECES";
                            }
                            if (item.Valor == 4)
                            {
                                Resultado_S = "CASI NUNCA";
                            }
                            if (item.Valor == 5)
                            {
                                Resultado_S = "NUNCA";
                            }

                            NombreDom = "N/A";
                            NombreDim = EDBateriaDimension.Nombre;

                            string pregunta = EDBateriaCuestionario.Pregunta;
                            int orden = EDBateriaCuestionario.Orden;

                            EDBateriaDominio.Puntaje += item.ValorResultado;
                            EDBateriaDimension.Puntaje += item.ValorResultado;
                            itemUs.Puntaje += item.ValorResultado;

                            ws0.Cells[filawb, 1].Value = NumeroId;
                            ws0.Cells[filawb, 2].Value = Nombre;
                            ws0.Cells[filawb, 3].Value = orden;
                            ws0.Cells[filawb, 4].Value = pregunta;
                            ws0.Cells[filawb, 5].Value = Resultado_S;
                            ws0.Cells[filawb, 6].Value = Resultado;
                            ws0.Cells[filawb, 7].Value = NombreDom;
                            ws0.Cells[filawb, 8].Value = NombreDim;

                            filawb++;

                            EDBateriaDominio.Puntaje += item.ValorResultado;
                            EDBateriaDimension.Puntaje += item.ValorResultado;
                            itemUs.Puntaje += item.ValorResultado;
                        }
                        int cont = 1;
                        int cont1 = 1;
                        foreach (var item in ListaDominios)
                        {
                            item.PuntajeTrans = (item.Puntaje / (decimal)item.FactorTransformacion) * 100;
                            for (int i1 = 1; i1 < 6; i1++)
                            {
                                decimal CotaA = evalriesgoDom[cont1, i1, 1];
                                decimal CotaB = evalriesgoDom[cont1, i1, 2];
                                if (item.PuntajeTrans >= evalriesgoDom[cont1, i1, 1] && item.PuntajeTrans <= evalriesgoDom[cont1, i1, 2])
                                {
                                    if (i1 == 1)
                                    {
                                        item.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                    }
                                    if (i1 == 2)
                                    {
                                        item.NivelRiesgoDesc = "Riesgo bajo";
                                    }
                                    if (i1 == 3)
                                    {
                                        item.NivelRiesgoDesc = "Riesgo medio";
                                    }
                                    if (i1 == 4)
                                    {
                                        item.NivelRiesgoDesc = "Riesgo alto";
                                    }
                                    if (i1 == 5)
                                    {
                                        item.NivelRiesgoDesc = "Riesgo muy alto";
                                    }
                                }
                            }
                            if (item.NivelRiesgoDesc == null)
                            {
                                decimal round = Math.Round(item.PuntajeTrans, 1);
                                for (int i1 = 1; i1 < 6; i1++)
                                {
                                    decimal CotaA = evalriesgoDom[cont1, i1, 1];
                                    decimal CotaB = evalriesgoDom[cont1, i1, 2];
                                    if (round >= CotaA && round <= CotaB)
                                    {
                                        if (i1 == 1)
                                        {
                                            item.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                        }
                                        if (i1 == 2)
                                        {
                                            item.NivelRiesgoDesc = "Riesgo bajo";
                                        }
                                        if (i1 == 3)
                                        {
                                            item.NivelRiesgoDesc = "Riesgo medio";
                                        }
                                        if (i1 == 4)
                                        {
                                            item.NivelRiesgoDesc = "Riesgo alto";
                                        }
                                        if (i1 == 5)
                                        {
                                            item.NivelRiesgoDesc = "Riesgo muy alto";
                                        }
                                    }
                                }
                            }



                            foreach (var item1 in item.ListaDimensiones)
                            {
                                item1.PuntajeTrans = (item1.Puntaje / (decimal)item1.FactorTransformacion) * 100;

                                for (int i1 = 1; i1 < 6; i1++)
                                {
                                    decimal CotaA = evalriesgo[cont, i1, 1];
                                    decimal CotaB = evalriesgo[cont, i1, 2];
                                    if (item1.PuntajeTrans >= evalriesgo[cont, i1, 1] && item1.PuntajeTrans <= evalriesgo[cont, i1, 2])
                                    {
                                        if (i1 == 1)
                                        {
                                            item1.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                        }
                                        if (i1 == 2)
                                        {
                                            item1.NivelRiesgoDesc = "Riesgo bajo";
                                        }
                                        if (i1 == 3)
                                        {
                                            item1.NivelRiesgoDesc = "Riesgo medio";
                                        }
                                        if (i1 == 4)
                                        {
                                            item1.NivelRiesgoDesc = "Riesgo alto";
                                        }
                                        if (i1 == 5)
                                        {
                                            item1.NivelRiesgoDesc = "Riesgo muy alto";
                                        }
                                    }
                                }
                                if (item1.NivelRiesgoDesc == null)
                                {
                                    decimal round = Math.Round(item1.PuntajeTrans, 1);
                                    for (int i1 = 1; i1 < 6; i1++)
                                    {
                                        decimal CotaA = evalriesgo[cont, i1, 1];
                                        decimal CotaB = evalriesgo[cont, i1, 2];
                                        if (round >= CotaA && round <= CotaB)
                                        {
                                            if (i1 == 1)
                                            {
                                                item1.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                            }
                                            if (i1 == 2)
                                            {
                                                item1.NivelRiesgoDesc = "Riesgo bajo";
                                            }
                                            if (i1 == 3)
                                            {
                                                item1.NivelRiesgoDesc = "Riesgo medio";
                                            }
                                            if (i1 == 4)
                                            {
                                                item1.NivelRiesgoDesc = "Riesgo alto";
                                            }
                                            if (i1 == 5)
                                            {
                                                item1.NivelRiesgoDesc = "Riesgo muy alto";
                                            }
                                        }
                                    }
                                }
                                cont++;
                            }
                            cont1++;
                        }
                        cont1 = 1;
                        itemUs.PuntajeTrans = (itemUs.Puntaje / (decimal)itemUs.FactorTransformacion) * 100;
                        itemUs.Listadominios = new List<EDBateriaDominio>();
                        itemUs.Listadominios = ListaDominios;
                        decimal[,,] evalriesgoTotal = new decimal[10, 10, 10];

                        evalriesgoTotal = baremosTotalExtraIntraA();

                        for (int i1 = 1; i1 < 6; i1++)
                        {
                            decimal CotaA = evalriesgoTotal[cont1, i1, 1];
                            decimal CotaB = evalriesgoTotal[cont1, i1, 2];
                            if (itemUs.PuntajeTrans >= evalriesgoTotal[cont1, i1, 1] && itemUs.PuntajeTrans <= evalriesgoTotal[cont1, i1, 2])
                            {
                                if (i1 == 1)
                                {
                                    itemUs.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                }
                                if (i1 == 2)
                                {
                                    itemUs.NivelRiesgoDesc = "Riesgo bajo";
                                }
                                if (i1 == 3)
                                {
                                    itemUs.NivelRiesgoDesc = "Riesgo medio";
                                }
                                if (i1 == 4)
                                {
                                    itemUs.NivelRiesgoDesc = "Riesgo alto";
                                }
                                if (i1 == 5)
                                {
                                    itemUs.NivelRiesgoDesc = "Riesgo muy alto";
                                }
                            }
                        }
                        if (itemUs.NivelRiesgoDesc == null)
                        {
                            decimal round = Math.Round(itemUs.PuntajeTrans, 1);
                            for (int i1 = 1; i1 < 6; i1++)
                            {
                                decimal CotaA = evalriesgoTotal[cont, i1, 1];
                                decimal CotaB = evalriesgoTotal[cont, i1, 2];
                                if (round >= CotaA && round <= CotaB)
                                {
                                    if (i1 == 1)
                                    {
                                        itemUs.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                    }
                                    if (i1 == 2)
                                    {
                                        itemUs.NivelRiesgoDesc = "Riesgo bajo";
                                    }
                                    if (i1 == 3)
                                    {
                                        itemUs.NivelRiesgoDesc = "Riesgo medio";
                                    }
                                    if (i1 == 4)
                                    {
                                        itemUs.NivelRiesgoDesc = "Riesgo alto";
                                    }
                                    if (i1 == 5)
                                    {
                                        itemUs.NivelRiesgoDesc = "Riesgo muy alto";
                                    }
                                }
                            }
                        }

                    }
                    #endregion
                    #region ResultadosextraFormB


                    if (EDBateriaGestionConsulta.Fk_Id_Bateria == 2 && itemUs.NumeroIntentos == 1)
                    {
                        int tipo = 0;
                        decimal[,,] evalriesgo = new decimal[10, 10, 10];
                        decimal[,,] evalriesgoDom = new decimal[10, 10, 10];
                        if (itemUs.BateriaInicial.TipoCargo == "Jefatura - tiene personal a cargo" || itemUs.BateriaInicial.TipoCargo == "Profesional, analista, técnico, tecnólogo")
                        {
                            tipo = 1;
                            evalriesgo = baremosExtra1();
                            evalriesgoDom = baremosExtra1();
                        }
                        if (itemUs.BateriaInicial.TipoCargo == "Auxiliar, asistente administrativo, asistente técnico" || itemUs.BateriaInicial.TipoCargo == "Operario, operador, ayudante, servicios generales")
                        {
                            tipo = 2;
                            evalriesgo = baremosExtra2();
                            evalriesgoDom = baremosExtra2();
                        }
                        foreach (var item in itemUs.ListaResultados)
                        {
                            int iddom = item.DominioInt;
                            int iddim = item.DimensionInt;

                            EDBateriaDominio EDBateriaDominio = ListaDominios.Where(s => s.Pk_Id_BateriaDimension == iddom).FirstOrDefault();
                            List<EDBateriaDimension> Listas = EDBateriaDominio.ListaDimensiones;
                            EDBateriaDimension EDBateriaDimension = Listas.Where(s => s.Pk_Id_BateriaDimension == iddim).FirstOrDefault();

                            EDBateriaCuestionario EDBateriaCuestionario = ListaCuestionario.Where(s => s.Pk_Id_BateriaCuestionario == item.Fk_Id_BateriaCuestionario).FirstOrDefault();
                            string NombreDom = "";
                            string NombreDim = "";
                            decimal Resultado = item.ValorResultado;
                            string Resultado_S = "";
                            if (item.Valor == 1)
                            {
                                Resultado_S = "SIEMPRE";
                            }
                            if (item.Valor == 2)
                            {
                                Resultado_S = "CASI SIEMPRE";
                            }
                            if (item.Valor == 3)
                            {
                                Resultado_S = "ALGUNAS VECES";
                            }
                            if (item.Valor == 4)
                            {
                                Resultado_S = "CASI NUNCA";
                            }
                            if (item.Valor == 5)
                            {
                                Resultado_S = "NUNCA";
                            }

                            NombreDom = "N/A";
                            NombreDim = EDBateriaDimension.Nombre;

                            string pregunta = EDBateriaCuestionario.Pregunta;
                            int orden = EDBateriaCuestionario.Orden;

                            EDBateriaDominio.Puntaje += item.ValorResultado;
                            EDBateriaDimension.Puntaje += item.ValorResultado;
                            itemUs.Puntaje += item.ValorResultado;

                            ws0.Cells[filawb, 1].Value = NumeroId;
                            ws0.Cells[filawb, 2].Value = Nombre;
                            ws0.Cells[filawb, 3].Value = orden;
                            ws0.Cells[filawb, 4].Value = pregunta;
                            ws0.Cells[filawb, 5].Value = Resultado_S;
                            ws0.Cells[filawb, 6].Value = Resultado;
                            ws0.Cells[filawb, 7].Value = NombreDom;
                            ws0.Cells[filawb, 8].Value = NombreDim;

                            filawb++;

                            EDBateriaDominio.Puntaje += item.ValorResultado;
                            EDBateriaDimension.Puntaje += item.ValorResultado;
                            itemUs.Puntaje += item.ValorResultado;
                        }
                        int cont = 1;
                        int cont1 = 1;
                        foreach (var item in ListaDominios)
                        {
                            item.PuntajeTrans = (item.Puntaje / (decimal)item.FactorTransformacion) * 100;
                            for (int i1 = 1; i1 < 6; i1++)
                            {
                                decimal CotaA = evalriesgoDom[cont1, i1, 1];
                                decimal CotaB = evalriesgoDom[cont1, i1, 2];
                                if (item.PuntajeTrans >= evalriesgoDom[cont1, i1, 1] && item.PuntajeTrans <= evalriesgoDom[cont1, i1, 2])
                                {
                                    if (i1 == 1)
                                    {
                                        item.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                    }
                                    if (i1 == 2)
                                    {
                                        item.NivelRiesgoDesc = "Riesgo bajo";
                                    }
                                    if (i1 == 3)
                                    {
                                        item.NivelRiesgoDesc = "Riesgo medio";
                                    }
                                    if (i1 == 4)
                                    {
                                        item.NivelRiesgoDesc = "Riesgo alto";
                                    }
                                    if (i1 == 5)
                                    {
                                        item.NivelRiesgoDesc = "Riesgo muy alto";
                                    }
                                }
                            }
                            if (item.NivelRiesgoDesc == null)
                            {
                                decimal round = Math.Round(item.PuntajeTrans, 1);
                                for (int i1 = 1; i1 < 6; i1++)
                                {
                                    decimal CotaA = evalriesgoDom[cont1, i1, 1];
                                    decimal CotaB = evalriesgoDom[cont1, i1, 2];
                                    if (round >= CotaA && round <= CotaB)
                                    {
                                        if (i1 == 1)
                                        {
                                            item.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                        }
                                        if (i1 == 2)
                                        {
                                            item.NivelRiesgoDesc = "Riesgo bajo";
                                        }
                                        if (i1 == 3)
                                        {
                                            item.NivelRiesgoDesc = "Riesgo medio";
                                        }
                                        if (i1 == 4)
                                        {
                                            item.NivelRiesgoDesc = "Riesgo alto";
                                        }
                                        if (i1 == 5)
                                        {
                                            item.NivelRiesgoDesc = "Riesgo muy alto";
                                        }
                                    }
                                }
                            }



                            foreach (var item1 in item.ListaDimensiones)
                            {
                                item1.PuntajeTrans = (item1.Puntaje / (decimal)item1.FactorTransformacion) * 100;

                                for (int i1 = 1; i1 < 6; i1++)
                                {
                                    decimal CotaA = evalriesgo[cont, i1, 1];
                                    decimal CotaB = evalriesgo[cont, i1, 2];
                                    if (item1.PuntajeTrans >= evalriesgo[cont, i1, 1] && item1.PuntajeTrans <= evalriesgo[cont, i1, 2])
                                    {
                                        if (i1 == 1)
                                        {
                                            item1.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                        }
                                        if (i1 == 2)
                                        {
                                            item1.NivelRiesgoDesc = "Riesgo bajo";
                                        }
                                        if (i1 == 3)
                                        {
                                            item1.NivelRiesgoDesc = "Riesgo medio";
                                        }
                                        if (i1 == 4)
                                        {
                                            item1.NivelRiesgoDesc = "Riesgo alto";
                                        }
                                        if (i1 == 5)
                                        {
                                            item1.NivelRiesgoDesc = "Riesgo muy alto";
                                        }
                                    }
                                }
                                if (item1.NivelRiesgoDesc == null)
                                {
                                    decimal round = Math.Round(item1.PuntajeTrans, 1);
                                    for (int i1 = 1; i1 < 6; i1++)
                                    {
                                        decimal CotaA = evalriesgo[cont, i1, 1];
                                        decimal CotaB = evalriesgo[cont, i1, 2];
                                        if (round >= CotaA && round <= CotaB)
                                        {
                                            if (i1 == 1)
                                            {
                                                item1.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                            }
                                            if (i1 == 2)
                                            {
                                                item1.NivelRiesgoDesc = "Riesgo bajo";
                                            }
                                            if (i1 == 3)
                                            {
                                                item1.NivelRiesgoDesc = "Riesgo medio";
                                            }
                                            if (i1 == 4)
                                            {
                                                item1.NivelRiesgoDesc = "Riesgo alto";
                                            }
                                            if (i1 == 5)
                                            {
                                                item1.NivelRiesgoDesc = "Riesgo muy alto";
                                            }
                                        }
                                    }
                                }
                                cont++;
                            }
                            cont1++;
                        }
                        cont1 = 1;
                        itemUs.PuntajeTrans = (itemUs.Puntaje / (decimal)itemUs.FactorTransformacion) * 100;
                        itemUs.Listadominios = new List<EDBateriaDominio>();
                        itemUs.Listadominios = ListaDominios;
                        decimal[,,] evalriesgoTotal = new decimal[10, 10, 10];

                        evalriesgoTotal = baremosTotalExtraIntraB();

                        for (int i1 = 1; i1 < 6; i1++)
                        {
                            decimal CotaA = evalriesgoTotal[cont1, i1, 1];
                            decimal CotaB = evalriesgoTotal[cont1, i1, 2];
                            if (itemUs.PuntajeTrans >= evalriesgoTotal[cont1, i1, 1] && itemUs.PuntajeTrans <= evalriesgoTotal[cont1, i1, 2])
                            {
                                if (i1 == 1)
                                {
                                    itemUs.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                }
                                if (i1 == 2)
                                {
                                    itemUs.NivelRiesgoDesc = "Riesgo bajo";
                                }
                                if (i1 == 3)
                                {
                                    itemUs.NivelRiesgoDesc = "Riesgo medio";
                                }
                                if (i1 == 4)
                                {
                                    itemUs.NivelRiesgoDesc = "Riesgo alto";
                                }
                                if (i1 == 5)
                                {
                                    itemUs.NivelRiesgoDesc = "Riesgo muy alto";
                                }
                            }
                        }
                        if (itemUs.NivelRiesgoDesc == null)
                        {
                            decimal round = Math.Round(itemUs.PuntajeTrans, 1);
                            for (int i1 = 1; i1 < 6; i1++)
                            {
                                decimal CotaA = evalriesgoTotal[cont, i1, 1];
                                decimal CotaB = evalriesgoTotal[cont, i1, 2];
                                if (round >= CotaA && round <= CotaB)
                                {
                                    if (i1 == 1)
                                    {
                                        itemUs.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                    }
                                    if (i1 == 2)
                                    {
                                        itemUs.NivelRiesgoDesc = "Riesgo bajo";
                                    }
                                    if (i1 == 3)
                                    {
                                        itemUs.NivelRiesgoDesc = "Riesgo medio";
                                    }
                                    if (i1 == 4)
                                    {
                                        itemUs.NivelRiesgoDesc = "Riesgo alto";
                                    }
                                    if (i1 == 5)
                                    {
                                        itemUs.NivelRiesgoDesc = "Riesgo muy alto";
                                    }
                                }
                            }
                        }
                    }
                    #endregion
                    #region ResultadosEstres


                    if (EDBateriaGestionConsulta.Fk_Id_Bateria == 4)
                    {

                        decimal[,,] evalriesgo = new decimal[10, 10, 10];
                        if (itemUs.BateriaInicial.TipoCargo == "Jefatura - tiene personal a cargo" || itemUs.BateriaInicial.TipoCargo == "Profesional, analista, técnico, tecnólogo")
                        {
                            evalriesgo = baremosEstres1();
                        }
                        if (itemUs.BateriaInicial.TipoCargo == "Auxiliar, asistente administrativo, asistente técnico" || itemUs.BateriaInicial.TipoCargo == "Operario, operador, ayudante, servicios generales")
                        {
                            evalriesgo = baremosEstres2();
                        }
                        decimal promedio1 = 0;
                        decimal promedio2 = 0;
                        decimal promedio3 = 0;
                        decimal promedio4 = 0;
                        int contp1 = 0;
                        int contp2 = 0;
                        int contp3 = 0;
                        int contp4 = 0;

                        int comienza = itemUs.ListaResultados.First().Fk_Id_BateriaCuestionario;
                        foreach (var item in itemUs.ListaResultados)
                        {
                            EDBateriaCuestionario EDBateriaCuestionario = ListaCuestionario.Where(s => s.Pk_Id_BateriaCuestionario == item.Fk_Id_BateriaCuestionario).FirstOrDefault();
                            string NombreDom = "";
                            string NombreDim = "";
                            decimal Resultado = item.ValorResultado;
                            string Resultado_S = "";
                            if (item.Valor == 1)
                            {
                                Resultado_S = "SIEMPRE";
                            }
                            if (item.Valor == 2)
                            {
                                Resultado_S = "CASI SIEMPRE";
                            }
                            if (item.Valor == 3)
                            {
                                Resultado_S = "A VECES";
                            }
                            if (item.Valor == 4)
                            {
                                Resultado_S = "NUNCA";
                            }


                            NombreDom = "N/A";
                            NombreDim = "N/A";

                            string pregunta = EDBateriaCuestionario.Pregunta;
                            int orden = EDBateriaCuestionario.Orden;


                            ws0.Cells[filawb, 1].Value = NumeroId;
                            ws0.Cells[filawb, 2].Value = Nombre;
                            ws0.Cells[filawb, 3].Value = orden;
                            ws0.Cells[filawb, 4].Value = pregunta;
                            ws0.Cells[filawb, 5].Value = Resultado_S;
                            ws0.Cells[filawb, 6].Value = Resultado;
                            ws0.Cells[filawb, 7].Value = NombreDom;
                            ws0.Cells[filawb, 8].Value = NombreDim;

                            filawb++;

                            if (item.Fk_Id_BateriaCuestionario >= comienza && item.Fk_Id_BateriaCuestionario <= comienza + 7)
                            {
                                promedio1 += item.ValorResultado;
                                contp1++;
                            }
                            if (item.Fk_Id_BateriaCuestionario >= comienza + 8 && item.Fk_Id_BateriaCuestionario <= comienza + 11)
                            {
                                promedio2 += item.ValorResultado;
                                contp2++;
                            }
                            if (item.Fk_Id_BateriaCuestionario >= comienza + 12 && item.Fk_Id_BateriaCuestionario <= comienza + 21)
                            {
                                promedio3 += item.ValorResultado;
                                contp3++;
                            }
                            if (item.Fk_Id_BateriaCuestionario >= comienza + 22 && item.Fk_Id_BateriaCuestionario <= comienza + 30)
                            {
                                promedio4 += item.ValorResultado;
                                contp4++;
                            }
                        }
                        promedio1 = promedio1 / contp1;
                        promedio2 = promedio2 / contp2;
                        promedio3 = promedio3 / contp3;
                        promedio4 = promedio4 / contp4;

                        promedio1 = promedio1 * 4;
                        promedio2 = promedio2 * 3;
                        promedio3 = promedio3 * 2;

                        decimal puntajetotal = promedio1 + promedio2 + promedio3 + promedio4;
                        itemUs.Puntaje = puntajetotal;
                        int cont1 = 1;
                        itemUs.PuntajeTrans = (itemUs.Puntaje / (decimal)itemUs.FactorTransformacion) * 100;
                        itemUs.Listadominios = new List<EDBateriaDominio>();
                        itemUs.Listadominios = ListaDominios;
                        decimal[,,] evalriesgoTotal = new decimal[10, 10, 10];
                        evalriesgoTotal = evalriesgo;

                        for (int i1 = 1; i1 < 6; i1++)
                        {
                            decimal CotaA = evalriesgoTotal[cont1, i1, 1];
                            decimal CotaB = evalriesgoTotal[cont1, i1, 2];
                            if (itemUs.PuntajeTrans >= evalriesgoTotal[cont1, i1, 1] && itemUs.PuntajeTrans <= evalriesgoTotal[cont1, i1, 2])
                            {
                                if (i1 == 1)
                                {
                                    itemUs.NivelRiesgoDesc = "Muy bajo";
                                }
                                if (i1 == 2)
                                {
                                    itemUs.NivelRiesgoDesc = "Bajo";
                                }
                                if (i1 == 3)
                                {
                                    itemUs.NivelRiesgoDesc = "Medio";
                                }
                                if (i1 == 4)
                                {
                                    itemUs.NivelRiesgoDesc = "Alto";
                                }
                                if (i1 == 5)
                                {
                                    itemUs.NivelRiesgoDesc = "Muy alto";
                                }
                            }
                        }
                        if (itemUs.NivelRiesgoDesc == null)
                        {
                            decimal round = Math.Round(itemUs.PuntajeTrans, 1);
                            for (int i1 = 1; i1 < 6; i1++)
                            {
                                decimal CotaA = evalriesgoTotal[cont1, i1, 1];
                                decimal CotaB = evalriesgoTotal[cont1, i1, 2];
                                if (itemUs.PuntajeTrans >= evalriesgoTotal[cont1, i1, 1] && itemUs.PuntajeTrans <= evalriesgoTotal[cont1, i1, 2])
                                {
                                    if (i1 == 1)
                                    {
                                        itemUs.NivelRiesgoDesc = "Muy bajo";
                                    }
                                    if (i1 == 2)
                                    {
                                        itemUs.NivelRiesgoDesc = "Bajo";
                                    }
                                    if (i1 == 3)
                                    {
                                        itemUs.NivelRiesgoDesc = "Medio";
                                    }
                                    if (i1 == 4)
                                    {
                                        itemUs.NivelRiesgoDesc = "Alto";
                                    }
                                    if (i1 == 5)
                                    {
                                        itemUs.NivelRiesgoDesc = "Muy alto";
                                    }
                                }
                            }
                        }

                    }
                    #endregion
                    #endregion
                }


                for (int i1 = 3; i1 < filawb; i1++)
                {
                    for (int i = 1; i < 9; i++)
                    {
                        ws0.Cells[i1, i].Style.Font.Size = 11;
                        ws0.Cells[i1, i].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        ws0.Cells[i1, i].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        ws0.Cells[i1, i].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        ws0.Cells[i1, i].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                        if (i==2 || i==4 || i==7 || i==8)
                        {
                            ws0.Cells[i1, i].Style.WrapText = true;
                        }
                    }
                    
                }

                var range = ws0.Cells[4, 1, filawb-1, 8];
                range.AutoFilter = true;

                for (int i = 5; i < filawb; i++)
                {
                    var fila = ws0.Row(i).Height = 15;
                }
                ws0.View.FreezePanes(5, 9);
                EDBateriaGestionConsulta.Informe = new EDBateriaUsuario();
                #region Dominios&Dimensiones

                List<EDBateriaDominio> ListaDominios1 = new List<EDBateriaDominio>();
                if (EDBateriaGestionConsulta.Fk_Id_Bateria == 1 && form == 0)
                {
                    ViewBag.NombreBat = "INTRALABORAL FORMA A";
                    EDBateriaGestionConsulta.Informe.NombreEncuestaTotal = "TOTAL GENERAL FACTORES DE RIESGO PSICOSOCIAL INTRALABORAL";
                    EDBateriaGestionConsulta.Informe.NombreEncuesta = "Cuestionario de Factores de Riesgo Psicosocial Intralaboral FORMA A";
                    EDBateriaGestionConsulta.Informe.FactorTransformacion = 492;
                    EDBateriaDominio EDBateriaDominio = new EDBateriaDominio();
                    EDBateriaDominio.Nombre = "Liderazgo y relaciones sociales en el trabajo";
                    EDBateriaDominio.Pk_Id_BateriaDimension = 1;
                    EDBateriaDominio.ListaDimensiones = LNBateria.ListaDimensiones(1, EDBateriaGestionConsulta.Fk_Id_Bateria);
                    EDBateriaDominio.FactorTransformacion = 164;

                    EDBateriaDominio EDBateriaDominio1 = new EDBateriaDominio();
                    EDBateriaDominio1.Nombre = "Control sobre el trabajo";
                    EDBateriaDominio1.Pk_Id_BateriaDimension = 2;
                    EDBateriaDominio1.ListaDimensiones = LNBateria.ListaDimensiones(2, EDBateriaGestionConsulta.Fk_Id_Bateria);
                    EDBateriaDominio1.FactorTransformacion = 84;

                    EDBateriaDominio EDBateriaDominio2 = new EDBateriaDominio();
                    EDBateriaDominio2.Nombre = "Demandas del trabajo";
                    EDBateriaDominio2.Pk_Id_BateriaDimension = 3;
                    EDBateriaDominio2.ListaDimensiones = LNBateria.ListaDimensiones(3, EDBateriaGestionConsulta.Fk_Id_Bateria);
                    EDBateriaDominio2.FactorTransformacion = 200;

                    EDBateriaDominio EDBateriaDominio3 = new EDBateriaDominio();
                    EDBateriaDominio3.Nombre = "Recompensas";
                    EDBateriaDominio3.Pk_Id_BateriaDimension = 4;
                    EDBateriaDominio3.ListaDimensiones = LNBateria.ListaDimensiones(4, EDBateriaGestionConsulta.Fk_Id_Bateria);
                    EDBateriaDominio3.FactorTransformacion = 44;

                    ListaDominios1.Add(EDBateriaDominio);
                    ListaDominios1.Add(EDBateriaDominio1);
                    ListaDominios1.Add(EDBateriaDominio2);
                    ListaDominios1.Add(EDBateriaDominio3);
                }
                if (EDBateriaGestionConsulta.Fk_Id_Bateria == 2 && form == 0)
                {
                    ViewBag.NombreBat = "INTRALABORAL FORMA B";
                    EDBateriaGestionConsulta.Informe.NombreEncuestaTotal = "TOTAL GENERAL FACTORES DE RIESGO PSICOSOCIAL INTRALABORAL";
                    EDBateriaGestionConsulta.Informe.NombreEncuesta = "Cuestionario de Factores de Riesgo Psicosocial Intralaboral FORMA B";
                    EDBateriaGestionConsulta.Informe.FactorTransformacion = 388;
                    EDBateriaDominio EDBateriaDominio = new EDBateriaDominio();
                    EDBateriaDominio.Nombre = "Liderazgo y relaciones sociales en el trabajo";
                    EDBateriaDominio.Pk_Id_BateriaDimension = 1;
                    EDBateriaDominio.ListaDimensiones = LNBateria.ListaDimensiones(1, EDBateriaGestionConsulta.Fk_Id_Bateria);
                    EDBateriaDominio.FactorTransformacion = 120;

                    EDBateriaDominio EDBateriaDominio1 = new EDBateriaDominio();
                    EDBateriaDominio1.Nombre = "Control sobre el trabajo";
                    EDBateriaDominio1.Pk_Id_BateriaDimension = 2;
                    EDBateriaDominio1.ListaDimensiones = LNBateria.ListaDimensiones(2, EDBateriaGestionConsulta.Fk_Id_Bateria);
                    EDBateriaDominio1.FactorTransformacion = 72;

                    EDBateriaDominio EDBateriaDominio2 = new EDBateriaDominio();
                    EDBateriaDominio2.Nombre = "Demandas del trabajo";
                    EDBateriaDominio2.Pk_Id_BateriaDimension = 3;
                    EDBateriaDominio2.ListaDimensiones = LNBateria.ListaDimensiones(3, EDBateriaGestionConsulta.Fk_Id_Bateria);
                    EDBateriaDominio2.FactorTransformacion = 156;

                    EDBateriaDominio EDBateriaDominio3 = new EDBateriaDominio();
                    EDBateriaDominio3.Nombre = "Recompensas";
                    EDBateriaDominio3.Pk_Id_BateriaDimension = 4;
                    EDBateriaDominio3.ListaDimensiones = LNBateria.ListaDimensiones(4, EDBateriaGestionConsulta.Fk_Id_Bateria);
                    EDBateriaDominio3.FactorTransformacion = 40;

                    ListaDominios1.Add(EDBateriaDominio);
                    ListaDominios1.Add(EDBateriaDominio1);
                    ListaDominios1.Add(EDBateriaDominio2);
                    ListaDominios1.Add(EDBateriaDominio3);

                }
                if (EDBateriaGestionConsulta.Fk_Id_Bateria == 1 && form == 1)
                {
                    ViewBag.NombreBat = "EXTRALABORAL";
                    EDBateriaGestionConsulta.Informe.NombreEncuestaTotal = "TOTAL GENERAL FACTORES DE RIESGO PSICOSOCIAL EXTRALABORAL";
                    EDBateriaGestionConsulta.Informe.NombreEncuesta = "Cuestionario de Factores de Riesgo Psicosocial Extralaboral";
                    EDBateriaGestionConsulta.Informe.FactorTransformacion = 124;
                    EDBateriaDominio EDBateriaDominio = new EDBateriaDominio();
                    EDBateriaDominio.Nombre = "N/A";
                    EDBateriaDominio.Pk_Id_BateriaDimension = 1;
                    EDBateriaDominio.ListaDimensiones = LNBateria.ListaDimensiones(1, 3);
                    EDBateriaDominio.FactorTransformacion = 1;

                    ListaDominios1.Add(EDBateriaDominio);
                }
                if (EDBateriaGestionConsulta.Fk_Id_Bateria == 2 && form == 1)
                {
                    ViewBag.NombreBat = "EXTRALABORAL";
                    EDBateriaGestionConsulta.Informe.NombreEncuestaTotal = "TOTAL GENERAL FACTORES DE RIESGO PSICOSOCIAL EXTRALABORAL";
                    EDBateriaGestionConsulta.Informe.NombreEncuesta = "Cuestionario de Factores de Riesgo Psicosocial Extralaboral";
                    EDBateriaGestionConsulta.Informe.FactorTransformacion = 124;
                    EDBateriaDominio EDBateriaDominio = new EDBateriaDominio();
                    EDBateriaDominio.Nombre = "N/A";
                    EDBateriaDominio.Pk_Id_BateriaDimension = 1;
                    EDBateriaDominio.ListaDimensiones = LNBateria.ListaDimensiones(1, 3);
                    EDBateriaDominio.FactorTransformacion = 1;

                    ListaDominios1.Add(EDBateriaDominio);
                }
                if (EDBateriaGestionConsulta.Fk_Id_Bateria == 4)
                {
                    ViewBag.NombreBat = "FACTORES DE ESTRÉS";
                    EDBateriaGestionConsulta.Informe.NombreEncuestaTotal = "TOTAL GENERAL SÍNTOMAS DE ESTRÉS";
                    EDBateriaGestionConsulta.Informe.NombreEncuesta = "Cuestionario de Factores de Estrés";
                    EDBateriaGestionConsulta.Informe.FactorTransformacion = 61.16;
                    EDBateriaDominio EDBateriaDominio = new EDBateriaDominio();
                    EDBateriaDominio.Nombre = "N/A";
                    EDBateriaDominio.Pk_Id_BateriaDimension = 1;
                    EDBateriaDominio.ListaDimensiones = LNBateria.ListaDimensiones(1, 4);
                    EDBateriaDominio.FactorTransformacion = 1;

                    ListaDominios1.Add(EDBateriaDominio);
                }
                #endregion
                EDBateriaGestionConsulta.Informe.Listadominios = ListaDominios1;
                if (contTer != 0)
                {
                    foreach (var item in EDBateriaGestionConsulta.Informe.Listadominios)
                    {
                        int IdDominio = item.Pk_Id_BateriaDimension;
                        foreach (var item1 in EDBateriaGestionConsulta.ListaUsuarios)
                        {
                            EDBateriaDimension EDBateriaDimension = new EDBateriaDimension();
                            var dominio = item1.Listadominios.Where(s => s.Pk_Id_BateriaDimension == IdDominio).FirstOrDefault();
                            if (dominio != null)
                            {
                                //grabar factor transformado
                                item.PuntajeTrans += dominio.PuntajeTrans / contTer;
                                var dimensiones = dominio.ListaDimensiones.ToList();
                                foreach (var item2 in dimensiones)
                                {
                                    int Iddim = item2.Pk_Id_BateriaDimension;
                                    var dimension = item.ListaDimensiones.Where(s => s.Pk_Id_BateriaDimension == Iddim).FirstOrDefault();
                                    if (dimension != null)
                                    {
                                        dimension.PuntajeTrans += item2.PuntajeTrans / contTer;
                                    }
                                }
                            }
                        }
                    }
                }
                foreach (var item in EDBateriaGestionConsulta.ListaUsuarios)
                {
                    EDBateriaGestionConsulta.Informe.PuntajeTrans += item.PuntajeTrans / contTer;
                }


                EDBateriaGestion = EDBateriaGestionConsulta;
                if (form == 0)
                {
                    ViewBag.form = "1";
                }
                if (form == 1)
                {
                    ViewBag.form = "0";
                }
                ViewBag.bateriaextra = false;
                if (EDBateriaGestion.bateriaExtra == 3)
                {
                    ViewBag.bateriaextra = true;
                }
                ws0.Cells[3, 1].Value = "RESULTADO " + nombrePrincipal.ToUpper();
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    string Fecha = DateTime.Now.ToString().Replace(" ", "").Replace(".", "").Replace("/", "").Replace(":", "");
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "AlisstaResultadoCuestionario" + Fecha + ".xlsx");
                }
            }


            return null;
        }

        #endregion
        #region resultados
        [HttpGet]
        public ActionResult ResultadosAplicacion(string IdUsuario)
        {
            bool mostrarPDF = true;
            ViewBag.IdGestion = "";
            ViewBag.FechaRegistro = "";
            ViewBag.Nombre = "";
            ViewBag.NombrePersona = "";
            ViewBag.Cedula = "";
            ViewBag.FechaDiligenciamiento = "";
            ViewBag.NombreEmpresa = "";
            ViewBag.Edad = "";
            


            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }
            List<EDBateria> ListaBaterias = LNBateria.ConsultarBaterias();
            EDBateriaUsuario EDBateriaUsuario = new EDBateriaUsuario();

            if (IdUsuario!=null)
            {
                int IdUsuarioInt = 0;
                if (int.TryParse(IdUsuario, out IdUsuarioInt))
                {
                    EDBateriaUsuario = LNBateria.ConsultarConvocadoId(IdUsuarioInt, usuarioActual.IdEmpresa);

                    if (EDBateriaUsuario.Edad!=null)
                    {
                        ViewBag.Edad = EDBateriaUsuario.Edad;
                    }
                    else
                    {
                        int año = EDBateriaUsuario.BateriaInicial.AñoNac;
                        DateTime Fecha = new DateTime(año, 1, 1);
                        DateTime Fecha1 = new DateTime(año, 12, 31);


                        int edad1 = DateTime.Today.AddTicks(-Fecha.Ticks).Year - 1;
                        int edad2 = DateTime.Today.AddTicks(-Fecha1.Ticks).Year - 1;

                        ViewBag.Edad = edad1.ToString() + " años";
                    }

                    int fkIdGestion = EDBateriaUsuario.Fk_Id_BateriaGestion;
                    EDBateriaGestion EDBateriaGestion = LNBateria.ConsultarGestion(fkIdGestion, usuarioActual.IdEmpresa);
                    ViewBag.NombrePersona = EDBateriaUsuario.Nombre;
                    ViewBag.Cedula = EDBateriaUsuario.NumeroIdentificacion;
                    DateTime fecharespuesta = EDBateriaUsuario.FechaRespuesta ?? DateTime.MinValue;
                    ViewBag.FechaDiligenciamiento = fecharespuesta.ToShortDateString();
                    ViewBag.IdGestion = EDBateriaGestion.Pk_Id_BateriaGestion.ToString();
                    ViewBag.FechaRegistro = EDBateriaGestion.FechaRegistro.ToShortDateString();
                    ViewBag.Nombre = ListaBaterias.Where(s => s.Pk_Id_Bateria == EDBateriaGestion.Fk_Id_Bateria).FirstOrDefault().Nombre;
                    ViewBag.NombreEmpresa = usuarioActual.RazonSocialEmpresa;

                    if (EDBateriaUsuario.Edad == null)
                    {
                        mostrarPDF = false;
                    }
                    if (EDBateriaUsuario.NombreEvaluador == null)
                    {
                        mostrarPDF = false;
                    }
                    if (EDBateriaUsuario.IdEvaluador == null)
                    {
                        mostrarPDF = false;
                    }
                    if (EDBateriaUsuario.Profesion == null)
                    {
                        mostrarPDF = false;
                    }
                    if (EDBateriaUsuario.Postgrado == null)
                    {
                        mostrarPDF = false;
                    }
                    if (EDBateriaUsuario.TarjetaProfesional == null)
                    {
                        mostrarPDF = false;
                    }
                    if (EDBateriaUsuario.Licencia == null)
                    {
                        mostrarPDF = false;
                    }
                    if (EDBateriaUsuario.FechaExpedicion == null)
                    {
                        mostrarPDF = false;
                    }

                    #region CalculoResultados 
                    EDBateriaUsuario.ListaResultados = LNBateria.ListaResultados(IdUsuarioInt);
                    List<EDBateriaDominio> ListaDominios = new List<EDBateriaDominio>();
                    if (EDBateriaGestion.Fk_Id_Bateria==1 && EDBateriaUsuario.NumeroIntentos==0)
                    {
                        EDBateriaUsuario.NombreEncuestaTotal = "TOTAL GENERAL FACTORES DE RIESGO PSICOSOCIAL INTRALABORAL";
                        EDBateriaUsuario.NombreEncuesta = "Cuestionario de Factores de Riesgo Psicosocial Intralaboral FORMA A";
                        EDBateriaUsuario.FactorTransformacion = 492;
                        EDBateriaDominio EDBateriaDominio = new EDBateriaDominio();
                        EDBateriaDominio.Nombre = "Liderazgo y relaciones sociales en el trabajo";
                        EDBateriaDominio.Pk_Id_BateriaDimension = 1;
                        EDBateriaDominio.ListaDimensiones = LNBateria.ListaDimensiones(1, EDBateriaGestion.Fk_Id_Bateria);
                        EDBateriaDominio.FactorTransformacion = 164;

                        EDBateriaDominio EDBateriaDominio1 = new EDBateriaDominio();
                        EDBateriaDominio1.Nombre = "Control sobre el trabajo";
                        EDBateriaDominio1.Pk_Id_BateriaDimension = 2;
                        EDBateriaDominio1.ListaDimensiones = LNBateria.ListaDimensiones(2, EDBateriaGestion.Fk_Id_Bateria);
                        EDBateriaDominio1.FactorTransformacion = 84;

                        EDBateriaDominio EDBateriaDominio2 = new EDBateriaDominio();
                        EDBateriaDominio2.Nombre = "Demandas del trabajo";
                        EDBateriaDominio2.Pk_Id_BateriaDimension = 3;
                        EDBateriaDominio2.ListaDimensiones = LNBateria.ListaDimensiones(3, EDBateriaGestion.Fk_Id_Bateria);
                        EDBateriaDominio2.FactorTransformacion = 200;

                        EDBateriaDominio EDBateriaDominio3 = new EDBateriaDominio();
                        EDBateriaDominio3.Nombre = "Recompensas";
                        EDBateriaDominio3.Pk_Id_BateriaDimension = 4;
                        EDBateriaDominio3.ListaDimensiones = LNBateria.ListaDimensiones(4, EDBateriaGestion.Fk_Id_Bateria);
                        EDBateriaDominio3.FactorTransformacion = 44;


                        ListaDominios.Add(EDBateriaDominio);
                        ListaDominios.Add(EDBateriaDominio1);
                        ListaDominios.Add(EDBateriaDominio2);
                        ListaDominios.Add(EDBateriaDominio3);
                    }
                    if (EDBateriaGestion.Fk_Id_Bateria == 2 && EDBateriaUsuario.NumeroIntentos == 0)
                    {
                        EDBateriaUsuario.NombreEncuestaTotal = "TOTAL GENERAL FACTORES DE RIESGO PSICOSOCIAL INTRALABORAL";
                        EDBateriaUsuario.NombreEncuesta = "Cuestionario de Factores de Riesgo Psicosocial Intralaboral FORMA B";
                        EDBateriaUsuario.FactorTransformacion = 388;
                        EDBateriaDominio EDBateriaDominio = new EDBateriaDominio();
                        EDBateriaDominio.Nombre = "Liderazgo y relaciones sociales en el trabajo";
                        EDBateriaDominio.Pk_Id_BateriaDimension = 1;
                        EDBateriaDominio.ListaDimensiones = LNBateria.ListaDimensiones(1, EDBateriaGestion.Fk_Id_Bateria);
                        EDBateriaDominio.FactorTransformacion = 120;

                        EDBateriaDominio EDBateriaDominio1 = new EDBateriaDominio();
                        EDBateriaDominio1.Nombre = "Control sobre el trabajo";
                        EDBateriaDominio1.Pk_Id_BateriaDimension = 2;
                        EDBateriaDominio1.ListaDimensiones = LNBateria.ListaDimensiones(2, EDBateriaGestion.Fk_Id_Bateria);
                        EDBateriaDominio1.FactorTransformacion = 72;

                        EDBateriaDominio EDBateriaDominio2 = new EDBateriaDominio();
                        EDBateriaDominio2.Nombre = "Demandas del trabajo";
                        EDBateriaDominio2.Pk_Id_BateriaDimension = 3;
                        EDBateriaDominio2.ListaDimensiones = LNBateria.ListaDimensiones(3, EDBateriaGestion.Fk_Id_Bateria);
                        EDBateriaDominio2.FactorTransformacion = 156;

                        EDBateriaDominio EDBateriaDominio3 = new EDBateriaDominio();
                        EDBateriaDominio3.Nombre = "Recompensas";
                        EDBateriaDominio3.Pk_Id_BateriaDimension = 4;
                        EDBateriaDominio3.ListaDimensiones = LNBateria.ListaDimensiones(4, EDBateriaGestion.Fk_Id_Bateria);
                        EDBateriaDominio3.FactorTransformacion = 40;

                        ListaDominios.Add(EDBateriaDominio);
                        ListaDominios.Add(EDBateriaDominio1);
                        ListaDominios.Add(EDBateriaDominio2);
                        ListaDominios.Add(EDBateriaDominio3);

                    }
                    if (EDBateriaGestion.Fk_Id_Bateria == 1 && EDBateriaUsuario.NumeroIntentos == 1)
                    {
                        EDBateriaUsuario.NombreEncuestaTotal = "TOTAL GENERAL FACTORES DE RIESGO PSICOSOCIAL EXTRALABORAL";
                        EDBateriaUsuario.NombreEncuesta = "Cuestionario de Factores de Riesgo Psicosocial Extralaboral";
                        EDBateriaUsuario.FactorTransformacion = 124;
                        EDBateriaDominio EDBateriaDominio = new EDBateriaDominio();
                        EDBateriaDominio.Nombre = "N/A";
                        EDBateriaDominio.Pk_Id_BateriaDimension = 1;
                        EDBateriaDominio.ListaDimensiones = LNBateria.ListaDimensiones(1, 3);
                        EDBateriaDominio.FactorTransformacion = 1;

                        ListaDominios.Add(EDBateriaDominio);
                    }
                    if (EDBateriaGestion.Fk_Id_Bateria == 2 && EDBateriaUsuario.NumeroIntentos == 1)
                    {
                        EDBateriaUsuario.NombreEncuestaTotal = "TOTAL GENERAL FACTORES DE RIESGO PSICOSOCIAL EXTRALABORAL";
                        EDBateriaUsuario.NombreEncuesta = "Cuestionario de Factores de Riesgo Psicosocial Extralaboral";
                        EDBateriaUsuario.FactorTransformacion = 124;
                        EDBateriaDominio EDBateriaDominio = new EDBateriaDominio();
                        EDBateriaDominio.Nombre = "N/A";
                        EDBateriaDominio.Pk_Id_BateriaDimension = 1;
                        EDBateriaDominio.ListaDimensiones = LNBateria.ListaDimensiones(1, 3);
                        EDBateriaDominio.FactorTransformacion = 1;

                        ListaDominios.Add(EDBateriaDominio);
                    }
                    if (EDBateriaGestion.Fk_Id_Bateria == 4)
                    {
                        EDBateriaUsuario.NombreEncuestaTotal = "TOTAL GENERAL SÍNTOMAS DE ESTRÉS";
                        EDBateriaUsuario.NombreEncuesta = "Cuestionario de Factores de Estrés";
                        EDBateriaUsuario.FactorTransformacion = 61.16;
                        EDBateriaDominio EDBateriaDominio = new EDBateriaDominio();
                        EDBateriaDominio.Nombre = "N/A";
                        EDBateriaDominio.Pk_Id_BateriaDimension = 1;
                        EDBateriaDominio.ListaDimensiones = LNBateria.ListaDimensiones(1, 4);
                        EDBateriaDominio.FactorTransformacion = 1;

                        ListaDominios.Add(EDBateriaDominio);
                    }

                    #region ResultadosIntraFormA

                    
                    if (EDBateriaGestion.Fk_Id_Bateria == 1 && EDBateriaUsuario.NumeroIntentos == 0)
                    {
                        decimal[,,] evalriesgo = baremosIntraA();
                        decimal[,,] evalriesgoDom = baremosDominioIntraA();
                        foreach (var item in EDBateriaUsuario.ListaResultados)
                        {
                            int iddom = item.DominioInt;
                            int iddim = item.DimensionInt;

                            EDBateriaDominio EDBateriaDominio = ListaDominios.Where(s => s.Pk_Id_BateriaDimension == iddom).FirstOrDefault();
                            List<EDBateriaDimension> Listas = EDBateriaDominio.ListaDimensiones;
                            EDBateriaDimension EDBateriaDimension = Listas.Where(s => s.Pk_Id_BateriaDimension == iddim).FirstOrDefault();

                            EDBateriaDominio.Puntaje += item.ValorResultado;
                            EDBateriaDimension.Puntaje += item.ValorResultado;
                            EDBateriaUsuario.Puntaje += item.ValorResultado;
                        }
                        int cont = 1;
                        int cont1 = 1;
                        foreach (var item in ListaDominios)
                        {
                            item.PuntajeTrans = (item.Puntaje/(decimal)item.FactorTransformacion) * 100;
                            for (int i1 = 1; i1 < 6; i1++)
                            {
                                decimal CotaA = evalriesgoDom[cont1, i1, 1];
                                decimal CotaB = evalriesgoDom[cont1, i1, 2];
                                if (item.PuntajeTrans >= evalriesgoDom[cont1, i1, 1] && item.PuntajeTrans <= evalriesgoDom[cont1, i1, 2])
                                {
                                    if (i1 == 1)
                                    {
                                        item.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                    }
                                    if (i1 == 2)
                                    {
                                        item.NivelRiesgoDesc = "Riesgo bajo";
                                    }
                                    if (i1 == 3)
                                    {
                                        item.NivelRiesgoDesc = "Riesgo medio";
                                    }
                                    if (i1 == 4)
                                    {
                                        item.NivelRiesgoDesc = "Riesgo alto";
                                    }
                                    if (i1 == 5)
                                    {
                                        item.NivelRiesgoDesc = "Riesgo muy alto";
                                    }
                                }
                            }
                            if (item.NivelRiesgoDesc == null)
                            {
                                decimal round = Math.Round(item.PuntajeTrans, 1);
                                for (int i1 = 1; i1 < 6; i1++)
                                {
                                    decimal CotaA = evalriesgoDom[cont1, i1, 1];
                                    decimal CotaB = evalriesgoDom[cont1, i1, 2];
                                    if (round >= CotaA && round <= CotaB)
                                    {
                                        if (i1 == 1)
                                        {
                                            item.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                        }
                                        if (i1 == 2)
                                        {
                                            item.NivelRiesgoDesc = "Riesgo bajo";
                                        }
                                        if (i1 == 3)
                                        {
                                            item.NivelRiesgoDesc = "Riesgo medio";
                                        }
                                        if (i1 == 4)
                                        {
                                            item.NivelRiesgoDesc = "Riesgo alto";
                                        }
                                        if (i1 == 5)
                                        {
                                            item.NivelRiesgoDesc = "Riesgo muy alto";
                                        }
                                    }
                                }
                            }


                            foreach (var item1 in item.ListaDimensiones)
                            {
                                item1.PuntajeTrans = (item1.Puntaje / (decimal)item1.FactorTransformacion) * 100;

                                for (int i1 = 1; i1 < 6; i1++)
                                {
                                    decimal CotaA = evalriesgo[cont, i1, 1];
                                    decimal CotaB = evalriesgo[cont, i1, 2];
                                    if (item1.PuntajeTrans>= evalriesgo[cont, i1, 1] && item1.PuntajeTrans <= evalriesgo[cont, i1, 2])
                                    {
                                        if (i1==1)
                                        {
                                            item1.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                        }
                                        if (i1 == 2)
                                        {
                                            item1.NivelRiesgoDesc = "Riesgo bajo";
                                        }
                                        if (i1 == 3)
                                        {
                                            item1.NivelRiesgoDesc = "Riesgo medio";
                                        }
                                        if (i1 == 4)
                                        {
                                            item1.NivelRiesgoDesc = "Riesgo alto";
                                        }
                                        if (i1 == 5)
                                        {
                                            item1.NivelRiesgoDesc = "Riesgo muy alto";
                                        }
                                    }
                                }
                                if (item1.NivelRiesgoDesc == null)
                                {
                                    decimal round = Math.Round(item1.PuntajeTrans, 1);
                                    for (int i1 = 1; i1 < 6; i1++)
                                    {
                                        decimal CotaA = evalriesgo[cont, i1, 1];
                                        decimal CotaB = evalriesgo[cont, i1, 2];
                                        if (round >= CotaA && round <= CotaB)
                                        {
                                            if (i1 == 1)
                                            {
                                                item1.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                            }
                                            if (i1 == 2)
                                            {
                                                item1.NivelRiesgoDesc = "Riesgo bajo";
                                            }
                                            if (i1 == 3)
                                            {
                                                item1.NivelRiesgoDesc = "Riesgo medio";
                                            }
                                            if (i1 == 4)
                                            {
                                                item1.NivelRiesgoDesc = "Riesgo alto";
                                            }
                                            if (i1 == 5)
                                            {
                                                item1.NivelRiesgoDesc = "Riesgo muy alto";
                                            }
                                        }
                                    }
                                }
                                cont++;
                            }
                            cont1++;
                        }
                        cont1 = 1;
                        EDBateriaUsuario.PuntajeTrans = (EDBateriaUsuario.Puntaje / (decimal)EDBateriaUsuario.FactorTransformacion) * 100;
                        EDBateriaUsuario.Listadominios = new List<EDBateriaDominio>();
                        EDBateriaUsuario.Listadominios = ListaDominios;
                        decimal[,,] evalriesgoTotal = new decimal[10,10,10];

                        string formdata = EDBateriaUsuario.TokenPrivado;

                        EDBateriaUsuario EDBateriaUsuarioExtra = LNBateria.ConsultarConvocadoKeyExtra(formdata,1);
                        if (EDBateriaUsuarioExtra!=null)
                        {
                            if (EDBateriaUsuarioExtra.Pk_Id_BateriaUsuario==0)
                            {
                                evalriesgoTotal = baremosTotalIntraA();
                            }
                            else
                            {
                                if (EDBateriaUsuarioExtra.RegistroOperacion=="Fin")
                                {
                                    evalriesgoTotal = baremosTotalExtraIntraA();
                                }
                                else
                                {
                                    evalriesgoTotal = baremosTotalIntraA();
                                }
                            }
                        }
                        else
                        {
                            evalriesgoTotal = baremosTotalIntraA();
                        }
                        
                        for (int i1 = 1; i1 < 6; i1++)
                        {
                            decimal CotaA = evalriesgoTotal[cont1, i1, 1];
                            decimal CotaB = evalriesgoTotal[cont1, i1, 2];
                            if (EDBateriaUsuario.PuntajeTrans >= evalriesgoTotal[cont1, i1, 1] && EDBateriaUsuario.PuntajeTrans <= evalriesgoTotal[cont1, i1, 2])
                            {
                                if (i1 == 1)
                                {
                                    EDBateriaUsuario.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                }
                                if (i1 == 2)
                                {
                                    EDBateriaUsuario.NivelRiesgoDesc = "Riesgo bajo";
                                }
                                if (i1 == 3)
                                {
                                    EDBateriaUsuario.NivelRiesgoDesc = "Riesgo medio";
                                }
                                if (i1 == 4)
                                {
                                    EDBateriaUsuario.NivelRiesgoDesc = "Riesgo alto";
                                }
                                if (i1 == 5)
                                {
                                    EDBateriaUsuario.NivelRiesgoDesc = "Riesgo muy alto";
                                }
                            }
                        }
                        if (EDBateriaUsuario.NivelRiesgoDesc == null)
                        {
                            decimal round = Math.Round(EDBateriaUsuario.PuntajeTrans, 1);
                            for (int i1 = 1; i1 < 6; i1++)
                            {
                                decimal CotaA = evalriesgoTotal[cont, i1, 1];
                                decimal CotaB = evalriesgoTotal[cont, i1, 2];
                                if (round >= CotaA && round <= CotaB)
                                {
                                    if (i1 == 1)
                                    {
                                        EDBateriaUsuario.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                    }
                                    if (i1 == 2)
                                    {
                                        EDBateriaUsuario.NivelRiesgoDesc = "Riesgo bajo";
                                    }
                                    if (i1 == 3)
                                    {
                                        EDBateriaUsuario.NivelRiesgoDesc = "Riesgo medio";
                                    }
                                    if (i1 == 4)
                                    {
                                        EDBateriaUsuario.NivelRiesgoDesc = "Riesgo alto";
                                    }
                                    if (i1 == 5)
                                    {
                                        EDBateriaUsuario.NivelRiesgoDesc = "Riesgo muy alto";
                                    }
                                }
                            }
                        }

                    }
                    #endregion
                    #region ResultadosIntraFormB


                    if (EDBateriaGestion.Fk_Id_Bateria == 2 && EDBateriaUsuario.NumeroIntentos == 0)
                    {
                        decimal[,,] evalriesgo = baremosIntraB();
                        decimal[,,] evalriesgoDom = baremosDominioIntraB();
                        foreach (var item in EDBateriaUsuario.ListaResultados)
                        {
                            int iddom = item.DominioInt;
                            int iddim = item.DimensionInt;

                            EDBateriaDominio EDBateriaDominio = ListaDominios.Where(s => s.Pk_Id_BateriaDimension == iddom).FirstOrDefault();
                            List<EDBateriaDimension> Listas = EDBateriaDominio.ListaDimensiones;
                            EDBateriaDimension EDBateriaDimension = Listas.Where(s => s.Pk_Id_BateriaDimension == iddim).FirstOrDefault();

                            EDBateriaDominio.Puntaje += item.ValorResultado;
                            EDBateriaDimension.Puntaje += item.ValorResultado;
                            EDBateriaUsuario.Puntaje += item.ValorResultado;
                        }
                        int cont = 1;
                        int cont1 = 1;
                        foreach (var item in ListaDominios)
                        {
                            item.PuntajeTrans = (item.Puntaje / (decimal)item.FactorTransformacion) * 100;
                            for (int i1 = 1; i1 < 6; i1++)
                            {
                                decimal CotaA = evalriesgoDom[cont1, i1, 1];
                                decimal CotaB = evalriesgoDom[cont1, i1, 2];
                                if (item.PuntajeTrans >= evalriesgoDom[cont1, i1, 1] && item.PuntajeTrans <= evalriesgoDom[cont1, i1, 2])
                                {
                                    if (i1 == 1)
                                    {
                                        item.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                    }
                                    if (i1 == 2)
                                    {
                                        item.NivelRiesgoDesc = "Riesgo bajo";
                                    }
                                    if (i1 == 3)
                                    {
                                        item.NivelRiesgoDesc = "Riesgo medio";
                                    }
                                    if (i1 == 4)
                                    {
                                        item.NivelRiesgoDesc = "Riesgo alto";
                                    }
                                    if (i1 == 5)
                                    {
                                        item.NivelRiesgoDesc = "Riesgo muy alto";
                                    }
                                }
                            }
                            if (item.NivelRiesgoDesc == null)
                            {
                                decimal round = Math.Round(item.PuntajeTrans, 1);
                                for (int i1 = 1; i1 < 6; i1++)
                                {
                                    decimal CotaA = evalriesgoDom[cont1, i1, 1];
                                    decimal CotaB = evalriesgoDom[cont1, i1, 2];
                                    if (round >= CotaA && round <= CotaB)
                                    {
                                        if (i1 == 1)
                                        {
                                            item.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                        }
                                        if (i1 == 2)
                                        {
                                            item.NivelRiesgoDesc = "Riesgo bajo";
                                        }
                                        if (i1 == 3)
                                        {
                                            item.NivelRiesgoDesc = "Riesgo medio";
                                        }
                                        if (i1 == 4)
                                        {
                                            item.NivelRiesgoDesc = "Riesgo alto";
                                        }
                                        if (i1 == 5)
                                        {
                                            item.NivelRiesgoDesc = "Riesgo muy alto";
                                        }
                                    }
                                }
                            }


                            foreach (var item1 in item.ListaDimensiones)
                            {
                                item1.PuntajeTrans = (item1.Puntaje / (decimal)item1.FactorTransformacion) * 100;

                                for (int i1 = 1; i1 < 6; i1++)
                                {
                                    decimal CotaA = evalriesgo[cont, i1, 1];
                                    decimal CotaB = evalriesgo[cont, i1, 2];
                                    if (item1.PuntajeTrans >= CotaA && item1.PuntajeTrans <= CotaB)
                                    {
                                        if (i1 == 1)
                                        {
                                            item1.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                        }
                                        if (i1 == 2)
                                        {
                                            item1.NivelRiesgoDesc = "Riesgo bajo";
                                        }
                                        if (i1 == 3)
                                        {
                                            item1.NivelRiesgoDesc = "Riesgo medio";
                                        }
                                        if (i1 == 4)
                                        {
                                            item1.NivelRiesgoDesc = "Riesgo alto";
                                        }
                                        if (i1 == 5)
                                        {
                                            item1.NivelRiesgoDesc = "Riesgo muy alto";
                                        }
                                    }
                                }
                                if (item1.NivelRiesgoDesc==null)
                                {
                                    decimal round= Math.Round(item1.PuntajeTrans, 1);
                                    for (int i1 = 1; i1 < 6; i1++)
                                    {
                                        decimal CotaA = evalriesgo[cont, i1, 1];
                                        decimal CotaB = evalriesgo[cont, i1, 2];
                                        if (round >= CotaA && round <= CotaB)
                                        {
                                            if (i1 == 1)
                                            {
                                                item1.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                            }
                                            if (i1 == 2)
                                            {
                                                item1.NivelRiesgoDesc = "Riesgo bajo";
                                            }
                                            if (i1 == 3)
                                            {
                                                item1.NivelRiesgoDesc = "Riesgo medio";
                                            }
                                            if (i1 == 4)
                                            {
                                                item1.NivelRiesgoDesc = "Riesgo alto";
                                            }
                                            if (i1 == 5)
                                            {
                                                item1.NivelRiesgoDesc = "Riesgo muy alto";
                                            }
                                        }
                                    }
                                }
                                cont++;



                            }
                            cont1++;
                        }
                        cont1 = 1;
                        EDBateriaUsuario.PuntajeTrans = (EDBateriaUsuario.Puntaje / (decimal)EDBateriaUsuario.FactorTransformacion) * 100;
                        EDBateriaUsuario.Listadominios = new List<EDBateriaDominio>();
                        EDBateriaUsuario.Listadominios = ListaDominios;
                        decimal[,,] evalriesgoTotal = new decimal[10, 10, 10];

                        string formdata = EDBateriaUsuario.TokenPrivado;

                        EDBateriaUsuario EDBateriaUsuarioExtra = LNBateria.ConsultarConvocadoKeyExtra(formdata, 1);
                        if (EDBateriaUsuarioExtra != null)
                        {
                            if (EDBateriaUsuarioExtra.Pk_Id_BateriaUsuario == 0)
                            {
                                evalriesgoTotal = baremosTotalIntraB();
                            }
                            else
                            {
                                if (EDBateriaUsuarioExtra.RegistroOperacion == "Fin")
                                {
                                    evalriesgoTotal = baremosTotalExtraIntraB();
                                }
                                else
                                {
                                    evalriesgoTotal = baremosTotalIntraB();
                                }
                            }
                        }
                        else
                        {
                            evalriesgoTotal = baremosTotalIntraB();
                        }

                        for (int i1 = 1; i1 < 6; i1++)
                        {
                            decimal CotaA = evalriesgoTotal[cont1, i1, 1];
                            decimal CotaB = evalriesgoTotal[cont1, i1, 2];
                            if (EDBateriaUsuario.PuntajeTrans >= evalriesgoTotal[cont1, i1, 1] && EDBateriaUsuario.PuntajeTrans <= evalriesgoTotal[cont1, i1, 2])
                            {
                                if (i1 == 1)
                                {
                                    EDBateriaUsuario.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                }
                                if (i1 == 2)
                                {
                                    EDBateriaUsuario.NivelRiesgoDesc = "Riesgo bajo";
                                }
                                if (i1 == 3)
                                {
                                    EDBateriaUsuario.NivelRiesgoDesc = "Riesgo medio";
                                }
                                if (i1 == 4)
                                {
                                    EDBateriaUsuario.NivelRiesgoDesc = "Riesgo alto";
                                }
                                if (i1 == 5)
                                {
                                    EDBateriaUsuario.NivelRiesgoDesc = "Riesgo muy alto";
                                }
                            }
                        }
                        if (EDBateriaUsuario.NivelRiesgoDesc == null)
                        {
                            decimal round = Math.Round(EDBateriaUsuario.PuntajeTrans, 1);
                            for (int i1 = 1; i1 < 6; i1++)
                            {
                                decimal CotaA = evalriesgoTotal[cont, i1, 1];
                                decimal CotaB = evalriesgoTotal[cont, i1, 2];
                                if (round >= CotaA && round <= CotaB)
                                {
                                    if (i1 == 1)
                                    {
                                        EDBateriaUsuario.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                    }
                                    if (i1 == 2)
                                    {
                                        EDBateriaUsuario.NivelRiesgoDesc = "Riesgo bajo";
                                    }
                                    if (i1 == 3)
                                    {
                                        EDBateriaUsuario.NivelRiesgoDesc = "Riesgo medio";
                                    }
                                    if (i1 == 4)
                                    {
                                        EDBateriaUsuario.NivelRiesgoDesc = "Riesgo alto";
                                    }
                                    if (i1 == 5)
                                    {
                                        EDBateriaUsuario.NivelRiesgoDesc = "Riesgo muy alto";
                                    }
                                }
                            }
                        }

                    }
                    #endregion
                    #region ResultadosextraFormA


                    if (EDBateriaGestion.Fk_Id_Bateria == 1 && EDBateriaUsuario.NumeroIntentos == 1)
                    {
                        int tipo = 0;
                        decimal[,,] evalriesgo = new decimal[10, 10, 10];
                        decimal[,,] evalriesgoDom = new decimal[10, 10, 10];
                        if (EDBateriaUsuario.BateriaInicial.TipoCargo== "Jefatura - tiene personal a cargo" || EDBateriaUsuario.BateriaInicial.TipoCargo == "Profesional, analista, técnico, tecnólogo")
                        {
                            tipo = 1;
                            evalriesgo = baremosExtra1();
                            evalriesgoDom = baremosExtra1();
                        }
                        if (EDBateriaUsuario.BateriaInicial.TipoCargo == "Auxiliar, asistente administrativo, asistente técnico" || EDBateriaUsuario.BateriaInicial.TipoCargo == "Operario, operador, ayudante, servicios generales")
                        {
                            tipo = 2;
                            evalriesgo = baremosExtra2();
                            evalriesgoDom = baremosExtra2();
                        }
                        foreach (var item in EDBateriaUsuario.ListaResultados)
                        {
                            int iddom = item.DominioInt;
                            int iddim = item.DimensionInt;

                            EDBateriaDominio EDBateriaDominio = ListaDominios.Where(s => s.Pk_Id_BateriaDimension == iddom).FirstOrDefault();
                            List<EDBateriaDimension> Listas = EDBateriaDominio.ListaDimensiones;
                            EDBateriaDimension EDBateriaDimension = Listas.Where(s => s.Pk_Id_BateriaDimension == iddim).FirstOrDefault();

                            EDBateriaDominio.Puntaje += item.ValorResultado;
                            EDBateriaDimension.Puntaje += item.ValorResultado;
                            EDBateriaUsuario.Puntaje += item.ValorResultado;
                        }
                        int cont = 1;
                        int cont1 = 1;
                        foreach (var item in ListaDominios)
                        {
                            item.PuntajeTrans = (item.Puntaje / (decimal)item.FactorTransformacion) * 100;
                            for (int i1 = 1; i1 < 6; i1++)
                            {
                                decimal CotaA = evalriesgoDom[cont1, i1, 1];
                                decimal CotaB = evalriesgoDom[cont1, i1, 2];
                                if (item.PuntajeTrans >= evalriesgoDom[cont1, i1, 1] && item.PuntajeTrans <= evalriesgoDom[cont1, i1, 2])
                                {
                                    if (i1 == 1)
                                    {
                                        item.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                    }
                                    if (i1 == 2)
                                    {
                                        item.NivelRiesgoDesc = "Riesgo bajo";
                                    }
                                    if (i1 == 3)
                                    {
                                        item.NivelRiesgoDesc = "Riesgo medio";
                                    }
                                    if (i1 == 4)
                                    {
                                        item.NivelRiesgoDesc = "Riesgo alto";
                                    }
                                    if (i1 == 5)
                                    {
                                        item.NivelRiesgoDesc = "Riesgo muy alto";
                                    }
                                }
                            }
                            if (item.NivelRiesgoDesc == null)
                            {
                                decimal round = Math.Round(item.PuntajeTrans, 1);
                                for (int i1 = 1; i1 < 6; i1++)
                                {
                                    decimal CotaA = evalriesgoDom[cont1, i1, 1];
                                    decimal CotaB = evalriesgoDom[cont1, i1, 2];
                                    if (round >= CotaA && round <= CotaB)
                                    {
                                        if (i1 == 1)
                                        {
                                            item.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                        }
                                        if (i1 == 2)
                                        {
                                            item.NivelRiesgoDesc = "Riesgo bajo";
                                        }
                                        if (i1 == 3)
                                        {
                                            item.NivelRiesgoDesc = "Riesgo medio";
                                        }
                                        if (i1 == 4)
                                        {
                                            item.NivelRiesgoDesc = "Riesgo alto";
                                        }
                                        if (i1 == 5)
                                        {
                                            item.NivelRiesgoDesc = "Riesgo muy alto";
                                        }
                                    }
                                }
                            }



                            foreach (var item1 in item.ListaDimensiones)
                            {
                                item1.PuntajeTrans = (item1.Puntaje / (decimal)item1.FactorTransformacion) * 100;

                                for (int i1 = 1; i1 < 6; i1++)
                                {
                                    decimal CotaA = evalriesgo[cont, i1, 1];
                                    decimal CotaB = evalriesgo[cont, i1, 2];
                                    if (item1.PuntajeTrans >= evalriesgo[cont, i1, 1] && item1.PuntajeTrans <= evalriesgo[cont, i1, 2])
                                    {
                                        if (i1 == 1)
                                        {
                                            item1.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                        }
                                        if (i1 == 2)
                                        {
                                            item1.NivelRiesgoDesc = "Riesgo bajo";
                                        }
                                        if (i1 == 3)
                                        {
                                            item1.NivelRiesgoDesc = "Riesgo medio";
                                        }
                                        if (i1 == 4)
                                        {
                                            item1.NivelRiesgoDesc = "Riesgo alto";
                                        }
                                        if (i1 == 5)
                                        {
                                            item1.NivelRiesgoDesc = "Riesgo muy alto";
                                        }
                                    }
                                }
                                if (item1.NivelRiesgoDesc == null)
                                {
                                    decimal round = Math.Round(item1.PuntajeTrans, 1);
                                    for (int i1 = 1; i1 < 6; i1++)
                                    {
                                        decimal CotaA = evalriesgo[cont, i1, 1];
                                        decimal CotaB = evalriesgo[cont, i1, 2];
                                        if (round >= CotaA && round <= CotaB)
                                        {
                                            if (i1 == 1)
                                            {
                                                item1.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                            }
                                            if (i1 == 2)
                                            {
                                                item1.NivelRiesgoDesc = "Riesgo bajo";
                                            }
                                            if (i1 == 3)
                                            {
                                                item1.NivelRiesgoDesc = "Riesgo medio";
                                            }
                                            if (i1 == 4)
                                            {
                                                item1.NivelRiesgoDesc = "Riesgo alto";
                                            }
                                            if (i1 == 5)
                                            {
                                                item1.NivelRiesgoDesc = "Riesgo muy alto";
                                            }
                                        }
                                    }
                                }
                                cont++;
                            }
                            cont1++;
                        }
                        cont1 = 1;
                        EDBateriaUsuario.PuntajeTrans = (EDBateriaUsuario.Puntaje / (decimal)EDBateriaUsuario.FactorTransformacion) * 100;
                        EDBateriaUsuario.Listadominios = new List<EDBateriaDominio>();
                        EDBateriaUsuario.Listadominios = ListaDominios;
                        decimal[,,] evalriesgoTotal = new decimal[10, 10, 10];

                        evalriesgoTotal = baremosTotalExtraIntraA();

                        for (int i1 = 1; i1 < 6; i1++)
                        {
                            decimal CotaA = evalriesgoTotal[cont1, i1, 1];
                            decimal CotaB = evalriesgoTotal[cont1, i1, 2];
                            if (EDBateriaUsuario.PuntajeTrans >= evalriesgoTotal[cont1, i1, 1] && EDBateriaUsuario.PuntajeTrans <= evalriesgoTotal[cont1, i1, 2])
                            {
                                if (i1 == 1)
                                {
                                    EDBateriaUsuario.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                }
                                if (i1 == 2)
                                {
                                    EDBateriaUsuario.NivelRiesgoDesc = "Riesgo bajo";
                                }
                                if (i1 == 3)
                                {
                                    EDBateriaUsuario.NivelRiesgoDesc = "Riesgo medio";
                                }
                                if (i1 == 4)
                                {
                                    EDBateriaUsuario.NivelRiesgoDesc = "Riesgo alto";
                                }
                                if (i1 == 5)
                                {
                                    EDBateriaUsuario.NivelRiesgoDesc = "Riesgo muy alto";
                                }
                            }
                        }
                        if (EDBateriaUsuario.NivelRiesgoDesc == null)
                        {
                            decimal round = Math.Round(EDBateriaUsuario.PuntajeTrans, 1);
                            for (int i1 = 1; i1 < 6; i1++)
                            {
                                decimal CotaA = evalriesgoTotal[cont, i1, 1];
                                decimal CotaB = evalriesgoTotal[cont, i1, 2];
                                if (round >= CotaA && round <= CotaB)
                                {
                                    if (i1 == 1)
                                    {
                                        EDBateriaUsuario.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                    }
                                    if (i1 == 2)
                                    {
                                        EDBateriaUsuario.NivelRiesgoDesc = "Riesgo bajo";
                                    }
                                    if (i1 == 3)
                                    {
                                        EDBateriaUsuario.NivelRiesgoDesc = "Riesgo medio";
                                    }
                                    if (i1 == 4)
                                    {
                                        EDBateriaUsuario.NivelRiesgoDesc = "Riesgo alto";
                                    }
                                    if (i1 == 5)
                                    {
                                        EDBateriaUsuario.NivelRiesgoDesc = "Riesgo muy alto";
                                    }
                                }
                            }
                        }

                    }
                    #endregion
                    #region ResultadosextraFormB


                    if (EDBateriaGestion.Fk_Id_Bateria == 2 && EDBateriaUsuario.NumeroIntentos == 1)
                    {
                        int tipo = 0;
                        decimal[,,] evalriesgo = new decimal[10, 10, 10];
                        decimal[,,] evalriesgoDom = new decimal[10, 10, 10];
                        if (EDBateriaUsuario.BateriaInicial.TipoCargo == "Jefatura - tiene personal a cargo" || EDBateriaUsuario.BateriaInicial.TipoCargo == "Profesional, analista, técnico, tecnólogo")
                        {
                            tipo = 1;
                            evalriesgo = baremosExtra1();
                            evalriesgoDom = baremosExtra1();
                        }
                        if (EDBateriaUsuario.BateriaInicial.TipoCargo == "Auxiliar, asistente administrativo, asistente técnico" || EDBateriaUsuario.BateriaInicial.TipoCargo == "Operario, operador, ayudante, servicios generales")
                        {
                            tipo = 2;
                            evalriesgo = baremosExtra2();
                            evalriesgoDom = baremosExtra2();
                        }
                        foreach (var item in EDBateriaUsuario.ListaResultados)
                        {
                            int iddom = item.DominioInt;
                            int iddim = item.DimensionInt;

                            EDBateriaDominio EDBateriaDominio = ListaDominios.Where(s => s.Pk_Id_BateriaDimension == iddom).FirstOrDefault();
                            List<EDBateriaDimension> Listas = EDBateriaDominio.ListaDimensiones;
                            EDBateriaDimension EDBateriaDimension = Listas.Where(s => s.Pk_Id_BateriaDimension == iddim).FirstOrDefault();

                            EDBateriaDominio.Puntaje += item.ValorResultado;
                            EDBateriaDimension.Puntaje += item.ValorResultado;
                            EDBateriaUsuario.Puntaje += item.ValorResultado;
                        }
                        int cont = 1;
                        int cont1 = 1;
                        foreach (var item in ListaDominios)
                        {
                            item.PuntajeTrans = (item.Puntaje / (decimal)item.FactorTransformacion) * 100;
                            for (int i1 = 1; i1 < 6; i1++)
                            {
                                decimal CotaA = evalriesgoDom[cont1, i1, 1];
                                decimal CotaB = evalriesgoDom[cont1, i1, 2];
                                if (item.PuntajeTrans >= evalriesgoDom[cont1, i1, 1] && item.PuntajeTrans <= evalriesgoDom[cont1, i1, 2])
                                {
                                    if (i1 == 1)
                                    {
                                        item.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                    }
                                    if (i1 == 2)
                                    {
                                        item.NivelRiesgoDesc = "Riesgo bajo";
                                    }
                                    if (i1 == 3)
                                    {
                                        item.NivelRiesgoDesc = "Riesgo medio";
                                    }
                                    if (i1 == 4)
                                    {
                                        item.NivelRiesgoDesc = "Riesgo alto";
                                    }
                                    if (i1 == 5)
                                    {
                                        item.NivelRiesgoDesc = "Riesgo muy alto";
                                    }
                                }
                            }
                            if (item.NivelRiesgoDesc == null)
                            {
                                decimal round = Math.Round(item.PuntajeTrans, 1);
                                for (int i1 = 1; i1 < 6; i1++)
                                {
                                    decimal CotaA = evalriesgoDom[cont1, i1, 1];
                                    decimal CotaB = evalriesgoDom[cont1, i1, 2];
                                    if (round >= CotaA && round <= CotaB)
                                    {
                                        if (i1 == 1)
                                        {
                                            item.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                        }
                                        if (i1 == 2)
                                        {
                                            item.NivelRiesgoDesc = "Riesgo bajo";
                                        }
                                        if (i1 == 3)
                                        {
                                            item.NivelRiesgoDesc = "Riesgo medio";
                                        }
                                        if (i1 == 4)
                                        {
                                            item.NivelRiesgoDesc = "Riesgo alto";
                                        }
                                        if (i1 == 5)
                                        {
                                            item.NivelRiesgoDesc = "Riesgo muy alto";
                                        }
                                    }
                                }
                            }



                            foreach (var item1 in item.ListaDimensiones)
                            {
                                item1.PuntajeTrans = (item1.Puntaje / (decimal)item1.FactorTransformacion) * 100;

                                for (int i1 = 1; i1 < 6; i1++)
                                {
                                    decimal CotaA = evalriesgo[cont, i1, 1];
                                    decimal CotaB = evalriesgo[cont, i1, 2];
                                    if (item1.PuntajeTrans >= evalriesgo[cont, i1, 1] && item1.PuntajeTrans <= evalriesgo[cont, i1, 2])
                                    {
                                        if (i1 == 1)
                                        {
                                            item1.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                        }
                                        if (i1 == 2)
                                        {
                                            item1.NivelRiesgoDesc = "Riesgo bajo";
                                        }
                                        if (i1 == 3)
                                        {
                                            item1.NivelRiesgoDesc = "Riesgo medio";
                                        }
                                        if (i1 == 4)
                                        {
                                            item1.NivelRiesgoDesc = "Riesgo alto";
                                        }
                                        if (i1 == 5)
                                        {
                                            item1.NivelRiesgoDesc = "Riesgo muy alto";
                                        }
                                    }
                                }
                                if (item1.NivelRiesgoDesc == null)
                                {
                                    decimal round = Math.Round(item1.PuntajeTrans, 1);
                                    for (int i1 = 1; i1 < 6; i1++)
                                    {
                                        decimal CotaA = evalriesgo[cont, i1, 1];
                                        decimal CotaB = evalriesgo[cont, i1, 2];
                                        if (round >= CotaA && round <= CotaB)
                                        {
                                            if (i1 == 1)
                                            {
                                                item1.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                            }
                                            if (i1 == 2)
                                            {
                                                item1.NivelRiesgoDesc = "Riesgo bajo";
                                            }
                                            if (i1 == 3)
                                            {
                                                item1.NivelRiesgoDesc = "Riesgo medio";
                                            }
                                            if (i1 == 4)
                                            {
                                                item1.NivelRiesgoDesc = "Riesgo alto";
                                            }
                                            if (i1 == 5)
                                            {
                                                item1.NivelRiesgoDesc = "Riesgo muy alto";
                                            }
                                        }
                                    }
                                }
                                cont++;
                            }
                            cont1++;
                        }
                        cont1 = 1;
                        EDBateriaUsuario.PuntajeTrans = (EDBateriaUsuario.Puntaje / (decimal)EDBateriaUsuario.FactorTransformacion) * 100;
                        EDBateriaUsuario.Listadominios = new List<EDBateriaDominio>();
                        EDBateriaUsuario.Listadominios = ListaDominios;
                        decimal[,,] evalriesgoTotal = new decimal[10, 10, 10];

                        evalriesgoTotal = baremosTotalExtraIntraB();

                        for (int i1 = 1; i1 < 6; i1++)
                        {
                            decimal CotaA = evalriesgoTotal[cont1, i1, 1];
                            decimal CotaB = evalriesgoTotal[cont1, i1, 2];
                            if (EDBateriaUsuario.PuntajeTrans >= evalriesgoTotal[cont1, i1, 1] && EDBateriaUsuario.PuntajeTrans <= evalriesgoTotal[cont1, i1, 2])
                            {
                                if (i1 == 1)
                                {
                                    EDBateriaUsuario.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                }
                                if (i1 == 2)
                                {
                                    EDBateriaUsuario.NivelRiesgoDesc = "Riesgo bajo";
                                }
                                if (i1 == 3)
                                {
                                    EDBateriaUsuario.NivelRiesgoDesc = "Riesgo medio";
                                }
                                if (i1 == 4)
                                {
                                    EDBateriaUsuario.NivelRiesgoDesc = "Riesgo alto";
                                }
                                if (i1 == 5)
                                {
                                    EDBateriaUsuario.NivelRiesgoDesc = "Riesgo muy alto";
                                }
                            }
                        }
                        if (EDBateriaUsuario.NivelRiesgoDesc == null)
                        {
                            decimal round = Math.Round(EDBateriaUsuario.PuntajeTrans, 1);
                            for (int i1 = 1; i1 < 6; i1++)
                            {
                                decimal CotaA = evalriesgoTotal[cont, i1, 1];
                                decimal CotaB = evalriesgoTotal[cont, i1, 2];
                                if (round >= CotaA && round <= CotaB)
                                {
                                    if (i1 == 1)
                                    {
                                        EDBateriaUsuario.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                    }
                                    if (i1 == 2)
                                    {
                                        EDBateriaUsuario.NivelRiesgoDesc = "Riesgo bajo";
                                    }
                                    if (i1 == 3)
                                    {
                                        EDBateriaUsuario.NivelRiesgoDesc = "Riesgo medio";
                                    }
                                    if (i1 == 4)
                                    {
                                        EDBateriaUsuario.NivelRiesgoDesc = "Riesgo alto";
                                    }
                                    if (i1 == 5)
                                    {
                                        EDBateriaUsuario.NivelRiesgoDesc = "Riesgo muy alto";
                                    }
                                }
                            }
                        }
                    }
                    #endregion

                    #region ResultadosEstres


                    if (EDBateriaGestion.Fk_Id_Bateria == 4)
                    {

                        decimal[,,] evalriesgo = new decimal[10, 10, 10];
                        if (EDBateriaUsuario.BateriaInicial.TipoCargo == "Jefatura - tiene personal a cargo" || EDBateriaUsuario.BateriaInicial.TipoCargo == "Profesional, analista, técnico, tecnólogo")
                        {
                            evalriesgo = baremosEstres1();
                        }
                        if (EDBateriaUsuario.BateriaInicial.TipoCargo == "Auxiliar, asistente administrativo, asistente técnico" || EDBateriaUsuario.BateriaInicial.TipoCargo == "Operario, operador, ayudante, servicios generales")
                        {
                            evalriesgo = baremosEstres2();
                        }
                        decimal promedio1 = 0;
                        decimal promedio2 = 0;
                        decimal promedio3 = 0;
                        decimal promedio4 = 0;
                        int contp1 = 0;
                        int contp2 = 0;
                        int contp3 = 0;
                        int contp4 = 0;

                        int comienza = EDBateriaUsuario.ListaResultados.First().Fk_Id_BateriaCuestionario;
                        foreach (var item in EDBateriaUsuario.ListaResultados)
                        {
                            //int iddom = item.DominioInt;
                            //int iddim = item.DimensionInt;

                            //EDBateriaDominio EDBateriaDominio = ListaDominios.Where(s => s.Pk_Id_BateriaDimension == iddom).FirstOrDefault();
                            //List<EDBateriaDimension> Listas = EDBateriaDominio.ListaDimensiones;
                            //EDBateriaDimension EDBateriaDimension = Listas.Where(s => s.Pk_Id_BateriaDimension == iddim).FirstOrDefault();

                            //EDBateriaDominio.Puntaje += item.ValorResultado;
                            //EDBateriaDimension.Puntaje += item.ValorResultado;
                            //EDBateriaUsuario.Puntaje += item.ValorResultado;
                            
                            if (item.Fk_Id_BateriaCuestionario >= comienza && item.Fk_Id_BateriaCuestionario <= comienza+7)
                            {
                                promedio1 += item.ValorResultado;
                                contp1++;
                            }
                            if (item.Fk_Id_BateriaCuestionario >= comienza+8 && item.Fk_Id_BateriaCuestionario <= comienza+11)
                            {
                                promedio2 += item.ValorResultado;
                                contp2++;
                            }
                            if (item.Fk_Id_BateriaCuestionario >= comienza+12 && item.Fk_Id_BateriaCuestionario <= comienza+21)
                            {
                                promedio3 += item.ValorResultado;
                                contp3++;
                            }
                            if (item.Fk_Id_BateriaCuestionario >= comienza+22 && item.Fk_Id_BateriaCuestionario <= comienza+30)
                            {
                                promedio4 += item.ValorResultado;
                                contp4++;
                            }
                        }
                        promedio1 = promedio1 / contp1;
                        promedio2 = promedio2 / contp2;
                        promedio3 = promedio3 / contp3;
                        promedio4 = promedio4 / contp4;

                        promedio1 = promedio1 * 4;
                        promedio2 = promedio2 * 3;
                        promedio3 = promedio3 * 2;

                        decimal puntajetotal = promedio1 + promedio2 + promedio3 + promedio4;
                        EDBateriaUsuario.Puntaje = puntajetotal;
                        int cont1 = 1;
                        EDBateriaUsuario.PuntajeTrans = (EDBateriaUsuario.Puntaje / (decimal)EDBateriaUsuario.FactorTransformacion) * 100;
                        EDBateriaUsuario.Listadominios = new List<EDBateriaDominio>();
                        EDBateriaUsuario.Listadominios = ListaDominios;
                        decimal[,,] evalriesgoTotal = new decimal[10, 10, 10];
                        evalriesgoTotal = evalriesgo;

                        for (int i1 = 1; i1 < 6; i1++)
                        {
                            decimal CotaA = evalriesgoTotal[cont1, i1, 1];
                            decimal CotaB = evalriesgoTotal[cont1, i1, 2];
                            if (EDBateriaUsuario.PuntajeTrans >= evalriesgoTotal[cont1, i1, 1] && EDBateriaUsuario.PuntajeTrans <= evalriesgoTotal[cont1, i1, 2])
                            {
                                if (i1 == 1)
                                {
                                    EDBateriaUsuario.NivelRiesgoDesc = "Muy bajo";
                                }
                                if (i1 == 2)
                                {
                                    EDBateriaUsuario.NivelRiesgoDesc = "Bajo";
                                }
                                if (i1 == 3)
                                {
                                    EDBateriaUsuario.NivelRiesgoDesc = "Medio";
                                }
                                if (i1 == 4)
                                {
                                    EDBateriaUsuario.NivelRiesgoDesc = "Alto";
                                }
                                if (i1 == 5)
                                {
                                    EDBateriaUsuario.NivelRiesgoDesc = "Muy alto";
                                }
                            }
                        }
                        if (EDBateriaUsuario.NivelRiesgoDesc == null)
                        {
                            decimal round = Math.Round(EDBateriaUsuario.PuntajeTrans, 1);
                            for (int i1 = 1; i1 < 6; i1++)
                            {
                                decimal CotaA = evalriesgoTotal[cont1, i1, 1];
                                decimal CotaB = evalriesgoTotal[cont1, i1, 2];
                                if (EDBateriaUsuario.PuntajeTrans >= evalriesgoTotal[cont1, i1, 1] && EDBateriaUsuario.PuntajeTrans <= evalriesgoTotal[cont1, i1, 2])
                                {
                                    if (i1 == 1)
                                    {
                                        EDBateriaUsuario.NivelRiesgoDesc = "Muy bajo";
                                    }
                                    if (i1 == 2)
                                    {
                                        EDBateriaUsuario.NivelRiesgoDesc = "Bajo";
                                    }
                                    if (i1 == 3)
                                    {
                                        EDBateriaUsuario.NivelRiesgoDesc = "Medio";
                                    }
                                    if (i1 == 4)
                                    {
                                        EDBateriaUsuario.NivelRiesgoDesc = "Alto";
                                    }
                                    if (i1 == 5)
                                    {
                                        EDBateriaUsuario.NivelRiesgoDesc = "Muy alto";
                                    }
                                }
                            }
                        }

                    }
                    #endregion

                    #endregion
                }
            }
            ViewBag.pdf = mostrarPDF;
            return View(EDBateriaUsuario);
        }

        [HttpPost]
        public ActionResult ActualizarResultado(EDBateriaUsuario EDBateriaUsuario)
        {
            EDBateriaUsuario EDBateriaUsuarioGuardar = new EDBateriaUsuario();
            bool Probar = false;
            string Estado = "No se ha guardado el resultado del cuestionario";

            string[] Validacion = new string[11] { "", "", "", "", "", "", "", "", "", "","" };
            bool[] boolValidacion = new bool[11] {false,false, false, false, false, false, false, false, false, false, false };

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                Estado = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { Estado, Probar, Validacion , boolValidacion });
            }


            string Edad = null;
            string NombreEvaluador = null;
            string IdEvaluador = null;
            string Profesion = null;
            string Postgrado = null;
            string TarjetaProfesional = null;
            string Licencia = null;
            string Observaciones = null;
            string Recomendaciones = null;
            DateTime FechaExpedicion = DateTime.MinValue;

            if (EDBateriaUsuario.Edad!=null)
            {
                if (EDBateriaUsuario.Edad.Replace(" ","")!=string.Empty)
                {
                    Edad = EDBateriaUsuario.Edad;
                }
                else
                {
                    EDBateriaUsuario.Edad = Edad;
                }
            }
            if (EDBateriaUsuario.NombreEvaluador != null)
            {
                if (EDBateriaUsuario.NombreEvaluador.Replace(" ", "") != string.Empty)
                {
                    NombreEvaluador = EDBateriaUsuario.NombreEvaluador;
                }
                else
                {
                    EDBateriaUsuario.NombreEvaluador = NombreEvaluador;
                }
            }
            if (EDBateriaUsuario.IdEvaluador != null)
            {
                if (EDBateriaUsuario.IdEvaluador.Replace(" ", "") != string.Empty)
                {
                    IdEvaluador = EDBateriaUsuario.IdEvaluador;
                }
                else
                {
                    EDBateriaUsuario.IdEvaluador = IdEvaluador;
                }
            }
            if (EDBateriaUsuario.Profesion != null)
            {
                if (EDBateriaUsuario.Profesion.Replace(" ", "") != string.Empty)
                {
                    Profesion = EDBateriaUsuario.Profesion;
                }
                else
                {
                    EDBateriaUsuario.Profesion = Profesion;
                }
            }
            if (EDBateriaUsuario.Postgrado != null)
            {
                if (EDBateriaUsuario.Postgrado.Replace(" ", "") != string.Empty)
                {
                    Postgrado = EDBateriaUsuario.Postgrado;
                }
                else
                {
                    EDBateriaUsuario.Postgrado = Postgrado;
                }
            }
            if (EDBateriaUsuario.TarjetaProfesional != null)
            {
                if (EDBateriaUsuario.TarjetaProfesional.Replace(" ", "") != string.Empty)
                {
                    TarjetaProfesional = EDBateriaUsuario.TarjetaProfesional;
                }
                else
                {
                    EDBateriaUsuario.TarjetaProfesional = TarjetaProfesional;
                }
            }
            if (EDBateriaUsuario.Licencia != null)
            {
                if (EDBateriaUsuario.Licencia.Replace(" ", "") != string.Empty)
                {
                    Licencia = EDBateriaUsuario.Licencia;
                }
                else
                {
                    EDBateriaUsuario.Licencia = Licencia;
                }
            }
            if (EDBateriaUsuario.Observaciones != null)
            {
                if (EDBateriaUsuario.Observaciones.Replace(" ", "") != string.Empty)
                {
                    Observaciones = EDBateriaUsuario.Observaciones;
                }
                else
                {
                    EDBateriaUsuario.Observaciones = Observaciones;
                }
            }
            if (EDBateriaUsuario.Recomendaciones != null)
            {
                if (EDBateriaUsuario.Recomendaciones.Replace(" ", "") != string.Empty)
                {
                    Recomendaciones = EDBateriaUsuario.Recomendaciones;
                }
                else
                {
                    EDBateriaUsuario.Recomendaciones = Recomendaciones;
                }
            }
            if (EDBateriaUsuario.FechaExpedicion != null)
            {
                if (EDBateriaUsuario.FechaExpedicion!=DateTime.MinValue)
                {
                    FechaExpedicion = EDBateriaUsuario.FechaExpedicion ?? DateTime.MinValue;
                }
                else
                {
                    EDBateriaUsuario.FechaExpedicion = null;
                }
            }

            if (EDBateriaUsuario.Pk_Id_BateriaUsuario != 0)
            {
                Probar = LNBateria.ActualizarResultados(EDBateriaUsuario, usuarioActual.IdEmpresa);
                if (Probar)
                {
                    Estado = "La información de los resultados de este cuestionario se han actualizado correctamente";
                    return Json(new { Estado, Probar, Validacion, boolValidacion });
                }
            }
            else
            {
                return Json(new { Estado, Probar, Validacion, boolValidacion });
            }
            return Json(new { Estado, Probar, Validacion, boolValidacion });
        }

        [AllowAnonymous]
        public ActionResult InformePDF(string id, string NitEmpresa, int IdEmpresa, string RazonSocialEmpresa)
        {
            bool mostrarPDF = true;
            ViewBag.IdGestion = "";
            ViewBag.FechaRegistro = "";
            ViewBag.Nombre = "";
            ViewBag.NombrePersona = "";
            ViewBag.Cedula = "";
            ViewBag.FechaDiligenciamiento = "";
            ViewBag.NombreEmpresa = "";
            ViewBag.Edad = "";
            ViewBag.Dia = DateTime.Now.Day.ToString();
            ViewBag.Mes = DateTime.Now.Month.ToString();
            ViewBag.Año = DateTime.Now.Year.ToString();

            List<EDBateria> ListaBaterias = LNBateria.ConsultarBaterias();
            EDBateriaUsuario EDBateriaUsuario = new EDBateriaUsuario();

            if (id != null)
            {
                int IdUsuarioInt = 0;
                if (int.TryParse(id, out IdUsuarioInt))
                {
                    EDBateriaUsuario = LNBateria.ConsultarConvocadoId(IdUsuarioInt, IdEmpresa);

                    if (EDBateriaUsuario.Edad != null)
                    {
                        ViewBag.Edad = EDBateriaUsuario.Edad;
                    }
                    else
                    {
                        int año = EDBateriaUsuario.BateriaInicial.AñoNac;
                        DateTime Fecha = new DateTime(año, 1, 1);
                        DateTime Fecha1 = new DateTime(año, 12, 31);


                        int edad1 = DateTime.Today.AddTicks(-Fecha.Ticks).Year - 1;
                        int edad2 = DateTime.Today.AddTicks(-Fecha1.Ticks).Year - 1;

                        ViewBag.Edad = edad1.ToString() + " años";
                    }

                    int fkIdGestion = EDBateriaUsuario.Fk_Id_BateriaGestion;
                    EDBateriaGestion EDBateriaGestion = LNBateria.ConsultarGestion(fkIdGestion, IdEmpresa);
                    ViewBag.NombrePersona = EDBateriaUsuario.Nombre;
                    ViewBag.Cedula = EDBateriaUsuario.NumeroIdentificacion;
                    DateTime fecharespuesta = EDBateriaUsuario.FechaRespuesta ?? DateTime.MinValue;
                    ViewBag.FechaDiligenciamiento = fecharespuesta.ToShortDateString();
                    ViewBag.IdGestion = EDBateriaGestion.Pk_Id_BateriaGestion.ToString();
                    ViewBag.FechaRegistro = EDBateriaGestion.FechaRegistro.ToShortDateString();
                    ViewBag.Nombre = ListaBaterias.Where(s => s.Pk_Id_Bateria == EDBateriaGestion.Fk_Id_Bateria).FirstOrDefault().Nombre;
                    ViewBag.NombreEmpresa = RazonSocialEmpresa;

                    if (EDBateriaUsuario.Edad == null)
                    {
                        mostrarPDF = false;
                    }
                    if (EDBateriaUsuario.NombreEvaluador == null)
                    {
                        mostrarPDF = false;
                    }
                    if (EDBateriaUsuario.IdEvaluador == null)
                    {
                        mostrarPDF = false;
                    }
                    if (EDBateriaUsuario.Profesion == null)
                    {
                        mostrarPDF = false;
                    }
                    if (EDBateriaUsuario.Postgrado == null)
                    {
                        mostrarPDF = false;
                    }
                    if (EDBateriaUsuario.TarjetaProfesional == null)
                    {
                        mostrarPDF = false;
                    }
                    if (EDBateriaUsuario.Licencia == null)
                    {
                        mostrarPDF = false;
                    }
                    if (EDBateriaUsuario.FechaExpedicion == null)
                    {
                        mostrarPDF = false;
                    }

                    #region CalculoResultados 
                    EDBateriaUsuario.ListaResultados = LNBateria.ListaResultados(IdUsuarioInt);
                    List<EDBateriaDominio> ListaDominios = new List<EDBateriaDominio>();
                    if (EDBateriaGestion.Fk_Id_Bateria == 1 && EDBateriaUsuario.NumeroIntentos == 0)
                    {
                        EDBateriaUsuario.NombreEncuestaInforme = "INFORME DE RESULTADOS DEL CUESTIONARIO DE FACTORES DE RIESGO PSICOSOCIAL INTRALABORAL – FORMA A";
                        EDBateriaUsuario.NombreEncuestaTotal = "TOTAL GENERAL FACTORES DE RIESGO PSICOSOCIAL INTRALABORAL";
                        EDBateriaUsuario.NombreEncuesta = "Cuestionario de Factores de Riesgo Psicosocial Intralaboral FORMA A";
                        EDBateriaUsuario.FactorTransformacion = 492;
                        EDBateriaDominio EDBateriaDominio = new EDBateriaDominio();
                        EDBateriaDominio.Nombre = "Liderazgo y relaciones sociales en el trabajo";
                        EDBateriaDominio.Pk_Id_BateriaDimension = 1;
                        EDBateriaDominio.ListaDimensiones = LNBateria.ListaDimensiones(1, EDBateriaGestion.Fk_Id_Bateria);
                        EDBateriaDominio.FactorTransformacion = 164;

                        EDBateriaDominio EDBateriaDominio1 = new EDBateriaDominio();
                        EDBateriaDominio1.Nombre = "Control sobre el trabajo";
                        EDBateriaDominio1.Pk_Id_BateriaDimension = 2;
                        EDBateriaDominio1.ListaDimensiones = LNBateria.ListaDimensiones(2, EDBateriaGestion.Fk_Id_Bateria);
                        EDBateriaDominio1.FactorTransformacion = 84;

                        EDBateriaDominio EDBateriaDominio2 = new EDBateriaDominio();
                        EDBateriaDominio2.Nombre = "Demandas del trabajo";
                        EDBateriaDominio2.Pk_Id_BateriaDimension = 3;
                        EDBateriaDominio2.ListaDimensiones = LNBateria.ListaDimensiones(3, EDBateriaGestion.Fk_Id_Bateria);
                        EDBateriaDominio2.FactorTransformacion = 200;

                        EDBateriaDominio EDBateriaDominio3 = new EDBateriaDominio();
                        EDBateriaDominio3.Nombre = "Recompensas";
                        EDBateriaDominio3.Pk_Id_BateriaDimension = 4;
                        EDBateriaDominio3.ListaDimensiones = LNBateria.ListaDimensiones(4, EDBateriaGestion.Fk_Id_Bateria);
                        EDBateriaDominio3.FactorTransformacion = 44;


                        ListaDominios.Add(EDBateriaDominio);
                        ListaDominios.Add(EDBateriaDominio1);
                        ListaDominios.Add(EDBateriaDominio2);
                        ListaDominios.Add(EDBateriaDominio3);
                    }
                    if (EDBateriaGestion.Fk_Id_Bateria == 2 && EDBateriaUsuario.NumeroIntentos == 0)
                    {
                        EDBateriaUsuario.NombreEncuestaInforme = "INFORME DE RESULTADOS DEL CUESTIONARIO DE FACTORES DE RIESGO PSICOSOCIAL INTRALABORAL – FORMA B";
                        EDBateriaUsuario.NombreEncuestaTotal = "TOTAL GENERAL FACTORES DE RIESGO PSICOSOCIAL INTRALABORAL";
                        EDBateriaUsuario.NombreEncuesta = "Cuestionario de Factores de Riesgo Psicosocial Intralaboral FORMA B";
                        EDBateriaUsuario.FactorTransformacion = 388;
                        EDBateriaDominio EDBateriaDominio = new EDBateriaDominio();
                        EDBateriaDominio.Nombre = "Liderazgo y relaciones sociales en el trabajo";
                        EDBateriaDominio.Pk_Id_BateriaDimension = 1;
                        EDBateriaDominio.ListaDimensiones = LNBateria.ListaDimensiones(1, EDBateriaGestion.Fk_Id_Bateria);
                        EDBateriaDominio.FactorTransformacion = 120;

                        EDBateriaDominio EDBateriaDominio1 = new EDBateriaDominio();
                        EDBateriaDominio1.Nombre = "Control sobre el trabajo";
                        EDBateriaDominio1.Pk_Id_BateriaDimension = 2;
                        EDBateriaDominio1.ListaDimensiones = LNBateria.ListaDimensiones(2, EDBateriaGestion.Fk_Id_Bateria);
                        EDBateriaDominio1.FactorTransformacion = 72;

                        EDBateriaDominio EDBateriaDominio2 = new EDBateriaDominio();
                        EDBateriaDominio2.Nombre = "Demandas del trabajo";
                        EDBateriaDominio2.Pk_Id_BateriaDimension = 3;
                        EDBateriaDominio2.ListaDimensiones = LNBateria.ListaDimensiones(3, EDBateriaGestion.Fk_Id_Bateria);
                        EDBateriaDominio2.FactorTransformacion = 156;

                        EDBateriaDominio EDBateriaDominio3 = new EDBateriaDominio();
                        EDBateriaDominio3.Nombre = "Recompensas";
                        EDBateriaDominio3.Pk_Id_BateriaDimension = 4;
                        EDBateriaDominio3.ListaDimensiones = LNBateria.ListaDimensiones(4, EDBateriaGestion.Fk_Id_Bateria);
                        EDBateriaDominio3.FactorTransformacion = 40;

                        ListaDominios.Add(EDBateriaDominio);
                        ListaDominios.Add(EDBateriaDominio1);
                        ListaDominios.Add(EDBateriaDominio2);
                        ListaDominios.Add(EDBateriaDominio3);

                    }
                    if (EDBateriaGestion.Fk_Id_Bateria == 1 && EDBateriaUsuario.NumeroIntentos == 1)
                    {
                        EDBateriaUsuario.NombreEncuestaInforme = "INFORME DE RESULTADOS DEL CUESTIONARIO DE FACTORES DE RIESGO PSICOSOCIAL EXTRALABORAL";
                        EDBateriaUsuario.NombreEncuestaTotal = "TOTAL GENERAL FACTORES DE RIESGO PSICOSOCIAL EXTRALABORAL";
                        EDBateriaUsuario.NombreEncuesta = "Cuestionario de Factores de Riesgo Psicosocial Extralaboral";
                        EDBateriaUsuario.FactorTransformacion = 124;
                        EDBateriaDominio EDBateriaDominio = new EDBateriaDominio();
                        EDBateriaDominio.Nombre = "Unico";
                        EDBateriaDominio.Pk_Id_BateriaDimension = 1;
                        EDBateriaDominio.ListaDimensiones = LNBateria.ListaDimensiones(1, 3);
                        EDBateriaDominio.FactorTransformacion = 1;

                        ListaDominios.Add(EDBateriaDominio);
                    }
                    if (EDBateriaGestion.Fk_Id_Bateria == 2 && EDBateriaUsuario.NumeroIntentos == 1)
                    {
                        EDBateriaUsuario.NombreEncuestaInforme = "INFORME DE RESULTADOS DEL CUESTIONARIO DE FACTORES DE RIESGO PSICOSOCIAL EXTRALABORAL";
                        EDBateriaUsuario.NombreEncuestaTotal = "TOTAL GENERAL FACTORES DE RIESGO PSICOSOCIAL EXTRALABORAL";
                        EDBateriaUsuario.NombreEncuesta = "Cuestionario de Factores de Riesgo Psicosocial Extralaboral";
                        EDBateriaUsuario.FactorTransformacion = 124;
                        EDBateriaDominio EDBateriaDominio = new EDBateriaDominio();
                        EDBateriaDominio.Nombre = "Unico";
                        EDBateriaDominio.Pk_Id_BateriaDimension = 1;
                        EDBateriaDominio.ListaDimensiones = LNBateria.ListaDimensiones(1, 3);
                        EDBateriaDominio.FactorTransformacion = 1;

                        ListaDominios.Add(EDBateriaDominio);
                    }
                    if (EDBateriaGestion.Fk_Id_Bateria == 4)
                    {
                        EDBateriaUsuario.NombreEncuestaInforme = "INFORME DE RESULTADOS DEL CUESTIONARIO PARA LA EVALUACIÓN DEL ESTRÉS – TERCERA VERSIÓN";
                        EDBateriaUsuario.NombreEncuestaTotal = "TOTAL GENERAL SÍNTOMAS DE ESTRÉS";
                        EDBateriaUsuario.NombreEncuesta = "Cuestionario de Factores de Estrés";
                        EDBateriaUsuario.FactorTransformacion = 61.16;
                        EDBateriaDominio EDBateriaDominio = new EDBateriaDominio();
                        EDBateriaDominio.Nombre = "Unico";
                        EDBateriaDominio.Pk_Id_BateriaDimension = 1;
                        EDBateriaDominio.ListaDimensiones = LNBateria.ListaDimensiones(1, 4);
                        EDBateriaDominio.FactorTransformacion = 1;

                        ListaDominios.Add(EDBateriaDominio);
                    }

                    #region ResultadosIntraFormA


                    if (EDBateriaGestion.Fk_Id_Bateria == 1 && EDBateriaUsuario.NumeroIntentos == 0)
                    {
                        decimal[,,] evalriesgo = baremosIntraA();
                        decimal[,,] evalriesgoDom = baremosDominioIntraA();
                        foreach (var item in EDBateriaUsuario.ListaResultados)
                        {
                            int iddom = item.DominioInt;
                            int iddim = item.DimensionInt;

                            EDBateriaDominio EDBateriaDominio = ListaDominios.Where(s => s.Pk_Id_BateriaDimension == iddom).FirstOrDefault();
                            List<EDBateriaDimension> Listas = EDBateriaDominio.ListaDimensiones;
                            EDBateriaDimension EDBateriaDimension = Listas.Where(s => s.Pk_Id_BateriaDimension == iddim).FirstOrDefault();

                            EDBateriaDominio.Puntaje += item.ValorResultado;
                            EDBateriaDimension.Puntaje += item.ValorResultado;
                            EDBateriaUsuario.Puntaje += item.ValorResultado;
                        }
                        int cont = 1;
                        int cont1 = 1;
                        foreach (var item in ListaDominios)
                        {
                            item.PuntajeTrans = (item.Puntaje / (decimal)item.FactorTransformacion) * 100;
                            for (int i1 = 1; i1 < 6; i1++)
                            {
                                decimal CotaA = evalriesgoDom[cont1, i1, 1];
                                decimal CotaB = evalriesgoDom[cont1, i1, 2];
                                if (item.PuntajeTrans >= evalriesgoDom[cont1, i1, 1] && item.PuntajeTrans <= evalriesgoDom[cont1, i1, 2])
                                {
                                    if (i1 == 1)
                                    {
                                        item.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                    }
                                    if (i1 == 2)
                                    {
                                        item.NivelRiesgoDesc = "Riesgo bajo";
                                    }
                                    if (i1 == 3)
                                    {
                                        item.NivelRiesgoDesc = "Riesgo medio";
                                    }
                                    if (i1 == 4)
                                    {
                                        item.NivelRiesgoDesc = "Riesgo alto";
                                    }
                                    if (i1 == 5)
                                    {
                                        item.NivelRiesgoDesc = "Riesgo muy alto";
                                    }
                                }
                            }
                            if (item.NivelRiesgoDesc == null)
                            {
                                decimal round = Math.Round(item.PuntajeTrans, 1);
                                for (int i1 = 1; i1 < 6; i1++)
                                {
                                    decimal CotaA = evalriesgoDom[cont1, i1, 1];
                                    decimal CotaB = evalriesgoDom[cont1, i1, 2];
                                    if (round >= CotaA && round <= CotaB)
                                    {
                                        if (i1 == 1)
                                        {
                                            item.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                        }
                                        if (i1 == 2)
                                        {
                                            item.NivelRiesgoDesc = "Riesgo bajo";
                                        }
                                        if (i1 == 3)
                                        {
                                            item.NivelRiesgoDesc = "Riesgo medio";
                                        }
                                        if (i1 == 4)
                                        {
                                            item.NivelRiesgoDesc = "Riesgo alto";
                                        }
                                        if (i1 == 5)
                                        {
                                            item.NivelRiesgoDesc = "Riesgo muy alto";
                                        }
                                    }
                                }
                            }


                            foreach (var item1 in item.ListaDimensiones)
                            {
                                item1.PuntajeTrans = (item1.Puntaje / (decimal)item1.FactorTransformacion) * 100;

                                for (int i1 = 1; i1 < 6; i1++)
                                {
                                    decimal CotaA = evalriesgo[cont, i1, 1];
                                    decimal CotaB = evalriesgo[cont, i1, 2];
                                    if (item1.PuntajeTrans >= evalriesgo[cont, i1, 1] && item1.PuntajeTrans <= evalriesgo[cont, i1, 2])
                                    {
                                        if (i1 == 1)
                                        {
                                            item1.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                        }
                                        if (i1 == 2)
                                        {
                                            item1.NivelRiesgoDesc = "Riesgo bajo";
                                        }
                                        if (i1 == 3)
                                        {
                                            item1.NivelRiesgoDesc = "Riesgo medio";
                                        }
                                        if (i1 == 4)
                                        {
                                            item1.NivelRiesgoDesc = "Riesgo alto";
                                        }
                                        if (i1 == 5)
                                        {
                                            item1.NivelRiesgoDesc = "Riesgo muy alto";
                                        }
                                    }
                                }
                                if (item1.NivelRiesgoDesc == null)
                                {
                                    decimal round = Math.Round(item1.PuntajeTrans, 1);
                                    for (int i1 = 1; i1 < 6; i1++)
                                    {
                                        decimal CotaA = evalriesgo[cont, i1, 1];
                                        decimal CotaB = evalriesgo[cont, i1, 2];
                                        if (round >= CotaA && round <= CotaB)
                                        {
                                            if (i1 == 1)
                                            {
                                                item1.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                            }
                                            if (i1 == 2)
                                            {
                                                item1.NivelRiesgoDesc = "Riesgo bajo";
                                            }
                                            if (i1 == 3)
                                            {
                                                item1.NivelRiesgoDesc = "Riesgo medio";
                                            }
                                            if (i1 == 4)
                                            {
                                                item1.NivelRiesgoDesc = "Riesgo alto";
                                            }
                                            if (i1 == 5)
                                            {
                                                item1.NivelRiesgoDesc = "Riesgo muy alto";
                                            }
                                        }
                                    }
                                }
                                cont++;
                            }
                            cont1++;
                        }
                        cont1 = 1;
                        EDBateriaUsuario.PuntajeTrans = (EDBateriaUsuario.Puntaje / (decimal)EDBateriaUsuario.FactorTransformacion) * 100;
                        EDBateriaUsuario.Listadominios = new List<EDBateriaDominio>();
                        EDBateriaUsuario.Listadominios = ListaDominios;
                        decimal[,,] evalriesgoTotal = new decimal[10, 10, 10];

                        string formdata = EDBateriaUsuario.TokenPrivado;

                        EDBateriaUsuario EDBateriaUsuarioExtra = LNBateria.ConsultarConvocadoKeyExtra(formdata, 1);
                        if (EDBateriaUsuarioExtra != null)
                        {
                            if (EDBateriaUsuarioExtra.Pk_Id_BateriaUsuario == 0)
                            {
                                evalriesgoTotal = baremosTotalIntraA();
                            }
                            else
                            {
                                if (EDBateriaUsuarioExtra.RegistroOperacion == "Fin")
                                {
                                    evalriesgoTotal = baremosTotalExtraIntraA();
                                }
                                else
                                {
                                    evalriesgoTotal = baremosTotalIntraA();
                                }
                            }
                        }
                        else
                        {
                            evalriesgoTotal = baremosTotalIntraA();
                        }

                        for (int i1 = 1; i1 < 6; i1++)
                        {
                            decimal CotaA = evalriesgoTotal[cont1, i1, 1];
                            decimal CotaB = evalriesgoTotal[cont1, i1, 2];
                            if (EDBateriaUsuario.PuntajeTrans >= evalriesgoTotal[cont1, i1, 1] && EDBateriaUsuario.PuntajeTrans <= evalriesgoTotal[cont1, i1, 2])
                            {
                                if (i1 == 1)
                                {
                                    EDBateriaUsuario.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                }
                                if (i1 == 2)
                                {
                                    EDBateriaUsuario.NivelRiesgoDesc = "Riesgo bajo";
                                }
                                if (i1 == 3)
                                {
                                    EDBateriaUsuario.NivelRiesgoDesc = "Riesgo medio";
                                }
                                if (i1 == 4)
                                {
                                    EDBateriaUsuario.NivelRiesgoDesc = "Riesgo alto";
                                }
                                if (i1 == 5)
                                {
                                    EDBateriaUsuario.NivelRiesgoDesc = "Riesgo muy alto";
                                }
                            }
                        }
                        if (EDBateriaUsuario.NivelRiesgoDesc == null)
                        {
                            decimal round = Math.Round(EDBateriaUsuario.PuntajeTrans, 1);
                            for (int i1 = 1; i1 < 6; i1++)
                            {
                                decimal CotaA = evalriesgoTotal[cont, i1, 1];
                                decimal CotaB = evalriesgoTotal[cont, i1, 2];
                                if (round >= CotaA && round <= CotaB)
                                {
                                    if (i1 == 1)
                                    {
                                        EDBateriaUsuario.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                    }
                                    if (i1 == 2)
                                    {
                                        EDBateriaUsuario.NivelRiesgoDesc = "Riesgo bajo";
                                    }
                                    if (i1 == 3)
                                    {
                                        EDBateriaUsuario.NivelRiesgoDesc = "Riesgo medio";
                                    }
                                    if (i1 == 4)
                                    {
                                        EDBateriaUsuario.NivelRiesgoDesc = "Riesgo alto";
                                    }
                                    if (i1 == 5)
                                    {
                                        EDBateriaUsuario.NivelRiesgoDesc = "Riesgo muy alto";
                                    }
                                }
                            }
                        }

                    }
                    #endregion
                    #region ResultadosIntraFormB


                    if (EDBateriaGestion.Fk_Id_Bateria == 2 && EDBateriaUsuario.NumeroIntentos == 0)
                    {
                        decimal[,,] evalriesgo = baremosIntraB();
                        decimal[,,] evalriesgoDom = baremosDominioIntraB();
                        foreach (var item in EDBateriaUsuario.ListaResultados)
                        {
                            int iddom = item.DominioInt;
                            int iddim = item.DimensionInt;

                            EDBateriaDominio EDBateriaDominio = ListaDominios.Where(s => s.Pk_Id_BateriaDimension == iddom).FirstOrDefault();
                            List<EDBateriaDimension> Listas = EDBateriaDominio.ListaDimensiones;
                            EDBateriaDimension EDBateriaDimension = Listas.Where(s => s.Pk_Id_BateriaDimension == iddim).FirstOrDefault();

                            EDBateriaDominio.Puntaje += item.ValorResultado;
                            EDBateriaDimension.Puntaje += item.ValorResultado;
                            EDBateriaUsuario.Puntaje += item.ValorResultado;
                        }
                        int cont = 1;
                        int cont1 = 1;
                        foreach (var item in ListaDominios)
                        {
                            item.PuntajeTrans = (item.Puntaje / (decimal)item.FactorTransformacion) * 100;
                            for (int i1 = 1; i1 < 6; i1++)
                            {
                                decimal CotaA = evalriesgoDom[cont1, i1, 1];
                                decimal CotaB = evalriesgoDom[cont1, i1, 2];
                                if (item.PuntajeTrans >= evalriesgoDom[cont1, i1, 1] && item.PuntajeTrans <= evalriesgoDom[cont1, i1, 2])
                                {
                                    if (i1 == 1)
                                    {
                                        item.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                    }
                                    if (i1 == 2)
                                    {
                                        item.NivelRiesgoDesc = "Riesgo bajo";
                                    }
                                    if (i1 == 3)
                                    {
                                        item.NivelRiesgoDesc = "Riesgo medio";
                                    }
                                    if (i1 == 4)
                                    {
                                        item.NivelRiesgoDesc = "Riesgo alto";
                                    }
                                    if (i1 == 5)
                                    {
                                        item.NivelRiesgoDesc = "Riesgo muy alto";
                                    }
                                }
                            }
                            if (item.NivelRiesgoDesc == null)
                            {
                                decimal round = Math.Round(item.PuntajeTrans, 1);
                                for (int i1 = 1; i1 < 6; i1++)
                                {
                                    decimal CotaA = evalriesgoDom[cont1, i1, 1];
                                    decimal CotaB = evalriesgoDom[cont1, i1, 2];
                                    if (round >= CotaA && round <= CotaB)
                                    {
                                        if (i1 == 1)
                                        {
                                            item.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                        }
                                        if (i1 == 2)
                                        {
                                            item.NivelRiesgoDesc = "Riesgo bajo";
                                        }
                                        if (i1 == 3)
                                        {
                                            item.NivelRiesgoDesc = "Riesgo medio";
                                        }
                                        if (i1 == 4)
                                        {
                                            item.NivelRiesgoDesc = "Riesgo alto";
                                        }
                                        if (i1 == 5)
                                        {
                                            item.NivelRiesgoDesc = "Riesgo muy alto";
                                        }
                                    }
                                }
                            }


                            foreach (var item1 in item.ListaDimensiones)
                            {
                                item1.PuntajeTrans = (item1.Puntaje / (decimal)item1.FactorTransformacion) * 100;

                                for (int i1 = 1; i1 < 6; i1++)
                                {
                                    decimal CotaA = evalriesgo[cont, i1, 1];
                                    decimal CotaB = evalriesgo[cont, i1, 2];
                                    if (item1.PuntajeTrans >= CotaA && item1.PuntajeTrans <= CotaB)
                                    {
                                        if (i1 == 1)
                                        {
                                            item1.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                        }
                                        if (i1 == 2)
                                        {
                                            item1.NivelRiesgoDesc = "Riesgo bajo";
                                        }
                                        if (i1 == 3)
                                        {
                                            item1.NivelRiesgoDesc = "Riesgo medio";
                                        }
                                        if (i1 == 4)
                                        {
                                            item1.NivelRiesgoDesc = "Riesgo alto";
                                        }
                                        if (i1 == 5)
                                        {
                                            item1.NivelRiesgoDesc = "Riesgo muy alto";
                                        }
                                    }
                                }
                                if (item1.NivelRiesgoDesc == null)
                                {
                                    decimal round = Math.Round(item1.PuntajeTrans, 1);
                                    for (int i1 = 1; i1 < 6; i1++)
                                    {
                                        decimal CotaA = evalriesgo[cont, i1, 1];
                                        decimal CotaB = evalriesgo[cont, i1, 2];
                                        if (round >= CotaA && round <= CotaB)
                                        {
                                            if (i1 == 1)
                                            {
                                                item1.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                            }
                                            if (i1 == 2)
                                            {
                                                item1.NivelRiesgoDesc = "Riesgo bajo";
                                            }
                                            if (i1 == 3)
                                            {
                                                item1.NivelRiesgoDesc = "Riesgo medio";
                                            }
                                            if (i1 == 4)
                                            {
                                                item1.NivelRiesgoDesc = "Riesgo alto";
                                            }
                                            if (i1 == 5)
                                            {
                                                item1.NivelRiesgoDesc = "Riesgo muy alto";
                                            }
                                        }
                                    }
                                }
                                cont++;



                            }
                            cont1++;
                        }
                        cont1 = 1;
                        EDBateriaUsuario.PuntajeTrans = (EDBateriaUsuario.Puntaje / (decimal)EDBateriaUsuario.FactorTransformacion) * 100;
                        EDBateriaUsuario.Listadominios = new List<EDBateriaDominio>();
                        EDBateriaUsuario.Listadominios = ListaDominios;
                        decimal[,,] evalriesgoTotal = new decimal[10, 10, 10];

                        string formdata = EDBateriaUsuario.TokenPrivado;

                        EDBateriaUsuario EDBateriaUsuarioExtra = LNBateria.ConsultarConvocadoKeyExtra(formdata, 1);
                        if (EDBateriaUsuarioExtra != null)
                        {
                            if (EDBateriaUsuarioExtra.Pk_Id_BateriaUsuario == 0)
                            {
                                evalriesgoTotal = baremosTotalIntraB();
                            }
                            else
                            {
                                if (EDBateriaUsuarioExtra.RegistroOperacion == "Fin")
                                {
                                    evalriesgoTotal = baremosTotalExtraIntraB();
                                }
                                else
                                {
                                    evalriesgoTotal = baremosTotalIntraB();
                                }
                            }
                        }
                        else
                        {
                            evalriesgoTotal = baremosTotalIntraB();
                        }

                        for (int i1 = 1; i1 < 6; i1++)
                        {
                            decimal CotaA = evalriesgoTotal[cont1, i1, 1];
                            decimal CotaB = evalriesgoTotal[cont1, i1, 2];
                            if (EDBateriaUsuario.PuntajeTrans >= evalriesgoTotal[cont1, i1, 1] && EDBateriaUsuario.PuntajeTrans <= evalriesgoTotal[cont1, i1, 2])
                            {
                                if (i1 == 1)
                                {
                                    EDBateriaUsuario.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                }
                                if (i1 == 2)
                                {
                                    EDBateriaUsuario.NivelRiesgoDesc = "Riesgo bajo";
                                }
                                if (i1 == 3)
                                {
                                    EDBateriaUsuario.NivelRiesgoDesc = "Riesgo medio";
                                }
                                if (i1 == 4)
                                {
                                    EDBateriaUsuario.NivelRiesgoDesc = "Riesgo alto";
                                }
                                if (i1 == 5)
                                {
                                    EDBateriaUsuario.NivelRiesgoDesc = "Riesgo muy alto";
                                }
                            }
                        }
                        if (EDBateriaUsuario.NivelRiesgoDesc == null)
                        {
                            decimal round = Math.Round(EDBateriaUsuario.PuntajeTrans, 1);
                            for (int i1 = 1; i1 < 6; i1++)
                            {
                                decimal CotaA = evalriesgoTotal[cont, i1, 1];
                                decimal CotaB = evalriesgoTotal[cont, i1, 2];
                                if (round >= CotaA && round <= CotaB)
                                {
                                    if (i1 == 1)
                                    {
                                        EDBateriaUsuario.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                    }
                                    if (i1 == 2)
                                    {
                                        EDBateriaUsuario.NivelRiesgoDesc = "Riesgo bajo";
                                    }
                                    if (i1 == 3)
                                    {
                                        EDBateriaUsuario.NivelRiesgoDesc = "Riesgo medio";
                                    }
                                    if (i1 == 4)
                                    {
                                        EDBateriaUsuario.NivelRiesgoDesc = "Riesgo alto";
                                    }
                                    if (i1 == 5)
                                    {
                                        EDBateriaUsuario.NivelRiesgoDesc = "Riesgo muy alto";
                                    }
                                }
                            }
                        }

                    }
                    #endregion
                    #region ResultadosextraFormA


                    if (EDBateriaGestion.Fk_Id_Bateria == 1 && EDBateriaUsuario.NumeroIntentos == 1)
                    {
                        int tipo = 0;
                        decimal[,,] evalriesgo = new decimal[10, 10, 10];
                        decimal[,,] evalriesgoDom = new decimal[10, 10, 10];
                        if (EDBateriaUsuario.BateriaInicial.TipoCargo == "Jefatura - tiene personal a cargo" || EDBateriaUsuario.BateriaInicial.TipoCargo == "Profesional, analista, técnico, tecnólogo")
                        {
                            tipo = 1;
                            evalriesgo = baremosExtra1();
                            evalriesgoDom = baremosExtra1();
                        }
                        if (EDBateriaUsuario.BateriaInicial.TipoCargo == "Auxiliar, asistente administrativo, asistente técnico" || EDBateriaUsuario.BateriaInicial.TipoCargo == "Operario, operador, ayudante, servicios generales")
                        {
                            tipo = 2;
                            evalriesgo = baremosExtra2();
                            evalriesgoDom = baremosExtra2();
                        }
                        foreach (var item in EDBateriaUsuario.ListaResultados)
                        {
                            int iddom = item.DominioInt;
                            int iddim = item.DimensionInt;

                            EDBateriaDominio EDBateriaDominio = ListaDominios.Where(s => s.Pk_Id_BateriaDimension == iddom).FirstOrDefault();
                            List<EDBateriaDimension> Listas = EDBateriaDominio.ListaDimensiones;
                            EDBateriaDimension EDBateriaDimension = Listas.Where(s => s.Pk_Id_BateriaDimension == iddim).FirstOrDefault();

                            EDBateriaDominio.Puntaje += item.ValorResultado;
                            EDBateriaDimension.Puntaje += item.ValorResultado;
                            EDBateriaUsuario.Puntaje += item.ValorResultado;
                        }
                        int cont = 1;
                        int cont1 = 1;
                        foreach (var item in ListaDominios)
                        {
                            item.PuntajeTrans = (item.Puntaje / (decimal)item.FactorTransformacion) * 100;
                            for (int i1 = 1; i1 < 6; i1++)
                            {
                                decimal CotaA = evalriesgoDom[cont1, i1, 1];
                                decimal CotaB = evalriesgoDom[cont1, i1, 2];
                                if (item.PuntajeTrans >= evalriesgoDom[cont1, i1, 1] && item.PuntajeTrans <= evalriesgoDom[cont1, i1, 2])
                                {
                                    if (i1 == 1)
                                    {
                                        item.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                    }
                                    if (i1 == 2)
                                    {
                                        item.NivelRiesgoDesc = "Riesgo bajo";
                                    }
                                    if (i1 == 3)
                                    {
                                        item.NivelRiesgoDesc = "Riesgo medio";
                                    }
                                    if (i1 == 4)
                                    {
                                        item.NivelRiesgoDesc = "Riesgo alto";
                                    }
                                    if (i1 == 5)
                                    {
                                        item.NivelRiesgoDesc = "Riesgo muy alto";
                                    }
                                }
                            }
                            if (item.NivelRiesgoDesc == null)
                            {
                                decimal round = Math.Round(item.PuntajeTrans, 1);
                                for (int i1 = 1; i1 < 6; i1++)
                                {
                                    decimal CotaA = evalriesgoDom[cont1, i1, 1];
                                    decimal CotaB = evalriesgoDom[cont1, i1, 2];
                                    if (round >= CotaA && round <= CotaB)
                                    {
                                        if (i1 == 1)
                                        {
                                            item.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                        }
                                        if (i1 == 2)
                                        {
                                            item.NivelRiesgoDesc = "Riesgo bajo";
                                        }
                                        if (i1 == 3)
                                        {
                                            item.NivelRiesgoDesc = "Riesgo medio";
                                        }
                                        if (i1 == 4)
                                        {
                                            item.NivelRiesgoDesc = "Riesgo alto";
                                        }
                                        if (i1 == 5)
                                        {
                                            item.NivelRiesgoDesc = "Riesgo muy alto";
                                        }
                                    }
                                }
                            }



                            foreach (var item1 in item.ListaDimensiones)
                            {
                                item1.PuntajeTrans = (item1.Puntaje / (decimal)item1.FactorTransformacion) * 100;

                                for (int i1 = 1; i1 < 6; i1++)
                                {
                                    decimal CotaA = evalriesgo[cont, i1, 1];
                                    decimal CotaB = evalriesgo[cont, i1, 2];
                                    if (item1.PuntajeTrans >= evalriesgo[cont, i1, 1] && item1.PuntajeTrans <= evalriesgo[cont, i1, 2])
                                    {
                                        if (i1 == 1)
                                        {
                                            item1.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                        }
                                        if (i1 == 2)
                                        {
                                            item1.NivelRiesgoDesc = "Riesgo bajo";
                                        }
                                        if (i1 == 3)
                                        {
                                            item1.NivelRiesgoDesc = "Riesgo medio";
                                        }
                                        if (i1 == 4)
                                        {
                                            item1.NivelRiesgoDesc = "Riesgo alto";
                                        }
                                        if (i1 == 5)
                                        {
                                            item1.NivelRiesgoDesc = "Riesgo muy alto";
                                        }
                                    }
                                }
                                if (item1.NivelRiesgoDesc == null)
                                {
                                    decimal round = Math.Round(item1.PuntajeTrans, 1);
                                    for (int i1 = 1; i1 < 6; i1++)
                                    {
                                        decimal CotaA = evalriesgo[cont, i1, 1];
                                        decimal CotaB = evalriesgo[cont, i1, 2];
                                        if (round >= CotaA && round <= CotaB)
                                        {
                                            if (i1 == 1)
                                            {
                                                item1.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                            }
                                            if (i1 == 2)
                                            {
                                                item1.NivelRiesgoDesc = "Riesgo bajo";
                                            }
                                            if (i1 == 3)
                                            {
                                                item1.NivelRiesgoDesc = "Riesgo medio";
                                            }
                                            if (i1 == 4)
                                            {
                                                item1.NivelRiesgoDesc = "Riesgo alto";
                                            }
                                            if (i1 == 5)
                                            {
                                                item1.NivelRiesgoDesc = "Riesgo muy alto";
                                            }
                                        }
                                    }
                                }
                                cont++;
                            }
                            cont1++;
                        }
                        cont1 = 1;
                        EDBateriaUsuario.PuntajeTrans = (EDBateriaUsuario.Puntaje / (decimal)EDBateriaUsuario.FactorTransformacion) * 100;
                        EDBateriaUsuario.Listadominios = new List<EDBateriaDominio>();
                        EDBateriaUsuario.Listadominios = ListaDominios;
                        decimal[,,] evalriesgoTotal = new decimal[10, 10, 10];

                        evalriesgoTotal = baremosTotalExtraIntraA();

                        for (int i1 = 1; i1 < 6; i1++)
                        {
                            decimal CotaA = evalriesgoTotal[cont1, i1, 1];
                            decimal CotaB = evalriesgoTotal[cont1, i1, 2];
                            if (EDBateriaUsuario.PuntajeTrans >= evalriesgoTotal[cont1, i1, 1] && EDBateriaUsuario.PuntajeTrans <= evalriesgoTotal[cont1, i1, 2])
                            {
                                if (i1 == 1)
                                {
                                    EDBateriaUsuario.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                }
                                if (i1 == 2)
                                {
                                    EDBateriaUsuario.NivelRiesgoDesc = "Riesgo bajo";
                                }
                                if (i1 == 3)
                                {
                                    EDBateriaUsuario.NivelRiesgoDesc = "Riesgo medio";
                                }
                                if (i1 == 4)
                                {
                                    EDBateriaUsuario.NivelRiesgoDesc = "Riesgo alto";
                                }
                                if (i1 == 5)
                                {
                                    EDBateriaUsuario.NivelRiesgoDesc = "Riesgo muy alto";
                                }
                            }
                        }
                        if (EDBateriaUsuario.NivelRiesgoDesc == null)
                        {
                            decimal round = Math.Round(EDBateriaUsuario.PuntajeTrans, 1);
                            for (int i1 = 1; i1 < 6; i1++)
                            {
                                decimal CotaA = evalriesgoTotal[cont, i1, 1];
                                decimal CotaB = evalriesgoTotal[cont, i1, 2];
                                if (round >= CotaA && round <= CotaB)
                                {
                                    if (i1 == 1)
                                    {
                                        EDBateriaUsuario.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                    }
                                    if (i1 == 2)
                                    {
                                        EDBateriaUsuario.NivelRiesgoDesc = "Riesgo bajo";
                                    }
                                    if (i1 == 3)
                                    {
                                        EDBateriaUsuario.NivelRiesgoDesc = "Riesgo medio";
                                    }
                                    if (i1 == 4)
                                    {
                                        EDBateriaUsuario.NivelRiesgoDesc = "Riesgo alto";
                                    }
                                    if (i1 == 5)
                                    {
                                        EDBateriaUsuario.NivelRiesgoDesc = "Riesgo muy alto";
                                    }
                                }
                            }
                        }

                    }
                    #endregion
                    #region ResultadosextraFormB


                    if (EDBateriaGestion.Fk_Id_Bateria == 2 && EDBateriaUsuario.NumeroIntentos == 1)
                    {
                        int tipo = 0;
                        decimal[,,] evalriesgo = new decimal[10, 10, 10];
                        decimal[,,] evalriesgoDom = new decimal[10, 10, 10];
                        if (EDBateriaUsuario.BateriaInicial.TipoCargo == "Jefatura - tiene personal a cargo" || EDBateriaUsuario.BateriaInicial.TipoCargo == "Profesional, analista, técnico, tecnólogo")
                        {
                            tipo = 1;
                            evalriesgo = baremosExtra1();
                            evalriesgoDom = baremosExtra1();
                        }
                        if (EDBateriaUsuario.BateriaInicial.TipoCargo == "Auxiliar, asistente administrativo, asistente técnico" || EDBateriaUsuario.BateriaInicial.TipoCargo == "Operario, operador, ayudante, servicios generales")
                        {
                            tipo = 2;
                            evalriesgo = baremosExtra2();
                            evalriesgoDom = baremosExtra2();
                        }
                        foreach (var item in EDBateriaUsuario.ListaResultados)
                        {
                            int iddom = item.DominioInt;
                            int iddim = item.DimensionInt;

                            EDBateriaDominio EDBateriaDominio = ListaDominios.Where(s => s.Pk_Id_BateriaDimension == iddom).FirstOrDefault();
                            List<EDBateriaDimension> Listas = EDBateriaDominio.ListaDimensiones;
                            EDBateriaDimension EDBateriaDimension = Listas.Where(s => s.Pk_Id_BateriaDimension == iddim).FirstOrDefault();

                            EDBateriaDominio.Puntaje += item.ValorResultado;
                            EDBateriaDimension.Puntaje += item.ValorResultado;
                            EDBateriaUsuario.Puntaje += item.ValorResultado;
                        }
                        int cont = 1;
                        int cont1 = 1;
                        foreach (var item in ListaDominios)
                        {
                            item.PuntajeTrans = (item.Puntaje / (decimal)item.FactorTransformacion) * 100;
                            for (int i1 = 1; i1 < 6; i1++)
                            {
                                decimal CotaA = evalriesgoDom[cont1, i1, 1];
                                decimal CotaB = evalriesgoDom[cont1, i1, 2];
                                if (item.PuntajeTrans >= evalriesgoDom[cont1, i1, 1] && item.PuntajeTrans <= evalriesgoDom[cont1, i1, 2])
                                {
                                    if (i1 == 1)
                                    {
                                        item.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                    }
                                    if (i1 == 2)
                                    {
                                        item.NivelRiesgoDesc = "Riesgo bajo";
                                    }
                                    if (i1 == 3)
                                    {
                                        item.NivelRiesgoDesc = "Riesgo medio";
                                    }
                                    if (i1 == 4)
                                    {
                                        item.NivelRiesgoDesc = "Riesgo alto";
                                    }
                                    if (i1 == 5)
                                    {
                                        item.NivelRiesgoDesc = "Riesgo muy alto";
                                    }
                                }
                            }
                            if (item.NivelRiesgoDesc == null)
                            {
                                decimal round = Math.Round(item.PuntajeTrans, 1);
                                for (int i1 = 1; i1 < 6; i1++)
                                {
                                    decimal CotaA = evalriesgoDom[cont1, i1, 1];
                                    decimal CotaB = evalriesgoDom[cont1, i1, 2];
                                    if (round >= CotaA && round <= CotaB)
                                    {
                                        if (i1 == 1)
                                        {
                                            item.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                        }
                                        if (i1 == 2)
                                        {
                                            item.NivelRiesgoDesc = "Riesgo bajo";
                                        }
                                        if (i1 == 3)
                                        {
                                            item.NivelRiesgoDesc = "Riesgo medio";
                                        }
                                        if (i1 == 4)
                                        {
                                            item.NivelRiesgoDesc = "Riesgo alto";
                                        }
                                        if (i1 == 5)
                                        {
                                            item.NivelRiesgoDesc = "Riesgo muy alto";
                                        }
                                    }
                                }
                            }



                            foreach (var item1 in item.ListaDimensiones)
                            {
                                item1.PuntajeTrans = (item1.Puntaje / (decimal)item1.FactorTransformacion) * 100;

                                for (int i1 = 1; i1 < 6; i1++)
                                {
                                    decimal CotaA = evalriesgo[cont, i1, 1];
                                    decimal CotaB = evalriesgo[cont, i1, 2];
                                    if (item1.PuntajeTrans >= evalriesgo[cont, i1, 1] && item1.PuntajeTrans <= evalriesgo[cont, i1, 2])
                                    {
                                        if (i1 == 1)
                                        {
                                            item1.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                        }
                                        if (i1 == 2)
                                        {
                                            item1.NivelRiesgoDesc = "Riesgo bajo";
                                        }
                                        if (i1 == 3)
                                        {
                                            item1.NivelRiesgoDesc = "Riesgo medio";
                                        }
                                        if (i1 == 4)
                                        {
                                            item1.NivelRiesgoDesc = "Riesgo alto";
                                        }
                                        if (i1 == 5)
                                        {
                                            item1.NivelRiesgoDesc = "Riesgo muy alto";
                                        }
                                    }
                                }
                                if (item1.NivelRiesgoDesc == null)
                                {
                                    decimal round = Math.Round(item1.PuntajeTrans, 1);
                                    for (int i1 = 1; i1 < 6; i1++)
                                    {
                                        decimal CotaA = evalriesgo[cont, i1, 1];
                                        decimal CotaB = evalriesgo[cont, i1, 2];
                                        if (round >= CotaA && round <= CotaB)
                                        {
                                            if (i1 == 1)
                                            {
                                                item1.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                            }
                                            if (i1 == 2)
                                            {
                                                item1.NivelRiesgoDesc = "Riesgo bajo";
                                            }
                                            if (i1 == 3)
                                            {
                                                item1.NivelRiesgoDesc = "Riesgo medio";
                                            }
                                            if (i1 == 4)
                                            {
                                                item1.NivelRiesgoDesc = "Riesgo alto";
                                            }
                                            if (i1 == 5)
                                            {
                                                item1.NivelRiesgoDesc = "Riesgo muy alto";
                                            }
                                        }
                                    }
                                }
                                cont++;
                            }
                            cont1++;
                        }
                        cont1 = 1;
                        EDBateriaUsuario.PuntajeTrans = (EDBateriaUsuario.Puntaje / (decimal)EDBateriaUsuario.FactorTransformacion) * 100;
                        EDBateriaUsuario.Listadominios = new List<EDBateriaDominio>();
                        EDBateriaUsuario.Listadominios = ListaDominios;
                        decimal[,,] evalriesgoTotal = new decimal[10, 10, 10];

                        evalriesgoTotal = baremosTotalExtraIntraB();

                        for (int i1 = 1; i1 < 6; i1++)
                        {
                            decimal CotaA = evalriesgoTotal[cont1, i1, 1];
                            decimal CotaB = evalriesgoTotal[cont1, i1, 2];
                            if (EDBateriaUsuario.PuntajeTrans >= evalriesgoTotal[cont1, i1, 1] && EDBateriaUsuario.PuntajeTrans <= evalriesgoTotal[cont1, i1, 2])
                            {
                                if (i1 == 1)
                                {
                                    EDBateriaUsuario.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                }
                                if (i1 == 2)
                                {
                                    EDBateriaUsuario.NivelRiesgoDesc = "Riesgo bajo";
                                }
                                if (i1 == 3)
                                {
                                    EDBateriaUsuario.NivelRiesgoDesc = "Riesgo medio";
                                }
                                if (i1 == 4)
                                {
                                    EDBateriaUsuario.NivelRiesgoDesc = "Riesgo alto";
                                }
                                if (i1 == 5)
                                {
                                    EDBateriaUsuario.NivelRiesgoDesc = "Riesgo muy alto";
                                }
                            }
                        }
                        if (EDBateriaUsuario.NivelRiesgoDesc == null)
                        {
                            decimal round = Math.Round(EDBateriaUsuario.PuntajeTrans, 1);
                            for (int i1 = 1; i1 < 6; i1++)
                            {
                                decimal CotaA = evalriesgoTotal[cont, i1, 1];
                                decimal CotaB = evalriesgoTotal[cont, i1, 2];
                                if (round >= CotaA && round <= CotaB)
                                {
                                    if (i1 == 1)
                                    {
                                        EDBateriaUsuario.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                    }
                                    if (i1 == 2)
                                    {
                                        EDBateriaUsuario.NivelRiesgoDesc = "Riesgo bajo";
                                    }
                                    if (i1 == 3)
                                    {
                                        EDBateriaUsuario.NivelRiesgoDesc = "Riesgo medio";
                                    }
                                    if (i1 == 4)
                                    {
                                        EDBateriaUsuario.NivelRiesgoDesc = "Riesgo alto";
                                    }
                                    if (i1 == 5)
                                    {
                                        EDBateriaUsuario.NivelRiesgoDesc = "Riesgo muy alto";
                                    }
                                }
                            }
                        }
                    }
                    #endregion

                    #region ResultadosEstres


                    if (EDBateriaGestion.Fk_Id_Bateria == 4)
                    {

                        decimal[,,] evalriesgo = new decimal[10, 10, 10];
                        if (EDBateriaUsuario.BateriaInicial.TipoCargo == "Jefatura - tiene personal a cargo" || EDBateriaUsuario.BateriaInicial.TipoCargo == "Profesional, analista, técnico, tecnólogo")
                        {
                            evalriesgo = baremosEstres1();
                        }
                        if (EDBateriaUsuario.BateriaInicial.TipoCargo == "Auxiliar, asistente administrativo, asistente técnico" || EDBateriaUsuario.BateriaInicial.TipoCargo == "Operario, operador, ayudante, servicios generales")
                        {
                            evalriesgo = baremosEstres2();
                        }
                        decimal promedio1 = 0;
                        decimal promedio2 = 0;
                        decimal promedio3 = 0;
                        decimal promedio4 = 0;
                        int contp1 = 0;
                        int contp2 = 0;
                        int contp3 = 0;
                        int contp4 = 0;

                        int comienza = EDBateriaUsuario.ListaResultados.First().Fk_Id_BateriaCuestionario;
                        foreach (var item in EDBateriaUsuario.ListaResultados)
                        {
                            //int iddom = item.DominioInt;
                            //int iddim = item.DimensionInt;

                            //EDBateriaDominio EDBateriaDominio = ListaDominios.Where(s => s.Pk_Id_BateriaDimension == iddom).FirstOrDefault();
                            //List<EDBateriaDimension> Listas = EDBateriaDominio.ListaDimensiones;
                            //EDBateriaDimension EDBateriaDimension = Listas.Where(s => s.Pk_Id_BateriaDimension == iddim).FirstOrDefault();

                            //EDBateriaDominio.Puntaje += item.ValorResultado;
                            //EDBateriaDimension.Puntaje += item.ValorResultado;
                            //EDBateriaUsuario.Puntaje += item.ValorResultado;

                            if (item.Fk_Id_BateriaCuestionario >= comienza && item.Fk_Id_BateriaCuestionario <= comienza + 7)
                            {
                                promedio1 += item.ValorResultado;
                                contp1++;
                            }
                            if (item.Fk_Id_BateriaCuestionario >= comienza + 8 && item.Fk_Id_BateriaCuestionario <= comienza + 11)
                            {
                                promedio2 += item.ValorResultado;
                                contp2++;
                            }
                            if (item.Fk_Id_BateriaCuestionario >= comienza + 12 && item.Fk_Id_BateriaCuestionario <= comienza + 21)
                            {
                                promedio3 += item.ValorResultado;
                                contp3++;
                            }
                            if (item.Fk_Id_BateriaCuestionario >= comienza + 22 && item.Fk_Id_BateriaCuestionario <= comienza + 30)
                            {
                                promedio4 += item.ValorResultado;
                                contp4++;
                            }
                        }
                        promedio1 = promedio1 / contp1;
                        promedio2 = promedio2 / contp2;
                        promedio3 = promedio3 / contp3;
                        promedio4 = promedio4 / contp4;

                        promedio1 = promedio1 * 4;
                        promedio2 = promedio2 * 3;
                        promedio3 = promedio3 * 2;

                        decimal puntajetotal = promedio1 + promedio2 + promedio3 + promedio4;
                        EDBateriaUsuario.Puntaje = puntajetotal;
                        int cont1 = 1;
                        EDBateriaUsuario.PuntajeTrans = (EDBateriaUsuario.Puntaje / (decimal)EDBateriaUsuario.FactorTransformacion) * 100;
                        EDBateriaUsuario.Listadominios = new List<EDBateriaDominio>();
                        EDBateriaUsuario.Listadominios = ListaDominios;
                        decimal[,,] evalriesgoTotal = new decimal[10, 10, 10];
                        evalriesgoTotal = evalriesgo;

                        for (int i1 = 1; i1 < 6; i1++)
                        {
                            decimal CotaA = evalriesgoTotal[cont1, i1, 1];
                            decimal CotaB = evalriesgoTotal[cont1, i1, 2];
                            if (EDBateriaUsuario.PuntajeTrans >= evalriesgoTotal[cont1, i1, 1] && EDBateriaUsuario.PuntajeTrans <= evalriesgoTotal[cont1, i1, 2])
                            {
                                if (i1 == 1)
                                {
                                    EDBateriaUsuario.NivelRiesgoDesc = "Muy bajo";
                                }
                                if (i1 == 2)
                                {
                                    EDBateriaUsuario.NivelRiesgoDesc = "Bajo";
                                }
                                if (i1 == 3)
                                {
                                    EDBateriaUsuario.NivelRiesgoDesc = "Medio";
                                }
                                if (i1 == 4)
                                {
                                    EDBateriaUsuario.NivelRiesgoDesc = "Alto";
                                }
                                if (i1 == 5)
                                {
                                    EDBateriaUsuario.NivelRiesgoDesc = "Muy alto";
                                }
                            }
                        }
                        if (EDBateriaUsuario.NivelRiesgoDesc == null)
                        {
                            decimal round = Math.Round(EDBateriaUsuario.PuntajeTrans, 1);
                            for (int i1 = 1; i1 < 6; i1++)
                            {
                                decimal CotaA = evalriesgoTotal[cont1, i1, 1];
                                decimal CotaB = evalriesgoTotal[cont1, i1, 2];
                                if (EDBateriaUsuario.PuntajeTrans >= evalriesgoTotal[cont1, i1, 1] && EDBateriaUsuario.PuntajeTrans <= evalriesgoTotal[cont1, i1, 2])
                                {
                                    if (i1 == 1)
                                    {
                                        EDBateriaUsuario.NivelRiesgoDesc = "Muy bajo";
                                    }
                                    if (i1 == 2)
                                    {
                                        EDBateriaUsuario.NivelRiesgoDesc = "Bajo";
                                    }
                                    if (i1 == 3)
                                    {
                                        EDBateriaUsuario.NivelRiesgoDesc = "Medio";
                                    }
                                    if (i1 == 4)
                                    {
                                        EDBateriaUsuario.NivelRiesgoDesc = "Alto";
                                    }
                                    if (i1 == 5)
                                    {
                                        EDBateriaUsuario.NivelRiesgoDesc = "Muy alto";
                                    }
                                }
                            }
                        }

                    }
                    #endregion

                    #endregion
                }
            }
            ViewBag.pdf = mostrarPDF;
            return View(EDBateriaUsuario);
        }
        public ActionResult UrlAsPDF(string IdUsuario)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }
            //string SwitchNombreEmpresa = usuarioActual.RazonSocialEmpresa;
            //string SwitchNitEmpresa = usuarioActual.NitEmpresa;
            //string SwitchNombreDocumento = "INFORME DE RESULTADOS DEL CUESTIONARIO";

            //var fullFooter = Url.Action("Footer", "BateriaGestion", null, Request.Url.Scheme);
            //var fullHeader = Url.Action("Header", "BateriaGestion", new { NombreEmpresa = SwitchNombreEmpresa, NitEmpresa = SwitchNitEmpresa, NombreInforme = SwitchNombreDocumento }, Request.Url.Scheme);

            //var uriFooter = new Uri(Url.Action("Footer", "BateriaGestion", null, Request.Url.Scheme));
            //var clean1 = uriFooter.GetComponents(UriComponents.AbsoluteUri & ~UriComponents.Port,
            //                   UriFormat.UriEscaped);

            //var uriHeader = new Uri(Url.Action("Header", "BateriaGestion", new { NombreEmpresa = SwitchNombreEmpresa, NitEmpresa = SwitchNitEmpresa, NombreInforme = SwitchNombreDocumento }, Request.Url.Scheme));
            //var clean2 = uriHeader.GetComponents(UriComponents.AbsoluteUri & ~UriComponents.Port,
            //                   UriFormat.UriEscaped);


            //string cusomtSwitches = string.Format("--footer-line --print-media-type --allow {0} --footer-html {0} --header-line --print-media-type --allow {1} --header-html {1} --header-spacing 5",
            //clean1, clean2);
            //string cusomtSwitches_Produccion = string.Format("--footer-line --print-media-type --allow {0} --footer-html {0} --header-line --print-media-type --allow {1} --header-html {1} --header-spacing 5",
            //fullFooter, fullHeader);
            //int Id_Empresa = usuarioActual.IdEmpresa;
            //int IdUsuarioInt = 0;
            //bool probar = int.TryParse(IdUsuario, out IdUsuarioInt);
            
            //var fullUrl = this.Url.Action("InformePDF", "BateriaGestion", new { id = IdUsuarioInt, NitEmpresa = SwitchNitEmpresa, IdEmpresa = usuarioActual.IdEmpresa, RazonSocialEmpresa = usuarioActual.RazonSocialEmpresa }, this.Request.Url.Scheme);
            //var fullUrl1 = new Uri(this.Url.Action("InformePDF", "BateriaGestion", new { id = IdUsuarioInt, NitEmpresa = SwitchNitEmpresa, IdEmpresa = usuarioActual.IdEmpresa, RazonSocialEmpresa = usuarioActual.RazonSocialEmpresa }, this.Request.Url.Scheme));
            //var clean0 = fullUrl1.GetComponents(UriComponents.AbsoluteUri & ~UriComponents.Port,
            //                   UriFormat.UriEscaped);


            string EncodedRazonSocial = System.Net.WebUtility.UrlEncode(usuarioActual.RazonSocialEmpresa);
            string EncodedNit = System.Net.WebUtility.UrlEncode(usuarioActual.NitEmpresa);
            string EncodedNombreInforme = System.Net.WebUtility.UrlEncode("INFORME DE RESULTADOS DEL CUESTIONARIO");
            var footurl = "https://alissta.gov.co/Acciones/Footer";
            var headerurl = "https://alissta.gov.co/Acciones/Header?NombreEmpresa=" + EncodedRazonSocial + "&NitEmpresa=" + EncodedNit + "&NombreInforme=" + EncodedNombreInforme;
            string cusomtSwitches = string.Format("--footer-line --print-media-type --allow {0} --footer-html {0} --header-line --print-media-type --allow {1} --header-html {1} --header-spacing 5",
            footurl, headerurl);
            var ReporteUrl = "https://alissta.gov.co/BateriaGestion/InformePDF?id=" + IdUsuario.ToString() + "&NitEmpresa=" + EncodedNit + "&IdEmpresa=" + usuarioActual.IdEmpresa.ToString()+ "&RazonSocialEmpresa="+ usuarioActual.RazonSocialEmpresa;

            return new Rotativa.UrlAsPdf(ReporteUrl)
            {
                FileName = "Alissta_Bateria" + DateTime.Now.ToString().Replace("/", "").Replace(":", "").Replace(" ", "") + ".pdf"
                ,
                PageSize = Rotativa.Options.Size.Letter
                ,
                CustomSwitches = cusomtSwitches
            };

        }
        public ActionResult UrlAsPDF1(string IdUsuario)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }
            string SwitchNombreEmpresa = usuarioActual.RazonSocialEmpresa;
            string SwitchNitEmpresa = usuarioActual.NitEmpresa;
            string SwitchNombreDocumento = "INFORME DE RESULTADOS DEL CUESTIONARIO";

            var fullFooter = Url.Action("Footer", "BateriaGestion", null, Request.Url.Scheme);
            var fullHeader = Url.Action("Header", "BateriaGestion", new { NombreEmpresa = SwitchNombreEmpresa, NitEmpresa = SwitchNitEmpresa, NombreInforme = SwitchNombreDocumento }, Request.Url.Scheme);

            var uriFooter = new Uri(Url.Action("Footer", "BateriaGestion", null, Request.Url.Scheme));
            var clean1 = uriFooter.GetComponents(UriComponents.AbsoluteUri & ~UriComponents.Port,
                               UriFormat.UriEscaped);

            var uriHeader = new Uri(Url.Action("Header", "BateriaGestion", new { NombreEmpresa = SwitchNombreEmpresa, NitEmpresa = SwitchNitEmpresa, NombreInforme = SwitchNombreDocumento }, Request.Url.Scheme));
            var clean2 = uriHeader.GetComponents(UriComponents.AbsoluteUri & ~UriComponents.Port,
                               UriFormat.UriEscaped);


            string cusomtSwitches = string.Format("--footer-line --print-media-type --allow {0} --footer-html {0} --header-line --print-media-type --allow {1} --header-html {1} --header-spacing 5",
            clean1, clean2);
            string cusomtSwitches_Produccion = string.Format("--footer-line --print-media-type --allow {0} --footer-html {0} --header-line --print-media-type --allow {1} --header-html {1} --header-spacing 5",
            fullFooter, fullHeader);
            int Id_Empresa = usuarioActual.IdEmpresa;
            int IdUsuarioInt = 0;
            bool probar = int.TryParse(IdUsuario, out IdUsuarioInt);

            var fullUrl = this.Url.Action("EstadisticaIndividual", "BateriaGestion", new { id = IdUsuarioInt, NitEmpresa = SwitchNitEmpresa, IdEmpresa = usuarioActual.IdEmpresa, RazonSocialEmpresa = usuarioActual.RazonSocialEmpresa }, this.Request.Url.Scheme);
            var fullUrl1 = new Uri(this.Url.Action("InformePDF", "EstadisticaIndividual", new { id = IdUsuarioInt, NitEmpresa = SwitchNitEmpresa, IdEmpresa = usuarioActual.IdEmpresa, RazonSocialEmpresa = usuarioActual.RazonSocialEmpresa }, this.Request.Url.Scheme));
            var clean0 = fullUrl1.GetComponents(UriComponents.AbsoluteUri & ~UriComponents.Port,
                               UriFormat.UriEscaped);


            //string EncodedRazonSocial = System.Net.WebUtility.UrlEncode(usuarioActual.RazonSocialEmpresa);
            //string EncodedNit = System.Net.WebUtility.UrlEncode(usuarioActual.NitEmpresa);
            //string EncodedNombreInforme = System.Net.WebUtility.UrlEncode("INFORME DE RESULTADOS DEL CUESTIONARIO");
            //var footurl = "https://alissta.gov.co/Acciones/Footer";
            //var headerurl = "https://alissta.gov.co/Acciones/Header?NombreEmpresa=" + EncodedRazonSocial + "&NitEmpresa=" + EncodedNit + "&NombreInforme=" + EncodedNombreInforme;
            //string cusomtSwitches = string.Format("--footer-line --print-media-type --allow {0} --footer-html {0} --header-line --print-media-type --allow {1} --header-html {1} --header-spacing 5",
            //footurl, headerurl);
            //var ReporteUrl = "https://alissta.gov.co/BateriaGestion/InformePDF?id=" + IdUsuario.ToString() + "&NitEmpresa=" + EncodedNit + "&IdEmpresa=" + usuarioActual.IdEmpresa.ToString() + "&RazonSocialEmpresa=" + usuarioActual.RazonSocialEmpresa;

            return new Rotativa.UrlAsPdf(fullUrl)
            {
                FileName = "Alissta_Bateria" + DateTime.Now.ToString().Replace("/", "").Replace(":", "").Replace(" ", "") + ".pdf"
                ,
                PageSize = Rotativa.Options.Size.Letter
                ,
                CustomSwitches = cusomtSwitches_Produccion
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

        [AllowAnonymous]
        [HttpGet]
        public ActionResult EstadisticaIndividual(string id, string NitEmpresa, int IdEmpresa, string RazonSocialEmpresa)
        {
            bool mostrarPDF = true;
            ViewBag.IdGestion = "";
            ViewBag.FechaRegistro = "";
            ViewBag.Nombre = "";
            ViewBag.NombrePersona = "";
            ViewBag.Cedula = "";
            ViewBag.FechaDiligenciamiento = "";
            ViewBag.NombreEmpresa = "";
            ViewBag.Edad = "";
            ViewBag.Dia = DateTime.Now.Day.ToString();
            ViewBag.Mes = DateTime.Now.Month.ToString();
            ViewBag.Año = DateTime.Now.Year.ToString();

            List<EDBateria> ListaBaterias = LNBateria.ConsultarBaterias();
            EDBateriaUsuario EDBateriaUsuario = new EDBateriaUsuario();

            if (id != null)
            {
                int IdUsuarioInt = 0;
                if (int.TryParse(id, out IdUsuarioInt))
                {
                    EDBateriaUsuario = LNBateria.ConsultarConvocadoId(IdUsuarioInt, IdEmpresa);

                    if (EDBateriaUsuario.Edad != null)
                    {
                        ViewBag.Edad = EDBateriaUsuario.Edad;
                    }
                    else
                    {
                        int año = EDBateriaUsuario.BateriaInicial.AñoNac;
                        DateTime Fecha = new DateTime(año, 1, 1);
                        DateTime Fecha1 = new DateTime(año, 12, 31);


                        int edad1 = DateTime.Today.AddTicks(-Fecha.Ticks).Year - 1;
                        int edad2 = DateTime.Today.AddTicks(-Fecha1.Ticks).Year - 1;

                        ViewBag.Edad = edad1.ToString() + " años";
                    }

                    int fkIdGestion = EDBateriaUsuario.Fk_Id_BateriaGestion;
                    EDBateriaGestion EDBateriaGestion = LNBateria.ConsultarGestion(fkIdGestion, IdEmpresa);
                    ViewBag.NombrePersona = EDBateriaUsuario.Nombre;
                    ViewBag.Cedula = EDBateriaUsuario.NumeroIdentificacion;
                    DateTime fecharespuesta = EDBateriaUsuario.FechaRespuesta ?? DateTime.MinValue;
                    ViewBag.FechaDiligenciamiento = fecharespuesta.ToShortDateString();
                    ViewBag.IdGestion = EDBateriaGestion.Pk_Id_BateriaGestion.ToString();
                    ViewBag.FechaRegistro = EDBateriaGestion.FechaRegistro.ToShortDateString();
                    ViewBag.Nombre = ListaBaterias.Where(s => s.Pk_Id_Bateria == EDBateriaGestion.Fk_Id_Bateria).FirstOrDefault().Nombre;
                    ViewBag.NombreEmpresa = RazonSocialEmpresa;

                    if (EDBateriaUsuario.Edad == null)
                    {
                        mostrarPDF = false;
                    }
                    if (EDBateriaUsuario.NombreEvaluador == null)
                    {
                        mostrarPDF = false;
                    }
                    if (EDBateriaUsuario.IdEvaluador == null)
                    {
                        mostrarPDF = false;
                    }
                    if (EDBateriaUsuario.Profesion == null)
                    {
                        mostrarPDF = false;
                    }
                    if (EDBateriaUsuario.Postgrado == null)
                    {
                        mostrarPDF = false;
                    }
                    if (EDBateriaUsuario.TarjetaProfesional == null)
                    {
                        mostrarPDF = false;
                    }
                    if (EDBateriaUsuario.Licencia == null)
                    {
                        mostrarPDF = false;
                    }
                    if (EDBateriaUsuario.FechaExpedicion == null)
                    {
                        mostrarPDF = false;
                    }

                    #region CalculoResultados 
                    EDBateriaUsuario.ListaResultados = LNBateria.ListaResultados(IdUsuarioInt);
                    List<EDBateriaDominio> ListaDominios = new List<EDBateriaDominio>();
                    if (EDBateriaGestion.Fk_Id_Bateria == 1 && EDBateriaUsuario.NumeroIntentos == 0)
                    {
                        EDBateriaUsuario.NombreEncuestaInforme = "INFORME DE RESULTADOS DEL CUESTIONARIO DE FACTORES DE RIESGO PSICOSOCIAL INTRALABORAL – FORMA A";
                        EDBateriaUsuario.NombreEncuestaTotal = "TOTAL GENERAL FACTORES DE RIESGO PSICOSOCIAL INTRALABORAL";
                        EDBateriaUsuario.NombreEncuesta = "Cuestionario de Factores de Riesgo Psicosocial Intralaboral FORMA A";
                        EDBateriaUsuario.FactorTransformacion = 492;
                        EDBateriaDominio EDBateriaDominio = new EDBateriaDominio();
                        EDBateriaDominio.Nombre = "Liderazgo y relaciones sociales en el trabajo";
                        EDBateriaDominio.Pk_Id_BateriaDimension = 1;
                        EDBateriaDominio.ListaDimensiones = LNBateria.ListaDimensiones(1, EDBateriaGestion.Fk_Id_Bateria);
                        EDBateriaDominio.FactorTransformacion = 164;

                        EDBateriaDominio EDBateriaDominio1 = new EDBateriaDominio();
                        EDBateriaDominio1.Nombre = "Control sobre el trabajo";
                        EDBateriaDominio1.Pk_Id_BateriaDimension = 2;
                        EDBateriaDominio1.ListaDimensiones = LNBateria.ListaDimensiones(2, EDBateriaGestion.Fk_Id_Bateria);
                        EDBateriaDominio1.FactorTransformacion = 84;

                        EDBateriaDominio EDBateriaDominio2 = new EDBateriaDominio();
                        EDBateriaDominio2.Nombre = "Demandas del trabajo";
                        EDBateriaDominio2.Pk_Id_BateriaDimension = 3;
                        EDBateriaDominio2.ListaDimensiones = LNBateria.ListaDimensiones(3, EDBateriaGestion.Fk_Id_Bateria);
                        EDBateriaDominio2.FactorTransformacion = 200;

                        EDBateriaDominio EDBateriaDominio3 = new EDBateriaDominio();
                        EDBateriaDominio3.Nombre = "Recompensas";
                        EDBateriaDominio3.Pk_Id_BateriaDimension = 4;
                        EDBateriaDominio3.ListaDimensiones = LNBateria.ListaDimensiones(4, EDBateriaGestion.Fk_Id_Bateria);
                        EDBateriaDominio3.FactorTransformacion = 44;


                        ListaDominios.Add(EDBateriaDominio);
                        ListaDominios.Add(EDBateriaDominio1);
                        ListaDominios.Add(EDBateriaDominio2);
                        ListaDominios.Add(EDBateriaDominio3);
                    }
                    if (EDBateriaGestion.Fk_Id_Bateria == 2 && EDBateriaUsuario.NumeroIntentos == 0)
                    {
                        EDBateriaUsuario.NombreEncuestaInforme = "INFORME DE RESULTADOS DEL CUESTIONARIO DE FACTORES DE RIESGO PSICOSOCIAL INTRALABORAL – FORMA B";
                        EDBateriaUsuario.NombreEncuestaTotal = "TOTAL GENERAL FACTORES DE RIESGO PSICOSOCIAL INTRALABORAL";
                        EDBateriaUsuario.NombreEncuesta = "Cuestionario de Factores de Riesgo Psicosocial Intralaboral FORMA B";
                        EDBateriaUsuario.FactorTransformacion = 388;
                        EDBateriaDominio EDBateriaDominio = new EDBateriaDominio();
                        EDBateriaDominio.Nombre = "Liderazgo y relaciones sociales en el trabajo";
                        EDBateriaDominio.Pk_Id_BateriaDimension = 1;
                        EDBateriaDominio.ListaDimensiones = LNBateria.ListaDimensiones(1, EDBateriaGestion.Fk_Id_Bateria);
                        EDBateriaDominio.FactorTransformacion = 120;

                        EDBateriaDominio EDBateriaDominio1 = new EDBateriaDominio();
                        EDBateriaDominio1.Nombre = "Control sobre el trabajo";
                        EDBateriaDominio1.Pk_Id_BateriaDimension = 2;
                        EDBateriaDominio1.ListaDimensiones = LNBateria.ListaDimensiones(2, EDBateriaGestion.Fk_Id_Bateria);
                        EDBateriaDominio1.FactorTransformacion = 72;

                        EDBateriaDominio EDBateriaDominio2 = new EDBateriaDominio();
                        EDBateriaDominio2.Nombre = "Demandas del trabajo";
                        EDBateriaDominio2.Pk_Id_BateriaDimension = 3;
                        EDBateriaDominio2.ListaDimensiones = LNBateria.ListaDimensiones(3, EDBateriaGestion.Fk_Id_Bateria);
                        EDBateriaDominio2.FactorTransformacion = 156;

                        EDBateriaDominio EDBateriaDominio3 = new EDBateriaDominio();
                        EDBateriaDominio3.Nombre = "Recompensas";
                        EDBateriaDominio3.Pk_Id_BateriaDimension = 4;
                        EDBateriaDominio3.ListaDimensiones = LNBateria.ListaDimensiones(4, EDBateriaGestion.Fk_Id_Bateria);
                        EDBateriaDominio3.FactorTransformacion = 40;

                        ListaDominios.Add(EDBateriaDominio);
                        ListaDominios.Add(EDBateriaDominio1);
                        ListaDominios.Add(EDBateriaDominio2);
                        ListaDominios.Add(EDBateriaDominio3);

                    }
                    if (EDBateriaGestion.Fk_Id_Bateria == 1 && EDBateriaUsuario.NumeroIntentos == 1)
                    {
                        EDBateriaUsuario.NombreEncuestaInforme = "INFORME DE RESULTADOS DEL CUESTIONARIO DE FACTORES DE RIESGO PSICOSOCIAL EXTRALABORAL";
                        EDBateriaUsuario.NombreEncuestaTotal = "TOTAL GENERAL FACTORES DE RIESGO PSICOSOCIAL EXTRALABORAL";
                        EDBateriaUsuario.NombreEncuesta = "Cuestionario de Factores de Riesgo Psicosocial Extralaboral";
                        EDBateriaUsuario.FactorTransformacion = 124;
                        EDBateriaDominio EDBateriaDominio = new EDBateriaDominio();
                        EDBateriaDominio.Nombre = "Unico";
                        EDBateriaDominio.Pk_Id_BateriaDimension = 1;
                        EDBateriaDominio.ListaDimensiones = LNBateria.ListaDimensiones(1, 3);
                        EDBateriaDominio.FactorTransformacion = 1;

                        ListaDominios.Add(EDBateriaDominio);
                    }
                    if (EDBateriaGestion.Fk_Id_Bateria == 2 && EDBateriaUsuario.NumeroIntentos == 1)
                    {
                        EDBateriaUsuario.NombreEncuestaInforme = "INFORME DE RESULTADOS DEL CUESTIONARIO DE FACTORES DE RIESGO PSICOSOCIAL EXTRALABORAL";
                        EDBateriaUsuario.NombreEncuestaTotal = "TOTAL GENERAL FACTORES DE RIESGO PSICOSOCIAL EXTRALABORAL";
                        EDBateriaUsuario.NombreEncuesta = "Cuestionario de Factores de Riesgo Psicosocial Extralaboral";
                        EDBateriaUsuario.FactorTransformacion = 124;
                        EDBateriaDominio EDBateriaDominio = new EDBateriaDominio();
                        EDBateriaDominio.Nombre = "Unico";
                        EDBateriaDominio.Pk_Id_BateriaDimension = 1;
                        EDBateriaDominio.ListaDimensiones = LNBateria.ListaDimensiones(1, 3);
                        EDBateriaDominio.FactorTransformacion = 1;

                        ListaDominios.Add(EDBateriaDominio);
                    }
                    if (EDBateriaGestion.Fk_Id_Bateria == 4)
                    {
                        EDBateriaUsuario.NombreEncuestaInforme = "INFORME DE RESULTADOS DEL CUESTIONARIO PARA LA EVALUACIÓN DEL ESTRÉS – TERCERA VERSIÓN";
                        EDBateriaUsuario.NombreEncuestaTotal = "TOTAL GENERAL SÍNTOMAS DE ESTRÉS";
                        EDBateriaUsuario.NombreEncuesta = "Cuestionario de Factores de Estrés";
                        EDBateriaUsuario.FactorTransformacion = 61.16;
                        EDBateriaDominio EDBateriaDominio = new EDBateriaDominio();
                        EDBateriaDominio.Nombre = "Unico";
                        EDBateriaDominio.Pk_Id_BateriaDimension = 1;
                        EDBateriaDominio.ListaDimensiones = LNBateria.ListaDimensiones(1, 4);
                        EDBateriaDominio.FactorTransformacion = 1;

                        ListaDominios.Add(EDBateriaDominio);
                    }

                    #region ResultadosIntraFormA


                    if (EDBateriaGestion.Fk_Id_Bateria == 1 && EDBateriaUsuario.NumeroIntentos == 0)
                    {
                        decimal[,,] evalriesgo = baremosIntraA();
                        decimal[,,] evalriesgoDom = baremosDominioIntraA();
                        foreach (var item in EDBateriaUsuario.ListaResultados)
                        {
                            int iddom = item.DominioInt;
                            int iddim = item.DimensionInt;

                            EDBateriaDominio EDBateriaDominio = ListaDominios.Where(s => s.Pk_Id_BateriaDimension == iddom).FirstOrDefault();
                            List<EDBateriaDimension> Listas = EDBateriaDominio.ListaDimensiones;
                            EDBateriaDimension EDBateriaDimension = Listas.Where(s => s.Pk_Id_BateriaDimension == iddim).FirstOrDefault();

                            EDBateriaDominio.Puntaje += item.ValorResultado;
                            EDBateriaDimension.Puntaje += item.ValorResultado;
                            EDBateriaUsuario.Puntaje += item.ValorResultado;
                        }
                        int cont = 1;
                        int cont1 = 1;
                        foreach (var item in ListaDominios)
                        {
                            item.PuntajeTrans = (item.Puntaje / (decimal)item.FactorTransformacion) * 100;
                            for (int i1 = 1; i1 < 6; i1++)
                            {
                                decimal CotaA = evalriesgoDom[cont1, i1, 1];
                                decimal CotaB = evalriesgoDom[cont1, i1, 2];
                                if (item.PuntajeTrans >= evalriesgoDom[cont1, i1, 1] && item.PuntajeTrans <= evalriesgoDom[cont1, i1, 2])
                                {
                                    if (i1 == 1)
                                    {
                                        item.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                    }
                                    if (i1 == 2)
                                    {
                                        item.NivelRiesgoDesc = "Riesgo bajo";
                                    }
                                    if (i1 == 3)
                                    {
                                        item.NivelRiesgoDesc = "Riesgo medio";
                                    }
                                    if (i1 == 4)
                                    {
                                        item.NivelRiesgoDesc = "Riesgo alto";
                                    }
                                    if (i1 == 5)
                                    {
                                        item.NivelRiesgoDesc = "Riesgo muy alto";
                                    }
                                }
                            }
                            if (item.NivelRiesgoDesc == null)
                            {
                                decimal round = Math.Round(item.PuntajeTrans, 1);
                                for (int i1 = 1; i1 < 6; i1++)
                                {
                                    decimal CotaA = evalriesgoDom[cont1, i1, 1];
                                    decimal CotaB = evalriesgoDom[cont1, i1, 2];
                                    if (round >= CotaA && round <= CotaB)
                                    {
                                        if (i1 == 1)
                                        {
                                            item.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                        }
                                        if (i1 == 2)
                                        {
                                            item.NivelRiesgoDesc = "Riesgo bajo";
                                        }
                                        if (i1 == 3)
                                        {
                                            item.NivelRiesgoDesc = "Riesgo medio";
                                        }
                                        if (i1 == 4)
                                        {
                                            item.NivelRiesgoDesc = "Riesgo alto";
                                        }
                                        if (i1 == 5)
                                        {
                                            item.NivelRiesgoDesc = "Riesgo muy alto";
                                        }
                                    }
                                }
                            }


                            foreach (var item1 in item.ListaDimensiones)
                            {
                                item1.PuntajeTrans = (item1.Puntaje / (decimal)item1.FactorTransformacion) * 100;

                                for (int i1 = 1; i1 < 6; i1++)
                                {
                                    decimal CotaA = evalriesgo[cont, i1, 1];
                                    decimal CotaB = evalriesgo[cont, i1, 2];
                                    if (item1.PuntajeTrans >= evalriesgo[cont, i1, 1] && item1.PuntajeTrans <= evalriesgo[cont, i1, 2])
                                    {
                                        if (i1 == 1)
                                        {
                                            item1.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                        }
                                        if (i1 == 2)
                                        {
                                            item1.NivelRiesgoDesc = "Riesgo bajo";
                                        }
                                        if (i1 == 3)
                                        {
                                            item1.NivelRiesgoDesc = "Riesgo medio";
                                        }
                                        if (i1 == 4)
                                        {
                                            item1.NivelRiesgoDesc = "Riesgo alto";
                                        }
                                        if (i1 == 5)
                                        {
                                            item1.NivelRiesgoDesc = "Riesgo muy alto";
                                        }
                                    }
                                }
                                if (item1.NivelRiesgoDesc == null)
                                {
                                    decimal round = Math.Round(item1.PuntajeTrans, 1);
                                    for (int i1 = 1; i1 < 6; i1++)
                                    {
                                        decimal CotaA = evalriesgo[cont, i1, 1];
                                        decimal CotaB = evalriesgo[cont, i1, 2];
                                        if (round >= CotaA && round <= CotaB)
                                        {
                                            if (i1 == 1)
                                            {
                                                item1.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                            }
                                            if (i1 == 2)
                                            {
                                                item1.NivelRiesgoDesc = "Riesgo bajo";
                                            }
                                            if (i1 == 3)
                                            {
                                                item1.NivelRiesgoDesc = "Riesgo medio";
                                            }
                                            if (i1 == 4)
                                            {
                                                item1.NivelRiesgoDesc = "Riesgo alto";
                                            }
                                            if (i1 == 5)
                                            {
                                                item1.NivelRiesgoDesc = "Riesgo muy alto";
                                            }
                                        }
                                    }
                                }
                                cont++;
                            }
                            cont1++;
                        }
                        cont1 = 1;
                        EDBateriaUsuario.PuntajeTrans = (EDBateriaUsuario.Puntaje / (decimal)EDBateriaUsuario.FactorTransformacion) * 100;
                        EDBateriaUsuario.Listadominios = new List<EDBateriaDominio>();
                        EDBateriaUsuario.Listadominios = ListaDominios;
                        decimal[,,] evalriesgoTotal = new decimal[10, 10, 10];

                        string formdata = EDBateriaUsuario.TokenPrivado;

                        EDBateriaUsuario EDBateriaUsuarioExtra = LNBateria.ConsultarConvocadoKeyExtra(formdata, 1);
                        if (EDBateriaUsuarioExtra != null)
                        {
                            if (EDBateriaUsuarioExtra.Pk_Id_BateriaUsuario == 0)
                            {
                                evalriesgoTotal = baremosTotalIntraA();
                            }
                            else
                            {
                                if (EDBateriaUsuarioExtra.RegistroOperacion == "Fin")
                                {
                                    evalriesgoTotal = baremosTotalExtraIntraA();
                                }
                                else
                                {
                                    evalriesgoTotal = baremosTotalIntraA();
                                }
                            }
                        }
                        else
                        {
                            evalriesgoTotal = baremosTotalIntraA();
                        }

                        for (int i1 = 1; i1 < 6; i1++)
                        {
                            decimal CotaA = evalriesgoTotal[cont1, i1, 1];
                            decimal CotaB = evalriesgoTotal[cont1, i1, 2];
                            if (EDBateriaUsuario.PuntajeTrans >= evalriesgoTotal[cont1, i1, 1] && EDBateriaUsuario.PuntajeTrans <= evalriesgoTotal[cont1, i1, 2])
                            {
                                if (i1 == 1)
                                {
                                    EDBateriaUsuario.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                }
                                if (i1 == 2)
                                {
                                    EDBateriaUsuario.NivelRiesgoDesc = "Riesgo bajo";
                                }
                                if (i1 == 3)
                                {
                                    EDBateriaUsuario.NivelRiesgoDesc = "Riesgo medio";
                                }
                                if (i1 == 4)
                                {
                                    EDBateriaUsuario.NivelRiesgoDesc = "Riesgo alto";
                                }
                                if (i1 == 5)
                                {
                                    EDBateriaUsuario.NivelRiesgoDesc = "Riesgo muy alto";
                                }
                            }
                        }
                        if (EDBateriaUsuario.NivelRiesgoDesc == null)
                        {
                            decimal round = Math.Round(EDBateriaUsuario.PuntajeTrans, 1);
                            for (int i1 = 1; i1 < 6; i1++)
                            {
                                decimal CotaA = evalriesgoTotal[cont, i1, 1];
                                decimal CotaB = evalriesgoTotal[cont, i1, 2];
                                if (round >= CotaA && round <= CotaB)
                                {
                                    if (i1 == 1)
                                    {
                                        EDBateriaUsuario.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                    }
                                    if (i1 == 2)
                                    {
                                        EDBateriaUsuario.NivelRiesgoDesc = "Riesgo bajo";
                                    }
                                    if (i1 == 3)
                                    {
                                        EDBateriaUsuario.NivelRiesgoDesc = "Riesgo medio";
                                    }
                                    if (i1 == 4)
                                    {
                                        EDBateriaUsuario.NivelRiesgoDesc = "Riesgo alto";
                                    }
                                    if (i1 == 5)
                                    {
                                        EDBateriaUsuario.NivelRiesgoDesc = "Riesgo muy alto";
                                    }
                                }
                            }
                        }

                    }
                    #endregion
                    #region ResultadosIntraFormB


                    if (EDBateriaGestion.Fk_Id_Bateria == 2 && EDBateriaUsuario.NumeroIntentos == 0)
                    {
                        decimal[,,] evalriesgo = baremosIntraB();
                        decimal[,,] evalriesgoDom = baremosDominioIntraB();
                        foreach (var item in EDBateriaUsuario.ListaResultados)
                        {
                            int iddom = item.DominioInt;
                            int iddim = item.DimensionInt;

                            EDBateriaDominio EDBateriaDominio = ListaDominios.Where(s => s.Pk_Id_BateriaDimension == iddom).FirstOrDefault();
                            List<EDBateriaDimension> Listas = EDBateriaDominio.ListaDimensiones;
                            EDBateriaDimension EDBateriaDimension = Listas.Where(s => s.Pk_Id_BateriaDimension == iddim).FirstOrDefault();

                            EDBateriaDominio.Puntaje += item.ValorResultado;
                            EDBateriaDimension.Puntaje += item.ValorResultado;
                            EDBateriaUsuario.Puntaje += item.ValorResultado;
                        }
                        int cont = 1;
                        int cont1 = 1;
                        foreach (var item in ListaDominios)
                        {
                            item.PuntajeTrans = (item.Puntaje / (decimal)item.FactorTransformacion) * 100;
                            for (int i1 = 1; i1 < 6; i1++)
                            {
                                decimal CotaA = evalriesgoDom[cont1, i1, 1];
                                decimal CotaB = evalriesgoDom[cont1, i1, 2];
                                if (item.PuntajeTrans >= evalriesgoDom[cont1, i1, 1] && item.PuntajeTrans <= evalriesgoDom[cont1, i1, 2])
                                {
                                    if (i1 == 1)
                                    {
                                        item.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                    }
                                    if (i1 == 2)
                                    {
                                        item.NivelRiesgoDesc = "Riesgo bajo";
                                    }
                                    if (i1 == 3)
                                    {
                                        item.NivelRiesgoDesc = "Riesgo medio";
                                    }
                                    if (i1 == 4)
                                    {
                                        item.NivelRiesgoDesc = "Riesgo alto";
                                    }
                                    if (i1 == 5)
                                    {
                                        item.NivelRiesgoDesc = "Riesgo muy alto";
                                    }
                                }
                            }
                            if (item.NivelRiesgoDesc == null)
                            {
                                decimal round = Math.Round(item.PuntajeTrans, 1);
                                for (int i1 = 1; i1 < 6; i1++)
                                {
                                    decimal CotaA = evalriesgoDom[cont1, i1, 1];
                                    decimal CotaB = evalriesgoDom[cont1, i1, 2];
                                    if (round >= CotaA && round <= CotaB)
                                    {
                                        if (i1 == 1)
                                        {
                                            item.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                        }
                                        if (i1 == 2)
                                        {
                                            item.NivelRiesgoDesc = "Riesgo bajo";
                                        }
                                        if (i1 == 3)
                                        {
                                            item.NivelRiesgoDesc = "Riesgo medio";
                                        }
                                        if (i1 == 4)
                                        {
                                            item.NivelRiesgoDesc = "Riesgo alto";
                                        }
                                        if (i1 == 5)
                                        {
                                            item.NivelRiesgoDesc = "Riesgo muy alto";
                                        }
                                    }
                                }
                            }


                            foreach (var item1 in item.ListaDimensiones)
                            {
                                item1.PuntajeTrans = (item1.Puntaje / (decimal)item1.FactorTransformacion) * 100;

                                for (int i1 = 1; i1 < 6; i1++)
                                {
                                    decimal CotaA = evalriesgo[cont, i1, 1];
                                    decimal CotaB = evalriesgo[cont, i1, 2];
                                    if (item1.PuntajeTrans >= CotaA && item1.PuntajeTrans <= CotaB)
                                    {
                                        if (i1 == 1)
                                        {
                                            item1.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                        }
                                        if (i1 == 2)
                                        {
                                            item1.NivelRiesgoDesc = "Riesgo bajo";
                                        }
                                        if (i1 == 3)
                                        {
                                            item1.NivelRiesgoDesc = "Riesgo medio";
                                        }
                                        if (i1 == 4)
                                        {
                                            item1.NivelRiesgoDesc = "Riesgo alto";
                                        }
                                        if (i1 == 5)
                                        {
                                            item1.NivelRiesgoDesc = "Riesgo muy alto";
                                        }
                                    }
                                }
                                if (item1.NivelRiesgoDesc == null)
                                {
                                    decimal round = Math.Round(item1.PuntajeTrans, 1);
                                    for (int i1 = 1; i1 < 6; i1++)
                                    {
                                        decimal CotaA = evalriesgo[cont, i1, 1];
                                        decimal CotaB = evalriesgo[cont, i1, 2];
                                        if (round >= CotaA && round <= CotaB)
                                        {
                                            if (i1 == 1)
                                            {
                                                item1.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                            }
                                            if (i1 == 2)
                                            {
                                                item1.NivelRiesgoDesc = "Riesgo bajo";
                                            }
                                            if (i1 == 3)
                                            {
                                                item1.NivelRiesgoDesc = "Riesgo medio";
                                            }
                                            if (i1 == 4)
                                            {
                                                item1.NivelRiesgoDesc = "Riesgo alto";
                                            }
                                            if (i1 == 5)
                                            {
                                                item1.NivelRiesgoDesc = "Riesgo muy alto";
                                            }
                                        }
                                    }
                                }
                                cont++;



                            }
                            cont1++;
                        }
                        cont1 = 1;
                        EDBateriaUsuario.PuntajeTrans = (EDBateriaUsuario.Puntaje / (decimal)EDBateriaUsuario.FactorTransformacion) * 100;
                        EDBateriaUsuario.Listadominios = new List<EDBateriaDominio>();
                        EDBateriaUsuario.Listadominios = ListaDominios;
                        decimal[,,] evalriesgoTotal = new decimal[10, 10, 10];

                        string formdata = EDBateriaUsuario.TokenPrivado;

                        EDBateriaUsuario EDBateriaUsuarioExtra = LNBateria.ConsultarConvocadoKeyExtra(formdata, 1);
                        if (EDBateriaUsuarioExtra != null)
                        {
                            if (EDBateriaUsuarioExtra.Pk_Id_BateriaUsuario == 0)
                            {
                                evalriesgoTotal = baremosTotalIntraB();
                            }
                            else
                            {
                                if (EDBateriaUsuarioExtra.RegistroOperacion == "Fin")
                                {
                                    evalriesgoTotal = baremosTotalExtraIntraB();
                                }
                                else
                                {
                                    evalriesgoTotal = baremosTotalIntraB();
                                }
                            }
                        }
                        else
                        {
                            evalriesgoTotal = baremosTotalIntraB();
                        }

                        for (int i1 = 1; i1 < 6; i1++)
                        {
                            decimal CotaA = evalriesgoTotal[cont1, i1, 1];
                            decimal CotaB = evalriesgoTotal[cont1, i1, 2];
                            if (EDBateriaUsuario.PuntajeTrans >= evalriesgoTotal[cont1, i1, 1] && EDBateriaUsuario.PuntajeTrans <= evalriesgoTotal[cont1, i1, 2])
                            {
                                if (i1 == 1)
                                {
                                    EDBateriaUsuario.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                }
                                if (i1 == 2)
                                {
                                    EDBateriaUsuario.NivelRiesgoDesc = "Riesgo bajo";
                                }
                                if (i1 == 3)
                                {
                                    EDBateriaUsuario.NivelRiesgoDesc = "Riesgo medio";
                                }
                                if (i1 == 4)
                                {
                                    EDBateriaUsuario.NivelRiesgoDesc = "Riesgo alto";
                                }
                                if (i1 == 5)
                                {
                                    EDBateriaUsuario.NivelRiesgoDesc = "Riesgo muy alto";
                                }
                            }
                        }
                        if (EDBateriaUsuario.NivelRiesgoDesc == null)
                        {
                            decimal round = Math.Round(EDBateriaUsuario.PuntajeTrans, 1);
                            for (int i1 = 1; i1 < 6; i1++)
                            {
                                decimal CotaA = evalriesgoTotal[cont, i1, 1];
                                decimal CotaB = evalriesgoTotal[cont, i1, 2];
                                if (round >= CotaA && round <= CotaB)
                                {
                                    if (i1 == 1)
                                    {
                                        EDBateriaUsuario.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                    }
                                    if (i1 == 2)
                                    {
                                        EDBateriaUsuario.NivelRiesgoDesc = "Riesgo bajo";
                                    }
                                    if (i1 == 3)
                                    {
                                        EDBateriaUsuario.NivelRiesgoDesc = "Riesgo medio";
                                    }
                                    if (i1 == 4)
                                    {
                                        EDBateriaUsuario.NivelRiesgoDesc = "Riesgo alto";
                                    }
                                    if (i1 == 5)
                                    {
                                        EDBateriaUsuario.NivelRiesgoDesc = "Riesgo muy alto";
                                    }
                                }
                            }
                        }

                    }
                    #endregion
                    #region ResultadosextraFormA


                    if (EDBateriaGestion.Fk_Id_Bateria == 1 && EDBateriaUsuario.NumeroIntentos == 1)
                    {
                        int tipo = 0;
                        decimal[,,] evalriesgo = new decimal[10, 10, 10];
                        decimal[,,] evalriesgoDom = new decimal[10, 10, 10];
                        if (EDBateriaUsuario.BateriaInicial.TipoCargo == "Jefatura - tiene personal a cargo" || EDBateriaUsuario.BateriaInicial.TipoCargo == "Profesional, analista, técnico, tecnólogo")
                        {
                            tipo = 1;
                            evalriesgo = baremosExtra1();
                            evalriesgoDom = baremosExtra1();
                        }
                        if (EDBateriaUsuario.BateriaInicial.TipoCargo == "Auxiliar, asistente administrativo, asistente técnico" || EDBateriaUsuario.BateriaInicial.TipoCargo == "Operario, operador, ayudante, servicios generales")
                        {
                            tipo = 2;
                            evalriesgo = baremosExtra2();
                            evalriesgoDom = baremosExtra2();
                        }
                        foreach (var item in EDBateriaUsuario.ListaResultados)
                        {
                            int iddom = item.DominioInt;
                            int iddim = item.DimensionInt;

                            EDBateriaDominio EDBateriaDominio = ListaDominios.Where(s => s.Pk_Id_BateriaDimension == iddom).FirstOrDefault();
                            List<EDBateriaDimension> Listas = EDBateriaDominio.ListaDimensiones;
                            EDBateriaDimension EDBateriaDimension = Listas.Where(s => s.Pk_Id_BateriaDimension == iddim).FirstOrDefault();

                            EDBateriaDominio.Puntaje += item.ValorResultado;
                            EDBateriaDimension.Puntaje += item.ValorResultado;
                            EDBateriaUsuario.Puntaje += item.ValorResultado;
                        }
                        int cont = 1;
                        int cont1 = 1;
                        foreach (var item in ListaDominios)
                        {
                            item.PuntajeTrans = (item.Puntaje / (decimal)item.FactorTransformacion) * 100;
                            for (int i1 = 1; i1 < 6; i1++)
                            {
                                decimal CotaA = evalriesgoDom[cont1, i1, 1];
                                decimal CotaB = evalriesgoDom[cont1, i1, 2];
                                if (item.PuntajeTrans >= evalriesgoDom[cont1, i1, 1] && item.PuntajeTrans <= evalriesgoDom[cont1, i1, 2])
                                {
                                    if (i1 == 1)
                                    {
                                        item.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                    }
                                    if (i1 == 2)
                                    {
                                        item.NivelRiesgoDesc = "Riesgo bajo";
                                    }
                                    if (i1 == 3)
                                    {
                                        item.NivelRiesgoDesc = "Riesgo medio";
                                    }
                                    if (i1 == 4)
                                    {
                                        item.NivelRiesgoDesc = "Riesgo alto";
                                    }
                                    if (i1 == 5)
                                    {
                                        item.NivelRiesgoDesc = "Riesgo muy alto";
                                    }
                                }
                            }
                            if (item.NivelRiesgoDesc == null)
                            {
                                decimal round = Math.Round(item.PuntajeTrans, 1);
                                for (int i1 = 1; i1 < 6; i1++)
                                {
                                    decimal CotaA = evalriesgoDom[cont1, i1, 1];
                                    decimal CotaB = evalriesgoDom[cont1, i1, 2];
                                    if (round >= CotaA && round <= CotaB)
                                    {
                                        if (i1 == 1)
                                        {
                                            item.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                        }
                                        if (i1 == 2)
                                        {
                                            item.NivelRiesgoDesc = "Riesgo bajo";
                                        }
                                        if (i1 == 3)
                                        {
                                            item.NivelRiesgoDesc = "Riesgo medio";
                                        }
                                        if (i1 == 4)
                                        {
                                            item.NivelRiesgoDesc = "Riesgo alto";
                                        }
                                        if (i1 == 5)
                                        {
                                            item.NivelRiesgoDesc = "Riesgo muy alto";
                                        }
                                    }
                                }
                            }



                            foreach (var item1 in item.ListaDimensiones)
                            {
                                item1.PuntajeTrans = (item1.Puntaje / (decimal)item1.FactorTransformacion) * 100;

                                for (int i1 = 1; i1 < 6; i1++)
                                {
                                    decimal CotaA = evalriesgo[cont, i1, 1];
                                    decimal CotaB = evalriesgo[cont, i1, 2];
                                    if (item1.PuntajeTrans >= evalriesgo[cont, i1, 1] && item1.PuntajeTrans <= evalriesgo[cont, i1, 2])
                                    {
                                        if (i1 == 1)
                                        {
                                            item1.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                        }
                                        if (i1 == 2)
                                        {
                                            item1.NivelRiesgoDesc = "Riesgo bajo";
                                        }
                                        if (i1 == 3)
                                        {
                                            item1.NivelRiesgoDesc = "Riesgo medio";
                                        }
                                        if (i1 == 4)
                                        {
                                            item1.NivelRiesgoDesc = "Riesgo alto";
                                        }
                                        if (i1 == 5)
                                        {
                                            item1.NivelRiesgoDesc = "Riesgo muy alto";
                                        }
                                    }
                                }
                                if (item1.NivelRiesgoDesc == null)
                                {
                                    decimal round = Math.Round(item1.PuntajeTrans, 1);
                                    for (int i1 = 1; i1 < 6; i1++)
                                    {
                                        decimal CotaA = evalriesgo[cont, i1, 1];
                                        decimal CotaB = evalriesgo[cont, i1, 2];
                                        if (round >= CotaA && round <= CotaB)
                                        {
                                            if (i1 == 1)
                                            {
                                                item1.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                            }
                                            if (i1 == 2)
                                            {
                                                item1.NivelRiesgoDesc = "Riesgo bajo";
                                            }
                                            if (i1 == 3)
                                            {
                                                item1.NivelRiesgoDesc = "Riesgo medio";
                                            }
                                            if (i1 == 4)
                                            {
                                                item1.NivelRiesgoDesc = "Riesgo alto";
                                            }
                                            if (i1 == 5)
                                            {
                                                item1.NivelRiesgoDesc = "Riesgo muy alto";
                                            }
                                        }
                                    }
                                }
                                cont++;
                            }
                            cont1++;
                        }
                        cont1 = 1;
                        EDBateriaUsuario.PuntajeTrans = (EDBateriaUsuario.Puntaje / (decimal)EDBateriaUsuario.FactorTransformacion) * 100;
                        EDBateriaUsuario.Listadominios = new List<EDBateriaDominio>();
                        EDBateriaUsuario.Listadominios = ListaDominios;
                        decimal[,,] evalriesgoTotal = new decimal[10, 10, 10];

                        evalriesgoTotal = baremosTotalExtraIntraA();

                        for (int i1 = 1; i1 < 6; i1++)
                        {
                            decimal CotaA = evalriesgoTotal[cont1, i1, 1];
                            decimal CotaB = evalriesgoTotal[cont1, i1, 2];
                            if (EDBateriaUsuario.PuntajeTrans >= evalriesgoTotal[cont1, i1, 1] && EDBateriaUsuario.PuntajeTrans <= evalriesgoTotal[cont1, i1, 2])
                            {
                                if (i1 == 1)
                                {
                                    EDBateriaUsuario.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                }
                                if (i1 == 2)
                                {
                                    EDBateriaUsuario.NivelRiesgoDesc = "Riesgo bajo";
                                }
                                if (i1 == 3)
                                {
                                    EDBateriaUsuario.NivelRiesgoDesc = "Riesgo medio";
                                }
                                if (i1 == 4)
                                {
                                    EDBateriaUsuario.NivelRiesgoDesc = "Riesgo alto";
                                }
                                if (i1 == 5)
                                {
                                    EDBateriaUsuario.NivelRiesgoDesc = "Riesgo muy alto";
                                }
                            }
                        }
                        if (EDBateriaUsuario.NivelRiesgoDesc == null)
                        {
                            decimal round = Math.Round(EDBateriaUsuario.PuntajeTrans, 1);
                            for (int i1 = 1; i1 < 6; i1++)
                            {
                                decimal CotaA = evalriesgoTotal[cont, i1, 1];
                                decimal CotaB = evalriesgoTotal[cont, i1, 2];
                                if (round >= CotaA && round <= CotaB)
                                {
                                    if (i1 == 1)
                                    {
                                        EDBateriaUsuario.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                    }
                                    if (i1 == 2)
                                    {
                                        EDBateriaUsuario.NivelRiesgoDesc = "Riesgo bajo";
                                    }
                                    if (i1 == 3)
                                    {
                                        EDBateriaUsuario.NivelRiesgoDesc = "Riesgo medio";
                                    }
                                    if (i1 == 4)
                                    {
                                        EDBateriaUsuario.NivelRiesgoDesc = "Riesgo alto";
                                    }
                                    if (i1 == 5)
                                    {
                                        EDBateriaUsuario.NivelRiesgoDesc = "Riesgo muy alto";
                                    }
                                }
                            }
                        }

                    }
                    #endregion
                    #region ResultadosextraFormB


                    if (EDBateriaGestion.Fk_Id_Bateria == 2 && EDBateriaUsuario.NumeroIntentos == 1)
                    {
                        int tipo = 0;
                        decimal[,,] evalriesgo = new decimal[10, 10, 10];
                        decimal[,,] evalriesgoDom = new decimal[10, 10, 10];
                        if (EDBateriaUsuario.BateriaInicial.TipoCargo == "Jefatura - tiene personal a cargo" || EDBateriaUsuario.BateriaInicial.TipoCargo == "Profesional, analista, técnico, tecnólogo")
                        {
                            tipo = 1;
                            evalriesgo = baremosExtra1();
                            evalriesgoDom = baremosExtra1();
                        }
                        if (EDBateriaUsuario.BateriaInicial.TipoCargo == "Auxiliar, asistente administrativo, asistente técnico" || EDBateriaUsuario.BateriaInicial.TipoCargo == "Operario, operador, ayudante, servicios generales")
                        {
                            tipo = 2;
                            evalriesgo = baremosExtra2();
                            evalriesgoDom = baremosExtra2();
                        }
                        foreach (var item in EDBateriaUsuario.ListaResultados)
                        {
                            int iddom = item.DominioInt;
                            int iddim = item.DimensionInt;

                            EDBateriaDominio EDBateriaDominio = ListaDominios.Where(s => s.Pk_Id_BateriaDimension == iddom).FirstOrDefault();
                            List<EDBateriaDimension> Listas = EDBateriaDominio.ListaDimensiones;
                            EDBateriaDimension EDBateriaDimension = Listas.Where(s => s.Pk_Id_BateriaDimension == iddim).FirstOrDefault();

                            EDBateriaDominio.Puntaje += item.ValorResultado;
                            EDBateriaDimension.Puntaje += item.ValorResultado;
                            EDBateriaUsuario.Puntaje += item.ValorResultado;
                        }
                        int cont = 1;
                        int cont1 = 1;
                        foreach (var item in ListaDominios)
                        {
                            item.PuntajeTrans = (item.Puntaje / (decimal)item.FactorTransformacion) * 100;
                            for (int i1 = 1; i1 < 6; i1++)
                            {
                                decimal CotaA = evalriesgoDom[cont1, i1, 1];
                                decimal CotaB = evalriesgoDom[cont1, i1, 2];
                                if (item.PuntajeTrans >= evalriesgoDom[cont1, i1, 1] && item.PuntajeTrans <= evalriesgoDom[cont1, i1, 2])
                                {
                                    if (i1 == 1)
                                    {
                                        item.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                    }
                                    if (i1 == 2)
                                    {
                                        item.NivelRiesgoDesc = "Riesgo bajo";
                                    }
                                    if (i1 == 3)
                                    {
                                        item.NivelRiesgoDesc = "Riesgo medio";
                                    }
                                    if (i1 == 4)
                                    {
                                        item.NivelRiesgoDesc = "Riesgo alto";
                                    }
                                    if (i1 == 5)
                                    {
                                        item.NivelRiesgoDesc = "Riesgo muy alto";
                                    }
                                }
                            }
                            if (item.NivelRiesgoDesc == null)
                            {
                                decimal round = Math.Round(item.PuntajeTrans, 1);
                                for (int i1 = 1; i1 < 6; i1++)
                                {
                                    decimal CotaA = evalriesgoDom[cont1, i1, 1];
                                    decimal CotaB = evalriesgoDom[cont1, i1, 2];
                                    if (round >= CotaA && round <= CotaB)
                                    {
                                        if (i1 == 1)
                                        {
                                            item.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                        }
                                        if (i1 == 2)
                                        {
                                            item.NivelRiesgoDesc = "Riesgo bajo";
                                        }
                                        if (i1 == 3)
                                        {
                                            item.NivelRiesgoDesc = "Riesgo medio";
                                        }
                                        if (i1 == 4)
                                        {
                                            item.NivelRiesgoDesc = "Riesgo alto";
                                        }
                                        if (i1 == 5)
                                        {
                                            item.NivelRiesgoDesc = "Riesgo muy alto";
                                        }
                                    }
                                }
                            }



                            foreach (var item1 in item.ListaDimensiones)
                            {
                                item1.PuntajeTrans = (item1.Puntaje / (decimal)item1.FactorTransformacion) * 100;

                                for (int i1 = 1; i1 < 6; i1++)
                                {
                                    decimal CotaA = evalriesgo[cont, i1, 1];
                                    decimal CotaB = evalriesgo[cont, i1, 2];
                                    if (item1.PuntajeTrans >= evalriesgo[cont, i1, 1] && item1.PuntajeTrans <= evalriesgo[cont, i1, 2])
                                    {
                                        if (i1 == 1)
                                        {
                                            item1.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                        }
                                        if (i1 == 2)
                                        {
                                            item1.NivelRiesgoDesc = "Riesgo bajo";
                                        }
                                        if (i1 == 3)
                                        {
                                            item1.NivelRiesgoDesc = "Riesgo medio";
                                        }
                                        if (i1 == 4)
                                        {
                                            item1.NivelRiesgoDesc = "Riesgo alto";
                                        }
                                        if (i1 == 5)
                                        {
                                            item1.NivelRiesgoDesc = "Riesgo muy alto";
                                        }
                                    }
                                }
                                if (item1.NivelRiesgoDesc == null)
                                {
                                    decimal round = Math.Round(item1.PuntajeTrans, 1);
                                    for (int i1 = 1; i1 < 6; i1++)
                                    {
                                        decimal CotaA = evalriesgo[cont, i1, 1];
                                        decimal CotaB = evalriesgo[cont, i1, 2];
                                        if (round >= CotaA && round <= CotaB)
                                        {
                                            if (i1 == 1)
                                            {
                                                item1.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                            }
                                            if (i1 == 2)
                                            {
                                                item1.NivelRiesgoDesc = "Riesgo bajo";
                                            }
                                            if (i1 == 3)
                                            {
                                                item1.NivelRiesgoDesc = "Riesgo medio";
                                            }
                                            if (i1 == 4)
                                            {
                                                item1.NivelRiesgoDesc = "Riesgo alto";
                                            }
                                            if (i1 == 5)
                                            {
                                                item1.NivelRiesgoDesc = "Riesgo muy alto";
                                            }
                                        }
                                    }
                                }
                                cont++;
                            }
                            cont1++;
                        }
                        cont1 = 1;
                        EDBateriaUsuario.PuntajeTrans = (EDBateriaUsuario.Puntaje / (decimal)EDBateriaUsuario.FactorTransformacion) * 100;
                        EDBateriaUsuario.Listadominios = new List<EDBateriaDominio>();
                        EDBateriaUsuario.Listadominios = ListaDominios;
                        decimal[,,] evalriesgoTotal = new decimal[10, 10, 10];

                        evalriesgoTotal = baremosTotalExtraIntraB();

                        for (int i1 = 1; i1 < 6; i1++)
                        {
                            decimal CotaA = evalriesgoTotal[cont1, i1, 1];
                            decimal CotaB = evalriesgoTotal[cont1, i1, 2];
                            if (EDBateriaUsuario.PuntajeTrans >= evalriesgoTotal[cont1, i1, 1] && EDBateriaUsuario.PuntajeTrans <= evalriesgoTotal[cont1, i1, 2])
                            {
                                if (i1 == 1)
                                {
                                    EDBateriaUsuario.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                }
                                if (i1 == 2)
                                {
                                    EDBateriaUsuario.NivelRiesgoDesc = "Riesgo bajo";
                                }
                                if (i1 == 3)
                                {
                                    EDBateriaUsuario.NivelRiesgoDesc = "Riesgo medio";
                                }
                                if (i1 == 4)
                                {
                                    EDBateriaUsuario.NivelRiesgoDesc = "Riesgo alto";
                                }
                                if (i1 == 5)
                                {
                                    EDBateriaUsuario.NivelRiesgoDesc = "Riesgo muy alto";
                                }
                            }
                        }
                        if (EDBateriaUsuario.NivelRiesgoDesc == null)
                        {
                            decimal round = Math.Round(EDBateriaUsuario.PuntajeTrans, 1);
                            for (int i1 = 1; i1 < 6; i1++)
                            {
                                decimal CotaA = evalriesgoTotal[cont, i1, 1];
                                decimal CotaB = evalriesgoTotal[cont, i1, 2];
                                if (round >= CotaA && round <= CotaB)
                                {
                                    if (i1 == 1)
                                    {
                                        EDBateriaUsuario.NivelRiesgoDesc = "Sin riesgo o riesgo despreciable";
                                    }
                                    if (i1 == 2)
                                    {
                                        EDBateriaUsuario.NivelRiesgoDesc = "Riesgo bajo";
                                    }
                                    if (i1 == 3)
                                    {
                                        EDBateriaUsuario.NivelRiesgoDesc = "Riesgo medio";
                                    }
                                    if (i1 == 4)
                                    {
                                        EDBateriaUsuario.NivelRiesgoDesc = "Riesgo alto";
                                    }
                                    if (i1 == 5)
                                    {
                                        EDBateriaUsuario.NivelRiesgoDesc = "Riesgo muy alto";
                                    }
                                }
                            }
                        }
                    }
                    #endregion

                    #region ResultadosEstres


                    if (EDBateriaGestion.Fk_Id_Bateria == 4)
                    {

                        decimal[,,] evalriesgo = new decimal[10, 10, 10];
                        if (EDBateriaUsuario.BateriaInicial.TipoCargo == "Jefatura - tiene personal a cargo" || EDBateriaUsuario.BateriaInicial.TipoCargo == "Profesional, analista, técnico, tecnólogo")
                        {
                            evalriesgo = baremosEstres1();
                        }
                        if (EDBateriaUsuario.BateriaInicial.TipoCargo == "Auxiliar, asistente administrativo, asistente técnico" || EDBateriaUsuario.BateriaInicial.TipoCargo == "Operario, operador, ayudante, servicios generales")
                        {
                            evalriesgo = baremosEstres2();
                        }
                        decimal promedio1 = 0;
                        decimal promedio2 = 0;
                        decimal promedio3 = 0;
                        decimal promedio4 = 0;
                        int contp1 = 0;
                        int contp2 = 0;
                        int contp3 = 0;
                        int contp4 = 0;

                        int comienza = EDBateriaUsuario.ListaResultados.First().Fk_Id_BateriaCuestionario;
                        foreach (var item in EDBateriaUsuario.ListaResultados)
                        {
                            //int iddom = item.DominioInt;
                            //int iddim = item.DimensionInt;

                            //EDBateriaDominio EDBateriaDominio = ListaDominios.Where(s => s.Pk_Id_BateriaDimension == iddom).FirstOrDefault();
                            //List<EDBateriaDimension> Listas = EDBateriaDominio.ListaDimensiones;
                            //EDBateriaDimension EDBateriaDimension = Listas.Where(s => s.Pk_Id_BateriaDimension == iddim).FirstOrDefault();

                            //EDBateriaDominio.Puntaje += item.ValorResultado;
                            //EDBateriaDimension.Puntaje += item.ValorResultado;
                            //EDBateriaUsuario.Puntaje += item.ValorResultado;

                            if (item.Fk_Id_BateriaCuestionario >= comienza && item.Fk_Id_BateriaCuestionario <= comienza + 7)
                            {
                                promedio1 += item.ValorResultado;
                                contp1++;
                            }
                            if (item.Fk_Id_BateriaCuestionario >= comienza + 8 && item.Fk_Id_BateriaCuestionario <= comienza + 11)
                            {
                                promedio2 += item.ValorResultado;
                                contp2++;
                            }
                            if (item.Fk_Id_BateriaCuestionario >= comienza + 12 && item.Fk_Id_BateriaCuestionario <= comienza + 21)
                            {
                                promedio3 += item.ValorResultado;
                                contp3++;
                            }
                            if (item.Fk_Id_BateriaCuestionario >= comienza + 22 && item.Fk_Id_BateriaCuestionario <= comienza + 30)
                            {
                                promedio4 += item.ValorResultado;
                                contp4++;
                            }
                        }
                        promedio1 = promedio1 / contp1;
                        promedio2 = promedio2 / contp2;
                        promedio3 = promedio3 / contp3;
                        promedio4 = promedio4 / contp4;

                        promedio1 = promedio1 * 4;
                        promedio2 = promedio2 * 3;
                        promedio3 = promedio3 * 2;

                        decimal puntajetotal = promedio1 + promedio2 + promedio3 + promedio4;
                        EDBateriaUsuario.Puntaje = puntajetotal;
                        int cont1 = 1;
                        EDBateriaUsuario.PuntajeTrans = (EDBateriaUsuario.Puntaje / (decimal)EDBateriaUsuario.FactorTransformacion) * 100;
                        EDBateriaUsuario.Listadominios = new List<EDBateriaDominio>();
                        EDBateriaUsuario.Listadominios = ListaDominios;
                        decimal[,,] evalriesgoTotal = new decimal[10, 10, 10];
                        evalriesgoTotal = evalriesgo;

                        for (int i1 = 1; i1 < 6; i1++)
                        {
                            decimal CotaA = evalriesgoTotal[cont1, i1, 1];
                            decimal CotaB = evalriesgoTotal[cont1, i1, 2];
                            if (EDBateriaUsuario.PuntajeTrans >= evalriesgoTotal[cont1, i1, 1] && EDBateriaUsuario.PuntajeTrans <= evalriesgoTotal[cont1, i1, 2])
                            {
                                if (i1 == 1)
                                {
                                    EDBateriaUsuario.NivelRiesgoDesc = "Muy bajo";
                                }
                                if (i1 == 2)
                                {
                                    EDBateriaUsuario.NivelRiesgoDesc = "Bajo";
                                }
                                if (i1 == 3)
                                {
                                    EDBateriaUsuario.NivelRiesgoDesc = "Medio";
                                }
                                if (i1 == 4)
                                {
                                    EDBateriaUsuario.NivelRiesgoDesc = "Alto";
                                }
                                if (i1 == 5)
                                {
                                    EDBateriaUsuario.NivelRiesgoDesc = "Muy alto";
                                }
                            }
                        }
                        if (EDBateriaUsuario.NivelRiesgoDesc == null)
                        {
                            decimal round = Math.Round(EDBateriaUsuario.PuntajeTrans, 1);
                            for (int i1 = 1; i1 < 6; i1++)
                            {
                                decimal CotaA = evalriesgoTotal[cont1, i1, 1];
                                decimal CotaB = evalriesgoTotal[cont1, i1, 2];
                                if (EDBateriaUsuario.PuntajeTrans >= evalriesgoTotal[cont1, i1, 1] && EDBateriaUsuario.PuntajeTrans <= evalriesgoTotal[cont1, i1, 2])
                                {
                                    if (i1 == 1)
                                    {
                                        EDBateriaUsuario.NivelRiesgoDesc = "Muy bajo";
                                    }
                                    if (i1 == 2)
                                    {
                                        EDBateriaUsuario.NivelRiesgoDesc = "Bajo";
                                    }
                                    if (i1 == 3)
                                    {
                                        EDBateriaUsuario.NivelRiesgoDesc = "Medio";
                                    }
                                    if (i1 == 4)
                                    {
                                        EDBateriaUsuario.NivelRiesgoDesc = "Alto";
                                    }
                                    if (i1 == 5)
                                    {
                                        EDBateriaUsuario.NivelRiesgoDesc = "Muy alto";
                                    }
                                }
                            }
                        }

                    }
                    #endregion

                    #endregion
                }
            }
            ViewBag.pdf = mostrarPDF;
            return View(EDBateriaUsuario);
        }

        #region baremos

        private decimal[,,] baremosIntraA()
        {
            decimal[,,] NivelRiesgo = new decimal[20, 6, 3];
            NivelRiesgo[1, 1, 1] = 0.0M;
            NivelRiesgo[2, 1, 1] = 0.0M;
            NivelRiesgo[3, 1, 1] = 0.0M;
            NivelRiesgo[4, 1, 1] = 0.0M;
            NivelRiesgo[5, 1, 1] = 0.0M;
            NivelRiesgo[6, 1, 1] = 0.0M;
            NivelRiesgo[7, 1, 1] = 0.0M;
            NivelRiesgo[8, 1, 1] = 0.0M;
            NivelRiesgo[9, 1, 1] = 0.0M;
            NivelRiesgo[10, 1, 1] = 0.0M;
            NivelRiesgo[11, 1, 1] = 0.0M;
            NivelRiesgo[12, 1, 1] = 0.0M;
            NivelRiesgo[13, 1, 1] = 0.0M;
            NivelRiesgo[14, 1, 1] = 0.0M;
            NivelRiesgo[15, 1, 1] = 0.0M;
            NivelRiesgo[16, 1, 1] = 0.0M;
            NivelRiesgo[17, 1, 1] = 0.0M;
            NivelRiesgo[18, 1, 1] = 0.0M;
            NivelRiesgo[19, 1, 1] = 0.0M;
            NivelRiesgo[1, 1, 2] = 3.8M;
            NivelRiesgo[2, 1, 2] = 5.4M;
            NivelRiesgo[3, 1, 2] = 10.0M;
            NivelRiesgo[4, 1, 2] = 13.9M;
            NivelRiesgo[5, 1, 2] = 0.9M;
            NivelRiesgo[6, 1, 2] = 0.9M;
            NivelRiesgo[7, 1, 2] = 12.5M;
            NivelRiesgo[8, 1, 2] = 0.9M;
            NivelRiesgo[9, 1, 2] = 8.3M;
            NivelRiesgo[10, 1, 2] = 14.6M;
            NivelRiesgo[11, 1, 2] = 16.7M;
            NivelRiesgo[12, 1, 2] = 25.0M;
            NivelRiesgo[13, 1, 2] = 18.8M;
            NivelRiesgo[14, 1, 2] = 37.5M;
            NivelRiesgo[15, 1, 2] = 60.0M;
            NivelRiesgo[16, 1, 2] = 15.0M;
            NivelRiesgo[17, 1, 2] = 8.3M;
            NivelRiesgo[18, 1, 2] = 0.9M;
            NivelRiesgo[19, 1, 2] = 4.2M;
            NivelRiesgo[1, 2, 1] = 3.9M;
            NivelRiesgo[2, 2, 1] = 5.5M;
            NivelRiesgo[3, 2, 1] = 10.1M;
            NivelRiesgo[4, 2, 1] = 14.0M;
            NivelRiesgo[5, 2, 1] = 1.0M;
            NivelRiesgo[6, 2, 1] = 1.0M;
            NivelRiesgo[7, 2, 1] = 12.6M;
            NivelRiesgo[8, 2, 1] = 1.0M;
            NivelRiesgo[9, 2, 1] = 8.4M;
            NivelRiesgo[10, 2, 1] = 14.7M;
            NivelRiesgo[11, 2, 1] = 16.8M;
            NivelRiesgo[12, 2, 1] = 25.1M;
            NivelRiesgo[13, 2, 1] = 18.9M;
            NivelRiesgo[14, 2, 1] = 37.6M;
            NivelRiesgo[15, 2, 1] = 60.1M;
            NivelRiesgo[16, 2, 1] = 15.1M;
            NivelRiesgo[17, 2, 1] = 8.4M;
            NivelRiesgo[18, 2, 1] = 1.0M;
            NivelRiesgo[19, 2, 1] = 4.3M;
            NivelRiesgo[1, 2, 2] = 15.4M;
            NivelRiesgo[2, 2, 2] = 16.1M;
            NivelRiesgo[3, 2, 2] = 25.0M;
            NivelRiesgo[4, 2, 2] = 25.0M;
            NivelRiesgo[5, 2, 2] = 10.7M;
            NivelRiesgo[6, 2, 2] = 16.7M;
            NivelRiesgo[7, 2, 2] = 25.0M;
            NivelRiesgo[8, 2, 2] = 6.3M;
            NivelRiesgo[9, 2, 2] = 25.0M;
            NivelRiesgo[10, 2, 2] = 22.9M;
            NivelRiesgo[11, 2, 2] = 25.0M;
            NivelRiesgo[12, 2, 2] = 33.3M;
            NivelRiesgo[13, 2, 2] = 31.3M;
            NivelRiesgo[14, 2, 2] = 54.2M;
            NivelRiesgo[15, 2, 2] = 70.0M;
            NivelRiesgo[16, 2, 2] = 25.0M;
            NivelRiesgo[17, 2, 2] = 25.0M;
            NivelRiesgo[18, 2, 2] = 5.0M;
            NivelRiesgo[19, 2, 2] = 16.7M;
            NivelRiesgo[1, 3, 1] = 15.5M;
            NivelRiesgo[2, 3, 1] = 16.2M;
            NivelRiesgo[3, 3, 1] = 25.1M;
            NivelRiesgo[4, 3, 1] = 25.1M;
            NivelRiesgo[5, 3, 1] = 10.8M;
            NivelRiesgo[6, 3, 1] = 16.8M;
            NivelRiesgo[7, 3, 1] = 25.1M;
            NivelRiesgo[8, 3, 1] = 6.4M;
            NivelRiesgo[9, 3, 1] = 25.1M;
            NivelRiesgo[10, 3, 1] = 23.0M;
            NivelRiesgo[11, 3, 1] = 25.1M;
            NivelRiesgo[12, 3, 1] = 33.4M;
            NivelRiesgo[13, 3, 1] = 31.4M;
            NivelRiesgo[14, 3, 1] = 54.3M;
            NivelRiesgo[15, 3, 1] = 70.1M;
            NivelRiesgo[16, 3, 1] = 25.1M;
            NivelRiesgo[17, 3, 1] = 25.1M;
            NivelRiesgo[18, 3, 1] = 5.1M;
            NivelRiesgo[19, 3, 1] = 16.8M;
            NivelRiesgo[1, 3, 2] = 30.8M;
            NivelRiesgo[2, 3, 2] = 25.0M;
            NivelRiesgo[3, 3, 2] = 40.0M;
            NivelRiesgo[4, 3, 2] = 33.3M;
            NivelRiesgo[5, 3, 2] = 21.4M;
            NivelRiesgo[6, 3, 2] = 33.3M;
            NivelRiesgo[7, 3, 2] = 37.5M;
            NivelRiesgo[8, 3, 2] = 18.8M;
            NivelRiesgo[9, 3, 2] = 41.7M;
            NivelRiesgo[10, 3, 2] = 31.3M;
            NivelRiesgo[11, 3, 2] = 33.3M;
            NivelRiesgo[12, 3, 2] = 45.8M;
            NivelRiesgo[13, 3, 2] = 43.8M;
            NivelRiesgo[14, 3, 2] = 66.7M;
            NivelRiesgo[15, 3, 2] = 80.0M;
            NivelRiesgo[16, 3, 2] = 35.0M;
            NivelRiesgo[17, 3, 2] = 33.3M;
            NivelRiesgo[18, 3, 2] = 10.0M;
            NivelRiesgo[19, 3, 2] = 25.0M;
            NivelRiesgo[1, 4, 1] = 30.9M;
            NivelRiesgo[2, 4, 1] = 25.1M;
            NivelRiesgo[3, 4, 1] = 40.1M;
            NivelRiesgo[4, 4, 1] = 33.4M;
            NivelRiesgo[5, 4, 1] = 21.5M;
            NivelRiesgo[6, 4, 1] = 33.4M;
            NivelRiesgo[7, 4, 1] = 37.6M;
            NivelRiesgo[8, 4, 1] = 18.9M;
            NivelRiesgo[9, 4, 1] = 41.8M;
            NivelRiesgo[10, 4, 1] = 31.4M;
            NivelRiesgo[11, 4, 1] = 33.4M;
            NivelRiesgo[12, 4, 1] = 45.9M;
            NivelRiesgo[13, 4, 1] = 43.9M;
            NivelRiesgo[14, 4, 1] = 66.8M;
            NivelRiesgo[15, 4, 1] = 80.1M;
            NivelRiesgo[16, 4, 1] = 35.1M;
            NivelRiesgo[17, 4, 1] = 33.4M;
            NivelRiesgo[18, 4, 1] = 10.1M;
            NivelRiesgo[19, 4, 1] = 25.1M;
            NivelRiesgo[1, 4, 2] = 46.2M;
            NivelRiesgo[2, 4, 2] = 37.5M;
            NivelRiesgo[3, 4, 2] = 55.0M;
            NivelRiesgo[4, 4, 2] = 47.2M;
            NivelRiesgo[5, 4, 2] = 39.3M;
            NivelRiesgo[6, 4, 2] = 50.0M;
            NivelRiesgo[7, 4, 2] = 50.0M;
            NivelRiesgo[8, 4, 2] = 31.3M;
            NivelRiesgo[9, 4, 2] = 58.3M;
            NivelRiesgo[10, 4, 2] = 39.6M;
            NivelRiesgo[11, 4, 2] = 47.2M;
            NivelRiesgo[12, 4, 2] = 54.2M;
            NivelRiesgo[13, 4, 2] = 50.0M;
            NivelRiesgo[14, 4, 2] = 79.2M;
            NivelRiesgo[15, 4, 2] = 90.0M;
            NivelRiesgo[16, 4, 2] = 45.0M;
            NivelRiesgo[17, 4, 2] = 50.0M;
            NivelRiesgo[18, 4, 2] = 20.0M;
            NivelRiesgo[19, 4, 2] = 37.5M;
            NivelRiesgo[1, 5, 1] = 46.3M;
            NivelRiesgo[2, 5, 1] = 37.6M;
            NivelRiesgo[3, 5, 1] = 55.1M;
            NivelRiesgo[4, 5, 1] = 47.3M;
            NivelRiesgo[5, 5, 1] = 39.4M;
            NivelRiesgo[6, 5, 1] = 50.1M;
            NivelRiesgo[7, 5, 1] = 50.1M;
            NivelRiesgo[8, 5, 1] = 31.4M;
            NivelRiesgo[9, 5, 1] = 58.4M;
            NivelRiesgo[10, 5, 1] = 39.7M;
            NivelRiesgo[11, 5, 1] = 47.3M;
            NivelRiesgo[12, 5, 1] = 54.3M;
            NivelRiesgo[13, 5, 1] = 50.1M;
            NivelRiesgo[14, 5, 1] = 79.3M;
            NivelRiesgo[15, 5, 1] = 90.1M;
            NivelRiesgo[16, 5, 1] = 45.1M;
            NivelRiesgo[17, 5, 1] = 50.1M;
            NivelRiesgo[18, 5, 1] = 20.1M;
            NivelRiesgo[19, 5, 1] = 37.6M;
            NivelRiesgo[1, 5, 2] = 100M;
            NivelRiesgo[2, 5, 2] = 100M;
            NivelRiesgo[3, 5, 2] = 100M;
            NivelRiesgo[4, 5, 2] = 100M;
            NivelRiesgo[5, 5, 2] = 100M;
            NivelRiesgo[6, 5, 2] = 100M;
            NivelRiesgo[7, 5, 2] = 100M;
            NivelRiesgo[8, 5, 2] = 100M;
            NivelRiesgo[9, 5, 2] = 100M;
            NivelRiesgo[10, 5, 2] = 100M;
            NivelRiesgo[11, 5, 2] = 100M;
            NivelRiesgo[12, 5, 2] = 100M;
            NivelRiesgo[13, 5, 2] = 100M;
            NivelRiesgo[14, 5, 2] = 100M;
            NivelRiesgo[15, 5, 2] = 100M;
            NivelRiesgo[16, 5, 2] = 100M;
            NivelRiesgo[17, 5, 2] = 100M;
            NivelRiesgo[18, 5, 2] = 100M;
            NivelRiesgo[19, 5, 2] = 100M;

            return NivelRiesgo;

        }
        private decimal[,,] baremosDominioIntraA()
        {
            decimal[,,] NivelRiesgo = new decimal[5, 6, 3];
            NivelRiesgo[1, 1, 1] = 0.0M;
            NivelRiesgo[2, 1, 1] = 0.0M;
            NivelRiesgo[3, 1, 1] = 0.0M;
            NivelRiesgo[4, 1, 1] = 0.0M;
            NivelRiesgo[1, 1, 2] = 9.1M;
            NivelRiesgo[2, 1, 2] = 10.7M;
            NivelRiesgo[3, 1, 2] = 28.5M;
            NivelRiesgo[4, 1, 2] = 4.5M;
            NivelRiesgo[1, 2, 1] = 9.2M;
            NivelRiesgo[2, 2, 1] = 10.8M;
            NivelRiesgo[3, 2, 1] = 28.6M;
            NivelRiesgo[4, 2, 1] = 4.6M;
            NivelRiesgo[1, 2, 2] = 17.7M;
            NivelRiesgo[2, 2, 2] = 19.0M;
            NivelRiesgo[3, 2, 2] = 35.0M;
            NivelRiesgo[4, 2, 2] = 11.4M;
            NivelRiesgo[1, 3, 1] = 17.8M;
            NivelRiesgo[2, 3, 1] = 19.1M;
            NivelRiesgo[3, 3, 1] = 35.1M;
            NivelRiesgo[4, 3, 1] = 11.5M;
            NivelRiesgo[1, 3, 2] = 25.6M;
            NivelRiesgo[2, 3, 2] = 29.8M;
            NivelRiesgo[3, 3, 2] = 41.5M;
            NivelRiesgo[4, 3, 2] = 20.5M;
            NivelRiesgo[1, 4, 1] = 25.7M;
            NivelRiesgo[2, 4, 1] = 29.9M;
            NivelRiesgo[3, 4, 1] = 41.6M;
            NivelRiesgo[4, 4, 1] = 20.6M;
            NivelRiesgo[1, 4, 2] = 34.8M;
            NivelRiesgo[2, 4, 2] = 40.5M;
            NivelRiesgo[3, 4, 2] = 47.5M;
            NivelRiesgo[4, 4, 2] = 29.5M;
            NivelRiesgo[1, 5, 1] = 34.9M;
            NivelRiesgo[2, 5, 1] = 40.6M;
            NivelRiesgo[3, 5, 1] = 47.6M;
            NivelRiesgo[4, 5, 1] = 29.6M;
            NivelRiesgo[1, 5, 2] = 100M;
            NivelRiesgo[2, 5, 2] = 100M;
            NivelRiesgo[3, 5, 2] = 100M;
            NivelRiesgo[4, 5, 2] = 100M;

            return NivelRiesgo;

        }
        private decimal[,,] baremosIntraB()
        {
            decimal[,,] NivelRiesgo = new decimal[17, 6, 3];
            NivelRiesgo[1, 1, 1] = 0.0M; NivelRiesgo[1, 2, 2] = 13.5M; NivelRiesgo[1, 4, 1] = 25.1M; NivelRiesgo[1, 5, 2] = 100M;
            NivelRiesgo[2, 1, 1] = 0.0M; NivelRiesgo[2, 2, 2] = 14.6M; NivelRiesgo[2, 4, 1] = 27.2M; NivelRiesgo[2, 5, 2] = 100M;
            NivelRiesgo[3, 1, 1] = 0.0M; NivelRiesgo[3, 2, 2] = 20.0M; NivelRiesgo[3, 4, 1] = 30.1M; NivelRiesgo[3, 5, 2] = 100M;
            NivelRiesgo[4, 1, 1] = 0.0M; NivelRiesgo[4, 2, 2] = 5.0M; NivelRiesgo[4, 4, 1] = 15.1M; NivelRiesgo[4, 5, 2] = 100M;
            NivelRiesgo[5, 1, 1] = 0.0M; NivelRiesgo[5, 2, 2] = 16.7M; NivelRiesgo[5, 4, 1] = 25.1M; NivelRiesgo[5, 5, 2] = 100M;
            NivelRiesgo[6, 1, 1] = 0.0M; NivelRiesgo[6, 2, 2] = 33.3M; NivelRiesgo[6, 4, 1] = 41.8M; NivelRiesgo[6, 5, 2] = 100M;
            NivelRiesgo[7, 1, 1] = 0.0M; NivelRiesgo[7, 2, 2] = 25.0M; NivelRiesgo[7, 4, 1] = 37.6M; NivelRiesgo[7, 5, 2] = 100M;
            NivelRiesgo[8, 1, 1] = 0.0M; NivelRiesgo[8, 2, 2] = 50.0M; NivelRiesgo[8, 4, 1] = 66.8M; NivelRiesgo[8, 5, 2] = 100M;
            NivelRiesgo[9, 1, 1] = 0.0M; NivelRiesgo[9, 2, 2] = 31.3M; NivelRiesgo[9, 4, 1] = 39.7M; NivelRiesgo[9, 5, 2] = 100M;
            NivelRiesgo[10, 1, 1] = 0.0M; NivelRiesgo[10, 2, 2] = 27.8M; NivelRiesgo[10, 4, 1] = 39.0M; NivelRiesgo[10, 5, 2] = 100M;
            NivelRiesgo[11, 1, 1] = 0.0M; NivelRiesgo[11, 2, 2] = 33.3M; NivelRiesgo[11, 4, 1] = 41.8M; NivelRiesgo[11, 5, 2] = 100M;
            NivelRiesgo[12, 1, 1] = 0.0M; NivelRiesgo[12, 2, 2] = 25.0M; NivelRiesgo[12, 4, 1] = 31.4M; NivelRiesgo[12, 5, 2] = 100M;
            NivelRiesgo[13, 1, 1] = 0.0M; NivelRiesgo[13, 2, 2] = 65.0M; NivelRiesgo[13, 4, 1] = 75.1M; NivelRiesgo[13, 5, 2] = 100M;
            NivelRiesgo[14, 1, 1] = 0.0M; NivelRiesgo[14, 2, 2] = 37.5M; NivelRiesgo[14, 4, 1] = 45.9M; NivelRiesgo[14, 5, 2] = 100M;
            NivelRiesgo[15, 1, 1] = 0.0M; NivelRiesgo[15, 2, 2] = 6.3M; NivelRiesgo[15, 4, 1] = 12.6M; NivelRiesgo[15, 5, 2] = 100M;
            NivelRiesgo[16, 1, 1] = 0.0M; NivelRiesgo[16, 2, 2] = 12.5M; NivelRiesgo[16, 4, 1] = 25.1M; NivelRiesgo[16, 5, 2] = 100M;
            NivelRiesgo[1, 1, 2] = 3.8M; NivelRiesgo[1, 3, 1] = 13.6M; NivelRiesgo[1, 4, 2] = 38.5M;
            NivelRiesgo[2, 1, 2] = 6.3M; NivelRiesgo[2, 3, 1] = 14.7M; NivelRiesgo[2, 4, 2] = 37.5M;
            NivelRiesgo[3, 1, 2] = 5.0M; NivelRiesgo[3, 3, 1] = 20.1M; NivelRiesgo[3, 4, 2] = 50.0M;
            NivelRiesgo[4, 1, 2] = 0.9M; NivelRiesgo[4, 3, 1] = 5.1M; NivelRiesgo[4, 4, 2] = 30.0M;
            NivelRiesgo[5, 1, 2] = 0.9M; NivelRiesgo[5, 3, 1] = 16.8M; NivelRiesgo[5, 4, 2] = 50.0M;
            NivelRiesgo[6, 1, 2] = 16.7M; NivelRiesgo[6, 3, 1] = 33.4M; NivelRiesgo[6, 4, 2] = 58.3M;
            NivelRiesgo[7, 1, 2] = 12.5M; NivelRiesgo[7, 3, 1] = 25.1M; NivelRiesgo[7, 4, 2] = 56.3M;
            NivelRiesgo[8, 1, 2] = 33.3M; NivelRiesgo[8, 3, 1] = 50.1M; NivelRiesgo[8, 4, 2] = 75.0M;
            NivelRiesgo[9, 1, 2] = 22.9M; NivelRiesgo[9, 3, 1] = 31.4M; NivelRiesgo[9, 4, 2] = 47.9M;
            NivelRiesgo[10, 1, 2] = 19.4M; NivelRiesgo[10, 3, 1] = 27.9M; NivelRiesgo[10, 4, 2] = 47.2M;
            NivelRiesgo[11, 1, 2] = 16.7M; NivelRiesgo[11, 3, 1] = 33.4M; NivelRiesgo[11, 4, 2] = 50.0M;
            NivelRiesgo[12, 1, 2] = 12.5M; NivelRiesgo[12, 3, 1] = 25.1M; NivelRiesgo[12, 4, 2] = 50.0M;
            NivelRiesgo[13, 1, 2] = 50.0M; NivelRiesgo[13, 3, 1] = 65.1M; NivelRiesgo[13, 4, 2] = 85.0M;
            NivelRiesgo[14, 1, 2] = 25.0M; NivelRiesgo[14, 3, 1] = 37.6M; NivelRiesgo[14, 4, 2] = 58.3M;
            NivelRiesgo[15, 1, 2] = 0.9M; NivelRiesgo[15, 3, 1] = 6.4M; NivelRiesgo[15, 4, 2] = 18.8M;
            NivelRiesgo[16, 1, 2] = 0.9M; NivelRiesgo[16, 3, 1] = 12.6M; NivelRiesgo[16, 4, 2] = 37.5M;
            NivelRiesgo[1, 2, 1] = 3.9M; NivelRiesgo[1, 3, 2] = 25.0M; NivelRiesgo[1, 5, 1] = 38.6M;
            NivelRiesgo[2, 2, 1] = 6.4M; NivelRiesgo[2, 3, 2] = 27.1M; NivelRiesgo[2, 5, 1] = 37.6M;
            NivelRiesgo[3, 2, 1] = 5.1M; NivelRiesgo[3, 3, 2] = 30.0M; NivelRiesgo[3, 5, 1] = 50.1M;
            NivelRiesgo[4, 2, 1] = 1.0M; NivelRiesgo[4, 3, 2] = 15.0M; NivelRiesgo[4, 5, 1] = 30.1M;
            NivelRiesgo[5, 2, 1] = 1.0M; NivelRiesgo[5, 3, 2] = 25.0M; NivelRiesgo[5, 5, 1] = 50.1M;
            NivelRiesgo[6, 2, 1] = 16.8M; NivelRiesgo[6, 3, 2] = 41.7M; NivelRiesgo[6, 5, 1] = 58.4M;
            NivelRiesgo[7, 2, 1] = 12.6M; NivelRiesgo[7, 3, 2] = 37.5M; NivelRiesgo[7, 5, 1] = 56.4M;
            NivelRiesgo[8, 2, 1] = 33.4M; NivelRiesgo[8, 3, 2] = 66.7M; NivelRiesgo[8, 5, 1] = 75.1M;
            NivelRiesgo[9, 2, 1] = 23.0M; NivelRiesgo[9, 3, 2] = 39.6M; NivelRiesgo[9, 5, 1] = 48.0M;
            NivelRiesgo[10, 2, 1] = 19.5M; NivelRiesgo[10, 3, 2] = 38.9M; NivelRiesgo[10, 5, 1] = 47.3M;
            NivelRiesgo[11, 2, 1] = 16.8M; NivelRiesgo[11, 3, 2] = 41.7M; NivelRiesgo[11, 5, 1] = 50.1M;
            NivelRiesgo[12, 2, 1] = 12.6M; NivelRiesgo[12, 3, 2] = 31.3M; NivelRiesgo[12, 5, 1] = 50.1M;
            NivelRiesgo[13, 2, 1] = 50.1M; NivelRiesgo[13, 3, 2] = 75.0M; NivelRiesgo[13, 5, 1] = 85.1M;
            NivelRiesgo[14, 2, 1] = 25.1M; NivelRiesgo[14, 3, 2] = 45.8M; NivelRiesgo[14, 5, 1] = 58.4M;
            NivelRiesgo[15, 2, 1] = 1.0M; NivelRiesgo[15, 3, 2] = 12.5M; NivelRiesgo[15, 5, 1] = 18.9M;
            NivelRiesgo[16, 2, 1] = 1.0M; NivelRiesgo[16, 3, 2] = 25.0M; NivelRiesgo[16, 5, 1] = 37.6M;
            return NivelRiesgo;

        }
        private decimal[,,] baremosDominioIntraB()
        {
            decimal[,,] NivelRiesgo = new decimal[5, 6, 3];
            NivelRiesgo[1, 1, 1] = 0.0M;
            NivelRiesgo[2, 1, 1] = 0.0M;
            NivelRiesgo[3, 1, 1] = 0.0M;
            NivelRiesgo[4, 1, 1] = 0.0M;
            NivelRiesgo[1, 1, 2] = 8.3M;
            NivelRiesgo[2, 1, 2] = 19.4M;
            NivelRiesgo[3, 1, 2] = 26.9M;
            NivelRiesgo[4, 1, 2] = 2.5M;
            NivelRiesgo[1, 2, 1] = 8.4M;
            NivelRiesgo[2, 2, 1] = 19.5M;
            NivelRiesgo[3, 2, 1] = 27.0M;
            NivelRiesgo[4, 2, 1] = 2.6M;
            NivelRiesgo[1, 2, 2] = 17.5M;
            NivelRiesgo[2, 2, 2] = 26.4M;
            NivelRiesgo[3, 2, 2] = 33.3M;
            NivelRiesgo[4, 2, 2] = 10.0M;
            NivelRiesgo[1, 3, 1] = 17.6M;
            NivelRiesgo[2, 3, 1] = 26.5M;
            NivelRiesgo[3, 3, 1] = 33.4M;
            NivelRiesgo[4, 3, 1] = 10.1M;
            NivelRiesgo[1, 3, 2] = 26.7M;
            NivelRiesgo[2, 3, 2] = 34.7M;
            NivelRiesgo[3, 3, 2] = 37.8M;
            NivelRiesgo[4, 3, 2] = 17.5M;
            NivelRiesgo[1, 4, 1] = 26.8M;
            NivelRiesgo[2, 4, 1] = 34.8M;
            NivelRiesgo[3, 4, 1] = 37.9M;
            NivelRiesgo[4, 4, 1] = 17.6M;
            NivelRiesgo[1, 4, 2] = 38.3M;
            NivelRiesgo[2, 4, 2] = 43.1M;
            NivelRiesgo[3, 4, 2] = 44.2M;
            NivelRiesgo[4, 4, 2] = 27.5M;
            NivelRiesgo[1, 5, 1] = 38.4M;
            NivelRiesgo[2, 5, 1] = 43.2M;
            NivelRiesgo[3, 5, 1] = 44.3M;
            NivelRiesgo[4, 5, 1] = 27.6M;
            NivelRiesgo[1, 5, 2] = 100M;
            NivelRiesgo[2, 5, 2] = 100M;
            NivelRiesgo[3, 5, 2] = 100M;
            NivelRiesgo[4, 5, 2] = 100M;


            return NivelRiesgo;

        }
        //Operarios, auxiliares
        private decimal[,,] baremosExtra2()
        {
            decimal[,,] NivelRiesgo = new decimal[9, 6, 3];
            NivelRiesgo[1, 1, 1] = 0.0M; NivelRiesgo[1, 3, 2] = 37.5M;
            NivelRiesgo[2, 1, 1] = 0.0M; NivelRiesgo[2, 3, 2] = 33.3M;
            NivelRiesgo[3, 1, 1] = 0.0M; NivelRiesgo[3, 3, 2] = 25.0M;
            NivelRiesgo[4, 1, 1] = 0.0M; NivelRiesgo[4, 3, 2] = 41.7M;
            NivelRiesgo[5, 1, 1] = 0.0M; NivelRiesgo[5, 3, 2] = 16.7M;
            NivelRiesgo[6, 1, 1] = 0.0M; NivelRiesgo[6, 3, 2] = 25.0M;
            NivelRiesgo[7, 1, 1] = 0.0M; NivelRiesgo[7, 3, 2] = 25.0M;
            NivelRiesgo[8, 1, 1] = 0.0M; NivelRiesgo[8, 3, 2] = 24.2M;
            NivelRiesgo[1, 1, 2] = 6.3M; NivelRiesgo[1, 4, 1] = 37.6M;
            NivelRiesgo[2, 1, 2] = 8.3M; NivelRiesgo[2, 4, 1] = 33.4M;
            NivelRiesgo[3, 1, 2] = 5.0M; NivelRiesgo[3, 4, 1] = 25.1M;
            NivelRiesgo[4, 1, 2] = 16.7M; NivelRiesgo[4, 4, 1] = 41.8M;
            NivelRiesgo[5, 1, 2] = 5.6M; NivelRiesgo[5, 4, 1] = 16.8M;
            NivelRiesgo[6, 1, 2] = 0.9M; NivelRiesgo[6, 4, 1] = 25.1M;
            NivelRiesgo[7, 1, 2] = 0.9M; NivelRiesgo[7, 4, 1] = 25.1M;
            NivelRiesgo[8, 1, 2] = 12.9M; NivelRiesgo[8, 4, 1] = 24.3M;
            NivelRiesgo[1, 2, 1] = 6.4M; NivelRiesgo[1, 4, 2] = 50.0M;
            NivelRiesgo[2, 2, 1] = 8.4M; NivelRiesgo[2, 4, 2] = 50.0M;
            NivelRiesgo[3, 2, 1] = 5.1M; NivelRiesgo[3, 4, 2] = 35.0M;
            NivelRiesgo[4, 2, 1] = 16.8M; NivelRiesgo[4, 4, 2] = 50.0M;
            NivelRiesgo[5, 2, 1] = 5.7M; NivelRiesgo[5, 4, 2] = 27.8M;
            NivelRiesgo[6, 2, 1] = 1.0M; NivelRiesgo[6, 4, 2] = 41.7M;
            NivelRiesgo[7, 2, 1] = 1.0M; NivelRiesgo[7, 4, 2] = 43.8M;
            NivelRiesgo[8, 2, 1] = 13.0M; NivelRiesgo[8, 4, 2] = 32.3M;
            NivelRiesgo[1, 2, 2] = 25.0M; NivelRiesgo[1, 5, 1] = 50.1M;
            NivelRiesgo[2, 2, 2] = 25.0M; NivelRiesgo[2, 5, 1] = 50.1M;
            NivelRiesgo[3, 2, 2] = 15.0M; NivelRiesgo[3, 5, 1] = 35.1M;
            NivelRiesgo[4, 2, 2] = 25.0M; NivelRiesgo[4, 5, 1] = 50.1M;
            NivelRiesgo[5, 2, 2] = 11.1M; NivelRiesgo[5, 5, 1] = 27.9M;
            NivelRiesgo[6, 2, 2] = 16.7M; NivelRiesgo[6, 5, 1] = 41.8M;
            NivelRiesgo[7, 2, 2] = 12.5M; NivelRiesgo[7, 5, 1] = 43.9M;
            NivelRiesgo[8, 2, 2] = 17.7M; NivelRiesgo[8, 5, 1] = 32.4M;
            NivelRiesgo[1, 3, 1] = 25.1M; NivelRiesgo[1, 5, 2] = 100M;
            NivelRiesgo[2, 3, 1] = 25.1M; NivelRiesgo[2, 5, 2] = 100M;
            NivelRiesgo[3, 3, 1] = 15.1M; NivelRiesgo[3, 5, 2] = 100M;
            NivelRiesgo[4, 3, 1] = 25.1M; NivelRiesgo[4, 5, 2] = 100M;
            NivelRiesgo[5, 3, 1] = 11.2M; NivelRiesgo[5, 5, 2] = 100M;
            NivelRiesgo[6, 3, 1] = 16.8M; NivelRiesgo[6, 5, 2] = 100M;
            NivelRiesgo[7, 3, 1] = 12.6M; NivelRiesgo[7, 5, 2] = 100M;
            NivelRiesgo[8, 3, 1] = 17.8M; NivelRiesgo[8, 5, 2] = 100M;


            return NivelRiesgo;

        }
        //jefaturas, tecnologos
        private decimal[,,] baremosExtra1()
        {
            decimal[,,] NivelRiesgo = new decimal[9, 6, 3];
            NivelRiesgo[1, 1, 1] = 0.0M; NivelRiesgo[1, 3, 2] = 37.5M;
            NivelRiesgo[2, 1, 1] = 0.0M; NivelRiesgo[2, 3, 2] = 33.3M;
            NivelRiesgo[3, 1, 1] = 0.0M; NivelRiesgo[3, 3, 2] = 20.0M;
            NivelRiesgo[4, 1, 1] = 0.0M; NivelRiesgo[4, 3, 2] = 33.3M;
            NivelRiesgo[5, 1, 1] = 0.0M; NivelRiesgo[5, 3, 2] = 13.9M;
            NivelRiesgo[6, 1, 1] = 0.0M; NivelRiesgo[6, 3, 2] = 25.0M;
            NivelRiesgo[7, 1, 1] = 0.0M; NivelRiesgo[7, 3, 2] = 25.0M;
            NivelRiesgo[8, 1, 1] = 0.0M; NivelRiesgo[8, 3, 2] = 22.6M;
            NivelRiesgo[1, 1, 2] = 6.3M; NivelRiesgo[1, 4, 1] = 37.6M;
            NivelRiesgo[2, 1, 2] = 8.3M; NivelRiesgo[2, 4, 1] = 33.4M;
            NivelRiesgo[3, 1, 2] = 0.9M; NivelRiesgo[3, 4, 1] = 20.1M;
            NivelRiesgo[4, 1, 2] = 8.3M; NivelRiesgo[4, 4, 1] = 33.4M;
            NivelRiesgo[5, 1, 2] = 5.6M; NivelRiesgo[5, 4, 1] = 14.0M;
            NivelRiesgo[6, 1, 2] = 8.3M; NivelRiesgo[6, 4, 1] = 25.1M;
            NivelRiesgo[7, 1, 2] = 0.9M; NivelRiesgo[7, 4, 1] = 25.1M;
            NivelRiesgo[8, 1, 2] = 11.3M; NivelRiesgo[8, 4, 1] = 22.7M;
            NivelRiesgo[1, 2, 1] = 6.4M; NivelRiesgo[1, 4, 2] = 50.0M;
            NivelRiesgo[2, 2, 1] = 8.4M; NivelRiesgo[2, 4, 2] = 50.0M;
            NivelRiesgo[3, 2, 1] = 1.0M; NivelRiesgo[3, 4, 2] = 30.0M;
            NivelRiesgo[4, 2, 1] = 8.4M; NivelRiesgo[4, 4, 2] = 50.0M;
            NivelRiesgo[5, 2, 1] = 5.7M; NivelRiesgo[5, 4, 2] = 22.2M;
            NivelRiesgo[6, 2, 1] = 8.4M; NivelRiesgo[6, 4, 2] = 41.7M;
            NivelRiesgo[7, 2, 1] = 1.0M; NivelRiesgo[7, 4, 2] = 43.8M;
            NivelRiesgo[8, 2, 1] = 11.4M; NivelRiesgo[8, 4, 2] = 29.0M;
            NivelRiesgo[1, 2, 2] = 25.0M; NivelRiesgo[1, 5, 1] = 50.1M;
            NivelRiesgo[2, 2, 2] = 25.0M; NivelRiesgo[2, 5, 1] = 50.1M;
            NivelRiesgo[3, 2, 2] = 10.0M; NivelRiesgo[3, 5, 1] = 30.1M;
            NivelRiesgo[4, 2, 2] = 25.0M; NivelRiesgo[4, 5, 1] = 50.1M;
            NivelRiesgo[5, 2, 2] = 11.1M; NivelRiesgo[5, 5, 1] = 22.3M;
            NivelRiesgo[6, 2, 2] = 16.7M; NivelRiesgo[6, 5, 1] = 41.8M;
            NivelRiesgo[7, 2, 2] = 12.5M; NivelRiesgo[7, 5, 1] = 43.9M;
            NivelRiesgo[8, 2, 2] = 16.9M; NivelRiesgo[8, 5, 1] = 29.1M;
            NivelRiesgo[1, 3, 1] = 25.1M; NivelRiesgo[1, 5, 2] = 100M;
            NivelRiesgo[2, 3, 1] = 25.1M; NivelRiesgo[2, 5, 2] = 100M;
            NivelRiesgo[3, 3, 1] = 10.1M; NivelRiesgo[3, 5, 2] = 100M;
            NivelRiesgo[4, 3, 1] = 25.1M; NivelRiesgo[4, 5, 2] = 100M;
            NivelRiesgo[5, 3, 1] = 11.2M; NivelRiesgo[5, 5, 2] = 100M;
            NivelRiesgo[6, 3, 1] = 16.8M; NivelRiesgo[6, 5, 2] = 100M;
            NivelRiesgo[7, 3, 1] = 12.6M; NivelRiesgo[7, 5, 2] = 100M;
            NivelRiesgo[8, 3, 1] = 17.0M; NivelRiesgo[8, 5, 2] = 100M;


            return NivelRiesgo;

        }
        //jefaturas, tecnologos
        private decimal[,,] baremosEstres1()
        {
            decimal[,,] NivelRiesgo = new decimal[2, 6, 3];
            NivelRiesgo[1, 1, 1] = 0.0M;
            NivelRiesgo[1, 1, 2] = 7.8M;
            NivelRiesgo[1, 2, 1] = 7.9M;
            NivelRiesgo[1, 2, 2] = 12.6M;
            NivelRiesgo[1, 3, 1] = 12.7M;
            NivelRiesgo[1, 3, 2] = 17.7M;
            NivelRiesgo[1, 4, 1] = 17.8M;
            NivelRiesgo[1, 4, 2] = 25.0M;
            NivelRiesgo[1, 5, 1] = 25.1M;
            NivelRiesgo[1, 5, 2] = 100M;
            return NivelRiesgo;
        }
        private decimal[,,] baremosEstres2()
        {
            decimal[,,] NivelRiesgo = new decimal[2, 6, 3];
            NivelRiesgo[1, 1, 1] = 0.0M;
            NivelRiesgo[1, 1, 2] = 6.5M;
            NivelRiesgo[1, 2, 1] = 6.6M;
            NivelRiesgo[1, 2, 2] = 11.8M;
            NivelRiesgo[1, 3, 1] = 11.9M;
            NivelRiesgo[1, 3, 2] = 17.0M;
            NivelRiesgo[1, 4, 1] = 17.1M;
            NivelRiesgo[1, 4, 2] = 23.4M;
            NivelRiesgo[1, 5, 1] = 23.5M;
            NivelRiesgo[1, 5, 2] = 100M;
            return NivelRiesgo;
        }

        private decimal[,,] baremosTotalExtraIntraA()
        {
            decimal[,,] NivelRiesgo = new decimal[2, 6, 3];
            NivelRiesgo[1, 1, 1] = 0.0M;
            NivelRiesgo[1, 1, 2] = 18.8M;
            NivelRiesgo[1, 2, 1] = 18.9M;
            NivelRiesgo[1, 2, 2] = 24.4M;
            NivelRiesgo[1, 3, 1] = 24.5M;
            NivelRiesgo[1, 3, 2] = 29.5M;
            NivelRiesgo[1, 4, 1] = 29.6M;
            NivelRiesgo[1, 4, 2] = 35.4M;
            NivelRiesgo[1, 5, 1] = 35.5M;
            NivelRiesgo[1, 5, 2] = 100M;
            return NivelRiesgo;
        }
        private decimal[,,] baremosTotalExtraIntraB()
        {
            decimal[,,] NivelRiesgo = new decimal[2, 6, 3];
            NivelRiesgo[1, 1, 1] = 0.0M;
            NivelRiesgo[1, 1, 2] = 19.9M;
            NivelRiesgo[1, 2, 1] = 20.0M;
            NivelRiesgo[1, 2, 2] = 24.8M;
            NivelRiesgo[1, 3, 1] = 24.9M;
            NivelRiesgo[1, 3, 2] = 29.5M;
            NivelRiesgo[1, 4, 1] = 29.6M;
            NivelRiesgo[1, 4, 2] = 35.4M;
            NivelRiesgo[1, 5, 1] = 35.5M;
            NivelRiesgo[1, 5, 2] = 100M;


            return NivelRiesgo;
        }

        private decimal[,,] baremosTotalIntraA()
        {
            decimal[,,] NivelRiesgo = new decimal[2, 6, 3];
            NivelRiesgo[1, 1, 1] = 0.0M;
            NivelRiesgo[1, 1, 2] = 19.7M;
            NivelRiesgo[1, 2, 1] = 19.8M;
            NivelRiesgo[1, 2, 2] = 25.8M;
            NivelRiesgo[1, 3, 1] = 25.9M;
            NivelRiesgo[1, 3, 2] = 31.5M;
            NivelRiesgo[1, 4, 1] = 31.6M;
            NivelRiesgo[1, 4, 2] = 38.0M;
            NivelRiesgo[1, 5, 1] = 38.1M;
            NivelRiesgo[1, 5, 2] = 100M;

            return NivelRiesgo;
        }
        private decimal[,,] baremosTotalIntraB()
        {
            decimal[,,] NivelRiesgo = new decimal[2, 6, 3];
            NivelRiesgo[1, 1, 1] = 0.0M;
            NivelRiesgo[1, 1, 2] = 20.6M;
            NivelRiesgo[1, 2, 1] = 20.7M;
            NivelRiesgo[1, 2, 2] = 26.0M;
            NivelRiesgo[1, 3, 1] = 26.1M;
            NivelRiesgo[1, 3, 2] = 31.2M;
            NivelRiesgo[1, 4, 1] = 31.3M;
            NivelRiesgo[1, 4, 2] = 38.7M;
            NivelRiesgo[1, 5, 1] = 38.8M;
            NivelRiesgo[1, 5, 2] = 100M;

            return NivelRiesgo;
        }
        #endregion
        #endregion
        #region AgregarFiltro
        [HttpPost]
        public ActionResult CrearUsuarioLista(List<EDBateriaUsuario> ListaEDBateriaUsuario)
        {
            List<EDBateriaUsuario> ListaUsuariosCreados = new List<EDBateriaUsuario>();
            List<string> ListaErrores = new List<string>();
            List<string> ListaCedulasError = new List<string>();
            List<string> ListaTipoError = new List<string>();
            bool Probar = false;
            int numeroAgregados = 0;
            int Idgestion = 0;
            string Estado = "No se ha guardado la información del convocado revise la información suministrada e intente de nuevo";
            int TipoConv = 1;
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                Estado = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { Estado, Probar, ListaUsuariosCreados, ListaErrores });
            }
            if (ListaEDBateriaUsuario!=null)
            {
                Probar = true;
                foreach (var item in ListaEDBateriaUsuario)
                {
                    
                    Idgestion = item.Fk_Id_BateriaGestion;
                    string a = item.CorreoElectronico;
                    string trimmed = a.Trim();
                    item.CorreoElectronico = trimmed;
                    string correo = item.CorreoElectronico;
                    bool ProbarCorreoMismo = LNBateria.VerificarCorreoExistente(correo, item.NumeroIdentificacion, item.Fk_Id_BateriaGestion);
                    if (ProbarCorreoMismo)
                    {
                        ListaErrores.Add(item.Nombre);
                        ListaCedulasError.Add(item.NumeroIdentificacion);
                        ListaTipoError.Add("1");
                    }

                    bool ProbarCorreo = ValidarEmail(correo);
                    if (!ProbarCorreo)
                    {
                        ListaErrores.Add(item.Nombre);
                        ListaCedulasError.Add(item.NumeroIdentificacion);
                        ListaTipoError.Add("2");
                    }
                    if (!ProbarCorreoMismo && ProbarCorreo)
                    {
                        item.Id_Empresa = usuarioActual.IdEmpresa;
                        item.TokenPrivado = RandomString(24);
                        item.EstadoEnvio = 0;
                        Idgestion = item.Fk_Id_BateriaGestion;
                        Probar = LNBateria.CrearConvocado(item);

                        if (Probar)
                        {

                            EDBateriaGestion EDBateriaGestion = LNBateria.ConsultarGestion(item.Fk_Id_BateriaGestion, usuarioActual.IdEmpresa);
                            if (EDBateriaGestion.bateriaExtra == 3)
                            {
                                EDBateriaUsuario EDBateriaUsuarioExtra = new EDBateriaUsuario();
                                EDBateriaUsuarioExtra.Nombre = item.Nombre;
                                EDBateriaUsuarioExtra.NumeroIdentificacion = item.NumeroIdentificacion;
                                EDBateriaUsuarioExtra.TipoDocumento = item.TipoDocumento;
                                EDBateriaUsuarioExtra.CorreoElectronico = item.CorreoElectronico;
                                EDBateriaUsuarioExtra.Id_Empresa = item.Id_Empresa;
                                EDBateriaUsuarioExtra.TokenPrivado = item.TokenPrivado;
                                EDBateriaUsuarioExtra.Fk_Id_BateriaGestion = item.Fk_Id_BateriaGestion;
                                EDBateriaUsuarioExtra.TipoConv = item.TipoConv;
                                EDBateriaUsuarioExtra.EstadoEnvio = item.EstadoEnvio;
                                EDBateriaUsuarioExtra.NumeroIntentos = 1;
                                Probar = LNBateria.CrearConvocado(EDBateriaUsuarioExtra);
                            }
                            
                            numeroAgregados++;
                        }
                    }
                    
                }
                ListaUsuariosCreados = LNBateria.ConsultarUsuariosCorreos(usuarioActual.IdEmpresa, TipoConv, Idgestion);
                return Json(new { Estado, Probar, ListaUsuariosCreados, numeroAgregados, ListaErrores, ListaCedulasError, ListaTipoError });
            }
            else
            {
                Estado = "No ha seleccionado ninguna persona para ser convocada";
                return Json(new { Estado, Probar, ListaUsuariosCreados, numeroAgregados, ListaErrores, ListaCedulasError, ListaTipoError });
            }
            
        }
        [HttpPost]
        public JsonResult ListaPersonas(string cargo)
        {
            List<EDRelacionesLaborales> ListaPersonas = new List<EDRelacionesLaborales>();
            bool probar = false;
            string resultado = "No existen Personas que cumplan con la condicion del cargo seleccionado";

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                resultado = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { probar, resultado, ListaPersonas }, JsonRequestBehavior.AllowGet);
            }

            ListaPersonas = ListaPersonasWS(usuarioActual.NitEmpresa, cargo);
            if (ListaPersonas.Count == 0)
            {
                return Json(new { probar, resultado, ListaPersonas }, JsonRequestBehavior.AllowGet);
            }

            probar = true;
            resultado = "Lista return";
            return Json(new { probar, resultado, ListaPersonas }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ListaPersonas1(string rol)
        {
            List<EDRelacionesLaborales> ListaPersonas = new List<EDRelacionesLaborales>();
            bool probar = false;
            string resultado = "No existen Personas que cumplan con la condicion del Rol seleccionado";

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                resultado = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { probar, resultado, ListaPersonas }, JsonRequestBehavior.AllowGet);
            }

            int introl = 0;
            if (rol!=null)
            {
                if (int.TryParse(rol, out introl))
                {
                    ListaPersonas = LNBateria.ConsultarConvocadosRol(introl);
                }
                else
                {
                    resultado = "Por favor elija el tipo de rol para realizar esta consulta";
                }
            }
            else
            {
                resultado = "Por favor elija el tipo de rol para realizar esta consulta";
            }
            

            if (ListaPersonas.Count == 0)
            {
                return Json(new { probar, resultado, ListaPersonas }, JsonRequestBehavior.AllowGet);
            }

            probar = true;
            resultado = "Lista return";
            return Json(new { probar, resultado, ListaPersonas }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult FiltrarUsuarios(List<String> values)
        {
            bool Probar = false;
            string Estado = "";
            string Nombre = "";

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                Estado = "El usuario no ha iniciado sesión en el sistema";
                return Json(new { Estado, Probar });
            }
            EDBateriaGestion EDBateriaGestion = new EDBateriaGestion();
            List<EDBateria> ListaBaterias = LNBateria.ConsultarBaterias();
            int IdGestionInt = 0;
            

            string filtro = values[0];
            string orden = values[1];
            string Id = values[2];

            if (int.TryParse(Id, out IdGestionInt))
            {
                EDBateriaGestion = LNBateria.ConsultarGestion(IdGestionInt, usuarioActual.IdEmpresa);
                Nombre = ListaBaterias.Where(s => s.Pk_Id_Bateria == EDBateriaGestion.Fk_Id_Bateria).FirstOrDefault().Nombre;
                EDBateriaGestion.NombreBateria = Nombre;
            }
            List<EDBateriaUsuario> ListaUsuarios = new List<EDBateriaUsuario>();
            List<EDBateriaUsuario> ListaUsuariosCons = new List<EDBateriaUsuario>();
            ListaUsuarios = EDBateriaGestion.ListaUsuarios;

            
            return Json(new { Estado, Probar, ListaUsuariosCons });
        }
        #endregion
        #region CargueMasivo

        public FileResult Download()
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(Server.MapPath(RutaExcelPlantilla));
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, "AlisstaPlantillaConvocados.xlsx");
        }
        public ActionResult Upload(FormCollection formCollection)
        {
            List<EDBateriaUsuario> ListaCargue = new List<EDBateriaUsuario>();
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }
            if (Request != null)
            {
                HttpPostedFileBase file = Request.Files["UploadedFile"];
                if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
                {
                    
                }
            }
            return View("CargueMasivoEPP", ListaCargue);
        }
        [HttpPost]
        public ActionResult UploadFiles()
        {
            List<EDBateriaUsuario> ListaCargue = new List<EDBateriaUsuario>();
            List<EDBateriaUsuario> ListaCargueG = new List<EDBateriaUsuario>();
            int ValIntGestion = 0;
            List<string> FilasError = new List<string>();
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }
            bool probar = false;
            string resultado = "";
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
                        if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
                        {
                            if (file.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                            {
                                string Fname = file.FileName;
                                CrearCarpeta(RutaExcelTemp);
                                string RandomName = RandomString(4) + DateTime.Now.ToString();
                                RandomName = RandomName.Replace(".", "").Replace(" ", "").Replace("/", "").Replace("-", "").Replace(":", "");
                                Fname = RandomName + Fname;
                                file.SaveAs(Server.MapPath(Path.Combine(RutaExcelTemp, Fname)));
                                string ruracargue = Path.Combine(RutaExcelTemp, Fname);

                                DataTable dtEPP = new DataTable();
                                try
                                {
                                    dtEPP = ReadAsDataTable1(Server.MapPath(ruracargue), true);
                                }
                                catch (Exception ex)
                                {
                                }
                                string ValIdGestion = string.Empty;
                                
                                try
                                {
                                    ValIdGestion = Request.Form[0].ToString();
                                }
                                catch (Exception)
                                {
                                    probar = false;
                                    resultado = "No se pudo completar la operación, por favor vuelva a intentar";
                                    return Json(new { probar, resultado, ListaCargueG });
                                }
                                foreach (DataRow row in dtEPP.Rows)
                                {
                                    if (row[0].ToString()!=string.Empty && row[1].ToString() != string.Empty && row[2].ToString() != string.Empty)
                                    {
                                        EDBateriaUsuario EDBateriaUsuario = new EDBateriaUsuario();
                                        if (int.TryParse(ValIdGestion, out ValIntGestion))
                                        {
                                            EDBateriaUsuario.Fk_Id_BateriaGestion = ValIntGestion;
                                        }
                                        string a = row[2].ToString();
                                        string trimmed = a.Trim();
                                        string correo = trimmed;
                                        
                                        bool ProbarCorreo = ValidarEmail(correo);
                                        if (!ProbarCorreo)
                                        {
                                            FilasError.Add("Error en la fila: " + row[4].ToString() + " Columna: C el correo no tiene el formato válido de un correo electrónico");
                                        }


                                        bool ProbarCorreoMismo = LNBateria.VerificarCorreoExistente(correo, row[0].ToString(), EDBateriaUsuario.Fk_Id_BateriaGestion);
                                        if (!ProbarCorreoMismo)
                                        {
                                            EDBateriaUsuario.Nombre = row[1].ToString();
                                            EDBateriaUsuario.NumeroIdentificacion = row[0].ToString();
                                            EDBateriaUsuario.TipoDocumento = "N/A";
                                            EDBateriaUsuario.CorreoElectronico = row[2].ToString();
                                            EDBateriaUsuario.Id_Empresa = usuarioActual.IdEmpresa;
                                            EDBateriaUsuario.TokenPrivado = RandomString(24);
                                            EDBateriaUsuario.EstadoEnvio = 0;

                                            EDBateriaUsuario.TipoConv = 4;
                                            ListaCargue.Add(EDBateriaUsuario);
                                        }
                                        
                                    }
                                    else
                                    {
                                        if (row[0].ToString()==string.Empty && row[1].ToString() == string.Empty && row[2].ToString() == string.Empty)
                                        {
                                        }
                                        else
                                        {
                                            if (row[0].ToString() == string.Empty)
                                            {
                                                FilasError.Add("Error en la fila: " + row[4].ToString() + " Columna: A");
                                            }
                                            if (row[1].ToString() == string.Empty)
                                            {
                                                FilasError.Add("Error en la fila: " + row[4].ToString() + " Columna: B");
                                            }
                                            if (row[2].ToString() == string.Empty)
                                            {
                                                FilasError.Add("Error en la fila: " + row[4].ToString() + " Columna: C");
                                            }
                                        }
                                    }
                                }
                                if (FilasError.Count > 0)
                                {
                                    string reuerrores = "";
                                    foreach (var item in FilasError)
                                    {
                                        if (reuerrores != "")
                                        {
                                            reuerrores += ", " + item;
                                        }
                                        else
                                        {
                                            reuerrores += item;
                                        }
                                    }
                                    probar = false;
                                    resultado = "No se pudo completar la operación, existen errores en la hoja de cálculo recuerde que debe registrar la cédula, nombre y correo del convocado antes de realizar el cargue: " + reuerrores + "  ";
                                    return Json(new { probar, resultado, ListaCargueG });
                                }
                            }
                            else
                            {
                                probar = false;
                                resultado = "No se pudo completar la operación, el formato del archivo no es una hoja de cálculo";
                                return Json(new { probar, resultado, ListaCargueG });
                            }
                        }
                        else
                        {
                            probar = false;
                            resultado = "No se pudo completar la operación, ocurrio un error al procesar el archivo es posible que este dañado";
                            return Json(new { probar, resultado, ListaCargueG });
                        }
                    }
                    bool extra = false;
                    EDBateriaGestion EDBateriaGestion = LNBateria.ConsultarGestion(ValIntGestion, usuarioActual.IdEmpresa);
                    if (EDBateriaGestion.bateriaExtra == 3)
                    {
                        extra = true;
                    }
                    
                    List<EDBateriaUsuario> ProbarGuardar = LNBateria.CrearConvocadoMasivo(ListaCargue, ValIntGestion, extra);
                    if (ProbarGuardar.Count>0)
                    {
                        ListaCargueG = LNBateria.ConsultarUsuariosCorreos(usuarioActual.IdEmpresa, 4, ValIntGestion);
                        int NumeroAgregados = ProbarGuardar.Count;
                        probar = true;
                        resultado = "Archivo cargado se agregaron "+ NumeroAgregados.ToString() + " nuevo(s) Convocado(s)";
                        return Json(new { probar, resultado, ListaCargueG });
                    }
                    else
                    {
                        probar = false;
                        resultado = "No existen nuevos convocados para agregar";
                        return Json(new { probar, resultado, ListaCargueG });
                    }

                }
                catch (Exception ex)
                {
                    resultado = "No se pudo completar la operación, por favor vuelva a intentar";
                    return Json(new { probar, resultado, ListaCargueG });
                }
            }
            else
            {
                resultado = "No ha seleccionado un archivo, antes de adjuntar asegurese que exista un archivo seleccionado";
                return Json(new { probar, resultado, ListaCargueG });
            }
        }
        public static DataTable ReadAsDataTable1(string fileName, bool hasHeader)
        {
            DataTable DataTable = new DataTable();
            using (var pck = new OfficeOpenXml.ExcelPackage())
            {
                using (var stream = System.IO.File.OpenRead(fileName))
                {
                    pck.Load(stream);
                }
                var ws = pck.Workbook.Worksheets.First();
                DataTable tbl = new DataTable();
                tbl.Columns.Add("Cedula", typeof(String));
                tbl.Columns.Add("Nombre", typeof(String));
                tbl.Columns.Add("Email", typeof(String));
                tbl.Columns.Add("Cell", typeof(String));
                tbl.Columns.Add("Row", typeof(String));

                var startRow = hasHeader ? 3 : 2;
                for (int rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
                {
                    var wsRow = ws.Cells[rowNum, 1, rowNum, 3];
                    DataRow row = tbl.Rows.Add();
                    int contCol = 0;
                    foreach (var cell in wsRow)
                    {

                        if (cell.Text == null)
                        {
                            row[cell.Start.Column - 1] = string.Empty;
                            row[3] = cell.Start.Column.ToString();
                            row[4] = rowNum.ToString();
                        }
                        else
                        {
                            if (cell.Text == "")
                            {
                                row[cell.Start.Column - 1] = string.Empty;
                                row[3] = cell.Start.Column.ToString();
                                row[4] = rowNum.ToString();
                            }
                            else
                            {
                                row[cell.Start.Column - 1] = cell.Text;
                                row[3] = cell.Start.Column.ToString();
                                row[4] = rowNum.ToString();
                            }
                        }
                        contCol++;
                    }
                }
                return tbl;
            }

            return DataTable;
        }

        #endregion
        #region MetodosExtra
        public void CambiarEstadoTodos()
        {
            LNBateria.CambiarEstadosInactivos();
        }
        private void CrearCarpeta(string path)
        {
            bool existeCarpeta = Directory.Exists(Server.MapPath(path));
            if (!existeCarpeta)
            {
                Directory.CreateDirectory(Server.MapPath(path));
            }

        }
        static double ConvertBytesToMegabytes(long bytes)
        {
            return (bytes / 1024f) / 1024f;
        }
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "123456789abcdefghijklmnopqrstvwxyzABCDEFGHIJKLMOPQRSTVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        private string DomainMapper(Match match)
        {
            // IdnMapping class with default property values.
            IdnMapping idn = new IdnMapping();

            string domainName = match.Groups[2].Value;
            try
            {
                domainName = idn.GetAscii(domainName);
            }
            catch (ArgumentException)
            {
                invalid = true;
            }
            return match.Groups[1].Value + domainName;
        }
        public bool ValidarEmail(string strIn)
        {
            invalid = false;
            if (String.IsNullOrEmpty(strIn))
                return false;

            // Use IdnMapping class to convert Unicode domain names.
            try
            {
                strIn = Regex.Replace(strIn, @"(@)(.+)$", this.DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }

            if (invalid)
                return false;

            // Return true if strIn is in valid e-mail format.
            try
            {
                return Regex.IsMatch(strIn,
                      @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                      @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                      RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
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
                                        EDRelacionesLaborales.Email = item1.EmailPersona;
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
                                            EDRelacionesLaborales.Email = item1.EmailPersona;
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
        private List<EDCargo> ListaCargosWS(string NIT)
        {
            List<string> ListaNombresCargosWS = new List<string>();
            List<string> NuevaListaCargos = new List<string>();
            List<EDCargo> NuevaListaCargosMaestro = new List<EDCargo>();


            string[] ListaEstados = new string[2] { "1", "2" };
            string[] ListaVinc = new string[2] { "1", "2" };

            for (int i = 0; i < 2; i++)
            {
                for (int i1 = 0; i1 < 2; i1++)
                {
                    try
                    {
                        var cliente = new RestSharp.RestClient(ConfigurationManager.AppSettings["Url"]);
                        var request = new RestRequest(ConfigurationManager.AppSettings["consultaEstadoTipoAfiliado"], RestSharp.Method.GET);
                        request.RequestFormat = DataFormat.Xml;
                        var usuario = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
                        request.Parameters.Clear();
                        request.AddParameter("tpEm", "ni");
                        request.AddParameter("docEm", NIT);
                        request.AddParameter("estadoAfi", ListaEstados[i]);
                        request.AddParameter("TipoVinAfi", ListaVinc[i1]);
                        request.AddHeader("Content-Type", "application/json");
                        request.AddHeader("Accept", "application/json");

                        ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

                        IRestResponse<List<EstadoTipoAfiliadoDTO>> response = cliente.Execute<List<EstadoTipoAfiliadoDTO>>(request);
                        var result = response.Content;

                        var respuesta = Newtonsoft.Json.JsonConvert.DeserializeObject<List<EstadoTipoAfiliadoDTO>>(result);
                        if (respuesta != null)
                        {
                            foreach (var item in respuesta)
                            {
                                if (item != null)
                                {
                                    if (item.cargo != null)
                                    {
                                        ListaNombresCargosWS.Add(item.cargo);
                                    }
                                }
                            }
                        }
                        var ListaNombresCargosNoDuples = ListaNombresCargosWS.Distinct();
                        if (ListaNombresCargosNoDuples != null)
                        {
                            foreach (var item in ListaNombresCargosNoDuples)
                            {
                                NuevaListaCargos.Add(item);
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            var ListaNombresCargosNoDuples1 = NuevaListaCargos.Distinct();

            int cont = 1;
            foreach (var item in ListaNombresCargosNoDuples1)
            {
                string NombreCargo = item;
                if (NombreCargo!=null)
                {
                    if (NombreCargo != "")
                    {
                        EDCargo EDCargo = new EDCargo();
                        EDCargo.IDCargo = cont;
                        EDCargo.NombreCargo = NombreCargo;
                        NuevaListaCargosMaestro.Add(EDCargo);
                        cont++;
                    }
                }
            }
            return NuevaListaCargosMaestro;
        }
        private List<EDRelacionesLaborales> ListaPersonasWS(string NIT, string cargobuscado)
        {
            List<EDRelacionesLaborales> ListaPersonas = new List<EDRelacionesLaborales>();
            string[] ListaEstados = new string[2] { "1", "2" };
            string[] ListaVinc = new string[2] { "1", "2" };

            for (int i = 0; i < 2; i++)
            {
                for (int i1 = 0; i1 < 2; i1++)
                {
                    try
                    {
                        var cliente = new RestSharp.RestClient(ConfigurationManager.AppSettings["Url"]);
                        var request = new RestRequest(ConfigurationManager.AppSettings["consultaEstadoTipoAfiliado"], RestSharp.Method.GET);
                        request.RequestFormat = DataFormat.Xml;
                        var usuario = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
                        request.Parameters.Clear();
                        request.AddParameter("tpEm", "ni");
                        request.AddParameter("docEm", NIT);
                        request.AddParameter("estadoAfi", ListaEstados[i]);
                        request.AddParameter("TipoVinAfi", ListaVinc[i1]);
                        request.AddHeader("Content-Type", "application/json");
                        request.AddHeader("Accept", "application/json");

                        ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

                        IRestResponse<List<EstadoTipoAfiliadoDTO>> response = cliente.Execute<List<EstadoTipoAfiliadoDTO>>(request);
                        var result = response.Content;

                        var respuesta = Newtonsoft.Json.JsonConvert.DeserializeObject<List<EstadoTipoAfiliadoDTO>>(result);
                        if (respuesta != null)
                        {
                            foreach (var item in respuesta)
                            {
                                if (item != null)
                                {
                                    if (item.cargo!=null)
                                    {
                                        if (item.cargo == cargobuscado)
                                        {
                                            EDRelacionesLaborales EDRelacionesLaborales = new EDRelacionesLaborales();
                                            EDRelacionesLaborales.Nombre1 = item.nombre1;
                                            EDRelacionesLaborales.Nombre2 = item.nombre2;
                                            EDRelacionesLaborales.Apellido1 = item.apellido1;
                                            EDRelacionesLaborales.Apellido2 = item.apellido2;
                                            if (item.emailAfi!=null)
                                            {
                                                EDRelacionesLaborales.Email = item.emailAfi.ToLower();
                                            }
                                            else
                                            {
                                                EDRelacionesLaborales.Email = item.emailAfi;
                                            }
                                            
                                            EDRelacionesLaborales.NumeroDocumento = item.docAfi;
                                            ListaPersonas.Add(EDRelacionesLaborales);
                                            
                                        }
                                    }
                                }
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
            ListaPersonas=ListaPersonas.OrderBy(s => s.Nombre1).ToList();

            return ListaPersonas;
        }
        private List<EDCargo> ConsultarCargos(string Nit)
        {
            List<EDCargo> ListaEDCargo = new List<EDCargo>();

            if (Session["CargosUsuario"] == null)
            {
                Session["CargosUsuario"] = ListaCargosWS(Nit);
                try
                {
                    ListaEDCargo = (List<EDCargo>)Session["CargosUsuario"];
                }
                catch (Exception)
                {

                }

            }
            else
            {
                List<EDCargo> RevisarLista = new List<EDCargo>();
                try
                {
                    RevisarLista = (List<EDCargo>)Session["CargosUsuario"];
                    if (RevisarLista.Count == 0)
                    {
                        Session["CargosUsuario"] = ListaCargosWS(Nit);
                        try
                        {
                            ListaEDCargo = (List<EDCargo>)Session["CargosUsuario"];
                        }
                        catch (Exception)
                        {

                        }
                    }
                    else
                    {
                        ListaEDCargo = RevisarLista;
                    }
                }
                catch (Exception)
                {
                    Session["CargosUsuario"] = ListaCargosWS(Nit);
                    try
                    {
                        ListaEDCargo = (List<EDCargo>)Session["CargosUsuario"];
                    }
                    catch (Exception)
                    {

                    }
                }
            }
            if (ListaEDCargo.Count > 0)
            {
                ListaEDCargo = ListaEDCargo.OrderBy(s => s.NombreCargo).ToList();
                ListaEDCargo = ListaEDCargo.Distinct().ToList();
            }
            return ListaEDCargo;
        }
        #endregion
        #region resultadosFormularios
        [HttpGet]
        public ActionResult InicialResultados(string IdUsuario)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }
            if (IdUsuario != null)
            {
                int IdUsuarioInt = 0;
                if (int.TryParse(IdUsuario, out IdUsuarioInt))
                {
                    EDBateriaUsuario EDBateriaUsuario = new EDBateriaUsuario();
                    EDBateriaUsuario = LNBateria.ConsultarConvocadoId(IdUsuarioInt, usuarioActual.IdEmpresa);
                    if (EDBateriaUsuario!=null)
                    {
                        if (EDBateriaUsuario.Pk_Id_BateriaUsuario!=0)
                        {
                            EDBateriaInicial EDBateriaInicial = new EDBateriaInicial();
                            EDBateriaInicial = LNBateria.ConsultarInicialKey(EDBateriaUsuario.Pk_Id_BateriaUsuario);
                            return View(EDBateriaInicial);
                        }
                    }

                }
            }

            return HttpNotFound();
                    
        }
        [HttpGet]
        public ActionResult PrincipalResultados(string IdUsuario)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }
            if (IdUsuario != null)
            {
                int IdUsuarioInt = 0;
                if (int.TryParse(IdUsuario, out IdUsuarioInt))
                {
                    EDBateriaUsuario EDBateriaUsuario = new EDBateriaUsuario();
                    EDBateriaUsuario = LNBateria.ConsultarConvocadoId(IdUsuarioInt, usuarioActual.IdEmpresa);
                    if (EDBateriaUsuario != null)
                    {
                        if (EDBateriaUsuario.Pk_Id_BateriaUsuario != 0)
                        {
                            int fkIdGestion = EDBateriaUsuario.Fk_Id_BateriaGestion;
                            EDBateriaGestion EDBateriaGestion = LNBateria.ConsultarGestion(fkIdGestion, usuarioActual.IdEmpresa);
                            if (EDBateriaUsuario.NumeroIntentos==0 && EDBateriaGestion.Fk_Id_Bateria==1)
                            {
                                return RedirectToAction("IntralaboralAResultados", new { IdUsuario = IdUsuario });
                            }
                            if (EDBateriaUsuario.NumeroIntentos == 0 && EDBateriaGestion.Fk_Id_Bateria == 2)
                            {
                                return RedirectToAction("IntralaboralBResultados", new { IdUsuario = IdUsuario });
                            }
                            if (EDBateriaUsuario.NumeroIntentos == 1 && EDBateriaGestion.Fk_Id_Bateria == 1)
                            {
                                return RedirectToAction("ExtralaboralResultados", new { IdUsuario = IdUsuario });
                            }
                            if (EDBateriaUsuario.NumeroIntentos == 1 && EDBateriaGestion.Fk_Id_Bateria == 2)
                            {
                                return RedirectToAction("ExtralaboralResultados", new { IdUsuario = IdUsuario });
                            }
                            if (EDBateriaUsuario.NumeroIntentos == 0 && EDBateriaGestion.Fk_Id_Bateria == 4)
                            {
                                return RedirectToAction("EstresResultados", new { IdUsuario = IdUsuario });
                            }
                        }
                    }

                }
            }
            return HttpNotFound();
        }

        [HttpGet]
        public ActionResult IntralaboralAResultados(string IdUsuario)
        {
            List<EDBateriaCuestionario> ListaCuestionario = new List<EDBateriaCuestionario>();
            ViewBag.NombrePersona = "";
            ViewBag.Cedula = "";
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }
            if (IdUsuario != null)
            {
                int IdUsuarioInt = 0;
                if (int.TryParse(IdUsuario, out IdUsuarioInt))
                {
                    EDBateriaUsuario EDBateriaUsuario = new EDBateriaUsuario();
                    EDBateriaUsuario = LNBateria.ConsultarConvocadoId(IdUsuarioInt, usuarioActual.IdEmpresa);
                    if (EDBateriaUsuario != null)
                    {
                        if (EDBateriaUsuario.Pk_Id_BateriaUsuario != 0)
                        {
                            ViewBag.NombrePersona = EDBateriaUsuario.Nombre;
                            ViewBag.Cedula = EDBateriaUsuario.NumeroIdentificacion;

                            ListaCuestionario = LNBateria.ConsultarFormulario(EDBateriaUsuario.Pk_Id_BateriaUsuario, 1);

                            EDBateriaInicial EDBateriaInicial = new EDBateriaInicial();
                            EDBateriaInicial = LNBateria.ConsultarInicialKey(EDBateriaUsuario.Pk_Id_BateriaUsuario);


                            if (EDBateriaUsuario.CheckPag9 != null)
                            {
                                if (EDBateriaUsuario.CheckPag9 == "Si")
                                {
                                    ViewBag.check9a = "checked=\"checked\"";
                                }
                                if (EDBateriaUsuario.CheckPag9 == "No")
                                {
                                    ViewBag.check9b = "checked=\"checked\"";
                                    foreach (var item in ListaCuestionario)
                                    {
                                        if (item.Orden >= 106 && item.Orden <= 114)
                                        {
                                            item.Valor = 0;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                foreach (var item in ListaCuestionario)
                                {
                                    if (item.Orden >= 106 && item.Orden <= 114)
                                    {
                                        item.Valor = 0;
                                    }
                                }
                            }
                            if (EDBateriaUsuario.CheckPag10 != null)
                            {
                                if (EDBateriaUsuario.CheckPag10 == "Si")
                                {
                                    ViewBag.check10a = "checked=\"checked\"";
                                }
                                if (EDBateriaUsuario.CheckPag10 == "No")
                                {
                                    foreach (var item in ListaCuestionario)
                                    {
                                        if (item.Orden >= 115 && item.Orden <= 123)
                                        {
                                            item.Valor = 0;
                                        }
                                    }
                                    ViewBag.check10b = "checked=\"checked\"";
                                }
                            }
                            else
                            {
                                foreach (var item in ListaCuestionario)
                                {
                                    if (item.Orden >= 115 && item.Orden <= 123)
                                    {
                                        item.Valor = 0;
                                    }
                                }
                            }

                            return View(ListaCuestionario);
                        }
                    }

                }
            }

            return HttpNotFound();

        }
        [HttpGet]
        public ActionResult IntralaboralBResultados(string IdUsuario)
        {
            List<EDBateriaCuestionario> ListaCuestionario = new List<EDBateriaCuestionario>();
            ViewBag.NombrePersona = "";
            ViewBag.Cedula = "";
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }
            if (IdUsuario != null)
            {
                int IdUsuarioInt = 0;
                if (int.TryParse(IdUsuario, out IdUsuarioInt))
                {
                    EDBateriaUsuario EDBateriaUsuario = new EDBateriaUsuario();
                    EDBateriaUsuario = LNBateria.ConsultarConvocadoId(IdUsuarioInt, usuarioActual.IdEmpresa);
                    if (EDBateriaUsuario != null)
                    {
                        if (EDBateriaUsuario.Pk_Id_BateriaUsuario != 0)
                        {
                            ViewBag.NombrePersona = EDBateriaUsuario.Nombre;
                            ViewBag.Cedula = EDBateriaUsuario.NumeroIdentificacion;

                            ListaCuestionario = LNBateria.ConsultarFormulario(EDBateriaUsuario.Pk_Id_BateriaUsuario, 2);

                            EDBateriaInicial EDBateriaInicial = new EDBateriaInicial();
                            EDBateriaInicial = LNBateria.ConsultarInicialKey(EDBateriaUsuario.Pk_Id_BateriaUsuario);


                            if (EDBateriaUsuario.CheckPag9 != null)
                            {
                                if (EDBateriaUsuario.CheckPag9 == "Si")
                                {
                                    ViewBag.check9a = "checked=\"checked\"";
                                }
                                if (EDBateriaUsuario.CheckPag9 == "No")
                                {
                                    ViewBag.check9b = "checked=\"checked\"";
                                    foreach (var item in ListaCuestionario)
                                    {
                                        if (item.Orden >= 106 && item.Orden <= 114)
                                        {
                                            item.Valor = 0;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                foreach (var item in ListaCuestionario)
                                {
                                    if (item.Orden >= 106 && item.Orden <= 114)
                                    {
                                        item.Valor = 0;
                                    }
                                }
                            }
                            if (EDBateriaUsuario.CheckPag10 != null)
                            {
                                if (EDBateriaUsuario.CheckPag10 == "Si")
                                {
                                    ViewBag.check10a = "checked=\"checked\"";
                                }
                                if (EDBateriaUsuario.CheckPag10 == "No")
                                {
                                    foreach (var item in ListaCuestionario)
                                    {
                                        if (item.Orden >= 115 && item.Orden <= 123)
                                        {
                                            item.Valor = 0;
                                        }
                                    }
                                    ViewBag.check10b = "checked=\"checked\"";
                                }
                            }
                            else
                            {
                                foreach (var item in ListaCuestionario)
                                {
                                    if (item.Orden >= 115 && item.Orden <= 123)
                                    {
                                        item.Valor = 0;
                                    }
                                }
                            }
                            return View(ListaCuestionario);
                        }
                    }

                }
            }

            return HttpNotFound();

        }
        [HttpGet]
        public ActionResult ExtralaboralResultados(string IdUsuario)
        {
            List<EDBateriaCuestionario> ListaCuestionario = new List<EDBateriaCuestionario>();
            ViewBag.NombrePersona = "";
            ViewBag.Cedula = "";
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }
            if (IdUsuario != null)
            {
                int IdUsuarioInt = 0;
                if (int.TryParse(IdUsuario, out IdUsuarioInt))
                {
                    EDBateriaUsuario EDBateriaUsuario = new EDBateriaUsuario();
                    EDBateriaUsuario = LNBateria.ConsultarConvocadoId(IdUsuarioInt, usuarioActual.IdEmpresa);
                    if (EDBateriaUsuario != null)
                    {
                        if (EDBateriaUsuario.Pk_Id_BateriaUsuario != 0)
                        {
                            ViewBag.NombrePersona = EDBateriaUsuario.Nombre;
                            ViewBag.Cedula = EDBateriaUsuario.NumeroIdentificacion;

                            ListaCuestionario = LNBateria.ConsultarFormulario(EDBateriaUsuario.Pk_Id_BateriaUsuario, 3);

                            EDBateriaInicial EDBateriaInicial = new EDBateriaInicial();
                            EDBateriaInicial = LNBateria.ConsultarInicialKey(EDBateriaUsuario.Pk_Id_BateriaUsuario);
                            return View(ListaCuestionario);
                        }
                    }

                }
            }

            return HttpNotFound();

        }
        [HttpGet]
        public ActionResult EstresResultados(string IdUsuario)
        {
            List<EDBateriaCuestionario> ListaCuestionario = new List<EDBateriaCuestionario>();
            ViewBag.NombrePersona = "";
            ViewBag.Cedula = "";
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                return RedirectToAction("Login", "Home");
            }
            if (IdUsuario != null)
            {
                int IdUsuarioInt = 0;
                if (int.TryParse(IdUsuario, out IdUsuarioInt))
                {
                    EDBateriaUsuario EDBateriaUsuario = new EDBateriaUsuario();
                    EDBateriaUsuario = LNBateria.ConsultarConvocadoId(IdUsuarioInt, usuarioActual.IdEmpresa);
                    if (EDBateriaUsuario != null)
                    {
                        if (EDBateriaUsuario.Pk_Id_BateriaUsuario != 0)
                        {
                            ViewBag.NombrePersona = EDBateriaUsuario.Nombre;
                            ViewBag.Cedula = EDBateriaUsuario.NumeroIdentificacion;

                            ListaCuestionario = LNBateria.ConsultarFormulario(EDBateriaUsuario.Pk_Id_BateriaUsuario, 4);

                            EDBateriaInicial EDBateriaInicial = new EDBateriaInicial();
                            EDBateriaInicial = LNBateria.ConsultarInicialKey(EDBateriaUsuario.Pk_Id_BateriaUsuario);
                            return View(ListaCuestionario);
                        }
                    }

                }
            }

            return HttpNotFound();

        }
        #endregion

    }
}