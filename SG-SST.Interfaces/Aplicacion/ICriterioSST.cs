
namespace SG_SST.Interfaces.Aplicacion
{
    using SG_SST.EntidadesDominio.Aplicacion;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using SG_SST.Models.Aplicacion;
    public interface ICriterioSST
    {
        /// <summary>
        /// Repositorio que me retorna una lista de objetos de tipo EDCriteriosSST
        /// </summary>
        /// <param name="idEmpresa"></param>
        /// <returns>lista de objetos de tipo EDCriteriosSST</returns>
        List<EDCriteriosSST> ObtenerCriteriosSST();

        /// <summary>
        /// Repositorio que me retorna una lista de objetos de tipo entero con los criterios seleccionados
        /// </summary>
        /// <param name="idEmpresa"></param>
        /// <returns>lista de enteros de tipo int con los criterios seleccionados</returns>
        List<EDProductoPorCriterioSSt> ObtenerCriteriosSSTSeleccionados(int id);

        /// <summary>
        /// Repositorio que me guarda el Producto y los criterios 
        /// </summary>
        /// <param name="productoCriterio">Criterios guardados objeto de tipo EDCriteriosSST </param>
        /// <returns>retorna verdadero si fue posible guardarlo en la base de datos o false del caso contrario</returns>
        bool GuardarProductoCriterio(EDProductoCriterio productoCriterio);

        /// <summary>
        /// Repositorio que me guarda el contratista y la calificacion de los criteriosst
        /// </summary>
        /// <param name="seleccionEvaluacion">contratista y la calificacion de los criteriossts guardados objeto de tipo EDSeleccionYEvaluacion </param>
        /// <returns>retorna verdadero si fue posible guardarlo en la base de datos o false del caso contrario</returns>
        bool GuardarSeleccionYEvaluacion(EDSeleccionYEvaluacion seleccionEvaluacion);

        /// <summary>
        /// Repositorio que me permite editar el contratista y la calificacion de los criteriosst
        /// </summary>
        /// <param name="seleccionEvaluacion">contratista y la calificacion de los criteriossts guardados objeto de tipo EDSeleccionYEvaluacion </param>
        /// <returns>retorna verdadero si fue posible editarlo en la base de datos o false del caso contrario</returns>
        bool EditarSeleccionYEvaluacion(EDSeleccionYEvaluacion seleccionEvaluacion);

        /// <summary>
        /// Repositorio que me edita el Producto y los criterios 
        /// </summary>
        /// <param name="productoCriterio">Criterios y productos editados objeto de tipo EDServicioProducto </param>
        /// <returns>retorna verdadero si fue posible editar en la base de datos o false del caso contrario</returns>
        bool EditarProductoCriterio(EDProductoCriterio productoCriterio);

        /// <summary>
        /// Repositorio que me retorna una lista de objetos de tipo ServicioOProducto
        /// </summary>
        /// <param name="idEmpresa">id o pk de la empresa</param>
        /// <returns>lista de objetos de tipo ServicioOProducto</returns>
        List<EDServicioProducto> ObtenerProductosPorCriterios(int idEmpresa);

        /// <summary>
        /// Repositorio que me retorna una lista de objetos de tipo Proveedor_Contratista Calificado
        /// </summary>
        /// <param name="idEmpresa">id o pk de la empresa</param>
        /// <returns>lista de objetos de tipo Proveedor_Contratista</returns>
        List<EDSeleccionYEvaluacion> ObtenerProveedoresContratistas(int idEmpresa);

         /// <summary>
        /// Repositorio que me retorna una lista de objetos de tipo Proveedor_Contratista
        /// </summary>
        /// <param name="idEmpresa">id o pk de la empresa</param>
        /// <returns>lista de objetos de tipo Proveedor_Contratista</returns>
        List<EDProveedorContratista> ObtenerListaProveedores(int idEmpresa);

        /// <summary>
        /// Repositorio que me retorna un objeto de tipo Proveedor_Contratista
        /// </summary>
        /// <param name="idEmpresa">id del proveedor calificado</param>
        /// <returns>objeto de tipo Proveedor_Contratista</returns>
        EDSeleccionYEvaluacion ObtenerProveedorContratistaEditar(int idProveePorCalif);

        /// <summary>
        /// Repositorio que me retorna un objeto de tipo Proveedor_Contratista
        /// </summary>
        /// <param name="idEmpresa">id del proveedor</param>
        /// <returns>objeto de tipo Proveedor_Contratista</returns>
        List<EDSeleccionYEvaluacion> ObtenerProveedorContratistaGraficar(int idProveedor);

        /// <summary>
        /// Repositorio que me retorna una lista de objetos de tipo ArchivoAnexo
        /// </summary>
        /// <param name="idEmpresa">id </param>
        /// <returns>lista de objetos de tipo ArchivoAnexo</returns>
        List<string> ConsultarAnexosProveedor(int idProveePorCalif);

        /// <summary>
        /// Repositorio que me retorna un objeto de tipo ServicioOProducto
        /// </summary>
        /// <param name="idEmpresa">id del ServicioOProducto</param>
        /// <returns>Un objeto de tipo ServicioOProducto</returns>
        EDServicioProducto ObtenerProducto(int idProducto);

        /// <summary>
        /// Repositorio que me retorna un objeto de tipo Proveedor_Contratista
        /// </summary>
        /// <param name="idEmpresa">id del proveedor</param>
        /// <returns>objeto de tipo Proveedor_Contratista</returns>
        EDProveedorContratista ObtenerProveedor(int idProveedor);

        /// <summary>
        /// Repositorio que me permite editar un Proveedor_Contratista
        /// </summary>
        /// <param name="idEmpresa">EDProveedorContratista</param>
        /// <returns>booleano true si fue exitosa la edicion Proveedor_Contratista de lo contrario false</returns>
        bool EditarProveedor(EDProveedorContratista ProveedorContratista);

        /// <summary>
        /// Repositorio que elimina una calificacion de un proveedor o contratista
        /// </summary>
        /// <param name="idProducto">id o pk de la calificacion del proveedor a eliminar</param>
        /// <returns>retorna verdadero si fue posible eliminarlo en la base de datos o false de lo contrario</returns>
        bool EliminarCalificacionProveedor(int idProveePorCalif);

        /// <summary>
        /// Repositorio que elimina un ProductoCriterio de adquisición de bienes o contrataciones
        /// </summary>
        /// <param name="idProducto">id o pk del ProductoCriterio a eliminar</param>
        /// <returns>retorna verdadero si fue posible eliminarlo en la base de datos o false de lo contrario</returns>
        bool EliminarProductoCriterio(int idProducto);

    }
}
