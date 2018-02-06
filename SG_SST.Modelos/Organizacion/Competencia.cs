
namespace SG_SST.Models.Organizacion
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Tbl_Competencia")]
    public class Competencia
    {
        [Key]
        public int Pk_Id_Competencia { get; set; }
        public int Id_rol { get; set; }
        public int Id_Cargo {get; set; }
        public string  Tematica {get; set;}
        public string Documento { get; set; }
        public int Id_SessionEmpresa { get; set; }
    }
}
