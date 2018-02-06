using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SG_SST.Logica.MedicionEvaluacion;
using SG_SST.EntidadesDominio.MedicionEvaluacion;
using SG_SST.Models.MedicionEvaluacion;

namespace SG_SST.MedicionEvaluacion.Servicios.Controllers
{
    public class PlanAccionServicioController : ApiController
    {

            [HttpGet]
            [ActionName("consultar-planes-accion")]
            public HttpResponseMessage ConsultarPlanesDeAccion(int nit)
            {
                try
                {
                    LNPlanDeAccion logica = new LNPlanDeAccion();
                    List<EDPlanDeAccion> result = logica.ObtenerListaPlanDeAccion(nit);
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

            [HttpPost]
            [ActionName("guardar-planes-accion")]
            public HttpResponseMessage GuardarPlanesDeAccion(EDActividadPlanDeAccion actividadPlanDeAccion)
            {
                try
                {
                    LNPlanDeAccion logica = new LNPlanDeAccion();
                    EDActividadPlanDeAccion result = logica.GuardarPlanesDeAccion(actividadPlanDeAccion);
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
            [HttpPost]
            [ActionName("eliminar-actividad-planes-accion")]
            public HttpResponseMessage EliminarActividad(EDActividadPlanDeAccion actividadPlanDeAccion)
            {
                try
                {
                    LNPlanDeAccion logica = new LNPlanDeAccion();
                    bool result = logica.EliminarActividad(actividadPlanDeAccion);
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

            [HttpPost]
            [ActionName("editar-planes-accion")]
            public HttpResponseMessage EditarActividad(EDActividadPlanDeAccion actividadPlanDeAccion)
            {
                try
                {
                    LNPlanDeAccion logica = new LNPlanDeAccion();
                    bool result = logica.EditarActividad(actividadPlanDeAccion);
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

            [HttpPost]
            [ActionName("adicionar-planes-accion")]
            public HttpResponseMessage AdicionarActividad(EDActividadPlanDeAccion actividadPlanDeAccion)
            {
                try
                {
                    LNPlanDeAccion logica = new LNPlanDeAccion();
                    bool result = logica.AdicionarActividad(actividadPlanDeAccion);
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
            [ActionName("buscar-planes-accion")]
            public HttpResponseMessage BuscarPlanesDeAccion(int nit, int Pk_Id_ModuloPlanAccion, string fechaInicial, string fechaFinal)
            {
                try
                {
                    LNPlanDeAccion logica = new LNPlanDeAccion();
                    List<EDPlanDeAccion> result = logica.ConsultarListaPlanDeAccion(nit, Pk_Id_ModuloPlanAccion, fechaInicial, fechaFinal);
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
            [ActionName("consultar-modulos")]
            public HttpResponseMessage ConsultarModulos(int nit)
            {
                try
                {
                    LNPlanDeAccion logica = new LNPlanDeAccion();
                    List<ModulosPlanAccion> result = logica.ObtenerModulos(nit);
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
        }

    
}
