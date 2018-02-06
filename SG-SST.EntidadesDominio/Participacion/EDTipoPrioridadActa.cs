using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Participacion
{
    public class EDTipoPrioridadActa
    {
        public int Id_TipoPrioridadMiembro { get; set; }
        public string DescripcionTipoMiembro { get; set; }

        public List<EDTipoPrioridadActa> TiposPrioridadesActas { get; set; }
    }
}
