using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Aplicacion
{
    public class EDSeleccionYEvaluacion
    {
        public int idProveedorContratista { get; set; }
        public int PK_ProveedorPorNumeroCalificacion { get; set; }
        public string nameProveedor { get; set; }
        public string nitProveedor { get; set; }
        public DateTime fechapi { get; set; }
        public double calif { get; set; }
        public string observacion { get; set; }
        public int NumeroCalificion { get; set; }
        public int IdEmpresa { get; set; }
        //public List<int> idServicioProducto { get; set; }
        public List<EDCalificacionInt> ListaProCritPorCalf { get; set; }
        public List<string> ListaArchivos { get; set; }
        public string fecha { get; set; }
        public DateTime vigencia { get; set; }
    }
}
