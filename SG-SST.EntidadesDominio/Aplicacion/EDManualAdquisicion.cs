using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Aplicacion
{
    public class EDManualAdquisicion
    {
        public int Id_Manuales_Bienes { get; set; }
        public string Nombre_Manual { get; set; }
        public int Fk_Empresa { get; set; }
    }
}
