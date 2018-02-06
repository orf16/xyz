using SG_SST.EntidadesDominio.Planificacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Aplicacion
{
    public class EDCondicionInsegura
    {
        public int EDpkCondicion { get; set; }
        public string EDDescribeCondicion { get; set; }
        public string EDUbicacionespecifica { get; set; }
        public string EDRiesgopeligro { get; set; }
        public string EDClasificacionRiesgo { get; set; }
        public string EDEvidenciacondicion { get; set; }
        public int EDConfiguracioncondicion { get; set; }
        public string EDescribePrioridad { get; set; }
        public int EDDiasDesde { get; set; }
        public int EDDiasHasta { get; set; }

        /// <summary>
        /// Datos Plan Accion Inspeccion 
        /// </summary>

        public int EDEstadoPlanAccion { get; set; }

        public string EDOtraClasePeligro { get; set; }
        public int EDEstadoCondicion { get; set; }

        public int EDPkInspeccion { get; set; }

        public List<EDTipoDePeligro> Peligros { get; set; }

        public List<EDClasificacionDePeligro> ClasificacionPeligros { get; set; }

        public List<EDConfiguracion> Configuraciones { get; set; }

    }


}
