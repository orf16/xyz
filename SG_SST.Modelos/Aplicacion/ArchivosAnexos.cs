
namespace SG_SST.Models.Aplicacion
{
    using SG_SST.Models.Empresas;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    [Table("Tbl_Archivos_Anexos")] 
    public class ArchivosAnexos
    {
        /// <summary>
        /// Obtiene y establece la clave primaria de los Archivos Anexados a la calificacion de los criterios.
        /// </summary>  
        [Key]
        public int PK_Archivos_Anexos { get; set; }
        /// <summary>
        /// Obtiene y establece la ruta para el guardado de los Archivos Anexados a la calificacion de los criterios.
        /// </summary>
        public string rutaAnexos { get; set; }
        public ICollection<ProveedorPorAnexo> ProveedorPorAnexo { get; set; }
    }
}
