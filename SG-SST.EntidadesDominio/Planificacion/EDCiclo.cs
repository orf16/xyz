using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Planificacion
{
    public class EDCiclo
    {
        public int Id_Ciclo { get; set; }
        public string Nombre { get; set; }
        public decimal Porcentaje_Max { get; set; }
        public int StandPorEvaluar { get; set; }
        public int CantidadCriterios { get; set; }
        public EDEstandar Estandar { get; set; }
        public List<EDEstandar> Estandares { get; set; }

        //para reportes
        public decimal PorcenRespondido { get; set; }
        public decimal PorcenObtenido { get; set; }
    }
}
