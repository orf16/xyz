using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SG_SST.Models.Login
{
    public class UsuarioSessionModel
    {
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Este campo debe contener solo números")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string ClaveAcceso { get; set; }
        public int IdEmpresa { get; set; }
        public string  NitEmpresa { get; set; }
        public string RazonSocialEmpresa { get; set; }
        public int CantidadDiasLaborales { get; set; }
        public string Documento { get; set; }
        public bool PrimerAcceso { get; set; }
        public string SiglaTipoDocumentoEmpresa { get; set; }
        public string SiglaTipoDocumentoEmpleado { get; set; }
    }
}