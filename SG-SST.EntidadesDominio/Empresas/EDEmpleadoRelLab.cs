using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Empresas
{
    public class EDEmpleadoRelLab
    {
        [Display(Name = "Tipo de documento")]
        public string TipoDocumento { get; set; }
        [Display(Name = "Número de documento")]
        public string NumeroDocumento { get; set; }
        [Display(Name = "Primer apellido")]
        public string Apellido1 { get; set; }
        [Display(Name = "Segundo apellido")]
        public string Apellido2 { get; set; }
        [Display(Name = "Primer nombre")]
        public string Nombre1 { get; set; }
        [Display(Name = "Segundo nombre")]
        public string Nombre2 { get; set; }
        [Display(Name = "Fecha de nacimiento")]
        public DateTime FechaNacimiento { get; set; }
        public string strFechaNacimiento { get; set; }
        [Display(Name = "Estado")]
        public string Estado { get; set; }
        [Display(Name = "Ocupación")]
        public string Ocupacion { get; set; }
        [Display(Name = "Cargo")]
        public string Cargo { get; set; }
        [Display(Name = "E-mail")]
        public string Email { get; set; }
        [Display(Name = "Tipo cotizante")]
        public string TipoCotizante { get; set; }
        public int PageCount { get; set; }

    }
}
