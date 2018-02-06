using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Usuario
{
    public class EDUsuarioPorAprobar
    {
        public int IdUsuarioPorAprobar { get; set; }
        public string TipoDocumentoEmpresa { get; set; }
        public string NumDocumentoEmpresa { get; set; }
        public string RazonSocialEmpresa { get; set; }
        public string DepartamentoSedePpalEmpresa { get; set; }
        public string MunicipioSedePpalEmpresa { get; set; }
        public string TipoDocumentoAfi { get; set; }
        public string NumDocumentoAfi { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Telefono { get; set; }
        public int RolUsuario { get; set; }
        public string NombreRol { get; set; }
        public string Correo { get; set; }
        public bool Aprobado { get; set; }
        public int CausalRechazo { get; set; }
        public string NombreCausalRechazo { get; set; }
        public DateTime PeriodoInactividad { get; set; }
        public List<PreguntasSeguridadSeleccionadas> PreguntasSeguridadSeleccionadas { get; set; }
    }
    public class PreguntasSeguridadSeleccionadas
    {
        public int CodPreguntaSeguridad { get; set; }
        public string RespuestaSeguridad { get; set; }
    }
}
