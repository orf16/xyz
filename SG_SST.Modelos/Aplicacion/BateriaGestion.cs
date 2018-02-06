using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SG_SST.Models.Planificacion;
using SG_SST.Models.Empresas;
using System;

namespace SG_SST.Models.Aplicacion
{
    [Table("Tbl_BateriaGestion")]
    public class BateriaGestion
    {
        [Key]
        public int Pk_Id_BateriaGestion { get; set; }

        [DisplayName("Fecha de Registro")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public DateTime FechaRegistro { get; set; }

        [DisplayName("Fecha de Finalización")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public DateTime FechaFinalizacion { get; set; }


        [DisplayName("Finalizada")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public bool Finalizada { get; set; }

        //0-> Convocatoria 1-> Abierta 2->Finalizada
        [Required()]
        public int Estado { get; set; }

        [Required()]
        public int bateriaExtra { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public string TokenPublico { get; set; }

        [ForeignKey("Empresa")]
        public int Fk_Id_Empresa { get; set; }
        [ForeignKey("PK_Empresa")]
        public virtual Empresa Empresa { get; set; }

        [ForeignKey("Bateria")]
        public int Fk_Id_Bateria { get; set; }
        [ForeignKey("Pk_Id_Bateria")]
        public virtual Bateria Bateria { get; set; }


        public ICollection<BateriaUsuario> BateriaUsuarios { get; set; }

    }
}
