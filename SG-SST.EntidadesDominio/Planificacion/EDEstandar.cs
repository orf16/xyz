using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Planificacion
{
    public class EDEstandar
    {
        public int Id_Estandar { get; set; } 
        public string Descripcion { get; set; }
        public decimal Porcentaje_Max { get; set; }
        public EDSubEstandar SubEstandar { get; set; }
        public List<EDSubEstandar> SubEstandares { get; set; }
        public decimal Calificacion { get; set; }
    }
}
