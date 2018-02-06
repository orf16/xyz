using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Empresas
{
    public class EDRol
    {
        public int Pk_Id_Rol { get; set; }
        public string Descripcion { get; set; }
        public int? Fk_Id_Empresa { get; set; }
    }
}
