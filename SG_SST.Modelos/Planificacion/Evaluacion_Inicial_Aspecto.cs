using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Models.Planificacion
{
    [Table("Tbl_Evaluacion_Inicial_Aspectos")]
    public partial class Evaluacion_Inicial_Aspecto
    {
        [Key]
        public int Pk_Id_Evaluacion_Inicial_Aspecto{ get; set; }

        [ForeignKey("Empresa_Aspecto")]
        public int Fk_Id_Empresa_Aspecto { get; set; }        
        public decimal Valor_Valoracion { get; set; }

        [ForeignKey("Pk_Id_Empresa_Aspecto")]
        public virtual Empresa_Aspecto Empresa_Aspecto { get; set; }        
    }
}
