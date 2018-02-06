using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Planificacion
{
    public class EDObjetivoSST
    {
        public int Id_Objetivo_Empresa { get; set; }
        public int Id_Empresa { get; set; }
        public string Objetivo { get; set; }
        public string Meta { get; set; }
    }
}
