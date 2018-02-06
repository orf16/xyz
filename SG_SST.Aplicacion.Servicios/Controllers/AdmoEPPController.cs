using SG_SST.EntidadesDominio.Aplicacion;
using SG_SST.Logica;
using SG_SST.Aplicacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SG_SST.Logica.Aplicacion;
using SG_SST.Audotoria;
using System.Configuration;
using System.IO;
using System.Net.Http.Headers;
using System.Drawing;
using System.Web;
using SG_SST.EntidadesDominio.Planificacion;
using SG_SST.Logica.Planificacion;

namespace SG_SST.Aplicacion.Servicios.Controllers
{
    public class AdmoEPPController : ApiController
    {
        private static string urlImages = "https://alissta.gov.co";
        private static string UrlQueryAfiliado = "wssst/afiliado?";
        private static string UrlQueryBase = "http://190.26.216.109:8080";

        [HttpGet]
        [ActionName("Obtener-ListaEPP")]
        public HttpResponseMessage ConsultarListaEPP(string Documento, string Nit)
        {
            LNPeligro LNPeligro = new LNPeligro();
            
            try
            {
                var logica = new LNEPP();
                var result = logica.ConsultaMatrizEppUsuario(Documento, Nit, UrlQueryBase, UrlQueryAfiliado);
                if (result != null)
                {
                    foreach (var item in result)
                    {
                        List<EDTipoDePeligro> ListaTipoPeligros = logica.ObtenerTiposDePeligro();
                        List<EDClasificacionDePeligro> ListaClasPeligros = new List<EDClasificacionDePeligro>();
                        if (ListaTipoPeligros != null)
                        {
                            foreach (var item1 in ListaTipoPeligros)
                            {
                                string DescripcionTipo = item1.Descripcion_Del_Peligro;
                                List<EDClasificacionDePeligro> ListaClasPeligro = new List<EDClasificacionDePeligro>();
                                ListaClasPeligro = LNPeligro.ObtenerClasificaciónPorTipo(item1.PK_Tipo_De_Peligro);
                                foreach (var item2 in ListaClasPeligro)
                                {
                                    string DescripcionClasePeligro = DescripcionTipo + " - " + item2.DescripcionClaseDePeligro;
                                    item2.DescripcionClaseDePeligro = DescripcionClasePeligro;
                                    ListaClasPeligros.Add(item2);
                                }
                            }
                        }
                        var peligro = (from pel in ListaClasPeligros
                                       where pel.IdClasificacionDePeligro == item.Fk_Id_Clasificacion_De_Peligro
                                       select pel.DescripcionClaseDePeligro).FirstOrDefault();
                        if (peligro != null)
                        {
                            item.Clasificacion_De_Peligro = peligro;
                        }
                    }
                    var response = Request.CreateResponse(HttpStatusCode.OK, result);
                    return response;
                }
                else
                {
                    var response = Request.CreateResponse(HttpStatusCode.ExpectationFailed);
                    return response;
                }
            }
            catch (Exception ex)
            {
                try
                {
                    var logica = new LNEPP();
                    var resultex = logica.ConsultaMatrizEppCargo2(Nit);
                    if (resultex != null)
                    {
                        var responseex = Request.CreateResponse(HttpStatusCode.OK, resultex);
                        return responseex;
                    }
                    else
                    {
                        var responseex = Request.CreateResponse(HttpStatusCode.ExpectationFailed);
                        return responseex;
                    }
                }
                catch (Exception ex1)
                {
                    var response = Request.CreateResponse(HttpStatusCode.InternalServerError, ex1.ToString());
                    return response;
                }
                
                
            }
            
        }
        [HttpGet]
        [ActionName("Obtener-ImagenEPP")]
        public HttpResponseMessage ObtenerImagen(int IdEPP, string Nit)
        {
            string baseUrl = "";
            try
            {
                var logica = new LNEPP();
                var resultepp = logica.ConsultarEPPAPP(IdEPP, Nit);
                if (resultepp.RutaImage == null || resultepp.ArchivoImagen == null)
                {
                    var response = Request.CreateResponse(HttpStatusCode.ExpectationFailed);
                    return response;
                }
                string RutaImagen = resultepp.RutaImage;
                baseUrl = urlImages;
                if (RutaImagen!=null)
                {
                    if (RutaImagen.Contains("~/"))
                    {
                        RutaImagen = resultepp.RutaImage.Replace("~", "");
                    }
                }
                var mappedPath = Path.Combine(RutaImagen, resultepp.ArchivoImagen);
                baseUrl = baseUrl + mappedPath;

                using (var wc = new System.Net.WebClient())
                {
                    try
                    {
                        byte[] bytes = wc.DownloadData(baseUrl);
                        if (bytes != null)
                        {
                            MemoryStream ms = new MemoryStream(bytes);
                            System.Drawing.Image img = System.Drawing.Image.FromStream(ms);

                            using (MemoryStream m = new MemoryStream())
                            {
                                img.Save(m, System.Drawing.Imaging.ImageFormat.Jpeg);
                                HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
                                result.Content = new ByteArrayContent(m.ToArray());
                                result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpg");
                                ms.Dispose();
                                return result;
                            }
                        }
                        else
                        {
                            var response = Request.CreateResponse(HttpStatusCode.ExpectationFailed);
                            return response;
                        }
                    }
                    catch (Exception)
                    {
                        var response = Request.CreateResponse(HttpStatusCode.ExpectationFailed);
                        return response;
                    }
                }
            }
            catch (Exception ex)
            {
                var response = Request.CreateResponse(HttpStatusCode.InternalServerError, ex.ToString()+"Especifico: url: "+ baseUrl);
                return response;
            }

        }
        [HttpGet]
        [ActionName("Obtener-ListaEPP-test")]
        public HttpResponseMessage ConsultarListaEPPtest(string Documento, string Nit)
        {
            try
            {
                var logica = new LNEPP();

                var result = logica.pruebaservicio(Documento, Nit, UrlQueryBase, UrlQueryAfiliado);
                if (result != null)
                {
                    var response = Request.CreateResponse(HttpStatusCode.OK, result);
                    return response;
                }
                else
                {
                    var response = Request.CreateResponse(HttpStatusCode.ExpectationFailed);
                    return response;
                }
            }
            catch (Exception ex)
            {
                var response = Request.CreateResponse(HttpStatusCode.InternalServerError, ex.ToString());
                return response;
            }
        }
        [HttpGet]
        [ActionName("Obtener-ListaEPP-test1")]
        public HttpResponseMessage ConsultarListaEPPtest1(string Documento, string Nit)
        {
            try
            {
                var logica = new LNEPP();

                var result = logica.pruebaservicio1(Documento, Nit, UrlQueryBase, UrlQueryAfiliado);
                if (result != null)
                {
                    var response = Request.CreateResponse(HttpStatusCode.OK, result);
                    return response;
                }
                else
                {
                    var response = Request.CreateResponse(HttpStatusCode.ExpectationFailed);
                    return response;
                }
            }
            catch (Exception ex)
            {
                var response = Request.CreateResponse(HttpStatusCode.InternalServerError, ex.ToString());
                return response;
            }
        }

    }
}
