using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Aplicacion
{
    public class EDServicioProducto
    {
        public int idServicioProducto { get; set; }
        public string DescripcionProducto { get; set; }
        public List<EDCriteriosSST> CriterioLista { get; set; }

        public List<int> selectProdCrit { get; set; }
    }
}
