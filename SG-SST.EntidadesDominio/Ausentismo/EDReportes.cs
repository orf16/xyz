using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Ausentismo
{
    public class EDReportes
    {
        public int anio { get; set; }
        public int idOrigen { get; set; }
        public int IdEmpresaUsuaria { get; set; }
        public int idSede { get; set; }
        public int IdDepartamento { get; set; }
        public string nitEmpresa { get; set; }
        
        public string CONTINGENCIA { get; set; }
        public string Departamento { get; set; }
        public string Enfermedades { get; set; }
        public string Eps { get; set; }
        public string Ocupacion { get; set; }
        public string Sede { get; set; }
        public string Sexo { get; set; }
        public string Descripcion { get; set; }
        public string Evento { get; set; }
        public Nullable<decimal> Ene { get; set; }
        public Nullable<decimal> Feb { get; set; }
        public Nullable<decimal> Mar { get; set; }
        public Nullable<decimal> Abr { get; set; }
        public Nullable<decimal> May { get; set; }
        public Nullable<decimal> Jun { get; set; }
        public Nullable<decimal> Jul { get; set; }
        public Nullable<decimal> Ago { get; set; }
        public Nullable<decimal> Sep { get; set; }
        public Nullable<decimal> Oct { get; set; }
        public Nullable<decimal> Nov { get; set; }
        public Nullable<decimal> Dic { get; set; }
        public Nullable<decimal> Total { get; set; }
    }

    public class EDReportesGenerados
    {
        public string Contingencia { get; set; }
        public string Evento { get; set; }
        public int Mes { get; set; }
        public decimal Total { get; set; }
    }

    public class EDDatosReportes
    {
        public int idContigencia { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }        
    }
}
