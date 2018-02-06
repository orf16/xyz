using System;
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
    [Table("Tbl_PlanVial")]
    public class PlanVial
    {
        [Key]
        public int Pk_Id_SegVial { get; set; }

        [DisplayName("Consecutivo")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public int Id_Consecutivo { get; set; }

        [DisplayName("Fecha de Registro")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public DateTime Fecha_Registro { get; set; }

        [DisplayName("Estado")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public bool Estado { get; set; }

        [DisplayName("Version")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public int Version { get; set; }


        [Required(ErrorMessage = "Debe ingresar el valor de la sede")]
        [ForeignKey("Sede")]
        public int Fk_Id_Sede { get; set; }

        [ForeignKey("Pk_Id_Sede")]
        public virtual Sede Sede { get; set; }


        public ICollection<SegVialResultado> SegVialResultados { get; set; }

    }
}
