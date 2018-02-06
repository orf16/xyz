using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Empresas
{
    public class EDMunicipio
    {
        public int IdMunicipio { get; set; }        
        public string NombreMunicipio { get; set; }        
        public string CodigoMunicipio { get; set; }        
        public int Nombre_Departamento { get; set; }        
        public EDDepartamento Departamento { get; set; }
        public List<EDSede> Sedes { get; set; }
        public EDSede Sede { get; set; }

    }
}
