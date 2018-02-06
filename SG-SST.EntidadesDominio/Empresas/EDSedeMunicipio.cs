using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Empresas
{
    public class EDSedeMunicipio
    {        
        public int Fk_id_Sede { get; set; }        
        public virtual EDSede Sede { get; set; }        
        public int Fk_Id_Municipio { get; set; }       
        public EDMunicipio Municipio { get; set; }
    }
}
