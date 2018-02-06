using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Planificacion
{
   public class EDCondicionesRiesgoPerfil
    {
        public int PKCondicionesRiesgoPerfil { get; set; }

        public string tipoPeligro { get; set; }

     
        public int FK_Tipo_De_Peligro { get; set; }
        public int FK_Clasificacion_De_Peligro { get; set; }
        public int FK_Clasificacion_De_PeligroE { get; set; }

   
        public string ClasificacionPeligro { get; set; }
        public string OtroPeligro { get; set; }

        public string tiempoExpos { get; set; }

        public int FKPerfilSocioDemografico { get; set; }

        public string descripcionPeligro { get; set; }

  

        public string Otro { get; set; }


    }
}
