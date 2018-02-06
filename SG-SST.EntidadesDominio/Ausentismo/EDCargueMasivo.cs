using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Ausentismo
{
    public class EDCargueMasivo
    {
        public int Id_Empresa_Usuaria { get; set; }
        public string path { get; set; }
        public string Message { get; set; }
        public string NitEmpresa { get; set; }
    }
}
