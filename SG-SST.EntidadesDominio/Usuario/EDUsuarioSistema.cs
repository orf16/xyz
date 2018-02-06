using System;
using System.Collections.Generic;

namespace SG_SST.EntidadesDominio.Usuario
{
    public class EDUsuarioSistema
    {
        public int IdUsuarioSistema { get; set; }
        public int CodEmpresa { get; set; }
        public int TipoDocumentoEmpresa { get; set; }
        public string TipoDocumentoSiglaEmpresa { get; set; }
        public string DocumentoEmpresa { get; set; }
        public string RazonSocial { get; set; }
        public int TipoDocumento { get; set; }
        public string TipoDocumentoSigla { get; set; }
        public string Documento { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public int IdRol { get; set; }
        public string ClaveSalt { get; set; }
        public string ClaveHash { get; set; }
        public string Correo { get; set; }
        public string Clave { get; set; }
        public bool Activo { get; set; }
        public bool PrimerAcceso { get; set; }
        public DateTime PeriodoInactividad { get; set; }
        public int DiasLaborables { get; set; }
        public List<PreguntasSeguridadSeleccionada> PreguntasSeguridad { get; set; }
    }
    public class PreguntasSeguridadSeleccionada
    {
        public int CodPreguntaSeguridad { get; set; }
        public string NombrePregunta { get; set; }
        public string RespuestaSeguridad { get; set; }
    }
}
