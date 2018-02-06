using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SG_SST.EntidadesDominio.MedicionEvaluacion;
using SG_SST.Models.MedicionEvaluacion;

namespace SG_SST.Interfaces.MedicionEvaluacion
{
    public interface IPlanDeAccion
    {
        List<EDPlanDeAccion> ObtenerListaPlanDeAccion(int nit);
        EDActividadPlanDeAccion GuardarPlanesDeAccion(EDActividadPlanDeAccion actividadPlanDeAccion);
        bool EliminarActividad(EDActividadPlanDeAccion actividadPlanDeAccion);
        bool EditarActividad(EDActividadPlanDeAccion actividadPlanDeAccion);
        bool AdicionarActividad(EDActividadPlanDeAccion actividadPlanDeAccion);
        List<EDPlanDeAccion> ConsultarListaPlanDeAccion(int nit, int Pk_Id_ModuloPlanAccion, string fechaInicial, string fechaFinal);
        List<ModulosPlanAccion> ObtenerModulos(int nit);
    }
}
