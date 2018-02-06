using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using SG_SST.Models.Empresas;
using System.ComponentModel.DataAnnotations.Schema;

namespace SG_SST.Models.MedicionEvaluacion
{
    [Table("Tbl_Hallazgo")]
    public class Hallazgo
    {
        [Key]
        public int Pk_Id_Hallazgo { get; set; }

        [MaxLength(200)]
        [DisplayName("Norma aplicable")]
        public string Halla_Norma { get; set; }

        [DataType(DataType.MultilineText)]
        [MaxLength(200)]
        [DisplayName("Numeral de la norma que incumple")]
        public string Halla_Numeral { get; set; }

        [DataType(DataType.MultilineText)]
        [DisplayName("Descripción del hallazgo")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        [MaxLength(2000)]
        public string Halla_Descripcion { get; set; }

        // Campo Historico
        [MaxLength(200)]
        [DisplayName("Proceso del hallazgo")]
        public string Halla_Proceso { get; set; }

        //FK
        public int Fk_Id_Accion { get; set; }
        public virtual Accion Accion { get; set; }
        //FK
        public int Fk_Id_Proceso { get; set; }
        public virtual Proceso Proceso { get; set; }
    }
}
