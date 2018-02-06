using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Planificacion
{
    public class EDSubEstandar
    {
        public int Id_SubEstandar { get; set; }
        public string Descripcion { get; set; }
        public string Descripcion_Corta { get; set; }
        public int Procentaje_Max { get; set; }
        public EDCriterio Criterio { get; set; }
        public decimal? CalTotal { get; set; }        
        public List<EDCriterio> Criterios { get; set; }
    }
}
