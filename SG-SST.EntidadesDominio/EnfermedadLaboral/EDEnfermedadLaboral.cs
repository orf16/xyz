using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.EnfermedadLaboral
{
    public class EDEnfermedadLaboral
    {
        //public string NumeroDocumento { get; set; }
        //public string NombreTrabajador { get; set; }
        //public DateTime FechaNacimiento { get; set; }
        //public string Geneero { get; set; }
        //public string Direccion { get; set; }
        //public string Telefono { get; set; }
        //public string Fax { get; set; }
        //public int IdDepartamento { get; set; }
        //public string Departamento { get; set; }
        //public int IdMunicipio { get; set; }
        //public string Municipio { get; set; }
        //public string Cargo { get; set; }
        public int IdEfermedadLaboral { get; set; }
        public int CodigoEmpleado { get; set; }
        public string Diagnostico { get; set; }
        public string DiagnosticoCIIE10 { get; set; }
        public string DocumentoFurel { get; set; }
        public string CartaEPS { get; set; }
        public DateTime FechaDocumentosCalificarEPS { get; set; }
        public List<string> TiposDocumentosEnviadosEPS { get; set; }
        public List<InstanciaRegistrada> InstanciasRegistradas { get; set; }
        //public string DescripcionCIIE10 { get; set; }
    }

    public class InstanciaRegistrada
    {
        public int IdInstancia { get; set; }
        public string Nombre { get; set; }
        public int EstadoInstancia { get; set; }
        public string QuienCalifica { get; set; }
        public DateTime FechaCalificacion { get; set; }
    }
}
