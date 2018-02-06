using SG_SST.Models.Empresas;
using SG_SST.Models.Organizacion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Models.Aplicacion
{
    [Table("Tbl_EPPSuministro")]
    public class EPPSuministro
    {
        [Key]
        public int Pk_Id_SuministroEPP { get; set; }

        [Display(Name = "Cedula del Trabajador")]
        [Required(ErrorMessage = "el campo {0} debe ser completado")]
        [MaxLength(100)]
        public string CedulaTrabajador { get; set; }

        [Display(Name = "Nombre del Trabajador")]
        [Required(ErrorMessage = "el campo {0} debe ser completado")]
        [MaxLength(500)]
        public string NombreTrabajador { get; set; }

        [Display(Name = "Fecha")]
        [DataType(DataType.Date)]
        [Required()]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }

        //Relacion - Proceso
        [Required(ErrorMessage = "El campo proceso es obligatorio")]
        [ForeignKey("Proceso")]
        public int Fk_Id_Proceso { get; set; }
        [ForeignKey("Pk_Id_Proceso")]
        public virtual Proceso Proceso { get; set; }
        //Relacion - Sede
        [Required(ErrorMessage = "El campo sede es obligatorio")]
        [ForeignKey("Sede")]
        public int Fk_Id_Sede { get; set; }
        [ForeignKey("Pk_Id_Sede")]
        public virtual Sede Sede { get; set; }
        //Relacion - Cargo
        [Required(ErrorMessage = "El campo cargo es obligatorio")]
        [ForeignKey("Cargo")]
        public int Fk_Id_Cargo { get; set; }
        [ForeignKey("Pk_Id_Cargo")]
        public virtual Cargo Cargo { get; set; }
        //Relacion - Empresa
        [ForeignKey("Empresa")]
        public int Fk_Id_Empresa { get; set; }
        [ForeignKey("PK_Empresa")]
        public virtual Empresa Empresa { get; set; }

        public ICollection<EPPSuministroEPP> EPPSuministroEPPs { get; set; }
    }
}
