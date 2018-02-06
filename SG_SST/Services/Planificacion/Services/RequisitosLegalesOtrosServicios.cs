
namespace SG_SST.Services.Planificacion.Services
{
    using SG_SST.Models.Planificacion;
    using SG_SST.Repositories.Planificacion.Repositories;
    using SG_SST.Repositories.Planificacion.IRepositories;
    using SG_SST.Services.Planificacion.IServices;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class RequisitosLegalesOtrosServicios : IRequisitosLegalesOtrosServicios
    {

        IRequisitosLegalesOtrosRepositorio gb;

        public RequisitosLegalesOtrosServicios()
        {
            gb = new RequisitosLegalesOtrosRepositorio();
        }

        #region "Métodos Públicos"

       /*
        public bool GrabarRequisitosLegalesOtros_formulario(RequisitosLegalesOtros objreq)
        {
            int PK_RequisitosLegales;

            PK_RequisitosLegales = objreq.PK_RequisitosLegalesOtros;




           RequisitosLegalesOtros objreqBusqueda = gb.BuscarRequisitosLegalesOtros(PK_RequisitosLegales);

           objreqBusqueda.Evidencia_Cumplimiento = objreq.Evidencia_Cumplimiento;
           objreqBusqueda.FK_Cumplimiento_Evaluacion = objreq.FK_Cumplimiento_Evaluacion;
           objreqBusqueda.Hallazgo = objreq.Hallazgo;
           objreqBusqueda.FK_Estado_RequisitoslegalesOtros = objreq.FK_Estado_RequisitoslegalesOtros;
           objreqBusqueda.Responsable = objreq.Responsable;
           objreqBusqueda.Fecha_Seguimiento_Control = objreq.Fecha_Seguimiento_Control;
           objreqBusqueda.Fecha_Actualizacion = objreq.Fecha_Actualizacion;



           return gb.ModificarRequisitosLegalesOtros_Matriz(objreqBusqueda);
        }
      */
        public RequisitosLegalesOtros BuscarRequisitosLegalesOtros(int PK_RequisitosLegales)
        {
            return gb.BuscarRequisitosLegalesOtros(PK_RequisitosLegales);
        }
        /*
        public bool EliminarRequisitosLegalesOtros(int PK_RequisitosLegales)
        {
            return gb.EliminaRequisitosLegalesOtros(PK_RequisitosLegales);
        }
        */
        public bool ModficarRequisitosLegalesOtros(RequisitosLegalesOtros objreq)
        {
            return gb.ModficarRequisitosLegalesOtros(objreq);
        }
        /*
        public List<RequisitosLegalesOtros> ObtenerRequisitosLegalesExcel_Servicio(int IDEmpresa)
        {
            return gb.ObtenerRequisitosLegalesExcel_Repositorio(IDEmpresa);
        }
        */
        public List<RequisitosLegalesOtros> Busqueda_RequisitosLegales_Peligro(string strPeligroBusqueda, int PK_Empresa)
        {
            return gb.Busqueda_RequisitosLegales_Peligro(strPeligroBusqueda, PK_Empresa);
        }

        public List<RequisitosLegalesPosipedia> Busqueda_PorActividadEconomica(int FK_Actividad_Economica, int PK_Empresa)
        {
            return gb.Busqueda_PorActividadEconomica(FK_Actividad_Economica, PK_Empresa);
        }
      
        public bool GrabarRequisitosLegalesOtros(MatrizRequisitosLegales OBJMatriz)
        {
            return gb.CrearMatriz(OBJMatriz);
        }
      
        public bool GuardarRequisitos_Seleccionados(RequisitosLegalesOtros objre,int varnombrematyriz)
        {
            return gb.GuardarRequisitos_Seleccionados(objre,varnombrematyriz);
        }


        public bool RelacionarRequisitosMatriz(Requisitos_Matriz objm)
        {
            return gb.RelacionarRequisitosMatriz(objm);
        }

        /*
        public List<RequisitosLegalesPosipedia> BusquedaRequisitoLegal(string TipoNorma, DateTime fechapublicacion, string EnteReq, string DescripcionReq)
        {
            return gb.BusquedaRequisitoLegal(TipoNorma, fechapublicacion, EnteReq, DescripcionReq);
        }
        */
        /*
        public List<RequisitosLegalesPosipedia> BusquedaRequisitoLegal(string TipoNorma)
        {
            return gb.BusquedaRequisitoLegal(TipoNorma);
        }
        */

        /*
        public List<RequisitosLegalesPosipedia> BusquedaRequisitoLegalTipoNormafechapublicacion(DateTime fechapublicacion)
        {
            return gb.BusquedaRequisitoLegalTipoNormafechapublicacion(fechapublicacion);
        }
        */


        public List<RequisitosLegalesPosipedia> BusquedaRequisitoLegalTipoNormafechapublicacion(string TipoNorma, DateTime? fechapublicacion, int? FK_ActividadEconomica)
        {
            return gb.BusquedaRequisitoLegalTipoNormafechapublicacion(TipoNorma, fechapublicacion,FK_ActividadEconomica);
        }

        public List<RequisitosLegalesPosipedia> BusquedaRequisitoLegalTipoNormaEnteReq(string TipoNorma, string EnteReq, int FK_ActividadEconomica)
        {
            return gb.BusquedaRequisitoLegalTipoNormaEnteReq(TipoNorma, EnteReq, FK_ActividadEconomica);
        }

        public List<RequisitosLegalesPosipedia> BusquedaRequisitoLegalTipoNormaDescripcionReq(string TipoNorma, string DescripcionReq, int FK_ActividadEconomica)
        {
            return gb.BusquedaRequisitoLegalTipoNormaDescripcionReq(TipoNorma, DescripcionReq, FK_ActividadEconomica);
        }


        public List<RequisitosLegalesPosipedia> BusquedaRequisitoLegalfechapublicacionEnteReq(DateTime fechapublicacion, string EnteReq, int FK_ActividadEconomica)
        {
            return gb.BusquedaRequisitoLegalfechapublicacionEnteReq(fechapublicacion, EnteReq, FK_ActividadEconomica );
        }


        public List<RequisitosLegalesPosipedia> BusquedaRequisitoLegalEnteReqDescripcionReq(string EnteReq, string DescripcionReq, int FK_ActividadEconomica)
        {
            return gb.BusquedaRequisitoLegalEnteReqDescripcionReq(EnteReq, DescripcionReq, FK_ActividadEconomica);
        }


        public List<RequisitosLegalesPosipedia> BusquedaRequisitoLegal(string TipoNorma, int FK_ActividadEconomica)
        {
            return gb.BusquedaRequisitoLegal(TipoNorma, FK_ActividadEconomica);
        }



        public List<RequisitosLegalesPosipedia> BusquedaRequisitoLegalfechapublicacion(DateTime fechapublicacion, int FK_ActividadEconomica)
        {
            return gb.BusquedaRequisitoLegalfechapublicacion(fechapublicacion, FK_ActividadEconomica);
        }

        public List<RequisitosLegalesPosipedia> BusquedaRequisitoLegalEnteReq(string EnteReq, int FK_ActividadEconomica)
        {
            return gb.BusquedaRequisitoLegalEnteReq(EnteReq, FK_ActividadEconomica);
        }



        public List<RequisitosLegalesPosipedia> BusquedaRequisitoLegalDescripcionReq(string DescripcionReq, int FK_ActividadEconomica)
        {
            return gb.BusquedaRequisitoLegalDescripcionReq(DescripcionReq, FK_ActividadEconomica);
        }



        public List<RequisitosLegalesPosipedia> BusquedaRequisitoLegalfechapublicacionDescripcionReq(DateTime fechapublicacion, string DescripcionReq, int FK_ActividadEconomica)
        {
            return gb.BusquedaRequisitoLegalfechapublicacionDescripcionReq(fechapublicacion, DescripcionReq, FK_ActividadEconomica);
        }


        public List<RequisitosLegalesPosipedia> BusquedaRequisitoLegalTipoNormafechapublicacionEnteReq(string TipoNorma, DateTime fechapublicacion, string EnteReq, int FK_ActividadEconomica)
        {
            return gb.BusquedaRequisitoLegalTipoNormafechapublicacionEnteReq(TipoNorma, fechapublicacion, EnteReq, FK_ActividadEconomica);
        }




        public List<RequisitosLegalesPosipedia> BusquedaRequisitoLegalTipoNormaEnteReqDescripcionReq(string TipoNorma, string EnteReq, string DescripcionReq, int FK_ActividadEconomica)
        {
            return gb.BusquedaRequisitoLegalTipoNormaEnteReqDescripcionReq(TipoNorma, EnteReq, DescripcionReq, FK_ActividadEconomica);
        }




        public List<RequisitosLegalesPosipedia> BusquedaRequisitoLegalTipoNormafechapublicacionDescripcionReq(string TipoNorma, DateTime fechapublicacion, string DescripcionReq, int FK_ActividadEconomica)

        {
            return gb.BusquedaRequisitoLegalTipoNormafechapublicacionDescripcionReq(TipoNorma,fechapublicacion,DescripcionReq, FK_ActividadEconomica);
        }


        public List<RequisitosLegalesPosipedia> BusquedaRequisitoLegalfechapublicacionEnteReqDescripcionReq(DateTime fechapublicacion, string EnteReq, string DescripcionReq, int FK_ActividadEconomica)

        {
            return gb.BusquedaRequisitoLegalfechapublicacionEnteReqDescripcionReq(fechapublicacion, EnteReq, DescripcionReq, FK_ActividadEconomica);
        }




        public List<RequisitosLegalesPosipedia> BusquedaRequisitoLegalTipoNormafechapublicacionEnteReqDescripcionReq(string TipoNorma, DateTime fechapublicacion, string EnteReq, string DescripcionReq, int FK_ActividadEconomica)

        {
            return gb.BusquedaRequisitoLegalTipoNormafechapublicacionEnteReqDescripcionReq( TipoNorma, fechapublicacion, EnteReq, DescripcionReq, FK_ActividadEconomica);
        }



      
         public bool GuardarNuevoRequisitos_SeleccionadosMatriz(RequisitosLegalesOtros objre, int PKMatriz)
         {
             return gb.GuardarNuevoRequisitos_SeleccionadosMatriz(objre, PKMatriz);
         }


        /*
         public bool EliminarMatrices(int PK_Matriz)
         {
             return gb.EliminaRequisitosLegalesOtros(PK_Matriz);
            // return false;
         }
        */
        
        public bool EliminarMatrices(int PK_Matriz)
        {
            return gb.EliminarMatrices(PK_Matriz);
        }



        /*
         public bool GrabarRequisitosLegalesOtros_formulario(RequisitosLegalesOtros objreq)
         {



             int PK_RequisitosLegales;

             PK_RequisitosLegales = objreq.PK_RequisitosLegalesOtros;




             RequisitosLegalesOtros objreqBusqueda = gb.BuscarRequisitosLegalesOtros(PK_RequisitosLegales);

             objreqBusqueda.Evidencia_Cumplimiento = objreq.Evidencia_Cumplimiento;
             objreqBusqueda.FK_Cumplimiento_Evaluacion = objreq.FK_Cumplimiento_Evaluacion;
             objreqBusqueda.Hallazgo = objreq.Hallazgo;
             objreqBusqueda.FK_Estado_RequisitoslegalesOtros = objreq.FK_Estado_RequisitoslegalesOtros;
             objreqBusqueda.Responsable = objreq.Responsable;
             objreqBusqueda.Fecha_Seguimiento_Control = objreq.Fecha_Seguimiento_Control;
             objreqBusqueda.Fecha_Actualizacion = objreq.Fecha_Actualizacion;



             return gb.ModificarRequisitosLegalesOtros_Matriz(objreqBusqueda);


             //return gb.GrabarRequisitosLegalesOtros(objreq);


         }

        */


         public bool GrabarRequisitosLegalesOtros_Formulario(RequisitosLegalesOtros objreqformulario, int pk_matriz)
         {

             return gb.GrabarRequisitosLegalesOtros_Formulario(objreqformulario, pk_matriz);
         
         
         }


         public bool Eliminar_ReqLegalesOtros(int PK_RequisitosLegales)
         {

             return gb.Eliminar_ReqLegalesOtros(PK_RequisitosLegales);

         }



         public bool valorvariable(int mivalor)
         {
             return gb.valorvariable(mivalor);
         }


         public bool Busqueda_Matricesduplicado(string NombreMatriz, int PK_Empresa)
         {
             return gb.Busqueda_Matricesduplicado(NombreMatriz, PK_Empresa);
         }








        #endregion

    }
}