
namespace SG_SST.InterfazManager.Aplicacion
{
    using SG_SST.Interfaces.Aplicacion;
    using SG_SST.Repositorio.Aplicacion;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public class IMAdquisicionBienes
    {
        public static IAdquicisionBienes AdquisicionBienes()
        {
            return new AdquisicionBienesManager();
        }
    }
}
