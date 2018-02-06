using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Planificacion
{
    public class EDPruebasClinicasDx
    {
      
        public int idPruebasClinicas { get; set; }
   
        public string Prueba_Clinica { get; set; }

        public int Trabajadores_Con_Prueba { get; set; }

        public double porcentajePrueba { get; set; }
    }
}
