using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Models.PlanTrabajoAnual
{
    [Table("Tbl_Plan_Empresa")]
    public class PlanEmpresa
    {
        [Key]
        public int pk_id_plan_empresa { get; set; }
        public int IdSede { get; set; }
        public string FechaDesde { get; set; }
        public string FechaHasta { get; set; }
        public string Vigencia { get; set; }
        public string PorcentajeEjecucion { get; set; }
        public string RepresentanteLegal { get; set; }
        public string RepresentanteSGSST { get; set; }
        public string NitEmpresa { get; set; }

    }
}
