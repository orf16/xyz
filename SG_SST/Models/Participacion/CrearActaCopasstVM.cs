using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SG_SST.Models.Participacion
{
    public class CrearActaCopasstVM
    {
        public int? Consecutivo_Acta { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Fecha { get; set; }
        public string TemaReunion { get; set; }
        public string Conclusiones { get; set; }
        public int Fk_Id_Sede { get; set; }
        public int Fk_Id_Empresa { get; set; }
        public string NombreEmpresa { get; set; }
        public string NombreUsuario { get; set; }
        public int Numero_Documento { get; set; }
        public string Nombre { get; set; }
        public int Fk_Id_TipoPrioridadMiembro { get; set; }
        public List<SelectListItem> TiposPrioridadMiembros { get; set; }
        public int? Fk_Id_TipoPrincipal { get; set; }
        public List<SelectListItem> TiposPrincipales { get; set; }
        public string TipoRepresentante { get; set; }
        public int? PK_Id_TemaActa { get; set; }
        public string TemaOrdenDia { get; set; }
        public string Observaciones { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime FechaProbable { get; set; }
        public string AccionARealizar { get; set; }
        public string Responsable { get; set; }
        public int? Fk_Id_Acta { get; set; }
        public int? PK_Id_Acta { get; set; }
        public List<MiembrosActaCopasstVM> MiembrosActaCopasst { get; set; }
        public List<ParticipantesActaCopasstVM> ParticipantesActaCopasst { get; set; }
        public List<TemasActaCopasstVM> TemasActaCopasst { get; set; }
        public List<TemasActaCopasstVM> TemasObservacionActaCopasst { get; set; }
        public List<AccionesActaCopasstVM> AccionesActaCopasst { get; set; }
    }

    public class MiembrosActaCopasstVM
    {
        public int Numero_Documento { get; set; }
        public string Nombre { get; set; }
        public int Fk_Id_TipoPrioridadMiembro { get; set; }
        public string Des_TipoPrioridadMiembro { get; set; }
        public int? Fk_Id_TipoPrincipal { get; set; }
        public string Des_TipoPrincipal { get; set; }
        public string TipoRepresentante { get; set; }
        public int Fk_Id_Acta { get; set; }
    }

    public class ParticipantesActaCopasstVM
    {
        public int Numero_Documento { get; set; }
        public string Nombre { get; set; }
        public int Fk_Id_Acta { get; set; }
    }

    public class TemasActaCopasstVM
    {
        public int PK_Id_TemaActa { get; set; }
        public string Tema { get; set; }
        public string Observaciones { get; set; }
        public int Fk_Id_Acta { get; set; }
    }

    public class AccionesActaCopasstVM
    {
        public int Pk_Id_AccionActaCopasst { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime FechaProbable { get; set; }
        public string AccionARealizar { get; set; }
        public string Responsable { get; set; }
        public int Fk_Id_Acta { get; set; }
    }


}
