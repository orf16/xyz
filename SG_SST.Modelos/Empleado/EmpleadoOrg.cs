using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SG_SST.Models
{
    [Table("Tbl_EmpleadoOrg")]
    public class EmpleadoOrg
    {

        [Key]
        public int Id_EmpleadoOrg { get; set; }        

        [Display(Name="Jefe Inmediato")]
        public string Jefe_Inmediato{ get; set; }

        [Required(ErrorMessage="El Cargo es Requerido")]
        [Display(Name = "Cargo Empleado")]
        public string Cargo_Empleado { get; set; }



        [ForeignKey("OrgChart")]
        public int? Fk_Id_EmpleadoOrg { get; set; }

        [ForeignKey("Id_EmpleadoOrg")]
        public virtual EmpleadoOrg OrgChart { get; set; }


        [ForeignKey("Organigrama")]
        public int Fk_Id_Organigrama{ get; set; }

        [ForeignKey("Pk_Id_Organigrama")]
        public virtual Organigrama Organigrama { get; set; }




    }
}