using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Aplicacion
{
    public class EDGestiondelcambioVista
    {

        public int PK_GestionDelCambio { get; set; }
        public DateTime Fecha { get; set; }
        public string DescripcionDeCambio { get; set; }
        public string RequisitoLegal { get; set; }
        public string Recomendaciones { get; set; }
        public DateTime FechaEjecucion { get; set; }
        public DateTime FechaSeguimiento { get; set; }
        public string Descripcion_Clase_De_Peligro { get; set; }
        public string Descripcion { get; set; }//rol
        public string Descripcion_Del_Peligro { get; set; }//TIPO PELIGRO 
        public string Otro { get; set; }
        public int fkClasificacionPeligro { get; set; }
    }
}
