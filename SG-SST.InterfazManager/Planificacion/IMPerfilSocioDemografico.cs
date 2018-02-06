using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SG_SST.Interfaces.Planificacion;
using SG_SST.Repositorio.Planificacion;

namespace SG_SST.InterfazManager.Planificacion
{
    public class IMPerfilSocioDemografico
    {
        public static IPerfilSocioDemografico PerfilSocioDemografico()
        {
            return new PerfilSocioDemograficoManager();         

        }






    }
}
