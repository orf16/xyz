using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SG_SST.Models.Participacion
{
    public class SeguimientoActaConvivenciaVM
    {
        public int PK_Id_Seguimiento { get; set; }
        public int Consecutivo_Evento { get; set; }
        public DateTime Fecha { get; set; }
        public string NombreParteInvolucrada { get; set; }
        public string CompromisosAdquiridos { get; set; }
        public string Observaciones { get; set; }
        public int? PK_Id_Acta { get; set; }
        public int? Fk_Id_Acta { get; set; }
        public int? IdSede { get; set; }
        public int? Pk_Id_Compromiso { get; set; }
        public string CompromisoPendiente { get; set; }
        public List<CompromisosPendientesVM> CompromisosPendientes { get; set; }
    }

    public class CompromisosPendientesVM
    {
        public int Pk_Id_Compromiso { get; set; }
        public string CompromisoPendiente { get; set; }
        public int FK_Id_Seguimiento { get; set; }
        public int PK_Id_Acta { get; set; }
        public int UsuarioSistema { get; set; }
        public string NombreUsuario { get; set; }
    }
}