using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Ausentismo
{
    public class EDIndicadores
    {
        public int Idcontingencia { get; set; }
        public string Contingencia { get; set; }
        public Variables Ene { get; set; }
        public Variables Feb { get; set; }
        public Variables Mar { get; set; }
        public Variables Abr { get; set; }
        public Variables May { get; set; }
        public Variables Jun { get; set; }
        public Variables Jul { get; set; }
        public Variables Ago { get; set; }
        public Variables Sep { get; set; }
        public Variables Oct { get; set; }
        public Variables Nov { get; set; }
        public Variables Dic { get; set; }
        public Nullable<int> Mes { get; set; }
        public Nullable<int> TotalMes { get; set; }
    }

    public class ResultadoConfiguracion
    {
        public Nullable<int> Mes { get; set; }
        public Nullable<decimal> TotalMes { get; set; }
        public Nullable<decimal> NumeroTrabajadores { get; set; }
    }

    public class Variables
    {
        public decimal VariableIF { get; set; }
        public decimal VariableIS { get; set; }
        public decimal VariableILI { get; set; }
        public decimal Tasa { get; set; }

    }
    public class EDIndicadoresGenerados
    {
        public int IdContingencia { get; set; }
        public string Contingencia { get; set; }
        public int Mes { get; set; }
        public int NumeroEventos { get; set; }
        public decimal DiasPorEventos { get; set; }
    }

    public class EDAcumuladoTotalContingencias
    {
        public int Mes { get; set; }
        public int EventosPorMes { get; set; }
        public decimal DiasAusenciaPorMes { get; set; }
        public decimal VariableIF { get; set; }
        public decimal VariableIS { get; set; }
        public decimal VariableILI { get; set; }
        public decimal Tasa { get; set; }
        public decimal HorasTrabajadas { get; set; }
        public decimal NumeroTrabajadores { get; set; }
        public TotalAcumulado TotalPeriodo { get; set; }
    }
    public class TotalAcumulado
    {
        public decimal TotalVariableIF { get; set; }
        public decimal TotalVariableIS { get; set; }
        public decimal TotalVariableILI { get; set; }
        public decimal TotalTasa { get; set; }
        public decimal TotalHorasTrabajadas { get; set; }
        public decimal TotalNumeroTrabajadores { get; set; }
    }
}
