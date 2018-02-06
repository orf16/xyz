using System.IO;
using System.Linq;
using System.Web.Mvc;
using SG_SST.Models;
using System.Collections.Generic;
using System.Web;
using SG_SST.Models.Empresas;
using SG_SST.EntidadesDominio.Empresas;
using SG_SST.Logica.Empresas;
using LinqToExcel;
using SG_SST.Models.Login;
using SG_SST.Models.Empresa;
using Newtonsoft.Json;
using System;
using ClosedXML.Excel;
using SG_SST.ServiceRequest;
using System.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System.Net;
using SG_SST.Dtos.Empresas;

namespace SG_SST.Controllers.Empresas
{


    public class RelacionesLaboralesController : Base.BaseController
    {

        LNRelacionesLaborales lnRL = new LNRelacionesLaborales();/// Defino variable gs
        Models.Empresa.EmpleadoRelLabModel edempleadoRL = new EmpleadoRelLabModel();
        private int numeroRegistros = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["numero_registros_x_consulta"]);

        //servicios
        string CapacidadEstadoEmpleado = ConfigurationManager.AppSettings["CapacidadEstadoEmpleado"];
        string CapacidadRelacionlaboralArchivoRelacionesLaborales = ConfigurationManager.AppSettings["CapacidadRelacionlaboralArchivoRelacionesLaborales"];
        string CapacidadRelacionlaboralTipoCotizante = ConfigurationManager.AppSettings["CapacidadRelacionlaboralTipoCotizante"];
        string CapacidadRelacionlaboralDescargaArchivoExcelEmpleado = ConfigurationManager.AppSettings["CapacidadRelacionlaboralDescargaArchivoExcelEmpleado"];
        string CapacidadRelacionlaboralDescargaArchivoExcelTerceroRelLab = ConfigurationManager.AppSettings["CapacidadRelacionlaboralDescargaArchivoExcelTerceroRelLab"];
        string CapacidadRelacionlaboralTiposTerceros = ConfigurationManager.AppSettings["CapacidadRelacionlaboralTiposTerceros"];
        string CapacidadRelacionlaboralListarRelacionesLabTerceros = ConfigurationManager.AppSettings["CapacidadRelacionlaboralListarRelacionesLabTerceros"];
        string CapacidadRelacionlaboralListarRelacionesLaborales = ConfigurationManager.AppSettings["CapacidadRelacionlaboralListarRelacionesLaborales"];
        string CapacidadRelacionlaboralRazonesSociales = ConfigurationManager.AppSettings["CapacidadRelacionlaboralRazonesSociales"];
        string CapacidadRelacionlaboralTiposInconsistencias = ConfigurationManager.AppSettings["CapacidadRelacionlaboralTiposInconsistencias"];
        string CapacidadRelacionlaboralGrabarNotificarInconsistenciaLaboral = ConfigurationManager.AppSettings["CapacidadRelacionlaboralGrabarNotificarInconsistenciaLaboral"];



        // GET: Cont_OtrasInteracciones

        //
        // GET: /RelacionesLaborales/
        public ActionResult Index()
        {

            ViewBag.ErrorProcesaArchivo = false;
            ServiceClient.EliminarParametros();
            var resultEstado_Empl = ServiceClient.ObtenerArrayJsonRestFul<EDTipos>(urlServicioEmpresas, CapacidadEstadoEmpleado, RestSharp.Method.GET);
            if (resultEstado_Empl.Count() > 0)
            {
                ViewBag.PK_IDEmpleadoEst = new SelectList(resultEstado_Empl, "Id_Tipo", "Descripcion");
            }

            ServiceClient.EliminarParametros();
            var resultTipoCotizante = ServiceClient.ObtenerArrayJsonRestFul<EDTipos>(urlServicioEmpresas, CapacidadRelacionlaboralTipoCotizante, RestSharp.Method.GET);
            if (resultTipoCotizante.Count() > 0)
            {
                ViewBag.Pk_Id_Cotizante = new SelectList(resultTipoCotizante, "Id_Tipo", "Descripcion");
            }

            return View(); //db.tblEmpleado.ToList());//lista para mostrar los archivos cargados


        }

        [HttpPost]
        public ActionResult Download()
        {
            string fileName = "PlantillaRelacionesLaborales.xls";
            string path = System.AppDomain.CurrentDomain.BaseDirectory + "Plantillas/";
            byte[] fileBytes = System.IO.File.ReadAllBytes(path + "PlantillaRelacionesLaborales.xls");
            return File(fileBytes, "application/vnd.ms-excel", fileName);
        }


