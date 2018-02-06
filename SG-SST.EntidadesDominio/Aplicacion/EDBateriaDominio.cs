using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Aplicacion
{
    public class EDBateriaDominio
    {
        public int Pk_Id_BateriaDimension { get; set; }
        public string Nombre { get; set; }
        public List<EDBateriaDimension> ListaDimensiones { get; set; }
        public decimal Puntaje { get; set; }
        public decimal PuntajeTrans { get; set; }
        public decimal NivelRiesgo { get; set; }
        public string NivelRiesgoDesc { get; set; }

        public double FactorTransformacion { get; set; }
    }
}
