using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SG_SST.Models.Aplicacion
{
    [Table("Tbl_EPPSuministroEPP")]
    public class EPPSuministroEPP
    {
        [Key]
        public int Pk_Id_EPPSuministroEPP { get; set; }

        [Display(Name = "Cantidad")]
        [Required(ErrorMessage = "el campo {0} debe ser completado")]
        public int Cantidad { get; set; }

        [Display(Name = "Fecha")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "el campo {0} debe ser completado")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }


        //Relación - EPP
        [ForeignKey("EPP")]
        public int Fk_Id_EPP { get; set; }

        [ForeignKey("PK_AdmoEPP")]
        public virtual EPP EPP { get; set; }

        //Relación - Suministro EPP
        [ForeignKey("EPPSuministro")]
        public int Fk_Id_EPPSuministro { get; set; }

        [ForeignKey("PK_Id_EPPSuministro")]
        public virtual EPPSuministro EPPSuministro { get; set; }


    }
}
