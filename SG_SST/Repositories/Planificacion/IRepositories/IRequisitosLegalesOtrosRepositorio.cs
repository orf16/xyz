using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SG_SST.Models.Planificacion;



namespace SG_SST.Repositories.Planificacion.IRepositories
{
    interface IRequisitosLegalesOtrosRepositorio
    {
        bool GrabarRequisitosLegalesOtros(RequisitosLegalesOtros objreq);
        bool ModficarRequisitosLegalesOtros(RequisitosLegalesOtros objreq);
        //bool EliminaRequisitosLegalesOtros(int PK_RequisitosLegales);
        RequisitosLegalesOtros BuscarRequisitosLegalesOtros(int PK_RequisitosLegales);
        //List<RequisitosLegalesOtros> ObtenerRequisitosLegalesExcel_Repositorio(int Pk_RequisitosLegales);
        List<RequisitosLegalesOtros> Busqueda_RequisitosLegales_Peligro(string strPeligroBusqueda, int PK_Empresa);
        List<RequisitosLegalesPosipedia> Busqueda_PorActividadEconomica(int FK_Actividad_Economica, int PK_Empresa);
        bool CrearMatriz(MatrizRequisitosLegales objmodmat);
        //bool GuardarRequisitos_Seleccionados(int id);
        bool GuardarRequisitos_Seleccionados(RequisitosLegalesOtros objre, int fkmatriz);
        bool RelacionarRequisitosMatriz(Requisitos_Matriz objmatr);
        //List<RequisitosLegalesPosipedia> BusquedaRequisitoLegal(string TipoNorma, DateTime fechapublicacion, string EnteReq, string DescripcionReq);
        //List<RequisitosLegalesPosipedia> BusquedaRequisitoLegal(string TipoNorma);


        //List<RequisitosLegalesPosipedia> BusquedaRequisitoLegalTipoNormafechapublicacion(DateTime fechapublicacion);
        //List<RequisitosLegalesPosipedia> BusquedaRequisitoLegalTipoNormaDescripcionReq(string DescripcionReq);
        //List<RequisitosLegalesPosipedia> BusquedaRequisitoLegalTipoNormaEnteReq(string EnteReq);

        List<RequisitosLegalesPosipedia> BusquedaRequisitoLegalTipoNormafechapublicacion(string TipoNorma, DateTime? fechapublicacion, int? FK_ActividadEconomica);
        List<RequisitosLegalesPosipedia> BusquedaRequisitoLegalTipoNormaEnteReq(string TipoNorma, string EnteReq, int FK_ActividadEconomica);
        List<RequisitosLegalesPosipedia> BusquedaRequisitoLegalTipoNormaDescripcionReq(string TipoNorma, string DescripcionReq, int FK_ActividadEconomica);
        List<RequisitosLegalesPosipedia> BusquedaRequisitoLegalfechapublicacionEnteReq(DateTime fechapublicacion, string EnteReq, int FK_ActividadEconomica);
        List<RequisitosLegalesPosipedia> BusquedaRequisitoLegalEnteReqDescripcionReq(string EnteReq, string DescripcionReq, int FK_ActividadEconomica);
        List<RequisitosLegalesPosipedia> BusquedaRequisitoLegal(string TipoNorma, int FK_ActividadEconomica);
        List<RequisitosLegalesPosipedia> BusquedaRequisitoLegalfechapublicacion(DateTime fechapublicacio, int FK_ActividadEconomica);
        List<RequisitosLegalesPosipedia> BusquedaRequisitoLegalEnteReq(string EnteReq, int FK_ActividadEconomica);
        List<RequisitosLegalesPosipedia> BusquedaRequisitoLegalDescripcionReq(string DescripcionReq, int FK_ActividadEconomica);
        List<RequisitosLegalesPosipedia> BusquedaRequisitoLegalfechapublicacionDescripcionReq(DateTime fechapublicacion, string DescripcionReq, int FK_ActividadEconomica);
        List<RequisitosLegalesPosipedia> BusquedaRequisitoLegalTipoNormafechapublicacionEnteReq(string TipoNorma, DateTime fechapublicacion, string EnteReq, int FK_ActividadEconomica);
        List<RequisitosLegalesPosipedia> BusquedaRequisitoLegalTipoNormaEnteReqDescripcionReq(string TipoNorma, string EnteReq, string DescripcionReq, int FK_ActividadEconomica);
        List<RequisitosLegalesPosipedia> BusquedaRequisitoLegalTipoNormafechapublicacionDescripcionReq(string TipoNorma, DateTime fechapublicacion, string DescripcionReq, int FK_ActividadEconomica);
        List<RequisitosLegalesPosipedia> BusquedaRequisitoLegalfechapublicacionEnteReqDescripcionReq(DateTime fechapublicacion, string EnteReq, string DescripcionReq, int FK_ActividadEconomica);
        List<RequisitosLegalesPosipedia> BusquedaRequisitoLegalTipoNormafechapublicacionEnteReqDescripcionReq(string TipoNorma, DateTime fechapublicacion, string EnteReq, string DescripcionReq, int FK_ActividadEconomica);
        bool GuardarNuevoRequisitos_SeleccionadosMatriz(RequisitosLegalesOtros objreq, int PKMatriz);
        bool ModificarRequisitosLegalesOtros_Matriz(RequisitosLegalesOtros objreq);
        bool EliminarMatrices(int PK_Matriz);
        bool GrabarRequisitosLegalesOtros_Formulario(RequisitosLegalesOtros objreqformulario, int pk_matriz);
        bool Eliminar_ReqLegalesOtros(int PK_RequisitosLegales);
        bool valorvariable(int mivalor);
         bool Busqueda_Matricesduplicado(string NombreMatriz, int PK_Empresa);





    }
}

