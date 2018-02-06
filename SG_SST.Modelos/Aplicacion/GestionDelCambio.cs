using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Models.Aplicacion
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using SG_SST.Models.Planificacion;
    using SG_SST.Models.Empresas;
    using System.Data.Entity.SqlServer;
    using System;

    [Table("Tbl_GestionDelCambio")]
    public class GestionDelCambio
    {
        [Key]
        public int PK_GestionDelCambio { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }
        public string DescripcionDeCambio { get; set; }


        public int FK_Clasificacion_De_Peligro { get; set; }

        public int FK_Tipo_De_Peligro { get; set; }



        public string RequisitoLegal { get; set; }
        public string Recomendaciones { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaEjecucion { get; set; }


        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaSeguimiento { get; set; }

        public string Otro { get; set; }


        /// <summary>
        /// Obtiene y establece una clave foranea a empresa
        /// </summary>
        [ForeignKey("Tbl_Rol")]
        public int FK_Id_Rol { get; set; }

        /// <summary>
        /// Obtiene y establece un objeto de tipo empresa
        /// </summary>
        [ForeignKey("Pk_Id_Rol")]
        public virtual Rol Tbl_Rol { get; set; }


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
