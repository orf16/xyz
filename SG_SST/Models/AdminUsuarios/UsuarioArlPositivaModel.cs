using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SG_SST.Models.AdminUsuarios
{
    public class UsuarioArlPositivaModel
    {
        public int IdUsuarioArlPositiva { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string TipoDocumentoEmpresa { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Este campo debe contener solo números")]
        public string DocumentoEmpresa { get; set; }
        public string RazonSocialEmpresa { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string DeptoSedePpalEmpresa { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string MunicipioSedePpalEmpresa { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string TipoDocumento { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Este campo debe contener solo números")]
        public string Documento { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Telefono { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [EmailAddress(ErrorMessage = "Debe Ingresar un correo válido")]
        public string EmailPersona { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int IdRolSeleccionado { get; set; }
        public string NombreRolSeleccionado { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string EstadoSeleccionado { get; set; }
        public List<SelectListItem> TiposDocumento { get; set; }
        public List<SelectListItem> RolesSistema { get; set; }
        public List<SelectListItem> Departamentos { get; set; }
        public List<SelectListItem> Municipios { get; set; }
        public List<SelectListItem> Estados { get; set; }
    }
}