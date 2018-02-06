using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Models.Planificacion
{
    [Table("Tbl_Empresa_Aspectos")]
    public partial class Empresa_Aspecto
    {
        public Empresa_Aspecto()
        {
            this.EvaluacionInicialAspectos = new List<Evaluacion_Inicial_Aspecto>();
        }

        [Key]
        public int Pk_Id_Empresa_Aspecto { get; set; }
        [ForeignKey("Empresa_Evaluar")]
        public int Fk_Id_Empresa_Evaluar { get; set; }
        [ForeignKey("Aspecto")]
        public int Fk_Id_Aspecto { get; set; }

        [ForeignKey("Pk_Id_Empresa_Evaluar")]
        public virtual Empresa_Evaluar Empresa_Evaluar { get; set; }
        [ForeignKey("Pk_Id_Aspecto")]
        public virtual Aspecto Aspecto { get; set; }

        public virtual ICollection<Evaluacion_Inicial_Aspecto> EvaluacionInicialAspectos { get; set; }
    }
}
