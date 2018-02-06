using Newtonsoft.Json;
using RestSharp;
using SG_SST.Dtos.Empresas;
using SG_SST.EntidadesDominio.Empresas;
using SG_SST.Logica.Empresas;
using SG_SST.Models.Empresas;
using SG_SST.Models.Login;
using SG_SST.ServiceRequest;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SG_SST.Controllers.Empresas
{
    public class CrearEmpresaUsuariaController : Base.BaseController
    {
        //servicios
        string urlServicioEmpresas = ConfigurationManager.AppSettings["UrlServicioEmpresas"];
        string CapacidadObtenerEmpresaUsuariaDepartamentos = ConfigurationManager.AppSettings["CapacidadObtenerEmpresaUsuariaDepartamentos"];
        string CapacidadObtenerEmpresaUsuariaMunicipios = ConfigurationManager.AppSettings["CapacidadObtenerEmpresaUsuariaMunicipios"];
        string CapacidadObtenerEmpresaUsuariaMunicipiosXDepartamento = ConfigurationManager.AppSettings["CapacidadObtenerEmpresaUsuariaMunicipiosXDepartamento"];
        string CapacidadObtenerEmpresaUsuariaDocumentos = ConfigurationManager.AppSettings["CapacidadObtenerEmpresaUsuariaDocumentos"];
        string CapacidadGrabarEmpresaUsuaria = ConfigurationManager.AppSettings["CapacidadGrabarEmpresaUsuaria"];


        // GET: CrearEmpresaUsuaria
        Models.Empresa.EmpresaUsuariaModel edEmpresaTemp = new Models.Empresa.EmpresaUsuariaModel();
        LNEmpresaUsuaria lnEU = new LNEmpresaUsuaria();/// Defino variable gs

        public ActionResult Index()
        {
            ViewBag.creacion = false;
            ViewBag.errorJson = false;
            if (Session["edEmpresaTemp.listEmpresasUsuaria"] == null)
                Session["edEmpresaTemp.listEmpresasUsuaria"] = new List<EDEmpresa_Usuaria>();

            UsuarioSessionModel usuarioSesion = new UsuarioSessionModel();
            var datosUsuario = DesEncriptar(Session["UsuarioSession"].ToString());
            var usuario = JsonConvert.DeserializeObject<UsuarioSessionModel>(datosUsuario);
            if (usuarioSesion != null)
            {
                ViewBag.Documento_Empresa = usuario.NitEmpresa;
                ViewBag.Nombre_Empresa = usuario.RazonSocialEmpresa;
            }

            return View();
        }

        public ActionResult ObtenerEmpresaUsuaria(string nitempresa)
        {
            ViewBag.ResultadosCargados = true;
            ViewBag.creacion = false;
            ViewBag.errorJson = false;
            var datos = string.Empty;
            if (Session["edEmpresaTemp.listEmpresasUsuaria"] == null)
                Session["edEmpresaTemp.listEmpresasUsuaria"] = new List<EDEmpresa_Usuaria>();
            edEmpresaTemp.listEmpresasUsuaria = (List<EDEmpresa_Usuaria>)Session["edEmpresaTemp.listEmpresasUsuaria"];

            UsuarioSessionModel usuarioSesion = new UsuarioSessionModel();
            var datosUsuario = DesEncriptar(Session["UsuarioSession"].ToString());
            var usuario = JsonConvert.DeserializeObject<UsuarioSessionModel>(datosUsuario);
            if (usuarioSesion != null)
            {
                ViewBag.Documento_Empresa = usuario.NitEmpresa;
                ViewBag.Nombre_Empresa = usuario.RazonSocialEmpresa;
            }

            edEmpresaTemp.DocumentoEmpresa = nitempresa;
            edEmpresaTemp.TipoDocumento = "NI";

            ServiceClient.EliminarParametros();
            var resultDepar = ServiceClient.ObtenerArrayJsonRestFul<EDDepartamento>(urlServicioEmpresas, CapacidadObtenerEmpresaUsuariaDepartamentos, RestSharp.Method.GET);
            if (resultDepar.Count() > 0)
            {
                edEmpresaTemp.lstDepartamentos = resultDepar.Select(c => new SelectListItem()
                {
                    Value = c.Codigo_Departamento,
                    Text = c.Nombre_Departamento
                }).ToList();
            }

            edEmpresaTemp.lstMunicipios = lnEU.lstMunicipios.Select(c => new SelectListItem()
            {
                Value = c.IdMunicipio.ToString(),
                Text = c.NombreMunicipio
            }).ToList();

            ServiceClient.EliminarParametros();
            var resultTipoDocumento = ServiceClient.ObtenerArrayJsonRestFul<EDTipoDocumento>(urlServicioEmpresas, CapacidadObtenerEmpresaUsuariaDocumentos, RestSharp.Method.GET);
            if (resultTipoDocumento.Count() > 0)
            {
                edEmpresaTemp.lstDocumentos = lnEU.lstDocumentos.Select(c => new SelectListItem()
                {
                    Value = c.Sigla.ToString(),
                    Text = c.Descripcion
                }).ToList();
            }


            int cont = 0;
            if (!string.IsNullOrEmpty(nitempresa))
            {
                cont = edEmpresaTemp.listEmpresasUsuaria.Count(x => x.DocumentoEmpresaUsuaria == nitempresa);
            }

            if (cont > 0)
            {
                ViewBag.errorJson = true;
                ViewBag.creacion = false;
                ViewBag.MessagesERR = "El número de identificación ya se encuentra en la lista de empresas a asociar.";
                ViewBag.TipoDocumento = "";
                ViewBag.TipoDocumentoDisabled = false;
            }
            else if (!string.IsNullOrEmpty(nitempresa))
            {
                var cliente = new RestSharp.RestClient(ConfigurationManager.AppSettings["Url"]);
                var request = new RestRequest(ConfigurationManager.AppSettings["consultaEmpresa"], RestSharp.Method.GET);
                request.RequestFormat = DataFormat.Xml;
                request.Parameters.Clear();
                request.AddParameter("tpDoc", "ni");
                request.AddParameter("doc", nitempresa);
                request.AddParameter("color", "orange");
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Accept", "application/json");

                //se omite la validación de certificado de SSL
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

                IRestResponse<List<EmpresaSiarpDTO>> response = cliente.Execute<List<EmpresaSiarpDTO>>(request);
                var result = response.Content;

                var respuesta = Newtonsoft.Json.JsonConvert.DeserializeObject<List<EmpresaSiarpDTO>>(result);
                ViewBag.errorJson = true;
                ViewBag.creacion = true;
                ViewBag.Messages = "";
                ViewBag.TipoDocumento = "";
                ViewBag.TipoDocumentoDisabled = false;

                if (respuesta != null)
                {
                    if (respuesta.Count > 0)
                    {
                        edEmpresaTemp.RazonSocial = respuesta[0].razonSocial;
                        edEmpresaTemp.TipoDocumento = respuesta[0].tipoDoc;
                        edEmpresaTemp.DocumentoEmpresa = respuesta[0].idEmpresa;
                        edEmpresaTemp.Direccion = respuesta[0].direccionEmpresa;
                        edEmpresaTemp.Departamento = respuesta[0].departamento;
                        edEmpresaTemp.Municipio = respuesta[0].municipio;
                        ViewBag.creacion = true;
                        ViewBag.Messages = "";
                        ViewBag.TipoDocumento = edEmpresaTemp.TipoDocumento;
                        ViewBag.Departamento = edEmpresaTemp.Departamento;
                        ViewBag.Municipio = edEmpresaTemp.Municipio;
                        ViewBag.TipoDocumentoDisabled = true;
                        edEmpresaTemp.lstDepartamentos = resultDepar.Select(c => new SelectListItem()
                        {
                            Value = c.Codigo_Departamento,
                            Text = c.Nombre_Departamento,
                            Selected = (c.Nombre_Departamento.Contains(edEmpresaTemp.Departamento)) ? true : false
                        }).ToList();

                        var depsel = edEmpresaTemp.lstDepartamentos.FirstOrDefault(a => a.Selected == true);

                        ServiceClient.EliminarParametros();
                        ServiceClient.AdicionarParametro("departamento", int.Parse(depsel.Value));
                        var resultMunicipio = ServiceClient.ObtenerArrayJsonRestFul<EDMunicipio>(urlServicioEmpresas, CapacidadObtenerEmpresaUsuariaMunicipiosXDepartamento, RestSharp.Method.GET);
                        if (resultMunicipio.Count() > 0)
                        {
                            edEmpresaTemp.lstMunicipios = resultMunicipio.Select(c => new SelectListItem()
                            {
                                Value = c.IdMunicipio.ToString(),
                                Text = c.NombreMunicipio,
                                Selected = (c.NombreMunicipio == edEmpresaTemp.Municipio) ? true : false
                            }).ToList();

                            Session["departamento_selected"] = int.Parse(depsel.Value);

                            var munsel = edEmpresaTemp.lstMunicipios.FirstOrDefault(a => a.Selected == true);
                            Session["municipio_selected"] = munsel.Value;
                        }

                    }
                    else
                    {
                        ViewBag.errorJson = true;
                        ViewBag.creacion = true;
                        ViewBag.MessagesERR = "No se encontraron datos relacionados con el nit ingresado, por favor ingrese la información de la empresa para finalizar la asociación";
                        ViewBag.TipoDocumento = "";
                        ViewBag.TipoDocumentoDisabled = false;
                    }
                }
                else
                {
                    ViewBag.errorJson = true;
                    ViewBag.creacion = true;
                    ViewBag.MessagesERR = "No se encontraron datos relacionados con el nit ingresado, por favor ingrese la información de la empresa para finalizar la asociación";
                    ViewBag.TipoDocumento = "";
                    ViewBag.TipoDocumentoDisabled = false;
                }
            }
            Session["edEmpresaTemp.listEmpresasUsuaria"] = edEmpresaTemp.listEmpresasUsuaria;
            return View("Index", edEmpresaTemp);

        }

        public ActionResult AdicionarEmpresaUsuaria(string RazonSocial_crea, string DocumentoEmpresa_crea, string Direccion_crea, string dd_Departamentos, string dd_Municipios, string dd_Documentos)
        {
            ViewBag.ResultadosCargados = true;
            ViewBag.creacion = false;
            ViewBag.errorJson = false;
            var datos = string.Empty;

            if (dd_Departamentos == null)
                dd_Departamentos = Session["departamento_selected"].ToString();

            if (dd_Municipios == null)
                dd_Municipios = Session["municipio_selected"].ToString();

            if (Session["edEmpresaTemp.listEmpresasUsuaria"] == null)
                Session["edEmpresaTemp.listEmpresasUsuaria"] = new List<EDEmpresa_Usuaria>();

            edEmpresaTemp.listEmpresasUsuaria = (List<EDEmpresa_Usuaria>)Session["edEmpresaTemp.listEmpresasUsuaria"];

            UsuarioSessionModel usuarioSesion = new UsuarioSessionModel();
            var datosUsuario = DesEncriptar(Session["UsuarioSession"].ToString());
            var usuario1 = JsonConvert.DeserializeObject<UsuarioSessionModel>(datosUsuario);
            if (usuarioSesion != null)
            {
                ViewBag.Documento_Empresa = usuario1.NitEmpresa;
                ViewBag.Nombre_Empresa = usuario1.RazonSocialEmpresa;
            }


            //validar que no exista el nit en la lista
            if (edEmpresaTemp.listEmpresasUsuaria.Where(u => u.DocumentoEmpresaUsuaria == DocumentoEmpresa_crea).Any())
            {
                ViewBag.Messages = "El nit de la empresa ya existe en la lista.";
            }
            else
            {
                ServiceClient.EliminarParametros();
                var resultDepar = ServiceClient.ObtenerArrayJsonRestFul<EDDepartamento>(urlServicioEmpresas, CapacidadObtenerEmpresaUsuariaDepartamentos, RestSharp.Method.GET);
                if (resultDepar.Count() > 0)
                {
                    edEmpresaTemp.lstDepartamentos = resultDepar.Select(c => new SelectListItem()
                    {
                        Value = c.Codigo_Departamento,
                        Text = c.Nombre_Departamento
                    }).ToList();
                }


                ServiceClient.EliminarParametros();
                ServiceClient.AdicionarParametro("departamento", int.Parse(dd_Departamentos));
                var resultMunicipio = ServiceClient.ObtenerArrayJsonRestFul<EDMunicipio>(urlServicioEmpresas, CapacidadObtenerEmpresaUsuariaMunicipiosXDepartamento, RestSharp.Method.GET);
                if (resultMunicipio.Count() > 0)
                {
                    edEmpresaTemp.lstMunicipios = resultMunicipio.Select(c => new SelectListItem()
                    {
                        Value = c.IdMunicipio.ToString(),
                        Text = c.NombreMunicipio
                    }).ToList();
                }


                EDEmpresa_Usuaria eu = new EDEmpresa_Usuaria();
                var usuario = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
                eu.DocumentoEmpresa = usuario.NitEmpresa;

                eu.RazonSocial = RazonSocial_crea;
                eu.TipoDocumento = (dd_Documentos==null) ? "NI" : dd_Documentos; 
                eu.DocumentoEmpresaUsuaria = DocumentoEmpresa_crea;
                eu.Direccion = Direccion_crea;
                eu.IdDepartamento = int.Parse(dd_Departamentos);
                eu.Id_Municipio = int.Parse(dd_Municipios);

                var departamento_txt = edEmpresaTemp.lstDepartamentos
                    .Where(c => c.Value == eu.IdDepartamento.ToString())
                    .Select(cc => cc.Text)
                    .SingleOrDefault();

                var municipio_txt = edEmpresaTemp.lstMunicipios
                    .Where(c => c.Value == eu.Id_Municipio.ToString())
                    .Select(cc => cc.Text)
                    .FirstOrDefault();

                eu.Departamento = departamento_txt;
                eu.Municipio = municipio_txt;

                edEmpresaTemp.listEmpresasUsuaria.Add(eu);

            }
            Session["edEmpresaTemp.listEmpresasUsuaria"] = edEmpresaTemp.listEmpresasUsuaria;
            return View("Index", edEmpresaTemp);
        }

        public ActionResult EliminarEmpresaUsuaria(string idNitParaEli)
        {
            ViewBag.ResultadosCargados = true;
            ViewBag.creacion = false;
            ViewBag.errorJson = false;
            var datos = string.Empty;
            if (Session["edEmpresaTemp.listEmpresasUsuaria"] == null)
                Session["edEmpresaTemp.listEmpresasUsuaria"] = new List<EDEmpresa_Usuaria>();
            edEmpresaTemp.listEmpresasUsuaria = (List<EDEmpresa_Usuaria>)Session["edEmpresaTemp.listEmpresasUsuaria"];

            UsuarioSessionModel usuarioSesion = new UsuarioSessionModel();
            var datosUsuario = DesEncriptar(Session["UsuarioSession"].ToString());
            var usuario = JsonConvert.DeserializeObject<UsuarioSessionModel>(datosUsuario);
            if (usuarioSesion != null)
            {
                ViewBag.Documento_Empresa = usuario.NitEmpresa;
                ViewBag.Nombre_Empresa = usuario.RazonSocialEmpresa;
            }


            ServiceClient.EliminarParametros();
            var resultDepar = ServiceClient.ObtenerArrayJsonRestFul<EDDepartamento>(urlServicioEmpresas, CapacidadObtenerEmpresaUsuariaDepartamentos, RestSharp.Method.GET);
            if (resultDepar.Count() > 0)
            {
                edEmpresaTemp.lstDepartamentos = resultDepar.Select(c => new SelectListItem()
                {
                    Value = c.Codigo_Departamento,
                    Text = c.Nombre_Departamento
                }).ToList();
            }

            ServiceClient.EliminarParametros();
            var resultMunicipio = ServiceClient.ObtenerArrayJsonRestFul<EDMunicipio>(urlServicioEmpresas, CapacidadObtenerEmpresaUsuariaMunicipios, RestSharp.Method.GET);
            if (resultMunicipio.Count() > 0)
            {
                edEmpresaTemp.lstMunicipios = lnEU.lstMunicipios.Select(c => new SelectListItem()
                {
                    Value = c.IdMunicipio.ToString(),
                    Text = c.NombreMunicipio
                }).ToList();
            }

            edEmpresaTemp.listEmpresasUsuaria.RemoveAll(x => x.DocumentoEmpresaUsuaria == idNitParaEli);
            Session["edEmpresaTemp.listEmpresasUsuaria"] = edEmpresaTemp.listEmpresasUsuaria;
            return View("Index", edEmpresaTemp);
        }

        public ActionResult GuardarEmpresasUsuarias()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "Debe estar autenticado para registrar una Ausencia.";
                return View();
            }
            else
            {
                ViewBag.Documento_Empresa = usuarioActual.NitEmpresa;
                ViewBag.Nombre_Empresa = usuarioActual.RazonSocialEmpresa;
            }

            ViewBag.ResultadosCargados = true;
            ViewBag.creacion = false;
            ViewBag.errorJson = false;
            var datos = string.Empty;
            if (Session["edEmpresaTemp.listEmpresasUsuaria"] == null)
                Session["edEmpresaTemp.listEmpresasUsuaria"] = new List<EDEmpresa_Usuaria>();
            List<EDEmpresa_Usuaria> lstEmpUsu = new List<EDEmpresa_Usuaria>();
            lstEmpUsu = (List<EDEmpresa_Usuaria>)Session["edEmpresaTemp.listEmpresasUsuaria"];

            if (lstEmpUsu.Count > 0)
            {
                string DocumentoEmpresaLogeada = "";
                var usuario = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
                DocumentoEmpresaLogeada = usuario.NitEmpresa;
                //lnEU.EliminaEmpresasUsuarias(DocumentoEmpresaLogeada);
                //se inserta los registros de empresas usuarias asociadas al nit logueado
                //EDListEmpresa_Usuaria lstEU = new EDListEmpresa_Usuaria();

                var lstEmpUsuCpy = new List<EDEmpresa_UsuariaA>();
                lstEmpUsuCpy = lstEmpUsu.Select(c => new EDEmpresa_UsuariaA()
                {
                    Departamento = c.Departamento,
                    Direccion = c.Direccion,
                    DocumentoEmpresa = c.DocumentoEmpresa,
                    DocumentoEmpresaUsuaria = c.DocumentoEmpresaUsuaria,
                    Estado_bd = c.Estado_bd,
                    IdDepartamento = c.IdDepartamento,
                    IdEmpresaUsuaria = c.IdEmpresaUsuaria,
                    IdTipoDocumento = c.IdTipoDocumento,
                    Id_Municipio = c.Id_Municipio,
                    Municipio = c.Municipio,
                    RazonSocial = c.RazonSocial,
                    TipoDocumento = c.TipoDocumento
                }).ToList();
                //new List<EDEmpresa_Usuaria>(lstEmpUsu);

                ServiceClient.EliminarParametros();
                var result = ServiceClient.RealizarPeticionesPostJsonRestFul<List<EDEmpresa_UsuariaA>>(urlServicioEmpresas, CapacidadGrabarEmpresaUsuaria, lstEmpUsuCpy);
                if (result != null)
                {
                    if (result[0].rta)
                        ViewBag.MessagesOK = result[0].Estado_bd;
                    else
                        ViewBag.MessagesERR = result[0].Estado_bd;
                }
                else
                    ViewBag.MessagesERR = "Error guardando empresas usuarias.";
                //
            }
            else
                ViewBag.MessagesERR = "Debe agregar empresas a asociar.";

            //se limpia la lista 
            edEmpresaTemp.listEmpresasUsuaria = new List<EDEmpresa_Usuaria>();
            Session["edEmpresaTemp.listEmpresasUsuaria"] = edEmpresaTemp.listEmpresasUsuaria;
            return View("Index", edEmpresaTemp);

        }

        public ActionResult ir_A_login()
        {
            var usuario = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            TempData["UsuarioSesion"] = usuario;
            return RedirectToAction("Index", "Home");
        }

    }
}