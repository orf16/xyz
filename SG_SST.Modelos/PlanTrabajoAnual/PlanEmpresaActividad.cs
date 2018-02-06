using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Models.PlanTrabajoAnual
{
    [Table("Tbl_Plan_Empresa_Actividad")]
    public class PlanEmpresaActividad
    {
        [Key]
        public int pk_id_actividad { get; set; }

        public int pk_plan_empresa { get; set; }
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
        public string FechaReProg { get; set; }
        public string FechaEje { get; set; }
        public string HoraProgIni { get; set; }
        public string HoraProgFin { get; set; }
        public string Estado { get; set; }

        public string NitEmpresa { get; set; }
    }
}
