using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Ausentismo
{
    public class EDAlertas
    {
        public Nullable<System.DateTime> FechaRegistro { get; set; }
        public string Documento { get; set; }
        public string Departamento { get; set; }
        public string Municipio { get; set; }
        public string Contingencia { get; set; }
        public string Diagnostico { get; set; }
        public decimal Dias_Ausencia { get; set; }
        public decimal costo { get; set; }
    }

    /// <summary>
    /// Parámetros para realizar la consulta de alertas de ausentismo.
    /// </summary>
    public class EDAlertaAusentismoParametros
    {
        public int IdEmpresaUsuaria { get; set; }
        public int AnioGestion { get; set; }
    }

    /// <summary>
    /// Representa una unidad de reporte para las alertas de ausentismo.
    /// </summary>
    public class EDAlertaAusentismo
    {
        public string EmpleadoNombre { get; set; }
        public string DocumentoPersona { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int IdContingencia { get; set; }
        public int DiasAusencia { get; set; }
        public int Id_Diagnostico { get; set; }
        public string DiagnosticoDescripcion { get; set; }
        public List<EDAlertaAusentismo> ListaAlertas { get; set; }
    }
}
