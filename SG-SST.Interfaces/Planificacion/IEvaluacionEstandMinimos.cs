using SG_SST.EntidadesDominio.Planificacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Interfaces.Planificacion
{
    public interface IEvaluacionEstandMinimos
    {
        List<EDCiclo> ObtenerCiclos();
        EDCiclo ObtenerStandares(int idCiclo);
        EDCiclo ObtenerEstandarPorCiclo(int idCiclo, int idCriterioActual, int idEmpres);
        EDCiclo ObtenerEstandarPorCiclo(int idCiclo, string Nit);
        EDEvaluacionEstandarMinimo GuardarEvaluacionEstandar(EDEvaluacionEstandarMinimo EvaluacionEstandar);
        int ObtenerCantidaEstdPorEvaluas(int idCiclo, string Nit);
        int ObtenerCantidaEstdPorEvaluas(int idCiclo, int idEmpresa);
        int ObtenerCantidadCriteriosPorEstandar(int idClico);
        EDValoracionFinal ObtenerCalificacion(string Nit);
        List<EDCiclo> ObtenerDatosInformeExcel(string Nit);
        List<EDCiclo> ObtenerDatosInicialesInformeExcel();
        List<EDActividad> ObtenerActividades(string Nit);
    }
}
