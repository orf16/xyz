using SG_SST.EntidadesDominio.MedicionEvaluacion;
using SG_SST.Models.MedicionEvaluacion;
using System.Collections.Generic;

namespace SG_SST.Interfaces.MedicionEvaluacion
{
    public interface IAuditoria
    {
        bool GuardarPrograma(EDAuditoriaPrograma ProgramaAuditoria);
        bool CrearAuditoria(EDAuditoria EDAuditorias);
        bool GuardarActividad(EDAuditoriaActividad EDAuditoriaActividad);
        bool GuardarActividadPlan(EDActividadAuditoria EDAuditoriaActividad);
        bool GuardarListaVer(EDAuditoriaVerificacion EDAuditoriaVerificacion);
        bool ActualizarPrograma(EDAuditoriaPrograma ProgramaAuditoria);
        bool ActualizarAuditoria(EDAuditoria EDAuditorias);
        bool ActualizarActividad(EDAuditoriaActividad EDAuditoriaActividad);
        bool ActualizarActividadPlan(EDActividadAuditoria EDAuditoriaActividad);
        bool ActualizarListaVer(EDAuditoriaVerificacion EDAuditoriaVerificacion);
        bool ActualizarConclusiones(EDAuditoriaInforme EDAuditoriaInforme);
        List<EDAuditoriaPrograma> ConsultaTodosProgramas(int Pkempresa);
        List<EDAuditoria> ConsultaAuditorias(int IdPrograma, string NitEmpresa);
        EDAuditoriaPrograma Consultaprograma(int IdPrograma);
        EDAuditoriaPrograma ConsultaprogramaEmpresa(int IdPrograma, int Idempresa);
        EDAuditoria ConsultaAuditoria(int IdAuditoria, string NitEmpresa);
        EDAuditoriaActividad ConsultaAuditoriaActividades(int IdAuditoria);
        EDAuditoriaActividad ConsultaAuditoriaActividad(int IdActividad);
        EDActividadAuditoria ConsultaAuditoriaActividadPlan(int IdActividad);
        EDAuditoriaVerificacion ConsultaListavers(int IdAuditoria);
        EDAuditoriaVerificacion ConsultaListaver(int IdListaVer);
        EDAuditoriaInforme ConsultaConclusiones(int IdAuditoria);
        bool EliminarActividad(EDAuditoriaActividad EDAuditoriaActividad);
        bool EliminarListaVer(EDAuditoriaVerificacion EDAuditoriaVerificacion);
        EDAuditoriaInforme ConsultaInforme(int IdAuditoria, string NitEmpresa);
        bool EliminarPrograma(int IdPrograma, int IdEmpresa);
        bool EliminarPlanAuditoria(int IdAuditoria);
        List<EDAuditoriaPrograma> ConsultaProgramaFiltros(string Año, string Nombre, string Sede, int IdEmpresa);
    }
}
