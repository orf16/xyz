using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Models.Ausentismo
{
    [Table("Tbl_Contingencias")]
    public class Contingencia
    {
        public Contingencia()
        {
            this.Ausencias = new List<Ausencia>();
        }
        [Key]
        public int PK_Id_Contingencia { get; set; }
        [ForeignKey("TipoContigencia")]
        public int FK_Tipo_Contingencia { get; set; }
        public string Detalle { get; set; }
        public System.DateTime Fecha_Ingreso { get; set; }
        public System.DateTime Fecha_Modificacion { get; set; }

        [ForeignKey("PK_Id_Tipo_Contigencia")]
        public virtual TipoContigencia TipoContigencia { get; set; }
        public virtual ICollection<Ausencia> Ausencias { get; set; }
    }
}
