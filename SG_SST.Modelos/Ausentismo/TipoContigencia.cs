using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Models.Ausentismo
{
    [Table("Tbl_Tipo_Contigencias")]
    public class TipoContigencia
    {
        [Key]
        public int PK_Id_Tipo_Contigencia { get; set; }
        [StringLength(50)]
        public string Descripcion { get; set; }
    }
}
