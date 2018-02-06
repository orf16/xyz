

namespace SG_SST.Models.Organizacion
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using SG_SST.Models.Empresas;


     [Table("Tbl_TipoModulo_Organizacion")]
    public class TipoModulo_Organizacion
    {
          [Key]
          public int ID_TipoModulo_Organizacion { get; set; }
          public string Descripcion_ModuloOrg { get; set; }

    }
}
