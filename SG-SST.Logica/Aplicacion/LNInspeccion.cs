using SG_SST.EntidadesDominio.Aplicacion;
using SG_SST.EntidadesDominio.Planificacion;
using SG_SST.Interfaces.Aplicacion;
using SG_SST.InterfazManager.Aplicacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Logica.Aplicacion
{
   public class LNInspeccion
    {
       private static IInspeccion insp = IMAplicacion.Inspeccion();
       private static IplanInspeccion ipi = IMPlanInspeccion.PlanInspeccion();


       public List<EDTipoInspeccion> ObtenerTiposInspeccion()
       {
           return insp.ObtenerTiposInspeccion();
       }
       public EDInspeccion EjecutarPlan(int consecutivo, string responsable, string fecha, string Describe, int id)
       {
           return ipi.EjecutarPlan(consecutivo, responsable, fecha, Describe, id);
       }

       public EDInspeccion ObtenerInfoInspeccion(int idinspeccion, int idcondicion)
       {
           return ipi.ObtenerInfoInspeccion(idinspeccion, idcondicion);
       }
       public EDCondicionInsegura ObtenerCondicionInsegura(int Idcondicion, int IdInspeccion)
       {
           return ipi.ObtenerCondicionInsegura(Idcondicion, IdInspeccion);
       }
       public EDInspeccion ObtenerInspeccionNoEjecutada (int id,int idp, int idi)
       {
           return ipi.ObtenerInspeccionNoEjecutada(id, idp, idi);
       }
       public EDPlanInspeccion ContinuarEjecucionPlan(int ConsecutivoPlanVM, string responsable, string Fecha, string DescripcionTipoInspeccionse, int Idplaninspeccion, int id)
       {
           return ipi.ContinuarEjecucionPlan(ConsecutivoPlanVM, responsable, Fecha, DescripcionTipoInspeccionse, Idplaninspeccion,id);
       }
       public EDPlanInspeccion GuardarPlaneacion(EDPlanInspeccion planinspeccion)
       {

           return ipi.GuardarPlaneacion(planinspeccion);
       }
       public EDInspeccion GuardarInspeccion(EDInspeccion Inspeccion)
       {
           return ipi.GuardarInspeccion(Inspeccion);
       }
       public EDPlanInspeccion ObtenerPlanInspeccion()
       {
           return ipi.ObtenerPlanInspeccion();
       }
       public bool EliminarCondicion(int IdCondicion)
       {
           return ipi.EliminarCondicion(IdCondicion);
       }
       public bool EliminarInspeccion(int idinspeccion, int idplaneacion)
       {
           return ipi.EliminarInspeccion(idinspeccion,idplaneacion);
       }

       public bool EliminarPlaneacion(int idplaneacion)
       {
           return ipi.EliminarPlaneacion(idplaneacion);
       }
       public List<EDInspeccion> ObtenerInspeccionPorEmpresa(int id)
       {
           return ipi.ObtenerInspeccionPorEmpresa(id);
       }
       
       public List<EDTipoDePeligro> ObtenerTiposDePeligro()
       {
           return ipi.ObtenerTiposDePeligro();
       }
       public List<EDPlanInspeccion> ObtenerPlaneacionPorEmpresa(int id)
       {
           return ipi.ObtenerplaneacionPorEmpresa(id);
       }

       public List<EDInspeccion> ObtenerPlaneacionPorEmpresase(int id)
       {
           return ipi.ObtenerplaneacionPorEmpresase(id);
       }
       public List<EDConfiguracion> ObtenerConfiguracionesInspeccion()
       {
           return ipi.ObtenerConfiguracionesInspeccion();
       }
       public List<EDConfiguracion> ObtenerConfiguracionesPorIns(int idi)
       {
           return ipi.ObtenerConfiguracionesPorIns(idi);
       }
       public List<EDCondicionInsegura> ObtenerCondicionesPorInspeccion(int idinspeccion)
       {
           return ipi.ObtenerCondicionesPorInspeccion(idinspeccion);
       }

       public List<EDPlanAccionInspeccion> ObtenerPlanAccionInspeccion()
       {
           return ipi.ObtenerPlanAccionInspeccion();
       }
       public List<EDConfiguracion> ObtenerConfiguraciones() 
       {
           return ipi.ObtenerConfiguraciones();
       }

       public List<EDPlanAccionCorrectiva> ObtenerCorrectivas(int IdEmpresa)
       {
           return ipi.ObtenerCorrectivas(IdEmpresa);
       }

      public List<EDPlanAccionCorrectiva> ObtenerTodasCorrectivas(int IdEmpresa)
      {
          return ipi.ObtenerTodasCorrectivas(IdEmpresa);
      }
       public List<EDPlanAccionInspeccion> ObtenerInspeccionPorfechaEstado(int IdSede, string FechaIn, string FechaFin)
       {
           return ipi.ObtenerInspeccionPorfechaEstado(IdSede, FechaIn, FechaFin);
       }

        public EDCondicionInsegura GuardarCondicionesInspeccion(EDCondicionInsegura condicion)
       {
           return ipi.GuardarCondicionesInspeccion(condicion);
       }
        public EDCondicionInsegura EditarCondicion(EDCondicionInsegura condicion)
        {
            return ipi.EditarCondicion(condicion);
        }

        public EDPlanAccionInspeccion GuardarPlanAccion(EDPlanAccionInspeccion plan)
        {
            return ipi.GuardarPlanAccion(plan);
        }
        public List<EDPlanAccionCorrectiva> GuardarPlanAccionCorrectiva(List<EDPlanAccionCorrectiva> planescorrectivos)
        {
            return ipi.GuardarPlanAccionCorrectiva(planescorrectivos);
        }

        public List<EDInspeccion> ObtenerInspecciones(int IdSede, string DescripcionTipoInspeccion, DateTime? FechaInicialB, DateTime? FechaFinal)
        {
            return ipi.ObtenerInspecciones(IdSede, DescripcionTipoInspeccion, FechaInicialB, FechaFinal);
        }

    }
}
