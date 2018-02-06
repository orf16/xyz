using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Ausentismo
{
    public class EDAfiliado
    {
        public string TipoDoc { get; set; }
        public string Documento { get; set; }
        public string TipoVinculacion { get; set; }
        public string Nombre1 { get; set; }
        public string Nombre2 { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Ocupacion { get; set; }
        public string Cargo { get; set; }
        public string Email { get; set; }
        public decimal Salario { get; set; }
    }
}
