using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Empresas
{
    public class EDSitioIncidente
    {
        public int Pk_Id_Sitio_Incidente { get; set; }
        public string Nombre_Sitio { get; set; }
        /// <summary>
        /// Equivale a la opción de selección "Es otro"
        /// </summary>
        public bool EsOtro { get; set; }
    }
}
