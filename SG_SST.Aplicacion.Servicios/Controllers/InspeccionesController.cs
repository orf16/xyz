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

namespace SG_SST.Aplicacion.Servicios.Controllers
{
    public class InspeccionesController : ApiController
    {

        [HttpGet]
        [ActionName("Obtener-Tipo-Inspeccion")]
        public HttpResponseMessage ConsultarTipoInspeccion()
        {            
            try
            {
                var logica = new LNInspeccion();
                var result = logica.ObtenerTiposInspeccion();
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
        [ActionName("Obtener-Inspeccion-No-Ejecutada")]
        public HttpResponseMessage ObtenerInspeccionNoEjecutada(int id, int idp, int idi)
        {
            try
            {
                var logica = new LNInspeccion();
                var result = logica.ObtenerInspeccionNoEjecutada(id, idp, idi);
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
        [ActionName("Capacidad-ObtenerTipos-Peligros")]
        public HttpResponseMessage ObtenerTiposDePeligro()
        {
            try
            {
                var logica = new LNInspeccion();
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
                var response = Request.CreateResponse(HttpStatusCode.InternalServerError);
                return response;
            }
        }




        [HttpGet]
        [ActionName("Obtener-Info-Inspeccion")]
        public HttpResponseMessage ObtenerInfoInspeccion(int idInspeccion, int idCondicion)

        {
            try
            {
                var logica = new LNInspeccion();
                var result = logica.ObtenerInfoInspeccion(idInspeccion, idCondicion);
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
        [ActionName("Obtener-Condicion-Insegura")]
        public HttpResponseMessage ObtenerCondicionInsegura(int IdCondicion, int IdInspeccion)
        {
            try
            {
                var logica = new LNInspeccion();
                var result = logica.ObtenerCondicionInsegura(IdCondicion, IdInspeccion);
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
        [ActionName("Ejecutar-Plan")]
        public HttpResponseMessage EjecutarPlan(int ConsecutivoPlanVM, string responsable, string Fecha, string DescripcionTipoInspeccionse, int id)
        {
            try
            {
                var logica = new LNInspeccion();
                var result = logica.EjecutarPlan(ConsecutivoPlanVM, responsable, Fecha, DescripcionTipoInspeccionse,id);
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

        [HttpDelete]
        [ActionName("Eliminar-Inspecciones")]
        public HttpResponseMessage ObtenerInspeccionNoEjecutada(int IdInspeccion, int IdPlaneacion)
        {
            try
            {
                var logica = new LNInspeccion();
                var result = logica.EliminarInspeccion(IdInspeccion, IdPlaneacion);
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

        [HttpDelete]
        [ActionName("Eliminar-CondicionI")]
        public HttpResponseMessage EliminarCondicion(int IdCondicion)
        {
            try
            {
                var logica = new LNInspeccion();
                var result = logica.EliminarCondicion(IdCondicion);
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

        [HttpDelete]
        [ActionName("Eliminar-Planeacion")]
        public HttpResponseMessage EliminarPlaneacion(int IdPlaneacion)
        {
            try
            {
                var logica = new LNInspeccion();
                var result = logica.EliminarPlaneacion(IdPlaneacion);
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
        [ActionName("Obtener-Configuracion-PrioridadporI")]
        public HttpResponseMessage ObtenerInspeccionNoEjecutada(int Idinspeccion)
        {
            try
            {
                var logica = new LNInspeccion();
                var result = logica.ObtenerConfiguracionesPorIns(Idinspeccion);
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
        [ActionName("Continuar-Ejecutar-Plan")]
        public HttpResponseMessage ContinuarEjecutarPlan(int consecutivo, string responsable, string fecha, string describeinspeccion, int pkplan, int id)
        {
            try
            {
                var logica = new LNInspeccion();
                var result = logica.ContinuarEjecucionPlan(consecutivo, responsable, fecha, describeinspeccion, pkplan, id);
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
        [ActionName("Obtener-Plan-Inspeccion")]
        public HttpResponseMessage ConsultarPlanInspeccion()
        {
            try
            {
                var logica = new LNInspeccion();
                var result = logica.ObtenerPlanInspeccion();
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
        [ActionName("Obtener-Inspecciones-Empresa")]
        public HttpResponseMessage ObtenerInspeccionesEmpresa(int id)
        {
            try
            {
                var logica = new LNInspeccion();
                var result = logica.ObtenerInspeccionPorEmpresa(id);
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
        [ActionName("Obtener-Planeaciones-Empresa")]
        public HttpResponseMessage ObtenerPlaneacionesEmpresa(int id)
        {
            try
            {
                var logica = new LNInspeccion();
                var result = logica.ObtenerPlaneacionPorEmpresa(id);
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
        [ActionName("Obtener-Planeaciones-Empresase")]
        public HttpResponseMessage ObtenerPlaneacionesEmpresase(int id)
        {
            try
            {
                var logica = new LNInspeccion();
                var result = logica.ObtenerPlaneacionPorEmpresase(id);
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
        [ActionName("Obtener-Inspeccion-Rango")]
        public HttpResponseMessage ConsultarInspeccionPorFechas(int idsede, string fechai, string fechaf, int NIT)
        {
            try
            {
                var logica = new LNInspeccion();
                var result = logica.ObtenerInspeccionPorfechaEstado(idsede, fechai, fechaf);
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
        [ActionName("Obtener-Inspeccion-Tipo")]
        public HttpResponseMessage ConsultarInspeccionPorTipo(int idsede, string tipoInspeccion, DateTime? fechai, DateTime? fechaf, int NIT)
        {
            try
            {
                var logica = new LNInspeccion();
                var result = logica.ObtenerInspecciones(idsede,tipoInspeccion, fechai, fechaf);
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
        [ActionName("Obtener-Configuracion")]
        public HttpResponseMessage ObtenerConfiguracion()
        {
            try
            {
                var logica = new LNInspeccion();
                var result = logica.ObtenerConfiguraciones();
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
        [ActionName("Obtener-Para-Correctiva")]
        public HttpResponseMessage ObtenerCorrectivas(int IdEmpresa)
        {
            try
            {
                var logica = new LNInspeccion();
                var result = logica.ObtenerCorrectivas(IdEmpresa);
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
        [ActionName("Obtener-Todas-Correctivas")]
        public HttpResponseMessage ObtenerTodasCorrectivas(int IdEmpresa)
        {
            try
            {
                var logica = new LNInspeccion();
                var result = logica.ObtenerTodasCorrectivas(IdEmpresa);
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
        [ActionName("Obtener-ConfiguracionInspeccion")]
        public HttpResponseMessage ObtenerConfiguracionInspeccion()
        {
            try
            {
                var logica = new LNInspeccion();
                var result = logica.ObtenerConfiguracionesInspeccion();
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
        [ActionName("Obtener-Condiciones-Inspeccion")]
        public HttpResponseMessage ObtenerCondicionesPorInspeccion(int idinspeccion)
        {
            try
            {
                var logica = new LNInspeccion();
                var result = logica.ObtenerCondicionesPorInspeccion(idinspeccion);
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
         [ActionName("Obtener-PlanAccion")]
         public HttpResponseMessage ObtenerPlanAccionInspeccion()
         {
             try
             {
                 var logica = new LNInspeccion();
                 var result = logica.ObtenerPlanAccionInspeccion();
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
        [ActionName("Guardar-Plan-Inspeccion")]

        public HttpResponseMessage GuardarPlanInspeccion(EDPlanInspeccion planinspeccion)
        {
            try
            {
                var logica = new LNInspeccion();
                var result = logica.GuardarPlaneacion(planinspeccion);
                if (result != null)
                {
                    var response = Request.CreateResponse<EDPlanInspeccion>(HttpStatusCode.Created, result);
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
        [ActionName("Guardar-PlanAccion")]

        public HttpResponseMessage GuardarPlanAccion(EDPlanAccionInspeccion plan)
        {
            try
            {
                var logica = new LNInspeccion();
                var result = logica.GuardarPlanAccion(plan);
                if (result != null)
                {
                    var response = Request.CreateResponse<EDPlanAccionInspeccion>(HttpStatusCode.Created, result);
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
        [ActionName("Guardar-Inspeccion")]

        public HttpResponseMessage GuardarInspeccion(EDInspeccion inspeccion)
        {
            try
            {
                var logica = new LNInspeccion();
                var result = logica.GuardarInspeccion(inspeccion);
                if (result != null)
                {
                    var response = Request.CreateResponse<EDInspeccion>(HttpStatusCode.Created, result);
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
        [ActionName("Guardar-Condicion-Insegura")]

        public HttpResponseMessage GuardarCondicionInspeccion(EDCondicionInsegura condicion)
        {
            try
            {
                var logica = new LNInspeccion();
                var result = logica.GuardarCondicionesInspeccion(condicion);
                if (result != null)
                {
                    var response = Request.CreateResponse<EDCondicionInsegura>(HttpStatusCode.Created, result);
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
        [ActionName("Editar-Condicion")]

        public HttpResponseMessage EditarCondicion(EDCondicionInsegura condicion)
        {
            try
            {
                var logica = new LNInspeccion();
                var result = logica.EditarCondicion(condicion);
                if (result != null)
                {
                    var response = Request.CreateResponse<EDCondicionInsegura>(HttpStatusCode.Created, result);
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
        [ActionName("Guardar-Plan-Correctivo")]

        public HttpResponseMessage GuardarPlanAccionCorrectiva(List<EDPlanAccionCorrectiva> planescorrectivos)
        {
            try
            {
                var logica = new LNInspeccion();
                var result = logica.GuardarPlanAccionCorrectiva(planescorrectivos);
                if (result != null)
                {
                    var response = Request.CreateResponse<List<EDPlanAccionCorrectiva>>(HttpStatusCode.Created, result);
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
