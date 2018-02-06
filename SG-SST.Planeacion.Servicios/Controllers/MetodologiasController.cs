using System;
using System.Net.Http;
using System.Web.Http;
using System.Net;
using SG_SST.Logica.Planificacion;
using SG_SST.EntidadesDominio.Planificacion;


namespace SG_SST.Planeacion.Servicios.Controllers
{
    public class MetodologiasController : ApiController
    {
        #region aplicacion
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
                var response = Request.CreateResponse(HttpStatusCode.InternalServerError,ex.ToString());
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
                var response = Request.CreateResponse(HttpStatusCode.InternalServerError,ex.ToString());
                return response;
            }
        }

        [HttpGet]
        [ActionName("niveles-de-exposicion")]
        public HttpResponseMessage ObtenerNivelesDeExposicion()
        {
            try
            {
                var logica = new LNMetodologia();
                var result = logica.ObtenerNivelesDeExposicion();
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

        [HttpGet]
        [ActionName("consecuencias")]
        public HttpResponseMessage ObtenerConsecuencias(int PK_TipoMedologia)
        {
            try
            {
                var logica = new LNMetodologia();
                var result = logica.ObtenerConsecuencias(PK_TipoMedologia);
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

        [HttpGet]
        [ActionName("consecuencias-grupo")]
        public HttpResponseMessage ObtenerConsecuenciasPorGrupo(int PK_Grupo)
        {
            try
            {
                var logica = new LNMetodologia();
                var result = logica.ObtenerConsecuenciasPorGrupo(PK_Grupo);
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

        [HttpGet]
        [ActionName("probabilidades")]
        public HttpResponseMessage ObtenerProbabilidades(int PK_TipoMedologia)
        {
            try
            {
                var logica = new LNMetodologia();
                var result = logica.ObtenerProbabilidades(PK_TipoMedologia);
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

        [HttpGet]
        [ActionName("niveles-de-deficiencia")]
        public HttpResponseMessage ObtenerNivelesDeDeficiencia(bool FLAG_Cuantitativa)
        {
            try
            {
                var logica = new LNMetodologia();
                var result = logica.ObtenerNivelesDeDeficiencia(FLAG_Cuantitativa);
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
        #endregion

        #region app
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
                var response = Request.CreateResponse(HttpStatusCode.InternalServerError,ex.ToString());
                return response;
            }
        }
        [HttpGet]
        [ActionName("peligros-identificados-filtro-ED")]
        public HttpResponseMessage ObtenerPeligrosIdentificadosFiltroAppp(int id_Sede, int idMetodologia, int id_Proceso = 0, string zonaLugar="", string actividad="") 
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
                var result = logica.ObtenerPeligrosIdentificadosFiltroApp(id_Sede, idMetodologia, id_Proceso, zonaLugar,actividad);
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
                var response = Request.CreateResponse(HttpStatusCode.InternalServerError,ex.ToString());
                return response;
            }
        }

        [HttpGet]
        [ActionName("valoracion-de-riesgos")]
        public HttpResponseMessage ValoracionDeRiesgosApp(int id_Sede, int idMetodologia,int idTipoPeligro)
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
                var response = Request.CreateResponse(HttpStatusCode.InternalServerError,ex.ToString());
                return response;
            }
        }

        [HttpGet]
        [ActionName("detalle-valoracion-de-riesgos")]
        public HttpResponseMessage DetalleValoracionDeRiesgosApp(int id_Sede, int idMetodologia, int idTipoPeligro,string intepretacionRiesgo)
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
                var response = Request.CreateResponse(HttpStatusCode.InternalServerError,ex.ToString());
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
                var response = Request.CreateResponse(HttpStatusCode.InternalServerError,ex.ToString());
                return response;
            }
        }

        [HttpGet]
        [ActionName("zona-lugar")]
        public HttpResponseMessage ZonLuagarMetodologiaApp(int id_Sede, int idMetodologia,int id_Proceso)
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
                var response = Request.CreateResponse(HttpStatusCode.InternalServerError,ex.ToString());
                return response;
            }
        }

        [HttpGet]
        [ActionName("actividades-metodologia")]
        public HttpResponseMessage ActividadMetodologiaApp(int id_Sede, int idMetodologia, int id_Proceso,string zonaLugar)
        {
            try
            {
                var logica = new LNMetodologia();
                var result = logica.ActividadMetodologiaApp(id_Sede, idMetodologia, id_Proceso,zonaLugar);
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
                var response = Request.CreateResponse(HttpStatusCode.InternalServerError,ex.ToString());
                return response;
            }
        }

        #endregion
    }
}