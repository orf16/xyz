using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Models.Planificacion
{
    [Table("Tbl_Criterios")]
    public partial class Criterio
    {
        public Criterio()
        {
            this.EvaluacionEstandaresMinimos = new  List<Evaluacion_Estandar_Minimo>();
        }
        [Key]
        public int Pk_Id_Criterio { get; set; }
        [ForeignKey("SubEstandar")]
        public int Fk_Id_SubEstandar { get; set; }
        [StringLength (4000)]
        public string Descripcion { get; set; }
        [StringLength(1000)]
        public string Descripcion_Corta { get; set; }
        [StringLength(10)]
        public string Numeral { get; set; }
        [StringLength(4000)]
        public string Marco_Legal { get; set; }
        [StringLength(4000)]
        public string Modo_Verificacion { get; set; }
        public decimal Valor { get; set; }


        [ForeignKey("Pk_Id_SubEstandar")]
        public virtual SubEstandar SubEstandar { get; set; }

        public virtual ICollection<Evaluacion_Estandar_Minimo> EvaluacionEstandaresMinimos { get; set; }
    }
}
