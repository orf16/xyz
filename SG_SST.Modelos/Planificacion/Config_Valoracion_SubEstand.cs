using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Models.Planificacion
{
    [Table("Tbl_Config_Valoracion_SubEstandares")]
    public class Config_Valoracion_SubEstand
    {
        public Config_Valoracion_SubEstand()
        {
            this.EvalucionEstandaresMinimos = new List<Evaluacion_Estandar_Minimo>();
        }

        [Key]
        public int Pk_Id_Config_Valoracion_SubEstand { get; set; }
        [ForeignKey("SubEstandar")] 
        public int Fk_Id_SubEstandar { get; set; }
        [ForeignKey("Valoracion_Criterio")]  
        public int Fk_Id_Valoracion_Criterio { get; set; }
        public int? Id_Dpendiente { get; set; }
        public decimal Valor { get; set; }

        //[ForeignKey("Pk_Id_SubEstandar")]
        public virtual SubEstandar SubEstandar { get; set; }
        [ForeignKey("Pk_Id_Valoracion_Criterio")]
        public virtual Valoracion_Criterio Valoracion_Criterio { get; set; }

        public virtual ICollection<Evaluacion_Estandar_Minimo> EvalucionEstandaresMinimos { get; set; }
    }
}
