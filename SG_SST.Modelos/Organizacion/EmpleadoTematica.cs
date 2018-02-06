
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
    [Table("Tbl_Empleado_Tematica")]
    public class EmpleadoTematica
    {
        [Key]
        public int Pk_Id_EmpleadoTematica { get; set; }

        public int Numero_Documento { get; set; }

        public string Nombre_Empleado { get; set; }

        public string Apellidos_Empleado { get; set; }
        public string Cargo_Empleado { get; set; }

        public string Email_Persona { get; set; }

        public int NitEmpresa { get; set; }

    }
}
