using SG_SST.EntidadesDominio.Empresas;
using SG_SST.EntidadesDominio.Revision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Interfaces.Revision
{
    public interface IRevision
    {
        EDSede ObtenerInformacionSede(int idsede);

        EDActaRevision GuardarActaRevision(EDActaRevision acta);

        EDActaRevision GuardarParticipanteRevision(EDActaRevision acta);

        List<EDParticipanteRevision> EliminarParticipanteRevision(string Documento, int PK_Id_Acta);

        List<EDActaRevision> ObtenerActasRevisionPorEmpresa(int id);

        EDActaRevision ObtenerActaRevisionPorId(int Id_Acta);

        List<EDParticipanteRevision> ObtenerParticipantesRevisionPorActa(int id_Acta);

        List<EDItemRevision> ObtenerItemsRevision();

        List<EDAgendaRevision> ObtenerTemas(int idActa);
        EDActaRevision GuardarTemaAgendaRevision(EDActaRevision acta);

        List<EDAgendaRevision> ObtenerAgendaPorActaRevision(int id);

        List<EDAgendaRevision> EliminarTemaAgendaRevision(int IdTema, int PK_Id_Acta);

        List<EDActaRevision> EliminarActaRevision(int PK_Id_Acta);

        string ValidarExisteFirma(int idEmpresa, string descripcion);

        EDAgendaRevision GuardarDesarrolloTemaAgendaRevision(EDAgendaRevision tema);

        EDAgendaRevision ObtenerTemaAgendaPorActaRevision(int id);

        EDAdjuntoAgendaRevision GuardarAdjuntoTemaAgendaRevision(EDAdjuntoAgendaRevision adjunto);

        List<EDAdjuntoAgendaRevision> EliminarAdjuntoTemaAgendaRevision(int idAdjunto, int idAgenda);

        bool ValidarExistePlanAccionCerradoRevision(int idActa);

        /// <summary>
        /// interfaces Robinson 
        /// </summary>
        /// <returns></returns>
        List<EDAgendaRevision> ObtenerTemasPlan(int idActa);
        EDPlanAccionRevision GrabarPlanAccionRevision(EDPlanAccionRevision planaacion);

        EDUsuario validarfirmareplegal(int idempresa);
        EDUsuario validarfirmaresponsable(int idempresa);
        List<EDPlanAccionRevision> ObtenerplanesporActa(int pkacta);
        EDActaRevision GuardaFirmaGerente(EDActaRevision actarevision);
        EDActaRevision ValidarFirmasRSRL(int idacta);
        bool CambiarEstadoRes(int idacta);
        bool CambiarEstadoRep(int idacta);
        bool EliminarPlanRevision(int idplaneacion);
        /// <summary>
        ///  fin interfaces Robinson 
        /// </summary>
        /// <returns></returns>
        ///
    }
}
