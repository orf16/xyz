using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Aplicacion
{
    public class EDProductoCriterio
    {
        public List<int> Pk_Id_Criterio1 { get; set; }
        public int Pk_id_Tipo_Servicio { get; set; }
        public string Tipo_Servicio { get; set; }
        public int Fk_Empresa { get; set; }
        public List<int> CritAnteriores { get; set; }
    }
}
