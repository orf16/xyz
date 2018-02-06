using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SG_SST.EntidadesDominio.Aplicacion;
using SG_SST.EntidadesDominio.Planificacion;

namespace SG_SST.Interfaces.Aplicacion
{
    public interface IplanInspeccion
    {
        EDInspeccion ObtenerInfoInspeccion(int idInspeccion, int idCondicion);
        EDPlanInspeccion GuardarPlaneacion(EDPlanInspeccion planinspeccion);
        EDPlanInspeccion ObtenerPlanInspeccion();
        bool EliminarCondicion(int IdCondicion);
        bool EliminarInspeccion(int idinspeccion, int idplaneacion);
        bool EliminarPlaneacion(int idplaneacion);
        EDPlanInspeccion ContinuarEjecucionPlan(int ConsecutivoPlanVM, string responsable, string Fecha, string DescripcionTipoInspeccionse, int Idplaninspeccion, int id);
        EDInspeccion EjecutarPlan(int consecutivo, string responsable, string fecha, string Describe, int id);
        List<EDInspeccion> ObtenerInspeccionPorEmpresa(int id);
        List<EDPlanInspeccion> ObtenerplaneacionPorEmpresa(int id);
        List<EDInspeccion> ObtenerplaneacionPorEmpresase(int id);
        List<EDTipoDePeligro> ObtenerTiposDePeligro();
        EDCondicionInsegura ObtenerCondicionInsegura(int IdCondicion, int IdInspeccion);
        EDInspeccion ObtenerInspeccionNoEjecutada(int id, int idp, int idi);
        List<EDConfiguracion> ObtenerConfiguraciones();
        List<EDConfiguracion> ObtenerConfiguracionesPorIns(int idi);
        List<EDPlanAccionInspeccion> ObtenerInspeccionPorfechaEstado(int idsede, string FechaIn, string FechaFin);
        List<EDConfiguracion> ObtenerConfiguracionesInspeccion();
        List<EDPlanAccionCorrectiva> ObtenerCorrectivas(int IdEmpresa);
        List<EDPlanAccionCorrectiva> ObtenerTodasCorrectivas(int IdEmpresa);
        List<EDCondicionInsegura> ObtenerCondicionesPorInspeccion(int idinspeccion);
        List<EDPlanAccionInspeccion> ObtenerPlanAccionInspeccion();
        EDInspeccion GuardarInspeccion(EDInspeccion inspeccion);
        EDCondicionInsegura GuardarCondicionesInspeccion(EDCondicionInsegura condicion);

        EDCondicionInsegura EditarCondicion(EDCondicionInsegura condicion);
        EDPlanAccionInspeccion GuardarPlanAccion(EDPlanAccionInspeccion plan);

        List<EDPlanAccionCorrectiva> GuardarPlanAccionCorrectiva(List<EDPlanAccionCorrectiva> planescorrectivos);

        List<EDInspeccion> ObtenerInspecciones(int IdSede, string DescripcionTipoInspeccion, DateTime? FechaInicialB, DateTime? FechaFinal);
    }
}