        public ActionResult CargarArchivoRelacionesLaborales(HttpPostedFileBase ArchivoRelacionesLaborales)
        {
            var path = "";
            bool rta = true;
            string mensaje_validacion = "";
            ViewBag.MessagesOK = "";
            ViewBag.MessagesERR = "";

            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "Debe estar autenticado para realizar la evalación.";
                return RedirectToAction("Login", "Home");
            }

            if (ArchivoRelacionesLaborales != null && ArchivoRelacionesLaborales.ContentLength > 0)
            {

                if (Path.GetExtension(ArchivoRelacionesLaborales.FileName).ToLower() == ".xls")
                {
                    path = Path.Combine(Server.MapPath("~/Content/ArchivosRelacionesLaborales"), usuarioActual.NitEmpresa + ".xls");
                    ArchivoRelacionesLaborales.SaveAs(path);
                    ViewBag.UploadSuccess = true;
                    try
                    {
                        var usuario = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
                        //creamos el libro a partir de la ruta
                        //var resp = ReadFileCVS(path, usuario.IdEmpresa);
                        var Book = new ExcelQueryFactory(path);
                        //Consulta con Linq
                        var resp = (from row in Book.Worksheet("RELACIONES LABORALES")
                                    let item = new EDRelacionesLaborales
                                    {
                                        //Id_Tematica = row[0].Cast<string>();
                                        TipoTercero = ValidarNullCell(row, 0),
                                        NitEmpresa = ValidarNullCell(row, 1),
                                        RazonSocial = ValidarNullCell(row, 2),
                                        TipoDocumento = ValidarNullCell(row, 3),
                                        NumeroDocumento = ValidarNullCell(row, 4),
                                        Apellido1 = ValidarNullCell(row, 5),
                                        Apellido2 = ValidarNullCell(row, 6),
                                        Nombre1 = ValidarNullCell(row, 7),
                                        Nombre2 = ValidarNullCell(row, 8),
                                        FechaNacimiento = ValidarNullCell(row, 9),
                                        Ocupacion = ValidarNullCell(row, 10),
                                        Cargo = ValidarNullCell(row, 11),
                                        Email = ValidarNullCell(row, 12),
                                        idEmpresa = usuario.IdEmpresa.ToString()
                                    }
                                    select item).ToList();

                        Book.Dispose();
                        //resp = ValidarFilaVacia(resp);
                        //rta = lnRL.GrabarArchivoRelacionesLaborales(resp, out mensaje_validacion);
                        ServiceClient.EliminarParametros();
                        var result = ServiceClient.RealizarPeticionesPostJsonRestFul<List<EDRelacionesLaborales>>(urlServicioEmpresas, CapacidadRelacionlaboralArchivoRelacionesLaborales, resp);

                        if (result.Count() > 0)
                        {
                            EDRelacionesLaborales euRL = result[0];
                            rta = euRL.Rta;
                            mensaje_validacion = euRL.Mensaje_validacion;
                        }

                    }
                    catch (System.Exception ex)
                    {
                        rta = false;
                        ViewBag.MessagesERR = "Error en el formato del archivo. por favor verifique.";
                        if (System.IO.File.Exists(path))
                            System.IO.File.Delete(path);
                    }
                }
                else
                {
                    rta = false;
                    ViewBag.MessagesERR = "Error en el formato del archivo. por favor verifique.";
                }
            }
            else
            {
                ViewBag.MessagesERR = "No ha seleccionado un documento para cargar";
                return View("Index");                
            }
            if (rta)
            {
                ViewBag.ErrorProcesaArchivo = false;
                ViewBag.MessagesOK = "Documento cargado correctamente";
                
                if (System.IO.File.Exists(path))
                    System.IO.File.Delete(path);
                return View("Index");
            }
            else
            {
                ViewBag.ErrorProcesaArchivo = true;
                ViewBag.MessagesERR = (mensaje_validacion == "") ? ViewBag.MessagesERR: mensaje_validacion;
                
                if (System.IO.File.Exists(path))
                    System.IO.File.Delete(path);

                return View("Index");
            }
        }


        private List<EDRelacionesLaborales> ValidarFilaVacia(List<EDRelacionesLaborales> lstEDRel)
        {

            List<EDRelacionesLaborales> lstEDRelA = new List<EDRelacionesLaborales>();

            foreach (EDRelacionesLaborales edr in lstEDRel)
            {
                if (edr.NitEmpresa != null && edr.NumeroDocumento != null)
                {
                    if (edr.NitEmpresa != "" && edr.NumeroDocumento != "")
                    {
                        lstEDRelA.Add(edr);
                    }
                }
            }
            return lstEDRelA;
        }

        private string ValidarNullCell(Row row, int fila)
        {
            string rta = "";
            try
            {
                rta = row[fila].Cast<string>();
            }
            catch
            {
                ; rta = "";
            }
            return rta;
        }

        public FileResult 
            DescargarRelLab(string dd_estado, string dd_tipoCotizante)
        {
            EDEmpresa_Usuaria eu = new EDEmpresa_Usuaria();
            var usuario = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            eu.DocumentoEmpresa = usuario.NitEmpresa;

            ConsultarWSRelacionesLaborales(1, eu.DocumentoEmpresa, dd_estado, dd_tipoCotizante);

            List<EstadoTipoAfiliadoDTO> lstEstadoTipoAfiliadoDTO = (List<EstadoTipoAfiliadoDTO>)Session["List<EstadoTipoAfiliadoDTO>"];
            if (lstEstadoTipoAfiliadoDTO != null)
            {
                if (lstEstadoTipoAfiliadoDTO.Count() > 0)
                {
                    System.Data.DataTable dt = RelacionesLaboralesWSDT(lstEstadoTipoAfiliadoDTO);

                    //lnRL.DescargaArchivoExcelEmpleado(eu.DocumentoEmpresa, estado, tipoCotizante);
                    dt.TableName = "ConRelLaborales(" + eu.DocumentoEmpresa + ")";
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt);

                        using (MemoryStream stream = new MemoryStream())
                        {
                            wb.SaveAs(stream);
                            return File(stream.ToArray(),
                                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                                "ConsultarRelacionesLaborales.xlsx");
                        }
                    }
                }
                else
                {
                    ViewBag.Messages = "No se encontraron registros. por favor verifique.";
                    return null;
                }
            }
            else
            {
                ViewBag.Messages = "No se encontraron registros. por favor verifique.";
                return null;
            }

        }

        public FileResult DescargarTerceroRelLab(string tipoTercero, string razonSocialNit)
        {

            EDEmpresa_Usuaria eu = new EDEmpresa_Usuaria();
            var usuario = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            eu.DocumentoEmpresa = usuario.NitEmpresa;

            System.Data.DataTable dt = null; // lnRL.DescargaArchivoExcelTerceroRelLab(eu.DocumentoEmpresa, tipoTercero);
            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("nit", eu.DocumentoEmpresa);
            ServiceClient.AdicionarParametro("tipoTercero", tipoTercero);
            ServiceClient.AdicionarParametro("razonSocialnit", razonSocialNit);

            var result = ServiceClient.ObtenerArrayJsonRestFul<EDEmpresa_Usuaria>(urlServicioEmpresas, CapacidadRelacionlaboralDescargaArchivoExcelTerceroRelLab, RestSharp.Method.GET);
            if (result.Count() > 0)
            {
                EDEmpresa_Usuaria euEU = result[0];
                dt = euEU.DT;
                dt.TableName = "ConTerRelLaborales(" + eu.DocumentoEmpresa + ")";
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);

                    if (dt.Rows.Count > 0)
                    {
                        using (MemoryStream stream = new MemoryStream())
                        {
                            wb.SaveAs(stream);
                            return File(stream.ToArray(),
                                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                                "ConsultarTerceroRelacionesLaborales.xlsx");
                        }
                    }
                    else
                    {
                        ViewBag.Messages = "No se encontraron registros. por favor verifique.";
                        return null;
                    }

                }
            }
            else
            {
                ViewBag.Messages = "No se encontraron registros. por favor verifique.";
                return null;
            }

        }
        public ActionResult ConsultaTercerosRelacionesLaborales()
        {
            if (LoginValido())
            {
                ServiceClient.EliminarParametros();
                var result = ServiceClient.ObtenerArrayJsonRestFul<EDTipos>(urlServicioEmpresas, CapacidadRelacionlaboralTiposTerceros, RestSharp.Method.GET);
                if (result.Count() > 0)
                {
                    edempleadoRL.lstTiposTerceros = result.Select(c => new SelectListItem()
                    {
                        Value = c.Id_Tipo.ToString(),
                        Text = c.Descripcion
                    }).ToList();

                    ViewBag.lstTiposTerceros = edempleadoRL.lstTiposTerceros;
                }

                UsuarioSessionModel usuarioSesion = new UsuarioSessionModel();
                var datosUsuario = DesEncriptar(Session["UsuarioSession"].ToString());
                var usuario = JsonConvert.DeserializeObject<UsuarioSessionModel>(datosUsuario);
                if (usuarioSesion != null)
                {
                    ViewBag.Documento_Empresa = usuario.NitEmpresa;
                    ViewBag.Nombre_Empresa = usuario.RazonSocialEmpresa;
                }

                //busca las razones sociales de tercero


                ServiceClient.EliminarParametros();
                ServiceClient.AdicionarParametro("NitEmpresaLogueada", usuario.NitEmpresa);

                var resultRazSoc = ServiceClient.ObtenerArrayJsonRestFul<EDTiposS>(urlServicioEmpresas, CapacidadRelacionlaboralRazonesSociales, RestSharp.Method.GET);
                if (resultRazSoc != null)
                {
                    edempleadoRL.lstRazonesSociales = resultRazSoc.Select(c => new SelectListItem()
                    {
                        Value = c.Id_Tipo,
                        Text = c.Descripcion
                    }).ToList();

                    ViewBag.lstRazonesSociales = edempleadoRL.lstRazonesSociales;
                }
                else
                    edempleadoRL.lstRazonesSociales = new List<SelectListItem>();



            }
            List<EDEmpleadoTercero> lstedRL = new List<EDEmpleadoTercero>();
            return View("ConsultarTercero", lstedRL);

        }


        public ActionResult ConsultaRelacionesLaborales()
        {

            if (LoginValido())
            {
                //List<EDTipos> lstEstadosEmpleados = Ser lnRL.DevuelveEstadosEmpleados();

                ServiceClient.EliminarParametros();
                var resultEmpEval = ServiceClient.ObtenerArrayJsonRestFul<EDTipos>(urlServicioEmpresas, CapacidadEstadoEmpleado, RestSharp.Method.GET);
                if (resultEmpEval.Count() > 0)
                {

                    edempleadoRL.lstEstados = resultEmpEval.Select(c => new SelectListItem()
                    {
                        Value = c.Id_Tipo.ToString(),
                        Text = c.Descripcion
                    }).ToList();
                }

                //List<EDTipos> lstTiposCotizantes = lnRL.DevuelveTiposCotizantes();
                ServiceClient.EliminarParametros();
                var resultTiposCotizantes = ServiceClient.ObtenerArrayJsonRestFul<EDTipos>(urlServicioEmpresas, CapacidadRelacionlaboralTipoCotizante, RestSharp.Method.GET);
                if (resultTiposCotizantes.Count() > 0)
                {
                    edempleadoRL.lstTiposCotizantes = resultTiposCotizantes.Select(c => new SelectListItem()
                    {
                        Value = c.Id_Tipo.ToString(),
                        Text = c.Descripcion
                    }).ToList();
                }
                ViewBag.lstEstados = edempleadoRL.lstEstados;
                ViewBag.lstTiposCotizantes = edempleadoRL.lstTiposCotizantes;

            }

            List<EDEmpleadoRelLab> lstedRL = new List<EDEmpleadoRelLab>();
            return View("Consultar", lstedRL);

        }


        private List<EDEmpleadoRelLab> ConsultarWSRelacionesLaborales(int id, string NitEmpresa, string estadoAfi, string TipoVinAfi)
        {
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

            var respuesta = Newtonsoft.Json.JsonConvert.DeserializeObject<List<EstadoTipoAfiliadoDTO>>(result);
            Session["List<EstadoTipoAfiliadoDTO>"] = respuesta;
            return BuscarRelacionesLaboralesWS(respuesta, id, NitEmpresa, estadoAfi, TipoVinAfi);

            /*fin consultar*/

        }

        public ActionResult BuscarRelLab(int id, string dd_estado, string dd_tipoCotizante, string dd_descargar)
        {

            if (LoginValido())
            {
                ServiceClient.EliminarParametros();
                var resultEmpEval = ServiceClient.ObtenerArrayJsonRestFul<EDTipos>(urlServicioEmpresas, CapacidadEstadoEmpleado, RestSharp.Method.GET);
                if (resultEmpEval.Count() > 0)
                {

                    edempleadoRL.lstEstados = resultEmpEval.Select(c => new SelectListItem()
                    {
                        Value = c.Id_Tipo.ToString(),
                        Text = c.Descripcion
                    }).ToList();
                }

                //List<EDTipos> lstTiposCotizantes = lnRL.DevuelveTiposCotizantes();
                ServiceClient.EliminarParametros();
                var resultTiposCotizantes = ServiceClient.ObtenerArrayJsonRestFul<EDTipos>(urlServicioEmpresas, CapacidadRelacionlaboralTipoCotizante, RestSharp.Method.GET);
                if (resultTiposCotizantes.Count() > 0)
                {
                    edempleadoRL.lstTiposCotizantes = resultTiposCotizantes.Select(c => new SelectListItem()
                    {
                        Value = c.Id_Tipo.ToString(),
                        Text = c.Descripcion
                    }).ToList();
                }


                if (dd_estado.Trim().Length == 0)
                    dd_estado = "0";

                if (dd_tipoCotizante.Trim().Length == 0)
                    dd_tipoCotizante = "0";

                if (dd_descargar.Trim().Length == 0)
                    dd_descargar = "0";
                

                var s_estado = (from x in resultEmpEval
                                where x.Id_Tipo.Equals(int.Parse(dd_estado))
                                select x).FirstOrDefault();

                string sa_estado = "";
                if (s_estado != null)
                    sa_estado = s_estado.Descripcion;
                else
                    sa_estado = "";

                Session["dd_estado"] = dd_estado;

                var s_tipoCotizante = (from x in resultTiposCotizantes
                                       where x.Id_Tipo.Equals(int.Parse(dd_tipoCotizante))
                                       select x).FirstOrDefault();

                string sa_tipoCotizante = "";
                if (s_tipoCotizante != null)
                    sa_tipoCotizante = s_tipoCotizante.Descripcion;
                else
                    sa_tipoCotizante = "";

                Session["dd_tipoCotizante"] = dd_tipoCotizante;

                ViewBag.lstEstados = edempleadoRL.lstEstados;
                ViewBag.lstTiposCotizantes = edempleadoRL.lstTiposCotizantes;
                var usuario = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
                string DocumentoEmpresa = usuario.NitEmpresa;


                if (dd_descargar == "0")
                    return View("Consultar", ConsultarWSRelacionesLaborales(1, DocumentoEmpresa, dd_estado, dd_tipoCotizante));
                else
                {
                    var resu =  DescargarRelLab(dd_estado, dd_tipoCotizante);
                    if (resu == null)
                        return View("Consultar");
                    else
                        return resu;

                }
                    
            }
            return null;

        }

        public ActionResult BuscarTerceroRelLab(int id, string dd_razonsocial, string dd_tipoTercero, string dd_descargar)
        {

            if (LoginValido())
            {
                UsuarioSessionModel usuarioSesion = new UsuarioSessionModel();
                var datosUsuario = DesEncriptar(Session["UsuarioSession"].ToString());
                var usuario1 = JsonConvert.DeserializeObject<UsuarioSessionModel>(datosUsuario);
                if (usuarioSesion != null)
                {
                    ViewBag.Documento_Empresa = usuario1.NitEmpresa;
                    ViewBag.Nombre_Empresa = usuario1.RazonSocialEmpresa;
                }

                //List<EDTipos> lstTiposTercero = lnRL.DevuelveTiposTerceros();
                ServiceClient.EliminarParametros();
                var resultTiposTercero = ServiceClient.ObtenerArrayJsonRestFul<EDTipos>(urlServicioEmpresas, CapacidadRelacionlaboralTiposTerceros, RestSharp.Method.GET);
                if (resultTiposTercero.Count() > 0)
                {
                    edempleadoRL.lstTiposTerceros = resultTiposTercero.Select(c => new SelectListItem()
                    {
                        Value = c.Id_Tipo.ToString(),
                        Text = c.Descripcion
                    }).ToList();
                }


                if (dd_tipoTercero.Trim().Length == 0)
                    dd_tipoTercero = "0";


                var s_tipoTercero = (from x in resultTiposTercero
                                     where x.Id_Tipo.Equals(int.Parse(dd_tipoTercero))
                                     select x).FirstOrDefault();

                string sa_tipoTercero = "";
                if (s_tipoTercero != null)
                    sa_tipoTercero = s_tipoTercero.Descripcion;
                else
                    sa_tipoTercero = "";

                Session["dd_tipoTercero"] = sa_tipoTercero;

                var usuario = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
                ServiceClient.EliminarParametros();
                ServiceClient.AdicionarParametro("NitEmpresaLogueada", usuario.NitEmpresa);

                var resultRazSoc = ServiceClient.ObtenerArrayJsonRestFul<EDTiposS>(urlServicioEmpresas, CapacidadRelacionlaboralRazonesSociales, RestSharp.Method.GET);
                if (resultRazSoc.Count() > 0)
                {
                    edempleadoRL.lstRazonesSociales = resultRazSoc.Select(c => new SelectListItem()
                    {
                        Value = c.Id_Tipo,
                        Text = c.Descripcion
                    }).ToList();
                }
                if (dd_razonsocial.Trim().Length == 0)
                    dd_razonsocial = "0";

                if (dd_descargar.Trim().Length == 0)
                    dd_descargar = "0";


                var s_razonsocial = (from x in resultRazSoc
                                     where x.Id_Tipo.Equals(dd_razonsocial)
                                     select x).FirstOrDefault();

                string sa_razonsocial = "";
                if (s_razonsocial != null)
                    sa_razonsocial = s_razonsocial.Descripcion;
                else
                    sa_razonsocial = "";


                Session["dd_tipoTercero"] = sa_tipoTercero;
                Session["dd_razonsocial"] = dd_razonsocial;


                ViewBag.lstTiposTerceros = edempleadoRL.lstTiposTerceros;
                ViewBag.lstRazonesSociales = edempleadoRL.lstRazonesSociales;

                string DocumentoEmpresa = usuario.NitEmpresa;
                ViewBag.NitEmpresa = usuario.NitEmpresa;
                ViewBag.RazonSocial = usuario.RazonSocialEmpresa;

                if (dd_descargar == "0")
                    return View("ConsultarTercero", BuscarTerceroRelacionesLaborales(id, DocumentoEmpresa, dd_razonsocial, sa_tipoTercero));
                else
                {
                    var resul =  DescargarTerceroRelLab(sa_tipoTercero, dd_razonsocial);
                    if (resul == null)
                        return View("ConsultarTercero");
                    else
                        return resul;
                }
            }
            return null;

        }

        public ActionResult ir_A_login()
        {
            var usuario = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            TempData["UsuarioSesion"] = usuario;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ListRelacionesLaborales(int id)
        {
            EDEmpresa_Usuaria eu = new EDEmpresa_Usuaria();
            var usuario = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            eu.DocumentoEmpresa = usuario.NitEmpresa;

            string estado = (string)Session["dd_estado"];
            string tipoCotizante = (string)Session["dd_tipoCotizante"];

            //return PartialView("ConsultaRelacionesLaborales", BuscarRelacionesLaborales(id, usuario.NitEmpresa, estado, tipoCotizante));
            return PartialView("ConsultaRelacionesLaborales", ConsultarWSRelacionesLaborales(id, eu.DocumentoEmpresa, estado, tipoCotizante));
        }

        public ActionResult ListTerceroRelacionesLaborales(int id)
        {
            EDEmpresa_Usuaria eu = new EDEmpresa_Usuaria();
            var usuario = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            eu.DocumentoEmpresa = usuario.NitEmpresa;
            ViewBag.NitEmpresa = usuario.NitEmpresa;
            ViewBag.RazonSocial = usuario.RazonSocialEmpresa;

            string tipoTercero = (string)Session["dd_tipoTercero"];
            string razonSocialNit = (string)Session["dd_razonsocial"];

            return PartialView("ConsultaTerceroRelacionesLaborales", BuscarTerceroRelacionesLaborales(id, usuario.NitEmpresa, razonSocialNit, tipoTercero));
        }

        public List<EDEmpleadoRelLab> BuscarRelacionesLaborales(int pageIndex, string DocumentoEmpresa, string estado, string tipoCotizante)
        {
            EDEmpresa_Usuaria eu = new EDEmpresa_Usuaria();
            var usuario = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            eu.DocumentoEmpresa = usuario.NitEmpresa;
            int pageCount = 0;

            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("estado", estado);
            ServiceClient.AdicionarParametro("tipoCotizante", tipoCotizante);
            ServiceClient.AdicionarParametro("DocumentoEmpresa", eu.DocumentoEmpresa);
            ServiceClient.AdicionarParametro("pageIndex", pageIndex);
            ServiceClient.AdicionarParametro("numeroRegistros", numeroRegistros);

            EDListEmpleadoRelLab edLstEmp = new EDListEmpleadoRelLab();
            var resultListEmpleadoRelLab = ServiceClient.ObtenerArrayJsonRestFul<EDListEmpleadoRelLab>(urlServicioEmpresas, CapacidadRelacionlaboralListarRelacionesLabTerceros, RestSharp.Method.GET);
            if (resultListEmpleadoRelLab.Count() > 0)
            {
                edLstEmp = resultListEmpleadoRelLab[0];
                pageCount = edLstEmp.pageCount;
                List<EDEmpleadoRelLab> lstempTer = edLstEmp.lstEmpleadoRelLab; //lnRL.ListarRelacionesLabTerceros(estado, tipoCotizante, eu.DocumentoEmpresa, pageIndex, numeroRegistros, out pageCount);
                ViewBag.PageCount = pageCount;
                ViewBag.PageIndex = pageIndex;
                return lstempTer;
            }
            else
                return null;
        }


        public List<EDEmpleadoRelLab> BuscarRelacionesLaboralesWS(List<EstadoTipoAfiliadoDTO> lstEstadoTipoAfiliadoDTO, int pageIndex, string DocumentoEmpresa, string estado, string tipoCotizante)
        {
            int pageCount = 0;
            int i = 1;
            if (pageIndex == 0)
                pageIndex = 1;

            int filaIni = (pageIndex - 1) * numeroRegistros + 1;
            int filaFin = pageIndex * numeroRegistros;
            List<EDEmpleadoRelLab> lstempTer = new List<EDEmpleadoRelLab>();
            foreach (EstadoTipoAfiliadoDTO eta in lstEstadoTipoAfiliadoDTO)
            {

                if (i >= filaIni & i <= filaFin)
                {
                    EDEmpleadoRelLab edERL = new EDEmpleadoRelLab();
                    edERL.Apellido1 = eta.apellido1;
                    edERL.Apellido2 = eta.apellido2;
                    edERL.Cargo = eta.cargo;
                    edERL.Email = eta.emailAfi;
                    edERL.FechaNacimiento = DateTime.ParseExact(eta.fecNac, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                    edERL.strFechaNacimiento = string.Format("{0}/{1}/{2}", edERL.FechaNacimiento.Day, edERL.FechaNacimiento.Month, edERL.FechaNacimiento.Year);
                    edERL.Nombre1 = eta.nombre1;
                    edERL.Nombre2 = eta.nombre2;
                    edERL.NumeroDocumento = eta.docAfi;
                    edERL.Ocupacion = eta.ocupacion;
                    edERL.TipoCotizante = eta.tipoCotizante;
                    edERL.TipoDocumento = eta.tipoDocAfi;
                    edERL.Estado = eta.estadoAfi;
                    lstempTer.Add(edERL);
                }
                i++;
            }

            int intPageCount = (int)lstEstadoTipoAfiliadoDTO.Count() / numeroRegistros;
            if (((int)lstEstadoTipoAfiliadoDTO.Count() / numeroRegistros) < lstEstadoTipoAfiliadoDTO.Count())
                ViewBag.PageCount = intPageCount + 1;
            else
                ViewBag.PageCount = intPageCount;
            ViewBag.PageIndex = pageIndex;
            return lstempTer;
        }
        public List<EDEmpleadoTercero> BuscarTerceroRelacionesLaborales(int pageIndex, string DocumentoEmpresa, string razonSocialNit, string tipoTercero)
        {
            EDEmpresa_Usuaria eu = new EDEmpresa_Usuaria();
            var usuario = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            eu.DocumentoEmpresa = usuario.NitEmpresa;
            int pageCount = 0;

            ServiceClient.EliminarParametros();
            ServiceClient.AdicionarParametro("razonSocialnit", (razonSocialNit == null) ? "" : razonSocialNit);
            ServiceClient.AdicionarParametro("tipoTercero", (tipoTercero == null) ? "" : tipoTercero);
            ServiceClient.AdicionarParametro("DocumentoEmpresa", eu.DocumentoEmpresa);
            ServiceClient.AdicionarParametro("pageIndex", pageIndex);
            ServiceClient.AdicionarParametro("numeroRegistros", numeroRegistros);

            
            var resultListEmpleadoRelLab = ServiceClient.ObtenerArrayJsonRestFul<EDEmpleadoTercero>(urlServicioEmpresas, CapacidadRelacionlaboralListarRelacionesLaborales, RestSharp.Method.GET);
            if (resultListEmpleadoRelLab != null)
            {
                if (resultListEmpleadoRelLab.Count() > 0)
                {
                    if (resultListEmpleadoRelLab[0] != null)
                    {
                        EDEmpleadoTercero edEmpTer = new EDEmpleadoTercero();
                        edEmpTer = resultListEmpleadoRelLab[0];
                        pageCount = edEmpTer.PageCount;
                        ViewBag.PageCount = pageCount;
                        ViewBag.PageIndex = pageIndex;

                        var lstEmpleadoTer = resultListEmpleadoRelLab.Select(c => new EDEmpleadoTercero()
                        {
                            Apellido1 = c.Apellido1,
                            TipoDocumento = c.TipoDocumento,
                            Apellido2 = c.Apellido2,
                            Email = c.Email,
                            FechaNacimiento = c.FechaNacimiento,
                            strFechaNacimiento = string.Format("{0}/{1}/{2}", c.FechaNacimiento.Day, c.FechaNacimiento.Month, c.FechaNacimiento.Year),
                            Nombre1 = c.Nombre1,
                            Nombre2 = c.Nombre2,
                            Cargo_Empl = c.Cargo_Empl,
                            Departamento = c.Departamento,
                            Direccion = c.Direccion,
                            Email_Empl = c.Email_Empl,
                            Fecha_ingreso_empresa = c.Fecha_ingreso_empresa,
                            Genero = c.Genero,
                            ID_Empleado = c.ID_Empleado,
                            Municipio = c.Municipio,
                            Numero_Documento_Empl = c.Numero_Documento_Empl,
                            Ocupacion_Empl = c.Ocupacion_Empl,
                            PK_Nit_Empresa = c.PK_Nit_Empresa,
                            RazonSocial = c.RazonSocial,
                            RelacionesLaboralesTercero = c.RelacionesLaboralesTercero,
                            Telefono = c.Telefono,
                            Zona = c.Zona
                        }).ToList();

                        return lstEmpleadoTer;
                    }
                    else
                        return null;
                }else
                    return null;
            }
            else
                return null;
        }

        private System.Data.DataTable RelacionesLaboralesWSDT(List<EstadoTipoAfiliadoDTO> lstEstadoTipoAfiliadoDTO)
        {

            System.Data.DataTable dt = lnRL.TablaEmpleados();

            List<EDEmpleadoRelLab> lstempTer = new List<EDEmpleadoRelLab>();
            foreach (EstadoTipoAfiliadoDTO eta in lstEstadoTipoAfiliadoDTO)
            {
                dt.Rows.Add(eta.tipoDocAfi, eta.docAfi, eta.apellido1, eta.apellido2, eta.nombre1, eta.nombre2, eta.fecNac, eta.estadoAfi, eta.ocupacion, eta.cargo, eta.emailAfi);
            }
            return dt;
        }

        public FileResult ExportarExcel(string dd_Estados, string dd_TiposCotizantes)
        {
            /*
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
                    return File(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "RecursosAsignados.xlsx");
                }
            }
            */
            return null;
        }

        private static List<EDRelacionesLaborales> ReadFileCVS(string strfileName, int IDEmpresa)
        {


            var resp = System.IO.File.ReadLines(strfileName)
                           .Skip(1)
                           .Where(s => s != "")
                           .Select(s => s.Split(new[] { ConfigurationManager.AppSettings["CVS_Separador"].ToString()[0] }))
                           .Select(a => new EDRelacionesLaborales
                           {
                               TipoTercero = a[0],
                               NitEmpresa = a[1],
                               RazonSocial = a[2],
                               TipoDocumento = a[3],
                               NumeroDocumento = a[4],
                               Apellido1 = a[5],
                               Apellido2 = a[6],
                               Nombre1 = a[7],
                               Nombre2 = a[8],
                               FechaNacimiento = a[9],
                               Ocupacion = a[10],
                               Cargo = a[11],
                               Email = a[12],
                               idEmpresa = IDEmpresa.ToString()
                           }).ToList();
            return resp;
        }

    }

}