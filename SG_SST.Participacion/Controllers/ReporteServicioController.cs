using SG_SST.EntidadesDominio.Participacion;
using SG_SST.Logica.Participacion;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using SG_SST.EntidadesDominio.Empresas;
using SG_SST.Logica.Empresas;
using SG_SST.Logica.Planificacion;
namespace SG_SST.Participacion.Controllers
{


    public class ReporteServicioController : ApiController
    {
        string rutaImagenesReportesCI = ConfigurationManager.AppSettings["rutaImagenesReportesCI"];
        string afiliadoempresaactivo = ConfigurationManager.AppSettings["afiliadoempresaactivo"];

        [HttpPost]
        [ActionName("Crear-Reporte")]
        public HttpResponseMessage GrabarReporte(EDReporte Reporte)
        {
            try
            {

                LNReporte logica = new LNReporte();
                var resultado = logica.GuardarReporte(Reporte);


                if (resultado != null)
                {
                    var response = Request.CreateResponse<EDReporte>(HttpStatusCode.Created, resultado);

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

                var response = Request.CreateResponse(HttpStatusCode.InternalServerError);
                return response;

            }
        }


        // Guardar reporte editado
        [HttpPost]
        [ActionName("Guardar-Reporte-Editado")]
        public HttpResponseMessage GrabarReporteEditado(EDReporte Reporte)
        {
            try
            {

                LNReporte logica = new LNReporte();
                var resultado = logica.GuardarReporteEditado(Reporte);


                if (resultado != null)
                {
                    var response = Request.CreateResponse<EDReporte>(HttpStatusCode.Created, resultado);

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

                var response = Request.CreateResponse(HttpStatusCode.InternalServerError);
                return response;

            }
        }





        //Descargar reporte en excel por identificacion 

        [HttpGet]
        [ActionName("descargar-reporteExcel-id")]
        public HttpResponseMessage ObtenerRepExcelPorReporte(int id)
        {
            HttpResponseMessage response = null;
            try
            {
                var logica = new LNReporte();
                var archivo = logica.ObtenerRepExcelPorReporte(id);
                if (archivo != null)
                {
                    response = Request.CreateResponse<byte[]>(HttpStatusCode.OK, archivo);
                    return response;
                }
                else
                {
                    response = Request.CreateResponse(HttpStatusCode.ExpectationFailed);
                    return response;
                }
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.InternalServerError);
                return response;
            }
        }

        //descargar reportes de la consulta

        [HttpPost]
        [ActionName("descargar-reportesExcel")]
        public HttpResponseMessage ObtenerReporteExcel(List<EDReporte> resultReporte, List<EDActividadesActosInseguros> resultActividades)
        {
            HttpResponseMessage response = null;
            try
            {
                var logica = new LNReporte();
                var archivo = logica.ObtenerReporteExcel(resultReporte, resultActividades);
                if (archivo != null)
                {
                    response = Request.CreateResponse<byte[]>(HttpStatusCode.OK, archivo);
                    return response;
                }
                else
                {
                    response = Request.CreateResponse(HttpStatusCode.ExpectationFailed);
                    return response;
                }
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.InternalServerError);
                return response;
            }
        }


        //Mostrar los reporte filtrados por empresas

        [HttpGet]
        [ActionName("visualizar-reporte-empresa")]

        public HttpResponseMessage ObtenerReportesPorEmpresa(string nit)
        {

            try
            {
                var logica = new LNReporte();
                var result = logica.ObtenerReportesPorEmpresa(nit);
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
                var response = Request.CreateResponse(HttpStatusCode.InternalServerError);
                return response;
            }
        }

        //Descargar reporte pdf por id
        [HttpGet]
        [ActionName("descargar-reportePDF-id")]

        public HttpResponseMessage ObtenerReporteCondicionesInsegurasPorID(int id)
        {

            try
            {
                var logica = new LNReporte();
                var result = logica.ObtenerReporteCondicionesInsegurasPorID(id);
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
                var response = Request.CreateResponse(HttpStatusCode.InternalServerError);
                return response;
            }
        }



        // Cargue los datos de las imagenes
        [HttpGet]
        [ActionName("visualizar-Imaganes")]

        public HttpResponseMessage ObtenerImagenesCondicionesInsegurasPorID(int id)
        {

            try
            {
                var logica = new LNReporte();
                var result = logica.ObtenerImagenesCondicionesInsegurasPorID(id);
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
                var response = Request.CreateResponse(HttpStatusCode.InternalServerError);
                return response;
            }
        }

