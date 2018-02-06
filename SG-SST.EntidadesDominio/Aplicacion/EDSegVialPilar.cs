using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Aplicacion
{
    public class EDSegVialPilar
    {
        public int Pk_Id_SegVialPilar { get; set; }
        public string Descripcion { get; set; }
        public int Version { get; set; }
        public decimal Valor_Ponderado { get; set; }
        public List<EDSegVialParametro> ListaParametros { get; set; }
        public decimal ValorObtenido { get; set; }
        public decimal ValorResultado { get; set; }
        public bool Mostrar { get; set; }
    }
}
