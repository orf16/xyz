using Newtonsoft.Json;
using SG_SST.EntidadesDominio.Ausentismo;
using SG_SST.Models.AdminUsuarios;
using SG_SST.Models.Ausentismo;
using SG_SST.Models.Login;
using SG_SST.Models.Planificacion;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace SG_SST.Controllers.Base
{
    public class BaseController : Controller
    {
        public string urlServicioEmpresas = ConfigurationManager.AppSettings["UrlServicioEmpresas"];
       
        public string CapacidadObtenerEmpresasusuarias = ConfigurationManager.AppSettings["CapacidadObtenerEmpresasusuarias"];
        public string CapacidadObtenerSedesPorNit = ConfigurationManager.AppSettings["CapacidadObtenerSedesPorNit"];
        public string CapacidadObtenerprocesosEmpresa = ConfigurationManager.AppSettings["CapacidadObtenerprocesosEmpresa"];
        public string CapacidadDescaragarplatillaausentismo = ConfigurationManager.AppSettings["CapacidadDescaragarplatillaausentismo"];
        public string urlServicioPlanificacion = ConfigurationManager.AppSettings["UrlServicioPlanificacion"];
        public string rutaPlantillaAusentismo = ConfigurationManager.AppSettings["rutaPlantillaAusentismo"];
        public string CapacidadCargarplatillaausentismo = ConfigurationManager.AppSettings["CapacidadCargarplatillaausentismo"];
        public string consultaAfiliadoEmpresaActivo = ConfigurationManager.AppSettings["consultaAfiliadoEmpresaActivo"];
        public string rutaArchivoTerminosCondiciones = ConfigurationManager.AppSettings["rutaArchivoTerminosCondiciones"];
        public string cantidadRegistrosPagina = ConfigurationManager.AppSettings["CantidadRegistrosPagina"];
        public bool registrarLogAuditioria = ConfigurationManager.AppSettings["RegistrarLogAuditioria"] == null ? false : Convert.ToBoolean(ConfigurationManager.AppSettings["registrarLogAuditioria"]);
        public int anioInicialGestPost = ConfigurationManager.AppSettings["AnioIncialGestPost"] == null ? 2011 : Convert.ToInt32(ConfigurationManager.AppSettings["AnioIncialGestPost"].ToString());
        public string CapacidadObtenerObjetivoAnalisis = ConfigurationManager.AppSettings["CapacidadObtenerObjetivoAnalisis"];
        public string CapacidadObtenerTipoAnalisis = ConfigurationManager.AppSettings["CapacidadObtenerTipoAnalisis"];
        public string urlServicioAutenticacion = ConfigurationManager.AppSettings["UrlServicioAutenticacion"];
        public string capacidadAutenticacion = ConfigurationManager.AppSettings["CapacidadAutenticacion"];
        public string urlServicioEnfermedadLaboral = ConfigurationManager.AppSettings["UrlServicioEnfermedadLaboral"];
        public string capacidadEnfermedadLaboral = ConfigurationManager.AppSettings["CapacidadEnfermedadLaboral"];
        public string rutaFisicaDocumentosEnfLabFurel = ConfigurationManager.AppSettings["RutaFisicaDocumentosEnfLabFurel"];
        public string rutaFisicaDocumentosEnfLabCartaEPS = ConfigurationManager.AppSettings["RutaFisicaDocumentosEnfLabCartaEPS"];
        public string rutaFisicaDocumentosEnfLabTiposDoc = ConfigurationManager.AppSettings["RutaFisicaDocumentosEnfLabTiposDoc"];
        public string usuarioImp = ConfigurationManager.AppSettings["UsuarioImp"];
        public string passwordImp = ConfigurationManager.AppSettings["PasswordImp"];
        public string dominio = ConfigurationManager.AppSettings["Dominio"];
        public string RenderRazorViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext,
                                                                         viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View,
                                             ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }

        internal List<SelectListItem> GetAnios(int resultAno)
        {
            int ano = DateTime.Now.Year - 2011;
            return Enumerable.Range(2011, ano + 1)
                .OrderByDescending(x => x).
                Select(x => new SelectListItem
                {
                    Value = x.ToString(),
                    Text = x.ToString()
                }).ToList();
            //List<SelectListItem> anios = new List<SelectListItem>();
            //int anoActual = DateTime.Now.Year;
            //for (int ano = anioInicialGestPost; ano <= anoActual; ano++)
            //{
            //    anios.Add(new SelectListItem { Value = ano.ToString(), Text = ano.ToString() });
            //}

            //return anios;
        }

        public UsuarioSessionModel ObtenerUusuarioLogueado(HttpContext context)
        {
            var usuario = (UsuarioSessionModel)ObtenerUsuarioEnSesion(context);
            return usuario;
        }

        /// Encripta una cadena
        public string Encriptar(string _cadenaAencriptar)
        {
            string result = string.Empty;
            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(_cadenaAencriptar);
            result = Convert.ToBase64String(encryted);
            return result;
        }

        /// Esta función desencripta la cadena que le envíamos en el parámentro de entrada.
        public string DesEncriptar(string _cadenaAdesencriptar)
        {
            string result = string.Empty;
            byte[] decryted = Convert.FromBase64String(_cadenaAdesencriptar);
            //result = System.Text.Encoding.Unicode.GetString(decryted, 0, decryted.ToArray().Length);
            result = System.Text.Encoding.Unicode.GetString(decryted);
            return result;
        }

        /// <summary>
        /// Guarda en sesion los datos del afiliado autenticado
        /// </summary>
        /// <param name="afiliado"></param>
        public void GuardarSesionUsuario(UsuarioSessionModel usuarioSesion)
        {
            if (Session["UsuarioSession"] == null) {
                if (usuarioSesion != null)
                    Session["UsuarioSession"] = Encriptar(JsonConvert.SerializeObject(usuarioSesion));
            }
            else if(Session["UsuarioSession"] != null && !String.IsNullOrWhiteSpace(Session["UsuarioSession"].ToString()))
            {
                var datosUsuario = DesEncriptar(Session["UsuarioSession"].ToString());
                var usuario = JsonConvert.DeserializeObject<UsuarioSessionModel>(datosUsuario);
                if (usuarioSesion != null) {
                    usuarioSesion.IdUsuario = usuario.IdUsuario;
                    usuarioSesion.IdEmpresa = usuario.IdEmpresa;
                    usuarioSesion.NitEmpresa = usuario.NitEmpresa;
                    usuarioSesion.RazonSocialEmpresa = usuario.RazonSocialEmpresa;
                    usuarioSesion.CantidadDiasLaborales = usuario.CantidadDiasLaborales;
                    Session["UsuarioSession"] = Encriptar(JsonConvert.SerializeObject(usuarioSesion));
                }
            }
        }

        /// <summary>
        /// Guarda en sesion la empresa a evaluar
        /// </summary>
        /// <param name="afiliado"></param>
        public void GuardarSesionAfiliado(EmpresaAfiliadoModel afiliado)
        {
          if (afiliado != null)
            Session["AfiliadoSession"] = Encriptar(JsonConvert.SerializeObject(afiliado));
        }

        /// <summary>
        /// Obtiene los datos de sesion asociados a un usuario
        /// </summary>
        /// <returns></returns>
        //public UsuarioSessionModel ObtenerUsuarioEnSesion(System.Web.HttpContext.Current)
        //{
        //    if (Session["UsuarioSession"] != null && !String.IsNullOrWhiteSpace(Session["UsuarioSession"].ToString()))
        //    {
        //        var datosUsuario = DesEncriptar(Session["UsuarioSession"].ToString());
        //        var usuario = JsonConvert.DeserializeObject<UsuarioSessionModel>(datosUsuario);
        //        return usuario;
        //    }
        //    else
        //        return null;
        //}


        /// <summary>
        /// Obtiene los datos de sesion asociados a un usuario
        /// </summary>
        /// <returns></returns>
        public UsuarioSessionModel ObtenerUsuarioEnSesion(HttpContext context)
        {
            System.Web.HttpContext.Current = context;
            if (System.Web.HttpContext.Current != null && System.Web.HttpContext.Current.Session["UsuarioSession"] != null
                && !String.IsNullOrWhiteSpace(System.Web.HttpContext.Current.Session["UsuarioSession"].ToString()))
            {
                var datosUsuario = DesEncriptar(System.Web.HttpContext.Current.Session["UsuarioSession"].ToString());
                var usuario = JsonConvert.DeserializeObject<UsuarioSessionModel>(datosUsuario);
                return usuario;
            }
            else
                return null;
        }

        public AfiliadoModel ObtenerAfiliadoEnSesion()
        {
            if (Session["AfiliadoSession"] != null && !String.IsNullOrWhiteSpace(Session["AfiliadoSession"].ToString()))
            {
                var datosEvaluacion = DesEncriptar(Session["AfiliadoSession"].ToString());
                var afiliado = JsonConvert.DeserializeObject<AfiliadoModel>(datosEvaluacion);
                return afiliado;
            }
            else
                return null;
        }

        public string ObtenerValorConformato(string valor)
        {
            try
            {
                return Regex.Match(valor, @"(([0-9]?(\.|\,)[1-9])*0*[1-9]*)*").Value;
            }catch
            {
                return valor;
            }
        }

        public bool LoginValido()
        {
            bool rta = false;
            string strUrl = HttpContext.Request.Url.GetLeftPart(UriPartial.Authority);

            ViewBag.ReturnUrl = strUrl;

            if (Session["UsuarioSession"] == null)
            {
                Response.Redirect(strUrl);
                rta = false;
            }
            else
                rta = true;

            return rta;
        }

        public void Enviar_a_Login()
        {
            bool rta = false;
            string strUrl = HttpContext.Request.Url.GetLeftPart(UriPartial.Authority);

            ViewBag.ReturnUrl = strUrl;

            Response.Redirect(strUrl);
        }

        /// <summary>
        /// Retorna la lista de los municipios para un departamento indicado.
        /// </summary>
        /// <param name="departamentoID"></param>
        /// <returns></returns>
        public IEnumerable<EDMunicipio> ObtenerMunicipios(int departamentoID)
        {
            var lnmunicipio = new Logica.Ausentismo.LNMunicipio();
            return lnmunicipio.ObtenerListadoMunicipio(departamentoID);
        }
    }
}