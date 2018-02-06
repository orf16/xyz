using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SG_SST.Models.Participacion
{
    public class CopasstVM
    {
        public int idSede { get; set; }
        public List<SelectListItem> Sedes { get; set; }
        public string NitEmpresaVM { get; set; }
        public string NombreSedeVM { get; set; }
        public string NombreEmpresaVM { get; set; }
        public string DireccionSedeVM { get; set; }
        public int PK_Id_Acta { get; set; }
        public int Consecutivo_Acta { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Fecha { get; set; }
        public string TemaReunion { get; set; }
        public string Conclusiones { get; set; }
        public int Fk_Id_Empresa { get; set; }
        public int Fk_Id_Sede { get; set; }
        public string NombreSede { get; set; }
        public int IdEmpresa { get; set; }
        public string NombreEmpresa { get; set; }
        public int Fk_Id_UsuarioSistema { get; set; }
        public string NombreUsuario { get; set; }
        public string NombreArchivo { get; set; }

        public List<CopasstVM> ActasCopasst { get; set; } 


    }
}