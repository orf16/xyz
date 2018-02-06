using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Planificacion
{
    public class EDPruebasPClinicasDx
    {
        
        public int idPruebasPClinicas { get; set; }
        
        public string Prueba_P_Clinica { get; set; }
       
        public int Trabajadores_Con_Prueba_P { get; set; }

        public double porcentajePruebaP { get; set; }
    }
}
