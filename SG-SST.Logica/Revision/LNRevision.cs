using SG_SST.EntidadesDominio.Empresas;
using SG_SST.EntidadesDominio.Revision;
using SG_SST.Interfaces.Revision;
using SG_SST.InterfazManager.Revision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Logica.Revision
{
    public class LNRevision
    {
        private static IRevision em = IMRevision.Revision();

        public EDSede ObtenerInformacionSede(int idsede)
        {
            return em.ObtenerInformacionSede(idsede);
        }

        public EDActaRevision GuardarActaRevision(EDActaRevision acta)
        {
            EDActaRevision mp = em.GuardarActaRevision(acta);
            return mp;
        }

        public EDActaRevision GuardarParticipanteRevision(EDActaRevision acta)
        {
            EDActaRevision mp = em.GuardarParticipanteRevision(acta);
            return mp;
        }

        public List<EDParticipanteRevision> EliminarParticipanteRevision(string Documento, int PK_Id_Acta)
        {
            return em.EliminarParticipanteRevision(Documento, PK_Id_Acta);
        }

        public List<EDActaRevision> ObtenerActasRevisionPorEmpresa(int empresa)
        {
            List<EDActaRevision> mp = em.ObtenerActasRevisionPorEmpresa(empresa);
            return mp;
        }

        public EDActaRevision ObtenerActaRevisionPorId(int Id_Acta)
        {
            EDActaRevision mp = em.ObtenerActaRevisionPorId(Id_Acta);
            return mp;
        }

        public List<EDParticipanteRevision> ObtenerParticipantesRevisionPorActa(int id_Acta)
        {
            return em.ObtenerParticipantesRevisionPorActa(id_Acta);
        }

        public List<EDItemRevision> ObtenerItemsRevision()
        {
            return em.ObtenerItemsRevision();
        }

        public List<EDAgendaRevision> ObtenerTemas(int IdActa)
        {
            return em.ObtenerTemas(IdActa);
        }
        public EDActaRevision GuardarTemaAgendaRevision(EDActaRevision acta)
        {
            return em.GuardarTemaAgendaRevision(acta);
        }

        public List<EDAgendaRevision> ObtenerAgendaPorActaRevision(int id)
        {
            return em.ObtenerAgendaPorActaRevision(id);
        }

        public List<EDAgendaRevision> EliminarTemaAgendaRevision(int IdTema, int PK_Id_Acta)
        {
            return em.EliminarTemaAgendaRevision(IdTema, PK_Id_Acta);
        }

        public List<EDActaRevision> EliminarActaRevision(int PK_Id_Acta)
        {
            return em.EliminarActaRevision(PK_Id_Acta);
        }

        public string ValidarExisteFirma(int idEmpresa, string descripcion)
        {
            return em.ValidarExisteFirma(idEmpresa, descripcion);
        }

        public EDAgendaRevision GuardarDesarrolloTemaAgendaRevision(EDAgendaRevision tema)
        {
            return em.GuardarDesarrolloTemaAgendaRevision(tema);
        }

        public EDAgendaRevision ObtenerTemaAgendaPorActaRevision(int id)
        {
            return em.ObtenerTemaAgendaPorActaRevision(id);
        }

        public EDAdjuntoAgendaRevision GuardarAdjuntoTemaAgendaRevision(EDAdjuntoAgendaRevision adjunto)
        {
            return em.GuardarAdjuntoTemaAgendaRevision(adjunto);
        }

        public  List<EDAdjuntoAgendaRevision> EliminarAdjuntoTemaAgendaRevision(int idAdjunto, int idAgenda)
        {
            return em.EliminarAdjuntoTemaAgendaRevision(idAdjunto, idAgenda);
        }

        public bool ValidarExistePlanAccionCerradoRevision(int idActa)
        {
            return em.ValidarExistePlanAccionCerradoRevision(idActa);
        }

        /// <summary>
        /// Logica de Negocio por Robinson. 
        /// </summary> 
        /// <returns></returns>
        public List<EDAgendaRevision> ObtenerTemasPlan(int IdActa)
        {
            return em.ObtenerTemasPlan(IdActa);
        }

        public EDPlanAccionRevision GrabarPlanAccionRevision(EDPlanAccionRevision planaccionrev)
        {
            return em.GrabarPlanAccionRevision(planaccionrev);
        }

        public List<EDPlanAccionRevision> ObtenerplanesporActa(int Pkacta)
        {
            return em.ObtenerplanesporActa(Pkacta);
        }

        public EDActaRevision GuardaFirmaGerente(EDActaRevision actarevision)
        {
            return em.GuardaFirmaGerente(actarevision);
        }

        public EDUsuario validarfirmareplegal(int Idempresa)
        {
            return em.validarfirmareplegal(Idempresa);
        }


        public EDUsuario validarfirmaresponsable(int Idempresa)
        {
            return em.validarfirmaresponsable(Idempresa);
        }

        public EDActaRevision ValidarFirmasRSRL(int idacta)
        {
            return em.ValidarFirmasRSRL(idacta);
        }

        public bool CambioEstadoRes(int idacta)
        {
            return em.CambiarEstadoRes(idacta);
        }
        public bool CambioEstadoRep(int idacta)
        {
            return em.CambiarEstadoRep(idacta);
        }
        public bool EliminarPlanRevision(int pkplan)
        {
            return em.EliminarPlanRevision(pkplan);
        }
        /// <summary>
        /// fin Logica de Negocio por Robinson. 
        /// </summary> 
        /// <returns></returns>
    }
}
