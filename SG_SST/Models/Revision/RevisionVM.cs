using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SG_SST.Models.Revision
{
    public class RevisionVM
    {
        public int PKActaRevision { get; set; }

        public int FKActa { get; set; }

        public string NombreActa { get; set; }

        public int NumActa { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime FechaCreacionActa { get; set; }

        public int FKSede { get; set; }

        public int PKSede { get; set; }

        public string NombreSede { get; set; }

        public List<SelectListItem> Sedes { get; set; }

        public int IdEmpresa { get; set; }

        public string NombreEmpresa { get; set; }

        public string NitEmpresa { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime FechaInicialRevision { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime FechaFinalRevision { get; set; }

        public string ElaboradaPor { get; set; }

        public bool ExisteFirmaGerenteGeneral { get; set; }

        public string FirmaGerenteGeneral { get; set; }

        public bool FirmaRepresentanteSGSST { get; set; }

        public string FirmaRepresentanteSGSSTFullPath { get; set; }

        public bool FirmaResponsableSGSST { get; set; }

        public string FirmaResponsableSGSSTFullPath { get; set; }

        public string LogoFullPath { get; set; }

        public string NombreParticipante { get; set; }

        public string DocumentoParticipante { get; set; }

        public string CargoParticipante { get; set; }

        public int FKItem { get; set; }

        public string Item { get; set; }

        public string DesarrolloItem { get; set; }


        public List<SelectListItem> Items { get; set; }

        public List<ParticipanteRevisionVM> ParticipantesActa { get; set; }

        public List<PlanAccionRevisionVM> PlanesAccionActa { get; set; }

        public List<AgendaRevisionVM> AgendaActa { get; set; }

        public List<ItemRevisionVM> Temas { get; set; }

        public List<ActaVM> Actas { get; set; }

    }

    public class ParticipanteRevisionVM
    {
        public int PKParticipanteRevision { get; set; }

        public string NombreParticipante { get; set; }

        public string DocumentoParticipante { get; set; }

        public string CargoParticipante { get; set; }

        public int FKActaRevision { get; set; }
    }

    public class PlanAccionRevisionVM
    {
        public int PKPlanAccion { get; set; }

        public int FKActa { get; set; }

        public string ActividadPlan { get; set; }

        public string ResponsablePlan { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime FechaPlan { get; set; }

        public int NumActa { get; set; }
    }

    public class AgendaRevisionVM
    {
        public int PKIdAgenda { get; set; }

        public int FKActaRevision { get; set; }

        public string TituloAgenda { get; set; }

        public string DesarrolloAgenda { get; set; }

        public int ConsecutivoActaRevVM { get; set; }

        public List<AdjuntoAgendaRevisionVM> AdjuntosAgenda { get; set; }
    }

    public class AdjuntoAgendaRevisionVM
    {
        public int PKAdjuntoAgendaRevision { get; set; }

        public int FKAgendaRevision { get; set; }

        public string NombreArchivo { get; set; }

        public string NombreArchivoFullPath { get; set; }

    }

    public class ItemRevisionVM
    {
        public int PKItemRevision { get; set; }

        public string TemaItem { get; set; }

        public bool TemaAgregado { get; set; }
    }

    public class ActaVM
    {
        public int PKActa { get; set; }

        public string NombreActa { get; set; }

        public int NumActa { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime FechaCreacionActa { get; set; }

        public int FKSede { get; set; }

        public int PKSede { get; set; }

        public List<SelectListItem> Sedes { get; set; }

        public int IdEmpresa { get; set; }

    }

}