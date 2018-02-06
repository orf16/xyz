using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Aplicacion
{
    public class EDGestionDelCambio
    {

        public int PK_GestionDelCambio { get; set; }
        public DateTime Fecha { get; set; }

        public string DescripcionDeCambio { get; set; }

        public int FK_Tipo_De_Peligro { get; set; }

        public int FK_Clasificacion_De_Peligro { get; set; }


        public string RequisitoLegal { get; set; }

        public string Recomendaciones { get; set; }
      
        public DateTime FechaEjecucion { get; set; }
    
        public DateTime FechaSeguimiento { get; set; }

        public string Otro { get; set; }

        public int FK_Id_Rol { get; set; }

        public int fkempresa { get; set; }

        public int fkClasificacionPeligro { get; set; }

    }
}