        // Cargue los datos de las actividades
        [HttpGet]
        [ActionName("visualizar-Actividades")]

        public HttpResponseMessage ObtenerActividadesCondicionesInsegurasPorID(int id)
        {

            try
            {
                var logica = new LNReporte();
                var result = logica.ObtenerActividadesCondicionesInsegurasPorID(id);
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
                var response = Request.CreateResponse(HttpStatusCode.InternalServerError);
                return response;
            }
        }


        private static Image RedimensionarImagen(Stream stream)
        {
            // Se crea un objeto Image, que contiene las propiedades de la imagen
            Image img = Image.FromStream(stream);
            int newH = 300;
            int newW = 300;
            // Si las dimensiones cambiaron, se modifica la imagen
            Bitmap newImg = new Bitmap(img, newW, newH);
            Graphics g = Graphics.FromImage(newImg);
            g.InterpolationMode =
                System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
            g.DrawImage(img, 0, 0, newImg.Width, newImg.Height);
            return newImg;
        }

        // obtienede de los reportes busqueda
        [HttpPost]
        [ActionName("obtener-reportes-busqueda")]

        public HttpResponseMessage ObteneReportesPorBusqueda(EDReporte reporte)
        {

            try
            {
                var logica = new LNReporte();
                var result = logica.ObteneReportesPorBusqueda(reporte);
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
                var response = Request.CreateResponse(HttpStatusCode.InternalServerError);
                return response;
            }
        }


        // obtiene las actividades busqueda
        [HttpPost]
        [ActionName("obtener-actividades-busqueda")]

        public HttpResponseMessage ObtenerActividadesPorBusqueda(EDReporte reporte)
        {

            try
            {
                var logica = new LNReporte();
                var result = logica.ObtenerActividadesPorBusqueda(reporte);
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
                var response = Request.CreateResponse(HttpStatusCode.InternalServerError);
                return response;
            }
        }

        //Eliminar imagenes

        [HttpDelete]
        [ActionName("Eliminar_Img_Rep")]
        public HttpResponseMessage ELiminarImagenReporte(int idImagen)
        {
            try
            {
                var logica = new LNReporte();
                bool result = logica.ELiminarImagenReporte(idImagen);
                if (result)
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
                //RegistroInformacion.EnviarError<DxDeCondicionSaludController>(ex.Message);
                var response = Request.CreateResponse(HttpStatusCode.InternalServerError);
                return response;
            }
        }

        [HttpGet]
        [ActionName("Consultar-imagen-id")]
        public HttpResponseMessage ObtenerImagen(int idImagen)
        {
            try
            {
                var logica = new LNReporte();
                var result = logica.ObtenerImagen(idImagen);
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
                //RegistroInformacion.EnviarError<DxDeCondicionSaludController>(ex.Message);
                var response = Request.CreateResponse(HttpStatusCode.InternalServerError);
                return response;
            }
        }

        #region app jorge

        // APP tipo de reporte
        [HttpGet]
        [ActionName("tipo-reporte")]
        public HttpResponseMessage ConsultarTipoReporte()
        {
            try
            {
                var logica = new LNReporte();
                var result = logica.ConsultarTipoReporte();
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
                var response = Request.CreateResponse(HttpStatusCode.InternalServerError,ex.ToString());
                return response;
            }
        }

     



