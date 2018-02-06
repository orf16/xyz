
namespace SG_SST.InterfazManager.Planificacion
{
    using SG_SST.Interfaces.Planificacion;
    using SG_SST.Repositorio.Planificacion;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class IMDxGralCondicionesDeSalud
    {
        public static IDxGralCondicionesDeSalud DxGralCondicionesDeSalud() 
        {
            return new DxGralCondicionesDeSaludManager();
        }
    }
}
