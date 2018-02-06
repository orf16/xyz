
namespace SG_SST.Logica.Aplicacion
{
    using OfficeOpenXml;
    using SG_SST.EntidadesDominio.Aplicacion;
    using SG_SST.Interfaces.Aplicacion;
    using SG_SST.InterfazManager.Aplicacion;
    using System;
    using SG_SST.Models.Aplicacion;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public class LNCriteriosSST
    {
        private static ICriterioSST CriteriosSST = IMCriteriosSST.Criterios();

        public List<EDCriteriosSST> ObtenerCriteriosSST()
        {
            return CriteriosSST.ObtenerCriteriosSST();
        }

        public List<EDProductoPorCriterioSSt> ObtenerCriteriosSSTSeleccionados(int id)
        {
            return CriteriosSST.ObtenerCriteriosSSTSeleccionados(id);
        }

        public bool GuardarProductoCriterio(EDProductoCriterio productoCriterio)
        {
            return CriteriosSST.GuardarProductoCriterio(productoCriterio);
        }

        public bool GuardarSeleccionYEvaluacion(EDSeleccionYEvaluacion seleccionEvaluacion)
        {
            return CriteriosSST.GuardarSeleccionYEvaluacion(seleccionEvaluacion);
        }

        public bool EditarSeleccionYEvaluacion(EDSeleccionYEvaluacion seleccionEvaluacion)
        {
            return CriteriosSST.EditarSeleccionYEvaluacion(seleccionEvaluacion);
        }

        public bool EditarProductoCriterio(EDProductoCriterio productocriterio)
        {
            return CriteriosSST.EditarProductoCriterio(productocriterio);
        }

        public List<EDServicioProducto> ObtenerProductosPorCriterios(int idEmpresa)
        {
            return CriteriosSST.ObtenerProductosPorCriterios(idEmpresa);
        }

        public List<EDSeleccionYEvaluacion> ObtenerProveedoresContratistas(int idEmpresa)
        {
            return CriteriosSST.ObtenerProveedoresContratistas(idEmpresa);
        }

        public List<EDProveedorContratista> ObtenerListaProveedores(int idEmpresa)
        {
            return CriteriosSST.ObtenerListaProveedores(idEmpresa);
        }

        public EDServicioProducto ObtenerProducto(int idProducto)
        {
            return CriteriosSST.ObtenerProducto(idProducto);
        }

        public EDSeleccionYEvaluacion ObtenerProveedorContratistaEditar(int idProveePorCalif)
        {
            return CriteriosSST.ObtenerProveedorContratistaEditar(idProveePorCalif);
        }
        public List<string> ConsultarAnexosProveedor(int idProveePorCalif)
        {
            return CriteriosSST.ConsultarAnexosProveedor(idProveePorCalif);
        }

        public bool EliminarCalificacionProveedor(int idProveePorCalif)
        {
            return CriteriosSST.EliminarCalificacionProveedor(idProveePorCalif);
        }
        public bool EliminarProductoCriterio(int idProducto)
        {
            return CriteriosSST.EliminarProductoCriterio(idProducto);
        }
        public List<EDSeleccionYEvaluacion> ObtenerProveedorContratistaGraficar(int idProveedor)
        {
            return CriteriosSST.ObtenerProveedorContratistaGraficar(idProveedor);
        }
        public EDProveedorContratista ObtenerProveedor(int idProveedor)
        {
            return CriteriosSST.ObtenerProveedor(idProveedor);
        }
        public bool EditarProveedor(EDProveedorContratista ProveedorContratista)
        {
            return CriteriosSST.EditarProveedor(ProveedorContratista);
        }
    }
}
