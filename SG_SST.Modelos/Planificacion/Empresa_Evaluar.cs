using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SG_SST.Models.Empresas;

namespace SG_SST.Models.Planificacion
{
    [Table("Tbl_Empresas_Evaluar")]
    public partial class Empresa_Evaluar
    {
        public Empresa_Evaluar()
        {
            this.EmpresaAspectos = new List<Empresa_Aspecto>();
            this.EvaluacionEstandaresMinimos = new List<Evaluacion_Estandar_Minimo>();
        }
        [Key]
        public int Pk_Id_Empresa_Evaluar { get; set; }

        [ForeignKey("Empresa")]
        public int Fk_Id_Empresa { get; set; }
        [ForeignKey("Pk_Id_Empresa")]
        public virtual Empresa Empresa { get; set; }
        public int CodSede { get; set; }
        public string Responsable_SGSST { get; set; }

        [StringLength(20)]
        public string Elaborado_Por { get; set; }

        [StringLength(15)]
        public string Num_Licencia_SOSL { get; set; }
        public DateTime? Fecha_Diligencia_Eval_Inicial { get; set; }

        public DateTime? Fecha_Diligencia_Eval_EstMin { get; set; }

        public virtual ICollection<Empresa_Aspecto> EmpresaAspectos { get; set; }
        public virtual ICollection<Evaluacion_Estandar_Minimo> EvaluacionEstandaresMinimos { get; set; }
    }
}
