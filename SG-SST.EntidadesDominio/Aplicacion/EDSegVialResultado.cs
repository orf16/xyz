using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Aplicacion
{
    public class EDSegVialResultado
    {
        public int Pk_Id_SegVialResultado { get; set; }
        public bool Aplica { get; set; }
        public bool Existencia { get; set; }
        public bool Responde { get; set; }
        public decimal ValorObtenido { get; set; }
        public string Observaciones { get; set; }
        public int Fk_Id_PlanVial { get; set; }
        public int Fk_Id_SegVialParametroDetalle { get; set; }

        public short Aplica_s { get; set; }
        public short Existencia_s { get; set; }
        public short Responde_s { get; set; }


        //Campos Adicionales para el formulario(Visualizacion)
        public EDSegVialPilar Pilar { get; set; }
        public EDSegVialParametro Parametro { get; set; }
        public EDSegVialDetalle DetalleParametro { get; set; }
        public EDPlanVial PlanVial { get; set; }

        
    }
}
