using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;

namespace SG_SST.Models.Usuarios
{
    public class UsuarioModel
    {
        public int IdUsuarioSistema { get; set; }
        public string Documento { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Correo { get; set; }
        public string Clave { get; set; }
        public List<SelectListItem> Empresas { get; set; }
        public int Id_Empresa { get; set; } 
    }
}