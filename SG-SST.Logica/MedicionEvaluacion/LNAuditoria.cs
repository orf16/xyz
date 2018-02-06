using SG_SST.EntidadesDominio.MedicionEvaluacion;
using SG_SST.Interfaces.MedicionEvaluacion;
using SG_SST.InterfazManager.MedicionEvaluacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Logica.MedicionEvaluacion
{
    public class LNAuditoria
    {
        private static IAuditoria aud = IMAuditoria.Auditoria();
        public bool GuardarPrograma(EDAuditoriaPrograma EDAuditoriaPrograma)
        {
            bool ProbarGuardar = aud.GuardarPrograma(EDAuditoriaPrograma);
            return ProbarGuardar;
        }
        public bool CrearAuditoria(EDAuditoria EDAuditorias)
        {
            bool ProbarGuardar = aud.CrearAuditoria(EDAuditorias);
            return ProbarGuardar;
        }
        public bool GuardarActividad(EDAuditoriaActividad EDAuditoriaActividad)
        {
            bool ProbarGuardar = aud.GuardarActividad(EDAuditoriaActividad);
            return ProbarGuardar;
        }
        public bool GuardarActividadPlan(EDActividadAuditoria EDAuditoriaActividad)
        {
            bool ProbarGuardar = aud.GuardarActividadPlan(EDAuditoriaActividad);
            return ProbarGuardar;
        }
        public bool GuardarListaVer(EDAuditoriaVerificacion EDAuditoriaVerificacion)
        {
            bool ProbarGuardar = aud.GuardarListaVer(EDAuditoriaVerificacion);
            return ProbarGuardar;
        }
        public bool ActualizarPrograma(EDAuditoriaPrograma EDAuditoriaPrograma)
        {
            bool ProbarGuardar = aud.ActualizarPrograma(EDAuditoriaPrograma);
            return ProbarGuardar;
        }
        public bool ActualizarConclusiones(EDAuditoriaInforme EDAuditoriaInforme)
        {
            bool ProbarGuardar = aud.ActualizarConclusiones(EDAuditoriaInforme);
            return ProbarGuardar;
        }
        public bool ActualizarAuditoria(EDAuditoria EDAuditorias)
        {
            bool ProbarGuardar = aud.ActualizarAuditoria(EDAuditorias);
            return ProbarGuardar;
        }
        public bool ActualizarActividad(EDAuditoriaActividad EDAuditoriaActividad)
        {
            bool ProbarGuardar = aud.ActualizarActividad(EDAuditoriaActividad);
            return ProbarGuardar;
        }
        public bool ActualizarActividadPlan(EDActividadAuditoria EDAuditoriaActividad)
        {
            bool ProbarGuardar = aud.ActualizarActividadPlan(EDAuditoriaActividad);
            return ProbarGuardar;
        }
        public bool ActualizarListaVer(EDAuditoriaVerificacion EDAuditoriaVerificacion)
        {
            bool ProbarGuardar = aud.ActualizarListaVer(EDAuditoriaVerificacion);
            return ProbarGuardar;
        }
        public List<EDAuditoriaPrograma> ConsultaTodosProgramas(int Pkempresa)
        {
            List<EDAuditoriaPrograma> NuevaLista = aud.ConsultaTodosProgramas(Pkempresa);
            return NuevaLista;
        }
        public List<EDAuditoria> ConsultaAuditorias(int IdPrograma, string NitEmpresa)
        {
            List<EDAuditoria> NuevaLista = aud.ConsultaAuditorias(IdPrograma, NitEmpresa);
            return NuevaLista;
        }
        public EDAuditoriaPrograma Consultaprograma(int IdPrograma)
        {
            EDAuditoriaPrograma EDProgramaConsulta = aud.Consultaprograma(IdPrograma);
            return EDProgramaConsulta;
        }
        public EDAuditoriaPrograma ConsultaprogramaEmpresa(int IdPrograma, int Idempresa)
        {
            EDAuditoriaPrograma EDProgramaConsulta = aud.ConsultaprogramaEmpresa(IdPrograma, Idempresa);
            return EDProgramaConsulta;
        }
        public EDAuditoriaInforme ConsultaConclusiones(int IdAuditoria)
        {
            EDAuditoriaInforme EDAuditoriaInformeConsulta = aud.ConsultaConclusiones(IdAuditoria);
            return EDAuditoriaInformeConsulta;
        }
        public List<EDAuditoriaPrograma> ConsultaProgramaFiltros(string Año, string Nombre, string Sede, int IdEmpresa)
        {
            List<EDAuditoriaPrograma> ListaEDPrograma = aud.ConsultaProgramaFiltros(Año, Nombre, Sede, IdEmpresa);
            return ListaEDPrograma;
        }
        public EDAuditoria ConsultaAuditoria(int IdAuditoria, string NitEmpresa)
        {
            EDAuditoria EDAuditoriaConsulta = aud.ConsultaAuditoria(IdAuditoria, NitEmpresa);
            return EDAuditoriaConsulta;
        }
        public EDAuditoriaActividad ConsultaAuditoriaActividades(int IdAuditoria)
        {
            EDAuditoriaActividad EDAuditoriaConsulta = aud.ConsultaAuditoriaActividades(IdAuditoria);
            return EDAuditoriaConsulta;
        }
        public EDAuditoriaActividad ConsultaAuditoriaActividad(int IdActividad)
        {
            EDAuditoriaActividad EDAuditoriaConsulta = aud.ConsultaAuditoriaActividad(IdActividad);
            return EDAuditoriaConsulta;
        }
        public EDActividadAuditoria ConsultaAuditoriaActividadPlan(int IdActividad)
        {
            EDActividadAuditoria EDAuditoriaConsulta = aud.ConsultaAuditoriaActividadPlan(IdActividad);
            return EDAuditoriaConsulta;
        }
        public EDAuditoriaVerificacion ConsultaListavers(int IdAuditoria)
        {
            EDAuditoriaVerificacion EDAuditoriaVerificacionConsulta = aud.ConsultaListavers(IdAuditoria);
            return EDAuditoriaVerificacionConsulta;
        }
        public EDAuditoriaVerificacion ConsultaListaver(int IdListaVer)
        {
            EDAuditoriaVerificacion EDAuditoriaVerificacionConsulta = aud.ConsultaListaver(IdListaVer);
            return EDAuditoriaVerificacionConsulta;
        }
        public bool EliminarActividad(EDAuditoriaActividad EDAuditoriaActividad)
        {
            bool ProbarEliminar = aud.EliminarActividad(EDAuditoriaActividad);
            return ProbarEliminar;
        }
        public bool EliminarListaVer(EDAuditoriaVerificacion EDAuditoriaVerificacion)
        {
            bool ProbarEliminar = aud.EliminarListaVer(EDAuditoriaVerificacion);
            return ProbarEliminar;
        }
        public EDAuditoriaInforme ConsultaInforme(int IdAuditoria, string NitEmpresa)
        {
            EDAuditoriaInforme EDAuditoriaInformeConsulta = aud.ConsultaInforme(IdAuditoria, NitEmpresa);
            return EDAuditoriaInformeConsulta;
        }
        public bool EliminarPrograma(int IdPrograma, int IdEmpresa)
        {
            bool ProbarEliminar = aud.EliminarPrograma(IdPrograma, IdEmpresa);
            return ProbarEliminar;
        }
        public bool EliminarPlanAuditoria(int IdAuditoria)
        {
            bool ProbarEliminar = aud.EliminarPlanAuditoria(IdAuditoria);
            return ProbarEliminar;
        }



    }
}
