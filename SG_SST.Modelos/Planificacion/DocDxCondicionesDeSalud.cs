
namespace SG_SST.Models.Planificacion
{
    using SG_SST.Models.Empresas;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    [Table("Tbl_Doc_Dx_Condiciones_De_Salud")]
    public class DocDxCondicionesDeSalud
    {
        /// <summary>
        /// Obtiene y establece la clave primaria de los documentos de diagnostico general de condiciones de salud.
        /// </summary>
        [Key]
        public int Pk_DocDxCondicionesDeSalud { get; set; }

        /// <summary>
        /// Obtiene y establece el nombre del Diagnostico.
        /// </summary>
        public string Nombre_Diagnostico { get; set; }

        /// <summary>
        /// Obtiene y establece el nombre del Documento.
        /// </summary>
        public string Nombre_Documento { get; set; }


        /// <summary>
        /// Obtiene y establece la clave foranea a la tabla  sede.
        /// </summary>
        [ForeignKey("Sede")]
        public int FK_Sede { get; set; }

        /// <summary>
        /// Obtiene y establece un objeto de sede.
        /// </summary>
        [ForeignKey("Pk_Id_Sede")]
        public virtual Sede Sede { get; set; }
    }
}
