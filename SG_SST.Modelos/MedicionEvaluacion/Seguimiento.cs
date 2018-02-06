using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace SG_SST.Models.MedicionEvaluacion
{
    [Table("Tbl_Seguimiento")]
    public class Seguimiento
    {
        [Key]
        public int Pk_Id_Seguimiento { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Fecha de seguimiento")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public DateTime Fecha_Seg { get; set; }


        [DataType(DataType.MultilineText)]
        [MaxLength(1100)]
        public string Observaciones { get; set; }

        [StringLength(250)]
        public string NombreArchivoSeg { get; set; }

        [StringLength(1200)]
        public string RutaArchivoSeg { get; set; }

        //FK
        public int Fk_Id_Accion { get; set; }
        public virtual Accion Accion { get; set; }
    }
}
