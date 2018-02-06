using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
//using System.Web.Mvc;

namespace SG_SST.EntidadesDominio.Aplicacion
{
    public class EDBateriaUsuario
    {
        public int Pk_Id_BateriaUsuario { get; set; }
        public string Nombre { get; set; }
        public string NumeroIdentificacion { get; set; }
        public string TipoDocumento { get; set; }
        public string CorreoElectronico { get; set; }
        public int Id_Empresa { get; set; }
        public string TokenPrivado { get; set; }
        public int Fk_Id_BateriaGestion { get; set; }
        public int TipoConv { get; set; }
        public int EstadoEnvio { get; set; }
        public int NumeroIntentos { get; set; }
        public string RegistroOperacion { get; set; }
        //[AllowHtml]
        public string MailBody { get; set; }
        public string NombreEncuesta { get; set; }
        public string NombreEncuestaTotal { get; set; }
        public string NombreEncuestaInforme { get; set; }
        public string CheckPag9 { get; set; }
        public string CheckPag10 { get; set; }
        public string DocumentoDigitado { get; set; }
        public string ConfirmacionParticipacion { get; set; }
        public string link { get; set; }

        public Nullable<System.DateTime> FechaEnvio { get; set; }

        public Nullable<System.DateTime> FechaRespuesta { get; set; }
        public List<EDBateriaResultado> ListaResultados { get; set; }
        public List<EDBateriaDominio> Listadominios { get; set; }
        public EDBateriaInicial BateriaInicial { get; set; }


        public decimal Puntaje { get; set; }
        public decimal PuntajeTrans { get; set; }
        public decimal NivelRiesgo { get; set; }
        public string NivelRiesgoDesc { get; set; }

        public double FactorTransformacion { get; set; }






        public string Edad { get; set; }
        public string NombreEvaluador { get; set; }
        public string IdEvaluador { get; set; }
        public string Profesion { get; set; }
        public string Postgrado { get; set; }
        public string TarjetaProfesional { get; set; }
        public string Licencia { get; set; }
        public string Observaciones { get; set; }
        public string Recomendaciones { get; set; }
        public Nullable<System.DateTime> FechaExpedicion { get; set; }
    }
}
