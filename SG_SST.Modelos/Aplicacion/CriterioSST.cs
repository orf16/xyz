
namespace SG_SST.Models.Aplicacion
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Data;
    using System.ComponentModel.DataAnnotations;
    using System.Collections;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("Tbl_CriterioSST")]
    public class CriterioSST
    {
        /// <summary>
        /// Obtiene y establece la clave primaria de CriterioSST.
        /// </summary>
        [Key]
        public int PK_CriterioSST { get; set; }

        /// <summary>
        /// Obtiene y establece el nombre del Manual.
        /// </summary>
        public string Criterio { get; set; }

        public ICollection<ProductoPorCriterio> ProductoPorCriterio { get; set; }
    }
}
