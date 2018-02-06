using SG_SST.EntidadesDominio.Aplicacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Interfaces.Aplicacion
{
    public interface IPlanTrabajo
    {
        bool CrearPlanTrabajo(EDAplicacionPlanTrabajo planTrabajo);
        List<EDAplicacionPlanTrabajo> ObtenerPlanesDeTrabajo(int idEmpresa);
        EDAplicacionPlanTrabajo EditarPlanTrabajo(int Pk_Id_PlanTrabajo);
        List<EDAplicacionPlanTrabajo> ObtenerPlanesDeTrabajoSede(int intsede);
        bool crearobjetivo(EDAplicacionPlanTrabajoDetalle EDAplicacionPlanTrabajoDetalle);
        bool actualizarobjetivo(EDAplicacionPlanTrabajoDetalle EDAplicacionPlanTrabajoDetalle);
        EDAplicacionPlanTrabajo ConsultarPlanTrabajo(int Pk_Id_PlanTrabajo, int IdEmpresa);
        bool crearactividad(EDAplicacionPlanTrabajoActividad EDAplicacionPlanTrabajoActividad);
        bool actualizaractividad(EDAplicacionPlanTrabajoActividad EDAplicacionPlanTrabajoActividad);
        List<EDAplicacionPlanTrabajo> ObtenerPlanesDeTrabajoFiltro(int idEmpresa, string Fantes, string Fdespues, int vigencia, int sede, string Tipo);
        bool EditarPlan(EDAplicacionPlanTrabajo EDAplicacionPlanTrabajo);

        bool crearprograma(EDAplicacionPlanTrabajoProgramacion EDAplicacionPlanTrabajoProgramacion);
        bool actualizarprograma(EDAplicacionPlanTrabajoProgramacion EDAplicacionPlanTrabajoProgramacion);

        bool EliminarPlanDeTrabajo(int Pk_Id_PlanTrabajo);
        bool EliminarObjetivoPlanDeTrabajo(int Pk_Id_ObjetivoPlanTrabajo);
        bool EliminarActividadPlanDeTrabajo(int Pk_Id_ActividadPlanTrabajo);
        bool EliminarProgramaPlanDeTrabajo(int Pk_Id_ProgramaPlanTrabajo);
    }
}
