
using SG_SST.EntidadesDominio.Planificacion;
using SG_SST.Logica.Empresas;
using SG_SST.Logica.Planificacion;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SG_SST.Planeacion.Servicios.Controllers
{
    public class EvaluacionController : ApiController
    {
        #region Evaluacion Inicial
        

        /// <summary>
        /// guarda la informacion de la evaluacion inicial de una empresa
        /// </summary>
        /// <param name="Empresa"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("inicial")]
        public HttpResponseMessage GuardarEvaluacionInicial(EDEmpresaEvaluar Empresa)
        {
            HttpResponseMessage response = null;
            try
            {
                LNEvaluacionInicial logica = new LNEvaluacionInicial();
                EDEmpresaEvaluar empresa = logica.CrearEmpresaEvaluar(Empresa);
                if (empresa != null)
                {
                    response = Request.CreateResponse<EDEmpresaEvaluar>(HttpStatusCode.Created, empresa);
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

        [HttpGet]
        [ActionName("aspectos-base")]
        public HttpResponseMessage ObtenerAspectosBase()
        {
            HttpResponseMessage response = null;
            try
            {
                LNEvaluacionInicial logica = new LNEvaluacionInicial();
                var AspectosBase = logica.ObtenerAspectosBase();
                if (AspectosBase != null)
                {
                    response = Request.CreateResponse(HttpStatusCode.OK, AspectosBase);
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
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
                return response;
            }
        }

        #endregion

        #region Evaluacion Estandares Minimos

        /// <summary>
        /// Obtiene la lista de los ciclos a evaluar
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("estandares-minimos")]
        public HttpResponseMessage ObtenerCiclos(string NIT)
        {
            HttpResponseMessage response = null;
            try
            {
                LNEvaluacionStandMinimos logica = new LNEvaluacionStandMinimos();
                var Ciclos = logica.ObtenerCiclos(NIT);
                if (Ciclos != null)
                {
                    response = Request.CreateResponse(HttpStatusCode.OK, Ciclos);
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
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
                return response;
            }
        }


        [HttpGet]
        [ActionName("Obtener-Empresa-Evaluar")]
        public HttpResponseMessage ObtenerEmpresaEvaluar(string Nit, string Responsable)
        {
            try
            {
                var logica = new LNEmpresa();
                var result = logica.ObtenerDatosEmpresaEvaluar(Nit, Responsable);
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

        /// <summary>
        /// Obtiene la lista de los ciclos a evaluar
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("estandares-minimos")]
        public HttpResponseMessage ObtenerEstandarPorCiclo(int idCiclo, string NIT)
        {
            HttpResponseMessage response = null;
            try
            {
                LNEvaluacionStandMinimos logica = new LNEvaluacionStandMinimos();
                var Estandares = logica.ObtenerEstandaresPorCiclo(idCiclo, NIT);
                if (Estandares != null)
                {
                    response = Request.CreateResponse<EDCiclo>(HttpStatusCode.OK, Estandares);
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

        [HttpPost]
        [ActionName("estandares-minimos")]
        public HttpResponseMessage GuardarEvaluacionEstandares(EDEvaluacionEstandarMinimo EvaluacionEstandar)
        {
            HttpResponseMessage response = null;
            try
            {
                LNEvaluacionStandMinimos logica = new LNEvaluacionStandMinimos();
                var result = logica.GuardarEvaluacionEstandar(EvaluacionEstandar);

                response = Request.CreateResponse<EDCiclo>(HttpStatusCode.Created, result);
                return response;

            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.InternalServerError);
                return response;
            }
        }

        [HttpGet]
        [ActionName("calificacion-estandares-minimos")]
        public HttpResponseMessage ObtenerCalificacionFinalEstandares(string NIT)
        {
            HttpResponseMessage response = null;
            try
            {
                LNEvaluacionStandMinimos logica = new LNEvaluacionStandMinimos();
                var calificacion = logica.ObtenerCalificacionFinal(NIT);
                if (calificacion != null)
                {
                    response = Request.CreateResponse<EDValoracionFinal>(HttpStatusCode.OK, calificacion);
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


        [HttpGet]
        [ActionName("estandares-actividades")]
        public HttpResponseMessage ObtenerActividades(string NIT)
        {
            HttpResponseMessage response = null;
            try
            {
                LNEvaluacionStandMinimos logica = new LNEvaluacionStandMinimos();
                var actividades = logica.ObtenerActividades(NIT);
                if (actividades != null)
                {
                    response = Request.CreateResponse(HttpStatusCode.OK, actividades);
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


        #endregion
    }
}
