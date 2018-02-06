using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Aplicacion
{
    public class EDInspeccion
    {
        public int? EDpkinspeccion { get; set; }
        public int? EDIdInspeccion { get; set; }
        public int? EDfkEmpresa { get; set; }
        public string EDNitEmpresa { get; set; }
        public string EDDescribeEmpresa { get; set; }
        public int? EDfkIdPlaneacionInspeccion { get; set; }
        public string EDDescribeinspeccion { get; set; }
        public int? EDsede { get; set; }

        public string EDdescribesede { get; set; }

        public int? EDproceso { get; set; }
        public string EDdescribeProceso { get; set; }
        public string EDarealugar { get; set; }
        public string EDhora { get; set; }
        public string EDresponsableLugar { get; set; }

        public int? EDplanAccionInspeccion { get; set; }
        public string EDEstadoPlanAccionInspeccion { get; set; }
        public int? EstadoIdED { get; set; }

        public DateTime FechaFinPlanED { get; set; }

        public DateTime? FechaCierrePlanED { get; set; }

        public int? EDEstadoInspeccion { get; set; }
        public int? EDpkTipoI { get; set; }
        public string EDDescripcionTipoI { get; set; }
        public DateTime EDFechaRealizacion { get; set; }
        public int? EDConsecutivo { get; set; }
        public int? EDpkCondicion { get; set; }
        public string EDDescribeCondicion { get; set; }


        /// <summary>
        /// Datos Plan Inspeccion 
        /// </summary>
        public string EDResponsablePlaneacion { get; set; }
        public string EDFechaPlaneacion { get; set; }

        public DateTime EDFechaPlaneacionIns { get; set; }
        public int? EDidEmpresa { get; set; }
        public string EDEStadoPlaneacion { get; set; }
        public int? EDConsecutivoPlaneacion { get; set; }
       /// <summary>
       /// Fin Datos Planeacion Inspeccion
       /// </summary>


        public  List<EDAdmoEMH> EDElementos { get; set; }

       
        public List<EDConfiguracion> Configuraciones { get; set; }
        public List<EDAsistente> Asistentes { get; set; }

        public List<EDCondicionInsegura> CondicionesIns { get; set; }
        public List<EDInspeccion> Inspecciones { get; set; }
    }
}
