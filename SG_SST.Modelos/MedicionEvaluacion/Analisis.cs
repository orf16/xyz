
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SG_SST.Models.MedicionEvaluacion
{
    [Table("Tbl_Analisis")]
    public class Analisis
    {
        [Key]
        public int Pk_Id_Analisis { get; set; }

        public int Id_Analisis { get; set; }

        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public short Tipo { get; set; }

        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        [MaxLength(200)]
        public string ValorTxt { get; set; }

        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public int Parent_Id { get; set; }

        //FK
        public int Fk_Id_Accion { get; set; }
        public virtual Accion Accion { get; set; }

    }
}
