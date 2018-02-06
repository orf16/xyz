using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Enumeraciones
{
    public class EnumPlanificacion
    {
        public enum ValoracionStandares
        {
            CumpleTotalMente = 1,
            NoCumple = 2,
            NoAplica = 3,
            Justifica = 4,
            NoJustifica = 5
        }

        public enum ValoracionEvalInicial
        {
            Cumple = 1,
            NoCumple = 2,
            CumpleParcial = 3
        }
    }
}
