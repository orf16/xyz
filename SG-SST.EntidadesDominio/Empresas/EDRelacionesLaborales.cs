using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Empresas
{
    public class EDRelacionesLaborales
    {
        public string TipoTercero { get; set; }
        public string NitEmpresa { get; set; }
        public string RazonSocial { get; set; }
        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public string Nombre1 { get; set; }
        public string Nombre2 { get; set; }
        public string FechaNacimiento { get; set; }
        public string Ocupacion { get; set; }
        public string Cargo { get; set; }
        public string Email { get; set; }

        public string idEmpresa { get; set; }

        public string Mensaje_validacion { get; set; }

        public bool Rta { get; set; }


    }
}
