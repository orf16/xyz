using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Planificacion
{
    public class EDPeligroIdentificadoApp
    {
        /// <summary>
        /// obtiene y establece la clasificacion del peligro
        /// </summary>
        public int idClasificacionPeligro { get; set; }

        /// <summary>
        /// obtiene y establece la descripcio de la clasificacion 
        /// </summary>
        public string descripcionClasificacion { get; set; }

        /// <summary>
        /// obtiene  y establece la cantidad de clasificacion de peligros por matriz de la sede
        /// </summary>
        public int cantidadDeClasifiacion { get; set; }


    }
}
