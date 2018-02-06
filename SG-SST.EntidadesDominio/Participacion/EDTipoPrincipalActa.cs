using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Participacion
{
    public class EDTipoPrincipalActa
    {
        public int Id_TipoPrincipal { get; set; }
        public string DescripcionTipoPrincipal { get; set; }

        public List<EDTipoPrincipalActa> TiposPrincipalesActa { get; set; }
    }
}
