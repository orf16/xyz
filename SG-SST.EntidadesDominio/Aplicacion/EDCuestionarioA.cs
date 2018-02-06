using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Aplicacion
{
    public class EDCuestionarioA
    {
        public List<EDBateriaCuestionario> ListaCuestionario { get; set; }
        public List<EDBateriaCuestionario> ListaCuestionarioInicial { get; set; }
        public EDBateriaGestion EDBateriaGestion { get; set; }
        public EDBateriaUsuario EDBateriaUsuario { get; set; }
    }
}
