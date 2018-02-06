
namespace SG_SST.Models.Organizacion
{
    using SG_SST.Models.Empresas;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    [Table("[Tbl_Empleado_Por_Tematica]")]
    public class EmpleadoPorTematica
    {
        [Key]
        public int Pk_Id_EmpleadoPorTematica { get; set; }

        [ForeignKey("EmpleadoTematica")]
        public int Fk_Id_Tematica { get; set; }

        [ForeignKey("Pk_Id_EmpleadoTematica")]
        public virtual EmpleadoTematica EmpleadoTematica { get; set; }

        [ForeignKey("Rol")]
        public int Fk_Id_Rol { get; set; }

        [ForeignKey("Pk_Id_Rol")]
        public virtual Rol Rol { get; set; }

    }
}
