using SG_SST.Interfaces.Planificacion;
using SG_SST.Repositorio.Planificacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.InterfazManager.Planificacion
{
    public class IMObjetivoSST
    {
        public static IObjetivoSST Objetivo()
        {
            return new ObjetivoSSTManager();
        }
    }
}
