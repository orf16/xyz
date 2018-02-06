

namespace SG_SST.Models.Politica
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using SG_SST.Models.Empresas;

     [Table("Tbl_OtrasInteracciones")]
    public class Mod_OtrasInteracciones
    {
        [Key]
        public int ID_OtrasInteraciones { get; set; }
        public int Nit_Empresa { get; set; }

        public string TipoDocumento_Archivo { get; set; }
        public string Archivo_OtrasInteracciones { get; set; }

  
        /// <summary>
        /// Obtiene y establece una clave foranea a empresa
        /// </summary>
        [ForeignKey("Empresa")]
        public int FK_Empresa { get; set; }

        /// <summary>
        /// Obtiene y establece un objeto de tipo empresa
        /// </summary>
        [ForeignKey("Pk_Id_Empresa")]
        public virtual Empresa Empresa { get; set; }
  

  
    }
 }