using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SG_SST.Models.Planificacion;
using SG_SST.Models.Empresas;

namespace SG_SST.Models.Aplicacion
{
    [Table("Tbl_SegVialResultado")]
    public class SegVialResultado
    {
        [Key]
        public int Pk_Id_SegVialResultado { get; set; }

        [DisplayName("Aplica")]
        public bool Aplica { get; set; }
        public short Aplica_s { get; set; }
        [DisplayName("Existencia")]
        public bool Existencia { get; set; }
        public short Existencia_s { get; set; }
        [DisplayName("Responde")]
        public bool Responde { get; set; }
        public short Responde_s { get; set; }

        
        [DisplayName("Valor Obtenido")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public decimal ValorObtenido { get; set; }

        [MaxLength(500)]
        [DisplayName("Observaciones")]
        public string Observaciones { get; set; }



        [Required(ErrorMessage = "Debe ingresar el valor del Plan Vial")]
        [ForeignKey("PlanVial")]
        public int Fk_Id_PlanVial { get; set; }
        [ForeignKey("Pk_Id_SegVial")]
        public virtual PlanVial PlanVial { get; set; }

        [Required(ErrorMessage = "Debe ingresar el valor del detalle del parametro")]
        [ForeignKey("SegVialDetalle")]
        public int Fk_Id_SegVialParametroDetalle { get; set; }
        [ForeignKey("Pk_Id_SegVialParametroDetalle")]
        public virtual SegVialDetalle SegVialDetalle { get; set; }
    }
}
