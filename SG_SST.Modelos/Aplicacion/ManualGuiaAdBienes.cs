
namespace SG_SST.Models.Aplicacion
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using SG_SST.Models.Empresas;
    using System.Data.Entity.SqlServer;
    using System;


    [Table("Tbl_ManualGuiaAdBienes")]
    public class ManualGuiaAdBienes
    {
        /// <summary>
        /// Obtiene y establece la clave primaria de los Manuales de adquisición de bienes.
        /// </summary>
        [Key]
        public int PK_ManualGuiaAdBienes { get; set; }

        /// <summary>
        /// Obtiene y establece el nombre del Manual.
        /// </summary>
        public string Nombre_Manual { get; set; }

        /// <summary>
        /// Obtiene y establece la clave foranea a la tabla  empresa.
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
