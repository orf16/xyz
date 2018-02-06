

namespace SG_SST.Models.Organizacion
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using SG_SST.Models.Empresas;
    using System.Data.Entity.SqlServer;
    using System;


    [Table("Tbl_Documentacion_Organizacion")]
   public class Documentacion_Organizacion
    {
         [Key]
         public int ID_Documentacion_Org { get; set; }
        
         [ForeignKey("TipoModulo_Organizacion")]
         public int FK_TipoModuloOrganizacion { get; set; }

         /// <summary>
         /// Obtiene y establece un objeto de tipo empresa
         /// </summary>
         [ForeignKey("ID_TipoModulo_Organizacion")]
         public virtual TipoModulo_Organizacion TipoModulo_Organizacion { get; set; } 

         public string NombreArchivo_Documentacion { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
         public DateTime FechaModificacion_Documentacion { get; set; }

         [ForeignKey("Empresa")]
         public int FK_Empresa { get; set; }

        /// <summary>
        /// Obtiene y establece un objeto de tipo empresa
        /// </summary>
        [ForeignKey("Pk_Id_Empresa")]
        public virtual Empresa Empresa { get; set; }

    }
}
