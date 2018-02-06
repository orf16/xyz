using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Aplicacion
{
    public class EDBateria
    {
        public int Pk_Id_Bateria { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Fecha_publicacion { get; set; }
        public string TiposAplicacion { get; set; }
        public string ModalidadesAplicacion { get; set; }
        public string Poblacion { get; set; }
        public string Objetivo { get; set; }
        public string Baremacion { get; set; }
        public string TipoInstrumento { get; set; }
        public string NumeroItems { get; set; }
        public string DuracionAplicacion { get; set; }
        public string Materiales { get; set; }
    }
}
