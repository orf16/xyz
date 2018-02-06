using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.PlanTrabajoAnual
{
    public class EDPlanEmpresa
    {
        public int pk_id_plan_empresa { get; set; }
        public int IdSede { get; set; }
        public string FechaDesde { get; set; }
        public string FechaHasta { get; set; }
        public string Vigencia { get; set; }

        /// <summary>
        /// ACTIVIDADES
        /// </summary>
        public string ObjetivosDescripcion { get; set; }
        public string ObjetivosMetas { get; set; }
        public string Actividad { get; set; }
        public string Responsable { get; set; }


        /// <summary>
        /// RECURSOS
        /// </summary>
        public string RecursosHumanos { get; set; }
        public string RecursosTecnologico { get; set; }
        public string RecursosFinanciero { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string FechaProg { get; set; }
        public string HoraProgIni { get; set; }
        public string HoraProgFin { get; set; }
        public string Estado { get; set; }
        public string PorcentajeEjecucion { get; set; }
        public string RepresentanteLegal { get; set; }
        public string RepresentanteSGSST { get; set; }
    }
}
