using SG_SST.EntidadesDominio.Ausentismo;
using System.Collections.Generic;

namespace SG_SST.Interfaces.Ausentismo
{
    public interface IReportes
    {
        List<EDDatosReportes> ReporteContingencia(EDReportes edReporte);
        List<EDDatosReportes> ReporteDepartamento(EDReportes edReporte);
        List<EDReportesGenerados> ReporteEvento(EDReportes edReporte);
        List<EDDatosReportes> ReporteDiasAusentismoEnfermedades(EDReportes edReporte);
        List<EDReportesGenerados> ReporteDiasEps(EDReportes edReporte);
        List<EDReportesGenerados> ReporteVincualcion(EDReportes edReporte);
        List<EDDatosReportes> ReporteSede(EDReportes edReporte);
        List<EDReportesGenerados> ReporteSexo(EDReportes edReporte);
        List<EDReportesGenerados> ReporteCostoContingencia(EDReportes edReporte);
        List<EDDatosReportes> ReporteDiasAusentismoPorProceso(EDReportes edReporte);
        List<EDReportesGenerados> ReporteCantidadAusenciasPorOcupacion(EDReportes edReporte);
        List<EDReportesGenerados> ReporteCantiEventPorEnfermedades(EDReportes edReporte);
        List<EDReportesGenerados> ReporteCantidadAusenGrupEtarios(EDReportes edReporte);
        int Obteneranoinicioempresa(string Nit);
    }
}
