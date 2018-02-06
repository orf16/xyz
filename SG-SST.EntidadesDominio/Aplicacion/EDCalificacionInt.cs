using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Aplicacion
{
    public class EDCalificacionInt
    {
        public int IdProductoCriterios { get; set; }

        public string Nombre_ServicioOProducto { get; set; } 
        public int idServicioProducto { get; set; }
        public string NombreCriterio { get; set; }
        public bool ddlViewBy { get; set; }
        public double califProducto { get; set; }
    }
}
