using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Empresas
{
    public class EDSede
    {
        public int IdSede { get; set; }
        public int IdEmpresa { get; set; }
        public string NombreSede { get; set; }
        public string DireccionSede { get; set; }
        public string Sector { get; set; }
        public int IdMunicipio { get; set; }
        public string NombreMunici { get; set; }
        public int IdDepto { get; set; }    
        public string NombreDepto { get; set; }
        public string CodigoMunicipio { get; set; }
        public string Telefono { get; set; }

        public string NombreEmpresa { get; set; }
        public string IDEmpresa { get; set; }              
    }
}
