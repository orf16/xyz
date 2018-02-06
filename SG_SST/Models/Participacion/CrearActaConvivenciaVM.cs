using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SG_SST.Models.Participacion
{
    public class CrearActaConvivenciaVM
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
        public List<MiembrosActaConvivenciaVM> MiembrosActaConvivencia { get; set; }
        public List<ParticipantesActaConvivenciaVM> ParticipantesActaConvivencia { get; set; }
        public List<TemasActaConvivenciaVM> TemasActaConvivencia { get; set; }
        public List<TemasActaConvivenciaVM> TemasObservacionActaConvivencia { get; set; }
        public List<AccionesActaConvivenciaVM> AccionesActaConvivencia { get; set; }
    }

    public class MiembrosActaConvivenciaVM
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

    public class ParticipantesActaConvivenciaVM
    {
        public int Numero_Documento { get; set; }
        public string Nombre { get; set; }
        public int Fk_Id_Acta { get; set; }
    }

    public class TemasActaConvivenciaVM
    {
        public int PK_Id_TemaActa { get; set; }
        public string Tema { get; set; }
        public string Observaciones { get; set; }
        public int Fk_Id_Acta { get; set; }
    }

    public class AccionesActaConvivenciaVM
    {
        public int Pk_Id_AccionActaConvivencia { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime FechaProbable { get; set; }
        public string AccionARealizar { get; set; }
        public string Responsable { get; set; }
        public int Fk_Id_Acta { get; set; }
    }


}
