using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Planificacion
{
    public class EDFiltrosPeligrosApp
    {
        public int id_Sede { get; set; }
        public int idMetodologia { get; set; }
        public int id_Proceso { get; set; }
        public string zonaLugar { get; set; }
        public string actividad { get; set; }
    }
}
