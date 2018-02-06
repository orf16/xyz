using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SG_SST.InterfazManager.MedicionEvaluacion;
using SG_SST.Interfaces.MedicionEvaluacion;
using SG_SST.EntidadesDominio.MedicionEvaluacion;
using SG_SST.Models.MedicionEvaluacion;


namespace SG_SST.Logica.MedicionEvaluacion
{
    public class LNPlanDeAccion
    {
        private static IPlanDeAccion iplanDeAccion =IMPlanDeAccion.PlanDeAccion();
        /// <summary>
        /// Metodo para obtener los diferentes tipos de documentos posibles que hay en la aplicación
        /// </summary>
        public List<EDPlanDeAccion> ObtenerListaPlanDeAccion(int nit)
        {
            List<EDPlanDeAccion> planDeAccionList = iplanDeAccion.ObtenerListaPlanDeAccion(nit);
            return planDeAccionList;
        }

        public EDActividadPlanDeAccion GuardarPlanesDeAccion(EDActividadPlanDeAccion actividadPlanDeAccion)
        {
            EDActividadPlanDeAccion planDeAccion = iplanDeAccion.GuardarPlanesDeAccion(actividadPlanDeAccion);
            return planDeAccion;
        }

        public bool EliminarActividad(EDActividadPlanDeAccion actividadPlanDeAccion)
        {
            bool planDeAccion = iplanDeAccion.EliminarActividad(actividadPlanDeAccion);
            return planDeAccion;
        }

        public bool EditarActividad(EDActividadPlanDeAccion actividadPlanDeAccion)
        {
            bool planDeAccion = iplanDeAccion.EditarActividad(actividadPlanDeAccion);
            return planDeAccion;
        }
        public bool AdicionarActividad(EDActividadPlanDeAccion actividadPlanDeAccion)
        {
            bool planDeAccion = iplanDeAccion.AdicionarActividad(actividadPlanDeAccion);
            return planDeAccion;
        }
        public List<EDPlanDeAccion> ConsultarListaPlanDeAccion(int nit, int Pk_Id_ModuloPlanAccion, string fechaInicial, string fechaFinal)
        {
            List<EDPlanDeAccion> planDeAccionList = iplanDeAccion.ConsultarListaPlanDeAccion(nit, Pk_Id_ModuloPlanAccion, fechaInicial, fechaFinal);
            return planDeAccionList;
        }
        public List<ModulosPlanAccion> ObtenerModulos(int nit)
        {
            List<ModulosPlanAccion> ListaMod = iplanDeAccion.ObtenerModulos(nit);
            return ListaMod;
        }
    }
}
