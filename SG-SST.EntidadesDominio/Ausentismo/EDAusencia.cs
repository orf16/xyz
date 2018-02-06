using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Ausentismo
{
    public class EDAusencia
    {
        public int IdAusencia { get; set; }
        public int IdAusenciaPadre { get; set; }
        public string NombrePersona { get; set; }
        public string Documento { get; set; }

        public int IdEmpresaUsuaria { get; set; }
        public string IdEmpresa { get; set; }
        public int idDepartamento { get; set; }
        public int idMunicipio { get; set; }
        public int IdContingencia { get; set; }
        public int IdDiagnostico { get; set; }
        public int IdSede { get; set; }
        public int IdProceso { get; set; }
        public DateTime FechaInicio { get; set; }
        public string strFechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string strFechaFin { get; set; }
        public decimal DiasAusencia { get; set; }
        public decimal Costo { get; set; }
        public decimal FactorPrestacional { get; set; }
        public string Observaciones { get; set; }
        public string Sexo { get; set; }
        public string TipoVinculacion { get; set; }
        public int IdOcupacion { get; set; }

        public Nullable<int> Edad { get; set; }
        public string Eps { get; set; }
        public string Result { get; set; }
        public int consecutivoPadre { get; set; }
    }
    public class Resultados
    {
        public string Departamento { get; set; }
        public string Municipio { get; set; }
        public string NombrePersona { get; set; }
        public string Documento { get; set; }
        public string TipoRegistro { get; set; }
        public int IdAusencias { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }
        public string nombreRegional { get; set; }
        public string Detalle { get; set; }
        public System.DateTime fechainicio { get; set; }
        public System.DateTime fechafin { get; set; }
        public decimal diasausencia { get; set; }
        public string Descripcion { get; set; }
        public decimal costo { get; set; }
        public decimal FactorPrestacional { get; set; }
        public string Observaciones { get; set; }
    }
}
