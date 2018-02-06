
namespace SG_SST.Aplicacion.Servicios.Controllers
{
    using SG_SST.EntidadesDominio.Aplicacion;
    using SG_SST.Logica.Aplicacion;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    public class CriteriosDeSSTController : ApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Empresa"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("Guardar_Servicios_Productos_Por_Criterios")]
        public HttpResponseMessage GuardarProductoCrite(EDProductoCriterio productoCriterio)
        {
            try
            {
                var logica = new LNCriteriosSST();
                bool result = logica.GuardarProductoCriterio(productoCriterio);
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
                var response = Request.CreateResponse(HttpStatusCode.InternalServerError);
                return response;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Empresa"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("Guardar_Seleccion_Evaluacion")]
        public HttpResponseMessage GuardarSeleccionEval(EDSeleccionYEvaluacion seleccionEvaluacion)
        {
            try
            {
                var logica = new LNCriteriosSST();
                bool result = logica.GuardarSeleccionYEvaluacion(seleccionEvaluacion);
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
                var response = Request.CreateResponse(HttpStatusCode.InternalServerError);
                return response;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Empresa"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("Editar_Seleccion_Evaluacion")]
        public HttpResponseMessage EditarSeleccionEval(EDSeleccionYEvaluacion seleccionEvaluacion)
        {
            try
            {
                var logica = new LNCriteriosSST();
                bool result = logica.EditarSeleccionYEvaluacion(seleccionEvaluacion);
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
                var response = Request.CreateResponse(HttpStatusCode.InternalServerError);
                return response;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Empresa"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("Editar_Producto_Criterio")]
        public HttpResponseMessage EditarProductoCrite(EDProductoCriterio productocriterio)
        {
            try
            {
                var logica = new LNCriteriosSST();
                bool result = logica.EditarProductoCriterio(productocriterio);
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
                var response = Request.CreateResponse(HttpStatusCode.InternalServerError);
                return response;
            }
        }

        [HttpGet]
        [ActionName("Obtener_Servicios_Productos_Por_Criterios")]
        public HttpResponseMessage ObtenerProductosPorCriterios(int idEmpresa)
        {
            try
            {
                var logica = new LNCriteriosSST();
                var result = logica.ObtenerProductosPorCriterios(idEmpresa);
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
        [ActionName("Obtener_Proveedor_Contratista")]
        public HttpResponseMessage ObtenerProveedoresContratista(int idEmpresa)
        {
            try
            {
                var logica = new LNCriteriosSST();
                var result = logica.ObtenerProveedoresContratistas(idEmpresa);
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
        [ActionName("Obtener_Lista_Proveedores")]
        public HttpResponseMessage ObtenerListaProveedores(int idEmpresa)
        {
            try
            {
                var logica = new LNCriteriosSST();
                var result = logica.ObtenerListaProveedores(idEmpresa);
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
        [ActionName("Obtener_Servicios_Producto")]
        public HttpResponseMessage ObtenerProducto(int idProducto)
        {
            try
            {
                var logica = new LNCriteriosSST();
                var result = logica.ObtenerProducto(idProducto);
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
        [ActionName("Obtener_Proveedor_Contratista_Editar")]
        public HttpResponseMessage ObtenerProveedorContratistaEditar(int idProveePorCalif)
        {
            try
            {
                var logica = new LNCriteriosSST();
                var result = logica.ObtenerProveedorContratistaEditar(idProveePorCalif);
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
        [ActionName("Consultar_Anexos_Proveedor")]
        public HttpResponseMessage ConsultarAnexosProveedor(int idProveePorCalif)
        {
            try
            {
                var logica = new LNCriteriosSST();
                var result = logica.ConsultarAnexosProveedor(idProveePorCalif);
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
        [ActionName("Eliminar_Proveedor_Contratista")]
        public HttpResponseMessage EliminarCalificacionProvvedor(int idProveePorCalif)
        {
            try
            {
                var logica = new LNCriteriosSST();
                bool result = logica.EliminarCalificacionProveedor(idProveePorCalif);
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

                var response = Request.CreateResponse(HttpStatusCode.InternalServerError);
                return response;
            }
        }

        [HttpDelete]
        [ActionName("Eliminar_Producto_Criterio")]
        public HttpResponseMessage EliminarProductoCriterio(int idProducto)
        {
            try
            {
                var logica = new LNCriteriosSST();
                bool result = logica.EliminarProductoCriterio(idProducto);
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

                var response = Request.CreateResponse(HttpStatusCode.InternalServerError);
                return response;
            }
        }

        [HttpGet]
        [ActionName("Obtener_Proveedor_Contratista_Graficar")]
        public HttpResponseMessage ObtenerProveedorContratistaGraficar(int idProveedor)
        {
            try
            {
                var logica = new LNCriteriosSST();
                var result = logica.ObtenerProveedorContratistaGraficar(idProveedor);
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
        [ActionName("Obtener_Proveedor")]
        public HttpResponseMessage ObtenerProveedor(int idProveedor)
        {
            try
            {
                var logica = new LNCriteriosSST();
                var result = logica.ObtenerProveedor(idProveedor);
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
        /// 
        /// </summary>
        /// <param name="Empresa"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("Editar_Proveedor")]
        public HttpResponseMessage EditarProveedor(EDProveedorContratista ProveedorContratista)
        {
            try
            {
                var logica = new LNCriteriosSST();
                bool result = logica.EditarProveedor(ProveedorContratista);
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
                var response = Request.CreateResponse(HttpStatusCode.InternalServerError);
                return response;
            }
        }
    }
}