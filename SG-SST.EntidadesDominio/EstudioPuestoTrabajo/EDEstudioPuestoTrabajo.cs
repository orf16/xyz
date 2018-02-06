using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.EstudioPuestoTrabajo
{
    public class EDEstudioPuestoTrabajo
    {
        public int IdEstudioPuestoTrabajo { get; set; }
        public string NumeroIdentificacion { get; set; }

        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public string Nombre1 { get; set; }
        public string Nombre2 { get; set; }
        public string Cargo { get; set; }
        public int IdSede { get; set; }
        public int IdProceso { get; set; }
        public int IdDiagnostico { get; set; }
        public string Diagnostico { get; set; }
        public int IdObjetivoAnalisis { get; set; }
        public int IdTipoAnalisis { get; set; }
        public DateTime FechaAnalisis { get; set; }
        public string FechaAnalisisStr { get; set; }
        public string Result { get; set; }
        public List<EDSeguimientoEstudioPuestoTrabajo> listaSeguimiento { get; set; }
        public List<EDArchivoEstudioPuestoTrabajo> listaArchivos { get; set; }
    }
}