        [HttpPost]
        [ActionName("guardar-reporte-app")]
        public HttpResponseMessage GuardarReporte(EDReporteApp reporte)
        {
            HttpResponseMessage response = null;




            try
            {
                LNReporte logica = new LNReporte();
            
                var rutaImagen = "";
              
                rutaImagen = "APP"+ Guid.NewGuid()+ ".jpeg";

                reporte.nombreImagen = rutaImagen;
                reporte.fechaEvento = Convert.ToDateTime(reporte.fechaOcurrencia);
   
                var resultado = logica.GuardarReporteAPP(reporte);

                if (resultado != null)
                {
                    response = Request.CreateResponse<EDReporteApp>(HttpStatusCode.Created, resultado);
                    
                    if(reporte.imagen!=null)
                    {

                        byte[] imageBytes = Convert.FromBase64String(reporte.imagen);
                        MemoryStream ms = new MemoryStream(imageBytes, 0,
                          imageBytes.Length);

                        // Convert byte[] to Image
                        ms.Write(imageBytes, 0, imageBytes.Length);
                        Image image = Image.FromStream(ms, true);
                        var path = "";
                        var ruta = rutaImagenesReportesCI + reporte.nitEmpresa;


                        path = Path.Combine((ruta), rutaImagen);

                        if (!Directory.Exists(ruta))
                        {
                            System.IO.Directory.CreateDirectory((ruta));
                        }


                        image.Save(path);
                    }
                    
                    return response;
                }
                else
                {
                    response = Request.CreateResponse(HttpStatusCode.ExpectationFailed);
                    return response;
                }
            }

            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, ex.ToString());
                return response;
            }
        }




        [HttpPost]
        [ActionName("guardar-incidente-app")]
        public HttpResponseMessage GuardarIncidenteApp(EDIncidenteAPP incidente)
        {
            HttpResponseMessage response = null;
            try
            {
                  incidente.Incidente_fecha = Convert.ToDateTime(incidente.strIncidente_fecha);
                    incidente.afiliadoempresaactivo = afiliadoempresaactivo;
            }
            catch(Exception e)
            {
                response = Request.CreateResponse("Error de web config" + e.ToString());
                return response;
            }

            try{
                  LNIncidenteApp logica = new LNIncidenteApp();
                EDIncidenteAPP inicente = logica.GuardarIncidenteAPP(incidente);
                if (inicente != null)
                {
                    response = Request.CreateResponse<EDIncidenteAPP>(HttpStatusCode.Created, inicente);
                    return response;
                }
                else
                {
                    response = Request.CreateResponse(HttpStatusCode.ExpectationFailed);
                    return response;
                }
            }
  
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.InternalServerError,ex.ToString());
                return response;
            }
        }







        [HttpGet]
        [ActionName("autentificar_app")]
        public HttpResponseMessage AutentificarAPP(string nitEmpresa,string documentoEmpleado)
        {
            try
            {
                var logica = new LNAutentificacionAPP();
                var result = logica.AutentificarAPP(nitEmpresa, documentoEmpleado, ConfigurationManager.AppSettings["afiliadoempresaactivo"], ConfigurationManager.AppSettings["consultaAfiliadoEmpresaActivo"]);
                if (result !=null)
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
                //RegistroInformacion.EnviarError<DxDeCondicionSaludController>(ex.Message);
                var response = Request.CreateResponse(HttpStatusCode.InternalServerError);
                return response;
            }
        }

        #endregion

        #region app Cristian


        [HttpGet]
        [ActionName("peligros-identificados")]
        public HttpResponseMessage ObtenerPeligrosIdentificadosApp(int id_Sede, int idMetodologia)
        {
            try
            {

                var logica = new LNMetodologia();
                var result = logica.ObtenerPeligrosIdentificadosApp(id_Sede, idMetodologia);
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
                // var error = new RegistraLog();
                // error.RegistrarError(typeof(MetodologiasController), ex.StackTrace, ex);
                var response = Request.CreateResponse(HttpStatusCode.InternalServerError, ex.ToString());
                return response;
            }
        }
        [HttpGet]
        [ActionName("peligros-identificados-filtro-ED")]
        public HttpResponseMessage ObtenerPeligrosIdentificadosFiltroAppp(int id_Sede, int idMetodologia, int id_Proceso = 0, string zonaLugar = "", string actividad = "")
        {

            var response = Request.CreateResponse(HttpStatusCode.InternalServerError);
            return response;
        }

        [HttpGet]
        [ActionName("peligros-identificados-filtro")]
        public HttpResponseMessage ObtenerPeligrosIdentificadosFiltroApp(int id_Sede, int idMetodologia, int id_Proceso = 0, string zonaLugar = "", string actividad = "")
        {
            try
            {
                var logica = new LNMetodologia();
                var result = logica.ObtenerPeligrosIdentificadosFiltroApp(id_Sede, idMetodologia, id_Proceso, zonaLugar, actividad);
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
                // var error = new RegistraLog();
                // error.RegistrarError(typeof(MetodologiasController), ex.StackTrace, ex);
                var response = Request.CreateResponse(HttpStatusCode.InternalServerError, ex.ToString());
                return response;
            }
        }

        [HttpGet]
        [ActionName("valoracion-de-riesgos")]
        public HttpResponseMessage ValoracionDeRiesgosApp(int id_Sede, int idMetodologia, int idTipoPeligro)
        {
            try
            {
                var logica = new LNMetodologia();
                var result = logica.ValoracionDeRiesgosApp(id_Sede, idMetodologia, idTipoPeligro);
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
                // var error = new RegistraLog();
                // error.RegistrarError(typeof(MetodologiasController), ex.StackTrace, ex);
                var response = Request.CreateResponse(HttpStatusCode.InternalServerError, ex.ToString());
                return response;
            }
        }

        [HttpGet]
        [ActionName("detalle-valoracion-de-riesgos")]
        public HttpResponseMessage DetalleValoracionDeRiesgosApp(int id_Sede, int idMetodologia, int idTipoPeligro, string intepretacionRiesgo)
        {
            try
            {
                var logica = new LNMetodologia();
                var result = logica.DetalleValoracionDeRiesgosApp(id_Sede, idMetodologia, idTipoPeligro, intepretacionRiesgo);
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
                // var error = new RegistraLog();
                // error.RegistrarError(typeof(MetodologiasController), ex.StackTrace, ex);
                var response = Request.CreateResponse(HttpStatusCode.InternalServerError, ex.ToString());
                return response;
            }
        }

        [HttpGet]
        [ActionName("procesos-metodologia")]
        public HttpResponseMessage ProcesosMetodologiaApp(int id_Sede, int idMetodologia)
        {
            try
            {
                var logica = new LNMetodologia();
                var result = logica.ProcesosMetodologiaApp(id_Sede, idMetodologia);
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
                // var error = new RegistraLog();
                // error.RegistrarError(typeof(MetodologiasController), ex.StackTrace, ex);
                var response = Request.CreateResponse(HttpStatusCode.InternalServerError, ex.ToString());
                return response;
            }
        }

        [HttpGet]
        [ActionName("zona-lugar")]
        public HttpResponseMessage ZonLuagarMetodologiaApp(int id_Sede, int idMetodologia, int id_Proceso)
        {
            try
            {
                var logica = new LNMetodologia();
                var result = logica.ZonLuagarMetodologiaApp(id_Sede, idMetodologia, id_Proceso);
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
                // var error = new RegistraLog();
                // error.RegistrarError(typeof(MetodologiasController), ex.StackTrace, ex);
                var response = Request.CreateResponse(HttpStatusCode.InternalServerError, ex.ToString());
                return response;
            }
        }

        [HttpGet]
        [ActionName("actividades-metodologia")]
        public HttpResponseMessage ActividadMetodologiaApp(int id_Sede, int idMetodologia, int id_Proceso, string zonaLugar)
        {
            try
            {
                var logica = new LNMetodologia();
                var result = logica.ActividadMetodologiaApp(id_Sede, idMetodologia, id_Proceso, zonaLugar);
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
                // var error = new RegistraLog();
                // error.RegistrarError(typeof(MetodologiasController), ex.StackTrace, ex);
                var response = Request.CreateResponse(HttpStatusCode.InternalServerError, ex.ToString());
                return response;
            }
        }

        [HttpGet]
        [ActionName("metodologiasPorSede")]
        public HttpResponseMessage obtenerMetodologias(int id_Sede)
        {
            try
            {
                var logica = new LNMetodologia();
                var result = logica.ObtenerMedologias(id_Sede);
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
                // var error = new RegistraLog();
                // error.RegistrarError(typeof(MetodologiasController), ex.StackTrace, ex);
                var response = Request.CreateResponse(HttpStatusCode.InternalServerError, ex.ToString());
                return response;
            }
        }


        [HttpGet]
        [ActionName("metodologias")]
        public HttpResponseMessage obtenerMetodologias()
        {
            try
            {
                var logica = new LNMetodologia();
                var result = logica.ObtenerMedologias();
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
                // var error = new RegistraLog();
                // error.RegistrarError(typeof(MetodologiasController), ex.StackTrace, ex);
                var response = Request.CreateResponse(HttpStatusCode.InternalServerError);
                return response;
            }
        }


        [HttpGet]
        [ActionName("tipos-de-peligro")]
        public HttpResponseMessage ObtenerTiposDePeligro()
        {
            try
            {
                var logica = new LNMetodologia();
                var result = logica.ObtenerTiposDePeligro();
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


        #endregion


        

    }
}
