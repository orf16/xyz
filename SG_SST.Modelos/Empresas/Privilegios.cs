

namespace SG_SST.Models.Empresas
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Collections.Generic;

    [Table("Tbl_Privilegios")]
    public class Privilegios
    {
        [Key]

        public int Pk_Id_Privilegio { get; set; }

        public string Descripcion { get; set; }

        public ICollection<PrivilegiosPorRol> PrivilegiosporRoles { get; set; }
    }
}