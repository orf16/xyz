using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SG_SST.Models.Planificacion;
using SG_SST.Models.Empresas;


namespace SG_SST.Models.Aplicacion
{
    [Table("Tbl_BateriaResultado")]
    public class BateriaResultado
    {

        [Key]
        public int Pk_Id_BateriaResultado { get; set; }

        [ForeignKey("BateriaUsuario")]
        public int Fk_Id_BateriaUsuario { get; set; }
        [ForeignKey("Pk_Id_BateriaUsuario")]
        public virtual BateriaUsuario BateriaUsuario { get; set; }

        [ForeignKey("BateriaCuestionario")]
        public int Fk_Id_BateriaCuestionario { get; set; }
        [ForeignKey("Pk_Id_BateriaCuestionario")]
        public virtual BateriaCuestionario BateriaCuestionario { get; set; }

        [DisplayName("Valor")]
        [Required()]
        public int Valor { get; set; }
    }
}
