using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Aplicacion
{
    public class EDSegVialParametro
    {
        public int Pk_Id_SegVialParametro { get; set; }
        public int Numero { get; set; }
        public string Numeral { get; set; }
        public string ParametroDef { get; set; }
        public decimal Valor_Parametro { get; set; }
        public int Fk_Id_SegVialPilar { get; set; }
        public decimal Valor_obtenido { get; set; }
        public bool Mostrar { get; set; }
        public int Fk_Id_Empresa { get; set; }
        public bool disabled { get; set; }
        public List<EDSegVialDetalle> ListaDetalles { get; set; }
    }
}
