using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Models.Ausentismo
{
    [Table("Tbl_Diagnosticos")]
    public class Diagnostico
    {
        public Diagnostico()
        {
            this.Ausencias = new List<Ausencia>();
        }
        [Key]
        public int PK_Id_Diagnostico { get; set; }
        public string Codigo_CIE { get; set; }
        public string Descripcion { get; set; }        
        public string Capitulo { get; set; }
        public virtual ICollection<Ausencia> Ausencias { get; set; }
    }
}
