using SG_SST.EntidadesDominio.Aplicacion;
using SG_SST.Interfaces.Aplicacion;
using SG_SST.InterfazManager.Aplicacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Logica.Aplicacion
{
    public class LNPlanTrabajo
    {
        private static IPlanTrabajo pt = IMPlanTrabajo.IPlanTrabajo();

        public bool CrearPlanTrabajo(EDAplicacionPlanTrabajo planTrabajo) 
        {
            return pt.CrearPlanTrabajo(planTrabajo);
        }

        public List<EDAplicacionPlanTrabajo> ObtenerPlanesDeTrabajo(int idEmpresa) 
        {
            return pt.ObtenerPlanesDeTrabajo(idEmpresa);
        }

        public EDAplicacionPlanTrabajo EditarPlanTrabajo(int Pk_Id_PlanTrabajo) 
        {
            return pt.EditarPlanTrabajo(Pk_Id_PlanTrabajo);
        }

        public List<EDAplicacionPlanTrabajo> ObtenerPlanesDeTrabajoSede(int intsede)
        {
            List<EDAplicacionPlanTrabajo> Planes = pt.ObtenerPlanesDeTrabajoSede(intsede);
            return Planes;
        }

        public bool crearobjetivo(EDAplicacionPlanTrabajoDetalle EDAplicacionPlanTrabajoDetalle)
        {
            bool guardar = pt.crearobjetivo(EDAplicacionPlanTrabajoDetalle);
            return guardar;
        }
        public bool actualizarobjetivo(EDAplicacionPlanTrabajoDetalle EDAplicacionPlanTrabajoDetalle)
        {
            bool guardar = pt.actualizarobjetivo(EDAplicacionPlanTrabajoDetalle);
            return guardar;
        }
        public EDAplicacionPlanTrabajo ConsultarPlanTrabajo(int Pk_Id_PlanTrabajo, int IdEmpresa)
        {
            return pt.ConsultarPlanTrabajo(Pk_Id_PlanTrabajo, IdEmpresa);
        }
        public bool crearactividad(EDAplicacionPlanTrabajoActividad EDAplicacionPlanTrabajoActividad)
        {
            bool guardar = pt.crearactividad(EDAplicacionPlanTrabajoActividad);
            return guardar;
        }
        public bool actualizaractividad(EDAplicacionPlanTrabajoActividad EDAplicacionPlanTrabajoActividad)
        {
            bool guardar = pt.actualizaractividad(EDAplicacionPlanTrabajoActividad);
            return guardar;
        }
        public List<EDAplicacionPlanTrabajo> ObtenerPlanesDeTrabajoFiltro(int idEmpresa, string Fantes, string Fdespues, int vigencia, int sede, string Tipo)
        {
            List<EDAplicacionPlanTrabajo> planFiltro = pt.ObtenerPlanesDeTrabajoFiltro(idEmpresa, Fantes, Fdespues, vigencia, sede, Tipo);
            return planFiltro;
        }
        public bool EditarPlan(EDAplicacionPlanTrabajo EDAplicacionPlanTrabajo)
        {
            bool guardar = pt.EditarPlan(EDAplicacionPlanTrabajo);
            return guardar;
        }
        public bool EliminarPlanDeTrabajo(int Pk_Id_PlanTrabajo)
        {
            return pt.EliminarPlanDeTrabajo(Pk_Id_PlanTrabajo);
        }

        public bool EliminarObjetivoPlanDeTrabajo(int Pk_Id_ObjetivoPlanTrabajo)
        {
            return pt.EliminarObjetivoPlanDeTrabajo(Pk_Id_ObjetivoPlanTrabajo);
        }

        public bool EliminarActividadPlanDeTrabajo(int Pk_Id_ActividadPlanTrabajo)
        {
            return pt.EliminarActividadPlanDeTrabajo(Pk_Id_ActividadPlanTrabajo);
        }

        public bool EliminarProgramaPlanDeTrabajo(int Pk_Id_ProgramaPlanTrabajo)
        {
            return pt.EliminarProgramaPlanDeTrabajo(Pk_Id_ProgramaPlanTrabajo);
        }


        public bool crearprograma(EDAplicacionPlanTrabajoProgramacion EDAplicacionPlanTrabajoProgramacion)
        {
            bool guardar = pt.crearprograma(EDAplicacionPlanTrabajoProgramacion);
            return guardar;
        }
        public bool actualizarprograma(EDAplicacionPlanTrabajoProgramacion EDAplicacionPlanTrabajoProgramacion)
        {
            bool guardar = pt.actualizarprograma(EDAplicacionPlanTrabajoProgramacion);
            return guardar;
        }
    }
}
