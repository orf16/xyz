using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Ausentismo
{
    public class EDConfiguracion
    {
        public int IdConfiguracion { get; set; }
        public string IdEmpresa { get; set; }
        public int Anio { get; set; }
        public int Mes { get; set; }
        public int DiasLaborales { get; set; }
        public decimal HorasLaborales { get; set; }
        public decimal NumeroTrabajadoresXT { get; set; }
        public decimal DiasTrabajadosDTM { get; set; }
        public decimal HorasHombreHTD { get; set; }
        public decimal HorasExtrasNHE { get; set; }
        public decimal HorasAusentismoNHA { get; set; }
        public DateTime FechaModificacion { get; set; }
        public decimal Total { get; set; }

        public int IdDiasLaborables { get; set; }
    }   
}
